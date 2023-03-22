<%@ Page Language="VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not String.IsNullOrEmpty(Request.QueryString("text")) AndAlso Request.QueryString("text").Trim().Length > 0 Then lblText.Text = Request.QueryString("text").Trim()
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>In-Progress</title>
        <link type="text/css" rel="stylesheet" href="../css/pom.net.css" />
    </head>
    <body style="margin:0px 0px 0px 0px; text-align:left; background-color:Transparent;">
        <form id="frmInProgress" runat="server">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><asp:Label ID="lblText" runat="server" CssClass="normalLabel" Text="Loading "></asp:Label></td>
                    <td><asp:Image ID="imgProgress" runat="server" CssClass="normalImage" ImageUrl="~/images/processing_circle.gif" ImageAlign="bottom" AlternateText="" /></td>
                </tr>
            </table>
        </form>
    </body>
</html>
