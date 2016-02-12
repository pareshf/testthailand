<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AgentRegistrationForm.aspx.cs" Inherits="CRM.WebApp.AgentRegistrationForm" %>
    <%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
   
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: bold;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 12px;
            font-style:normal;
            font-weight:normal;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 20px;
            font-style:normal;
            font-weight:normal;            
        }
        .errorclass
        {
            font-family: Verdana;
            font-size: 12px;
            color: Red;
        }
    </style>
    <div>
        <asp:Label runat="server" Text="Agent Registration Form" ID="headlbl" Width="200px"
            Font-Bold="true" Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <table width="100%">
                    <tr width="100%">
                        <td width="50%">
                            <table >
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Agent id" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_UNQ_ID" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Agent Company Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_COMPANY_NAME" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpTitle" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:TextBox ID="txtClientname" runat="server" Width="65px"></asp:TextBox>
                                        <asp:TextBox ID="txtClientlastname" runat="server" Width="65px"></asp:TextBox>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Mobile" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_REL_MOBILE" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Email" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_REL_EMAIL" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Password" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="PASSWORD" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Agent Type" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpAgentType" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Communication Mode" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpmode" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Telephone" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_REL_PHONE" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Designation" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpDesignation" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Payment Terms" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpTerms" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table >
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Branch Name" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CHAIN_NAME" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="Addresss Type" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpaddtype" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Address Line 1" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_ADDRESS_LINE1" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Address Line 2" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_ADDRESS_LINE2" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="City" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="drpcity" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="State" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="drpstate" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="Country" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td> 
                                        <asp:DropDownList ID="drpcountry" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Status"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpuserstatus" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Pincode" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_PINCODE" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="Website" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="WEBSITE" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Register" runat="server" OnClick="register_onclick" Width="100px" Text="Register">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
