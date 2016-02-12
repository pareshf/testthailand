<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="ApplicationSettings.aspx.cs" Inherits="CRM.WebApp.Views.Administration.ApplicationSettings" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"> </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageTitle" runat="server" Text="Application Settings"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="ltlTableHeader" Text="SMTP Settings"></asp:Literal>
                    </th>
                    <th>
                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblSmtpUsername" runat="server" Text="SMTP Username:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmtpUsername" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSmtpUsername" runat="server" ControlToValidate="txtSmtpUsername"
                            ValidationGroup="vgSmtp" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmtpPassword" runat="server" Text="SMTP Password:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmtpPassword" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSmtpPassword" runat="server" ControlToValidate="txtSmtpPassword"
                            ValidationGroup="vgSmtp" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmtpHost" runat="server" Text="SMTP Host:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmtpHost" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSmtpHost"
                            ValidationGroup="vgSmtp" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmtpPort" runat="server" Text="SMTP Port:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmtpPort" runat="server" ValidationGroup="vgSmtp" Width="350px"
                            OnKeyPress="javascript:return ValidNumber('event')"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSmtpPort"
                            ValidationGroup="vgSmtp" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSaveSmtpSettings" runat="server" Text="Save" ValidationGroup="vgSmtp"
                            OnClick="btnSaveSmtpSettings_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="Literal1" Text="SMS Settings"></asp:Literal>
                    </th>
                    <th>
                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                            <asp:Literal ID="Literal2" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblSmsUsername" runat="server" Text="SMS Username:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmsUsername" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSmsUsername"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmsPassword" runat="server" Text="SMS Password:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmsPassword" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSmsPassword"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmsDomain" runat="server" Text="SMS Domain:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmsDomain" runat="server" ValidationGroup="vgSmtp" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSmsDomain"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSenderId" runat="server" Text="SMS Sender Id:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmsSenderId" runat="server" ValidationGroup="vgSms" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSmsSenderId"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSaveSmsSettings" runat="server" Text="Save" ValidationGroup="vgSms"
                            OnClick="btnSaveSmsSettings_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="Literal3" Text="Alert Message Format"></asp:Literal>
                    </th>
                    <th colspan="2">
                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                            <asp:Literal ID="Literal4" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblSmsNewCust" runat="server" Text="SMS - NewCustomer:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtSmsNewCustomer" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSmsNewCustomer" runat="server" ControlToValidate="txtSmsNewCustomer"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                            <asp:Label ID="Label5" runat="server" Text="Allowed Tag {:Customer UniqId}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSmsNewEnquiry" runat="server" Text="SMS - NewEnquiry:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSmsNewEnqury" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSms"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSmsNewEnquiry" runat="server" ControlToValidate="txtSmsNewEnqury"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                            <asp:Label ID="Label3" runat="server" Text="Allowed Tag {:Employee UniqId}">
                            </asp:Label>
                        </small>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMailNewCustomer" runat="server" Text="Mail - NewCustomer:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMailNewCustomerAlt" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSms"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMailNewCustomer" runat="server" ControlToValidate="txtMailNewCustomerAlt"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                            <asp:Label ID="lbl1" runat="server" Text="Allowed Tag {:Employee UniqId}">
                            </asp:Label>
                        </small>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMailNewEnquiry" runat="server" Text="Mail - NewEnquiry:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMailNewEnquiry" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSms"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMailNewEnquiry" runat="server" ControlToValidate="txtMailNewEnquiry"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                            <asp:Label ID="Label4" runat="server" Text="Allowed Tag {:Customer UniqId}">
                            </asp:Label>
                        </small>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgSms" OnClick="btnSaveAlertMsgFormat_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="Literal5" Text="Alert Setting"></asp:Literal>
                    </th>
                    <th colspan="2">
                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                            <asp:Literal ID="Literal6" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="Label1" runat="server" Text="New Customer:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="radNewCustomer" runat="server" ValidationGroup="vgSms">
                            <asp:ListItem Value="Sms" Text="Sms" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="Mail" Text="Mail" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="Both" Text="Both" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="None" Text="None" Selected="False"></asp:ListItem>
                        </asp:RadioButtonList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="radNewCustomer"
                                        ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="New Enquiry:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="radNewEnquiry" runat="server" ValidationGroup="vgSms">
                            <asp:ListItem Value="Sms" Text="Sms" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="Mail" Text="Mail" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="Both" Text="Both" Selected="False"></asp:ListItem>
                            <asp:ListItem Value="None" Text="None" Selected="False"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="radNewEnquiry"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Save" ValidationGroup="vgSms" OnClick="btnSaveAlertSettings_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
