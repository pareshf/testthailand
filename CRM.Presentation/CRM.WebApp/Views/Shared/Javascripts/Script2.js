var EMP_ID = "";
var USER_ID = "";
var customersCommandName = "";
var roleCommandName = "";
var roleTableView = null;
var ordersCommandName = "";
var ContectCommandName = "";
var customersTableView = null;
var contectTableView = null;
var ordersTableView = null;
var _resultofAllcompany = null;
var _resultofAllRole = null;
var _resultofcompany = null;
var _resultofRole = null;
var COMP_NAME = "";
var DEPT_NAME = "";
var ROLE_NAME = "";
var s = "";
var m = "";

function radgridmaster_Command(sender, args) {
    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    customersTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(customersTableView.get_currentPageIndex() * customersTableView.get_pageSize(),customersTableView.get_pageSize(),
                        customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(),updateGrid);
    customersCommandName = args.get_commandName();
}
function radgridrole_Command(sender, args) {

}
//edited customer id to empid......ronak patel
function radgridmaster_RowSelected(sender, args) {
    try {

   //  debugger;
        EMP_ID = args.get_gridDataItem()._dataItem.EMP_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        loadOrders();
        loadContect();
     //   loadAllCompany();
      //  loadAllRole();
     //   loadRoles();  // load roles
      //  loadCompany();
        loadMyRole();
    }
    catch (e) { }
}

function radgridrole_RowSelected(sender, args) {
    try {
        USER_ID = args.get_gridDataItem()._dataItem.USER_ID;
        COMP_NAME = args.get_gridDataItem()._dataItem.COMPANY_NAME;
        DEPT_NAME = args.get_gridDataItem()._dataItem.DEPARTMENT_NAME;
        ROLE_NAME = args.get_gridDataItem()._dataItem.ROLE_NAME;
    }
    catch (e) { }
}
// function created by ronak patel

function updateGrid(result) {
    customersTableView.set_dataSource(result);
    customersTableView.dataBind();
    if (result.length > 0) {
        customersTableView.selectItem(0);
        EMP_ID = result[0]["EMP_ID"];
    }
    else {
        EMP_ID = "";
    }
    if (customersCommandName == "Filter" || customersCommandName == "Load") {
        CRM.WebApp.webservice.EmployeeWebService.GetCustomersCount(updateVirtualItemCount);
    }
    //loadOrders();
}

function loadOrders() {
    ordersCommandName = "Load";
    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateOrdersGrid);
    
}
function loadMyRole() {
    CRM.WebApp.webservice.EmployeeWebService.GetMyRole(EMP_ID, updateMyRoleGrid);
}
//load contect

function loadContect() {
    CRM.WebApp.webservice.EmployeeWebService.GetContectByEMP_ID(EMP_ID, updateContectGrid);
    
}

//load roles
function loadRoles() {
    CRM.WebApp.webservice.EmployeeWebService.GetRolebyEmpID(EMP_ID,updateRoleListBox);
}


//load All company
function loadAllCompany() {
    CRM.WebApp.webservice.EmployeeWebService.GetAllCompany(updateAllCompanyListBox);
}

//load All Role
function loadAllRole() {
    CRM.WebApp.webservice.EmployeeWebService.GetAllRole(updateAllRoleListBox);
}

//load company
function loadCompany() {
    CRM.WebApp.webservice.EmployeeWebService.GetCompanybyEmpID(EMP_ID, updateCompanyListBox);
}

function updateVirtualItemCount(result) {
    customersTableView.set_virtualItemCount(result);
}

function radgriddetails_Command(sender, args) {
    args.set_cancel(true);
    ordersCommandName = args.get_commandName();
    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID,updateOrdersGrid);
}

function radgridcontectdetails_Command(sender, args) {
    args.set_cancel(true);
    ContectCommandName = args.get_commandName();
    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateContectGrid);
}

function updateRoleListBox(result) {
    _resultofRole = result;
    CRM.WebApp.webservice.EmployeeWebService.rolecount(updateRoleListBoxcount);
}

//all role count

