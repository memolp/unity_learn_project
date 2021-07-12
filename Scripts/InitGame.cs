/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/7
 * Time: 14:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using UnityEngine;

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
		}
		
		void Start()
		{
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