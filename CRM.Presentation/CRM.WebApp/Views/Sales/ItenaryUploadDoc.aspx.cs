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

namespace CRM.WebApp.Views.Sales
{
    public partial class ItenaryUploadDoc : System.Web.UI.Page
    {
        TourmasterStoreprocedures objtourmasterprocedure = new TourmasterStoreprocedures();
        int tourid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            tourid = int.Parse(Page.Request.QueryString["key"].ToString());
            if (!IsPostBack)
            {
                TourmasterStoreprocedures objtourentity = new TourmasterStoreprocedures();
                DataSet ds = objtourentity.getdataforuploaddetails(tourid);

                allcountry.DataSource = ds.Tables[0];
                allcountry.DataBind();
                allcountry.DataTextField = "COUNTRY_NAME";
                allcountry.DataValueField = "COUNTRY_ID";
                
                allcitytravel.DataSource = ds.Tables[1];
                allcitytravel.DataBind();
                allcitytravel.DataTextField = "CITY_NAME";
                allcitytravel.DataTextField = "CITY_ID";

                allstartendcity.DataSource = ds.Tables[1];
                allstartendcity.DataBind();
                allstartendcity.DataTextField = "CITY_NAME";
                allstartendcity.DataValueField = "CITY_ID";

               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
               {
                   try
                   {
                       if (ds.Tables[2].Rows[0]["COUNTRIES_FOR_VISA_TEXT"].ToString().Contains(ds.Tables[0].Rows[i]["COUNTRY_NAME"].ToString().Trim()))
                       {
                           ListItem item = new ListItem(ds.Tables[0].Rows[i]["COUNTRY_NAME"].ToString());
                           Selectedcountry.Items.Add(item);
                       }
                   }
                   catch (Exception ex) { }
               }
               for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
               {
                   try
                   {
                       if (ds.Tables[3].Rows[0]["CITY_TEXT"].ToString().Contains(ds.Tables[1].Rows[i]["CITY_NAME"].ToString().Trim()))
                       {
                           ListItem item = new ListItem(ds.Tables[1].Rows[i]["CITY_NAME"].ToString());
                           SelectedTravel.Items.Add(item);
                       }
                   }
                   catch (Exception ex) { }
               }
               for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
               {
                   try
                   {

                       if (ds.Tables[4].Rows[0]["START_END_CITY_TEXT"].ToString().Contains(ds.Tables[1].Rows[i]["CITY_NAME"].ToString().Trim()))
                       {
                           ListItem item = new ListItem(ds.Tables[1].Rows[i]["CITY_NAME"].ToString());
                           Selectedcity.Items.Add(item);
                       }
                   }
                   catch (Exception ex) { }
               }
                //set anchor to all document
               for (int j = 0; j < ds.Tables[5].Rows.Count; j++)
               {
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupfulldetail" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancfulldetail.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfulldetail.Checked = true;
                      
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupfulldetail")
                   {
                       ancfulldetail.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfulldetail.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupitenary" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkitenary.Checked = true;   
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupitenary")
                   {
                       ancitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkitenary.Checked = false;  
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCharges" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anccharged.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcharges.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCharges")
                   {
                       anccharged.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcharges.Checked = false;
                   }
                   
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupComparision" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anccomparision.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcomparision.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupComparision")
                   {
                       anccomparision.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcomparision.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCondition" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anccondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcondition.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCondition")
                   {
                       anccondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcondition.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCost" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anccost.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcost.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupCost")
                   {
                       anccost.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkcost.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupDetails" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancdetails.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkdetails.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupDetails")
                   {
                       ancdetails.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkdetails.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupfinalItenary" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancfinalitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfinalitenary.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupfinalItenary")
                   {
                       ancfinalitenary.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfinalitenary.Checked = false;
                   }
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupGuidelines" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancguidline.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkguidelines.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupGuidelines")
                   {
                       ancguidline.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkguidelines.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupHighlights" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anchighlight.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkhighlights.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupHighlights")
                   {
                       anchighlight.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkhighlights.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupLimitations" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anclimitations.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chklimitations.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupLimitations")
                   {
                       anclimitations.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chklimitations.Checked = false;
                   }
                   
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupNotes" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancnotes.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chknotes.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupNotes")
                   {
                       ancnotes.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chknotes.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupSheet" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancsheet.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chksheet.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupSheet")
                   {
                       ancsheet.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chksheet.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupTerms" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancterms.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkterms.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupTerms")
                   {
                       ancterms.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkterms.Checked = false;                       
                   }
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupTermsCondition" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       anctermsandcondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfinalcondition.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupTermsCondition")
                   {
                       anctermsandcondition.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkfinalcondition.Checked = false;
                   }

                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupUSP" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancusp.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkusp.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupUSP")
                   {
                       ancusp.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkusp.Checked = false;
                   }
                   
                   if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupVouchures" && ds.Tables[5].Rows[j]["IS_DEFAULT_DOC"].ToString() == "1")
                   {
                       ancvoucheres.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkvouchures.Checked = true;
                   }
                   else if (ds.Tables[5].Rows[j]["TITLE"].ToString() == "flupVouchures")
                   {
                       ancvoucheres.HRef = "~/marketingdocuments/" + tourid + "/" + ds.Tables[5].Rows[j]["ATTACHMENT"].ToString();
                       chkvouchures.Checked = false;
                   }
               }
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            int flag=1;
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
                flupitenary.SaveAs(Server.MapPath("~/marketingdocuments/"+tourid.ToString()+"/") + tourid.ToString() + flupitenary.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupitenary", tourid.ToString() + flupitenary.FileName, 1, flupitenary.FileName,flag);
            }
            if (flupCharges.HasFile)
            {
                flupCharges.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCharges.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupCharges", tourid.ToString() + flupCharges.FileName, 0, flupCharges.FileName,flag);
            }
            if (flupfulldetail.HasFile)
            {
                flupfulldetail.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupfulldetail.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupfulldetail", tourid.ToString() + flupfulldetail.FileName, 0, flupfulldetail.FileName, flag);
            }
            if (flupComparision.HasFile)
            {
                flupComparision.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupComparision.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupComparision", tourid.ToString() + flupComparision.FileName, 0, flupComparision.FileName,flag);
            }
            if (flupCondition.HasFile)
            {
                flupCondition.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCondition.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupCondition", tourid.ToString() + flupCondition.FileName, 0, flupCondition.FileName,flag);
            }
            if (flupCost.HasFile)
            {
                flupCost.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupCost.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupCost", tourid.ToString() + flupCost.FileName, 0, flupCost.FileName,flag);
            }
            if (flupDetails.HasFile)
            {
                flupDetails.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupDetails.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupDetails", tourid.ToString() + flupDetails.FileName, 0, flupDetails.FileName,flag);
            }
            if (flupfinalItenary.HasFile)
            {
                flupfinalItenary.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupfinalItenary.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupfinalItenary", tourid.ToString() + flupfinalItenary.FileName, 0, flupfinalItenary.FileName,flag);
            }
            if (flupGuidelines.HasFile)
            {
                flupGuidelines.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupGuidelines.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupGuidelines", tourid.ToString() + flupGuidelines.FileName, 0, flupGuidelines.FileName,flag);
            }
            if (flupHighlights.HasFile)
            {
                flupHighlights.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupHighlights.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupHighlights", tourid.ToString() + flupHighlights.FileName, 0, flupHighlights.FileName,flag);
            }
            if (flupLimitations.HasFile)
            {
                flupLimitations.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupLimitations.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupLimitations", tourid.ToString() + flupLimitations.FileName, 0, flupLimitations.FileName,flag);
            }
            if (flupNotes.HasFile)
            {
                flupNotes.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupNotes.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupNotes", tourid.ToString() + flupNotes.FileName, 0, flupNotes.FileName,flag);
            }
            if (flupSheet.HasFile)
            {
                flupSheet.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupSheet.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupSheet", tourid.ToString() + flupSheet.FileName, 0, flupSheet.FileName,flag);
            }
            if (flupTerms.HasFile)
            {
                flupTerms.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupTerms.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupTerms", tourid.ToString() + flupTerms.FileName, 0, flupTerms.FileName,flag);
            }
            if (flupTermsCondition.HasFile)
            {
                flupTermsCondition.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupTermsCondition.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupTermsCondition", tourid.ToString() + flupTermsCondition.FileName, 0, flupTermsCondition.FileName,flag);
            }
            if (flupUSP.HasFile)
            {
                flupUSP.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupUSP.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupUSP", tourid.ToString() + flupUSP.FileName, 0, flupUSP.FileName,flag);
            }
            if (flupVouchures.HasFile)
            {
                flupVouchures.SaveAs(Server.MapPath("~/marketingdocuments/" + tourid.ToString() + "/") + tourid.ToString() + flupVouchures.FileName);
                objtourmasterprocedure.insertMarketingMaterial(tourid, "flupVouchures", tourid.ToString() + flupVouchures.FileName, 0, flupVouchures.FileName,flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupfulldetail", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupitenary", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupCharges", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupComparision", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupCondition", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupCost", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupDetails", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupfinalItenary", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupGuidelines", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupHighlights", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupLimitations", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupNotes", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupSheet", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupTerms", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupTermsCondition", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupUSP", flag);
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
                objtourmasterprocedure.updateMarketingMaterial(tourid, "flupVouchures", flag);
            }

            Response.Write("<script language='javascript'> { window.close();}</script>");          
        }
    }
}