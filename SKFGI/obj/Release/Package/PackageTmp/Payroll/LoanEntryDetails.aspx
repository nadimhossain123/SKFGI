<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LoanEntryDetails.aspx.cs" Inherits="CollegeERP.Payroll.LoanEntryDetails" Title="Loan Entry Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Employee');
                return false;
            }
            else if (isValidDate(document.getElementById('<%=txtApplicationDate.ClientID%>').value) == false) {
                alert("Enter Loan Application Date in DD/MM/YYYY format");
                return false;
            }
            else if (isValidDate(document.getElementById('<%=txtSanctionDate.ClientID%>').value) == false) {
                alert("Enter Loan Sanction Date in DD/MM/YYYY format");
                return false;
            }
            else if (CompareDate() == false) {
                alert("Loan Application Date Should Be Less Than Loan Sanction Date");
                return false;
            }
            else if (isNumber(document.getElementById('<%=txtLoanAmount.ClientID%>').value) == false || parseFloat(document.getElementById('<%=txtLoanAmount.ClientID%>').value) == 0)
            {
                alert('Enter Loan Amount In Proper Format');
                return false;
            }
            else if (isNumber(document.getElementById('<%=txtTotalTerm.ClientID%>').value) == false || parseFloat(document.getElementById('<%=txtTotalTerm.ClientID%>').value) == 0)
            {
                alert('Enter Total Term In Proper Format');
                return false;
            }
            else if (isNumber(document.getElementById('<%=txtInterestRate.ClientID%>').value) == false)
            {
                alert('Enter Interest Rate In Proper Format');
                return false;
            }
            else {return true;}
        }
        function GetLoanAmt() 
        {

            var LoanAmount= document.getElementById('<%=txtLoanAmount.ClientID%>').value;
            var InterestAmt= document.getElementById('<%=txtInterestAmount.ClientID%>').value;
            var TotTerm =document.getElementById('<%=txtTotalTerm.ClientID%>').value;
            
            if (isNumber(LoanAmount) && isNumber(InterestAmt) && isNumber(TotTerm))
            {
                var EMIAmount =roundNumber((parseFloat(LoanAmount) + parseFloat(InterestAmt))/ TotTerm) ;            
                var RefundAmount =roundNumber(parseFloat(LoanAmount) + parseFloat(InterestAmt));
            
                document.getElementById('<%=txtRefundAmount.ClientID%>').value =RefundAmount
                document.getElementById('<%=txtEMIAmount.ClientID%>').value = EMIAmount
            }
              
        } 
        
        function InterestAmt() 
        {

            var LoanAmount= document.getElementById('<%=txtLoanAmount.ClientID%>').value;
            var InterestRate= document.getElementById('<%=txtInterestRate.ClientID%>').value;
            
            if (isNumber(LoanAmount) && isNumber(InterestRate))
            {
                document.getElementById('<%=txtInterestAmount.ClientID%>').value = roundNumber((LoanAmount * InterestRate)/100);
                GetLoanAmt();
            }
       }  
        
     function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
        } 
     
       function roundNumber(num) 
        {
	    var result = Math.round(num*Math.pow(10,2))/Math.pow(10,2);
	    return result;
        }   
     function isValidDate(s) {
        // format D(D)/M(M)/(YY)YY
        var dateFormat = /^\d{1,4}[\.|\/|-]\d{1,2}[\.|\/|-]\d{1,4}$/;

        if (dateFormat.test(s)) {
            // remove any leading zeros from date values
            s = s.replace(/0*(\d*)/gi, "$1");
            var dateArray = s.split(/[\.|\/|-]/);

            // correct month value
            dateArray[1] = dateArray[1] - 1;

            // correct year value
            if (dateArray[2].length < 4) {
                // correct year value
                dateArray[2] = (parseInt(dateArray[2]) < 50) ? 2000 + parseInt(dateArray[2]) : 1900 + parseInt(dateArray[2]);
            }

            var testDate = new Date(dateArray[2], dateArray[1], dateArray[0]);
            if (testDate.getDate() != dateArray[0] || testDate.getMonth() != dateArray[1] || testDate.getFullYear() != dateArray[2]) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
    
        function CompareDate() {
            var StartDate = document.getElementById('<%=txtApplicationDate.ClientID%>').value;
            var EndDate = document.getElementById('<%=txtSanctionDate.ClientID%>').value;
            var strSplitStartDate = StartDate.split('/')
            var myDateStart = new Date();
            myDateStart.setFullYear(strSplitStartDate[2], strSplitStartDate[1], strSplitStartDate[0]);

            var strSplitEndDate = EndDate.split('/')
            myDateEnd = new Date();
            myDateEnd.setFullYear(strSplitEndDate[2], strSplitEndDate[1], strSplitEndDate[0]);

            if (myDateStart > myDateEnd) {
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
		<h5>Loan Entry Details</h5>
    </div>
    
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <br /> 
            <uc3:Message ID="Message" runat="server" />
            <br />
            <div style="width:740px;">
                <table width="100%" align="center" class="table">
                <tr>
                    <td align="left" width="20%" class="label">Employee Name<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true" 
                            CssClass="dropdownList" Width="150px" DataValueField="EmployeeId" 
                            DataTextField="FullName" 
                            onselectedindexchanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">Employee Code</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Department</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">Designation</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Category</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="left" width="20%"></td>
                    <td align="left" width="30%"></td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Loan Application Date<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtApplicationDate" OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" width="20%" class="label">Loan Sanction Date<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtSanctionDate" runat="server" CssClass="textbox_required" Width="150px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtSanctionDate" OnClientDateSelectionChanged="" Enabled="True">
                        </asp:CalendarExtender>
                    </td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Loan Description</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Width="210px" Height="30px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Loan Amount<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="textbox_required" Width="150px" onkeyup="InterestAmt()"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtLoanAmount"></asp:FilteredTextBoxExtender>
                    </td>
                    <td align="left" width="20%" class="label">Total Term(Month)<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtTotalTerm" runat="server" CssClass="textbox_required" Width="150px" MaxLength="2" onkeyup="InterestAmt()"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Numbers" TargetControlID="txtTotalTerm"></asp:FilteredTextBoxExtender>
                    </td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Interest Rate (%)<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtInterestRate" runat="server" CssClass="textbox_required" Width="150px" MaxLength="5" onkeyup="InterestAmt()"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="ftb3" runat="server" FilterType="Custom" ValidChars="0123456789." TargetControlID="txtInterestRate"></asp:FilteredTextBoxExtender>
                    </td>
                    <td align="left" width="20%" class="label">Interest Amount</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtInterestAmount" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>  
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Total Refund Amount</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtRefundAmount" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                    <td align="left" width="20%" class="label">EMI Amount</td>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="textbox_disabled" Width="150px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%" class="label">Deduction Month<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlDeductionMonth" runat="server" CssClass="dropdownList" Width="150px">
                            <asp:ListItem Value="1" Text="JAN"></asp:ListItem>
                            <asp:ListItem Value="2" Text="FEB"></asp:ListItem>
                            <asp:ListItem Value="3" Text="MARCH"></asp:ListItem>
                            <asp:ListItem Value="4" Text="APRIL"></asp:ListItem>
                            <asp:ListItem Value="5" Text="MAY"></asp:ListItem>
                            <asp:ListItem Value="6" Text="JUN"></asp:ListItem>
                            <asp:ListItem Value="7" Text="JULY"></asp:ListItem>
                            <asp:ListItem Value="8" Text="AUG"></asp:ListItem>
                            <asp:ListItem Value="9" Text="SEP"></asp:ListItem>
                            <asp:ListItem Value="10" Text="OCT"></asp:ListItem>
                            <asp:ListItem Value="11" Text="NOV"></asp:ListItem>
                            <asp:ListItem Value="12" Text="DEC"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" width="20%" class="label">Deduction Year<span class="req">*</span></td>
                    <td align="left" width="30%">
                        <asp:DropDownList ID="ddlDeductionYear" runat="server" CssClass="dropdownList" Width="150px">
                            <asp:ListItem Value="2011" Text="2011"></asp:ListItem>
                            <asp:ListItem Value="2012" Text="2012"></asp:ListItem>
                            <asp:ListItem Value="2013" Text="2013"></asp:ListItem>
                            <asp:ListItem Value="2014" Text="2014"></asp:ListItem>
                            <asp:ListItem Value="2015" Text="2015"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="20%"></td>
                    <td align="left" width="30%"></td>
                    <td align="left" width="20%"></td>
                    <td align="left" width="30%">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                                OnClientClick="javascript:return Validation()" 
                            onclick="btnSave_Click"/>
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" 
                            onclick="btnCancel_Click" />
                    </td>  
                </tr>
                </table>
                
                <br />
                
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvLoan" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="false"
                                 DataKeyNames="LoanId" onrowdeleting="dgvLoan_RowDeleting" 
                                onrowediting="dgvLoan_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" />
                                    <asp:BoundField DataField="LoanTotalMonth" HeaderText="Total Term Month" />
                                    <asp:BoundField DataField="LoanInterestRate" HeaderText="Interest Rate" />
                                    <asp:BoundField DataField="LoanEMIAmount" HeaderText="EMI " />
                                    <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_icon.gif" CommandName="Delete" OnClientClick="return confirm('Are You Sure?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        
                            </Columns>
                        
                            <HeaderStyle CssClass="HeaderStyle"  />
	                        <RowStyle CssClass="RowStyle" />
	                        <EmptyDataRowStyle CssClass="EditRowStyle" />
	                        <AlternatingRowStyle CssClass="AltRowStyle" />
	                        <PagerStyle CssClass="PagerStyle" />
                        </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
