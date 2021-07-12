/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/7
 * Time: 14:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System.Collections;
using UnityEngine;

namespace QSmale.Core
{
	/// <summary>
	/// 全局对象
	/// </summary>
	public static class Global
	{
		private static MonoBehaviour _g_obj;
		/// <summary>
		/// 全局初始化
		/// </summary>
		/// <param name="obj"></param>
		public static void Init(MonoBehaviour obj)
		{
			_g_obj = obj;
		}
		/// <summary>
		/// 全局的协程对象
		/// </summary>
		/// <param name="iter"></param>
		/// <returns></returns>
		public static Coroutine StartCoroutine(IEnumerator iter)
		{
			return _g_obj.StartCoroutine(iter);
		}
		/// <summary>
		/// 全局的协程对象
		/// </summary>
		/// <param name="methodName"></param>
		/// <returns></returns>
		public static Coroutine StartCoroutine(string methodName)
		{
			return _g_obj.StartCoroutine(methodName);
		}
		/// <summary>
		/// 全局的协程对象
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Coroutine StartCoroutine(string methodName, object value)
		{
			return _g_obj.StartCoroutine(methodName, value);
		}

	}
}
