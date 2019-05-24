/**  版本信息模板在安装目录下，可自行修改。
* position.cs
*
* 功 能： N/A
* 类 名： position
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/3/21 16:40:40   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace LogisticsChain.Entity
{
	/// <summary>
	/// position:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PositionEntity
	{
		public PositionEntity()
		{}
		#region Model
		private string _id;
		private DateTime? _createtime;
		private string _longitude;
		private string _latitude;
		private string _waybill;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string longitude
		{
			set{ _longitude=value;}
			get{return _longitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string latitude
		{
			set{ _latitude=value;}
			get{return _latitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Waybill
		{
			set{ _waybill=value;}
			get{return _waybill;}
		}
		#endregion Model

	}
}

