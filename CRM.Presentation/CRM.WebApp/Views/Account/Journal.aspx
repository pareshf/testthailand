<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="CRM.WebApp.Views.Account.Journal" %>

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
        <asp:UpdatePanel ID="update_contra" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label63" runat="server" Text="Journal Voucher" class="pageTitle" Width="400px"
                    Font-Bold="true" Font-Size="Large"></asp:Label>
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
                            <asp:Label ID="Label2" runat="server" Text="Voucher No." CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td style="width: 120px">
                            <asp:Label ID="lbl_voucher_no" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="lblGLDate" runat="server" Text="GL Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td style="width: 120px">
                            <asp:TextBox ID="txtgldate" runat="server" Width="150px"></asp:TextBox>
                            <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender22" runat="server" targetcontrolid="txtgldate"
                                watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
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
                        <td style="width: 200px">
                            <asp:Label ID="Label18" runat="server" Text="Debit" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 200px">
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
                        <td style="width: 200px">
                            <asp:TextBox ID="row1_txt_debit" runat="server" OnTextChanged="row1_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
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
                        <td style="width: 200px">
                            <asp:TextBox ID="row2_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
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
