<%@ Page Title="School Master" Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true" 
CodeBehind="SchoolMaster.aspx.cs" Inherits="CollegeERP.Common.SchoolMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function Validation() {
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtSchool.ClientID%>'), "School Name", 1)) return false;
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtaddress.ClientID%>'), "Address", 1)) return false; 
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlstate.ClientID%>'), "State", 0)) return false;
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlDistrict.ClientID%>'), "District", 0)) return false;
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=ddlCity.ClientID%>'), "City", 0)) return false;
        if (!checkforvaliddata(eval('document.forms[0].' + '<%=txtpin.ClientID%>'), "Pin", 1)) return false; 
        

        return true;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ToolkitScriptManager ID="ToolScript1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="title">
        <h5>
            District Master</h5>
    </div>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 750px;">
        <uc3:Message ID="Message" runat="server" />
        <br />
        <table align="center" width="100%" class="table">
        <tr>
                <td align="left" width="20%" class="label">
                    School Name<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtSchool" runat="server" CssClass="textbox_required" Width="140px"
                        MaxLength="199"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td align="left" width="20%" class="label">
                    Address<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtaddress" runat="server" CssClass="textbox_required" Width="140px"
                        TextMode="MultiLine" Height="70px"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="left" width="20%" class="label">
                    State <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="dropdownList" 
                        Width="140px" AutoPostBack="True" 
                        onselectedindexchanged="ddlstate_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" width="20%" class="label">
                    District <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" 
                        Width="140px" AutoPostBack="True" 
                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" width="20%" class="label">
                    City <span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownList" 
                        Width="140px"  
                      >
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td align="left" width="20%" class="label">
                    Pin<span class="req">*</span>
                </td>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtpin" runat="server" CssClass="textbox_required" Width="140px"
                        MaxLength="9"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="fft" runat="server" TargetControlID="txtpin" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClientClick="return Validation()"
                        OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Reset" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table width="100%" align="center" class="table">
            <tr>
                <td align="left" width="10%" class="label">
                    School 
                </td>
                <td align="left" width="15%">
                    <asp:TextBox ID="txtSchoolsearch" runat="server" CssClass="textbox" Width="140px"
                        MaxLength="199"></asp:TextBox>
                </td>
                 <td align="left" width="10%" class="label">
                    State 
                </td>
                 <td align="left" width="15%">
                    <asp:DropDownList ID="ddlStatesearch" runat="server" CssClass="dropdownList" 
                         Width="140px" AutoPostBack="True" 
                         onselectedindexchanged="ddlStatesearch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                 <td align="left" width="10%" class="label">
                    District 
                </td>
                 <td align="left" width="15%">
                    <asp:DropDownList ID="ddlDistrictSearch" runat="server" CssClass="dropdownList" 
                         Width="140px" AutoPostBack="True" 
                         onselectedindexchanged="ddlDistrictSearch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                   <td align="left" width="10%" class="label">
                    City 
                </td>
                <td align="left" width="10%">
                    
                    <asp:DropDownList ID="ddlcitysearch" runat="server" CssClass="dropdownList" 
                        Width="140px"  
                      >
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Search" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="9">
                    <asp:GridView ID="dgvState" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        Width="100%" DataKeyNames="SchoolID" PageSize="15" OnPageIndexChanging="dgvState_PageIndexChanging"
                        OnRowCommand="dgvState_RowCommand" EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="SlNo" HeaderText="Sl No">
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="State" HeaderText="State Name" />
                            <asp:BoundField DataField="District" HeaderText="District Name" />
                            <asp:BoundField DataField="City" HeaderText="City Name" />
                            <asp:BoundField DataField="School" HeaderText="School Name" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="PIN" HeaderText="PIN" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_icon.gif" CommandName="Ed"
                                        CommandArgument='<%#Eval("SchoolID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="height: 10px; width: 100%;">
                                <tr class="RowStyle">
                                    <td>
                                        Sorry! No Records Found
                                    </td>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
