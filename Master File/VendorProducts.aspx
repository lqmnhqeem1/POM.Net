<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="VendorProducts.aspx.vb" Inherits="IM_POM_VB.Net.VendorProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tContent" runat="server">

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor-Product</title>
    <script type="text/javascript" src="../commonjs/common.js"></script>
    <script type="text/javascript" src="/js/jquery-3.2.1.min.js"></script>
    <link href="../css/pom.net.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/js/util.js"></script>
    <script type="text/javascript" src="/js/index.js"></script>
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .button-div {
            text-align: right;
        }
        .button-width {
            width: 120px;
            height: 45px;
            margin-right:18px;
        }
        .header-top {
            vertical-align:top !important;
        }
    </style>
</head>--%>

<%--<body>--%>
    <div class="col p-4">
    <h1 class="display-5">Vendor Product</h1>
    <br />
        <div class="card">
            <div class="card-body">
                <%--<form id="frmVendorProducts" runat="server" style="height: 90%; width: 90%">--%>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label ID="lblVendorCode" Font-Bold="true" runat="server"  Text="Vendor Code"></asp:label>
                          <asp:Label ID="lblVendorCodeVal" class="form-control-plaintext" runat="server" Text="10600201"></asp:Label>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblVendorDesc" runat="server" Text="Vendor Description" Font-Bold="true"></asp:Label>
                          <asp:Label ID="lblVendorDescVal" runat="server" class="form-control-plaintext" text="ROYAL HAMPER HRP T-59VP"></asp:Label>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblDepartment" runat="server" Font-Bold="True" Text="Participating Department"></asp:Label>
                          <asp:Label ID="lblDepartmentVal" runat="server" Text="2" CssClass="form-control-plaintext"></asp:Label>
                        </div>
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="redLabel"></asp:Label>
                <%--</form>--%>
            </div>
        </div>
        <br />
        <div class="card">
            <h5 class="card-header font-weight-light">Details</h5>
            <form runat="server">
                <div class="card-body" style="overflow-x: auto;">
                    <asp:DataGrid ID="grdVendorProduct" GridLines="Vertical" Width="100%" CellPadding="1" CellSpacing="0" AutoGenerateColumns="False" CssClass="normalDataGrid" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <%--LOH- Comment--%>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:RadioButton runat="server" ID="rdoVendorProduct" CssClass="normalRadioButton"></asp:RadioButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Product_Code" HeaderText="Product Code">
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Wrap="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Product_Desc" HeaderText="Product Description">
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="280px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="280px" Wrap="True"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Case_Id" HeaderText="Case Id">
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Case_Qty" HeaderText="Case Qty">
                                <HeaderStyle Width="70px" HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Case_Cost" HeaderText="Case Cost">
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Min Order Qty" DataField="Min_Order_Qty">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Purc. Qty" DataField="Purc_qty">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Free Prod. " DataField="Free_Product_Code">
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Free Case Id" DataField="Free_Case_Pack_Id">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Free Case Qty" DataField="Free_Case_Qty">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Free Qty" DataField="Free_qty">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="UOM" DataField="UOM">
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="UOM Name" DataField="UOM_Name">
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Status" DataField="Status">
                                <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Wrap="False" />
                                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle" Width="60px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="department_code" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="department_desc" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>
                    <div style="text-align: right;">
                        <asp:LinkButton ID="lbtnVPCHistory" runat="server">Vendor Product Cost History</asp:LinkButton>
                        <asp:LinkButton ID="lbtnLRCHistory" runat="server">Last Received Cost History</asp:LinkButton>   
                    </div>
                    </div>
                </form>
                </div>
    </div>
<%--</body>--%>
<%--</html>--%>
</asp:Content>