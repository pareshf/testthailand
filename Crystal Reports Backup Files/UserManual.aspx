﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="UserManual.aspx.cs" Inherits="CRM.WebApp.Views.Settings.UserManual" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 20px;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: normal;
        }
        
        .gridlabel
        {
            font-family: Verdana;
            font-size: 14px;
            font-weight: bold;
        }
        .error
        {
            font-family: Arial;
            font-size: 12px;
            color: #FF0000;
        }
    </style>

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


    <asp:Label ID="Label63" runat="server" Text="User Manual" class="pageTitle" Width="400px"
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
                        <asp:Label ID="Label1" runat="server" Text="Register"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button2" runat="server" Text="Download" 
                            onclick="Button2_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label2" runat="server" Text="Create sub accounts"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                 <td>
                        <asp:Button ID="Button3" runat="server" Text="Download" 
                            onclick="Button3_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label3" runat="server" Text="Create FIT Package"></asp:Label>
                    </td>
                   
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button4" runat="server" Text="Download" 
                            onclick="Button4_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label4" runat="server" Text="Update FIT Package"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button5" runat="server" Text="Download" 
                            onclick="Button5_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label5" runat="server" Text="Book FIT Package"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                 <td>
                        <asp:Button ID="Button6" runat="server" Text="Download" 
                            onclick="Button6_Click" />
                    </td>
                    <td style="width: 250px">
                        <asp:Label ID="Label6" runat="server" Text="Reconfirm FIT Package using Credit Limit"></asp:Label>
                    </td>
                   
                </tr>
                <tr style="background-color: #f3f3f3">
                 <td>
                        <asp:Button ID="Button7" runat="server" Text="Download" 
                            onclick="Button7_Click" />
                    </td>
                    <td style="width: 300px">
                        <asp:Label ID="Label7" runat="server" Text="Reconfirm FIT Package using Cash on Arrival "></asp:Label>
                    </td>
                   
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button8" runat="server" Text="Download" 
                            onclick="Button8_Click" />
                    </td>
                    <td style="width: 400px">
                        <asp:Label ID="Label8" runat="server" Text="Reconfirm FIT Package using Credit Card"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button9" runat="server" Text="Download" 
                            onclick="Button9_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label9" runat="server" Text="Ledger Report"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button10" runat="server" Text="Download" 
                            onclick="Button10_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label10" runat="server" Text="Credit Limit Usage Report"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button11" runat="server" Text="Download" 
                            onclick="Button11_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label11" runat="server" Text="Cancel FIT Booking"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button12" runat="server" Text="Download" 
                            onclick="Button12_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label12" runat="server" Text="Download Invoice"></asp:Label>
                    </td>
                    
                </tr>
                <tr style="background-color: #f3f3f3">
                <td>
                        <asp:Button ID="Button13" runat="server" Text="Download" 
                            onclick="Button13_Click" />
                    </td>
                    <td style="width: 200px">
                        <asp:Label ID="Label13" runat="server" Text="Download Service Vouchers"></asp:Label>
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
 <asp:PostBackTrigger ControlID="Button7" />
 <asp:PostBackTrigger ControlID="Button8" />
 <asp:PostBackTrigger ControlID="Button9" />
 <asp:PostBackTrigger ControlID="Button10" />
                                    <asp:PostBackTrigger ControlID="Button11" />
<asp:PostBackTrigger ControlID="Button12" />
                                    <asp:PostBackTrigger ControlID="Button13" />
                               </Triggers>
    </asp:UpdatePanel>
</asp:Content>
