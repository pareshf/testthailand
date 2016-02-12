<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileNotFound.aspx.cs" Inherits="CRM.WebApp.FileNotFound" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%= System.Configuration.ConfigurationSettings.AppSettings["PageTitle"].ToString() %></title>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="3" cellspacing="2" width="100%" border="0">
            <tr>
                <td valign="middle" width="88%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="12%">
                                <asp:Image ID="Image2" runat="server" 
                                    ImageUrl="~/Views/Shared/Images/flamingo.jpg" />
                            </td>
                            <td width="1%">&nbsp;</td>
                            <td width="87%" valign="middle">
                                <div style="background-color: Maroon; color: White; font-family: Arial;
                                    font-size: 14px; font-weight: bold; padding:7px 0px 7px 5px">
                                    &nbsp;Page not found on server</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" 
                        Text="&lt;br /&gt;The page you are looking for might have been removed, had its name changed, or is temporarily unavailable."></asp:Label>
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Go Back to Default Page" 
                        PostBackUrl="~/Default.aspx"></asp:LinkButton>
                    <br />
                </td>
            </tr>
            </table>
    </form>
</body>
</html>
