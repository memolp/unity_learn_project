/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/6
 * Time: 20:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#define ANDROID_GOOGLE_AAB
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if ANDROID_GOOGLE_AAB
using Google.Play.AssetDelivery;
#endif

namespace QSmale.Core
{
	/// <summary>
	/// 资源加载
	/// </summary>
	public class ResouceLoader
	{
		#region 单例模块
		static ResouceLoader _instance = new ResouceLoader();
		/// <summary>
		/// 返回单例对象
		/// </summary>
		/// <returns></returns>
		public static ResouceLoader Instance
		{
			get
			{
				return _instance;
			}
		}
		ResouceLoader(){}
		#endregion
		
		AssetBundleManifest _manifest = null;
		Dictionary<string, BundleItemInfo> _bundleInfos = null;
		/// <summary>
		/// 缓存已加载的Bundle，不可重复加载。
		/// </summary>
		Dictionary<string, AssetBundle> _cacheBundles = new Dictionary<string, AssetBundle>();
		/// <summary>
		/// 缓存正在加载中的Bundle
		/// </summary>
		Dictionary<string, int> _cacheLoadingBundles = new Dictionary<string, int>();
		/// <summary>
		/// 加载Manifest文件
		/// </summary>
		/// <returns></returns>
		public bool LoadBundleManifest()
		{
			if(_manifest != null) return false; //暂不支持加载多个
			_bundleInfos = new Dictionary<string, BundleItemInfo>();
			string manifest_path = Path.Combine(Application.streamingAssetsPath, Const.BUNDLE_MAIN_NAME);
			AssetBundle manifest_bundle = AssetBundle.LoadFromFile(manifest_path);
			BundleManifest manifest = manifest_bundle.LoadAsset<BundleManifest>(Const.BUNDLE_ASSETS_INFO);
			// 将数据缓存起来
			foreach(BundleItemInfo bundle in manifest.bundles)
			{
				foreach(string asset_path in bundle.assets)
				{
					_bundleInfos.Add(asset_path, bundle);
				//	UnityEngine.Debug.Log(string.Format("Asset with Bundle: ${0}$", asset_path));
				}
			}
			return true;
		}
		/// <summary>
		/// 加载指定的Bundle，会先从缓存中查看是否已经加载。
		/// </summary>
		/// <param name="bundle_name"></param>
		/// <param name="asset_obj"></param>
		/// <returns></returns>
		IEnumerator LoadBundleAsync(string bundle_name, AssetObject asset_obj)
		{
			UnityEngine.Debug.Log($"LoadBundle: {bundle_name}");
			AssetBundle bundle = null;
			// 看对应的Bundle是否已经加载
			if(_cacheBundles.TryGetValue(bundle_name, out bundle))
			{
				yield return SetupBundleObject(asset_obj, bundle);;
			}
			else
			{
				#if ANDROID_GOOGLE_AAB
				// Google加载方式
				//string bundle_url = Path.Combine("assetpack", bundle_name);
				PlayAssetBundleRequest request = PlayAssetDelivery.RetrieveAssetBundleAsync(bundle_name);
				while (!request.IsDone) 
				{
					if(request.Status == AssetDeliveryStatus.WaitingForWifi)
					{
						var userConfirmationOperation = PlayAssetDelivery.ShowCellularDataConfirmation();
						yield return userConfirmationOperation;
						if((userConfirmationOperation.Error != AssetDeliveryErrorCode.NoError) ||
						   (userConfirmationOperation.GetResult() != ConfirmationDialogResult.Accepted))
						{
							yield return null;
						}
						yield return new WaitUntil(()=>request.Status != AssetDeliveryStatus.WaitingForWifi);
					}
					yield return null;
				}
				if(request.Error != AssetDeliveryErrorCode.NoError)
				{
					yield return null;
				}
				bundle = request.AssetBundle;
				#else
				// 普通的Bundle加载方式
				var req = AssetBundle.LoadFromFileAsync(Path.Combine(Const.BUNDLE_STORE_PATH, bundle_name));
				yield return req;
				bundle = req.assetBundle;
				#endif
				if(bundle != null)
				{
					_cacheLoadingBundles.Remove(bundle_name); //从加载中移除
					_cacheBundles.Add(bundle_name, bundle);  // 添加到已加载列表
					yield return SetupBundleObject(asset_obj, bundle);
				}
			}
			yield return null;
		}
		/// <summary>
		/// 装载Bundle对象
		/// </summary>
		/// <param name="asset_obj"></param>
		/// <param name="bundle"></param>
		/// <returns></returns>
		IEnumerator SetupBundleObject(AssetObject asset_obj, AssetBundle bundle)
		{
			if(asset_obj != null)
			{
				if(asset_obj.assetType == AssetsType.SCENE)
				{
					yield return Pri_LoadScene(asset_obj.assetPath, LoadSceneMode.Additive);
				}else
				{
					var obj = bundle.LoadAsset<GameObject>(asset_obj.assetPath);
					asset_obj.onLoadEnd(obj);
					yield return null;
				}
			}
			yield return null;
		}
		/// <summary>
		/// 从Bundle加载资源，会自动加载依赖相关的Bundle
		/// </summary>
		/// <param name="asset_obj"></param>
		/// <returns></returns>
		IEnumerator LoadAssetFromBundle(AssetObject asset_obj)
		{
			// 先根据资源路径找bundle
			UnityEngine.Debug.Log(string.Format("load Asset with Bundle: {0}", asset_obj.assetPath));
			// 资源正在加载中
			if(_cacheLoadingBundles.ContainsKey(asset_obj.assetPath))
			{
				yield return null;
			}
			// 先根据资源路径找bundle
			BundleItemInfo bundleInfo = null;
			// 先获取Bundle相关的信息
			string asset_path = asset_obj.assetPath.ToLower(); // 记录里面全部用的是小写（后续可以改）
			if(!_bundleInfos.TryGetValue(asset_path, out bundleInfo))
			{
				UnityEngine.Debug.LogError(string.Format("${0}$ asset is not exist!", asset_path));
				yield return null;
			}
			//先加载依赖的Bundle
			foreach(string dep in bundleInfo.depends)
			{
				yield return LoadBundleAsync(dep, null);
			}
			yield return LoadBundleAsync(bundleInfo.bundle_name, asset_obj);
		}
		
