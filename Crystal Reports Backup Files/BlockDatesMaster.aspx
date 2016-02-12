<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="BlockDatesMaster.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.BlockDatesMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/Views/FIT/MulticheckDropdown.ascx" TagName="DropDownControl"
    TagPrefix="uc1" %>
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

         <div style="position:relative">
       
        <asp:Label ID="Label55" runat="server" Text="Block Dates Master" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
         <asp:UpdatePanel ID="upHotelPty" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td><asp:Label runat="server" ID="lblFromdate" Text = "From Date"></asp:Label>&nbsp;<span
                        class="error">*</span></td>
                            <td><asp:TextBox runat="server" ID="txtFromdate" Width="150px"></asp:TextBox>
                             <ajax:calendarextender id="CalendarExtenderpty1" runat="server" targetcontrolid="txtFromdate"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="From Date is Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtFromdate"></asp:RequiredFieldValidator>
                                            </td>
                        </tr>

                        <tr>
                            <td><asp:Label runat="server" ID="lblTodate" Text = "To date"></asp:Label>&nbsp;<span
                        class="error">*</span></td>
                            <td><asp:TextBox runat="server" ID="txtTodate" Width="150px"></asp:TextBox>
                            <ajax:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtTodate"
                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="To Date is Required"
                                            CssClass="errorclass" ValidationGroup="Required"  ControlToValidate="txtTodate"></asp:RequiredFieldValidator>
                                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <div class="pageTitle">
                    <asp:Button runat="server" ID="btnSave" Text="Save" ValidationGroup="Required" Width="100px" OnClick="btnSave_Click"/>
                    </div>

                </ContentTemplate>
                </asp:UpdatePanel>

                <div class="pageTitle">
                <asp:Label runat="server" Text="Block Dates" ID="lblHeadBlockDates"></asp:Label>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                   <asp:GridView ID="GV_Result" runat="server" 
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false" Width="400px" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging">
                               
                                   <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="FROM_DATE" HeaderText="From Date" />
                                    <asp:BoundField DataField="TO_DATE" HeaderText="To Date" />
                                    </Columns>
                                </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>

                </div>

</div>
</asp:Content>
