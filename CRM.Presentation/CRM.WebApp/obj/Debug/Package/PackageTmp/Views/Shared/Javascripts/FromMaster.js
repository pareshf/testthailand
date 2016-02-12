var FORM_ID = "";
var FromTypeCommand = "";
var FromTypeTableView = null;


function radgridFrom_Command(sender, args) {
    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    FromTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.FromMasterWebService.GetFromDetail(FromTypeTableView.get_currentPageIndex() * FromTypeTableView.get_pageSize(), FromTypeTableView.get_pageSize(), FromTypeTableView.get_sortExpressions().toString(), FromTypeTableView.get_filterExpressions().toDynamicLinq(), updatefromdetail);
    FromTypeCommand = args.get_commandName;

}
function radgridFrom_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        FORM_ID = args.get_gridDataItem()._dataItem.FORM_ID;


    }
    catch (e) { }
}
function updatefromdetailVirtualCount(result) { FromTypeTableView.set_virtualItemCount(result); }

function updatefromdetail(result) {

    FromTypeTableView.set_dataSource(result);
    FromTypeTableView.dataBind();
    if (result.length > 0) { FromTypeTableView.selectItem(0); }

    if (FromTypeCommand == "Filter" || FromTypeCommand == "Load") { CRM.WebApp.webservice.FromMasterWebService.FromDetailCount(updatefromdetailVirtualCount); }
}

// Double Click Save Data

function addFrom(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = FromTypeTableView.get_dataItems()[currentRowIndex].findElement("FORM_NAME").value;
    ary[2] = FromTypeTableView.get_dataItems()[currentRowIndex].findElement("FILE_NAME").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.FORM_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.FromMasterWebService.InsertUpdateFromMaster(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.FromMasterWebService.GetFromDetail(0, FromTypeTableView.get_pageSize(), FromTypeTableView.get_sortExpressions().toString(), FromTypeTableView.get_filterExpressions().toDynamicLinq(), updatefromdetail);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}