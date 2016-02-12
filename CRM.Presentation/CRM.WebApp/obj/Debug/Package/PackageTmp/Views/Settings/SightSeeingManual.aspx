<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SightSeeingManual.aspx.cs" Inherits="CRM.WebApp.Views.Settings.SightSeeingManual" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
 <script language="javascript" type="text/javascript">

     var sessionTimeout = "<%= Session.Timeout %>";

     var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
     setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


          <asp:Label ID="Label63" runat="server" Text="Sight Seeing Manual" class="pageTitle" Width="400px"
        Font-Bold="true" Font-Size="Large"></asp:Label>
    <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
        ChildrenAsTriggers="false">
        <ContentTemplate>
            <table width="800px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse: collapse;
                border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                <tr style="background-color: #f3f3f3">
                <td style="width: 10px">
                        <asp:Button ID="Button1" runat="server" Text="Download" 
                            onclick="Button1_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label1" runat="server" Text="Bangkok Sight Seeing"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button2" runat="server" Text="Download" 
                            onclick="Button2_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label2" runat="server" Text="Pattaya Sight Seeing"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                 <td>
                        <asp:Button ID="Button3" runat="server" Text="Download" 
                            onclick="Button3_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label3" runat="server" Text="Phuket Sight Seeing"></asp:Label>
                    </td>
                   
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button4" runat="server" Text="Download" 
                            onclick="Button4_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label4" runat="server" Text="Krabi Sight Seeing"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button5" runat="server" Text="Download" 
                            onclick="Button5_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label5" runat="server" Text="Samui Sight Seeing"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                 <td>
                        <asp:Button ID="Button6" runat="server" Text="Download" 
                            onclick="Button6_Click" />
                    </td>
                    <td style="width: 250px">
                        <asp:Label ID="Label6" runat="server" Text="Chiangmai Sight Seeing"></asp:Label>
                    </td>
                   
                </tr>
          
            </table>
        </ContentTemplate>
        <Triggers>
                                   <asp:PostBackTrigger ControlID="Button1" />
 <asp:PostBackTrigger ControlID="Button2" />
 <asp:PostBackTrigger ControlID="Button3" />
 <asp:PostBackTrigger ControlID="Button4" />
 <asp:PostBackTrigger ControlID="Button5" />
 <asp:PostBackTrigger ControlID="Button6" />
 
                               </Triggers>
    </asp:UpdatePanel>
</asp:Content>
