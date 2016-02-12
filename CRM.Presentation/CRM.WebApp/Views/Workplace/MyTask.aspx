<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="MyTask.aspx.cs" Inherits="CRM.WebApp.Views.Workplace.MyTask" %>

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
 <telerik:radcodeblock id = "RadCodeBlock1" runat = "server">
    
    
    
    <script type ="text/javascript" src="../Shared/Javascripts/TaskMasterGrid.js"></script>
    
 
    <script type ="text/javascript">


        function pageLoad() {
     
            taskTabelView = $find("<%= radgritaskdmaster.ClientID %>").get_masterTableView();
            taskCommandName = "Load";
            CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskDetails(taskTabelView.get_currentPageIndex() * taskTabelView.get_pageSize(), taskTabelView.get_pageSize(), taskTabelView.get_sortExpressions().toString(), taskTabelView.get_filterExpressions().toDynamicLinq(), updatetaskGrid);
           

        }
      
        document.forms[0].onsubmit = function () {

            var hasDeletedItems = $find("<%=radgritaskdmaster.ClientID %>").deletedItems.length > 0;
            if (hasDeletedItems) {

                if (!confirm("Are You Sure To Delete?")) {

                    $find("<%=radgritaskdmaster.ClientID%>")._deletedItems = [];
                    $find("<%=radgritaskdmaster.ClientID%>").updateClientState();
                }

            }

        }

         var currentTextBox = null;
         var currentDatePicker = null;

         function showPopup(sender, e) {

             try {

                 currentTextBox = sender;
                 var datePicker = $find("<%= RadDatePicker1.ClientID %>");
                 currentDatePicker = datePicker;
                 datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));
                 var position = datePicker.getElementPosition(sender);
                 datePicker.showPopup(position.x, position.y + sender.offsetHeight);
                    
             }
             catch (e) { }

         }

         function dateSelected(sender, args) { 
         
            try{

                if (currentTextBox != null) {

                    currentTextBox.value = args.get_newDate().format('MM/dd/yyyy');
                    currentDatePicker.hidePopup();
                
                }

            }
            catch (e) {}

        }

        function parseDate(sender, e) {

            currentDatePicker.hidePopup();
        }

        function deleteCurrent() {
           
            var table = $find("<%=radgritaskdmaster.ClientID%>").get_masterTableView().get_element();
            var row = table.rows[currentRowIndex];
            table.deleteRow(currentRowIndex);
            var dataItem = $find(row.id)

            if (dataItem) {

                dataItem.dispose();
                Array.remove($find("<%= radgritaskdmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
            }

            var gridItems = $find("<%= radgritaskdmaster.ClientID %>").get_masterTableView().get_dataItems();
            CRM.WebApp.webservice.MyTaskMasterWebService.DeleteTaskByMYTASKID(MYTASK_ID);
            gridItems[gridItems.length - 1].set_selected(true);
        
        }

        function rowadded(sender,args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            for (var i = 0; i < 14; i++) {
 
                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_TITLE') {

                    ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_REGARDING') {

                    ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_TASK_TYPE_NAME') {

                    ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_ASSIGN_TO') {

                    ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_DURATION') {

                    ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_ACTUAL_TIME_TAKEN') {

                    ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_START_DATE') {

                    ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_END_DATE') {

                    ary[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_PRIORITY_NAME') {

                    ary[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_PRODUCT_DESC') {

                    ary[10] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_PRODUCT_CODE') {

                    ary[11] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_STATUS_NAME') {

                    ary[12] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl04_PROJECT_REMARKS') {

                    ary[13] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].value
                }

                
            }


            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
            if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
            if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
            if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
            if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
            if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
            if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
            if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
            if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
            if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
            if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
            if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
            if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;

            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.MYTASK_ID;

            try {
                CRM.WebApp.webservice.MyTaskMasterWebService.InsertUpdateMytask(ary);
                CRM.WebApp.webservice.MyTaskMasterWebService.GetTaskDetails(0, taskTabelView.get_pageSize(), taskTabelView.get_sortExpressions().toString(), taskTabelView.get_filterExpressions().toDynamicLinq(), updatetaskGrid);
                var masterTable = $find("<%= radgritaskdmaster.ClientID %>").get_masterTableView();
                //masterTable.rebind();
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }        

        }

    
   
        function PopUpShowing(sender, args) {

            var divmore = document.getElementById('divmore');
            divmore.style.display = 'block';
            divmore.style.position = 'Absolute';
            divmore.style.left = screen.width / 2 - 150;
            divmore.style.top = screen.height / 2 - 150;
            var IMG = document.getElementById("imgexistingimage");
            IMG.src = args.srcElement.all[1].value;
        }

        function disablepopup() {

            var divmore = document.getElementById('divmore');
            divmore.style.display = 'none';
            return false;
        }
    
    </script>
 
 </telerik:radcodeblock>
 <%--<a href="#" onclick="pageLoad();">Click Me!</a>--%>

    
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageTask" runat="server" Text="My Task"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">

         $(document).ready(function () {
             //alert("in ready function");
             var empname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
             var tasktype = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TASK_NAME_AUTOSEARCH";
             var taskstatus = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_TASK_STATUS_AUTOSEARCH";
             var taskpriority = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_PRIORITY_AUTOSEARCH";
             var taskproduct = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_PRODUCT_AUTOSEARCH";
             for (var i = 1; i < 55; i++) {
                 if (i < 10)
                     i = '0' + i;
                 $("#ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl" + i + "_ASSIGN_TO").autocomplete(empname);
                 $("#ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl" + i + "_TASK_TYPE_NAME").autocomplete(tasktype);
                 $("#ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(taskstatus);
                 $("#ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl" + i + "_PRIORITY_NAME").autocomplete(taskpriority);
                 $("#ctl00_cphPageContent_radgritaskdmaster_ctl00_ctl" + i + "_PRODUCT_DESC").autocomplete(taskproduct);

             }

         });
     </script>
 
 <%--"<% MY TASK MASTER %>"--%>
    

        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900" maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
       
  <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Task?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table> 
    
        <telerik:radgrid id ="radgritaskdmaster" runat = "server" allowpaging = "true" allowmultirowselection = "false"
            allowsorting = "True" pagesize = "10" itemstyle-wrap="false" enableembeddedskins="false" Width="2500"
            allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="MYTASK_ID" AllowMultiColumSorting="true" EditMode ="InPlace">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               
               <Columns>
                
                    <%-- template column for raw data editing result --%>
                    <telerik:GridTemplateColumn SortExpression ="MYTASK_ID" DataField="MYTASK_ID" HeaderText="MYTASK_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="MYTASK_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                       
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TITLE" DataField="TITLE" HeaderText="Task Title">
                        <ItemTemplate>
                            <asp:TextBox ID="TITLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="REGARDING" DataField="REGARDING" HeaderText="Task Regarding">
                        <ItemTemplate>
                            <asp:TextBox ID="REGARDING" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
               
                    <telerik:GridTemplateColumn SortExpression ="TASK_TYPE_NAME" DataField="TASK_TYPE_NAME" HeaderText="Task Type">
                        <ItemTemplate>
                            <asp:TextBox ID="TASK_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ASSIGN_TO" DataField="ASSIGN_TO" HeaderText="Task AssignTo">
                        <ItemTemplate>
                            <asp:TextBox ID="ASSIGN_TO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DURATION" DataField="DURATION" HeaderText="Task Duration">
                        <ItemTemplate>
                            <asp:TextBox ID="DURATION" runat="server" CssClass="radinput" readonly="true" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACTUAL_TIME_TAKEN" DataField="ACTUAL_TIME_TAKEN" HeaderText="ActualTime Taken">
                        <ItemTemplate>
                            <asp:TextBox ID="ACTUAL_TIME_TAKEN" runat="server" CssClass="radinput" readonly="true" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACTUAL_COMPLETION_DATE" DataField="ACTUAL_COMPLETION_DATE" HeaderText="Actual Completion Date">
                        <ItemTemplate>
                            <asp:TextBox ID="ACTUAL_COMPLETION_DATE" runat="server" CssClass="radinput" readonly="true" style="background-color:LightBlue"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="START_DATE" DataField="START_DATE" HeaderText="Task StartDate" >
                        <ItemTemplate>
                            <asp:TextBox ID="START_DATE" runat="server" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);" CssClass="radinput"  >
                            </asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="END_DATE" DataField="END_DATE" HeaderText="Task EndDate" >
                        <ItemTemplate>
                            <asp:TextBox ID="END_DATE" runat="server" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);" CssClass="radinput" >
                            </asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PRIORITY_NAME" DataField="PRIORITY_NAME" HeaderText="Priority Name">
                        <ItemTemplate>
                            <asp:TextBox ID="PRIORITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATUS_NAME" DataField="STATUS_NAME" HeaderText="Task Status">
                        <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>

                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PROJECT_REMARKS" DataField="PROJECT_REMARKS" HeaderText="Progress Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="PROJECT_REMARKS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="PRODUCT_DESC" DataField="PRODUCT_DESC" HeaderText="Product Description">
                        <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PRODUCT_CODE" DataField="PRODUCT_CODE" HeaderText="Product Code">
                        <ItemTemplate>
                            <asp:TextBox ID="PRODUCT_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "More" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="rowadded(this,event);">
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
                <ClientEvents OnCommand="radgritaskdmaster_Command" OnRowSelected="radgritaskdmaster_RowSelected"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
 </div> 
</asp:Content>
