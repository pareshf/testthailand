<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="LedgerReports.aspx.cs" Inherits="CRM.WebApp.Views.Account.StandardReport.LedgerReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <script type="text/javascript">
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
        
    </script>
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Ledger Reports"></asp:Literal>
    </div>
    
    <div>
        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    </div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Ledger"></asp:Label>
            </td>
            <td>
                <%-- <asp:DropDownList ID="drpAgentType" runat="server" >
                            </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="reqMobile" runat="server" ControlToValidate="txtfromdate"
                    ErrorMessage="Please Select Ledger." ValidationGroup="valid" Font-Size="12px"></asp:RequiredFieldValidator>--%>
                <telerik:radcombobox id="drpBoard" runat="server" onitemsrequested="drpBoard_ItemsRequested"
                    enableloadondemand="True" showmoreresultsbox="true" enablevirtualscrolling="true"
                    width="250px" height="150px">
                </telerik:radcombobox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                    ErrorMessage="Board Name is Required" ControlToValidate="drpBoard" ValidationGroup="valid"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblfromdate" runat="server" Text="From Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                    Visible="true" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                    onkeydown="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="To Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="<%$appSettings:TextBoxWidth%>" Visible="true"
                    onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnshow_Click"
                    ValidationGroup="valid" />
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div>
    <asp:UpdatePanel ID="upnlReport" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <rsweb:ReportViewer ID="rptViewer1" runat="server" AsyncRendering="true" BorderWidth="1px"
                Height="8.5in" ProcessingMode="Remote" 
                Width="14in">
            </rsweb:ReportViewer>
       </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
