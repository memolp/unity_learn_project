/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/9
 * Time: 10:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UnityEngine;

namespace QSmale.Core
{
	/// <summary>
	/// 接口类
	/// </summary>
	public interface IUICore
	{
		
	}
	
	public class UIWindow : IUICore
	{
		/// <summary>
		/// UI资源prefab
		/// </summary>
		public GameObject gameobject{set;get;}
		/// <summary>
		/// 保留定义
		/// </summary>
		public static Type UIWindowType = typeof(UIWindow);
		/// <summary>
		/// UI数据，方便扩展UI数据填充
		/// </summary>
		public System.Object uiData{set;get;}
		/// <summary>
		/// UI加载资源
		/// </summary>
		/// <param name="uiInfo"></param>
		public virtual void onLoad(UIAssets uiInfo)
		{
			//Debug.Log($"UIWindow onLoad, url:{uiInfo.assetPath}");
			uiInfo.uiWindow = this;
			Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(uiInfo));
		}
		/// <summary>
		/// UI加载资源完成
		/// </summary>
		public virtual void onLoadEnd(UIAssets uiInfo)
		{
			// 实例化
			gameobject = (GameObject)UnityEngine.Object.Instantiate(uiInfo.ObjData, uiInfo.objParent);
			// 调用创建方法
			this.onCreate();
			// 调用显示
			this.Show();
		}
		/// <summary>
		/// UI创建时执行
		/// </summary>
		public virtual void onCreate()
		{
			
			
		}
		/// <summary>
		/// UI销毁时执行
		/// </summary>
		public virtual void onDestory()
		{
			
		}
		/// <summary>
		/// 显示UI
		/// </summary>
		public virtual void Show()
		{
			if(!gameobject.activeSelf)
			{
				gameobject.SetActive(true);
			}
		}
		/// <summary>
		/// 隐藏
		/// </summary>
		public virtual void Hide()
		{
			if(gameobject.activeSelf)
			{
				gameobject.SetActive(false);
			}
		}
	}
}