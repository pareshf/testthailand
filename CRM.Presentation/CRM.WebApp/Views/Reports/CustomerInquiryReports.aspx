<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="CustomerInquiryReports.aspx.cs" Inherits="CRM.WebApp.Views.Reports.CustomerInquiryReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
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
        .style1
        {
            width: 129px;
        }
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">

        <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
        <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
        <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
        
        <script type="text/javascript">

            function pageLoad() {

                customermasterTableView = $find("<%= radgridcustomermaster.ClientID %>").get_masterTableView();
                customermasterCommandName = "Load";

                //document.getElementById('hdncustunqid').val() = CUST_UNQ_ID;



            }
            function SearchCustomer() {

                scomapany = $("#ctl00_cphPageContent_txtcompany").val();
                scity = $("#ctl00_cphPageContent_txtCity").val();
                scode = $("#ctl00_cphPageContent_txtCode").val();
                stype = $("#ctl00_cphPageContent_txttype").val();
                sbranch = $("#ctl00_cphPageContent_txtBranch").val();
                semp = $("#ctl00_cphPageContent_txtEmployee").val();
                scommode = $("#ctl00_cphPageContent_txtcommod").val();
                srelation = $("#ctl00_cphPageContent_txtrelation").val();
                suniquetid = $("#ctl00_cphPageContent_txtCustunqid").val();
                sfname = $("#ctl00_cphPageContent_txtfname").val();
                slname = $("#ctl00_cphPageContent_txtlname").val();
                semail = $("#ctl00_cphPageContent_txtEmail").val();
                smob = $("#ctl00_cphPageContent_txtMobile").val();
                stele = $("#ctl00_cphPageContent_txtTelephone").val();

                CRM.WebApp.webservice.CustomerInquiriesReport.GetCustomer(customermasterTableView.get_currentPageIndex() * customermasterTableView.get_pageSize(), customermasterTableView.get_pageSize(), customermasterTableView.get_sortExpressions().toString(), customermasterTableView.get_filterExpressions().toDynamicLinq(), scomapany, scity, scode, stype, sbranch, semp, scommode, srelation, suniquetid, sfname, slname, semail, smob, stele, updateCustGrid);

            }

            function ViewReport() {
                window.open('CustomersAllInquiiresReport.aspx?key=' + CUST_UNQ_ID);   
            }
                    
        </script>

    </telerik:radcodeblock>
    <script type="text/javascript" src="../Shared/Javascripts/CustomerInquiryReport.js"></script>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <%--Dopdown Queryies--%>
    <script type="text/javascript">

        $(document).ready(function () {

            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var title = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var profession = "../../webservice/autocomplete.ashx?key=FETCH_PROFESSION_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var codename = "../../webservice/autocomplete.ashx?key=FETCH_CODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var modename = "../../webservice/autocomplete.ashx?key=FETCH_COMMUNICATION_MODE_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var Designation = "../../webservice/autocomplete.ashx?key=FETCH_DESIGNATION_FOR_FAR_HOTEL_MASTER";
            var addresstype = "../../webservice/autocomplete.ashx?key=FETCH_ADDRESS_TYPE_FOR_EMPLOYEE_MASTER";
            var Customertype = "../../webservice/autocomplete.ashx?key=FETCH_CUSTOMER_TYPE_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var Branch = "../../webservice/autocomplete.ashx?key=FETCH_BRANCH_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var meal = "../../webservice/autocomplete.ashx?key=FETCH_MEAL_DATA_FOR_BOOKINGMASTER_AUTOSEARCH";
            var nationality = "../../webservice/autocomplete.ashx?key=FETCH_NATIONALITY_FOR_BOOKING_MASTER";
            var relation = "../../webservice/autocomplete.ashx?key=FETCH_RELATION_NAME_FOR_CUSTOMER_MASTER_AUTOSEARCH";
            var comapnyname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_CUST_COMPANY_NAME_AUTOSEARCH";
            var gender = "../../webservice/autocomplete.ashx?key=FETCH_GENDER_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var marital = "../../webservice/autocomplete.ashx?key=FETCH_MATERIAL_FOR_EMPLOYEE_MASTER";
            var employee = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var religion = "../../webservice/autocomplete.ashx?key=FETCH_RELIGION_NAME_FOR_CUST_CUSTOMER_MASTER";
            var groupname = "../../webservice/autocomplete.ashx?key=FETCH_GROUPE_NAME_FOR_CUST_CUSTOMER_MASTER";

            for (var i = 1; i < 50; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_txtcompany").autocomplete(comapnyname);
                $("#ctl00_cphPageContent_txtCity").autocomplete(city);
                $("#ctl00_cphPageContent_txtCode").autocomplete(codename);
                $("#ctl00_cphPageContent_txttype").autocomplete(Customertype);
                $("#ctl00_cphPageContent_txtBranch").autocomplete(Branch);
                $("#ctl00_cphPageContent_txtEmployee").autocomplete(employee);
                $("#ctl00_cphPageContent_txtcommod").autocomplete(modename);
                $("#ctl00_cphPageContent_txtrelation").autocomplete(relation);

            }

        });


    </script>
    <%--Designing Mode--%>
    <div>
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hdncustunqid" runat="server" Visible="true" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal6" runat="server" Text="Customer Inquiry Report"></asp:Literal>
            <br />
        </div>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Customer Unique Id"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCustunqid" runat="server" TabIndex="1" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Relation:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtrelation" runat="server" TabIndex="2" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblfname" runat="server" Text="First Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfname" runat="server" TabIndex="3" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbllaname" runat="server" Text="Last Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtlname" runat="server" TabIndex="4" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Company:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcompany" runat="server" TabIndex="5" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblmobile" runat="server" Text="Mobile No.:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMobile" runat="server" TabIndex="6" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblemail" runat="server" Text="Email:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="7" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbltele" runat="server" Text="Telephone No.:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelephone" runat="server" TabIndex="8" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcode" runat="server" Text="Code :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" TabIndex="9" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBranch" runat="server" Text="Branch :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBranch" runat="server" TabIndex="10" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmployee" runat="server" Text="Employee :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployee" runat="server" TabIndex="11" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblcommod" runat="server" Text="Communication Mode :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcommod" runat="server" TabIndex="12" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" TabIndex="13" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Type:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttype" runat="server" TabIndex="14" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="Button2" runat="server" Text="Search" OnClientClick="SearchCustomer();" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divradmastergrid">
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridcustomermaster" runat="server" allowpaging="true" allowmultirowselection="true"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True" width="3000">
                <MasterTableView ClientDataKeyNames="CUST_ID" AllowMultiColumnSorting="true" EditMode="InPlace">
                    <Columns>

                 <telerik:GridTemplateColumn SortExpression="CUST_UNQ_ID" DataField="CUST_UNQ_ID" HeaderText="Customer Unique Id" Visible="true" >
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                        <ItemTemplate>
                            <asp:TextBox ID="TITLE_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_SURNAME" DataField="CUST_SURNAME" HeaderText="Customer Surname">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_SURNAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>                    
                    <telerik:GridTemplateColumn SortExpression="CUST_NAME" DataField="CUST_NAME" HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_MOBILE" DataField="CUST_REL_MOBILE" HeaderText="Mobile No">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_MOBILE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                                   
                    <telerik:GridTemplateColumn SortExpression="CUST_REL_PHONE" DataField="CUST_REL_PHONE" HeaderText="Telephone No.">
                        <ItemTemplate>
                            <asp:TextBox ID="CUST_REL_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="BRANCH" DataField="BRANCH" HeaderText="Branch">
                        <ItemTemplate>
                            <asp:TextBox ID="BRANCH" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMPLOYEE" DataField="EMPLOYEE" HeaderText="Employee">
                        <ItemTemplate>
                            <asp:TextBox ID="EMPLOYEE" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    
                    <telerik:GridTemplateColumn SortExpression="RELATION_DESC" DataField="RELATION_DESC" HeaderText="Relation With Cust">
                        <ItemTemplate>
                            <asp:TextBox ID="RELATION_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
                    <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true" AllowColumnsReorder="True">
                        <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                        <ClientEvents OnCommand="radgridcustomermaster_Command" OnRowSelected="radgridcustomermaster_RowSelected"/>
                        <Selecting AllowRowSelect="true"/>
                    </ClientSettings>
                    </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="View Report" OnClientClick="ViewReport();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
