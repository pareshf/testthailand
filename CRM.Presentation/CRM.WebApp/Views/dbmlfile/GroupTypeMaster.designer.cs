﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.WebApp.Views.dbmlfile
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="THAILAND_CRM")]
	public partial class GroupTypeMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public GroupTypeMasterDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public GroupTypeMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GroupTypeMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GroupTypeMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GroupTypeMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_GROUP_TYPE_MASTER> VIEW_GROUP_TYPE_MASTERs
		{
			get
			{
				return this.GetTable<VIEW_GROUP_TYPE_MASTER>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_GROUP_TYPE_MASTER")]
	public partial class VIEW_GROUP_TYPE_MASTER
	{
		
		private int _GROUP_TYPE_ID;
		
		private string _GROUP_TYPE;
		
		public VIEW_GROUP_TYPE_MASTER()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GROUP_TYPE_ID", DbType="Int NOT NULL")]
		public int GROUP_TYPE_ID
		{
			get
			{
				return this._GROUP_TYPE_ID;
			}
			set
			{
				if ((this._GROUP_TYPE_ID != value))
				{
					this._GROUP_TYPE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GROUP_TYPE", DbType="VarChar(100)")]
		public string GROUP_TYPE
		{
			get
			{
				return this._GROUP_TYPE;
			}
			set
			{
				if ((this._GROUP_TYPE != value))
				{
					this._GROUP_TYPE = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
