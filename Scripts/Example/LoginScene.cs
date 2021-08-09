/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/8/9
 * Time: 18:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using QSmale.Core;

namespace QSmale.Test
{
	[SceneDefine("LoginScene", "Assets/ArtData/Scenes/Login.unity")]
	public class LoginScene: SceneBase
	{
		public override void onLoadEnd(SceneAsset asset)
		{
			base.onLoadEnd(asset);
			UIManager.Instance.ShowWindow("LoginUI");
		}
	}
}