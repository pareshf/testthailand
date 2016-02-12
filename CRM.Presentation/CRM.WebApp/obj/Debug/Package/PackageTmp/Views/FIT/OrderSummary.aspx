<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="OrderSummary.aspx.cs" Inherits="CRM.WebApp.Views.FIT.OrderSummary" %>



<%--<%@ Register TagPrefix="crm" TagName="FlashMessage" Src="~/Views/Shared/Controls/Message/FlashMessage.ascx"%>
--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
&nbsp;<br />
<style>

    .sectionHeader
        {
           
            font-family: Arial;
            font-weight: bold;
        margin-left: 0px;
    }
.headlabel
{
    font-size:"40px";
    font-weight:bold;
      font-family: Verdana;
      
}

.fieldlabel
{
     font-family: Verdana;
            font-size: 12px;
}
.textboxstyle
{
     width: 50px;
}
  .buttonstyle
    {
        width: 150px;
    }
    </style>

 <script type="text/javascript">
var currentTextBox = null; var currentDatePicker = null; function showPopup(sender, e) {
                try { currentTextBox = sender; var datePicker = $find("<%= RadDatePicker1.ClientID %>"); currentDatePicker = datePicker; datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value)); var position = datePicker.getElementPosition(sender); datePicker.showPopup(position.x, position.y + sender.offsetHeight); }
                catch (e) { }
            }
            function dateSelected(sender, args) {
                try { if (currentTextBox != null) { currentTextBox.value = args.get_newDate().format('dd/MM/yyyy'); currentDatePicker.hidePopup(); } }
                catch (e) { }

            }
            function parseDate(sender, e) { currentDatePicker.hidePopup(); }


 </script>


<asp:Label runat="server" Text="Order Summary" ID="headlbl" Width="200px" Font-Bold="true" Font-Size="Large" ></asp:Label>
 <%--<div class="pageTitle" style="float: left">
        <asp:Literal ID="lblFitBooking" runat="server" Text="Order Summary"></asp:Literal>
    </div>--%>

     <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <br />
    <br />
   
    
    <br />
   <%-- <table style="width: 250px">
    <tr>
  <td align="left" valign="middle">
    <asp:UpdatePanel ID="upFlash" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        
                        <crm:FlashMessage ID="flashControl" runat="server" />
                        &nbsp;
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                </tr>
                </table>--%>
    <br />
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
<%--Hotel Details--%>
<asp:UpdatePanel ID="UpdatePanel_Hotel_header" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:Label ID="lblHotelDetail" runat="server" Text="Hotels" CssClass="headlabel"></asp:Label>
<table width="1000px" id="hotelheader" runat="server">



<tr>
        <td style="width:100px;">
        

             <asp:Label ID="Label3" runat="server"  CssClass="sectionHeader" Text="Sr No" align="left"></asp:Label>
        </td>
        <td style="width:250px;">
          
             <asp:Label ID="Label4" runat="server" CssClass="sectionHeader" Text="Product Description"></asp:Label>
        </td>
         <td style="width:100px;">
            
             <asp:Label ID="Label5" runat="server" CssClass="sectionHeader" Text="City" ></asp:Label>
        </td>
        <td style="width:150px;">
            
             <asp:Label ID="Label18" runat="server" CssClass="sectionHeader" Text="Room Type" ></asp:Label>
        </td>
        <td style="width:100px;">
             <asp:Label ID="Label6" runat="server" CssClass="sectionHeader" Text="Qty"></asp:Label>
        </td>
        <td style="width:150px;">
             <asp:Label ID="Label7" runat="server" CssClass="sectionHeader" Text="From Date" ></asp:Label>
        </td>
         <td style="width:150px;">
             <asp:Label ID="Label8" runat="server" CssClass="sectionHeader" Text="To Date" ></asp:Label>
        </td>
        <%-- <td width="10%">
             <asp:Label ID="Label9" runat="server" CssClass="sectionHeader" Text="From Time"></asp:Label>
        </td>
         <td width="10%">
             <asp:Label ID="Label10" runat="server" CssClass="sectionHeader" Text="To Time"></asp:Label>
        </td>--%>

    </tr>
    


