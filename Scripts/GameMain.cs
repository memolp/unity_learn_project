#define USE_ASSEMBLY_REG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QSmale.Core
{
	
	public class GameMain
	{
		public static void Main()
	    {
			if(!ResouceLoader.Instance.LoadBundleManifest())
			{
				UnityEngine.Debug.LogError("LoadBundleManifest error");
				return;
			}
			
			UIManager.Instance.InitUILayout();
			#if USE_ASSEMBLY_REG
			Assembly assmebly = Assembly.GetCallingAssembly();
			foreach(Type type in assmebly.GetTypes())
			{
				UIManager.Instance.InitLoadWithAssembly(type);
			}
			#endif

			SceneManager.sceneLoaded += OnSceneLoadEnd;
			
			UnityEngine.Debug.Log("START.......");
	    	AssetObject scene = new AssetObject();
	    	scene.assetPath = "Assets/Scenes/Login.unity";
	    	scene.assetType = AssetsType.SCENE;
	    	Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(scene));
	    	
	    }
		
		static void OnSceneLoadEnd(Scene scene, LoadSceneMode mode)
		{
			GameObject obj = GameObject.Find("SceneRoot");
	    	if(obj  != null)
	    	{
	    		AssetObject sc_p = new AssetObject();
		    	sc_p.assetPath = "Assets/Prefabs/scene_ttt.prefab";
		    	sc_p.objType = typeof(UnityEngine.GameObject);
		    	sc_p.assetType = AssetsType.PREFAB;
		    	sc_p.objParent = obj.transform;
		    	Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(sc_p));
		    	//GameObject go = (GameObject)GameObject.Instantiate(sc_p.ObjData, obj.transform);
		    	
		    	//UIManager.Instance.OpenPanel("Assets/UI/LoginUI.prefab", UILayer.UI_NORMAL);
		    	UIManager.Instance.ShowWindow("LoginUI");
	    	}
		}
		                               
	}
}