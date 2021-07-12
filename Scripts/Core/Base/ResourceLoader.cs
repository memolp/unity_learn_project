/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/6
 * Time: 20:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		/// 加载Manifest文件
		/// </summary>
		/// <param name="manifest_path"></param>
		/// <returns></returns>
		public bool LoadBundleManifest()
		{
			if(_manifest != null) return false; //暂不支持加载多个
			_bundleInfos = new Dictionary<string, BundleItemInfo>();
			string manifest_path = Path.Combine(Const.BUNDLE_STORE_PATH, Const.BUNDLE_MAIN_NAME);
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
		/// <returns></returns>
		AssetBundle LoadBundle(string bundle_name)
		{
			//UnityEngine.Debug.Log($"LoadBundle: {bundle_name}");
			// 看对应的Bundle是否已经加载
			AssetBundle bundle = null;
			if(!_cacheBundles.TryGetValue(bundle_name, out bundle))
			{
				bundle = AssetBundle.LoadFromFile(Path.Combine(Const.BUNDLE_STORE_PATH, bundle_name));
				_cacheBundles.Add(bundle_name, bundle);
			}
			return bundle;
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
				AssetBundle dep_bundle = LoadBundle(dep);
			}
			AssetBundle bundle = LoadBundle(bundleInfo.bundle_name);
			if(asset_obj.assetType == AssetsType.SCENE)
			{
				yield return Pri_LoadScene(asset_obj.assetPath, LoadSceneMode.Additive);
			}else
			{
				var obj = bundle.LoadAsset<GameObject>(asset_obj.assetPath);
				asset_obj.ObjData = obj;
				//GameObject.Instantiate(obj, asset_obj.objParent);
				yield return asset_obj;
			}
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