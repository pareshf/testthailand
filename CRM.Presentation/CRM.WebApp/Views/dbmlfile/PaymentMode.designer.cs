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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="FULL_CRM_ASHISH")]
	public partial class PaymentModeDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public PaymentModeDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["FULL_CRM_ASHISHConnectionString4"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentModeDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentModeDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentModeDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PaymentModeDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_PAYMENT_MODE> VIEW_PAYMENT_MODEs
		{
			get
			{
				return this.GetTable<VIEW_PAYMENT_MODE>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PAYMENT_MODE")]
	public partial class VIEW_PAYMENT_MODE
	{
		
		private int _PAYMENT_MODE_ID;
		
		private string _PAYMENT_MODE_NAME;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		public VIEW_PAYMENT_MODE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_MODE_ID", DbType="Int NOT NULL")]
		public int PAYMENT_MODE_ID
		{
			get
			{
				return this._PAYMENT_MODE_ID;
			}
			set
			{
				if ((this._PAYMENT_MODE_ID != value))
				{
					this._PAYMENT_MODE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_MODE_NAME", DbType="VarChar(50)")]
		public string PAYMENT_MODE_NAME
		{
			get
			{
				return this._PAYMENT_MODE_NAME;
			}
			set
			{
				if ((this._PAYMENT_MODE_NAME != value))
				{
					this._PAYMENT_MODE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_CREATED", DbType="DateTime")]
		public System.Nullable<System.DateTime> DATE_CREATED
		{
			get
			{
				return this._DATE_CREATED;
			}
			set
			{
				if ((this._DATE_CREATED != value))
				{
					this._DATE_CREATED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CREATED_BY", DbType="Int")]
		public System.Nullable<int> CREATED_BY
		{
			get
			{
				return this._CREATED_BY;
			}
			set
			{
				if ((this._CREATED_BY != value))
				{
					this._CREATED_BY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_MODIFIED", DbType="DateTime")]
		public System.Nullable<System.DateTime> DATE_MODIFIED
		{
			get
			{
				return this._DATE_MODIFIED;
			}
			set
			{
				if ((this._DATE_MODIFIED != value))
				{
					this._DATE_MODIFIED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MODIFIED_BY", DbType="Int")]
		public System.Nullable<int> MODIFIED_BY
		{
			get
			{
				return this._MODIFIED_BY;
			}
			set
			{
				if ((this._MODIFIED_BY != value))
				{
					this._MODIFIED_BY = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