</table>
</ContentTemplate>
</asp:UpdatePanel>

<%--<div id="Container" onclick="__doPostBack('UpdatePanel_TourDetails', '');">--%>
 <asp:UpdatePanel ID="UpdatePanel_Hotel_Data" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:DataList ID="dlhoteldetails" runat="server" OnItemCommand="dlhoteldetails_ItemCommand">


<ItemTemplate>

<table width="1215px" id="hoteldata" runat="server">

    
    <tr>
    
        <td  style="width:100px;">
           <asp:Label ID="lblHD_Srno" runat="server"  Text='<%# Bind("ORDER_NO") %>'  ></asp:Label>
        </td>

        <td style="width:250px;">
             <asp:Label ID="lblHD_productdesc" runat="server"  Text='<%# Bind("CHAIN_NAME") %>'  ></asp:Label>
        </td>

         <td style="width:100px;">
             <asp:Label ID="lblHD_city" runat="server"  Text='<%# Bind("CITY_NAME") %>' ></asp:Label>
        </td>

        <td style="width:150px;">
            <asp:Label ID="txtHD_roomtype" runat="server" Text='<%# Bind("ROOM_TYPE") %>' ></asp:Label>
          </td>

        <td style="width:100px;">
            <asp:TextBox ID="txtHD_qty" runat="server" Text='<%# Bind("QUANTITY") %>'   Width="50px"></asp:TextBox>
           <%--<asp:Label ID="lblHD_qty" runat="server"  Text='<%# Bind("QUANTITY") %>'  Width="100px"></asp:Label>--%>
        </td>

         

        <td style="width:150px;">
         <asp:TextBox ID="txtHD_fromdate" runat="server" Text='<%# Bind("FROM_DATE") %>'   onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Width="100px"></asp:TextBox>
             <%--<asp:Label ID="lblHD_fromdate" runat="server"  Text='<%# Bind("FROM_DATE") %>' Width="80px"></asp:Label>--%>
        </td>
         <td style="width:150px;">
          <asp:TextBox ID="txtHD_todate" runat="server" Text='<%# Bind("TO_DATE") %>'  onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);" Width="100px"></asp:TextBox>
             <%--<asp:Label ID="lblHD_todate" runat="server"  Text='<%# Bind("TO_DATE") %>' Width="100px"></asp:Label>--%>
        </td>
        <td>
             <asp:Label ID="lblHotelcart_id" runat="server"  Text='<%# Bind("HOTEL_CART_ID") %>'  Visible="false"></asp:Label>
        </td>
         <td>
             <asp:Label ID="lblhotelpricelistid" runat="server"  Text='<%# Bind("SUPPLIER_HOTEL_PRICE_LIST_ID") %>' Visible="false"></asp:Label>
        </td>
      
       
        <td style="width:100px;">
            <asp:Button ID="btnHD_update" runat="server" Text="UPDATE" CommandName="UpdateHD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("HOTEL_CART_ID"))%>' />
        </td>
        <td style="width:100px;">
            <asp:Button ID="btnHD_delete" runat="server" Text="DELETE" CommandName="DeleteHD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("HOTEL_CART_ID"))%>' />
        </td>
    </tr>
    
</table>

</ItemTemplate>

</asp:DataList>
</ContentTemplate>
</asp:UpdatePanel>
<%--</div>--%>
<br />
<br />

<%--Cruise Detail--%>
<asp:UpdatePanel ID="UpdatePanel_Cruise_Header" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:Label ID="Label24" runat="server" Text="Cruise" CssClass="headlabel"></asp:Label>
<table width="1000px" id="cruiseheader" runat="server">



