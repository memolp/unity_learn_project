/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/8/9
 * Time: 18:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UnityEngine.SceneManagement;
 
namespace QSmale.Core
{
	/// <summary>
	/// 场景资源定义，继承自基类，并重新加载完成后的逻辑
	/// </summary>
 	public class SceneAsset: AssetObject
 	{
 		/// <summary>
 		/// 场景名
 		/// </summary>
 		public string sceneName{get;set;}
 		/// <summary>
		/// 加载模式
		/// </summary>
		public LoadSceneMode sceneMode{get; set;}
		/// <summary>
		/// 当前加载的场景对象
		/// </summary>
		public SceneBase sceneObj{get;set;}
		/// <summary>
		/// 场景加载完后调用
		/// </summary>
		/// <param name="obj"></param>
		public override void onLoadEnd(UnityEngine.Object obj)
		{
			base.onLoadEnd(obj);
			this.sceneObj.onLoadEnd(this);
		}
 	}
 	
 	/// <summary>
 	/// 场景接口
 	/// </summary>
 	public interface ISceneCore
 	{
 		
 	}
 	
 	/// <summary>
 	/// 场景基类，实现了加载和加载完成的结果
 	/// </summary>
 	public class SceneBase : ISceneCore
 	{
 		/// <summary>
 		/// 开始时进行场景加载
 		/// </summary>
 		/// <param name="asset"></param>
 		public virtual void onLoad(SceneAsset asset)
 		{
 			asset.sceneObj = this;
 			Global.StartCoroutine(ResouceLoader.Instance.LoadAsset(asset));
 		}
 		/// <summary>
 		/// 场景加载完成调用
 		/// </summary>
 		/// <param name="asset"></param>
 		public virtual void onLoadEnd(SceneAsset asset)
 		{
 			
 		}
 	}
}