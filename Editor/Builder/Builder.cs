/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/7
 * Time: 10:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using QSmale.Core;

namespace QSmale.Editor
{
	/// <summary>
	/// 打包脚本
	/// </summary>
	public class Builder
	{
		[MenuItem("Tools/BuildApp")]
		static void BuildGame()
		{
			string[] startup_scene = new string[]{"Assets/Scenes/Init.unity"};
			BuildReport result = BuildPipeline.BuildPlayer(startup_scene, Const.BUILD_APP_PATH, BuildTarget.Android, BuildOptions.None);
		}
	}
}