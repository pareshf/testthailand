<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SightSeeingPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.SightSeeingPriceListMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
 <div>
        <asp:Label ID="Label55" runat="server" Text="Sight Seeing Price List Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
             <br />
             <br />
             <asp:UpdatePanel ID="upSightSeeingPriceList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="pageTitle">
                <table>
                     <tr>
                        <td >
                            <asp:Label ID="lblSightSeeingname" runat="server" Text="Sight Seeing Name"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpSightSeeingname" Width="205px" OnSelectedIndexChanged="drpSupplierName_SelectedIndexChanged" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                        </td>
                    </tr>    
                                      
                     <tr>
                        <td >
                            <asp:Label ID="lblSightSeeingPackageName" runat="server" Text="Sight Seeing Package Name"> </asp:Label>
                            
                        </td>
                        <td>&nbsp; 
                            <asp:TextBox ID="txtSightSeeingPackageName" runat="server"  Width="200px"></asp:TextBox>
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
                            <asp:Label ID="lblisMealApplicable" runat="server" Text="Is Meal Applicable"> </asp:Label>                            
                        </td>
                        <td>  &nbsp;                           
                            <asp:CheckBox ID="chkisMealApplicable" runat="server" />
                        </td>
                    </tr>    
                     <tr>
                        <td >
                            <asp:Label ID="lblStatus" runat="server" Text="Status"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpStatus" Width="205px"></asp:DropDownList>
                        </td>
                    </tr> 
                     <tr>
                        <td >
                            <asp:Label ID="lblcity" runat="server" Text="City"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:TextBox ID="txtCity" runat="server"  Width="200px" ReadOnly="True"></asp:TextBox>
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
