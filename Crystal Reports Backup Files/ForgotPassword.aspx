<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="CRM.WebApp.ForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Views/Shared/Controls/Templates/ProductFooter.ascx" TagName="ProductFooter"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<script language="javascript" type="text/javascript">
</script>

<head runat="server">
    <title>
        <%= System.Configuration.ConfigurationSettings.AppSettings["PageTitle"].ToString() %>
    </title>
    <link href="Views/Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
 

    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="80" align="center" valign="bottom">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:ScriptManager ID="scriptManagerId" runat="server">
                </asp:ScriptManager>
                <div align="center">
                    <asp:UpdatePanel ID="upPanel" runat="server">
                        <ContentTemplate>
                            <table width="505" style="height: 312px" border="0" align="center" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td width="505" style="height: 312px; background: url(Views/Shared/Images/bkg.gif) no-repeat"
                                        valign="top">
                                        <table width="505" style="height: 312px;" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top">
                                                    <table cellpadding="0" cellspacing="0" width="100%" style="height: 312px;">
                                                        <tr>
                                                            <td style="height: 150px" align="center">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Views/Shared/Images/logo3.jpg" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 45px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" align="center">
                                                                <asp:Label ID="lblUsername0" runat="server" Text="Global Customer Care" Font-Bold="True"
                                                                    ForeColor="#25407B"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblUsername1" runat="server" Text="Email: support@travelzunlimited.com"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblUsername2" runat="server" Text="Phone: (079) 39830925" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="50%" align="left" valign="top">
                                                    <table width="100%" border="0" cellpadding="2" cellspacing="3">
                                                        <tr>
                                                            <td style="height: 35px" align="left" valign="middle">
                                                                &nbsp;<asp:Label ID="lblLoginHeader" runat="server" Text="Retrive Password" SkinID="SknLoginHeader"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <caption>
                                                            
                                                            <tr>
                                                                <td align="left" style="height: 35px" valign="middle">
                                                                    <asp:Wizard ID="ForgotPasswordWizard" runat="server" ActiveStepIndex="0" 
                                                                        BackColor="Transparent" BorderStyle="None" DisplaySideBar="false" 
                                                                        OnActiveStepChanged="ForgotPasswordWizard_ActiveStepChanged" 
                                                                        OnFinishButtonClick="ForgotPasswordWizard_FinishButtonClick" 
                                                                        OnNextButtonClick="ForgotPasswordWizard_NextButtonClick" Width="100%">
                                                                        <StartNavigationTemplate>
                                                                            <div align="center">
                                                                                <asp:Button ID="StartNextButton" runat="server" BorderStyle="Solid" 
                                                                                    OnClick="StartNextButton_Click" Text="Submit" />
                                                                                <%--  CommandName="MoveNext"--%>
                                                                            </div>
                                                                            <br />
                                                                        </StartNavigationTemplate>
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <WizardSteps>
                                                                            <asp:WizardStep ID="wizardStep1" runat="server" Title="">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        <br />
                                                            <br />
                                                            <br />
                                                                                            <asp:Label ID="lblUserName" runat="server" Font-Bold="false" Text="Username (Email):"></asp:Label>
                                                                                            <br />
                                                                                            <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" ToolTip="Username" 
                                                                                                Width="200px"></asp:TextBox>
                                                                                            <br />
                                                                                            <asp:Label ID="lblUserNameRequire" runat="server" Font-Bold="false" 
                                                                                                Font-Names="Arial" Font-Size="11px" ForeColor="Red"></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left" style="display:none">
                                                                                            <asp:Label ID="lblEmailAddress" runat="server" Font-Bold="false" 
                                                                                                Text="Email Address :"></asp:Label>
                                                                                            <br />
                                                                                            <asp:TextBox ID="txtEmailId" runat="server" MaxLength="50" ToolTip="Email Id" 
                                                                                                Width="200px"></asp:TextBox>
                                                                                            <br />
                                                                                            <asp:Label ID="lblEmailIdRequire" runat="server" Font-Bold="false" 
                                                                                                Font-Names="Arial" Font-Size="11px" ForeColor="Red"></asp:Label>
                                                                                            <asp:Label ID="lblEmailIdNotValid" runat="server" Font-Bold="false" 
                                                                                                Font-Names="Arial" Font-Size="11px" ForeColor="Red"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:WizardStep>
                                                                            <asp:WizardStep ID="wizardStep2" runat="server" AllowReturn="false" 
                                                                                EnableTheming="False" StepType="Step" Title="Step 2 of 3">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lblSeqQusEmail" runat="server" Font-Bold="False" 
                                                                                                Text="Email Address :"></asp:Label>
                                                                                            <br />
                                                                                            <asp:Label ID="lblSeqQusEmailIdFatch" runat="server" Font-Bold="True"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lblSeqQus" runat="server" Font-Bold="false" 
                                                                                                Text="Security Question :"></asp:Label>
                                                                                            <br />
                                                                                            <asp:Label ID="lblSeqQusFetch" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lbltxtSeqQusAns" runat="server" Font-Bold="false" 
                                                                                                Text="Answer :"></asp:Label>
                                                                                            <br />
                                                                                            <asp:TextBox ID="txtSeqQusAns" runat="server" EnableViewState="False" 
                                                                                                MaxLength="40" ToolTip="Security Answer"></asp:TextBox>
                                                                                            <div>
                                                                                                <asp:Label ID="lblSeqAnsRequire" runat="server" Font-Names="Arial" 
                                                                                                    Font-Size="11px" ForeColor="Red"></asp:Label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                        </td>
                                                                                        <td align="left">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:WizardStep>
                                                                            <asp:WizardStep ID="wizardStep3" runat="server" Title="Step 3 of 3">
                                                                                <table cellpadding="3" cellspacing="3">
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lblGetUsername" runat="server" Font-Bold="false" 
                                                                                                Text="<%$appSettings:EmailMassege %>"></asp:Label>
                                                                                        </td>
                                                                                        <td align="justify">
                                                                                            <asp:Label ID="lblGetUsernameFetch" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:WizardStep>
                                                                        </WizardSteps>
                                                                        <StepNavigationTemplate>
                                                                            <div align="center">
                                                                                <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" 
                                                                                    Text="Next" />
                                                                            </div>
                                                                            <br />
                                                                        </StepNavigationTemplate>
                                                                        <FinishNavigationTemplate>
                                                                            <div align="center">
                                                                                <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" 
                                                                                    Text="Finish" />
                                                                            </div>
                                                                            <br />
                                                                        </FinishNavigationTemplate>
                                                                        <HeaderStyle BackColor="Transparent" Font-Bold="true" Font-Names="Arial" 
                                                                            Font-Size="12px" ForeColor="#666666" Height="25px" />
                                                                        <StepStyle BackColor="Transparent" />
                                                                    </asp:Wizard>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="height: 35px" valign="top">
                                                                    <div>
                                                                        <asp:LinkButton ID="lnlbtnForgetPassword" runat="server" 
                                                                            PostBackUrl="~/Login.aspx" Text="Back To Login"></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 35px" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </caption>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
    <div>
        <uc1:ProductFooter ID="ProductFooter1" runat="server" />
    </div>
    </form>
</body>
</html>
