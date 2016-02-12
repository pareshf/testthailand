<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SitePhoto.aspx.cs" Inherits="CRM.WebApp.Views.Sales.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
    <%--<asp:ScriptManager ID="ScriptManager" runat="server">
     <Services>
     <asp:ServiceReference Path="~/webservice/SiteMasterWebservice.asmx"/>
     </Services>
    </asp:ScriptManager>--%>
        
        <%--<table style="font-size: 12px; font-family: Verdana;">
        <tr>
           <td>
                    
                    Site Photo :<br />
           </td>
        </tr> 
        <tr>
             <td>
                    <asp:FileUpload ID="sitephoto" runat="server" />
             </td>
             <td>
                    <a runat="server" href="" id="photosite" target="_blank">View</a>
                    
                </td>
        </tr>
              <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"/>
                </td>
            </tr>
        </table>--%>

        <table>
            <tr>
                <td>
                    Site Photo :<br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="sitephoto" runat="server" />
                </td>
           
                <td>
                    <a runat="server" href="" id="photosite" target="_blank">View</a>
                </td>
             </tr>
            
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
