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
	public partial class TourCheckListDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public TourCheckListDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TourCheckListDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TourCheckListDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TourCheckListDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TourCheckListDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_BOOKING_DETAIL_FOR_CHECKLIST> VIEW_BOOKING_DETAIL_FOR_CHECKLISTs
		{
			get
			{
				return this.GetTable<VIEW_BOOKING_DETAIL_FOR_CHECKLIST>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_PASSENGER_CHECKLIST_DETAIL> VIEW_PASSENGER_CHECKLIST_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_PASSENGER_CHECKLIST_DETAIL>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_BOOKING_DETAIL_FOR_CHECKLIST")]
	public partial class VIEW_BOOKING_DETAIL_FOR_CHECKLIST
	{
		
		private System.Nullable<int> _TOUR_ID;
		
		private int _BOOKING_ID;
		
		private string _CUST_SURNAME;
		
		private string _CUST_NAME;
		
		private string _TITLE_DESC;
		
		public VIEW_BOOKING_DETAIL_FOR_CHECKLIST()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_ID", DbType="Int NOT NULL")]
		public int BOOKING_ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_SURNAME", DbType="VarChar(50)")]
		public string CUST_SURNAME
		{
			get
			{
				return this._CUST_SURNAME;
			}
			set
			{
				if ((this._CUST_SURNAME != value))
				{
					this._CUST_SURNAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_NAME", DbType="VarChar(255)")]
		public string CUST_NAME
		{
			get
			{
				return this._CUST_NAME;
			}
			set
			{
				if ((this._CUST_NAME != value))
				{
					this._CUST_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TITLE_DESC", DbType="VarChar(10)")]
		public string TITLE_DESC
		{
			get
			{
				return this._TITLE_DESC;
			}
			set
			{
				if ((this._TITLE_DESC != value))
				{
					this._TITLE_DESC = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PASSENGER_CHECKLIST_DETAIL")]
	public partial class VIEW_PASSENGER_CHECKLIST_DETAIL
	{
		
		private System.Nullable<int> _BOOKING_ID;
		
		private string _CHECKED;
		
		private string _CHECKLIST_DESCRIPTION;
		
		private System.Nullable<int> _CHECKLIST_DETAIL_ID;
		
		private int _SR_NO;
		
		public VIEW_PASSENGER_CHECKLIST_DETAIL()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKED", DbType="VarChar(3) NOT NULL", CanBeNull=false)]
		public string CHECKED
		{
			get
			{
				return this._CHECKED;
			}
			set
			{
				if ((this._CHECKED != value))
				{
					this._CHECKED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKLIST_DESCRIPTION", DbType="VarChar(255)")]
		public string CHECKLIST_DESCRIPTION
		{
			get
			{
				return this._CHECKLIST_DESCRIPTION;
			}
			set
			{
				if ((this._CHECKLIST_DESCRIPTION != value))
				{
					this._CHECKLIST_DESCRIPTION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKLIST_DETAIL_ID", DbType="Int")]
		public System.Nullable<int> CHECKLIST_DETAIL_ID
		{
			get
			{
				return this._CHECKLIST_DETAIL_ID;
			}
			set
			{
				if ((this._CHECKLIST_DETAIL_ID != value))
				{
					this._CHECKLIST_DETAIL_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SR_NO", DbType="Int NOT NULL")]
		public int SR_NO
		{
			get
			{
				return this._SR_NO;
			}
			set
			{
				if ((this._SR_NO != value))
				{
					this._SR_NO = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
