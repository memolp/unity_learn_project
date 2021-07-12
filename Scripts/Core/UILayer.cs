/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/8
 * Time: 20:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace QSmale.Core
{
	public enum UILayer
	{
		UI_BACKGROUND = 1,  // 最低层
		UI_NORMAL,			// 通用层
		UI_TOP,				// 最顶层
		UI_LAYER_MAX,		// 始终都放在最后面
	}
	
	public class UIAssets: AssetObject
	{
		UILayer _layer = UILayer.UI_NORMAL;
		/// <summary>
		/// UI层级
		/// </summary>
		public UILayer uiLayer
		{
			get
			{
				return _layer;
			}
			set
			{
				_layer = value;
			}
		}
	}
}