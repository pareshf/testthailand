<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="FITPayment.aspx.cs" Inherits="CRM.WebApp.Views.FIT.FITPayment" %>

    <%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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

    <asp:Label runat="server" Text="FIT PAYMENT" ID="headlbl" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="paymentmode" class="pageTitle">
        <table width="475px" cellspacing="5" cellpadding="5">
            <tr>
                <td width="220px">
                    <asp:Label runat="server" Text="Payment Mode" ID="payment_mode" CssClass="lblstyle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpPayment" runat="server" Width="233px"                     
                        OnSelectedIndexChanged="drpPayment_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="credit_limit" runat="server" class="pageTitle">
        <asp:UpdatePanel ID="UpdatePanel_Hotel_header" runat="server" UpdateMode="Conditional"
              ChildrenAsTriggers="false">
            <ContentTemplate>
                 <table style="border-collapse: collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5"
                    cellpadding="5">
                    <tr id="c1" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Total Credit Limit" ID="lbltotalcreditlmit" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblcreditlimitAmount" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="Label2" CssClass="lblstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr id="c2" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Credit Limit Used As on Date" ID="lblcrlimituseddate"
                                CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblcrlimitdate" align="left" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="lblcrlimitdatecurrency" align="left" CssClass="lblstyle"></asp:Label>
                        </td>
                    </tr>
                    <tr id="c3" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Current Usable" ID="lblcurrentusable" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblcurrentusableamount" align="left" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="lblcurrentusablecurrency" align="left" CssClass="lblstyle"></asp:Label>
                        </td>
                    </tr>
                     <tr id="Tr_Total_invoice" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Total Invoice Amount" ID="Label6" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblTotalInvoice" align="left" CssClass="lblstyle" Width="199px"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="Label9" align="left" CssClass="lblstyle" ></asp:Label>
                        </td>
                    </tr>
                     <tr id="Tr_Discount" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Discount Amount" ID="Label5" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblDiscount" align="left" CssClass="lblstyle" Width="199px"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="Label7" align="left" CssClass="lblstyle" ></asp:Label>
                        </td>
                    </tr>

                    <tr id="trBankCharge" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Bank Charges" ID="Label8" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lblBnakCharges" align="left" CssClass="lblstyle" Width="199px"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="Label11" align="left" CssClass="lblstyle" ></asp:Label>
                        </td>
                    </tr>

                    <tr id="c4" style="display:none" runat="server">
                        <td>
                            <asp:Label runat="server" Text="Final Invoice Amount" ID="lblTotalinvoiceamt" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="" ID="lbltotalInvoiceAmount" align="left" CssClass="lblstyle" Width="199px"></asp:Label>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label runat="server" Text="USD" ID="lbltotalinvoicecurrency" align="left" CssClass="lblstyle" ></asp:Label>
                        </td>
                    </tr>
                    
                    <tr id="c5" style="display:none" runat="server">
                        <td>
                            <asp:Label ID="lblbookingrefNO" runat="server" Text="Your Company Booking Refernce No"
                                CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:TextBox ID="txtBook_ref_no" runat="server" CssClass="textboxstyle" Width="199px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server" ErrorMessage="No of Adult Required "
                                CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtBook_ref_no"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="c9" style="display:none" runat="server">
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Additional Charges"
                                CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label ID="lbladditional" runat="server" CssClass="lblstyle" Width="199px" />
                        </td>
                         <td>
                            <%--Here bind Database Field in text--%>
                            <asp:Label ID="lblcurr" runat="server" Text="" align="left" CssClass="lblstyle" ></asp:Label>
                        </td>
                    </tr>
                    <tr id="c6" style="display:none" runat="server">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Agent Main Password" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpassowrd" runat="server" CssClass="textboxstyle" Width="199px"
                                TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ErrorMessage="Password is Required" CssClass="errorclass" ValidationGroup="Required"
                                    ControlToValidate="txtpassowrd" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="authorisation" runat="server" style="display:none">
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Authorisation Number" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtauthorisation" runat="server" CssClass="textboxstyle" Width="199px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3" runat="server" ErrorMessage="Authorisation No is Required"
                                CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtauthorisation"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        
    </div>
    <br />
   
    <asp:UpdatePanel ID="updatebutton" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="Table1" runat="server" class="pageTitle">
                <tr>
                    <td>
                        &nbsp;
                        <asp:Button ID="btnpatnow" runat="server" Text="Pay Now" 
                            OnClick="btnpatnow_Click" Visible="false" />
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="btnsend" runat="server" Text="Send Request For Payment Approval"
                            OnClick="btnsend_Click" Visible="false" />
                    </td>
                     <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/FIT/paypalbtn.gif" 
                               OnClick="ImageButton1_Click" Visible="false"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
   
        <br />
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updatebutton">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <form id="form2" runat="server" visible="false">
        
        <div>
        
            <rsweb:reportviewer ID="rptViewer1" runat="server" BorderColor="Silver" 
                BorderStyle="Solid" BorderWidth="1px" Height="8.5in" Width="14in">
            </rsweb:reportviewer>
        
        </div>
    </form>
</asp:Content>
