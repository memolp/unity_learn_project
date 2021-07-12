/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/12
 * Time: 11:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace QSmale.Core
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false,Inherited=false)]
	public class SystemDefine : Attribute
	{
		public static Type SystemDefineType = typeof(SystemDefine);
		/// <summary>
		/// 系统的名称
		/// </summary>
		public string sysName {set;get;}
		/// <summary>
		/// 是否是单例类
		/// </summary>
		public bool isSingleton {set;get;}
		/// <summary>
		/// 系统编号，越小的越在前面初始化
		/// </summary>
		public int sysOrder {set; get;}
		/// <summary>
		/// 系统的类
		/// </summary>
		public Type sysType {set; get;}
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="name"></param>
		/// <param name="singleton"></param>
		/// <param name="order"></param>
		public SystemDefine(string name, bool singleton, int order)
		{
			this.sysName = name;
			this.isSingleton = singleton;
			this.sysOrder = order;
		}
	}
}