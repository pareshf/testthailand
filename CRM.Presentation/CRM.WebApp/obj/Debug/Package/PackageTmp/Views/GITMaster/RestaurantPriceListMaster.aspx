<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="RestaurantPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.RestaurantPriceListMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
 <div>
        <asp:Label ID="Label55" runat="server" Text="Restaurant Price List Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
             <br />
             <br />
             <asp:UpdatePanel ID="upRestaurantPriceList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="pageTitle">
                <table>
                     <tr>
                        <td >
                            <asp:Label ID="lblRestaurantname" runat="server" Text="Restaurant Name"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpRestaurantname" Width="205px" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                        </td>
                    </tr>     
                     <tr>
                        <td >
                            <asp:Label ID="lblMeal" runat="server" Text="Meal Name"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpMeal" Width="205px"  AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr> 
                     <tr>
                        <td >
                            <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate"> </asp:Label>
                            
                        </td>
                        <td>     &nbsp;                        
                            <asp:TextBox ID="txtAdultRate" runat="server"  Width="200px"></asp:TextBox>                            
                        </td>
                    </tr>      
                     <tr>
                        <td >
                            <asp:Label ID="lblChildRate" runat="server" Text="Child Rate"> </asp:Label>
                            
                        </td>
                        <td>     &nbsp;                        
                            <asp:TextBox ID="txtChildRate" runat="server"  Width="200px"></asp:TextBox>                            
                        </td>
                    </tr>                      
                     <tr>
                        <td >
                            <asp:Label ID="lblcurrency" runat="server" Text="Currency"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:TextBox ID="txtcurrency" runat="server" Text="THB" Width="200px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>    
                    
                </table>
            </div>
            <br />
           
           </ContentTemplate>
           </asp:UpdatePanel>
       
             <div class="pageTitle" id="div1">
                <table>
                <tr>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text = "Save" Width="100px" OnClick="btnSave_Click"/>&nbsp; &nbsp;
                    <asp:Button runat="server" ID="btnBack" Text="Back"  Width="100px"  OnClick="btnBack_Click"/>
                </td>
                <td>
                    
                </td>
                </tr>
                </table>
            </div>
             </div>
</asp:Content>
