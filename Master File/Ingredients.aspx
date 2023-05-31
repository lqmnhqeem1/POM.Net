<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Ingredients.aspx.vb" Inherits="IM_POM_VB.Net.Ingredients" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ingredients Details</title>
           <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
     <link rel="stylesheet" href="/css/styles.css" type="text/css" />
</head>
<body style="height: 400px; width: 960px">
    <form id="frmIngredients" runat="server" style="height: 90%; width: 90%">
        <table id="tblDetail" class="table table-striped" cellpadding="1" cellspacing="0" align="center" style="width: 100%">
            <tr>
                <td align="right">
                    <a href="javascript:window.close();">Close X</a></td>
            </tr>
            <tr class="title">
                <td align="center">
                    Ingredients</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="entry" id="Table1" runat="server" cellpadding="1" cellspacing="0" style="width: 100%">
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
                                <asp:Label ID="Label2" runat="server" CssClass="normalLabel" Text="Malay Description"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblMalay" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="Label8" runat="server" CssClass="normalLabel" Text="Min. Weight/Berat Bersih"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblMinWeight" runat="server" CssClass="normalLabel"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label9" runat="server" CssClass="captionLabel" Text="Ingredients/Ramuan"></asp:Label><hr />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="entry" id="tblDetail1" runat="server" cellpadding="1" cellspacing="0"
                        style="width: 100%">
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="Label4" runat="server" CssClass="normalLabel" Text="Ingredients 1"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblIng1" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="Label11" runat="server" CssClass="normalLabel" Text="Ingredients 2"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblIng2" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="Label12" runat="server" CssClass="normalLabel" Text="Ingredients 3"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblIng3" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="Label1" runat="server" CssClass="captionLabel" Text="Nutrition Information"></asp:Label><hr />
                </td>
            </tr>
            <tr>
                <td>
                    <table class="entry" id="tblDetail2" runat="server" cellpadding="1" cellspacing="0"
                        style="width: 100%">
                        <tr class="d0" style="height:18px">
                            <td align="left" style="height: 14px">
                                <asp:Label ID="Label3" runat="server" CssClass="normalLabel" Text="Maklumat" Font-Bold="True"></asp:Label></td>
                            <td align="left" style="height: 14px">
                                <asp:Label ID="Label5" runat="server" CssClass="normalLabel" Text="Per/Hidangan Setiap"
                                    Font-Bold="True"></asp:Label></td>
                            <td align="left" style="height: 14px">
                                <asp:Label ID="Label6" runat="server" CssClass="normalLabel" Text="Per Serving/Setiap"
                                    Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td align="left">
                                <asp:Label ID="Label7" runat="server" CssClass="normalLabel" Text="Energy/Tenaga"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblHidEnergy" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblSerEnergy" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td align="left">
                                <asp:Label ID="lblPrdType" runat="server" CssClass="normalLabel" Text="Protein/Protein"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblHidProtein" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblSerProtein" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td align="left">
                                <asp:Label ID="lblPurchaseType" runat="server" CssClass="normalLabel" Text="Fat/Lemak"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblHidFat" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblSerFat" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td align="left">
                                <asp:Label ID="lblPrivateLabel" runat="server" CssClass="normalLabel" Text="Carbohydrate/Karbohidrat"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblHidCarbo" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblSerCarbo" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td align="left">
                                <asp:Label ID="Label10" runat="server" CssClass="normalLabel" Text="Nutrition"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblHidNutrition" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblSerNutrition" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
    
