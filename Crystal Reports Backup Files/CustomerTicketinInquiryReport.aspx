﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerTicketinInquiryReport.aspx.cs" Inherits="CRM.WebApp.Views.Reports.CustomerTicketinInquiryReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
    body
    {
        font-family:Arial;
        font-size:12px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div>
        
            <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderColor="Silver" 
                BorderStyle="Solid" BorderWidth="1px" Height="8.5in" Width="14in">
            </rsweb:ReportViewer>
        
        </div>
    </form>
</body>
</html>
