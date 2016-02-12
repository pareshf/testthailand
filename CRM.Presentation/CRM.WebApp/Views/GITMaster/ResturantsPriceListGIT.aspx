<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ResturantsPriceListGIT.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.ResturantsPriceListGIT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

  <asp:Label ID="Label55" runat="server" Text="Resturant Price List Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
    <br />
    <br />
    <div class="pageTitle" >
        <asp:UpdatePanel ID="upResturamt" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <table  width="400px">
                    <tr>
                        <td style="width:200px">
                            <asp:Label ID="Label1" runat="server" Text="Supplier Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        <td style="width:200px">
                            <asp:DropDownList ID="drpSupplierName" runat="server" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Meal Type"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpMealType" runat="server" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>

                     <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Adult Rate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdultRate" runat="server" Width="200px">
                            </asp:TextBox>
                        </td>
                    </tr>

                      <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Child Rate"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChildRate" runat="server" Width="200px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />

                <asp:Button runat="server" ID="btnSave" Text = "Save" Width="100px"/>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
