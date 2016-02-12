var PROGRAM_ID = "";
var SUB_PROG_ID = "";
var progarmCommandName = "";
var subprogramCommandName = "";
var subprogramTableView = null;
var programTableView = null;
var masterTableView = null;
var programAccessTableView = null;
var subProgramAccessTableView = null;
var masterCommandName = "";
var DEPT_ID = "";
var ROLE_ID = "";
var COMP_ID = "";
var programAccessCommandName = null;

function radgridprogrammaster_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    programTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(programTableView.get_currentPageIndex() * programTableView.get_pageSize(), programTableView.get_pageSize(), programTableView.get_sortExpressions().toString(), programTableView.get_filterExpressions().toDynamicLinq(), updateprogrammasterGrid);
    progarmCommandName = args.get_commandName;

}

function radgridprogrammaster_RowSelected(sender, args) {

    try {
        PROGRAM_ID = args.get_gridDataItem()._dataItem.PROGRAM_ID;
        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        loadsubprogram();
    }
    catch (e) { }
}

function radgridsubprogrammaster_RowSelected(sender, args) {
    try {

        SUB_PROG_ID = args.get_gridDataItem()._dataItem.PROGRAM_SUB_ID;
        
     }
    catch (e) { }
}

function updateVirtualItemCount(result) {

    programTableView.set_virtualItemCount(result);
}

function loadProgram() {

    progarmCommandName = "Load";
    CRM.WebApp.webservice.ProgramMasterWebService.GetProgramDetails(PROGRAM_ID, updateprogrammasterGrid);

}

function updateprogrammasterGrid(result) {
    programTableView.set_dataSource(result);
    programTableView.dataBind();
    if (result.length > 0) { programTableView.selectItem(0); PROGRAM_ID = result[0]["PROGRAM_ID"]; }
    else { PROGRAM_ID = ""; }
    if (progarmCommandName == "Filter" || progarmCommandName == "Load") { CRM.WebApp.webservice.ProgramMasterWebService.GetProgramCount(updateVirtualItemCount); }
}

function radgridMaster_Command(sender, args) {
    //args.set_Cancel(true);
    CRM.WebApp.webservice.ProgramMasterWebService.GetDeptRoleDetails(masterTableView.get_currentPageIndex() * masterTableView.get_pageSize(), masterTableView.get_pageSize(), masterTableView.get_sortExpressions().toString(), masterTableView.get_filterExpressions().toDynamicLinq(), updateMasterGrid);
    masterCommandName = args.get_commandName;
}
function radgridMaster_RowSelected(sender, args) {
    try {
        DEPT_ID = args.get_gridDataItem()._dataItem.DEPT_ID;
        ROLE_ID = args.get_gridDataItem()._dataItem.ROLE_ID;
        COMP_ID = args.get_gridDataItem()._dataItem.COMPANY_ID;
        loadProgramAccessGrid();
        loadSubProgramAccessGrid();
    }
    catch (e) { }
}
function radgridProgramAccess_Command(sender, args) {
     //  args.set_cancel(true);
    // CRM.WebApp.webservice.ProgramMasterWebService.GetProgDetails(programAccessTableView.get_currentPageIndex() * programAccessTableView.get_pageSize(), programAccessTableView.get_pageSize(), programAccessTableView.get_sortExpressions().toString(), programAccessTableView.get_filterExpressions().toDynamicLinq(), updateProgAccessGrid);
    // programAccessCommandName = args.get_commandName;
}
function radgridSubProgramAccess_Command(sender, args) {

}
function radgridsubprogrammaster_Command(sender, args) {

    CRM.WebApp.webservice.ProgramMasterWebService.GetSubProgramDetails(subprogramTableView.get_currentPageIndex() * subprogramTableView.get_pageSize(), subprogramTableView.get_pageSize(), subprogramTableView.get_sortExpressions().toString(), subprogramTableView.get_filterExpressions().toDynamicLinq(), updatesubprogramGrid);
    subprogramCommandName = args.get_commandName;
}
function loadProgramAccessGrid() {

    CRM.WebApp.webservice.ProgramMasterWebService.GetProgAccessDetails(DEPT_ID, ROLE_ID, COMP_ID, updateProgAccessGrid);

}
function loadSubProgramAccessGrid() {
    CRM.WebApp.webservice.ProgramMasterWebService.GetSubProgAccessDetails(DEPT_ID, ROLE_ID,COMP_ID, updateSubProgAccessGrid);
}
function updateMasterGrid(result) {
    masterTableView.set_dataSource(result);
    masterTableView.dataBind();
    if (result.length > 0) { masterTableView.selectItem(0); DEPT_ID = result[0]["DEPT_ID"]; ROLE_ID = result[0]["ROLE_ID"]; }
    else {DEPT_ID = "";ROLE_ID = "";}
    if (masterCommandName == "Filter" || masterCommandName == "Load") { CRM.WebApp.webservice.ProgramMasterWebService.GetCount(updateMasterVirtualCount); }
}
function loadsubprogram() {
    CRM.WebApp.webservice.ProgramMasterWebService.GetSubProgramDetails(PROGRAM_ID, updatesubprogramGrid);
}
function updateProgAccessGrid(result) {
    programAccessTableView.set_dataSource(result);
    programAccessTableView.dataBind();
    if (programAccessCommandName == "Filter" || programAccessCommandName == "Load") { CRM.WebApp.webservice.ProgramMasterWebService.GetProgramAccessCount(updateProgramVirtualCount); }
}
function updateSubProgAccessGrid(result) {
    subProgramAccessTableView.set_dataSource(result);
    subProgramAccessTableView.dataBind();
}
function updateMasterVirtualCount(result) {
    masterTableView.set_virtualItemCount(result);
}
function updateProgramVirtualCount(result) {
    programAccessTableView.set_virtualItemCount(result);
}
function updatesubprogramGrid(result) {
    subprogramTableView.set_dataSource(result);
    subprogramTableView.dataBind();
    if (result.length > 0) { subprogramTableView.selectItem(0);}
}
function radgridprogrammaster_pageSizechanged(source,e) {
    if (e.NewPageSize > 1) {
        pageSize = programTableView.get_pageSize();
        //alert(pageSize);
        programTableView.set_pageSize(pageSize);
    }
}