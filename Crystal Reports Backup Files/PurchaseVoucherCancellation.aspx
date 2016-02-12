<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="PurchaseVoucherCancellation.aspx.cs" Inherits="CRM.WebApp.Views.Account.PurchaseVoucherCancellation" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<script language="javascript" type="text/javascript">

    var sessionTimeout = "<%= Session.Timeout %>";

    var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
    setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

     <div>
     <asp:Label ID="Label63" runat="server" Text="Purchase Cancellation" class="pageTitle"
                    Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
               
     </div>

      <br />
      <br />


     <div>
        <asp:UpdatePanel ID="uppanelInvoice" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblSelectInvoice" runat="server" Text="Select Invoice"></asp:Label>

                        </td>

                        <td>
                            <asp:DropDownList ID="drpInvoice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpInvoice_SelectedIndexChanged" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
            </asp:UpdatePanel>
     </div>
     <br />

     <div>
        <asp:UpdatePanel ID="update_PurcahseCancellation" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>

              <asp:GridView ID="GridInvoice" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="Silver"
                               >
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="200px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderPurchaseVoucherNo" runat="server" Text="Purchase Voucher No" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblPurchaseVoucherNo" runat="server" Text='<%# Bind("PURCHASE_INVOICE_NO") %>' CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderSupplierName" runat="server" Text="Supplier Name" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SUPPLIER_COMPANY_NAME") %>' CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderSupplierType" runat="server" Text="Supplier Name" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSupplierType" runat="server" Text='<%# Bind("SUPPLIER_TYPE_NAME") %>' CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderVoucherAmount" runat="server" Text="Voucher Amount[THB]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherAmount" runat="server" Text='<%# Bind("VOUCHER_AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderInvoicePurchaseAmount" runat="server" Text="Invoice Setteled Amount[THB]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblInvoicePurchaseAmount" runat="server" CssClass="lblstyleGIT" Text='<%# Bind("SETTLED_AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderCancellationFees" runat="server" Text="Cancellation Fees[THB]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCancellationFees" runat="server" Text = '<%# Bind("[CANCELLATION FEES]") %>'>
                                            <%--OnTextChanged="txtSettledAmount_TextChanged"
                                                AutoPostBack="true" Text = "0" Style="text-align: right" >--%></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderGlDescription" runat="server" Text="Gl Description" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGLDescription" runat="server" CssClass="lblstyleGIT" Text='<%# Bind("GL_DESCRIPTION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHeaderPurchaseVoucherStatus" runat="server" Text="Gl Description" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchaseVoucherStatus" runat="server" CssClass="lblstyleGIT" Text='<%# Bind("VOUCHER_STATUS_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                
                
           <br />
           <br />

     <br />

     <div>
        <asp:Button ID="btnSave" Text = "Save" runat="server" OnClick = "btnSave_Click"/>
     </div>
      </ContentTemplate>
            </asp:UpdatePanel>
            </div>
     

</asp:Content>
