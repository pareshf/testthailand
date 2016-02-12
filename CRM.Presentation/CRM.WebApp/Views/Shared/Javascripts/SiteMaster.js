var SIGHT_SEEING_SRNO = "";
var SIGHTSEEING_ID = "";
var siteCommandName = "";
var siteTabelView = null;
var sitePhotoCommandName = "";
var sitePhotoTabelView = null;

function radgridsitemaster_Command(sender, args) {

    args.set_cancel(true);
    pageSize = sender.get_masterTableView().get_pageSize();
    siteTabelView.set_pageSize(pageSize);
    CRM.WebApp.webservice.SiteMasterWebService.GetSiteDetails(siteTabelView.get_currentPageIndex() * siteTabelView.get_pageSize(), siteTabelView.get_pageSize(), siteTabelView.get_sortExpressions().toString(), siteTabelView.get_filterExpressions().toDynamicLinq(), updatesiteGrid);
    siteCommandName = args.get_commandName;
}
function radgridphotomaster_Command(sender, args) {
    
}
function radgridsitemaster_RowSelected(sender, args) {

    try {
        
        SIGHT_SEEING_SRNO = args.get_gridDataItem()._dataItem.SIGHT_SEEING_SRNO;
        CRM.WebApp.webservice.SiteMasterWebService.GetSitePhotoDetails(SIGHT_SEEING_SRNO, updatesitephotoGrid);
       
    }
    catch (e) { } 
}
function radgridphotomaster_RowSelected(sender, args) {

    SIGHTSEEING_ID = args.get_gridDataItem()._dataItem.SIGHTSEEING_ID;
    currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
    sitePhotoCommandName = "Load";
}

function updateVirtualItemCount(result) { siteTabelView.set_virtualItemCount(result); }

function updatePhotoVirtualItemCount(result) {
    sitePhotoTabelView.set_virtualItemCount(result);
}
function updatesiteGrid(result) {
    siteTabelView.set_dataSource(result);
    siteTabelView.dataBind();
    if (result.length > 0) { siteTabelView.selectItem(0); SIGHT_SEEING_SRNO = result[0]["SIGHT_SEEING_SRNO"]; }
    else { SIGHT_SEEING_SRNO = ""; }
    if (siteCommandName == "Filter" || siteCommandName == "Load") { CRM.WebApp.webservice.SiteMasterWebService.GetSiteCount(updateVirtualItemCount); }
}
function loadSite() {
    siteCommandName = "Load";
    CRM.WebApp.webservice.SiteMasterWebservice.GetSiteDetails(SIGHT_SEEING_SRNO, updatesiteGrid);

}
function updatesitephotoGrid(result) {
   
    sitePhotoTabelView.set_dataSource(result);
    sitePhotoTabelView.dataBind();
    if (result.length > 0) { sitePhotoTabelView.selectItem(0); SIGHTSEEING_ID = result[0]["SIGHTSEEING_ID"]; }
    else { SIGHTSEEING_ID = ""; }
    if (siteCommandName == "Filter" || siteCommandName == "Load") { CRM.WebApp.webservice.SiteMasterWebService.GetPhotoCount(updatePhotoVirtualItemCount); }
}
function loadPhotoDetail() {
   
    sitePhotoCommandName = "Load";
    CRM.WebApp.webservice.SiteMasterWebservice.GetSitePhotoDetails(SIGHT_SEEING_SRNO, updatesitephotoGrid);

}

// Double Click Save

function addNewsSiteseeing(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];


    ary[0] = siteTabelView.get_dataItems()[currentRowIndex].findElement("SITE_NAME").value;
    ary[1] = siteTabelView.get_dataItems()[currentRowIndex].findElement("SITE_SEEING_DETAILS").value;
    ary[2] = siteTabelView.get_dataItems()[currentRowIndex].findElement("CITY_NAME").value;
    ary[3] = siteTabelView.get_dataItems()[currentRowIndex].findElement("ENTRY_FEE").value;
    ary[4] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SIGHT_SEEING_SRNO;
    for (i = 0; i < 5; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.SiteMasterWebService.insertupdateSite(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.SiteMasterWebService.GetSiteDetails(0, siteTabelView.get_pageSize(), siteTabelView.get_sortExpressions().toString(), siteTabelView.get_filterExpressions().toDynamicLinq(), updatesiteGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function addNewPhotoDetails(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var a = [];

    a[0] = sitePhotoTabelView.get_dataItems()[currentRowIndex].findElement("PHOTO_TITLE").value;
    a[1] = sitePhotoTabelView.get_dataItems()[currentRowIndex].findElement("PHOTO_DESCRIPATION").value;
    a[2] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SIGHT_SEEING_SRNO;
    a[3] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.SIGHTSEEING_ID;
    for (i = 0; i < 1; i++) {
        if (a[i] == "" || a[i] == 'null') a[i] = 0;
    }
    try {
        CRM.WebApp.webservice.SiteMasterWebService.insertupdatePhoto(a);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.SiteMasterWebService.GetSitePhotoDetails(SIGHT_SEEING_SRNO, updatesitephotoGrid);
    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}