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
	[UIDefine("LoginUI", "Assets/UI/LoginUI.prefab", typeof(LoginUI))]
	public class LoginUI : UIWindow
	{
		public override void onCreate(UIAssets uiInfo)
		{
			base.onCreate(uiInfo);
			Transform btn_guest = gameobject.transform.Find("login_info/btn_guest");
			Button btn =  btn_guest.GetComponent<Button>();
			btn.onClick.AddListener(OnBtnGuestClick);
		}
		
		void OnBtnGuestClick()
		{
			this.Hide();
		}
	}
}
