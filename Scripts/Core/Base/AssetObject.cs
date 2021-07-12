/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/7
 * Time: 11:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace QSmale.Core
{
	public class AssetObject
	{
		UnityEngine.Object obj_ = null;
		/// <summary>
		/// 资源对象
		/// </summary>
		public UnityEngine.Object ObjData
		{
			get
			{
				return obj_;
			}
			set
			{
				obj_ = value;
			}
		}
		
		string path_ = string.Empty;
		/// <summary>
		/// 资源路径
		/// </summary>
		public string assetPath
		{
			get
			{
				return path_;
			}
			set
			{
				path_ = value;
			}
		}
		Type obj_type_ = null;
		/// <summary>
		/// 资源对象类型
		/// </summary>
		public Type objType
		{
			get
			{
				return obj_type_;
			}
			set
			{
				obj_type_ = value;
			}
		}
		AssetsType asset_type_ = AssetsType.NORMAL;
		/// <summary>
		/// 加载的资源类型
		/// </summary>
		public AssetsType assetType
		{
			get
			{
				return asset_type_;
			}
			set
			{
				asset_type_ = value;
			}
		}
		UnityEngine.Transform parent_ = null;
		/// <summary>
		/// 对象的父对象GameObject
		/// </summary>
		public UnityEngine.Transform objParent
		{
			get
			{
				return parent_;
			}
			set
			{
				parent_ = value;
			}
		}
		/// <summary>
		/// 默认构造
		/// </summary>
		public AssetObject()
		{
			
		}
		/// <summary>
		/// 构造加载的资源
		/// </summary>
		/// <param name="asset_path"></param>
		/// <param name="obj_type"></param>
		/// <param name="asset_type"></param>
		public AssetObject(string asset_path, Type obj_type, AssetsType asset_type)
		{
			path_ = asset_path;
			obj_type_ = obj_type;
			asset_type_ = asset_type;
		}
	}
}