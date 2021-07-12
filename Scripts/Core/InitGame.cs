/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/7
 * Time: 14:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System;
using UnityEngine;
using System.Reflection;

namespace QSmale.Core
{
	/// <summary>
	/// 将此代码挂到初始的场景上面即可。
	/// 所有代码的入口在GameMain中的Main开始。
	/// </summary>
	public class InitGame : MonoBehaviour
	{
		
		void Awake()
		{
			//设置为不准销毁
			DontDestroyOnLoad(this);
			//初始化全局
			Global.Init(this);
			UnityEngine.Debug.Log("[Assembly] Begin load Assembly");
			// 加载全部的类，获取对应的注册类
			//GetCallingAssembly  调用当前方法的方法所在的程序集
			//GetExecutingAssembly 当前方法所在的程序集
			Assembly assmebly = Assembly.GetExecutingAssembly();
			// 遍历全部的类，进行注册
			foreach(Type type in assmebly.GetTypes())
			{
				//UnityEngine.Debug.Log(string.Format("[Assembly] cls: {0}", type.ToString()));
				// 只针对实现接口的类做处理
				if(type.GetInterface("ISystemCore") != null)
				{
					SystemDefine sysInfo = Attribute.GetCustomAttribute(type, SystemDefine.SystemDefineType) as SystemDefine;
					if(sysInfo != null)
					{
						sysInfo.sysType = type;
						SystemManager.Instance.RegisterSystem(sysInfo);
					}
					continue;
				}
				// 只针对实现接口的类做处理
				if(type.GetInterface("IUICore") != null)
				{
					UIDefine uiInfo = Attribute.GetCustomAttribute(type, UIDefine.UIDefineType) as UIDefine;
					if(uiInfo != null)
					{
						uiInfo.uiType = type;
						UIManager.Instance.RegisterUIWindows(uiInfo);
					}
					continue;
				}
			}
			UnityEngine.Debug.Log("[Assembly] End load Assembly");
		}
		
		void Start()
		{
			// 加载Bundle资源
			if(!ResouceLoader.Instance.LoadBundleManifest())
			{
				UnityEngine.Debug.LogError("LoadBundleManifest error");
				return;
			}
			// 初始化UI布局
			UIManager.Instance.InitUILayout();
			// 初始化所有系统
			SystemManager.Instance.OnInitSystem();
			// 进入主逻辑
			GameMain.Main();
		}
		
		void Update()
		{
			
		}
		
		void Destory()
		{
			UnityEngine.Debug.Log("Destory.......");
		}
	}
}