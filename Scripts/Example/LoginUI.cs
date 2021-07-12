/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/9
 * Time: 14:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UnityEngine;
using UnityEngine.UI;
using QSmale.Core;

namespace QSmale.Test
{
	[UIDefine("LoginUI", "Assets/UI/LoginUI.prefab")]
	public class LoginUI : UIWindow
	{
		public override void onCreate(UIAssets uiInfo)
		{
			base.onCreate(uiInfo);
			Transform btn_guest = gameobject.transform.Find("login_info/btn_guest");
			Button btn =  btn_guest.GetComponent<Button>();
			btn.onClick.AddListener(OnBtnGuestClick);
			
			Transform btn_google = gameobject.transform.Find("login_info/btn_google");
			Button btn1 = btn_google.GetComponent<Button>();
			btn1.onClick.AddListener(OnBtnGoogleClick);
		}
		
		void OnBtnGuestClick()
		{
			this.Hide();
		}
		
		void OnBtnGoogleClick()
		{
			AssetObject scene = new AssetObject();
	    	scene.assetPath = "Assets/Scenes/Battle_1000.unity";
	    	scene.assetType = AssetsType.SCENE;
	    	Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(scene));
	    	this.Hide();
	 
		}
	}
}
