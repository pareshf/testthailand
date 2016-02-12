<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AllGitPackages.aspx.cs" Inherits="CRM.WebApp.Views.GIT.AllGitPackages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">


       <script language="javascript" type="text/javascript">

           var sessionTimeout = "<%= Session.Timeout %>";

           var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
           setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>
  <div>
        <asp:Label ID="Label55" runat="server" Text="GIT All Packages" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="upda" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" Width="938px" OnPageIndexChanging="GV_Result_PageIndexChanging">
                                <pagersettings mode="NumericFirstLast" position="Bottom" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="GIT_TOUR_ID" HeaderText="Tour ID" />
                                    <asp:BoundField DataField="GIT_PACKAGE_NAME" HeaderText="Package Name" />
                                    <asp:BoundField DataField="GIT_GROUP_NAME" HeaderText="Group Name" />
                                    <asp:BoundField DataField="START_DATE" HeaderText="From Date" />
                                    <asp:BoundField DataField="END_DATE" HeaderText="To Date" />
                                    <asp:BoundField DataField="TOTAL_NO_OF_ROOMS" HeaderText="No. of Rooms" />
                                    <asp:BoundField DataField="CUST_REL_NAME" HeaderText="Order By" />
                                    <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" />
                                   
                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                            </ContentTemplate>
                            </asp:UpdatePanel>
       </div>

</asp:Content>
