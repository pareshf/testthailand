﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bookingstatus.ascx.cs" Inherits="CRM.WebApp.Views.Charts.Customer.bookingstatus" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
   		<table cellpadding="0" cellspacing="0">
			<tr>
				<td>
					<asp:chart id="Chart1" runat="server" Width="250px" 
                        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Palette="None" 
                        BorderDashStyle="Solid" BackSecondaryColor="White" Height="250px"
                        BorderWidth="2px" BorderColor="#1A3B69" 
                        PaletteCustomColors="Teal" 
                       >
						<legends>
							<asp:Legend IsTextAutoFit="False" Name="Default" BackColor="Transparent" 
                                Font="Trebuchet MS, 8.25pt, style=Bold" Enabled="False"></asp:Legend>
						</legends>
						<series>
							<asp:Series Name="Area" BorderColor="180, 26, 59, 105" ChartType="Area">
							</asp:Series>
						</series> 
						<chartareas>
							<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" 
                                BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="White" 
                                ShadowColor="Transparent" BackGradientStyle="TopBottom">
							<axisy linecolor="64, 64, 64, 64">
									<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
									<majorgrid linecolor="64, 64, 64, 64" />
								</axisy>
								<axisx linecolor="64, 64, 64, 64">
									<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
									<majorgrid linecolor="64, 64, 64, 64" />
								</axisx>
							</asp:ChartArea>
						</chartareas>
					    <BorderSkin BackColor="Transparent" BorderColor="Gray" />
					</asp:chart>
                 
                   
                </td>
			</tr>
		</table>