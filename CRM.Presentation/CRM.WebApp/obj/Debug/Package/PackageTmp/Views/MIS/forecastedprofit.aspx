<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="forecastedprofit.aspx.cs" Inherits="CRM.WebApp.Views.MIS.forecastedprofit" %>

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
         label
        {
            padding-right: 10px;
        }
    </style>
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
        //        function print() {
        //            window.open('dailyoperationdetalis.aspx?key' = +FROM_DATE);
        //            // window.open('BookingReport2.aspx?key=' + BOOKING_ID);
        //        }
    </script>
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Forecasted Profit Reports"></asp:Literal>
    </div>
    <div>
        <telerik:RadDatePicker ID="RadDatePicker1" Style="display: none;" MinDate="01/01/1900"
            MaxDate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:RadDatePicker>
    </div>

     <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <contenttemplate>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Company Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                    class="error"></span>
            </td>
            <td>
                <%--  <asp:DropDownList ID="drpAgent" runat="server" Width="200px" OnSelectedIndexChanged="drpAgent_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>--%>
                <telerik:RadComboBox ID="drpAgent" runat="server" Width="269px" AutoPostBack="true"
                    ShowDropDownOnTextboxClick="true" AllowCustomText="false" OnSelectedIndexChanged="drpAgent_SelectedIndexChanged"
                    ValidationGroup="Required">
                </telerik:RadComboBox>
                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Company Name Required"
                    CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" InitialValue="Please Select"
                    ControlToValidate="drpAgent"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Invoice Type" CssClass="lblstyle"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rbtninvoicetype" runat="server" Style="padding-right: 5px">
                    <asp:ListItem Text="All" Value="2" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="USD" Value="0"></asp:ListItem>
                    <asp:ListItem Text="THB" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblfromdate" runat="server" Text="From Date" CssClass="lblstyle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtfromdate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                    Visible="true" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                    onkeydown="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="To Date" CssClass="lblstyle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="<%$appSettings:TextBoxWidth%>" Visible="true"
                    onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="To Date is required." ValidationGroup="valid"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Travel From Date" CssClass="lblstyle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttraveltodate" runat="server" Width="<%$appSettings:TextBoxWidth%>"
                    Visible="true" onclick="showPopup(this, event);" onfocus="showPopup(this, event);"
                    onkeydown="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Travel To Date" CssClass="lblstyle"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txttravelfromdate" runat="server" Width="<%$appSettings:TextBoxWidth%>" Visible="true"
                    onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnshow_Click" ValidationGroup="Required" />
             <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click"/>
            </td>
        </tr>
    </table>
    </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
