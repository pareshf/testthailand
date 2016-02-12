function pageLoad() {
    customersTableView = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
    customersCommandName = "Load";
    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
    ordersTableView = $find("<%= radgriddetails.ClientID %>").get_masterTableView();
    RadListBox2View = $find("<%= RadListBox2.ClientID %>");
    RadListBox3View = $find("<%= RadListBox3.ClientID %>");
    RadListBox4View = $find("<%= RadListBox4.ClientID %>");
    RadListBox5View = $find("<%= RadListBox5.ClientID %>");
    contectTableView = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
}
document.forms[0].onsubmit = function () {
    var hasDeletedItems = $find("<%= radgridmaster.ClientID %>")._deletedItems.length > 0;
    if (hasDeletedItems) {
        if (!confirm("Are You Sure To Delete?")) {
            $find("<%= radgridmaster.ClientID %>")._deletedItems = [];
            $find("<%= radgridmaster.ClientID %>").updateClientState();
        }
    }
}
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
}

var counterfornewrowleft = 1;
var counterfornewrowright = 0;
function AddorUpdateCurrent() {
}
function newrowContectadded(sender, args) {
    currentRowIndex = sender.parentNode.parentNode.rowIndex;
    var ary = [];
    //employee id
    for (var i = 0; i < 9; i++) {
        // employee title id
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COMBOCOUNTRY_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOCOUNTRY_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOCOUNTRY_NAME') {
                ary[0] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[0] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].control._value;
        }
        //employee surname
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE1_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE1_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWADDRESS_LINE1_wrapper') {
                ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].all[0].value;
        }
        // employee name
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_ADDRESS_LINE2_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_ADDRESS_LINE2_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_ADDRESS_LINE2_wrapper') {
                ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].all[0].value;
        }
        //employee dob
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COMBOSTATE_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOSTATE_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOSTATE_NAME') {
                ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].control._value;
        }
        //emp phone
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COMBOCITY_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOCITY_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOCITY_NAME') {
                ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].control._value;
        }
        //employee marital status
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_COMBOADDRESS_TYPE_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOADDRESS_TYPE_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWCOMBOADDRESS_TYPE_NAME') {
                ary[5] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].childNodes[0].control._value;
        }
        //employee gender
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PINCODE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWPINCODE_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWPINCODE_wrapper') {
                ary[6] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].all[0].value;
        }
        //emp email
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl04_PHONE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_PHONE_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_RADGRIDCONTECT111_ctl00_ctl03_ctl00_NEWPHONE_wrapper') {
                ary[7] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 1].cells[i].all[0].all[0].value;
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
        debugger;
        ary[11] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.EMP_CONTACT_SRNO;
        if (ary[11] == null || ary[11] == 'undefined')
            ary[11] = 0;
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
    }
    catch (e) {
        ary[11] = 0;
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
        var masterTable = $find("<%= RADGRIDCONTECT111.ClientID %>").get_masterTableView();
        masterTable.rebind();
    }
    //finally insert or update data

}

function newrowadded(sender, args) {
    currentRowIndex = sender.parentNode.parentNode.rowIndex;
    var ary = [];
    //employee id
    for (var i = 0; i < 13; i++) {
        // employee title id
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOTITLE_DESC' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOTITLE_DESC') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOTITLE_DESC') {
                ary[0] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[0] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
        //employee surname
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_SURNAME_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_SURNAME_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_SURNAME_wrapper') {
                ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[1] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].all[0].value;
        }
        // employee name
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_NAME_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_NAME_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_NAME_wrapper') {
                ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[2] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].all[0].value;
        }
        //employee dob
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_DOB' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_DOB') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_DOB') {
                ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value = '';
            }
            else
                ary[3] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].value;
        }
        //emp phone
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOMARITAL_DATA' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOMARITAL_DATA') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOMARITAL_DATA') {
                ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[4] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
        //employee marital status
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOEMP_GENDER' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOEMP_GENDER') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOEMP_GENDER') {
                ary[5] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[5] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
        //employee gender
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_EMAIL_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_EMAIL_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_EMAIL_wrapper') {
                ary[6] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[6] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].all[0].value;
        }
        //emp email
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_MOBILE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_MOBILE_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_MOBILE_wrapper') {
                ary[7] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[7] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].all[0].value;
        }
        // emp mobile
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_EMP_PHONE_wrapper' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_PHONE_wrapper') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWEMP_PHONE_wrapper') {
                ary[8] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
                sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value = '';
            }
            else
                ary[8] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].all[0].all[0].value;
        }
        //emp quelification id 
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOQUALIFICATION_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOQUALIFICATION_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOQUALIFICATION_NAME') {
                ary[9] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[9] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
        //emp status
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOSTATUS_NAME' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOSTATUS_NAME') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOSTATUS_NAME') {
                ary[10] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[10] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
        //date created
        date = new Date().format('MM/dd/yyyy');
        ary[11] = date;
        //pass today date
        //created by
        //pass loggged in person id
        //reporting to
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl04_COMBOREPORTING_TO' || sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOREPORTING_TO') {
            if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgridmaster_ctl00_ctl03_ctl00_NEWCOMBOREPORTING_TO') {
                ary[13] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].control._value;
            }
            else
                ary[13] = sender.parentNode.parentNode.parentNode.rows[sender.parentNode.parentNode.rowIndex - 2].cells[i].childNodes[0].control._value;
        }
    }
    try {
        ary[14] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 2]._dataItem.EMP_ID;
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary);
    }
    catch (e) {
        ary[14] = 0;
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary);
        var masterTable = $find("<%= radgridmaster.ClientID %>").get_masterTableView();
        masterTable.rebind();
    }
}
function useradded(sender, args) {
    currentDatePicker.hidePopup();
    currentRowIndex = sender.parentNode.parentNode.rowIndex;
    var ary = [];
    //employee id
    for (var i = 0; i < 6; i++) {
        //employee username
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_USER_NAME_wrapper')
            ary[1] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
        //employee START DATE
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_FROM_DATE')
            ary[2] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
        //emp END DATE
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_TO_DATE')
            ary[3] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].value;
        //employee PASSWORD
        if (sender.parentNode.parentNode.parentNode.rows[0].cells[i].childNodes[0].id == 'ctl00_cphPageContent_radgriddetails_ctl00_ctl04_PASSWORD_wrapper')
            ary[4] = sender.parentNode.parentNode.parentNode.rows[0].cells[i].all[0].all[0].value;
        //EMP ID
        ary[5] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[0]._dataItem.EMP_ID;
    }
    CRM.WebApp.webservice.EmployeeWebService.UpdateUserDetails(ary);
    try {
        var masterTable = $find("<%= radgriddetails.ClientID %>").get_masterTableView();
        masterTable.rebind();
    } catch (e) { }
    // do code to clear all footer control data from here then you can add more data from here
    // code to add new row in radgrid
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