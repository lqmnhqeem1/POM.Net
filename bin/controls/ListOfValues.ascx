<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ListOfValues.ascx.vb" Inherits="IM_POM_VB.Net.ListOfValues" %>

<table cellpadding="1" cellspacing="0">
    <tr>
        <td valign="bottom"><asp:TextBox ID="txtLOVCode" runat="server" CssClass="normalTextBox" MaxLength="10"></asp:TextBox></td>
        <td valign="baseline"><asp:HyperLink ID="btnSearch" runat="server" CssClass="normalHyperLink" ImageUrl="~/images/dot3button.png" BorderStyle="none"></asp:HyperLink></td>
    </tr>
</table>
