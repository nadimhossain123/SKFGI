<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="EmployeeAttendanceNew.aspx.cs" Inherits="CollegeERP.Common.EmployeeAttendanceNew"
    Title="Employee Attendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function Validation()
        {
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Year');
                return false;
            }
            else if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0)
            {
                alert('Please Select Month');
                return false;
            }
            else {return true;}
        }

    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="toolScript1" runat="server">
    </asp:ToolkitScriptManager>
     <%-- <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>--%>
    <div class="title">
        <h5>
            Employee Attendance</h5>
    </div>
    <uc3:Message ID="Message" runat="server" />
    <div style="width: 940px;">
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="20%" class="label">
                    Year<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="dropdownList"
                        Width="150px" DataValueField="YearNo" DataTextField="YearName" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left" width="20%" class="label">
                    Month
                </td>
                <td align="left" width="30%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropdownList" Width="150px"
                        DataValueField="MonthNo" DataTextField="MonthName">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnImport" runat="server" CssClass="button" Text="Generate Attendance"
                        OnClientClick="return Validation()" onclick="btnImport_Click" />
                    &nbsp;
                    <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Show Attendance"
                        OnClientClick="return Validation()" onclick="btnShow_Click" />
                </td>
            </tr>
        </table>
   
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                <asp:GridView ID="dgvLeaveDetail" runat="server" AutoGenerateColumns="false" DataKeyNames="leaveid" 
                onrowcommand="dgvLeaveDetail_RowCommand">
                <Columns>    
                                 <asp:BoundField DataField="NoOfDays" HeaderText="No Of Day" 
                                    HeaderStyle-Width="70px" >
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                 </asp:BoundField>  
                                 <asp:TemplateField HeaderText="From Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="150px" CssClass="textbox" 
                                        Text='<%# Eval("startdate") %>' Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="cal_Theme1"
                                          PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtFromDate"
                                          OnClientDateSelectionChanged="" Enabled="True">
                                          </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="End Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="150px" CssClass="textbox" 
                                        Text='<%# Eval("enddate") %>' Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="cal_Theme1"
                                          PopupPosition="BottomRight" Format="dd MMM yyyy" TargetControlID="txtEndDate"
                                          OnClientDateSelectionChanged="" Enabled="True">
                                          </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnEdit" 
                                        ImageUrl="~/Images/edit_icon.gif" runat="server" OnClick="imgBtnEdit_Click"  CommandName="Change"   
                                                CommandArgument="<%#((GridViewRow) Container).RowIndex %>"  />
                                        </ItemTemplate>
                                 </asp:TemplateField>  
                                 </Columns>                           
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                    <asp:GridView ID="dgvAttendance" runat="server" AutoGenerateColumns="false" GridLines="None"
                        Width="100%" DataKeyNames="Id,EmpId" onrowcommand="dgvAttendance_RowCommand" >
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No">
                                <ItemTemplate>
                                   <%#  ((GridViewRow)Container).RowIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" 
                                HeaderStyle-Width="70px" >
                            <HeaderStyle Width="70px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" 
                                HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Total Days" />
                            <%--<asp:BoundField DataField="Present" HeaderText="Present" />--%>
                            <asp:TemplateField HeaderText="Present">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPresent" runat="server" Width="50px" CssClass="textbox" 
                                    Text='<%# Eval("Present") %>' Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Absent" HeaderText="Absent Days" />--%>
                            <asp:TemplateField HeaderText="Absent">
                                <ItemTemplate>
                                    
                                    <asp:LinkButton ID="lbtnAbsent" runat="server" Text='<%# Eval("Absent") %>' 
                                    onclick="lbtnAbsent_Click"></asp:LinkButton>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="CL" HeaderText="C L Taken" />--%>
                            <asp:TemplateField HeaderText="C L Taken">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtCL" runat="server" Width="50px" CssClass="textbox" onkeyup="txtCL_TextChanged" 
                                        Text='<%# Eval("CL") %>'  ></asp:TextBox>--%>
                                    <asp:LinkButton ID="lbtnCL" runat="server" Text='<%# Eval("CL") %>' 
                                        onclick="lbtnCL_Click"></asp:LinkButton>  
                                         <%-- <a href="#" onclick="PopUpLeaveDetail.aspx?Id=<%# DataBinder.Eval (Container.DataItem, "id") %>&LeaveTypeId=1');">
                                        <%# DataBinder.Eval(Container.DataItem, "CL")%>--%>
                                    </a>                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="EL" HeaderText="E L Taken" />--%>
                            <asp:TemplateField HeaderText="E L Taken">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtEL" runat="server" Width="50px" CssClass="textbox" Text='<%# Eval("EL") %>'></asp:TextBox>--%>
                                    <asp:LinkButton ID="lbtnEL" runat="server" Text='<%# Eval("EL") %>' 
                                        onclick="lbtnEL_Click"></asp:LinkButton> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Medical" HeaderText="Medical Taken" />--%>
                            <asp:TemplateField HeaderText="Medical Taken">
                                <ItemTemplate>
                                   <%-- <asp:TextBox ID="txtMedical" runat="server" Width="50px" CssClass="textbox" Text='<%# Eval("Medical") %>'></asp:TextBox>--%>
                                   <asp:LinkButton ID="lbtnMedical" runat="server" Text='<%# Eval("Medical") %>' 
                                        onclick="lbtnMedical_Click"></asp:LinkButton> 
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField DataField="SpecialLeave" HeaderText="SpecialLeave" />--%>
                            <asp:TemplateField HeaderText="Special Leave">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtSpecial" runat="server" Width="50px" CssClass="textbox" Text='<%# Eval("SpecialLeave") %>'></asp:TextBox>--%>
                                    <asp:LinkButton ID="lbtnSpecial" runat="server" Text='<%# Eval("SpecialLeave") %>' 
                                        onclick="lbtnSpecial_Click"></asp:LinkButton> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Holiday" HeaderText="Holiday" />--%>
                            <asp:TemplateField HeaderText="Holiday">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHoliday" runat="server" Width="50px" CssClass="textbox" Text='<%# Eval("Holiday") %>'
                                    Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="OffDay" HeaderText="OffDay" />--%>
                             <asp:TemplateField HeaderText="OffDay">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOffDay" runat="server" Width="50px" CssClass="textbox" 
                                    Text='<%# Eval("OffDay") %>' Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="TotalPayDay" HeaderText="TotalPayDay" />--%>
                             <asp:TemplateField HeaderText="TotalPayDay">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalPayDay" runat="server" Width="50px" CssClass="textbox" 
                                    Text='<%# Eval("TotalPayDay") %>' Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnEdit" 
                                        ImageUrl="~/Images/edit_icon.gif" runat="server" OnClick="imgBtnEdit_Click"  CommandName="Change"   
                                                CommandArgument="<%#((GridViewRow) Container).RowIndex %>"  />
                                        </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
                </tr>
                <tr>
                <td align="center">
                    <asp:GridView ID="dgvAttendanceRpt" runat="server" AutoGenerateColumns="false" GridLines="None"
                        Width="100%" DataKeyNames="Id,EmpId" onrowcommand="dgvAttendance_RowCommand" Visible="false"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No">
                                <ItemTemplate>
                                   <%#  ((GridViewRow)Container).RowIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpCode" HeaderText="Employee Code" 
                                HeaderStyle-Width="70px" >
                            <HeaderStyle Width="70px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" 
                                HeaderStyle-Width="150px" >
                            <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalDays" HeaderText="Total Days" />
                            <asp:BoundField DataField="Present" HeaderText="Present" />
                            <asp:BoundField DataField="Absent" HeaderText="Absent Days" />                           
                            <asp:BoundField DataField="CL" HeaderText="C L Taken" />
                            <asp:BoundField DataField="EL" HeaderText="E L Taken" />
                            <asp:BoundField DataField="Medical" HeaderText="Medical Taken" />                           
                           <asp:BoundField DataField="SpecialLeave" HeaderText="SpecialLeave" />                           
                            <asp:BoundField DataField="Holiday" HeaderText="Holiday" />                            
                            <asp:BoundField DataField="OffDay" HeaderText="OffDay" />                            
                            <asp:BoundField DataField="TotalPayDay" HeaderText="TotalPayDay" />
                            
                            <%--<asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnEdit" 
                                        ImageUrl="~/Images/edit_icon.gif" runat="server" OnClick="imgBtnEdit_Click"  CommandName="Change"   
                                                CommandArgument="<%#((GridViewRow) Container).RowIndex %>"  />
                                        </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td  align="right">
                    <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download Attendance Report"
                        OnClick="btnDownload_Click" meta:resourcekey="btnDownloadResource1" Visible="false" />
                        
                </td>
            </tr>
        </table>
        <%--**************************// POP UP //**********************************--%>    
         <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
    CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="269px" Width="600px" style="display:none; border:solid 5px #C0C0C0" >
