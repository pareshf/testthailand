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
	public partial class ConvertionRateMasterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public ConvertionRateMasterDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ConvertionRateMasterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConvertionRateMasterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConvertionRateMasterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ConvertionRateMasterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_CONVERSION_RATE_MASTER> VIEW_CONVERSION_RATE_MASTERs
		{
			get
			{
				return this.GetTable<VIEW_CONVERSION_RATE_MASTER>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_CONVERSION_RATE_MASTER")]
	public partial class VIEW_CONVERSION_RATE_MASTER
	{
		
		private int _CONVERSION_RATE_ID;
		
		private string _FROM_CURRENCY;
		
		private string _TO_CURRENCY;
		
		private System.Nullable<decimal> _CONVERSION_RATE;
		
		public VIEW_CONVERSION_RATE_MASTER()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CONVERSION_RATE_ID", DbType="Int NOT NULL")]
		public int CONVERSION_RATE_ID
		{
			get
			{
				return this._CONVERSION_RATE_ID;
			}
			set
			{
				if ((this._CONVERSION_RATE_ID != value))
				{
					this._CONVERSION_RATE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FROM_CURRENCY", DbType="VarChar(50)")]
		public string FROM_CURRENCY
		{
			get
			{
				return this._FROM_CURRENCY;
			}
			set
			{
				if ((this._FROM_CURRENCY != value))
				{
					this._FROM_CURRENCY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TO_CURRENCY", DbType="VarChar(50)")]
		public string TO_CURRENCY
		{
			get
			{
				return this._TO_CURRENCY;
			}
			set
			{
				if ((this._TO_CURRENCY != value))
				{
					this._TO_CURRENCY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CONVERSION_RATE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> CONVERSION_RATE
		{
			get
			{
				return this._CONVERSION_RATE;
			}
			set
			{
				if ((this._CONVERSION_RATE != value))
				{
					this._CONVERSION_RATE = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
