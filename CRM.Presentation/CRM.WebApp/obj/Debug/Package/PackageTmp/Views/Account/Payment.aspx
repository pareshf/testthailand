﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="CRM.WebApp.Views.Account.Payment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
<link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">


 <script language="javascript" type="text/javascript">

     var sessionTimeout = "<%= Session.Timeout %>";

     var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
     setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>



     <asp:Label ID="Label63" runat="server" Text="Accounts Payment Voucher" class="pageTitle" Width="400px" Font-Bold="true"
            Font-Size="Large"></asp:Label>
 
            <div>
            
                 <asp:UpdatePanel ID="update_voucher" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
            <ContentTemplate>
                
                <table width="800px" id="AccountsVoucher1" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">

                <tr style="background-color:#f3f3f3">
                
                <td style="width: 120px">
                    <asp:Label ID="Label1" runat="server" Text="Voucher Type" ></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpvoucher_type" runat="server" Width="250px"   AutoPostBack="true" >
                    </asp:DropDownList>
                </td>
                </tr>

                <tr style="background-color:#f3f3f3">
                
                <td style="width: 120px">
                    <asp:Label ID="Label42" runat="server" Text="Voucher No." ></asp:Label>
                </td>

                <td style="width: 120px">
                    <asp:Label ID="lbl_voucher_no" runat="server" Text="" ></asp:Label>
                </td>
                </tr>

               <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Supplier Type" CssClass = "lblstyle"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpsupplier_type" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="drpsupplier_type_SelectedIndexChanged">
                                    
                                    </asp:DropDownList>
                                </td>
               </tr>
               <tr>
                                <td>
                                    <asp:Label ID="Label39" runat="server" Text="Supplier" CssClass = "lblstyle"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpsupplier" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="drpsupplier_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                </tr>

                <tr style="background-color:#f3f3f3">
                <td>
                    <asp:Label ID="Label32" runat="server" Text="Voucher Date"></asp:Label>
                </td>
                <td>
                   <asp:Label ID="lbl_voucher_date" runat="server" Text = ""></asp:Label>
                </td>
                </tr>

                <tr style="background-color:#f3f3f3">
                <td>
                    <asp:Label ID="Label43" runat="server" Text="GL Date"></asp:Label>
                </td>
                <td>
                   <asp:TextBox ID="txtgl_date" runat="server" 
               ontextchanged="txtgl_date_TextChanged" AutoPostBack="true"></asp:TextBox>

               <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtgl_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
                </td>
                </tr>

                 <tr style="background-color:#f3f3f3">
                <td>
                    <asp:Label ID="Label44" runat="server" Text="On Account"></asp:Label>
                </td>
                <td>
                   <asp:CheckBox ID="chk_onaccount" runat="server" OnCheckedChanged="chk_onaccount_CheckedChanged" AutoPostBack="true" Enabled="false"/>
                </td>
                </tr>

                </table>
            
            </ContentTemplate>
            </asp:UpdatePanel>

            </div>

            <br />

            <div>
             <asp:UpdatePanel ID="update_forex" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
             <ContentTemplate>
  <asp:Label ID="Label10" runat="server" Text="Foreign Exchange" class="pageTitle" Width="150px"
         style="font-weight:normal; font-size:16px; font-family:Verdana"></asp:Label>

         
             
                <table width="800px" id="Table1" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6                        #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" >

                  <tr style="background-color:#f3f3f3;display:none">
                       <td style="width: 120px">
                           <asp:Label ID="Label21" runat="server" Text="Foreign Currency" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="drp_currency"  runat="server" Width="150px" >
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3" id="Tr1" runat="server">
                       <td>
                           <asp:Label ID="Label12" runat="server" Text="Amount (THB)" CssClass="headlabel">
                          </asp:Label>&nbsp;<span class="error">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_amount" runat="server" Width="155px" AutoPostBack="true"  OnTextChanged="txt_amount_TextChanged"
                                ></asp:TextBox>
                                <asp:RequiredFieldValidator
                               ID="RequiredFieldValidator2" runat="server" ErrorMessage="Amount is required" ValidationGroup="required" Font-Size="12px" ControlToValidate="txt_amount" CssClass="gridlabel"></asp:RequiredFieldValidator>
                         
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3" id="Tr2" runat="server">
                       <td >
                           <asp:Label ID="Label23" runat="server" Text="Bank Fees" CssClass="headlabel">
                          </asp:Label><%-- &nbsp;<span class="error">*</span>--%>
                        </td>
                        <td>
                           <asp:TextBox ID="txtBankFees" runat="server" Width="155px" OnTextChanged="txtBankFees_TextChanged" AutoPostBack="true"></asp:TextBox>
                           
                         </td>
                 </tr>
                  
                 </table>
              </ContentTemplate>
              </asp:UpdatePanel>
            </div>
            <br />
            <div>

             <div id="Debit_Selection">
   
    <asp:UpdatePanel ID="update_bedit_select" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
            <ContentTemplate>
        <table width="900px" id="grid2_table" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" visible="false">
   <tr id="row_head" runat="server" style="width:1000px; background-color:#f3f3f3" >
      <td style="width:150px">
        <asp:Label ID="Label26" runat="server" Text="Sr. No." CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:Label ID="Label33" runat="server" Text="General Leger Code" CssClass="gridlabel"></asp:Label>
      </td>
       <td style="width:200px">
          <asp:Label ID="Label35" runat="server" Text="Debit" CssClass="gridlabel"></asp:Label>
          
      </td>
       <td style="width:200px">
          <asp:Label ID="Label36" runat="server" Text="Credit" CssClass="gridlabel" ></asp:Label>
      </td>
    
   </tr>

    <tr id="row1_debit" runat="server" style="width:1000px; background-color:#f3f3f3">
      <td style="width:150px">
        <asp:Label ID="Label37" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:Label ID="lbl_gl_code" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
      </td>
       <td style="width:200px">
          <asp:Label ID="lbl_row1_debit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
           &nbsp;
          <asp:Label ID="Label40" runat="server" Text="THB" CssClass="gridlabel" ></asp:Label>
      </td>
       <td style="width:200px">
          <asp:Label ID="lbl_row1_credit" runat="server" Text="" CssClass="gridlabel" ></asp:Label>
         
      </td>
   
   </tr>

   <tr id="row2_debit" runat="server" style="width:1000px; background-color:#f3f3f3">
      <td style="width:150px">
        <asp:Label ID="Label38" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="drp_gl_code"  runat="server" Width="150px"/>
      </td>
       <td style="width:200px">
          <asp:Label ID="lbl_row2_debit" runat="server" Text="" CssClass="gridlabel"></asp:Label>
      </td>
       <td style="width:200px">
          <asp:Label ID="lbl_row2_credit" runat="server" Text="" CssClass="gridlabel" ></asp:Label>
          &nbsp;
          <asp:Label ID="Label41" runat="server" Text="THB" CssClass="gridlabel" ></asp:Label>
      </td>
     
   </tr>
   </table>
   </ContentTemplate>
   </asp:UpdatePanel>

   </div>

            </div>

            <br />
            <br />
            <div>
   <asp:UpdatePanel ID="updategrid" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
            <ContentTemplate>
        <table width="1000px" id="AccountsVoucher4" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
   <tr id="gridheading" runat="server" style="width:1000px; background-color:#f3f3f3">
      <td style="width:150px">
        <asp:Label ID="Label3" runat="server" Text="Sr. No." CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:Label ID="Label16" runat="server" Text="Voucher No." CssClass="gridlabel"></asp:Label>
      </td>
       <td style="width:200px">
          <asp:Label ID="Label17" runat="server" Text="Voucher Amount" CssClass="gridlabel"></asp:Label>
      </td>
       
       <td style="width:200px">
          <asp:Label ID="Label19" runat="server" Text="Setteled Amount" CssClass="gridlabel"></asp:Label>
      </td>

      <td style="width:200px">
          <asp:Label ID="Label18" runat="server" Text="Balance to be Paid" CssClass="gridlabel" ></asp:Label>
      </td>

      <td style="display:none;" id="lblduedate" runat="server">
            <asp:Label ID="lbl_duedate" runat="server" Text="Due Date" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:150px;display:none">
          <asp:Label ID="Label4" runat="server" Text="Against Sales Invoice" CssClass="gridlabel"></asp:Label>

         
      </td>
        <td style="width:100px">
          
      </td>
   </tr>


