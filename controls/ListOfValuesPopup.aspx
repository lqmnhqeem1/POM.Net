2<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListOfValuesPopup.aspx.vb"
    Inherits="IM_POM_VB.Net.ListOfValuesPopup" %>

<%@ Register Src="~/controls/InProgress.ascx" TagName="InProgress" TagPrefix="pom" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List of Values</title>
    <link href="../css/pom.net.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../commonjs/common.js"></script>

    <script type="text/javascript" src="js/listofvalues.js?cb=<%= (new Random().Next(0,10000)) %>"></script>

</head>
<body style="height: 650px; width: 960px" onkeydown="checkKey(event);">
    <form id="frmListOfValues" runat="server" method="post" style="overflow: auto; height: 100%; width: 90%">
        <asp:ScriptManager ID="smLOV" runat="server">
        </asp:ScriptManager>
        <table cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr class="title">
                <td style="text-align: left">
                    <asp:Label ID="lblPageTitle" runat="server" Text="List Of Values" CssClass="titleLabel"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="lblSearch" runat="server" CssClass="captionLabel" Text="Search"></asp:Label><hr />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Table ID="tblSearchParam" runat="server" CssClass="searchTable" CellSpacing="0"
                        CellPadding="1" Width="100%">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:UpdateProgress ID="upgLOVSearch" runat="server" AssociatedUpdatePanelID="upLOVSearch">
                                    <ProgressTemplate>
                                        <div style="top: 0px; left: 48px; z-index: 19; position: absolute; background-image: url('../images/honey.png');
                                            width: 90%; height: 90%; border: solid 1px #000000;">
                                            <table style="border: none; width: 100%; height: 100%; vertical-align: middle; text-align: center;">
                                                <tr>
                                                    <td align="right" style="width: 50%;">
                                                        <asp:Label ID="lblProgress" runat="server" CssClass="redLabel" Text="Loading"></asp:Label></td>
                                                    <td align="left" style="width: 50%;">
                                                        <img src="../images/processing_circle.gif" alt="" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upLOVSearch" runat="server" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="searchButton" Text="Search" Visible="false"
                                            OnClientClick="javascript:return validateListOfValuesEntry();" />
                                        <asp:Button ID="btnReset" runat="server" CssClass="resetButton" Text="Reset" Visible="false"
                                            OnClientClick="javascript:return clearSearchParams();" CausesValidation="false" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trResult" runat="server" visible="true" align="left">
                <td>
                    <asp:Label ID="lblResult" runat="server" CssClass="captionLabel" Text="Result"></asp:Label><hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdateProgress ID="upgLOVResult" runat="server" AssociatedUpdatePanelID="upLOVResult">
                        <ProgressTemplate>
                            <div style="top: 0px; left: 48px; z-index: 19; position: absolute; background-image: url('../images/honey.png');
                                width: 90%; height: 90%; border: solid 1px #000000;">
                                <table style="border: none; width: 100%; height: 100%; vertical-align: middle; text-align: center;">
                                    <tr>
                                        <td align="right" style="width: 50%;">
                                            <asp:Label ID="lblProgPage" runat="server" CssClass="redLabel" Text="Loading"></asp:Label></td>
                                        <td align="left" style="width: 50%;">
                                            <img src="../images/processing_circle.gif" alt="" /></td>
                                    </tr>
                                </table>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr align="center" valign="top">
                <td>
                    <asp:UpdatePanel ID="upLOVResult" runat="server" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="dgLOV" EventName="PageIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnDone" />
                        </Triggers>
                        <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" style="border: none; margin: 0px 0px 0px 0px;">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 350px; width: 860px;">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Visible="false"></asp:Label>
                                            <asp:DataGrid ID="dgLOV" runat="server" AutoGenerateColumns="false" Width="98%" CssClass="normalDataGrid"
                                                GridLines="vertical" PageSize="20" AllowPaging="true" ShowFooter="true" PagerStyle-Mode="NumericPages">
                                                <HeaderStyle CssClass="normalDataGridHeader" />
                                                <ItemStyle CssClass="normalDataGridItem" />
                                                <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                                                <SelectedItemStyle CssClass="normalDataGridSelectedItem" />
                                                <PagerStyle CssClass="normalDataGridPager" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="optSelect" runat="server" CssClass="normalRadioButton" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                        <input type="hidden" id="hdFullValue" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr />
                                        <asp:Button ID="btnDone" runat="server" CssClass="normalButton" Text="Done" Enabled="false" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="normalButton" Text="Cancel" OnClientClick="javascript:self.close(); return false;"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
