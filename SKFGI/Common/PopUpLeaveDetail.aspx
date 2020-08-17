<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpLeaveDetail.aspx.cs" Inherits="CollegeERP.Common.PopUpLeaveDetail" Title="Employee Role Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../UserControl/Message.ascx" tagname="Message" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Employee Leave Detail</title>
    <link href="../Styles/style.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/Control.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/blue.css" rel="Stylesheet" type="text/css" />
    <link href="../Styles/reset.css" rel="Stylesheet" type="text/css" />
</head>
<body style=" background-color:#fff;">
    <script language="javascript" type="text/javascript" src="../Scripts/ssjscript.js"></script>
    <script type="text/javascript">
        
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Script1" runat="server">
        </asp:ScriptManager>
        
      <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
        <br />  
        <uc3:Message ID="Message" runat="server" />
        <br />
        <center>
            <div style="width:700px;">
                     <br />
                        <table width="90%" align="center" class="table">
                            <tr>
                                <td align="center">
                                    
                                      <asp:GridView ID="dgvLeaveDetail" runat="server" AutoGenerateColumns="false">
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
                                 <asp:BoundField DataField="enddate" HeaderText="End Date" 
                                    HeaderStyle-Width="70px" >
                                    <HeaderStyle Width="70px"></HeaderStyle>
                                 </asp:BoundField>  
                                 </Columns>                           
                            <HeaderStyle CssClass="HeaderStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            </asp:GridView>
                               
                                </td>
                            </tr>
                        </table>
            </div>
            <br />
            <br />
            
        </center>
        </ContentTemplate>
      </asp:UpdatePanel>               
    </form>
</body>
</html>
