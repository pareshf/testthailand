<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CRM.WebApp.Login" %>

<%@ Register src="Views/Shared/Controls/Templates/ProductFooter.ascx" tagname="ProductFooter" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%= System.Configuration.ConfigurationSettings.AppSettings["PageTitle"].ToString() %>
    </title>
    <link href="Views/Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
     
    </head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="80" align="center" valign="bottom">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="505" style="height: 312px" border="0" align="center" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td width="505" style="height: 312px; background: url(Views/Shared/Images/bkg.gif) no-repeat">
                            <table width="505" style="height: 312px;" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" style="height: 312px;">
                                            <tr>
                                                <td style="height:150px" align="center">
                                                    <asp:Image ID="Image1" runat="server" 
                                                        ImageUrl="~/Views/Shared/Images/logo3.jpg" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 45px" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="center">
                                                    <asp:Label ID="lblUsername0" runat="server" Text="Global Customer Care" 
                                                        Font-Bold="True" ForeColor="#25407B"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblUsername1" runat="server" Text="Email: support@travelzunlimited.com"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblUsername2" runat="server" Text="Phone: (079) 39830925" Visible="false"></asp:Label>
                                                    </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" align="left" valign="top">
                                        <table width="100%" border="0" cellpadding="2" cellspacing="3">
                                            <tr>
                                                <td style="height: 35px" width="14">
                                                </td>
                                                <td style="height: 35px" align="left" valign="middle">
                                                    &nbsp;<asp:Label ID="lblLoginHeader" runat="server" Text="Login" SkinID="SknLoginHeader"></asp:Label>
                                                </td>
                                                <td style="height: 35px" width="16">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 12px;">
                                                </td>
                                                <td style="height: 12px;">
                                                </td>
                                                <td style="height: 12px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUsername" runat="server" Text="Username (Email)"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember me next time" />
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" />
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lnlbtnForgetPassword" runat="server" Text="Forgot password?"
                                                        PostBackUrl="~/ForgotPassword.aspx"></asp:LinkButton>
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:LinkButton ID="lnkbtnRegister" runat="server" Text="Register" 
                                                       PostBackUrl="~/NewAgentRegistrationForm.aspx"></asp:LinkButton>
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 40px">
                                                    &nbsp;
                                                </td>
                                                <td align="center" style="height: 40px">
                                                    &nbsp;<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td align="center" style="height: 40px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    
    <uc1:ProductFooter ID="ProductFooter1" runat="server" />
    
    </form>
</body>
</html>
