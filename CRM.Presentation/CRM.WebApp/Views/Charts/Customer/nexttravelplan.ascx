<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="nexttravelplan.ascx.cs"
    Inherits="CRM.WebApp.Views.Charts.Customer.nexttravelplan" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Chart ID="Chart1" runat="server" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)"
    BorderWidth="2px" BackGradientStyle="TopBottom" BackSecondaryColor="White" BorderDashStyle="Solid"
    BorderColor="#1A3B69" Height="250px" Width="250px" Palette="None" PaletteCustomColors="5, 141, 199">
    <Legends>
        <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent"
            Font="Trebuchet MS, 8.25pt, style=Bold">
        </asp:Legend>
    </Legends>
    <Series>
        <asp:Series ChartArea="ChartArea1" ChartType="FastLine" Legend="Default" Name="Series1">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
            BackSecondaryColor="White" BackColor="Transparent" ShadowColor="Transparent"
            BackGradientStyle="TopBottom">
            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                WallWidth="0" IsClustered="False"></Area3DStyle>
            <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                <MajorGrid LineColor="64, 64, 64, 64" />
            </AxisY>
            <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                <MajorGrid LineColor="64, 64, 64, 64" />
            </AxisX>
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>
