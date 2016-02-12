<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AccountGroupMaster.aspx.cs" Inherits="CRM.WebApp.Views.Account.AccountGroupMaster" %>


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

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/AccountsGroupGridScript.js"></script>
        <script type="text/javascript">

            var company = '<%=Session["COMPANY_NAME"]%>';
           
            function pageLoad() {

                
                GroupTableView = $find("<%= radgridaccountgroup.ClientID %>").get_masterTableView();
                GroupCommandName = "Load";
               // CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);

                if (GroupTableView.PageSize = 10) {
                    CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);
                }
                else if (GroupTableView.PageSize > 10) {
                    CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);
                }
                else if (GroupTableView.PageSize > 20) {
                    CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);
                }
            }
            function deleteCurrent() {
                CRM.WebApp.webservice.AccountsGroupWebService.delAccountsGroup(ACCOUNT_GROUP_ID);
                CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);
            }
            function addAccountGroup(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_CODE").value;
                ary[2] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_NAME").value;
                ary[3] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_TYPE").value;
                ary[4] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_DISPLAY_NAME").value;
                ary[5] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_ORDER").value;
                ary[6] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("GROUP_CODE_UNDER").value;
                //ary[7] = GroupTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
                ary[7] = company;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.ACCOUNT_GROUP_ID;
//                for (i = 0; i < 8; i++) {
//                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                //                }
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                try {
                    CRM.WebApp.webservice.AccountsGroupWebService.InsertUpdateAccountsGroup(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.AccountsGroupWebService.GetAccountGroup(0, GroupTableView.get_pageSize(), GroupTableView.get_sortExpressions().toString(), GroupTableView.get_filterExpressions().toDynamicLinq(), updateAccountsGroupGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
  
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Account Group Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var grouptype = "../../webservice/autocomplete.ashx?key=GET_GROUPTYPE_FOR_AUTOSEARCH";
            var groupdisplay = "../../webservice/autocomplete.ashx?key=GET_GROUP_DISPLAY_FOR_AUTOSEARCH";
            var company = "../../webservice/autocomplete.ashx?key=GET_COMPANY_NAME_FOR_AUTOSEARCH";
            var groupname = "../../webservice/autocomplete.ashx?key=GET_GROUP_NAME_FOR_AUTOSEARCH";

                        for (i = 0; i < 55; i++) {
                            if (i < 10)
                                i = '0' + i;

                            $("#ctl00_cphPageContent_radgridaccountgroup_ctl00_ctl" + i + "_GROUP_TYPE").autocomplete(grouptype);
                            $("#ctl00_cphPageContent_radgridaccountgroup_ctl00_ctl" + i + "_GROUP_DISPLAY_NAME").autocomplete(groupdisplay);
                            $("#ctl00_cphPageContent_radgridaccountgroup_ctl00_ctl" + i + "_COMPANY_NAME").autocomplete(company);
                            $("#ctl00_cphPageContent_radgridaccountgroup_ctl00_ctl" + i + "_GROUP_CODE_UNDER").autocomplete(groupname);
                        }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Group?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server"/>
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridaccountgroup" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="ACCOUNT_GROUP_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1200px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="ACCOUNT_GROUP_ID" DataField="ACCOUNT_GROUP_ID" HeaderText="ACCOUNT_GROUP_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="ACCOUNT_GROUP_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GROUP_CODE" DataField="GROUP_CODE" HeaderText="Group Code">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_NAME" DataField="GROUP_NAME" HeaderText="Group Name">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_TYPE" DataField="GROUP_TYPE" HeaderText="Group Type">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_DISPLAY_NAME" DataField="GROUP_DISPLAY_NAME" HeaderText="Group Display">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_DISPLAY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="GROUP_ORDER" DataField="GROUP_ORDER" HeaderText="Group Order">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_ORDER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="GROUP_CODE_UNDER" DataField="GROUP_CODE_UNDER" HeaderText="Group Under">
                          <ItemTemplate>
                            <asp:TextBox ID="GROUP_CODE_UNDER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <%--<telerik:GridTemplateColumn SortExpression ="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Company">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>--%>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addAccountGroup(this,event);">
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
                <ClientEvents OnCommand="radgridaccountgroup_Command" OnRowSelected="radgridaccountgroup_RowSelected" OnRowDblClick="AccountGroup"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
