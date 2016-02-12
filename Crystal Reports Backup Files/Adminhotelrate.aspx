<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adminhotelrate.aspx.cs"
    Inherits="CRM.WebApp.Views.FIT.Adminhotelrate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>THAILAND_CRM Unlimited</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-family: Calibri">
            <asp:Repeater ID="rpthotelpricelist" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr style="background-color: gray">
                            <td>
                                Hotel Name
                            </td>
                            <td>
                                Room Type
                            </td>
                            <td>
                                Single Room Cost
                            </td>
                            <td>
                                Double Room Cost
                            </td>
                            <td>
                                Triple Room Cost
                            </td>
                            <td>
                                Extra Adult Cost
                            </td>
                            <td>
                                Extra CWB Cost
                            </td>
                            <td>
                                Extra CNB Cost
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("CHAIN_NAME")%>
                        </td>
                        <td>
                            <%#Eval("ROOM_TYPE_NAME")%>
                        </td>
                        <td>
                            <%#Eval("SINGLE_ROOM_TOTAL")%>
                        </td>
                        <td>
                            <%#Eval("DOUBLE_ROOM_TOTAL")%>
                        </td>
                        <td>
                            <%#Eval("TRIPLE_ROOM_TOTAL")%>
                        </td>
                        <td>
                            <%#Eval("EXTRA_ADULT_TOTAL")%>
                        </td>
                        <td>
                            <%#Eval("EXTRA_CWB_COST_TOTAL")%>
                        </td>
                        <td>
                            <%#Eval("EXTRA_CNB_COST_TOTAL")%>
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
