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
using System.Collections.Generic;

namespace QSmale.Core
{
	public class UIManager
	{
		#region 静态实例
		static UIManager _instance = null;
		public static UIManager Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new UIManager();
				}
				return _instance;
			}
		}
		UIManager() {}
		#endregion
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
			ui_root = GameObject.Instantiate(obj);
			ui_root.name = Const.UI_SYSTEM_PREFAB;
			// 设置为不能销毁
			GameObject.DontDestroyOnLoad(ui_root);
			
			// 创建UI根面板
			Transform ui_root_canvas = ui_root.transform.Find("Canvas");
			int max_layer = (int)UILayer.UI_LAYER_MAX;
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
		Dictionary<string,UIDefine> _regUIData = new Dictionary<string, UIDefine>();
		/// <summary>
		/// 通过UI名称获取UI对象
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public UIDefine GetUI(string name)
		{
			UIDefine type = null;
			if(_regUIData.TryGetValue(name, out type))
			{
				return type;
			}
			return null;
		}
		/// <summary>
		/// 外部调用加载全部的的Assembly
		/// </summary>
		/// <param name="type"></param>
		public void InitLoadWithAssembly(Type type)
		{
			if(!type.IsSubclassOf(UIWindow.UIWindowType))
			{
				return;
			}
			
			foreach(System.Object obj in type.GetCustomAttributes(false))
			{
				if(obj.GetType() == typeof(UIDefine))
				{
					UIDefine uiInfo = (UIDefine) obj;
					_regUIData.Add(uiInfo.uiName, uiInfo);
					return;
				}
			}
			                    
		}
		Dictionary<string,UIAssets> ui_panels = new Dictionary<string, UIAssets>();
		public void ShowWindow(string uiName)
		{
			UIDefine uiInfo = GetUI(uiName);
			if(uiInfo == null)
			{
				UnityEngine.Debug.LogError($"UIName: {uiName} is not exist!");
				return;
			}
			Type windowType = uiInfo.uiType;
			UIAssets uiasset = new UIAssets();
			uiasset.assetPath = uiInfo.uiPrefab;
			uiasset.assetType = AssetsType.UI;
			uiasset.objType = typeof(GameObject);
			uiasset.objParent = ui_layers[uiInfo.uiLayer].transform;
			
			UIWindow window = (UIWindow)Activator.CreateInstance(windowType);
			window.onCreate(uiasset);
			window.Show();
			
			//this.OpenPanel(uiInfo.uiPrefab, uiInfo.uiLayer);
		}
		/// <summary>
		/// 打开面板
		/// </summary>
		/// <param name="panel_path"></param>
		/// <param name="layer"></param>
		public void OpenPanel(string panel_path, UILayer layer)
		{
			UIAssets ui_panel = null;
			if(!ui_panels.TryGetValue(panel_path, out ui_panel))
			{
				ui_panel = new UIAssets();
				ui_panel.assetPath = panel_path;
				ui_panel.assetType = AssetsType.UI;
				ui_panel.objType = typeof(GameObject);
				ui_panel.objParent = ui_layers[layer].transform;
				Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(ui_panel));
				ui_panels[panel_path] = ui_panel;
			}else
			{
				GameObject obj = (GameObject)ui_panel.ObjData;
				obj.SetActive(true);
			}
		}
		/// <summary>
		/// 关闭面板
		/// </summary>
		/// <param name="panel_path"></param>
		public void ClosePanel(string panel_path)
		{
			GameObject obj = (GameObject)ui_panels[panel_path].ObjData;
			obj.SetActive(false);
		}
		/// <summary>
		/// 关闭全部面板
		/// </summary>
		public void CloseAllPanel()
		{
			foreach(UIAssets ui_panel in ui_panels.Values)
			{
				GameObject obj = (GameObject)ui_panel.ObjData;
				if(obj.activeSelf)
				{
					obj.SetActive(false);
				}
			}
		}
	}
}