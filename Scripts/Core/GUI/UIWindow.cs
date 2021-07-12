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
		public GameObject gameobject{set;get;}
		/// <summary>
		/// 保留定义
		/// </summary>
		public static Type UIWindowType = typeof(UIWindow);
		/// <summary>
		/// UI创建时执行
		/// </summary>
		public virtual void onCreate(UIAssets uiInfo)
		{
			//Debug.Log($"UIWindow onCreate, url:{uiInfo.assetPath}");
			Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(uiInfo));
			gameobject = (GameObject)UnityEngine.Object.Instantiate(uiInfo.ObjData, uiInfo.objParent);
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