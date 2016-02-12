<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FitPackageMaster.aspx.cs" Inherits="CRM.WebApp.Views.FIT.FitPackageMaster" %>

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
            <script type="text/javascript" src="../Shared/Javascripts/FitePackageGridScript.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                FitPackageTableView = $find("<%= radgridfitpackage.ClientID %>").get_masterTableView();
                FitMappingTableView = $find("<%= radgridfitcitymapping.ClientID %>").get_masterTableView();
                FitPackageCommandName = "Load";
               // CRM.WebApp.webservice.FitPackageWebService.GetFit(FitPackageTableView.get_currentPageIndex() * FitPackageTableView.get_pageSize(), FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);


                if (FitPackageTableView.PageSize = 10) {
                    CRM.WebApp.webservice.FitPackageWebService.GetFit(0, FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);
                }
                else if (FitPackageTableView.PageSize > 10) {
                    CRM.WebApp.webservice.FitPackageWebService.GetFit(0, FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);
                }
                else if (FitPackageTableView.PageSize > 20) {
                    CRM.WebApp.webservice.FitPackageWebService.GetFit(0, FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);
                }

            }
            function deleteCurrent() {
                CRM.WebApp.webservice.FitPackageWebService.delFitPackage(FIT_PACKAGE_ID);
                CRM.WebApp.webservice.FitPackageWebService.GetFit(FitPackageTableView.get_currentPageIndex() * FitPackageTableView.get_pageSize(), FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);

            }
            function deleteCity() {

                CRM.WebApp.webservice.FitPackageWebService.delCities(FIT_PACKAGE_CITY_ID);
                CRM.WebApp.webservice.FitPackageWebService.GetCity(FIT_PACKAGE_ID, updateFitCityPackage);
            }
            function addnewFit(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[1] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("FIT_PACKAGE_NAME").value;
                ary[2] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("PACKAGE_ORDER").value;
                ary[3] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("IS_VISIBLE").value;
               // ary[4] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("PACKAGE_MARGIN").value;
                ary[5] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("SURCHARGE").value;
                ary[6] = FitPackageTableView.get_dataItems()[currentRowIndex - 1].findElement("MINIMUM_NIGHTS").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FIT_PACKAGE_ID;
                for (i = 0; i < 7; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.FitPackageWebService.InsertUpdateFitPackage(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.FitPackageWebService.GetFit(0, FitPackageTableView.get_pageSize(), FitPackageTableView.get_sortExpressions().toString(), FitPackageTableView.get_filterExpressions().toDynamicLinq(), updateFitPackage);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function addnewPackageCity(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = FitMappingTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.FIT_PACKAGE_CITY_ID;
                for (i = 0; i < 2; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.FitPackageWebService.InsertUpdateFitPackageCityMapping(ary);
                    CRM.WebApp.webservice.FitPackageWebService.GetCity(FIT_PACKAGE_ID, updateFitCityPackage)
                    alert('Record Save Successfully');
                    

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function AddNewCity() {

                CRM.WebApp.webservice.FitPackageWebService.InsertNewCity(FIT_PACKAGE_ID);
                CRM.WebApp.webservice.FitPackageWebService.GetCity(FIT_PACKAGE_ID, updateFitCityPackage)
            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="FIT Package Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

                        var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
                        var a = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";

                        for (i = 0; i < 55; i++) {
                            if (i < 10)
                                i = '0' + i;

                            $("#ctl00_cphPageContent_radgridfitcitymapping_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                            $("#ctl00_cphPageContent_radgridfitpackage_ctl00_ctl" + i + "_IS_VISIBLE").autocomplete(a);
                        }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Fit Package?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
         </div>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridfitpackage" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="FIT_PACKAGE_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="800px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_ID" DataField="FIT_PACKAGE_ID" HeaderText="FIT_PACKAGE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_NAME" DataField="FIT_PACKAGE_NAME" HeaderText="Fit Package">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="MINIMUM_NIGHTS" DataField="MINIMUM_NIGHTS" HeaderText="Minimum Nights">
                          <ItemTemplate>
                            <asp:TextBox ID="MINIMUM_NIGHTS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PACKAGE_ORDER" DataField="PACKAGE_ORDER" HeaderText="Package Order">
                          <ItemTemplate>
                            <asp:TextBox ID="PACKAGE_ORDER" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PACKAGE_MARGIN" DataField="PACKAGE_MARGIN" HeaderText="Package Margin" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PACKAGE_MARGIN" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SURCHARGE" DataField="SURCHARGE" HeaderText="SurCharge">
                          <ItemTemplate>
                            <asp:TextBox ID="SURCHARGE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="IS_VISIBLE" DataField="IS_VISIBLE" HeaderText="Is Visible?">
                          <ItemTemplate>
                            <asp:TextBox ID="IS_VISIBLE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewFit(this,event);">
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
                <ClientEvents OnCommand="radgridfitpackage_Command" OnRowSelected="radgridfitpackage_RowSelected" OnRowDblClick="addFitPackage"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal1" runat="server" Text="Cities"></asp:Literal>
    </div>
    <br />
    <br />
    <table>
            <tr>
                <td>
                        <asp:Button ID="Button1" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this City?'))return false; deleteCity(); return false;"
                            Text="Delete" runat="server"/>
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridfitcitymapping" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="FIT_PACKAGE_CITY_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_ID" DataField="FIT_PACKAGE_ID" HeaderText="FIT_PACKAGE_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="FIT_PACKAGE_CITY_ID" DataField="FIT_PACKAGE_CITY_ID" HeaderText="FIT_PACKAGE_CITY_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="FIT_PACKAGE_CITY_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewPackageCity(this,event);">
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
                <ClientEvents OnCommand="radgridfitcitymapping_Command" OnRowSelected="radgridfitcitymapping_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="lbAddContact" runat="server" Text="Add New City"
                                OnClientClick="AddNewCity();"></asp:LinkButton>
                </td>
            </tr>
        </table>
</asp:Content>
