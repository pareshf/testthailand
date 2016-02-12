<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="BookingCheckList.aspx.cs" Inherits="CRM.WebApp.Views.Sales.BookingCheckList" %>

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
            <script type="text/javascript" src="../Shared/Javascripts/TourCheckList.js"></script>
        <script type="text/javascript">
            var grid;
            var signature = '<%=Session["signature"]%>';
            function pageLoad() {
                TourCheckListTableView = $find("<%= radgridtourchecklist.ClientID %>").get_masterTableView();
                BookingTableView = $find("<%= radgridPassangerMaster.ClientID %>").get_masterTableView();

                var q = window.location.search.substring(1);
                if (q != "") {
                    booking_id = getValue("BOOKING_ID");
                    TourCheckListCommand = "Load";
                    BookingCommand = "Load";
                    CRM.WebApp.webservice.TourBookingCheckList.GetBookingCheckList(BookingTableView.get_currentPageIndex() * BookingTableView.get_pageSize(), BookingTableView.get_pageSize(), BookingTableView.get_sortExpressions().toString(), BookingTableView.get_filterExpressions().toDynamicLinq(), booking_id, updatebookingchecklist);

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

            function UpdateCheckListDetail(sender, args) {
                var currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SR_NO;
                ary[1] = TourCheckListTableView.get_dataItems()[currentRowIndex - 1].findElement("CHECKED").value;

                try {

                    CRM.WebApp.webservice.TourBookingCheckList.InsertUpdateChecklist(ary);
                    CRM.WebApp.webservice.TourBookingCheckList.GetCheckListName(booking_id, updateTourCheckList);
                    // alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }

            function GridCreated() {
                grid = this;
            }


            function Save() {
                var ary = [];
                var flag = 1;


                for (var i = 0; i < TourCheckListTableView._dataSource.length; i++) {
                    //ary[0] = TourCheckListTableView._dataSource[i].SR_NO;
                    ary[1] = TourCheckListTableView._dataSource[i].CHECKED;
                    ary[0] = TourCheckListTableView.get_dataItems()[i].findElement("SR_NO").value;
                    try {
                        CRM.WebApp.webservice.TourBookingCheckList.InsertUpdateChecklist(ary);
                    }
                    catch (e) {
                        flag = 0;
                    }
                }
                if (flag == 1) {
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.TourBookingCheckList.GetCheckListName(booking_id, updateTourCheckList);
                }
                else {
                    alert('Wrong Data Inserted');
                }
            }

            function getTourName(sender) {
                var value = sender.value;
                CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
            }

            function getTourId(sender) {
                var tourcode = sender.value;
                $.get("../../webservice/autocomplete.ashx/ProcessRequest", { q: tourcode, key: "FETCH_TOUR_CODE?" + globalvalue }, function (data) { TOUR_ID = data; });
            }

            function getTourPassenger() {
                CRM.WebApp.webservice.TourBookingCheckList.GetBookingCheckListOnTourId(BookingTableView.get_currentPageIndex() * BookingTableView.get_pageSize(), BookingTableView.get_pageSize(), BookingTableView.get_sortExpressions().toString(), BookingTableView.get_filterExpressions().toDynamicLinq(), TOUR_ID, updatebookingchecklist);
            }

            function SaveAgreement() {
                try {
                    CRM.WebApp.webservice.TourBookingCheckList.UpdateAgreement(booking_id);
                    alert('Booking Process Complete Successfully');
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function Check() {

                var signature1 = document.getElementById('ctl00_cphPageContent_txtsignaturepword');
                if (signature1.value == signature) {


                    $('#ctl00_cphPageContent_btnAccept').show();
                    $('#ctl00_cphPageContent_txtsignaturepword').val('');
                }
                else {

                    alert("Enter Correct Signature Password.")
                }
            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Booking Check List"></asp:Literal>
    </div>
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            $("#ctl00_cphPageContent_txttourshortname").autocomplete(tourshortname);
            $("#ctl00_cphPageContent_txttourcodename").autocomplete(tourcode);

            var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";   //set query for dropdown....multiple entry
            var tourshortname = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_SHORT_SHORT_NAME_AUTOSEARCH";
            var tourcode = "../../webservice/autocomplete.ashx?key=FETCH_TOUR_CODE_ON_TOUR_SHORT_NAME?" + globalvalue;
            $("#ctl00_cphPageContent_txttourShortName").autocomplete(tourshortname);  //set id for dropdown ....multiple entry
            $("#ctl00_cphPageContent_txtTourCode").autocomplete(tourcode);  //set id for dropdown ....multiple entry
            for (var i = 1; i < 55; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;
                $("#ctl00_cphPageContent_radgridtourchecklist_ctl00_ctl" + i + "_CHECKED").autocomplete(a);  //set id for dropdown ....multiple entry


            }
        });       
    </script>
    <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTourShortName" runat="server" Text="Tour Short Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttourShortName" runat="server" Width="250px" onblur="getTourName(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTourCode" runat="server" Text="Tour Code"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTourCode" runat="server" Width="250px" onblur="getTourId(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOk" runat="server" Text="OK" OnClientClick="getTourPassenger();" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Booking List"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridPassangerMaster" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="25" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
                        <MasterTableView  AllowMultiColumSorting="true" EditMode ="InPlace" Width="450px">
                            <RowIndicatorColumn>
                            </RowIndicatorColumn>
                            <Columns>
                            <telerik:GridTemplateColumn SortExpression ="TOUR_ID" DataField="TOUR_ID" HeaderText="TOUR_ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TOUR_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                    </ItemTemplate>  
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn SortExpression ="BOOKING_ID" DataField="BOOKING_ID" HeaderText="BOOKING_ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="BOOKING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                                    </ItemTemplate>  
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                                    </ItemTemplate>  
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn SortExpression ="CUST_NAME" DataField="CUST_NAME" HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                    </ItemTemplate>  
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridTemplateColumn SortExpression ="CUST_SURNAME" DataField="CUST_SURNAME" HeaderText="Surname">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUST_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                                    </ItemTemplate>  
                            </telerik:GridTemplateColumn>

                            
                            </Columns>
                        </MasterTableView>
                         <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridPassangerMaster_Command" OnRowSelected="radgridPassangerMaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
                    </telerik:radgrid>
            </td>
        </tr>
    </table>
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Check List Detail"></asp:Literal>
    </div>
    <br />
    <br />
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridtourchecklist" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="30" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView AllowMultiColumSorting="true" EditMode ="InPlace" Width="450px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>
                   
                    <telerik:GridTemplateColumn SortExpression ="SR_NO" DataField="SR_NO" HeaderText="SR_NO" Visible="false" UniqueName="SR_NO">
                          <ItemTemplate>
                            <asp:TextBox ID="SR_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    
                     <telerik:GridTemplateColumn SortExpression ="CHECKLIST_DESCRIPTION" DataField="CHECKLIST_DESCRIPTION" HeaderText="Description" UniqueName="CHECKLIST_DESCRIPTION">
                          <ItemTemplate>
                            <asp:TextBox ID="CHECKLIST_DESCRIPTION" runat="server" CssClass="radinput" readOnly="true"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="Select" DataField="CHECKED" UniqueName="CHECKED">
                          <ItemTemplate>
                              <asp:TextBox ID="CHECKED" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="UpdateCheckListDetail(this,event);">
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
                <ClientEvents OnCommand="radgridtourchecklist_Command" OnRowSelected="radgridtourchecklist_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblsignature" runat="server" Text="Signature Password" Width="120px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtsignaturepword" runat="server" TextMode="Password" Width="90px"></asp:TextBox>
                </td>
             <td>
                <asp:Button ID="btnsingature" runat="server" Text="I Sign and Agree on This CheckList" OnClientClick="Check();" />
            </td>
            
                
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAccept" runat="server" Text="Complete Booking Process" OnClientClick="SaveAgreement();"
                        Style="display: none" />
                </td>
            </tr>
            <%--<br />
            <tr>
              <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnNext" runat="server" Text="Go To Next" />
                </td>
            </tr>--%>
        </table>
    </div>
</asp:Content>