<%------------------------------------------- ROW 1------------------------------------------------------------%>

   <tr id="row1" runat="server" style="width:1300px">
      <td style="width:100px">
        <asp:Label ID="Label20" runat="server" Text="1" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row1_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row1_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row1_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row1_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row1_txt_received" runat="server" 
               ontextchanged="row1_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
          <asp:Label ID="row1_lbl_bal_paid" runat="server" Text = ""></asp:Label>

      </td>

       <td style="display:none;" id="row1_due_Date" runat="server">
        <asp:TextBox ID="row1_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" runat="server" TargetControlID="row1_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>

      </td>
      <td style="width:200px;display:none">
          <asp:DropDownList ID="row1_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
     
        <td style="width:100px">
           <asp:Button ID="row1_btn_view" Text="View" runat="server" CssClass="BtnStyle" style="display:none" OnClick="row1_btn_view_onclick"/>
        
      </td>

     
   </tr>
  

   

   <%------------------------------------------- ROW 2------------------------------------------------------------%>
   <tr id="row2" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label5" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row2_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row2_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row2_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row2_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row2_txt_received" runat="server" 
               ontextchanged="row2_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>

                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="row2_txt_received" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
      </td>

      <td style="width:300px">
           <asp:Label ID="row2_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

       <td style="display:none;" id="row2_due_Date" runat="server">
        <asp:TextBox ID="row2_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row2_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
       <td style="width:100px">
           <asp:Button ID="row2_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row2_btn_view_onclick" />
      </td>
      <td style="width:100px">
           <asp:Button ID="row2_btn_remove" Text="REMOVE" runat="server" 
               CssClass="BtnStyle" onclick="row2_btn_remove_Click" />
      </td>
  
   </tr>

   <%------------------------------------------- ROW 3------------------------------------------------------------%>
    <tr id="row3" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label8" runat="server" Text="3" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row3_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row3_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row3_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row3_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
      
       <td style="width:300px">
          <asp:TextBox ID="row3_txt_received" runat="server" 
               ontextchanged="row3_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>

           
      </td>

       <td style="width:300px">
          <asp:Label ID="row3_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

        <td style="display:none;" id="row3_due_Date" runat="server">
        <asp:TextBox ID="row3_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" runat="server" TargetControlID="row3_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>   

      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row3_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
      <td style="width:100px">
           <asp:Button ID="row3_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row3_btn_view_onclick"/>
      </td>
   <td style="width:100px">
           <asp:Button ID="row3_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row3_btn_remove_Click"/>
      </td>
   </tr>

   <%------------------------------------------- ROW 4------------------------------------------------------------%>
    <tr id="row4" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label11" runat="server" Text="4" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row4_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row4_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row4_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row4_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row4_txt_received" runat="server" 
               ontextchanged="row4_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
           <asp:Label ID="row4_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

       <td style="display:none;" id="row4_due_Date" runat="server">
        <asp:TextBox ID="row4_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>
        
        <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" runat="server" TargetControlID="row4_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>   
      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row4_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
    <td style="width:100px">
           <asp:Button ID="row4_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row4_btn_view_onclick" />
      </td>
       <td style="width:100px">
           <asp:Button ID="row4_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row4_btn_remove_Click"/>
      </td>
   </tr>

   <%------------------------------------------- ROW 5------------------------------------------------------------%>
    <tr id="row5" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label14" runat="server" Text="5" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row5_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row5_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row5_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row5_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row5_txt_received" runat="server" 
               ontextchanged="row5_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
          <asp:Label ID="row5_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

       <td style="display:none;" id="row5_due_Date" runat="server">
        <asp:TextBox ID="row5_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" runat="server" TargetControlID="row5_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>   

      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row5_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
       <td style="width:100px">
           <asp:Button ID="row5_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row5_btn_view_onclick"/>
      </td>
   <td style="width:100px">
           <asp:Button ID="row5_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row5_btn_remove_Click" />
      </td>
   </tr>

   <%------------------------------------------- ROW 6------------------------------------------------------------%>
    <tr id="row6" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label22" runat="server" Text="6" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row6_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row6_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row6_lbl_amount" runat="server" ></asp:Label>
          <asp:Label ID="row6_lbl_currency" runat="server" ></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row6_txt_received" runat="server" 
               ontextchanged="row6_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
          <asp:Label ID="row6_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

      <td style="display:none;" id="row6_due_Date" runat="server">
        <asp:TextBox ID="row6_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

         <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender10" runat="server" TargetControlID="row6_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>   
      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row6_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
       <td style="width:100px">
           <asp:Button ID="row6_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row6_btn_view_onclick"/>
      </td>
      <td style="width:100px">
           <asp:Button ID="row6_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row6_btn_remove_Click"/>
      </td>
  
   </tr>

   <%------------------------------------------- ROW 7------------------------------------------------------------%>
    <tr id="row7" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label25" runat="server" Text="7" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row7_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row7_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row7_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row7_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row7_txt_received" runat="server" 
               ontextchanged="row7_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
          <asp:Label ID="row7_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

      <td style="display:none;" id="row7_due_Date" runat="server"> 
        <asp:TextBox ID="row7_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender11" runat="server" TargetControlID="row7_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender> 
      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row7_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
    <td style="width:100px">
           <asp:Button ID="row7_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row7_btn_view_onclick" />
      </td>
       <td style="width:100px">
           <asp:Button ID="row7_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row7_btn_remove_Click"/>
      </td>
   </tr>

   <%------------------------------------------- ROW 8------------------------------------------------------------%>
    <tr id="row8" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label28" runat="server" Text="2" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row8_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row8_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row8_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row8_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row8_txt_received" runat="server" 
               ontextchanged="row8_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
            <asp:Label ID="row8_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

      <td style="display:none;" id="row8_due_Date" runat="server">
        <asp:TextBox ID="row8_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

         <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender12" runat="server" TargetControlID="row8_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row8_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
  <td style="width:100px">
           <asp:Button ID="row8_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row8_btn_view_onclick" />
      </td>
      <td style="width:100px">
           <asp:Button ID="row8_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row8_btn_remove_Click" />
      </td>
   </tr>

