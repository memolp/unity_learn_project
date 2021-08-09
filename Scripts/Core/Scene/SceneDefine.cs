/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/8/9
 * Time: 17:56
 * 
 * 场景属性定义 - 类似UI的方式
 * 将这种特殊方式最大化利用起来
 * 虽然这不一定很好
 */
using System;
using System.Collections.Generic;

 
namespace QSmale.Core
{
 	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false,Inherited=false)]
 	public class SceneDefine: Attribute
	{
		public static Type SceneDefineType = typeof(SceneDefine);
		/// <summary>
		/// 场景的名称-唯一
		/// </summary>
		public string sceneName{get;set;}
		/// <summary>
		/// 场景的路径
		/// </summary>
		public string scenePath{get;set;}
		/// <summary>
		/// 场景类
		/// </summary>
		public Type sceneType{get; set;}
		/// <summary>
		/// 设置场景的名称和路径
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public SceneDefine(string name, string path)
		{
			this.sceneName = name;
			this.scenePath = path;
		}
	}
 }

