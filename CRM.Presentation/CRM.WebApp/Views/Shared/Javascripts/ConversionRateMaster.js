var CONVERSION_RATE_ID = "";
var ConversionCommandName = "";
var ConversionTableView = null;
var s = "";


function radgridconvertionratemaster_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    ConversionTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ConversionRateMaster.GetConvertionRate(ConversionTableView.get_currentPageIndex() * ConversionTableView.get_pageSize(), ConversionTableView.get_pageSize(), ConversionTableView.get_sortExpressions().toString(), ConversionTableView.get_filterExpressions().toDynamicLinq(), updateConversionRateName);
    ConversionCommandName = args.get_commandName;

}
function radgridconvertionratemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CONVERSION_RATE_ID = args.get_gridDataItem()._dataItem.CONVERSION_RATE_ID;


    }
    catch (e) { }
}
function updateConversionRateVirtualCount(result) { ConversionTableView.set_virtualItemCount(result); }

function updateConversionRateName(result) {

    ConversionTableView.set_dataSource(result);
    ConversionTableView.dataBind();
    if (result.length > 0) { ConversionTableView.selectItem(0); }

    if (ConversionCommandName == "Filter" || ConversionCommandName == "Load") { CRM.WebApp.webservice.ConversionRateMaster.GetConversionRateCount(updateConversionRateVirtualCount); }
}

// Double Click Save Data

function addMyConversionRate(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = ConversionTableView.get_dataItems()[currentRowIndex].findElement("FROM_CURRENCY").value;
    ary[2] = ConversionTableView.get_dataItems()[currentRowIndex].findElement("TO_CURRENCY").value;
    ary[3] = ConversionTableView.get_dataItems()[currentRowIndex].findElement("CONVERSION_RATE").value;
    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CONVERSION_RATE_ID;
    for (i = 0; i < 4; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.ConversionRateMaster.InsertUpdateConversionMaster(ary,s,OnCallBack);
        CRM.WebApp.webservice.ConversionRateMaster.GetConvertionRate(0, ConversionTableView.get_pageSize(), ConversionTableView.get_sortExpressions().toString(), ConversionTableView.get_filterExpressions().toDynamicLinq(), updateConversionRateName);

        //alert('Record Save Successfully');

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function OnCallBack(results, userContext, sender) {

    if (results == "Error") {

        alert('This Currency Conversion Already Exist.');
    }
    else if (results == "") {
        alert('Record Save Successfully');
    }
    else {

        alert('Record Save Successfully');
    }


}