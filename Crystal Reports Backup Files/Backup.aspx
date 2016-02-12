<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="Backup.aspx.cs" Inherits="CRM.WebApp.Views.Administration.Backup"
    Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle">
        <asp:Literal ID="lblPageAddress" runat="server" Text="Backup"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th colspan="2">
                        <asp:Literal runat="server" ID="ltlTableHeader" Text="New Backup"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label ID="lblFileName" runat="server" Text="FileName :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFileName" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                        <asp:Label ID="lblBackupType" runat="server" Text="Backup Type :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbBackupType" runat="server" RepeatDirection="Vertical">
                            <asp:ListItem Text="Only Database" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Full Backup" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnBackup" runat="server" Text="Backup" OnClick="btnBackup_onClick" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBackup" />           
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                <tr>
                    <th colspan="2">
                        <asp:Literal runat="server" ID="Literal1" Text="New Backup"></asp:Literal>
                    </th>
                </tr>                
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="rgBackup" runat="server" AllowPaging="true" AllowSorting="true"
                            Width="50%">
                            <ClientSettings>
                                <Selecting AllowRowSelect="true"></Selecting>
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="25px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="unChkBackup">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBackup" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="Backup File" DataField="BACKUP_FILE_NAME" UniqueName="unBackupFileName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Size (in MB)" DataField="FILE_LENGTH" UniqueName="unFileLength">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Created Date" DataField="CREATED_DATE" UniqueName="unCreatedDate">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 2px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">                        
                        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>            
            <asp:PostBackTrigger ControlID="btnDownload" />
            <asp:AsyncPostBackTrigger ControlID="btnBackup" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
