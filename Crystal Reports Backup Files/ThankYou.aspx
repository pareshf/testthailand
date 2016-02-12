<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="CRM.WebApp.ThankYou" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" width="100%" align="center" cellpadding="0" cellspacing="0" id="tblMainContentHolder">
            <tr>
                <td width="15%" align="left">
                    <asp:Image ID="imgLogo" runat="server" Height="50px" Width="148px" ImageAlign="Top"
                        ImageUrl="~/Views/Shared/Images/logo4.jpg" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label runat="server" Text="Thank you for your Registration!" ID="headlbl" Width="500px"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:Label runat="server" Text="We will get back to you very soon!" ID="Label1" Width="500px"
            Font-Size="Large" class="pageTitle"></asp:Label>
    </div>
    </form>
</body>
</html>
