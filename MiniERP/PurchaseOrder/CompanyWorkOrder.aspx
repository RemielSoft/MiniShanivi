<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ERP.Master" AutoEventWireup="true"
    CodeBehind="CompanyWorkOrder.aspx.cs" Inherits="MiniERP.PurchaseOrder.CompanyWorkOrder" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:tabcontainer id="TabContainer1" runat="server" activetabindex="0">
        <asp:TabPanel runat="server" HeaderText="Company Work Order" ID="TabPanel1">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Company Work Order</h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Contract Date
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextWorkOrderDate" CssClass="TextBox" runat="server"></asp:TextBox>
                                        <asp:LinkButton ID="ImageButton2" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextWorkOrderDate"
                                            PopupButtonID="ImageButton2">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="center">
                                        Contract Number
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox15" CssClass="TextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Work Order Description
                                    </td>
                                    <td colspan="3" class="center">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                        
                                        
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <hr />
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Amount
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox2" CssClass="TextBox" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td class="center">
                                        Work Order Number
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox3" CssClass="TextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Area
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox7" CssClass="TextBox" runat="server"></asp:TextBox>
                                        
                                    </td>
                                    <td class="center">
                                        Location
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox12" CssClass="TextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                
                            </tbody>
                        </table>
                        <br />
                        <div class="Button_align">
                            <asp:Button ID="btnAdd1" runat="server" Text="Add" CssClass="button_color action" />
                        </div>
                        <br />
                        <br />
                        <table class="mGrid">
                            <tr>
                                <th>
                                    Work Order Number
                                </th>
                                <th>
                                    Amount (INR)
                                </th>
                                <th style=" width:50px;">
                                    Action
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    WO-001
                                </td>
                                <td>
                                    200,000
                                </td>
                                <td>
                                    <asp:Image ID="Image9" runat="server" ToolTip="Edit" CssClass="button icon145 " />
                                    <asp:Image ID="Image11" runat="server" ToolTip="Delete" CssClass="button icon186 " />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    WO-002
                                </td>
                                <td>
                                    300,000
                                </td>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ToolTip="Edit" CssClass="button icon145 " />
                                    <asp:Image ID="Image2" runat="server" ToolTip="Delete" CssClass="button icon186 " />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="text-align: right; font-weight: bold;">
                                    Total
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="TextBox6" Text="500,000 INR" CssClass="width70" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-bordered table-striped table-condensed">
                            <tbody>
                                <tr>
                                    <td class="center">
                                        Service Tax
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox8" CssClass="TextBox" runat="server"></asp:TextBox>
                                        %
                                    </td>
                                    <td class="center">
                                        VAT
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox9" CssClass="TextBox" runat="server"></asp:TextBox>
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        CST (with C Form)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox10" CssClass="TextBox" runat="server"></asp:TextBox>
                                        %
                                    </td>
                                    <td class="center">
                                        CST (Without C Form)
                                    </td>
                                    <td class="center">
                                        <asp:TextBox ID="TextBox17" CssClass="TextBox" runat="server"></asp:TextBox>
                                        %
                                    </td>
                                </tr>
                                <tr>
                                    <td class="center">
                                        Freight
                                    </td>
                                    <td colspan="3" class="center">
                                        <asp:TextBox ID="TextBox11" CssClass="TextBox" runat="server"></asp:TextBox>
                                        INR
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        Total Discount
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" Width="120px" runat="server">
                                            <asp:ListItem>Fix</asp:ListItem>
                                            <asp:ListItem>Percentage</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TextBox19" Width="120px" runat="server"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Text="%,INR"></asp:Label>
                                    </td>
                                    <td>
                                        Total Net Value
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox20" CssClass="TextBox" runat="server"></asp:TextBox>
                                        INR
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <div class="Button_align">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button_color action red" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button_color action green" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Bank Guarantee">
            <ContentTemplate>
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            Bank Guarantee</h2>
                    </div>
                    <div class="box-content">
                         <table class="table table-bordered table-striped table-condensed">
                        <tbody>
                            
                            <tr>
                                <td class="center">
                                    Start Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextStartDate1" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="ImageButton1" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextStartDate1"
                                        PopupButtonID="ImageButton1">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="center">
                                    End Date
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextEndDate1" CssClass="TextBox" runat="server"></asp:TextBox>
                                    <asp:LinkButton ID="ImageButton21" runat="server" ToolTip="Select Date" CssClass="Calender icon175" ></asp:LinkButton>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TextEndDate1"
                                        PopupButtonID="ImageButton21">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Amount
                                 </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox4" CssClass="TextBox" runat="server" ></asp:TextBox>
                                </td>
                                <td class="center">
                                    Bank Name
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox5" CssClass="TextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="center">
                                    Upload Document
                                </td>
                                <td class="center">
                                    <asp:FileUpload ID="FileUpload1" CssClass="upload" runat="server" />
                                </td>
                                <td class="center">
                                    Remarks
                                </td>
                                <td class="center">
                                    <asp:TextBox ID="TextBox51" runat="server" CssClass="mlttext" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="Button_align">
                        <asp:Button ID="Button1" runat="server" Text="Reset" CssClass="button_color action red" />
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button_color action" />
                    </div>
                    <br /><br />
                    <table class="mGrid">
                        <tr>
                           
                            
                            <th>
                                Start Date
                            </th>
                            <th>
                                End Date
                            </th>
                            <th>
                                Amount
                            </th>
                            <th>
                                Bank Name
                            </th>
                            <th>
                                Upload Document
                            </th>
                            <th>
                                Remarks</th>
                            <th style=" width:50px;">
                                Action
                            </th>
                        </tr>
                        <tr>
                            
                            <td>
                                12-jan-2013
                            </td>
                            <td>
                                12-feb-2013
                            </td>
                            <td>
                                4000
                            </td>
                            <td>
                                HDFC
                            </td>
                            <td>
                                N/A
                            </td>
                            <td>
                                N/A
                            </td>
                            <td>
                                <asp:Image ID="Image3" runat="server" ToolTip="Edit" CssClass="button icon145 " />
                                    <asp:Image ID="Image4" runat="server" ToolTip="Delete" CssClass="button icon186 " />
                            </td>
                        </tr>
                    </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:tabcontainer>
    
</asp:Content>
