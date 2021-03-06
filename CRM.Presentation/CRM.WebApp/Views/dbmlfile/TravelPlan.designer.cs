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
	public partial class TravelPlanDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public TravelPlanDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TravelPlanDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TravelPlanDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TravelPlanDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TravelPlanDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW> VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEWs
		{
			get
			{
				return this.GetTable<VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_CUSTOMER_VISA_DETAIL> VIEW_CUSTOMER_VISA_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_CUSTOMER_VISA_DETAIL>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US> VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US
		{
			get
			{
				return this.GetTable<VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_AIRLINE_DETAIL> VIEW_AIRLINE_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_AIRLINE_DETAIL>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_CUSTOMER_TRAVEL_WITH_OTHER> VIEW_CUSTOMER_TRAVEL_WITH_OTHERs
		{
			get
			{
				return this.GetTable<VIEW_CUSTOMER_TRAVEL_WITH_OTHER>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW")]
	public partial class VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW
	{
		
		private System.Nullable<int> _CUST_ID;
		
		private int _SR_NO;
		
		private string _NEXT_TRAVEL_PLAN_DATE;
		
		private System.Nullable<int> _NO_OF_PERSONS;
		
		private string _DESCRIPTION;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _INQUIRY_STATUS_NAME;
		
		private string _TOUR_SHORT_NAME;
		
		private string _TOUR_TYPE_NAME;
		
		private string _REGION_SHORT_NAME;
		
		private string _STATUS_NAME;
		
		private string _STATE_NAME;
		
		private string _COUNTRY_NAME;
		
		public VIEW_FOR_CUSTOMER_NEXT_TRAVEL_PLAN_NEW()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NEXT_TRAVEL_PLAN_DATE", DbType="VarChar(10)")]
		public string NEXT_TRAVEL_PLAN_DATE
		{
			get
			{
				return this._NEXT_TRAVEL_PLAN_DATE;
			}
			set
			{
				if ((this._NEXT_TRAVEL_PLAN_DATE != value))
				{
					this._NEXT_TRAVEL_PLAN_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NO_OF_PERSONS", DbType="Int")]
		public System.Nullable<int> NO_OF_PERSONS
		{
			get
			{
				return this._NO_OF_PERSONS;
			}
			set
			{
				if ((this._NO_OF_PERSONS != value))
				{
					this._NO_OF_PERSONS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRIPTION", DbType="VarChar(255)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_INQUIRY_STATUS_NAME", DbType="VarChar(100)")]
		public string INQUIRY_STATUS_NAME
		{
			get
			{
				return this._INQUIRY_STATUS_NAME;
			}
			set
			{
				if ((this._INQUIRY_STATUS_NAME != value))
				{
					this._INQUIRY_STATUS_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOUR_SHORT_NAME", DbType="VarChar(50)")]
		public string TOUR_SHORT_NAME
		{
			get
			{
				return this._TOUR_SHORT_NAME;
			}
			set
			{
				if ((this._TOUR_SHORT_NAME != value))
				{
					this._TOUR_SHORT_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOUR_TYPE_NAME", DbType="VarChar(50)")]
		public string TOUR_TYPE_NAME
		{
			get
			{
				return this._TOUR_TYPE_NAME;
			}
			set
			{
				if ((this._TOUR_TYPE_NAME != value))
				{
					this._TOUR_TYPE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REGION_SHORT_NAME", DbType="NVarChar(25)")]
		public string REGION_SHORT_NAME
		{
			get
			{
				return this._REGION_SHORT_NAME;
			}
			set
			{
				if ((this._REGION_SHORT_NAME != value))
				{
					this._REGION_SHORT_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATUS_NAME", DbType="VarChar(25)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_STATE_NAME", DbType="VarChar(50)")]
		public string STATE_NAME
		{
			get
			{
				return this._STATE_NAME;
			}
			set
			{
				if ((this._STATE_NAME != value))
				{
					this._STATE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COUNTRY_NAME", DbType="VarChar(255)")]
		public string COUNTRY_NAME
		{
			get
			{
				return this._COUNTRY_NAME;
			}
			set
			{
				if ((this._COUNTRY_NAME != value))
				{
					this._COUNTRY_NAME = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_CUSTOMER_VISA_DETAIL")]
	public partial class VIEW_CUSTOMER_VISA_DETAIL
	{
		
		private System.Nullable<int> _CUST_ID;
		
		private System.Nullable<int> _CUST_REL_ID;
		
		private int _SR_NO;
		
		private string _VISA_EXPIRY_DATE;
		
		private System.Nullable<int> _CUST_REL_SRNO;
		
		private string _COUNTRY_NAME;
		
		public VIEW_CUSTOMER_VISA_DETAIL()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_REL_ID", DbType="Int")]
		public System.Nullable<int> CUST_REL_ID
		{
			get
			{
				return this._CUST_REL_ID;
			}
			set
			{
				if ((this._CUST_REL_ID != value))
				{
					this._CUST_REL_ID = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VISA_EXPIRY_DATE", DbType="VarChar(10)")]
		public string VISA_EXPIRY_DATE
		{
			get
			{
				return this._VISA_EXPIRY_DATE;
			}
			set
			{
				if ((this._VISA_EXPIRY_DATE != value))
				{
					this._VISA_EXPIRY_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_REL_SRNO", DbType="Int")]
		public System.Nullable<int> CUST_REL_SRNO
		{
			get
			{
				return this._CUST_REL_SRNO;
			}
			set
			{
				if ((this._CUST_REL_SRNO != value))
				{
					this._CUST_REL_SRNO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COUNTRY_NAME", DbType="VarChar(50)")]
		public string COUNTRY_NAME
		{
			get
			{
				return this._COUNTRY_NAME;
			}
			set
			{
				if ((this._COUNTRY_NAME != value))
				{
					this._COUNTRY_NAME = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US")]
	public partial class VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US
	{
		
		private int _CUST_ID;
		
		private int _SR_NO;
		
		private string _CUSTOMER_TRAVEL_DATE;
		
		private string _DESCRIPTION;
		
		private System.Nullable<int> _NO_OF_PERSONS;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _COUNTRY_ID;
		
		public VIEW_CUSTOMER_TRAVEL_HISTORY_WITH_US()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_ID", DbType="Int NOT NULL")]
		public int CUST_ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUSTOMER_TRAVEL_DATE", DbType="VarChar(10)")]
		public string CUSTOMER_TRAVEL_DATE
		{
			get
			{
				return this._CUSTOMER_TRAVEL_DATE;
			}
			set
			{
				if ((this._CUSTOMER_TRAVEL_DATE != value))
				{
					this._CUSTOMER_TRAVEL_DATE = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NO_OF_PERSONS", DbType="Int")]
		public System.Nullable<int> NO_OF_PERSONS
		{
			get
			{
				return this._NO_OF_PERSONS;
			}
			set
			{
				if ((this._NO_OF_PERSONS != value))
				{
					this._NO_OF_PERSONS = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COUNTRY_ID", DbType="VarChar(50)")]
		public string COUNTRY_ID
		{
			get
			{
				return this._COUNTRY_ID;
			}
			set
			{
				if ((this._COUNTRY_ID != value))
				{
					this._COUNTRY_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_AIRLINE_DETAIL")]
	public partial class VIEW_AIRLINE_DETAIL
	{
		
		private System.Nullable<int> _CUST_ID;
		
		private System.Nullable<int> _CUST_REL_ID;
		
		private int _SR_NO;
		
		private System.Nullable<int> _PREF_AIRLINE_ID;
		
		private System.Nullable<int> _PREF_CLASS_ID;
		
		private string _FREQUENTLY_FLY_NO;
		
		private string _CORPORATE_CLIENT_NO;
		
		private string _CUST_NAME;
		
		private string _AIRLINE_NAME;
		
		private string _CLASS_NAME;
		
		private System.Nullable<int> _CUST_REL_SRNO;
		
		public VIEW_AIRLINE_DETAIL()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_REL_ID", DbType="Int")]
		public System.Nullable<int> CUST_REL_ID
		{
			get
			{
				return this._CUST_REL_ID;
			}
			set
			{
				if ((this._CUST_REL_ID != value))
				{
					this._CUST_REL_ID = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PREF_AIRLINE_ID", DbType="Int")]
		public System.Nullable<int> PREF_AIRLINE_ID
		{
			get
			{
				return this._PREF_AIRLINE_ID;
			}
			set
			{
				if ((this._PREF_AIRLINE_ID != value))
				{
					this._PREF_AIRLINE_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PREF_CLASS_ID", DbType="Int")]
		public System.Nullable<int> PREF_CLASS_ID
		{
			get
			{
				return this._PREF_CLASS_ID;
			}
			set
			{
				if ((this._PREF_CLASS_ID != value))
				{
					this._PREF_CLASS_ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FREQUENTLY_FLY_NO", DbType="NVarChar(50)")]
		public string FREQUENTLY_FLY_NO
		{
			get
			{
				return this._FREQUENTLY_FLY_NO;
			}
			set
			{
				if ((this._FREQUENTLY_FLY_NO != value))
				{
					this._FREQUENTLY_FLY_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CORPORATE_CLIENT_NO", DbType="NVarChar(50)")]
		public string CORPORATE_CLIENT_NO
		{
			get
			{
				return this._CORPORATE_CLIENT_NO;
			}
			set
			{
				if ((this._CORPORATE_CLIENT_NO != value))
				{
					this._CORPORATE_CLIENT_NO = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AIRLINE_NAME", DbType="VarChar(50)")]
		public string AIRLINE_NAME
		{
			get
			{
				return this._AIRLINE_NAME;
			}
			set
			{
				if ((this._AIRLINE_NAME != value))
				{
					this._AIRLINE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CLASS_NAME", DbType="VarChar(50)")]
		public string CLASS_NAME
		{
			get
			{
				return this._CLASS_NAME;
			}
			set
			{
				if ((this._CLASS_NAME != value))
				{
					this._CLASS_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_REL_SRNO", DbType="Int")]
		public System.Nullable<int> CUST_REL_SRNO
		{
			get
			{
				return this._CUST_REL_SRNO;
			}
			set
			{
				if ((this._CUST_REL_SRNO != value))
				{
					this._CUST_REL_SRNO = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_CUSTOMER_TRAVEL_WITH_OTHER")]
	public partial class VIEW_CUSTOMER_TRAVEL_WITH_OTHER
	{
		
		private int _CUST_ID;
		
		private int _SR_NO;
		
		private string _TRAVEL_DATE;
		
		private string _DESCRIPATION;
		
		private System.Nullable<int> _NO_OF_PERSON;
		
		private System.Nullable<System.DateTime> _DATE_CREATED;
		
		private System.Nullable<int> _CREATED_BY;
		
		private System.Nullable<System.DateTime> _DATE_MODIFIED;
		
		private System.Nullable<int> _MODIFIED_BY;
		
		private string _COUNTRY_NAME;
		
		private string _AGENT_NAME;
		
		public VIEW_CUSTOMER_TRAVEL_WITH_OTHER()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CUST_ID", DbType="Int NOT NULL")]
		public int CUST_ID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TRAVEL_DATE", DbType="VarChar(10)")]
		public string TRAVEL_DATE
		{
			get
			{
				return this._TRAVEL_DATE;
			}
			set
			{
				if ((this._TRAVEL_DATE != value))
				{
					this._TRAVEL_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DESCRIPATION", DbType="VarChar(255)")]
		public string DESCRIPATION
		{
			get
			{
				return this._DESCRIPATION;
			}
			set
			{
				if ((this._DESCRIPATION != value))
				{
					this._DESCRIPATION = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NO_OF_PERSON", DbType="Int")]
		public System.Nullable<int> NO_OF_PERSON
		{
			get
			{
				return this._NO_OF_PERSON;
			}
			set
			{
				if ((this._NO_OF_PERSON != value))
				{
					this._NO_OF_PERSON = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_COUNTRY_NAME", DbType="VarChar(50)")]
		public string COUNTRY_NAME
		{
			get
			{
				return this._COUNTRY_NAME;
			}
			set
			{
				if ((this._COUNTRY_NAME != value))
				{
					this._COUNTRY_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AGENT_NAME", DbType="VarChar(100)")]
		public string AGENT_NAME
		{
			get
			{
				return this._AGENT_NAME;
			}
			set
			{
				if ((this._AGENT_NAME != value))
				{
					this._AGENT_NAME = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
