var EMAIL_CONFIG_ID = "";
var EmailConfigCommand = "";
var EmailConfigTableView = null;


function radgridEmailConfig_Command(sender, args) {
    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    EmailConfigTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.EmailConfigMaster.GetEmailConfig(EmailConfigTableView.get_currentPageIndex() * EmailConfigTableView.get_pageSize(), EmailConfigTableView.get_pageSize(), EmailConfigTableView.get_sortExpressions().toString(), EmailConfigTableView.get_filterExpressions().toDynamicLinq(), updateEmailConfig);
    EmailConfigCommand = args.get_commandName;

}
function radgridEmailConfig_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        EMAIL_CONFIG_ID = args.get_gridDataItem()._dataItem.EMAIL_CONFIG_ID;


    }
    catch (e) { }
}
function updateEmailConfigCount(result) { EmailConfigTableView.set_virtualItemCount(result); }

function updateEmailConfig(result) {

    EmailConfigTableView.set_dataSource(result);
    EmailConfigTableView.dataBind();
    if (result.length > 0) { EmailConfigTableView.selectItem(0); }

    if (EmailConfigCommand == "Filter" || EmailConfigCommand == "Load") { CRM.WebApp.webservice.EmailConfigMaster.GetEmailConfigCount(updateEmailConfigCount); }
}

// Double Click Save Data

function addEmailConfig(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = EmailConfigTableView.get_dataItems()[currentRowIndex].findElement("SMTP_USERID").value;
    ary[2] = EmailConfigTableView.get_dataItems()[currentRowIndex].findElement("SMTP_PASSWORD").value;
    ary[3] = EmailConfigTableView.get_dataItems()[currentRowIndex].findElement("SMTP_HOST").value;
    ary[4] = EmailConfigTableView.get_dataItems()[currentRowIndex].findElement("SMTP_PORT").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.EMAIL_CONFIG_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.EmailConfigMaster.InsertUpdateEmailConfig(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.EmailConfigMaster.GetEmailConfig(0, EmailConfigTableView.get_pageSize(), EmailConfigTableView.get_sortExpressions().toString(), EmailConfigTableView.get_filterExpressions().toDynamicLinq(), updateEmailConfig);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }
}