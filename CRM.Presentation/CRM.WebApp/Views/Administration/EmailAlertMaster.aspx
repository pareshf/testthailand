<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailAlertMaster.aspx.cs" MasterPageFile="~/Views/Shared/CRMMaster.Master" Inherits="CRM.WebApp.Views.Administration.EmailAlertMaster" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"> </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageTitle" runat="server" Text="Email Alerts"></asp:Literal>
    </div>
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
                        <asp:Label ID="lblBirthdateofemp" runat="server" Text="Birthday of Employee:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubBirthdateofemp" runat="server" 
                            Width="200px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtsubBirthdateofemp"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                 
                        <asp:TextBox ID="txtBirthdateofemp" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEMAILNewCustomer" runat="server" ControlToValidate="txtBirthdateofemp"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
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
                     <asp:TextBox ID="txtsubCustomerBirthdate" runat="server" 
                            Width="200px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtsubCustomerBirthdate"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtCustomerBirthdate" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerBirthdate"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
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
                     <asp:TextBox ID="txtsubcustAnnvsry" runat="server" Width="200px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtsubcustAnnvsry"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtcustAnnvsry" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcustAnnvsry"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                       <small>
                         <asp:Label ID="Label1" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lbltotalbookedinq" runat="server" Text="Today's Total Booked Inquires:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                    
                             <asp:TextBox ID="txtsubtotalbookedinq" runat="server" 
                            Width="200px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtsubtotalbookedinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txttotalbookedinq" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttotalbookedinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                            
                    </td>
                    <td>
                        <small>
                       <asp:Label ID="Label2" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lbltotalcnclinq" runat="server" Text="Today's Total Canceled Inquires:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubtotalcnclinq" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtsubtotalcnclinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txttotalcnclinq" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotalcnclinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <small>
                         <asp:Label ID="Label3" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewcst" runat="server" Text="Today's Total New Customers Created:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubnewcst" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtsubnewcst"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                 
                        <asp:TextBox ID="txtnewcst" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtnewcst"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                       <small>
                          <asp:Label ID="Label4" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewinq" runat="server" Text="Today's Total New Inquiries Created :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubnewinq" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtsubnewinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtnewinq" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtnewinq"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                       <small>
                          <asp:Label ID="Label5" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblAddingNewemp" runat="server" Text="Addition of New Employee :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubAddingNewemp" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtsubAddingNewemp"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtAddingNewemp" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAddingNewemp"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    <small>
                       <asp:Label ID="Label6" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewadditionof_D_tour" runat="server" Text="New Domestic Tour Added in CRM :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubnewadditionof_D_tour" runat="server" 
                            Width="200px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtsubnewadditionof_D_tour"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                
                        <asp:TextBox ID="txtnewadditionof_D_tour" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtnewadditionof_D_tour"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                      <small>
                       <asp:Label ID="Label7" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblnewadditionof_I_tour" runat="server" Text="New International Tour Added in CRM :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubnewadditionof_I_tour" runat="server" 
                            Width="200px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtsubnewadditionof_I_tour"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtnewadditionof_I_tour" runat="server" TextMode="MultiLine" Rows="3"
                            Width="400px" ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtnewadditionof_I_tour"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                      <small>
                          <asp:Label ID="Label8" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcond1" runat="server" Text="If Inquiry Status is Book and Token Amount is 0 or Blank :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubcond1" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtsubcond1"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                   
                        <asp:TextBox ID="txtcond1" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcond1"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                     <small>
                         <asp:Label ID="Label9" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcond2" runat="server" Text="Missed Next Follow-up Date :"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubcond2" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtsubcond2"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                  
                        <asp:TextBox ID="txtcond2" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtcond2"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                       <small>
                           <asp:Label ID="Label10" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 130px">
                        <asp:Label ID="lblcoond3" runat="server" Text="Varition in Avalible sheats to Booked sheets for Tour:"></asp:Label>
                        <span class="error">*</span>
                    </td>
                    <td style="width: 400px">
                     <asp:TextBox ID="txtsubcoond3" runat="server" 
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtsubcoond3"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                   
                        <asp:TextBox ID="txtcoond3" runat="server" TextMode="MultiLine" Rows="3" Width="400px"
                            ValidationGroup="vgSmtp"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtcoond3"
                            ValidationGroup="vgEMAIL" Display="Dynamic" ErrorMessage="<br />Required" CssClass="error"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                         <small>
                           <asp:Label ID="Label11" runat="server" Text="Allowed Tag {_Identity_}">
                            </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgEMAIL" OnClick="btnSaveAlertMsgFormat_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