<tr>
        <td style="width:100px;">
             <asp:Label ID="Label25" runat="server"  CssClass="sectionHeader" Text="Sr No" ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="Label26" runat="server" CssClass="sectionHeader" Text="Product Description"></asp:Label>
        </td>
         <td style="width:100px;">
             <asp:Label ID="Label27" runat="server" CssClass="sectionHeader" Text="City" ></asp:Label>
        </td>
         <td style="width:150px;">
             <asp:Label ID="Label31" runat="server" CssClass="sectionHeader" Text="Room Type" ></asp:Label>
        </td>
        <td style="width:100px;">
             <asp:Label ID="Label28" runat="server" CssClass="sectionHeader" Text="Qty"></asp:Label>
        </td>
        <td style="width:150px;">
             <asp:Label ID="Label29" runat="server" CssClass="sectionHeader" Text="From Date" ></asp:Label>
        </td>
         <td style="width:150px;">
             <asp:Label ID="Label30" runat="server" CssClass="sectionHeader" Text="To Date" ></asp:Label>
        </td>
         <%--<td width="10%">
             <asp:Label ID="Label31" runat="server" CssClass="sectionHeader" Text="From Time"></asp:Label>
        </td>
         <td width="10%">
             <asp:Label ID="Label32" runat="server" CssClass="sectionHeader" Text="To Time"></asp:Label>
        </td>--%>

    </tr>
    


</table>
</ContentTemplate>
</asp:UpdatePanel>


 <asp:UpdatePanel ID="UpdatePanel_Cruise_Data" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:DataList ID="dlcruisedetails" runat="server" OnItemCommand="dlcruisedetails_ItemCommand">


<ItemTemplate>

<table width="1215px" id="cruiseheader" runat="server">

    
    <tr>
    
        <td style="width:100px;">
           <asp:Label ID="lblCD_Srno" runat="server"  Text='<%# Bind("ORDER_NO") %>' ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="lblCD_productdesc" runat="server"  Text='<%# Bind("CHAIN_NAME") %>' ></asp:Label>
        </td>

         <td style="width:100px;">
             <asp:Label ID="lblCD_city" runat="server"  Text='<%# Bind("CITY_NAME") %>' ></asp:Label>
        </td>

        <td style="width:150px;">
             <asp:Label ID="Label32" runat="server"  Text='<%# Bind("ROOM_TYPE") %>' ></asp:Label>
        </td>

        <td style="width:100px;">
        <asp:TextBox ID="txtCD_qty" runat="server" Text='<%# Bind("QUANTITY") %>' Width="50px"></asp:TextBox>
          <%-- <asp:Label ID="lblCD_qty" runat="server"  Text='<%# Bind("QUANTITY") %>' Width="100px"></asp:Label>--%>
        </td>

        <td style="width:150px;">
        <asp:TextBox ID="txtCD_fromdate" runat="server" Text='<%# Bind("FROM_DATE") %>'  Width="100px" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
            <%-- <asp:Label ID="lblCD_fromdate" runat="server"  Text='<%# Bind("FROM_DATE") %>' Width="100px"></asp:Label>--%>
        </td>

         <td style="width:150px;">
        <asp:TextBox ID="txtCD_todate" runat="server" Text='<%# Bind("TO_DATE") %>'  Width="100px" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
            <%-- <asp:Label ID="lblCD_todate" runat="server"  Text='<%# Bind("TO_DATE") %>' Width="100px"></asp:Label>--%>
        </td>
        <td>
             <asp:Label ID="lblCruisecart_id" runat="server"  Text='<%# Bind("CRUISE_CART_ID") %>' Width="100px" Visible="false"></asp:Label>
        </td>
         <%--<td>
             <asp:Label ID="lbltotime" runat="server"  Text='<%# Bind("CHAIN_NAME") %>' Width="100px"></asp:Label>
        </td>--%>
        <td style="width:100px;">
            <asp:Button ID="btnCD_update" runat="server" Text="UPDATE" CommandName="UpdateCD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("CRUISE_CART_ID"))%>' />
        </td>
        <td style="width:100px;">
            <asp:Button ID="btnCD_delete" runat="server" Text="DELETE" CommandName="DeleteCD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("CRUISE_CART_ID"))%>' />
        </td>
    </tr>

