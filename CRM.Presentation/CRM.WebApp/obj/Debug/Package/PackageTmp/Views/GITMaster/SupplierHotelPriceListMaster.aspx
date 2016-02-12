<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SupplierHotelPriceListMaster.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.SupplierHotelPriceListMaster" %>
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
        <asp:Label ID="Label55" runat="server" Text="Hotel Price List Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
             <br />
             <br />
             <asp:UpdatePanel ID="upHotelPriceList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div class="pageTitle">
                <table>
                     <tr>
                        <td >
                            <asp:Label ID="lblsuppliername" runat="server" Text="Suppller Name"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpsuppliername" Width="205px" OnSelectedIndexChanged="drpSupplierName_SelectedIndexChanged" AutoPostBack="true" Enabled="False"></asp:DropDownList>
                        </td>
                    </tr>    
                     <tr>
                        <td >
                            <asp:Label ID="lblRoomType" runat="server" Text="Room Type"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:DropDownList runat="server" ID="drpRoomType" Width="205px"></asp:DropDownList>
                        </td>
                    </tr>                           
                     <tr>
                        <td >
                            <asp:Label ID="lblSingleRoomRate" runat="server" Text="Single Room Rate"> </asp:Label>
                            
                        </td>
                        <td>&nbsp; 
                            <asp:TextBox ID="txtSingleRoomRate" runat="server"  Width="200px"></asp:TextBox>
                        </td>
                    </tr>        
                     <tr>
                        <td >
                            <asp:Label ID="lblDoubleRoomRate" runat="server" Text="Double Room Rate"> </asp:Label>
                            
                        </td>
                        <td>     &nbsp;                        
                            <asp:TextBox ID="txtDoubleRoomRate" runat="server"  Width="200px"></asp:TextBox>                            
                        </td>
                    </tr>      
                     <tr>
                        <td >
                            <asp:Label ID="lblTripleRoomRate" runat="server" Text="Triple Room Rate"> </asp:Label>
                            
                        </td>
                        <td>     &nbsp;                        
                            <asp:TextBox ID="txtTripleRoomRate" runat="server"  Width="200px"></asp:TextBox>                            
                        </td>
                    </tr>   
                     <tr>
                        <td >
                            <asp:Label ID="lblextraadultrate" runat="server" Text="extra adult rate"> </asp:Label>
                            
                        </td>
                        <td>&nbsp; 
                            <asp:TextBox ID="txtextraadultrate" runat="server"  Width="200px"></asp:TextBox>                                                        
                        </td>
                    </tr>       
                     <tr>
                        <td >
                            <asp:Label ID="lblExtraCWBRate" runat="server" Text="Extra CWB Rate"> </asp:Label>                            
                        </td>
                        <td>     &nbsp;                        
                            <asp:TextBox ID="txtExtraCWBRate" runat="server"  Width="200px"></asp:TextBox>                                                                                    
                        </td>
                    </tr>     
                     <tr>
                        <td >
                            <asp:Label ID="lblextraCNBRate" runat="server" Text="extra CNB Rate"> </asp:Label>                            
                        </td>
                        <td>   &nbsp;                          
                            <asp:TextBox ID="txtextraCNBRate" runat="server"  Width="200px"></asp:TextBox>                            
                        </td>
                    </tr>         
                     <tr>
                        <td >
                            <asp:Label ID="lblisdefault" runat="server" Text="Is default"> </asp:Label>                            
                        </td>
                        <td>  &nbsp;                           
                            <asp:CheckBox ID="chkisdefault" runat="server" />
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
                     <%-- <tr>
                        <td >
                            <asp:Label ID="lblToissueVoucher" runat="server" Text="To Issue Service Voucher"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:TextBox ID="txtToissueVoucher" runat="server"  Width="200px"></asp:TextBox>
                        </td>
                    </tr>   --%>
                     <tr>
                        <td >
                            <asp:Label ID="lblcurrency" runat="server" Text="Currency"> </asp:Label>
                            
                        </td>
                        <td>   &nbsp;                       
                            <asp:TextBox ID="txtcurrency" runat="server" Text="THB" Width="200px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>    
                     <tr>
                        <td >
                            <asp:Label ID="lblConf" runat="server" Text="Is Conference Applicable"> </asp:Label>                            
                        </td>
                        <td>  &nbsp;                           
                            <asp:CheckBox ID="chkisconfapplicable" runat="server" />
                        </td>
                    </tr>     
                     <tr>
                        <td >
                            <asp:Label ID="lblgaladinner" runat="server" Text="Is Gala Applicable"> </asp:Label>                            
                        </td>
                        <td>  &nbsp;                           
                            <asp:CheckBox ID="chkisgaladinnerApplicable" runat="server" />
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
