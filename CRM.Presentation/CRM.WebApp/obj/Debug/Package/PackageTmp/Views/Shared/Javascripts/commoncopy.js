/***************************************************************************************
Function Name     : displayCalender
Description       : This function display rad telerik calander above podel popup panel.
Input Parameters  : sender, args.
Output Parameters : true/false.
Author            : Nitesh D. Parmar
Date              : Jan/21/2010
***************************************************************************************/
function displayCalender(sender, args) {
    //Set Z-Index of calender control.
    Telerik.Web.UI.Calendar.Popup.zIndex = 100100;
}


/***************************************************************************************************************
Function Name     : HeaderCheckBox_Click
Description       : This function check and uncheck all checkbox of rad gridview by 
Header checkbox click event.
Input Parameters  : gridControlId, cellIndex, checkBoxClentId.
Output Parameters : void.
Author            : Nitesh
Date              : MAr/3/2010
***************************************************************************************************************/
function HeaderCheckBox_Click(gridControlId, cellIndex, checkBoxClentId) {
    var radGrid = $find(gridControlId);
    var objMasterTable = radGrid.get_masterTableView()
    var objGrid = radGrid.get_masterTableView().get_element().tBodies[0];
    var objCheckBox = document.getElementById(checkBoxClentId);
    var cell;
    if (objGrid.rows.length > 0) {
        //loop starts from 0. rows[0] points to the header.
        for (i = 0; i < objGrid.rows.length; i++) {
            //get the reference of first column                    
            cell = objGrid.rows[i].cells[cellIndex];
            if (!cell.firstChild.firstChild.disabled) {
                if (cell.firstChild.firstChild.type == "checkbox") {
                    //assign the status of the Select All checkbox to the cell checkbox within the grid
                    cell.firstChild.firstChild.checked = objCheckBox.checked;
                    if (objCheckBox.checked) {
                        objMasterTable.selectItem(i);
                    }
                    else {
                        objMasterTable.deselectItem(i);
                    }
                }
            }
        }
    }
}

/***************************************************************************************************************
Function Name     : RowCheckBox_Click
Description       : This function select rows of grid by perticular row's checkbox click event.
Input Parameters  : gridControlId, cellIndex, checkBoxClentId.
Output Parameters : void.
Author            : Nitesh
Date              : MAr/3/2010
***************************************************************************************************************/
function RowCheckBox_Click(gridControlId, chkBox, rowIndex) {
    var grid = $find(gridControlId);
    var masterTable = grid.get_masterTableView();

    if (chkBox.checked) {
        masterTable.selectItem(rowIndex);
    }
    else {
        masterTable.deselectItem(rowIndex);
    }
}

/***************************************************************************************************************
Function Name     : ValidateDelete
Description       : This function validate rows selection for delete event of delete button.
Input Parameters  : gridControlId.
Output Parameters : false.
Author            : Nitesh
Date              : Mar/3/2010
***************************************************************************************************************/
function ValidateDelete(gridControlId) {
    var radGrid = $find(gridControlId)
    //    var row = RadGrid.get_masterTableView().get_selectedItems().length;
    //    if (row == 0) {
    //        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
    //        return false;
    //    }
    //    else 
    //    {
    //        blockConfirm(deleteAlertMessage, event, 300, 110, null, 'Delete Confirmation');
    //        
    //    }


    var grid = radGrid.get_masterTableView().get_element().tBodies[0];
    var isChecked = 0;
    if (grid.rows.length > 0) {
        //loop starts from 0. rows[0] points to the header.
        for (i = 0; i < grid.rows.length; i++) {
            //get the reference of first column
            cell1 = grid.rows[i].cells[0];
            if (cell1.firstChild.firstChild.type == "checkbox") {
                if (cell1.firstChild.firstChild.checked) {
                    isChecked++;
                }
            }
        }
    }

    if (isChecked == 0) {
        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
        return false;
    }
    else {
        blockConfirm(deleteAlertMessage, event, 300, 110, null, 'Delete Confirmation');

    }


}

