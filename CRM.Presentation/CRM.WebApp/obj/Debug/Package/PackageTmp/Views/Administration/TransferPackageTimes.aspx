<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TransferPackageTimes.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TransferPackageTimes" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<script src="../../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

          <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
              
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10" Width="800px">
                                <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="FROM_PLACE" HeaderText="Transfer Package Name" />
                                    <asp:BoundField DataField="TRANSFER_PACKAGE_PRICE_ID" HeaderText="Transfer Package Id." />
                                    
                                    <%--<asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No" />--%>
                                  <%--  <asp:BoundField DataField="VOUCHER_STATUS" HeaderText="Voucher Status" />
                                    <asp:BoundField DataField="VOUCHER_TYPE" HeaderText="Voucher Type" />
                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                                    <asp:BoundField DataField="DATE" HeaderText="Date" />--%>
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
