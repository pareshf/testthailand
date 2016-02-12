<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="NewChart.aspx.cs" Inherits="CRM.WebApp.Views.Account.NewChart" %>

<%@ Register Src="~/Views/Charts/Account/IncomeComaparasion.ascx" TagName="IncomeChart"
    TagPrefix="uc1" %>

    <%@ Register Src="~/Views/Charts/Account/AgentBySales.ascx" TagName="AgentChart"
    TagPrefix="uc3" %>

    <%@ Register Src="~/Views/Charts/Account/ExpenceComparasion.ascx" TagName="ExpenceChart"
    TagPrefix="uc2" %>

     <%@ Register Src="~/Views/Charts/Account/ExpenseBreakDown.ascx" TagName="ExpenceBreakdownChart"
    TagPrefix="uc4" %>

    <%@ Register Src="~/Views/Charts/Account/IncomeExpense.ascx" TagName="IncomeExpense"
    TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<table width="100%">

<tr>

<%--<td>

</td>--%>
<td>
<asp:Label ID = "lbl1" runat="server" Text = "Prev Year Income Comparasion"></asp:Label>
<uc1:IncomeChart ID="chart1" runat="server" />

</td>

<td>
<asp:Label ID = "Label1" runat="server" Text = "Prev Year Expense Comparasion"></asp:Label>
<uc2:ExpenceChart ID="chart2" runat="server" />
</td>

</tr>

</table>
<br />
<br />
<table width="100%">
<tr>
<td>
<asp:Label ID = "Label2" runat="server" Text = "Top Customers By Sales"></asp:Label>
<uc3:AgentChart ID="chart3" runat="server"/>
</td>

<td>
<%--<asp:Label ID = "Label3" runat="server" Text = "Expense Break Down"></asp:Label>
<uc4:ExpenceBreakdownChart ID="AgentChart1" runat="server"/>--%>
</td>
</tr>
</table>

<br />
<br />

<table>
<tr>
<td>
<%--<asp:Label ID = "Label4" runat="server" Text = "Income Expense Comparasion"></asp:Label>
<uc5:IncomeExpense ID="ExpenceBreakdownChart1" runat="server"/>--%>
</td>

</tr>
</table>
</asp:Content>
