<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="MarketingCustomerNew.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.MarketingCustomerNew" %>

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
    
        <script type="text/javascript" src="../Shared/Javascripts/MarketingCustomerGridScript.js"></script>
        <script type="text/javascript">
            var targetlist_id;
            function pageLoad() {
               
                targetlist_id = getValue("TARGETLIST_ID");
                MarketingCustomerTableView = $find("<%= radgridMarketingCustomer.ClientID %>").get_masterTableView();
                MarketingCustomerCommand = "Load";

                var q = window.location.search.substring(1);
                if (q != "") {

                    CRM.WebApp.webservice.MarketingCustomerWebService.GetData(targetlist_id, updateMarketingCustomer);
                     

                }
                else {

                    CRM.WebApp.webservice.MarketingCustomerWebService.GetMarketingCustomer(MarketingCustomerTableView.get_currentPageIndex() * MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_sortExpressions().toString(), MarketingCustomerTableView.get_filterExpressions().toDynamicLinq(), fname, lname, city, state, country, mobile, phone, cmode, ccompany, updateMarketingCustomer);
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
            function delMarCust() {
                var table = $find("<%= radgridMarketingCustomer.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridMarketingCustomer.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridMarketingCustomer.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.MarketingCustomerWebService.delMarketingCustomer(MAR_DATA_ID);
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function addnewMarketingCustomer(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[0] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
                ary[2] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("MAR_DATA_NAME").value;
                ary[3] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("MAR_DATA_SURNAME").value;
                ary[4] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE_1").value;
                ary[5] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE_2").value;
                ary[6] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("MOBILE_NO").value;
                ary[7] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE_NO").value;
                ary[8] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[9] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[10] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[11] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("COMMUNICATION_MODE_NAME").value;
                ary[12] = MarketingCustomerTableView.get_dataItems()[currentRowIndex - 1].findElement("CUST_COMPANY_NAME").value;

                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.MAR_DATA_ID;
                for (i = 0; i < 13; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.MarketingCustomerWebService.InsertUpdateMarketingCustomer(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.MarketingCustomerWebService.GetMarketingCustomer(MarketingCustomerTableView.get_currentPageIndex() * MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_sortExpressions().toString(), MarketingCustomerTableView.get_filterExpressions().toDynamicLinq(), fname, lname, city, state, country, mobile, phone, cmode, ccompany, updateMarketingCustomer);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function showpnl() {
                document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

                document.getElementById('<%=Button2.ClientID %>').style.display = "";
                document.getElementById('<%=Button1.ClientID %>').style.display = "none";
            }
            function SearchResult() {

                document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
                document.getElementById('<%=Button1.ClientID %>').style.display = "";
                document.getElementById('<%=Button2.ClientID %>').style.display = "none";
                fname = $("#ctl00_cphPageContent_txtfname").val();
                lname = $("#ctl00_cphPageContent_txtlname").val();
                city = $("#ctl00_cphPageContent_txtcity").val();
                state = $("#ctl00_cphPageContent_txtstate").val();
                country = $("#ctl00_cphPageContent_txtcountry").val();
                mobile = $("#ctl00_cphPageContent_txtMobile").val();
                phone = $("#ctl00_cphPageContent_txttelephon").val();
                cmode = $("#ctl00_cphPageContent_txtcommunicatinmode").val();
                ccompany = $("#ctl00_cphPageContent_txtcustcompany").val();
                
                CRM.WebApp.webservice.MarketingCustomerWebService.GetMarketingCustomer(MarketingCustomerTableView.get_currentPageIndex() * MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_pageSize(), MarketingCustomerTableView.get_sortExpressions().toString(), MarketingCustomerTableView.get_filterExpressions().toDynamicLinq(),fname,lname,city,state,country,mobile,phone,cmode,ccompany, updateMarketingCustomer);
            }
        </script>
   </telerik:radcodeblock>
   <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblpagename" runat="server" Text="Marketing Customer Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {


            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var cmode = "../../webservice/autocomplete.ashx?key=FETCH_COMMUNICATION_MODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var custcompany = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CUST_COMPANY_NAME_AUTOSEARCH";

            $("#ctl00_cphPageContent_txtcity").autocomplete(city);
            $("#ctl00_cphPageContent_txtstate").autocomplete(state);
            $("#ctl00_cphPageContent_txtcountry").autocomplete(country);
            $("#ctl00_cphPageContent_txtcommunicatinmode").autocomplete(cmode);
            $("#ctl00_cphPageContent_txtcustcompany").autocomplete(custcompany);
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_COMMUNICATION_MODE_NAME").autocomplete(cmode);
                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_CUST_COMPANY_NAME").autocomplete(custcompany);
                $("#ctl00_cphPageContent_radgridMarketingCustomer_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(title);
            }

        });       
    </script>
        <table>
            <tr>
                <td>
                    <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblfname" runat="server" Text="First Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbllaname" runat="server" Text="Last Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtlname" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcity" runat="server" Text="City:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcity" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblstate" runat="server" Text="State:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtstate" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcountry" runat="server" Text="Country:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcountry" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblmobile" runat="server" Text="Mobile No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobile" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblphone" runat="server" Text="Telephone No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txttelephon" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="lblcmode" runat="server" Text="Communication Mode:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcommunicatinmode" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcustcompany" runat="server" Text="Customer Company:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcustcompany" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClientClick="showpnl();"
                        Style="float: right; margin-right: 10px; display: block; color: black;"
                        CssClass="button" />
                    <asp:Button ID="Button2" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                        display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
                </td>
                <td>
                <asp:Button ID="btndelete" CssClass="button" Style="float: right; margin-right: 10px;
                    color: black;" OnClientClick="if(!confirm('Are you sure you want to delete this Marketing Customer?'))return false; delMarCust(); return false;"
                    Text="Delete" runat="server" Visible="true" />
                </td>
             </tr>
       </table>
    <table>
        <tr>
            <td>
                <telerik:radgrid id="radgridMarketingCustomer" runat="server" allowpaging="true" allowmultirowselection="false"
                    allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                    allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="MAR_DATA_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1800px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="MAR_DATA_ID" DataField="MAR_DATA_ID" HeaderText="MAR_DATA_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="MAR_DATA_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TARGETLIST_ID" DataField="TARGETLIST_ID" HeaderText="TARGETLIST_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="TARGETLIST_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                    <HeaderStyle HorizontalAlign="Left" Width="60px"/>
                          <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MAR_DATA_NAME" DataField="MAR_DATA_NAME" HeaderText="Name">
                          <ItemTemplate>
                            <asp:TextBox ID="MAR_DATA_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MAR_DATA_SURNAME" DataField="MAR_DATA_SURNAME" HeaderText="Surname">
                          <ItemTemplate>
                            <asp:TextBox ID="MAR_DATA_SURNAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE_1" DataField="ADDRESS_LINE_1" HeaderText="Address Line1">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE_1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE_2" DataField="ADDRESS_LINE_2" HeaderText="Address Line 2">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE_2" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MOBILE_NO" DataField="MOBILE_NO" HeaderText="Mobile">
                          <ItemTemplate>
                            <asp:TextBox ID="MOBILE_NO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHONE_NO" DataField="PHONE_NO" HeaderText="Phone">
                          <ItemTemplate>
                            <asp:TextBox ID="PHONE_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="▼City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="▼State">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="▼Country">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COMMUNICATION_MODE_NAME" DataField="COMMUNICATION_MODE_NAME" HeaderText="▼Communication Mode">
                          <ItemTemplate>
                            <asp:TextBox ID="COMMUNICATION_MODE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CUST_COMPANY_NAME" DataField="CUST_COMPANY_NAME" HeaderText="▼Customer Company">
                          <ItemTemplate>
                            <asp:TextBox ID="CUST_COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewMarketingCustomer(this,event);">
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
                <ClientEvents OnCommand="radgridMarketingCustomer_Command" OnRowSelected="radgridMarketingCustomer_RowSelected" OnRowDblClick="MarketingCustomerRowClick"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
            </td>
        </tr>
</table>
</asp:Content>