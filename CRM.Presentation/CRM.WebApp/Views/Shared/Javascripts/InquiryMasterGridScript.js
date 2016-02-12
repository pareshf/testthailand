var inquirymastercommandname = "";
var inquirymasterTableView = null;
var CUST_ID = "";


function radgridinquirymaster_Command(sender, args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    inquirymasterTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.inquiryMasterWebService.GetInq(inquirymasterTableView.get_currentPageIndex() * inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_pageSize(), inquirymasterTableView.get_sortExpressions().toString(), inquirymasterTableView.get_filterExpressions().toDynamicLinq(), updatetinquiry);
    inquirymastercommandname = args.get_commandName;
}
function radgridinquirymaster_RowSelected(sender, args) {
    try {

    }
    catch (e) { }
}

function updatetinquiry(result) {

    inquirymasterTableView.set_dataSource(result);
    inquirymasterTableView.dataBind();
    if (result.length > 0) { inquirymasterTableView.selectItem(0); }

    if (inquirymastercommandname == "Filter" || inquirymastercommandname == "Load") { CRM.WebApp.webservice.InquiryMasterWebService.InqMasterCount(updateInqVirtualItemCount); }
}

function updateInqVirtualItemCount(result) {
    inquirymasterTableView.set_virtualItemCount(result);
}