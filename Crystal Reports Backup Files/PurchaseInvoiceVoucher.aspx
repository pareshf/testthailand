<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="PurchaseInvoiceVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.PurchaseInvoiceVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
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
    <asp:Label ID="lbltitle" runat="server" Text="Purchase Voucher" Width="400px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="Header">
        <asp:UpdatePanel ID="UpdatePanel_Generate_Invoice" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier Type" CssClass="lblstyle"></asp:Label>&nbsp;
                            <span class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpsupplier_type" runat="server" Width="250px" OnSelectedIndexChanged="drpsupplier_type_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier type is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="drpsupplier_type"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Supplier" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpsupplier" runat="server" Width="250px" OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Supplier is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="drpsupplier"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trInvoiceNo" style="display:none">
                        <td>
                            <asp:Label ID="Label27" runat="server" Text="Purchase Voucher No." CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchaseVoucher" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Due Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdue_date" runat="server" Width="250px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Due date is required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtdue_date"></asp:RequiredFieldValidator>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txtdue_date"
                                WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                            </ajax:TextBoxWatermarkExtender>
                             <ajax:calendarextender id="CalendarExtenderpty2" runat="server" targetcontrolid="txtdue_date"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="GL Date" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgl_date" runat="server" Width="250px"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtgl_date"
                                WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                            </ajax:TextBoxWatermarkExtender>
                              <ajax:calendarextender id="CalendarExtenderpty3" runat="server" targetcontrolid="txtgl_date"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                        </td>
                    </tr>

                     <tr id="trsrno" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label55" runat="server" Text="Voucher Sr. No" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtvouchersrno" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                            
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />

        <div>
          <asp:Label id="lblVoucherHeader" runat="server" Text = "Voucher Details" Font-Size="Medium"></asp:Label>
          <br />
          <br />
        <asp:UpdatePanel ID="updategrid" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="900px" id="AccountsVoucher4" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <tr id="gridheading" runat="server" style="width: 1000px; background-color: #f3f3f3">
                        <td style="width: 150px">
                            <asp:Label ID="Label7" runat="server" Text="Sr. No." CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:Label ID="Label16" runat="server" Text="GL Description" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:Label ID="Label17" runat="server" Text="Currency" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:Label ID="Label18" runat="server" Text="Debit" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:Label ID="Label19" runat="server" Text="Credit" CssClass="gridlabel"></asp:Label>
                        </td>
                       
                    </tr>
                    <%----------------------------------------------------------- ROW 1 -------------------------------------------------------------%>
                    <tr id="row1" runat="server" style="width: 1300px">
                        <td style="width: 100px">
                            <asp:Label ID="Label20" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row1_drp_glcode" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                           
                            <asp:Label ID="lbl_row1" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                           
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row1_txt_credit" runat="server" 
                                Enabled="false"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <%----------------------------------------------------------- ROW 2 ------------------------------------------------------------%>
                    <tr id="row2" runat="server" style="width: 1300px;">
                        <td style="width: 100px">
                            <asp:Label ID="Label25" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row2_drp_glcode" runat="server" Width="200px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                           
                            <asp:Label ID="Label44" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row2_txt_debit" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                        
                        </td>
                      
                    </tr>
                    </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>

                    <br />

    <div id="Grid">
        
        <asp:UpdatePanel ID="upInvoiceGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="90%">
                    <tr>
                        <td>
                            <asp:GridView ID="GridInvoice" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="Silver">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="200px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblInvoice" runat="server" Text="Invoice No" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpInvoiceNo" runat="server" OnSelectedIndexChanged="drpInvoiceNo_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblInvoiceAmount" runat="server" Text="Sales Invoice Amount [USD]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtInvoiceAmount" runat="server" Enabled = "false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblInvoiceAmountTHB" runat="server" Text="Sales Invoice Amount [THB]-(DCR)" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtInvoiceAmountTHB" runat="server" Enabled = "false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text="Sales Invoice Amount [THB]-(RCR)" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblsettledamount" runat="server" Text="Purchase Amount [THB]" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSettledAmount" runat="server" OnTextChanged="txtSettledAmount_TextChanged"
                                                AutoPostBack="true" Text = "0" Style="text-align: right" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTourNameHeader" runat="server" Text="Tour Name" CssClass="lblstyleGIT"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTourName" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                  <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemaingBalance" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountSettled" runat="server" CssClass="lblstyleGIT"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnHotelRemove" Text="Remove" OnClick="btnRemove_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />
                           </td>

                         <tr>
                        
                         <td>

                          <asp:Label Width ="50%" ID="lblb" runat="server"></asp:Label> 
                          <asp:Label ID="Label9" Text = "Voucher Amount :" runat="server"  CssClass="lblstyleGIT" />
                          
                           
                            <asp:Label ID="lblsettleamt" runat="server"  CssClass="lblstyleGIT" Font-Bold="true" Font-Size="15px"></asp:Label>
                            </td>
                            </tr>
                            
                            <tr>
                            <td>
                           
                            <asp:Label Width ="50px" ID="Label8" runat="server"></asp:Label> 
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAddHotel_Click" Width="100px"/>

                            
                            </td>
                            </tr>
                        </td>
                    </tr>
                </table>
               
              
           
            </ContentTemplate>
        </asp:UpdatePanel>
         </div>
         <br />

      

        <div id="naration">
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Narration" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Order Status" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID = "drpOrderStatus" runat = "server" Width = "200px" Enabled="false"></asp:DropDownList>
                        </td>
                    </tr>
                    </table>

                    <table>
                    
                    <tr>
                    <td>
                        <asp:Button runat="server" ID="btnSave" Text = "Save" OnClick="btnSave_Click" ValidationGroup="Required"/>
                    </td>
                    </tr>
                    </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
   
</asp:Content>
