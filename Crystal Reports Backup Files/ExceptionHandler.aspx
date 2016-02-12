<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionHandler.aspx.cs"
    Inherits="CRM.WebApp.ExceptionHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%= System.Configuration.ConfigurationSettings.AppSettings["PageTitle"].ToString() %>
    </title>

    <script language="javascript" type="text/javascript">
        function ToggleError(obj) {
            if (obj.innerHTML == "Show") {
                obj.innerHTML = "Hide";
                document.getElementById("divErrorDesc").style.display = "block";
            }
            else {
                obj.innerHTML = "Show";
                document.getElementById("divErrorDesc").style.display = "none";
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="2" width="100%" border="0">
            <tr>
                <td valign="middle" width="88%">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="12%">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Views/Shared/Images/flamingo.jpg" />
                            </td>
                            <td width="1%">&nbsp;</td>
                            <td width="87%" valign="middle">
                                <div style="background-color: Maroon; color: White; font-family: Arial;
                                    font-size: 14px; font-weight: bold; padding:7px 0px 7px 5px">
                                    Server Error
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="<br />Server encountered an error and could not serve your request. "></asp:Label>
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Go Back to Default Page" PostBackUrl="~/Default.aspx"></asp:LinkButton>
                    <br />
                    <br />
                    <a href="#" onclick="ToggleError(this)" class="lnk">Show</a>
                    <asp:Label ID="Label2" runat="server" Text=" more information on error"></asp:Label>
                    &nbsp;<br />
                </td>
            </tr>
            <tr id="divErrorDesc" style="display: none" class="normaltext">
                <td>
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
