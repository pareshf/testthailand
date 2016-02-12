var commandName = "";
var tableView = null;
var ordersView = null;
var ordersCommandName = "";

function radgridmaster_Command(sender, args) {
    args.set_cancel(true);

    commandName = args.get_commandName();

    CRM.WebApp.webservice.EmployeeWebService.GetEmployee(tableView.get_currentPageIndex() * tableView.get_pageSize(), tableView.get_pageSize(),
                    tableView.get_sortExpressions().toString(), tableView.get_filterExpressions().toDynamicLinq(),
                        updateGrid);
}

function updateGrid(result) {
    tableView.set_dataSource(result);
    tableView.dataBind();

    var dataItems = tableView.get_dataItems();

    if (commandName == "Filter" || commandName == "Load") {
        CRM.WebApp.webservice.EmployeeWebService.GetCustomersCount(updateVirtualItemCount);
    }
}

function updateVirtualItemCount(result) {
    tableView.set_virtualItemCount(result);
}


function radgridmaster_HierarchyExpanding(sender, args) {
    var EMP_ID = args.getDataKeyValue("EMP_ID");
    var nestedViewItem = args.get_nestedViewItem();

    ordersView = $telerik.findControl(nestedViewItem, "radgriddetails").get_masterTableView();

    ordersCommandName = "Load";

    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, ordersView.get_currentPageIndex() * ordersView.get_pageSize(), ordersView.get_pageSize(),
                    ordersView.get_sortExpressions().toString(), ordersView.get_filterExpressions().toDynamicLinq(),
                        updateOrdersGrid);
}

function updateOrdersGrid(result) {
    ordersView.set_dataSource(result);
    ordersView.dataBind();

    if (ordersCommandName == "Filter" || ordersCommandName == "Load") {
        CRM.WebApp.webservice.EmployeeWebService.GetOrdersByEMP_IDCount(updateOrdersVirtualItemCount);
    }
}

function updateOrdersVirtualItemCount(result) {
    ordersView.set_virtualItemCount(result);
}

function radgriddetails_Command(sender, args) {
    ordersView = sender.get_masterTableView();
    var nestedViewItem = sender.get_element().parentNode.parentNode;
    var parentGridDataItem = $find(Telerik.Web.UI.Grid.GetNodePreviousSiblingByTagName(nestedViewItem, "tr").id);
    var EMP_ID = parentGridDataItem.getDataKeyValue("EMP_ID");

    args.set_cancel(true);

    ordersCommandName = args.get_commandName();

    CRM.WebApp.webservice.EmployeeWebService.GetDetailsByEMP_ID(EMP_ID, ordersView.get_currentPageIndex() * ordersView.get_pageSize(), ordersView.get_pageSize(),
                    ordersView.get_sortExpressions().toString(), ordersView.get_filterExpressions().toDynamicLinq(),
                        updateOrdersGrid);
}