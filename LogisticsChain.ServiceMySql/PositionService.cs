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
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
//using Maticsoft.DBUtility;//Please add references
namespace LogisticsChain.ServiceMySql
{
	/// <summary>
	/// 数据访问类:position
	/// </summary>
	public partial class PositionService
	{
		public PositionService()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from position");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,36)			};
			parameters[0].Value = Id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(LogisticsChain.Entity.PositionEntity model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into position(");
			strSql.Append("Id,CreateTime,longitude,latitude,Waybill)");
			strSql.Append(" values (");
			strSql.Append("@Id,@CreateTime,@longitude,@latitude,@Waybill)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,36),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@longitude", MySqlDbType.VarChar,12),
					new MySqlParameter("@latitude", MySqlDbType.VarChar,12),
					new MySqlParameter("@Waybill", MySqlDbType.VarChar,36)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CreateTime;
			parameters[2].Value = model.longitude;
			parameters[3].Value = model.latitude;
			parameters[4].Value = model.Waybill;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LogisticsChain.Entity.PositionEntity model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update position set ");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("longitude=@longitude,");
			strSql.Append("latitude=@latitude,");
			strSql.Append("Waybill=@Waybill");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@longitude", MySqlDbType.VarChar,12),
					new MySqlParameter("@latitude", MySqlDbType.VarChar,12),
					new MySqlParameter("@Waybill", MySqlDbType.VarChar,36),
					new MySqlParameter("@Id", MySqlDbType.VarChar,36)};
			parameters[0].Value = model.CreateTime;
			parameters[1].Value = model.longitude;
			parameters[2].Value = model.latitude;
			parameters[3].Value = model.Waybill;
			parameters[4].Value = model.Id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from position ");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,36)			};
			parameters[0].Value = Id;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from position ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LogisticsChain.Entity.PositionEntity GetModel(string Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,CreateTime,longitude,latitude,Waybill from position ");
			strSql.Append(" where Id=@Id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Id", MySqlDbType.VarChar,36)			};
			parameters[0].Value = Id;

			LogisticsChain.Entity.PositionEntity model=new LogisticsChain.Entity.PositionEntity();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LogisticsChain.Entity.PositionEntity DataRowToModel(DataRow row)
		{
			LogisticsChain.Entity.PositionEntity model=new LogisticsChain.Entity.PositionEntity();
			if (row != null)
			{
				if(row["Id"]!=null)
				{
					model.Id=row["Id"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["longitude"]!=null)
				{
					model.longitude=row["longitude"].ToString();
				}
				if(row["latitude"]!=null)
				{
					model.latitude=row["latitude"].ToString();
				}
				if(row["Waybill"]!=null)
				{
					model.Waybill=row["Waybill"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,CreateTime,longitude,latitude,Waybill ");
			strSql.Append(" FROM position ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM position ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperMySQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from position T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "position";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

