<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductGroupDetails.aspx.vb" Inherits="ProductGroupDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Group Details</title>
    <link href="../css/pom.net.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../commonjs/common.js"></script>

</head>
<body style="height: 300px; width: 960px">
    <form id="frmPrdGrpDetail" runat="server" style="height: 90%; width: 90%">
        <table cellpadding="1" cellspacing="0" align="center" style="width: 100%">
            <tr>
                <td align="right">
                    <a href="javascript:window.close();">Close X</a></td>
            </tr>
            <tr class="title" align="center">
                <td>
                    Summary</td>
            </tr>
            <tr>
                <td>
                    <table class="entry" cellpadding="1" cellspacing="0" width="100%">
                        <tr class="d0">
                            <td class="c0" style="height: 14px">
                                <asp:Label runat="server" ID="lblProductGroup" CssClass="normalLabel">Product Group Code</asp:Label>
                            </td>
                            <td class="c1" style="height: 14px">
                                <asp:Label runat="server" ID="lblProductGroupValue" CssClass="normalLabel"></asp:Label>
                            </td>
                            <td class="c0" style="height: 14px">
                                <asp:Label runat="server" ID="lblGroupDesc" CssClass="normalLabel">Group Description</asp:Label>
                            </td>
                            <td class="c1" style="height: 14px">
                                <asp:Label runat="server" ID="lblGroupDescValue" CssClass="normalLabel"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblResults" runat="server" CssClass="redLabel"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="dgPrdGrpDetails" runat="server" AutoGenerateColumns="false" Width="100%"
                        CssClass="normalDataGrid" GridLines="vertical" ShowFooter="true">
                        <HeaderStyle CssClass="normalDataGridHeader" />
                        <ItemStyle CssClass="normalDataGridItem" />
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem" />
                        <Columns>
                            <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LONG_DESCRIPTION" HeaderText="Long Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    
                    <%--Add By Farnia @ 21 May 2014 For DCL 5090 - Unable to search range discount with subclass group--%>
                    <asp:DataGrid ID="dgSubClassGroupDetail" runat="server" AutoGenerateColumns="false" Width="100%"
                        CssClass="normalDataGrid" GridLines="vertical" ShowFooter="true">
                        <HeaderStyle CssClass="normalDataGridHeader" />
                        <ItemStyle CssClass="normalDataGridItem" />
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem" />
                        <Columns>
                            <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="Sub Class Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-Width="20%"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="Long Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <hr />
                    <asp:Button ID="btnPrint" runat="server" CssClass="normalButton" Text="Print" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
