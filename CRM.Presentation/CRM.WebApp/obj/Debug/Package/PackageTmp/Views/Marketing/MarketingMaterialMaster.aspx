<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="MarketingMaterialMaster.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.MarketingMaterialMaster" %>
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
        <script type="text/javascript" src="../Shared/Javascripts/CustomerPayment.js"></script>
        
        <script type="text/javascript">

            function pageLoad() {



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
           
        </script>
    </telerik:radcodeblock>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Marketing Material Master"></asp:Literal>
    </div>
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";

            $("#ctl00_cphPageContent_txtTour").autocomplete(tourshortname);
            

        });       
    </script>
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMarId" runat="server" Text="Material id :" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMarId" runat="server" Width="196px" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltour" runat="server" Text="Tour Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTour" runat="server" Width="196px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
               <asp:Label ID="lbltitle" runat="server" Text="Title :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="195px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbltype" runat="server" Text="Type :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtType" runat="server" Width="195px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
               <asp:Label ID="lblExpirationdate" runat="server" Text="Expiration Date :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExpirationdate" runat="server" Width="195px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldescription" runat="server" Text="Description:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtdescription" runat="server" Width="195px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
                <asp:Label ID="lblEmbadedcode" runat="server" Text="Embaded Code:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEmbadedcode" runat="server" Width="195px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWeburl" runat="server" Text="Web Url:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWeburl" runat="server" Width="195px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click"/>
                <asp:Label ID="lblcheck" runat="server" Text="0" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
   <asp:GridView ID="gridMarketingMaterial" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" DataKeyNames="MAR_ID" OnPageIndexChanging="gridMarketingMaterial_OnPageIndexChanging"
                    onrowdeleting="gridMarketingMaterial_RowDeleting" 
                    OnSelectedIndexChanging="gridMarketingMaterial_SelectedIndexChanging" 
                    BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
                    CellPadding="3" CellSpacing="1" GridLines="None">
    <Columns>
        <asp:ButtonField CommandName="Select" Text="Edit" ButtonType="Button" HeaderText="Edit"/>
        <asp:ButtonField CommandName="Delete" Text="Delete" ButtonType="Button" HeaderText="Delete" />
        <asp:BoundField HeaderText="Mar Id" DataField="MAR_ID" Visible="false"/>
        <asp:BoundField HeaderText="Tour Name" DataField="TOUR_ID"/>
        <asp:BoundField HeaderText="Title" DataField="TITLE" ControlStyle-Width="40px">
<ControlStyle Width="40px"></ControlStyle>
        </asp:BoundField>
        <asp:BoundField HeaderText="Type" DataField="TYPE"/>
        <asp:BoundField HeaderText="Expiration Date" DataField="EXPIRATION_DATE"/>
        <asp:BoundField HeaderText="Description" DataField="DESCRIPTION"/>
        <asp:BoundField HeaderText="Embaded Code" DataField="EMBEDCODE"/>
        <asp:BoundField HeaderText="Web Url" DataField="WEBURL"/>
        <asp:BoundField HeaderText="Document" DataField="ATTACHMENT"/>
    </Columns>
       <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
       <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
       <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
       <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
       <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
       <SortedAscendingCellStyle BackColor="#F1F1F1" />
       <SortedAscendingHeaderStyle BackColor="#594B9C" />
       <SortedDescendingCellStyle BackColor="#CAC9C9" />
       <SortedDescendingHeaderStyle BackColor="#33276A" />
    </asp:GridView>
        </td>
        </tr>
    </table>
</asp:Content>
