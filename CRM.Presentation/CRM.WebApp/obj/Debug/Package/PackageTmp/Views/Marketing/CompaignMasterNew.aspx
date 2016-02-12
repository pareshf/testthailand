<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CompaignMasterNew.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.CompaignMasterNew" %>

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
    
        <script type="text/javascript" src="../Shared/Javascripts/CampaignMaster.js"></script>
        <script type="text/javascript">
            
            function pageLoad() {

                CampaignTableView = $find("<%= radgridCompaignmaster.ClientID %>").get_masterTableView();
                CampaignDetailsTableView = $find("<%= radgridcampaigndetails.ClientID %>").get_masterTableView();
                mappingTableView = $find("<%= radgridmapping.ClientID %>").get_masterTableView();
                
                CampaignDetailsCommand = "Load";
                CampaignCommand = "Load";
                mappingCommand = "Load";
                if (CampaignTableView.PageSize = 10) {
                    CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);
                    
                }
                else if (CampaignTableView.PageSize > 10) {
                    CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);
                    
                }
                else if (CampaignTableView.PageSize > 20) {
                    CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);

                }
                /* */
                
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

                try {

                    if (currentTextBox != null) {

                        currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                        currentDatePicker.hidePopup();

                    }

                }
                catch (e) { }

            }

            function parseDate(sender, e) {

                currentDatePicker.hidePopup();
            }
            function delCampaign() {
                var table = $find("<%= radgridCompaignmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridCompaignmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridCompaignmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.CampaignMasterWebService.delCampaign(CAMPAIGN_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewcompaign(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[0] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAIGN_NAME").value;
                ary[2] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAIGN_CODE").value;
                ary[3] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("START_DATE").value;
                ary[4] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("END_DATE").value;
                ary[5] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[6] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;
                ary[7] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("TYPE").value;
                ary[8] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("BUDGET").value;
                ary[9] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("ACTUAL_COST").value;
                ary[10] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("EXPECTED_COST").value;
                ary[11] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("MISC_COST").value;
                ary[12] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("EXPECTED_REVENU").value;
               // ary[13] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("OFFER").value;
                ary[13] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("DESCRIPTION").value;
               // ary[15] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("EMPLOYEE").value;
               // ary[16] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("TRACKER_NAME").value;
               // ary[17] = CampaignTableView.get_dataItems()[currentRowIndex - 1].findElement("TRACKER_LINK").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CAMPAIGN_ID;
                for (i = 0; i < 14; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.CampaignMasterWebService.InsertUpdateCampaign(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewcam_mar(sender,args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = mappingTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAIGN_NAME").value;
                ary[1] = mappingTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_SHORT_NAME").value;
                ary[2] = mappingTableView.get_dataItems()[currentRowIndex - 1].findElement("TOUR_CODE").value;
                ary[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CAMP_MAR_ID
                for (i = 0; i < 4; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.CampaignMasterWebService.InsertUpdateforMapping(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CampaignMasterWebService.GetMapping(CAMPAIGN_ID, updatemappingGrid);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function addnewCampaigndetails(sender, args) {
                
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = CampaignDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("TARGETLIST_NAME").value;
               // ary[2] = CampaignDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAIGN_NAME").value;
                //ary[3] = CampaignDetailsTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_TYPE_NAME").value;
                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.TARGET_DETAILS_ID;
                for (i = 0; i < 2; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.CampaignMasterWebService.InsertUpdateCampaignTargetListDetail(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CampaignMasterWebService.GetTargetList(CAMPAIGN_ID, updatecampaigndetail);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function Redirect() {

                window.location = "MarketingCustomerNew.aspx?TARGETLIST_ID=" + TARGETLIST_ID;
            }
            function addnewMapping() {

                CRM.WebApp.webservice.CampaignMasterWebService.InsertNewRow(CAMPAIGN_ID);
            }
            function addnewTargetlist() {

                CRM.WebApp.webservice.CampaignMasterWebService.InsertNewTargetList(CAMPAIGN_ID);
            }
            function getTourName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getTourId(sender) {
                var tourcode = sender.value;
                $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
            }
        </script>
   </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblpagename" runat="server" Text="Campaign Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
       
        $(document).ready(function () {
            
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TOUR";
            var compaignstatus = "../../webservice/autocomplete.ashx?key=GET_COMPAIGN_STATUS_FOR_COMPAIGN_MASTER";
            //var employee = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var compaigntype = "../../webservice/autocomplete.ashx?key=FETCH_COMPAIGN_TYPE_FOR_AUTOSEARCH";
            var compaignname = "../../webservice/autocomplete.ashx?key=GET_CAMPAIGN_NAME_AUTOSEARCH";
            var targetlistname = "../../webservice/autocomplete.ashx?key=GET_TARGET_LIST_NAME_FOR_AUTOSEARCH";
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridCompaignmaster_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridCompaignmaster_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(compaignstatus);
                $("#ctl00_cphPageContent_radgridCompaignmaster_ctl00_ctl" + i + "_TYPE").autocomplete(compaigntype);
              //  $("#ctl00_cphPageContent_radgridCompaignmaster_ctl00_ctl" + i + "_EMPLOYEE").autocomplete(employee);
                $("#ctl00_cphPageContent_radgridmapping_ctl00_ctl" + i + "_CAMPAIGN_NAME").autocomplete(compaignname);
                $("#ctl00_cphPageContent_radgridmapping_ctl00_ctl" + i + "_TOUR_SHORT_NAME").autocomplete(tourshortname);
                $("#ctl00_cphPageContent_radgridcampaigndetails_ctl00_ctl" + i + "_TARGETLIST_NAME").autocomplete(targetlistname);
                $("#ctl00_cphPageContent_radgridmapping_ctl00_ctl" + i + "_TOUR_CODE").autocomplete(tourcode);
            }
            
        });       
    </script>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Campaign Detail?'))return false; delCampaign(); return false;"
                    Text="Delete" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridCompaignmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CAMPAIGN_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2100px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CAMPAIGN_ID" DataField="CAMPAIGN_ID" HeaderText="CAMPAIGN_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CAMPAIGN_NAME" DataField="CAMPAIGN_NAME" HeaderText="Campaign Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CAMPAIGN_CODE" DataField="CAMPAIGN_CODE" HeaderText="Campaign Code">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="START_DATE" DataField="START_DATE" HeaderText=" Start Date">
                          <ItemTemplate>
                            <asp:TextBox ID="START_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="END_DATE" DataField="END_DATE" HeaderText="End Date">
                          <ItemTemplate>
                            <asp:TextBox ID="END_DATE" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="▼Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATUS_NAME" DataField="STATUS_NAME" HeaderText="▼Status">
                          <ItemTemplate>
                            <asp:TextBox ID="STATUS_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TYPE" DataField="TYPE" HeaderText="▼Campaign Type">
                          <ItemTemplate>
                            <asp:TextBox ID="TYPE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="EMPLOYEE" DataField="EMPLOYEE" HeaderText="▼Employee Name" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="EMPLOYEE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BUDGET" DataField="BUDGET" HeaderText="Budget">
                          <ItemTemplate>
                            <asp:TextBox ID="BUDGET" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EXPECTED_COST" DataField="EXPECTED_COST" HeaderText="Expected Cost">
                          <ItemTemplate>
                            <asp:TextBox ID="EXPECTED_COST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACTUAL_COST" DataField="ACTUAL_COST" HeaderText="Actual Cost">
                          <ItemTemplate>
                            <asp:TextBox ID="ACTUAL_COST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MISC_COST" DataField="MISC_COST" HeaderText="Misc. Cost">
                          <ItemTemplate>
                            <asp:TextBox ID="MISC_COST" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EXPECTED_REVENU" DataField="EXPECTED_REVENU" HeaderText="Expected Revenue">
                          <ItemTemplate>
                            <asp:TextBox ID="EXPECTED_REVENU" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="OFFER" DataField="OFFER" HeaderText="Offer" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DESCRIPTION" DataField="DESCRIPTION" HeaderText="Description">
                          <ItemTemplate>
                            <asp:TextBox ID="DESCRIPTION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TRACKER_NAME" DataField="TRACKER_NAME" HeaderText="Tracker Name" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TRACKER_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TRACKER_LINK" DataField="TRACKER_LINK" HeaderText="Tracker Link" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TRACKER_LINK" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewcompaign(this,event);">
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
                <ClientEvents OnCommand="radgridcampaign_Command" OnRowSelected="radgridcampaign_RowSelected" OnRowDblClick="CampaignRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
    </table>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblmapping" runat="server" Text="Campaign Marketing Mapping"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridmapping" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CAMP_MAR_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="600px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CAMP_MAR_ID" DataField="CAMP_MAR_ID" HeaderText="Campaign Name" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMP_MAR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CAMPAIGN_NAME" DataField="CAMPAIGN_NAME" HeaderText="Campaign Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_SHORT_NAME" DataField="TOUR_SHORT_NAME" HeaderText="Tour Short Name">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_SHORT_NAME" runat="server" CssClass="radinput" onblur="getTourName(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TOUR_CODE" DataField="TOUR_CODE" HeaderText="Tour Code">
                          <ItemTemplate>
                            <asp:TextBox ID="TOUR_CODE" runat="server" CssClass="radinput" onblur="getTourId(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewcam_mar(this,event);">
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
                <ClientEvents OnCommand="radgridmapping_Command" OnRowSelected="radgridmapping_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
               </telerik:radgrid>
               <asp:LinkButton ID="lbAddPayment" runat="server" Text="Add New" OnClientClick="addnewMapping();"></asp:LinkButton> 
            </td>
        </tr>
    </table>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="TargetList Detail"></asp:Literal>
    </div>
    <br />
    <br />
     <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridcampaigndetails" runat="server" allowpaging="false" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="TARGET_DETAILS_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="TARGET_DETAILS_ID" DataField="TARGET_DETAILS_ID" HeaderText="TARGET_DETAILS_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TARGET_DETAILS_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TARGETLIST_NAME" DataField="TARGETLIST_NAME" HeaderText="▼Target List Name">
                          <ItemTemplate>
                            <asp:TextBox ID="TARGETLIST_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CAMPAIGN_NAME" DataField="CAMPAIGN_NAME" HeaderText="▼Campaign Name" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_TYPE_NAME" DataField="CUST_TYPE_NAME" HeaderText="▼Customer Type" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_TYPE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewCampaigndetails(this,event);">
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
                <ClientEvents OnCommand="radgridcampaigndetails_Command" OnRowSelected="radgridcampaigndetails_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
               <asp:LinkButton ID="LinkButton1" runat="server" Text="Add New" OnClientClick="addnewTargetlist();"></asp:LinkButton> 
            </td>
        </tr>
</table>
<table>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Go To Next" OnClientClick="Redirect();"
                    Style="color: black;" />
            </td>
        </tr>
    </table>
</asp:Content>
