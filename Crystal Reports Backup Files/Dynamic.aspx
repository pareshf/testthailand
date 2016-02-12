<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dynamic.aspx.cs" Inherits="CRM.WebApp.Views.MIS.Dynamic" %>
<%@ Register assembly="Aceoffix, Version=3.0.0.1, Culture=neutral, PublicKeyToken=e6a26169e940f541" namespace="Aceoffix" tagprefix="ace" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
