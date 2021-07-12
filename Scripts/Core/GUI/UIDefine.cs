/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/9
 * Time: 10:36
 * 
 * UIDefine，主要是为了方便管理界面
 * 1. 使用编辑器制作UI界面，生产prefab
 * 2. 写对应的界面类，继承自UIWindow，并设置prefab路径
 * 3. 通过UIManager进行显示和关闭界面
 */
using System;
using System.Collections.Generic;

namespace QSmale.Core
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false,Inherited=false)]
	public class UIDefine : Attribute
	{
		public static Type UIDefineType = typeof(UIDefine);
		/// <summary>
		/// UI界面名称（唯一）
		/// </summary>
		public string uiName{get;set;}
		/// <summary>
		/// UI界面的Prefab
		/// </summary>
		public string uiPrefab{get;set;}
		/// <summary>
		/// UI界面所在的层
		/// </summary>
		public UILayer uiLayer{get;set;}
		/// <summary>
		/// UI界面类
		/// </summary>
		public Type uiType{get; set;}
		/// <summary>
		/// 设置UI的prefab路径
		/// </summary>
		/// <param name="name"></param>
		/// <param name="prefab"></param>
		/// <param name="type"></param>
		public UIDefine(string name, string prefab)
		{
			this.uiName = name;
			this.uiPrefab = prefab;
			this.uiLayer = UILayer.UI_NORMAL;
		}
		/// <summary>
		/// 设置UI的prefab路径，并指定放置的层级
		/// </summary>
		/// <param name="name"></param>
		/// <param name="prefab"></param>
		/// <param name="type"></param>
		/// <param name="layer"></param>
		public UIDefine(string name, string prefab, UILayer layer)
		{
			this.uiName = name;
			this.uiPrefab = prefab;
			this.uiLayer = layer;
		}
		
	}
}