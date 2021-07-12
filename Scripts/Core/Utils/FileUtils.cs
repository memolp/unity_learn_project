/*
 * Created by SharpDevelop.
 * User: qingf
 * Date: 2021/7/6
 * Time: 20:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace QSmale.Core
{
	public class FileUtils
	{
		static MD5 _md5_obj = MD5.Create();
		/// <summary>
		/// [unsafe]计算文件的MD5- 需要自己检查文件是否存在
		/// </summary>
		/// <param name="file_path"></param>
		/// <returns></returns>
		public static string calc_md5(string file_path)
		{
			FileStream fs = File.OpenRead(file_path);
			byte[] data = _md5_obj.ComputeHash(fs);
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<data.Length; i++)
			{
				sb.Append(data[i].ToString("x2").ToUpper());
			}
			fs.Close();
			return sb.ToString();
		}
		/// <summary>
		/// [unsafe]获取文件的大小 -需要自己检查文件是否存在
		/// </summary>
		/// <param name="file_path"></param>
		/// <returns></returns>
		public static long calc_size(string file_path)
		{
			FileInfo info = new FileInfo(file_path);
			return info.Length;
		}
	}
}
