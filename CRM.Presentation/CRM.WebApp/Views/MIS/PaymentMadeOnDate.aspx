<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="PaymentMadeOnDate.aspx.cs" Inherits="CRM.WebApp.Views.MIS.PaymentMadeOnDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

<table>

<tr>
    <td>
        <asp:Label Text="Date" runat="server"></asp:Label>
    </td>

    <td>
        <asp:TextBox runat="server" ID="txtDate" Width="150px"></asp:TextBox>
           <ajax:calendarextender id="CalendarExtender8" runat="server" targetcontrolid="txtDate"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
    </td>
</tr>

<tr>

</tr>

<tr>
    <td colspan="2">

     <asp:Button ID="btnShow" runat="server" Text="Show Report" OnClick="btnshow_Click" ValidationGroup="Required" />

    </td>
</tr>

</table>


</asp:Content>
