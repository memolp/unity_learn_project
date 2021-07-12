/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/6
 * Time: 19:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using UnityEngine;
 
namespace QSmale.Core
{
	public static class Const
	{
		public static string BUNDLE_STORE_PATH = Application.streamingAssetsPath;
		public static string BUNDLE_MAIN_NAME = "assets_info.ab";
		public static string BUNDLE_ASSETS_INFO = "Assets/Config/android_bundle_info.asset";
		public static string BUILD_APP_PATH = "GTest.apk";
		/// <summary>
		/// UIRoot.prefab 在Resources目录下面。
		/// </summary>
		public static string UI_SYSTEM_PREFAB = "UIRoot"; 
	}
}
