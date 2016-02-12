<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportingMail.aspx.cs" Inherits="CRM.WebApp.Views.Administration.ReportingMail" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    
    <div>
        <div class="pageTitle" style="float: left">
            <br />
            <asp:Literal ID="Literal6" runat="server" Text="Employee Inquiry Report"></asp:Literal>
        </div>
        <br />
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Inquiry From Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" runat="server" TabIndex="1" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTodate" runat="server" Text="Inquiry To Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTodate" runat="server" TabIndex="1" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnemail" runat="server" Text="Sent Report" OnClick="btnemail_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="grdiemail" runat="server" ShowFooter="True" AutoGenerateColumns="False" BorderColor="#85888A" BorderWidth="1px" CellSpacing="0" GridLines="None">
                        <HeaderStyle BackColor="#85888A" ForeColor="White" HorizontalAlign="Left" Height="25px" />
                        <RowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField HeaderText="EMP_NAME" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="EMP_NAME" runat="server" Width="100px" MaxLength="25"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMP_SURNAME" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="EMP_SURNAME" runat="server" Width="100px" MaxLength="5"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TO_EMAIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="TO_EMAIL" runat="server" CssClass="textbox" Width="100px" MaxLength="5"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="USER_NAME" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="USER_NAME" runat="server" CssClass="textbox" Width="100px" MaxLength="5"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DEPARTMENT_NAME" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="DEPARTMENT_NAME" runat="server" CssClass="textbox" Width="100px" MaxLength="3"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText=" REPORTING_TO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="REPORTING_TO" runat="server" CssClass="textbox" Width="100px" MaxLength="3"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="FROM_EMAIL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="FROM_EMAIL" runat="server" CssClass="textbox" Width="100px" MaxLength="3"></asp:TextBox><br />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
     <div>
        
            <rsweb:reportviewer ID="rptViewer1" runat="server" BorderColor="Silver" 
                BorderStyle="Solid" BorderWidth="1px" Height="8.5in" Width="14in">
            </rsweb:reportviewer>
        
     </div>
   
</asp:Content>
