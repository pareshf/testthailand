<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    CodeBehind="EmployeeMaster.aspx.cs" Inherits="CRM.WebApp.Views.Administration.EmployeeMaster" %>

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
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Shared/Javascripts/Script2.js"></script>
        <script type="text/javascript">
            function pageLoad() {
               
                customersTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                customersCommandName = "Load";
                CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
                ordersTableView = $find("<%= radgriddetails.ClientID %>").get_masterTableView();
             //   RadListBox2View = $find("<%= RadListBox2.ClientID %>");
             //   RadListBox3View = $find("<%= RadListBox3.ClientID %>");
              //  RadListBox4View = $find("<%= RadListBox4.ClientID %>");
               // RadListBox5View = $find("<%= RadListBox5.ClientID %>");
                contectTableView = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
                roleTableView = $find("<%= radgridrole.ClientID %>").get_masterTableView();
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
                        currentTextBox.value = args.get_newDate().format('MM/dd/yyyy');
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
                var table = $find("<%= radgridmaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex];
                table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridmaster.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridmaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.EmployeeWebService.DeleteEmployeeByEmployeeID(EMP_ID);
                gridItems[gridItems.length - 1].set_selected(true);
               // var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
               // masterTable.rebind();
            }

            var counterfornewrowleft = 1;
            var counterfornewrowright = 0;
            function AddorUpdateCurrent() {
            }
            //            function newrowContectadded(sender, args) {
            //              //  debugger;
            //                currentRowIndex = sender.parentNode.parentNode.rowIndex;
            //                var ary = [];
            //                //employee id
            //              
            //                for (var i = 0; i < 9; i++) {
            //                    // coountry
            //           
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COUNTRY_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOUNTRY_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOUNTRY_NAME'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COUNTRY_NAME') {
            //                            ary[0] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else
            //                            ary[0] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].childNodes[0].control._value;
            //                    }
            //                    //address line 1
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE1_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE1_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE1_wrapper'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE1_wrapper') {
            //                            ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].firstChild.value;
            //                            
            //                        }
            //                        else
            //                            ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].all[0].all[0].value;
            //                    }
            //                    // address line 2
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE2_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE2_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE2_wrapper'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE2_wrapper') {
            //                            ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].firstChild.value;
            //                           
            //                        }
            //                        else
            //                            ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].all[0].all[0].value;
            //                    }
            //                    //state
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_STATE_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWSTATE_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWSTATE_NAME'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_STATE_NAME') {
            //                            ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else
            //                            ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].control._value;
            //                    }
            //                    //city
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_CITY_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCITY_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCITY_NAME'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_CITY_NAME') {
            //                            ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else
            //                            ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].childNodes[0].control._value;
            //                    }
            //                    //address type
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_TYPE_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_TYPE_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_TYPE_NAME'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_TYPE_NAME') {
            //                            ary[5] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else
            //                            ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].childNodes[0].control._value;
            //                    }
            //                    //pincode
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PINCODE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWPINCODE_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWPINCODE_wrapper'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PINCODE_wrapper') {
            //                            ary[6] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].firstChild.value;
            //                            
            //                        }
            //                        else
            //                            ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].all[0].all[0].value;
            //                    }
            //                    //phone
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PHONE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEW_PHONE_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEW_PHONE_wrapper'||sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PHONE_wrapper') {
            //                            ary[7] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].firstChild.value;
            //                            
            //                        }
            //                        else
            //                            ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex-1].cells[i].all[0].all[0].value;
            //                    }
            //                 
            //                    //date created
            //                    date = new Date().format('MM/dd/yyyy');
            //                    ary[8] = date;
            //                    //pass today date
            //                    //created by
            //                    //pass loggged in person id
            //                    //reporting to
            //                }
            //                try {
            //                    ary[9] = EMP_ID;
            //                   // debugger;
            //                   ary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_CONTACT_SRNO;
            //                    if(ary[11]==null || ary[11]=='undefined')
            //                        ary[11] = 0;
            //                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
            //                    var masterTable = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
            //                    masterTable.rebind();
            //                }
            //                catch (e) {
            //                    ary[11] = 0;
            //                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
            //                    var masterTable = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
            //                    masterTable.rebind();
            //                }
            //                //finally insert or update data
            //                
            //            }
            function newrowContectadded(sender, args) {
               
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                //employee id

                for (var i = 0; i < 9; i++) {
                    // country
                    // debugger;
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COUNTRY_NAME') {
                        ary[0] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //address line 1

                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE1') {
                        ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    // address line 2

                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE2') {

                        ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //state
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_STATE_NAME') {

                        ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //city
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_CITY_NAME') {

                        ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //address type                    
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_TYPE_NAME') {

                        ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //pincode
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PINCODE') {

                        ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //phone
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PHONE') {

                        ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].value;
                    }

                    //date created
                    date = new Date().format('MM/dd/yyyy');
                    ary[8] = date;
                    //pass today date
                    //created by
                    //pass loggged in person id
                    //reporting to
                }
            
                try {
                    ary[9] = EMP_ID;
                   
                    ary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_CONTACT_SRNO;
                    if (ary[11] == null || ary[11] == 'undefined')
                        ary[11] = 0;
                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
                    var masterTable = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
                    masterTable.rebind();
                    alert('Record Save Successfully');
                }
                catch (e) {
                   masterTable.rebind();
                    alert("Wrong Data Inserted");
                    
                }
                //finally insert or update data

            }

            //            function newrowadded(sender, args) {
            //             
            //                currentRowIndex = sender.parentNode.parentNode.rowIndex;
            //                var ary = [];
            //            //    employee id
            //                for (var i = 0; i < 13; i++) {
            //                 //    employee title id
            //                //  debugger;
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TITLE_DESC' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWTITLE_DESC') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWTITLE_DESC') {
            //                            ary[0] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TITLE_DESC') {
            //                            ary[0] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                //    employee surname
            //                 //   debugger;
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_SURNAME_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_SURNAME_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_SURNAME_wrapper') {
            //                            ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].firstChild.value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_SURNAME_wrapper') {
            //                            ary[1] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].firstChild.value;
            //                        }
            //                    }
            //                //     employee name
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_NAME_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_NAME_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_NAME_wrapper') {
            //                            ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].firstChild.value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_NAME_wrapper') {
            //                            ary[2] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].all[0].firstChild.value;
            //                        }
            //                    }
            //                //    employee dob
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_DOB' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_DOB') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_DOB') {
            //                            ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_DOB') {
            //                            ary[3] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].all[0].value;
            //                        }
            //                    }
            //                  //  emp gender
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_GENDER' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_GENDER') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_GENDER') {
            //                            ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_GENDER') {
            //                            ary[4] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                 //   employee email
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_EMAIL_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_EMAIL_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_EMAIL_wrapper') {
            //                            ary[5] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_EMAIL_wrapper') {
            //                            ary[5] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].childNodes[0].value;
            //                        }
            //                    }
            //                //    employee mobile
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_MOBILE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_MOBILE_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_MOBILE_wrapper') {
            //                            ary[6] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_MOBILE_wrapper') {
            //                            ary[6] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].all[0].all[0].value;
            //                        }
            //                    }
            //                   // emp phone
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_PHONE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_PHONE_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_PHONE_wrapper') {
            //                            ary[7] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_PHONE_wrapper') {
            //                            ary[7] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].all[0].all[0].value;
            //                        }
            //                    }
            //                 //    emp maritalstatus
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_MARITAL_DATA' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWMARITAL_DATA') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWMARITAL_DATA') {
            //                            ary[8] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;

            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_MARITAL_DATA') {
            //                            ary[8] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].all[0].value;
            //                        }
            //                    }
            //                  //  emp quelification id 
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_QUALIFICATION_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWQUALIFICATION_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWQUALIFICATION_NAME') {
            //                            ary[9] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_QUALIFICATION_NAME') {
            //                            ary[9] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                  //  emp status
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_STATUS_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWSTATUS_NAME') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWSTATUS_NAME') {
            //                            ary[10] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_STATUS_NAME') {
            //                            ary[10] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                    //date created
            //                    date = new Date().format('MM/dd/yyyy');
            //                    ary[11] = date;
            //                   // pass today date
            //                    //created by
            //                    //pass loggged in person id
            //                    //reporting to
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_REPORTING_TO' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWREPORTING_TO') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWREPORTING_TO') {
            //                            ary[13] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].value;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_REPORTING_TO') {
            //                            ary[13] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_ID_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_ID_wrapper') {
            //                        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_ID_wrapper') {
            //                            ary[14] = EMP_ID;
            //                        }
            //                        else if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_ID_wrapper') {
            //                            ary[14] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 2].cells[i].childNodes[0].value;
            //                        }
            //                    }
            //                }
            //           
            //                try {
            //                 
            //                   ary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 2]._dataItem.EMP_ID;
            //                   var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
            //                   CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary);
            //                   
            //                    masterTable.rebind();
            //                    alert("Record Save Successfully")
            //                }
            //                catch (e) {
            //                    alert(e);   
            //                    ary[14] = 0;
            //                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary);
            //                    var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
            //                    masterTable.rebind();
            //                }
            //            }
            function newrowadded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                //    employee id
                for (var i = 0; i < 12; i++) {
                    if (i < 10)
                        i = '0' + i;
                   
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_TITLE_DESC') {
                        ary[0] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;
                    }
                    //    employee surname
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_SURNAME') {
                        ary[1] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;
                    }

                    //     employee name

                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_NAME') {
                        ary[2] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;
                    }
                    //    employee dob
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_DOB') {
                        ary[3] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    //  emp gender
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_GENDER') {
                        ary[4] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    //   employee email
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_EMAIL') {
                        ary[5] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    //    employee mobile
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_MOBILE') {
                        ary[6] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    // emp phone
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_PHONE') {
                        ary[7] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    //    emp maritalstatus
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_MARITAL_DATA') {
                        ary[8] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].all[0].value;

                    }
                    //  emp quelification id 
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_QUALIFICATION_NAME') {
                        ary[9] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                    //  emp status
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_STATUS_NAME') {
                        ary[10] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                   
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_SIGNATURE_PASSWORD') {
                        ary[15] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                    //date created
                    date = new Date().format('MM/dd/yyyy');
                    ary[11] = date;
                  
                }

                if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                if (ary[5] == "" || ary[5] == 'null') ary[5] = 0;
                if (ary[6] == "" || ary[6] == 'null') ary[6] = 0;
                if (ary[7] == "" || ary[7] == 'null') ary[7] = 0;
                if (ary[8] == "" || ary[8] == 'null') ary[8] = 0;
                if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
                if (ary[10] == "" || ary[10] == 'null') ary[10] = 0;
                if (ary[11] == "" || ary[11] == 'null') ary[11] = 0;
                if (ary[12] == "" || ary[12] == 'null') ary[12] = 0;
                if (ary[13] == "" || ary[13] == 'null') ary[13] = 0;
                if (ary[14] == "" || ary[14] == 'null') ary[14] = 0;
                try {
                    
                    ary[13] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_ID;
                    var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
                    CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary);

                    masterTable.rebind();
                    alert("Record Save Successfully")
                }
                catch (e) {
                    alert("Wrong Data Inserted");

                }
            }

            function useradded(sender, args) {
                currentDatePicker.hidePopup();
                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                //employee id
                for (var i = 0; i < 6; i++) {
                    //debugger;
                    //employee username
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_USER_NAME')
                        ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
                    //employee START DATE
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_FROM_DATE')
                        ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
                    //emp END DATE
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_TO_DATE')
                        ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
                    //employee PASSWORD
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_PASSWORD')
                        ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
                    //EMP ID
                    ary[5] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[0]._dataItem.EMP_ID;
                }
                ary[0] = 0;
                if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
                if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
                if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
                if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;
                CRM.WebApp.webservice.EmployeeWebService.UpdateUserDetails(ary);
                try {
                    var masterTable = $find("<%= radgriddetails.ClientID %>").get_masterTableView();
                    masterTable.rebind();
                    alert('Record Save Successfully');
                } catch (e) {

                    alert('Wrong Data Inserted');
                }
                // do code to clear all footer control data from here then you can add more data from here
                // code to add new row in radgrid
            }
            function roleadded(sender, args) {


                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];
                for (var i = 0; i < 4; i++) {
                    if (i < 10)
                        i = '0' + i;
                    
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_COMPANY_NAME') {
                        ary[1] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;
                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_DEPARTMENT_NAME') {
                        ary[2] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_ROLE_NAME') {
                        ary[3] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                    if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridrole_ctl00_ctl04_REPORTING_TO') {
                        ary[4] = sender.parentNode.parentNode.parentNode.rows[currentRowIndex - 1].cells[i].childNodes[0].value;

                    }
                    

                }
                ary[0] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[0]._dataItem.USER_ID;
                CRM.WebApp.webservice.EmployeeWebService.InsertUserRoleForEmployee(ary);
                try {
                    var masterTable = $find("<%= radgridrole.ClientID %>").get_masterTableView();
                    masterTable.rebind();
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
            //transfer data of listbox left to right
            function transferRight() {
                var listBox2 = $find("<%= RadListBox2.ClientID %>");
                var selectedItem = listBox2.get_selectedItem();
                if (selectedItem == null) {
                    alert("You need to select a item first.");
                    return false;
                }
                listBox2.transferToDestination(selectedItem);
                //add to database
                CRM.WebApp.webservice.EmployeeWebService.InsertAssignCompany(selectedItem._properties._data.value, EMP_ID);
                return false;
            }
            function transferLeft() {
                var listBox2 = $find("<%= RadListBox2.ClientID %>");
                var listBox3 = listBox2.get_transferTo();
                var selectedItem = listBox3.get_selectedItem();
                if (selectedItem == null) {
                    alert("You need to select a item first.");
                    return false;
                }
                listBox2.transferFromDestination(selectedItem);
                CRM.WebApp.webservice.EmployeeWebService.UnAssignCompany(selectedItem._properties._data.value, EMP_ID);
                return false;
            }
            function transferRight4() {
                var listBox2 = $find("<%= RadListBox4.ClientID %>");

                var selectedItem = listBox2.get_selectedItem();
                if (selectedItem == null) {
                    alert("You need to select a item first.");
                    return false;
                }
                listBox2.transferToDestination(selectedItem);
                CRM.WebApp.webservice.EmployeeWebService.InsertAssignRole(EMP_ID, 0, selectedItem._properties._data.value);
                return false;
            }

            function transferLeft4() {
                var listBox2 = $find("<%= RadListBox4.ClientID %>");
                var listBox3 = listBox2.get_transferTo();
                var selectedItem = listBox3.get_selectedItem();
                if (selectedItem == null) {
                    alert("You need to select a item first.");
                    return false;
                }
                listBox2.transferFromDestination(selectedItem);
                CRM.WebApp.webservice.EmployeeWebService.UnAssignRole(EMP_ID, 0, selectedItem._properties._data.value);
                return false;
            }
            function saveall() {

            }
            function deleteRole() {
                var table = $find("<%= radgridrole.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex];
                table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridrole.ClientID %>").get_masterTableView()._dataItems, dataItem);
                }
                var gridItems = $find("<%= radgridrole.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.EmployeeWebService.DeleteEmployeeRole(USER_ID,COMP_NAME,DEPT_NAME,ROLE_NAME);
                gridItems[gridItems.length - 1].set_selected(true);
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
                allowsorting="True" showfooter="false" pagesize="10" enableembeddedskins="false"
                allowautomaticdeletes="True" allowautomaticinserts="True"  PagerStyle="AlwaysVisible">
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
                            <%--<telerik:RadTextBox ID="TITLE_DESC" runat="server" Width="100%" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOTITLE_DESC" 
                                runat="server" 
                                DataSourceID="SqlDataSource1"
                                DataTextField="TITLE_DESC" 
                                DataValueField="TITLE_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT TITLE_ID,TITLE_DESC FROM dbo.COMMON_TITLE_MASTER" /> --%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox id="NEWTITLE_DESC" runat="server"></asp:TextBox>
                         <%--<telerik:RadComboBox ID="NEWCOMBOTITLE_DESC" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOTITLE_DESC"
                                DataTextField="TITLE_DESC" 
                                DataValueField="TITLE_ID" Width="100%">                                
                            </telerik:RadComboBox> 
                             <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOTITLE_DESC" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT TITLE_ID,TITLE_DESC FROM dbo.COMMON_TITLE_MASTER" />  --%>
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
                            <%--<telerik:RadTextBox ID="EMP_GENDER" runat="server" Width="100%" Visible="false" />
                             <telerik:RadComboBox ID="COMBOEMP_GENDER" 
                                runat="server" Width="100%">                                
                                 <Items>
                                 <telerik:RadComboBoxItem Text="Male" Value="M" />
                                 <telerik:RadComboBoxItem Text="Female" Value="F" />
                                 </Items>
                            </telerik:RadComboBox>  --%>
                        </ItemTemplate>
                         <FooterTemplate>
                                <asp:TextBox ID="NEWEMP_GENDER" runat="server" CssClass="radinput"></asp:TextBox>
                        <%--<telerik:RadComboBox ID="NEWCOMBOEMP_GENDER" 
                                runat="server" Width="100%">                                
                                 <Items>
                                 <telerik:RadComboBoxItem Text="Male" Value="M" />
                                 <telerik:RadComboBoxItem Text="Female" Value="F" />
                                 </Items>
                            </telerik:RadComboBox>--%>   
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
                            <%--<telerik:RadTextBox ID="MARITAL_DATA" runat="server" Width="100%" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOMARITAL_DATA" 
                                runat="server" 
                                DataSourceID="SqlDataSource2"
                                DataTextField="MARITAL_STATUS_NAME" 
                                DataValueField="MARITAL_STATUS_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select MARITAL_STATUS_ID,MARITAL_STATUS_NAME from dbo.COMMON_MARITAL_STATUS_MASTER" />--%> 
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:TextBox id="NEWMARITAL_DATA" runat="server" CssClass="radinput"></asp:TextBox>
                            <%--<telerik:RadComboBox ID="NEWCOMBOMARITAL_DATA" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOMARITAL"
                                DataTextField="MARITAL_STATUS_NAME" 
                                DataValueField="MARITAL_STATUS_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOMARITAL" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select MARITAL_STATUS_ID,MARITAL_STATUS_NAME from dbo.COMMON_MARITAL_STATUS_MASTER" />  --%>
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
                           <%-- <telerik:RadTextBox ID="QUALIFICATION_NAME" runat="server" Width="100%" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOQUALIFICATION_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSource3"
                                DataTextField="QUALIFICATION_NAME" 
                                DataValueField="QUALIFICATION_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select QUALIFICATION_ID,QUALIFICATION_NAME from dbo.COMMON_QUALIFICATION_MASTER" />--%>
                        </ItemTemplate>
                         <FooterTemplate>
                         <asp:TextBox id="NEWQUALIFICATION_NAME" runat="server"></asp:TextBox>
                            <%--<telerik:RadComboBox ID="NEWCOMBOQUALIFICATION_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOQUALIFICATION"
                                DataTextField="QUALIFICATION_NAME" 
                                DataValueField="QUALIFICATION_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOQUALIFICATION" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select QUALIFICATION_ID,QUALIFICATION_NAME from dbo.COMMON_QUALIFICATION_MASTER" />--%>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATUS_NAME" DataField="STATUS_NAME" HeaderText="Status">
                        <ItemTemplate>
                                <asp:TextBox id="STATUS_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                            <%--<telerik:RadTextBox ID="STATUS_NAME" runat="server" Width="100%" Visible="false"/>
                             <telerik:RadComboBox ID="COMBOSTATUS_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSource4"
                                DataTextField="STATUS_NAME" 
                                DataValueField="STATUS_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select STATUS_ID,STATUS_NAME from dbo.COMMON_STATUS_MASTER" />--%>
                        </ItemTemplate>
                         <FooterTemplate>
                                 <asp:TextBox id="NEWSTATUS_NAME" runat="server"></asp:TextBox>
                            <%-- <telerik:RadComboBox ID="NEWCOMBOSTATUS_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOSTATUS"
                                DataTextField="STATUS_NAME" 
                                DataValueField="STATUS_ID" Width="100%">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOSTATUS" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="select STATUS_ID,STATUS_NAME from dbo.COMMON_STATUS_MASTER" />--%>
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
                    <telerik:GridTemplateColumn SortExpression="SIGNATURE_PASSWORD" DataField="SIGNATURE_PASSWORD" HeaderText="Signatue Password">
                        <ItemTemplate>
                            <asp:TextBox ID="SIGNATURE_PASSWORD" runat="server" Width="100%" CssClass="radinput" TextMode="password"></asp:TextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%-- <telerik:GridTemplateColumn SortExpression="SIGNATURE_PASSWORD" DataField="SIGNATURE_PASSWORD" HeaderText="Signature Password">
                        <ItemTemplate>
                            <asp:TextBox ID="SIGNATURE_PASSWORD" runat="server" CssClass="radinput" TextMode="Password"></asp:TextBox>                      
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
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
                <ClientEvents OnCommand="radgridmaster_Command" OnRowSelected="radgridmaster_RowSelected" OnRowDataBound="radgridmaster_RowDataBound" />
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
                allowfilteringbycolumn="False" gridlines="None">
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
                <ClientEvents OnCommand="radgriddetails_Command" />
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
                    <telerik:radgrid id="RADGRIDCONTECT111" runat="server" allowpaging="False" allowsorting="False" allowmultirowselection="false"
                        allowfilteringbycolumn="False" gridlines="None" width="100%" showfooter="false" allowautomaticdeletes="True" allowautomaticinserts="True">
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
                            <%--<telerik:RadTextBox ID="COUNTRY_NAME" runat="server" Width="100px" Visible="false" />
                            <telerik:RadComboBox ID="COMBOCOUNTRY_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceCOMBOCOUNTRY_NAME"
                                DataTextField="COUNTRY_NAME" 
                                DataValueField="COUNTRY_ID" Width="100px">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceCOMBOCOUNTRY_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT COUNTRY_ID,COUNTRY_NAME FROM dbo.COMMON_COUNTRY_MASTER" /> --%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWCOUNTRY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                        <%-- <telerik:RadComboBox ID="NEWCOMBOCOUNTRY_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOCOUNTRY_NAME"
                                DataTextField="COUNTRY_NAME" 
                                DataValueField="COUNTRY_ID" Width="100px">                                
                            </telerik:RadComboBox> 
                             <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOCOUNTRY_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT COUNTRY_ID,COUNTRY_NAME FROM dbo.COMMON_COUNTRY_MASTER" />  --%>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="STATE_NAME" DataField="STATE_NAME" HeaderText="State">
                        <ItemTemplate>
                            <asp:TextBox ID="STATE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                           <%-- <telerik:RadTextBox ID="STATE_NAME" runat="server" Width="100px" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOSTATE_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceCOMBOSTATE_NAME"
                                DataTextField="STATE_NAME" 
                                DataValueField="STATE_ID" Width="100px">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceCOMBOSTATE_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT STATE_ID,STATE_NAME FROM dbo.COMMON_STATE_MASTER" /> --%>
                        </ItemTemplate>
                        <FooterTemplate>
                             <asp:TextBox ID="NEWSTATE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                         <%--<telerik:RadComboBox ID="NEWCOMBOSTATE_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOSTATE_NAME"
                                DataTextField="STATE_NAME" 
                                DataValueField="STATE_ID" Width="100px">                                
                            </telerik:RadComboBox> 
                             <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOSTATE_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT STATE_ID,STATE_NAME FROM dbo.COMMON_STATE_MASTER" />  --%>
                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="CITY_NAME" DataField="CITY_NAME" HeaderText="City">
                        <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                            <%--<telerik:RadTextBox ID="CITY_NAME" runat="server" Width="100%" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOCITY_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceCOMBOCITY_NAME"
                                DataTextField="CITY_NAME" 
                                DataValueField="CITY_ID" Width="100px">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceCOMBOCITY_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT CITY_NAME,CITY_ID FROM dbo.COMMON_CITY_MASTER" /> --%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWCITY_NAME" runat="server" CssClass="radinput"></asp:TextBox>
                         <%--<telerik:RadComboBox ID="NEWCOMBOCITY_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOCITY_NAME"
                                DataTextField="CITY_NAME" 
                                DataValueField="CITY_ID" Width="100px">                                
                            </telerik:RadComboBox> 
                             <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOCITY_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT CITY_ID,CITY_NAME FROM dbo.COMMON_CITY_MASTER" />  
--%>                        </FooterTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="ADDRESS_TYPE_NAME" DataField="ADDRESS_TYPE_NAME" HeaderText="Address Type">
                        <ItemTemplate>
                            <asp:TextBox ID="ADDRESS_TYPE_NAME" runat="server" Width="100%" CssClass="radinput"></asp:TextBox>
                            <%--<telerik:RadTextBox ID="ADDRESS_TYPE_NAME" runat="server" Width="100%" Visible="false"/>
                            <telerik:RadComboBox ID="COMBOADDRESS_TYPE_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceCOMBOADDRESS_TYPE_NAME"
                                DataTextField="ADDRESS_TYPE_NAME" 
                                DataValueField="ADDRESS_TYPE_ID" Width="100px">                                
                            </telerik:RadComboBox>  
                            <asp:SqlDataSource runat="server" ID="SqlDataSourceCOMBOADDRESS_TYPE_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT ADDRESS_TYPE_ID,ADDRESS_TYPE_NAME FROM dbo.COMMON_ADDRESS_TYPE_MASTER" /> --%>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NEWADDRESS_TYPE_NAME" runat="server"></asp:TextBox>
                         <%--<telerik:RadComboBox ID="NEWCOMBOADDRESS_TYPE_NAME" 
                                runat="server" 
                                DataSourceID="SqlDataSourceNEWCOMBOADDRESS_TYPE_NAME"
                                DataTextField="ADDRESS_TYPE_NAME" 
                                DataValueField="ADDRESS_TYPE_ID" Width="100px">                                
                            </telerik:RadComboBox> 
                             <asp:SqlDataSource runat="server" ID="SqlDataSourceNEWCOMBOADDRESS_TYPE_NAME" ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT DISTINCT ADDRESS_TYPE_ID,ADDRESS_TYPE_NAME FROM dbo.COMMON_ADDRESS_TYPE_MASTER" />  --%>
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
                <ClientEvents OnCommand="radgridcontectdetails_Command" OnRowDataBound="radgridContectmaster_RowDataBound"/>
            </ClientSettings>
        </telerik:radgrid>
                </td>
            </tr>
        </table>
        <br />
      <table Visible="false">
            
                   <%-- <span class="pageTitle" style="float: left; font-size: 12px;">Company :</span>--%>
                   <%-- <span class="pageTitle" style="float: left; font-size: 12px;">Assigned Company :</span>--%>
                   <%-- <span class="pageTitle" style="float: left; font-size: 12px;">Role :</span>--%>
                   <%-- <span class="pageTitle" style="float: left; font-size: 12px;">Assigned Role :</span>--%>
            <tr>
                <td>
                    <telerik:radlistbox id="RadListBox2" runat="server" height="130px" transfertoid="RadListBox3"
                        datatextfield="COMPANY_NAME" datavaluefield="COMPANY_ID" Visible="false"></telerik:radlistbox>
                </td>
                <td>
                    <asp:Button ID="Button3" OnClientClick="return transferRight()" Text=">>" runat="server" Visible="false" /><br />
                    <asp:Button ID="Button4" OnClientClick="return transferLeft()" Text="<<" runat="server" Visible="false" />
                </td>
                <td>
                    <telerik:radlistbox id="RadListBox3" runat="server" height="130px" datatextfield="COMPANY_NAME"
                        datavaluefield="COMPANY_ID" Visible="false"></telerik:radlistbox>
                </td>
                <td style="width: 20px;">
                </td>
                <td>
                    <telerik:radlistbox id="RadListBox4" runat="server" height="130px" transfertoid="RadListBox5"
                        datatextfield="ROLE_NAME" datavaluefield="ROLE_ID" Visible="false"></telerik:radlistbox>
                </td>
                <td>
                    <asp:Button ID="btnleft" OnClientClick="return transferRight4()" Text=">>" runat="server" Visible="false"/><br />
                    <asp:Button ID="btnright" OnClientClick="return transferLeft4()" Text="<<" runat="server" Visible="false" />
                </td>
                <td>
                    <telerik:radlistbox id="RadListBox5" runat="server" height="130px" datatextfield="ROLE_NAME"
                        datavaluefield="ROLE_ID" Visible="false"></telerik:radlistbox>
                </td>
            </tr>
        </table>
   </div>
    <script>
        document.getElementById('divradmastergrid').style.width = screen.width - 200 + 'px';
        document.getElementById('divradgriddetails').style.width = screen.width - 200 + 'px';
    </script>
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
                <ClientEvents OnCommand="radgridrole_Command" OnRowSelected="radgridrole_RowSelected"/>
                <Selecting AllowRowSelect="True"/>
            </ClientSettings>
         </telerik:radgrid>
</asp:Content>
