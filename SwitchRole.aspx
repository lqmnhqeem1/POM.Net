<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SwitchRole.aspx.vb" Inherits="IM_POM_VB.Net.SwitchRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Switch Role</title>
    <link href="css/pom.net.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="commonjs/common.js"></script>

    <script type="text/javascript" src="commonjs/rootlevelpage.js"></script>

    <script type="text/javascript" language="javascript">
            function checkKey(event)
            {
                switch(event.keyCode)
                {
                    case 13:
                        document.getElementById('btnSwitchRole').focus();
                        break;
                }
            }
    </script>
</head>
<body onkeydown="checkKey(event)">
    <%--<form id="form1" runat="server">
        <div>
        </div>
    </form>--%>
    <table style="height: 100%; width: <%=screenpx%>; background-color: #ffffff;">
        <tr>
            <td>
                <form id="frmSwitchRole" runat="server" method="post">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table id="tblHeader" style="width: 100%;">
                                    <tr>
                                        <td style="text-align: left; vertical-align: top; width: 25%;">
                                            <asp:Image ID="imgGiant" runat="server" ImageUrl="~/images/applogo.jpg" AlternateText="Giant" />
                                        </td>
                                        <td style="width: 50%; vertical-align: middle;" valign="middle">
                                            <asp:Label ID="lblStore" runat="server" CssClass="systemLabel" Text="Store »"></asp:Label>
                                            <asp:Label ID="lblStoreId" runat="server" CssClass="normalLabel" Text="" Font-Bold="true"
                                                ForeColor="black"></asp:Label>
                                        </td>
                                        <td style="width: 25%; vertical-align: middle; text-align: right;" valign="middle">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblUser" runat="server" CssClass="systemLabel" Text="User »"></asp:Label>
                                                        <asp:Label ID="lblUserId" runat="server" CssClass="normalLabel" ForeColor="black"
                                                            Font-Bold="true" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a id="lnkSignOut" runat="server">
                                                            <asp:Label ID="lblSignOut" runat="server" CssClass="normalHyperLink" Font-Bold="true"
                                                                ForeColor="#000000" Text="Sign Out"></asp:Label>
                                                            <asp:Image ID="imgSignOut" runat="server" ImageUrl="~/images/logout.gif" AlternateText="" />
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="title">
                            <td>
                                <asp:Label ID="lblPageTile" runat="server" CssClass="titleLabel" Text="Switch Role"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Visible="false"></asp:Label></td>
                        </tr>
                        <tr class="caption">
                            <td>
                                <asp:Label ID="lblRoles" runat="server" CssClass="captionLabel" Text="Select Role"></asp:Label><hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:DataGrid ID="dgRole" runat="server" AutoGenerateColumns="false" GridLines="vertical"
                                    CssClass="normalDataGrid" Width="100%">
                                    <HeaderStyle CssClass="normalDataGridHeader" />
                                    <ItemStyle CssClass="normalDataGridItem" />
                                    <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                                    <EditItemStyle CssClass="normalDataGridEditItem" />
                                    <Columns>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="optRole" runat="server" CssClass="normalRadioButton" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleId" runat="server" CssClass="normalLabel" Text='<%# DataBinder.Eval(Container.DataItem, "roleid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="left" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleDesc" runat="server" CssClass="normalLabel" Text='<%# DataBinder.Eval(Container.DataItem, "roledesc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr id="hrAbButton" runat="server" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="baseline">
                                <asp:Button ID="btnSwitchRole" runat="server" CssClass="normalButton" Text="Ok" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
