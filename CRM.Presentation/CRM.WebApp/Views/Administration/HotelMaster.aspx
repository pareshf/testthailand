<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.HotelMaster" %>

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
    <script type ="text/javascript" src="../Shared/Javascripts/HotelMaster.js"></script>
    <script type ="text/javascript">


        function pageLoad() {


            HotelTableView = $find("<%= radgridhotelmaster.ClientID %>").get_masterTableView();
            HotelContactDetailView = $find("<%= radgridHotelDetail.ClientID %>").get_masterTableView();
            HotelCurrencyPriceMasterView = $find("<%= radgridHotelCurrencyPriceMasterView.ClientID %>").get_masterTableView();
            HotelphotoView = $find("<%= radgridHotalphotomaster.ClientID %>").get_masterTableView();
            HotelComandName = "Load";

            if (HotelTableView.PageSize == 10) {
                CRM.WebApp.webservice.HotelMaster.getHotelDetail(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
            }

            else if (HotelTableView.PageSize > 10) {
                CRM.WebApp.webservice.HotelMaster.getHotelDetail(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
            }

            else if (HotelTableView.PageSize > 20) {
                CRM.WebApp.webservice.HotelMaster.getHotelDetail(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
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

        function deleteCurrent() {

            var table = $find("<%=radgridhotelmaster.ClientID%>").get_masterTableView().get_element();
            var row = table.rows[currentRowIndex];
            table.deleteRow(currentRowIndex);
            var dataItem = $find(row.id)

            if (dataItem) {

                dataItem.dispose();
                Array.remove($find("<%= radgridhotelmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
            }

            var gridItems = $find("<%= radgridhotelmaster.ClientID %>").get_masterTableView().get_dataItems();
            CRM.WebApp.webservice.HotelMaster.Deletehotel(HOTEL_ID);
            gridItems[gridItems.length - 1].set_selected(true);

        }

        function rowadded(sender, args) {

           
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];

            ary[1] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("HOTEL_NAME").value;
            ary[2] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("HOTEL_RATING").value;
            ary[3] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE1").value;
            ary[4] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE2").value;
            ary[5] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
            ary[6] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
            ary[7] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
            ary[8] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("PINCODE").value;
            ary[9] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("EMAIL").value;
            ary[10] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;
            ary[11] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("FAX").value;
            ary[12] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("HOTEL_WEBSITE").value;
            ary[13] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("GMT").value;
            ary[14] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_IN_TIME").value;
            ary[15] = HotelTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECK_OUT_TIME").value;





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
            if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
            if (ary[15] == "" || ary[15] == 'null') ary[15] = 0;

            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.HOTEL_ID;

            try {
                CRM.WebApp.webservice.HotelMaster.InsertUpdateHotel(ary);

                CRM.WebApp.webservice.HotelMaster.getHotelDetail(HotelTableView.get_currentPageIndex() * HotelTableView.get_pageSize(), HotelTableView.get_pageSize(), HotelTableView.get_sortExpressions().toString(), HotelTableView.get_filterExpressions().toDynamicLinq(), updateHotelGrid);
                var masterTable = $find("<%= radgridhotelmaster.ClientID %>").get_masterTableView();
                //masterTable.rebind();
                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        function contactrowadded(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
           
            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.HOTEL_ID;
            ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CONTACT_SRNO;
            ary[2] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
            ary[3] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("NAME").value;
            ary[4] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("SURNAME").value;
            ary[5] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("DESIGNATION_DESC").value;
            ary[6] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("EMAIL").value;
            ary[7] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("MOBILE").value;
            ary[8] = HotelContactDetailView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;


            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
            if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
            if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
            if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
            if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
            if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
            if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
            if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
            if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;



            try {

                CRM.WebApp.webservice.HotelMaster.InsertUpdateHotelContactDetail(ary);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.HotelMaster.GetHotelContectDetailByHotel_ID(HOTEL_ID, updateHotelContactDetailGrid);

                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        function CurrencyPricerowadded(sender, args) {

           
            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var ary = [];
            ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.HOTEL_ID;
            ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CURRENCY_PRICE_ID;
            ary[2] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex - 1].findElement("ROOM_TYPE_NAME").value;
            ary[3] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
            ary[4] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex - 1].findElement("AMOUNT").value;
            ary[5] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex - 1].findElement("TAX").value;
            ary[6] = HotelCurrencyPriceMasterView.get_dataItems()[currentRowIndex - 1].findElement("GST").value;

            if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;



            try {

                CRM.WebApp.webservice.HotelMaster.InsertUpdateHotelCurrencyPriceMaster(ary);
                CRM.WebApp.webservice.HotelMaster.GetHotelCurrencyPriceMasterByHotel_ID(HOTEL_ID, updateHotelCurrencyPriceMasterGrid);

                alert('Record Save Successfully');

            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }

        //        function PopUpShowing(sender, args) {

        //            var divmore = document.getElementById('divmore');
        //            divmore.style.display = 'block';
        //            divmore.style.position = 'Absolute';
        //            divmore.style.left = screen.width / 2 - 150;
        //            divmore.style.top = screen.height / 2 - 150;
        //            var IMG = document.getElementById("imgexistingimage");
        //            IMG.src = args.srcElement.all[1].value;
        //        }

        //        function disablepopup() {

        //            var divmore = document.getElementById('divmore');
        //            divmore.style.display = 'none';
        //            return false;
        //        }
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
        function AddNewRoom() {

            CRM.WebApp.webservice.HotelMaster.InsertNewRoomType(HOTEL_ID);
        }

        function servicetypeadded(sender, args) {
            var a = [];
            a[0] = document.getElementById('<%=txtservicetype.ClientID%>').value;
            a[1] = HOTEL_ID;

            if (a[0] == "" || a[0] == 'null') a[0] = 0;
            if (a[1] == "" || a[1] == 'null') a[1] = 0;
            try {
                CRM.WebApp.webservice.HotelMaster.InsertUpdateSrviceType(a);
                alert('Record Save Successfully');
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }

        }
        function newHotelPhotoadded(sender, args) {

            currentRowIndex = sender.parentNode.parentNode.rowIndex;
            var a = [];

            a[0] = HotelphotoView.get_dataItems()[currentRowIndex - 1].findElement("PHOTO_TITLE").value;
            a[1] = HotelphotoView.get_dataItems()[currentRowIndex - 1].findElement("PHOTO_DESC").value;
            a[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PHOTO_SRNO;
            a[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.HOTEL_ID;
            for (i = 0; i < 1; i++) {
                if (a[i] == "" || a[i] == 'null') a[i] = 0;
            }
            try {
                CRM.WebApp.webservice.HotelMaster.insertupdatePhotoDetail(a);
                alert('Record Save Successfully');
                CRM.WebApp.webservice.HotelMaster.GetHotelPhotoDetails(HOTEL_ID, updateHotelphotoGrid);
            }
            catch (e) {
                alert('Wrong Data Inserted');
            }
        }
        function openuploadnewphoto() {
            window.open('HotelPhoto.aspx?key=' + PHOTO_SRNO + '&key1=' + HOTEL_ID);
        }
        function AddNewHotelPhoto() {
            CRM.WebApp.webservice.HotelMaster.insertnewPhoto(HOTEL_ID);
        }
        
    </script>
 
 </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageHotel" runat="server" Text="Hotel Master"></asp:Literal>
    </div>
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            //alert("in ready function");
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var Designation = "../../webservice/autocomplete.ashx?key=FETCH_DESIGNATION_FOR_FAR_HOTEL_MASTER";
            var RoomType = "../../webservice/autocomplete.ashx?key=FETCH_ROOM_TYPE_FOR_HOTEL_AUTOSEARCH";
            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";

            for (var i = 1; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridhotelmaster_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridhotelmaster_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridhotelmaster_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridHotelDetail_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
                $("#ctl00_cphPageContent_radgridHotelDetail_ctl00_ctl" + i + "_DESIGNATION_DESC").autocomplete(Designation);
                $("#ctl00_cphPageContent_radgridHotelCurrencyPriceMasterView_ctl00_ctl" + i + "_ROOM_TYPE_NAME").autocomplete(RoomType);
                $("#ctl00_cphPageContent_radgridHotelCurrencyPriceMasterView_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);


            }

        });
    </script>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
    </telerik:raddatepicker>
    <div id="radHotelmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Task?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:radgrid id="radgridhotelmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        width="2500" allowautomaticdeletes="True" allowautomaticinserts="True" >
               <MasterTableView ClientDataKeyNames="HOTEL_ID" AllowMultiColumSorting="true" EditMode ="InPlace" PagerStyle Mode="NextPrevAndNumeric">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               
               <Columns>
                
                    <%-- template column for raw data editing result --%>
                    <telerik:GridTemplateColumn SortExpression ="HOTEL_ID" DataField="HOTEL_ID" HeaderText="Hotel Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="HOTEL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                       
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="HOTEL_NAME" DataField="HOTEL_NAME" HeaderText="Hotel Name">
                        <ItemTemplate>
                            <asp:TextBox ID="HOTEL_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="HOTEL_RATING" DataField="HOTEL_RATING" HeaderText="Hotel Rating">
                        <ItemTemplate>
                            <asp:TextBox ID="HOTEL_RATING" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
               
                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE1" DataField="ADDRESS_LINE1" HeaderText="Address Line1">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE2" DataField="ADDRESS_LINE2" HeaderText="Address Line2">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="State Name">
                        <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country Name">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PINCODE" DataField="PINCODE" HeaderText="Pincode" >
                        <ItemTemplate>
                            <asp:TextBox ID="PINCODE" runat="server" CssClass="radinput"  >
                            </asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="EMAIL" DataField="EMAIL" HeaderText="Email" >
                        <ItemTemplate>
                            <asp:TextBox ID="EMAIL" runat="server" CssClass="radinput" >
                            </asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHONE" DataField="PHONE" HeaderText="Phone">
                        <ItemTemplate>
                            <asp:TextBox ID="PHONE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FAX" DataField="FAX" HeaderText="Fax">
                        <ItemTemplate>
                            <asp:TextBox ID="FAX" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>

                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="HOTEL_WEBSITE" DataField="HOTEL_WEBSITE" HeaderText="Hotel Website">
                        <ItemTemplate>
                            <asp:TextBox ID="HOTEL_WEBSITE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>


                    <telerik:GridTemplateColumn SortExpression ="GMT" DataField="GMT" HeaderText="GMT">
                        <ItemTemplate>
                            <asp:TextBox ID="GMT" runat="server" CssClass="radinput" onclick="showPopup(this,event);" onfocus="showPopup(this,event);" onkeydown="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CHECK_IN_TIME" DataField="CHECK_IN_TIME" HeaderText="Check In Time">
                        <ItemTemplate>
                            <asp:TextBox ID="CHECK_IN_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CHECK_OUT_TIME" DataField="CHECK_OUT_TIME" HeaderText="Check Out Time">
                        <ItemTemplate>
                            <asp:TextBox ID="CHECK_OUT_TIME" runat="server" CssClass="radinput" ></asp:TextBox>
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
                <ClientEvents OnCommand="radgridhotelmaster_Command" OnRowSelected="radgridhotelmaster_RowSelected" OnRowDblClick="HotelRowClick"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal1" runat="server" Text="Hotel Contact Detail"></asp:Literal>
            <br />
        </div>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridHotelDetail" allowpaging="false" runat="server" pagesize="10"
                        itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True">
            <MasterTableView ClientDataKeyNames="HOTEL_ID" AllowMultiColumnSorting="true" EditMode="InPlace" Width="1000px">
            <RowIndicatorColumn>
             </RowIndicatorColumn>
              <Columns>
                        <%--template column for raw data editing result--%>
                    <telerik:GridTemplateColumn DataField="HOTEL_ID" HeaderText="HOTEL_ID" Visible="false">
                        <ItemTemplate>
                              <asp:TextBox ID="HOTEL_ID" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="CONTACT_SRNO" DataField="CONTACT_SRNO" HeaderText="Contact Sr No" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CONTACT_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title Description">
                        <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    

                    <telerik:GridTemplateColumn DataField="NAME" HeaderText="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                   <%-- <telerik:GridTemplateColumn SortExpression ="SURNAME" DataField="SURNAME" HeaderText="Surname">
                        <ItemTemplate>
                            <asp:TextBox ID="SURNAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>


                    <telerik:GridTemplateColumn DataField="SURNAME" HeaderText="Surname">
                        <ItemTemplate>
                            <asp:TextBox ID="SURNAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    
                    <telerik:GridTemplateColumn SortExpression ="DESIGNATION_DESC" DataField="DESIGNATION_DESC" HeaderText="Designation">
                        <ItemTemplate>
                            <asp:TextBox ID="DESIGNATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="EMAIL" DataField="EMAIL" HeaderText="Email">
                        <ItemTemplate>
                            <asp:TextBox ID="EMAIL" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="MOBILE" DataField="MOBILE" HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:TextBox ID="MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="PHONE" DataField="PHONE" HeaderText="Phone">
                        <ItemTemplate>
                            <asp:TextBox ID="PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="contactrowadded(this,event);">
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
                <ClientEvents OnCommand="radgridHotelDetail_Command" OnRowDblClick="HotelDetailRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
         
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal2" runat="server" Text="Hotel Currency Price Master"></asp:Literal>
            <br />
        </div>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridHotelCurrencyPriceMasterView" allowpaging="false" runat="server"
                        pagesize="10" itemstyle-wrap="false" enableembeddedskins="false" allowautomaticinserts="True">
            <MasterTableView ClientDataKeyNames="HOTEL_ID" AllowMultiColumnSorting="true" EditMode="InPlace" Width="1000px">
            <RowIndicatorColumn>
             </RowIndicatorColumn>
              <Columns>
                        <%--template column for raw data editing result--%>
                    <telerik:GridTemplateColumn DataField="HOTEL_ID" HeaderText="HOTEL_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="HOTEL_ID" runat="server" CssClass="radinput"></asp:TextBox>
                                                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_PRICE_ID" DataField="CURRENCY_PRICE_ID" HeaderText="Currency Price Id" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_PRICE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="ROOM_TYPE_NAME" DataField="ROOM_TYPE_NAME" HeaderText="Room Type Name">
                        <ItemTemplate>
                            <asp:TextBox ID="ROOM_TYPE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="CurrencyName">
                        <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="AMOUNT" DataField="AMOUNT" HeaderText="Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression ="TAX" DataField="TAX" HeaderText="TAX">
                        <ItemTemplate>
                            <asp:TextBox ID="TAX" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="GST" DataField="GST" HeaderText="GST">
                        <ItemTemplate>
                            <asp:TextBox ID="GST" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="CurrencyPricerowadded(this,event);">
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
                <ClientEvents OnCommand="radgridHotelCurrencyPriceMaster_Command" OnRowDblClick="HotelPriceRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
        </telerik:radgrid>
        <asp:LinkButton ID="lbAddRoom" runat="server" Text="Add Another" OnClientClick="AddNewRoom();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPhotodetail" runat="server" Text="Hotel Photo Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <br />
    <div>
        <%--<table>
            <tr>
                <td>
                    <asp:Button ID="DeleteSubProg" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this sitephoto?'))return false;deleteCurrentSitePhoto(); return false;"
                        Text="Delete Sub Program" runat="server" />
                </td>
            </tr>
        </table>--%>
         <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridHotalphotomaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="HOTEL_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>  

                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="HOTEL_ID" DataField="HOTEL_ID" HeaderText="HOTEL_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="HOTEL_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHOTO_SRNO" DataField="PHOTO_SRNO" HeaderText="PHOTO_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PHOTO_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHOTO_TITLE" DataField="PHOTO_TITLE" HeaderText="PHOTO TITLE">
                          <ItemTemplate>
                            <asp:TextBox ID="PHOTO_TITLE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHOTO_DESC" DataField="PHOTO_DESC" HeaderText="DESCRIPATION">
                          <ItemTemplate>
                            <asp:TextBox ID="PHOTO_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploadphoto" runat="server" Text="PHOTO" onClientclick="openuploadnewphoto()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newHotelPhotoadded(this,event);">
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
                <ClientEvents OnCommand="radgridHotalphotomaster_Command" OnRowSelected="radgridHotalphotomaster_RowSelected" OnRowDblClick="PhotoRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                    <asp:LinkButton ID="lbAddHotelphoto" runat="server" Text="Add Another HOTEL Photo"
                        OnClientClick="AddNewHotelPhoto();"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal5" runat="server" Text="Hotel Service Type"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtservicetype" runat="server" TextMode="MultiLine" Width="400px"
                        Height="70px">
                    </asp:TextBox>
                    <%--<eo:Editor runat="server">
            </eo:Editor>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="servicetypeadded(this,event);" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<%--OnRowSelected="radgridphotomaster_RowSelected"--%>
