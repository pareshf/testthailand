<%@ Control Language="C#" AutoEventWireup="true" Inherits="Dock_Examples_Default_ExchangeRates" Codebehind="ExchangeRates.ascx.cs" %>
 <br />
 Base currency:
 <br />
 <asp:dropdownlist runat="server" id="BaseCurrencyList" enableviewstate="false" />
 <br /><br />
 Foreign currency:
 <br />
 <asp:dropdownlist runat="server" id="ForeignCurrencyList" enableviewstate="false" />
 <br /><br />
 <asp:button runat="server" id="SubmitBtn" text="Calculate Rate" onclick="SubmitBtn_Click" />
 <br /><br />
 <asp:updatepanel runat="server" id="UpdatePanelHoroscopes">
	<contenttemplate>
		<asp:literal runat="server" id="Result" />
	</contenttemplate>
	<triggers>
		<asp:asyncpostbacktrigger controlid="SubmitBtn" />
	</triggers>
</asp:updatepanel>
 <br />