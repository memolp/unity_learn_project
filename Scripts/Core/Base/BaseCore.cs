/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/12
 * Time: 16:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QSmale.Core
{
	/// <summary>
	/// 单例类模板
	/// </summary>
	public class TSingleton<T> where T:class, new()
	{
		private static T _instance;
		/// <summary>
		/// 获取单例对象
		/// </summary>
		public static T Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = Activator.CreateInstance(typeof(T)) as T;
				}
				return _instance;
			}
		}
	}
	
}