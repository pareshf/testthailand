var OFFER_ID = "";
var OfferCommandName = "";
var OfferTableView = null;


function radgridSpecialOffer_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    OfferTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(OfferTableView.get_currentPageIndex() * OfferTableView.get_pageSize(), OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);
    OfferCommandName = args.get_commandName;

}
function radgridSpecialOffer_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        OFFER_ID = args.get_gridDataItem()._dataItem.OFFER_ID;


    }
    catch (e) { }
}
function updateOfferVirtualCount(result) { OfferTableView.set_virtualItemCount(result); }

function updateOffer(result) {

    OfferTableView.set_dataSource(result);
    OfferTableView.dataBind();
    if (result.length > 0) { OfferTableView.selectItem(0); }

    if (OfferCommandName == "Filter" || OfferCommandName == "Load") { CRM.WebApp.webservice.SpecialOfferWebService.GetOfferCount(updateOfferVirtualCount); }
}

// Double Click Save
function addSpecialOffer(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = OfferTableView.get_dataItems()[currentRowIndex].findElement("PACKAGE_NAME").value;
    ary[2] = OfferTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY").value;
    ary[3] = OfferTableView.get_dataItems()[currentRowIndex].findElement("PRICE").value;
    ary[4] = OfferTableView.get_dataItems()[currentRowIndex].findElement("DISPLAY_ON_DASHBOARD").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.OFFER_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.SpecialOfferWebService.InsertUpdateSpecialOffer(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.SpecialOfferWebService.GetOffer(0, OfferTableView.get_pageSize(), OfferTableView.get_sortExpressions().toString(), OfferTableView.get_filterExpressions().toDynamicLinq(), updateOffer);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}