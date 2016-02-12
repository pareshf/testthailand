var INQUIRY_ID = "0";
var PrerequisiteCommandName = "";
var PrerequisiteTabelView = null;

function radgridprerequisitengmaster_Command(sender, args) {

  
    //args.set_Cancel(true);
    CRM.WebApp.webservice.BookingInformationWebService.GetPrerequisiteDetails(0, PrerequisiteTabelView.get_currentpageIndex() * PrerequisiteTabelView.get_pageSize(), PrerequisiteTabelView.get_sortExpressions().toString(), PrerequisiteTabelView.get_filterExpressions().toDynamicLinq(),value,updatetaskGrid);
    PrerequisiteCommandName = args.get_commandName;
}

function radgridprerequisitengmaster_RowSelected(sender, args) {

    try {
        INQUIRY_ID = args.get_gridDataItem()._dataItem.INQUIRY_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    }
    catch (e) { }
}

function updateVirtualItemCount(result) { PrerequisiteTabelView.set_virtualItemCount(result); }

function updateprerequisiteGrid(result) {
    PrerequisiteTabelView.set_dataSource(result);
    PrerequisiteTabelView.dataBind();
}
function loadPrereq() {
    PrerequisiteCommandName = "Load";
    CRM.WebApp.webservice.BookingInformationWebService.GetPrerequisiteDetails(INQUIRY_ID, updateprerequisiteGrid);
}