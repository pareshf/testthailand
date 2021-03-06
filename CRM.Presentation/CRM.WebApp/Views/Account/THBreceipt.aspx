﻿                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="THBreceipt.aspx.cs" Inherits="CRM.WebApp.Views.Account.THBreceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <asp:Label ID="Label63" runat="server" Text="Accounts Receipt Voucher" class="pageTitle"
        Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
    <div>
        <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="800px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label1" runat="server" Text="Voucher Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpvoucher_type" runat="server" Width="250px" OnSelectedIndexChanged="drpvoucher_type_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label41" runat="server" Text="Voucher No."></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_voucher_no" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Agent Company Name"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpagent_company_name" runat="server" Width="250px" OnSelectedIndexChanged="drpagent_company_name_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label32" runat="server" Text="Voucher Date"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_voucher_date" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label42" runat="server" Text="GL Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgl_date" runat="server" AutoPostBack="true" ontextchanged="txtgl_date_TextChanged"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtgl_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label43" runat="server" Text="On Account"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chk_onaccount" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="update_forex" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label10" runat="server" Text="Foreign Exchange" class="pageTitle"
                    Width="150px" Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
                <table width="800px" id="Table1" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5">
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label21" runat="server" Text="Foreign Currency" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drp_currency" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="Tr1" runat="server">
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Amount (THB)" CssClass="headlabel">
                            </asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_amount" runat="server" Width="155px" OnTextChanged="txt_amount_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Amount is required"
                                ValidationGroup="required" Font-Size="12px" ControlToValidate="txt_amount" CssClass="gridlabel"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--<tr id="Tr2" style="background-color: #f3f3f3" runat="server">
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="Exchange Rate" CssClass="headlabel">
                            </asp:Label>
                            &nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ex_rate" runat="server" Width="155px" ControlToValidate="txt_ex_rate"
                                OnTextChanged="txt_ex_rate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Exchange Rate is required"
                                ValidationGroup="required" Font-Size="12px" ControlToValidate="txt_ex_rate" CssClass="gridlabel"></asp:RequiredFieldValidator>
                        </td>
                    </tr>--%>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <br />
    <div>
        <div id="Debit_Selection">
            <asp:UpdatePanel ID="update_bedit_select" runat="server" Visible="true" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <table width="900px" id="grid2_table" runat="server" border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" visible="false">
                        <tr id="row_head" runat="server" style="width: 1000px; background-color: #f3f3f3">
                            <td style="width: 150px">
                                <asp:Label ID="Label26" runat="server" Text="Sr. No." CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 400px">
                                <asp:Label ID="Label33" runat="server" Text="General Leger Code" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label35" runat="server" Text="Debit" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="Label36" runat="server" Text="Credit" CssClass="gridlabel"></asp:Label>
                            </td>
                            <%-- <td style="width:200px">
          <asp:Label ID="Label37" runat="server" Text="Amount Received" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:150px">
          <asp:Label ID="Label38" runat="server" Text="Invoice Status" CssClass="gridlabel"></asp:Label>
      </td>--%>
                        </tr>
                        <tr id="row1_debit" runat="server" style="width: 1000px; background-color: #f3f3f3">
                            <td style="width: 150px">
                                <asp:Label ID="Label37" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 400px">
                                <asp:Label ID="lbl_gl_code" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_row1_debit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_row1_credit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
                                &nbsp;
                                <asp:Label ID="Label39" runat="server" Text="THB" CssClass="gridlabel"></asp:Label>
                            </td>
                            <%-- <td style="width:200px">
          <asp:Label ID="Label37" runat="server" Text="Amount Received" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:150px">
          <asp:Label ID="Label38" runat="server" Text="Invoice Status" CssClass="gridlabel"></asp:Label>
      </td>--%>
                        </tr>
                        <tr id="row2_debit" runat="server" style="width: 1000px; background-color: #f3f3f3">
                            <td style="width: 150px">
                                <asp:Label ID="Label38" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 400px">
                                <asp:DropDownList ID="drp_gl_code" runat="server" Width="150px" />
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_row2_debit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
                                &nbsp;
                                <asp:Label ID="Label40" runat="server" Text="THB" CssClass="gridlabel"></asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:Label ID="lbl_row2_credit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
                            </td>
                            <%-- <td style="width:200px">
          <asp:Label ID="Label37" runat="server" Text="Amount Received" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:150px">
          <asp:Label ID="Label38" runat="server" Text="Invoice Status" CssClass="gridlabel"></asp:Label>
      </td>--%>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upReceipt" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:GridView ID="GridReceipt" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="Silver"
                    Width="700px">
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="200px">
                            <HeaderTemplate>
                                <asp:Label ID="lblInvoiceno" runat="server" Text="Invoice No." CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:DropDownList ID="drpinvoice" runat="server" Width="200px" onselectedindexchanged="row1_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label ID="lblAmtTHB" runat="server" Text="Invoice Amount [THB]" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAmountTHB" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label ID="lblAmountSettled" runat="server" Text="Amount Settled" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtAmountSettled" runat="server"  OnTextChanged="txtSettledAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                <asp:Label ID="lblBaltobepaid" runat="server" Text="Balance to be paid" CssClass="lblstyleGIT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBalancetobepaid" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDetailId" runat="server" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ControlStyle-Width="100px">
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOldAmount" runat="server" Visible="false"></asp:Label>
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
                <table >                
                <tr id="Tr3" runat="server" >
                   
                    <td >
                        <asp:Label ID="Label30" runat="server" Text="Total Amount Received" CssClass="gridlabel"
                            Font-Bold="true"></asp:Label>
                    </td>
                    
                   <td></td>
                    <td >
                        <asp:Label ID="lbl_total_amount" runat="server" Text="0" CssClass="gridlabel"></asp:Label>
                    </td>
                    
                </tr>
                </table>
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />

                <br />

               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
        class="pageTitle">
        <ContentTemplate>
            <table id="Table2" runat="server">
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Confirm" Visible="false" />
                        <%--<asp:Button ID="Button3" runat="server" Text="Wait List" Visible="false" />
                        <asp:Button ID="Button4" runat="server" Text="Change Of Hotel" Visible="false" />--%>
                    </td>
                </tr>
            </table>
            <%--  <ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
                PopupControlID="pnlCompanyRoleSelection" TargetControlID="Button1" Drag="true"
                PopupDragHandleControlID="pnlCompanyRoleSelectionHeader" CancelControlID="ImageButton1">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px" style="display:none;">
                <asp:Panel ID="Panel1" runat="server" Width="350px">
                    <fieldset style="background-color: White">
                        <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitleAlert" runat="server" Text="Please Enter Reconfirmation Date" ForeColor="#FEFEFE"
                                            Font-Size="15px"></asp:Label>
                                    </td>
                                    <td style="width: 17px;" align="center" valign="middle">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                            Style="cursor: pointer;" ToolTip="Close" />
                                        <%--<asp:Button ID="Button5" runat="server" Text="Button" style="display:none" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                       
                    </fieldset>
                </asp:Panel>
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="paymentdetails1">
        <asp:UpdatePanel ID="update_payments" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label6" runat="server" Text="Payment Details" class="pageTitle" Width="150px"
                    Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
                <table width="800px" id="AccountsVoucher3" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <%-- <tr style="background-color:#f3f3f3">
        <td>
                         <asp:Label ID="Label26" runat="server" Text="General Leger Code" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td >
                            <asp:DropDownList ID="drp_gl_code"  runat="server" Width="150px"/>
                        </td>
        </tr>--%>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="Payment Date" CssClass="headlabel">
                            </asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_payment_date" runat="server" CssClass="headlabel" Width="155px"
                                AutoPostBack="true" ontextchanged="txt_payment_date_TextChanged"> 
                            </asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txt_payment_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                            <ajax:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txt_payment_date"
                                Format="dd/MM/yyyy" PopupButtonID="Image1" />
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label9" runat="server" Text="Payment Mode" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drppayment_mode" runat="server" OnSelectedIndexChanged="drppayment_mode_SelectedIndexChanged"
                                Width="150px" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="bank_name_tr" runat="server">
                        <td>
                            <asp:Label ID="lbl_bank" runat="server" Text="Bank" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpbank_name" runat="server" Width="150px" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="branch_tr" runat="server">
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Branch" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpbranch" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="cheque_no_tr" runat="server">
                        <td>
                            <asp:Label ID="lbl_chq_no" runat="server" Text="Cheque No" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcheque_no" runat="server" CssClass="headlabel" Width="155px"> 
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="cheque_date_tr" runat="server">
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Cheque Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcheque_date" runat="server" CssClass="headlabel" AutoPostBack="true"
                                Width="155px"> 
                            </asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtcheque_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="cash_receipt_no_tr" runat="server">
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Cash Receipt No" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcash_receipt" runat="server" CssClass="headlabel" Width="155px"> 
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="cash_receipt_date_tr" runat="server">
                        <td>
                            <asp:Label ID="lblAttachment" runat="server" Text="Attachment" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:TextBox ID="txtcashreceipt_date" runat="server" CssClass="headlabel" AutoPostBack="true"
                                Width="155px" Visible="false"> 
                            </asp:TextBox>
                            <%-- <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtcashreceipt_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>--%>
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="voucher_status" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="800px" id="AccountsVoucher2" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <%-- <tr style="background-color:#f3f3f3">
                       <td style="width: 120px">
                         <asp:Label ID="Label5" runat="server" Text="Summary" CssClass="headlabel">
                          </asp:Label>
                      </td>
                </tr>--%>
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label27" runat="server" Text="Narration" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_narration" runat="server" TextMode="MultiLine" Width="300px"
                                Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label29" runat="server" Text="Voucher Status" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpvoucher_status" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="Buttons" runat="server">
        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="BtnStyle" Width="100px" onclick="btnSave_Click"
            ValidationGroup="required" />
    </div>
</asp:Content>
