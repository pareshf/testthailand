<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="GroupTypeMaster.aspx.cs" Inherits="CRM.WebApp.Views.AccountMaster.GroupTypeMaster" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<style>
     .textboxstyle
        {
            width: 50px;
        }
         .lblstyle
        {
            width: 100px;
            font-family:Verdana ;
            font-size:12px
        }
</style>
<br />
<asp:Label ID="headLabel" runat="server" Text="Account Group Type Master" Font-Bold="true" Font-Size="16px"></asp:Label>
<br />

<div>

<asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>

            <table width="1000px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
            
          <tr style="background-color:#f3f3f3">
                <td style="width:200px" ><asp:Label ID="Label1" runat="server" Text="Group Type Name" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span></td>
                <td style="width:400px"><asp:TextBox ID="txtGroupName" runat="server" Width="200px"></asp:TextBox>
                 <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator1" runat="server" ErrorMessage="Group Name is required." ValidationGroup="required" Font-Size="12px" ControlToValidate="txtGroupName"></asp:RequiredFieldValidator></td>
            </tr>

           <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label2" runat="server" Text="From Digit" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span></td>
                <td style="width:400px"><asp:TextBox ID="txtFromDigit" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator2" runat="server" ErrorMessage="From Digit is required." ValidationGroup="required" Font-Size="12px" ControlToValidate="txtFromDigit"></asp:RequiredFieldValidator>
                               </td>
            </tr>

           <tr style="background-color:#f3f3f3">
                <td style="width:200px"><asp:Label ID="Label3" runat="server" Text="Interval" CssClass="lblstyle"></asp:Label>&nbsp;<span class="error">*</span></td>
                <td style="width:600px"><asp:TextBox ID="txtInterval" runat="server" Width="200px" ControlToValidate="txtInterval"></asp:TextBox>
                <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator3" runat="server" ErrorMessage="Interval is required." ValidationGroup="required" Font-Size="12px" ControlToValidate="txtInterval"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Digit is allowed." ValidationExpression="^[0-9]+$" ControlToValidate="txtInterval" ValidationGroup="required" Font-Size="12px" Display="Dynamic" ></asp:RegularExpressionValidator >
                               </td>
            </tr>
            
            </table>

            </ContentTemplate>
            </asp:UpdatePanel>

</div>
<br />
<div>
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" ValidationGroup="required" />
</div>

<br />
<br />
<asp:Label ID="Label4" runat="server" Text="Account Group Type Master Records" Font-Bold="true" Font-Size="14px"></asp:Label>
<br />

<div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>
                <asp:GridView ID="GV_Result" runat="server" OnSelectedIndexChanging="GV_Result_SelectedIndexChanging"
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="true" OnPageIndexChanging="GV_Result_PageIndexChanging" pagesize="10" width="800px">
                                <pagersettings mode="NumericFirstLast" position="Bottom" pagebuttoncount="10"/>
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Edit" />
                                    <asp:BoundField DataField="GROUP_TYPE_ID" HeaderText="Group Type ID." />
                                    <asp:BoundField DataField="GROUP_TYPE_NAME" HeaderText="Group Type Name" />
                                    <asp:BoundField DataField="FROM_DIGIT" HeaderText="Starting Digit" />
                               
                                    <asp:BoundField DataField="INTERVAL" HeaderText="Interval" />
                                    
                                     
                                </Columns>
                                <HeaderStyle CssClass="rgHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
            </ContentTemplate>
            </asp:UpdatePanel>

</div>


</asp:Content>
