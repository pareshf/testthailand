<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AccessRights.aspx.cs" Inherits="CRM.WebApp.Views.Settings.AccessRights" %>

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

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/ProgramMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {
                masterTableView = $find("<%= radgridMaster.ClientID %>").get_masterTableView();
                programAccessTableView = $find("<%= radgridProgramAccess.ClientID %>").get_masterTableView();
                subProgramAccessTableView = $find("<%= radgridSubProgramAccess.ClientID %>").get_masterTableView();
                masterCommandName = "Load";
                programAccessCommandName = "Load";
                CRM.WebApp.webservice.ProgramMasterWebService.GetDeptRoleDetails(masterTableView.get_currentPageIndex() * masterTableView.get_pageSize(), masterTableView.get_pageSize(), masterTableView.get_sortExpressions().toString(), masterTableView.get_filterExpressions().toDynamicLinq(), updateMasterGrid);
                //CRM.WebApp.webservice.ProgramMasterWebService.GetProgDetails(programAccessTableView.get_currentPageIndex() * programAccessTableView.get_pageSize(), programAccessTableView.get_pageSize(), programAccessTableView.get_sortExpressions().toString(), programAccessTableView.get_filterExpressions().toDynamicLinq(), updateProgAccessGrid);
                
            }

            function SaveProgramRecords(sender, args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                
               /* for (var i = 0; i < 8; i++) {
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_PROG_NAME') {
                        ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_READ_ACCESS') {
                        ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_WRITE_ACCESS') {
                        ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_DELETE_ACCESS') {
                        ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_PRINT_ACCESS') {
                        ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_CUST_DISCOUNT_L') {
                        ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_CUST_DISCOUNT_H') {
                        ary[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl04_CUST_TYPE') {
                        ary[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                } */
                ary[0] = DEPT_ID;
                ary[1] = ROLE_ID;
                ary[2] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("PROG_NAME").value;
                ary[3] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("READ_ACCESS").value;
                ary[4] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("WRITE_ACCESS").value;
                ary[5] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("DELETE_ACCESS").value;
                ary[6] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("PRINT_ACCESS").value;
                ary[7] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_DISCOUNT").value;
                ary[8] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("TICKETING_DISCOUNT").value;
                ary[9] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_TYPE").value;
                ary[10] = COMP_ID;
                ary[11] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("CHANGE_CUST_OWNER").value;
                ary[12] = programAccessTableView.get_dataItems()[currentRowIndex - 1].findElement("VISA_DISCOUNT").value;

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = "F";
                if (ary[4] == "" || ary[4] == 'null') ary[4] = "F";
                if (ary[5] == "" || ary[5] == 'null') ary[5] = "F";
                if (ary[6] == "" || ary[6] == 'null') ary[6] = "F";
                if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
                if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
                if (ary[9] == "" || ary[9] == 'null') ary[9] = "F";
                if (ary[11] == "" || ary[11] == 'null') ary[11] = "F";
                if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
                try {
                    CRM.WebApp.webservice.ProgramMasterWebService.InsertUpdateProgramAccess(ary);
                    CRM.WebApp.webservice.ProgramMasterWebService.GetProgAccessDetails(DEPT_ID, ROLE_ID, COMP_ID, updateProgAccessGrid);
                    //var masterTable = $find("<%= radgridProgramAccess.ClientID %>").get_masterTableView();
                    //masterTable.rebind();
                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }

            function SaveSubProgramRecords(sender, args) {
                var ary = [];
                ary[0] = DEPT_ID;
                ary[1] = ROLE_ID;
                for (var i = 0; i < 5; i++) {
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl04_PROGRAM_SUB_NAME') {
                        ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl04_READ_ACCESS') {
                        ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl04_WRITE_ACCESS') {
                        ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl04_DELETE_ACCESS') {
                        ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl04_PRINT_ACCESS') {
                        ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                }
                ary[7] = COMP_ID;
                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = "F";
                if (ary[4] == "" || ary[4] == 'null') ary[4] = "F";
                if (ary[5] == "" || ary[5] == 'null') ary[5] = "F";
                if (ary[6] == "" || ary[6] == 'null') ary[6] = "F";
                if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
                try {
                    CRM.WebApp.webservice.ProgramMasterWebService.InsertUpdateSubProgramAccess(ary);
                    CRM.WebApp.webservice.ProgramMasterWebService.GetSubProgAccessDetails(DEPT_ID, ROLE_ID, COMP_ID, updateSubProgAccessGrid);
                    //var masterTable = $find("<%= radgridSubProgramAccess.ClientID %>").get_masterTableView();
                    //masterTable.rebind();
                    alert('Record Save Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }

            function InsertNewProgramAccess() {
                CRM.WebApp.webservice.ProgramMasterWebService.InsertNewProgramAccess(DEPT_ID, ROLE_ID);
                var masterTable = $find("<%= radgridProgramAccess.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function InsertNewSubProgramAccess() {
                CRM.WebApp.webservice.ProgramMasterWebService.InsertNewSubProgramAccess(DEPT_ID, ROLE_ID);
                var masterTable = $find("<%= radgridSubProgramAccess.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function AddNewDeptAndRole() {
                var comp = document.getElementById("ctl00_cphPageContent_txtComp").value;
                var dept = document.getElementById("ctl00_cphPageContent_txtDept").value;
                var role = document.getElementById("ctl00_cphPageContent_txtRole").value;
                var temp = CRM.WebApp.webservice.ProgramMasterWebService.InsertNewDeptRole(dept, role, comp);
                CRM.WebApp.webservice.ProgramMasterWebService.GetDeptRoleDetails(masterTableView.get_currentPageIndex() * masterTableView.get_pageSize(), masterTableView.get_pageSize(), masterTableView.get_sortExpressions().toString(), masterTableView.get_filterExpressions().toDynamicLinq(), updateMasterGrid);
                if (temp == '1') {
                    alert('Record Save Successfully');
                }
                else if (temp == '2') {
                    alert('Record Already Exist');
                }
            }
        </script>
    </telerik:radcodeblock>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var compname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            var deptname = "../../webservice/autocomplete.ashx?key=FETCH_DEPT_NAME_AUTOSEARCH";
            var rolename = "../../webservice/autocomplete.ashx?key=FETCH_ROLE_NAME_AUTOSEARCH";
            var progname = "../../webservice/autocomplete.ashx?key=FETCH_PROGRAMTEXT_FOR_PROGRAM_MASTER_AUTOSEARCH";
            var subprogname = "../../webservice/autocomplete.ashx?key=FETCH_SUBPROGRAM_NAME_FOR_SUBPROGRAM_MASTER";

            $("#ctl00_cphPageContent_txtComp").autocomplete(compname);
            $("#ctl00_cphPageContent_txtDept").autocomplete(deptname);
            $("#ctl00_cphPageContent_txtRole").autocomplete(rolename);

            for (var i = 1; i < 55; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridProgramAccess_ctl00_ctl" + i + "_PROG_NAME").autocomplete(progname);
                $("#ctl00_cphPageContent_radgridSubProgramAccess_ctl00_ctl" + i + "_PROGRAM_SUB_NAME").autocomplete(subprogname);
            }
        });
    </script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Access Rights"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr valign="top">
            <td valign="top">
                <div>
                    <telerik:radgrid id="radgridMaster" runat="server" pagesize="10"  allowpaging="true" allowmultirowselection="false"
                        itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
                        headerstyle-wrap="false">
            <MasterTableView ClientDataKeyNames="" EditMode="InPlace" Width="300" AllowMultiColumnSorting="true">
                <Columns>
                    <telerik:GridTemplateColumn SortExpression="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Company">
                        <ItemTemplate>
                             <asp:TextBox ID="COMPANY_NAME"  runat="server" CssClass="radinput"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn SortExpression="DEPT_NAME" DataField="DEPT_NAME" HeaderText="Department">
                        <ItemTemplate>
                             <asp:TextBox  ID="DEPT_NAME" runat="server" CssClass="radinput"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn SortExpression="ROLE" DataField="ROLE" HeaderText="Role">
                        <ItemTemplate>
                             <asp:TextBox  ID="ROLE" runat="server" CssClass="radinput"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn SortExpression="COMPANY_ID" DataField="COMPANY_ID" HeaderText="Company Id" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox  ID="COMPANY_ID" runat="server" CssClass="radinput" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn SortExpression="ROLE_ID" DataField="ROLE_ID" HeaderText="Role Id" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox  ID="ROLE_ID" runat="server" CssClass="radinput" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridTemplateColumn SortExpression="DEPT_ID" DataField="DEPT_ID" HeaderText="Dept Id" Visible="false">
                        <ItemTemplate>
                             <asp:TextBox  ID="DEPT_ID" runat="server" CssClass="radinput" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                </Columns>
            </MasterTableView>    
            <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridMaster_Command" OnRowSelected="radgridMaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                </div>
            </td>
            <td valign="top">
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblComp" runat="server" Text="Company"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtComp" runat="server" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDept" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDept" runat="server" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblRole" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRole" runat="server" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" 
                                    OnClientClick="AddNewDeptAndRole();"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal2" runat="server" Text="Program Rights"></asp:Literal>
        </div>
        <br />
        <br />
        <telerik:radgrid id="radgridProgramAccess" runat="server" pagesize="150" allowpaging="false"
            itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
            headerstyle-wrap="false">
            <MasterTableView ClientDataKeyNames="" EditMode="InPlace" width="900px">
                <Columns>
                    <telerik:GridTemplateColumn SortExpression="DEPT_ID" DataField="DEPT_ID" Visible="False">
                        <ItemTemplate>
                             <asp:TextBox  ID="DEPT_ID" runat="server" CssClass="radinput"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ROLE_ID" DataField="ROLE_ID" Visible="False">
                        <ItemTemplate>
                             <asp:TextBox  ID="ROLE_ID" runat="server" CssClass="radinput"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="PROG_NAME" DataField="PROG_NAME" HeaderText="Program Name">
                        <ItemTemplate>
                             <asp:TextBox  ID="PROG_NAME" runat="server" CssClass="radinput" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="READ_ACCESS" HeaderText="Read">
                        <ItemTemplate>
                            <asp:TextBox id="READ_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="WRITE_ACCESS" HeaderText="Write">
                        <ItemTemplate>
                            <asp:TextBox id="WRITE_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DELETE_ACCESS" HeaderText="Delete">
                        <ItemTemplate>
                            <asp:TextBox id="DELETE_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PRINT_ACCESS" HeaderText="Print">
                        <ItemTemplate>
                            <asp:TextBox id="PRINT_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TOUR_DISCOUNT" HeaderText="Tour Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="TOUR_DISCOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TICKETING_DISCOUNT" HeaderText="Ticketing Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="TICKETING_DISCOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="VISA_DISCOUNT" HeaderText="Visa Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="VISA_DISCOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CUST_TYPE" HeaderText="Customer Type Gold">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_TYPE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CHANGE_CUST_OWNER" HeaderText="Change Customer Owner">
                        <ItemTemplate>
                            <asp:TextBox ID="CHANGE_CUST_OWNER" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="SaveProgramRecords(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridProgramAccess_Command"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
    </telerik:radgrid>
        <%--<asp:LinkButton ID="lbAddProgram" runat="server" Text="Add Program Rights" OnClientClick="InsertNewProgramAccess();"></asp:LinkButton>--%>
    </div>
    <br />
    <div>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal3" runat="server" Text="Sub Program Rights"></asp:Literal>
        </div>
        <br />
        <br />
        <telerik:radgrid id="radgridSubProgramAccess" runat="server" pagesize="150" allowpaging="false"
            itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True"
            headerstyle-wrap="false">
            <MasterTableView ClientDataKeyNames="" EditMode="InPlace" width="500px">
                <Columns>
                    <telerik:GridTemplateColumn SortExpression="PROGRAM_SUB_NAME" DataField="PROGRAM_SUB_NAME" HeaderText="Sub Program Name">
                        <ItemTemplate>
                             <asp:TextBox  ID="PROGRAM_SUB_NAME" runat="server" CssClass="radinput" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox >
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="READ_ACCESS" HeaderText="Read">
                        <ItemTemplate>
                            <asp:TextBox id="READ_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="WRITE_ACCESS" HeaderText="Write">
                        <ItemTemplate>
                            <asp:TextBox id="WRITE_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DELETE_ACCESS" HeaderText="Delete">
                        <ItemTemplate>
                            <asp:TextBox id="DELETE_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="PRINT_ACCESS" HeaderText="Print">
                        <ItemTemplate>
                            <asp:TextBox id="PRINT_ACCESS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="SaveSubProgramRecords(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings AllowColumnsReorder="true" ReorderColumnsOnClient="true" AllowDragToGroup="true">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridSubProgramAccess_Command"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
    </telerik:radgrid>
        <%--<asp:LinkButton ID="lbAddSubProg" runat="server" Text="Add Sub Program Rights" OnClientClick="InsertNewSubProgramAccess();"></asp:LinkButton>--%>
    </div>
</asp:Content>
