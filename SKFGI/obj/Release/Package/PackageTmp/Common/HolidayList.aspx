<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="HolidayList.aspx.cs" Inherits="CollegeERP.Common.HolidayList" Title="Yearly Holiday Fixation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
         function Validation()
        {
         if (isValidDate(document.getElementById('<%=txtHolidayDate.ClientID%>').value) == false) {
            alert("Enter Holiday Date in DD/MM/YYYY format");
            return false;
            } 
         else if (document.getElementById('<%=txtName.ClientID%>').value == '') {
            alert("Enter Holiday Name");
            return false;
            }  
         else {return true;}            
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="ToolScript1" runat="server"></asp:ToolkitScriptManager>
    <div class="title">
		<h5>Yearly Holiday Fixation</h5>
    </div>
    <asp:UpdatePanel ID="UP1" runat="server">
    <ContentTemplate>
    <div style="width:740px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="30%" class="label">Holiday Date (dd/mm/yyyy)<span class="req">*</span></td>
                <td align="left" width="20%">
                    <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="textbox_required" Width="120px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                         PopupPosition="BottomRight" Format="dd/MM/yyyy" TargetControlID="txtHolidayDate" OnClientDateSelectionChanged="" Enabled="True">
                    </asp:CalendarExtender>
                </td>
                <td align="left" width="15%" class="label">Holiday Name<span class="req">*</span></td>
                <td align="left" width="35%">
                    <asp:TextBox ID="txtName" runat="server" CssClass="textbox_required" Width="180px"></asp:TextBox>
                </td>  
            </tr>
            <tr>
                <td align="left" width="30%" class="label">Holiday Remarks</td>
                <td align="left" colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox" Width="445px" TextMode="MultiLine" Height="35px" Style="resize: none"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%"></td>
                <td align="right" colspan="3">
                     <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                        OnClientClick="javascript:return Validation()" onclick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" onclick="btnCancel_Click"      
                    />
                </td>
            </tr>
        </table>
        
        <br />
        <br />
        
        <table width="100%" align="center" class="table">
            <tr>
                <td align="center">
                    <asp:GridView ID="dgvHoliday" runat="server" AutoGenerateColumns="false" 
                        Width="100%" AllowPaging="true" PageSize="10"
                         DataKeyNames="HolidayListId" 
                        onpageindexchanging="dgvHoliday_PageIndexChanging" 
                        onrowdeleting="dgvHoliday_RowDeleting" onrowediting="dgvHoliday_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="HolidayName" HeaderText="Holiday Name" />
                            <asp:BoundField DataField="HolidayDate" HeaderText="Holiday Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="HolidayYear" HeaderText="Holiday Year" />
                            <asp:BoundField DataField="HolidayRemarks" HeaderText="Holiday Remarks" />
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
	                <PagerStyle CssClass="PagerStyle" HorizontalAlign="Left" />
                    </asp:GridView> 
                </td>
            </tr>
        </table>
    </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
