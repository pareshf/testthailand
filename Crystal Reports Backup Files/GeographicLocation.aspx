<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="GeographicLocation.aspx.cs" Inherits="CRM.WebApp.Views.Administration.GeographicLocation" %>

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

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/GeographicLocationGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                CountryTableView = $find("<%= radgridCountry.ClientID %>").get_masterTableView();
                StateTableView = $find("<%= radgridState.ClientID %>").get_masterTableView();
                CityTableView = $find("<%= radgridCity.ClientID %>").get_masterTableView();
                CountryCommandName = "Load";
                StateCommandName = "Load";
                CityCommandName = "Load";
                //CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);


                if (CountryTableView.PageSize = 10) {
                    CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);
                }
                else if (CountryTableView.PageSize > 10) {
                    CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);
                }
                else if (CountryTableView.PageSize > 20) {
                    CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);
                }

            }
            function deleteCurrent() {
                
                CRM.WebApp.webservice.GeographicLocationWebService.delCountryName(COUNTRY_ID);
                CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);
            }
            function deleteState() {

                CRM.WebApp.webservice.GeographicLocationWebService.deleteState(STATE_ID);
                CRM.WebApp.webservice.GeographicLocationWebService.GetStateName(COUNTRY_ID, updateStateName)
             

            }
            function deleteCity() {

                CRM.WebApp.webservice.GeographicLocationWebService.deleteCity(CITY_ID);
                CRM.WebApp.webservice.GeographicLocationWebService.GetCityName(STATE_ID, updateCityGrid)


            }
            function addnewcountry(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                ary[0] = CountryTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[2] = CountryTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_CODE").value;
                ary[3] = CountryTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_CURRENCY_SYMBOL").value;
                ary[4] = CountryTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY_NAME").value;
                ary[5] = CountryTableView.get_dataItems()[currentRowIndex - 1].findElement("CONTINENT_NAME").value;

                ary[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.COUNTRY_ID;
                for (i = 0; i < 7; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateCountryName(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.GeographicLocationWebService.GetCountryName(0, CountryTableView.get_pageSize(), CountryTableView.get_sortExpressions().toString(), CountryTableView.get_filterExpressions().toDynamicLinq(), updateCountryName);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewState(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var arr = [];
               
                arr[0] = StateTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                arr[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.STATE_ID;
                for (i = 0; i < 3; i++) {
                    if (arr[i] == "" || arr[i] == 'null') arr[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateStateName(arr);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.GeographicLocationWebService.GetStateName(COUNTRY_ID, updateStateName)

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewCity(sender,args) {
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var a = [];
               
                a[0] = CityTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                a[1] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.CITY_ID;
                for (i = 0; i < 3; i++) {
                    if (a[i] == "" || a[i] == 'null') a[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.GeographicLocationWebService.InsertUpdateCityName(a);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.GeographicLocationWebService.GetCityName(COUNTRY_ID, updateCityGrid)

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function AddNewState() {
                CRM.WebApp.webservice.GeographicLocationWebService.InsertNewState(COUNTRY_ID);
            }
            function AddNewCity() {

                CRM.WebApp.webservice.GeographicLocationWebService.InsertNewCity(COUNTRY_ID, STATE_ID);
            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Country Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            //var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var Currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_TYPE_FOR_ROOM_AUTOSEARCH";
            var Continent = "../../webservice/autocomplete.ashx?key=CONTINENT_NAME_FOR_COMMON_COUNTRY_FOR_AUTOSEARCH";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

               // $("#ctl00_cphPageContent_radgridCountry_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_radgridCountry_ctl00_ctl" + i + "_CURRENCY_NAME").autocomplete(Currency);
                $("#ctl00_cphPageContent_radgridCountry_ctl00_ctl" + i + "_CONTINENT_NAME").autocomplete(Continent);
                $("#ctl00_cphPageContent_radgridCountry_ctl00_ctl" + i + "_STATE_NAME").autocomplete(Continent);
                $("#ctl00_cphPageContent_radgridCountry_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Country?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCountry" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="COUNTRY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="800px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COUNTRY_CODE" DataField="COUNTRY_CODE" HeaderText="Country Code">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COUNTRY_CURRENCY_SYMBOL" DataField="COUNTRY_CURRENCY_SYMBOL" HeaderText="Currency Symbol">
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_CURRENCY_SYMBOL" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CURRENCY_NAME" DataField="CURRENCY_NAME" HeaderText="Currency Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CONTINENT_NAME" DataField="CONTINENT_NAME" HeaderText="Continent Name">
                          <ItemTemplate>
                            <asp:TextBox ID="CONTINENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewcountry(this,event);">
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
                <ClientEvents OnCommand="radgridCountry_Command" OnRowSelected="radgridCountry_RowSelected" OnRowDblClick="addCountries"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="State"></asp:Literal>
    </div>
    <br />
    <br />
     <table>
            <tr>
                <td>
                        <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete State?'))return false; deleteState(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
    <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridState" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="STATE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="STATE_ID" DataField="STATE_ID" HeaderText="STATE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY_ID" Visible="false" >
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewState(this,event);">
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
                <ClientEvents OnCommand="radgridState_Command" OnRowSelected="radgridState_RowSelected" OnRowDblClick="addMyStates"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
               <asp:LinkButton ID="lbAddstate" runat="server" Text="Add New State" OnClientClick="AddNewState();"></asp:LinkButton>
                </td>

                <td>
                
                    
                </td>
            </tr>
            
        </table>
        <br />
        <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="City"></asp:Literal>
    </div>
    <br />
    <br />
     <table>
            <tr>
                <td>
                        <asp:Button ID="Button2" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete City?'))return false; deleteCity(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
    <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridCity" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="CITY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="CITY_ID" DataField="CITY_ID" HeaderText="CITY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COUNTRY_ID" DataField="COUNTRY_ID" HeaderText="COUNTRY_ID" Visible="false" >
                          <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression ="STATE_ID" DataField="STATE_ID" HeaderText="STATE_ID" Visible="false" >
                          <ItemTemplate>
                            <asp:TextBox ID="STATE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewCity(this,event);">
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
                <ClientEvents OnCommand="radgridCity_Command" OnRowSelected="radgridCity_RowSelected" OnRowDblClick="addMyCity"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
               <asp:LinkButton ID="LinkButton1" runat="server" Text="Add New City" OnClientClick="AddNewCity();"></asp:LinkButton>
                </td>
            </tr>
        </table>

</asp:Content>
