<%@ Control Language="vb" AutoEventWireup="false" CodeFile="ListOfValues.ascx.vb" Inherits="ListOfValues" %>

<table cellpadding="1" cellspacing="0">
    <tr>
        <td valign="bottom"><asp:TextBox ID="txtLOVCode" runat="server" CssClass="normalTextBox" MaxLength="10"></asp:TextBox></td>
        <td valign="baseline"><asp:HyperLink ID="btnSearch" runat="server" CssClass="normalHyperLink" ImageUrl="~/Images/dot3button.png" BorderStyle="none"></asp:HyperLink></td>
    </tr>
</table>
