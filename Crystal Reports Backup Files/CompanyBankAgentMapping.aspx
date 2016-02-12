<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CompanyBankAgentMapping.aspx.cs" Inherits="CRM.WebApp.Views.Account.CompanyBankAgentMapping" %>


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

         <script type="text/javascript" src="../Shared/Javascripts/CompanyBankAgentMapping.js"></script>
       
         <script type="text/javascript">

             function pageLoad() {

                 CompanyBankAgentTableView = $find("<%= radgridCompanyBankAgentMapping.ClientID %>").get_masterTableView();
                 CompanyBankAgentCommand = "Load";

                 CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.GetCompanyBankAgent(0, CompanyBankAgentTableView.get_pageSize(), CompanyBankAgentTableView.get_sortExpressions().toString(), CompanyBankAgentTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankAgentdetail);
             }

             function addCompanyBankDetails(sender, args) {

                 currentRowIndex = sender.parentNode.parentNode.rowIndex;
                 var ary = [];

                 ary[1] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
                 ary[2] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_COMPANY_NAME").value;
                 ary[3] = CompanyBankAgentTableView.get_dataItems()[currentRowIndex - 1].findElement("COMP_BANK_BRNACH").value;
                 ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.BANK_MAPPING_ID;

                 if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                 try {

                     CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.InsetUpdateCompanyBankAgent(ary);
                     alert('Record Save Successfully');
                     CRM.WebApp.webservice.CompanyBankAgnetMappingWebservice.GetCompanyBankAgent(0, CompanyBankAgentTableView.get_pageSize(), CompanyBankAgentTableView.get_sortExpressions().toString(), CompanyBankAgentTableView.get_filterExpressions().toDynamicLinq(), updateCompanyBankAgentdetail);

                 }
                 catch (e) {
                     alert('Wrong Data Inserted');
                 }
             }
             function getComapnyBankName(sender) {

                 var value = sender.value;
                 CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
             }
         </script>

    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Company Bank Agent Mapping"></asp:Literal>
    </div>
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var company_name = "../../webservice/autocomplete.ashx?key=FETCH_AGENT_COMPANY ";
            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var company_bank_branch = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_BRANCH?" + globalvalue;
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridCompanyBankAgentMapping_ctl00_ctl" + i + "_CUST_COMPANY_NAME").autocomplete(company_name);
                $("#ctl00_cphPageContent_radgridCompanyBankAgentMapping_ctl00_ctl" + i + "_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridCompanyBankAgentMapping_ctl00_ctl" + i + "_COMP_BANK_BRNACH").autocomplete(company_bank_branch);
            }

        });       
    </script>
    <br />
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCompanyBankAgentMapping" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView ClientDataKeyNames="BANK_MAPPING_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="900px">
                    <RowIndicatorColumn>
                     </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="BANK_MAPPING_ID" DataField="BANK_MAPPING_ID" HeaderText="BANK_MAPPING_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_MAPPING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                     <telerik:GridTemplateColumn SortExpression ="CUST_COMPANY_NAME" DataField="CUST_COMPANY_NAME" HeaderText="Company Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="BANK_NAME" DataField="BANK_NAME" HeaderText="Bank Name">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_NAME" runat="server" CssClass="radinput" onblur="getComapnyBankName(this);" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COMP_BANK_BRNACH" DataField="COMP_BANK_BRNACH" HeaderText="Bank Branch">
                          <ItemTemplate>
                            <asp:TextBox ID="COMP_BANK_BRNACH" runat="server" CssClass="radinput" ></asp:TextBox>
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
                <ClientEvents OnCommand="radgridCompanyBankAgentMapping_Command" OnRowSelected="radgridCompanyBankAgentMapping_RowSelected" OnRowDblClick="addCompanyBankAgent"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>
