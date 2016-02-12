<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SpecialOffer.aspx.cs" Inherits="CRM.WebApp.Views.Settings.SpecialOffer" %>



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
            <script type="text/javascript" src="../Shared/Javascripts/SpecialOffer.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                OfferTableView = $find("<%= radgridSpecialOffer.ClientID %>").get_masterTableView();
                OfferCommandName = "Load";


                if (OfferTableView.PageSize = 10) {
                    CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);
                }
                else if (OfferTableView.PageSize > 10) {
                    CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);
                }
                else if (OfferTableView.PageSize > 20) {
                    CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);
                }

            }
            function deleteCurrent() {

                CRM.WebApp.webservice.SpecialOfferWebService.delSpecialOffer(OFFER_ID);
                CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);
            }
            function addOffer(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

                ary[1] = OfferTableView.get_dataItems()[currentRowIndex - 1].findElement("PACKAGE_NAME").value;
                ary[2] = OfferTableView.get_dataItems()[currentRowIndex - 1].findElement("CURRENCY").value;
                ary[3] = OfferTableView.get_dataItems()[currentRowIndex - 1].findElement("PRICE").value;
                ary[4] = OfferTableView.get_dataItems()[currentRowIndex - 1].findElement("DISPLAY_ON_DASHBOARD").value;

                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.OFFER_ID;

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

                try {
                    CRM.WebApp.webservice.SpecialOfferWebService.InsertUpdateSpecialOffer(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Special Offer"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var yes_no = "../../webservice/autocomplete.ashx?key=FETCH_YES_NO";
            var currency = "../../webservice/autocomplete.ashx?key=FETCH_CURRENCY_NAME";

            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridSpecialOffer_ctl00_ctl" + i + "_CURRENCY").autocomplete(currency);
                $("#ctl00_cphPageContent_radgridSpecialOffer_ctl00_ctl" + i + "_DISPLAY_ON_DASHBOARD").autocomplete(yes_no);
            }

        });       
        </script>
        <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Offer?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridSpecialOffer" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="50" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="OFFER_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="600px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="OFFER_ID" DataField="OFFER_ID" HeaderText="OFFER_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="OFFER_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="PACKAGE_NAME" DataField="PACKAGE_NAME" HeaderText="Package">
                          <ItemTemplate>
                            <asp:TextBox ID="PACKAGE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CURRENCY" DataField="CURRENCY" HeaderText="Currency">
                          <ItemTemplate>
                            <asp:TextBox ID="CURRENCY" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PRICE" DataField="PRICE" HeaderText="Price">
                          <ItemTemplate>
                            <asp:TextBox ID="PRICE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DISPLAY_ON_DASHBOARD" DataField="DISPLAY_ON_DASHBOARD" HeaderText="Display On Dashboard">
                          <ItemTemplate>
                            <asp:TextBox ID="DISPLAY_ON_DASHBOARD" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addOffer(this,event);">
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
                <ClientEvents OnCommand="radgridSpecialOffer_Command" OnRowSelected="radgridSpecialOffer_RowSelected" OnRowDblClick="addSpecialOffer"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>