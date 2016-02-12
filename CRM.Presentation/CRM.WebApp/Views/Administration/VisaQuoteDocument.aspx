<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisaQuoteDocument.aspx.cs" Inherits="CRM.WebApp.Views.Administration.VisaQuoteDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr style: align="center">
                <td>
                        Visa Requirement Document <br /><br />
                </td>
            </tr>
            <tr>
                <td>
                     Require Document :
                </td>
                <td>
                    <asp:FileUpload ID="Require" runat="server" />
                </td>
           
                <td>
                    <a runat="server" href="" id="reqdoc" target="_blank">View</a>
                </td>
            </tr>

             <tr>
                <td>
                     Questionaire Document :
                </td>

                 <td>
                    <asp:FileUpload ID="Question" runat="server" />
                </td>
           
                <td>
                    <a runat="server" href="" id="questiondoc" target="_blank">View</a>
                </td>
            </tr>
            <br />
            <br />
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
