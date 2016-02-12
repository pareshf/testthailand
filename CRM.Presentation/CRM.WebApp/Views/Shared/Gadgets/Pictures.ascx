<%@ Control Language="C#" AutoEventWireup="true" Inherits="Dock_Examples_Default_Pictures" Codebehind="Pictures.ascx.cs" %>
<asp:updatepanel runat="server" id="UpdatePanelPictures">
    <contenttemplate>
        <asp:formview id="ImagesView" runat="server" allowpaging="true" pagersettings-mode="NextPrevious" 
            onpageindexchanging="ImagesView_PageIndexChanging"  >
            <itemtemplate>
                <asp:hyperlink runat="server" id="Image" imageurl='<%#Eval("ThumbURL") %>' navigateurl='<%#Eval("ImageURL") %>' target="_blank" />
            </itemtemplate>
        </asp:formview>
    </contenttemplate>
</asp:updatepanel>