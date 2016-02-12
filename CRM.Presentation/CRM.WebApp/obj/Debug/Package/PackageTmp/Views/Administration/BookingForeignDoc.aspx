<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingForeignDoc.aspx.cs" Inherits="CRM.WebApp.Views.Administration.BookingForeignDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script>
        function alertmsg() {
            alert('Record Save Successfully');
        }
</script>
    <form id="form1" runat="server">    
    <table>
            <tr>
                <td>
                    Documents :<br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="Document" runat="server" />
                </td>           
                <td>
                    <a runat="server" href="" id="docsite" target="_blank">View</a>
                </td>
             </tr>            
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="return alertmsg();" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
