using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;
using System.Data;
using Telerik.Web.UI;


namespace CRM.WebApp.Views.Marketing
{
    public partial class MarketingMaterialDoc : System.Web.UI.Page
    {
        MarketingMaterialStoredProcedure objmarketindoc = new MarketingMaterialStoredProcedure();

        int tourid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            tourid = int.Parse(Page.Request.QueryString["key"].ToString());
            if (!IsPostBack)
            {
                DataSet ds = objmarketindoc.getMarketingMaterialDoc(tourid);
                DataSet ds1 = objmarketindoc.GetEmbadCodeandWeburl(tourid);

                if (ds1.Tables[0].Rows.Count == 0)
                {

                }
                else
                {
                    txtembadcode.Text = ds1.Tables[0].Rows[0]["EMBEDCODE"].ToString();
                    txtweburl.Text = ds1.Tables[0].Rows[0]["WEBURL"].ToString();
                }
                /*
                MarketingMaterialStoredProcedure objmarketindoc = new MarketingMaterialStoredProcedure();
               
               // marDoc.HRef = "~/MarketingMaterialDoc/" + marketingdocid + "/" + ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupitenary" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        marDoc.HRef = "~/MarketingMaterialDoc/" + marketingdocid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        chkitenary.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupitenary")
                    {
                        marDoc.HRef = "~/MarketingMaterialDoc/" + marketingdocid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        chkitenary.Checked = false;
                    }
                } */
                
                //set anchor to all document
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupfulldetail" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancfulldetail.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox1.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfulldetail.Checked = true;

                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupfulldetail")
                    {
                        ancfulldetail.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox1.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfulldetail.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupitenary" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox2.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkitenary.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupitenary")
                    {
                        ancitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox2.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkitenary.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCharges" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anccharged.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox10.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcharges.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCharges")
                    {
                        anccharged.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox10.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcharges.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupComparision" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anccomparision.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox14.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcomparision.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupComparision")
                    {
                        anccomparision.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox14.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcomparision.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCondition" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anccondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox8.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcondition.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCondition")
                    {
                        anccondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                        TextBox8.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcondition.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCost" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anccost.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox11.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcost.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupCost")
                    {
                        anccost.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox11.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkcost.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupDetails" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancdetails.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox4.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkdetails.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupDetails")
                    {
                        ancdetails.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox4.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkdetails.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupfinalItenary" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancfinalitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox15.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfinalitenary.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupfinalItenary")
                    {
                        ancfinalitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox15.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfinalitenary.Checked = false;
                    }
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupGuidelines" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancguidline.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox6.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkguidelines.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupGuidelines")
                    {
                        ancguidline.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox6.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkguidelines.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupHighlights" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anchighlight.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox3.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkhighlights.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupHighlights")
                    {
                        anchighlight.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox3.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkhighlights.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupLimitations" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anclimitations.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox13.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chklimitations.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupLimitations")
                    {
                        anclimitations.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox13.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chklimitations.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupNotes" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancnotes.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox5.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chknotes.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupNotes")
                    {
                        ancnotes.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox5.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chknotes.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupSheet" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancsheet.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox7.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chksheet.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupSheet")
                    {
                        ancsheet.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox7.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chksheet.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupTerms" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancterms.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox9.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkterms.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupTerms")
                    {
                        ancterms.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox9.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkterms.Checked = false;
                    }
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupTermsCondition" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        anctermsandcondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox16.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfinalcondition.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupTermsCondition")
                    {
                        anctermsandcondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox16.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkfinalcondition.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupUSP" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancusp.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox12.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkusp.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupUSP")
                    {
                        ancusp.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox12.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkusp.Checked = false;
                    }

                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupVouchures" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancvoucheres.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox17.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkvouchures.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "flupVouchures")
                    {
                        ancvoucheres.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox17.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkvouchures.Checked = false;
                    }
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupBrocerfile" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancbrocer.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox18.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkvouchures.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupBrocerfile")
                    {
                        ancbrocer.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox18.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        Chkbrocer.Checked = false;
                    }
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupPresentationfile" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancpresentation.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox19.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkvouchures.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupPresentationfile")
                    {
                        ancpresentation.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox19.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        ChkPresentation.Checked = false;
                    }
                    if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupEmailformat" && ds.Tables[0].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                    {
                        ancemailformat.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox20.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        chkvouchures.Checked = true;
                    }
                    else if (ds.Tables[0].Rows[j]["TITLE"].ToString() == "FlupEmailformat")
                    {
                        ancemailformat.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[0].Rows[j]["ATTACHMENT"].ToString();
                        TextBox20.Text = ds.Tables[0].Rows[j]["EXPIRATION_DATE"].ToString();
                        Chkemailformat.Checked = false;
                    }
                }

            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            /* int flag = 1;
             if (!System.IO.Directory.Exists(Server.MapPath("~/MarketingMaterialDoc/" + marketingdocid.ToString() + "/")))
                 System.IO.Directory.CreateDirectory(Server.MapPath("~/MarketingMaterialDoc/" + marketingdocid.ToString() + "/"));

             if (marketingDoc.HasFile)
             {
                 marketingDoc.SaveAs(Server.MapPath("~/MarketingMaterialDoc/" + marketingdocid.ToString() + "/") + marketingdocid.ToString() + marketingDoc.FileName);
                 objmarketindoc.InsertUpdateMarketingDocument(marketingdocid, marketingdocid.ToString() + marketingDoc.FileName, "flupitenary", 1, flag);
             }
             if (marDoc.HRef != null)
             {
                 if (chkitenary.Checked == true)
                 {
                     flag = 1;

                 }
                 else
                 {
                     flag = 0;
                 }
                 objmarketindoc.UpadateMarketingDoc(marketingdocid, "flupitenary", flag);
             }
             Response.Write("<script language='javascript'> { window.close();}</script>"); */

            int flag = 1;
            if (!System.IO.Directory.Exists(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/")))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/"));
            //foreach (ListItem li in Selectedcountry.Items) //lstbox is ID of ListBox
            //{
            //    lstbxItems = lstbxItems + "," + ((ListItem)(li)).Text;
            //}
            //objtourmasterprocedure.AssignCountry(tourid, lstbxItems.ToString());
            //lstbxItems = null;


            //foreach (ListItem li in SelectedTravel.Items) //lstbox is ID of ListBox
            //{
            //    lstbxItems = lstbxItems + "," + ((ListItem)(li)).Text;

            //}

            //objtourmasterprocedure.AssignCITY(tourid, lstbxItems.ToString());
            //lstbxItems = null;

            //foreach (ListItem li in Selectedcity.Items) //lstbox is ID of ListBox
            //{
            //    lstbxItems = lstbxItems + "," + ((ListItem)(li)).Text;
            //}
            //objtourmasterprocedure.AssignStartEndCity(tourid, lstbxItems.ToString());
            //lstbxItems = null;

            if (flupitenary.HasFile)
            {
                flupitenary.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupitenary.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupitenary", tourid.ToString() + flupitenary.FileName, 1, flupitenary.FileName, flag,TextBox2.Text);
            }
            if (flupCharges.HasFile)
            {
                flupCharges.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCharges.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupCharges", tourid.ToString() + flupCharges.FileName, 0, flupCharges.FileName, flag,TextBox10.Text);
            }
            if (flupfulldetail.HasFile)
            {
                flupfulldetail.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupfulldetail.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupfulldetail", tourid.ToString() + flupfulldetail.FileName, 0, flupfulldetail.FileName, flag,TextBox1.Text);
            }
            if (flupComparision.HasFile)
            {
                flupComparision.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupComparision.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupComparision", tourid.ToString() + flupComparision.FileName, 0, flupComparision.FileName, flag,TextBox14.Text);
            }
            if (flupCondition.HasFile)
            {
                flupCondition.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCondition.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupCondition", tourid.ToString() + flupCondition.FileName, 0, flupCondition.FileName, flag,TextBox8.Text);
            }
            if (flupCost.HasFile)
            {
                flupCost.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCost.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupCost", tourid.ToString() + flupCost.FileName, 0, flupCost.FileName, flag,TextBox11.Text);
            }
            if (flupDetails.HasFile)
            {
                flupDetails.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupDetails.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupDetails", tourid.ToString() + flupDetails.FileName, 0, flupDetails.FileName, flag,TextBox4.Text);
            }
            if (flupfinalItenary.HasFile)
            {
                flupfinalItenary.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupfinalItenary.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupfinalItenary", tourid.ToString() + flupfinalItenary.FileName, 0, flupfinalItenary.FileName, flag,TextBox15.Text);
            }
            if (flupGuidelines.HasFile)
            {
                flupGuidelines.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupGuidelines.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupGuidelines", tourid.ToString() + flupGuidelines.FileName, 0, flupGuidelines.FileName, flag,TextBox6.Text);
            }
            if (flupHighlights.HasFile)
            {
                flupHighlights.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupHighlights.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupHighlights", tourid.ToString() + flupHighlights.FileName, 0, flupHighlights.FileName, flag,TextBox3.Text);
            }
            if (flupLimitations.HasFile)
            {
                flupLimitations.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupLimitations.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupLimitations", tourid.ToString() + flupLimitations.FileName, 0, flupLimitations.FileName, flag,TextBox13.Text);
            }
            if (flupNotes.HasFile)
            {
                flupNotes.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupNotes.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupNotes", tourid.ToString() + flupNotes.FileName, 0, flupNotes.FileName, flag,TextBox5.Text);
            }
            if (flupSheet.HasFile)
            {
                flupSheet.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupSheet.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupSheet", tourid.ToString() + flupSheet.FileName, 0, flupSheet.FileName, flag,TextBox7.Text);
            }
            if (flupTerms.HasFile)
            {
                flupTerms.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupTerms.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupTerms", tourid.ToString() + flupTerms.FileName, 0, flupTerms.FileName, flag,TextBox9.Text);
            }
            if (flupTermsCondition.HasFile)
            {
                flupTermsCondition.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupTermsCondition.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupTermsCondition", tourid.ToString() + flupTermsCondition.FileName, 0, flupTermsCondition.FileName, flag,TextBox8.Text);
            }
            if (flupUSP.HasFile)
            {
                flupUSP.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupUSP.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupUSP", tourid.ToString() + flupUSP.FileName, 0, flupUSP.FileName, flag,TextBox12.Text);
            }
            if (flupVouchures.HasFile)
            {
                flupVouchures.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupVouchures.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "flupVouchures", tourid.ToString() + flupVouchures.FileName, 0, flupVouchures.FileName, flag,TextBox17.Text);
            }
            if (FlupBrocerfile.HasFile)
            {
                FlupBrocerfile.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + FlupBrocerfile.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "FlupBrocerfile", tourid.ToString() + FlupBrocerfile.FileName, 0, FlupBrocerfile.FileName, flag,TextBox18.Text);
            }
            if (FlupPresentationfile.HasFile)
            {
                FlupPresentationfile.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + FlupPresentationfile.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "FlupBrocerfile", tourid.ToString() + FlupPresentationfile.FileName, 0, FlupPresentationfile.FileName, flag,TextBox19.Text);
            }
            if (FlupEmailformat.HasFile)
            {
                FlupEmailformat.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + FlupEmailformat.FileName);
                objmarketindoc.insertMarketingMaterial(tourid, "FlupBrocerfile", tourid.ToString() + FlupEmailformat.FileName, 0, FlupEmailformat.FileName, flag,TextBox20.Text);
            }
            if (ancfulldetail.HRef != null)
            {
                if (chkfulldetail.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupfulldetail", flag);
            }
            if (ancitenary.HRef != null)
            {
                if (chkitenary.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupitenary", flag);
            }
            if (anccharged.HRef != null)
            {
                if (chkcharges.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupCharges", flag);
            }
            if (anccomparision.HRef != null)
            {
                if (chkcomparision.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupComparision", flag);
            }
            if (anccondition.HRef != null)
            {
                if (chkcondition.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupCondition", flag);
            }
            if (anccost.HRef != null)
            {
                if (chkcost.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupCost", flag);
            }
            if (ancdetails.HRef != null)
            {
                if (chkdetails.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupDetails", flag);
            }
            if (ancfinalitenary.HRef != null)
            {
                if (chkfinalitenary.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupfinalItenary", flag);
            }
            if (ancguidline.HRef != null)
            {
                if (chkguidelines.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupGuidelines", flag);
            }
            if (anchighlight.HRef != null)
            {
                if (chkhighlights.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupHighlights", flag);
            }
            if (anclimitations.HRef != null)
            {
                if (chklimitations.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupLimitations", flag);
            }
            if (ancnotes.HRef != null)
            {
                if (chknotes.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupNotes", flag);
            }
            if (ancsheet.HRef != null)
            {
                if (chksheet.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupSheet", flag);
            }
            if (ancterms.HRef != null)
            {
                if (chkterms.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupTerms", flag);
            }
            if (anctermsandcondition.HRef != null)
            {
                if (chkfinalcondition.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupTermsCondition", flag);
            }
            if (ancusp.HRef != null)
            {
                if (chkusp.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupUSP", flag);
            }
            if (ancvoucheres.HRef != null)
            {
                if (chkvouchures.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "flupVouchures", flag);
            }
            if (ancbrocer.HRef != null)
            {
                if (Chkbrocer.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "FlupBrocerfile", flag);
            }
            if (ancpresentation.HRef != null)
            {
                if (ChkPresentation.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "FlupPresentationfile", flag);
            }
            if (ancemailformat.HRef != null)
            {
                if (Chkemailformat.Checked == true)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
                objmarketindoc.updateMarketingMaterial(tourid, "FlupEmailformat", flag);
            }
            objmarketindoc.InsertUpdateEmbadCode(tourid, txtembadcode.Text, txtweburl.Text);
            txtembadcode.Text = "";
            txtweburl.Text = "";
            Response.Write("<script>alert('Record Save Successfully.')</script>");
           // Response.Write("<script language='javascript'> { window.close();}</script>");          
        }

    }        
}