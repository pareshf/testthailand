<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.SupplierMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        div.RadGrid_Default .rgFilterRow td
        {
            background-color: #e5e5e5;
        }
        div.RadGrid_Default .rgHeader
        {
            background-color: #F3F3F3;
            background-position: 0 0;
            background-repeat: repeat-x !important;
            border-color: #E6E6E6 #E6E6E6 #CCCCCC;
            color: #636363;
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            height: 25px;
            line-height: 16px;
            text-align: left;
            text-decoration: none;
            text-indent: 0;
        }
        
        .RadMenu_Default
        {
            background-color: #fff;
            border: solid 0px #fff;
        }
        .RadMenu_Default UL.rmRootGroup
        {
            background-color: #fff;
            border: solid 0px #fff;
            padding: 2px;
        }
        .RadMenu rmLink
        {
            padding-left: 0px;
        }
        .RadMenu_Default .rmLink
        {
            color: #000;
            text-decoration: none;
            font-family: Verdana;
            font-size: 8pt;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText
        {
            border: solid 0px #fff;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem:hover
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink
        {
            color: #000;
            padding-top: 2px;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadPanelBar_Default .rpSlide
        {
            padding-left: 2px;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .radinput
        {
            width: 100%;
            border: 0px solid #c2c2c2;
        }
    </style>

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/SupplierMasterSp.js"></script>
        <script type="text/javascript">
            var empid;
            var supp_id = "";
            function pageLoad() {

                empid = '<%=Session["empid"]%>';
                supp_id = '<%=Session["supplier_id"]%>';


                SupplierTableView = $find("<%= radgridsuppliermaster.ClientID %>").get_masterTableView();
                ContectDetailTableView = $find("<%= radgridsupplierContectDetail.ClientID %>").get_masterTableView();
                SupplierEmployeeTableView = $find("<%= radgridsupplierEmployeeDetail.ClientID %>").get_masterTableView();
                SupplierCommand = "Load";
                //ContectDetailCommand = "Load";

                if (empid == '121') {
                    

                    if (SupplierTableView.PageSize = 10) {
                        CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);
                    }
                    else if (SupplierTableView.PageSize > 10) {
                        CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);
                    }
                    else if (SupplierTableView.PageSize > 20) {
                        CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);
                    }
                }
                else {
                    CRM.WebApp.webservice.SupplierMasterWebService.GetCustDetailWithID(supp_id, updateSupplierName);
                }

            }


            function deleteCurrent() {

                CRM.WebApp.webservice.SupplierMasterWebService.deleteSupplier(SUPPLIER_ID);
                CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);


            }
            function deleteContectDetail() {


                CRM.WebApp.webservice.SupplierMasterWebService.deleteSupplierContect(SUPPLIER_SR_NO);
                CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, website, branch, updateContectdetail);

            }
            function deleteEmployeeDetail() {


                CRM.WebApp.webservice.SupplierMasterWebService.deleteSupplierEmployee(SUPPLIER_REL_SRNO);
                CRM.WebApp.webservice.SupplierMasterWebService.EmployeeDetail(SUPPLIER_SR_NO, updateSupplierEmployee);


            }
            function addnewSupplier(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[0] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_COMPANY_NAME").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_ID;
                ary[2] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("REMARKS").value;
                // ary[3] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_PHOTO_PATH").value;
                //ary[4] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("CREDIT_LIMIT").value;
                ary[4] = "0";
                // ary[5] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMUNICATION_TIME").value;
                //ary[6] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("ACCOUNTING_CODE").value;
                ary[7] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_UNQ_ID;
                //ary[8] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[8] = "";
                ary[9] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMUNICATION_MODE_NAME").value;
                //ary[10] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("DESIGNATION_DESC").value;
                ary[11] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_TYPE_NAME").value;
                // ary[12] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_NAME").value;
                ary[13] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_MOBILE").value;
                ary[14] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_PHONE").value;
                ary[15] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_EMAIL").value;
                ary[16] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("PASSWORD").value;
                //ary[17] = SupplierTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_ISSUE_SERVICE_VOUCHER").value;
                ary[17] = "";
                ary[20] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_REL_SRNO;
                for (i = 0; i < 21; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                if (ary[15] != '' && ary[16] != '') {
                    if (ary[0] != '') {

                        try {

                            CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplier(ary, s, OnCallBack1);
                            CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);

                            //alert('Record Save Successfully');

                        }
                        catch (e) {
                            alert('Wrong Data Inserted');
                        }
                    }

                    else {
                        alert('Enter Supplier Company Name.')
                    }
                }
                else {
                    alert('Enter Email & Password.')
                }
            }
            function OnCallBack1(results, userContext, sender) {

               
                
                if (results == "N") {

                    alert('This Email Already Exist.');
                }
                else if (results == "Y") {

                    alert('Record Save Successfully');
                }
                else if (results == "") {
                    alert('Record Save Successfully');
                }

            }
            function addnewSupplierContect(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_TYPE_NAME").value;
                ary[2] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE1").value;
                ary[3] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE2").value;
                ary[4] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[5] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[6] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[7] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("PINCODE").value;
                ary[8] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;
                ary[9] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("WEBSITE").value;
                ary[10] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CHAIN_NAME").value;
                //ary[11] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("VIDEO_CODE").value;
                // ary[12] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("LOGO").value;
                ary[13] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_IN_TIME").value;
                ary[14] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_OUT_TIME").value;
                ary[15] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("FAX_NO_1").value;
                ary[16] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("FAX_NO_2").value;
                ary[17] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("RATING_STAR").value;
                ary[18] = ContectDetailTableView.get_dataItems()[currentRowIndex - 1].findElement("LOCATION").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_SR_NO;
                //ary[17] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[1].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_REL_SRNO;
                for (i = 0; i < 16; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
                }
                try {
                    if (ary[17] > 5) {
                        alert('Hotel Star value not allowed greater then 5.');
                    }
                    else {
                        CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplierContect(ary);
                        //CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(SupplierTableView.get_currentPageIndex() * SupplierTableView.get_pageSize(), SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), updateSupplierName);
                        CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);
                        alert('Record Save Successfully');
                    }

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function AddNewDetail() {

                CRM.WebApp.webservice.SupplierMasterWebService.InsertNewContect(SUPPLIER_ID);
                CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);
            }
            function addnewSupplierEmployee(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
                ary[2] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_SURNAME").value;
                ary[3] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_NAME").value;
                //ary[4] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_GENDER").value;
                ary[4] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_EMAIL").value;
                ary[5] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_MOBILE").value;
                ary[6] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_REL_PHONE").value;
                ary[7] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("PASSWORD").value;
                ary[8] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("ALTERNATE_EMAIL").value;
                //ary[10] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_ACCOUNT").value;
                ary[9] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("USER_STATUS_NAME").value;
                //ary[12] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("CREDIT_LIMIT").value;
                //ary[13] = SupplierEmployeeTableView.get_dataItems()[currentRowIndex - 1].findElement("PARENT_SUPPLIER").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_REL_SRNO;


                for (i = 0; i < 10; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = "";
                }

                if (ary[4] != '' && ary[7] != '') {
                    try {

                        CRM.WebApp.webservice.SupplierMasterWebService.InsertUpdateSupplierEmployeeNew(ary, s, OnCallBack);
                        CRM.WebApp.webservice.SupplierMasterWebService.EmployeeDetail(SUPPLIER_SR_NO, updateSupplierEmployee);

                        //alert('Record Save Successfully');

                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }
                }
                else {
                    alert('Enter Valid Email & Password.')
                }
            }
            function OnCallBack(results, userContext, sender) {

                
                if (results == "N") {

                    alert('This Email Already Exist.');
                }
                else if (results == "Y") {

                    alert('Record Save Successfully');
                }
                else if (results == "") {
                    alert('Record Save Successfully');
                }

            }
            function AddNewEmployeeDetail() {

                CRM.WebApp.webservice.SupplierMasterWebService.InsertNewEmployee(SUPPLIER_SR_NO);
                CRM.WebApp.webservice.SupplierMasterWebService.EmployeeDetail(SUPPLIER_SR_NO, updateSupplierEmployee);
            }
            function showpnl() {
                document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                document.getElementById('<%=Button4.ClientID %>').style.display = "";
                document.getElementById('<%=Button3.ClientID %>').style.display = "none";
            }
            function SearchResult() {

                document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                document.getElementById('<%=Button3.ClientID %>').style.display = "";
                document.getElementById('<%=Button4.ClientID %>').style.display = "none";


                scommode = $("#ctl00_cphPageContent_txtcommunicatinmode").val();
                sfname = $("#ctl00_cphPageContent_txtfname").val();
                slname = $("#ctl00_cphPageContent_txtlname").val();
                semail = $("#ctl00_cphPageContent_txtemail").val();
                smob = $("#ctl00_cphPageContent_txtMobile").val();
                stele = $("#ctl00_cphPageContent_txttelephon").val();

                city = $("#ctl00_cphPageContent_txtCity").val();
                state = $("#ctl00_cphPageContent_txtstate").val();
                country = $("#ctl00_cphPageContent_txtcountry").val();
                // branch = $("#ctl00_cphPageContent_txtbranch").val();
                //website = $("#ctl00_cphPageContent_txtwebsite").val();

                var checking = document.getElementById('<%= chkrel.ClientID %>');
                if (checking.checked == true) {


                    // CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(0, SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);
                    CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);
                }
                else {

                    CRM.WebApp.webservice.SupplierMasterWebService.GetSupplier(SupplierTableView.get_currentPageIndex() * SupplierTableView.get_pageSize(), SupplierTableView.get_pageSize(), SupplierTableView.get_sortExpressions().toString(), SupplierTableView.get_filterExpressions().toDynamicLinq(), scommode, sfname, slname, semail, smob, stele, updateSupplierName);
                    //CRM.WebApp.webservice.SupplierMasterWebService.Contectdetail(SUPPLIER_ID, city, state, country, updateContectdetail);
                }

            }
            function GenerateCode() {

                CRM.WebApp.webservice.SupplierMasterWebService.InsertCode(SUPPLIER_COMPANY_NAME, SUPPLIER_ID, Supplier);
                alert("Code Generated Sucessfully");
            }
          </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            var modename = "../../webservice/autocomplete.ashx?key=FETCH_COMMUNICATION_MODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var groupname = "../../webservice/autocomplete.ashx?key=FETCH_GROUPE_NAME_FOR_CUST_CUSTOMER_MASTER";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_NAME";
            var Designation = "../../webservice/autocomplete.ashx?key=FETCH_DESIGNATION_FOR_FAR_HOTEL_MASTER";
            var SupplierType = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_TYPE_NAME_FOR_AUTO_SEARCH";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var addresstype = "../../webservice/autocomplete.ashx?key=FETCH_ADDRESS_TYPE_FOR_EMPLOYEE_MASTER";
            var emptitle = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var gender = "../../webservice/autocomplete.ashx?key=FETCH_GENDER_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var userstatus = "../../webservice/autocomplete.ashx?key=GET_USER_STATUS_FOR_CUSTOMER_RELATION_AUTOSEARCH";
            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var parentsupplier = "../../webservice/autocomplete.ashx?key=GET_PARENT_SUPPLIER_NAME_FOR_SUPPLIER_EMPLOYEE_AUTOSEARCH";
            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var supplier_company = "../../webservice/autocomplete.ashx?key=GET_SUPPLIER_NAME_FOR_SUPPLIER";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_COMMUNICATION_MODE_NAME").autocomplete(modename);
                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_GROUP_NAME").autocomplete(groupname);
                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_DESIGNATION_DESC").autocomplete(Designation);
                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_SUPPLIER_TYPE_NAME").autocomplete(SupplierType);
                $("#ctl00_cphPageContent_radgridsuppliermaster_ctl00_ctl" + i + "_TO_ISSUE_SERVICE_VOUCHER").autocomplete(a);

                $("#ctl00_cphPageContent_radgridsupplierContectDetail_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridsupplierContectDetail_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridsupplierContectDetail_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridsupplierContectDetail_ctl00_ctl" + i + "_ADDRESS_TYPE_NAME").autocomplete(addresstype);


                $("#ctl00_cphPageContent_radgridsupplierEmployeeDetail_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(emptitle);
                $("#ctl00_cphPageContent_radgridsupplierEmployeeDetail_ctl00_ctl" + i + "_SUPPLIER_REL_GENDER").autocomplete(gender);
                $("#ctl00_cphPageContent_radgridsupplierEmployeeDetail_ctl00_ctl" + i + "_USER_STATUS_NAME").autocomplete(userstatus);
                $("#ctl00_cphPageContent_radgridsupplierEmployeeDetail_ctl00_ctl" + i + "_IS_ACCOUNT").autocomplete(a);
                $("#ctl00_cphPageContent_radgridsupplierEmployeeDetail_ctl00_ctl" + i + "_PARENT_SUPPLIER").autocomplete(parentsupplier);


                $("#ctl00_cphPageContent_txtcommunicatinmode").autocomplete(modename);
                $("#ctl00_cphPageContent_txtlname").autocomplete(SupplierType);

                $("#ctl00_cphPageContent_txtCity").autocomplete(city);
                $("#ctl00_cphPageContent_txtstate").autocomplete(state);
                $("#ctl00_cphPageContent_txtcountry").autocomplete(country);
                $("#ctl00_cphPageContent_txtfname").autocomplete(supplier_company);

            }

        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Supplier?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnglCode" runat="server" CssClass="button" Style="float: right;
                        margin-right: 10px; color: black; font-weight: bold;" Text="Generate GL Code"
                        OnClientClick="GenerateCode();" Visible="false" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridsuppliermaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1800px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_ID" DataField="SUPPLIER_ID" HeaderText="Supplier Id" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_SRNO" DataField="SUPPLIER_REL_SRNO" HeaderText="SUPPLIER_REL_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_UNQ_ID" DataField="SUPPLIER_UNQ_ID" HeaderText="Supplier Unique ID" Visible="false">
                     <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_UNQ_ID" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GL_CODE" DataField="GL_CODE" HeaderText="GL Code">
                     <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="GL_CODE" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_COMPANY_NAME" DataField="SUPPLIER_COMPANY_NAME" HeaderText="Supplier Name">
                    <HeaderStyle HorizontalAlign="Left" Width="150px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_TYPE_NAME" DataField="SUPPLIER_TYPE_NAME" HeaderText="Supplier Type">
                       <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_MOBILE" DataField="SUPPLIER_REL_MOBILE" HeaderText="Mobile">
                       <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_PHONE" DataField="SUPPLIER_REL_PHONE" HeaderText="Phone">
                       <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_EMAIL" DataField="SUPPLIER_REL_EMAIL" HeaderText="Email [User Name]">
                       <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PASSWORD" DataField="PASSWORD" HeaderText="Password">
                    <HeaderStyle HorizontalAlign="Left" Width="150px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="PASSWORD" runat="server" CssClass="radinput" TextMode="Password"></asp:TextBox>
                        </ItemTemplate>  
               </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_NAME" DataField="GROUP_NAME" HeaderText="Group" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Credit Limit Currency" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CREDIT_LIMIT" DataField="CREDIT_LIMIT" HeaderText="Credit Limit" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="CREDIT_LIMIT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACCOUNTING_CODE" DataField="ACCOUNTING_CODE" HeaderText="Accounting Code" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="ACCOUNTING_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COMMUNICATION_MODE_NAME" DataField="COMMUNICATION_MODE_NAME" HeaderText="Prefered Commnication Mode">
                    <HeaderStyle HorizontalAlign="Left" Width="150px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="COMMUNICATION_MODE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn SortExpression ="COMMUNICATION_TIME" DataField="COMMUNICATION_TIME" HeaderText="Communication Time" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="COMMUNICATION_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                <telerik:GridTemplateColumn SortExpression ="DESIGNATION_DESC" DataField="DESIGNATION_DESC" HeaderText="Designation" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="DESIGNATION_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                 <telerik:GridTemplateColumn SortExpression ="SUPPLIER_PHOTO_PATH" DataField="SUPPLIER_PHOTO_PATH" HeaderText="Supplier Photo" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_PHOTO_PATH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="REMARKS" DataField="REMARKS" HeaderText="Remarks">
                    <HeaderStyle HorizontalAlign="Left" Width="80px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="REMARKS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn> 

                    <telerik:GridTemplateColumn SortExpression ="TO_ISSUE_SERVICE_VOUCHER" DataField="TO_ISSUE_SERVICE_VOUCHER" HeaderText="TO Issue Service Voucher?" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="120px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="TO_ISSUE_SERVICE_VOUCHER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn> 

                    <telerik:GridTemplateColumn SortExpression ="CREATED_BY" DataField="CREATED_BY" HeaderText="Created By" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CREATED_BY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DATE_CREATED" DataField="DATE_CREATED" HeaderText="Date Created" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="DATE_CREATED" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MODIFIED_BY" DataField="MODIFIED_BY" HeaderText="Modified By" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MODIFIED_BY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MODIFY_DATE" DataField="MODIFY_DATE" HeaderText="Modified Date" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MODIFY_DATE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewSupplier(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridsuppliermaster_Command" OnRowSelected="radgridsuppliermaster_RowSelected" OnRowDblClick="addSupplier"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        <div id="div3">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Search" OnClientClick="showpnl();"
                            Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button" />
                        <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                            display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                            <table>
                                <tr>
                                    <%--<td>
                                    <asp:Label ID="cust_id" runat="server" Text="Customer Id:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcust_id" runat="server" Width="250px"></asp:TextBox>
                                </td>--%>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Contact Detail:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkrel" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblfname" runat="server" Text="Supplier Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbllaname" runat="server" Text="Supplier Type:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlname" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtemail" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblmobile" runat="server" Text="Mobile No.:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMobile" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Telephone No.:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttelephon" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Communication Mode:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcommunicatinmode" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="City:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="State:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtstate" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Country:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcountry" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Branch:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtbranch" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Website:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtwebsite" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div class="pageTitle" style="float: left">
                <asp:Literal ID="Literal1" runat="server" Text="Supplier Contect Detail"></asp:Literal>
            </div>
            <br />
            <br />
            <div id="Div1">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                                color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Contect Detail?'))return false; deleteContectDetail(); return false;"
                                Text="Delete" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <telerik:radgrid id="radgridsupplierContectDetail" runat="server" allowpaging="false"
                                allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                                enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1500px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_SR_NO" DataField="SUPPLIER_SR_NO" HeaderText="Supplier Id" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_ID" DataField="SUPPLIER_ID" HeaderText="Supplier ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   <%-- <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_ID" DataField="SUPPLIER_REL_ID" HeaderText="SUPPLIER_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn SortExpression ="CHAIN_NAME" DataField="CHAIN_NAME" HeaderText="Branch">
                          <ItemTemplate>
                            <asp:TextBox ID="CHAIN_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn> 

                      <telerik:GridTemplateColumn SortExpression ="ADDRESS_TYPE_NAME" DataField="ADDRESS_TYPE_NAME" HeaderText="Address Type">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE1" DataField="ADDRESS_LINE1" HeaderText="Address Line1">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE2" DataField="ADDRESS_LINE2" HeaderText="Address Line2">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PINCODE" DataField="PINCODE" HeaderText="Pincode">
                          <ItemTemplate>
                            <asp:TextBox ID="PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn SortExpression ="PHONE" DataField="PHONE" HeaderText="Phone">
                          <ItemTemplate>
                            <asp:TextBox ID="PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                 <telerik:GridTemplateColumn SortExpression ="WEBSITE" DataField="WEBSITE" HeaderText="WebSite">
                          <ItemTemplate>
                            <asp:TextBox ID="WEBSITE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FAX_NO_1" DataField="FAX_NO_1" HeaderText="Fax 1">
                          <ItemTemplate>
                            <asp:TextBox ID="FAX_NO_1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FAX_NO_2" DataField="FAX_NO_2" HeaderText="Fax 2">
                          <ItemTemplate>
                            <asp:TextBox ID="FAX_NO_2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="VIDEO_CODE" DataField="VIDEO_CODE" HeaderText="Video" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="VIDEO_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="LOGO" DataField="LOGO" HeaderText="LOGO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="LOGO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHECK_IN_TIME" DataField="CHECK_IN_TIME" HeaderText="Check In Time">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECK_IN_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHECK_OUT_TIME" DataField="CHECK_OUT_TIME" HeaderText="Check Out Time">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECK_OUT_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="RATING_STAR" DataField="RATING_STAR" HeaderText="Star">
                          <ItemTemplate>
                            <asp:TextBox ID="RATING_STAR" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="LOCATION" DataField="LOCATION" HeaderText="Location">
                          <ItemTemplate>
                            <asp:TextBox ID="LOCATION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewSupplierContect(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridsupplierContectDetail_Command" OnRowSelected="radgridsupplierContectDetail_RowSelected" OnRowDblClick="addContect"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                            <asp:LinkButton ID="lbAddContact" runat="server" Text="Add Another Contact Detail"
                                OnClientClick="AddNewDetail();"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal2" runat="server" Text="Supplier Employee Detail"></asp:Literal>
        </div>
        <br />
        <br />
        <div id="Div2">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Button2" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Employee Detail?'))return false; deleteEmployeeDetail(); return false;"
                            Text="Delete" runat="server" Visible="false" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <telerik:radgrid id="radgridsupplierEmployeeDetail" runat="server" allowpaging="false"
                            allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                            enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SUPPLIER_REL_SRNO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1500px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_SRNO" DataField="SUPPLIER_REL_SRNO" HeaderText="SUPPLIER_REL_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_SR_NO" DataField="SUPPLIER_SR_NO" HeaderText="SUPPLIER_SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_SR_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   <%-- <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_ID" DataField="SUPPLIER_REL_ID" HeaderText="SUPPLIER_REL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                      <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_SURNAME" DataField="SUPPLIER_REL_SURNAME" HeaderText="Surname">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_NAME" DataField="SUPPLIER_REL_NAME" HeaderText="Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_GENDER" DataField="SUPPLIER_REL_GENDER" HeaderText="Gender" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_GENDER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_EMAIL" DataField="SUPPLIER_REL_EMAIL" HeaderText="Email [User Name]">
                    <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PASSWORD" DataField="PASSWORD" HeaderText="Password">
                          <ItemTemplate>
                            <asp:TextBox ID="PASSWORD" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                   
                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_MOBILE" DataField="SUPPLIER_REL_MOBILE" HeaderText="Mobile">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_REL_PHONE" DataField="SUPPLIER_REL_PHONE" HeaderText="Pincode">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_REL_PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                

                 <telerik:GridTemplateColumn SortExpression ="ALTERNATE_EMAIL" DataField="ALTERNATE_EMAIL" HeaderText="Alternate Email">
                          <ItemTemplate>
                            <asp:TextBox ID="ALTERNATE_EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_ACCOUNT" DataField="IS_ACCOUNT" HeaderText="IS Account" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_ACCOUNT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn SortExpression ="PARRENT_SUPPLIER_ID" DataField="PARRENT_SUPPLIER_ID" HeaderText="PARRENT_SUPPLIER_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PARRENT_SUPPLIER_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="USER_STATUS_NAME" DataField="USER_STATUS_NAME" HeaderText="User Status">
                          <ItemTemplate>
                            <asp:TextBox ID="USER_STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CREDIT_LIMIT" DataField="CREDIT_LIMIT" HeaderText="Credit Limit Currency" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CREDIT_LIMIT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PARENT_SUPPLIER" DataField="PARENT_SUPPLIER" HeaderText="PARENT_SUPPLIER" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PARENT_SUPPLIER" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewSupplierEmployee(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridsupplierEmployeeDetail_Command" OnRowSelected="radgridsupplierEmployeeDetail_RowSelected" OnRowDblClick="addEmployee"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Add Another Employee Detail"
                            OnClientClick="AddNewEmployeeDetail();"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