		/// <summary>
		/// 加载资源
		/// </summary>
		/// <param name="asset_obj"></param>
		/// <returns></returns>
		public IEnumerator LoadAsset(AssetObject asset_obj)
		{
			if(GlobalAttributes.Instance.bundle_mode)
			{
				yield return LoadAssetFromBundle(asset_obj);
			}else
			{
#if UNITY_EDITOR
				if(asset_obj.assetType == AssetsType.SCENE)
				{
					yield return Pri_LoadScene(asset_obj.assetPath, LoadSceneMode.Additive);
				}else
				{
					var obj = UnityEditor.AssetDatabase.LoadAssetAtPath(asset_obj.assetPath, asset_obj.objType);
					asset_obj.ObjData = obj;
					//GameObject.Instantiate(obj, asset_obj.objParent);
					yield return asset_obj;
				}
#else
				UnityEngine.Debug.LogError(string.Format("LoadAsset: {0} not support!"));
#endif
			}
			yield return null;
		}
		/// <summary>
		/// 加载场景
		/// </summary>
		/// <param name="scene_path">路径，Assets/xxx</param>
		/// <param name="mode">模式</param>
		/// <returns></returns>
		IEnumerator Pri_LoadScene(string scene_path, LoadSceneMode mode)
		{
			Scene cur_scene = SceneManager.GetActiveScene();
			AsyncOperation load_async = SceneManager.LoadSceneAsync(scene_path, mode);
			load_async.allowSceneActivation = true;
			yield return load_async;
			AsyncOperation unload_async = SceneManager.UnloadSceneAsync(cur_scene);
			yield return unload_async;
		}
	}
}