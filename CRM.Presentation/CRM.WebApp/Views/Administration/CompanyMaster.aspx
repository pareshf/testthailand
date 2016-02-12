<%@ Page Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true"
    CodeBehind="CompanyMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.CompanyMaster"
    Title="Company Master" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ActionBar.ascx" TagName="ActionBar"
    TagPrefix="crmUC" %>
<%@ Register Src="~/Views/Shared/Controls/Navigation/ControlBox.ascx" TagName="ControlBox"
    TagPrefix="crmUC" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj, oCompany) {
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
                radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oCompany);
            }
            return false;
        } 
        
         function ramRequestStarted(ajaxManager, eventArgs)
         {
            //alert(eventArgs.EventTarget);
            if (eventArgs.EventTarget == "ctl00$cphPageContent$acbCompany$imgExcelExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCompany$imgWordExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCompany$imgPdfExport" || eventArgs.EventTarget == "ctl00$cphPageContent$acbCompany$imgCsvExport") {
                eventArgs.EnableAjax = false;               
         }
         }    
        
    </script>
    <script language="javascript" type="text/javascript">
        var selectionAlertMessage = '<%=ConfigurationManager.AppSettings["AtleastOneRecord"].ToString() %>';
        var deleteAlertMessage = '<%=ConfigurationManager.AppSettings["DeleteAlert"].ToString() %>';



        function ValidateGridForEdit(gridControlId) {

            var radgridmaster = $find(gridControlId);
            var row = radgridmaster.get_masterTableView().get_selectedItems().length;
            if (row == 0) {
                radalert(selectionAlertMessage, 330, 110, 'Warning Message');
                return false;
            }
            else if (row > 1) {
                radalert('Select only one row', 330, 110, 'Warning Message');
                return false;
            }
        }



        function fnOpen() {
            window.open("../Shared/Upload/UploadPhoto.aspx", "Upload", "width=360px,height=120px, menubar=0,toolbar=0,directories=0,status=0,resizable=0,scrollbars=0, dependent=0, screenX=0; left=100, screenY=0, top=100", "");
        }
        
        
    </script>
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="ramRequestStarted" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCompany$imgExcelExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCompany$imgWordExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCompany$imgPdfExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="upGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ctl00$cphPageContent$acbCompany$imgCsvExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UpvwContact" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
       <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Style="z-index: 100100">
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
    <div class="pageTitle">
        <asp:Literal ID="lblPageCompany" runat="server" Text="Company Master"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="upActionBar" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <crmUC:ActionBar ID="acbCompany" runat="server" OnbtnSaveClick="acbCompany_SaveClick"
                OnimgCsvExport_Click="acbCompany_CsvExportClick" OnimgExcelExport_Click="acbCompany_ExcelExportClick"
                OnimgPdfExport_Click="acbCompany_PdfExportClick" OnimgWordExport_Click="acbCompany_WordExportClick"
                OnbtnCancelClick="acbCompany_CancelClick" OnbtnNewClick="acbCompany_NewClick"
                SaveButtonValidationGroup="ReqCompany" OnbtnSaveNewClick="acbCompany_SaveNewClick"
                SaveNewButtonValidationGroup="ReqCompany" OnbtnDeleteClick="acbCompany_DeleteClick"
                OnbtnSearchClick="acbCompany_SearchClick" OnbtnEditClick="acbCompany_EditClick" OnbtnRefreshClick="acbCompany_RefreshClick"   />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upAddNew" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddNewMode" runat="server" Visible="false">
                            <telerik:RadTabStrip ID="rtsCompanyDetails" runat="server" AutoPostBack="true" OnTabClick="rtsCompanyDetails_TabClick"
                                SelectedIndex="0">
                                <Tabs>
                                    <telerik:RadTab Text="Company Details" Value="CompanyDetails" Selected="True" />
                                    <telerik:RadTab Text="Contact Information" Value="ContactInfo" />
                                </Tabs>
                            </telerik:RadTabStrip>
                            <asp:MultiView ID="mvCompany" runat="server">
                                <asp:View ID="vwCompany" runat="server">
                                    <asp:UpdatePanel ID="upViewCompany" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                                <tr>
                                                    <th colspan="2">
                                                        <asp:Literal runat="server" ID="Literal1" Text="New Company"></asp:Literal>
                                                    </th>
                                                    <th colspan="3">
                                                        <div class="MandatoryNote" align="right">
                                                            <asp:Literal ID="Literal2" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                                        </div>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%">
                                                        <asp:Label ID="lblCompanyType" Text="Type :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td style="width: 25%">
                                                        <telerik:RadComboBox ID="radCmbType" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radCmbType_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radCmbType"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Select Type"> </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMobile" Text="Mobile :" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMobile" TabIndex="8" runat="server" MaxLength="12" OnKeyPress="javascript:return ValidNumber('event')"
                                                            Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                    
                                                    
                                                       <td rowspan="7" valign="top">
                                                        <asp:Panel ID="pnlEmployeeImage" runat="server" Height="152px" HorizontalAlign="Center"
                                                            Width="102px">
                                                            <asp:Image ID="imgCompany" runat="server" BorderColor="Black" BorderWidth="1px"
                                                                Height="100px" ImageAlign="Top" Width="100px" ToolTip="Double click to change photo" />
                                                                <br />
                                                                  <input id="BtnBrowse" type="button" value="Browse Logo"  onclick="fnOpen()" />
                                       <asp:Label ID="lblFilePath" runat="server" Font-Names="Arial" Font-Size="11px"></asp:Label>
                                                                
                                                        </asp:Panel>
                                                    </td>
                                                    
                                                    
                                                    
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%">
                                                        <asp:Label ID="lblCompanyName" Text="Company Name :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompanyName" TabIndex="2" runat="server" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCompanyName"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Enter Company Name"> </asp:RequiredFieldValidator>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="lblPhone" Text="Phone :" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPhone"  runat="server" MaxLength="50" TabIndex="9" OnKeyPress="javascript:return ValidPhneNumber('event')"
                                                            Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="lblAddresssLine1" Text="Address Line1 :"   runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddressline1" TabIndex="3"   runat="server" MaxLength="100" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                  
                                                    
                                                    
                                                     <td>
                                                        <asp:Label ID="lblfax" Text="Fax :" runat="server"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtFax" runat="server" TabIndex="10" MaxLength="14" OnKeyPress="javascript:return ValidPhneNumber('event')"
                                                            Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                    
                                                    
                                                </tr>
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                <tr>
                                                
                                                <td>
                                                        <asp:Label ID="lblAddressLine2" Text="Address Line2 :" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAddressline2" TabIndex="4" runat="server" MaxLength="100" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                <td>
                                                        <asp:Label ID="lblEmailId" Text="Email Id :" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailId" MaxLength="50" TabIndex="11" runat="server" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="error"
                                                            runat="server" ControlToValidate="txtEmailId" ValidationGroup="Company" ValidationExpression=".*@.*\..*"
                                                            ErrorMessage="Enter valid Email.">
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                
                                                </tr>
                                                
                                                <tr>
                                                
                                                
                                                 <td>
                                                        <asp:Label ID="lblCountry" Text="Country :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="radCmbCountry" TabIndex="5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radCmbCountry_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCmbCountry"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Select Country"> </asp:RequiredFieldValidator>
                                                    </td>
                                                
                                                  <td>
                                                        <asp:Label ID="lblPinCode" Text="Pincode :" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPinCode" TabIndex="12" runat="server" MaxLength="6" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
                                                    </td>
                                                   
                                                </tr>
                                                
                                                <tr>
                                                
                                                <td>
                                                        <asp:Label ID="lblState" Text="State :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="radCmbState" TabIndex="6" AutoPostBack="true" runat="server" OnSelectedIndexChanged="radCmbState_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="radCmbState"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Select State"> </asp:RequiredFieldValidator>
                                                    </td>
                                                   <td>
                                                        <asp:Label ID="lblRegion" Text="Region :" runat="server"></asp:Label>
                                                        <%--   <span class="error">*</span>--%>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="radCmbRegion" TabIndex="13" runat="server">
                                                        </telerik:RadComboBox>
                                                        <%-- &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="radCmbRegion"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Select Region"> </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    
                                                   
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCity" Text="City :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="radCmbCity" TabIndex="7" runat="server">
                                                        </telerik:RadComboBox>
                                                        &nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="radCmbCity"
                                                            ValidationGroup="Company" CssClass="error" ErrorMessage="Select City"> </asp:RequiredFieldValidator>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="lblUnderWhome"  Text="Under Whome? :" runat="server"></asp:Label>
                                                        <span class="error">*</span>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="radCmbUnderWhome" TabIndex="14" runat="server" HighlightTemplatedItems="true"
                                                            Width="200px" EnableLoadOnDemand="true" DataTextField="COMPANY_NAME" DataValueField="COMPANY_ID"
                                                            DropDownWidth="400px">
                                                            <HeaderTemplate>
                                                                <table width="300px" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td width="200px">
                                                                            Company Name
                                                                        </td>
                                                                        <td width="100px">
                                                                            Type
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table width="300">
                                                                    <tr>
                                                                        <td width="200">
                                                                            <%# DataBinder.Eval(Container.DataItem, "COMPANY_NAME")%>
                                                                        </td>
                                                                        <td width="100px">
                                                                            <%# DataBinder.Eval(Container.DataItem, "IS_COMPANY_FRANCHISE_NAME")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                  
                                                </tr>
                                               
                                               
                                            </table>
                                            
                                            
                                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4" border="0">
                                                <tr>
                                                    <th colspan="3">
                                                        <asp:Literal ID="litDept" runat="server" Text="Department"></asp:Literal>
                                                    </th>
                                                    <tr>
                                                        <td width="15%">
                                                            <div>
                                                                <asp:Label ID="lblDepartmentList" runat="server" Text="Department Name"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:ListBox ID="lstDepartmentName" runat="server" Height="150px" TabIndex="16" Width="150px">
                                                                </asp:ListBox>
                                                            </div>
                                                        </td>
                                                        <td align="center" width="3%">
                                                            <div>
                                                                <div>
                                                                    <asp:Button ID="btnAssignRightDept" runat="server"  OnClick="btnAssignRightDepartment_Click"
                                                                        Text="&gt;&gt;" ToolTip="Assign Department to Company" />
                                                                </div>
                                                                <div>
                                                                    <asp:Button ID="btnAssignLeftDept" runat="server"  OnClick="btnAssignLeftDepartment_Click"
                                                                        Text="&lt;&lt;" ToolTip="Unassign Department to Company" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td align="left" width="82%">
                                                            <div>
                                                                <asp:Label ID="lblAssignedDept" runat="server" Text="Assigned Department"></asp:Label>
                                                            </div>
                                                            <div>
                                                                <asp:ListBox ID="lstAssignDeptName" runat="server" Height="150px" Width="150px">
                                                                </asp:ListBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                            
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="acbCompany" />
                                            <asp:AsyncPostBackTrigger ControlID="cbxCompany" />
                                            <asp:AsyncPostBackTrigger ControlID="rtsCompanyDetails" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:View>
                                <asp:View ID="vwContact" runat="server">
                                    <asp:UpdatePanel ID="UpvwContact" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlContactAdd" runat="server" Visible="false">
                                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <th>
                                                            <asp:Literal runat="server" ID="Literal3" Text="New Contact"></asp:Literal>
                                                        </th>
                                                        <th>
                                                            <div class="MandatoryNote" align="right">
                                                                <asp:Literal ID="Literal4" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="lblTitle" runat="server" Text="Title :"></asp:Label>
                                                            <span class="error">*</span>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="radCmbTitle" runat="server">
                                                            </telerik:RadComboBox>
                                                            &nbsp;
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="radCmbTitle"
                                                                ValidationGroup="Contact" CssClass="error" ErrorMessage="select valid Title"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text="Name :"></asp:Label>
                                                            <span class="error">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtName" runat="server" Width="155px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtName"
                                                                ValidationGroup="Contact" CssClass="error" ErrorMessage="Enter valid Name"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPersonMobile" runat="server" Text="Mobile :"></asp:Label>
                                                            <span class="error">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPersonMobile" runat="server" MaxLength="12" OnKeyPress="javascript:return ValidNumber('event')"
                                                                Width="155px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPersonMobile"
                                                                ValidationGroup="Contact" CssClass="error" ErrorMessage="Enter Mobile Number"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPersonPhone" runat="server" Text="Phone :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPersonPhone" runat="server" MaxLength="14" OnKeyPress="javascript:return ValidPhneNumber('event')"
                                                                Width="155px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPersonEmail" runat="server" Text="Email :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPersonEmail" runat="server" Width="155px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:RegularExpressionValidator ID="valRegEx" CssClass="error" runat="server" ControlToValidate="txtPersonEmail"
                                                                ValidationGroup="Contact" ValidationExpression=".*@.*\..*" ErrorMessage="Enter valid Email.">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlContactGrid" runat="server" Visible="false">
                                                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <th>
                                                            <asp:Literal runat="server" ID="Literal5" Text="Contact Details"></asp:Literal>
                                                        </th>
                                                        <th>
                                                            <div class="MandatoryNote" align="right">
                                                                <asp:Literal ID="Literal6" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <telerik:RadGrid ID="radgrdContact" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                                                AutoGenerateColumns="false" AllowMultiRowSelection="true" AllowMultiRowEdit="true"
                                                                AllowPaging="true" AllowSorting="true" ClientSettings-EnableRowHoverStyle="true"
                                                                OnNeedDataSource="radgrdContact_NeedDataSource" OnPreRender="radgrdContact_PreRender"
                                                                OnItemDataBound="radgrdContact_ItemDataBound" EnableEmbeddedSkins="false">
                                                                <ClientSettings>
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
                                                                                <asp:CheckBox ID="chkHeadWrTCt" runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkItemWrtCt" runat="server" OnCheckedChanged="chkItemWrtCt_CheckChanged"
                                                                                    onclick="javascript:SelectRow(this)" AutoPostBack="false" />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Company Id" UniqueName="CompanyId" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdCustCompanyIdItem" runat="server" Text='<%# Bind("COMPANY_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="GrdCustCompanyIdEdit" runat="server" Text='<%# Bind("COMPANY_ID") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Id" UniqueName="SR_NO" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdCustIdItem" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lblGrdCustIdEdit" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Title" UniqueName="Title" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContTitle" runat="server" Text='<%# Bind("TITLE_DESC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadComboBox ID="lblGrdContTitleEdit" runat="server">
                                                                                </telerik:RadComboBox>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="lblGrdContTitleEdit"
                                                                                    CssClass="error" ErrorMessage="Select Valid Title"> </asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="TitleID" UniqueName="TITLE_ID" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContTitleId" runat="server" Text='<%# Bind("TITLE_ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lblGrdContTitleIdEdit" Text='<%# Bind("TITLE_ID") %>' runat="server"></asp:Label>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Name" UniqueName="NAME" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="lblGrdContNameEdit" runat="server" Text='<%# Bind("NAME") %>'></asp:TextBox>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="lblGrdContNameEdit"
                                                                                    CssClass="error" ErrorMessage="Enter Name"> </asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="E Mail" UniqueName="EMAIL" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="lblGrdContEmailEdit" runat="server" Text='<%# Bind("EMAIL") %>'></asp:TextBox>
                                                                                <br />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator41" CssClass="error"
                                                                                    runat="server" ControlToValidate="lblGrdContEmailEdit" ValidationExpression=".*@.*\..*"
                                                                                    ErrorMessage="Enter valid Email.">
                                                                                </asp:RegularExpressionValidator>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Mobile" UniqueName="MOBILE" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContMobile" runat="server" Text='<%# Bind("MOBILE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="lblGrdContMobileEdit" runat="server" MaxLength="12" OnKeyPress="javascript:return ValidNumber('event')"
                                                                                    Text='<%# Bind("MOBILE") %>'></asp:TextBox>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator01" runat="server" ControlToValidate="lblGrdContMobileEdit"
                                                                                    CssClass="error" ErrorMessage="Enter mobile number"> </asp:RequiredFieldValidator>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Phone" UniqueName="PHONE" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGrdContPhone" runat="server" Text='<%# Bind("PHONE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="lblGrdContEmailPhoneEdit" MaxLength="14" OnKeyPress="javascript:return ValidPhneNumber('event')"
                                                                                    runat="server" Text='<%# Bind("PHONE") %>'></asp:TextBox>
                                                                                <br />
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                    <EditFormSettings>
                                                                    </EditFormSettings>
                                                                    <PagerStyle AlwaysVisible="True" />
                                                                </MasterTableView>
                                                                <FilterMenu EnableEmbeddedSkins="False" EnableTheming="True">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                            </telerik:RadGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="acbCompany" />
                                            <asp:AsyncPostBackTrigger ControlID="cbxCompany" />
                                            <asp:AsyncPostBackTrigger ControlID="rtsCompanyDetails" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:View>
                            </asp:MultiView>
                            <crmUC:ControlBox ID="cbxCompany" runat="server" OnbtnSaveClick="cbxCompany_SaveClick"
                                VisibleSaveNewButton="false" VisibleCopyButton="false" OnbtnCancelClick="cbxCompany_CancelClick"
                                OnbtnClearClick="cbxCompany_ClearClick" />
                            <%--OnbtnNewClick="cbxCompany_NewClick"
                                 OnbtnEditClick="cbxCompany_EditClick" OnbtnDeleteClick="cbxCompany_DeleteClick"  OnbtnSearchClick="cbxCompany_SearchClick"--%>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbCompany" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGrid" runat="server">
                            <telerik:RadGrid ID="radgrdCompany" runat="server" PageSize="<%$appSettings:GridPageSize %>"
                                Width="100%" AllowMultiRowEdit="true" OnPreRender="radgrdCompany_PreRender" OnItemDataBound="radgrdCompany_ItemDataBound"
                                OnNeedDataSource="radgrdCompany_NeedDataSource">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true"></Selecting>
                                    <%--  <ClientEvents OnRowClick="OnRowClick" />--%>
                                </ClientSettings>
                                <MasterTableView AutoGenerateColumns="False" EditMode="InPlace" AllowSorting="True"
                                    PagerStyle-AlwaysVisible="true">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="25px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemStyle CssClass="ItemAlign" Width="25px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="25px" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeadWrT" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkItemWrt" runat="server" AutoPostBack="false" OnCheckedChanged="chkItemWrt_CheckChanged" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%--   <telerik:GridBoundColumn HeaderText="Company Id" UniqueName="Company Id" DataField="COMPANY_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Id" UniqueName="Company Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompIdItem" runat="server" Text='<%# Bind("COMPANY_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="Type" UniqueName="IS_COMPANY_FRANCHISE_NAME"
                                            ItemStyle-HorizontalAlign="Left" DataField="IS_COMPANY_FRANCHISE_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="TypeId" UniqueName="IS_COMPANY_FRANCHISE" DataField="IS_COMPANY_FRANCHISE"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Name" UniqueName="COMPANY_NAME" DataField="COMPANY_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Address 1" UniqueName="ADDRESS_LINE1" DataField="ADDRESS_LINE1"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Address 2" UniqueName="ADDRESS_LINE2" DataField="ADDRESS_LINE2"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="CIty Id" UniqueName="CITY_ID" DataField="CITY_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="City" UniqueName="CITY_NAME" DataField="CITY_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="STATEId" UniqueName="STATE_ID" DataField="STATE_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="State" UniqueName="STATE_NAME" DataField="STATE_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="CountryId" UniqueName="COUNTRY_ID" DataField="COUNTRY_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Country" UniqueName="COUNTRY_NAME" DataField="COUNTRY_NAME">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Pincode" UniqueName="PINCODE" DataField="PINCODE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Mobile" UniqueName="MOBILE" DataField="MOBILE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Phone" UniqueName="PHONE" DataField="PHONE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Fax" UniqueName="FAX" DataField="FAX">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Email" UniqueName="EMAIL_ID" DataField="EMAIL_ID">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="RegionId" UniqueName="REGION_ID" DataField="REGION_ID"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Region" UniqueName="REGION_LONG_NAME" DataField="REGION_LONG_NAME"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Parent Company" UniqueName="PARENT_COMPANY"
                                            DataField="PARENT_COMPANY" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="Parent Company Id" UniqueName="PARENT_COMPANY_ID"
                                            DataField="PARENT_COMPANY_ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <div>
                            <asp:HiddenField ID="hdnCheckIndex" runat="server" />
                            <asp:HiddenField ID="hdnCheckIndexCt" runat="server" />
                            <asp:HiddenField ID="hdnEditableMode" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="acbCompany" />
                        <asp:AsyncPostBackTrigger ControlID="cbxCompany" />
                        <asp:AsyncPostBackTrigger ControlID="rtsCompanyDetails" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
   