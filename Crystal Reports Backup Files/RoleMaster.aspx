<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="RoleMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.RoleMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
     <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
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
    
    <telerik:radcodeblock id = "RadCodeBlock11" runat = "server">
        <script type="text/javascript" src="../Shared/Javascripts/RoleMaster.js"></script>

        <script type="text/javascript">

            function pageLoad() {

                roleTableView = $find("<%=radgridrolemaster.ClientID %>").get_masterTableView();
                roleCommandName = "Load";
                CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskDetails(0, roleTableView.get_pageSize(), roleTableView.get_sortExpressions().toString(), roleTableView.get_filterExpressions().toDynamicLinq(), updaterolemasterGrid);
            }

            document.forms[0].onsubmit = function () {

                var hasDeletedItems = $find("<%=radgridrolemaster.ClientID %>").deletedItems.length > 0;
                if (hasDeletedItems) {

                    if (!confirm("Are You Sure To Delete?")) {

                        $find("<%=radgridrolemaster.ClientID%>")._deletedItems = [];
                        $find("<%=radgridrolemaster.ClientID%>").updateClientState();
                    }

                }

            }
            function deleteCurrent() {

                var table = $find("<%=radgridrolemaster.ClientID%>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex];
                table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id)

                if (dataItem) {

                    dataItem.dispose();
                    Array.remove($find("<%=radgridrolemaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }

                var gridItems = $find("<%=radgridrolemaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.RoleMasterWebService.DeleteRoleByRoleId(ROLE_ID);
                gridItems[gridItems.length - 1].set_selected(true);

            }
            function newrowadded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            for (var i = 0; i < 3; i++) {

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_TITLE') {

                    ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_REGARDING') {

                    ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                date = new Date().format('MM/dd/yyyy');
                ary[3] = date;
            }

            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;

            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.ROLE_ID;

            try {
                CRM.WebApp.webservice.RoleMasterWebService.insertupdateRole(ary);
                CRM.WebApp.webservice.RoleMasterWebService.GetRoleDetails(0, roleTableView.get_pageSize(), roleTableView.get_sortExpressions().toString(), roleTableView.get_filterExpressions().toDynamicLinq(), updaterolemasterGrid);
                var masterTable = $find("<%= radgridrolemaster.ClientID %>").get_masterTableView();
                //masterTable.rebind();
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }        
          }
             
        
        </script>

    </telerik:radcodeblock>

<script type="text/javascript" src="../Shared/Javascripts/RoleMaster.js"></script>

<div class="pageTitle" style="float: left">
        <asp:Literal ID="lblRole" runat="server" Text="Role Master"></asp:Literal>
</div>
<br />
<br />
<div id = "radmastergrid1">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Task?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table> 

         <telerik:radgrid id ="radgridrolemaster" runat = "server" allowpaging = "true" allowmultirowselection = "false"
            allowsorting = "True" pagesize = "10" itemstyle-wrap="false" enableembeddedskins="false"
            allowautomaticdeletes="True" allowautomaticinserts="True" width="800px">
        <MasterTableView ClientDataKeyNames="ROLE_ID" AllowMultiColumSorting="true" EditMode ="InPlace">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
        <columns>
            
             <telerik:GridTemplateColumn SortExpression ="ROLE_ID" DataField="ROLE_ID" HeaderText="ROLE_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="ROLE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
             </telerik:GridTemplateColumn>
             <telerik:GridTemplateColumn SortExpression ="ROLE_NAME" DataField="ROLE_NAME" HeaderText="ROLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="ROLE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn SortExpression ="CREATED_BY" DataField="CREATED_BY" HeaderText="CREATED BY">
                        <ItemTemplate>
                            <asp:TextBox ID="CREATED_BY" runat="server" CssClass="radinput" readonly="true"></asp:TextBox>
                        </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn SortExpression ="DATE_CREATED" DataField="DATE_CREATED" HeaderText="DATE CREATED">
                        <ItemTemplate>
                            <asp:TextBox ID="DATE_CREATED" runat="server" CssClass="radinput" readonly="true"></asp:TextBox>
                        </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn SortExpression ="MODIFIED_BY" DataField="MODIFIED_BY" HeaderText="MODIFIED BY">
                        <ItemTemplate>
                            <asp:TextBox ID="MODIFIED_BY" runat="server" CssClass="radinput" readOnly="true"></asp:TextBox>
                        </ItemTemplate>
            </telerik:GridTemplateColumn>
             <telerik:GridTemplateColumn SortExpression ="DATE_MODIFIED" DataField="DATE_MODIFIED" HeaderText="DATE MODIFIED">
                        <ItemTemplate>
                            <asp:TextBox ID="DATE_MODIFIED" runat="server" CssClass="radinput" readonly="true"></asp:TextBox>
                        </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "More" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowadded(this,event);">
                                    &raquo;
                                </a>
                            </ItemTemplate>
             </telerik:GridTemplateColumn>
        </columns>
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridrolemaster_Command" OnRowSelected="radgridrolemaster_RowSelected"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
 </div> 
</asp:Content>

