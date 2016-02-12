<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="CRM.WebApp.Views.Settings.UserProfile" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
     <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
     <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
<style>
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        div.RadGrid_Default .rgFilterRow td
        {
            background-color: #e5e5e5;
        }
        div.RadGrid_Default .rgHeader
        {
            background-color: #F3F3F3;
            background-position: 0 0;
            background-repeat: repeat-x !important;
            border-color: #E6E6E6 #E6E6E6 #CCCCCC;
            color: #636363;
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            height: 25px;
            line-height: 16px;
            text-align: left;
            text-decoration: none;
            text-indent: 0;
        }
        
        .RadMenu_Default
        {
            background-color: #fff;
            border: solid 0px #fff;
        }
        .RadMenu_Default UL.rmRootGroup
        {
            background-color: #fff;
            border: solid 0px #fff;
            padding: 2px;
        }
        .RadMenu rmLink
        {
            padding-left: 0px;
        }
        .RadMenu_Default .rmLink
        {
            color: #000;
            text-decoration: none;
            font-family: Verdana;
            font-size: 8pt;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText
        {
            border: solid 0px #fff;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem:hover
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink
        {
            color: #000;
            padding-top: 2px;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadPanelBar_Default .rpSlide
        {
            padding-left: 2px;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .radinput
        {
            width: 100%;
            border: 0px solid #c2c2c2;
        }
    .style2
    {
        width: 145px;
    }
    .style3
    {
        width: 144px;
    }
    </style>

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>


    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type ="text/javascript" src="../Shared/Javascripts/UserProfileGridScript.js"></script>
    <script type="text/javascript">
        var pword = '<%=Session["password"]%>';
        var flag = '<%=Session["FLAG"]%>';
        var username = '<%=Session["usersname"]%>';
        function pageLoad() {

            var Employee = '<%=Session["empid"]%>';
           
          
            document.getElementById('<%=txtid.ClientID%>').value = Employee;
            CRM.WebApp.webservice.UserProfileWebService.GetProfileInfo(Employee, output);
            CRM.WebApp.webservice.UserProfileWebService.GetUserInfo(Employee, output1);

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
            }

            function PopUpShowing(sender, args) {

                 var divmore = document.getElementById('divmore');
                 divmore.style.display = 'block';
                 divmore.style.position = 'Absolute';
                 divmore.style.left = screen.width / 2 - 150;
                 divmore.style.top = screen.height / 2 - 150;
                 var IMG = document.getElementById("imgexistingimage");
                 IMG.src = args.srcElement.all[1].value;
            }

            function disablepopup() {

                var divmore = document.getElementById('divmore');
                divmore.style.display = 'none';
                return false;
            }
            function SaveProfile() {
               
                var a = [];
                
                a[0] = document.getElementById('<%=txttitle.ClientID%>').value;
                a[1] = document.getElementById('<%=txtname.ClientID%>').value;
                a[2] = document.getElementById('<%=txtsurname.ClientID%>').value;
                a[3] = document.getElementById('<%=txtbirthdate.ClientID%>').value;
                //a[4] = document.getElementById('<%=txtdepartment.ClientID%>').value;
               // a[5] = document.getElementById('<%=txtdesignation.ClientID%>').value;
                //a[6] = document.getElementById('<%=txtmanager.ClientID%>').value;
                a[4] = document.getElementById('<%=txtjoiningdate.ClientID%>').value;
                a[5] = document.getElementById('<%=txtMaritalstatus.ClientID%>').value;
                a[6] = document.getElementById('<%=txtgender.ClientID%>').value;
                a[7] = document.getElementById('<%=txtqualification.ClientID%>').value;
                a[8] = document.getElementById('<%=txtmobile.ClientID%>').value;
                a[9] = document.getElementById('<%=txtphone.ClientID%>').value;
                a[10] = document.getElementById('<%=txtstatus.ClientID%>').value;
                a[11] = document.getElementById('<%=txtsignature.ClientID%>').value;
                a[12] = document.getElementById('<%=txtEditEmail.ClientID%>').value;
                a[13] = document.getElementById('<%=txtid.ClientID%>').value;

                if (a[0] == "" || a[0] == 'null') a[0] = 0;
                if (a[1] == "" || a[1] == 'null') a[1] = 0;
                if (a[2] == "" || a[2] == 'null') a[2] = 0;
                if (a[3] == "" || a[3] == 'null') a[3] = 0;
                if (a[4] == "" || a[4] == 'null') a[4] = 0;
                if (a[5] == "" || a[5] == 'null') a[5] = 0;
                if (a[6] == "" || a[6] == 'null') a[6] = 0;
                if (a[7] == "" || a[7] == 'null') a[7] = 0;
                if (a[8] == "" || a[8] == 'null') a[8] = 0;
                if (a[9] == "" || a[9] == 'null') a[9] = 0;
                if (a[10] == "" || a[10] == 'null') a[10] = 0;
                if (a[11] == "" || a[11] == 'null') a[11] = 0;
                if (a[12] == "" || a[12] == 'null') a[12] = 0;
                if (a[13] == "" || a[13] == 'null') a[13] = 0;


                try {
                   
                    CRM.WebApp.webservice.UserProfileWebService.InsertUpdateProfileInfo(a);
                    alert('Record Save Successfully');
                    
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function saveUserInfo() {
                
                var a = [];
                
                a[0] = document.getElementById('<%=txtusername.ClientID%>').value;
                if (document.getElementById('<%=txtEditConfirmPassword.ClientID%>').value != "") {
                    a[1] = document.getElementById('<%=txtEditConfirmPassword.ClientID%>').value;
                }
                else {
                    a[1] = pword;
                }
                a[2] = document.getElementById('<%=txtsequrityquestion.ClientID%>').value;
                a[3] = document.getElementById('<%=txtEditSecurityAnswers.ClientID%>').value;
                a[4] = document.getElementById('<%=txtid.ClientID%>').value;
                a[5] = flag;
               
                if (a[0] == "" || a[0] == 'null') a[0] = 0;
                //if (a[1] == "" || a[1] == 'null') a[1] = 0;
                if (a[2] == "" || a[2] == 'null') a[2] = 0;
                if (a[3] == "" || a[3] == 'null') a[3] = 0;
                if (a[4] == "" || a[4] == 'null') a[4] = 0;
                if (a[5] == "" || a[5] == 'null') a[5] = 0;
              

                try {

                    CRM.WebApp.webservice.UserProfileWebService.UpdateUserDetail(a);
                    alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
               
            }
            
            function saveUserInfo1() {
                
                var a = [];
                
                a[0] = document.getElementById('<%=txtusername.ClientID%>').value;
                if (document.getElementById('<%=txtEditConfirmPassword.ClientID%>').value != "") {
                    a[1] = document.getElementById('<%=txtEditConfirmPassword.ClientID%>').value;
                }
                else {
                    a[1] = pword;
                }
                a[2] = document.getElementById('<%=txtsequrityquestion.ClientID%>').value;
                a[3] = document.getElementById('<%=txtEditSecurityAnswers.ClientID%>').value;
                a[4] = document.getElementById('<%=txtid.ClientID%>').value;
                a[5] = flag;

                if (a[0] == "" || a[0] == 'null') a[0] = 0;
                //if (a[1] == "" || a[1] == 'null') a[1] = 0;
                if (a[2] == "" || a[2] == 'null') a[2] = 0;
                if (a[3] == "" || a[3] == 'null') a[3] = 0;
                if (a[4] == "" || a[4] == 'null') a[4] = 0;
                if (a[5] == "" || a[5] == 'null') a[5] = 0;


                try {
                   
                    CRM.WebApp.webservice.UserProfileWebService.UpdateUserDetailValidation(a, s, OnCallBack);
                    //alert('Record Save Successfully');

                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
            function OnCallBack(results, userContext, sender) {

                
                if (results == "N") {

                    alert('This Email Already Exist.');
                }
                else if (results == "Y") {

                    alert('Record Save Successfully');
                }
                else if (results == "") {
                    alert('Record Save Successfully');
                }

            }
            function CheckUser(sender) {

                var user = document.getElementById('<%=txtusername.ClientID%>').value;
                
                if (user == username) {
                   
                    $('#ctl00_cphPageContent_txtEditCurrentPassword').show();

                    saveUserInfo();
                    
                }
                else if (user != username) {
                    $('#ctl00_cphPageContent_txtEditCurrentPassword').hide();
                    saveUserInfo1();
                    
                }
                else
                { }

            }
            function CheckMyEmail(sender)
            {
                var user = document.getElementById('<%=txtusername.ClientID%>').value;
                
                if (user == username) {
                   
                    $('#ctl00_cphPageContent_txtEditCurrentPassword').show();

                }
                else if (user != username) {

                    $('#ctl00_cphPageContent_txtEditCurrentPassword').hide();
                   
                }
                else
                { }
            }
            function CheckOldPassword(sender) {
               
                var word = document.getElementById('<%=txtEditCurrentPassword.ClientID%>').value;

                if (word == pword) {

                   
                    $('#ctl00_cphPageContent_txtEditNewPassword').show();
                    $('#ctl00_cphPageContent_txtEditConfirmPassword').show();
                }
                else {

                    alert('Enter Correct Password');
                }
            }
            function ConfirmedPword(sender) {
               
                var p1 = document.getElementById('<%=txtEditNewPassword.ClientID%>').value;
                var p2 = document.getElementById('<%=txtEditConfirmPassword.ClientID%>').value;

                if (p1 == p2) {

                  
                   // alert('Password Match');
                }
                else {

                    alert('New Password & Confirmed Password Not Match.');
                }
            }
    </script>
 </telerik:radcodeblock>

    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function () {

            //var emptitle = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var maritalstatus = "../../webservice/autocomplete.ashx?key=FETCH_MATERIAL_FOR_EMPLOYEE_MASTER";
            var qualification = "../../webservice/autocomplete.ashx?key=FETCH_QUALIFICATION_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var empstatus = "../../webservice/autocomplete.ashx?key=FETCH_STATUS_FOR_EMPLOYEE_MASTER_AUTOSEARCH";
            var gender = "../../webservice/autocomplete.ashx?key=FETCH_GENDER_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var question = "../../webservice/autocomplete.ashx?key=FETCH_SEQURITY_QUESTION_NAME_FOR_AUTOSEARCH";
            //var companyname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            //var departmentname = "../../webservice/autocomplete.ashx?key=FETCH_DEPARTMENT_FOR_EMPLOYEE_MASTER";
            //var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            //var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            //var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";

            $("#ctl00_cphPageContent_txttourname").autocomplete(maritalstatus);
            $("#ctl00_cphPageContent_txtqualification").autocomplete(qualification);
            $("#ctl00_cphPageContent_txtstatus").autocomplete(empstatus);
            $("#ctl00_cphPageContent_txtgender").autocomplete(gender);
            $("#ctl00_cphPageContent_txtsequrityquestion").autocomplete(question);

        });
    </script>
    
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
     <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    
    <div class="pageTitle">
        <asp:Literal ID="lblPageProfile" runat="server" Text="My Profile"></asp:Literal>
   </div>
    <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <tr>
                        <th colspan="2">
                            <asp:Literal runat="server" ID="Literal1" Text="Edit Profile"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblid" runat="server" Text="Employee Id :" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtid" runat="server" Width="150px" Visible="true" Text="0" ReadOnly="true" style="background-color: #F5F5F5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label1" runat="server" Text="Title :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txttitle" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label16" runat="server" Text="Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtname" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label18" runat="server" Text="Surname :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsurname" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label20" runat="server" Text="Birth Date :"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtbirthdate" runat="server" Width="150px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label22" runat="server" Text="Department :" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdepartment" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label24" runat="server" Text="Designation :" Visible="false"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtdesignation" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label26" runat="server" Text="Manager :" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtmanager" runat="server" Width="150px" ReadOnly="true" style="background-color: #F5F5F5" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label28" runat="server" Text="Joining date :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtjoiningdate" runat="server" Width="150px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label30" runat="server" Text="Marital Status"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtMaritalstatus" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label32" runat="server" Text="Gender :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtgender" runat="server" Width="150px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label34" runat="server" Text="Qualification :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtqualification" runat="server" Width="150px"></asp:TextBox> 
                               
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label36" runat="server" Text="Mobile :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtmobile" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label38" runat="server" Text="Phone :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtphone" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label40" runat="server" Text="Email :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditEmail" runat="server" Width="150px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label42" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtstatus" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblsignature" runat="server" Text="Signature Password :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsignature" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style2">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSaveInfo" runat="server" Text="Save" OnClientClick="SaveProfile();" Width="100px" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
       </table>
       <%--Designing of Employee Details Start--%>

       <%--Designing of User Details Start--%>


                <table width="100%" class="TableForm" cellspacing="0" cellpadding="4">
                    <th colspan="2">
                        <asp:Literal runat="server" ID="Literal5" Text="Edit User Credential"></asp:Literal>
                    </th>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label21" runat="server" Text="User Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtusername" runat="server" Width="330px" onblur="CheckMyEmail(this);" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label27" runat="server" Text=" Old Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditCurrentPassword"  TextMode="Password" runat="server" onblur="CheckOldPassword(this);"
                                Width="331px"  Style="display: none;" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label25" runat="server" Text="New Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditNewPassword" TextMode="Password" runat="server" 
                                Width="331px" Style="display: none;" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label23" runat="server"  Text="Confirm Password"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditConfirmPassword" TextMode="Password" runat="server" 
                                Width="331px" Style="display: none;" onblur="ConfirmedPword(this);" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label29" runat="server" Text="Security Question"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsequrityquestion" runat="server" Width="330px" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="Label31" runat="server" Text="Security Answer"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEditSecurityAnswers" runat="server" Width="330px" TabIndex="6"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                        </td>
                        <td>
                            <asp:Button ID="btnSavePassword" runat="server" Text="Save" TabIndex="7" OnClientClick="CheckUser();"/>&nbsp;&nbsp;
                            <asp:Button ID="btnCancelpassword" runat="server" Text="Cancel" TabIndex="8"/>
                        </td>
                    </tr>
                </table> 
</asp:Content>