<%------------------------------------------- ROW 9------------------------------------------------------------%>
    <tr id="row9" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label31" runat="server" Text="9" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row9_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row9_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row9_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row9_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
       
       <td style="width:300px">
          <asp:TextBox ID="row9_txt_received" runat="server" 
               ontextchanged="row9_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>

      <td style="width:300px">
          <asp:Label ID="row9_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

      <td style="display:none;" id="row9_due_Date" runat="server">
        <asp:TextBox ID="row9_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender13" runat="server" TargetControlID="row9_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>

      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row9_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
  <td style="width:100px">
           <asp:Button ID="row9_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false" OnClick="row9_btn_view_onclick"/>
      </td>
       <td style="width:100px">
           <asp:Button ID="row9_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row9_btn_remove_Click"/>
      </td>
   </tr>

   <%------------------------------------------- ROW 10------------------------------------------------------------%>
    <tr id="row10" runat="server" style="width:1300px ;  display:none">
      <td style="width:100px">
        <asp:Label ID="Label34" runat="server" Text="10" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:400px">
          <asp:DropDownList ID="row10_drp_invoice" runat="server" Width="200px" onselectedindexchanged="row10_drp_invoice_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
      </td>
       <td style="width:200px">
          
          <asp:Label ID="row10_lbl_amount" runat="server" Text = ""></asp:Label>
          <asp:Label ID="row10_lbl_currency" runat="server" Text = ""></asp:Label>
      </td>
      
       <td style="width:300px">
          <asp:TextBox ID="row10_txt_received" runat="server" 
               ontextchanged="row10_txt_received_TextChanged" AutoPostBack="true"></asp:TextBox>
      </td>


       <td style="width:300px">
          <asp:Label ID="row10_lbl_bal_paid" runat="server" Text = ""></asp:Label>
      </td>

      <td style="display:none;" id="row10_due_Date" runat="server">
        <asp:TextBox ID="row10_txt_duedate" runat="server" 
                AutoPostBack="true" style="display:none"></asp:TextBox>

        <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender14" runat="server" TargetControlID="row10_txt_duedate" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
      </td>

      <td style="width:200px;display:none">
          <asp:DropDownList ID="row10_drp_status" runat="server" Width="150px" Enabled="false">
          </asp:DropDownList>
      </td>
       <td style="width:100px">
           <asp:Button ID="row10_btn_view" Text="View" runat="server" CssClass="BtnStyle" Visible="false"  OnClick="row10_btn_view_onclick"/>
      </td>
      <td style="width:100px">
           <asp:Button ID="row10_btn_remove" Text="REMOVE" runat="server" CssClass="BtnStyle"  onclick="row10_btn_remove_Click"/>
      </td>
  <%------------------------------------------- ROW 11------------------------------------------------------------%>
   </tr>
    <tr id="Tr3" runat="server" style="width:900px ">
      <td style="width:100px">
        
      </td>
      <td style="width:400px">
         <asp:Label ID="Label30" runat="server" Text = "Total Amount Received" CssClass="gridlabel" Font-Bold="true"></asp:Label>
      </td>
       
       <td style="width:100px">
          
      </td>
       <td style="width:300px">
          <asp:Label ID="lbl_total_amount" runat="server" Text = "" CssClass="gridlabel"></asp:Label>
      </td>
      <td style="width:200px">
       
      </td>
  
   </tr>
    </table>
    <br />
    <table>
      <tr>
                    <td>
   <asp:Button ID="btnadd2" runat="server" Text = "ADD" onclick="btnadd2_Click" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd3" runat="server" Text = "ADD" onclick="btnadd3_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd4" runat="server" Text = "ADD" onclick="btnadd4_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd5" runat="server" Text = "ADD" onclick="btnadd5_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd6" runat="server" Text = "ADD" onclick="btnadd6_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd7" runat="server" Text = "ADD" onclick="btnadd7_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd8" runat="server" Text = "ADD" onclick="btnadd8_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd9" runat="server" Text = "ADD" onclick="btnadd9_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            <asp:Button ID="btnadd10" runat="server" Text = "ADD" onclick="btnadd10_Click" style="display:none" width="100px" CssClass="BtnStyle"/>
            </td>
            </tr>
