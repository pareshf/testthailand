<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomesticTours.aspx.cs"
    Inherits="CRM.WebPortal.DomesticTours" MasterPageFile="~/CrmWeb.Master" %>

<%@ MasterType VirtualPath="~/CrmWeb.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
        <asp:View ID="viewType" runat="server">
            <table width="980" style="height: 20px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="20" class="linespace">
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="headline">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <img src="../site/images/leftmainhead.jpg" alt="" />
                                            </td>
                                            <td bgcolor="#992F28" class="headline">
                                                Domestic Tours
                                            </td>
                                            <td>
                                                <img src="../site/images/rightmainhead.jpg" alt="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1px" bgcolor="#93221B">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td height="20" class="linespace">
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:DataList ID="dlstType" runat="server" RepeatColumns="4" OnItemCommand="dlstType_ItemCommand"
                    CellPadding="10">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgPhoto" runat="server" CommandArgument='<%# Bind("TOUR_TYPE_ID") %>'
                                        ImageUrl='<%# Bind("IMAGE_URL") %>' SkinID="ImageThumbnail" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnType" runat="server" CssClass="thumb_link" Text='<%# Bind("TOUR_TYPE_DESC") %>'
                                        CommandArgument='<%# Bind("TOUR_TYPE_ID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:View>
        <asp:View ID="viewStates" runat="server">
            <table width="980" style="height: 20px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="20" class="linespace">
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="headline">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <img src="../site/images/leftmainhead.jpg" alt="" />
                                            </td>
                                            <td bgcolor="#992F28" class="headline">
                                                <%--<asp:Label ID="lblTourTypes" runat="server"></asp:Label>--%>
                                                <asp:LinkButton ID="lbtnTourTypes1" runat="server" Font-Underline="False" CssClass="linkButton"
                                                    OnClick="lbtnTourTypes1_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <img src="../site/images/rightmainhead.jpg" alt="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1px" bgcolor="#93221B">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td height="20" class="linespace">
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:DataList ID="dlstState" runat="server" RepeatColumns="4" OnItemCommand="dlstState_ItemCommand"
                    CellPadding="10">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgPhoto" runat="server" CommandArgument='<%# Bind("STATE_ID") %>'
                                        ImageUrl='<%# Bind("IMAGE_URL") %>' SkinID="ImageThumbnail" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnState" runat="server" CssClass="thumb_link" Text='<%# Bind("STATE_NAME") %>'
                                        CommandArgument='<%# Bind("STATE_ID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:View>
        <asp:View ID="viewTours" runat="server">
            <table width="980" style="height: 20px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="20" class="linespace">
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="headline">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <img src="../site/images/leftmainhead.jpg" alt="" />
                                            </td>
                                            <td bgcolor="#992F28" class="headline">
                                                <%--<asp:Label ID="lblState" runat="server"></asp:Label>--%>
                                                <asp:LinkButton ID="lbtnState1" runat="server" Font-Underline="False" CssClass="linkButton"
                                                    OnClick="lbtnState1_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <img src="../site/images/rightmainhead.jpg" alt="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1px" bgcolor="#93221B">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td height="20" class="linespace">
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:DataList ID="dlstTour" runat="server" RepeatColumns="4" OnItemCommand="dlstTour_ItemCommand"
                    CellPadding="10">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgPhoto" runat="server" CommandArgument='<%# Bind("TOUR_ID") %>'
                                        SkinID="ImageThumbnail" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnTour" runat="server" CssClass="thumb_link" Text='<%# Bind("TOUR_NAME") %>'
                                        CommandArgument='<%# Bind("TOUR_ID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="headline123">
                                       <asp:Label ID="lblDayss" runat="server" ForeColor="#992F28"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:View>
        <asp:View ID="viewToursDetails" runat="server">
            <table width="980" style="height: 20px" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="20" class="linespace">
                    </td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="headline">
                                    <table border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <img src="../site/images/leftmainhead.jpg" alt="" />
                                            </td>
                                            <td bgcolor="#992F28" class="headline">
                                                <%--<asp:Label ID="lblTour" runat="server"></asp:Label>--%>
                                                <asp:LinkButton ID="lbtnTour1" runat="server" Font-Underline="False" CssClass="linkButton"
                                                    OnClick="lbtnTour1_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <img src="../site/images/rightmainhead.jpg" alt="" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="1px" bgcolor="#93221B">
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td height="20" class="linespace">
                    </td>
                </tr>
            </table>
            <div align="center" style="width: 100%; margin: 10px 10px 10px 10px;">
                <div class="headline">
                    <asp:Label ID="lblTourName" runat="server" ForeColor="#992F28"></asp:Label>
                </div>
                <div class="headline">
                    <asp:Label ID="lblDays" runat="server" ForeColor="#992F28"></asp:Label>
                </div>
                <div class="headline" visible="false">
                    <asp:Label ID="lblTourCode" runat="server" ForeColor="#992F28" Visible="false"></asp:Label>
                </div>
                <div class="headline" style="margin-bottom: 10px;">
                    <asp:Label ID="lblTourDuration" runat="server" ForeColor="#992F28" Visible="false"></asp:Label>
                </div>
                <div id="tour_hightlight" style="display: none">
                    <div class="teb_matter">
                        <div class="matter_ftest">
                            <div id="dvHighlights" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tour_cost" style="display: none">
                    <div class="teb_matter">
                        <div class="matter_ftest">
                            <div id="dvCost" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="itinerary" style="display: none">
                    <div class="teb_matter">
                        <div class="matter_ftest">
                            <div id="dvIntenary" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="important_notes" style="display: none">
                    <div class="teb_matter">
                        <div class="matter_ftest">
                            <div id="dvImpNotes" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="terms" style="display: none">
                    <div class="teb_matter">
                        <div class="matter_ftest">
                            <div id="dvTerms" runat="server">
                            </div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    var g = new JSTabs(880);
                    g.addTab("Tour Hightlights", document.getElementById("tour_hightlight").innerHTML);
                    g.addTab("Tour Cost", document.getElementById("tour_cost").innerHTML);
                    g.addTab("Itinerary", document.getElementById("itinerary").innerHTML);
                    g.addTab("Important Notes", document.getElementById("important_notes").innerHTML); //Another elements content
                    g.addTab("Terms", document.getElementById("terms").innerHTML);
                    g.build();
                </script>

            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
