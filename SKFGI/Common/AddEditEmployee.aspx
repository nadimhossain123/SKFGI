<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="True" CodeBehind="AddEditEmployee.aspx.cs" Inherits="CollegeERP.Common.AddEditEmployee" Title="Employee Information" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
     function OneTextToOther() 
      {
        var Add=document.getElementById('<%=txtCorrespondesAdd.ClientID%>').value;
        document.getElementById('<%=txtPermanentAdd.ClientID%>').value=Add;
        
        var City=document.getElementById('<%=txtCorrespondesCity.ClientID%>').value;
        document.getElementById('<%=txtPermanentCity.ClientID%>').value=City;
        
        var State=document.getElementById('<%=txtCorrespondesState.ClientID%>').value;
        document.getElementById('<%=txtPermanentState.ClientID%>').value=State;
        
        var Pin=document.getElementById('<%=txtCorrespondesPIN.ClientID%>').value;
        document.getElementById('<%=txtPermanentPIN.ClientID%>').value=Pin;
      }
      
      function Validation()
      {
        if (document.getElementById('<%=txtFName.ClientID%>').value == '')
        {
            alert("Enter First Name");
            return false;
        }
        else if (document.getElementById('<%=txtLName.ClientID%>').value == '')
        {
            alert("Enter Last Name");
            return false;
        }
        else if (document.getElementById('<%=ddlCompany.ClientID%>').selectedIndex == 0)
        {
            alert("Please Select Company");
            return false;
        }
        else if (document.getElementById('<%=txtEmpCode_Entry.ClientID%>').value == '')
        {
            alert("Enter Employee Code");
            return false;
        }
        else if (isValidDate(document.getElementById('<%=txtDOB.ClientID%>').value) == false) {
            alert("Enter DOB in DD/MM/YYYY format");
            return false;
            }
        else if (document.getElementById('<%=txtCorrespondesAdd.ClientID%>').value == '')
        {
            alert("Enter Correspondes Address");
            return false;
        }
        else if (document.getElementById('<%=ddlPaymode.ClientID%>').selectedIndex == 0 && document.getElementById('<%=txtACNo.ClientID%>').value == '')
        {
            alert("Enter Bank A/C No");
            return false;
        }
        else if (document.getElementById('<%=txtEmail1.ClientID%>').value != '' && IsValidEmail(document.getElementById('<%=txtEmail1.ClientID%>').value) == false) {
            alert("Enter Email ID in Proper Format");
            return false;
            }
        else if (document.getElementById('<%=txtEmail2.ClientID%>').value != '' && IsValidEmail(document.getElementById('<%=txtEmail2.ClientID%>').value) == false) {
            alert("Enter Email ID in Proper Format");
            return false;
            }    
        else if (document.getElementById('<%=txtIssuedate.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtIssuedate.ClientID%>').value) == false) {
            alert("Enter Passport Issue Date in DD/MM/YYYY format");
            return false;
            }
        else if (document.getElementById('<%=txtExpiryDate.ClientID%>').value != '' && isValidDate(document.getElementById('<%=txtExpiryDate.ClientID%>').value) == false) {
            alert("Enter Passport Expiry Date in DD/MM/YYYY format");
            return false;
            }
       else {return confirm('Are You Sure');}         
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
    
        function IsValidEmail(s)
            {
            var filter =/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!filter.test(s)) {
                return false;
                }
                else
                {return true;}
             } 
             
    function openpopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=50, width=950,align=center, height=500,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
     
     function openRolepopup(poplocation, querystring, popheight, popwidth,poptop, popleft)
     {                
        var popposition='left = 200, top=15, width=850,align=center, height=640,menubar=no, scrollbars=yes, resizable=no';                
               
        var NewWindow = window.open(poplocation,'',popposition);                
        if (NewWindow.focus!=null){                        
        NewWindow.focus();                
        }        
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server"></asp:ToolkitScriptManager>

    <div class="title">
		<h5>Manage Employee</h5>
    </div>
<%--<asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>--%>
        
       <uc3:Message ID="Message" runat="server" />
        
        
        <h6 align="left" style="color:#00356A;">Basic Details</h6>
        <div style="width:820px;">
            
            
            <table width="100%" align="center" class="table">
                   <tr>
                        <td align="left" width="15%" class="label">First Name<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtFName" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Middle Name</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtMName" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Last Name<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtLName" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">DOB<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtDOB" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtDOB" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="15%" class="label">Gender</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlGender" runat="server" Width="110px" CssClass="dropdownList">
                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%" class="label">Marital Status</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="110px" CssClass="dropdownList">
                                <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
                                <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
                                <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="15%" class="label">Blood Group</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtBloodGroup" runat="server" CssClass="textbox" Width="110px" MaxLength="3"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Nationality</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlNationality" runat="server" Width="110px" CssClass="dropdownList">
                                <asp:ListItem Value="Indian" Text="Indian"></asp:ListItem>
                                <asp:ListItem Value="NRI" Text="NRI"></asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%" class="label">Cast</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtCast" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">Religion</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtReligion" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Company<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlCompany" runat="server" Width="110px" CssClass="dropdownList" DataValueField="CompanyId" DataTextField="CompanyName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%" class="label">Leave Manager</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlLeaveManager" runat="server" Width="110px" CssClass="dropdownList" DataValueField="EmployeeId" DataTextField="FullName">
                            </asp:DropDownList>
                        </td>
                    </tr>  
                    <tr>
                        <td align="left" width="15%" class="label">Claim Approver</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlClaimApprover" runat="server" Width="110px" CssClass="dropdownList" DataValueField="EmployeeId" DataTextField="FullName">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%" class="label">Photo</td>
                        <td align="left" width="18%" >
                            <asp:FileUpload ID="uploadImage" runat="server" />
                        </td>
                        <td align="left" width="15%" class="label">Emp Code<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtEmpCode_Entry" runat="server" CssClass="textbox_required" Width="110px"></asp:TextBox>
                        </td>
                    </tr>   
            </table>
        </div>
        
        
        
        
        
        
        <br />
        <h6 align="left" style="color:#00356A;">Address Details</h6>
        <div style="width:820px;">
            
            
            <table width="100%" align="center" class="table">
                   <tr>
                        <td align="left" width="15%" class="label">Correspondes Add<span class="req">*</span></td>
                        <td align="left" width="18%" colspan="3">
                            <asp:TextBox ID="txtCorrespondesAdd" runat="server" CssClass="textbox_required" Width="420px" Height="30px" TextMode="MultiLine" Style="resize: none" onkeyup="OneTextToOther();"></asp:TextBox>
                        </td>
                        
                        <td align="left" width="15%" class="label"></td>
                        <td align="left" width="18%">
                            
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">City</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtCorrespondesCity" runat="server" CssClass="textbox" Width="110px" onkeyup="OneTextToOther();"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">State</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtCorrespondesState" runat="server" CssClass="textbox" Width="110px" onkeyup="OneTextToOther();"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">PIN</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtCorrespondesPIN" runat="server" CssClass="textbox" Width="110px" MaxLength="6" onkeyup="OneTextToOther();"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb1" runat="server" FilterType="Numbers" TargetControlID="txtCorrespondesPIN"></asp:FilteredTextBoxExtender>
                        </td>
                        
                    </tr>
                    
                    
                    <tr>
                        <td align="left" width="15%" class="label">Permanent Add</td>
                        <td align="left" width="18%" colspan="3">
                            <asp:TextBox ID="txtPermanentAdd" runat="server" CssClass="textbox" Width="420px" Height="30px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        
                        <td align="left" width="15%" class="label"></td>
                        <td align="left" width="18%">
                            
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">City</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtPermanentCity" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">State</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtPermanentState" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">PIN</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtPermanentPIN" runat="server" CssClass="textbox" Width="110px" MaxLength="6"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftb2" runat="server" FilterType="Numbers" TargetControlID="txtPermanentPIN"></asp:FilteredTextBoxExtender>
                        </td>
                        
                    </tr>  
            </table>
        </div>
        
        
        
        
        <br />
       
        <h6 align="left" style="color:#00356A;">Contact Details</h6>
        <div style="width:820px;">
            
            
            <table width="100%" align="center" class="table">
                   <tr>
                        <td align="left" width="15%" class="label">Contact No (1)</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtContactNo1" runat="server" CssClass="textbox" Width="110px" MaxLength="15"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Contact No (2)</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtContactNo2" runat="server" CssClass="textbox" Width="110px" MaxLength="15"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Email Id (1)</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtEmail1" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">Email Id (2)</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtEmail2" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Country</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label"></td>
                        <td align="left" width="18%">
                            
                        </td>
                    </tr>    
            </table>
        </div>
        
        
        
        <br />
                
        <h6 align="left" style="color:#00356A;">Passport & Bank Details</h6>
        <div style="width:820px;">
            
            
            <table width="100%" align="center" class="table">
                   <tr>
                        <td align="left" width="15%" class="label">Passport No</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtPassportNo" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Place of Issue</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtPlaceOfIssue" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">Issue Date</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtIssuedate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtIssuedate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">Expiry Date</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate" OnClientDateSelectionChanged="" Enabled="True">
                            </asp:CalendarExtender>
                        </td>
                        <td align="left" width="15%" class="label">Paymode</td>
                        <td align="left" width="18%">
                            <asp:DropDownList ID="ddlPaymode" runat="server" CssClass="dropdownList" Width="110px">
                                <asp:ListItem Value="A/C TRANSFER" Text="A/C TRANSFER"></asp:ListItem>
                                <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                                <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="15%" class="label">Bank Name</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="15%" class="label">Branch Address</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtBranchAddress" runat="server" CssClass="textbox" Width="110px" Height="30px" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">A/C. No.<span class="req">*</span></td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtACNo" runat="server" CssClass="textbox" Width="110px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">IFS Code</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtIFSCode" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left" width="15%" class="label">Active</td>
                        <td align="left" width="18%">
                            <asp:CheckBox ID="ChkIsActive" runat="server" />
                        </td>
                        <td align="left" width="15%" class="label">Permanent</td>
                        <td align="left" width="18%">
                            <asp:CheckBox ID="ChkIsPermanent" runat="server" AutoPostBack="true" 
                                oncheckedchanged="ChkIsPermanent_CheckedChanged" />
                        </td>
                        <td align="left" width="15%" class="label">Contract Period</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtContractPeriod" runat="server" CssClass="textbox" Width="110px" onKeyPress="return NumbersOnly(event)"></asp:TextBox>
                        </td>
                    </tr>     
            </table>
            
            <table width="100%">
                <tr>
                    <td align="center">
                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                OnClientClick="javascript:return Validation()" 
                             onclick="btnSave_Click" />
                         &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"     
                        />
                    </td>
                
                </tr>
            </table>
        </div>
        
        
        
        
        <br />
        <br />
        <h6 align="left" style="color:#00356A;">Employee List</h6>
            <div style="width:1000px;">
                <table width="100%" align="center" class="table">
                   <tr>
                        <td align="left" width="15%" class="label">Emp Code</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">First Name</td>
                        <td align="left" width="18%">
                            <asp:TextBox ID="txtFNameSearch" runat="server" CssClass="textbox" Width="110px"></asp:TextBox>
                        </td>
                        <td align="left" width="15%" class="label">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                onclick="btnSearch_Click" />
                        </td>
                        <td align="left" width="18%">
                        </td>
                    </tr> 
                  </table>
                  <br />
                  <table width="100%" align="center">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="dgvEmployee" runat="server" AutoGenerateColumns="false" 
                                 Width="100%" AllowPaging="true" PageSize="15" GridLines="None"
                                 DataKeyNames="EmployeeId" onrowediting="dgvEmployee_RowEditing" 
                                onpageindexchanging="dgvEmployee_PageIndexChanging" 
                                onrowdatabound="dgvEmployee_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Photo">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgIDCard" runat="server" ImageUrl='<%#Bind("Photo") %>' Width="50px" Height="50px" ToolTip="Click For ID Card" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" />
                                <asp:BoundField DataField="FullName" HeaderText="Employee Name" />
                                <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                <asp:BoundField DataField="IsActive" HeaderText="Status" />
                                <asp:TemplateField HeaderText="Official Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOfficial" runat="server" Text="Official"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkQualification" runat="server" Text="Qualification"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Family Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFamily" runat="server" Text="Family"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Work Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkWork" runat="server" Text="Work"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role Details">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRole" runat="server" Text="Role"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Terms">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTerms" runat="server" Text="Terms"></asp:LinkButton> 
                                    </ItemTemplate>
                                </asp:TemplateField>                
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/print-icon.JPG" Width="25px" Height="25px" ToolTip="Print Appointment Letter" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                        
                        </Columns>
                    <PagerSettings Mode="Numeric" PageButtonCount="8" />
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
            
            <br />
            <br />
            <br />
            <br />
            <br />
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
