<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="TransferServiceVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.TransferServiceVoucher" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
        <asp:Label runat="server" Text="Transfer Package Voucher Detail" ID="headlbl" Width="400px"
            Font-Bold="true" Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updatetransfervoucher" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false" class="pageTitle">
            <ContentTemplate>
                <table>
                   
                    <tr>
                        <td>
                            <asp:Label ID="lblagent" runat="server" Text="Agent" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAgent" runat="server" Width="255px" AutoPostBack="true"
                                OnSelectedIndexChanged="drp_Agent_SelectedIndexChanged" TabIndex="3">
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
                            <asp:Label ID="lblpaxname" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpaxname" runat="server" Width="250px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier Name" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpsupplier" runat="server" Width="255px" OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged"
                                AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </td>
                    </tr>
                 
                </table>
                <br />
                <table width="800px">
                    <tr>
                        <td>
                            <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                AllowPaging="false" OnRowDataBound="dlhoteldetails_ItemDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label26" runat="server" Text="Select" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rbtnTPselect" runat="server" GroupName="TransferPackage" OnCheckedChanged="CheckChanged"
                                                AutoPostBack="true" /><%--OnCheckedChanged="CheckChanged"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label21" runat="server" Text="Sr No" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPsrno" runat="server" CssClass="lblstyle" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label22" runat="server" Text="Package Details" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTPpackageDetails" runat="server" Text='<%# Bind("NAME") %>' CssClass="lblstyle"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label23" runat="server" Text="SIC / PVT" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drptransfer" runat="server" OnSelectedIndexChanged="drptransfer_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Selected="True">SIC</asp:ListItem>
                                                <asp:ListItem>PVT</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:Label ID="Label24" runat="server" Text="Date" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTPdate" runat="server" CssClass="textboxstyle" Width="100px"></asp:TextBox><ajax:textboxwatermarkextender
                                                id="TextBoxWatermarkExtendertp" runat="server" targetcontrolid="txtTPdate" watermarktext="dd/MM/yyyy">
                                            </ajax:textboxwatermarkextender>
                                            <%--OnTextChanged="txtTPdate_TextChanged"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:Label ID="Label25" runat="server" Text="Time" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="tpdrp_time" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <%--OnSelectedIndexChanged="tpdrp_time_SelectedIndexChanged"--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderTemplate>
                                            <asp:Label ID="Label69" runat="server" Text="Payment Date" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtduedate" runat="server" CssClass="textboxstyle" Width="100px"
                                                Visible="false"></asp:TextBox>
                                            <ajax:textboxwatermarkextender id="TextBoxWatermarkExtendertpdate" runat="server"
                                                targetcontrolid="txtduedate" watermarktext="dd/MM/yyyy">
                                            </ajax:textboxwatermarkextender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lbltransferpac" runat="server" Text="Fees" CssClass="headlabel"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttrans" runat="server" CssClass="textboxstyle" Visible="false"
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltp_priceid" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_PRICE_ID") %>'
                                                CssClass="lblstyle" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltp_flag" runat="server" Text='<%# Bind("FLAG") %>' CssClass="lblstyle"
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltp_detialid" runat="server" Text='<%# Bind("TRANSFER_PACKAGE_FROM_TO_DETAIL_ID") %>'
                                                CssClass="lblstyle" Visible="false"></asp:Label>
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
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="11" />
                        </td>
                        <td>
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
