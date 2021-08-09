#define USE_ASSEMBLY_REG

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QSmale.Core
{
	
	public class GameMain
	{
		public static void Main()
	    {
			UnityEngine.Debug.Log("START.......");
			// 原有场景加载的方式
			/*SceneManager.sceneLoaded += OnSceneLoadEnd;
	    	AssetObject scene = new AssetObject();
	    	scene.assetPath = "Assets/ArtData/Scenes/Login.unity";
	    	scene.assetType = AssetsType.SCENE;
	    	Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(scene));*/
			// 改成了新的场景加载模式，这样隐藏了很多内容。
	    	SceneManager.Instance.ChangeScene("LoginScene");
	    	
	    }
		/*
		static void OnSceneLoadEnd(Scene scene, LoadSceneMode mode)
		{
			GameObject obj = GameObject.Find("SceneRoot");
	    	if(obj  != null)
	    	{
	    		AssetObject sc_p = new AssetObject();
		    	sc_p.assetPath = "Assets/ArtData/Prefabs/scene_ttt.prefab";
		    	sc_p.objType = typeof(UnityEngine.GameObject);
		    	sc_p.assetType = AssetsType.PREFAB;
		    	sc_p.objParent = obj.transform;
		    	Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(sc_p));
		    	
		    	if(scene.name.IndexOf("Login") >=0)
		    		UIManager.Instance.ShowWindow("LoginUI");
	    	}
		}*/
		                               
	}
}