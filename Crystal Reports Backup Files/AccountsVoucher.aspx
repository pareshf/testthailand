<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AccountsVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.AccountsVoucher" %>

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
    <style>
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 20px;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: normal;
        }
        
        .gridlabel
        {
            font-family: Verdana;
            font-size: 14px;
            font-weight: bold;
        }
        
        #TextArea1
        {
            width: 390px;
        }
    </style>

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    
    <div>
        <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
             
                <asp:Label ID="Label63" runat="server" Text="Sales/Purchase Voucher" class="pageTitle"
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

                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="Label10" runat="server" Text="GL Date" CssClass="headlabel">
                            </asp:Label>
                        </td>

                        <td style="width: 120px">
                            <asp:Label ID="lblgl_date" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                        </td>
                    </tr>
                    <%---------------------------------------NOT SHOWN IN FRONT END-----------------------------------------------%>
                    <tr style="background-color: #f3f3f3" runat="server" visible="false">
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
                    <tr style="background-color: #f3f3f3" runat="server" visible="false">
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
                    <tr style="background-color: #f3f3f3" id="id_6" runat="server" visible="false">
                        <td>
                            <asp:Label ID="Label82" runat="server" Text="Invoice Amount" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_inovice_amount" runat="server" Text="" CssClass="headlabel">
                            </asp:Label>
                            &nbsp; &nbsp;
                            <asp:Label ID="lbl_currency_name" runat="server" Text="" CssClass="headlabel">
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
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txt_voucher_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <%--  <tr style="background-color:#f3f3f3">
                       <td >
                         <asp:Label ID="Label4" runat="server" Text="Sequence No" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                          <asp:TextBox ID="TextBox4" runat="server" CssClass="headlabel" Width="155px"  > 
                                </asp:TextBox>
                        </td>
                    </tr>--%>
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
                        <td style="width: 300px">
                            <asp:TextBox ID="row1_txt_debit" runat="server" OnTextChanged="row1_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row1_txt_credit" runat="server" OnTextChanged="row1_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%--<asp:CheckBox ID="row1_cb" Text="" runat="server" OnCheckedChanged="row1_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
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
                        <td style="width: 300px">
                            <asp:TextBox ID="row2_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row2_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 3 ---------------------------------------------------------%>
                    <tr id="row3" runat="server" style="width: 1400px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label26" runat="server" Text="3" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row3_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row3_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label45" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row3_txt_debit" runat="server" OnTextChanged="row3_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row3_txt_credit" runat="server" OnTextChanged="row3_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row3_cb" Text="" runat="server" OnCheckedChanged="row3_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row3_cb" runat="server" OnCheckedChanged="row3_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove3" runat="server" Text="REMOVE" OnClick="btnremove3_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 4 ---------------------------------------------------%>
                    <tr id="row4" runat="server" style="width: 1400px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label27" runat="server" Text="4" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row4_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row4_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label46" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row4_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row4_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove4" runat="server" Text="REMOVE" OnClick="btnremove4_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 5 ----------------------------------------------------------------%>
                    <tr id="row5" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label28" runat="server" Text="5" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row5_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row5_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label47" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row5_txt_debit" runat="server" OnTextChanged="row5_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row5_txt_credit" runat="server" OnTextChanged="row5_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row5_cb" Text="" runat="server" OnCheckedChanged="row5_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row5_cb" runat="server" OnCheckedChanged="row5_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove5" runat="server" Text="REMOVE" OnClick="btnremove5_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------------- ROW 6 ----------------------------------------------------------%>
                    <tr id="row6" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label29" runat="server" Text="6" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row6_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row6_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label48" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row6_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row6_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove6" runat="server" Text="REMOVE" OnClick="btnremove6_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 7------------------------------------------------------------%>
                    <tr id="row7" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label30" runat="server" Text="7" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row7_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row7_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label49" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row7_txt_debit" runat="server" OnTextChanged="row7_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row7_txt_credit" runat="server" OnTextChanged="row7_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%--<asp:CheckBox ID="row7_cb" Text="" runat="server" OnCheckedChanged="row7_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row7_cb" runat="server" OnCheckedChanged="row7_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove7" runat="server" Text="REMOVE" OnClick="btnremove7_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 8------------------------------------------------------------%>
                    <tr id="row8" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label31" runat="server" Text="8" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row8_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row8_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label50" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row8_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row8_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove8" runat="server" Text="REMOVE" OnClick="btnremove8_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 9------------------------------------------------------------%>
                    <tr id="row9" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label32" runat="server" Text="9" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row9_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row9_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label51" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row9_txt_debit" runat="server" OnTextChanged="row9_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row9_txt_credit" runat="server" OnTextChanged="row9_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row9_cb" Text="" runat="server" OnCheckedChanged="row9_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row9_cb" runat="server" OnCheckedChanged="row9_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove9" runat="server" Text="REMOVE" OnClick="btnremove9_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 10------------------------------------------------------------%>
                    <tr id="row10" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label33" runat="server" Text="10" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row10_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row10_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label52" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row10_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row10_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove10" runat="server" Text="REMOVE" OnClick="btnremove10_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 11 -------------------------------------------------------------%>
                    <tr id="row11" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label34" runat="server" Text="11" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row11_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row11_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label53" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row11_txt_debit" runat="server" OnTextChanged="row11_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row11_txt_credit" runat="server" OnTextChanged="row11_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row11_cb" Text="" runat="server" OnCheckedChanged="row11_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row11_cb" runat="server" OnCheckedChanged="row11_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove11" runat="server" Text="REMOVE" OnClick="btnremove11_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 12 ------------------------------------------------------------%>
                    <tr id="row12" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label35" runat="server" Text="12" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row12_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row12_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label54" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row12_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row12_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove12" runat="server" Text="REMOVE" OnClick="btnremove12_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 13 ---------------------------------------------------------%>
                    <tr id="row13" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label36" runat="server" Text="13" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row13_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row13_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label55" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row13_txt_debit" runat="server" OnTextChanged="row13_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row13_txt_credit" runat="server" OnTextChanged="row13_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%--  <asp:CheckBox ID="row13_cb" Text="" runat="server" OnCheckedChanged="row13_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row13_cb" runat="server" OnCheckedChanged="row13_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove13" runat="server" Text="REMOVE" OnClick="btnremove13_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 14 ---------------------------------------------------%>
                    <tr id="row14" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label37" runat="server" Text="14" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row14_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row14_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label56" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row14_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row14_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <%--<td style="width:200px">
          <asp:CheckBox ID="row15_cb" Text="" runat="server" OnCheckedChanged="row15_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                        <%--</td>--%>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove14" runat="server" Text="REMOVE" OnClick="btnremove14_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%----------------------------------------------------------- ROW 15 ----------------------------------------------------------------%>
                    <tr id="row15" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label38" runat="server" Text="15" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row15_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row15_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label57" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row15_txt_debit" runat="server" OnTextChanged="row15_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row15_txt_credit" runat="server" OnTextChanged="row15_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row15_cb" Text="" runat="server" OnCheckedChanged="row15_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row15_cb" runat="server" OnCheckedChanged="row15_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove15" runat="server" Text="REMOVE" OnClick="btnremove15_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------------- ROW 16 ----------------------------------------------------------%>
                    <tr id="row16" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label39" runat="server" Text="16" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row16_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row16_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label58" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row16_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row16_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove16" runat="server" Text="REMOVE" OnClick="btnremove16_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 17------------------------------------------------------------%>
                    <tr id="row17" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label40" runat="server" Text="17" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row17_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row17_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label59" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row17_txt_debit" runat="server" OnTextChanged="row17_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row17_txt_credit" runat="server" OnTextChanged="row17_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row17_cb" Text="" runat="server" OnCheckedChanged="row17_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row17_cb" runat="server" OnCheckedChanged="row17_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove17" runat="server" Text="REMOVE" OnClick="btnremove17_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 18------------------------------------------------------------%>
                    <tr id="row18" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label41" runat="server" Text="18" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row18_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row18_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label60" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row18_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row18_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove18" runat="server" Text="REMOVE" OnClick="btnremove18_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 19------------------------------------------------------------%>
                    <tr id="row19" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label42" runat="server" Text="19" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row19_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row19_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label61" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row19_txt_debit" runat="server" OnTextChanged="row19_txt_debit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row19_txt_credit" runat="server" OnTextChanged="row19_txt_credit_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                            <%-- <asp:CheckBox ID="row19_cb" Text="" runat="server" OnCheckedChanged="row19_cb_CheckedChanged" AutoPostBack="true" Visible="false"/> --%>
                            <asp:RadioButton ID="row19_cb" runat="server" OnCheckedChanged="row19_cb_CheckedChanged"
                                AutoPostBack="true" Visible="false" GroupName="pd" />
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove19" runat="server" Text="REMOVE" OnClick="btnremove19_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                    <%------------------------------------------- ROW 20------------------------------------------------------------%>
                    <tr id="row20" runat="server" style="width: 1300px; display: none">
                        <td style="width: 100px">
                            <asp:Label ID="Label43" runat="server" Text="20" CssClass="gridlabel"></asp:Label>
                        </td>
                        <td style="width: 400px">
                            <asp:DropDownList ID="row20_drp_gl" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 200px">
                            <asp:DropDownList ID="row20_drp_currency" runat="server" Width="200px" Visible="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label62" runat="server" Text="THB"></asp:Label>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row20_txt_debit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 300px">
                            <asp:TextBox ID="row20_txt_credit" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 200px">
                        </td>
                        <td style="width: 100px">
                            <asp:Button ID="btnremove20" runat="server" Text="REMOVE" OnClick="btnremove20_Click"
                                Width="100px" CssClass="BtnStyle" />
                        </td>
                    </tr>
                </table>
                <br />
                &nbsp; &nbsp;
                <asp:Button ID="btnadd2" runat="server" Text="ADD" OnClick="btnadd2_Click" Width="100px"
                    CssClass="BtnStyle" Style="display: none" />
                <asp:Button ID="btnadd3" runat="server" Text="ADD" OnClick="btnadd3_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd4" runat="server" Text="ADD" OnClick="btnadd4_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd5" runat="server" Text="ADD" OnClick="btnadd5_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd6" runat="server" Text="ADD" OnClick="btnadd6_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd7" runat="server" Text="ADD" OnClick="btnadd7_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd8" runat="server" Text="ADD" OnClick="btnadd8_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd9" runat="server" Text="ADD" OnClick="btnadd9_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd10" runat="server" Text="ADD" OnClick="btnadd10_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd11" runat="server" Text="ADD" OnClick="btnadd11_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd12" runat="server" Text="ADD" OnClick="btnadd12_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd13" runat="server" Text="ADD" OnClick="btnadd13_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd14" runat="server" Text="ADD" OnClick="btnadd14_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd15" runat="server" Text="ADD" OnClick="btnadd15_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd16" runat="server" Text="ADD" OnClick="btnadd16_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd17" runat="server" Text="ADD" OnClick="btnadd17_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd18" runat="server" Text="ADD" OnClick="btnadd18_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd19" runat="server" Text="ADD" OnClick="btnadd19_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
                <asp:Button ID="btnadd20" runat="server" Text="ADD" OnClick="btnadd20_Click" Style="display: none"
                    Width="100px" CssClass="BtnStyle" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div id="paymentdetails1">
        <asp:UpdatePanel ID="update_payments" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label8" runat="server" Text="Payment Details" class="pageTitle" Width="150px"
                    Style="font-weight: normal; font-size: 16px; font-family: Verdana" Visible="false"></asp:Label>
                <table width="800px" id="AccountsVoucher3" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5" visible="false">
                    <tr style="background-color: #f3f3f3">
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="Payment Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_payment_date" runat="server" CssClass="headlabel" Width="155px"
                                OnTextChanged="txt_payment_date_TextChanged" AutoPostBack="true"> 
                            </asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txt_payment_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3">
                        <td style="width: 120px">
                            <asp:Label ID="lbl_payment_mode" runat="server" Text="Payment Mode" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drppayment_mode" runat="server" Width="150px" OnSelectedIndexChanged="drppayment_mode_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="bank_name_tr" runat="server">
                        <td>
                            <asp:Label ID="lbl_bank" runat="server" Text="Bank" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpbank_name" runat="server" Width="150px" OnSelectedIndexChanged="drpbank_name_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="branch_tr" runat="server">
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Branch" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpbranch" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3; display: none" id="cheque_no_tr" runat="server">
                        <td>
                            <asp:Label ID="lbl_chk_no" runat="server" Text="Cheque No" CssClass="headlabel">
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
                            <asp:TextBox ID="txtcheque_date" runat="server" CssClass="headlabel" Width="155px"
                                OnTextChanged="txtcheque_date_TextChanged" AutoPostBack="true"> 
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
                            <asp:Label ID="Label14" runat="server" Text="Cash Receipt Date" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcashreceipt_date" runat="server" CssClass="headlabel" Width="155px"
                                OnTextChanged="txtcashreceipt_date_TextChanged" AutoPostBack="true"> 
                            </asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtcashreceipt_date"
                                WatermarkText="dd/MM/yyyy">
                            </ajax:TextBoxWatermarkExtender>
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="update_forex" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="Label5" runat="server" Text="Foreign Exchange" class="pageTitle" Width="150px"
                    Style="font-weight: normal; font-size: 16px; font-family: Verdana" Visible="false"></asp:Label>
                <table width="800px" id="Table1" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5" visible="false">
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
                            <asp:Label ID="Label22" runat="server" Text="Amount" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_amount" runat="server" Width="155px" OnTextChanged="txt_amount_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="background-color: #f3f3f3" id="Tr2" runat="server">
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="Exchange Rate" CssClass="headlabel">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_ex_rate" runat="server" Width="155px" OnTextChanged="txt_ex_rate_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <caption>
                        <br />
                    </caption>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<div id="paymentdetails2" >
  <asp:UpdatePanel ID="update_payments_2" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
              <ContentTemplate>

  <asp:Label ID="Label69" runat="server" Text="Payment Details" class="pageTitle" Width="150px"
         style="font-weight:normal; font-size:16px; font-family:Verdana" visible="false"></asp:Label>
        
          

        <table width="800px" id="Table2" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" visible="false">


        <tr style="background-color:#f3f3f3">
        <td >
                         <asp:Label ID="Label70" runat="server" Text="Payment Date" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td >
                          <asp:TextBox ID="TextBox1" runat="server" CssClass="headlabel" Width="155px" > 
                                </asp:TextBox>
                                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txt_payment_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
                        </td>
        </tr>

                  <tr style="background-color:#f3f3f3">
                       <td style="width: 120px">
                           <asp:Label ID="Label71" runat="server" Text="Payment Mode" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="DropDownList1"  runat="server" Width="150px" 
                                onselectedindexchanged="drppayment_mode_SelectedIndexChanged" AutoPostBack="true">
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr3" runat="server">
                       <td>
                           <asp:Label ID="Label72" runat="server" Text="Bank" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="DropDownList2"  runat="server" Width="150px" 
                                onselectedindexchanged="drpbank_name_SelectedIndexChanged" AutoPostBack="true">
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr4" runat="server">
                       <td >
                           <asp:Label ID="Label73" runat="server" Text="Branch" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="DropDownList3"  runat="server" Width="150px">
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr5" runat="server">
                       <td >
                         <asp:Label ID="Label74" runat="server" Text="Cheque No" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                          <asp:TextBox ID="TextBox2" runat="server" CssClass="headlabel" Width="155px" > 
                                </asp:TextBox>
                        </td>
                    </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr6" runat="server">
                       <td >
                         <asp:Label ID="Label75" runat="server" Text="Cheque Date" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                       
                          <asp:TextBox ID="TextBox3" runat="server" CssClass="headlabel" Width="155px" > 
                                </asp:TextBox>
                           <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="txtcheque_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
  
                        </td>
                    </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr7" runat="server">
                       <td >
                           <asp:Label ID="Label76" runat="server" Text="Cash Receipt No" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="TextBox4" runat="server" CssClass="headlabel" width="155px"> 
                                </asp:TextBox>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="Tr8" runat="server">
                       <td >
                         <asp:Label ID="Label77" runat="server" Text="Cash Receipt Date" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                          <asp:TextBox ID="TextBox5" runat="server" CssClass="headlabel" Width="155px"  > 
                                </asp:TextBox>

                        </td>
                       
                    </tr>
                    <br />
       </table>
      </ContentTemplate>
      </asp:UpdatePanel>
      

      
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
             <ContentTemplate>
  <asp:Label ID="Label78" runat="server" Text="Foreign Exchange" class="pageTitle" Width="150px"
         style="font-weight:normal; font-size:16px; font-family:Verdana" Visible="false"></asp:Label>

         
             
                <table width="800px" id="Table3" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" Visible="false">

                  <tr style="background-color:#f3f3f3">
                       <td style="width: 120px">
                           <asp:Label ID="Label79" runat="server" Text="Foreign Currency" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="DropDownList4"  runat="server" Width="150px" >
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3" id="Tr9" runat="server">
                       <td>
                           <asp:Label ID="Label80" runat="server" Text="Amount" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" Width="155px" 
                                ontextchanged="TextBox6_TextChanged"></asp:TextBox>
                         
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3" id="Tr10" runat="server">
                       <td >
                           <asp:Label ID="Label81" runat="server" Text="Exchange Rate" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="TextBox7" runat="server" Width="155px"></asp:TextBox>
                         </td>
                 </tr>
                    <caption>
                        <br />
                    </caption>
                 </table>
              </ContentTemplate>
              </asp:UpdatePanel>
              
         
</div>--%>
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
    <br />
    <div id="Buttons" runat="server">
        <asp:UpdatePanel ID="update_btn" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="BtnStyle" Width="100px"
                    OnClick="btnSave_Click" />&nbsp;&nbsp;
                <asp:Button ID="Button2" Text="Prepare Voucher" runat="server" CssClass="BtnStyle"
                    Width="100px" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="Button3" Text="Approve Voucher" runat="server" CssClass="BtnStyle"
                    Width="100px" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="Button4" Text="Post Voucher" runat="server" CssClass="BtnStyle" Width="100px"
                    Visible="false" />&nbsp;&nbsp;
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
</asp:Content>