/***************************************************************************************************************
Function Name     : ValidateEdit
Description       : This function validate rows selection for edit event of edit button.
Input Parameters  : gridControlId.
Output Parameters : false.
Author            : Nitesh
Date              : Mar/3/2010
***************************************************************************************************************/
function ValidateEdit(gridControlId) {
    var radGrid = $find(gridControlId);
    //    var row = radGrid.get_masterTableView().get_selectedItems().length;
    //    if (row == 0) {
    //        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
    //        return false;
    //    }   
    var grid = radGrid.get_masterTableView().get_element().tBodies[0];
    var isChecked = 0;
    if (grid.rows.length > 0) {
        //loop starts from 0. rows[0] points to the header.
        for (i = 0; i < grid.rows.length; i++) {
            //get the reference of first column
            cell1 = grid.rows[i].cells[0];
            if (cell1.firstChild.firstChild.type == "checkbox") {
                if (cell1.firstChild.firstChild.checked) {
                    isChecked++;
                }
            }
        }
    }

    if (isChecked == 0) {
        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
        return false;
    }
}

/***************************************************************************************************************
Function Name     : ValidateEdit
Description       : This function validate rows selection for edit event of edit button.
Input Parameters  : gridControlId.
Output Parameters : false.
Author            : Nitesh
Date              : Mar/3/2010
***************************************************************************************************************/
function ValidateSelectionOnlyOne(gridControlId) {
    var radGrid = $find(gridControlId);
    //    var row = radGrid.get_masterTableView().get_selectedItems().length;
    //    if (row == 0) 
    //    {
    //        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
    //        return false;
    //    } 
    //    else if (row > 1) 
    //    {
    //        radalert(selectOnlyOneRecord, 330, 110, 'Warning Message');
    //        return false;
    //    } 

    var grid = radGrid.get_masterTableView().get_element().tBodies[0];
    var isChecked = 0;
    if (grid.rows.length > 0) {
        //loop starts from 0. rows[0] points to the header.
        for (i = 0; i < grid.rows.length; i++) {
            //get the reference of first column
            cell1 = grid.rows[i].cells[0];
            if (cell1.firstChild.firstChild.type == "checkbox") {
                if (cell1.firstChild.firstChild.checked) {
                    isChecked++;
                }
            }
        }
    }

    if (isChecked == 0) {
        radalert(selectionAlertMessage, 330, 110, 'Warning Message');
        return false;
    }
    else if (isChecked > 1) {
        radalert(selectOnlyOneRecord, 330, 110, 'Warning Message');
        return false;
    }

}

/*************************************************************************************
Function Name     : validNumber
Description       : This function allow user to enter only interger value.
Input Parameters  : void.
Output Parameters : true/false.
Author            : Nitesh D. Parmar
Date              : Jan/21/2010
*************************************************************************************/
function ValidNumber(event) {
    //if Keycode not between 0 to 9 return false
    var keyCode = (event.which) ? event.which : (window.event) ? window.event.keyCode : -1;
    if (keyCode >= 48 && keyCode <= 57) {
        return true;
    }
    if (keyCode == 8 || keyCode == -1) {
        return true;
    }
    else {
        return false;
    }
}

function ValidPhneNumber(event) {
    //if Keycode not between 0 to 9 return false
    var keyCode = (event.which) ? event.which : (window.event) ? window.event.keyCode : -1;
    if (keyCode >= 48 && keyCode <= 57 || keyCode == 45) {
        return true;
    }
    if (keyCode == 8 || keyCode == -1) {
        return true;
    }
    else {
        return false;
    }
}

function ValidDecimal(event) {
    //if Keycode not between 0 to 9 return false
    var keyCode = (event.which) ? event.which : (window.event) ? window.event.keyCode : -1;
    if (keyCode >= 48 && keyCode <= 57 || keyCode == 46) {
        return true;
    }
    if (keyCode == 8 || keyCode == -1) {
        return true;
    }
    else {
        return false;
    }
}


function OnRowClick(sender, eventargs) {
    var MasterTable = sender.get_masterTableView();
    MasterTable.deselectItem(eventargs.get_itemIndexHierarchical());
}