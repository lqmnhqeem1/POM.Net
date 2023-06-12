<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LinkProducts.aspx.vb" Inherits="IM_POM_VB.Net.LinkProducts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Link Products</title>
         <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
     <link rel="stylesheet" href="/css/styles.css" type="text/css" />
</head>
<body style="height: 450px; width: 960px">
    <form id="frmLinkProducts" runat="server" style="height: 90%; width: 90%">
         <div class="card-header rounded-top">
             <div class="row">
                 <div class="col-4"> <asp:Label ID="lblSetInfo" runat="server" CssClass="text-white h5" Text="Set Information"></asp:Label></div>
                 <div class="col-8 d-flex justify-content-end"><a href="javascript:window.close();">Close X</a></div>
             </div>
       </div>
          <div class="container alert alert-danger d-flex justify-content-center" id="alertError" runat="server" visible="false">
             <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label>
        </div>
        <table id="tblLinkProducts" cellpadding="1" cellspacing="0" align="center" style="width: 100%">
            <tr>
                <td align="right">
                    </td>
            </tr>
            <tr class="title">
            
            </tr>
            <tr>
                <td align="center">
                    </td>
            </tr>
            <tr>
                <td>
                   
                </td>
            </tr>
        </table>
         <asp:DataGrid ID="dgProduct" Style="width: 100%" GridLines="Vertical" CellPadding="2"
                        AutoGenerateColumns="False" CssClass="table table-bordered" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="product_code" HeaderText="Product Code" ReadOnly="True">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="long_description" HeaderText=" Product Description" ReadOnly="True">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Set_Code" HeaderText="Set Code" ReadOnly="True">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" VerticalAlign="Middle" Width="15%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Qty" HeaderText="Set Qty" ReadOnly="True">
                                <HeaderStyle Width="80px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" Width="15%" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="P/C" ReadOnly="True" Visible="False">
                                <HeaderStyle Width="80px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>
    </form>
</body>
</html>