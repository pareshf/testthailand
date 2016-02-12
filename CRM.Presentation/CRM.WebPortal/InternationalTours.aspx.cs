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
	public partial class InternationalTours : System.Web.UI.Page
	{
		static int tourSubType = 0;
		static int tourRegion = 0;
		static int tourCountry = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				WebDataAccess webDataAccess = new WebDataAccess();
				dlstType.DataSource = webDataAccess.GetTourType();
				dlstType.DataBind();
			}
		}

		protected void dlstType_ItemCommand(object source, DataListCommandEventArgs e)
		{
			LinkButton lbtnType = (LinkButton)e.Item.FindControl("lbtnType");
			lbtnRegion1.Text = lbtnType.Text;
			tourSubType = Convert.ToInt32(lbtnType.CommandArgument);
			//mvMain.SetActiveView(viewRegion);
			//WebDataAccess webDataAccess = new WebDataAccess();
			//dlstRegion.DataSource = webDataAccess.GetTourRegion("I", Convert.ToInt32(lbtnType.CommandArgument));
			//dlstRegion.DataBind();



			//LinkButton lbtnRegion = (LinkButton)e.Item.FindControl("lbtnRegion");
			//lbtnRegion1.Text = lbtnRegion.Text;
			//tourRegion = Convert.ToInt32(lbtnRegion.CommandArgument);
			mvMain.SetActiveView(viewCountries);
			WebDataAccess webDataAccess = new WebDataAccess();
			dlstCountry.DataSource = webDataAccess.GetTourCountry("I", tourSubType);
			dlstCountry.DataBind();
		}

		protected void dlstRegion_ItemCommand(object source, DataListCommandEventArgs e)
		{
			LinkButton lbtnRegion = (LinkButton)e.Item.FindControl("lbtnRegion");
			lbtnRegion1.Text = lbtnRegion.Text;
			tourRegion = Convert.ToInt32(lbtnRegion.CommandArgument);
			mvMain.SetActiveView(viewCountries);
			WebDataAccess webDataAccess = new WebDataAccess();
			dlstCountry.DataSource = webDataAccess.GetTourCountry("I", tourSubType);
			dlstCountry.DataBind();

		}

		protected void dlstCountry_ItemCommand(object source, DataListCommandEventArgs e)
		{
			LinkButton lbtnCountry = (LinkButton)e.Item.FindControl("lbtnCountry");
			lbtnCountry1.Text = lbtnCountry.Text;
			mvMain.SetActiveView(viewTours);
			WebDataAccess webDataAccess = new WebDataAccess();

			DataTable dt = webDataAccess.GetTour("I", tourSubType, Convert.ToInt32(lbtnCountry.CommandArgument));
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
					Label Label1 = (Label)item.FindControl("Label1");
					if (Label1 != null)
					{
						Label1.Text = row["NO_OF_NIGHTS"].ToString() + " Nights/" + row["NO_OF_DAYS"].ToString() + " Days";
					}
				}


			}
		}

		protected void dlstTour_ItemCommand(object source, DataListCommandEventArgs e)
		{
			LinkButton lbtnTour = (LinkButton)e.Item.FindControl("lbtnTour");
			mvMain.SetActiveView(viewToursDetails);
			lbtnTour1.Text = lbtnCountry1.Text;
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

		protected void lbtnRegion1_Click(object sender, EventArgs e)
		{
			mvMain.SetActiveView(viewRegion);
		}

		protected void lbtnCountry1_Click(object sender, EventArgs e)
		{
			mvMain.SetActiveView(viewCountries);
		}

		protected void lbtnTour1_Click(object sender, EventArgs e)
		{
			mvMain.SetActiveView(viewTours);
		}


	}
}
