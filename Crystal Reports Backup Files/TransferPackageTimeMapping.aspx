<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TransferPackageTimeMapping.aspx.cs" Inherits="CRM.WebApp.Views.BackOffice.TransferPackageTimeMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
        <asp:Label ID="Label55" runat="server" Text="Transport Package Time Mapping" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
             <br />
             <br />
             <asp:UpdatePanel ID="upTransferTime" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="pageTitle">
                <table>
                 <tr>
                        <td >
                            <asp:Label ID="lblTransferFrom" runat="server" Text="Transfer From"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpTransferFrom" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>          
                 <tr>
                        <td >
                            <asp:Label ID="lblTransferTo" runat="server" Text="Transfer To"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                        
                            <asp:DropDownList runat="server" ID="drpTransferTo" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>    
                 <%-- <tr>
                        <td >
                            <asp:Label ID="lblTime" runat="server" Text="Time"> </asp:Label>
                            
                        </td>
                        <td>                            
                            <asp:DropDownList runat="server" ID="drpTime" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>    --%>               
                </table>
            </div>
            <br />
           
           </ContentTemplate>
           </asp:UpdatePanel>

   
             <div class="pageTitle" id="divButtons">
                <table>
                <tr>
                <td>
                    <asp:Button runat="server" ID="btnUpdate" Text = "Show" Width="100px" OnClick="btnShow_Click"/>&nbsp; &nbsp;
                    
                </td>
                <td>
                    
                </td>
                </tr>
                </table>
            </div>
            <br />
            
             <div id="divgridtransferpackagetimming" class="pageTitle">
             <asp:Label ID="Label2" runat="server" Text="Transfer Package Timming" class="pageTitle" Width="400px"
            Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
            <br />
            
           <asp:UpdatePanel ID="uptimegrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
           <ContentTemplate>

                <asp:GridView runat="server" ID="gridTransferTime" AutoGenerateColumns="false" AllowPaging="true" SkinID="sknSubGrid" Width="600px" OnPageIndexChanging="GV_time_PageIndexChanging" PageSize = "10">
                
                <Columns>

                 <%--   <asp:TemplateField ControlStyle-Width="30px">
                    <HeaderTemplate>                                    
                     </HeaderTemplate>
                     <ItemTemplate>
                     <asp:Button runat="server" ID="btnEdit" Text = "Edit" />
                      </ItemTemplate>
                   </asp:TemplateField>--%>

                 <asp:TemplateField ControlStyle-Width="100px">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Transfer From" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblFrom" runat="server" Text='<%# Bind("FROM_NAME") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <asp:TemplateField ControlStyle-Width="100px">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader2" runat="server" Text="Transfer To" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblTo" runat="server" Text='<%# Bind("TO_NAME") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                 <%-- <asp:TemplateField ControlStyle-Width="30px">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblTime" runat="server" Text='<%# Bind("TIME") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                 <asp:TemplateField ControlStyle-Width="80px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpTime" runat="server" >
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                                        
                </Columns>
                </asp:GridView>
                 <br />
                    <asp:Button ID="btnAddBoat" runat="server" Text = "ADD" OnClick="btnAdd_Click"/>
           </ContentTemplate>
           </asp:UpdatePanel>
            </div>
            <br />
       
             <div class="pageTitle" id="div1">
                <table>
                <tr>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text = "Save" Width="100px" OnClick="btnSave_Click"/>&nbsp; &nbsp;
                    <asp:Button runat="server" ID="btnBack" Text="Back"  Width="100px"/>
                </td>
                <td>
                    
                </td>
                </tr>
                </table>
            </div>
             </div>
</asp:Content>
