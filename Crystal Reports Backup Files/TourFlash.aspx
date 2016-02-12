<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="TourFlash.aspx.cs" Inherits="CRM.WebApp.Views.Administration.TourFlash" ValidateRequest="false"
    Title="" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ControlBox.ascx" TagName="ControlBox"
    TagPrefix="crmUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Shared/Javascripts/Common.js" type="text/javascript">
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="pageTitle">
                <asp:Literal ID="lblPageAddress" runat="server" Text="Tour Flash"></asp:Literal>
            </div>
            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4" border="0">
                <tr>
                    <th>
                        <asp:Literal runat="server" ID="ltlTableHeader" Text="Tour Flash"></asp:Literal>
                    </th>
                    <th>
                        <div class="MandatoryNote" align="right">
                            <asp:Literal ID="ltlMandatoryNote" runat="server">Fields marked with <span class="error">*</span> are mandatory.&nbsp;</asp:Literal>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Top-Left :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTopLeft" runat="server" Rows="5" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Top-Right :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTopRight" runat="server" Rows="5" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Bottom-Left :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBottomLeft" runat="server" Rows="5" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Bottom-Right :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBottomRight" runat="server" Rows="5" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <crmUC:ControlBox ID="ControlBox1" runat="server" SaveButtonValidationGroup="Save"
                VisibleCopyButton="false" VisibleSaveNewButton="false" OnbtnCancelClick="ControlBox1_ResetClick"
                OnbtnClearClick="ControlBox1_ClearClick" OnbtnSaveClick="ControlBox1_SaveClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
