<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    CodeBehind="Dashboard.aspx.cs" Inherits="CRM.WebApp.Views.Workplace.Dashboard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <script type="text/javascript">
        var zones = [];

        function MaximizeMinimize(dock, args) {

            if (args.command.get_name() == "MaximizeMinimize") {

                if (dock.get_dockZoneID() != "ctl00_cphPageContent_RadDockZoneFullMode") {

                    var minimizeZone = $find("ctl00_cphPageContent_RadDockZoneFullMode");

                    //dock.set_width(200);
                    args.command.set_text("Minimize");
                    dock.repaint();
                    zones[dock.get_id()] = dock.get_dockZoneID();
                    minimizeZone.dock(dock);

                }
                else {
                    args.command.set_text("Maximize");
                    $find(zones[dock.get_id()]).dock(dock);
                }

                args.set_cancel(true);
            }
        }
        function Calendarfrom_OnDateClick(calendarInstance, args) {
            //set set_Cancel(true) to cancel AutoPostBackOnDayClick - if any;
            if (args.get_renderDay().get_date() != null) {
                var calday = args.get_renderDay().get_date()[2];
                if (calday <= 9)
                    calday = '0' + calday;
                var calmon = args.get_renderDay().get_date()[1];
                if (calmon <= 9)
                    calmon = '0' + calmon;
                var calyear = args.get_renderDay().get_date()[0];
            }
            document.getElementById('ctl00_cphPageContent_txtfrom').value = calmon + '/' + calday + '/' + calyear;
        };
        function Calendarto_OnDateClick(calendarInstance, args) {
            //set set_Cancel(true) to cancel AutoPostBackOnDayClick - if any;
            if (args.get_renderDay().get_date() != null) {
                var calday = args.get_renderDay().get_date()[2];
                if (calday <= 9)
                    calday = '0' + calday;
                var calmon = args.get_renderDay().get_date()[1];
                if (calmon <= 9)
                    calmon = '0' + calmon;
                var calyear = args.get_renderDay().get_date()[0];
            }
            document.getElementById('ctl00_cphPageContent_txtTo').value = calmon + '/' + calday + '/' + calyear;
        };
        function clearallfields() {
            document.getElementById('ctl00_cphPageContent_txtfrom').value = '';
            document.getElementById('ctl00_cphPageContent_txtTo').value = '';
            document.getElementById('ctl00_cphPageContent_ddlEmployee')[0].selected = true;
            document.getElementById('ctl00_cphPageContent_ddlfilter')[0].selected = true;
        }
    </script>
    <style>
        .RadDock
        {
            margin-top: 0px !important;
            margin-right: 0px !important;
            margin-bottom: 20px !important;
            margin-left: 0px !important;
        }
        .borderline
        {
            border-style: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="false" UpdateMode="Conditional" >
        <ContentTemplate>
            <div style="width: 100%; float: left;">
                <div class="pageTitle" style="float: left; width: 100%; vertical-align: middle">
                    <asp:Literal ID="lblPageTitle" runat="server" Text="Dashboard"></asp:Literal>
                    <div style="width: 880px;" align="right">
                        <asp:ImageButton ID="imgBtnCustomize" runat="server" ImageUrl="~/Views/Shared/Images/dashboard_customize_button.gif"
                            OnClientClick="return false" />
                        <asp:Panel ID="pnlGadgetsBox" runat="server">
                            <table align="right" class="exportpanelbg" cellpadding="5" >
                                <tr>
                                    <td colspan="3" valign="top" align="left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:DataList ID="dtlsgadget" runat="server" SelectedItemStyle-BackColor="#c2c2c2"
                                                        RepeatDirection="Horizontal" RepeatColumns="4" CellPadding="5">
                                                        <ItemTemplate>
                                                            <div align="left">
                                                                <asp:CheckBox ID="chk1" runat="server" />&nbsp;&nbsp;
                                                                <asp:Label ID="lblGadgeturl" runat="server" Text='<%#Eval("GADGET_URL") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblgadgetname" runat="server" Text='<%#Eval("GADGET_NAME") %>'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <telerik:radcalendar id="radcalfrom" runat="server" width="100px" height="50px" enablemultiselect="false">
                       <clientevents ondateclick="Calendarfrom_OnDateClick"></ClientEvents>
                       </telerik:radcalendar>
                                    </td>
                                    <td valign="top" align="right">
                                        <telerik:radcalendar id="radcalto" runat="server" width="100px" height="50px" enablemultiselect="false">
                         <clientevents ondateclick="Calendarto_OnDateClick"></ClientEvents>
                        </telerik:radcalendar>
                                    </td>
                                    <td valign="top" align="right">
                                        <span>From Date :</span>
                                        <asp:TextBox ID="txtfrom" runat="server"></asp:TextBox><br />
                                        <br />
                                        <span>To Date :</span>
                                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox><br />
                                        <br />
                                        <span>Filter :</span>
                                        <asp:DropDownList ID="ddlfilter" runat="server" Width="160px" OnSelectedIndexChanged="ddlfilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <span>Agent :</span><asp:DropDownList ID="ddlEmployee" runat="server" Width="160px">
                                        </asp:DropDownList>
                                        <div style="vertical-align: bottom; width: 100%;" align="right">
                                            <br />
                                            <asp:Button ID="btnload" Text="Apply" runat="server" OnClick="btnload_Click" />
                                            <asp:Button ID="btncancle" Text="Clear" runat="server" OnClientClick="return clearallfields()" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
                <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlGadgetsBox"
                    ExpandControlID="imgBtnCustomize" Collapsed="true" TextLabelID="TextToControl1"
                    ExpandedText="" CollapsedText="" ImageControlID="ImageToControl" ExpandedImage="~/Views/Shared/Images/dashboard_customize_button.gif"
                    CollapsedImage="~/Views/Shared/Images/dashboard_customize_button.gif" SuppressPostBack="False"
                    CollapseControlID="imgBtnCustomize">
                </ajax:CollapsiblePanelExtender>
            </div>
            <div>
                <br />
                <table style="background-color: #ffffff; margin-left:10px;">
                    <tr>
                        <td valign="top">
                        <%--<telerik:RadDockLayout runat="server" ID="RadDockLayout1">--%>
                <telerik:raddocklayout runat="server" id="RadDockLayout1" onsavedocklayout="RadDockLayout1_SaveDockLayout"
                    onloaddocklayout="RadDockLayout1_LoadDockLayout">
                    <br /><br /><br />
                    <table cellspacing="0" width="900px" align="left"><tr><td valign="top" style="padding:0px;margin-bottom:0px;"><telerik:RadDockZone runat="server" ID="RadDockZone1" Width="370px"  Orientation="Vertical" FitDocks="true" Style="border-style:none;">
                                    </telerik:RadDockZone> </td><td valign="top" style="padding:0px;"><telerik:RadDockZone runat="server" ID="RadDockZone2" Orientation="Vertical" Width="370px"  FitDocks="true" Style="border-style:none;">
                                    </telerik:RadDockZone></td><td valign="top" style="padding:0px;"><telerik:RadDockZone runat="server" ID="RadDockZone3" Orientation="Vertical" Width="370px" FitDocks="true" Style="border-style:none;">
                                    </telerik:RadDockZone></td>
                                    </tr>
                                  </table>

                </telerik:raddocklayout>
                        </td>
                    </tr>
                </table>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




    <div style="width: 0px; height: 0px; overflow: hidden; position: absolute; left: -10000px;">
        Hidden UpdatePanel, which is used to help with saving state when minimizing, moving
        and closing docks. This way the docks state is saved faster (no need to update the
        docking zones).
        <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
