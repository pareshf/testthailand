<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="CRM.WebApp.SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <style type="text/css">
        .style1
        {
            height: 76px;
        }
    </style>

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
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Views/Shared/Images/Jaivver.jpg" />
                            </td>
                            <td width="1%">&nbsp;</td>
                            <td width="87%" valign="middle">
                                <div style="background-color: Maroon; color: White; font-family: Arial;
                                    font-size: 14px; font-weight: bold; padding:7px 0px 7px 5px">
                                   Session Is Expired
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="Label1" runat="server" 
                        Text="&lt;br /&gt;Your Session is expired.  "></asp:Label>
                        
                        
                        
                        
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Click here to login again" 
                        PostBackUrl="~/Login.aspx"></asp:LinkButton>
                        
                        
                    <br />
                    <br />
                    &nbsp;<br />
                </td>
            </tr>
            <tr id="divErrorDesc" style="display: none" class="normaltext">
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>