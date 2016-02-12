<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ChartofAccount.aspx.cs" Inherits="CRM.WebApp.Views.AccountMaster.ChartofAccount" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
     .textboxstyle
        {
            width: 250px;
        }
         .lblstyle
        {
            width: 100px;
            font-family:Verdana ;
            font-size:12px
        }
</style>
<br />

<asp:Label ID="headLabel" runat="server" Text="Chart Of Account" Font-Bold="true" Font-Size="16px"></asp:Label>
<br />

<div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>

            <table width="1000px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">


            <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label1" runat="server" Text="Account Type" CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <asp:DropDownList ID="drpAccType" runat="server" CssClass="textboxstyle"  
                        AutoPostBack="true" onselectedindexchanged="drpAccType_SelectedIndexChanged">
                    </asp:DropDownList>
            </td>
            </tr>

              <tr id="trACE" runat="server" visible="false">
                <td><asp:Label ID="lblASE" runat="server" Text="" CssClass="lblstyle"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="drpACE" runat="server" CssClass="textboxstyle" 
                        onselectedindexchanged="drpACE_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
            </td>
            </tr>

            <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="GL Code" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
            
            <asp:TextBox ID="txtGlcode" runat="server" Enabled="false" width="200px"></asp:TextBox>
            </td>
            </tr>

            <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Account Name" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span>
            </td>
                
            <td>
            <asp:TextBox ID="txtAccountname" runat="server" width="200px" 
                    ontextchanged="txtAccountname_TextChanged" AutoPostBack="true"></asp:TextBox>

              <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator2" runat="server" ErrorMessage="Account Name is Required." ValidationGroup="required" Font-Size="12px" ControlToValidate="txtAccountname"></asp:RequiredFieldValidator>
            </td>
            </tr>

            <tr id="trAddress" runat="server" visible="false">
            <td>
                <asp:Label ID="Label4" runat="server" Text="Address" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblAddress" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trcity" runat="server" visible="false">
            <td>
                <asp:Label ID="Label5" runat="server" Text="City" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblCity" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trState" runat="server" visible="false">
            <td>
                <asp:Label ID="Label6" runat="server" Text="State" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblState" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trCountry" runat="server" visible="false">
            <td>
                <asp:Label ID="Label7" runat="server" Text="Country" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblCountry" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trEmail" runat="server" visible="false">
            <td>
                <asp:Label ID="Label8" runat="server" Text="E-Mail" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblEmail" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trPhno" runat="server" visible="false">
            <td>
                <asp:Label ID="Label9" runat="server" Text="Phone No" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblPhno" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            <tr id="trFaxno" runat="server" visible="false">
            <td>
                <asp:Label ID="Label10" runat="server" Text="Fax No" CssClass="lblstyle"></asp:Label>
            </td>
                
            <td>
           <asp:Label ID="lblfaxno" runat="server" Text="" CssClass="lblstyle"></asp:Label>
            
            </td>
            </tr>

            </table>

            </ContentTemplate>

            </asp:UpdatePanel>
</div>
            <br />

            <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>

            <table width="1000px" id="Table1" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">

            <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label11" runat="server" Text="Under Group" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span></td>
                <td style="width:400px">
                    <telerik:radcombobox id="drpQuestion" runat="server" autopostback="True" markfirstmatch="true"
                                                        highlighttemplateditems="true" height="150px" width="350px" 
                                                        onitemdatabound="RadComboBox1_ItemDataBound">


                                                  <ItemTemplate>
                                                    <table>
                                                    <tr>
                                                    
                                                    <td style="width:200px"><%# DataBinder.Eval(Container.DataItem, "MAIN_GROUP")%> </td>
                                                    
                                                    <td style="width:150px"><%# DataBinder.Eval(Container.DataItem, "UNDER_GROUP")%></td>
                                                    </tr>
                                                    </table>
                                                     <%--<ul>
                                                            <li class="col1">
                                                                    <%# DataBinder.Eval(Container.DataItem, "MAIN_GROUP")%></li>
                                                    </ul>
                                                    <ul>
                                                             <li class="col2">
                                                                     <%# DataBinder.Eval(Container.DataItem, "UNDER_GROUP")%></li>
                                                                    
                                                     </ul>--%>
                                                     </ItemTemplate>
                                                 </telerik:radcombobox>

                    <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator1" runat="server" ErrorMessage="Account Name is Required." ValidationGroup="required" Font-Size="12px" ControlToValidate="drpQuestion"></asp:RequiredFieldValidator>
            </td>
            </tr>

            <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label12" runat="server" Text="Opening Balance" CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <asp:TextBox ID = "txtOpbalance" runat="server" Width="200px"> </asp:TextBox>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="50px">
                    </asp:DropDownList>
            </td>
            </tr>

            <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label13" runat="server" Text="Opening Balance as on Date" CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <asp:TextBox ID = "txtOPdate" runat="server" CssClass="textboxstyle"> </asp:TextBox>
                      <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtOPdate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
            </td>
            </tr>

             <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label14" runat="server" Text="Income Tax No." CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <asp:TextBox ID = "txtIncometax" runat="server" CssClass="textboxstyle"> </asp:TextBox>
            </td>
            </tr>

             <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label15" runat="server" Text="Sales Tax No." CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <asp:TextBox ID = "txtSalestax" runat="server" CssClass="textboxstyle"> </asp:TextBox>
            </td>
            </tr>

            <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label16" runat="server" Text="Attachment" CssClass="lblstyle"></asp:Label></td>
                <td style="width:400px">
                    <telerik:radupload runat="server" id="upTermsConditionsAttachement" controlobjectsvisibility="None"></telerik:radupload>
                            <a runat="server" href="" id="termcondition">View</a>
            </td>
            </tr>

            </table>

            </ContentTemplate>
           <%-- <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
         <%--   <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" /> 
        </Triggers>--%>
            </asp:UpdatePanel>
            </div>
            <br />
            <br />

            <div>
             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <asp:Button ID="Button1" runat="server" Text="Save" ValidationGroup="required" 
                    onclick="Button1_Click"/>
                
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>

</asp:Content>
