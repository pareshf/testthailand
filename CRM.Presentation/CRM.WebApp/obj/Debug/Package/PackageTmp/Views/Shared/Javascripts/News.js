var NEWS_ID = "";
var NewsCommandName = "";
var NewsTableView = null;


function radgridNews_Command(sender, args) {

    args.set_cancel(true);
    var pageSize = sender.get_masterTableView().get_pageSize();
    NewsTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.NewsWebService.GetNews(NewsTableView.get_currentPageIndex() * NewsTableView.get_pageSize(), NewsTableView.get_pageSize(), NewsTableView.get_sortExpressions().toString(), NewsTableView.get_filterExpressions().toDynamicLinq(), updateNews);
    departmentCommandName = args.get_commandName;

}
function radgridNews_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        NEWS_ID = args.get_gridDataItem()._dataItem.NEWS_ID;


    }
    catch (e) { }
}
function updateNewsVirtualCount(result) { NewsTableView.set_virtualItemCount(result); }

function updateNews(result) {

    NewsTableView.set_dataSource(result);
    NewsTableView.dataBind();
    if (result.length > 0) { NewsTableView.selectItem(0); }

    if (NewsCommandName == "Filter" || NewsCommandName == "Load") { CRM.WebApp.webservice.NewsWebService.GetNewsCount(updateNewsVirtualCount); }
}


// Double Click Save
function addNewsMaster(sender, eventArgs) {

    currentRowIndex = eventArgs.get_itemIndexHierarchical();

    var ary = [];

    ary[1] = NewsTableView.get_dataItems()[currentRowIndex].findElement("NEWS_HEADER").value;
    ary[2] = NewsTableView.get_dataItems()[currentRowIndex].findElement("NEWS_DESCRIPTION").value;
    ary[3] = NewsTableView.get_dataItems()[currentRowIndex].findElement("DISPLAY_ON_DASHBOARD").value;

    ary[0] = eventArgs._tableView._element.parentNode.children[0].control._dataItems[currentRowIndex]._dataItem.NEWS_ID;

    if (ary[0] == "" || ary[0] == 'null') ary[0] = 0;

    try {
        CRM.WebApp.webservice.NewsWebService.InsertUpdateNews(ary);
        alert('Record Save Successfully');
        CRM.WebApp.webservice.NewsWebService.GetNews(0, NewsTableView.get_pageSize(), NewsTableView.get_sortExpressions().toString(), NewsTableView.get_filterExpressions().toDynamicLinq(), updateNews);

    }
    catch (e) {
        alert('Wrong Data Inserted');
    }

}
