<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AdminBookingFit.aspx.cs" Inherits="CRM.WebApp.Views.FIT.AdminBookingFit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <script language="javascript" type="text/javascript">

        var sessionTimeout = "<%= Session.Timeout %>";

        var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
    </script>
    <div>
        <asp:Label runat="server" Text="FIT BOOKING" ID="headlbl" Width="200px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />
        <asp:UpdatePanel ID="UpdatePanel_Hotel_header" runat="server" UpdateMode="Conditional"
            class="pageTitle">
            <ContentTemplate>
                <asp:Label ID="lblPackage" runat="server" Text="Select Package" CssClass="headlabel"></asp:Label>&nbsp;<span
                    class="error">*</span>
                <br />
                <br />
                <asp:DataList ID="datalist_packages" runat="server">
                    <ItemTemplate>
                        <table width="400px" id="Package" runat="server" border="1" style="border-collapse: collapse;
                            border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                            <tr>
                                <td style="width: 350px;">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FIT_PACKAGE_NAME") %>' CssClass="lblstyle"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButton ID="rbtnpackage" runat="server" GroupName="Pack" OnCheckedChanged="CheckChanged"
                                        AutoPostBack="true" />
                                </td>
                                <td style="display: none">
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FIT_PACKAGE_ID") %>' CssClass="lblstyle"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <%-----------------------------------------------------------------------CLIENT DETAILS----------------------------------------------------------------------------------------------------%>
        <asp:UpdatePanel ID="UpdatePanel_TourDetails" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:Label ID="headinglabel" runat="server" Text="Client Details" CssClass="headlabel"
                    class="pageTitle"></asp:Label>
                <br />
                <table width="100%">
                    <tr>
                        <td width="35%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="On Behalf Of Agent" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="160px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="Sub Agents" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpSubagent" runat="server" Width="160px" OnSelectedIndexChanged="drpSubagent_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="Label9" runat="server" Text="Client Name" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td width="70%">
                                        <asp:DropDownList ID="drpTitle" runat="server" Width="50px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtClientname" runat="server" Width="45px" ControlToValidate="txtClientname"
                                            OnTextChanged="txtClientname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:TextBox ID="txtClientlastname" runat="server" Width="45px" OnTextChanged="txtClientlastname_TextChanged"></asp:TextBox>&nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Client Name Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtClientname"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="No of Adult" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_Adult" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="No of Adult Required "
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtNo_Adult"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="No of CWB" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_CWB" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="No of CNB" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_CNB" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="No of Infant" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_Infant" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="No. Of Rooms Single" CssClass="fieldlabel"
                                            Width="160px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomSingle" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="No. Of Rooms Double / Twin" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomDouble" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="No. Of Rooms Triple" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoroomTriple" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trpac" style="display: none" runat="server">
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Favourite Package" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkpakage" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="No of Nights" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_OfNights" runat="server" CssClass="textboxstyle" Width="160px"
                                            OnTextChanged="txtNo_OfNights_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="No of Nights Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtNo_OfNights"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text="Order Status" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="160px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Tour Name" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTourname" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="Label12" runat="server" Text="From Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox ID="txtFrom_Date" runat="server" CssClass="textboxstyle" Width="160px"
                                            Text="dd/MM/yyyy" OnTextChanged="txtFrom_Date_TextChanged" AutoPostBack="true"
                                            onblur="message(this);"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="From Date Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtFrom_Date"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <ajax:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtFrom_Date"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="To Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTo_Date" runat="server" CssClass="textboxstyle" Width="160px"
                                            ReadOnly="true"> 
                                        </asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="To Date Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtTo_Date"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Arrival Flight No." CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArrival_Flight" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Time" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="atime" runat="server" visible="false">*</span>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Width="90px">
                                            <TimeView TimeFormat="h:mm tt">
                                            </TimeView>
                                            <DateInput DisplayDateFormat="hh:mm tt">
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <asp:Label ID="Label23" runat="server" Text="Departure Flight No." CssClass="fieldlabel"
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="txtDeparture_Flight" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                    <td width="5%">
                                        <asp:Label ID="Label18" runat="server" Text="Time" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="dtime" runat="server" visible="false">*</span>
                                    </td>
                                    <td width="5%">
                                        <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Width="90px">
                                            <TimeView TimeFormat="h:mm tt">
                                            </TimeView>
                                            <DateInput DisplayDateFormat="hh:mm tt">
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                    </td>
                                </tr>
                                <tr id="landing" runat="server" style="display: none">
                                    <td width="30%">
                                        <asp:Label ID="Label32" runat="server" Text="Arrival Flight Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="spanLanding" runat="server">*</span>
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox ID="txtlanddate" runat="server" CssClass="textboxstyle" Width="160px"
                                            Text="dd/MM/yyyy"></asp:TextBox><br />
                                       <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender1112" runat="server" targetcontrolid="txtlanddate"
                                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                        </ajax:textboxwatermarkextender>
                                        <ajax:calendarextender id="CalendarExtender3" runat="server" targetcontrolid="txtlanddate"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr id="fly" runat="server" style="display: none">
                                    <td width="30%">
                                        <asp:Label ID="Label31" runat="server" Text="Departure Flight Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="spanFly" runat="server">*</span>
                                    </td>
                                    <td width="70%">
                                        <asp:TextBox ID="txtflydate" runat="server" CssClass="textboxstyle" Width="160px"
                                            Text="dd/MM/yyyy"></asp:TextBox><br />
                                      <ajax:textboxwatermarkextender id="TextBoxWatermarkExtender1111" runat="server" targetcontrolid="txtflydate"
                                            watermarktext="dd/MM/yyyy" watermarkcssclass="watermarked">
                                        </ajax:textboxwatermarkextender>
                                        <ajax:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtflydate"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="FIT Package Type" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList3" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="FIT package Type is Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="DropDownList3"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="CANCELLAETION" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Agent Cancellation Fees" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfees" runat="server" CssClass="textboxstyle" Width="160px"></asp:TextBox>
                                        <asp:Label ID="Label22" runat="server" Text="USD" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="discount" runat="server" style="display: none">
                                    <td>
                                        <asp:Label ID="lbldiscount" runat="server" Text="Discount" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkdiscount" runat="server" OnCheckedChanged="Checked_Changed"
                                            AutoPostBack="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%-------Dicount Popup-------%>
                <asp:Panel ID="Paneldicount" runat="server" CssClass="modalPopup" Width="350px" Style="display: none;">
                    <asp:Panel ID="Panel1" runat="server" Width="350px">
                        <fieldset style="background-color: White">
                            <asp:Panel ID="pnldiscount" runat="server" CssClass="panelhead">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbldisc" runat="server" Text="Please Enter Discount Amount" ForeColor="#FEFEFE"
                                                Font-Size="15px"></asp:Label>
                                        </td>
                                        <td style="width: 17px;" align="center" valign="middle">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                                Style="cursor: pointer;" ToolTip="Close" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label26" runat="server" Text="Quoted Cost" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:Label ID="Label27" runat="server" Text="" CssClass="lblstyle"></asp:Label>&nbsp;<asp:Label
                                            ID="Label28" runat="server" Text="USD" CssClass="lblstyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label25" runat="server" Text="Discount" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="txtdiscount" runat="server" Width="100px" ValidationGroup="popup"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="#"
                                            ControlToValidate="txtdiscount" ValidationExpression="^\d+(\.\d\d)?$" ErrorMessage="Please Enter Only Numbers"
                                            ValidationGroup="popup" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label29" runat="server" Text="" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSavediscount" runat="server" Text="Save" ValidationGroup="popup"
                                            OnClick="btnSave_discount" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updateHotel" runat="server">
            <ContentTemplate>
                &nbsp;&nbsp;
                <asp:Button Text="Next" runat="server" ID="btnNext" Width="80px" OnClick="btnNext_Click"
                    ValidationGroup="Required" />
                &nbsp;&nbsp;
                <asp:Button Text="Back" runat="server" ID="btnback" Width="80px" OnClick="btnBack_Click"
                    Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
</asp:Content>
