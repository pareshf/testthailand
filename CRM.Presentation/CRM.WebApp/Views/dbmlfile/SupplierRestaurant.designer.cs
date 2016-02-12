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
	public partial class SupplierRestaurantDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public SupplierRestaurantDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SupplierRestaurantDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SupplierRestaurantDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SupplierRestaurantDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SupplierRestaurantDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_RESTAURATNT_PRICE_LIST> VIEW_RESTAURATNT_PRICE_LISTs
		{
			get
			{
				return this.GetTable<VIEW_RESTAURATNT_PRICE_LIST>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_RESTAURATNT_PRICE_LIST")]
	public partial class VIEW_RESTAURATNT_PRICE_LIST
	{
		
		private int _SUPPLIER_RESTAURANT_PRICE_LIST_ID;
		
		private System.Nullable<int> _SUPPLIER_SR_NO;
		
		private string _EFFECTIVE_FROM_DATE;
		
		private string _EFFECTIVE_TO_DATE;
		
		private string _DINNER_FROM_DATE;
		
		private string _DINNER_TO_DATE;
		
		private string _DINNER_FROM_TIME;
		
		private string _DINNER_TO_TIME;
		
		private string _MEAL_DESC;
		
		private string _DINNER_DESCRIPTION;
		
		private System.Nullable<decimal> _GIT_RATE;
		
		private System.Nullable<decimal> _FIT_RATE;
		
		private System.Nullable<decimal> _FIT_DISCOUNT;
		
		private System.Nullable<decimal> _GIT_DISCOUNT;
		
		private System.Nullable<decimal> _MARGIN_AMOUNT;
		
		private System.Nullable<decimal> _MARGIN_AMOUNT_IN_PERCENTAGE;
		
		private string _UPLOAD_RATE_DOCUMENT;
		
		private string _CURRENCY_NAME;
		
		private System.Nullable<int> _CUST_ID;
		
		private string _CUSTOMER_NAME;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _PAYMENT_TERMS;
		
		private string _CHAIN_NAME;
		
		private System.Nullable<decimal> _GIT_MARGIN_AMOUNT;
		
		private System.Nullable<decimal> _GIT_MARGIN_AMOUNT_IN_PERCENTAGE;
		
		private string _MEAL_TYPE;
		
		private string _CITY_NAME;
		
		private System.Nullable<decimal> _CHILD_RATE;
		
		private System.Nullable<decimal> _ADULT_RATE;
		
		private string _TO_ISSUE_SERVICE_VOUCHER;
		
		private System.Nullable<decimal> _A_MARGIN_IN_AMOUNT;
		
		private System.Nullable<decimal> _A_PLUS_MARGIN_IN_AMOUNT;
		
		private System.Nullable<decimal> _A_PLUS_PLUS_MARGIN_IN_AMOUNT;
		
		private System.Nullable<decimal> _A_MARGIN_AMOUNT_IN_PERCENTAGE;
		
		private System.Nullable<decimal> _A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE;
		
		private System.Nullable<decimal> _A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE;
		
		public VIEW_RESTAURATNT_PRICE_LIST()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SUPPLIER_RESTAURANT_PRICE_LIST_ID", DbType="Int NOT NULL")]
		public int SUPPLIER_RESTAURANT_PRICE_LIST_ID
		{
			get
			{
				return this._SUPPLIER_RESTAURANT_PRICE_LIST_ID;
			}
			set
			{
				if ((this._SUPPLIER_RESTAURANT_PRICE_LIST_ID != value))
				{
					this._SUPPLIER_RESTAURANT_PRICE_LIST_ID = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EFFECTIVE_FROM_DATE", DbType="VarChar(10)")]
		public string EFFECTIVE_FROM_DATE
		{
			get
			{
				return this._EFFECTIVE_FROM_DATE;
			}
			set
			{
				if ((this._EFFECTIVE_FROM_DATE != value))
				{
					this._EFFECTIVE_FROM_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EFFECTIVE_TO_DATE", DbType="VarChar(10)")]
		public string EFFECTIVE_TO_DATE
		{
			get
			{
				return this._EFFECTIVE_TO_DATE;
			}
			set
			{
				if ((this._EFFECTIVE_TO_DATE != value))
				{
					this._EFFECTIVE_TO_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DINNER_FROM_DATE", DbType="VarChar(10)")]
		public string DINNER_FROM_DATE
		{
			get
			{
				return this._DINNER_FROM_DATE;
			}
			set
			{
				if ((this._DINNER_FROM_DATE != value))
				{
					this._DINNER_FROM_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DINNER_TO_DATE", DbType="VarChar(10)")]
		public string DINNER_TO_DATE
		{
			get
			{
				return this._DINNER_TO_DATE;
			}
			set
			{
				if ((this._DINNER_TO_DATE != value))
				{
					this._DINNER_TO_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DINNER_FROM_TIME", DbType="VarChar(50)")]
		public string DINNER_FROM_TIME
		{
			get
			{
				return this._DINNER_FROM_TIME;
			}
			set
			{
				if ((this._DINNER_FROM_TIME != value))
				{
					this._DINNER_FROM_TIME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DINNER_TO_TIME", DbType="VarChar(50)")]
		public string DINNER_TO_TIME
		{
			get
			{
				return this._DINNER_TO_TIME;
			}
			set
			{
				if ((this._DINNER_TO_TIME != value))
				{
					this._DINNER_TO_TIME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEAL_DESC", DbType="VarChar(255)")]
		public string MEAL_DESC
		{
			get
			{
				return this._MEAL_DESC;
			}
			set
			{
				if ((this._MEAL_DESC != value))
				{
					this._MEAL_DESC = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DINNER_DESCRIPTION", DbType="VarChar(MAX)")]
		public string DINNER_DESCRIPTION
		{
			get
			{
				return this._DINNER_DESCRIPTION;
			}
			set
			{
				if ((this._DINNER_DESCRIPTION != value))
				{
					this._DINNER_DESCRIPTION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIT_RATE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> GIT_RATE
		{
			get
			{
				return this._GIT_RATE;
			}
			set
			{
				if ((this._GIT_RATE != value))
				{
					this._GIT_RATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FIT_RATE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> FIT_RATE
		{
			get
			{
				return this._FIT_RATE;
			}
			set
			{
				if ((this._FIT_RATE != value))
				{
					this._FIT_RATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FIT_DISCOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> FIT_DISCOUNT
		{
			get
			{
				return this._FIT_DISCOUNT;
			}
			set
			{
				if ((this._FIT_DISCOUNT != value))
				{
					this._FIT_DISCOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIT_DISCOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> GIT_DISCOUNT
		{
			get
			{
				return this._GIT_DISCOUNT;
			}
			set
			{
				if ((this._GIT_DISCOUNT != value))
				{
					this._GIT_DISCOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MARGIN_AMOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> MARGIN_AMOUNT
		{
			get
			{
				return this._MARGIN_AMOUNT;
			}
			set
			{
				if ((this._MARGIN_AMOUNT != value))
				{
					this._MARGIN_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MARGIN_AMOUNT_IN_PERCENTAGE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> MARGIN_AMOUNT_IN_PERCENTAGE
		{
			get
			{
				return this._MARGIN_AMOUNT_IN_PERCENTAGE;
			}
			set
			{
				if ((this._MARGIN_AMOUNT_IN_PERCENTAGE != value))
				{
					this._MARGIN_AMOUNT_IN_PERCENTAGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UPLOAD_RATE_DOCUMENT", DbType="VarChar(MAX)")]
		public string UPLOAD_RATE_DOCUMENT
		{
			get
			{
				return this._UPLOAD_RATE_DOCUMENT;
			}
			set
			{
				if ((this._UPLOAD_RATE_DOCUMENT != value))
				{
					this._UPLOAD_RATE_DOCUMENT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CURRENCY_NAME", DbType="VarChar(50)")]
		public string CURRENCY_NAME
		{
			get
			{
				return this._CURRENCY_NAME;
			}
			set
			{
				if ((this._CURRENCY_NAME != value))
				{
					this._CURRENCY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_ID", DbType="Int")]
		public System.Nullable<int> CUST_ID
		{
			get
			{
				return this._CUST_ID;
			}
			set
			{
				if ((this._CUST_ID != value))
				{
					this._CUST_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUSTOMER_NAME", DbType="VarChar(306)")]
		public string CUSTOMER_NAME
		{
			get
			{
				return this._CUSTOMER_NAME;
			}
			set
			{
				if ((this._CUSTOMER_NAME != value))
				{
					this._CUSTOMER_NAME = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_TERMS", DbType="VarChar(50)")]
		public string PAYMENT_TERMS
		{
			get
			{
				return this._PAYMENT_TERMS;
			}
			set
			{
				if ((this._PAYMENT_TERMS != value))
				{
					this._PAYMENT_TERMS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHAIN_NAME", DbType="VarChar(MAX)")]
		public string CHAIN_NAME
		{
			get
			{
				return this._CHAIN_NAME;
			}
			set
			{
				if ((this._CHAIN_NAME != value))
				{
					this._CHAIN_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIT_MARGIN_AMOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> GIT_MARGIN_AMOUNT
		{
			get
			{
				return this._GIT_MARGIN_AMOUNT;
			}
			set
			{
				if ((this._GIT_MARGIN_AMOUNT != value))
				{
					this._GIT_MARGIN_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GIT_MARGIN_AMOUNT_IN_PERCENTAGE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> GIT_MARGIN_AMOUNT_IN_PERCENTAGE
		{
			get
			{
				return this._GIT_MARGIN_AMOUNT_IN_PERCENTAGE;
			}
			set
			{
				if ((this._GIT_MARGIN_AMOUNT_IN_PERCENTAGE != value))
				{
					this._GIT_MARGIN_AMOUNT_IN_PERCENTAGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MEAL_TYPE", DbType="VarChar(50)")]
		public string MEAL_TYPE
		{
			get
			{
				return this._MEAL_TYPE;
			}
			set
			{
				if ((this._MEAL_TYPE != value))
				{
					this._MEAL_TYPE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CITY_NAME", DbType="VarChar(50)")]
		public string CITY_NAME
		{
			get
			{
				return this._CITY_NAME;
			}
			set
			{
				if ((this._CITY_NAME != value))
				{
					this._CITY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHILD_RATE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> CHILD_RATE
		{
			get
			{
				return this._CHILD_RATE;
			}
			set
			{
				if ((this._CHILD_RATE != value))
				{
					this._CHILD_RATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADULT_RATE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> ADULT_RATE
		{
			get
			{
				return this._ADULT_RATE;
			}
			set
			{
				if ((this._ADULT_RATE != value))
				{
					this._ADULT_RATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TO_ISSUE_SERVICE_VOUCHER", DbType="VarChar(50)")]
		public string TO_ISSUE_SERVICE_VOUCHER
		{
			get
			{
				return this._TO_ISSUE_SERVICE_VOUCHER;
			}
			set
			{
				if ((this._TO_ISSUE_SERVICE_VOUCHER != value))
				{
					this._TO_ISSUE_SERVICE_VOUCHER = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_MARGIN_IN_AMOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_MARGIN_IN_AMOUNT
		{
			get
			{
				return this._A_MARGIN_IN_AMOUNT;
			}
			set
			{
				if ((this._A_MARGIN_IN_AMOUNT != value))
				{
					this._A_MARGIN_IN_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_PLUS_MARGIN_IN_AMOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_PLUS_MARGIN_IN_AMOUNT
		{
			get
			{
				return this._A_PLUS_MARGIN_IN_AMOUNT;
			}
			set
			{
				if ((this._A_PLUS_MARGIN_IN_AMOUNT != value))
				{
					this._A_PLUS_MARGIN_IN_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_PLUS_PLUS_MARGIN_IN_AMOUNT", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_PLUS_PLUS_MARGIN_IN_AMOUNT
		{
			get
			{
				return this._A_PLUS_PLUS_MARGIN_IN_AMOUNT;
			}
			set
			{
				if ((this._A_PLUS_PLUS_MARGIN_IN_AMOUNT != value))
				{
					this._A_PLUS_PLUS_MARGIN_IN_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_MARGIN_AMOUNT_IN_PERCENTAGE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_MARGIN_AMOUNT_IN_PERCENTAGE
		{
			get
			{
				return this._A_MARGIN_AMOUNT_IN_PERCENTAGE;
			}
			set
			{
				if ((this._A_MARGIN_AMOUNT_IN_PERCENTAGE != value))
				{
					this._A_MARGIN_AMOUNT_IN_PERCENTAGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE
		{
			get
			{
				return this._A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE;
			}
			set
			{
				if ((this._A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE != value))
				{
					this._A_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE", DbType="Decimal(16,2)")]
		public System.Nullable<decimal> A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE
		{
			get
			{
				return this._A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE;
			}
			set
			{
				if ((this._A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE != value))
				{
					this._A_PLUS_PLUS_MARGIN_AMOUNT_IN_PERCENTAGE = value;
				}
			}
		}
	}
}
#pragma warning restore 1591