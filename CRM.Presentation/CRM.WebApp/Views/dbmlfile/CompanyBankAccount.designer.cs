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
	public partial class CompanyBankAccountDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public CompanyBankAccountDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["THAILAND_CRMConnectionString3"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyBankAccountDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyBankAccountDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyBankAccountDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyBankAccountDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_FOR_COMPANY_BANK_DETAIL> VIEW_FOR_COMPANY_BANK_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_FOR_COMPANY_BANK_DETAIL>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_FOR_COMPANY_BANK_DETAILS")]
	public partial class VIEW_FOR_COMPANY_BANK_DETAIL
	{
		
		private int _COMP_ACC_ID;
		
		private string _BANK_ACC_NAME;
		
		private string _ACC_NO_BANK;
		
		private string _COMP_BANK_BRNACH;
		
		private string _SWIFT_CODE;
		
		private string _BANK_ADD_COMP;
		
		private string _COMPANY_NAME_BANK;
		
		private string _BANK_NAME_OF_COMP;
		
		private string _IBOB;
		
		public VIEW_FOR_COMPANY_BANK_DETAIL()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COMP_ACC_ID", DbType="Int NOT NULL")]
		public int COMP_ACC_ID
		{
			get
			{
				return this._COMP_ACC_ID;
			}
			set
			{
				if ((this._COMP_ACC_ID != value))
				{
					this._COMP_ACC_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_ACC_NAME", DbType="VarChar(100)")]
		public string BANK_ACC_NAME
		{
			get
			{
				return this._BANK_ACC_NAME;
			}
			set
			{
				if ((this._BANK_ACC_NAME != value))
				{
					this._BANK_ACC_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ACC_NO_BANK", DbType="VarChar(100)")]
		public string ACC_NO_BANK
		{
			get
			{
				return this._ACC_NO_BANK;
			}
			set
			{
				if ((this._ACC_NO_BANK != value))
				{
					this._ACC_NO_BANK = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COMP_BANK_BRNACH", DbType="VarChar(100)")]
		public string COMP_BANK_BRNACH
		{
			get
			{
				return this._COMP_BANK_BRNACH;
			}
			set
			{
				if ((this._COMP_BANK_BRNACH != value))
				{
					this._COMP_BANK_BRNACH = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SWIFT_CODE", DbType="VarChar(100)")]
		public string SWIFT_CODE
		{
			get
			{
				return this._SWIFT_CODE;
			}
			set
			{
				if ((this._SWIFT_CODE != value))
				{
					this._SWIFT_CODE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_ADD_COMP", DbType="VarChar(MAX)")]
		public string BANK_ADD_COMP
		{
			get
			{
				return this._BANK_ADD_COMP;
			}
			set
			{
				if ((this._BANK_ADD_COMP != value))
				{
					this._BANK_ADD_COMP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COMPANY_NAME_BANK", DbType="VarChar(100)")]
		public string COMPANY_NAME_BANK
		{
			get
			{
				return this._COMPANY_NAME_BANK;
			}
			set
			{
				if ((this._COMPANY_NAME_BANK != value))
				{
					this._COMPANY_NAME_BANK = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_NAME_OF_COMP", DbType="VarChar(50)")]
		public string BANK_NAME_OF_COMP
		{
			get
			{
				return this._BANK_NAME_OF_COMP;
			}
			set
			{
				if ((this._BANK_NAME_OF_COMP != value))
				{
					this._BANK_NAME_OF_COMP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IBOB", DbType="VarChar(100)")]
		public string IBOB
		{
			get
			{
				return this._IBOB;
			}
			set
			{
				if ((this._IBOB != value))
				{
					this._IBOB = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