</table>

</ItemTemplate>

</asp:DataList>
</ContentTemplate>
</asp:UpdatePanel>

<br />
<br />

<asp:UpdatePanel ID="UpdatePanel_Sight_Header" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<%--Sight Seeing Detail--%>
<asp:Label ID="Label33" runat="server" Text="Sight Seeing Detail" CssClass="headlabel"></asp:Label>
<table width="650px" id="sightheader" runat="server">



<tr>
        <td style="width:100px;">
             <asp:Label ID="Label34" runat="server"  CssClass="sectionHeader" Text="Sr No" ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="Label35" runat="server" CssClass="sectionHeader" Text="Product Description"></asp:Label>
        </td>
        <%-- <td width="10%">
             <asp:Label ID="Label36" runat="server" CssClass="sectionHeader" Text="City" ></asp:Label>
        </td>
        <td width="10%">
             <asp:Label ID="Label37" runat="server" CssClass="sectionHeader" Text="Qty"></asp:Label>
        </td>--%>
        <td style="width:150px;">
             <asp:Label ID="Label38" runat="server" CssClass="sectionHeader" Text="Date" Width="100px"></asp:Label>
        </td>
         <td style="width:150px;">
             <asp:Label ID="Label39" runat="server" CssClass="sectionHeader" Text="Time" Width="100px"></asp:Label>
        </td>
         <%--<td width="10%">
             <asp:Label ID="Label40" runat="server" CssClass="sectionHeader" Text="From Time"></asp:Label>
        </td>
         <td width="10%">
             <asp:Label ID="Label41" runat="server" CssClass="sectionHeader" Text="To Time"></asp:Label>
        </td>--%>

    </tr>
    


</table>
</ContentTemplate>
</asp:UpdatePanel>


 <asp:UpdatePanel ID="UpdatePanel_Sight_Data" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:DataList ID="dlsightseeing" runat="server" OnItemCommand="dlsightseeing_ItemCommand">


<ItemTemplate>

<table width="850px">

    
    <tr>
    
        <td style="width:100px;">
           <asp:Label ID="lblSD_Srno" runat="server"  Text='<%# Bind("ORDER_NO") %>'  ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="lblSD_productdesc" runat="server"  Text='<%# Bind("SITE_NAME") %>' ></asp:Label>
        </td>
        <%-- <td>
             <asp:Label ID="lblcity" runat="server"  Text='<%# Bind("CITY_NAME") %>' Width="100px"></asp:Label>
        </td>
        <td>
           <asp:Label ID="lblqty" runat="server"  Text='<%# Bind("QUANTITY") %>' Width="100px"></asp:Label>
        </td>--%>
        <td style="width:150px;">
           <asp:TextBox ID="txtSD_date" runat="server" Text='<%# Bind("DATE") %>'  Width="100px" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
             <%--<asp:Label ID="lblSD_date" runat="server"  Text='<%# Bind("DATE") %>' Width="100px"></asp:Label>--%>
        </td>
         <td style="width:150px;">
             <asp:TextBox ID="txtSD_time" runat="server" Text='<%# Bind("TIME") %>'  Width="100px"></asp:TextBox>
           <%--  <asp:Label ID="lblSD_time" runat="server"  Text='<%# Bind("TIME") %>' Width="100px"></asp:Label>--%>
        </td>
        <td>
             <asp:Label ID="lblSightcart_id" runat="server"  Text='<%# Bind("SERVICE_CART_ID") %>' Width="100px" Visible="false"></asp:Label>
        </td>
        <%-- <td>
             <asp:Label ID="lbltotime" runat="server"  Text='<%# Bind("CHAIN_NAME") %>' Width="100px"></asp:Label>
        </td>--%>
        <td style="width:100px;">
            <asp:Button ID="btnSD_update" runat="server" Text="UPDATE" CommandName="UpdateSD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("SERVICE_CART_ID"))%>' />
        </td>
        <td style="width:100px;">
            <asp:Button ID="btnSD_delete" runat="server" Text="DELETE" CommandName="DeleteSD_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("SERVICE_CART_ID"))%>' />
        </td>
    </tr>

