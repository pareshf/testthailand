<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRM.WebPortal.Default"
    MasterPageFile="~/CrmWeb.Master" %>

<%@ MasterType VirtualPath="~/CrmWeb.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="5px" colspan="5">
            </td>
        </tr>
        <tr>
            <td class="head_text" style="background: url(../site/images/head_bg.png) no-repeat;
                width: 326px; height: 49px;">
                Best International Bargins
            </td>
            <td width="1px" bgcolor="#FFFFFF">
            </td>
            <td class="head_text" style="background: url(../site/images/head_bg.png) no-repeat;
                width: 326px; height: 49px;">
                Best International Air Fare
            </td>
            <td width="1px" bgcolor="#FFFFFF">
            </td>
            <td class="head_text" style="background: url(../site/images/head_bg.png) no-repeat;
                width: 326px; height: 49px;">
                Best Indian Bargins
            </td>
        </tr>
        <tr>
            <td style="background: url(../site/images/bg_text.jpg) no-repeat; width: 326px; height: 494px;"
                valign="top">
                <table width="326px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="subbanner" width="270" height="150">
                            <div id="dvTopLeft" runat="server">
                            </div>
                            <%--<object type="application/x-shockwave-flash" data="only_thailand.swf" width="270"
                                height="150">
                                <param name="movie" value="../site/flash/only_thailand.swf" />
                            </object>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="subbanner" width="270" height="150">
                            <div id="dvBottomLeft" runat="server">
                            </div>
                            <%--<object type="application/x-shockwave-flash" data="singapore_malaysia_thailand.swf"
                                width="270" height="150">
                                <param name="movie" value="../site/flash/singapore_malaysia_thailand.swf" />
                            </object>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1px" bgcolor="#ffffff">
            </td>
            <td style="background: url(../site/images/bg_text.jpg) no-repeat; width: 326px; height: 494px;"
                valign="top">
                <table width="326" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="127px" class="text">
                            <asp:ListView ID="lstInternationalFares" runat="server">
                                <LayoutTemplate>
                                    <table width="95%" border="0" cellspacing="0" cellpadding="0">
                                        <%--<thead>
                                            <tr>
                                                <th>
                                                    Place
                                                </th>
                                                <th>
                                                    Fare
                                                </th>
                                            </tr>
                                        </thead>--%>
                                        <tbody>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("PLACE")%>
                                        </td>
                                        <td align="right">
                                            <%# Eval("FARE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td class="head_text" style="background: url(../site/images/head_bg.png) no-repeat;
                            height: 49px; width: 326px;">
                            Best Domestic Air Fare
                        </td>
                    </tr>
                    <tr>
                        <td height="318px" class="text">
                            <asp:ListView ID="lstDomesticFares" runat="server">
                                <LayoutTemplate>
                                    <table width="95%" border="0" cellspacing="0" cellpadding="0">
                                        <%--<thead>
                                            <tr>
                                                <th>
                                                    Place
                                                </th>
                                                <th>
                                                    Fare
                                                </th>
                                            </tr>
                                        </thead>--%>
                                        <tbody>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("PLACE")%>
                                        </td>
                                        <td align="right">
                                            <%# Eval("FARE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <span class="text_red" style="font-size: 11px;">All above fares are subject to change
                                and availability.</span>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="1px" bgcolor="#ffffff">
            </td>
            <td valign="top" style="background: url(../site/images/bg_text.jpg) no-repeat; width: 326px;
                height: 494px;">
                <table width="326px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="subbanner" width="270" height="150">
                            <%--<object type="application/x-shockwave-flash" data="goa.swf" width="270" height="150">
                                <param name="movie" value="../site/flash/goa.swf" />
                            </object>--%>
                            <div id="dvTopRight" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="subbanner" width="270" height="150">
                            <%--<object type="application/x-shockwave-flash" data="kerala.swf" width="270" height="150">
                                <param name="movie" value="../site/flash/kerala.swf" />
                            </object>--%>
                            <div id="dvBottomRight" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
