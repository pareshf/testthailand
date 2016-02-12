<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AgentDownloadVoucher.aspx.cs" Inherits="CRM.WebApp.Views.Account.AgentDownloadVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

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

    <asp:Label runat="server" Text="Download Vouchers" ID="headlbl" Width="400px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
         <div>
   <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
            <ContentTemplate>

            <table>
            
            <tr>
            <td>
                <asp:Label ID="LBL" runat="server" Text="Quotation Reference No."></asp:Label></td>
            <td>
                <asp:DropDownList ID="drp_quote" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="drp_quote_SelectedIndexChanged" width="200px">
                </asp:DropDownList>
            
            </tr>

            </table>

            <br />
            <br />

            <table width="100%">
            <tr>
            <td>
             <asp:GridView ID="GV_Result" runat="server" 
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10"   onrowcommand="GV_Result_RowCommand">
                              <Columns>
                              
                                      <asp:BoundField DataField="SUPPLIER_TYPE_NAME" HeaderText="Supplier Type" />
                                   <asp:BoundField DataField="SUPPLIER_COMPANY_NAME" HeaderText="Company Name" />
                                  
                                  <asp:BoundField DataField="TRANSFER_PACKAGE_DETAIL_NAME" HeaderText="Transfer Package Name" />
                                    
                                    <asp:BoundField DataField="DATE" HeaderText="Date" />
                               <asp:buttonfield buttontype="Button" 
                  commandname="Download" 
                  text="Download PDF"/>
           
                 </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

          

            </ContentTemplate>
             <Triggers>
                                   <asp:PostBackTrigger ControlID="GV_Result" />
                               </Triggers>
            </asp:UpdatePanel>
            </div>
</asp:Content>