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
	public partial class SiteSeeingPriceListDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public SiteSeeingPriceListDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SiteSeeingPriceListDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteSeeingPriceListDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteSeeingPriceListDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteSeeingPriceListDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_SITE_SEEING_PRICE_LIST> VIEW_SITE_SEEING_PRICE_LISTs
		{
			get
			{
				return this.GetTable<VIEW_SITE_SEEING_PRICE_LIST>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_SITE_SEEING_PRICE_LIST")]
	public partial class VIEW_SITE_SEEING_PRICE_LIST
	{
		
		private int _SIGHT_SEEING_PRICE_ID;
		
		private string _SIGHT_SEEING_DATE;
		
		private string _SIGHT_SEEING_TIME;
		
		private System.Nullable<decimal> _GIT_RATE;
		
		private System.Nullable<decimal> _FIT_RATE;
		
		private System.Nullable<decimal> _FIT_DISCOUNT;
		
		private System.Nullable<decimal> _GIT_DISCOUNT;
		
		private System.Nullable<decimal> _MARGIN_AMOUNT;
		
		private System.Nullable<decimal> _MARGIN_AMOUNT_IN_PERCENTAGE;
		
		private string _CURRENCY_NAME;
		
		private string _EFFECTIVE_FROM_DATE;
		
		private string _EFFECTIVE_TO_DATE;
		
		private string _DINNER_DESCRIPTION;
		
		private string _SIC_PVT_FLAG;
		
		private string _CITY_NAME;
		
		private string _PAYMENT_TERMS;
		
		public VIEW_SITE_SEEING_PRICE_LIST()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIGHT_SEEING_PRICE_ID", DbType="Int NOT NULL")]
		public int SIGHT_SEEING_PRICE_ID
		{
			get
			{
				return this._SIGHT_SEEING_PRICE_ID;
			}
			set
			{
				if ((this._SIGHT_SEEING_PRICE_ID != value))
				{
					this._SIGHT_SEEING_PRICE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIGHT_SEEING_DATE", DbType="VarChar(10)")]
		public string SIGHT_SEEING_DATE
		{
			get
			{
				return this._SIGHT_SEEING_DATE;
			}
			set
			{
				if ((this._SIGHT_SEEING_DATE != value))
				{
					this._SIGHT_SEEING_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIGHT_SEEING_TIME", DbType="VarChar(50)")]
		public string SIGHT_SEEING_TIME
		{
			get
			{
				return this._SIGHT_SEEING_TIME;
			}
			set
			{
				if ((this._SIGHT_SEEING_TIME != value))
				{
					this._SIGHT_SEEING_TIME = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SIC_PVT_FLAG", DbType="VarChar(50)")]
		public string SIC_PVT_FLAG
		{
			get
			{
				return this._SIC_PVT_FLAG;
			}
			set
			{
				if ((this._SIC_PVT_FLAG != value))
				{
					this._SIC_PVT_FLAG = value;
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
	}
}
#pragma warning restore 1591
