<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/CRMMaster.Master"
    AutoEventWireup="true" CodeBehind="GitDetails.aspx.cs" Inherits="CRM.WebApp.Views.GIT.GitDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ MasterType VirtualPath="~/Views/Shared/CRMMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphIncludes" runat="server">
    <link href="../Shared/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Shared/Javascripts/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphPageContent" runat="server">
    <style>
       
        .headlabel
        {
            font-size: "40px";
            font-weight: normal;
            font-family: Verdana;
        }
    </style>

       <script language="javascript" type="text/javascript">

           var sessionTimeout = "<%= Session.Timeout %>";

           var sTimeout = parseInt(sessionTimeout) * 60 * 1000;
           setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
        </script>

    <div>
        <asp:Label ID="Label55" runat="server" Text="GIT Package Details" class="pageTitle"
            Width="200px" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblhotel" runat="server" Text="Hotels" class="pageTitle" Width="100px"
            Style="font-weight: normal; font-size: 16px; font-family: Verdana"></asp:Label>
        <br />
        <%--------------------------------HOTELS---------------------------------------------%>
       <div id="divhotels" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div id="divHotel1" runat="server" class="pageTitle" visible="false" style = "padding-right: 10px;">
            <asp:UpdatePanel ID="upHotel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel1" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel1" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server" ></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Visible="false" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server" Visible="false" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Visible="false" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  Visible="false"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Visible="false" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" Visible="false"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm1_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                   
                    <br />
                    <asp:Button ID="btnAddHotel1" runat="server" Text = "Add Hotel" OnClick="btnAddHotel_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            
            <br />

        </div>
        
        <div id="divHotel2" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel2" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel2" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName2_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype2_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server" OnTextChanged="txtCheckInDate2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Visible="false" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  Visible="false"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Visible="false" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  Visible="false"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Visible="false" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" Visible="false"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm2_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel2" runat="server" Text = "Add Hotel" OnClick="btnAddHotel2_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
         
        <div id="divHotel3" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel3" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel3" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName3_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype3_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate3_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm3_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel3" runat="server" Text = "Add Hotel" OnClick="btnAddHotel3_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
         
        <div id="divHotel4" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel4" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel4" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName4_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype4_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate4_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm4_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel4" runat="server" Text = "Add Hotel" OnClick="btnAddHotel4_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        
        <div id="divHotel5" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel5" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel5" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName5_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype5_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate5_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove5_Click" OnCheckedChanged="rdoConfirm5_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate5_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel5" runat="server" Text = "Add Hotel" OnClick="btnAddHotel5_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
         
        <div id="divHotel6" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel6" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel6" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName6_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype6_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate6_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm6_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel6" runat="server" Text = "Add Hotel" OnClick="btnAddHotel6_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
          
        <div id="divHotel7" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel7" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel7" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName7_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype7_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate7_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm7_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel7" runat="server" Text = "Add Hotel" OnClick="btnAddHotel7_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
          
        <div id="divHotel8" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel8" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel8" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName8_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype8_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate8_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm8_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel8" runat="server" Text = "Add Hotel" OnClick="btnAddHotel8_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
        
        <div id="divHotel9" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel9" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel9" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName9_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype9_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate9_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm9_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel9" runat="server" Text = "Add Hotel" OnClick="btnAddHotel9_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>

        <div id="divHotel10" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upHotel10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblHotel10" runat="server" Text="" CssClass="headlabel"></asp:Label>&nbsp;<span
                        class="error">*</span>
                    <br />
                     <asp:GridView ID="GridHotel10" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel" runat="server" Text="Hotels" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotelName" runat="server" OnSelectedIndexChanged="drpHotelName10_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblRoomType" runat="server" Text="Room Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpRoomType" runat="server" OnSelectedIndexChanged="drpRoomtype10_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckInDate" runat="server" Text="Check in Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckInDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtCheckInDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCheckOutDate" runat="server" Text="Check out Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCheckOutDate" runat="server" OnTextChanged="txtCheckOutDate10_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtCheckOutDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNights" runat="server" Text="No of Nights" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNights" runat="server"></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAddToCart" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddToCart" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblSingleRoom" runat="server" Text="No of Single Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSingleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDoubleRoom" runat="server" Text="No of Double Room" CssClass="lblstyleGIT"  ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoubleRoom" runat="server"  ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="110px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTripleRoom" runat="server" Text="No of Triple Room" CssClass="lblstyleGIT" ></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTripleRoom" runat="server" ></asp:TextBox>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRemove" Text = "Remove" OnClick="btnRemove10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnHotelRate" Text = "Rate" OnClick="btnHotelRate10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdoConfirm" runat="server" GroupName="HotelConfirm" Visible="false" OnCheckedChanged="rdoConfirm10_CheckedChanged" AutoPostBack="true"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnAddHotel10" runat="server" Text = "Add Hotel" OnClick="btnAddHotel10_Click"/>


                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
         

        <div id="HotelConfirmpopup">
         <asp:UpdatePanel ID="Updateconfirm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>

                 <table id="Table1" runat="server">
                    <tr>
                        <td>
                            <asp:Button ID="btnConfirmHotel" runat="server" Text="Confirm" Visible="false" AutoPostBack="true"  OnClick="btnConfirm_click"/>
                          <%--  <asp:Button ID="Button13" runat="server" Text="Wait List" Visible="false" OnClick="Button3_onclick" />
                            <asp:Button ID="Button14" runat="server" Text="Change Of Hotel" Visible="false" />--%>
                        </td>
                    </tr>
                </table>
                <%-- <ajax:modalpopupextender id="PopEx_lnkBtnChangePreference" runat="server" backgroundcssclass="modalPopupBackground"
                    popupcontrolid="pnlCompanyRoleSelection" targetcontrolid="btnConfirmHotel" drag="true"
                    popupdraghandlecontrolid="pnlCompanyRoleSelectionHeader" cancelcontrolid="ImageButton1">
                </ajax:modalpopupextender>--%>
                 <asp:Panel ID="pnlCompanyRoleSelection" runat="server" CssClass="modalPopup" Width="350px"
                    Style="display: none;">
                    <asp:Panel ID="Panel1" runat="server" Width="350px">
                        <fieldset style="background-color: White">
                            <asp:Panel ID="pnlCompanyRoleSelectionHeader" runat="server" CssClass="panelhead">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTitleAlert" runat="server" Text="Please Enter Reconfirmation Date"
                                                ForeColor="#FEFEFE" Font-Size="15px"></asp:Label>
                                        </td>
                                        <td style="width: 17px;" align="center" valign="middle">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                                Style="cursor: pointer;" ToolTip="Close" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label19" runat="server" Text="Reconfirmation Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="txtreconfirmdate" runat="server" Width="100px" 
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:Label ID="labelformet" runat="server" Text="dd/mm/yyyy"></asp:Label>
                                        <ajax:calendarextender id="CalendarExtenderReconfirmation" runat="server" targetcontrolid="txtreconfirmdate"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelerror" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label56" runat="server" Text="Confirmation Number" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="txtconfirmnumber" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:Label ID="Label62" runat="server" Text="Payment Date" CssClass="lblstyle"></asp:Label>&nbsp;<span
                                            class="error">*</span>
                                    </td>
                                    <td style="width: 55%">
                                        <asp:TextBox ID="txtpayment" runat="server" Width="100px" 
                                            ></asp:TextBox>
                                        <asp:Label ID="label76" runat="server" Text="dd/mm/yyyy"></asp:Label>
                                        <ajax:calendarextender id="CalendarExtenderPayment" runat="server" targetcontrolid="txtpayment"
                                            format="dd/MM/yyyy" popupbuttonid="Image1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="label63" runat="server" Text=""></asp:Label>
                                    </td>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Reconfirmation date is required"
                                            ControlToValidate="txtreconfirmdate" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Confirmation Number is required"
                                            ControlToValidate="txtconfirmnumber" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 45%">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Payment date is required"
                                            ControlToValidate="txtpayment" Display="Static" ValidationGroup="popup" CssClass="lblstyle"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnhotelconfirm" runat="server" Text="Confirm" ValidationGroup="popup" OnClick="btnConfirmHotel_click" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
                 </ContentTemplate>
        </asp:UpdatePanel>
         </div>

        </td>
        </tr>
        </table>
        </div>
        <br />
        <br />  
        <%--------------------------------TRANSPORT PACKAGE---------------------------------------------%>

        <asp:Label ID="Label20" runat="server" Text="Trasport Package" Style="font-weight: normal; font-size: 16px; font-family: Verdana" class="pageTitle"></asp:Label>&nbsp;<span class="error">*</span>

         <div id="divTransportPackage" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div class="pageTitle" style = "padding-right: 10px;">
            <asp:UpdatePanel ID="uptransferPackage" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="900px">
                        <tr>
                            <td>
                                <asp:GridView ID="GridTrasport" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" OnRowDataBound="dlhoteldetails_ItemDataBound">
                                    <Columns>
                                        <asp:TemplateField ControlStyle-Width = "50px" ItemStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label21" runat="server" Text="Sr No" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTPsrno" runat="server" CssClass="lblstyleGIT" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width = "250px" ItemStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label22" runat="server" Text="Package Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTPpackageDetails" runat="server" Text='<%# Bind("NAME") %>' CssClass="lblstyle"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ControlStyle-Width = "100px" >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDate" runat="server"  Visible="false"></asp:TextBox>
                                                 <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ControlStyle-Width = "100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:radtimepicker id="rdtpTime" runat="server"  >        
                                                <TimeView TimeFormat="h:mm tt"></TimeView>
                                                <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                                </telerik:radtimepicker>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ControlStyle-Width = "50px" ItemStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label26" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rbtnTPselect" runat="server" GroupName="TransferPackage" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField ControlStyle-Width = "100px" ItemStyle-Width="100px">
                                            <HeaderTemplate>
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID = "btnTrasportRate" runat="server" Text = "Rate" OnClick="btnTrasportRate_Click"/> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width = "100px" ItemStyle-Width="100px">
                                            <HeaderTemplate>
                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID = "btnTrasportTime" runat="server" Text = "Save Date/Time" OnClick="btnTrasportTime_Click" Visible="false"/> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_priceid" runat="server" Text='<%# Bind("GIT_PACKAGE_ID") %>'
                                                    CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltp_detialid" runat="server" Text='<%# Bind("GIT_TRANSFER_PACKAGE_DETAIL_ID") %>'
                                                    CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </td>
        </tr>
        </table>
        </div>

        <br />
        <br />

          <%--------------------------------MEALS---------------------------------------------%>

         <asp:Label ID="Label2" runat="server" Text="Meals" Style="font-weight: normal; font-size: 16px; font-family: Verdana" class="pageTitle"></asp:Label><%--&nbsp;<span class="error">*</span>--%>

         <div id="div2" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div id="divMeals1" runat="server" class="pageTitle" visible="false" style = "padding-right: 10px;">
            <asp:UpdatePanel ID="upMeal1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity1" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal1" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvelue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant1_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px" >
                                <HeaderTemplate >
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealAdd1" OnClick="btnMealAdd1_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
             <br />
        </div>       

        <div id="divMeals2" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity2" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal2" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant2_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealAdd2" OnClick="btnMealAdd2_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
             <br />
        </div>
       
        <div id="divMeals3" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity3" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal3" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant3_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate3_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd3" OnClick="btnMealAdd3_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
              <br />
        </div>
         
        <div id="divMeals4" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity4" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal4" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant4_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate4_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd4" OnClick="btnMealAdd4_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
              <br />
        </div>
         
        <div id="divMeals5" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity5" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal5" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant5_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove5_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate5_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd5" OnClick="btnMealAdd5_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
           
        <div id="divMeals6" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity6" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal6" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant6_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate6_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd6" OnClick="btnMealAdd6_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
           
        <div id="divMeals7" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity7" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal7" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant7_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate7_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd7" OnClick="btnMealAdd7_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
             <br />
        </div>
          
        <div id="divMeals8" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity8" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal8" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant8_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate8_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd8" OnClick="btnMealAdd8_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
              <br />
        </div>
         
        <div id="divMeals9" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity9" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal9" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant9_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate9_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd9" OnClick="btnMealAdd9_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
               <br />
        </div>
       
        <div id="divMeals10" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upMeal10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblMealCity10" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridMeal10" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblvalue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpResturant" runat="server" OnSelectedIndexChanged="drpResturant10_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblmealtype" runat="server" Text="Meal Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpMealType" runat="server">
                                    </asp:DropDownList>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMeal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVage" runat="server" Text="Veg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVage" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblNonVage" runat="server" Text="NonVeg" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNonVag" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblJain" runat="server" Text="Jain" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtJain" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnMealRemove" Text = "Remove" OnClick="btnMealRemove10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnMealRate" Text = "Save" runat="server" OnClick="btnMealRate10_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Meal" runat="server" ID="btnMealsAdd10" OnClick="btnMealAdd10_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
              <br />
        </div>

        </td>
        </tr>
        </table>
        </div>
          <br />
        <br />
          <%--------------------------------CONFERENCE---------------------------------------------%>

         <asp:Label ID="Label4" runat="server" Text="Conference" Style="font-weight: normal; font-size: 16px; font-family: Verdana" class="pageTitle" ></asp:Label><%--&nbsp;<span class="error">*</span>--%>

         <div id="div3" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div id="divConf1" runat="server" class="pageTitle" visible="false" style = "padding-right: 10px;">
            <asp:UpdatePanel ID="upConf1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity1" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf1" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf1" OnClick="btnConf1_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
        <div id="divConf2" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity2" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf2" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="Button1" OnClick="btnConf2_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
      
        <div id="divConf3" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity3" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf3" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate3_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf3" OnClick="btnConf3_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div id="divConf4" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity4" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf4" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate4_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf4" OnClick="btnConf4_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div id="divConf5" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity5" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf5" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove5_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate5_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf5" OnClick="btnConf5_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
        <div id="divConf6" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity6" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf6" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate6_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf6" OnClick="btnConf6_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         
        <div id="divConf7" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity7" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf7" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate7_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf7" OnClick="btnConf7_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div id="divConf8" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity8" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf8" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate8_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf8" OnClick="btnConf8_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
        <div id="divConf9" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity9" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf9" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate9_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf9" OnClick="btnConf9_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
        <div id="divConf10" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upConf10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblConfCity10" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridConf10" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblConferenceType" runat="server" Text="Conference Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpConfType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConf" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnConfRemove" Text = "Remove" OnClick="btnConfRemove10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnConferenceRate" Text = "Save" runat="server" OnClick="btnConferenceRate10_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Conference" runat="server" ID="btnConf10" OnClick="btnConf10_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        </td>
        </tr>
        </table>
        </div>

          <br />
        <br />
         <%--------------------------------GALA DINNER---------------------------------------------%>

         <asp:Label ID="Label6" runat="server" Text="Gala Dinner" Style="font-weight: normal; font-size: 16px; font-family: Verdana" class="pageTitle" ></asp:Label><%--&nbsp;<span class="error">*</span>--%>

         <div id="div4" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
       <div id="divGala1" runat="server" class="pageTitle" style = "padding-right: 10px;" >
            <asp:UpdatePanel ID="upGala1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity1" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala1" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala" OnClick="btnGala_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
       <div id="divGala2" runat="server" class="pageTitle" visible= "false">
            <asp:UpdatePanel ID="upGala2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity2" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala2" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="Button2" OnClick="btnGala2_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
       <div id="divGala3" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity3" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala3" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate3_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala3" OnClick="btnGala3_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         
       <div id="divGala4" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity4" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala4" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate4_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala4" OnClick="btnGala4_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
       <div id="divGala5" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity5" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala5" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove5_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate5_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala5" OnClick="btnGala5_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

       <div id="divGala6" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity6" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala6" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lbleGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate6_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala6" OnClick="btnGala6_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
       <div id="divGala7" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity7" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala7" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate7_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala7" OnClick="btnGala7_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         
       <div id="divGala8" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity8" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala8" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate8_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala8" OnClick="btnGala8_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
       <div id="divGala9" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity9" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala9" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate9_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala9" OnClick="btnGala9_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
       
       <div id="divGala10" runat="server" class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upGala10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
                <ContentTemplate>
                    <asp:Label ID="lblGalaCity10" runat="server" Text="" CssClass="headlabel"></asp:Label>
                    <br />
                    <asp:GridView ID="gridGala10" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="700px" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField ControlStyle-Width="200px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblVenue" runat="server" Text="Venue" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpHotel" runat="server">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField ControlStyle-Width="300px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblGalaType" runat="server" Text="Gala Dinner Type" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpGalaType" runat="server">
                                    </asp:DropDownList>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <telerik:radtimepicker id="rdtpTime" runat="server"  width="90px">        
                                    <TimeView TimeFormat="h:mm tt"></TimeView>
                                    <DateInput DisplayDateFormat="hh:mm tt"></DateInput>
                                    </telerik:radtimepicker>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHotel6" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGala" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnGalaRemove" Text = "Remove" OnClick="btnGalaRemove10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Button ID="btnGalaDinnerRate" Text = "Save" runat="server" OnClick="btnGalaDinnerRate10_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                   <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button Text="Add Gala Dinner" runat="server" ID="btnGala10" OnClick="btnGala10_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </td>
        </tr>
        </table>
        </div>

           <br />
        <br />
         <%--------------------------------SITE SEEING---------------------------------------------%>

         <asp:Label ID="Label13" runat="server" Text="Sightseeing" Style="font-weight: normal; font-size: 16px; font-family: Verdana" class="pageTitle"></asp:Label>

         <div id="div5" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div id="divSite1" runat="server"  class="pageTitle" visible="false" style = "padding-right: 10px;">
           
        
            <asp:UpdatePanel ID="upSite1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity1" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite1" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                      
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"  OnSelectedIndexChanged="drpSite_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                                <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                               <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="btnSiteAdd" OnClick="btnSiteAdd_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </div>
    
        <div id="divSite2" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity2" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite2" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"  OnSelectedIndexChanged="drpSite2_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                      


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove2_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate2_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="btnSiteAdd2" OnClick="btnSiteAdd2_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite3" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity3" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite3" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"   OnSelectedIndexChanged="drpSite3_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove3_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate3_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button3" OnClick="btnSiteAdd3_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite4" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity4" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite4" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"   OnSelectedIndexChanged="drpSite4_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                     


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove4_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>


                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate4_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button4" OnClick="btnSiteAdd4_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite5" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity5" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite5" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"  OnSelectedIndexChanged="drpSite5_SelectedIndexChanged" AutoPostBack="true" />
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                         
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove5_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate5_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button5" OnClick="btnSiteAdd5_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite6" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity6" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite6" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"   OnSelectedIndexChanged="drpSite6_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove6_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate6_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button6" OnClick="btnSiteAdd6_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite7" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity7" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite7" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"   OnSelectedIndexChanged="drpSite7_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove7_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate7_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button7" OnClick="btnSiteAdd7_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite8" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity8" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite8" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"   OnSelectedIndexChanged="drpSite8_SelectedIndexChanged" AutoPostBack="true"/>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                      

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove8_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate8_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button8" OnClick="btnSiteAdd8_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite9" runat="server"  class="pageTitle" visible="false">
           
        
            <asp:UpdatePanel ID="upSite9" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity9" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite9" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                                        <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"  OnSelectedIndexChanged="drpSite9_SelectedIndexChanged" AutoPostBack="true" />
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                      

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                           <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove9_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate9_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button9" OnClick="btnSiteAdd9_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div id="divSite10" runat="server"  class="pageTitle" visible="false">
            <asp:UpdatePanel ID="upSite10" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:Label ID="lblSiteCity10" runat="server" Text="" CssClass="headlabel" Width="550px"
                ></asp:Label>

           
            <br />
                  
                                <asp:GridView ID="gridSite10" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false" Width="700px">
                                    <Columns>
                                   
                             <asp:TemplateField  ControlStyle-Width="300px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Sightseeing Details" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteDetails" runat="server"  OnSelectedIndexChanged="drpSite10_SelectedIndexChanged" AutoPostBack="true" />
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteDate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSiteDate" Width = "150px"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calSite1" runat="server" targetcontrolid="txtSiteDate" 
                                                    format="dd/MM/yyyy" popupbuttonid="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSiteTime" runat="server" Text="Time" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSiteTime" runat="server" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                             <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblstyle4" runat="server" Text="Add to Cart" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSiteSelect" runat="server" GroupName="sightseeingBKK" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchildRate" runat="server" Text="Child Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtChildRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    <asp:Label ID="lblAdultRate" runat="server" Text="Adult Rate" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdultRate" runat="server"></asp:TextBox>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                    
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnSiteRemove" Text = "Remove" OnClick="btnSiteRemove10_Click"/>
                                </ItemTemplate>
                            </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnSiteRate" runat="server" Text="Save" Width="100px" OnClick="btnSiteRate10_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="100px">
                                <HeaderTemplate>
                                   <asp:Label ID="lblReconfirmedDate" runat="server" Text="Payment Date" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:TextBox ID="txtReconfirmedDate" runat="server" Visible="false"></asp:TextBox>
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtReconfirmedDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                     <br />
                     <asp:Button Text="Add Sightseeing" runat="server" ID="Button10" OnClick="btnSiteAdd10_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        
        </td>
        </tr>
        </table>
        </div>

         <br />
        <br />
        <%--------------------------------ADDITIONAL SERVICES---------------------------------------------%>
        <asp:Label ID="lbladdservice" runat="server" Text="Additional Services" Style="font-weight: normal; font-size: 16px;
            font-family: Verdana" class="pageTitle"></asp:Label>

            <div id="divAddServ" runat="server" class="pageTitle" >
        <table border = "1" class="pageTitle" >
        <tr>
        <td>
        <div id="divAddServices" runat="server"  class="pageTitle" style = "padding-right: 10px;"> 
          
            <br />
        
          <asp:UpdatePanel ID="upAddServices" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                   
               
                                <asp:GridView ID="gridAddServices" runat="server" AutoGenerateColumns="False" SkinID="sknSubGrid"
                                    AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="200px" ControlStyle-Width="250px" >
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsrno" runat="server" Text="Services" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtServices"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="250px" ItemStyle-Width="150px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldetalis" runat="server" Text="Supplier" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpSupplier" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text="Date" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtenderAdService" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNoPax" runat="server" Text="No of PAX" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNoPax"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblNetPrice" runat="server" Text="Net Price (THB)" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNetPrice"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSellPrice" runat="server" Text="Sell Price (THB)" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtSellPrice"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblFrom" runat="server" Text="From" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtFrom"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTo" runat="server" Text="To" CssClass="lblstyleGIT"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtTo"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblchkad" runat="server" Text="Aditional" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkAditional" Visible="false"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                       <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblpassenger" runat="server" Text="No Of Passanger" CssClass="lblstyleGIT" Visible="false"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNoOfPassanger" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
                        <br />
                         <asp:Button Text="ADD" runat="server" ID="btnAddService" OnClick="btnAddService_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />

        </div>
        </td>
        </tr>
        </table>
        </div>
        <br />
        <br />

        <div>
        <asp:UpdatePanel ID="upExRate" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional"> 
        <ContentTemplate>   
            <table>
            <tr>
            <td>
        <asp:Label ID="lblExRate" runat="server" Text="Exchange Rate"></asp:Label></td>
        <td><asp:TextBox ID="txtExRate" runat="server" Width="100px"></asp:TextBox></td>
        </tr>

         <tr>
            <td>
        <asp:Label ID="lblmarginAmount" runat="server" Text="Margin Amount (THB)"></asp:Label></td>
        <td><asp:TextBox ID="txtMarginAmount" runat="server" Width="100px"></asp:TextBox></td>
        </tr>
        </table>

        <br />

        <table>
        <tr>
        <td>
            <asp:Button runat="server" Text="Request for Quote" ID="btnQuoteRequest" Width="150px" OnClick="btnQuoteRequest_Click"/>
            <asp:Button runat="server" Text="Generate Quote" ID="btnGenerateQuote" Width="150px" OnClick="btnGenerateQuote_Click"/>
            <asp:Button runat="server" Text="Book" ID="btnBook" Width="150px" Visible="false"  OnClick="btnBook_Click"/>
            <asp:Button runat="server" Text="Confirm Payment Date" ID="btnconformPaymentdate" Width="150px" Visible = "false"  OnClick="btnConfPaymentDate_Click"/>
            <asp:Button runat="server" Text="Download Quote" ID="btnDownloadQuote" Width="150px" OnClick="btnDownloadQuote_Click"  />
            <asp:Button runat="server" Text="Send Quote to Agent" ID="btnSendQuote" Width="150px" Visible="false"/>
            <%--<asp:Button runat="server" Text="Enter Flight Details" ID="btnflightdetails" Width="150px" OnClick="btnEnterFlightDetails_Click"/>--%>
            <asp:Button runat="server" Text="Reconfirm" ID="btnreconform" Width="150px" OnClick="btnReconform_Click"/>
            <asp:Button runat="server" Text="Save Rooms" ID="btnSaveRooms" Width="150px" OnClick="btnSaverooms_Click"/>
            <asp:Button runat="server" Text="Back" ID="btnback" Width="150px" OnClick="btnBack_Click" />
            <asp:Button runat="server" Text="Save Meal Type" ID="btnmealsave" Width="150px" OnClick="btnMealsSave_Click" Visible="false" />
            <asp:Button runat="server" Text="Save Passanger" ID="btnpessSave" Width="150px" OnClick="btnAdditionalSave_Click" Visible="false" />

            <asp:Button runat="server" Text="Request for Cancellation" ID="btnCancellationRequest" Width="150px" OnClick="btnCancellationRequest_Click" Visible="false" />
            <asp:Button runat="server" Text="Approve Cancellation" ID="btnApproveCancellation" Width="150px" OnClick="btnApproveCancellation_Click" Visible="false" />
            <asp:Button runat="server" Text="DisApprove Cancellation" ID="btnDisApproveCancellation" Width="150px" OnClick="btnDisApproveCancellation_Click" Visible="false" />
        </td>
        </tr>
        </table>

        <div id="AllCombinationPopup">
         <%--<asp:UpdatePanel ID="Upallcombination" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>--%>

             <table id="Table2" runat="server">
                    <tr>
                        <td>
                            <asp:Button ID="btnshowallcomb" runat="server" Text="Confirm" Visible = "false" />                         
                        </td>
                    </tr>
                </table>

                        
                         <asp:Panel ID="pnlAllcombinationSelection" runat="server" CssClass="modalPopup" Width="550px"
                    Style="display: none;">
                    <asp:Panel ID="Panel2" runat="server" Width="550px">
                        <fieldset style="background-color: White">
                            <asp:Panel ID="pnlAllcombinationSelectionHeader" runat="server" CssClass="panelhead">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="All Combination"
                                                ForeColor="#FEFEFE" Font-Size="15px"></asp:Label>
                                        </td>
                                        <td style="width: 17px;" align="center" valign="middle">
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                                Style="cursor: pointer;" ToolTip="Close" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                <asp:GridView ID="gvallcomb" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="520px">
                                <Columns>                            
                            
                            <asp:TemplateField ControlStyle-Width="500px">
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblallcombination" runat="server" text="<%#Bind('NAME') %>">                                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible = "false">
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="Name" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblallcombinationID" runat="server" text="<%#Bind('GIT_QUOTE_HOTEL_ID') %>">                                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-Width="20px">
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="Select" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID = "rbselect" runat = "server" GroupName="Popup" AutoPostBack ="true" OnCheckedChanged="BookCheckChanged"/>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                                         
                                  </Columns>
                                 </asp:GridView>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Button ID="btnpopupmail" runat="server" Text="Mail" OnClick="ButtonPopupmail_Click"/>
                                    </td>
                                </tr>
                            </table>                           
                            
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
         </div> 
         <div id="SelectSlab">
         <%--<asp:UpdatePanel ID="Upallcombination" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
            class="pageTitle">
            <ContentTemplate>--%>
                   <asp:Panel ID="pnlSelectSlab" runat="server" CssClass="modalPopup" Width="300px"
                    Style="display: none;">
                    <asp:Panel ID="Panel4" runat="server" Width="300px">
                        <fieldset style="background-color: White">
                            <asp:Panel ID="pnlSelectSlabHeader" runat="server" CssClass="panelhead">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="All Combination"
                                                ForeColor="#FEFEFE" Font-Size="15px"></asp:Label>
                                        </td>
                                        <td style="width: 17px;" align="center" valign="middle">
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Views/Shared/Images/close.png"
                                                Style="cursor: pointer;" ToolTip="Close" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%">
                                <tr>
                                <asp:GridView ID="gvSelectSlab" runat="server" AutoGenerateColumns="false" SkinID="sknSubGrid" Width="290px">
                                <Columns>                            
                             <asp:TemplateField Visible = "false">
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="slabid" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblslabid" runat="server" text="<%#Bind('GIT_TOUR_SLAB_ID') %>">                                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField >
                                <HeaderTemplate>
                                    <asp:Label ID="lblSelectSlab" runat="server" Text="Slab" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblallcombination" runat="server" text="<%#Bind('SLAB_ID') %>">                                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblpax" runat="server" Text="No of Pax" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofpax" runat="server" text="<%#Bind('NO_OF_PAX') %>">                                    
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label21" runat="server" Text="Select" CssClass="lblstyleGIT"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID = "rbSelectSlab" runat = "server" GroupName="Popup" AutoPostBack ="true" OnCheckedChanged="SlabCheckChanged"/>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                                         
                                  </Columns>
                                 </asp:GridView>
                                </tr>
                                <tr>
                                <td>
                               <br />
                                </tr>
                                </td>
                                 <tr>
                                    <td>
                                        <asp:Button ID="btnConfirmSlab" runat="server" Text="ConfirmSlab" OnClick="ButtonConfirmSlab_Click"/>
                                    </td>
                                </tr>
                            </table>                           
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
         </div>
        </ContentTemplate>
        <Triggers>
                                   <asp:PostBackTrigger ControlID="btnDownloadQuote" />
                                   </Triggers>
        </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdateProgress22" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="upExRate">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage22" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Updateconfirm">
            <ProgressTemplate>
                <div class="TransparentGrayBackground">
                </div>
                <div class="Sample6PageUpdateProgress">
                    <asp:Image ID="ajaxLoadNotificationImage222" runat="server" ImageUrl="~/Views/FIT/ajax-loader.gif"
                        AlternateText="" />
                    &nbsp;Please Wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        
         

    <%--FORM :- 2 --%>
    <form id="form2" runat="server" visible="false">
        <div>
            <rsweb:ReportViewer ID="rptViewer1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                Height="8.5in" Width="14in">
            </rsweb:ReportViewer>
        </div>
        </form>
</asp:Content>
