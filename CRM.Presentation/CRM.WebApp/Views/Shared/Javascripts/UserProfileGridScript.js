var EMP_ID = "0";
var UserProfileTableView = null;
var UserProfileCommandName = "";
var UserDetailTableView = null;
var userDetailCommandName = "";
var s = "";

function loadProfileInfo() {
    CRM.WebApp.webservice.UserProfileWebService.GetProfileInfo(Employee, output);
}
function output(result) {

           document.getElementById('ctl00_cphPageContent_txttitle').value = result[0][0].TITLE_DESC;
           document.getElementById('ctl00_cphPageContent_txtname').value = result[0][0].EMP_NAME;
           document.getElementById('ctl00_cphPageContent_txtsurname').value = result[0][0].EMP_SURNAME;
           document.getElementById('ctl00_cphPageContent_txtbirthdate').value = result[0][0].EMP_DOB;
           //document.getElementById('ctl00_cphPageContent_txtdepartment').value = result[0][0].DEPARTMENT_NAME;
           //document.getElementById('ctl00_cphPageContent_txtdesignation').value = result[0][0].FOREIGN_CURRENCY_AGENT_NAME;
           //document.getElementById('ctl00_cphPageContent_txtmanager').value = result[0][0].BOOKING_CODE;
           document.getElementById('ctl00_cphPageContent_txtjoiningdate').value = result[0][0].EMP_DOJ;
           document.getElementById('ctl00_cphPageContent_txtMaritalstatus').value = result[0][0].MARITAL_STATUS_NAME;
           document.getElementById('ctl00_cphPageContent_txtgender').value = result[0][0].EMP_GENDER;
           document.getElementById('ctl00_cphPageContent_txtqualification').value = result[0][0].QUALIFICATION_NAME;
           document.getElementById('ctl00_cphPageContent_txtmobile').value = result[0][0].EMP_MOBILE;
           document.getElementById('ctl00_cphPageContent_txtphone').value = result[0][0].EMP_PHONE;
           document.getElementById('ctl00_cphPageContent_txtstatus').value = result[0][0].STATUS_NAME;
           document.getElementById('ctl00_cphPageContent_txtsignature').value = result[0][0].BOOKING_STATUS;
           document.getElementById('ctl00_cphPageContent_txtid').value = result[0][0].EMP_ID;
           document.getElementById('ctl00_cphPageContent_txtsignature').value = result[0][0].SIGNATURE_PASSWORD;
           document.getElementById('ctl00_cphPageContent_txtEditEmail').value = result[0][0].EMP_EMAIL;
       }
       function loadUserDetail() {

           CRM.WebApp.webservice.UserProfileWebService.GetUserInfo(Employee, output1);
       }
       function output1(result) {

           document.getElementById('ctl00_cphPageContent_txtusername').value = result[0][0].USER_NAME;
           //document.getElementById('ctl00_cphPageContent_txtEditCurrentPassword').value = result[0][0].PASSWORD;
          // document.getElementById('ctl00_cphPageContent_txtEditNewPassword').value = result[0][0].EMP_SURNAME;
           //document.getElementById('ctl00_cphPageContent_txtEditConfirmPassword').value = result[0][0].EMP_DOB;
           document.getElementById('ctl00_cphPageContent_txtsequrityquestion').value = result[0][0].SECURITY_QUESTION_DESC;
           document.getElementById('ctl00_cphPageContent_txtEditSecurityAnswers').value = result[0][0].SECURITY_ANSWERS;
           document.getElementById('ctl00_cphPageContent_txtid').value = result[0][0].EMP_ID;
}