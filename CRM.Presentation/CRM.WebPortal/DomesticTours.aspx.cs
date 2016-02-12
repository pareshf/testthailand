using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using CRM.DataAccess.WebPortalDal;
using CRM.WebPortal.DataAccess;

namespace CRM.WebPortal
{
    public partial class DomesticTours : System.Web.UI.Page
    {
        static int tourSubType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebDataAccess webDataAccess = new WebDataAccess();
                dlstType.DataSource = webDataAccess.GetDomesticTourType();
                dlstType.DataBind();
            }
        }

        protected void dlstType_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LinkButton lbtnType = (LinkButton)e.Item.FindControl("lbtnType");
            lbtnTourTypes1.Text = lbtnType.Text;
            tourSubType = Convert.ToInt32(lbtnType.CommandArgument);
            mvMain.SetActiveView(viewStates);
            WebDataAccess webDataAccess = new WebDataAccess();
            dlstState.DataSource = webDataAccess.GetTourState("D", Convert.ToInt32(lbtnType.CommandArgument));
            dlstState.DataBind();
        }

        protected void dlstState_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LinkButton lbtnState = (LinkButton)e.Item.FindControl("lbtnState");
            lbtnState1.Text = lbtnState.Text;
            mvMain.SetActiveView(viewTours);
            WebDataAccess webDataAccess = new WebDataAccess();

            DataTable dt = webDataAccess.GetTourByState("D", tourSubType, Convert.ToInt32(lbtnState.CommandArgument));
            dlstTour.DataSource = dt;
            dlstTour.DataBind();
            int tourId = 0;
            foreach (DataListItem item in dlstTour.Items)
            {
                DataRow row = dt.Rows[item.ItemIndex];
                if (!string.IsNullOrEmpty(Convert.ToString(row["WEB_PHOTO_CONTENT"])))
                {
                    ImageButton imgPhoto = (ImageButton)item.FindControl("imgPhoto");
					

					
                    if (imgPhoto != null)
                    {
                        Int32.TryParse(imgPhoto.CommandArgument, out tourId);
                        //HttpContext.Current.Session["DataRow"] = row;
                        imgPhoto.ImageUrl = "~/Shared/ImageHandler.ashx?id=" + tourId + "&phototype=tour";
                    }
                }
				if (!string.IsNullOrEmpty(Convert.ToString(row["NO_OF_NIGHTS"])) && !string.IsNullOrEmpty(Convert.ToString(row["NO_OF_DAYS"])))
				{
					Label lblDayss = (Label)item.FindControl("lblDayss");
					if (lblDayss != null)
					{
						lblDayss.Text = row["NO_OF_NIGHTS"].ToString() + " Nights/" + row["NO_OF_DAYS"].ToString() + " Days";
					}
				}
            }
        }

        protected void dlstTour_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LinkButton lbtnTour = (LinkButton)e.Item.FindControl("lbtnTour");
            mvMain.SetActiveView(viewToursDetails);
            lbtnTour1.Text = lbtnState1.Text;
            WebDataAccess webDataAccess = new WebDataAccess();
            DataTable dt = webDataAccess.GetTourDetail(Convert.ToInt32(lbtnTour.CommandArgument));
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblTourName.Text = row["TOUR_SHORT_NAME"].ToString();
                lblDays.Text = row["NO_OF_NIGHTS"].ToString() + " Nights/" + row["NO_OF_DAYS"].ToString() + " Days";
                lblTourCode.Text = "Tour Code : " + row["TOUR_CODE"].ToString();
                lblTourDuration.Text = "From " + row["TOUR_FROM_DATE"].ToString() + " To " + row["TOUR_TO_DATE"].ToString();

                dvHighlights.InnerHtml = row["WEB_HIGHLIGHT"].ToString();
                dvCost.InnerHtml = row["WEB_TOUR_COST"].ToString();
                dvIntenary.InnerHtml = row["WEB_ITENARY"].ToString();
                dvImpNotes.InnerHtml = row["WEB_IMPORTANT_NOTES"].ToString();
                dvTerms.InnerHtml = row["WEB_TERMS"].ToString();
            }
        }

        protected void lbtnTourTypes1_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewType);
        }

        protected void lbtnState1_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewStates);
        }

        protected void lbtnTour1_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewTours);
        }
    }
}
