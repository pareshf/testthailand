var PROFESSION_ID = "";
var ProfessionTableView = null;
var ProfessionCommandName = "";

function radgridProfession_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    ProfessionTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ProfessionMasterWebService.GetProfessionName(ProfessionTableView.get_currentPageIndex() * ProfessionTableView.get_pageSize(), ProfessionTableView.get_pageSize(), ProfessionTableView.get_sortExpressions().toString(), ProfessionTableView.get_filterExpressions().toDynamicLinq(), updateProfessionGrid);
    ProfessionCommandName = args.get_commandName;

}
function radgridProfession_RowSelected(sender, args) {

    try {
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        PROFESSION_ID = args.get_gridDataItem()._dataItem.PROFESSION_ID;

    }
    catch (e) { }

}

function updateProfessionVirtualItemCount(result) {

    ProfessionTableView.set_virtualItemCount(result);

}

function updateProfessionGrid(result) {

    ProfessionTableView.set_dataSource(result);
    ProfessionTableView.dataBind();
    if (result.length > 0) { ProfessionTableView.selectItem(0); }

    if (ProfessionCommandName == "Filter" || ProfessionCommandName == "Load") { CRM.WebApp.webservice.ProfessionMasterWebService.ProfessionNameCount(updateProfessionVirtualItemCount); }

}