<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAgentRegistrationForm.aspx.cs"
    Inherits="CRM.WebApp.NewAgentRegistrationForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
    <style>
        .sectionHeader
        {
            font-family: Arial;
            font-weight: bold;
            margin-left: 0px;
        }
        .headlabel
        {
            font-size: "40px";
            font-weight: bold;
            font-family: Verdana;
        }
        
        .fieldlabel
        {
            font-family: Verdana;
            font-size: 12px;
            font-style: normal;
            font-weight: normal;
        }
        .textboxstyle
        {
            width: 50px;
        }
        .buttonstyle
        {
            width: 150px;
        }
        
        .lblstyle
        {
            font-family: Verdana;
            font-size: 20px;
            font-style: normal;
            font-weight: normal;
        }
        .errorclass
        {
            font-family: Verdana;
            font-size: 12px;
            color: Red;
        }
        .error
        {
            color: Red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div>
        <table border="0" width="100%" align="center" cellpadding="0" cellspacing="0" id="tblMainContentHolder">
            <tr>
                <td width="15%" align="left">
                    <asp:Image ID="imgLogo" runat="server" Height="50px" Width="148px" ImageAlign="Top"
                        ImageUrl="~/Views/Shared/Images/logo4.jpg" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label runat="server" Text="Agent Registration Form" ID="headlbl" Width="250px"
            Font-Bold="true" Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" class="pageTitle">
            <ContentTemplate>
                <table>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Agent id" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_UNQ_ID" runat="server" ReadOnly="true" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Agent Company Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_COMPANY_NAME" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqCompName" runat="server" ControlToValidate="CUST_COMPANY_NAME"
                                ErrorMessage="Company name is required" ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Fully Offline Agent" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkoffline" runat="server" AutoPostBack="True" OnCheckedChanged="chkoffline_CheckedChanged" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Width="85px"></asp:Label>
                            <asp:Label ID="lbl1" Text="First Name" runat="server" Width="69px"></asp:Label>
                            <asp:Label ID="Label17" Text="Last Name" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Agent Name" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTitle" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:TextBox ID="txtClientname" runat="server" Width="65px" Text="First Name"></asp:TextBox>
                            <asp:TextBox ID="txtClientlastname" runat="server" Width="65px" Text="Last Name"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="reqAgentName" runat="server" ControlToValidate="txtClientname"
                                ErrorMessage="Agent name is required" ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Mobile" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_REL_MOBILE" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqMobile" runat="server" ControlToValidate="CUST_REL_MOBILE"
                                ErrorMessage="Mobile No is required." ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Email" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error" id="spanemail" runat="server">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_REL_EMAIL" runat="server" Width="250px"></asp:TextBox>
                            <asp:Label ID="lblerror" runat="server" Text="Email already exist. Enter another E-mail."
                                ForeColor="Red" Visible="false" CssClass="lblstyle"></asp:Label>
                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="CUST_REL_EMAIL"
                                ErrorMessage="Email is required." ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Password" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error" id="spanpass" runat="server">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="PASSWORD" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="PASSWORD"
                                ErrorMessage="Password is required." ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Agent Type" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpAgentType" runat="server" AutoPostBack="true" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Communication Mode" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpmode" runat="server" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqCommuMode" runat="server" ControlToValidate="drpmode"
                                ErrorMessage="Communication mode is required." ValidationGroup="valid" InitialValue="0"
                                CssClass="lblstyle" Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Telephone" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_REL_PHONE" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPhone" runat="server" ControlToValidate="CUST_REL_PHONE"
                                ErrorMessage="Phone No is required." ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Designation" CssClass="lblstyle"></asp:Label>
                            &nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpDesignation" runat="server" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqDesignation" runat="server" ControlToValidate="drpDesignation"
                                ErrorMessage="Designation is required." ValidationGroup="valid" InitialValue="0"
                                CssClass="lblstyle" Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Payment Terms" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpTerms" runat="server" AutoPostBack="true" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Branch Name" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="CHAIN_NAME" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="tr1" visible="false">
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Addresss Type" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpaddtype" runat="server" AutoPostBack="true" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqAddType" runat="server" ControlToValidate="drpaddtype"
                                ErrorMessage="Address Type is required." ValidationGroup="valid" InitialValue="0"
                                CssClass="lblstyle" Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="Address Line 1" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_ADDRESS_LINE1" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqAddLine1" runat="server" ControlToValidate="CUST_ADDRESS_LINE1"
                                ErrorMessage="Address is required." ValidationGroup="valid" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Address Line 2" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_ADDRESS_LINE2" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="City" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpcity" runat="server" AutoPostBack="true" Width="250px" OnSelectedIndexChanged="drpcity_selectedindexchanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqCity" runat="server" ControlToValidate="drpcity"
                                ErrorMessage="City is required." ValidationGroup="valid" InitialValue="0" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="State" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpstate" runat="server" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqState" runat="server" ControlToValidate="drpstate"
                                ErrorMessage="State is required." ValidationGroup="valid" InitialValue="0" CssClass="lblstyle"
                                Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="Country" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                class="error">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpcountry" runat="server" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqCountry" runat="server" ControlToValidate="drpcountry"
                                ErrorMessage="Country is required." ValidationGroup="valid" InitialValue="0"
                                CssClass="lblstyle" Font-Size="12px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpuserstatus" runat="server" AutoPostBack="true" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="Pincode" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="CUST_PINCODE" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CUST_PHONE" runat="server" CssClass="radinput"></asp:TextBox>
                                    </td>
                                </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="Label24" runat="server" Text="Website" CssClass="lblstyle"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="WEBSITE" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Security Question" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpsecurity" runat="server" AutoPostBack="true" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Security Answer" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>--%>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblWordVerification" runat="server" Text="Word Verification"> </asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="ImgCaptcha" runat="server" ImageUrl="~/Captcha/captcha.ashx" /><br />
                            <%-- <cwc:Captcha ID="Captcha1" runat="Server" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server" Text="Enter Verification"></asp:Label>
                            &nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtword" runat="server" Width="190px" Class="textbox" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Register" runat="server" OnClick="register_onclick" Width="100px"
                                Text="Register" ValidationGroup="valid"></asp:Button>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Updateconfirm">
        <ProgressTemplate>
            <div class="TransparentGrayBackground">
            </div>
            <div class="Sample6PageUpdateProgress">
                <asp:Image ID="ajaxLoadNotificationImage" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                    AlternateText="" />
                &nbsp;Please Wait...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>
