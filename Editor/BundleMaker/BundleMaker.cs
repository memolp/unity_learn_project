using System.IO;
using UnityEditor;
using UnityEngine;
using QSmale.Core;
using System.Collections.Generic;

namespace QSmale.Editor
{
	/// <summary>
	/// 打Bundle脚本
	/// </summary>
	public class BundleMaker
	{
		/// <summary>
		/// 打包Bundle, Bundle来自Editor设置
		/// </summary>
		[MenuItem("Tools/BuildBundleDefault")]
		static void BuildAssetBundle()
		{
			// 创建目录
			if(!Directory.Exists(Const.BUNDLE_STORE_PATH))
			{
				Directory.CreateDirectory(Const.BUNDLE_STORE_PATH);
			}
			string bundle_assets = Path.Combine(Const.BUNDLE_STORE_PATH);
			AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(bundle_assets, BuildAssetBundleOptions.None, BuildTarget.Android);
			if(manifest == null)
			{
				UnityEngine.Debug.LogError("打包Bundle失败!");
				return;
			}
			UnityEngine.Debug.Log("打包Bundle完成");
			BundleMaker.ExportBundleManifest(manifest);
			UnityEngine.Debug.Log("写Bundle完成");
		}
		
		[MenuItem("Tools/BuildBundleCollect")]
		static void BuildAssetBundleWithCollect()
		{
			
		}
		/// <summary>
		/// 导出Bundle的关系图，方便代码读取
		/// </summary>
		/// <param name="manifest"></param>
		static void ExportBundleManifest(AssetBundleManifest manifest)
		{
			BundleManifest bundleInfo = new BundleManifest();
			string[] bundle_names = manifest.GetAllAssetBundles();
			BundleItemInfo[] bundles = new BundleItemInfo[bundle_names.Length];
			for(int i=0; i<bundle_names.Length; i++)
			{
				string bundle_name = bundle_names[i];
				string full_path = Path.Combine(Const.BUNDLE_STORE_PATH, bundle_name);
				if(!File.Exists(full_path))
				{
					UnityEngine.Debug.LogError(string.Format("Bundle {0} 路径不存在", full_path));
					continue;
				}
				// 加载指定的Bundle，然后获取其资源列表
				AssetBundle asset_bundle = AssetBundle.LoadFromFile(full_path);
				// 创建关联数据
				BundleItemInfo bundle = new BundleItemInfo();
				bundle.bundle_name = bundle_name;
				bundle.size = (int)FileUtils.calc_size(full_path);
				bundle.md5 = FileUtils.calc_md5(full_path);
				if(bundle_name.IndexOf("scene") >= 0)
				{
					bundle.assets = asset_bundle.GetAllScenePaths(); // 这个获取的资源路径名称是按照实际的大小写
				}else
				{
					bundle.assets = asset_bundle.GetAllAssetNames(); // 这个获取的资源路径名称全是小写
				}
				/*List<string> depends = new List<string>();
				foreach(string asset_name in bundle.assets)
				{
					depends.AddRange(manifest.GetAllDependencies(asset_name));
				}*/
				bundle.depends = manifest.GetAllDependencies(bundle_name);
				bundles[i] = bundle;
				// 立即卸载
				asset_bundle.Unload(false);
			}
			bundleInfo.bundle_version = 1;
			bundleInfo.bundles = bundles;
			// 创建资源
			AssetDatabase.CreateAsset(bundleInfo, Const.BUNDLE_ASSETS_INFO);
			AssetDatabase.Refresh();
			
			//string main_bundle = Path.Combine(Const.BUNDLE_STORE_PATH, Const.BUNDLE_MAIN_NAME);
			AssetBundleBuild main_bundle = new AssetBundleBuild();
			main_bundle.assetBundleName = Const.BUNDLE_MAIN_NAME;
			main_bundle.assetNames = new string[] {Const.BUNDLE_ASSETS_INFO};
			
			//var result = BuildPipeline.BuildAssetBundle(null,new UnityEngine.Object[1]{bundleInfo}, main_bundle, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.Android);
			var result = BuildPipeline.BuildAssetBundles(Const.BUNDLE_STORE_PATH, new AssetBundleBuild[1]{main_bundle}, BuildAssetBundleOptions.None, BuildTarget.Android);
			if(!result)
			{
				UnityEngine.Debug.LogError("生成MainBundle失败");
			}
		}
	}
}