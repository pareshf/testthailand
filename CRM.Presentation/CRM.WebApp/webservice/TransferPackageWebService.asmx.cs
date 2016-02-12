using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using CRM.WebApp.Views.dbmlfile;
using CRM.WebApp.Views;
using System.Collections;
using CRM.Model.Security;
using CRM.DataAccess.AdministratorEntity;


namespace CRM.WebApp.webservice
{
    /// <summary>
    /// Summary description for TransferPackageWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class TransferPackageWebService : System.Web.Services.WebService
    {

        IQueryable<VIEW_TRANSFER_PACKAGE_PRICE_LIST> TransferPackage = new TransferPackageDataContext().VIEW_TRANSFER_PACKAGE_PRICE_LISTs.AsQueryable<VIEW_TRANSFER_PACKAGE_PRICE_LIST>();
        
        IQueryable<VIEW_TRANSFER_PACKAGE_DETAIL> DETAIL = new TransferPackageDataContext().VIEW_TRANSFER_PACKAGE_DETAILs.AsQueryable<VIEW_TRANSFER_PACKAGE_DETAIL>();
        

        [WebMethod(EnableSession = true)]
        public List<VIEW_TRANSFER_PACKAGE_PRICE_LIST> GetTransferPackage(int startIndex, int maximumRows, string sortExpression, string filterExpression, string scity, string sfname, string slname)
        {
            if (!String.IsNullOrEmpty(filterExpression))
            {
                TransferPackage = TransferPackage.Where(filterExpression);
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                TransferPackage = TransferPackage.OrderBy(sortExpression);
            }
            if (!String.IsNullOrEmpty(scity))
            {
                TransferPackageDataContext db = new TransferPackageDataContext();
                TransferPackage = db.VIEW_TRANSFER_PACKAGE_PRICE_LISTs.Where(p => (p.FIT_PACKAGE_NAME.Contains(scity)));
            }
            if (!String.IsNullOrEmpty(sfname))
            {
                TransferPackageDataContext db = new TransferPackageDataContext();
                TransferPackage = db.VIEW_TRANSFER_PACKAGE_PRICE_LISTs.Where(p => (p.FROM_PLACE.Contains(sfname)));
            }
            if (!String.IsNullOrEmpty(slname))
            {
                TransferPackageDataContext db = new TransferPackageDataContext();
                TransferPackage = db.VIEW_TRANSFER_PACKAGE_PRICE_LISTs.Where(p => (p.TO_PLACE.Contains(slname)));
            }
            
            else
            {
                TransferPackage = TransferPackage.OrderBy("TRANSFER_PACKAGE_PRICE_ID ASC");
            }

            return TransferPackage.Skip(startIndex).Take(maximumRows).ToList();


        }
        
        [WebMethod(EnableSession = true)]
        public int GetTransferPackageCount()
        {
            return (int)TransferPackage.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertUpdateTransferPackage(ArrayList TransferPackage)
        {

            TransferPackageStoredProcedure objinserttransfer = new TransferPackageStoredProcedure();
            TransferPackage.Insert(19, Session["usersid"].ToString());
            objinserttransfer.InsertUpdateTransferPackage(TransferPackage);
            //System.Data.DataSet ds1 = objinserttransfer.CheckValidation();


            //int j = 0;
            //int k = 0;
            //int l = 0;
            //int m = 0;
            //int n = 0;
            //int O = 0;
           
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{

            //    if (ds1.Tables[0].Rows[i]["CURRENCY_NAME"].ToString().Equals(TransferPackage[6]) || TransferPackage[6].Equals(""))
            //    {
            //        j = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    if (ds1.Tables[1].Rows[i]["CHAIN_NAME"].ToString().Equals(TransferPackage[10]) || TransferPackage[10].Equals(""))
            //    {
            //        k = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[2].Rows.Count; i++)
            //{

            //    if (ds1.Tables[2].Rows[i]["PAYMENT_TERMS"].ToString().Equals(TransferPackage[7]) || TransferPackage[7].Equals(""))
            //    {
            //        l = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[3].Rows.Count; i++)
            //{

            //    if (ds1.Tables[3].Rows[i]["STATUS"].ToString().Equals(TransferPackage[11]) || TransferPackage[11].Equals(""))
            //    {
            //        m = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[4].Rows.Count; i++)
            //{

            //    if (ds1.Tables[4].Rows[i]["AGENT"].ToString().Equals(TransferPackage[8]) || TransferPackage[8].Equals(""))
            //    {
            //        n = 1;
            //        break;

            //    }

            //}
            //for (int i = 0; i < ds1.Tables[7].Rows.Count; i++)
            //{

            //    if (ds1.Tables[7].Rows[i]["FIT_PACKAGE_NAME"].ToString().Equals(TransferPackage[9]) || TransferPackage[9].Equals(""))
            //    {
            //        O = 1;
            //        break;

            //    }

            //}
            //if (j == 0)
            //{
            //    s = "currency";
            //}
            //else if (j == 1 && k == 0)
            //{
            //    s = "Restaurant";
            //}
            //else if (j == 1 && k == 1 && l == 0)
            //{
            //    s = "Payment";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 0)
            //{
            //    s = "status";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 0)
            //{
            //    s = "agent";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O == 0)
            //{
            //    s = "Package";
            //}
            //else if (j == 1 && k == 1 && l == 1 && m == 1 && n == 1 && O == 1)
            //{
                
            //    System.Data.DataSet ds = objinserttransfer.InsertUpdateTransferPackage(TransferPackage);
            //    s = ds.Tables[0].Rows[0]["ABC"].ToString();  

            //}
            //else
            //{

            //}
            //return s;
        }
        
        [WebMethod(EnableSession = true)]
        public void delTransferPackage(int deltransferpackageid)
        {
            TransferPackageStoredProcedure objdeltransfer = new TransferPackageStoredProcedure();
            objdeltransfer.delTransferPackage(deltransferpackageid);
        }

        [WebMethod(EnableSession = true)]
        public void CopyData(int transid)
        {

            TransferPackageStoredProcedure objempentity = new TransferPackageStoredProcedure(); 
            System.Data.DataSet ds = objempentity.CopyData(transid);
           
        }
        
        [WebMethod(EnableSession = true)]
        public string InsertUpdateTransferPackagedetail(ArrayList detail,string d)
        {

            CRM.DataAccess.AdministratorEntity.TransferPackageStoredProcedure objinserttransferdetail = new CRM.DataAccess.AdministratorEntity.TransferPackageStoredProcedure();
            System.Data.DataSet ds2 = objinserttransferdetail.CheckValidation();

            int j = 0;
            int k = 0;
            int l = 0;
            int m = 0;
           
            for (int i = 0; i < ds2.Tables[13].Rows.Count; i++)
            {

                if (ds2.Tables[13].Rows[i]["TRANSFER_PACKAGE_FROM_TO_NAME"].ToString().Equals(detail[1]) || detail[1].Equals(""))
                {
                    j = 1;
                    break;

                }

            }
            for (int i = 0; i < ds2.Tables[13].Rows.Count; i++)
            {

                if (ds2.Tables[13].Rows[i]["TRANSFER_PACKAGE_FROM_TO_NAME"].ToString().Equals(detail[2]) || detail[2].Equals(""))
                {
                    k = 1;
                    break;

                }

            }
            for (int i = 0; i < ds2.Tables[15].Rows.Count; i++)
            {

                if (ds2.Tables[15].Rows[i]["SIGHT_SEEING_PACKAGE_NAME"].ToString().Equals(detail[3]) || detail[3].Equals(""))
                {
                    l = 1;
                    break;

                }

            }
            for (int i = 0; i < ds2.Tables[14].Rows.Count; i++)
            {

                if (ds2.Tables[14].Rows[i]["ARRIVAL_DEPARTURE_TYPE"].ToString().Equals(detail[4]) || detail[4].Equals(""))
                {
                    m = 1;
                    break;

                }

            }

            if (j == 0)
            {
                d = "From_Place";
            }
            else if (j == 1 && k == 0)
            {
                d = "To_Place";
            }
            else if (j == 1 && k == 1 && l == 0)
            {
                d = "Sight_Package";
            }
            else if (j == 1 && k == 1 && l == 1 && m == 0)
            {
                d = "flag";
            }
            else if (j == 1 && k == 1 && l == 1 && m == 1)
            {

                objinserttransferdetail.InsertUpdateTransferDetail(detail);

            }
            else
            {

            }
            return d;
            
        }
        [WebMethod(EnableSession = true)]
        public List<VIEW_TRANSFER_PACKAGE_DETAIL> GetTransferPackagedetail(string TRANSFER_PACKAGE_PRICE_ID)
        {
            DETAIL = DETAIL.Where(String.Format(@"TRANSFER_PACKAGE_PRICE_ID == {0}", TRANSFER_PACKAGE_PRICE_ID));
            return DETAIL.ToList();
        }
        [WebMethod(EnableSession = true)]
        public int GetTransferdetailCount()
        {
            return (int)DETAIL.Count();
        }
        [WebMethod(EnableSession = true)]
        public void InsertNewTransferDetail(string TRANSFER_PACKAGE_PRICE_ID)
        {
            TransferPackageStoredProcedure objinsertnewdetail = new TransferPackageStoredProcedure();
            objinsertnewdetail.InsertNewTransferDetail(TRANSFER_PACKAGE_PRICE_ID);
        }
    }
}
