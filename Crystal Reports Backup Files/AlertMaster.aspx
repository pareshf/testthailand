<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    CodeBehind="AlertMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.AlertMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"> </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageTitle" runat="server" Text="Application Settings"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="Literal3" Text="SMS Alerts"></asp:Literal>
                    </th>
                    <th colspan="2">
                        <div class="MandatoryNote" align="right" style="margin-top: 3px; margin-bottom: 3px">
                            <asp:Literal ID="Literal4" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblBirthdateofemp" runat="server" Text="Birthday of Employee:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtBirthdateofemp" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSmsNewCustomer" runat="server" ControlToValidate="txtBirthdateofemp"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                    </td>
                    <td>
                        <small>
                           <asp:Label ID="Label40" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblCustomerBirthdate" runat="server" Text="Birthday of Customer:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtCustomerBirthdate" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerBirthdate"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                           <asp:Label ID="Label41" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcustAnnvsry" runat="server" Text="Marriage Anniversary of the Customer:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtcustAnnvsry" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcustAnnvsry"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                      <asp:Label ID="Label42" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lbltotalbookedinq" runat="server" Text="Today's Total Booked Inquires:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txttotalbookedinq" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttotalbookedinq"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                         <asp:Label ID="Label43" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lbltotalcnclinq" runat="server" Text="Today's Total Canceled Inquires:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txttotalcnclinq" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotalcnclinq"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                         <asp:Label ID="Label44" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewcst" runat="server" Text="Today's Total New Customers Created:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtnewcst" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtnewcst"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                          <asp:Label ID="Label45" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewinq" runat="server" Text="Today's Total New Inquiries Created :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtnewinq" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtnewinq"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                          <asp:Label ID="Label46" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblAddingNewemp" runat="server" Text="Addition of New Employee :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtAddingNewemp" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAddingNewemp"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                          <asp:Label ID="Label47" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewadditionof_D_tour" runat="server" Text="New Domestic Tour Added in CRM :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtnewadditionof_D_tour" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtnewadditionof_D_tour"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                          <asp:Label ID="Label48" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewadditionof_I_tour" runat="server" Text="New International Tour Added in CRM :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtnewadditionof_I_tour" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtnewadditionof_I_tour"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                           <asp:Label ID="Label49" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcond1" runat="server" Text="If Inquiry Status is Book and Token Amount is 0 or Blank :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtcond1" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcond1"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                           <asp:Label ID="Label50" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcond2" runat="server" Text="Missed Next Follow-up Date :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtcond2" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtcond2"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                         <asp:Label ID="Label51" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcoond3" runat="server" Text="Varition in Avalible sheats to Booked sheets for Tour:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox ID="txtcoond3" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtcoond3"
                            ValidationGroup="vgSms" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                          <asp:Label ID="Label52" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
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
</asp:Content>
