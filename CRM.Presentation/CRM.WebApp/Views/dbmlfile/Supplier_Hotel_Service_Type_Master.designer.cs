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
	public partial class Supplier_Hotel_Service_Type_MasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public Supplier_Hotel_Service_Type_MasterDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public Supplier_Hotel_Service_Type_MasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Supplier_Hotel_Service_Type_MasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Supplier_Hotel_Service_Type_MasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public Supplier_Hotel_Service_Type_MasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER> VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTERs
		{
			get
			{
				return this.GetTable<VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER")]
	public partial class VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER
	{
		
		private int _SERVICE_TYPE_ID;
		
		private string _SERVICE_TYPE_DESC;
		
		private System.Nullable<int> _SUPPLIER_SR_NO;
		
		private string _SUPPLIER_NAME;
		
		public VIEW_SUPPLIER_HOTEL_SERVICE_TYPE_MASTER()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SERVICE_TYPE_ID", DbType="Int NOT NULL")]
		public int SERVICE_TYPE_ID
		{
			get
			{
				return this._SERVICE_TYPE_ID;
			}
			set
			{
				if ((this._SERVICE_TYPE_ID != value))
				{
					this._SERVICE_TYPE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SERVICE_TYPE_DESC", DbType="VarChar(50)")]
		public string SERVICE_TYPE_DESC
		{
			get
			{
				return this._SERVICE_TYPE_DESC;
			}
			set
			{
				if ((this._SERVICE_TYPE_DESC != value))
				{
					this._SERVICE_TYPE_DESC = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUPPLIER_SR_NO", DbType="Int")]
		public System.Nullable<int> SUPPLIER_SR_NO
		{
			get
			{
				return this._SUPPLIER_SR_NO;
			}
			set
			{
				if ((this._SUPPLIER_SR_NO != value))
				{
					this._SUPPLIER_SR_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUPPLIER_NAME", DbType="VarChar(MAX)")]
		public string SUPPLIER_NAME
		{
			get
			{
				return this._SUPPLIER_NAME;
			}
			set
			{
				if ((this._SUPPLIER_NAME != value))
				{
					this._SUPPLIER_NAME = value;
				}
			}
		}
	}
}
#pragma warning restore 1591