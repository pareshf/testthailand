<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CreateEmail.aspx.cs" Inherits="CRM.WebApp.Views.EmailSettings.CreateEmail" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
    <script type="text/javascript" src="../Shared/Javascripts/jquery-latest.js"></script>
    <script type="text/javascript" src="../Shared/Javascripts/jquery.validate.js"></script>
    <div>
        <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <%--<asp:Label ID="lblhotel" runat="server" Text="Accounts Voucher" class="pageTitle" Width="150px"
         style="font-weight:normal; font-size:16px; font-family:Verdana"></asp:Label>--%>
                <asp:Label ID="Label63" runat="server" Text="Email Format Entry" class="pageTitle"
                    Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
                <br />
                <br />

               <table width="800px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse: collapse;
                    border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                
                <tr>
                
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Event Name"></asp:Label>
                        &nbsp;<span class="error">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="drp_eventname" runat="server" Width ="200px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                runat="server" ErrorMessage="Event Name is Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="drp_eventname" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Email Description"></asp:Label></td>
                    <td>
                       
                        <asp:TextBox ID="txt_description" runat="server" TextMode="MultiLine" 
                            Height="80px" Width="400px"></asp:TextBox>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="From Adress User Type"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       
                        <asp:DropDownList ID="drp_fromadresstype" runat="server" Width ="200px">
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server" ErrorMessage="From Address type is Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="drp_fromadresstype" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>

                 <tr>
                
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="To Adress User Type"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       
                       <asp:DropDownList ID="drp_toadresstype" runat="server" Width ="200px">
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                runat="server" ErrorMessage="To  Address type is Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="drp_toadresstype" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="CC Adress User Type"></asp:Label></td>
                    <td>
                       
                        <asp:DropDownList ID="drp_ccadresstype" runat="server" Width ="200px">
                        </asp:DropDownList>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="BCC Adress User Type"></asp:Label></td>
                    <td>
                       
                        <asp:DropDownList ID="drp_bccadresstype" runat="server" Width ="200px">
                        </asp:DropDownList>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Email Subject"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       
                        <asp:TextBox ID="txt_subject" runat="server" TextMode="MultiLine" Width="400px" 
                            Height="80px"></asp:TextBox>

                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                runat="server" ErrorMessage="Subject type is Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="txt_subject" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Email Body"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       
                        <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" BasePath="~/ckeditor/"
                                        Width="630"></CKEditor:CKEditorControl>

                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                runat="server" ErrorMessage="Main Body type is Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="CKEditorControl1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                
                </tr>

                <tr>
                
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Auto Email"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes" GroupName="Auto"/>
                       <asp:RadioButton ID="RadioButton2" runat="server" Text="No" GroupName="Auto"/>
                    </td>
                    
                </tr>

                 <tr>
                
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="ON/OFF Email"></asp:Label>&nbsp;<span class="error">*</span></td>
                    <td>
                       <asp:RadioButton ID="RadioButton3" runat="server" Text="ON" GroupName="onoffemail"/>
                       <asp:RadioButton ID="RadioButton4" runat="server" Text="OFF" GroupName="onoffemail"/>
                    </td>
                    
                </tr>
                
                </table>
                <br />
                <br />
                <table >
                <tr>
                <td>
                <asp:Button id="btnsave" runat="server" Text ="Save" onclick="btnsave_Click"/>
                </td>
                </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
</asp:Content>
