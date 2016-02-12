<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyRoleNavigationBox.ascx.cs"
    Inherits="CRM.WebApp.Views.Shared.Controls.Navigation.CompanyRoleNavigationBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">
    function SelectMeOnly(objRadioButton, grdName) {
        var i, obj, pageElements;
        if (navigator.userAgent.indexOf("MSIE") != -1) {
            //IE browser  
            pageElements = document.all;
        }
        else if (navigator.userAgent.indexOf("Mozilla") != -1 || navigator.userAgent.indexOf("Opera") != -1) {
            //FireFox/Opera browser  
            pageElements = document.documentElement.getElementsByTagName("input");
        }
        for (i = 0; i < pageElements.length; i++) {
            obj = pageElements[i];

            if (obj.type == "radio") {
                //alert(objRadioButton.id);
                //alert(obj.id);
                //if (objRadioButton.id.substr(0, grdName.length) == grdName)  
                //{  
                if (objRadioButton.id == obj.id) {
                    obj.checked = true;
                }
                else {
                    obj.checked = false;
                }
                //}  
            }
        }
    }  
</script>

<telerik:RadGrid ID="grdCompanyRole" runat="server" Width="100%" AllowPaging="false" AllowSorting="false">
    <ClientSettings>
        <Selecting AllowRowSelect="true"></Selecting>
    </ClientSettings>
    <MasterTableView AutoGenerateColumns="False">
        <RowIndicatorColumn>
            <HeaderStyle Width="25px"></HeaderStyle>
        </RowIndicatorColumn>
        <Columns>
            <telerik:GridTemplateColumn ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:RadioButton ID="rbCompanyRole" runat="server" GroupName="rbGrprbCompanyRole" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Company" DataField="COMPANY_NAME" UniqueName="COMPANY_NAME">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Type" DataField="IS_COMPANY_FRANCHISE" UniqueName="IS_COMPANY_FRANCHISE"
                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Role" DataField="ROLE_NAME" UniqueName="ROLE_NAME">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Company" DataField="COMPANY_ID" UniqueName="COMPANY_ID"
                Visible="false">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Company" DataField="ROLE_ID" UniqueName="ROLE_ID"
                Visible="false">
            </telerik:GridBoundColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<table width="100%">
    <tr>
        <td align="left">
            <asp:Label ID="lblFooter" runat="server" Text="With selected"></asp:Label>&nbsp;
            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
            <asp:Button ID="btnLoadAndSetAsDefault" runat="server" Text="Load & Set as Default"
                OnClick="btnLoadAndSetAsDefault_Click" />
        </td>
    </tr>
</table>
