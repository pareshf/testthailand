var ADDRESS_TYPE_ID = "";
var AddressTypeCommand = "";
var AddressTypeTableView = null;


function radgridaddresstypemaster_Command(sender,args) {

    pageSize = sender.get_masterTableView().get_pageSize();
    AddressTypeTableView.set_pageSize(pageSize);
    CRM.WebApp.webservice.AddressTypeMasterWebService.GetAddressType(AddressTypeTableView.get_currentPageIndex() * AddressTypeTableView.get_pageSize(), AddressTypeTableView.get_pageSize(), AddressTypeTableView.get_sortExpressions().toString(), AddressTypeTableView.get_filterExpressions().toDynamicLinq(), updateAddressTypeName);
    AddressTypeCommand = args.get_commandName;

}
function radgridaddresstypemaster_RowSelected(sender, args) {

    try {

        currentRowIndex = args.get_gridDataItem().get_element().rowIndex;
        ADDRESS_TYPE_ID = args.get_gridDataItem()._dataItem.ADDRESS_TYPE_ID;
        
       
    }
    catch (e) { }
}
function updateAddresstypeVirtualCount(result) { AddressTypeTableView.set_virtualItemCount(result); }

function updateAddressTypeName(result) {
   
    AddressTypeTableView.set_dataSource(result);
    AddressTypeTableView.dataBind();
    if (result.length > 0) { AddressTypeTableView.selectItem(0);}

    if (AddressTypeCommand == "Filter" || AddressTypeCommand == "Load") {CRM.WebApp.webservice.AddressTypeMasterWebService.GetAddressTypeCount(updateAddresstypeVirtualCount); }
}

