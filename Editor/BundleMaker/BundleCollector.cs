/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/8/6
 * Time: 9:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QSmale.Core;

namespace QSmale.Editor
{
	public class BundleCollector
	{
		/**忽略后缀名*/
		public static string IGNORE_EXT = ".meta";
		public static string ASSETS_PATH = Path.Combine(Application.dataPath,"ArtData");
		
		private List<AssetBundleBuild> m_abBuilds;
		public BundleCollector()
		{
			m_abBuilds = new List<AssetBundleBuild>();
		}
		/** 收集资源 */
		public void StartCollect()
		{
			m_abBuilds.Clear();
			List<string> files = new List<string>();
			this.GetFilesFromDirectory(ASSETS_PATH, ref files);
			foreach (string path in files) 
			{
				if(path.EndsWith("Init.unity"))
				{
					continue;
				}
				string md5 = FileUtils.calc_md5(path);
				string asset_bundle_name = "K_" + md5;
				AssetBundleBuild ab = new AssetBundleBuild();
				ab.assetBundleName = asset_bundle_name;
				ab.assetNames = new string[]{path};
				m_abBuilds.Add(ab);
			}
		}
		/** 获取收集的资源列表 */
		public AssetBundleBuild[] get_builds()
		{
			return m_abBuilds.ToArray();
		}
		/** 获取目录下的全部文件，包含子文件夹 */
		private void GetFilesFromDirectory(string dirPath, ref List<string> files)
		{
			// 遍历目录下面的文件
			foreach (string path in Directory.EnumerateFiles(dirPath))
			{
				string ext = Path.GetExtension(path);
				if(ext.Equals(IGNORE_EXT))
				{
					continue;
				}
				files.Add(path.Replace(Application.dataPath, "Assets").Replace("\\", "/"));
			}
			// 遍历目录下的其他目录
			foreach (string path in Directory.GetDirectories(dirPath))
			{
				this.GetFilesFromDirectory(path, ref files);
			}
		}
	}
}