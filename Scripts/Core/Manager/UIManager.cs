/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/8
 * Time: 17:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace QSmale.Core
{
	/// <summary>
	/// UI管理器
	/// </summary>
	public class UIManager : TSingleton<UIManager>
	{
		public UIManager() {}
		/// <summary>
		/// UI的根节点
		/// </summary>
		private GameObject ui_root = null;
		/// <summary>
		/// UI层级
		/// </summary>
		private Dictionary<UILayer, GameObject> ui_layers = new Dictionary<UILayer, GameObject>();
		/// <summary>
		/// 初始化UI布局，后面的UI都将挂在这个布局上面
		/// </summary>
		public void InitUILayout()
		{
			//从Resources里面加载UI节点
			var obj = (GameObject)Resources.Load<GameObject>(Const.UI_SYSTEM_PREFAB);
			ui_root = UnityEngine.Object.Instantiate(obj);
			ui_root.name = Const.UI_SYSTEM_PREFAB;
			// 设置为不能销毁
			UnityEngine.Object.DontDestroyOnLoad(ui_root);
			
			// 创建UI根面板
			Transform ui_root_canvas = ui_root.transform.Find("Canvas");
			const int max_layer = (int)UILayer.UI_LAYER_MAX;
			for(int i=1; i<max_layer; i++)
			{
				GameObject layer = new GameObject(string.Format("Layer_{0}", i));
				layer.transform.parent = ui_root_canvas;
				layer.transform.localPosition = Vector3.zero;
				layer.transform.localScale = Vector3.one;
				this.ui_layers.Add((UILayer)i, layer);
			}
		}
		/// <summary>
		/// 记录当前已注册的UI界面信息
		/// </summary>
		Dictionary<string,UIDefine> _regUIWindows= new Dictionary<string, UIDefine>();
		/// <summary>
		/// 通过UI名称获取UI对象
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public UIDefine GetUI(string name)
		{
			UIDefine type = null;
			if(_regUIWindows.TryGetValue(name, out type))
			{
				return type;
			}
			return null;
		}
		/// <summary>
		/// 注册窗口界面
		/// </summary>
		/// <param name="uiInfo"></param>
		public void RegisterUIWindows(UIDefine uiInfo)
		{
			//UnityEngine.Debug.Log($"Add UI {uiInfo.uiName} {uiInfo.uiPrefab}");
			_regUIWindows.Add(uiInfo.uiName, uiInfo);           
		}
		/// <summary>
		/// 记录当前已创建的UI界面
		/// </summary>
		Dictionary<string,UIWindow> _uiWindows = new Dictionary<string, UIWindow>();
		/// <summary>
		/// 显示窗口
		/// </summary>
		/// <param name="uiName"></param>
		public void ShowWindow(string uiName)
		{
			ShowWindow(uiName, null);
		}
		/// <summary>
		/// 显示窗口，携带数据
		/// </summary>
		/// <param name="uiName"></param>
		/// <param name="data"></param>
		public void ShowWindow(string uiName, System.Object data)
		{
			UIWindow window = null;
			// 先看是否已经创建过
			if(!_uiWindows.TryGetValue(uiName, out window))
			{
				UIDefine uiInfo = GetUI(uiName);
				if(uiInfo == null)
				{
					UnityEngine.Debug.LogError($"UIName: {uiName} is not exist!");
					return;
				}
				UIAssets uiasset = new UIAssets();
				uiasset.assetPath = uiInfo.uiPrefab;
				uiasset.assetType = AssetsType.PREFAB;
				uiasset.objType = typeof(GameObject);
				uiasset.objParent = ui_layers[uiInfo.uiLayer].transform;
				
				window = (UIWindow)Activator.CreateInstance(uiInfo.uiType);
				window.onCreate(uiasset);
			}
			window.updateData(data);
			window.Show();
		}
		/// <summary>
		/// 关闭界面
		/// </summary>
		/// <param name="uiName"></param>
		public void CloseWindow(string uiName)
		{
			UIWindow window = null;
			// 先看是否已经创建过
			if(_uiWindows.TryGetValue(uiName, out window))
			{
				window.Hide();
			}
		}
		/// <summary>
		/// 关闭全部面板
		/// </summary>
		public void CloseAllWindows()
		{
			foreach(UIWindow window in _uiWindows.Values)
			{
				window.Hide();
			}
		}
	}
}