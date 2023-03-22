<%@ Page Language="vb" AutoEventWireup="false" CodeFile="ProductStyle.aspx.vb" Inherits="ProductStyle" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Style</title>
    <link href="../css/pom.net.css" type="text/css" rel="stylesheet">
</head>
<body style="height: 150px; width: 960px">
    <form id="frmStyle" runat="server" style="height: 90%; width: 90%">
        <table id="tblDetail" cellpadding="1" cellspacing="0" align="center" style="width: 100%">
            <tr>
                <td align="right">
                    <a href="javascript:window.close();">Close X</a></td>
            </tr>
            <tr class="title">
                <td align="center">
                    Style Details</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="entry" id="tblDetail1" runat="server" cellpadding="1" cellspacing="0"
                        style="width: 100%">
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblPrdCode" runat="server" CssClass="normalLabel" Text="Product Code"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPrdCodeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblPrdDesc" runat="server" CssClass="normalLabel" Text="Product Description"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPrdDescvalue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="Label1" runat="server" CssClass="normalLabel" Text="Style Code"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblStyleCode" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblPrdManuType" runat="server" CssClass="normalLabel" Text="Style Description"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblStyleDesc" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
