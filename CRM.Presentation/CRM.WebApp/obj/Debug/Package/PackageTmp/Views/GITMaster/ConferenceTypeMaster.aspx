<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ConferenceTypeMaster.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.ConferenceTypeMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">


 <div>
        <asp:Label ID="Label55" runat="server" Text="Conference Type Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
             <br />
             <br />

             <asp:UpdatePanel ID="upConferenceType" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="pageTitle">
                <table>
                 <tr>
                        <td >
                            <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:TextBox ID="txtConferenceType" runat="server" Width="200px" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>          
                             
                </table>
            </div>         
           
           </ContentTemplate>
           </asp:UpdatePanel>
             <div class="pageTitle" id="div1">
                <table>
                <tr>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text = "Save" Width="100px" OnClick="btnSave_Click" />&nbsp; 
                    <asp:Button runat="server" ID="btnEdit" Text="Edit"  Width="100px" OnClick="btnEdit_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnDelete" Text="Delete"  Width="100px" OnClick="btnDelete_Click" />&nbsp;
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel"  Width="100px" OnClick="btnCancle_Click" />
                </td>
                <td>
                    
                </td>
                </tr>
                </table>
            </div>

             <br />

             <div class="pageTitle">
           <asp:UpdatePanel ID="upConferenceTypegrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
           <ContentTemplate>

                <asp:GridView runat="server" ID="gridConferenceType" AutoGenerateColumns="false" AllowPaging="true" SkinID="sknSubGrid" Width="300px" OnPageIndexChanging="GV_time_PageIndexChanging" PageSize = "10">
                
                <Columns>                     
                 <asp:TemplateField ControlStyle-Width="20px">
                        <HeaderTemplate>
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckChanged" ></asp:RadioButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField ControlStyle-Width="20px" Visible="false">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="ConferenceType ID" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblConferenceTypeID" runat="server" Text='<%# Bind("CONFERENCE_TYPE_ID") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField ControlStyle-Width="200px">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblConferenceType" runat="server" Text='<%# Bind("CONFERENCE_TYPE") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                     
                </Columns>
                </asp:GridView>
              
           </ContentTemplate>
           </asp:UpdatePanel>
           </div>
</div>
</asp:Content>
