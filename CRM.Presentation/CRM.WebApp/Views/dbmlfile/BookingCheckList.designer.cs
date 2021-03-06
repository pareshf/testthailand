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
	public partial class BookingCheckListDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public BookingCheckListDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BookingCheckListDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingCheckListDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingCheckListDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BookingCheckListDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_FOR_BOOKING_CHECKLIST_MASTER> VIEW_FOR_BOOKING_CHECKLIST_MASTERs
		{
			get
			{
				return this.GetTable<VIEW_FOR_BOOKING_CHECKLIST_MASTER>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_FOR_BOOKING_CHECKLIST_DETAIL> VIEW_FOR_BOOKING_CHECKLIST_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_FOR_BOOKING_CHECKLIST_DETAIL>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_FOR_BOOKING_CHECKLIST_MASTER")]
	public partial class VIEW_FOR_BOOKING_CHECKLIST_MASTER
	{
		
		private int _CHECKLIST_ID;
		
		private string _CHECKLIST_FOR;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _DESCRIPTION;
		
		private string _DEPARTMENT_NAME;
		
		public VIEW_FOR_BOOKING_CHECKLIST_MASTER()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKLIST_ID", DbType="Int NOT NULL")]
		public int CHECKLIST_ID
		{
			get
			{
				return this._CHECKLIST_ID;
			}
			set
			{
				if ((this._CHECKLIST_ID != value))
				{
					this._CHECKLIST_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKLIST_FOR", DbType="VarChar(255)")]
		public string CHECKLIST_FOR
		{
			get
			{
				return this._CHECKLIST_FOR;
			}
			set
			{
				if ((this._CHECKLIST_FOR != value))
				{
					this._CHECKLIST_FOR = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRIPTION", DbType="VarChar(MAX)")]
		public string DESCRIPTION
		{
			get
			{
				return this._DESCRIPTION;
			}
			set
			{
				if ((this._DESCRIPTION != value))
				{
					this._DESCRIPTION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DEPARTMENT_NAME", DbType="VarChar(50)")]
		public string DEPARTMENT_NAME
		{
			get
			{
				return this._DEPARTMENT_NAME;
			}
			set
			{
				if ((this._DEPARTMENT_NAME != value))
				{
					this._DEPARTMENT_NAME = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_FOR_BOOKING_CHECKLIST_DETAILS")]
	public partial class VIEW_FOR_BOOKING_CHECKLIST_DETAIL
	{
		
		private int _CHECKLIST_ID;
		
		private System.Nullable<int> _SR_NO;
		
		private string _CHECKLIST_DESCRIPTION;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _PRIORITY_NAME;
		
		private string _IMPORTANCE;
		
		public VIEW_FOR_BOOKING_CHECKLIST_DETAIL()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECKLIST_ID", DbType="Int NOT NULL")]
		public int CHECKLIST_ID
		{
			get
			{
				return this._CHECKLIST_ID;
			}
			set
			{
				if ((this._CHECKLIST_ID != value))
				{
					this._CHECKLIST_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SR_NO", DbType="Int")]
		public System.Nullable<int> SR_NO
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PRIORITY_NAME", DbType="VarChar(50)")]
		public string PRIORITY_NAME
		{
			get
			{
				return this._PRIORITY_NAME;
			}
			set
			{
				if ((this._PRIORITY_NAME != value))
				{
					this._PRIORITY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IMPORTANCE", DbType="VarChar(50)")]
		public string IMPORTANCE
		{
			get
			{
				return this._IMPORTANCE;
			}
			set
			{
				if ((this._IMPORTANCE != value))
				{
					this._IMPORTANCE = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
