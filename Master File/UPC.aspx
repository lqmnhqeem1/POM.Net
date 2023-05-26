<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UPC.aspx.vb" Inherits="IM_POM_VB.Net.UPC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Universal Product Code Info</title>
    	
	  <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
      <link rel="stylesheet" href="/css/styles.css" type="text/css" />
</head>
<body>
    <form id="frmUpc" runat="server">
        <center>
              <div class="card-header w-25 rounded-top">
           <a class="d-flex justify-content-end text-white" href="javascript:window.close();">Close X</a>
           <asp:Label ID="lblFP" runat="server" CssClass="text-white h5" Text="USP"></asp:Label>
       </div>
            <div class="container alert alert-danger d-flex justify-content-center" id="alertError" runat="server" visible="false">
            <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label>
        </div>
            <div>
                 <table id="tblDetail" cellpadding="1" cellspacing="0" align="center">
            <tr>
                <td>
                    <asp:DataGrid ID="dgUPC" GridLines="Vertical" CellPadding="2" AutoGenerateColumns="False"
                        CssClass="table table-bordered" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="product_UPC" HeaderText="UPC Code" ReadOnly="True">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle"  Width="40%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UPC_type" HeaderText="UPC Type" ReadOnly="True">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"  Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Active">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" Enabled="false" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem, "Active_Flag")%>'>
                                    </asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Primary_UPC" HeaderText="Primary UPC" ReadOnly="True">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"  Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
            </div>
        </center>
        
    </form>
</body>
</html>
