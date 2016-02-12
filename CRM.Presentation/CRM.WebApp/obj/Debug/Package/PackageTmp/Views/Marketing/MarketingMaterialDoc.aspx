<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketingMaterialDoc.aspx.cs" Inherits="CRM.WebApp.Views.Marketing.MarketingMaterialDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        var currentTextBox = null;
        var currentDatePicker = null;

        function showPopup(sender, e) {

            try {

                currentTextBox = sender;
                var datePicker = $find("<%= RadDatePicker1.ClientID %>");
                currentDatePicker = datePicker;
                datePicker.set_selectedDate(currentDatePicker.get_dateInput().parseDate(sender.value));
                var position = datePicker.getElementPosition(sender);
                datePicker.showPopup(position.x, position.y + sender.offsetHeight);

            }
            catch (e) { }

        }

        function dateSelected(sender, args) {

            try {

                if (currentTextBox != null) {

                    currentTextBox.value = args.get_newDate().format('dd/MM/yyyy');
                    currentDatePicker.hidePopup();

                }

            }
            catch (e) { }

        }

        function parseDate(sender, e) {

            currentDatePicker.hidePopup();
        }
    </script>
    
    <telerik:raddatepicker id="RadDatePicker1" style="display: none;" mindate="01/01/1900"
        maxdate="12/31/2100" runat="server">
            <ClientEvents OnDateSelected="dateSelected" />
        </telerik:raddatepicker>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <br />
        <br />
       <asp:Literal ID="lbltext" runat="server" Text="Upload Marketing Document :"></asp:Literal>
       <br />
       <br />
        <%--<table>
            <tr>
                <td>
                    Marketing Document :&nbsp;&nbsp;
                </td>
                <td>
                    <asp:FileUpload ID="marketingDoc" runat="server" />&nbsp;&nbsp;
                </td>
           
                <td>
                    <a runat="server" href="" id="marDoc" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="chkitenary" runat="server" /> 
                </td>
             </tr>
            
            <tr>
                <td colspan="2" align="left">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>--%>
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
               <td>
                    <asp:Label ID="Label1" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label14" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label15" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
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
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Brocer File
                </td>
                <td>
                    <asp:FileUpload ID="FlupBrocerfile" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancbrocer" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="Chkbrocer" runat="server" /> 
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Presentation File
                </td>
                <td>
                    <asp:FileUpload ID="FlupPresentationfile" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancpresentation" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="ChkPresentation" runat="server" /> 
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Email Format
                </td>
                <td>
                    <asp:FileUpload ID="FlupEmailformat" runat="server" />
                </td>
                <td>
                    <a runat="server" href="" id="ancemailformat" target="_blank">View</a>
                </td>
                <td>
                    <asp:CheckBox ID="Chkemailformat" runat="server" /> 
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Text="Expiry Date"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server" Width="130px" onclick="showPopup(this, event);"  onfocus="showPopup(this, event);" onkeydown ="parseDate(this, event);"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    Embad Code
                </td>
                <td>
                    <asp:TextBox ID="txtembadcode" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    web Url Brocer
                </td>
                <td>
                    <asp:TextBox ID="txtweburl" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
