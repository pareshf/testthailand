<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="PaymentTermMaster.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.PaymentTermMaster" %>

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
    </style>

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/PaymentTermsMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                PaymentTableView = $find("<%= radgridpaymenttermmaster.ClientID %>").get_masterTableView();
                PaymentCommandName = "Load";
                //CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);

                if (PaymentTableView.PageSize = 10) {
                    CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);
                }
                else if (PaymentTableView.PageSize > 10) {
                    CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);
                }
                else if (PaymentTableView.PageSize > 20) {
                    CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);
                }
            }
            function deleteCurrent() {

                CRM.WebApp.webservice.PaymentTermsMaster.deletePaymentTerms(PAYMENT_TERMS_ID);
                CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);

            }
            function addnewPaymentTerms(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;

                var ary = [];
                ary[1] = PaymentTableView.get_dataItems()[currentRowIndex - 1].findElement("PAYMENT_TERMS").value;
               
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.PAYMENT_TERMS_ID;
                for (i = 0; i < 2; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.PaymentTermsMaster.InsertUpdatePayment(ary);
                    CRM.WebApp.webservice.PaymentTermsMaster.GetPaymentterms(0, PaymentTableView.get_pageSize(), PaymentTableView.get_sortExpressions().toString(), PaymentTableView.get_filterExpressions().toDynamicLinq(), updatePaymentTypeName);

                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
        </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Payment Terms Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {



            // $("#ctl00_cphPageContent_radgridaddresstypemaster_ctl00_ctl03_ctl01_PageSizeComboBox_DropDown").val();



        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Paymentterms?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridpaymenttermmaster" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="PAYMENT_TERMS_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
                
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS_ID" DataField="PAYMENT_TERMS_ID" HeaderText="PAYMENT_TERMS_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PAYMENT_TERMS" DataField="PAYMENT_TERMS" HeaderText="Payment Terms">
                          <ItemTemplate>
                            <asp:TextBox ID="PAYMENT_TERMS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     
                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewPaymentTerms(this,event);">
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
                <ClientEvents OnCommand="radgridpaymenttermmaster_Command" OnRowSelected="radgridpaymenttermmaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
