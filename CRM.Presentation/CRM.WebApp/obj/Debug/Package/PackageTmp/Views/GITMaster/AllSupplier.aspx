<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AllSupplier.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.AllSupplier" %>
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

       <div class="pageTitle">
        <asp:Label ID="Label55" runat="server" Text="All Supplier" class="pageTitle" Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="upSupplierGrid" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
        <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search_onclick" Style="float: right;
                                margin-right: 10px; display: block; color: black;" CssClass="button" />
                            <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                display: none; color: black;" CssClass="button" OnClick="searchnow_onclick" />
                        </td>
                    </tr>
                </table>
        <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>                       
                        <tr>
                            <td >
                                <asp:Label ID="Label5" runat="server" Text="Supplier Type" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td> &nbsp;
                                <asp:DropDownList ID="drpSupplierType" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Supplier Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td> &nbsp;
                                <asp:DropDownList ID="drpSupplierName" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
        <asp:GridView ID="GV_Supplier" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" Width="938px" AutoPostBack="true" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging" OnPageIndexChanging="GV_Result_PageIndexChanging">
            <pagersettings mode="NumericFirstLast" position="Bottom" />
            <Columns>     
                    <asp:TemplateField ControlStyle-Width="10px" ItemStyle-Width = "20px">
                        <HeaderTemplate>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb1" runat="server" AutoPostBack="true"  OnCheckedChanged="CheckChanged"  ></asp:RadioButton>
                        </ItemTemplate>
                    </asp:TemplateField>       
             <asp:TemplateField  Visible="false">
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Transfer ID" CssClass="lblstyleGIT"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblSupplierID" runat="server" Text='<%# Bind("SUPPLIER_ID") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:BoundField DataField="SUPPLIER_UNQ_ID" HeaderText="Supplier Unique ID" Visible = "false" />
            <%--<asp:BoundField DataField="SUPPLIER_COMPANY_NAME" HeaderText="Supplier Name"  />--%>
            <asp:TemplateField  >
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Supplier Name" ></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblSupplierName" runat="server" Text='<%# Bind("SUPPLIER_COMPANY_NAME") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField  >
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="Supplier Type" ></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblSupplierType" runat="server" Text='<%# Bind("SUPPLIER_TYPE_NAME") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
            <asp:BoundField DataField="REMARKS" HeaderText="Remark" Visible = "false"/>
            <asp:BoundField DataField="DESIGNATION_DESC" HeaderText="Designation" Visible = "false"/>
            <asp:BoundField DataField="CREDIT_LIMIT" HeaderText="Credit Limit" Visible = "false"/>
            <%--<asp:BoundField DataField="GL_CODE" HeaderText="GL Code" />            --%>
              <asp:TemplateField  >
                        <HeaderTemplate>
                            <asp:Label ID = "lblHeader1" runat="server" Text="GL Code" ></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID = "lblGLCODE" runat="server" Text='<%# Bind("GL_CODE") %>' CssClass="lblstyle"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                      
            </Columns>
           <HeaderStyle CssClass="rgHeader" />
           </asp:GridView>
           </ContentTemplate>
           </asp:UpdatePanel>
           <br />
         
         <asp:UpdatePanel runat="server" ID="upButtons" ChildrenAsTriggers="false" UpdateMode="Conditional">
           <ContentTemplate>
        <asp:Button runat="server" ID="btnFitRate" Text = "Fit Rate" Width="100px" OnClick = "btnFITRATE_Click"/>&nbsp; &nbsp;
        <asp:Button runat="server" ID="btnGitRate" Text = "Git Rate" Width= "100px" OnClick="btnGITRate_Click"/> 
    </ContentTemplate>
    </asp:UpdatePanel>
   
         
       </div>
       
</asp:Content>