<%--        <asp:UpdatePanel ID="UP2" runat="server">
            <ContentTemplate>--%>
            <table width="100%" style=" width:100%; height:100%" cellpadding="0" cellspacing="0">
            <tr style="background-color:#356BA0">
                <td style=" height:30px; color:White; font-weight:bold; font-size:larger" 
                    align="center">Leave Details</td>
            </tr>
            <%--<tr>
            <td>
                <IFRAME id="frame1" src="PopUpLeaveDetail.aspx" height="220px" Width="500px"  >

                </IFRAME>
            </td>
           </tr>--%>
           <tr>
            <td style="height:20px">
                <div id="dvAbsent"  >
                    <asp:Panel ID="pnlAbsent" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td class="label" style="width:30%">
                                    <asp:Label ID="Label1" runat="server" Text="Enter Total Days :"></asp:Label>
                                </td>
                                <td style="width:30%">
                                    <asp:TextBox ID="txtDays" CssClass="textbox" runat="server"></asp:TextBox>
                                    <input id="hdnEmployeeId" type="hidden"  runat="server"/>
                                </td>
                                <td style="width:30%">
                                    <asp:Button ID="btnUpdateAbsent" runat="server" Text="Update" CssClass="button" onclick="btnUpdateAbsent_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>                 
                
                
                </div>
                
            </td>
           </tr>
           
            <tr>
            <td align="center" style="height:160px">
                &nbsp;</td>
            </tr>
            <tr>
            <td align="right" style="padding-right:10px">
            <br />
            <br />
            <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" />
            </td>
            </tr>
            </table>
<%--            </ContentTemplate>
            </asp:UpdatePanel>--%>
            </asp:Panel>
            


</div>

</asp:Content>
