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
	public partial class CruiseRoomAllocationDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public CruiseRoomAllocationDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["CRM"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CruiseRoomAllocationDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CruiseRoomAllocationDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CruiseRoomAllocationDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CruiseRoomAllocationDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<VIEW_BOOKING_CRUISE_DETAIL> VIEW_BOOKING_CRUISE_DETAILs
		{
			get
			{
				return this.GetTable<VIEW_BOOKING_CRUISE_DETAIL>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_CRUISE_ROOMING_LIST> VIEW_CRUISE_ROOMING_LISTs
		{
			get
			{
				return this.GetTable<VIEW_CRUISE_ROOMING_LIST>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT> VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORTs
		{
			get
			{
				return this.GetTable<VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT>();
			}
		}
		
		public System.Data.Linq.Table<VIEW_PASSENGER_DETAIL_FOR_CRUISE> VIEW_PASSENGER_DETAIL_FOR_CRUISEs
		{
			get
			{
				return this.GetTable<VIEW_PASSENGER_DETAIL_FOR_CRUISE>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_BOOKING_CRUISE_DETAILS")]
	public partial class VIEW_BOOKING_CRUISE_DETAIL
	{
		
		private string _BOOKING_REQ_TO;
		
		private string _CHECK_REQ_TO;
		
		private string _APPROVED_BY;
		
		private string _BOOKED_BY;
		
		private int _CRUISE_SR_NO;
		
		private string _DATE_OF_SAILING;
		
		private string _DATE_OF_ARRIVAL;
		
		private System.Nullable<int> _CABINE_TO_BE_BLOCKED;
		
		private string _CHECK_REQ_DATE;
		
		private string _CHECK_COMMENTS;
		
		private System.Nullable<int> _TOTAL_ROOMS_BLOCKED;
		
		private string _TIME_LIMIT;
		
		private string _BOOKING_REQ_DATE;
		
		private System.Nullable<decimal> _TOTAL_AMOUNT;
		
		private System.Nullable<decimal> _TAX;
		
		private System.Nullable<decimal> _GST;
		
		private System.Nullable<int> _TOTAL_CABINES_ALLOTED;
		
		private System.Nullable<int> _PARTIAL_CABINES_ALLOTED;
		
		private System.Nullable<int> _TOTAL_ADULTS_ALLOTED;
		
		private System.Nullable<int> _TOTAL_CWB_ALLOTED;
		
		private System.Nullable<int> _TOTAL_CNB_ALLOTED;
		
		private System.Nullable<int> _TOTAL_INFANT_ALLOTED;
		
		private System.Nullable<int> _AVAILABLE_CABINES;
		
		private string _CRUISE_NAME;
		
		private string _TIME_OF_DEPARTURE;
		
		private string _TIME_OF_ARRIVAL;
		
		private string _CRUISE_COMP_NAME;
		
		private string _CABINE_CATEGORY;
		
		private string _BOOKING_STATUS_NAME;
		
		private System.Nullable<int> _TOUR_ID;
		
		private System.Nullable<int> _BOOKING_CRUISE_SRNO;
		
		private System.Nullable<int> _BOOKING_DETAIL_ID;
		
		public VIEW_BOOKING_CRUISE_DETAIL()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_REQ_TO", DbType="VarChar(101)")]
		public string BOOKING_REQ_TO
		{
			get
			{
				return this._BOOKING_REQ_TO;
			}
			set
			{
				if ((this._BOOKING_REQ_TO != value))
				{
					this._BOOKING_REQ_TO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_REQ_TO", DbType="VarChar(101)")]
		public string CHECK_REQ_TO
		{
			get
			{
				return this._CHECK_REQ_TO;
			}
			set
			{
				if ((this._CHECK_REQ_TO != value))
				{
					this._CHECK_REQ_TO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPROVED_BY", DbType="VarChar(50)")]
		public string APPROVED_BY
		{
			get
			{
				return this._APPROVED_BY;
			}
			set
			{
				if ((this._APPROVED_BY != value))
				{
					this._APPROVED_BY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKED_BY", DbType="VarChar(50)")]
		public string BOOKED_BY
		{
			get
			{
				return this._BOOKED_BY;
			}
			set
			{
				if ((this._BOOKED_BY != value))
				{
					this._BOOKED_BY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_SR_NO", DbType="Int NOT NULL")]
		public int CRUISE_SR_NO
		{
			get
			{
				return this._CRUISE_SR_NO;
			}
			set
			{
				if ((this._CRUISE_SR_NO != value))
				{
					this._CRUISE_SR_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_OF_SAILING", DbType="VarChar(10)")]
		public string DATE_OF_SAILING
		{
			get
			{
				return this._DATE_OF_SAILING;
			}
			set
			{
				if ((this._DATE_OF_SAILING != value))
				{
					this._DATE_OF_SAILING = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_OF_ARRIVAL", DbType="VarChar(10)")]
		public string DATE_OF_ARRIVAL
		{
			get
			{
				return this._DATE_OF_ARRIVAL;
			}
			set
			{
				if ((this._DATE_OF_ARRIVAL != value))
				{
					this._DATE_OF_ARRIVAL = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CABINE_TO_BE_BLOCKED", DbType="Int")]
		public System.Nullable<int> CABINE_TO_BE_BLOCKED
		{
			get
			{
				return this._CABINE_TO_BE_BLOCKED;
			}
			set
			{
				if ((this._CABINE_TO_BE_BLOCKED != value))
				{
					this._CABINE_TO_BE_BLOCKED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_REQ_DATE", DbType="VarChar(10)")]
		public string CHECK_REQ_DATE
		{
			get
			{
				return this._CHECK_REQ_DATE;
			}
			set
			{
				if ((this._CHECK_REQ_DATE != value))
				{
					this._CHECK_REQ_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_COMMENTS", DbType="VarChar(500)")]
		public string CHECK_COMMENTS
		{
			get
			{
				return this._CHECK_COMMENTS;
			}
			set
			{
				if ((this._CHECK_COMMENTS != value))
				{
					this._CHECK_COMMENTS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_ROOMS_BLOCKED", DbType="Int")]
		public System.Nullable<int> TOTAL_ROOMS_BLOCKED
		{
			get
			{
				return this._TOTAL_ROOMS_BLOCKED;
			}
			set
			{
				if ((this._TOTAL_ROOMS_BLOCKED != value))
				{
					this._TOTAL_ROOMS_BLOCKED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_LIMIT", DbType="VarChar(10)")]
		public string TIME_LIMIT
		{
			get
			{
				return this._TIME_LIMIT;
			}
			set
			{
				if ((this._TIME_LIMIT != value))
				{
					this._TIME_LIMIT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_REQ_DATE", DbType="VarChar(10)")]
		public string BOOKING_REQ_DATE
		{
			get
			{
				return this._BOOKING_REQ_DATE;
			}
			set
			{
				if ((this._BOOKING_REQ_DATE != value))
				{
					this._BOOKING_REQ_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_AMOUNT", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> TOTAL_AMOUNT
		{
			get
			{
				return this._TOTAL_AMOUNT;
			}
			set
			{
				if ((this._TOTAL_AMOUNT != value))
				{
					this._TOTAL_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TAX", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> TAX
		{
			get
			{
				return this._TAX;
			}
			set
			{
				if ((this._TAX != value))
				{
					this._TAX = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GST", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> GST
		{
			get
			{
				return this._GST;
			}
			set
			{
				if ((this._GST != value))
				{
					this._GST = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CABINES_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CABINES_ALLOTED
		{
			get
			{
				return this._TOTAL_CABINES_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CABINES_ALLOTED != value))
				{
					this._TOTAL_CABINES_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PARTIAL_CABINES_ALLOTED", DbType="Int")]
		public System.Nullable<int> PARTIAL_CABINES_ALLOTED
		{
			get
			{
				return this._PARTIAL_CABINES_ALLOTED;
			}
			set
			{
				if ((this._PARTIAL_CABINES_ALLOTED != value))
				{
					this._PARTIAL_CABINES_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_ADULTS_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_ADULTS_ALLOTED
		{
			get
			{
				return this._TOTAL_ADULTS_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_ADULTS_ALLOTED != value))
				{
					this._TOTAL_ADULTS_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CWB_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CWB_ALLOTED
		{
			get
			{
				return this._TOTAL_CWB_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CWB_ALLOTED != value))
				{
					this._TOTAL_CWB_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CNB_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CNB_ALLOTED
		{
			get
			{
				return this._TOTAL_CNB_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CNB_ALLOTED != value))
				{
					this._TOTAL_CNB_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_INFANT_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_INFANT_ALLOTED
		{
			get
			{
				return this._TOTAL_INFANT_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_INFANT_ALLOTED != value))
				{
					this._TOTAL_INFANT_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AVAILABLE_CABINES", DbType="Int")]
		public System.Nullable<int> AVAILABLE_CABINES
		{
			get
			{
				return this._AVAILABLE_CABINES;
			}
			set
			{
				if ((this._AVAILABLE_CABINES != value))
				{
					this._AVAILABLE_CABINES = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_NAME", DbType="VarChar(50)")]
		public string CRUISE_NAME
		{
			get
			{
				return this._CRUISE_NAME;
			}
			set
			{
				if ((this._CRUISE_NAME != value))
				{
					this._CRUISE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_OF_DEPARTURE", DbType="VarChar(10)")]
		public string TIME_OF_DEPARTURE
		{
			get
			{
				return this._TIME_OF_DEPARTURE;
			}
			set
			{
				if ((this._TIME_OF_DEPARTURE != value))
				{
					this._TIME_OF_DEPARTURE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_OF_ARRIVAL", DbType="VarChar(10)")]
		public string TIME_OF_ARRIVAL
		{
			get
			{
				return this._TIME_OF_ARRIVAL;
			}
			set
			{
				if ((this._TIME_OF_ARRIVAL != value))
				{
					this._TIME_OF_ARRIVAL = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_COMP_NAME", DbType="VarChar(50)")]
		public string CRUISE_COMP_NAME
		{
			get
			{
				return this._CRUISE_COMP_NAME;
			}
			set
			{
				if ((this._CRUISE_COMP_NAME != value))
				{
					this._CRUISE_COMP_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CABINE_CATEGORY", DbType="VarChar(100)")]
		public string CABINE_CATEGORY
		{
			get
			{
				return this._CABINE_CATEGORY;
			}
			set
			{
				if ((this._CABINE_CATEGORY != value))
				{
					this._CABINE_CATEGORY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_STATUS_NAME", DbType="VarChar(100)")]
		public string BOOKING_STATUS_NAME
		{
			get
			{
				return this._BOOKING_STATUS_NAME;
			}
			set
			{
				if ((this._BOOKING_STATUS_NAME != value))
				{
					this._BOOKING_STATUS_NAME = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_CRUISE_SRNO", DbType="Int")]
		public System.Nullable<int> BOOKING_CRUISE_SRNO
		{
			get
			{
				return this._BOOKING_CRUISE_SRNO;
			}
			set
			{
				if ((this._BOOKING_CRUISE_SRNO != value))
				{
					this._BOOKING_CRUISE_SRNO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_DETAIL_ID", DbType="Int")]
		public System.Nullable<int> BOOKING_DETAIL_ID
		{
			get
			{
				return this._BOOKING_DETAIL_ID;
			}
			set
			{
				if ((this._BOOKING_DETAIL_ID != value))
				{
					this._BOOKING_DETAIL_ID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_CRUISE_ROOMING_LIST")]
	public partial class VIEW_CRUISE_ROOMING_LIST
	{
		
		private int _SR_NO;
		
		private System.Nullable<int> _BOOKING_CRUISE_SRNO;
		
		private System.Nullable<int> _DECK_NO;
		
		private System.Nullable<int> _CABINE_NO;
		
		private string _ADULT1;
		
		private string _ADULT2;
		
		private string _ADULT3;
		
		private string _CWB;
		
		private string _CNB1;
		
		private string _CNB2;
		
		private string _INFANT;
		
		public VIEW_CRUISE_ROOMING_LIST()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_CRUISE_SRNO", DbType="Int")]
		public System.Nullable<int> BOOKING_CRUISE_SRNO
		{
			get
			{
				return this._BOOKING_CRUISE_SRNO;
			}
			set
			{
				if ((this._BOOKING_CRUISE_SRNO != value))
				{
					this._BOOKING_CRUISE_SRNO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DECK_NO", DbType="Int")]
		public System.Nullable<int> DECK_NO
		{
			get
			{
				return this._DECK_NO;
			}
			set
			{
				if ((this._DECK_NO != value))
				{
					this._DECK_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CABINE_NO", DbType="Int")]
		public System.Nullable<int> CABINE_NO
		{
			get
			{
				return this._CABINE_NO;
			}
			set
			{
				if ((this._CABINE_NO != value))
				{
					this._CABINE_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADULT1", DbType="VarChar(500)")]
		public string ADULT1
		{
			get
			{
				return this._ADULT1;
			}
			set
			{
				if ((this._ADULT1 != value))
				{
					this._ADULT1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADULT2", DbType="VarChar(500)")]
		public string ADULT2
		{
			get
			{
				return this._ADULT2;
			}
			set
			{
				if ((this._ADULT2 != value))
				{
					this._ADULT2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ADULT3", DbType="VarChar(500)")]
		public string ADULT3
		{
			get
			{
				return this._ADULT3;
			}
			set
			{
				if ((this._ADULT3 != value))
				{
					this._ADULT3 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CWB", DbType="VarChar(500)")]
		public string CWB
		{
			get
			{
				return this._CWB;
			}
			set
			{
				if ((this._CWB != value))
				{
					this._CWB = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CNB1", DbType="VarChar(500)")]
		public string CNB1
		{
			get
			{
				return this._CNB1;
			}
			set
			{
				if ((this._CNB1 != value))
				{
					this._CNB1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CNB2", DbType="VarChar(500)")]
		public string CNB2
		{
			get
			{
				return this._CNB2;
			}
			set
			{
				if ((this._CNB2 != value))
				{
					this._CNB2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_INFANT", DbType="VarChar(500)")]
		public string INFANT
		{
			get
			{
				return this._INFANT;
			}
			set
			{
				if ((this._INFANT != value))
				{
					this._INFANT = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT")]
	public partial class VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT
	{
		
		private string _BOOKING_REQ_TO;
		
		private string _CHECK_REQ_TO;
		
		private string _APPROVED_BY;
		
		private string _BOOKED_BY;
		
		private int _CRUISE_SR_NO;
		
		private string _DATE_OF_SAILING;
		
		private string _DATE_OF_ARRIVAL;
		
		private System.Nullable<int> _CABINE_TO_BE_BLOCKED;
		
		private string _CHECK_REQ_DATE;
		
		private string _CHECK_COMMENTS;
		
		private System.Nullable<int> _TOTAL_ROOMS_BLOCKED;
		
		private string _TIME_LIMIT;
		
		private string _BOOKING_REQ_DATE;
		
		private System.Nullable<decimal> _TOTAL_AMOUNT;
		
		private System.Nullable<decimal> _TAX;
		
		private System.Nullable<decimal> _GST;
		
		private System.Nullable<int> _TOTAL_CABINES_ALLOTED;
		
		private System.Nullable<int> _PARTIAL_CABINES_ALLOTED;
		
		private System.Nullable<int> _TOTAL_ADULTS_ALLOTED;
		
		private System.Nullable<int> _TOTAL_CWB_ALLOTED;
		
		private System.Nullable<int> _TOTAL_CNB_ALLOTED;
		
		private System.Nullable<int> _TOTAL_INFANT_ALLOTED;
		
		private System.Nullable<int> _AVAILABLE_CABINES;
		
		private string _CRUISE_NAME;
		
		private string _TIME_OF_DEPARTURE;
		
		private string _TIME_OF_ARRIVAL;
		
		private string _CRUISE_COMP_NAME;
		
		private string _CABINE_CATEGORY;
		
		private string _BOOKING_STATUS_NAME;
		
		private System.Nullable<int> _TOUR_ID;
		
		public VIEW_BOOKING_CRUISE_DETAIL_FOR_REPORT()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_REQ_TO", DbType="VarChar(101)")]
		public string BOOKING_REQ_TO
		{
			get
			{
				return this._BOOKING_REQ_TO;
			}
			set
			{
				if ((this._BOOKING_REQ_TO != value))
				{
					this._BOOKING_REQ_TO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_REQ_TO", DbType="VarChar(101)")]
		public string CHECK_REQ_TO
		{
			get
			{
				return this._CHECK_REQ_TO;
			}
			set
			{
				if ((this._CHECK_REQ_TO != value))
				{
					this._CHECK_REQ_TO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_APPROVED_BY", DbType="VarChar(50)")]
		public string APPROVED_BY
		{
			get
			{
				return this._APPROVED_BY;
			}
			set
			{
				if ((this._APPROVED_BY != value))
				{
					this._APPROVED_BY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKED_BY", DbType="VarChar(50)")]
		public string BOOKED_BY
		{
			get
			{
				return this._BOOKED_BY;
			}
			set
			{
				if ((this._BOOKED_BY != value))
				{
					this._BOOKED_BY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_SR_NO", DbType="Int NOT NULL")]
		public int CRUISE_SR_NO
		{
			get
			{
				return this._CRUISE_SR_NO;
			}
			set
			{
				if ((this._CRUISE_SR_NO != value))
				{
					this._CRUISE_SR_NO = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_OF_SAILING", DbType="VarChar(10)")]
		public string DATE_OF_SAILING
		{
			get
			{
				return this._DATE_OF_SAILING;
			}
			set
			{
				if ((this._DATE_OF_SAILING != value))
				{
					this._DATE_OF_SAILING = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_OF_ARRIVAL", DbType="VarChar(10)")]
		public string DATE_OF_ARRIVAL
		{
			get
			{
				return this._DATE_OF_ARRIVAL;
			}
			set
			{
				if ((this._DATE_OF_ARRIVAL != value))
				{
					this._DATE_OF_ARRIVAL = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CABINE_TO_BE_BLOCKED", DbType="Int")]
		public System.Nullable<int> CABINE_TO_BE_BLOCKED
		{
			get
			{
				return this._CABINE_TO_BE_BLOCKED;
			}
			set
			{
				if ((this._CABINE_TO_BE_BLOCKED != value))
				{
					this._CABINE_TO_BE_BLOCKED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_REQ_DATE", DbType="VarChar(10)")]
		public string CHECK_REQ_DATE
		{
			get
			{
				return this._CHECK_REQ_DATE;
			}
			set
			{
				if ((this._CHECK_REQ_DATE != value))
				{
					this._CHECK_REQ_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CHECK_COMMENTS", DbType="VarChar(500)")]
		public string CHECK_COMMENTS
		{
			get
			{
				return this._CHECK_COMMENTS;
			}
			set
			{
				if ((this._CHECK_COMMENTS != value))
				{
					this._CHECK_COMMENTS = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_ROOMS_BLOCKED", DbType="Int")]
		public System.Nullable<int> TOTAL_ROOMS_BLOCKED
		{
			get
			{
				return this._TOTAL_ROOMS_BLOCKED;
			}
			set
			{
				if ((this._TOTAL_ROOMS_BLOCKED != value))
				{
					this._TOTAL_ROOMS_BLOCKED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_LIMIT", DbType="VarChar(10)")]
		public string TIME_LIMIT
		{
			get
			{
				return this._TIME_LIMIT;
			}
			set
			{
				if ((this._TIME_LIMIT != value))
				{
					this._TIME_LIMIT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_REQ_DATE", DbType="VarChar(10)")]
		public string BOOKING_REQ_DATE
		{
			get
			{
				return this._BOOKING_REQ_DATE;
			}
			set
			{
				if ((this._BOOKING_REQ_DATE != value))
				{
					this._BOOKING_REQ_DATE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_AMOUNT", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> TOTAL_AMOUNT
		{
			get
			{
				return this._TOTAL_AMOUNT;
			}
			set
			{
				if ((this._TOTAL_AMOUNT != value))
				{
					this._TOTAL_AMOUNT = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TAX", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> TAX
		{
			get
			{
				return this._TAX;
			}
			set
			{
				if ((this._TAX != value))
				{
					this._TAX = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GST", DbType="Decimal(12,2)")]
		public System.Nullable<decimal> GST
		{
			get
			{
				return this._GST;
			}
			set
			{
				if ((this._GST != value))
				{
					this._GST = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CABINES_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CABINES_ALLOTED
		{
			get
			{
				return this._TOTAL_CABINES_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CABINES_ALLOTED != value))
				{
					this._TOTAL_CABINES_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PARTIAL_CABINES_ALLOTED", DbType="Int")]
		public System.Nullable<int> PARTIAL_CABINES_ALLOTED
		{
			get
			{
				return this._PARTIAL_CABINES_ALLOTED;
			}
			set
			{
				if ((this._PARTIAL_CABINES_ALLOTED != value))
				{
					this._PARTIAL_CABINES_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_ADULTS_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_ADULTS_ALLOTED
		{
			get
			{
				return this._TOTAL_ADULTS_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_ADULTS_ALLOTED != value))
				{
					this._TOTAL_ADULTS_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CWB_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CWB_ALLOTED
		{
			get
			{
				return this._TOTAL_CWB_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CWB_ALLOTED != value))
				{
					this._TOTAL_CWB_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_CNB_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_CNB_ALLOTED
		{
			get
			{
				return this._TOTAL_CNB_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_CNB_ALLOTED != value))
				{
					this._TOTAL_CNB_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TOTAL_INFANT_ALLOTED", DbType="Int")]
		public System.Nullable<int> TOTAL_INFANT_ALLOTED
		{
			get
			{
				return this._TOTAL_INFANT_ALLOTED;
			}
			set
			{
				if ((this._TOTAL_INFANT_ALLOTED != value))
				{
					this._TOTAL_INFANT_ALLOTED = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AVAILABLE_CABINES", DbType="Int")]
		public System.Nullable<int> AVAILABLE_CABINES
		{
			get
			{
				return this._AVAILABLE_CABINES;
			}
			set
			{
				if ((this._AVAILABLE_CABINES != value))
				{
					this._AVAILABLE_CABINES = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_NAME", DbType="VarChar(50)")]
		public string CRUISE_NAME
		{
			get
			{
				return this._CRUISE_NAME;
			}
			set
			{
				if ((this._CRUISE_NAME != value))
				{
					this._CRUISE_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_OF_DEPARTURE", DbType="VarChar(10)")]
		public string TIME_OF_DEPARTURE
		{
			get
			{
				return this._TIME_OF_DEPARTURE;
			}
			set
			{
				if ((this._TIME_OF_DEPARTURE != value))
				{
					this._TIME_OF_DEPARTURE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TIME_OF_ARRIVAL", DbType="VarChar(10)")]
		public string TIME_OF_ARRIVAL
		{
			get
			{
				return this._TIME_OF_ARRIVAL;
			}
			set
			{
				if ((this._TIME_OF_ARRIVAL != value))
				{
					this._TIME_OF_ARRIVAL = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CRUISE_COMP_NAME", DbType="VarChar(50)")]
		public string CRUISE_COMP_NAME
		{
			get
			{
				return this._CRUISE_COMP_NAME;
			}
			set
			{
				if ((this._CRUISE_COMP_NAME != value))
				{
					this._CRUISE_COMP_NAME = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CABINE_CATEGORY", DbType="VarChar(100)")]
		public string CABINE_CATEGORY
		{
			get
			{
				return this._CABINE_CATEGORY;
			}
			set
			{
				if ((this._CABINE_CATEGORY != value))
				{
					this._CABINE_CATEGORY = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_STATUS_NAME", DbType="VarChar(100)")]
		public string BOOKING_STATUS_NAME
		{
			get
			{
				return this._BOOKING_STATUS_NAME;
			}
			set
			{
				if ((this._BOOKING_STATUS_NAME != value))
				{
					this._BOOKING_STATUS_NAME = value;
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
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VIEW_PASSENGER_DETAIL_FOR_CRUISE")]
	public partial class VIEW_PASSENGER_DETAIL_FOR_CRUISE
	{
		
		private int _BOOKING_DETAIL_ID;
		
		private System.Nullable<int> _BOOKING_ID;
		
		private string _TITLE_DESC;
		
		private string _CUST_NAME;
		
		private string _CUST_SURNAME;
		
		private System.Nullable<int> _CUST_REL_SRNO;
		
		private string _SHARE_ROOM_IN_CRUISE;
		
		private System.Nullable<bool> _IS_CHECKED;
		
		public VIEW_PASSENGER_DETAIL_FOR_CRUISE()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BOOKING_DETAIL_ID", DbType="Int NOT NULL")]
		public int BOOKING_DETAIL_ID
		{
			get
			{
				return this._BOOKING_DETAIL_ID;
			}
			set
			{
				if ((this._BOOKING_DETAIL_ID != value))
				{
					this._BOOKING_DETAIL_ID = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SHARE_ROOM_IN_CRUISE", DbType="VarChar(50)")]
		public string SHARE_ROOM_IN_CRUISE
		{
			get
			{
				return this._SHARE_ROOM_IN_CRUISE;
			}
			set
			{
				if ((this._SHARE_ROOM_IN_CRUISE != value))
				{
					this._SHARE_ROOM_IN_CRUISE = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IS_CHECKED", DbType="Bit")]
		public System.Nullable<bool> IS_CHECKED
		{
			get
			{
				return this._IS_CHECKED;
			}
			set
			{
				if ((this._IS_CHECKED != value))
				{
					this._IS_CHECKED = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
