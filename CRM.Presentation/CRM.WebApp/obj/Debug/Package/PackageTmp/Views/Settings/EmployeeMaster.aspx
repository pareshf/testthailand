<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="EmployeeMaster.aspx.cs" Inherits="CRM.WebApp.Views.Settings.EmployeeMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <%--<script src="../Shared/Javascripts/progress.js" type="text/javascript"></script>--%>
    <%--style over ridding for filter background color change--%>
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
    </style>

     <script language="javascript" type="text/javascript">

         var sessionTimeout = "<%= Session.Timeout %>";

         var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
         setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/Script2.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                customersTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                customersCommandName = "Load";
                ordersTableView = $find("<%= radgriddetails.ClientID %>").get_masterTableView();

                contectTableView = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
                roleTableView = $find("<%= radgridrole.ClientID %>").get_masterTableView();

                if (customersTableView.PageSize = 10) {
                    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
                }
                else if (customersTableView.PageSize > 10) {
                    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
                }
                else if (customersTableView.PageSize > 20) {
                    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
                }
            }
          
        </script>
        <script type="text/javascript">
            document.forms[0].onsubmit = function () {
                var hasDeletedItems = $find("<%= radgridmaster.ClientID %>")._deletedItems.length > 0;
                if (hasDeletedItems) {
                    if (!confirm("Are You Sure To Delete?")) {
                        $find("<%= radgridmaster.ClientID %>")._deletedItems = [];
                        $find("<%= radgridmaster.ClientID %>").updateClientState();
                    }
                }
            } 
        </script>
        <script type="text/javascript">
            var currentTextBox = null;
            var currentDatePicker = null;
            //This method is called to handle the onclick and onfocus client side events for the texbox
            function showPopup(sender, e) {
                try {
                    //this is a reference to the texbox which raised the event
                    currentTextBox = sender;

                    //this gets a reference to the datepicker, which will be shown, to facilitate
                    //the selection of a date
                    var datePicker = $find("<%= RadDatePicker1.ClientID %>");

                    //this variable is used to store a reference to the date picker, which is currently
                    //active
                    currentDatePicker = datePicker;

                    //this method first parses the date, that the user entered or selected, and then
                    //sets it as a selected date to the picker
                    datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));

                    //the code lines below show the calendar, which is used to select a date. The showPopup
                    //function takes three arguments - the x and y coordinates where to show the calendar, as 
                    //well as its height, derived from the offsetHeight property of the textbox 
                    var position = datePicker.getElementPosition(sender);
                    datePicker.showPopup(position.x, position.y + sender.offsetHeight);
                }
                catch (e) { }
            }

            //this handler is used to set the text of the TextBox to the value of selected from the popup 
            function dateSelected(sender, args) {
                try {
                    if (currentTextBox != null) {
                        //currentTextBox is the currently selected TextBox. Its value is set to the newly selected
                        //value of the picker
                        currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                        currentDatePicker.hidePopup();
                    }
                }
                catch (e) { }
            }

            //this function is used to parse the date entered or selected by the user
            function parseDate(sender, e) {
                currentDatePicker.hidePopup();
                // if (currentDatePicker != null) {
                //     var date = currentDatePicker.get_dateInput().parseDate(sender.value);
                //    var dateInput = currentDatePicker.get_dateInput();

                //    if (date == null) {
                //        date = currentDatePicker.get_selectedDate();
                //  currentDatePicker.hidePopup();
                //    }
                //  var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());
                // sender.value = formattedDate;

                //    }
            }

            function radgridContectmaster_RowDataBound(sender, args) {
                var radTextBox1 = args.get_item().findControl("ADDRESS_TYPE_NAME"); // find control
                var combo = args.get_item().findControl("COMBOADDRESS_TYPE_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox1) {
                        var itm = combo.findItemByText(radTextBox1._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }


                var radTextBox1 = args.get_item().findControl("COUNTRY_NAME"); // find control
                var combo = args.get_item().findControl("COMBOCOUNTRY_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox1) {
                        var itm = combo.findItemByText(radTextBox1._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }


                var radTextBox1 = args.get_item().findControl("STATE_NAME"); // find control
                var combo = args.get_item().findControl("COMBOSTATE_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox1) {
                        var itm = combo.findItemByText(radTextBox1._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }


                var radTextBox1 = args.get_item().findControl("CITY_NAME"); // find control
                var combo = args.get_item().findControl("COMBOCITY_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox1) {
                        var itm = combo.findItemByText(radTextBox1._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }
            }

            function radgridmaster_RowDataBound(sender, args) {
                var radTextBox1 = args.get_item().findControl("TITLE_DESC"); // find control
                var combo = args.get_item().findControl("COMBOTITLE_DESC");
                //get selected value for dropdown
                try {
                    if (radTextBox1) {
                        var itm = combo.findItemByText(radTextBox1._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }

                var radTextBox2 = args.get_item().findControl("EMP_GENDER"); // find control
                var combo = args.get_item().findControl("COMBOEMP_GENDER");
                //get selected value for dropdown
                try {
                    if (radTextBox2) {
                        var itm = combo.findItemByValue(radTextBox2._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }

                var radTextBox3 = args.get_item().findControl("MARITAL_DATA"); // find control
                var combo = args.get_item().findControl("COMBOMARITAL_DATA");
                //get selected value for dropdown
                try {
                    if (radTextBox3) {
                        var itm = combo.findItemByText(radTextBox3._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }

                var radTextBox4 = args.get_item().findControl("QUALIFICATION_NAME"); // find control
                var combo = args.get_item().findControl("COMBOQUALIFICATION_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox4) {
                        var itm = combo.findItemByText(radTextBox4._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }

                var radTextBox5 = args.get_item().findControl("STATUS_NAME"); // find control
                var combo = args.get_item().findControl("COMBOSTATUS_NAME");
                //get selected value for dropdown
                try {
                    if (radTextBox5) {
                        var itm = combo.findItemByText(radTextBox5._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                } catch (e) { }

                try {

                    var radTextBox6 = args.get_item().findControl("REPORTING_TO"); // find control
                    var combo = args.get_item().findControl("COMBOREPORTING_TO");
                    if (radTextBox6) {
                        //get selected value for dropdown
                        var itm = combo.findItemByText(radTextBox6._value);
                        itm.select();
                    }
                    else if (combo) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text("-Select-");
                        combo.trackChanges();
                        combo.get_items().add(comboItem);
                        comboItem.select();
                    }
                }
                catch (e) { }
                //setting blank row for footer control data 

                //args.get_item().findControl("NEWEMP_NAME").value = '';
            }
            function deleteCurrent() {
               
                CRM.WebApp.webservice.EmployeeWebService.DeleteEmployeeByEmployeeID(EMP_ID);
                CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
            }

            var counterfornewrowleft = 1;
            var counterfornewrowright = 0;
            function AddorUpdateCurrent() {
            }
            function newrowContectadded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
               
                ary[0] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("COUNTRY_NAME").value;
                ary[1] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE1").value;
                ary[2] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_LINE2").value;
                ary[3] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("STATE_NAME").value;
                ary[4] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[5] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("ADDRESS_TYPE_NAME").value;
                ary[6] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("PINCODE").value;
                ary[7] = contectTableView.get_dataItems()[currentRowIndex - 1].findElement("PHONE").value;
                ary[8] = EMP_ID;
                ary[9] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_CONTACT_SRNO;
                if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
                try {
                    //ary[9] = EMP_ID;
                    //ary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_CONTACT_SRNO;
                    //if (ary[10] == null || ary[10] == 'undefined')
                    //    ary[10] = 0;
                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
                    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateContectGrid);
                    //var masterTable = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
                    //masterTable.rebind();
                    alert('Record Save Successfully');
                }
                catch (e) {
                    masterTable.rebind();
                    alert("Wrong Data Inserted");

                }

          }
          function newrowadded(sender, args) {

               currentRowIndex = sender.parentNode.parentNode.rowIndex;
               var ary = [];
               
               ary[1] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("TITLE_DESC").value;
               ary[2] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_SURNAME").value;
               ary[3] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_NAME").value;
               ary[4] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_DOB").value;
               ary[5] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_GENDER").value;
               ary[6] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_EMAIL").value;
               ary[7] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_MOBILE").value;
               ary[8] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMP_PHONE").value;
               ary[9] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("MARITAL_DATA").value;
               ary[10] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("QUALIFICATION_NAME").value;
               ary[11] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("STATUS_NAME").value;
               ary[12] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("SIGNATURE_PASSWORD").value;
               ary[13] = customersTableView.get_dataItems()[currentRowIndex - 1].findElement("EMPLOYEE_SALARY").value;
            
               ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_ID;

               if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
               try {
                   CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary,m,OnCallBack1);
                   CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
                  // alert('Record Save Successfully');
               }
               catch (e) {
                   alert('Wrong Data Inserted');
               }

           }
           function OnCallBack1(results, userContext, sender) {


               if (results == "N") {

                   alert('This Emplyee Name Already Exist.');
               }
               else if (results == "Y") {

                   alert('Record Save Successfully');
               }
               else if (results == "") {
                   alert('Record Save Successfully');
               }

           }
            function useradded(sender, args) {
                // currentDatePicker.hidePopup();
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                
                ary[0] = 0;
                ary[1] = ordersTableView.get_dataItems()[currentRowIndex - 1].findElement("USER_NAME").value;
                ary[2] = ordersTableView.get_dataItems()[currentRowIndex - 1].findElement("FROM_DATE").value;
                ary[3] = ordersTableView.get_dataItems()[currentRowIndex - 1].findElement("TO_DATE").value;
                ary[4] = ordersTableView.get_dataItems()[currentRowIndex - 1].findElement("PASSWORD").value;
                ary[5] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[0]._dataItem.EMP_ID;

                //}
               
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;

                try {

                    CRM.WebApp.webservice.EmployeeWebService.UpdateUserDetails(ary,s,OnCallBack);
                    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateOrdersGrid);
                    //var masterTable = $find("<%= radgriddetails.ClientID %>").get_masterTableView();
                    //masterTable.rebind();
                    //alert('Record Save Successfully');
                } catch (e) {

                    alert('Wrong Data Inserted');
                }
                // do code to clear all footer control data from here then you can add more data from here
                // code to add new row in radgrid
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
            function roleadded(sender, args) {


                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
//                for (var i = 0; i < 4; i++) {
//                    if (i < 10)
//                        i = '0' + i;
//                   
//                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_COMPANY_NAME') {
//                        ary[1] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;
//                    }
//                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_DEPARTMENT_NAME') {
//                        ary[2] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

//                    }
//                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_ROLE_NAME') {
//                        ary[3] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

//                    }
//                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_REPORTING_TO') {
//                        ary[4] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

//                    }


                //                }
                ary[1] = roleTableView.get_dataItems()[currentRowIndex - 1].findElement("COMPANY_NAME").value;
                ary[2] = roleTableView.get_dataItems()[currentRowIndex - 1].findElement("DEPARTMENT_NAME").value;
                ary[3] = roleTableView.get_dataItems()[currentRowIndex - 1].findElement("ROLE_NAME").value;
                ary[4] = roleTableView.get_dataItems()[currentRowIndex - 1].findElement("REPORTING_TO").value;
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[0]._dataItem.USER_ID;

                try {
                    CRM.WebApp.webservice.EmployeeWebService.InsertUserRoleForEmployee(ary);
                    CRM.WebApp.webservice.EmployeeWebService.GetMyRole(EMP_ID, updateMyRoleGrid);
                    //var masterTable = $find("<%= radgridrole.ClientID %>").get_masterTableView();
                    //masterTable.rebind();

                    alert('Record Save Successfully');
                } catch (e) {
                    alert('Wrong Data Inserted');
                }


            }

        </script>
        <script type="text/javascript">
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
            function saveall() {

            }
            function deleteRole() {
                
                CRM.WebApp.webservice.EmployeeWebService.DeleteEmployeeRole(USER_ID, COMP_NAME, DEPT_NAME, ROLE_NAME);
                CRM.WebApp.webservice.EmployeeWebService.GetMyRole(EMP_ID, updateMyRoleGrid);
            }
            
  </script>
    </telerik:radcodeblock>
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var emptitle = "../../webservice/autocomplete.ashx?key=FETCH_TITLE_FOR_EMPLOYEE_MASTER";
            var maritalstatus = "../../webservice/autocomplete.ashx?key=FETCH_MATERIAL_FOR_EMPLOYEE_MASTER";
            var qualification = "../../webservice/autocomplete.ashx?key=FETCH_QUALIFICATION_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var empstatus = "../../webservice/autocomplete.ashx?key=FETCH_STATUS_FOR_EMPLOYEE_MASTER_AUTOSEARCH";
            var reporting = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";
            var country = "../../webservice/autocomplete.ashx?key=FETCH_COUNTRY_FOR_EMPLOYEE_MASTER";
            var state = "../../webservice/autocomplete.ashx?key=FETCH_STATE_FOR_EMPLOYEE_MASTER";
            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            var addresstype = "../../webservice/autocomplete.ashx?key=FETCH_ADDRESS_TYPE_FOR_EMPLOYEE_MASTER";
            var gender = "../../webservice/autocomplete.ashx?key=FETCH_GENDER_FOR_EMPLOYEEMASTER_AUTOSEARCH";
            var role = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_ROLE_NAME";
            var companyname = "../../webservice/autocomplete.ashx?key=FETCH_DATA_FOR_COMPANY_NAME_AUTOSEARCH";
            var departmentname = "../../webservice/autocomplete.ashx?key=FETCH_DEPARTMENT_FOR_EMPLOYEE_MASTER";
            var reportingto = "../../webservice/autocomplete.ashx?key=FETCH_DATA_OF_EMPLOYEE_FOR_TASKMASTER_AUTOSEARCH";

            var a = $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl05_TITLE_DESC").val;
            for (var i = 1; i < 15; i++) { //single entry per grid
                if (i < 10)
                    i = '0' + i;

                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_TITLE_DESC").autocomplete(emptitle);
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_MARITAL_DATA").autocomplete(maritalstatus);
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_QUALIFICATION_NAME").autocomplete(qualification);
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_EMP_GENDER").autocomplete(gender);
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_STATUS_NAME").autocomplete(empstatus);
                $("#ctl00_cphPageContent_radgridmaster_ctl00_ctl" + i + "_REPORTING_TO").autocomplete(reporting);
                $("#ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl" + i + "_COUNTRY_NAME").autocomplete(country);
                $("#ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl" + i + "_STATE_NAME").autocomplete(state);
                $("#ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
                $("#ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl" + i + "_ADDRESS_TYPE_NAME").autocomplete(addresstype);
                $("#ctl00_cphPageContent_radgridrole_ctl00_ctl" + i + "_ROLE_NAME").autocomplete(role);
                $("#ctl00_cphPageContent_radgridrole_ctl00_ctl" + i + "_COMPANY_NAME").autocomplete(companyname);
                $("#ctl00_cphPageContent_radgridrole_ctl00_ctl" + i + "_DEPARTMENT_NAME").autocomplete(departmentname);
                $("#ctl00_cphPageContent_radgridrole_ctl00_ctl" + i + "_REPORTING_TO").autocomplete(reportingto);

            }
        });
    </script>
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageEmployee" runat="server" Text="Employee Master"></asp:Literal>
    </div>
    <div>
        <br />
        <br />
        <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
            maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
        <div id="divradmastergrid" style="overflow: auto; height: 110%;">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this employee?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnloadtocrm" OnClientClick="AddorUpdateCurrent();" CssClass="button"
                            Style="float: right; margin-right: 10px; display: none; color: black; font-weight: bold;"
                            Text="Add Row" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnsaveall" OnClientClick="saveall();" CssClass="button" Style="float: right;
                            margin-right: 10px; color: black; font-weight: bold;" Text="Save All" runat="server" />
                    </td>
                </tr>
            </table>
            <telerik:radgrid id="radgridmaster" runat="server" allowpaging="true" allowmultirowselection="false"
                allowsorting="True" showfooter="false" pagesize="50" enableembeddedskins="false"
                allowautomaticdeletes="True" allowautomaticinserts="True" pagerstyle="AlwaysVisible">
            <ItemStyle Wrap="false" />
            <MasterTableView ClientDataKeyNames="EMP_ID" AllowMultiColumnSorting="true" EditMode="InPlace" TableLayout="Fixed">
            <RowIndicatorColumn>
            <HeaderStyle Width="25px"></HeaderStyle>
             </RowIndicatorColumn>
              <Columns>
                        <%--template column for raw data editing result--%>
                    <telerik:GridTemplateColumn SortExpression="EMP_ID" DataField="EMP_ID" HeaderText="EMP_ID" Visible="false">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="EMP_ID" runat="server" Width="100%" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn SortExpression="TITLE_DESC" DataField="TITLE_DESC" HeaderText="Title">
                        <ItemTemplate>
                            <asp:TextBox id="TITLE_DESC" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox id="NEWTITLE_DESC" runat="server"></asp:TextBox>
                         
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_NAME" DataField="EMP_NAME" HeaderText="Name" Groupable="True" GroupByExpression="EMP_NAME Group By EMP_NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <asp:TextBox ID="NEWEMP_NAME" runat="server" Width="100%"></asp:TextBox> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_SURNAME" DataField="EMP_SURNAME" HeaderText="Surname">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_SURNAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                          <telerik:RadTextBox ID="NEWEMP_SURNAME" runat="server" Width="100%" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_DOB" DataField="EMP_DOB" HeaderText="Birth Date" ItemStyle-Wrap="false">
                        <ItemTemplate>
                         <asp:TextBox ID="EMP_DOB" Width="100%" 
                            onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput"></asp:TextBox></ItemTemplate>
                             <FooterTemplate>
                        <asp:TextBox ID="NEWEMP_DOB" Width="100%" 
                            onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server"></asp:TextBox> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn SortExpression="EMP_MARITAL_STATUS" DataField="EMP_MARITAL_STATUS" HeaderText="EMP_MARITAL_STATUS" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_MARITAL_STATUS" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                         <telerik:RadTextBox ID="NEWEMP_MARITAL_STATUS" runat="server" Width="100%" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn SortExpression="EMP_GENDER" DataField="EMP_GENDER" HeaderText="Gender">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_GENDER" runat="server" CssClass="radinput"></asp:TextBox>
                            
                        </ItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="NEWEMP_GENDER" runat="server" CssClass="radinput"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_EMAIL" DataField="EMP_EMAIL" HeaderText="Email">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_EMAIL" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEWEMP_EMAIL" runat="server" Width="100%" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_MOBILE" DataField="EMP_MOBILE" HeaderText="Mobile">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_MOBILE" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEWEMP_MOBILE" runat="server" Width="100%" /> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="EMP_PHONE" DataField="EMP_PHONE" HeaderText="Phone">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_PHONE" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEWEMP_PHONE" runat="server" Width="100%" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="MARITAL_DATA" DataField="MARITAL_DATA" HeaderText="Marital Status">
                        <ItemTemplate>
                            <asp:TextBox id="MARITAL_DATA" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:TextBox id="NEWMARITAL_DATA" runat="server" CssClass="radinput"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="EMP_QUALIFICATION_ID" DataField="EMP_QUALIFICATION_ID" HeaderText="EMP_QUALIFICATION_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_QUALIFICATION_ID" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="QUALIFICATION_NAME" DataField="QUALIFICATION_NAME" HeaderText="Qualification">
                        <ItemTemplate>
                            <asp:TextBox id="QUALIFICATION_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                         <asp:TextBox id="NEWQUALIFICATION_NAME" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATUS_NAME" DataField="STATUS_NAME" HeaderText="Status">
                        <ItemTemplate>
                                <asp:TextBox id="STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                            
                        </ItemTemplate>
                         <FooterTemplate>
                                 <asp:TextBox id="NEWSTATUS_NAME" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="EMP_STATUS" DataField="EMP_STATUS" HeaderText="EMP_STATUS" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_STATUS" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                         <telerik:RadTextBox ID="NEWEMP_STATUS" runat="server" Width="100%" /> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression="EMPLOYEE_SALARY" DataField="EMPLOYEE_SALARY" HeaderText="Salary">
                        <ItemTemplate>
                            <asp:TextBox ID="EMPLOYEE_SALARY" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression="SIGNATURE_PASSWORD" DataField="SIGNATURE_PASSWORD" HeaderText="Signatue Password">
                        <ItemTemplate>
                            <asp:TextBox ID="SIGNATURE_PASSWORD" runat="server" Width="100%" CssClass="radinput" TextMode="password"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                     <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="More" href="#" onclick="PopUpShowing(this,event)" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowadded(this,event);">
                            &raquo;<telerik:RadTextBox ID="PHOTO" runat="server" CssClass="disable"/>
                            </a>
                          <div id="divmore" style="width:300px;display:none;background-color:#fff;border:1px solid #c2c2c2;"><br />
                        <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Upload Image :"></asp:Literal>
    </div> <br /><br /><img id="imgexistingimage" src="" alt="No Image Available" /><br /><br />
                       <span>Set Image :</span> <asp:FileUpload ID="flupld" runat="server"></asp:FileUpload><br /><br />
                       <asp:Button ID="btnok" runat="server" Text="Done !" />&nbsp;&nbsp; <asp:Button ID="btncalcel" runat="server" Text="Cancel" OnClientClick="return disablepopup()" />
                       <br /><br /> </div>
                        </ItemTemplate>
                         <FooterTemplate>&nbsp;&nbsp;
                         <a id="NewMore" href="#" onclick="PopUpShowing(this,event)" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowadded(this,event);">
                            &raquo;<telerik:RadTextBox ID="PHOTO" runat="server" CssClass="disable"/>
                            </a>
                          <div id="Newdivmore" style="width:300px;display:none;background-color:#fff;border:1px solid #c2c2c2;"><br />
                        <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal3" runat="server" Text="Upload Image :"></asp:Literal>
    </div> <br /><br /><img id="img1" src="" alt="No Image Available" /><br /><br />
                       <span>Set Image :</span> <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload><br /><br />
                       <asp:Button ID="Button1" runat="server" Text="Done !" />&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="Cancel" OnClientClick="return disablepopup()" />
                       <br /><br /> </div>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridmaster_Command" OnRowSelected="radgridmaster_RowSelected" OnRowDataBound="radgridmaster_RowDataBound" OnRowDblClick="addEmployee"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings>
             
        </telerik:radgrid>
        </div>
        <br />
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal1" runat="server" Text="User Detail"></asp:Literal>
        </div>
        <br />
        <br />
        <div id="divradgriddetails" style="overflow: auto; height: 100%;">
            <telerik:radgrid id="radgriddetails" runat="server" allowpaging="False" allowsorting="False"
                allowfilteringbycolumn="False" gridlines="None" showfooter="false">
            <ItemStyle Wrap="false" />
            <MasterTableView AllowMultiColumnSorting="true">
                <Columns>
                 <telerik:GridTemplateColumn SortExpression="EMP_ID" DataField="EMP_ID" HeaderText="EMP_ID" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_ID" runat="server" Width="80px" ></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="USER_NAME" DataField="USER_NAME" HeaderText="User Name">
                        <ItemTemplate>
                            <asp:TextBox ID="USER_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="PASSWORD" DataField="PASSWORD" HeaderText="Password">
                        <ItemTemplate>
                            <asp:TextBox ID="PASSWORD" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="FROM_DATE" DataField="FROM_DATE" HeaderText="From Date">
                        <ItemTemplate>
                            <asp:TextBox ID="FROM_DATE" runat="server" Width="100%" 
                            onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="TO_DATE" DataField="TO_DATE" HeaderText="To Date">
                        <ItemTemplate>
                            <asp:TextBox ID="TO_DATE" runat="server" Width="100%"
                            onclick="showPopup(this, event);"
                            onfocus="showPopup(this, event);"
                            onkeydown ="parseDate(this, event);" runat="server" CssClass="radinput"></asp:TextBox> 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="CREATED_BY" DataField="CREATED_BY" HeaderText="Created by">
                        <ItemTemplate>
                            <asp:Label ID="CREATED_BY" runat="server" Width="100%"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn SortExpression="MODIFIED_BY" DataField="MODIFIED_BY" HeaderText="Modified By">
                        <ItemTemplate>
                            <asp:Label ID="MODIFIED_BY" runat="server" Width="100%"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                      <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="useradded(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnCommand="radgriddetails_Command"  OnRowDblClick="addUserDetail"/>
            </ClientSettings>
        </telerik:radgrid>
        </div>
        <br />
        <table width="100%">
            <tr>
                <td>
                    <div class="pageTitle" style="float: left">
                        <asp:Literal ID="Literal4" runat="server" Text="Contect Detail"></asp:Literal>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:radgrid id="RADGRIDCONTECT111" runat="server" allowpaging="False" allowsorting="False"
                        allowmultirowselection="false" allowfilteringbycolumn="False" gridlines="None"
                        width="100%" showfooter="false" allowautomaticdeletes="True" allowautomaticinserts="True">
            <ItemStyle Wrap="false" />
            <MasterTableView AllowMultiColumnSorting="true" TableLayout="Fixed" EditMode="InPlace">
                <Columns>
                 <telerik:GridTemplateColumn DataField="EMP_CONTACT_SRNO" HeaderText="EMP_CONTACT_SRNO" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_CONTACT_SRNO" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn SortExpression="COUNTRY_NAME" DataField="COUNTRY_NAME" HeaderText="Country">
                        <ItemTemplate>
                            <asp:TextBox ID="COUNTRY_NAME" runat="server" CssClass="radinput"  Width="100%"></asp:TextBox>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWCOUNTRY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                           
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:TextBox ID="NEWSTATE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                         
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                        <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWCITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                         
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ADDRESS_TYPE_NAME" DataField="ADDRESS_TYPE_NAME" HeaderText="Address Type">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_TYPE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWADDRESS_TYPE_NAME" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ADDRESS_LINE1" DataField="ADDRESS_LINE1" HeaderText="AddressLine 1">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE1" runat="server" Width="100px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEWADDRESS_LINE1" runat="server" Width="100px"></telerik:RadTextBox> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ADDRESS_LINE2" DataField="ADDRESS_LINE2" HeaderText="AddressLine 2">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_LINE2" runat="server" Width="100px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                          <telerik:RadTextBox ID="NEWADDRESS_LINE2" runat="server" Width="100px" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn SortExpression="PINCODE" DataField="PINCODE" HeaderText="Pincode">
                        <ItemTemplate>
                            <asp:TextBox ID="PINCODE" runat="server" Width="100px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEWPINCODE" runat="server" Width="100px" /> 
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="PHONE" DataField="PHONE" HeaderText="Phone">
                        <ItemTemplate>
                            <asp:TextBox ID="PHONE" runat="server" Width="100px" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>
                         <FooterTemplate>
                        <telerik:RadTextBox ID="NEW_PHONE" runat="server" Width="100px" />
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A2" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowContectadded(this,event);">
                            &raquo;<asp:TextBox ID="PHOTO" runat="server" CssClass="disable"/>
                            </a>
                        </ItemTemplate>
                         <FooterTemplate>&nbsp;&nbsp;
                         <a id="A3" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newrowContectadded(this,event);">
                            &raquo;<telerik:RadTextBox ID="NEWPHOTO" runat="server" CssClass="disable"/>
                            </a>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnCommand="radgridcontectdetails_Command" OnRowDataBound="radgridContectmaster_RowDataBound" OnRowDblClick="addContectDetail"/>
            </ClientSettings>
        </telerik:radgrid>
                </td>
            </tr>
        </table>
        <div class="pageTitle" style="float: left">
            <asp:Literal ID="Literal5" runat="server" Text="User Role"></asp:Literal>
        </div>
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button5" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this role of employee?'))return false; deleteRole(); return false;"
                        Text="Delete Role" runat="server" />
                </td>
            </tr>
        </table>
        <telerik:radgrid id="radgridrole" runat="server" allowpaging="false" allowmultirowselection="false"
            allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
            allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="USER_ID" AllowMultiColumSorting="true" EditMode ="InPlace">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
               
               <Columns>
                        <telerik:GridTemplateColumn SortExpression ="USER_ID" DataField="USER_ID" HeaderText="COMPANY NAME" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="USER_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn SortExpression ="EMP_ID" DataField="EMP_ID" HeaderText="COMPANY NAME" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="EMP_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>

                         <telerik:GridTemplateColumn SortExpression ="COMPANY_NAME" DataField="COMPANY_NAME" HeaderText="COMPANY NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="COMPANY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="DEPARTMENT_NAME" DataField="DEPARTMENT_NAME" HeaderText="DEPARTMENT NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="DEPARTMENT_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                       
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="ROLE_NAME" DataField="ROLE_NAME" HeaderText="ROLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="ROLE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression ="REPORTING_TO" DataField="REPORTING_TO" HeaderText="REPORTING TO">
                        <ItemTemplate>
                            <asp:TextBox ID="REPORTING_TO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>
                        
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField= "PHOTO">
                        <HeaderStyle Width = "25px" />
                            <ItemTemplate>
                                <a id = "A4" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="roleadded(this,event);">
                                    &raquo;
                                </a>
                                
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>

               </Columns>   
               </MasterTableView>
               
            <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridrole_Command" OnRowSelected="radgridrole_RowSelected" OnRowDblClick="addRole"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
</asp:Content>
