<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AgentReports.aspx.cs" Inherits="CRM.WebApp.Views.MIS.AgentReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

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
            font-size: "12px";
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 12px;
        }
        .textboxstyle
        {
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
        .errorclass
        {
            font-family: Verdana;
            font-size: 12px;
            color: Red;
        }
        
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        
        
        .style6
        {
            width: 52px;
        }
        
        .style11
        {
            width: 205px;
        }
        .style12
        {
            width: 138px;
        }
        
        .style13
        {
            width: 214px;
        }
    </style>

    <asp:Label runat="server" Text="Agent Reports" ID="headlbl" Width="200px" Font-Bold="true"
        Font-Size="Large" class="pageTitle"></asp:Label>
    <br />
    <div id="paymentmode" class="pageTitle">
        <table width="475px" cellspacing="5" cellpadding="5">
            <tr>
                <td width="220px">
                    <asp:Label runat="server" Text="Agent Comapny Name" ID="payment_mode" CssClass="lblstyle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drp_agent" runat="server" Width="233px" 
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td width="220px">
                    <asp:Label runat="server" Text="From Quoted Date" ID="Label1" CssClass="lblstyle"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txt_fromdate" runat="server"></asp:TextBox>
     <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender21" runat="server" TargetControlID="txt_fromdate"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                </td>
            </tr>

            <tr>
                <td width="220px">
                    <asp:Label runat="server" Text="To Quoted Date" ID="Label2" CssClass="lblstyle"></asp:Label>
                </td>
                <td>
                     <asp:TextBox ID="txt_todate" runat="server"></asp:TextBox>
     <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender22" runat="server" TargetControlID="txt_todate"
                                    WatermarkText="dd/MM/yyyy" WatermarkCssClass="watermarked">
                                </ajax:TextBoxWatermarkExtender>
                </td>
            </tr>
        </table>
        <br />
        <table>
        <tr>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Show Report" 
                onclick="Button1_Click" />
        </td>
        </tr>
        </table>
    </div>
</asp:Content>
