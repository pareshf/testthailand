<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewExcel.aspx.cs" Inherits="CRM.WebApp.Views.EXCELS.ViewExcel" %>

<%@ Register assembly="Aceoffix, Version=3.0.0.1, Culture=neutral, PublicKeyToken=e6a26169e940f541" namespace="Aceoffix" tagprefix="ace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script language="javascript" type="text/javascript">
    function OnCustomMenuClick(iIndex, sCaption) {
        var AceoffixCtrl1 = document.getElementById("AceoffixCtrl1");
        if (iIndex == 0) alert("The caption of titlebar is: " + AceoffixCtrl1.Caption);
        if (iIndex == 2) SaveAsHTML();
    }
    function SaveDocument() {
        document.getElementById("AceoffixCtrl1").SaveDocument();
    }
    function SaveAsHTML() {
        document.getElementById("AceoffixCtrl1").SaveDocumentAsHTML();
        window.location = "viewhtml.aspx?mht=../doc/editexcel.mht";
    }
    function ShowPageSetupDlg() {
        document.getElementById("AceoffixCtrl1").ShowDialog(5); //Page setup dialog box
    }
    function ShowPrintDlg() {
        document.getElementById("AceoffixCtrl1").ShowDialog(4); //Print dialog box
    }
    function SwitchFullScreen() {
        document.getElementById("AceoffixCtrl1").FullScreen = !document.getElementById("AceoffixCtrl1").FullScreen;
    }
</script>
    <form id="form1" runat="server">
  
    <div style="width:auto; height:600px;">
      <ace:AceoffixCtrl ID="AceoffixCtrl1" runat="server" Menubar="False" 
          OfficeToolbars="False" onload="AceoffixCtrl1_Load" 
          Theme="Office2010" BorderStyle="BorderThin">
      </ace:AceoffixCtrl>
    </div>
    </form>
</body>
</html>
