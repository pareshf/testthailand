<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="ProgramMaster.aspx.cs" Inherits="CRM.WebApp.Views.Settings.ProgramMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
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

                    programTableView = $find("<%= radgridprogrammaster.ClientID %>").get_masterTableView();
                    subprogramTableView = $find("<%=radgridsubprogrammaster.ClientID %>").get_masterTableView();
                    progarmCommandName = "Load";
                    
                    if (programTableView.PageSize = 10) {
                        CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(0, programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);
                    }
                    else if (programTableView.PageSize > 10) {
                        CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(0, programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);
                    }
                    else if (programTableView.PageSize > 20) {
                        CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(0, programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);
                    }
                    
                }
                
                function deleteCurrent() {

                    
                    CRM.WebApp.webservice.ProgramMasterWebService.DeleteMyProgram(PROGRAM_ID);
                    CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(0, programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);

                }

                function deleteCurrentSubProg() {
                    
                    CRM.WebApp.webservice.ProgramMasterWebService.DeleteSubProgram(SUB_PROG_ID);
                    CRM.WebApp.webservice.ProgramMasterWebService.GetSubProgramDetails(PROGRAM_ID, updatesubprogramGrid);
                    
                }

                function newrowadded(sender, args) {

                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var ary = [];
                    for (var i = 0; i < 7; i++) {


                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_MODULE_NAME') {

                            ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_PROGRAM_TYPE_NAME') {

                            ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_PROGRAM_NAME') {

                            ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_PROGRAM_TEXT') {

                            ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_PROGRAM_ACCESS_KEY') {

                            ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }
                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl04_PROGRAM_SORT_ORDER') {

                            ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }
                    }


                    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                    if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                    if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                    if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;

                    ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PROGRAM_ID;

                    try {
                        CRM.WebApp.webservice.ProgramMasterWebService.InsertUpdateMyProgram(ary);
                        CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(0, programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);
                        //var masterTable = $find("<%= radgridprogrammaster.ClientID %>").get_masterTableView();
                        //masterTable.rebind();
                        alert('Record Save Successfully');

                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }

                }
                function newsubrowadded(sender, args) {

                    currentRowIndex = sender.parentNode.parentNode.rowIndex;
                    var ary = [];

                    for (var i = 0; i < 4; i++) {
                        //debugger;

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl04_PROGRAM_SUB_NAME') {

                            ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl04_PARENT_NAME') {

                            ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }

                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl04_NEWPROGRAM_TEXT') {

                            ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                        }
                    }


                    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                    if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;


                    ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PROGRAM_SUB_ID;

                    try {
                        CRM.WebApp.webservice.ProgramMasterWebService.InsertUpdateSubProgram(ary);
                        var masterTable = $find("<%= radgridsubprogrammaster.ClientID %>").get_masterTableView();
                        //masterTable.rebind();
                        alert('Record Save Successfully');

                    }
                    catch (e) {
                        alert('Wrong Data Inserted');
                    }

                }

                </script>
        
        </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageProgram" runat="server" Text="Program"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            //debugger;
            var modulename = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_PROGRAM_MASTER_AUTOSEARCH";
            var programtext = "../../webservice/autocomplete.ashx?key=FETCH_PROGRAMTEXT_FOR_PROGRAM_MASTER_AUTOSEARCH";
            var programtype = "../../webservice/autocomplete.ashx?key=FETCH_PROGRAMTYPE_FOR_PROGRAM_MASTER_AUTOSEARCH";
            var subprogram = "../../webservice/autocomplete.ashx?key=FETCH_SUBPROGRAM_NAME_FOR_SUBPROGRAM_MASTER";
            var parentname = "../../webservice/autocomplete.ashx?key=FETCH_SUBPROGRAM_NAME_FOR_SUBPROGRAM_MASTER";
            var subprogramtext = "../../webservice/autocomplete.ashx?key=FETCH_PROGRAMTEXT_FOR_PROGRAM_MASTER_AUTOSEARCH";
            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl" + i + "_MODULE_NAME").autocomplete(modulename);
                $("#ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl" + i + "_PROGRAM_TYPE_NAME").autocomplete(programtype);
                //$("#ctl00_cphPageContent_radgridprogrammaster_ctl00_ctl" + i + "_PROGRAM_TEXT").autocomplete(programtype);
                // $("#ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl" + i + "_PROGRAM_SUB_NAME").autocomplete(subprogram);
                $("#ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl" + i + "_PARENT_NAME").autocomplete(parentname);
                $("#ctl00_cphPageContent_radgridsubprogrammaster_ctl00_ctl" + i + "_NEWPROGRAM_TEXT").autocomplete(subprogramtext);
            }


        });
       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Program?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <telerik:radgrid id="radgridprogrammaster" runat="server" pagesize="50" allowpaging="true"
            itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinsert="true"
            headerstyle-wrap="false" width="700px" allowmultirowselection="false">
        <MasterTableView ClientDataKeyNames="PROGRAM_ID" AllowMultiColumSorting="true" EditMode ="InPlace" >
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               <Columns>
                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_ID" DataField="PROGRAM_ID" HeaderText="PROGRAM_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MODULE_NAME" DataField="MODULE_NAME" HeaderText="MODULE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="MODULE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_TYPE_NAME" DataField="PROGRAM_TYPE_NAME" HeaderText="PROGRAM TYPE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_NAME" DataField="PROGRAM_NAME" HeaderText="PROGRAM PATH">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_TEXT" DataField="PROGRAM_TEXT" HeaderText="PROGRAM NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_TEXT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_ACCESS_KEY" DataField="PROGRAM_ACCESS_KEY" HeaderText="ACCESS KEY">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_ACCESS_KEY" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_SORT_ORDER" DataField="PROGRAM_SORT_ORDER" HeaderText="SORT ORDER">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_SORT_ORDER" runat="server" CssClass="radinput"></asp:TextBox>
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
               </Columns>
               </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridprogrammaster_Command" OnRowSelected="radgridprogrammaster_RowSelected" />
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
    </div>
    <div class="pageTitle" style="float: left">
        <br />
        <asp:Literal ID="Literal1" runat="server" Text="SubProgram"></asp:Literal>
        <br />
    </div>
    <br />
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="DeleteSubProg" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Sub Program?'))return false;deleteCurrentSubProg(); return false;"
                        Text="Delete Sub Program" runat="server" />
                </td>
            </tr>
        </table>
        <telerik:radgrid id="radgridsubprogrammaster" runat="server" pagesize="150" allowpaging="false"
            itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinsert="true"
            headerstyle-wrap="false" width="700px" allowmultirowselection="false">
        <MasterTableView ClientDataKeyNames="PROGRAM_SUB_ID" AllowMultiColumSorting="true" EditMode ="InPlace" >
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               <Columns>
                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_SUB_ID" DataField="PROGRAM_SUB_ID" HeaderText="PROGRAM_SUB_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_SUB_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROGRAM_SUB_NAME" DataField="PROGRAM_SUB_NAME" HeaderText="SUB PROGRAM NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="PROGRAM_SUB_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PARENT_NAME" DataField="PARENT_NAME" HeaderText="PARENT NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="PARENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="NEWPROGRAM_TEXT" DataField="NEWPROGRAM_TEXT" HeaderText="MAIN PROGRAM">
                        <ItemTemplate>
                            <asp:TextBox ID="NEWPROGRAM_TEXT" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newsubrowadded(this,event);">
                                    &raquo;
                                </a>
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
               </Columns>
               </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridsubprogrammaster_Command" OnRowSelected="radgridsubprogrammaster_RowSelected"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
    </div>
</asp:Content>