</table>
  
  <%--<ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
                PopupControlID="pnlCompanyRoleSelection" TargetControlID="row1_btn_view" Drag="true"
                PopupDragHandleControlID="pnlCompanyRoleSelectionHeader" CancelControlID="ImageButton1">
            </ajax:ModalPopupExtender>--%>
            <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px" style="display:none;" Height="200px">
                <asp:Panel ID="Panel1" runat="server" Width="350px">
                    <fieldset style="background-color: White">
                        <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitleAlert" runat="server" Text="" ForeColor="#FEFEFE"
                                            Font-Size="15px"></asp:Label>
                                    </td>
                                    <td style="width: 17px;" align="center" valign="middle">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                            Style="cursor: pointer;" ToolTip="Close" />
                                        <asp:Button ID="Button5" runat="server" Text="Button" style="display:none" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table style="width: 100%">
                            <tr>
                            
                            <td>
                                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" SkinID="sknSubGrid">
                                <Columns>
                                <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                                 <asp:BoundField DataField="DR_AMOUNT" HeaderText="Amount" />
                                 <asp:BoundField DataField="DATE" HeaderText="Voucher Date" />
                                      <asp:BoundField DataField="PAY_DATE" HeaderText="Payment Date" />
                                      
                                </Columns>
                                </asp:GridView>
                          
                            </td>
                            </tr>
                            
                            </table>
                        </asp:Panel>
                        <br />
                       
                    </fieldset>
                </asp:Panel>
            </asp:Panel>

               
  
   </ContentTemplate>
   </asp:UpdatePanel>
   </div>

         <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" class="pageTitle">
        <ContentTemplate>
            <table id="Table2" runat="server">
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Confirm" Visible="false" />
                        
                        <%--<asp:Button ID="Button3" runat="server" Text="Wait List" Visible="false" />
                        <asp:Button ID="Button4" runat="server" Text="Change Of Hotel" Visible="false" />--%>
                    </td>
                </tr>
            </table>
          <%--  <ajax:ModalPopupExtender ID="PopEx_lnkBtnChangePreference" runat="server" BackgroundCssClass="modalPopupBackground"
                PopupControlID="pnlCompanyRoleSelection" TargetControlID="Button1" Drag="true"
                PopupDragHandleControlID="pnlCompanyRoleSelectionHeader" CancelControlID="ImageButton1">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px" style="display:none;">
                <asp:Panel ID="Panel1" runat="server" Width="350px">
                    <fieldset style="background-color: White">
                        <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTitleAlert" runat="server" Text="Please Enter Reconfirmation Date" ForeColor="#FEFEFE"
                                            Font-Size="15px"></asp:Label>
                                    </td>
                                    <td style="width: 17px;" align="center" valign="middle">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                            Style="cursor: pointer;" ToolTip="Close" />
                                        <%--<asp:Button ID="Button5" runat="server" Text="Button" style="display:none" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                       
                    </fieldset>
                </asp:Panel>
            </asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
   

   

    <div id="paymentdetails1" >

  <asp:UpdatePanel ID="update_payments" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
              <ContentTemplate>

  <asp:Label ID="Label6" runat="server" Text="Payment Details" class="pageTitle" Width="150px"
         style="font-weight:normal; font-size:16px; font-family:Verdana"></asp:Label>
        
          

        <table width="800px" id="AccountsVoucher3" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5" >


       <%-- <tr style="background-color:#f3f3f3">
        <td>
                         <asp:Label ID="Label26" runat="server" Text="General Leger Code" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td >
                            <asp:DropDownList ID="drp_gl_code"  runat="server" Width="150px"/>
                        </td>
        </tr>--%>

        <tr style="background-color:#f3f3f3">
        <td >
                         <asp:Label ID="Label24" runat="server" Text="Payment Date" CssClass="headlabel">
                          </asp:Label>&nbsp;<span class="error">*</span>
                       </td>
                       <td >
                          <asp:TextBox ID="txt_payment_date" runat="server" CssClass="headlabel" 
                               Width="155px" ontextchanged="txt_payment_date_TextChanged" AutoPostBack="true"> 
                                </asp:TextBox>
                                 <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txt_payment_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
                            <ajax:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txt_payment_date"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                        </td>
        </tr>

                  <tr style="background-color:#f3f3f3">
                       <td style="width: 120px">
                           <asp:Label ID="Label9" runat="server" Text="Payment Mode" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="drppayment_mode"  runat="server" Width="150px" 
                              onselectedindexchanged="drppayment_mode_SelectedIndexChanged" AutoPostBack="true"  >
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="bank_name_tr" runat="server">
                       <td>
                           <asp:Label ID="lbl_bank" runat="server" Text="Bank" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="drpbank_name"  runat="server" Width="150px" 
                            onselectedindexchanged="drpbank_name_SelectedIndexChanged" AutoPostBack="true">
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="branch_tr" runat="server">
                       <td >
                           <asp:Label ID="Label7" runat="server" Text="Branch" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="drpbranch"  runat="server" Width="150px">
                         </asp:DropDownList>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="cheque_no_tr" runat="server">
                       <td >
                         <asp:Label ID="lbl_chq_no" runat="server" Text="Cheque No" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                          <asp:TextBox ID="txtcheque_no" runat="server" CssClass="headlabel" Width="155px" > 
                                </asp:TextBox>
                        </td>
                    </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="cheque_date_tr" runat="server">
                       <td >
                         <asp:Label ID="Label13" runat="server" Text="Cheque Date" CssClass="headlabel" >
                          </asp:Label>
                       </td>
                       <td>
                       
                          <asp:TextBox ID="txtcheque_date" runat="server" CssClass="headlabel" Ontextchanged="txtcheque_date_TextChanged" AutoPostBack="true"
                               Width="155px" > 
                                </asp:TextBox>
                           <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtcheque_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
  
                        </td>
                    </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="cash_receipt_no_tr" runat="server">
                       <td >
                           <asp:Label ID="Label15" runat="server" Text="Cash Receipt No" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtcash_receipt" runat="server" CssClass="headlabel" width="155px"> 
                                </asp:TextBox>
                         </td>
                 </tr>
                  <tr style="background-color:#f3f3f3; display:none" id="cash_receipt_date_tr" runat="server">
                       <td >
                         <asp:Label ID="lblAttachment" runat="server" Text="Attachment" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                          <asp:TextBox ID="txtcashreceipt_date" runat="server" CssClass="headlabel" ontextchanged="txtcashreceipt_date_TextChanged" AutoPostBack="true"
                               Width="155px" Visible="false"> 
                                </asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtcashreceipt_date" WatermarkText="dd/MM/yyyy">
                           </ajax:TextBoxWatermarkExtender>
                        </td>
                       
                    </tr>
                    <caption>
                        <br />
            </caption>
       </table>
      </ContentTemplate>
      </asp:UpdatePanel>
      
 
 
