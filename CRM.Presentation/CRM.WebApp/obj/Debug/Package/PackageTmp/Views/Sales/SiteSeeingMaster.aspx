<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SiteSeeingMaster.aspx.cs" Inherits="CRM.WebApp.Views.Sales.SiteSeeingMaster" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="cntIncludes" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="cntPageContent" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
        .disable
        {
            display: none;
            width: 0px;
            height: 0px;
            border: 0px solid #fff;
        }
        div.RadGrid_Default .rgFilterRow td
        {
            background-color: #e5e5e5;
        }
        div.RadGrid_Default .rgHeader
        {
            background-color: #F3F3F3;
            background-position: 0 0;
            background-repeat: repeat-x !important;
            border-color: #E6E6E6 #E6E6E6 #CCCCCC;
            color: #636363;
            font-family: Arial;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            height: 25px;
            line-height: 16px;
            text-align: left;
            text-decoration: none;
            text-indent: 0;
        }
        
        .RadMenu_Default
        {
            background-color: #fff;
            border: solid 0px #fff;
        }
        .RadMenu_Default UL.rmRootGroup
        {
            background-color: #fff;
            border: solid 0px #fff;
            padding: 2px;
        }
        .RadMenu rmLink
        {
            padding-left: 0px;
        }
        .RadMenu_Default .rmLink
        {
            color: #000;
            text-decoration: none;
            font-family: Verdana;
            font-size: 8pt;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText
        {
            border: solid 0px #fff;
            padding-top: 2px;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink .rmText:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem:hover
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmVertical .rmItem .rmLink:active
        {
            border: solid 0px #fff;
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink
        {
            color: #000;
            padding-top: 2px;
        }
        .RadMenu_Default .rmGroup .rmItem .rmLink:hover
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadPanelBar_Default .rpSlide
        {
            padding-left: 2px;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .RadMenu_Default .rmLink:active
        {
            background-color: #a4a2a3;
            color: #fff;
        }
        .radinput
        {
            width: 100%;
            border: 0px solid #c2c2c2;
        }
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
            <script type="text/javascript" src="../Shared/Javascripts/SiteMaster.js"></script>
        <script type="text/javascript">
            function pageLoad() {

                siteTabelView = $find("<%= radgridsitemaster.ClientID %>").get_masterTableView();
                sitePhotoTabelView = $find("<%= radgridphotomaster.ClientID %>").get_masterTableView();
                siteCommandName = "Load";
                CRM.WebApp.webservice.SiteMasterWebService.GetSiteDetails(siteTabelView.get_currentPageIndex() * siteTabelView.get_pageSize(), siteTabelView.get_pageSize(), siteTabelView.get_sortExpressions().toString(), siteTabelView.get_filterExpressions().toDynamicLinq(), updatesiteGrid);

            }
            function deleteCurrent() {
                var table = $find("<%= radgridsitemaster.ClientID %>").get_masterTableView().get_element();
                var row = table.rows[currentRowIndex]; table.deleteRow(currentRowIndex);
                var dataItem = $find(row.id);
                if (dataItem) {
                    dataItem.dispose();
                    Array.remove($find("<%= radgridsitemaster.ClientID %>").get_masterTableView()._dataItems, dataItem); 
                 }
                var gridItems = $find("<%= radgridsitemaster.ClientID %>").get_masterTableView().get_dataItems();
                CRM.WebApp.webservice.SiteMasterWebService.DeleteMySite(SIGHT_SEEING_SRNO); 
                gridItems[gridItems.length - 1].set_selected(true);
            }
            function deleteCurrentSitePhoto() {

                CRM.WebApp.webservice.SiteMasterWebService.DeleteSitePhoto(SIGHTSEEING_ID);
                CRM.WebApp.webservice.SiteMasterWebService.GetSitePhotoDetails(SIGHT_SEEING_SRNO, updatesitephotoGrid);

            }
            function PopUpShowing(sender, args) {
                var divmore = document.getElementById('divmore');
                divmore.style.display = 'block';
                divmore.style.position = 'Absolute';
                divmore.style.left = screen.width / 2 - 150;
                divmore.style.top = screen.height / 2 - 150;
                var IMG = document.getElementById("imgexistingimage");
                IMG.src = args.srcElement.all[1].value;
            }
            function disablepopup() {
                var divmore = document.getElementById('divmore');
                divmore.style.display = 'none';
                return false;
            }
            function PopUpShowing(sender, args) { var divmore = document.getElementById('divmore'); divmore.style.display = 'block'; divmore.style.position = 'Absolute'; divmore.style.left = screen.width / 2 - 150; divmore.style.top = screen.height / 2 - 150; var IMG = document.getElementById("imgexistingimage"); IMG.src = args.srcElement.all[1].value; }
            function disablepopup() { var divmore = document.getElementById('divmore'); divmore.style.display = 'none'; return false; }
            function openuploadnewphoto() {
                window.open('SitePhoto.aspx?key=' + SIGHTSEEING_ID + '&key1=' + SIGHT_SEEING_SRNO);
            }
            function AddNewSitePhoto() {
                CRM.WebApp.webservice.SiteMasterWebService.insertnewPhoto(SIGHT_SEEING_SRNO);
            }
            function addnewsitedetail(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var ary = [];

               
                ary[0] = siteTabelView.get_dataItems()[currentRowIndex - 1].findElement("SITE_NAME").value;
                ary[1] = siteTabelView.get_dataItems()[currentRowIndex - 1].findElement("SITE_SEEING_DETAILS").value;
                ary[2] = siteTabelView.get_dataItems()[currentRowIndex - 1].findElement("CITY_NAME").value;
                ary[3] = siteTabelView.get_dataItems()[currentRowIndex - 1].findElement("ENTRY_FEE").value;
                ary[4] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[1].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SIGHT_SEEING_SRNO;
                for (i = 0; i < 5; i++) {
                    if (ary[i] == "" || ary[i] == 'null') ary[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.SiteMasterWebService.insertupdateSite(ary);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.SiteMasterWebService.GetSiteDetails(siteTabelView.get_currentPageIndex() * siteTabelView.get_pageSize(), siteTabelView.get_pageSize(), siteTabelView.get_sortExpressions().toString(), siteTabelView.get_filterExpressions().toDynamicLinq(), updatesiteGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }

            }
            function newsitePhotoadded(sender, args) {

                currentRowIndex = sender.parentNode.parentNode.rowIndex;
                var a = [];
                
                a[0] = sitePhotoTabelView.get_dataItems()[currentRowIndex - 1].findElement("PHOTO_TITLE").value;
                a[1] = sitePhotoTabelView.get_dataItems()[currentRowIndex - 1].findElement("PHOTO_DESCRIPATION").value;
                a[2] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SIGHT_SEEING_SRNO;
                a[3] = sender.parentNode.parentNode.parentNode.parentNode.parentNode.children[0].control._dataItems[sender.parentNode.parentNode.rowIndex - 1]._dataItem.SIGHTSEEING_ID;
                for (i = 0; i < 1; i++) {
                    if (a[i] == "" || a[i] == 'null') a[i] = 0;
                }
                try {
                    CRM.WebApp.webservice.SiteMasterWebService.insertupdatePhoto(a);
                    alert('Record Save Successfully');
                    CRM.WebApp.webservice.SiteMasterWebService.GetSitePhotoDetails(SIGHT_SEEING_SRNO, updatesitephotoGrid);
                }
                catch (e) {
                    alert('Wrong Data Inserted');
                }
            }
        </script>

    </telerik:radcodeblock> 
    <br />
    <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPageSite" runat="server" Text="Site Seeing Master"></asp:Literal>
    </div>
    <br />
    <br />
    <script src="../Shared/Javascripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../webservice/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../../webservice/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var city = "../../webservice/autocomplete.ashx?key=FETCH_CITY_FOR_EMPLOYEE_MASTER";
            
            for (i = 0; i < 55; i++) {
                if (i < 10)
                    i = '0' + i;

                  $("#ctl00_cphPageContent_radgridsitemaster_ctl00_ctl" + i + "_CITY_NAME").autocomplete(city);
            }

        });       </script>
       
     <div id = "radmastergrid">
        <table>
            <tr>
                <td>
                        <asp:Button ID="Delete" CssClass="button" Style="float: right; margin-right: 10px;
                            color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this Site?'))return false; deleteCurrent(); return false;"
                            Text="Delete" runat="server" />
                    </td>
            </tr>
        </table> 
        <table>
            <tr>
                <td>
                    <telerik:radgrid id="radgridsitemaster" runat="server" allowpaging="true" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SIGHT_SEEING_SRNO" AllowMultiColumSorting="true" EditMode ="InPlace" Width="500px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SIGHT_SEEING_SRNO" DataField="SIGHT_SEEING_SRNO" HeaderText="SIGHT_SEEING_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHT_SEEING_SRNO" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SITE_NAME" DataField="SITE_NAME" HeaderText="SITE NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="SITE_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SITE_SEEING_DETAILS" DataField="SITE_SEEING_DETAILS" HeaderText="SITE DETAILS">
                          <ItemTemplate>
                            <asp:TextBox ID="SITE_SEEING_DETAILS" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="CITY_NAME" DataField="CITY_NAME" HeaderText="CITY NAME">
                          <ItemTemplate>
                            <asp:TextBox ID="CITY_NAME" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                     <telerik:GridTemplateColumn SortExpression ="ENTRY_FEE" DataField="ENTRY_FEE" HeaderText="ENTRY_FEE">
                          <ItemTemplate>
                            <asp:TextBox ID="ENTRY_FEE" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn>
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                        <a id="A1" href="#" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="addnewsitedetail(this,event);">
                            &raquo;
                            </a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridsitemaster_Command" OnRowSelected="radgridsitemaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                </td>
            </tr>
        </table>
        </div>
        <div class="pageTitle" style="float: left">
        <asp:Literal ID="lblPhotodetail" runat="server" Text="Site Photo Detail"></asp:Literal>
    </div>
   <br />
   <br />
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="DeleteSubProg" CssClass="button" Style="float: right; margin-right: 10px;
                        color: black; font-weight: bold;" OnClientClick="if(!confirm('Are you sure you want to delete this sitephoto?'))return false;deleteCurrentSitePhoto(); return false;"
                        Text="Delete Site Photo" runat="server" />
                </td>
            </tr>
        </table>
    <table >
            <tr>
                <td>
                    <telerik:radgrid id="radgridphotomaster" runat="server" allowpaging="false" allowmultirowselection="false"
                        allowsorting="True" pagesize="10" itemstyle-wrap="false" enableembeddedskins="false"
                        allowautomaticdeletes="True" allowautomaticinserts="True">
               <MasterTableView ClientDataKeyNames="SIGHTSEEING_ID" AllowMultiColumSorting="true" EditMode ="InPlace" Width="400px">
               <RowIndicatorColumn>
               </RowIndicatorColumn>
                    <Columns>

                    <telerik:GridTemplateColumn SortExpression ="SIGHTSEEING_ID" DataField="SIGHTSEEING_ID" HeaderText="SIGHTSEEING_ID" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHTSEEING_ID" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="SIGHT_SEEING_SRNO" DataField="SIGHT_SEEING_SRNO" HeaderText="SIGHT_SEEING_SRNO" Visible="false">
                          <ItemTemplate>
                            <asp:TextBox ID="SIGHT_SEEING_SRNO" runat="server" CssClass="radinput"></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHOTO_TITLE" DataField="PHOTO_TITLE" HeaderText="PHOTO TITLE">
                          <ItemTemplate>
                            <asp:TextBox ID="PHOTO_TITLE" runat="server" CssClass="radinput"  ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn SortExpression ="PHOTO_DESCRIPATION" DataField="PHOTO_DESCRIPATION" HeaderText="DESCRIPATION">
                          <ItemTemplate>
                            <asp:TextBox ID="PHOTO_DESCRIPATION" runat="server" CssClass="radinput" ></asp:TextBox>
                        </ItemTemplate>  
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn SortExpression="" DataField="" AllowFiltering="false" ShowSortIcon="false" HeaderText="PHOTO">
                    <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="65px" />
                        <ItemTemplate>
                            <asp:Button id="uploadphoto" runat="server" Text="PHOTO" onClientclick="openuploadnewphoto()" />
                            </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" ShowSortIcon="false" DataField="PHOTO">
                     <ItemStyle CssClass="ItemAlign" Width="25px" />
                      <HeaderStyle Width="25px" />
                        <ItemTemplate>
                       <a id="More" href="#" onclick="PopUpShowing(this,event)" style="border:0px solid #fff;text-decoration:none;height:14px;width:14px;" onkeydown="newsitePhotoadded(this,event);">
                            &raquo;<telerik:RadTextBox ID="PHOTO" runat="server" CssClass="disable"/>
                            </a>
                          <div id="divmore" style="width:300px;display:none;background-color:#fff;border:1px solid #c2c2c2;"><br />
                        <div class="pageTitle" style="float: left">
        <asp:Literal ID="Literal2" runat="server" Text="Upload Image :"></asp:Literal>
    </div> <br /><br /><img id="imgexistingimage" src="" alt="No Image Available"/><br /><br />
                       <span>Set Image :</span> <asp:FileUpload ID="flupld" runat="server"></asp:FileUpload><br /><br />
                       <asp:Button ID="btnok" runat="server" Text="Done !"/>&nbsp;&nbsp; <asp:Button ID="btncalcel" runat="server" Text="Cancel" OnClientClick="return disablepopup()" />
                       <br /><br /> </div>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
               <ClientSettings ReorderColumnsOnClient="true" AllowDragToGroup="true"
                AllowColumnsReorder="True">
                 <KeyboardNavigationSettings AllowActiveRowCycle="true"/>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" />
                <ClientEvents OnCommand="radgridphotomaster_Command" OnRowSelected="radgridphotomaster_RowSelected"/>
                <Selecting AllowRowSelect="true"/>
            </ClientSettings> 
               </telerik:radgrid>
                <asp:LinkButton ID="lbAddsitephoto" runat="server" Text="Add Another Site Photo" OnClientClick="AddNewSitePhoto();"></asp:LinkButton>
                </td>
            </tr>
        </table>
    
</asp:Content>
