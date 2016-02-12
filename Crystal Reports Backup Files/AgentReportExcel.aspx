<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentReportExcel.aspx.cs" Inherits="CRM.WebApp.Views.MIS.AgentReportExcel" %>

<%@ Register assembly="Aceoffix, Version=3.0.0.1, Culture=neutral, PublicKeyToken=e6a26169e940f541" namespace="Aceoffix" tagprefix="ace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<script language="javascript" type="text/javascript">
//    function OnCustomMenuClick(iIndex, sCaption) {
//        var AceoffixCtrl1 = document.getElementById("AceoffixCtrl1");
//        if (iIndex == 0) alert("The caption of titlebar is: " + AceoffixCtrl1.Caption);
//        if (iIndex == 2) SaveAsHTML();
//    }
    function SaveAsDocument() {
        document.getElementById("AceoffixCtrl1").ShowDialog(3); //Save file to local computer
    }
//    function SaveAsHTML() {
//        document.getElementById("AceoffixCtrl1").SaveDocumentAsHTML();
//        window.location = "viewhtml.aspx?mht=../doc/editexcel.mht";
//    }
    function ShowPageSetupDlg() {
        document.getElementById("AceoffixCtrl1").ShowDialog(5); //Page setup dialog box
    }
    function ShowPrintDlg() {
        document.getElementById("AceoffixCtrl1").ShowDialog(4); //Print dialog box
    }
    function SwitchFullScreen() {
        document.getElementById("AceoffixCtrl1").FullScreen = !document.getElementById("AceoffixCtrl1").FullScreen;
    }

    function OnAfterDocumentOpened() {

        var sheet2 = document.getElementById("AceoffixCtrl1").DocumentObject.Sheets(2);

        sheet2.PivotTables(1).RefreshTable();

    }

    
</script>
    <form id="form1" runat="server">
     <div style="width:auto; height:600px;">
      <ace:AceoffixCtrl ID="AceoffixCtrl1" runat="server" Menubar="False" 
         onload="AceoffixCtrl1_Load" 
          Theme="Office2010" BorderStyle="BorderThin">
      </ace:AceoffixCtrl>
  </div>

 <%-- <div>
  <asp:Label ID="lbl" runat="server" Visible="false">
  </asp:Label>

  <asp:Label ID="lbl2" runat="server" Visible="false"> </asp:Label>
  </div>--%>
    </form>
</body>
</html>
