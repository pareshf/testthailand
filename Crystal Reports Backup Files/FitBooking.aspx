<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="FitBooking.aspx.cs" Inherits="CRM.WebApp.Views.FIT.FitBooking" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .textBox
        {
            width: 200;
        }
        .pageLabel
        {
            padding-left: 10px;
            font-family: Arial;
        }
        .sectionHeader
        {
            padding-left: 10px;
            font-family: Arial;
            font-weight: bold;
        }
         .error
        {
            font-family: Arial;
            font-size: 12px;
            color: #FF0000;
        }
        .searchstyle
        {
            font-family:Arial;
            font-weight:bold;
            font-size:12px;
        }
         .requiredfiled
        {
            font-family: Verdana;
            font-size: 12px;
            color:Red ;
        }
    </style>
    <script type="text/javascript" src="../Shared/Javascripts/FitBookingJS.js"></script>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            //            function pageLoad() {
            //                HotelTableView = $find("<%=dlHotelDetails.ClientID %>").get_masterTableView();
            //            }

            var currentTextBox = null; var currentDatePicker = null; function showPopup(sender, e) {
                try { currentTextBox = sender; var datePicker = $find("<%= RadDatePicker1.ClientID %>"); currentDatePicker = datePicker; datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value)); var position = datePicker.getElementPosition(sender); datePicker.showPopup(position.x, position.y + sender.offsetHeight); }
                catch (e) { }
            }
            function dateSelected(sender, args) {
                try { if (currentTextBox != null) { currentTextBox.value = args.get_newDate().format('dd/MM/yyyy'); currentDatePicker.hidePopup(); } }
                catch (e) { }

            }
            function parseDate(sender, e) { currentDatePicker.hidePopup(); }

            function bindData() {
                CRM.WebApp.webservice.FitBooking.GetHotelDetail(updateHotelGrid);
            }

            function updateHotelGrid(result) {
                HotelTableView = $find("<%=dlHotelDetails.ClientID %>").get_masterTableView();
                HotelTableView.set_dataSource(result);
                HotelTableView.dataBind();
            }
    </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblFitBooking" runat="server" Text="FIT Bookings"></asp:Literal>
    </div>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <br />
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Shared/Javascripts/TimeDropDown.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var CityName = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";

            $("#ctl00_cphPageContent_txtCity").autocomplete(CityName);

            $(".scroll").click(function (event) {
                //prevent the default action for the click event
                event.preventDefault();

                //get the full url - like mysitecom/index.htm#home
                var full_url = this.href;

                //split the url by # and get the anchor target name - home in mysitecom/index.htm#home
                var parts = full_url.split("#");
                var trgt = parts[1];

                //get the top offset of the target anchor
                var target_offset = $("#" + trgt).offset();
                var target_top = target_offset.top;

                //goto that anchor by setting the body scroll top to anchor top
                $('html, body').animate({ scrollTop: target_top }, 500);
            });

        });
    </script>
    <%--Search Panel--%>
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="lblTravelDate" runat="server" Text="Travel Date" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDateSearch" runat="server" Width="200" onclick="showPopup(this, event);"
                                onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="From"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToDateSearch" runat="server" Width="200" onclick="showPopup(this, event);"
                                onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Text="To"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCity" runat="server" Text="City" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="lblSearch" runat="server" Text="Search" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" Width="500"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 150px">
                            <asp:Label ID="lblAll" runat="server" Text="All" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAll" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHotel" runat="server" Text="Hotel" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkHotel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCruise" runat="server" Text="Cruise" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCruise" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSightSeeing" runat="server" Text="Sightseeing(Optional)" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSight" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTransferPackage" runat="server" Text="Transfer Package" CssClass="pageLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTransferPackage" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px; padding-top: 25px;">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" style="padding-left: 75px;">
                <table border="1" style="border-color: LightGray; border-collapse: collapse">
                    <tr>
                        <td>
                            <table>
                                <tr align="center">
                                    <td>
                                        <asp:Label ID="lblMyCart" runat="server" Text="My Cart" CssClass="sectionHeader"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                                        <td>
                                                            <asp:Label ID="lblProduct" runat="server" Text="Product" CssClass="sectionHeader"></asp:Label>
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            &nbsp;
                                                            
                                                            <asp:Label ID="lblQty" runat="server" CssClass="sectionHeader" Text="Quantity"></asp:Label>
                                                        </td>
                                                    </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upHotelCart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:DataList ID="dlHotelCart" runat="server" RepeatDirection="Vertical">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <%-- <td>
                                                                                <asp:Label ID="lblHotelOrderNo" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="width: 200px;">
                                                                                <asp:Label ID="lblHotelDetail" runat="server" Text='<%# Bind("CHAIN_NAME") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <asp:Label ID="lblHotelQty" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upCruiseCart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DataList ID="dlCruiseCart" runat="server" RepeatDirection="Vertical">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <%--  <td>
                                                                                <asp:Label ID="lblCruiseOrderNo" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="width: 200px;">
                                                                                <asp:Label ID="lblCruiseDetail" runat="server" Text='<%# Bind("CHAIN_NAME") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <asp:Label ID="lblCruiseQty" runat="server" Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upSightCart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DataList ID="dlSightCart" runat="server" RepeatDirection="Vertical">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <%-- <td>
                                                                                <asp:Label ID="lblSightOrderNo" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="width: 200px;">
                                                                                <asp:Label ID="lblSightDetail" runat="server" Text='<%# Bind("SITE_NAME") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <asp:Label ID="lblSightQty" runat="server" Text='<%# Bind("DATE") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upTransferCart" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DataList ID="dlTransferCart" runat="server" RepeatDirection="Vertical">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <%-- <td>
                                                                                <asp:Label ID="lblTransferOrderNo" runat="server" Text='<%# Bind("ORDER_NO") %>'></asp:Label>
                                                                            </td>--%>
                                                                            <td style="width: 200px;">
                                                                                <asp:Label ID="lblTransferDetail" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                <asp:Label ID="lblTransferQty" runat="server" Text='<%# Bind("DATE") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnMyCart" runat="server" Text="View Cart" OnClick="btnMyCart_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <asp:UpdatePanel ID="upSearchResultCount" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div id="searchCount" runat="server" visible="false">
            <table>
                <tr>
                    <td style="padding-left: 10px;">
                        <asp:Label ID="lblSearchResult" runat="server" Text="Search Results" CssClass="searchstyle"
                            Visible="false"></asp:Label>
                    </td>
                    <td style="padding-left: 10px;">
                        <a href="#hotel" style="font-weight:bold; font-size:12px; font-family:Arial" >Hotel </a>:
                        <%--<asp:Label ID="lblNoOfHotel" runat="server" Text="Hotel : " Visible="false"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblHotelSearchCount" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td style="padding-left: 10px;">
                        <a href="#cruise" style="font-weight:bold; font-size:12px; font-family:Arial">Cruise </a>:
                        <%--<asp:Label ID="lblNoOfCruise" runat="server" Text="Cruise : " Visible="false"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblCruiseSearchCount" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td style="padding-left: 10px;">
                        <a href="#sight" style="font-weight:bold; font-size:12px; font-family:Arial">Sightseeing (Optional) </a>:
                        <%--<asp:Label ID="lblNoOfSight" runat="server" Text="Sight Seeing : " Visible="false"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblSightSearchCount" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td style="padding-left: 10px;">
                        <a href="#transfer" style="font-weight:bold; font-size:12px; font-family:Arial">Transfer Package </a>:
                        <%--<asp:Label ID="lblNoOfTransfer" runat="server" Text="Transfer Package : " Visible="false"></asp:Label>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblTransferSearchCount" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <%--End Search Panel--%>
    <br />
    <table>
        <tr>
            <td>
                <%--Hotel Details--%>
                <div id="hotel">
                <asp:UpdatePanel ID="upHotelDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHotelDetail" runat="server" Text="Hotels" CssClass="sectionHeader" style="font-size:medium;"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dlHotelDetails" runat="server" RepeatDirection="Vertical" OnItemCommand="dlHotelDetails_ItemCommand">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSrNoHotel" runat="server" Text='<%# Bind("SUPPLIER_HOTEL_PRICE_LIST_ID") %>'
                                                            Visible="false"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblHotelName" runat="server" CssClass="pageLabel" Text="Hotel Name : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHotelNameFromDb" runat="server" Text='<%# Bind("CHAIN_NAME") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="lblRoomTypeName" runat="server" CssClass="pageLabel" Text="Room Type : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRoomTypeNameFromDb" runat="server" Text='<%# Bind("ROOM_TYPE_NAME") %>'></asp:Label>
                                                    </td>
                                                </tr>--%>
                                               
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="lblRoomDesc" runat="server" CssClass="pageLabel" Text="Room Description : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRoomDescFromDb" runat="server" Text='<%# Bind("ROOM_DESC") %>'></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                 <tr>
                                                    <td>
                                                        <asp:Label ID="lblRoomTypeName" runat="server" CssClass="pageLabel" Text="Room Type : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpRoomType" runat="server">
                                                           <asp:ListItem Text="Single" Value="1" Selected="True"></asp:ListItem>
                                                           <asp:ListItem Text="Double/Twin" Value="2"></asp:ListItem>
                                                           <asp:ListItem Text="Triple" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblQtyHotel" runat="server" Text="No Of Room :" CssClass="pageLabel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQtyHotel" runat="server" Width="25" Text="1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFromDateHotel" runat="server" Text="From Date :" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDateHotel" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="From Date Is Required" ControlToValidate="txtFromDateHotel" CssClass="requiredfiled" ValidationGroup="Hotel"></asp:RequiredFieldValidator>
