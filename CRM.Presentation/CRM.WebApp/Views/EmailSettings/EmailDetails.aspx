<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailDetails.aspx.cs" Inherits="CRM.WebApp.Views.EmailSettings.EmailDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
                <table>
                <tr>
                          <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="From" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               
                               <asp:Label ID="lblfrom" runat="server"></asp:Label>
                            </td>
                     </tr>
                    <tr>
                          <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="To" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                
                                <asp:Label ID="lblto" runat="server"></asp:Label>
                            </td>
                     </tr>
                     <tr>
                          <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="CC" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                
                                <asp:Label ID="lblcc" runat="server"></asp:Label>
                            </td>
                     </tr>
                      <tr>
                          <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="BCC" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                
                                <asp:Label ID="lblbcc" runat="server"></asp:Label>
                            </td>
                     </tr>
                     <tr>
                          <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               
                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                            </td>
                     </tr>
                     <tr>
                          <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Time" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                               
                                <asp:Label ID="lbltime" runat="server"></asp:Label>
                            </td>
                     </tr>
                      <tr>
                          <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Subject" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                              
                               <asp:Label ID="lblSubject" runat="server"></asp:Label>
                            </td>
                     </tr>
                     <tr>   
                             <td width="180px"></td>                      
                            <td>
                               
                                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            </td>
                     </tr>
                </table>
               
    </div>
    </form>
</body>
</html>
