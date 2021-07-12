/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/12
 * Time: 11:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QSmale.Core
{
	public interface ISystemCore
	{
		
	}
	/// <summary>
	/// 管理器基类
	/// </summary>
	public class SystemBase : ISystemCore
	{
		/// <summary>
		/// 方便外部获取
		/// </summary>
		public static Type SystemBaseType = typeof(SystemBase);
		/// <summary>
		/// 初始化管理器
		/// </summary>
		public void OnInitSystem()
		{
			
		}
	}
}