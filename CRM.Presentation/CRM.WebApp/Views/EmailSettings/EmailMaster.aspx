<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="EmailMaster.aspx.cs" Inherits="CRM.WebApp.Views.EmailSettings.EmailMaster" %>


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
     <div>
        <asp:Label runat="server" Text="Search Email Trail" ID="headlbl" Width="300px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search_onclick" Style="float: right;
                                margin-right: 10px; display: block; color: black;" CssClass="button" />
                            <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                display: none; color: black;" CssClass="button" OnClick="searchnow_onclick" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                     <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Quote ID" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuoteId" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpagent" runat="server" Width="250px"></asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfromdate" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" Width="938px" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="VIEW"/>
                                     <asp:BoundField DataField="EMAIL_TRAIL_MASTER_ID" HeaderText="Master ID" />
                                    <asp:BoundField DataField="QUOTE_ID" HeaderText="Quotation ID" />
                                    <asp:BoundField DataField="FROM_DATE" HeaderText="Date" />
                                    <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" />
                                   
                                 </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>                
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
</asp:Content>
