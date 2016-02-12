<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="ApprovedAgent.aspx.cs" Inherits="CRM.WebApp.Views.FIT.ApprovedAgent" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
   

    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <div>
        <asp:Label runat="server" Text="Approve Agent" ID="headlbl" Width="200px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <%-- <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Search" OnClick="search_onclick" Style="float: right;
                                margin-right: 10px; display: block; color: black;" CssClass="button" />
                            <asp:Button ID="Button4" runat="server" Text="Search Now" Style="float: right; margin-right: 10px;
                                display: none; color: black;" CssClass="button" OnClick="searchnow_onclick" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMainHead" runat="server" Style="display: none">
                    <table>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Quotation Refrence No" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtrefrence" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label6" runat="server" Text="Client Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr>
                            <td width="180px">
                                <asp:Label ID="Label7" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label2" runat="server" Text="Tour Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label3" runat="server" Text="From Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label4" runat="server" Text="To Date" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label5" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="GV_Result" runat="server" OnRowCommand="GV_Result_RowCommand" AutoGenerateColumns="False"
                                SkinID="sknSubGrid" AllowPaging="false" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Approved" Text="Approve" />
                                    <asp:ButtonField ButtonType="Button" CommandName="DisApproved" Text="Disapprove" />
                                    <%--<asp:CommandField ShowSelectButton="True" ButtonType="Button" AccessibleHeaderText="Approved"
                                        SelectText="Approve" ControlStyle-Width="60px"/>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" AccessibleHeaderText="DisApproved"
                                        SelectText="Disapprove" ControlStyle-Width="75px"/>--%>
                                    <asp:BoundField DataField="CUST_ID" HeaderText="Agent No" />
                                    <asp:BoundField DataField="AGENT_NAME" HeaderText="Agent Name" />
                                    <asp:BoundField DataField="CUST_COMPANY_NAME" HeaderText="Agent Company Name" />
                                    
                                    <asp:BoundField DataField="CUST_REL_MOBILE" HeaderText="Mobile" />
                                    <asp:BoundField DataField="CUST_REL_EMAIL" HeaderText="Email" />
                                    <asp:BoundField DataField="USER_STATUS_NAME" HeaderText="Status" />
                                    <asp:BoundField DataField="CITY_NAME" HeaderText="City" />
                                    <asp:BoundField DataField="STATE_NAME" HeaderText="State" />
                                    <asp:BoundField DataField="COUNTRY_NAME" HeaderText="Country" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle" runat="server" Text="Credit Limit (USD)" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcredit" runat="server" Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Agent Type" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drptype" runat="server" Width="60px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Bank" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpbank" runat="server" Width="120px" OnSelectedIndexChanged="Drp_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Branch" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpbranch" runat="server" Width="120px" OnSelectedIndexChanged="DrpBranch_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Account No" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpaccountname" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Bank Charge Applicable" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkBankCharge" AutoPostBack="true" OnCheckedChanged="chkBankCharge_CheckChnaged"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblstyle1" runat="server" Text="Bank Charge" CssClass="lblstyle"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCharge" runat="server" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IS_OFFLINE" HeaderText="Is Offline" />

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenField1" runat="server" />
             
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
