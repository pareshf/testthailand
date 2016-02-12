<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="MyProfile.aspx.cs" Inherits="CRM.WebApp.Views.Customers.MyProfile"
    Title="My Profile" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ControlBox.ascx" TagName="ControlBox"
    TagPrefix="crmUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">


     
        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
      

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oEmployee) {
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
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oEmployee);
            }
            return false;
        } 
    </script>

    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 13%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100">
        <confirmtemplate>
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
        </confirmtemplate>
    </telerik:RadWindowManager>
    <div class="pageTitle">
        <asp:Literal ID="lblPageProfile" runat="server" Text="My Profile"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpProfile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlDisplayInfo" runat="server" Visible="false">
                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <tr>
                        <th colspan="2">
                            <asp:Literal runat="server" ID="Literal2" Text="User Information"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblt" runat="server" Text="Title :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblName1" runat="server" Text="Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server" Text="Surname :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSurName" runat="server" Text="Surname"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label4" runat="server" Text="Birth Date :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDob" runat="server" Text="Dob"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label5" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDepartment" runat="server" Text="Dept"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label6" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblDesignation" runat="server" Text="Desg."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label7" runat="server" Text="Manager :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblmgr" runat="server" Text="Manager"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label8" runat="server" Text="Joining date :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbldoj" runat="server" Text="Doj"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblmState" runat="server" Text="M State"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label10" runat="server" Text="Gender :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label11" runat="server" Text="Qualification :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblQualification" runat="server" Text="Qualification"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label12" runat="server" Text="Mobile :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label13" runat="server" Text="Phone :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label14" runat="server" Text="Email :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label15" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnEditPersonalDtl" runat="server" OnClick="btnEditPersonalDtl_Click"
                                Text="Edit Details" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlEditInfo" runat="server" Visible="false">
                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <tr>
                        <th colspan="2">
                            <asp:Literal runat="server" ID="Literal1" Text="Edit Profile"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Text="Title :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTitleAdd" runat="server"></asp:Label>
                            <telerik:RadComboBox ID="radCmbTitleAdd" runat="server" Visible='false'>
                            </telerik:RadComboBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radCmbTitleAdd"
                                ValidationGroup="Edit" CssClass="error" ErrorMessage="Select Title"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label16" runat="server" Text="Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditName" runat="server" Text="Name"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label18" runat="server" Text="Surname :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditSurname" runat="server" Text="Surname"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label20" runat="server" Text="Birth Date :"></asp:Label>
                        </td>
                        <td>
                            
                                                        <telerik:RadDatePicker ID="RadDateDobAdd"  runat="server">
                                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                            </Calendar>
                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                            <DateInput DateFormat="dd/MM/yyyy" Width="97%" DisplayDateFormat="dd/MM/yyyy">
                                                            </DateInput>
                                                        </telerik:RadDatePicker>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="RadDateDobAdd"
                                                            ValidationGroup="Edit" CssClass="error" ErrorMessage="Select Date of Birth"> </asp:RequiredFieldValidator>
                                                    
                            
                            
                            
                        
                            
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label22" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbleditdept" runat="server" Text="Dept"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label24" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbleditdesig" runat="server" Text="Desg."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label26" runat="server" Text="Manager :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbleditmgr" runat="server" Text="Manager"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label28" runat="server" Text="Joining date :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbleditdoj" runat="server" Text="Doj"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label30" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radCmbMaritalStatusAdd" runat="server">
                            </telerik:RadComboBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="radCmbMaritalStatusAdd"
                                ValidationGroup="Edit" CssClass="error" ErrorMessage="Select Marital Status"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label32" runat="server" Text="Gender :"></asp:Label>
                        </td>
                        <td>
                        
                        
                         <asp:RadioButtonList ID="rblstPerGender" runat="server" >
                                                            <asp:ListItem  Text="Male" Value="M">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="F">
                                                            </asp:ListItem>
                         </asp:RadioButtonList>
                        
                        
                        
                          
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label34" runat="server" Text="Qualification :"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radCmbQualificationAdd" runat="server">
                            </telerik:RadComboBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="radCmbQualificationAdd"
                                ValidationGroup="Edit" CssClass="error" ErrorMessage="Select Qualification"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label36" runat="server" Text="Mobile :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txteditMobile" runat="server" OnKeyPress="javascript:return ValidNumber('event')"
                                Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label38" runat="server" Text="Phone :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditPhone" runat="server" MaxLength="12" OnKeyPress="javascript:return ValidPhneNumber('event')"
                                Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label40" runat="server" Text="Email :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditEmail" runat="server" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="error"
                                runat="server" ControlToValidate="txtEditEmail" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                ValidationGroup="Edit" ErrorMessage="Enter valid Email.">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label42" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbleditstatus" runat="server" Text="Status"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSaveInfo" runat="server" OnClick="btnSaveInfo_Click" Text="Save"
                                Width="100px" ValidationGroup="Edit" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancelInfo_Click" Text="Cancel"
                                Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnEditPersonalDtl" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveInfo" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upEditCredencial" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PnlCredencial" runat="server">
                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <th colspan="2">
                        <asp:Literal runat="server" ID="Literal4" Text="User Credential Details"></asp:Literal>
                    </th>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label17" runat="server" Text="User Name"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label2" runat="server" Text="Security Question"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSecurityQuestion" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label19" runat="server" Text="Security Answers"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSecurityAnswers" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td>
                            <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" />
                            <%-- OnClick="btnCancelpassword_Click"--%>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PnlEditCredencial" runat="server" Visible="false">
                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <th colspan="2">
                        <asp:Literal runat="server" ID="Literal5" Text="Edit User Credential"></asp:Literal>
                    </th>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label21" runat="server" Text="User Name"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditusername" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label27" runat="server" Text=" Old Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditCurrentPassword"  TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label25" runat="server" Text="New Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditNewPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label23" runat="server"  Text="Confirm Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                            &nbsp
                            <asp:CompareValidator ID="cmpPassowrd" runat="server" CssClass="error" ControlToValidate="txtEditConfirmPassword"
                                ValidationGroup="EditCredential" ControlToCompare="txtEditNewPassword" ErrorMessage="Password do not mach.">  </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label29" runat="server" Text="Security Question"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radCmbEditSecurityQuestion" runat="server" Width="300px">
                            </telerik:RadComboBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCmbEditSecurityQuestion"
                                ValidationGroup="EditCredential" CssClass="error" ErrorMessage="Select Security Question"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label31" runat="server" Text="Security Answer"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditSecurityAnswers" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEditSecurityAnswers"
                                ValidationGroup="EditCredential" CssClass="error" ErrorMessage="Enter Security Answers"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td>
                            <asp:Button ID="btnSavePassword" runat="server" Text="Save" OnClick="btnSavePassword_Click"
                                ValidationGroup="EditCredential" />
                            <asp:Button ID="btnCancelpassword" runat="server" Text="Cancel" OnClick="btnCancelpassword_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSavePassword" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelpassword" />
            <asp:AsyncPostBackTrigger ControlID="btnChangePassword" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upTheme" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlTheme" runat="server">
                <table width="100%" class="TableForm">
                    <th colspan="2">
                        <asp:Literal runat="server" ID="Literal3" Text="Change Theme"></asp:Literal>
                    </th>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblTheame" runat="server" Text="Select Theme"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="radcmbTheme" runat="server" HighlightTemplatedItems="true"
                                EnableLoadOnDemand="true" DataTextField="ThemeName" DataValueField="ThemeName"
                                DropDownWidth="200px"  Width="200px"    OnItemDataBound="radcmbTheme_ItemDataBound">
                                <itemtemplate>
                                                                    <table width="150px">
                                                                        <tr>
                                                                            <td width="100px">
                                                                                <%# DataBinder.Eval(Container.DataItem, "ThemeName")%>
                                                                            </td>
                                                                            <td width="50px">
                                                                        <asp:Image ID="imgTheme" runat="server" ImageAlign="AbsMiddle" />
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                    </table>
                            </itemtemplate>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td>
                            <asp:Button ID="btnLoad" runat="server" Text="Load Theme" OnClick="btnLoad_Click" />
                            <asp:Button ID="btnSetDefault" runat="server" Text="Set as Default" OnClick="btnSetDefault_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnLoad" />
            <asp:AsyncPostBackTrigger ControlID="btnSetDefault" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
