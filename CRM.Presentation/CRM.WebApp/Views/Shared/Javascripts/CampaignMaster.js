var CAMPAIGN_ID = "";
var CAMPGAIN_ID = "";
var TARGETLIST_ID= "";
var CampaignCommand = "";
var CampaignTableView = null;
var CampaignDetailsTableView=null;
var CampaignDetailsCommand = "";
var mappingTableView = null;
var mappingCommand = "";
var globalvalue="";

function radgridcampaign_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    CampaignTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);
    CampaignCommand = args.get_commandName;

}
function radgridcampaign_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        CAMPAIGN_ID = args.get_gridDataItem()._dataItem.CAMPAIGN_ID;
        loadtargetlist();
        loadmapping();
    }
    catch (e) { }
}
function updateCampaignVirtualCount(result) { CampaignTableView.set_virtualItemCount(result); }

function updateCampaignGrid(result) {

    CampaignTableView.set_dataSource(result);
    CampaignTableView.dataBind();
    if (result.length > 0) { CampaignTableView.selectItem(0); }

    if (CampaignCommand == "Filter" || CampaignCommand == "Load") { CRM.WebApp.webservice.CampaignMasterWebService.GetCompaignCount(updateCampaignVirtualCount); }
}
/*double Row Click Save*/
function CampaignRowClick(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[0] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("CAMPAIGN_NAME").value;
    ary[2] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("CAMPAIGN_CODE").value;
    ary[3] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("START_DATE").value;
    ary[4] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("END_DATE").value;
    ary[5] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("CURRENCY_NAME").value;
    ary[6] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("STATUS_NAME").value;
    ary[7] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("TYPE").value;
    ary[8] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("BUDGET").value;
    ary[9] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("ACTUAL_COST").value;
    ary[10] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("EXPECTED_COST").value;
    ary[11] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("MISC_COST").value;
    ary[12] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("EXPECTED_REVENU").value;
    ary[13] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("OFFER").value;
    ary[14] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("DESCRIPTION").value;
    ary[15] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("EMPLOYEE").value;
    ary[16] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("TRACKER_NAME").value;
    ary[17] = CampaignTableView.get_dataItems()[currentRowIndex].findElement("TRACKER_LINK").value;
    ary[1] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.CAMPAIGN_ID;
    for (i = 0; i < 18; i++) {
        if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
    }
    try {
        CRM.WebApp.webservice.CampaignMasterWebService.InsertUpdateCampaign(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.CampaignMasterWebService.GetCompaign(CampaignTableView.get_currentPageIndex() * CampaignTableView.get_pageSize(), CampaignTableView.get_pageSize(), CampaignTableView.get_sortExpressions().toString(), CampaignTableView.get_filterExpressions().toDynamicLinq(), updateCampaignGrid);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}
function radgridmapping_Command(sender, args) {


}
function radgridmapping_RowSelected(sender, args) {

}
function radgridcampaigndetails_Command(sender, args) {

}
function radgridcampaigndetails_RowSelected(sender, args) {

    TARGETLIST_ID = args.get_gridDataItem()._dataItem.TARGETLIST_ID;

}
function loadtargetlist() {
    CRM.WebApp.webservice.CampaignMasterWebService.GetTargetList(CAMPAIGN_ID, updatecampaigndetail);
}
function loadmapping() {

    CRM.WebApp.webservice.CampaignMasterWebService.GetMapping(CAMPAIGN_ID, updatemappingGrid);
}
function updatecampaignVirtualCount(result) { CampaignDetailsTableView.set_virtualItemCount(result); }

function updatecampaigndetail(result) {

    CampaignDetailsTableView.set_dataSource(result);
    CampaignDetailsTableView.dataBind();
    if (result.length > 0) { CampaignDetailsTableView.selectItem(0); }

    if (CampaignDetailsCommand == "Filter" || CampaignDetailsCommand == "Load") { CRM.WebApp.webservice.CampaignMasterWebService.GetComapaingTargetCount(updatecampaignVirtualCount); }
}
function updatemappingVirtualCount(result) { mappingTableView.set_virtualItemCount(result); }

function updatemappingGrid(result) {

    mappingTableView.set_dataSource(result);
    mappingTableView.dataBind();
    if (result.length > 0) { mappingTableView.selectItem(0); }
    
    if (mappingCommand == "Filter" || mappingCommand == "Load") { CRM.WebApp.webservice.CampaignMasterWebService.GetMappingCount(updatemappingVirtualCount); }
}