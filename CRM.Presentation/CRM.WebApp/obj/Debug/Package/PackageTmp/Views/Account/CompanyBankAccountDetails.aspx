<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CompanyBankAccountDetails.aspx.cs" Inherits="CRM.WebApp.Views.Account.CompanyBankAccountDetails" %>

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

         <script type="text/javascript" src="../Shared/Javascripts/CompanyBankAccount.js"></script>
       
         <script type="text/javascript">

             function pageLoad() {

                 
                 CompanyBankAccountTableView = $find("<%= radgridCompanyBankAccount.ClientID %>").get_masterTableView();
                 CompanyBankAccountCommand = "Load";

                 //CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);

                 if (CompanyBankAccountTableView.PageSize = 10) {
                     CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);
                 }
                 else if (CompanyBankAccountTableView.PageSize > 10) {
                     CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);
                 }
                 else if (CompanyBankAccountTableView.PageSize > 20) {
                     CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);
                 }
             }

             function addCompanyBankDetails(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];
                 
                 ary[1] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME_BANK").value;
                 ary[2] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME_OF_COMP").value;
                 ary[3] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMP_BANK_BRNACH").value;
                 ary[4] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_ADD_COMP").value;
                 ary[5] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("ACC_NO_BANK").value;
                 ary[6] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_ACC_NAME").value;
                 ary[7] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("SWIFT_CODE").value;
                 ary[8] = CompanyBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("IBOB").value;
                 
                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.COMP_ACC_ID;

                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 try {

                     CRM.WebApp.webservice.CompanyBankAccountWebservice.InsetUpdateCompanyBank(ary, s, OnCallBack);
                     // alert('Record Save Successfully');
                     CRM.WebApp.webservice.CompanyBankAccountWebservice.GetCompanyBankAcount(0, CompanyBankAccountTableView.get_pageSize(), CompanyBankAccountTableView.get_sortExpressions().toString(), CompanyBankAccountTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankdetail);

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }
             }
             function OnCallBack(results, userContext, sender) {
                
                 if (results == "N") {

                     alert('This Account No. Already Exist.');
                 }
                 else if (results == "Y") {

                     alert('Record Save Successfully');
                 }
                 else if (results == "") {
                     alert('Record Save Successfully');
                 }


             }
         </script>



    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Company Bank Account Deatils"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var company_name = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_NAME_FROM_COMPANY_MASTER";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var ibob = "../../webservice/autocomplete.ashx?key=FETCH_INWORD_OUT_WORD_BANK_DROPDOWN";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridCompanyBankAccount_ctl00_ctl" + i + "_COMPANY_NAME_BANK").autocomplete(company_name);
                $("#ctl00_cphPageContent_radgridCompanyBankAccount_ctl00_ctl" + i + "_BANK_NAME_OF_COMP").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridCompanyBankAccount_ctl00_ctl" + i + "_IBOB").autocomplete(ibob);

            }

        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCompanyBankAccount" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="50" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView ClientDataKeyNames="COMP_ACC_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1200px">
                    <RowIndicatorColumn>
                     </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="COMP_ACC_ID" DataField="COMP_ACC_ID" HeaderText="COMP_ACC_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="COMP_ACC_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COMPANY_NAME_BANK" DataField="COMPANY_NAME_BANK" HeaderText="Company">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME_BANK" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BANK_NAME_OF_COMP" DataField="BANK_NAME_OF_COMP" HeaderText="Company Bank Name">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_NAME_OF_COMP" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COMP_BANK_BRNACH" DataField="COMP_BANK_BRNACH" HeaderText="Bank Branch">
                          <ItemTemplate>
                            <asp:TextBox ID="COMP_BANK_BRNACH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BANK_ADD_COMP" DataField="BANK_ADD_COMP" HeaderText="Bank Address">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_ADD_COMP" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACC_NO_BANK" DataField="ACC_NO_BANK" HeaderText="Company Account No.">
                          <ItemTemplate>
                            <asp:TextBox ID="ACC_NO_BANK" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BANK_ACC_NAME" DataField="BANK_ACC_NAME" HeaderText="Company Account Name">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_ACC_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SWIFT_CODE" DataField="SWIFT_CODE" HeaderText="Swift Code">
                          <ItemTemplate>
                            <asp:TextBox ID="SWIFT_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IBOB" DataField="IBOB" HeaderText="IW/OW Bank">
                          <ItemTemplate>
                            <asp:TextBox ID="IBOB" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                   
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addCompanyBankDetails(this,event);">
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
                <ClientEvents OnCommand="radgridCompanyBankAccount_Command" OnRowSelected="radgridCompanyBankAccount_RowSelected" OnRowDblClick="addCompanyBank"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