function updateAllRoleListBox(result) {
    _resultofAllRole = result;
    CRM.WebApp.webservice.EmployeeWebService.rolecount(updateAllRoleListBoxcount);
}
//function updateAllRoleListBoxcount(countiee4) {
//  RadListBox4View.get_items().clear();
//    var item = new Telerik.Web.UI.RadListBoxItem();
//    for (var i = 0; i < countiee4; i++) {
//        try {
//            var items = RadListBox4View.get_items(); RadListBox4View.trackChanges();
//            var item = new Telerik.Web.UI.RadListBoxItem();
//            item.set_text(_resultofAllRole[i].ROLE_NAME);
//            item.set_value(_resultofAllRole[i].ROLE_ID);
//            RadListBox4View.get_items().add(item);
//            RadListBox4View.commitChanges();
//        } catch (e) { }
//    }
//   
//   
//}
// all company count

function updateAllCompanyListBox(result) {
    _resultofAllcompany = result;
    CRM.WebApp.webservice.EmployeeWebService.rolecount(updateAllCompanyListBoxcount);
}
//function updateAllCompanyListBoxcount(countiee3) {
//    RadListBox2View.get_items().clear();
//    for (var i = 0; i < countiee3; i++) {
//        try {
//            var items = RadListBox2View.get_items(); RadListBox2View.trackChanges();
//            var item = new Telerik.Web.UI.RadListBoxItem();
//            item.set_text(_resultofAllcompany[i].COMPANY_NAME);
//            item.set_value(_resultofAllcompany[i].COMPANY_ID);
//            RadListBox2View.get_items().add(item);
//            RadListBox2View.commitChanges();
//        } catch (e) { }
//    }
//   
//}

//function updateRoleListBoxcount(countiee1) {
//   RadListBox5View.get_items().clear();

//    for (var i = 0; i < countiee1; i++) {
//        try {
//            var item = new Telerik.Web.UI.RadListBoxItem();
//            item.set_text(_resultofRole[i].ROLE_NAME);
//            item.set_value(_resultofRole[i].ROLE_ID);
//        RadListBox5View.trackChanges();
//        //remove already inserted item from list box
//        var itemrempove = RadListBox4View.findItemByText(item._text);
//        RadListBox4View.get_items().remove(itemrempove);
//        //remove process over
//        RadListBox5View.get_items().add(itemrempove);
//        RadListBox5View.commitChanges();
//    } catch (e) { }
//    }
//}
//function updateCompanyListBoxcount(countiee2) {
//        RadListBox3View.get_items().clear();
//    var item = new Telerik.Web.UI.RadListBoxItem();
//    for (var i = 0; i < countiee2; i++) {
//        try {
//            item.set_text(_resultofcompany[i].COMPANY_NAME);
//            item.set_value(_resultofcompany[i].COMPANY_ID);
//       
//        RadListBox3View.trackChanges();
//        //remove already inserted item from list box
//        var itemrempove = RadListBox2View.findItemByText(item._text);
//        RadListBox2View.get_items().remove(itemrempove);
//        RadListBox3View.get_items().add(itemrempove);
//        RadListBox3View.commitChanges();
//    } catch (e) { }
//    }
//}
function updateCompanyListBox(result) {
    _resultofcompany = result;
    CRM.WebApp.webservice.EmployeeWebService.companycount(updateCompanyListBoxcount);
}

function updateOrdersGrid(result) {
    ordersTableView.set_dataSource(result);
    ordersTableView.dataBind();
    if (ordersCommandName == "Filter" || ordersCommandName == "Load") {
        CRM.WebApp.webservice.EmployeeWebService.GetOrdersByEMP_IDCount(updateOrdersVirtualItemCount);
    }
}

function updateContectGrid(result) {
    contectTableView.set_dataSource(result);
    contectTableView.dataBind();
        //  CRM.WebApp.webservice.EmployeeWebService.GetOrdersByEMP_IDCount(updateOrdersVirtualItemCount);
}

function updateOrdersVirtualItemCount(result) {
    ordersTableView.set_virtualItemCount(result);
}
function updateMyRoleGrid(result) {
    roleTableView.set_dataSource(result);
    roleTableView.dataBind();
    //if (result.length > 0) { roleTableView.selectItem(0); }
}

// Double Click Save kirit Employee Grid

