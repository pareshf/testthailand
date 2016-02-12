<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InquiryBounce.ascx.cs" Inherits="CRM.WebApp.Views.Charts.Customer.InquiryBounce" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
   
						<asp:CHART id="Chart1" runat="server" Palette="None"
                            Height="250px" Width="250px" BorderDashStyle="Solid" 
                            BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105" 
                            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" RightToLeft="Yes" 
                            
                            PaletteCustomColors="5, 141, 199; 80, 180, 50; 247, 221, 14; 237, 86, 27; 255, 178, 13; 147, 54, 168; 122, 126, 129; ">
							<legends>
								<asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" 
                                    BackColor="Transparent" IsEquallySpacedItems="True" 
                                    Font="Trebuchet MS, 8pt, style=Bold" IsTextAutoFit="False" Name="Default" 
                                    LegendStyle="Column"></asp:Legend>
							</legends>
							<series>
								<asp:Series ChartArea="Area1" XValueType="Single" Name="Series1" 
                                    ChartType="Pie" Font="Trebuchet MS, 8.25pt, style=Bold" 
                                    CustomProperties="DoughnutRadius=2, PieLineColor=White, CollectedSliceExploded=True, CollectedLabel=Other, MinimumRelativePieSize=20" 
                                    MarkerStyle="Circle" BorderColor="White" Color="180, 65, 140, 240" 
                                    YValueType="Single" Label="#PERCENT{P1}">
									<points>
										
									</points>
								</asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="Area1" BorderColor="White" BackSecondaryColor="Transparent" BackColor="Transparent" ShadowColor="Transparent" BackGradientStyle="TopBottom">
									<axisy2>
										<MajorGrid Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisy2>
									<axisx2>
										<MajorGrid Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisx2>
									<Position Height="65" Width="65" Y="10" />
									<area3dstyle PointGapDepth="900" Rotation="162" IsRightAngleAxes="False" 
                                        WallWidth="2" IsClustered="True" lightstyle="None" />
									<axisy LineColor="White">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisy>
									<axisx LineColor="White">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" Enabled="False" />
										<MajorTickMark Enabled="False" />
									</axisx>
								</asp:ChartArea>
							</chartareas>
						</asp:CHART>
				