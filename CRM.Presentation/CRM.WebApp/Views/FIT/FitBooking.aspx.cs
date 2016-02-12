using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.DataAccess.FIT;
using System.Data;
using System.Data.Common;
using System.Collections;


namespace CRM.WebApp.Views.FIT
{
    public partial class FitBooking : System.Web.UI.Page
    {

        FitBookingStoreProcedure objFitBookingStoreProcedure = new FitBookingStoreProcedure();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["usersid"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindMyCart();
            upHotelCart.Update();
            upCruiseCart.Update();
            upSightCart.Update();
            upTransferCart.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtFromDateSearch.Text == "From" || txtToDateSearch.Text == "To" || txtCity.Text == "")
            {
                if (txtFromDateSearch.Text == "From")
                {
                    Master.DisplayMessage("From Date Is Required", "successMessage", 2000);
                }
                else if (txtToDateSearch.Text == "To")
                {
                    Master.DisplayMessage("To Date Is Required", "successMessage", 2000);
                }
                else if (txtCity.Text == "")
                {
                    Master.DisplayMessage("City Is Required", "successMessage", 2000);
                }
                else
                {
                    Master.DisplayMessage("Please Fill All Required Data", "successMessage", 2000);
                }

            }
            else
            {
                //Fetch data according to check box
                if (chkAll.Checked == true)
                {
                    fetchHotelData();
                    fetchCruiseData();
                    fetchSightData();
                    fetchTransferData();
                }
                else
                {
                    if (chkHotel.Checked == true)
                    {
                        fetchHotelData();
                    }
                    if (chkCruise.Checked == true)
                    {
                        fetchCruiseData();
                    }
                    if (chkSight.Checked == true)
                    {
                        fetchSightData();
                    }
                    if (chkTransferPackage.Checked == true)
                    {
                        fetchTransferData();
                    }
                }

                //Update the panels
                upSearchResultCount.Update();
                upHotelDetail.Update();
                upCruiseDetail.Update();
                upSightSeeing.Update();
                upTransferPackage.Update();
            }
        }

        #region Search Methods
        protected void fetchHotelData()
        {
            lblHotelDetail.Visible = true;

            DataTable dt = objFitBookingStoreProcedure.fetchHotelDetailForSearch(txtFromDateSearch.Text, txtToDateSearch.Text, txtCity.Text, txtSearch.Text);

            dlHotelDetails.DataSource = dt;
            dlHotelDetails.DataBind();

            lblHotelSearchCount.Text = dt.Rows.Count.ToString();
            //lblNoOfHotel.Visible = true;
            lblHotelSearchCount.Visible = true;
            lblSearchResult.Visible = true;
            searchCount.Visible = true;
        }

        protected void fetchCruiseData()
        {
            lblCruiseDetail.Visible = true;

            DataTable dt = objFitBookingStoreProcedure.fetchCruiseDetailForSearch(txtFromDateSearch.Text, txtToDateSearch.Text, txtCity.Text, txtSearch.Text);

            dlCruiseDetail.DataSource = dt;
            dlCruiseDetail.DataBind();

            lblCruiseSearchCount.Text = dt.Rows.Count.ToString();
            //lblNoOfCruise.Visible = true;
            lblCruiseSearchCount.Visible = true;
            lblSearchResult.Visible = true;
            searchCount.Visible = true;
        }

        protected void fetchSightData()
        {
            lblSightSeeingDetail.Visible = true;

            DataTable dt = objFitBookingStoreProcedure.fetchSightDetailForSearch(txtFromDateSearch.Text, txtToDateSearch.Text, txtCity.Text, txtSearch.Text);

            dlSightSeeing.DataSource = dt;
            dlSightSeeing.DataBind();

            lblSightSearchCount.Text = dt.Rows.Count.ToString();
            //lblNoOfSight.Visible = true;
            lblSightSearchCount.Visible = true;
            lblSearchResult.Visible = true;
        }

        protected void fetchTransferData()
        {
            lblTransferPackageDetail.Visible = true;

            DataTable dt = objFitBookingStoreProcedure.fetchTransferDetailForSearch(txtFromDateSearch.Text, txtToDateSearch.Text, txtCity.Text);

            dlTransferPackage.DataSource = dt;
            dlTransferPackage.DataBind();

            lblTransferSearchCount.Text = dt.Rows.Count.ToString();
            //lblNoOfTransfer.Visible = true;
            lblTransferSearchCount.Visible = true;
            lblSearchResult.Visible = true;
            searchCount.Visible = true;
        }

