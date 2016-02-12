<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SightSeeingPriceListDetails.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.SightSeeingPriceListDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>


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
       <div class="pageTitle">
        <asp:Label ID="Label55" runat="server" Text="Sight Seeing Price List Details" class="pageTitle" Width="600px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="upSightSeeingPriceList" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>
         <table>
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
                                <asp:Label ID="Label5" runat="server" Text="Package Name" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpackname" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="Label1" runat="server" Text="Status" CssClass="lblstyle"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpStatus" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />  
        <asp:GridView ID="GV_SightSeeingPriceList" runat="server"  OnSelectedIndexChanging="GV_Result_SelectedIndexChanging" Width="1000px"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  OnPageIndexChanging="GV_Result_PageIndexChanging">
            <pagersettings mode="NumericFirstLast" position="Bottom" />
            <Columns>     
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />  
            <asp:BoundField DataField="SIGHT_SEEING_PRICE_ID" HeaderText="Sr No" />   
            <asp:BoundField DataField="SUPPLIER_SR_NO" HeaderText="Supplier Sr No" Visible ="false" /> 
            <asp:BoundField DataField="CHAIN_NAME" HeaderText="Sigth Seeing Name" />             
            <asp:BoundField DataField="SIGHT_SEEING_PACKAGE_NAME" HeaderText="Sigth Seeing Package" />            
            <asp:BoundField DataField="ADULT_RATE" HeaderText="Adult Rate" />
            <asp:BoundField DataField="CHILD_RATE" HeaderText="Child Rate" />            
            <asp:BoundField DataField="IS_MEAL_APPLICABLE" HeaderText="Is Meal Applicable" />
            <asp:BoundField DataField="STATUS" HeaderText="Status" />            
            <asp:BoundField DataField="CITY_NAME" HeaderText="City Name" />
                      
            
            
                                               
            </Columns>
           <HeaderStyle CssClass="rgHeader" />
           </asp:GridView>
           </ContentTemplate>
           </asp:UpdatePanel>
           <br />
         
         <asp:UpdatePanel runat="server" ID="upButtons" ChildrenAsTriggers="false" UpdateMode="Conditional">
           <ContentTemplate>
            <asp:Button runat="server" ID="btnNew" Text = "New" Width="100px" OnClick="btnNew_Click"/>&nbsp; &nbsp; 
            <asp:Button runat="server" ID="btnBack" Text="Back"  Width="100px"  OnClick="btnBack_Click"/>           
         </ContentTemplate>
          </asp:UpdatePanel>
   
         
       </div>

</asp:Content>
