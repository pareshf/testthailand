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
	public partial class BookingForeignMoneyTransferAgentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public BookingForeignMoneyTransferAgentDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["FULL_CRM_ASHISHConnectionString13"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BookingForeignMoneyTransferAgentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingForeignMoneyTransferAgentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingForeignMoneyTransferAgentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingForeignMoneyTransferAgentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_PAYMENT_FOREIGN_REPORT> VIEW_PAYMENT_FOREIGN_REPORTs
		{
			get
			{
				return this.GetTable<VIEW_PAYMENT_FOREIGN_REPORT>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_PAYMENT_FOREIGN_AGENT> VIEW_PAYMENT_FOREIGN_AGENTs
		{
			get
			{
				return this.GetTable<VIEW_PAYMENT_FOREIGN_AGENT>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_PAYMENT_FOREIGN_SIMPLE> VIEW_PAYMENT_FOREIGN_SIMPLEs
		{
			get
			{
				return this.GetTable<VIEW_PAYMENT_FOREIGN_SIMPLE>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PAYMENT_FOREIGN_REPORT")]
	public partial class VIEW_PAYMENT_FOREIGN_REPORT
	{
		
		private System.Nullable<int> _TOUR_ID;
		
		private System.Nullable<decimal> _AMT1;
		
		private System.Nullable<decimal> _AMT2;
		
		private System.Nullable<decimal> _TOT_AMT;
		
		private string _PAYMENT_CURR_CODE;
		
		public VIEW_PAYMENT_FOREIGN_REPORT()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOUR_ID", DbType="Int")]
		public System.Nullable<int> TOUR_ID
		{
			get
			{
				return this._TOUR_ID;
			}
			set
			{
				if ((this._TOUR_ID != value))
				{
					this._TOUR_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AMT1", DbType="Decimal(38,2)")]
		public System.Nullable<decimal> AMT1
		{
			get
			{
				return this._AMT1;
			}
			set
			{
				if ((this._AMT1 != value))
				{
					this._AMT1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AMT2", DbType="Decimal(38,2)")]
		public System.Nullable<decimal> AMT2
		{
			get
			{
				return this._AMT2;
			}
			set
			{
				if ((this._AMT2 != value))
				{
					this._AMT2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOT_AMT", DbType="Decimal(38,2)")]
		public System.Nullable<decimal> TOT_AMT
		{
			get
			{
				return this._TOT_AMT;
			}
			set
			{
				if ((this._TOT_AMT != value))
				{
					this._TOT_AMT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_CURR_CODE", DbType="VarChar(50)")]
		public string PAYMENT_CURR_CODE
		{
			get
			{
				return this._PAYMENT_CURR_CODE;
			}
			set
			{
				if ((this._PAYMENT_CURR_CODE != value))
				{
					this._PAYMENT_CURR_CODE = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PAYMENT_FOREIGN_AGENT")]
	public partial class VIEW_PAYMENT_FOREIGN_AGENT
	{
		
		private int _FA_SR_NO;
		
		private System.Nullable<int> _AGENT_ID;
		
		private System.Nullable<int> _BOOKING_ID;
		
		private System.Nullable<int> _FOREIGN_CURR_AGENT_ID;
		
		private string _PAYMENT_CURR_CODE;
		
		private string _PAYMENT_MODE_NAME;
		
		private string _REC_CHEQUE_DD_NO;
		
		private System.Nullable<decimal> _AMOUNT;
		
		private string _BANK_NAME;
		
		private string _BANK_BRANCH_NAME;
		
		private string _RECEIPT_NO;
		
		private string _RECEIPT_DATE;
		
		private string _USER_NAME;
		
		private System.Nullable<decimal> _SERVICE_CHARGE;
		
		private string _COMPANY_NAME;
		
		private System.Nullable<int> _TOUR_ID;
		
		private string _STATUS_NAME;
		
		public VIEW_PAYMENT_FOREIGN_AGENT()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FA_SR_NO", DbType="Int NOT NULL")]
		public int FA_SR_NO
		{
			get
			{
				return this._FA_SR_NO;
			}
			set
			{
				if ((this._FA_SR_NO != value))
				{
					this._FA_SR_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AGENT_ID", DbType="Int")]
		public System.Nullable<int> AGENT_ID
		{
			get
			{
				return this._AGENT_ID;
			}
			set
			{
				if ((this._AGENT_ID != value))
				{
					this._AGENT_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_ID", DbType="Int")]
		public System.Nullable<int> BOOKING_ID
		{
			get
			{
				return this._BOOKING_ID;
			}
			set
			{
				if ((this._BOOKING_ID != value))
				{
					this._BOOKING_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FOREIGN_CURR_AGENT_ID", DbType="Int")]
		public System.Nullable<int> FOREIGN_CURR_AGENT_ID
		{
			get
			{
				return this._FOREIGN_CURR_AGENT_ID;
			}
			set
			{
				if ((this._FOREIGN_CURR_AGENT_ID != value))
				{
					this._FOREIGN_CURR_AGENT_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_CURR_CODE", DbType="VarChar(50)")]
		public string PAYMENT_CURR_CODE
		{
			get
			{
				return this._PAYMENT_CURR_CODE;
			}
			set
			{
				if ((this._PAYMENT_CURR_CODE != value))
				{
					this._PAYMENT_CURR_CODE = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REC_CHEQUE_DD_NO", DbType="VarChar(50)")]
		public string REC_CHEQUE_DD_NO
		{
			get
			{
				return this._REC_CHEQUE_DD_NO;
			}
			set
			{
				if ((this._REC_CHEQUE_DD_NO != value))
				{
					this._REC_CHEQUE_DD_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AMOUNT", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> AMOUNT
		{
			get
			{
				return this._AMOUNT;
			}
			set
			{
				if ((this._AMOUNT != value))
				{
					this._AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_NAME", DbType="VarChar(50)")]
		public string BANK_NAME
		{
			get
			{
				return this._BANK_NAME;
			}
			set
			{
				if ((this._BANK_NAME != value))
				{
					this._BANK_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_BRANCH_NAME", DbType="VarChar(50)")]
		public string BANK_BRANCH_NAME
		{
			get
			{
				return this._BANK_BRANCH_NAME;
			}
			set
			{
				if ((this._BANK_BRANCH_NAME != value))
				{
					this._BANK_BRANCH_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RECEIPT_NO", DbType="VarChar(25)")]
		public string RECEIPT_NO
		{
			get
			{
				return this._RECEIPT_NO;
			}
			set
			{
				if ((this._RECEIPT_NO != value))
				{
					this._RECEIPT_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RECEIPT_DATE", DbType="VarChar(10)")]
		public string RECEIPT_DATE
		{
			get
			{
				return this._RECEIPT_DATE;
			}
			set
			{
				if ((this._RECEIPT_DATE != value))
				{
					this._RECEIPT_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_NAME", DbType="VarChar(50)")]
		public string USER_NAME
		{
			get
			{
				return this._USER_NAME;
			}
			set
			{
				if ((this._USER_NAME != value))
				{
					this._USER_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SERVICE_CHARGE", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> SERVICE_CHARGE
		{
			get
			{
				return this._SERVICE_CHARGE;
			}
			set
			{
				if ((this._SERVICE_CHARGE != value))
				{
					this._SERVICE_CHARGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COMPANY_NAME", DbType="VarChar(100)")]
		public string COMPANY_NAME
		{
			get
			{
				return this._COMPANY_NAME;
			}
			set
			{
				if ((this._COMPANY_NAME != value))
				{
					this._COMPANY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOUR_ID", DbType="Int")]
		public System.Nullable<int> TOUR_ID
		{
			get
			{
				return this._TOUR_ID;
			}
			set
			{
				if ((this._TOUR_ID != value))
				{
					this._TOUR_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_NAME", DbType="VarChar(100)")]
		public string STATUS_NAME
		{
			get
			{
				return this._STATUS_NAME;
			}
			set
			{
				if ((this._STATUS_NAME != value))
				{
					this._STATUS_NAME = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PAYMENT_FOREIGN_SIMPLE")]
	public partial class VIEW_PAYMENT_FOREIGN_SIMPLE
	{
		
		private int _FA_SR_NO;
		
		private string _AGENT_ID;
		
		private System.Nullable<int> _BOOKING_ID;
		
		private string _FOREIGN_CURR_AGENT_ID;
		
		private string _PAYMENT_CURR_CODE;
		
		private string _PAYMENT_MODE_NAME;
		
		private string _REC_CHEQUE_DD_NO;
		
		private System.Nullable<decimal> _AMOUNT;
		
		private string _BANK_NAME;
		
		private string _BANK_BRANCH_NAME;
		
		private string _RECEIPT_NO;
		
		private string _RECEIPT_DATE;
		
		private string _USER_NAME;
		
		private System.Nullable<decimal> _SERVICE_CHARGE;
		
		private string _COMPANY_NAME;
		
		private System.Nullable<int> _TOUR_ID;
		
		private string _STATUS_NAME;
		
		public VIEW_PAYMENT_FOREIGN_SIMPLE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FA_SR_NO", DbType="Int NOT NULL")]
		public int FA_SR_NO
		{
			get
			{
				return this._FA_SR_NO;
			}
			set
			{
				if ((this._FA_SR_NO != value))
				{
					this._FA_SR_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AGENT_ID", DbType="VarChar(50)")]
		public string AGENT_ID
		{
			get
			{
				return this._AGENT_ID;
			}
			set
			{
				if ((this._AGENT_ID != value))
				{
					this._AGENT_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_ID", DbType="Int")]
		public System.Nullable<int> BOOKING_ID
		{
			get
			{
				return this._BOOKING_ID;
			}
			set
			{
				if ((this._BOOKING_ID != value))
				{
					this._BOOKING_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FOREIGN_CURR_AGENT_ID", DbType="VarChar(50)")]
		public string FOREIGN_CURR_AGENT_ID
		{
			get
			{
				return this._FOREIGN_CURR_AGENT_ID;
			}
			set
			{
				if ((this._FOREIGN_CURR_AGENT_ID != value))
				{
					this._FOREIGN_CURR_AGENT_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PAYMENT_CURR_CODE", DbType="VarChar(50)")]
		public string PAYMENT_CURR_CODE
		{
			get
			{
				return this._PAYMENT_CURR_CODE;
			}
			set
			{
				if ((this._PAYMENT_CURR_CODE != value))
				{
					this._PAYMENT_CURR_CODE = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REC_CHEQUE_DD_NO", DbType="VarChar(50)")]
		public string REC_CHEQUE_DD_NO
		{
			get
			{
				return this._REC_CHEQUE_DD_NO;
			}
			set
			{
				if ((this._REC_CHEQUE_DD_NO != value))
				{
					this._REC_CHEQUE_DD_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AMOUNT", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> AMOUNT
		{
			get
			{
				return this._AMOUNT;
			}
			set
			{
				if ((this._AMOUNT != value))
				{
					this._AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_NAME", DbType="VarChar(50)")]
		public string BANK_NAME
		{
			get
			{
				return this._BANK_NAME;
			}
			set
			{
				if ((this._BANK_NAME != value))
				{
					this._BANK_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BANK_BRANCH_NAME", DbType="VarChar(50)")]
		public string BANK_BRANCH_NAME
		{
			get
			{
				return this._BANK_BRANCH_NAME;
			}
			set
			{
				if ((this._BANK_BRANCH_NAME != value))
				{
					this._BANK_BRANCH_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RECEIPT_NO", DbType="VarChar(25)")]
		public string RECEIPT_NO
		{
			get
			{
				return this._RECEIPT_NO;
			}
			set
			{
				if ((this._RECEIPT_NO != value))
				{
					this._RECEIPT_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RECEIPT_DATE", DbType="VarChar(10)")]
		public string RECEIPT_DATE
		{
			get
			{
				return this._RECEIPT_DATE;
			}
			set
			{
				if ((this._RECEIPT_DATE != value))
				{
					this._RECEIPT_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_USER_NAME", DbType="VarChar(50)")]
		public string USER_NAME
		{
			get
			{
				return this._USER_NAME;
			}
			set
			{
				if ((this._USER_NAME != value))
				{
					this._USER_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SERVICE_CHARGE", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> SERVICE_CHARGE
		{
			get
			{
				return this._SERVICE_CHARGE;
			}
			set
			{
				if ((this._SERVICE_CHARGE != value))
				{
					this._SERVICE_CHARGE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COMPANY_NAME", DbType="VarChar(100)")]
		public string COMPANY_NAME
		{
			get
			{
				return this._COMPANY_NAME;
			}
			set
			{
				if ((this._COMPANY_NAME != value))
				{
					this._COMPANY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOUR_ID", DbType="Int")]
		public System.Nullable<int> TOUR_ID
		{
			get
			{
				return this._TOUR_ID;
			}
			set
			{
				if ((this._TOUR_ID != value))
				{
					this._TOUR_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_NAME", DbType="VarChar(100)")]
		public string STATUS_NAME
		{
			get
			{
				return this._STATUS_NAME;
			}
			set
			{
				if ((this._STATUS_NAME != value))
				{
					this._STATUS_NAME = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