        #endregion


        #region Item Command Events Of Datalist
        protected void dlHotelDetails_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AddToCartHotel")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                AddHotelToCart(i);
                
            }
        }

        protected void dlSightSeeing_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AddToCartSight")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                AddSightToCart(i);
            //    Master.DisplayMessage("Sight Added To Cart Successfully", "successMessage", 1000);
            }
        }

        protected void dlCruiseDetail_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AddToCartCruise")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                AddCruiseToCart(i);
              //  Master.DisplayMessage("Cruise Added To Cart Successfully", "successMessage", 1000);
            }
        }

        protected void dlTransferPackage_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AddToCartTransfer")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                AddTransferToCart(i);
             //   Master.DisplayMessage("Transfer Package Added To Cart Successfully", "successMessage", 1000);
            }
        }
        #endregion


        #region Add To Cart
        protected void AddHotelToCart(int pk)
        {
            foreach (DataListItem item in dlHotelDetails.Items)
            {
                Label lblPk = (Label)item.FindControl("lblSrNoHotel");
                if (Convert.ToInt32(lblPk.Text) == pk)
                {
                    TextBox NoOfRooms = (TextBox)item.FindControl("txtQtyHotel");
                    TextBox txtFromDate = (TextBox)item.FindControl("txtFromDateHotel");
                    TextBox txtTodate = (TextBox)item.FindControl("txtToDateHotel");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpRoomType");

                    if(txtFromDate.Text=="")
                    {
                        Master.DisplayMessage("From Date Is Required.", "successMessage", 3000);
                    }
                    else if (txtTodate.Text == "")
                    {
                        Master.DisplayMessage("To Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("From Date Must Be Within Search Date.", "successMessage", 3000); 
                    }
                    else if ((DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("To Date Must Be Within Search Date.", "successMessage", 3000);
                    }
                    else if((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null))))
                    {
                        Master.DisplayMessage("To Date Must Be After From Date.", "successMessage", 3000);
                    }
                    else
                    {
                        objFitBookingStoreProcedure.insertUpdateHotelIntoCart(txtCity.Text, pk, txtFromDate.Text, txtTodate.Text, NoOfRooms.Text, Session["usersid"].ToString(), drpRoomType.SelectedValue.ToString());
                        DataTable dt = objFitBookingStoreProcedure.fetchMaxHotelCartSrNo(Session["usersid"].ToString());
                        String table_id = dt.Rows[0]["HOTEL_CART_ID"].ToString();
                        objFitBookingStoreProcedure.insertUpdateCartOrderDetail(table_id, "H", Session["usersid"].ToString());
                        BindMyCart();
                        upHotelCart.Update();
                        Master.DisplayMessage("Hotel Added To Cart Successfully", "successMessage", 1000);
                    }
                    break;
                }
            }
        }

        protected void AddSightToCart(int pk)
        {
            foreach (DataListItem item in dlSightSeeing.Items)
            {
                Label lblPk = (Label)item.FindControl("lblSrNoSightSeeing");
                if (Convert.ToInt32(lblPk.Text) == pk)
                {
                    TextBox txtDate = (TextBox)item.FindControl("txtDateSightSeeing");
                    TextBox txtTime = (TextBox)item.FindControl("txtTimeSightSeeing");

                  


                    if (txtDate.Text=="")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("Date Must Be Within Search Date.", "successMessage", 3000);
                    }
                    else
                    {
                        if (txtTime.Text == "")
                        {
                            txtTime.Text = "00:00:00";
                        }
                    objFitBookingStoreProcedure.insertUpdateSightIntoCart(pk, txtDate.Text, txtTime.Text, Session["usersid"].ToString());
                    DataTable dt = objFitBookingStoreProcedure.fetchMaxServiceCartSrNo(Session["usersid"].ToString());
                    String table_id = dt.Rows[0]["SERVICE_CART_ID"].ToString();
                    objFitBookingStoreProcedure.insertUpdateCartOrderDetail(table_id, "S", Session["usersid"].ToString());
                    BindMyCart();
                    upSightCart.Update();
                    Master.DisplayMessage("Sight Added To Cart Successfully", "successMessage", 1000);
                  
                    }
                    break;
                }
            }
        }

        protected void AddCruiseToCart(int pk)
        {
            foreach (DataListItem item in dlCruiseDetail.Items)
            {
                Label lblPk = (Label)item.FindControl("lblSrNoCruise");
                if (Convert.ToInt32(lblPk.Text) == pk)
                {
                    TextBox NoOfRooms = (TextBox)item.FindControl("txtQtyCruise");
                    TextBox txtFromDate = (TextBox)item.FindControl("txtFromDateCruise");
                    TextBox txtTodate = (TextBox)item.FindControl("txtToDateCruise");
                    DropDownList drpRoomType = (DropDownList)item.FindControl("drpCabineType");

                    if(txtFromDate.Text=="")
                    {
                        Master.DisplayMessage("From Date Is Required.", "successMessage", 3000);
                    }
                    else if (txtTodate.Text == "")
                    {
                        Master.DisplayMessage("To Date Is Required.", "successMessage", 3000);
                    }
                    else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("From Date Must Be Within Search Date.", "successMessage", 3000); 
                    }
                    else if ((DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                    {
                        Master.DisplayMessage("To Date Must Be Within Search Date.", "successMessage", 3000);
                    }
                     else if ((DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", null) > (DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", null))))
                     {
                         Master.DisplayMessage("To Date Must Be After From Date.", "successMessage", 3000);
                     }
                     else
                     {
                         objFitBookingStoreProcedure.insertUpdateCruiseIntoCart(txtCity.Text, pk, txtFromDate.Text, txtTodate.Text, NoOfRooms.Text, Session["usersid"].ToString(), drpRoomType.SelectedValue.ToString());
                         DataTable dt = objFitBookingStoreProcedure.fetchMaxCruiseCartSrNo(Session["usersid"].ToString());
                         String table_id = dt.Rows[0]["CRUISE_CART_ID"].ToString();
                         objFitBookingStoreProcedure.insertUpdateCartOrderDetail(table_id, "C", Session["usersid"].ToString());
                         BindMyCart();
                         upCruiseCart.Update();
                         Master.DisplayMessage("Cruise Added To Cart Successfully", "successMessage", 1000);
                     }
                    break;
                }
            }
        }

        protected void AddTransferToCart(int pk)
        {
            foreach (DataListItem item in dlTransferPackage.Items)
            {
                Label lblPk = (Label)item.FindControl("lblTransferPackage");
                if (Convert.ToInt32(lblPk.Text) == pk)
                {
                    TextBox txtDate = (TextBox)item.FindControl("txtDateTransferPackage");
                    TextBox txtTime = (TextBox)item.FindControl("txtTimeTransferPackage");

                   
                     if (txtDate.Text=="")
                    {
                        Master.DisplayMessage("Date Is Required.", "successMessage", 3000);
                    }
                     else if ((DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) < DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", null)) || (DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", null) > DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", null)))
                     {
                         Master.DisplayMessage("Date Must Be Within Search Date.", "successMessage", 3000);
                     }
                     else
                     {
                         if (txtTime.Text == "")
                         {
                             txtTime.Text = "00:00:00";
                         }

                         objFitBookingStoreProcedure.insertUpdateTransferIntoCart(pk, txtDate.Text, txtTime.Text, Session["usersid"].ToString());
                         DataTable dt = objFitBookingStoreProcedure.fetchMaxServiceCartSrNo(Session["usersid"].ToString());
                         String table_id = dt.Rows[0]["SERVICE_CART_ID"].ToString();
                         objFitBookingStoreProcedure.insertUpdateCartOrderDetail(table_id, "S", Session["usersid"].ToString());
                         BindMyCart();
                         upTransferCart.Update();
                         Master.DisplayMessage("Transfer Package Added To Cart Successfully", "successMessage", 1000);
                     }
                    break;
                }
            }
        }
        #endregion

        #region Bind My Cart
        protected void BindMyCart()
        {
            DataTable dt = objFitBookingStoreProcedure.fetchHotalCartData(Session["usersid"].ToString());
            dlHotelCart.DataSource = dt;
            dlHotelCart.DataBind();

            DataTable dt_cruise = objFitBookingStoreProcedure.fetchCruiseCartData(Session["usersid"].ToString());
            dlCruiseCart.DataSource = dt_cruise;
            dlCruiseCart.DataBind();

            DataTable dt_sight = objFitBookingStoreProcedure.fetchSightCartData(Session["usersid"].ToString());
            dlSightCart.DataSource = dt_sight;
            dlSightCart.DataBind();

            DataTable dt_transfer = objFitBookingStoreProcedure.fetchTransferCartData(Session["usersid"].ToString());
            dlTransferCart.DataSource = dt_transfer;
            dlTransferCart.DataBind();
        }
        #endregion

        protected void btnMyCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/FIT/OrderSummary.aspx");
        }

    }
}