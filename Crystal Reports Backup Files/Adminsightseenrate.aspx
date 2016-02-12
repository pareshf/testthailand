<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adminsightseenrate.aspx.cs"
    Inherits="CRM.WebApp.Views.FIT.Adminsightseenrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-family: Calibri">
            <asp:Repeater ID="rptsightseenpricelist" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr style="background-color: gray">
                            <td>
                                No of Pax
                            </td>
                            <td>
                                Adult SIC Rate
                            </td>
                            <td>
                                Adult PVT Rate
                            </td>
                            <td>
                                Child SIC Rate
                            </td>
                            <td>
                                Child PVT Rate
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("NO_OF_PAX")%>
                        </td>
                        <td>
                            <%#Eval("ADULT_SIC_RATE")%>
                        </td>
                        <td>
                            <%#Eval("ADULT_PVT_RATE")%>
                        </td>
                        <td>
                            <%#Eval("CHILD_SIC_RATE")%>
                        </td>
                        <td>
                            <%#Eval("CHILD_PVT_RATE")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
