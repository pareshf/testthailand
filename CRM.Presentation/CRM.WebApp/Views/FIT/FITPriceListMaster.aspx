<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="FITPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.FIT.FITPriceListMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%> 
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>   
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<div>
    <asp:Label ID="Header" runat="server" Text="Hotel Price List Master" class="pageTitle" Width="400px"
                        Font-Bold="true" Font-Size="Large"> </asp:Label>

    <br/>
    <br />
    <table>
        <tr>
            <td>
             <asp:UpdatePanel ID="upHotelPrice" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_SupplierName" runat="server" Text="Supplier Name"> </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList  ID="ddl_SupplierName" Width="210px" runat="server" OnSelectedIndexChanged="ddl_SupplierName_SelectedIndexChanged" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_CIty" runat="server" Text="City"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_City" runat="server"  Width="210px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_FromDate" runat="server" Text="From Date"> </asp:Label>&nbsp;<span
                                            class="error" id="spanFly" runat="server">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="210px" > </asp:TextBox>
                                    <ajax:CalendarExtender ID="FromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                     <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender1111" runat="server" targetcontrolid="txtFromDate"
                                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                        </ajax:textboxwatermarkextender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="From Date is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ToDate" runat="server" Text="To Date"> </asp:Label>&nbsp;<span
                                            class="error" id="span1" runat="server">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtToDate" runat="server" Width="210px" > </asp:TextBox>
                                    <ajax:CalendarExtender ID="ToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender1" runat="server" targetcontrolid="txtToDate"
                                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                        </ajax:textboxwatermarkextender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="To Date is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_RoomType" runat="server" Text="Room Type"> </asp:Label>&nbsp;<span
                                            class="error" id="span2" runat="server">*</span>
                                </td>
                                <td>
                                   <asp:DropDownList  ID="ddl_RoomType" Width="210px" runat="server" OnSelectedIndexChanged="ddl_RoomType_SelectedIndexChanged" AutoPostBack="true" Enabled="True"></asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Room Type is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="ddl_RoomType"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_SingleRoom" runat="server" Text="Single Room Rate"> </asp:Label>&nbsp;<span
                                            class="error" id="span3" runat="server">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSingleRoom" runat="server" Width="210px"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Single Room Rate is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtSingleRoom"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_DoubleRoom" runat="server" Text="Double Room Rate"> </asp:Label>&nbsp;<span
                                            class="error" id="span4" runat="server">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server" Width="210px"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Double Room Rate is Required."
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtDoubleRoom"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_TripleRoom" runat="server" Text="Triple Room Rate"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ExtraAdult" runat="server" Text="Extra Adult Rate"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtraAdult" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ExtraCWB" runat="server" Text="Extra CWB Rate"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtraCWB" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ExtraCNB" runat="server" Text="Extra CNB Rate"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtExtraCNB" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_IsDefault" runat="server" Text="Is Default"> </asp:Label>
                                </td>
                                <td>
                                   <asp:CheckBox ID="chbx_IsDefault" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Status" runat="server" Text="Status"> </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList  ID="ddl_Status" Width="210px" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_AMargin" runat="server" Text="A Margin Amount"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAMargin" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_APMargin" runat="server" Text="A+ Margin Amount"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAPMargin" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="A++ Margin Amount"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAPPMargin" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="A Margin %"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAMarginP" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="A+ Margin %"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAPMarginP" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="A++ Margin %"> </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAPPmarginP" runat="server" Width="210px"> </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Currency" runat="server" Text="Currency"> </asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Currency" runat="server" AutoPostBack ="true" Enabled="false"></asp:DropDownList>
                                </td>
                            </tr>
                            
                        </table>
                    </div>
                    <br />
                </ContentTemplate>
             </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table>
                        <tr>
                            <td >
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" ValidationGroup="Required"/>&nbsp &nbsp
                                <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click" />
                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    
    </table>
</div>
</asp:Content>
