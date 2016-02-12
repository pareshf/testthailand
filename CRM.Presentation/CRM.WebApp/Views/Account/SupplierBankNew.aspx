<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SupplierBankNew.aspx.cs" Inherits="CRM.WebApp.Views.Account.SupplierBankNew" %>

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
       <script type="text/javascript" src="../Shared/Javascripts/SupplierBank.js"></script>
       
       <script type="text/javascript">

       
           function pageLoad() {

               SupplierBankAccountTableView = $find("<%= radgridSupplierBankAccount.ClientID %>").get_masterTableView();
               SupplierBankAccountCommand = "Load";
               CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankAcount(0, SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_sortExpressions().toString(), SupplierBankAccountTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierBankdetail);


           }
           function addSupplierbankAccount(sender, args) {

               currentRowIndex = sender.parentNode.parentNode.rowIndex;
               var ary = [];
              
               ary[1] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
               ary[2] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("ACC_NO").value;
               ary[3] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("SUPP_ACC_NAME").value;
               ary[4] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_NAME").value;
               ary[5] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_BRANCH").value;
               ary[6] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("BANK_ADDRESS").value;
               ary[7] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("SWIFT_CODE").value;
               ary[8] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_BANK_NAME").value;
               ary[9] = SupplierBankAccountTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_BANK_BRANCH").value;
               ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SUPPLIER_BANK_ACCOUNT_ID;
               //                for (i = 0; i < 9; i++) {
               //                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
               //                }
               if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
               try {

                   CRM.WebApp.webservice.SupplierBankAccountWebService.InsertUpdateSupplierBankAccount(ary);
                   alert('Record Save Successfully');
                   CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankAcount(0, SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_sortExpressions().toString(), SupplierBankAccountTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierBankdetail);

               }
               catch (e) {
                   alert('Wrong Data Inserted');
               }

           }
           function getComapnyBankName(sender) {
               
               var value = sender.value;
               CRM.WebApp.webservice.AutoComplete.searchQueryResult(value);
           }
           function showpnl() {

               document.getElementById('<%=pnlMainHead.ClientID %>').style.display = "";

               document.getElementById('<%=Button4.ClientID %>').style.display = "";
               document.getElementById('<%=Button3.ClientID %>').style.display = "none";
           }
           function SearchResult() {

               document.getElementById('<%= pnlMainHead.ClientID %>').style.display = "none";
               document.getElementById('<%=Button3.ClientID %>').style.display = "";
               document.getElementById('<%=Button4.ClientID %>').style.display = "none";

               
               sfname = $("#ctl00_cphPageContent_txtfname").val();

               CRM.WebApp.webservice.SupplierBankAccountWebService.GetSupplierBankAcount(0, SupplierBankAccountTableView.get_pageSize(), SupplierBankAccountTableView.get_sortExpressions().toString(), SupplierBankAccountTableView.get_filterExpressions().toDynamicLinq(), sfname, updateSupplierBankdetail);
           }

       </script>
    </telerik:radcodeblock>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Supplier Bank Account"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var bank = "../../webservice/autocomplete.ashx?key=FETCH_BANKNAME_FOR_BOOKING_MASTER";
            var company_bank_branch = "../../webservice/autocomplete.ashx?key=FETCH_COMPANY_BRANCH?" + globalvalue;
            var supplier_company_name = "../../webservice/autocomplete.ashx?key=FETCH_SUPPLIER_COMPANY_NAME_FOR_ACCOUNT_DETAILS";


            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridSupplierBankAccount_ctl00_ctl" + i + "_BANK_NAME_SUPP").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridSupplierBankAccount_ctl00_ctl" + i + "_COMPANY_NAME").autocomplete(supplier_company_name);
                $("#ctl00_cphPageContent_radgridSupplierBankAccount_ctl00_ctl" + i + "_COMPANY_BANK_NAME").autocomplete(bank);
                $("#ctl00_cphPageContent_radgridSupplierBankAccount_ctl00_ctl" + i + "_COMPANY_BANK_BRANCH").autocomplete(company_bank_branch);

            }
            $("#ctl00_cphPageContent_txtfname").autocomplete(supplier_company_name);

        });       
    </script>
    <div id="radmastergrid">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Supplier Bank Account Detail?'))return false; deleteCurrent(); return false;"
                        Text="Delete" runat="server" Visible="false"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridSupplierBankAccount" runat="server" allowpaging="true"
                        allowmultirowselection="false" allowsorting="True" pagesize="10" itemstyle-wrap="false"
                        enableembeddedskins="false" allowautomaticdeletes="True" allowautomaticinserts="True">
                    <MasterTableView ClientDataKeyNames="SUPPLIER_BANK_ACCOUNT_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="1200px">
                    <RowIndicatorColumn>
                     </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SUPPLIER_BANK_ACCOUNT_ID" DataField="SUPPLIER_BANK_ACCOUNT_ID" HeaderText="SUPPLIER_BANK_ACCOUNT_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPPLIER_BANK_ACCOUNT_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="Supplier Company">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ACC_NO" DataField="ACC_NO" HeaderText="Account No.">
                          <ItemTemplate>
                            <asp:TextBox ID="ACC_NO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="SUPP_ACC_NAME" DataField="SUPP_ACC_NAME" HeaderText="Account Name">
                          <ItemTemplate>
                            <asp:TextBox ID="SUPP_ACC_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BANK_NAME_SUPP" DataField="BANK_NAME_SUPP" HeaderText="Bank">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_NAME_SUPP" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BANK_BRANCH" DataField="BANK_BRANCH" HeaderText="Supplier Bank Branch">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_BRANCH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="BANK_ADDRESS" DataField="BANK_ADDRESS" HeaderText="Bank Address">
                          <ItemTemplate>
                            <asp:TextBox ID="BANK_ADDRESS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SWIFT_CODE" DataField="SWIFT_CODE" HeaderText="Swift Code">
                          <ItemTemplate>
                            <asp:TextBox ID="SWIFT_CODE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COMPANY_BANK_NAME" DataField="COMPANY_BANK_NAME" HeaderText="Company Bank">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_BANK_NAME" runat="server" CssClass="radinput" onblur="getComapnyBankName(this);"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="COMPANY_BANK_BRANCH" DataField="COMPANY_BANK_BRANCH" HeaderText="Company Bank Branch">
                          <ItemTemplate>
                            <asp:TextBox ID="COMPANY_BANK_BRANCH" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addSupplierbankAccount(this,event);">
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
                <ClientEvents OnCommand="radgridSupplierBankAccount_Command" OnRowSelected="radgridSupplierBankAccount_RowSelected" OnRowDblClick="addSupplierBankDetail"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <table>
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Search" OnClientClick="showpnl();"
                    Style="float: right; margin-right: 10px; display: block; color: black;" CssClass="button" />
                <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                    display: none; color: black;" CssClass="button" OnClientClick="SearchResult();" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                        <tr>
                            
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Employee Detail:" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkrel" runat="server" onClick="check();" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfname" runat="server" Text="Company Name:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfname" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