</div>

    <div id="voucher_status" runat="server">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="true" UpdateMode="Conditional"
            ChildrenAsTriggers="false" >
            <ContentTemplate>
        <table width="800px" id="AccountsVoucher2" runat="server" border="1" style="border-collapse:collapse; border-color: #E6E6E6 #E6E6E6 #CCCCCC" cellspacing="5" cellpadding="5">
    
                <%-- <tr style="background-color:#f3f3f3">
                       <td style="width: 120px">
                         <asp:Label ID="Label5" runat="server" Text="Summary" CssClass="headlabel">
                          </asp:Label>
                      </td>
                </tr>--%>
                 <tr >
                       <td style="width: 120px">
                         <asp:Label ID="Label27" runat="server" Text="Narration" CssClass="headlabel">
                          </asp:Label>
                       </td>
                       <td>
                           <asp:TextBox ID="txt_narration" runat="server" TextMode="MultiLine" 
                               Width="300px" Height="50px"></asp:TextBox>
                       </td>
                 </tr>
                 <tr >
                       <td >
                       <asp:Label ID="Label29" runat="server" Text="Voucher Status" CssClass="headlabel">
                          </asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="drpvoucher_status"  runat="server" Width="150px">
                         </asp:DropDownList>
                         </td>

               </tr>
         </table>
 </ContentTemplate>
        </asp:UpdatePanel>
  </div>

    <div id = "Buttons" runat="server">
     <%--   <asp:Button ID="Button2" Text="Save" runat="server" CssClass="BtnStyle"  
          Width="100px" onclick="btnSave_Click" ValidationGroup="required"  />--%>
        <asp:Button ID="Button2" runat="server" Text="Save" Width="100px" OnClick="btnsave_Click" />
          </div>


</asp:Content>
