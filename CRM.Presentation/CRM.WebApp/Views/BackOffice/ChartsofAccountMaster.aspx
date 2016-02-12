<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ChartsofAccountMaster.aspx.cs" Inherits="CRM.WebApp.Views.Account.ChartsofAccountMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cphIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
     <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="cphPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
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
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/ChartsofAccounts.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                ChartAccountTableView = $find("<%= radgridchartofaccount.ClientID %>").get_masterTableView();
                ChartAccountCommandName = "Load";
                CRM.WebApp.webservice.ChartsofAccountsWebService.GetChartsofAccounts(ChartAccountTableView.get_currentPageIndex() * ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_sortExpressions().toString(), ChartAccountTableView.get_filterExpressions().toDynamicLinq(), updatechartofAccounts);


            }
            function deleteCurrent() {
                
                CRM.WebApp.webservice.ChartsofAccountsWebService.delChartsofAccount(CHART_OF_ACCOUNTS_ID);
                CRM.WebApp.webservice.ChartsofAccountsWebService.GetChartsofAccounts(ChartAccountTableView.get_currentPageIndex() * ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_sortExpressions().toString(), ChartAccountTableView.get_filterExpressions().toDynamicLinq(), updatechartofAccounts);
            }
            function addChartsofAccount(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[1] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("GL_CODE").value;
                ary[2] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("GL_DESCRIPTION").value;
                ary[3] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_NAME").value;
                ary[4] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("SIDE_CODE_NAME").value;
                ary[5] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("OP_BALANCE").value;
                ary[6] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("OP_BAL_TYPE").value;
                ary[7] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("OP_BALANCE_MONTH").value;
                ary[8] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("CL_BALANCE").value;
                ary[9] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("CL_BAL_TYPE").value;
                ary[10] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("CL_BALANCE_MONTH").value;
                ary[11] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
                ary[12] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPPLIER_AGENT_ID").value;
                ary[13] = ChartAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("ACC_NAME").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CHART_OF_ACCOUNTS_ID;
                //                for (i = 0; i < 8; i++) {
                //                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                //                }
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                try {
                    CRM.WebApp.webservice.ChartsofAccountsWebService.InsertUpdateChartAccount(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.ChartsofAccountsWebService.GetChartsofAccounts(ChartAccountTableView.get_currentPageIndex() * ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_pageSize(), ChartAccountTableView.get_sortExpressions().toString(), ChartAccountTableView.get_filterExpressions().toDynamicLinq(), updatechartofAccounts);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Charts of Account Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            //var grouptype = "../../webservice/autocomplete.ashx?key=GET_GROUPTYPE_FOR_AUTOSEARCH";
            var sidecode = "../../webservice/autocomplete.ashx?key=GET_SIDE_CODE_FOR_AUTOSEARCH";
            var company = "../../webservice/autocomplete.ashx?key=GET_COMPANY_NAME_FOR_AUTOSEARCH";
            var groupdisplay = "../../webservice/autocomplete.ashx?key=GET_GROUP_DISPLAY_FOR_AUTOSEARCH";
            var baltype = "../../webservice/autocomplete.ashx?key=GET_BALANCE_TYPE_FOR_AUTOSEARCH";
            var supplier_agent = "../../webservice/autocomplete.ashx?key=FETCH_AGENT_SUPPLIER_NAME_AUTOSEARCH";
            var groupname = "../../webservice/autocomplete.ashx?key=GET_GROUP_NAME_FOR_AUTOSEARCH";
            var company_acc_name = "../../webservice/autocomplete.ashx?key=GET_COMPANY_NAME_FOR_CHART_OF_ACCOUNT";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

               // $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_GROUP_TYPE").autocomplete(grouptype);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_SIDE_CODE_NAME").autocomplete(sidecode);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_COMPANY_NAME").autocomplete(company);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_GROUP_DISPLAY_NAME").autocomplete(groupdisplay);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_OP_BAL_TYPE").autocomplete(baltype);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_CL_BAL_TYPE").autocomplete(baltype);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_SUPPLIER_AGENT_ID").autocomplete(supplier_agent);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_GROUP_NAME").autocomplete(groupname);
                $("#ctl00_cphPageContent_radgridchartofaccount_ctl00_ctl" + i + "_ACC_NAME").autocomplete(company_acc_name);
            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Account?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server"/>
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridchartofaccount" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CHART_OF_ACCOUNTS_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1700px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CHART_OF_ACCOUNTS_ID" DataField="CHART_OF_ACCOUNTS_ID" HeaderText="CHART_OF_ACCOUNTS_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CHART_OF_ACCOUNTS_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GL_CODE" DataField="GL_CODE" HeaderText="GL Code">
                          <ItemTemplate>
                            <asp:TextBox ID="GL_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GL_DESCRIPTION" DataField="GL_DESCRIPTION" HeaderText="GL Description">
                          <ItemTemplate>
                            <asp:TextBox ID="GL_DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_NAME" DataField="GROUP_NAME" HeaderText="Group Name">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="SIDE_CODE_NAME" DataField="SIDE_CODE_NAME" HeaderText="Side Code">
                          <ItemTemplate>
                            <asp:TextBox ID="SIDE_CODE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OP_BALANCE" DataField="OP_BALANCE" HeaderText="OP.Balance">
                          <ItemTemplate>
                            <asp:TextBox ID="OP_BALANCE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OP_BAL_TYPE" DataField="OP_BAL_TYPE" HeaderText="OP.Balance Type">
                          <ItemTemplate>
                            <asp:TextBox ID="OP_BAL_TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="OP_BALANCE_MONTH" DataField="OP_BALANCE_MONTH" HeaderText="OP.Balance Month">
                          <ItemTemplate>
                            <asp:TextBox ID="OP_BALANCE_MONTH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CL_BALANCE" DataField="CL_BALANCE" HeaderText="CL.Balance">
                          <ItemTemplate>
                            <asp:TextBox ID="CL_BALANCE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CL_BAL_TYPE" DataField="CL_BAL_TYPE" HeaderText="CL.Balance Type">
                          <ItemTemplate>
                            <asp:TextBox ID="CL_BAL_TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CL_BALANCE_MONTH" DataField="CL_BALANCE_MONTH" HeaderText="CL.Balance Month">
                          <ItemTemplate>
                            <asp:TextBox ID="CL_BALANCE_MONTH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Company">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_AGENT_ID" DataField="SUPPLIER_AGENT_ID" HeaderText="Supplier/Agent">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_AGENT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACC_NAME" DataField="ACC_NAME" HeaderText="Company Account">
                          <ItemTemplate>
                            <asp:TextBox ID="ACC_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addChartsofAccount(this,event);">
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
                <ClientEvents OnCommand="radgridchartofaccount_Command" OnRowSelected="radgridchartofaccount_RowSelected" OnRowDblClick="chartsofaccount"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
