/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/6
 * Time: 20:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using UnityEngine;
 
namespace QSmale.Core
{
	/// <summary>
	/// Bundle对象
	/// </summary>
	[Serializable]
	public class BundleItem
	{
		/// <summary>
		/// Bundle的名称
		/// </summary>
		[SerializeField]
		public string bundle_name;
		/// <summary>
		/// Bundle大小
		/// </summary>
		[SerializeField]
		public int size;
		/// <summary>
		/// Bundle的MD5值
		/// </summary>
		[SerializeField]
		public string md5;
	}
	/// <summary>
	/// Bundle对象，更详细的描述，包含资源列表和依赖列表
	/// </summary>
	[Serializable]
	public class BundleItemInfo: BundleItem
	{
		/// <summary>
		/// 资源列表，这个Bundle包含的资源列表
		/// </summary>
		[SerializeField]
		public string[] assets;
		/// <summary>
		/// 资源的依赖列表,每个资源对应一个string[]列表 
		/// </summary>
		[SerializeField]
		public string[] depends;
	}
	
	public class BundleManifest: ScriptableObject
	{
		public uint bundle_version;
		public BundleItemInfo[] bundles;
	}
}