</table>

</ItemTemplate>

</asp:DataList>
</ContentTemplate>
</asp:UpdatePanel>

<br />
<br />

<%--Transfer Package--%>
<asp:UpdatePanel ID="UpdatePanel_Transfer_Header" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:Label ID="Label42" runat="server" Text="Transfer Package" CssClass="headlabel"></asp:Label>
<table width="650px" id="transferheader" runat="server">



<tr>
        <td style="width:100px;">
             <asp:Label ID="Label43" runat="server"  CssClass="sectionHeader" Text="Sr No" ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="Label44" runat="server" CssClass="sectionHeader" Text="Product Description"></asp:Label>
        </td>
        <%-- <td width="10%">
             <asp:Label ID="Label45" runat="server" CssClass="sectionHeader" Text="City" ></asp:Label>
        </td>
        <td width="10%">
             <asp:Label ID="Label46" runat="server" CssClass="sectionHeader" Text="Qty"></asp:Label>
        </td>--%>
        <td style="width:150px;">
             <asp:Label ID="Label47" runat="server" CssClass="sectionHeader" Text="Date" ></asp:Label>
        </td>
         <td style="width:150px;">
             <asp:Label ID="Label48" runat="server" CssClass="sectionHeader" Text="Time" ></asp:Label>
        </td>
         <%--<td width="10%">
             <asp:Label ID="Label49" runat="server" CssClass="sectionHeader" Text="From Time"></asp:Label>
        </td>
         <td width="10%">
             <asp:Label ID="Label50" runat="server" CssClass="sectionHeader" Text="To Time"></asp:Label>
        </td>--%>

    </tr>
    


</table>
</ContentTemplate>
</asp:UpdatePanel>


 <asp:UpdatePanel ID="UpdatePanel_Transfer_Data" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
<asp:DataList ID="dltransferpackage" runat="server" OnItemCommand="dltransferpackage_ItemCommand">


<ItemTemplate>

<table width="850px">

    
    <tr>
    
        <td style="width:100px;">
           <asp:Label ID="lblTP_Srno" runat="server"  Text='<%# Bind("ORDER_NO") %>'  ></asp:Label>
        </td>
        <td style="width:250px;">
             <asp:Label ID="lblTP_productdesc" runat="server"  Text='<%# Bind("NAME") %>' ></asp:Label>
        </td>
        <%-- <td>
             <asp:Label ID="lblcity" runat="server"  Text='<%# Bind("CITY_NAME") %>' Width="100px"></asp:Label>
        </td>
        <td>
           <asp:Label ID="lblqty" runat="server"  Text='<%# Bind("QUANTITY") %>' Width="100px"></asp:Label>
        </td>--%>
        <td style="width:150px;">
         <asp:TextBox ID="txtTP_date" runat="server" Text='<%# Bind("DATE") %>'  Width="100px" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox>
            <%-- <asp:Label ID="lblTP_date" runat="server"  Text='<%# Bind("DATE") %>' Width="100px"></asp:Label>--%>
        </td>
         <td style="width:150px;">
          <asp:TextBox ID="txtTP_time" runat="server" Text='<%# Bind("TIME") %>'  Width="100px"></asp:TextBox>
             <%--<asp:Label ID="lblTP_time" runat="server"  Text='<%# Bind("TIME") %>' Width="100px"></asp:Label>--%>
        </td>
        <td>
             <asp:Label ID="lblTransfercart_id" runat="server"  Text='<%# Bind("SERVICE_CART_ID") %>' Width="100px" Visible="false"></asp:Label>
        </td>
         <%--<td>
             <asp:Label ID="lbltotime" runat="server"  Text='<%# Bind("CHAIN_NAME") %>' Width="100px"></asp:Label>
        </td>--%>
        <td style="width:100px;">
            <asp:Button ID="btnTP_update" runat="server" Text="UPDATE" CommandName="UpdateTP_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("SERVICE_CART_ID"))%>' />
        </td>
        <td style="width:100px;">
            <asp:Button ID="btnTP_delete" runat="server" Text="DELETE" CommandName="DeleteTP_Record" CommandArgument='<%#string.Format("{0}|{1}",Eval("CART_ORDER_ID"),Eval("SERVICE_CART_ID"))%>' />
        </td>
    </tr>

