<%@ control language="C#" autoeventwireup="true" inherits="Dock_Examples_Default_Horoscopes" Codebehind="Horoscopes.ascx.cs" %>
<asp:dropdownlist runat="server" id="DropDownSigns" enableviewstate="false" autopostback="true" 
	onselectedindexchanged="DropDownSigns_SelectedIndexChanged">
</asp:dropdownlist>
<asp:updatepanel runat="server" id="UpdatePanelHoroscopes">
	<contenttemplate>
		<asp:literal runat="server" id="Horoscope" />
	</contenttemplate>
	<triggers>
		<asp:asyncpostbacktrigger controlid="DropDownSigns" eventname="SelectedIndexChanged" />
	</triggers>
</asp:updatepanel>
