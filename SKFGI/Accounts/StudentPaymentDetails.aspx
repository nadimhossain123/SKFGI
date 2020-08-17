<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="StudentPaymentDetails.aspx.cs" Inherits="CollegeERP.Accounts.StudentPaymentDetails"
    Title="Student Payment Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function openPopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
        {                
            var popposition='left = 200, top=15, width=950,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
            var NewWindow = window.open(poplocation,'',popposition);                
            if (NewWindow.focus!=null){                        
            NewWindow.focus();                
        }        
     }
     
     function Validation()
        {
            var field = document.getElementById('<%=txtFromDate.ClientID%>');
            var field1 = document.getElementById('<%=txtToDate.ClientID%>');
            if (field.value == '' || field1.value == '') {
                 alert('Enter Date Range');
                 return false;
            }
            else { return true; }
        }
    
    function expandcollapse(obj)
    {
        var div = document.getElementById(obj);
        var img = document.getElementById('img' + obj);
        
        if (div.style.display == "none")
        {
            $(div).show("slow");
            img.src = "../Images/minus.gif";
            
        }
        else
        {
            $(div).hide("slow");
            img.src = "../Images/plus.gif";
        }
    } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            Student Payment Details</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <table width="80%" align="center" class="table">
        <tr>
            <td align="left" class="label" width="10%">
                Receipt No
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
            </td>
            <td align="left" class="label" width="5%">
                From
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                    PopupPosition="Right" Format="dd MMM yyyy" TargetControlID="txtFromDate" OnClientDateSelectionChanged=""
                    Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td align="left" class="label" width="5%">
                To
            </td>
            <td align="left" width="20%">
                <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                    PopupPosition="Right" Format="dd MMM yyyy" TargetControlID="txtToDate" OnClientDateSelectionChanged=""
                    Enabled="True">
                </asp:CalendarExtender>
            </td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClientClick="javascript:return Validation()"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="80%" align="center" class="table">
        <tr>
            <td align="center">
                <asp:GridView ID="dgvPaymentMaster" runat="server" AutoGenerateColumns="false" Width="100%"
                    AllowPaging="false" GridLines="None" DataKeyNames="PaymentId,company_id" OnRowDataBound="dgvPaymentMaster_RowDataBound"
                    OnRowDeleting="dgvPaymentMaster_RowDeleting" ShowFooter="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="javascript:expandcollapse('div<%# Eval("PaymentId") %>');">
                                    <img id="imgdiv<%# Eval("PaymentId") %>" width="11px" border="0" src="../Images/plus.gif" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MoneyReceiptNo" HeaderText="Receipt No" />
                        <asp:BoundField DataField="FeesBookNumber" HeaderText="Fees Book Number" />
                        <asp:BoundField DataField="name" HeaderText="Student Name" />
                        <%--<asp:BoundField DataField="Amount" HeaderText="Amount" />--%>
                        <asp:TemplateField  HeaderText="Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>'/>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                            <b>Total : <asp:Label ID="lblCashtotal" runat="server" /></b>
                                    </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Refund">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkIsRefund" runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IsRefund")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="Delete" ImageUrl="~/Images/delete_icon.gif"
                                    runat="server" OnClientClick="return confirm('Are You Sure?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print.gif" Width="17px"
                                    Height="17px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <tr>
                                    <td colspan="100%">
                                        <div id="div<%# Eval("PaymentId") %>" style="display: none; position: relative; left: 15px;
                                            overflow: auto; width: 95%">
                                            <asp:GridView ID="dgvPaymentDetails" Width="99%" AutoGenerateColumns="false" runat="server"
                                                DataKeyNames="PaymentDetailsId" AllowPaging="false" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="fees" HeaderText="Fees Head" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                </Columns>
                                                <HeaderStyle CssClass="HeaderStyle" />
                                                <RowStyle CssClass="RowStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="rowstyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <RowStyle CssClass="RowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClick="btnPrint_Click"
                    Visible="false" />&nbsp;
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" Text="Export To Excel"
                    OnClick="btnExportToExcel_Click" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>
