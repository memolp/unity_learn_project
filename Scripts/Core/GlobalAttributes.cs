/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/8
 * Time: 10:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using UnityEngine;

namespace QSmale.Core
{
	public class GlobalAttributes
	{
		static GlobalAttributes _instance = null;
		/// <summary>
		/// 单例类
		/// </summary>
		public static GlobalAttributes Instance
		{
			get
			{
				if(_instance == null)
					_instance = new GlobalAttributes();
				return _instance;
			}
		}
		/// <summary>
		/// 单例类
		/// </summary>
		GlobalAttributes(){}
		/// <summary>
		/// 是否使用Bundle模式运行
		/// </summary>
		public bool bundle_mode = false;	
	}
}