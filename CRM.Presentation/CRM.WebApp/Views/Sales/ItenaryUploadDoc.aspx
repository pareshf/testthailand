<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItenaryUploadDoc.aspx.cs"
    Inherits="CRM.WebApp.Views.Sales.ItenaryUploadDoc" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManagerCRMMaster" runat="server">
     <Services>
     <asp:ServiceReference Path="~/webservice/TourMasterWebService.asmx" />
     </Services>
    </asp:ScriptManager>
    <div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script>
            var qrStr = window.location.search;
            var spQrStr = qrStr.substring(1);
            var arrQrStr = new Array();
            // splits each of pair
            var arr = spQrStr.split('&');
            for (var i = 0; i < arr.length; i++) {
                // splits each of field-value pair
                var index = arr[i].indexOf('=');
                var key = arr[i].substring(0, index);
                var val = arr[i].substring(index + 1);
                // saves each of field-value pair in an array variable
                arrQrStr[key] = val;
            }
            var TOUR_ID = arrQrStr["key"];

            function transferRight1() {
                var listfrom = document.getElementById('allcountry');
                var listto = document.getElementById('Selectedcountry');
                var optn;
                var sentresult = null;
                var listLength = listfrom.options.length;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listto.options.length; j++) {
                            sentresult = sentresult + ',' + listto.options[j].text;
                        }
                        break;
                    }
                }
                CRM.WebApp.webservice.TourMasterWebService.AssignCountry(TOUR_ID, sentresult);
            }
            function transferLeft1() {
                var listfrom = document.getElementById('Selectedcountry');
                var listto = document.getElementById('allcountry');
                var optn;
                var listLength = listfrom.options.length;
                var sentresult = null;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listfrom.options.length; j++) {
                            sentresult = sentresult + ',' + listfrom.options[j].text;
                        }
                            break;
                    }
                    }
                    CRM.WebApp.webservice.TourMasterWebService.AssignCountry(TOUR_ID, sentresult);
          }
          function transferRight2() {
                var listfrom = document.getElementById('allcitytravel');
                var listto = document.getElementById('SelectedTravel');
                var optn;
                var sentresult = null;
                var listLength = listfrom.options.length;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listto.options.length; j++) {
                            sentresult = sentresult + ',' + listto.options[j].text;
                        }
                        break;
                    }
                }
                CRM.WebApp.webservice.TourMasterWebService.AssignCITY(TOUR_ID, sentresult);
            }
            function transferLeft2() {
                var listfrom = document.getElementById('SelectedTravel');
                var listto = document.getElementById('allcitytravel');
                var optn;
                var sentresult = null;
                var listLength = listfrom.options.length;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listfrom.options.length; j++) {
                            sentresult = sentresult + ',' + listfrom.options[j].text;
                        }
                        break;
                    }
                }
                CRM.WebApp.webservice.TourMasterWebService.AssignCITY(TOUR_ID, sentresult);
            }
            function transferRight3() {
                var listfrom = document.getElementById('allstartendcity');
                var listto = document.getElementById('Selectedcity');
                var optn;
                var sentresult = null;
                var listLength = listfrom.options.length;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listto.options.length; j++) {
                            sentresult = sentresult + ',' + listto.options[j].text;
                        }
                        break;
                    }
                }
                CRM.WebApp.webservice.TourMasterWebService.AssignStartEndCity(TOUR_ID, sentresult);
            }
            function transferLeft3() {
                var listfrom = document.getElementById('Selectedcity');
                var listto = document.getElementById('allstartendcity');
                var optn;
                var sentresult = null;
                var listLength = listfrom.options.length;
                for (var i = 0; i < listLength; i++) {
                    if (listfrom.options[i].selected) {
                        var optn = document.createElement("OPTION");
                        optn.text = listfrom.options[i].text;
                        optn.value = listfrom.options[i].value;
                        listto.options.add(optn);
                        listfrom.remove(listfrom.selectedIndex);
                        for (var j = 0; j < listfrom.options.length; j++) {
                            sentresult = sentresult + ',' + listfrom.options[j].text;
                        }
                        break;
                    }
                }
                CRM.WebApp.webservice.TourMasterWebService.AssignStartEndCity(TOUR_ID, sentresult);
            }
            function alertmsg() {
                alert('Record Save Successfully');
            }
           
        </script>
        
        </telerik:radcodeblock>
        
        <table style="font-size: 12px; font-family: Verdana;">
            <tr>
                <td>
                    Countries For Visa
                </td><td></td>
                <td>
                    Selected Country
                </td>
                <td>
                    City To Travel
                </td><td></td>
                <td>
                    Selected City
                </td>
                <td>
                    Start End City
                </td><td></td>
                <td>
                    Selected City
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox id="allcountry"  datatextfield="COUNTRY_NAME" 
                        datavaluefield="COUNTRY_ID" runat="server" height="130px" Width="130px"></asp:ListBox>
                </td>
                <td>
                    <button id="btnleft1" onclick="return transferRight1();"> >> </button><br />
                    <button id="btnright1" onclick="return transferLeft1();"> << </button>               
                     </td>
                <td>
                    <asp:ListBox id="Selectedcountry" runat="server"  height="130px" Width="130px"></asp:ListBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:ListBox id="allcitytravel"
                        datatextfield="CITY_NAME" datavaluefield="CITY_ID" runat="server" height="130px" Width="130px"></asp:ListBox>
                </td>
                <td>
                   <button id="btnleft2" onclick="return transferRight2();"> >> </button><br />
                    <button id="btnright2" onclick="return transferLeft2();"> << </button>
                </td>
                <td>
                    <asp:ListBox id="SelectedTravel" runat="server"  enablemarkmatches="true" Width="130px"
                        height="130px"></asp:ListBox>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ListBox id="allstartendcity" 
                        datatextfield="CITY_NAME" datavaluefield="CITY_ID" runat="server" height="130px" Width="130px"></asp:ListBox>
                </td>
                <td>
                   <button id="btnleft3" onclick="return transferRight3();"> >> </button><br />
                    <button id="btnright3" onclick="return transferLeft3();"> << </button>
                </td>
                <td>
               <asp:ListBox id="Selectedcity" 
                        runat="server" height="130px" Width="130px"></asp:ListBox>
                </td>
            </tr>
        </table>
        <br />
        <table style="font-size: 12px; font-family: Verdana;">
            <tr>
                <td>
                    Full Detail
                </td>
                <td>
                    <asp:FileUpload ID="flupfulldetail" runat="server" />
                </td>
                <td>
                    <a runat="server" id="ancfulldetail" href="" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkfulldetail" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Tour Itenary
                </td>
                <td>
                    <asp:FileUpload ID="flupitenary" runat="server" />
                </td>
                <td>
                    <a runat="server" id="ancitenary" href="" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkitenary" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Tour Highlights
                </td>
                <td>
                    <asp:FileUpload ID="flupHighlights" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anchighlight" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkhighlights" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Destination Details
                </td>
                <td>
                    <asp:FileUpload ID="flupDetails" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancdetails" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkdetails" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Important Notes
                </td>
                <td>
                    <asp:FileUpload ID="flupNotes" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancnotes" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chknotes" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    General Guidelines
                </td>
                <td>
                    <asp:FileUpload ID="flupGuidelines" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancguidline" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkguidelines" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Cost Sheet
                </td>
                <td>
                    <asp:FileUpload ID="flupSheet" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancsheet" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chksheet" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Terms Condition
                </td>
                <td>
                    <asp:FileUpload ID="flupCondition" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anccondition" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkcondition" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Payment Terms
                </td>
                <td>
                    <asp:FileUpload ID="flupTerms" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancterms" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkterms" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Cancellation Charges
                </td>
                <td>
                    <asp:FileUpload ID="flupCharges" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anccharged" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkcharges" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Optional Siteseeing Cost
                </td>
                <td>
                    <asp:FileUpload ID="flupCost" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anccost" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkcost" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    USP
                </td>
                <td>
                    <asp:FileUpload ID="flupUSP" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancusp" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkusp" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Limitations
                </td>
                <td>
                    <asp:FileUpload ID="flupLimitations" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anclimitations" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chklimitations" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Compititor Comparision
                </td>
                <td>
                    <asp:FileUpload ID="flupComparision" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anccomparision" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkcomparision" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Final Itenary
                </td>
                <td>
                    <asp:FileUpload ID="flupfinalItenary" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancfinalitenary" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkfinalitenary" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Final Terms and Condition
                </td>
                <td>
                    <asp:FileUpload ID="flupTermsCondition" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="anctermsandcondition" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkfinalcondition" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td>
                    Final Vouchures
                </td>
                <td>
                    <asp:FileUpload ID="flupVouchures" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancvoucheres" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkvouchures" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClientClick=" return alertmsg();" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
