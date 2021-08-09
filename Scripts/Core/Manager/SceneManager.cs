/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/8/9
 * Time: 18:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace QSmale.Core
{
	public class SceneManager : TSingleton<SceneManager>
	{
		public SceneManager(){}
		
		Dictionary<string,SceneDefine> _regScenes= new Dictionary<string, SceneDefine>();
		/// <summary>
		/// 注册当前已写的场景
		/// </summary>
		/// <param name="sceneInfo"></param>
		public void RegisterScene(SceneDefine sceneInfo)
		{
			_regScenes.Add(sceneInfo.sceneName, sceneInfo);
		}
		
		string _current_scene_name = null;
		/// <summary>
		/// 切换场景
		/// </summary>
		/// <param name="name"></param>
		/// <param name="mode"></param>
		public void ChangeScene(string name, LoadSceneMode mode)
		{
			if(name.Equals(_current_scene_name))
			{
				UnityEngine.Debug.LogWarning($"scene {name} is currenct scene");
				return;
			}
			SceneDefine sceneInfo = null;
			if(!_regScenes.TryGetValue(name, out sceneInfo))
			{
				UnityEngine.Debug.LogError($"scene {name} is not exist!");
				return;
			}
			
			SceneAsset asset = new SceneAsset();
			asset.assetPath = sceneInfo.scenePath;
			asset.assetType = AssetsType.SCENE;
			asset.sceneName = sceneInfo.sceneName;
			asset.sceneMode = mode;
			// 创建加载对象，执行加载
			SceneBase scene = Activator.CreateInstance(sceneInfo.sceneType) as SceneBase;
			scene.onLoad(asset);
			_current_scene_name = name;
		}
		/// <summary>
		/// 以附加的方式加载场景
		/// </summary>
		/// <param name="name"></param>
		public void ChangeScene(string name)
		{
			this.ChangeScene(name, LoadSceneMode.Additive);
		}
	}
}