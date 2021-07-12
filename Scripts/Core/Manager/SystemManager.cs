/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/12
 * Time: 11:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Reflection;
using System.Collections.Generic;

namespace QSmale.Core
{
	/// <summary>
	/// 系统管理器
	/// </summary>
	public class SystemManager: TSingleton<SystemManager>
	{
		public SystemManager(){}
		
		List<SystemDefine> _systemInfoList = new List<SystemDefine>();
		/// <summary>
		/// 注册系统
		/// </summary>
		/// <param name="type"></param>
		/// <param name="sysInfo"></param>
		public void RegisterSystem(SystemDefine sysInfo)
		{
			_systemInfoList.Add(sysInfo);  //插入表
		}
		/// <summary>
		/// 初始化系统
		/// </summary>
		public void OnInitSystem()
		{
			// 先排序
			_systemInfoList.Sort((a,b)=>{ return (a.sysOrder < b.sysOrder) ? -1:1;});
			// 便利创建并初始化
			foreach(SystemDefine sysInfo in _systemInfoList)
			{
				SystemBase sysObj = (SystemBase)Activator.CreateInstance(sysInfo.sysType);
				sysObj.OnInitSystem();
			}
		}
		
		
	}
}