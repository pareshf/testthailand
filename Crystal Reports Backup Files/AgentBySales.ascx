<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgentBySales.ascx.cs"
    Inherits="CRM.WebApp.Views.Charts.Account.AgentBySales" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Chart ID="Chart1" runat="server" Height="250px" Width="300px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
                Palette="None" BorderDashStyle="Solid" BackSecondaryColor="White" BorderWidth="2px"
                BorderColor="#1A3B69" PaletteCustomColors="5, 141, 199">
                <Legends>
                    <asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                        Enabled="False" >
                    </asp:Legend>
                </Legends>
                <Series>
                    <asp:Series Name="Bar" BorderColor="180, 26, 59, 105" ChartType="Bar">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                        <AxisY LineColor="64, 64, 64, 64" Title="Amount (THB)">
                            <MajorGrid Enabled="false"></MajorGrid>
                        </AxisY>
                        <AxisX LineColor="64, 64, 64, 64" IsStartedFromZero="true" Title="Agent Name">
                            <MajorGrid Enabled="false"></MajorGrid>
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
                <BorderSkin BackColor="Transparent" BorderColor="Gray" />
            </asp:Chart>
        </td>
    </tr>
</table>
