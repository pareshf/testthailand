<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SalesCancellation.aspx.cs" Inherits="CRM.WebApp.Views.Account.SalesCancelation" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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

    <div>
        <asp:UpdatePanel ID="update_salesCancellation" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label63" runat="server" Text="Sales Cancellation" class="pageTitle"
                    Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
                <br />
                <br />
                <table width="800px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label3" runat="server" Text="Voucher Type" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpvoucher_type" runat="server" Width="155px" OnSelectedIndexChanged="drpvoucher_type_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label9" runat="server" Text="Voucher No." CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td style="width: 120px">
                            <asp:Label ID="lblVoucher_no" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                   
                    <%---------------------------------------NOT SHOWN IN FRONT END-----------------------------------------------%>
                    <tr id="Tr1" style="background-color: #f3f3f3" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lbl_acc" runat="server" Text="Account Group" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drp_account_grp" runat="server" Width="155px" OnSelectedIndexChanged="drp_account_grp_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="Tr2" style="background-color: #f3f3f3" runat="server" visible="false">
                        <td>
                            <asp:Label ID="lbl_gl" runat="server" Text="General Leger Code" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drp_gl_code" runat="server" Width="155px" OnSelectedIndexChanged="drp_gl_code_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%----------------------------------------------END------------------------------------------------%>
                    <tr style="background-color: #f3f3f3" id="invoice_no_tr" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Invoice No" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drp_invoice_no" runat="server" Width="155px" OnSelectedIndexChanged="drp_invoice_no_SelectedIndexChanged"
                                AutoPostBack="true" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox ID="txt_invoice_no" runat="server" Visible="false" Width="155px"></asp:TextBox>
                        </td>
                    </tr>
                    <%---------------------------------------INVOICE DETAILS--------------------------------------%>
                    <tr style="background-color: #f3f3f3" id="id_1" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label64" runat="server" Text="Client Name" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_client_name" runat="server" Text="" CssClass="headlabel" Visible="false">
                            </asp:Label>
                            <asp:DropDownList ID="drp_client_name" runat="server" Width="155px" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="id_2" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label65" runat="server" Text="Tour Name" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_tour_short_name" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="id_3" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label67" runat="server" Text="Package" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_package_name" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="id_4" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label66" runat="server" Text="Ordered By" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_created_by" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="id_5" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label68" runat="server" Text="Invoice Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_inovice_date" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="Tr3" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="GL Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgl_date" runat="server" CssClass="headlabel" Width="155px"> 
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="id_6" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label82" runat="server" Text="Cancellation Amount" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            
                            <asp:TextBox ID = "txtCancellationAmount" runat="server" OnTextChanged = "txtCancellationAmount_TextChanged" AutoPostBack="true" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="lbl_currency_name" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>

                    <tr style="background-color: #f3f3f3" id="Tr5" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Invoice Amount" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblInvoiceAmount" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                            &nbsp; &nbsp;
                            <asp:Label ID="lblInvoiceAmountCurrency" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <%-- ----------------------------------------- END INVOICE DETAILS----------------------------------------%>
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="lbl_voucher_date" runat="server" Text="Voucher Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_voucher_date" runat="server" CssClass="headlabel" Width="155px"> 
                            </asp:TextBox>
                            <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender2" runat="server" targetcontrolid="txt_voucher_date"
                                watermarktext="dd/MM/yyyy">
                            </ajax:textboxwatermarkextender>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    <div>
        <asp:UpdatePanel ID="updategrid" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="900px" id="AccountsVoucher4" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <tr id="gridheading" runat="server" style="width: 1000px; background-color: #f3f3f3">
                        <td style="width: 150px">
                            <asp:Label ID="Label1" runat="server" Text="Sr. No." CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:Label ID="Label16" runat="server" Text="General Leger Code" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:Label ID="Label17" runat="server" Text="Currency" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:Label ID="Label18" runat="server" Text="Debit" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:Label ID="Label19" runat="server" Text="Credit" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:Label ID="Label4" runat="server" Text="Payments Details" CssClass="gridlabel"
                                Visible="false"></asp:Label>
                        </td>
                        <td style="width: 50px">
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 1 -------------------------------------------------------------%>
                    <tr id="row1" runat="server" style="width: 1300px">
                        <td style="width: 100px">
                            <asp:Label ID="Label20" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row1_drp_glcode" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row1_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="lbl_row1" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="row1_txt_debit" runat="server" OnTextChanged="row1_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="row1_txt_credit" runat="server" OnTextChanged="row1_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <asp:RadioButton ID="row1_cb" runat="server" OnCheckedChanged="row1_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 2 ------------------------------------------------------------%>
                    <tr id="row2" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label25" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row2_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row2_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label44" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="row2_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 310px">
                            <asp:TextBox ID="row2_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div id="voucher_status" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <table width="800px" id="AccountsVoucher2" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                    <tr>
                        <td style="width: 120px">
                            <asp:Label ID="Label6" runat="server" Text="Narration" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_narration" runat="server" TextMode="MultiLine" Width="300px"
                                Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Voucher Status" CssClass="headlabel">
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
        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="BtnStyle" Width="100px"
            OnClick="btnsave_Click" />&nbsp;&nbsp;
    </div>
</asp:Content>
