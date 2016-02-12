<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AgentReminder.aspx.cs" Inherits="CRM.WebApp.Views.FIT.AgentReminder" %>
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
        <asp:Label runat="server" Text="FIT - All Bookings To be Reconfirmed" ID="headlbl" Width="600px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
           
                <table>
                    <tr>
                        <td><asp:Button runat="server" ID="btnSelect" Text = "Select All" OnClick="btnSelect_onclick" /></td>
                        <td><asp:Button runat="server" ID="btnReminder" Text = "Send Reminder" OnClick="btnReminder_onclick"/></td>
                    </tr>
                </table>

                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" 
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  pagesize="10"  OnPageIndexChanging="GV_Result_PageIndexChanging">
                                <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10" />
                           <%--     OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                               --%>
                                <Columns>
                                    
                                           <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Select" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <%--<asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />--%>
                                    <asp:BoundField DataField="QUOTE_ID" HeaderText="Quotation Refrence No" />
                                    <asp:BoundField DataField="TOUR_NAME" HeaderText="Tour Name" />
                                    <asp:BoundField DataField="FROM_DATE" HeaderText="From Date" />
                                    <asp:BoundField DataField="TO_DATE" HeaderText="To Date" />
                                    <asp:BoundField DataField="ORDER_STATUS" HeaderText="Status" />
                                    <asp:BoundField DataField="ADULT" HeaderText="No Adults" />
                                    <asp:BoundField DataField="CWB" HeaderText="No CWB" />
                                    <asp:BoundField DataField="CNB" HeaderText="No CNB" />
                                    <asp:BoundField DataField="TOUR_ID" HeaderText="Tour Id" />
                                     <asp:BoundField DataField="RECONFIRMATION_DATE" HeaderText="Cut of Date" />
                                     <asp:BoundField DataField="CUST_REL_NAME" HeaderText="Order By" />
                                     <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Company Name" />
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

           <asp:UpdateProgress ID="UpdateProgress22" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Updateconfirm">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage22" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
