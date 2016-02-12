<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TransportPackgePriceListGIT.aspx.cs" Inherits="CRM.WebApp.Views.GITMaster.TransportPackgePriceListGIT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">

  <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">

    <asp:Label ID="Label55" runat="server" Text="Transfer Package Master" class="pageTitle"
            Width="400px" Font-Bold="true" Font-Size="Large"></asp:Label>
    <br />
    <br />

        <div class="pageTitle" >
        <asp:UpdatePanel ID="upTrasport" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>

            <table>
            
             <tr>
                <td>
                    <asp:Label runat="server" ID="Label1" Text = "Trasport Package Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                </td>

                <td> &nbsp;&nbsp;
                    <asp:TextBox ID="txtTrasportPackageName" runat="server" Width="200px"> </asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Client Name Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtTrasportPackageName"></asp:RequiredFieldValidator>
                </td>
            </tr>

             <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="GIT Package Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        <td> &nbsp;&nbsp;
                            <asp:DropDownList ID="drpGitPackageName" runat="server" Width="205px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Client Name Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="drpGitPackageName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

             <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Supplier Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        <td> &nbsp;&nbsp;
                            <asp:DropDownList ID="drpSupplierName" runat="server" Width="205px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Client Name Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="drpSupplierName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

             <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        <td> &nbsp;&nbsp;
                            <asp:DropDownList ID="drpStatus" runat="server" Width="205px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Client Name Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="drpStatus"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

            </table>
            <br />

            <asp:Button runat="server" ID="btnSave" Text = "Save" OnClick="btnSave_Click" ValidationGroup="Required"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
                
        <asp:UpdatePanel ID="upTrasportDetails" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Label ID="lblTransferDetailsHeader" runat="server" Text = "Trasport Details" Font-Size = "12" Visible = "false" class="pageTitle"> </asp:Label>
            <div runat="server" class="pageTitle" id="TrasportDetails" visible = "false" style= "border: 1px coral solid; border-color:#C0C0C0; margin-left :10px; margin-right:500px">
            
             <br />        
                       <asp:GridView ID="gridDetails" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="500px" Visible = "false">
                                    <Columns>
                                    
                                     <asp:TemplateField  ControlStyle-Width="200px" ItemStyle-Width = "200px" >
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="FROM" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate >
                                                <asp:DropDownList ID="drpFrom" runat="server"  />
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField ControlStyle-Width="200px" ItemStyle-Width = "200px" >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="TO" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpTo" runat="server"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                        </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="ADD" runat="server" ID="btnSiteAdd"  OnClick="btnAdd_Click"/>
                     <br />
                     <br />
                     <asp:Button Text="Save Trasport Details" runat="server" ID="btnTrasportDetails" OnClick="btnSaveDetails_Click" />   
                        </div>
            </ContentTemplate>

            </asp:UpdatePanel>
                 
        <br /> 

        
        <asp:UpdatePanel runat="server" ID="upCoach" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Label ID="lblHeaderCoachDetails" runat="server" Text = "Coach Details" Font-Size ="12" Visible = "false" class="pageTitle"> </asp:Label>
        <div class="pageTitle" id="divCoach" runat="server" visible="false" style= "border: 1px coral solid; border-color: #C0C0C0; margin-left :10px; margin-right:500px" >
        
            
        <br />    
                <table class="pageTitle">
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Supplier Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        
                        <td>
                             <asp:DropDownList ID="drpCoachSupplier" runat="server" Width="205px"></asp:DropDownList>
                        </td>  
                    </tr>

                    <tr>
                        <td>
                              <asp:Label ID="Label6" runat="server" Text="Rate"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>

                        <td>
                             <asp:TextBox ID="txtCoachRate" runat="server" Width="200px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button runat="server" Text = "Save Coach" ID="btnCoach" OnClick="btnSaveCoach_Click"/>
                <br />
                <br />
                <asp:Label runat="server" ID="lbl11" Text = "All Coaches Rates"></asp:Label>
                <br />
                   <table width="100%" class="pageTitle">
                    <tr>
                        <td>
                            <asp:GridView ID="gridAllCoached" runat="server" OnSelectedIndexChanging="GV_Coach_SelectedIndexChanging"
                         
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  pagesize="10" Width="500px">
                                  <%-- OnSelectedIndexChanging="GV_Result_SelectedIndexChanging" OnPageIndexChanging="GV_Result_PageIndexChanging"--%>
                                <%--<pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>--%>
                                <Columns>
                                    <asp:CommandField  ShowSelectButton="True" ButtonType="Button" SelectText="Edit" ControlStyle-Width ="30px" ItemStyle-Width = "30px" />
                                   <%-- <asp:BoundField DataField="ACCOUNT_VOUCHER_ID" HeaderText="Voucher Id" />
                                    <asp:BoundField DataField="SEQ_NO" HeaderText="Sequence No" />--%>
                                  
                                    <%--<asp:BoundField DataField="COACH_PRICE_LIST_ID" HeaderText="CoachId" Visible = "false" />--%>
                                    <asp:TemplateField Visible = "false">
                                     <HeaderTemplate>
                                      <asp:Label ID = "lblHeader1" runat="server" Text="Coach ID" CssClass="lblstyleGIT"></asp:Label>
                                 </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID = "lblCoachID" runat="server" Text='<%# Bind("COACH_PRICE_LIST_ID") %>' CssClass="lblstyle"></asp:Label>
                                     </ItemTemplate>
                                         </asp:TemplateField>
                                    <asp:BoundField DataField="CHAIN_NAME" HeaderText="Coach Name" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    <asp:BoundField DataField="COACH_RATE" HeaderText="Coach Rate" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
       
              
       </div>
         <br />
            </ContentTemplate>
        </asp:UpdatePanel> 

        <asp:UpdatePanel runat="server" ID="upBoat" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Label ID="lblHeaderBoatDetails" runat="server" Text = "Boat Details" Font-Size = "12" Visible = "false" class="pageTitle"> </asp:Label>
              <div class="pageTitle" id="divboat" runat="server" visible="false" style= "border: 1px coral solid; border-color: #C0C0C0; margin-left :10px; margin-right:500px" >
            
        <br />    
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Supplier Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        
                        <td>
                             <asp:DropDownList ID="drpBoatSupplier" runat="server" Width="205px"></asp:DropDownList>
                        </td>  
                    </tr>

                    <tr>
                        <td>
                              <asp:Label ID="Label10" runat="server" Text="Rate"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>

                        <td>
                             <asp:TextBox ID="txtBoatRate" runat="server" Width="200px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button runat="server" Text = "Save Boat Rate" ID="Button1" OnClick="btnSaveBoat_Click"/>
                <br />
                <br />
                <asp:Label runat="server" ID="Label11" Text = "All Boats Rates"></asp:Label>
                <br />
                   <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gridBoats" runat="server" OnSelectedIndexChanging="GV_Boat_SelectedIndexChanging"
                         
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  pagesize="10" Width="500px">
                                  
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" ControlStyle-Width ="30px" ItemStyle-Width = "30px"/>
                                   
                                  <asp:TemplateField Visible = "false">
                                     <HeaderTemplate >
                                      <asp:Label ID = "lblHeader1" runat="server" Text="Guide ID" CssClass="lblstyleGIT"></asp:Label>
                                 </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID = "lblBoatID" runat="server" Text='<%# Bind("BOAT_PRICE_LIST_ID") %>' CssClass="lblstyle"></asp:Label>
                                     </ItemTemplate>
                                         </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="CHAIN_NAME" HeaderText="Boat" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    <asp:BoundField DataField="BOAT_RATE" HeaderText="Boat Rate" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                 </div>
                 <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    
        <asp:UpdatePanel runat="server" ID="upGuide" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Label ID="lblHeaderGuideDetails" runat="server" Text = "Guide Details" Font-Size ="12" Visible = "false" class="pageTitle"> </asp:Label>
             <div class="pageTitle" id="divGuide" runat="server" visible="false" style= "border: 1px coral solid; border-color: #C0C0C0; margin-left :10px; margin-right:500px" > 
            
        <br />    
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Supplier Name"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>
                        
                        <td>
                             <asp:DropDownList ID="drpGuide" runat="server" Width="205px"></asp:DropDownList>
                        </td>  
                    </tr>

                    <tr>
                        <td>
                              <asp:Label ID="Label14" runat="server" Text="Rate"></asp:Label>&nbsp;<span
                        class="error">*</span>
                        </td>

                        <td>
                             <asp:TextBox ID="txtGuideRate" runat="server" Width="200px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button runat="server" Text = "Save Guide Rate" ID="Button2" OnClick="btnSaveGuide_Click"/>
                <br />
                <br />
                <asp:Label runat="server" ID="Label15" Text = "All Guide Rates"></asp:Label>
                <br />
                   <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="gridGuide" runat="server"  OnSelectedIndexChanging="GV_Guide_SelectedIndexChanging"
                         
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true"  pagesize="10" Width="500px">
                                  <%-- OnSelectedIndexChanging="GV_Result_SelectedIndexChanging" OnPageIndexChanging="GV_Result_PageIndexChanging"--%>
                                <%--<pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>--%>
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" ControlStyle-Width ="30px" ItemStyle-Width = "30px"/>
                                   <%-- <asp:BoundField DataField="ACCOUNT_VOUCHER_ID" HeaderText="Voucher Id" />
                                    <asp:BoundField DataField="SEQ_NO" HeaderText="Sequence No" />--%>
                                  <asp:TemplateField Visible = "false">
                                     <HeaderTemplate>
                                      <asp:Label ID = "lblHeader1" runat="server" Text="Guide ID" CssClass="lblstyleGIT"></asp:Label>
                                 </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID = "lblGuideID" runat="server" Text='<%# Bind("GUIDE_PRICE_LIST_ID") %>' CssClass="lblstyle"></asp:Label>
                                     </ItemTemplate>
                                         </asp:TemplateField>
                                    <%--<asp:BoundField DataField="GUIDE_PRICE_LIST_ID" HeaderText="Guide ID" Visible = "false" />--%>
                                    <asp:BoundField DataField="CHAIN_NAME" HeaderText="Guide" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    <asp:BoundField DataField="GUIDE_RATE" HeaderText="Guide Rate" ControlStyle-Width ="200px" ItemStyle-Width = "200px"/>
                                    
                                     

                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                 </div>
            </ContentTemplate>
        </asp:UpdatePanel>

   

</asp:Content>