--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblToDateHotel" runat="server" Text="To Date :" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDateHotel" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="To Date Is Required" ControlToValidate="txtToDateHotel" CssClass="requiredfiled" ValidationGroup="Hotel"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddToCartHotel" runat="server" Text="Add To Cart" CommandName="AddToCartHotel"
                                                            CommandArgument='<%# Bind("SUPPLIER_HOTEL_PRICE_LIST_ID") %>' ValidationGroup="Hotel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <br />
                <%--Cruise Detail--%>
                <div id="cruise">
                <asp:UpdatePanel ID="upCruiseDetail" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCruiseDetail" runat="server" Text="Cruises" CssClass="sectionHeader" style="font-size:medium;"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dlCruiseDetail" runat="server" RepeatDirection="Vertical" OnItemCommand="dlCruiseDetail_ItemCommand">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSrNoCruise" runat="server" Text='<%# Bind("SUPPLIER_CRUISE_PRICE_LIST_ID") %>'
                                                            Visible="false"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><asp:Label ID="lblCruiseCompanyName" runat="server" CssClass="pageLabel" Text="Company Name : "></asp:Label></td>
                                                    <td><asp:Label ID="lblCruiseCompFromDb" runat="server" CssClass="pageLabel" Text='<%# Bind("CHAIN_NAME") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCruiseDesc" runat="server" CssClass="pageLabel" Text="Cruise Description : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCruiseDescFromDb" runat="server" Text='<%# Bind("CRUISE_DESC") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCruiseView" runat="server" CssClass="pageLabel" Text="Cruise View : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCruiseViewFromDb" runat="server" Text='<%# Bind("CRUISE_VIEW") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td>
                                                        <asp:Label ID="lblCabinCategory" runat="server" CssClass="pageLabel" Text="Cabine Category : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCabinCategoryFromDb" runat="server" Text='<%# Bind("CATEGORY_DESC") %>'></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDeckNo" runat="server" CssClass="pageLabel" Text="Deck No : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDeckNoFromDb" runat="server" Text='<%# Bind("DECK_NO") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCabinCategory" runat="server" CssClass="pageLabel" Text="Cabine Category : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="drpCabineType" runat="server">
                                                           <asp:ListItem Text="Single" Value="1" Selected="True"></asp:ListItem>
                                                           <asp:ListItem Text="Double/Twin" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Triple" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblQtyCruise" runat="server" Text="No Of Cabines :" CssClass="pageLabel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQtyCruise" runat="server" Width="25" Text="1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFromDateCruise" runat="server" Text="From Date :" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDateCruise" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblToDateCruise" runat="server" Text="To Date :" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDateCruise" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddToCartCruise" runat="server" Text="Add To Cart" CommandName="AddToCartCruise"
                                                            CommandArgument='<%# Bind("SUPPLIER_CRUISE_PRICE_LIST_ID") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <br />
                <%--Sight Seeing Detail--%>
                <div id="sight">
                <asp:UpdatePanel ID="upSightSeeing" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSightSeeingDetail" runat="server" Text="Sight Seeing" CssClass="sectionHeader" style="font-size:medium;"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dlSightSeeing" runat="server" RepeatDirection="Vertical" OnItemCommand="dlSightSeeing_ItemCommand">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSrNoSightSeeing" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                            Visible="false"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSightName" runat="server" CssClass="pageLabel" Text="Sight Name :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSightNameFromDb" runat="server" Text='<%# Bind("SITE_NAME") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDinnerDesc" runat="server" CssClass="pageLabel" Text="Lunch/Dinner :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDinnerDescFromDb" runat="server" Text='<%# Bind("DINNER_DESCRIPTION") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDateSightSeeing" runat="server" CssClass="pageLabel" Text="Date :"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateSightSeeing" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTimeSightSeeing" runat="server" CssClass="pageLabel" Text="Time(hh:mm:ss) :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTimeSightSeeing" runat="server" Width="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddToCartSightSeeing" runat="server" Text="Add To Cart" CommandName="AddToCartSight"
                                                            CommandArgument='<%# Bind("SIGHT_SEEING_PRICE_ID") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                <br />
                <%--Transfer Package--%>
                <div id="transfer">
                <asp:UpdatePanel ID="upTransferPackage" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransferPackageDetail" runat="server" Text="Transfer Package" style="font-size:medium;"
                                        CssClass="sectionHeader" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="dlTransferPackage" runat="server" RepeatDirection="Vertical" OnItemCommand="dlTransferPackage_ItemCommand">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferPackage" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_PRICE_ID") %>'
                                                            Visible="false"></asp:Label><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblVehicalName" runat="server" CssClass="pageLabel" Text="Name : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVehicalNameFromDb" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferFromPlace" runat="server" CssClass="pageLabel" Text="From : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransferFromPlaceFromDb" runat="server" Text='<%# Bind("FROM_PLACE") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferToPlace" runat="server" CssClass="pageLabel" Text="To : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransferToPlaceFromDb" runat="server" Text='<%# Bind("TO_PLACE") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferFromTime" runat="server" CssClass="pageLabel" Text="From Time : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransferFromTimeFromDb" runat="server" Text='<%# Bind("FROM_TIME") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferToTime" runat="server" CssClass="pageLabel" Text="To Time : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransferToTimeDb" runat="server" Text='<%# Bind("TO_TIME") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTransferType" runat="server" CssClass="pageLabel" Text="Type : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTransferTypeFromDb" runat="server" Text='<%# Bind("COACH_CAR_FLAG") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDateTransferPackage" runat="server" Text="Date :" CssClass="pageLabel"></asp:Label>&nbsp;<span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateTransferPackage" runat="server" Width="100" onclick="showPopup(this, event);"
                                                            onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTimeTransferPackage" runat="server" Text="Time(hh:mm:ss) :" CssClass="pageLabel"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTimeTransferPackage" runat="server" Width="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddToCartTransferPackage" runat="server" Text="Add To Cart" CommandName="AddToCartTransfer"
                                                            CommandArgument='<%# Bind("TRANSFER_PACKAGE_PRICE_ID") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
