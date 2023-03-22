<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductHierarchyPopUp.aspx.vb" Inherits="IM_POM_VB.Net.ProductHierarchyPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Product Hierarchy</title>
    <!--<link href="css/pom.net.css" type="text/css" rel="stylesheet" />-->
</head>
<body>
    <form id="form1" runat="server">
    <table  align="center">
    <tr>
    <td align="right"colspan="4" ><a href="javascript:window.close();">Close 
							X</a></td>
							</tr>
				<tr class="title" align="center" >
					<td colspan="4"><asp:label id="lblFuturePrice" Runat="server" Text="Product Hierarchy"></asp:label></td>
				</tr>
				<tr class="d0">
				<td align="left"><asp:label id="lblDiv" Runat="server" CssClass="paddedLabel" Text="Division"></asp:label></td>
				<td align="left"><asp:label id="lblDivision" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				<td align="left"><asp:label id="lblDept" Runat="server" CssClass="paddedLabel" Text="Department"></asp:label></td>
				<td align="left"><asp:label id="lblDepartment" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				</tr>
				<tr class="d1">
				<td align="left"><asp:label id="lblCat" Runat="server" CssClass="paddedLabel" Text="Category"></asp:label></td>
				<td align="left"><asp:label id="lblCategory" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				<td align="left"><asp:label id="lblSubCat" Runat="server" CssClass="paddedLabel" Text=" Sub-Category"></asp:label></td>
				<td align="left"><asp:label id="lblSubCategory" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				</tr>
				<tr class="d0">
				<td align="left"><asp:label id="lblClass" Runat="server" CssClass="paddedLabel" Text="Class"></asp:label></td>
				<td align="left"><asp:label id="lblClassValue" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				<td align="left"><asp:label id="lblSubClass" Runat="server" CssClass="paddedLabel" Text=" Sub-Class"></asp:label></td>
				<td align="left"><asp:label id="lblSubClassValue" Runat="server" CssClass="paddedLabel" Text=""></asp:label></td>
				</tr>
				</table>
    </form>
</body>
</html>

