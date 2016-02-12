<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="AddAnnouncement.aspx.cs" Inherits="CRM.WebApp.Views.Workplace.AddAnnouncement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function newrowadded(sender, args) {
        var arr = [];
        arr[0] = document.getElementById('<%=txtTitle.ClientID%>').value;
        arr[1] = document.getElementById('<%=txtBody.ClientID%>').value;
        arr[2] = document.getElementById('<%=txtUrl.ClientID%>').value;
        arr[3] = document.getElementById('<%=txtExpDate.ClientID%>').value;
        arr[4] = 0;

        if (arr[2] == "" || arr[2] == 'null') arr[2] = 0;
        if (arr[3] == "" || arr[3] == 'null') arr[3] = 0;
        try {
            alert('1');
            CRM.WebApp.webservice.AnnouncementWebService.InsertUpdateAnnouncement(arr);  
            alert('Record Save Successfully');
        }
        catch (e) {
            alert(e);
            alert('Wrong Data Inserted');
        }
    }
    var currentTextBox = null; var currentDatePicker = null; 
    function showPopup(sender, e) {
        try { currentTextBox = sender; var datePicker = $find("<%= txtExpDate %>"); currentDatePicker = datePicker; datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value)); var position = datePicker.getElementPosition(sender); datePicker.showPopup(position.x, position.y + sender.offsetHeight); }
        catch (e) { }
    }
    function parseDate(sender, e) { currentDatePicker.hidePopup(); }
    function dateSelected(sender, args) {
        try { if (currentTextBox != null) { currentTextBox.value = args.get_newDate().format('MM/dd/yyyy'); currentDatePicker.hidePopup(); } }
        catch (e) { }
    }
    function PopUpShowing(sender, args) { var divmore = document.getElementById('divmore'); divmore.style.display = 'block'; divmore.style.position = 'Absolute'; divmore.style.left = screen.width / 2 - 150; divmore.style.top = screen.height / 2 - 150; var IMG = document.getElementById("imgexistingimage"); IMG.src = args.srcElement.all[1].value; }
    function disablepopup() { var divmore = document.getElementById('divmore'); divmore.style.display = 'none'; return false; }
</script>

<telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
</telerik:raddatepicker>

    <div class="pageTitle">
        <asp:Literal ID="lblAnnouncement" runat="server" Text="New Announcement"></asp:Literal>
    </div>
    <form id="form1">
    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
        <tr>
            <th colspan="2">
                <asp:Literal runat="server" ID="Literal1" Text="New Announcement"></asp:Literal>
            </th>
            <th colspan="3">
                <div class="MandatoryNote" align="right">
                    <asp:Literal ID="Literal2" runat="server">Fields marked with <span class="error">*</span> are mandatory.</asp:Literal>
                </div>
            </th>
        </tr>
        <tr>
            <td style="width : 15%">
                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                <span class="error">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="<%$appSettings:TextBoxWidth%>"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="requireTitle" runat="server" ValidationGroup="Announce" ControlToValidate="txtTitle" ErrorMessage="Enter Title" CssClass="error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width : 15%">
                <asp:Label ID="lblBody" runat="server" Text="Description"></asp:Label>
                <span class="error">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtBody" runat="server" Width="<%$appSettings:TextBoxWidth %>" TextMode="MultiLine"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="requireBody" runat="server" ValidationGroup="Announce" ControlToValidate="txtBody" ErrorMessage="Enter Description" CssClass="error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width : 15%">
                <asp:Label ID="lblUrl" runat="server" Text="Info-URL"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUrl" runat="server" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width : 15%">
                <asp:Label ID="lblExpDate" runat="server" Text="Expiration Date"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExpDate" runat="server" Width="<%$appSettings:TextBoxWidth %>"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Announce" OnClientClick="newrowadded(this,event);"/>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
