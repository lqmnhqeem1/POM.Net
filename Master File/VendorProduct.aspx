<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VendorProduct.aspx.vb" Inherits="IM_POM_VB.Net.VendorProduct" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor-Product</title>

    <script type="text/javascript" src="../commonjs/common.js"></script>

    <script language="javascript" type="text/javascript">
        var strPrd =  '<%=strPrdtId%>'
        var strPD = '<%=strPrdDesc%>'
        var strVendCode = '<%=strVendC%>'
        var strVendName = '<%=strVendN%>'
        var strCase = '<%=strCaseId%>'
        var strCaseQty = '<%=strCaseQty%>'
    </script>

    	  <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
      <link rel="stylesheet" href="/css/styles.css" type="text/css" />
</head>
<body style="height: 500px; width: 960px">
    <form id="frmVendorProduct" runat="server" style="height: 90%; width: 90%">
         <div class="card-header rounded-top">
             <div class="row">
                 <div class="col-4"> <asp:Label ID="lblSetInfo" runat="server" CssClass="text-white h5" Text="Vendor Product"></asp:Label></div>
                 <div class="col-8 d-flex justify-content-end"><a href="javascript:window.close();">Close X</a></div>
             </div>
       </div>
        <table id="tblVendorProduct" cellpadding="1" cellspacing="0" align="center" style="width: 100%">

            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td>
                  
                        <asp:DataGrid ID="dgVenProduct" GridLines="Vertical" CellPadding="2" Width="1490px"
                            AutoGenerateColumns="False" CssClass="table table-bordered" runat="server" AllowPaging="False"
                            PageSize="20">
                            <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                            <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                            <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                            <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn Visible="True">
                                    <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:RadioButton runat="server" ID="rdoVendorProduct" CssClass="normalRadioButton"></asp:RadioButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Vendor_Code" HeaderText="Vendor Code" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Vendor_Name" HeaderText="Vendor Name" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Case_Id" HeaderText="Case Id" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="85px"></HeaderStyle>
                                    <ItemStyle Width="85px" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Case_Qty" HeaderText="Case Qty" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" Width="50px" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Case_Cost" HeaderText="Case Cost" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" Width="60px" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Min Order Qty" ReadOnly="True" DataField="Min_Order_Qty">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="50px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="UOM" ReadOnly="True" DataField="UOM">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="UOM Name" ReadOnly="True" DataField="UOM_Name">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="40px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Status" ReadOnly="True" DataField="Status">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Primary Vendor" ReadOnly="True" DataField="Primary_Flag">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Sourced" ReadOnly="True" DataField="Sourced">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Return Flag" ReadOnly="True" DataField="Return_Flag">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Dist. type" ReadOnly="True" DataField="Distribution_type">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Supplying DC" ReadOnly="True" DataField="PreferredDC">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Case_Pack_Desc" HeaderText="Case Description" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="250px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Purc. Qty" ReadOnly="True" DataField="Purc_qty">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="50px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Free Prod. " ReadOnly="True" DataField="Free_Product_Code">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="70px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"/>
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Free Case Id" ReadOnly="True" DataField="Free_Case_Pack_Id">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="85px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="85px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Free Case Qty" ReadOnly="True" DataField="Free_Case_Qty">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="50px" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Free Qty" ReadOnly="True" DataField="Free_qty">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="50px"/>
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px" />
                                </asp:BoundColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                        </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:LinkButton ID="lbtnViewHistory" runat="server">View Cost History</asp:LinkButton></td>
            </tr>
        </table>
    </form>
</body>
</html>
