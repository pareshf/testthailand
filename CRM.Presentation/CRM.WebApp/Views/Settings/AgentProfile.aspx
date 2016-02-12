<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AgentProfile.aspx.cs" Inherits="CRM.WebApp.Views.Settings.AgentProfile" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        window.blockConfirm = function (text, mozEvent, oWidth, oHeight, callerObj, oEmployee) {
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
                var callBackFn = function (arg) {
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

        var currentTextBox = null;
        var currentDatePicker = null;

        function showPopup(sender, e) {

            try {

                currentTextBox = sender;
                var datePicker = $find("<%= RadDatePicker1.ClientID %>");
                currentDatePicker = datePicker;
                datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));
                var position = datePicker.getElementPosition(sender);
                datePicker.showPopup(position.x, position.y + sender.offsetHeight);

            }
            catch (e) { }

        }

        function dateSelected(sender, args) {
            try {
                if (currentTextBox != null) {
                    currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                    currentDatePicker.hidePopup();
                }
            }
            catch (e) { }
        }

        function parseDate(sender, e) {

            currentDatePicker.hidePopup();

        

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
   
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
   <%-- <telerik:radwindowmanager id="RadWindowManager1" runat="server" style="z-index: 100100">
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
    </telerik:radwindowmanager>--%>
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="01/01/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
    </telerik:raddatepicker>
    <div class="pageTitle">
        <asp:Literal ID="lblPageProfile" runat="server" Text="My Profile"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpProfile" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlEditInfo" runat="server">
                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                       <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Text="Title :"></asp:Label>
                        </td>
                        <td>
                            <telerik:radcombobox id="radCmbAgent" runat="server" >
                            </telerik:radcombobox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label16" runat="server" Text="Name :"></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtClientname" runat="server" Text="First Name" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label18" runat="server" Text="Surname :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientlastname" runat="server" Text="Last Name" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label20" runat="server" Text="Birth Date :"></asp:Label>
                        </td>
                        <td>
                         <asp:TextBox ID="txtbirthdate" runat="server" Width="<%$appSettings:TextBoxWidth %>" onclick="showPopup(this, event);" onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label24" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td>
                            <telerik:radcombobox id="radCmbDesignation" runat="server" >
                            </telerik:radcombobox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label30" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td>
                            <telerik:radcombobox id="radCmbMaritalStatusAdd" runat="server">
                            </telerik:radcombobox>
                           
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
                            <asp:Label ID="Label40" runat="server" Text="Email(User Name) :"></asp:Label>
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
                             <telerik:radcombobox id="Cmbdrpuserstatus" runat="server" >
                                </telerik:radcombobox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSaveInfo" runat="server" OnClick="save_Click" Text="Save"
                                Width="100px" ValidationGroup="Edit" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false"
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
    </asp:UpdatePanel>

    <br />
    <br />
    <div class="pageTitle">
        <asp:Literal ID="Literal1" runat="server" Text="Change Password"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
            
             <tr>
             
             <td>
                  <asp:Label ID="Label2" runat="server" Text="Old Password"></asp:Label>
             </td>
                <td>
                  <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" OnTextChanged="txtOldPassword_TextChanged" Width="200px" AutoPostBack="true"></asp:TextBox>
                  <asp:RequiredFieldValidator
                      ID="RequiredFieldValidator1" runat="server" ErrorMessage="Old Password is required" ValidationGroup="change" ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator>

             </td>

             </tr>

              <tr>
             
             <td>
                  <asp:Label ID="Label3" runat="server" Text="New Password"></asp:Label>
             </td>
                <td>
                  <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                     <asp:RequiredFieldValidator
                      ID="RequiredFieldValidator2" runat="server" ErrorMessage="New Password is required" ValidationGroup="change" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
             </td>

             </tr>

             <tr>
             
             <td>
                  <asp:Label ID="Label4" runat="server" Text="Confirm New Password"></asp:Label>
             </td>
                <td>
                  <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                  <asp:RequiredFieldValidator
                      ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm New Password is required" ValidationGroup="change" ControlToValidate="txtConfirmNewPassword"></asp:RequiredFieldValidator>
                      <asp:CompareValidator
                          ID="CompareValidator1" runat="server" ErrorMessage="Password is not same as New password" ControlToValidate="txtConfirmNewPassword" ControlToCompare="txtNewPassword"></asp:CompareValidator>
             </td>

             </tr>

             <tr>
             <td>
                   <asp:Button ID="Button1" runat="server" OnClick="Change_Click" Text="Change Password"
                                Width="150px" ValidationGroup="change" />
             </td>
             </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
