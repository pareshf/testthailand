<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="SightSeeingVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.SightSeeingVoucher" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        <asp:Label runat="server" Text="Sight Seeing Voucher Detail" ID="headlbl" Width="400px"
            Font-Bold="true" Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateservicevoucher" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false" class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="lblvoucher" runat="server" Text="Voucher No." CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVoucherNo" runat="server" Width="150px" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblagent" runat="server" Text="Agent" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAgent" runat="server" Width="255px" AutoPostBack="true"
                                TabIndex="3" OnSelectedIndexChanged="drp_Agent_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Against Sales Invoice" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" Width="255px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcity" runat="server" Text="City" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpCity" runat="server" Width="255px" AutoPostBack="true" TabIndex="5"
                                OnSelectedIndexChanged="drp_City_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblpaxname" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpaxname" runat="server" Width="255px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="800px">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblsrno" runat="server" Text="Sr No" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSS_Ptysrno" runat="server" Text='<%# Bind("SR_NO") %>' CssClass="lblstyle"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSS_PtypackageDetails" runat="server" Text='<%# Bind("SIGHT_SEEING_PACKAGE_NAME") %>'
                                                CssClass="lblstyle"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="SIC / PVT" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true">
                                                <asp:ListItem>SIC</asp:ListItem>
                                                <asp:ListItem>PVT</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle2" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSS_Ptydate" runat="server" CssClass="textboxstyle" Width="80px"
                                                AutoPostBack="true" OnTextChanged="txtSS_Ptydate_TextChanged"></asp:TextBox><ajax:textboxwatermarkextender
                                                    id="TextBoxWatermarkExtender1" runat="server" targetcontrolid="txtSS_Ptydate"
                                                    watermarktext="dd/MM/yyyy">
                                                </ajax:textboxwatermarkextender>
                                            <%--" --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle3" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drp_ss_time" runat="server" AutoPostBack="true" />
                                        </ItemTemplate>
                                        <%--OnSelectedIndexChanged="drp_pty_time_selectedindexchanged"--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle4" runat="server" Text="No of meals" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSS_PtynoofMeals" runat="server" CssClass="textboxstyle" Width="80px"
                                                Text="1"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle4" runat="server" Text="Select" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cb_pty_select" runat="server" GroupName="sightseeingBKK" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpty_meal" runat="server" Text='<%# Bind("IS_MEAL_APPLICABLE") %>'
                                                CssClass="headlabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="Due_Date" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtptyssdate" runat="server" CssClass="textboxstyle" Visible="false"
                                                Width="70px"></asp:TextBox><ajax:textboxwatermarkextender id="TextBoxWatermarkExtenderptydate"
                                                    runat="server" targetcontrolid="txtptyssdate" watermarktext="dd/MM/yyyy">
                                                </ajax:textboxwatermarkextender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="phu_fees" runat="server" Text="Fees" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtssptyfees" runat="server" CssClass="textboxstyle" Width="100px"
                                                Visible="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpty_SIGHTID" runat="server" Text='<%# Bind("SIGHT_SEEING_PRICE_ID") %>'
                                                CssClass="headlabel"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="9" />
                        </td>
                        <td>
                            <%--<asp:Button ID="Button5" runat="server" Text="Download Quote" Width="150px" style="display:none;" OnClick="btnDownloadQuote_Click" />--%>
                            <a id="lnkbtn" runat="server" style="font-family: Verdana; font-size: 12px; display: none;">
                                Download Voucher</a>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <form id="form2" runat="server" visible="false">
        <div>
            <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                BorderWidth="1px" Height="8.5in" Width="14in">
            </rsweb:ReportViewer>
        </div>
        </form>
    </div>
</asp:Content>