</table>

</ItemTemplate>

</asp:DataList>
</ContentTemplate>
</asp:UpdatePanel>

<br />
<br />

<asp:Label ID="headinglabel" runat="server" Text ="Tour Details" CssClass="headlabel"></asp:Label>
<asp:UpdatePanel ID="UpdatePanel_TourDetails" runat="server" UpdateMode="Conditional" >
 <%--<Triggers>
            <asp:AsyncPostBackTrigger controlid="btnHD_update" eventname="ItemCommand" />
        </Triggers>--%>
                    <ContentTemplate>
<table width="100%">

    <tr>
    
        <td width="50%">
          <%--  <asp:UpdatePanel ID="UpdatePanel_TourDetails_1" runat="server" UpdateMode="Conditional">
            <Triggers>
            <asp:AsyncPostBackTrigger controlid="btnHD_update" eventname="ItemCommand" />
        </Triggers>
                    <ContentTemplate>--%>
            <table width="100%">
            

                <tr>
                
                    <td width="30%">
                        <asp:Label ID="Label9" runat="server" Text="Client Name" CssClass="fieldlabel"></asp:Label></td>
                    <td width="70%">
                        <asp:TextBox ID="txtClientname" runat="server" Width="200px"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Tour Name" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTourname" runat="server" Width="200px"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="No Adult" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNo_Adult" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox ></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="No CWB" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNo_CWB" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox ></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="No CNB" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNo_CNB" runat="server" CssClass="textboxstyle" Width="200px" ></asp:TextBox ></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="No Infant" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNo_Infant" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox ></td>
                </tr>
            
                 <tr>
                
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="No. Of Rooms Single" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNoroomSingle" runat="server" CssClass="textboxstyle" Width="200px" ></asp:TextBox ></td>
                </tr>

                 <tr>
                
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="No. Of Rooms Double / Twin" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNoroomDouble" runat="server" CssClass="textboxstyle" Width="200px" ></asp:TextBox ></td>
                </tr>

                 <tr>
                
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="No of Nights" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNo_OfNights" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label36" runat="server" Text="Order Status" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <telerik:radcombobox runat="server" id="RadCmbClass" autopostback="True" enableloadondemand="True"
                                width="205px" height="150px" onitemsrequested="RadCmbclass_ItemsRequested" showmoreresultsbox="true"
                                enablevirtualscrolling="true"></telerik:radcombobox>
                    </td>
                </tr>

                 <%--<tr>
                
                    <td>
                        <asp:Label ID="Label36" runat="server" Text="Order Status" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <telerik:radcombobox runat="server" autopostback="True" id="radcmb_orderstatus"
                        enableloadondemand="True" showmoreresultsbox="true" enablevirtualscrolling="true"
                        onitemsrequested="RadCmbOrderstatus_ItemsRequested" 
                        onclientblur="Get_ExamField" ></telerik:radcombobox>--%>
                        <%--<asp:TextBox ID="txtorder_status" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>--%>

                <%--<tr>
                
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox ></td>
                </tr>--%>

                <%--<tr>
                
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="Booking Status" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtBooking_Status" runat="server" CssClass="textboxstyle"></asp:TextBox ></td>
                </tr>--%>

            </table>
          <%--  </ContentTemplate>
