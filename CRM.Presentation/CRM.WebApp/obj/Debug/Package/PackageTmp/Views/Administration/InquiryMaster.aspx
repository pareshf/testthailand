<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="InquiryMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.InquiryMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
     <script type="text/javascript">

         var CUST_ID = "";
         var receivednby = '<%=Session["usersname"]%>';

         function pageLoad() {

             CUST_ID = getValue("CUST_ID");
             inquirymasterTableView = $find("<%= radgridinquirymaster.ClientID %>").get_masterTableView();
             inquirymastercommandname = "Load";

             var q = window.location.search.substring(1);
             if (q != "") {
                 CRM.WebApp.webservice.InquiryMasterWebService.GetInqwithcustid(CUST_ID, updatetinquiry);
             }
             else {
                 CRM.WebApp.webservice.InquiryMasterWebService.GetInq(inquirymasterTableView.get_currentPageIndex() * inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_sortExpressions().toString(), inquirymasterTableView.get_filterExpressions().toDynamicLinq(), updatetinquiry);

             }


             var receivednby = '<%=Session["usersname"]%>';
             var grid = $find("<%=radgridinquirymaster.ClientID %>")
             var masterTable = grid.get_masterTableView();
             
             for (var j = 0; j < masterTable.get_dataItems().length; j++) {
                 var recuser;
                 masterTable.get_dataItems()[j].findElement("ASSIGNED_TO").value = receivednby;
                 recuser = receivednby;
             }

             for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                 var a = "";

                 // current date display
                 var today = new Date().format('dd/MM/yyyy');
                 var txtdate = masterTable.get_dataItems()[i].findElement("INQUIRY_DATE");
                 txtdate.value = today;
             }
         }

         function getValue(variable) {

             var query = window.location.search.substring(1);
             var vars = query.split("&");
             for (var i = 0; i < vars.length; i++) {
                 var pair = vars[i].split("=");
                 if (pair[0] == variable) {
                     return pair[1];
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

         function InquiryAdded(sender, args) {
             currentRowIndex = sender.parentNode.parentNode.rowIndex;
             var ary = [];

             ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CUST_REL_SRNO;
             ary[1] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUSTOMER_UNQ_ID").value;
             ary[2] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
             ary[3] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_SURNAME").value;
             ary[4] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_NAME").value;
             ary[5] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_EMAIL").value;
             ary[6] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_REL_MOBILE").value;
             ary[7] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_TYPE_DESC").value;
             ary[8] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("INQUIRY_ID").value;
             ary[9] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("INQUIRY_DATE").value;
             ary[10] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("RATING_NAME").value;
             ary[11] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("INQUIRY_STATUS_NAME").value;
             ary[12] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
             ary[13] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ASSIGNED_TO").value;
             ary[14] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TRANSFERED_BY").value;
             ary[15] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("INQUIRY_FOR").value;
             ary[16] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("REGION_NAME").value;
             ary[17] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
             ary[18] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
             ary[19] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_ADULTS").value;
             ary[20] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_CWB").value;
             ary[21] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_CNB").value;
             ary[22] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NO_OF_INFANT").value;
             ary[23] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("REMARKS").value;
             ary[24] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TOTAL_BUDGET").value;
             ary[25] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TRAVEL_DURATION").value;
             ary[26] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("TRAVEL_DATE").value;
             ary[27] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("NEXT_FOLLOWUP_DATE").value;
             ary[28] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("AGENT_NAME").value;
             ary[29] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("REFERENCE").value;
             ary[30] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("REFERENCE_NAME").value;
             ary[31] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP1_DATE").value;
             ary[32] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP1_REMARKS").value;
             ary[33] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP2_DATE").value;
             ary[34] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP2_REMARKS").value;
             ary[35] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP3_DATE").value;
             ary[36] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP3_REMARKS").value;
             ary[37] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP4_DATE").value;
             ary[38] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP4_REMARKS").value;
             ary[39] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP5_DATE").value;
             ary[40] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP5_REMARKS").value;
             ary[41] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP6_DATE").value;
             ary[42] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP6_REMARKS").value;
             ary[43] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP7_DATE").value;
             ary[44] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP7_REMARKS").value;
             ary[45] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP8_DATE").value;
             ary[46] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP8_REMARKS").value;
             ary[47] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP9_DATE").value;
             ary[48] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP9_REMARKS").value;
             ary[49] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP10_DATE").value;
             ary[50] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FOLLOWUP10_REMARKS").value;
             ary[51] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAIGN_NAME").value;
             ary[52] = inquirymasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CAMPAGIN_OWNER").value;

             for (i = 0; i < 41; i++) {
                 if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
             }
             if (!ary[2] == 0 && !ary[3] == 0 && !ary[4] == 0 && !ary[15] == 0 && !ary[16] == 0 && !ary[19] == 0 && !ary[7] == 0) {
                 if (!ary[5] == 0 || !ary[11] == 0) {
                     try {
                         CRM.WebApp.webservice.InquiryMasterWebService.InsertUpdateInq(ary); // add new cust
                         CRM.WebApp.webservice.InquiryMasterWebService.GetInq(inquirymasterTableView.get_currentPageIndex() * inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_sortExpressions().toString(), inquirymasterTableView.get_filterExpressions().toDynamicLinq(), updatetinquiry);

                         alert('Record Save Successfully');
                     }
                     catch (e) {
                         alert('Wrong Data Inserted');
                     }
                 }
                 else {
                     alert('Enter Mobile No Or Phone No');
                 }
             }
             else {
                 alert('Enter Full Customer Detail');
             }
         }
            </script>
   </telerik:radcodeblock>
    <script type="text/javascript" src="../Shared/Javascripts/InquiryMasterGridScript.js"></script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageCust" runat="server" Text="Inquiry Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var rating = "../../webservice/autocomplete.ashx?key=FETCH_RATING_AUTOSEARCH";
            var status = "../../webservice/autocomplete.ashx?key=FETCH_STATUS_AUTOSEARCH";
            var regionname = "../../webservice/autocomplete.ashx?key=GET_REGION_TYPE_FOR_AUTOSERCH";
            var agent = "../../webservice/autocomplete.ashx?key=FETCH_AGENT_AUTOSEARCH";
            var refrence = "../../webservice/autocomplete.ashx?key=GET_REFRENCE_NAME_AUTOSEARCH";
            var campaign = "../../webservice/autocomplete.ashx?key=GET_COMPAIGN_NAME_AUTOSEARCH"; 

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_RATING_NAME").autocomplete(rating);
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_INQUIRY_STATUS_NAME").autocomplete(status);
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_REGION_NAME").autocomplete(regionname);
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_AGENT_NAME").autocomplete(agent);
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_REFERENCE_NAME").autocomplete(refrence);
                $("#ctl00_cphPageContent_radgridinquirymaster_ctl00_ctl" + i + "_CAMPAIGN_NAME").autocomplete(campaign);
            }

        });       
    </script>
    <div id="divradmastergrid">
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridinquirymaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="4850">
            <MasterTableView ClientDataKeyNames="INQUIRY_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                  <Columns>                                     
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_SRNO" DataField="CUST_REL_SRNO" HeaderText="Cust rel sr no " Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                 
                    <telerik:GridTemplateColumn SortExpression="CUSTOMER_UNQ_ID" DataField="CUSTOMER_UNQ_ID" HeaderText="Customer Id" >
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="CUSTOMER_UNQ_ID" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                        <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_SURNAME" DataField="CUST_REL_SURNAME" HeaderText="Surname">
                        <ItemTemplate>
                             <asp:TextBox ID="CUST_REL_SURNAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_NAME" DataField="CUST_REL_NAME" HeaderText="Name">
                  <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_NAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_EMAIL" DataField="CUST_REL_EMAIL" HeaderText="Email">                       
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_EMAIL" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                   
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE" HeaderText="Mobile">                    
                         <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_TYPE_DESC" DataField="CUST_TYPE_DESC" HeaderText="Type">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_TYPE_DESC" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="INQUIRY_ID" DataField="INQUIRY_ID" HeaderText="Inquiry Id">
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_ID" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="INQUIRY_DATE" DataField="INQUIRY_DATE" HeaderText="Inquiry Date">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_DATE" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="RATING_NAME" DataField="RATING_NAME" HeaderText="Rating">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="RATING_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="INQUIRY_STATUS_NAME" DataField="INQUIRY_STATUS_NAME" HeaderText="Status">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Branch">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" style="background-color:LightBlue" readonly="true" text="H.O"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ASSIGNED_TO" DataField="ASSIGNED_TO" HeaderText="Assigned To">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="ASSIGNED_TO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="TRANSFERED_BY" DataField="TRANSFERED_BY" HeaderText="Transfered By">
                       <ItemTemplate>
                            <asp:TextBox ID="TRANSFERED_BY" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="INQUIRY_FOR" DataField="INQUIRY_FOR" HeaderText="Inquiry For">                    
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="INQUIRY_FOR" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="REGION_NAME" DataField="REGION_NAME" HeaderText="Region">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="REGION_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="NO_OF_ADULTS" DataField="NO_OF_ADULTS" HeaderText="Adults">                       
                        <ItemTemplate>                            
                            <asp:TextBox ID="NO_OF_ADULTS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="NO_OF_CWB" DataField="NO_OF_CWB" HeaderText="CWB">
                        <ItemTemplate>
                            <asp:TextBox ID="NO_OF_CWB" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="NO_OF_CNB" DataField="NO_OF_CNB" HeaderText="CNB">
                        <ItemTemplate>
                            <asp:TextBox ID="NO_OF_CNB" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="NO_OF_INFANT" DataField="NO_OF_INFANT" HeaderText="Infant">
                        <ItemTemplate>
                            <asp:TextBox ID="NO_OF_INFANT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="REMARKS" DataField="REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="TOTAL_BUDGET" DataField="TOTAL_BUDGET" HeaderText="Total Budget">                    
                        <ItemTemplate>
                            <asp:TextBox ID="TOTAL_BUDGET" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="TRAVEL_DURATION" DataField="TRAVEL_DURATION" HeaderText="Travel Duration">
                        <ItemTemplate>
                            <asp:TextBox ID="TRAVEL_DURATION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="TRAVEL_DATE" DataField="TRAVEL_DATE" HeaderText="Travel Date">
                        <ItemTemplate>
                            <asp:TextBox ID="TRAVEL_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="NEXT_FOLLOWUP_DATE" DataField="NEXT_FOLLOWUP_DATE" HeaderText="Next Followup Date">
                        <ItemTemplate>
                            <asp:TextBox ID="NEXT_FOLLOWUP_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="AGENT_NAME" DataField="AGENT_NAME" HeaderText="Agent Name">
                        <ItemTemplate>
                            <asp:TextBox ID="AGENT_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="REFERENCE" DataField="REFERENCE" HeaderText="Refrence">
                        <ItemTemplate>
                            <asp:TextBox ID="REFERENCE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="REFERENCE_NAME" DataField="REFERENCE_NAME" HeaderText="Refrence Name">
                        <ItemTemplate>
                            <asp:TextBox ID="REFERENCE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP1_DATE" DataField="FOLLOWUP1_DATE" HeaderText="1st Followup Date">
                       <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP1_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP1_REMARKS" DataField="FOLLOWUP1_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP1_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP2_DATE" DataField="FOLLOWUP2_DATE" HeaderText="2nd Followup Date">
                      <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP2_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP2_REMARKS" DataField="FOLLOWUP2_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP2_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP3_DATE" DataField="FOLLOWUP3_DATE" HeaderText="3rd Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP3_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP3_REMARKS" DataField="FOLLOWUP3_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP3_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP4_DATE" DataField="FOLLOWUP4_DATE" HeaderText="4th Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP4_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP4_REMARKS" DataField="FOLLOWUP4_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP4_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP5_DATE" DataField="FOLLOWUP5_DATE" HeaderText="5th Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP5_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP5_REMARKS" DataField="FOLLOWUP5_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP5_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP6_DATE" DataField="FOLLOWUP6_DATE" HeaderText="6th Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP6_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP6_REMARKS" DataField="FOLLOWUP6_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP6_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP7_DATE" DataField="FOLLOWUP7_DATE" HeaderText="7th Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP7_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP7_REMARKS" DataField="FOLLOWUP7_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP7_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP8_DATE" DataField="FOLLOWUP8_DATE" HeaderText="8th Followup Date">
                       <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP8_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP8_REMARKS" DataField="FOLLOWUP8_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP8_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP9_DATE" DataField="FOLLOWUP9_DATE" HeaderText="9th Followup Date">
                       <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP9_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP9_REMARKS" DataField="FOLLOWUP9_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP9_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FOLLOWUP10_DATE" DataField="FOLLOWUP10_DATE" HeaderText="10th Followup Date">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" ForeColor = "Red"/>
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP10_DATE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="FOLLOWUP10_REMARKS" DataField="FOLLOWUP10_REMARKS" HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="FOLLOWUP10_REMARKS" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CAMPAIGN_NAME" DataField="CAMPAIGN_NAME" HeaderText="Camapaign">
                        <ItemTemplate>
                            <asp:TextBox ID="CAMPAIGN_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CAMPAGIN_OWNER" DataField="CAMPAGIN_OWNER" HeaderText="Camapaign Owner">
                        <ItemTemplate>
                            <asp:TextBox ID="CAMPAGIN_OWNER" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="InquiryAdded(this,event);">
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
                <ClientEvents OnCommand="radgridinquirymaster_Command" OnRowSelected="radgridinquirymaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
