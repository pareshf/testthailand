<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PanelBarNavigation.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.PanelBarNavigation" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
<div style="background-color: #fff;">
    <telerik:radpanelbar id="RadPanelBar2" runat="server" width="152px" skin="Default"
        enableembeddedskins="false" allowcollapseallitems="true">
        <CollapseAnimation Duration="100" Type="None" />
        <ExpandAnimation Duration="100" Type="None" />
    </telerik:radpanelbar>
</div>
<style type="text/css">
    .RadMenu_Sitefinity UL.rmRootGroup
    {
        background-color: #fff;
        border: solid 0px #fff;
    }
    .RadMenu rmLink
    {
        padding-left: 0px;
    }
    .RadMenu_Sitefinity .rmLink
    {
        color: #000;
    }
    .RadMenu_Sitefinity .rmVertical .rmItem .rmLink .rmText
    {
        border: solid 0px #fff;
    }
    .RadMenu_Sitefinity .rmVertical .rmItem .rmLink .rmText:hover
    {
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadMenu_Sitefinity .rmVertical .rmItem:hover
    {
        border: solid 0px #fff;
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadMenu_Sitefinity .rmVertical .rmItem .rmLink:active
    {
        border: solid 0px #fff;
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadMenu_Sitefinity .rmVertical .rmItem .rmLink:active
    {
        border: solid 0px #fff;
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadMenu_Sitefinity .rmGroup .rmItem .rmLink
    {
        color: #000;
    }
    .RadMenu_Sitefinity .rmGroup .rmItem .rmLink:hover
    {
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadPanelBar_Default .rpSlide
    {
        padding-left: 2px;
    }
    .RadMenu_Sitefinity .rmLink:active
    {
        background-color: #a4a2a3;
        color: #fff;
    }
    .RadMenu_Sitefinity .rmLink:active
    {
        background-color: #a4a2a3;
        color: #fff;
    }
    
     .RadPanelBar .rpItem
    {
        overflow: visible !important;
    }
   .RadPanelBar,
      .RadPanelBar .rpSlide,
     .RadPanelBar .rpGroup,
     .RadPanelBar .rpItem,
     .RadPanelBar .rpTemplate
        {
            overflow:visible !important;
        }
         
        div.RadPanelBar .rpLevel1 .rpItem
        {
            padding:0;
        }
         
        * html .RadPanelBar .RadMenu ul.rmRootGroup
        {
            zoom: 1;
        }
         
        div.RadMenu .rmRootGroup
        {
            border: 0;
        }
         
        div.RadMenu .rmLink
        {
            float: none;
        }
    
    
   
</style>