</asp:UpdatePanel>--%>
        </td>

        <%--td width="25%" valign="top">

            <table width="100%" >
            
                    <tr>
                
                    <td width="35%">
                        <asp:Label ID="Label11" runat="server" Text="No of Days" CssClass="fieldlabel"></asp:Label></td>
                    <td width="65%">
                        <asp:TextBox ID="txtNo_OfDays" runat="server" CssClass="textboxstyle"></asp:TextBox></td>
                </tr>

                 <tr>
                
                    <td width="35%">
                        <asp:Label ID="Label19" runat="server" Text="No of Nights" CssClass="fieldlabel"></asp:Label></td>
                    <td width="65%">
                        <asp:TextBox ID="txtNo_OfNights" runat="server" CssClass="textboxstyle"></asp:TextBox></td>
                </tr>
         
            </table>

        </td>--%>

        <td width="50%" valign="top">

       <%-- <asp:UpdatePanel ID="UpdatePanel_TourDetails_2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
            <table width="100%">
            
                <tr>
                
                    <td width="30%">
                        <asp:Label ID="Label12" runat="server" Text="From Date" CssClass="fieldlabel"></asp:Label></td>
                    <td width="70%">
                        <asp:TextBox ID="txtFrom_Date" runat="server" CssClass="textboxstyle" Width="200px" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox></td>
                </tr>

                  <tr>
                
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="To Date" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTo_Date" runat="server" CssClass="textboxstyle" Width="200px" onfocus="showPopup(this, event);" onkeydown="parseDate(this, event);"></asp:TextBox></td>
                </tr>

                  <tr>
                
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="From Time" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtFrom_Time" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>

                  <tr>
                
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="To Time" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTo_Time" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>

                 <tr>
                
                    <td width="35%">
                        <asp:Label ID="Label13" runat="server" Text="Arrival Flight" CssClass="fieldlabel"></asp:Label></td>
                    <td width="65%">
                        <asp:TextBox ID="txtArrival_Flight" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Departure Flight" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDeparture_Flight" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxstyle" Width="200px"></asp:TextBox ></td>
                </tr>
            
            </table>
           <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
        </td>

        <%--<td width="25%" valign="top">

            <table width="100%">
            
                <tr>
                
                    <td width="35%">
                        <asp:Label ID="Label13" runat="server" Text="Arrival Flight" CssClass="fieldlabel"></asp:Label></td>
                    <td width="65%">
                        <asp:TextBox ID="txtArrival_Flight" runat="server" CssClass="textboxstyle"></asp:TextBox></td>
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Departure Flight" CssClass="fieldlabel"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDeparture_Flight" runat="server" CssClass="textboxstyle"></asp:TextBox></td>
                </tr>

            </table>

        </td>--%>

    </tr>

</table>
</ContentTemplate>
</asp:UpdatePanel>

<br />
<br />

<table>

    <tr>
    
        <td>
            <asp:Button ID="btnGet_Quote" runat="server" Text="Get Quote" 
                CssClass="buttonstyle" onclick="btnGet_Quote_Click"/></td>
        <td><asp:Button ID="btnCheck_Out" runat="server" Text="Check Out" CssClass="buttonstyle"/></td>
        <td><asp:Button ID="btnContinue_Purchase" runat="server" Text="Continue Purchase" 
                CssClass="buttonstyle" onclick="btnContinue_Purchase_Click"/></td>
    </tr>

</table>

</asp:Content>
