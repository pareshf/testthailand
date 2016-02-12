<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="GitGroupInformation.aspx.cs" Inherits="CRM.WebApp.Views.GIT.GitGroupInformation"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
 <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server" >

   <script language="javascript" type="text/javascript">

       var sessionTimeout = "<%= Session.Timeout %>";

       var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
       setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

<div>
 <asp:Label runat="server" Text="GIT BOOKING" ID="headlbl" Width="200px" Font-Bold="true"
            Font-Size="Large" class="pageTitle"></asp:Label>
        <br />

        <asp:UpdatePanel ID="UpdatePanelGitPackages" runat="server" UpdateMode="Conditional"
            class="pageTitle">
            <ContentTemplate>
                <asp:Label ID="lblPackage" runat="server" Text="Select Package" CssClass="headlabel" Font-Bold="true"></asp:Label>&nbsp;<span
                    class="error">*</span>
                <br />
                <br />
                <asp:DataList ID="datalist_packages" runat="server">
                    <ItemTemplate>
                        <table width="400px" id="Package" runat="server" border="1" style="border-collapse: collapse;
                            border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
                            <tr>
                                <td style="width: 350px;">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("GIT_PACKAGE_NAME") %>' CssClass="lblstyleGIT"></asp:Label>
                                </td>
                                <td>
                                   
                                    <asp:RadioButton ID="rbtnpackage" runat="server" GroupName="Pack" OnCheckedChanged="CheckChanged" AutoPostBack="true"
                                         />
                                   
                                </td>
                                <td style="display: none">
                                    <asp:Label ID="lblPackgeId" runat="server" Text='<%# Bind("GIT_PACKAGE_ID") %>' CssClass="lblstyleGIT"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br />

         <%-----------------------------------------------------------------------CLIENT DETAILS----------------------------------------------------------------------------------------------------%>
        <asp:UpdatePanel ID="UpdatePanel_TourDetails" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="false">
            <ContentTemplate>
                &nbsp;&nbsp;
                <asp:Label ID="headinglabel" runat="server" Text="Client Details" CssClass="headlabel"
                    class="pageTitle" Font-Bold="true"></asp:Label>
                <br />
                 <table width="100%">
                    <tr>
                        <td width="35%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="On Behalf Of Agent" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server" Width="250px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="Sub Agents" CssClass="lblstyle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpSubagent" runat="server" Width="250px" OnSelectedIndexChanged="drpSubagent_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%">
                                        <asp:Label ID="Label9" runat="server" Text="Group Name" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td width="75%">
                                       <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server" ErrorMessage="Group Name Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="txtGroupName" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>

                                 <tr>
                                    <td width="25%">
                                        <asp:Label ID="Label27" runat="server" Text="Total Rooms" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td width="75%">
                                       <asp:TextBox ID="txtTotalRoom" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server" ErrorMessage="Group Name Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="txtGroupName" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>

                                 <tr style="display:none">
                                    <td width="25%">
                                        <asp:Label ID="Label28" runat="server" Text="No of Single Rooms" CssClass="fieldlabel"></asp:Label>
                                        <%--&nbsp;<span
                                            class="error">*</span>--%>
                                    </td>
                                    <td width="75%">
                                       <asp:TextBox ID="txtSingleRooms" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                          
                                    </td>
                                </tr>

                                  <tr style="display:none">
                                    <td width="25%">
                                        <asp:Label ID="Label29" runat="server" Text="No of Double Rooms" CssClass="fieldlabel"></asp:Label><%--&nbsp;<span
                                            class="error">*</span>--%>
                                    </td>
                                    <td width="75%">
                                       <asp:TextBox ID="txtDoubleRooms" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                          
                                    </td>
                                </tr>
                             
                             <tr style="display:none">
                                    <td width="25%">
                                        <asp:Label ID="Label30" runat="server" Text="No of Triple Rooms" CssClass="fieldlabel"></asp:Label><%--&nbsp;<span
                                            class="error">*</span>--%>
                                    </td>
                                    <td width="75%">
                                       <asp:TextBox ID="txtTripleRooms" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                          
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="No of Adult" CssClass="fieldlabel"></asp:Label><%--&nbsp;<span
                                            class="error">*</span>--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_Adult" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                runat="server" ErrorMessage="No of Adult Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="txtNo_Adult" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="No of CWB" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_CWB" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="No of CNB" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_CNB" runat="server" CssClass="textboxstyle" Width="160px"
                                           ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="No of Infant" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNo_Infant" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="No. Of Nights" CssClass="fieldlabel"
                                            ></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNoofNights" runat="server" CssClass="textboxstyle" Width="160px" 
                                           ></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                runat="server" ErrorMessage="No of Nights Required " CssClass="errorclass" ValidationGroup="Required"
                                                ControlToValidate="txtNoofNights" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Start Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="textboxstyle" Width="160px" OnTextChanged="txtFrom_Date_TextChanged" AutoPostBack="true" 
                                           ></asp:TextBox>
                                             <ajax:CalendarExtender ID="CalendarExtender1" runat="server" targetcontrolid="txtStartDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Start Date is Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="End Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="End Date is Required"
                                            CssClass="errorclass" ValidationGroup="Required" Display="Dynamic" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Package Name" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                         <asp:TextBox ID="txtPackageName" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                </tr>

                                 <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Order Status" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:DropDownList ID="drpOrderStatus" runat="server" Width="160px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                 <tr id="trCancelFees" runat="server" visible="false">
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Text="Cancellation Fees" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:TextBox ID="txtCancelFees" runat="server" Width="160px" ></asp:TextBox>
                                          <asp:Label ID = "lblUSD" Text = "USD" runat ="server"></asp:Label>
                                    </td>
                                </tr>
                            
                            </table>
                        </td>
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <td valign="top">
                           <table width="90%">
                               <tr>
                               <td>
                                <table>
                                <tr>
                                <td><asp:Label ID="lblRooms" runat="server" Text = "Select Slab" Font-Bold="true" Width="135px"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                               
                                <asp:Button ID="btnSelectAll" runat="server" Text="SELECT ALL" OnClick="btnSelectAll_Click"/></td>
                                
                                </tr>

                                <tr>
                                <td>
                                     <asp:GridView ID="GridSlab" runat="server" 
                                AutoGenerateColumns="False" SkinID="sknSubGrid" AllowPaging="false" Width="250px">
                                <Columns>
                                
                            <asp:BoundField DataField="NO_OF_PAX" HeaderText="No of Pax" ItemStyle-HorizontalAlign="Center"/>
                               

                                      <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSelect" runat="server" Text = "Select"></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>


                                    
                                    </asp:TemplateField>

                                     <asp:TemplateField Visible="false">
                                    
                                    <HeaderTemplate>
                                      
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblSlabId" runat="server" Text = '<%# Bind("SLAB_ID") %>'></asp:Label>
                                    </ItemTemplate>


                                    
                                    </asp:TemplateField>

                                
                                </Columns>
                                </asp:GridView>
                                </td>
                                </tr>

                                
                               <%-- <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="No of Nights" CssClass="fieldlabel"
                                            Width="80px"></asp:Label><span class="error">*</span>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtNo_OfNights" runat="server" CssClass="textboxstyle" Width="160px"
                                            OnTextChanged="txtNo_OfNights_TextChanged" AutoPostBack="true" ></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="No of Nights Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtNo_OfNights"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text="Order Status" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="160px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Tour Name" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTourname" runat="server" Width="200px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="From Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtFrom_Date" runat="server" CssClass="textboxstyle" Width="160px"
                                             Text="dd/MM/yyyy" OnTextChanged="txtFrom_Date_TextChanged" AutoPostBack="true"
                                            onblur="message(this);"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="From Date Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtFrom_Date"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <ajax:calendarextender id="CalendarExtender8" runat="server" targetcontrolid="txtFrom_Date"
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="To Date" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtTo_Date" runat="server" CssClass="textboxstyle" Width="160px"
                                           ReadOnly="true"> 
                                        </asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="To Date Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="txtTo_Date"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                      
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Arrival Flight No." CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArrival_Flight" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Time" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="atime" runat="server" visible="false">*</span>
                                    </td>
                                    <td>
                                        <telerik:radtimepicker id="RadTimePicker1" runat="server"  width="90px"> 
         
            <TimeView TimeFormat="h:mm tt"></TimeView> 
            <DateInput DisplayDateFormat="hh:mm tt">
   </DateInput>

        
    </telerik:radtimepicker>
                                      
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="Departure Flight No." CssClass="fieldlabel"
                                            Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeparture_Flight" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Time" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error" id="dtime" runat="server" visible="false">*</span>
                                    </td>
                                    <td>
                                        <telerik:radtimepicker id="RadTimePicker2" runat="server" width="90px"> 
                       <TimeView TimeFormat="h:mm tt"></TimeView> 
                       <DateInput DisplayDateFormat="hh:mm tt">
                        </DateInput>
                         </telerik:radtimepicker>
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="Remarks" CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="FIT Package Type" CssClass="fieldlabel"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList3" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="FIT package Type is Required"
                                            CssClass="errorclass" ValidationGroup="Required" ControlToValidate="DropDownList3"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="cancellationfees" style="display: none" runat="server">
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Cancellation Fees " CssClass="fieldlabel"></asp:Label>
                                    </td>
                                    <td class="style1">
                                        <asp:TextBox ID="txtcancellation" runat="server" CssClass="textboxstyle" Width="160px"
                                            ></asp:TextBox>
                                    </td>
                                </tr>--%>
                            </table>
                                </td>
                             <td valign="top">
                                <br />
                                
                                <table border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC; width:150px" >
                                <tr style="background-color: #f3f3f3">
                                <td style="width:100px">
                                    <asp:Label ID="Label20" runat="server" Text="PAX"  Font-Bold="true" CssClass="headlabel"></asp:Label>
                                </td>
                                <td style="width:150px">
                                    <asp:Label ID="Label21" runat="server" Text="Select Slab"  Font-Bold="true" CssClass="headlabel"></asp:Label>
                                </td>
                                </tr>
                                </table>

                                <table  border="1" style="border-collapse: collapse;
                        border-color: #E6E6E6 #E6E6E6 #CCCCCC; width:150px "  >
                                <tr >
                                <td style="width:90px"><asp:Label ID="Label4" runat="server" Text="55"   CssClass="fieldlabel"></asp:Label></td>
                                <td style="width:150px">
                                <asp:Label ID="Label8" runat="server" Text="25" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                </tr>

                                

                                
                                <tr>
                                <td>
                                <asp:Label ID="Label12" runat="server" Text="60" CssClass="fieldlabel" ></asp:Label>

                                </td>
                                <td>
                                <asp:Label ID="Label22" runat="server" Text="30" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                </tr>

                                 <tr>
                                <td>
                                <asp:Label ID="Label13" runat="server" Text="65" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                <td>
                                <asp:Label ID="Label23" runat="server" Text="30" CssClass="fieldlabel"></asp:Label>
                                </td>
                                </tr>

                                <tr>
                                <td>
                                <asp:Label ID="Label17" runat="server" Text="70" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                <td>
                                <asp:Label ID="Label24" runat="server" Text="35" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                </tr>

                                <tr>
                                <td>
                                <asp:Label ID="Label18" runat="server" Text="110" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                <td>
                                <asp:Label ID="Label25" runat="server" Text="35" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                </tr>

                                <tr>
                                <td>
                                <asp:Label ID="Label19" runat="server" Text="120" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                <td>
                                <asp:Label ID="Label26" runat="server" Text="40" CssClass="fieldlabel" ></asp:Label>
                                </td>
                                </tr>
                                </table>
                             </td>

                             </tr>

                             </table>
                                            
                           
                        </td>
                        
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <table>
        <tr>
        <td>
        <asp:Button runat="server" ID="BtnNext" Text="Next" width="100px" OnClick="BtnNext_Click" ValidationGroup= "Required" />
        </td>
        </tr>
        </table>
</div>

</asp:Content>
