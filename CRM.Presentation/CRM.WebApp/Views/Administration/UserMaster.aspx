<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="UserMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.UserMaster"
    Title="User Master - Travel CRM" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ControlBox.ascx" TagPrefix="UC_CB"
    TagName="ControlBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">

    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oTitle) {
            var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually
            //Cancel the event
            ev.cancelBubble = true;
            ev.returnValue = false; 
            if (ev.stopPropagation) ev.stopPropagation();
            if (ev.preventDefault) ev.preventDefault();

            //Determine who is the caller
            var callerObj = ev.srcElement ? ev.srcElement : ev.target;
            //Call the original radconfirm and pass it all necessary parameters
            if (callerObj) {
                //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again.
                var callBackFn = function(arg) {
                    if (arg) {
                        callerObj["onclick"] = "";
                        if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz
                        else if (callerObj.tagName == "A") //We assume it is a link button!
                        {
                            try {
                                eval(callerObj.href)
                            }
                            catch (e) { }
                        }
                    }
                }
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle);
            }
            return false;
        } 
    </script>

    <script language="javascript" type="text/javascript">
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';
    </script>

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageAddress" runat="server" Text="User Master"></asp:Literal>
    </div>
    <div style="float: right; padding-top: 3px" class="normaltext"></div>
    <div align="right">
        <table cellpadding="0" cellspacing="0" border="0" style="height: 35px;">
            <tr>
                <td style="height: 30px;" valign="top">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../Shared/Images/Background/non-selected_first.jpg"
                        Width="31" Height="30" />
                </td>
                <td style="background: url(../Shared/Images/Background/non-selected_bg.jpg) repeat-x;
                    padding-bottom: 5px;">
                    <asp:LinkButton ID="lnkRegisterEmp" runat="server" PostBackUrl="~/Views/HR/EmployeeMaster.aspx?Action=AddNew"
                        Text="Create Employee" CssClass="teb_text"></asp:LinkButton>
                </td>
                <td style="height: 30px;" valign="top">
                    <asp:Image ID="Image2" runat="server" ImageUrl="../Shared/Images/Background/selected_sec.jpg"
                        Width="32" Height="30" />
                </td>
                <td style="background: url(../Shared/Images/Background/selected_bg.jpg) repeat-x;
                    padding-bottom: 5px;">
                    <asp:LinkButton ID="lnkRegisterUser" runat="server" PostBackUrl="~/Views/Administration/UserMaster.aspx?Action=addnewuser"
                        Text="Create User Access For Employee" CssClass="teb_text_selected"></asp:LinkButton>
                </td>
                <td style="height: 30px;" valign="top">
                    <img src="../Shared/Images/Background/selected_last.jpg" width="18" height="30" alt="" /></td>
            </tr>
        </table>
        <%--<b>Step 3:</b> Finish &nbsp;&nbsp;&nbsp;&nbsp;--%></div>
    <%--   <div style="float:right; padding-top:3px" class="normaltext">
    &nbsp;&nbsp;&nbsp;
    <b>Step 1:</b> <asp:LinkButton ID="lnkRegisterEmp" runat="server" PostBackUrl="~/Views/Administration/UserMaster.aspx?Action=AddNewUser" Text="Create New User"></asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Views/Shared/Images/Icon/PanelBar/icon_forms.gif" />
    <b>Step 2:</b> <asp:LinkButton ID="lnkRegisterUser" runat="server" PostBackUrl="~/Views/Administration/UserMaster.aspx?Action=AssignRole" Text="Assign Role"></asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;
   
    </div>--%>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td>
                <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <crmUC:ActionBar ID="acbUser" runat="server" OnbtnNewClick="acbUser_NewClick" SaveButtonValidationGroup="User"
                            OnbtnDeleteClick="acbUser_DeleteClick" OnbtnEditClick="acbUser_EditClick" OnbtnSearchClick="acbUser_SearchClick"
                            OnbtnRefreshClick="acbAddress_RefreshClick" VisibleSearchTextBox="true" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:UpdatePanel ID="upEditMode" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddMode" runat="server" Visible="false">
                            <telerik:RadTabStrip ID="radTabStripe" runat="server" MultiPageID="radMultiPage">
                                <Tabs>
                                    <telerik:RadTab Text="User Info">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="User Access">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="radMultiPage" runat="server">
                                <telerik:RadPageView ID="radPageUserInfo" runat="server">
                                    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                        <tr>
                                            <th colspan="2">
                                                <asp:Literal runat="server" ID="ltlTableHeader" Text="New User"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <asp:Label ID="lblUserName" runat="server" Text="UserName :"></asp:Label>
                                                &nbsp;<asp:Label ID="lblNameRequire" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" runat="server" MaxLength="25"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ControlToValidate="txtUserName"
                                                    CssClass="error" ErrorMessage="Required" Display="Dynamic" ValidationGroup="User"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label>
                                                &nbsp;<asp:Label ID="lblPasswordRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtPassword" runat="server" ControlToValidate="txtPassword"
                                                    CssClass="error" ErrorMessage="Required" Display="Dynamic" ValidationGroup="User"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRetypePassword" runat="server" Text="Retype Password :"></asp:Label>
                                                &nbsp;<asp:Label ID="lblRetypePasswordRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRetypePassword" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                                                    CssClass="error" ErrorMessage="Required" Display="Dynamic" ValidationGroup="User"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cmptxtRetypePassword" runat="server" Display="Dynamic"
                                                    ControlToCompare="txtPassword" ControlToValidate="txtRetypePassword" ErrorMessage="Password not match"
                                                    ValidationGroup="User" Operator="Equal" CssClass="error" Type="String"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name :"></asp:Label>
                                                &nbsp;<asp:Label ID="lblEmployeeNameRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <%--<telerik:RadComboBox runat="server" ID="ddlEmployeeName" HighlightTemplatedItems="true"
                                                    Width="225px">
                                                    <%--DropDownWidth="400px"
                                                    <HeaderTemplate>
                                                        <table width="350px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="150px">
                                                                    Employee
                                                                </td>
                                                                <%--<td width="100px">
                                                                    Designation
                                                                </td>
                                                                <td width="100px">
                                                                    Department
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="350px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="150px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "EMP_NAME")%>
                                                                </td>
                                                                <%--<td width="100px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "DESIGNATION_DESC")%>
                                                                </td>
                                                                <td width="100px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "DEPARTMENT_NAME")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                                </telerik:RadComboBox>--%>
                                                <telerik:RadComboBox ID="ddlEmployeeName" runat="server" Width="225px" Height="<%$appSettings:ComboBoxHeight %>">
                                                </telerik:RadComboBox>
                                                <asp:CompareValidator ID="cmpddlEmployeeName" ControlToValidate="ddlEmployeeName"
                                                    Operator="NotEqual" ValueToCompare="--Select--" Type="String" runat="server"
                                                    ErrorMessage="Required" CssClass="error" ValidationGroup="User"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSecurityQuestionId" runat="server" Text="Security Question :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblSecurityQuestionIdRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="ddlSecurityQus" runat="server" Width="225px" Height="<%$appSettings:ComboBoxHeight %>">
                                                </telerik:RadComboBox>
                                                <asp:CompareValidator ID="cmpddlSecurityQus" ControlToValidate="ddlSecurityQus" Operator="NotEqual"
                                                    ValueToCompare="--Select--" Type="String" runat="server" CssClass="error" ErrorMessage="Required"
                                                    ValidationGroup="User"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSecurityQusAns" runat="server" Text="Security Answer :"></asp:Label>&nbsp;
                                                <asp:Label ID="lblSecurityQusAnsRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSecurityQusAns" runat="server" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtSecurityQusAns" runat="server" ControlToValidate="txtSecurityQusAns"
                                                    CssClass="error" ErrorMessage="Required" Display="Dynamic" ValidationGroup="User"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFromDate" runat="server" Text="From Date :"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblFromDateRequired" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="English (United States)"
                                                    MinDate="1900-01-01">
                                                    <DateInput ID="DateInput1" runat="server" DateFormat="d/M/yyyy">
                                                    </DateInput>
                                                    <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <asp:RequiredFieldValidator ID="rfvrdpFromDate" runat="server" ControlToValidate="rdpFromDate"
                                                    CssClass="error" ErrorMessage="Required" Display="Dynamic" ValidationGroup="User"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTodate" runat="server" Text="To Date :"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="English (United States)"
                                                    MinDate="1900-01-01">
                                                    <DateInput ID="DateInput2" runat="server" DateFormat="d/M/yyyy">
                                                    </DateInput>
                                                    <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                                        ViewSelectorText="x">
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <%--<asp:RequiredFieldValidator ID="rfvrdpToDate" Display="Dynamic" runat="server" ControlToValidate="rdpToDate"
                                                    CssClass="error" ErrorMessage="Required" ValidationGroup="User"></asp:RequiredFieldValidator>--%>
                                                <asp:CompareValidator ID="cmprdpToDate" runat="server" ControlToCompare="rdpFromDate"
                                                    ControlToValidate="rdpToDate" Type="Date" ValidationGroup="User" Operator="GreaterThanEqual"
                                                    Display="Dynamic" CssClass="error" ErrorMessage="Less than from data"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td colspan="2">
                                                <asp:UpdatePanel ID="upControlBox" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <UC_CB:ControlBox ID="ucControlBox" runat="server" VisibleCopyButton="false" SaveButtonValidationGroup="User"
                                                            VisibleSaveNewButton="false" OnbtnSaveClick="acbUser_SaveClick" OnbtnCancelClick="acbUser_CancelClick"
                                                            OnbtnClearClick="upControlBox_ClearClick" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="radPageUserAccess" runat="server">
                                    <table width="100%" class="TableForm">
                                        <tr>
                                            <th colspan="2">
                                                <asp:Literal runat="server" ID="ltrTableHeaderAccess" Text="User Access"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="70%">
                                                    <tr align="left" valign="middle">
                                                        <td style="width: 20%;">
                                                            <div style="width: 100%; float: left; height: 20px;">
                                                                <asp:Label ID="lblCompanyList" runat="server" Text="Company"></asp:Label>
                                                            </div>
                                                            <div style="width: 100%; float: left;">
                                                                <asp:ListBox ID="lstCompanyName" runat="server" Height="150px" Width="180px"></asp:ListBox>
                                                            </div>
                                                        </td>
                                                        <td style="width: 5%;">
                                                            <div>
                                                                <div style="width: 100%; float: left; height: 30px;">
                                                                    <asp:Button ID="btnAssignRightCompany" runat="server" Text=">>" OnClick="btnAssignRightCompany_Click"
                                                                        ToolTip="Assign Company to user" />
                                                                </div>
                                                                <div style="width: 100%; float: left; height: 30px;">
                                                                    <asp:Button ID="btnAssignLeftCompany" runat="server" Text="<<" OnClick="btnAssignLeftCompany_Click"
                                                                        ToolTip="Unassign Company to user" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div style="width: 100%; float: left; height: 20px;">
                                                                <asp:Label ID="lblAssignedCompany" runat="server" Text="Assigned Company"></asp:Label>
                                                            </div>
                                                            <div style="width: 100%; float: left;">
                                                                <asp:ListBox ID="lstAssignCompanyName" runat="server" Height="150px" Width="180px"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="lstAssignCompanyName_OnSelectedIndexChanged">
                                                                </asp:ListBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                        </td>
                                                    </tr>
                                                    <tr valign="middle">
                                                        <td>
                                                            <div style="width: 100%; float: left; height: 20px;">
                                                                <asp:Label ID="lblRoleList" runat="server" Text="Role"></asp:Label>
                                                            </div>
                                                            <div style="width: 100%; float: left;">
                                                                <asp:ListBox ID="lstRole" runat="server" Height="150px" Width="180px"></asp:ListBox>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div>
                                                                <div style="width: 100%; float: left; height: 30px;">
                                                                    <asp:Button ID="btnAssignRightRole" runat="server" Text=">>" OnClick="btnAssignRightRole_Click"
                                                                        ToolTip="Assign role to company" />
                                                                </div>
                                                                <div style="width: 100%; float: left; height: 30px;">
                                                                    <asp:Button ID="btnAssignLeftRole" runat="server" Text="<<" OnClick="btnAssignLeftRole_Click"
                                                                        ToolTip="Unassign role to company" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div style="width: 100%; float: left; height: 20px;">
                                                                <asp:Label ID="lblAssignedRole" runat="server" Text="Assigned Role"></asp:Label>
                                                            </div>
                                                            <div style="width: 100%; float: left;">
                                                                <asp:ListBox ID="lstAssignRole" runat="server" Height="150px" Width="180px"></asp:ListBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                        <UC_CB:ControlBox ID="ucAccessFooter" runat="server" VisibleCopyButton="false" VisibleSaveNewButton="false"
                                                            VisibleClearButton="false" VisibleSaveButton="false" OnbtnCancelClick="usCancelAccessFoolter_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbUser" />
                        <asp:AsyncPostBackTrigger ControlID="ucControlBox" />
                        <asp:AsyncPostBackTrigger ControlID="ucAccessFooter" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <telerik:RadGrid ID="radgrdUser" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="true"
                                AllowMultiRowEdit="true" AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                OnPreRender="radgrdUser_PreRender" OnItemDataBound="radgrdUser_ItemDataBound"
                                OnNeedDataSource="radgrdUser_NeedDataSource" PageSize="<%$appSettings:GridPageSize %>">
                                <ClientSettings>
                                    <ClientEvents OnRowClick="OnRowClick" />
                                    <Selecting AllowRowSelect="true"></Selecting>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </HeaderContextMenu>
                                <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                    PagerStyle-AlwaysVisible="true">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemStyle CssClass="ItemAlign" Width="25px" />
                                            <HeaderStyle Width="25px" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Id" UniqueName="USER_ID" DataField="USER_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="User Name" UniqueName="USER_NAME" DataField="USER_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Employee Name" UniqueName="EMP_NAME" DataField="EMP_NAME"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Employee ID" UniqueName="EMP_ID" DataField="EMP_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Employee Name" UniqueName="EMP_FULL_NAME" DataField="EMP_FULL_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="From Date" UniqueName="FROM_DATE" DataField="FROM_DATE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="To Date" UniqueName="TO_DATE" DataField="TO_DATE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Created By" UniqueName="CREATED_BY" DataField="CREATED_BY">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Modified By" UniqueName="MODIFIED_BY" DataField="MODIFIED_BY">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Password" UniqueName="PASSWORD" DataField="PASSWORD"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Security Qus" UniqueName="SECURITY_QUESTION_DESC"
                                            DataField="SECURITY_QUESTION_DESC" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Security Ans" UniqueName="SECURITY_ANSWERS"
                                            DataField="SECURITY_ANSWERS" Visible="false">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True" />
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </FilterMenu>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbUser" />
                        <asp:AsyncPostBackTrigger ControlID="ucControlBox" />
                        <asp:AsyncPostBackTrigger ControlID="ucAccessFooter" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100"
                    Behaviors="Move">
                    <ConfirmTemplate>
                        <div class="rwDialogPopup radconfirm">
                            <div class="rwDialogText">
                                {1}
                            </div>
                            <div>
                                <a onclick="$find('{0}').close(true);" class="rwPopupButton" href="javascript:void(0);">
                                    <span class="rwOuterSpan"><span class="rwInnerSpan">##LOC[OK]##</span></span></a>
                                <a onclick="$find('{0}').close(false);" class="rwPopupButton" href="javascript:void(0);">
                                    <span class="rwOuterSpan"><span class="rwInnerSpan">##LOC[Cancel]##</span></span></a>
                            </div>
                        </div>
                    </ConfirmTemplate>
                </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
</asp:Content>