function addEmployee(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = customersTableView.get_dataItems()[currentRowIndex].findElement("TITLE_DESC").value;
    ary[2] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_SURNAME").value;
    ary[3] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_NAME").value;
    ary[4] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_DOB").value;
    ary[5] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_GENDER").value;
    ary[6] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_EMAIL").value;
    ary[7] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_MOBILE").value;
    ary[8] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMP_PHONE").value;
    ary[9] = customersTableView.get_dataItems()[currentRowIndex].findElement("MARITAL_DATA").value;
    ary[10] = customersTableView.get_dataItems()[currentRowIndex].findElement("QUALIFICATION_NAME").value;
    ary[11] = customersTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    ary[12] = customersTableView.get_dataItems()[currentRowIndex].findElement("SIGNATURE_PASSWORD").value;
    ary[13] = customersTableView.get_dataItems()[currentRowIndex].findElement("EMPLOYEE_SALARY").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EMP_ID;

    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    try {
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployee(ary,m,OnCallBack1);
        CRM.WebApp.webservice.EmployeeWebService.GetEmployee(0, customersTableView.get_pageSize(), customersTableView.get_sortExpressions().toString(), customersTableView.get_filterExpressions().toDynamicLinq(), updateGrid);
        //alert('Record Save Successfully');
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function OnCallBack1(results, userContext, sender) {

    debugger;
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
// Double Click Save kirit User Grid

function addUserDetail(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = 0;
    ary[1] = ordersTableView.get_dataItems()[currentRowIndex].findElement("USER_NAME").value;
    ary[2] = ordersTableView.get_dataItems()[currentRowIndex].findElement("FROM_DATE").value;
    ary[3] = ordersTableView.get_dataItems()[currentRowIndex].findElement("TO_DATE").value;
    ary[4] = ordersTableView.get_dataItems()[currentRowIndex].findElement("PASSWORD").value;
    ary[5] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EMP_ID;

    
    if (ary[1] == "" || ary[1] == 'null') ary[1] = 0;
    if (ary[2] == "" || ary[2] == 'null') ary[2] = 0;
    if (ary[3] == "" || ary[3] == 'null') ary[3] = 0;
    if (ary[4] == "" || ary[4] == 'null') ary[4] = 0;

    try {

        CRM.WebApp.webservice.EmployeeWebService.UpdateUserDetails(ary, s, OnCallBack);
        CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateOrdersGrid);
        
    } catch (e) {

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
// Double Click Save kirit Contect Grid

function addContectDetail(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();


    var ary = [];

    ary[0] = contectTableView.get_dataItems()[currentRowIndex].findElement("COUNTRY_NAME").value;
    ary[1] = contectTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE1").value;
    ary[2] = contectTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_LINE2").value;
    ary[3] = contectTableView.get_dataItems()[currentRowIndex].findElement("STATE_NAME").value;
    ary[4] = contectTableView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[5] = contectTableView.get_dataItems()[currentRowIndex].findElement("ADDRESS_TYPE_NAME").value;
    ary[6] = contectTableView.get_dataItems()[currentRowIndex].findElement("PINCODE").value;
    ary[7] = contectTableView.get_dataItems()[currentRowIndex].findElement("PHONE").value;
    ary[8] = EMP_ID;
    ary[9] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EMP_CONTACT_SRNO;
    if (ary[9] == "" || ary[9] == 'null') ary[9] = 0;
    try {
        
        CRM.WebApp.webservice.EmployeeWebService.InsertUpdateEmployeeContectDetails(ary);
        CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, updateContectGrid);
        alert('Record Save Successfully');
    }
    catch (e) {
        masterTable.rebind();
        alert("Wrong Data Inserted");

    }

}
function addRole(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];


    ary[1] = roleTableView.get_dataItems()[currentRowIndex].findElement("COMPANY_NAME").value;
    ary[2] = roleTableView.get_dataItems()[currentRowIndex].findElement("DEPARTMENT_NAME").value;
    ary[3] = roleTableView.get_dataItems()[currentRowIndex].findElement("ROLE_NAME").value;
    ary[4] = roleTableView.get_dataItems()[currentRowIndex].findElement("REPORTING_TO").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.USER_ID;

    try {
            CRM.WebApp.webservice.EmployeeWebService.InsertUserRoleForEmployee(ary);
            CRM.WebApp.webservice.EmployeeWebService.GetMyRole(EMP_ID, updateMyRoleGrid);
            alert('Record Save Successfully');

     } 
    catch (e) {
        alert('Wrong Data Inserted');
    }
}