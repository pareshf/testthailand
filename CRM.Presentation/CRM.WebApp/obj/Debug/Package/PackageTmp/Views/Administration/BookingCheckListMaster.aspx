<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="BookingCheckListMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.BookingCheckListMaster" %>

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
            <script type="text/javascript" src="../Shared/Javascripts/BookingChecklistGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                BookingCheckTableView = $find("<%= radgridbookingChecklist.ClientID %>").get_masterTableView();
                CheckListDetailsTableView = $find("<%= radgridChecklistdetails.ClientID %>").get_masterTableView();
                BookingCheckListCommand = "Load";
                CRM.WebApp.webservice.BookingCheckListWebService.GetCheckList(BookingCheckTableView.get_currentPageIndex() * BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_sortExpressions().toString(), BookingCheckTableView.get_filterExpressions().toDynamicLinq(), updateCheckListGrid);


            }
            function deleteCurrent() {
                var table = $find("<%= radgridbookingChecklist.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridbookingChecklist.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridbookingChecklist.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.BookingCheckListWebService.deleteMyCheckList(CHECKLIST_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteChkListDetail() {

                CRM.WebApp.webservice.BookingCheckListWebService.deleteChkdetails(SR_NO);
                CRM.WebApp.webservice.BookingCheckListWebService.GetCheckListDetails(CHECKLIST_ID, updateCheckListDetail)
            }
            function addnewchecklist(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = BookingCheckTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECKLIST_FOR").value;
                ary[2] = BookingCheckTableView.get_dataItems()[currentRowIndex - 1].findElement("DEPARTMENT_NAME").value;
                ary[3] = BookingCheckTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CHECKLIST_ID;
                for (i = 0; i < 5; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.BookingCheckListWebService.InsertUpdateforCheckList(ary);
                    alert('Record Save Successfully');

                    CRM.WebApp.webservice.BookingCheckListWebService.GetCheckList(BookingCheckTableView.get_currentPageIndex() * BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_sortExpressions().toString(), BookingCheckTableView.get_filterExpressions().toDynamicLinq(), updateCheckListGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewchecklistdetails(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = CheckListDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECKLIST_DESCRIPTION").value;
                ary[2] = CheckListDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("PRIORITY_NAME").value;
                ary[3] = CheckListDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("IMPORTANCE").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                for (i = 0; i < 5; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.BookingCheckListWebService.InsertUpdateCheckListDetails(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.BookingCheckListWebService.GetCheckList(BookingCheckTableView.get_currentPageIndex() * BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_pageSize(), BookingCheckTableView.get_sortExpressions().toString(), BookingCheckTableView.get_filterExpressions().toDynamicLinq(), updateCheckListGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function AddNewCheckList(sender, args) {

                CRM.WebApp.webservice.BookingCheckListWebService.InsertNewCheckList(CHECKLIST_ID);
            }
        </script>
    </telerik:radcodeblock>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Booking CheckList Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var departmentname = "../../webservice/autocomplete.ashx?key=FETCH_DEPARTMENT_FOR_EMPLOYEE_MASTER";
            var taskpriority = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_PRIORITY_AUTOSEARCH";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridbookingChecklist_ctl00_ctl" + i + "_DEPARTMENT_NAME").autocomplete(departmentname);
                $("#ctl00_cphPageContent_radgridChecklistdetails_ctl00_ctl" + i + "_PRIORITY_NAME").autocomplete(taskpriority);
            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this CheckList?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridbookingChecklist" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CHECKLIST_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CHECKLIST_ID" DataField="CHECKLIST_ID" HeaderText="CHECKLIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECKLIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CHECKLIST_FOR" DataField="CHECKLIST_FOR" HeaderText="CHECKLIST FOR">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECKLIST_FOR" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DEPARTMENT_NAME" DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="DEPARTMENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewchecklist(this,event);">
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
                <ClientEvents OnCommand="radgridbookingChecklist_Command" OnRowSelected="radgridbookingChecklist_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <br />
        <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblchketail" runat="server" Text="Booking Check List Detail"></asp:Literal>
    </div>
    <br />
    <br />
        <table>
            <tr>
                <td>
                        <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this CheckList Details?'))return false; deleteChkListDetail(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridChecklistdetails" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SR_NO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="650px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CHECKLIST_DESCRIPTION" DataField="CHECKLIST_DESCRIPTION" HeaderText="CHECKLIST DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECKLIST_DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PRIORITY_NAME" DataField="PRIORITY_NAME" HeaderText="PRIORITY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="PRIORITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IMPORTANCE" DataField="IMPORTANCE" HeaderText="IMPORTANCE">
                          <ItemTemplate>
                            <asp:TextBox ID="IMPORTANCE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewchecklistdetails(this,event);">
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
                <ClientEvents OnCommand="radgridChecklistdetails_Command" OnRowSelected="radgridChecklistdetails_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
               <asp:LinkButton ID="lbAddchkdetail" runat="server" Text="Add Another Check List Detail" OnClientClick="AddNewCheckList();"></asp:LinkButton>
                </td>
            </tr>
        </table>
</asp:Content>
