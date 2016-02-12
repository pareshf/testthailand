<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncomeExpense.ascx.cs" Inherits="CRM.WebApp.Views.Charts.Account.IncomeExpense" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>



    <table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSource1">
                <Series>
                    <asp:Series Name="Series1" ChartType="StackedColumn100" XValueMember="MONTH" 
                        YValueMembers="TOTAL_EXPENCE_AMOUNT">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:THAILAND_CRMConnectionString6 %>" 
                SelectCommand="GEDJET_INCOME_EXPENSE_COMPANRASION" 
                SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </td>
        </tr>
        </table>
