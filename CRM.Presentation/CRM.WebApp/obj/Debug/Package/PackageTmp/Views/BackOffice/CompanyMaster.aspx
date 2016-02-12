<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.CompanyMaster" %>


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

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/CompanyMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                CompanyMasterTableView = $find("<%= radgridCompanyMaster.ClientID %>").get_masterTableView();
                CompanyMasterCommand = "Load";
                CRM.WebApp.webservice.CompanyWebService.GetCompanyName(CompanyMasterTableView.get_currentPageIndex() * CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_sortExpressions().toString(), CompanyMasterTableView.get_filterExpressions().toDynamicLinq(), updateCompany);

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
            function deleteCoach() {
                
                CRM.WebApp.webservice.CompanyWebService.deleteCompany(COMPANY_ID);
                CRM.WebApp.webservice.CompanyWebService.GetCompanyName(CompanyMasterTableView.get_currentPageIndex() * CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_sortExpressions().toString(), CompanyMasterTableView.get_filterExpressions().toDynamicLinq(), updateCompany);
               
            }
            function addnewCompanyName(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                
                ary[1] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
                ary[2] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE1").value;
                ary[3] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE2").value;
                ary[4] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[5] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[6] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[7] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PINCODE").value;
                ary[8] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("MOBILE").value;
                ary[9] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;
                ary[10] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FAX").value;
                ary[11] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("EMAIL").value;
                ary[12] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_COMPANY_FRANCHISE").value;
                ary[13] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("PARENT_NAME").value;
                ary[14] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;

                ary[15] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("ENABLE_AUTO_BAKUP").value;
                ary[16] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_SYMBOL").value;
                ary[17] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("FINANCIAL_YEAR_FROM").value;
                ary[18] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BOOK_BEGINING_FROM").value;
                ary[19] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("SECURITY_PASSWORD").value;
                ary[20] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BASE_CURRENCY").value;
                ary[21] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("BASE_CURRENCY_SYMBOL").value;
                ary[22] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_SYMBOL_SUFFIXED_TO_AMOUNT").value;
                ary[23] = CompanyMasterTableView.get_dataItems()[currentRowIndex - 1].findElement("SYMBOL_FOR_DECIMAL_PORTION").value;
                


                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.COMPANY_ID;
                
                if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                try {
                    CRM.WebApp.webservice.CompanyWebService.InsertUpdateCompany(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.CompanyWebService.GetCompanyName(CompanyMasterTableView.get_currentPageIndex() * CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_pageSize(), CompanyMasterTableView.get_sortExpressions().toString(), CompanyMasterTableView.get_filterExpressions().toDynamicLinq(), updateCompany);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPagename" runat="server" Text="Company Master"></asp:Literal>
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
            var company_branch = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_BRACH_FOR_COMPANY_MASTER";
            var parent_Company = "../../webservice/autocomplete.ashx?key=GET_BUSINESS_COMPANY_NAME_FOR_AUTOSEARCH";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_NAME";
            var yes_no = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";

            
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_IS_COMPANY_FRANCHISE").autocomplete(company_branch);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_PARENT_NAME").autocomplete(parent_Company);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_BASE_CURRENCY").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_ENABLE_AUTO_BAKUP").autocomplete(yes_no);
                $("#ctl00_cphPageContent_radgridCompanyMaster_ctl00_ctl" + i + "_IS_SYMBOL_SUFFIXED_TO_AMOUNT").autocomplete(yes_no);
                
            }        

        });       
        </script>
        <div id = "radmastergrid">
        <div id="Div1">
        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
        </div>
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Company Detail?'))return false; deleteCoach(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCompanyMaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="COMPANY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="2900px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="COMPANY_ID" DataField="COMPANY_ID" HeaderText="COMPANY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_COMPANY_FRANCHISE" DataField="IS_COMPANY_FRANCHISE" HeaderText="TYPE">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_COMPANY_FRANCHISE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Company Name">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE1" DataField="ADDRESS_LINE1" HeaderText="Address Line 1">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE1" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ADDRESS_LINE2" DataField="ADDRESS_LINE2" HeaderText="Address Line 2">
                          <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE2" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Statutary Compliance For">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PINCODE" DataField="PINCODE" HeaderText="Pincode">
                          <ItemTemplate>
                            <asp:TextBox ID="PINCODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MOBILE" DataField="MOBILE" HeaderText="Mobile">
                          <ItemTemplate>
                            <asp:TextBox ID="MOBILE" runat="server" CssClass="radinput" ></asp:TextBox>
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

                    <telerik:GridTemplateColumn SortExpression ="EMAIL" DataField="EMAIL" HeaderText="Email">
                          <ItemTemplate>
                            <asp:TextBox ID="EMAIL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="PARENT_NAME" DataField="PARENT_NAME" HeaderText="Parent Company">
                          <ItemTemplate>
                            <asp:TextBox ID="PARENT_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>


                     <telerik:GridTemplateColumn SortExpression ="ENABLE_AUTO_BAKUP" DataField="ENABLE_AUTO_BAKUP" HeaderText="Enable Auto Backup">
                          <ItemTemplate>
                            <asp:TextBox ID="ENABLE_AUTO_BAKUP" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY_SYMBOL" DataField="CURRENCY_SYMBOL" HeaderText="Currency Symbol">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_SYMBOL" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FINANCIAL_YEAR_FROM" DataField="FINANCIAL_YEAR_FROM" HeaderText="Financial Year From">
                          <ItemTemplate>
                            <asp:TextBox ID="FINANCIAL_YEAR_FROM" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BOOK_BEGINING_FROM" DataField="BOOK_BEGINING_FROM" HeaderText="Book Begining From">
                          <ItemTemplate>
                            <asp:TextBox ID="BOOK_BEGINING_FROM" runat="server" CssClass="radinput" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SECURITY_PASSWORD" DataField="SECURITY_PASSWORD" HeaderText="Security Password">
                          <ItemTemplate>
                            <asp:TextBox ID="SECURITY_PASSWORD" runat="server" CssClass="radinput" TextMode="Password"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BASE_CURRENCY" DataField="BASE_CURRENCY" HeaderText="Base Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="BASE_CURRENCY" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BASE_CURRENCY_SYMBOL" DataField="BASE_CURRENCY_SYMBOL" HeaderText="Base Currency Symbol">
                          <ItemTemplate>
                            <asp:TextBox ID="BASE_CURRENCY_SYMBOL" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_SYMBOL_SUFFIXED_TO_AMOUNT" DataField="IS_SYMBOL_SUFFIXED_TO_AMOUNT" HeaderText="Is Symbol Suffixed To Amount">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_SYMBOL_SUFFIXED_TO_AMOUNT" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SYMBOL_FOR_DECIMAL_PORTION" DataField="SYMBOL_FOR_DECIMAL_PORTION" HeaderText="Is Symbol For Decimal Portion">
                          <ItemTemplate>
                            <asp:TextBox ID="SYMBOL_FOR_DECIMAL_PORTION" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewCompanyName(this,event);">
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
                <ClientEvents OnCommand="radgridCompanyMaster_Command" OnRowSelected="radgridCompanyMaster_RowSelected" OnRowDblClick="addCompany"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
