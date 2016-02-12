<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="totalinquirycreated.ascx.cs" Inherits="CRM.WebApp.Views.Charts.Customer.totalinquirycreated" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:chart id="Chart1" runat="server" 
    ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" imagetype="Png" 
    BackColor="White" BorderWidth="2" BackGradientStyle="TopBottom" 
    BackSecondaryColor="White" Palette="None" BorderDashStyle="Solid" 
    BorderColor="26, 59, 105" Height="250px" Width="250px" 
    PaletteCustomColors="5, 141, 199">
								<legends>
									<asp:legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:legend>
								</legends>
								<series>
									</series>
								<chartareas>
									<asp:chartarea Name="ChartArea1" BorderColor="64, 64, 64, 64" 
                                        BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="Transparent" 
                                        ShadowColor="Transparent" BackGradientStyle="TopBottom">
										<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
										<axisy linecolor="64, 64, 64, 64" IsLabelAutoFit="False">
											<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
											<majorgrid linecolor="64, 64, 64, 64" />
										</axisy>
										<axisx linecolor="64, 64, 64, 64" IsLabelAutoFit="False">
											<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
											<majorgrid linecolor="64, 64, 64, 64" />
										</axisx>
									</asp:chartarea>
								</chartareas>
							</asp:chart>
			