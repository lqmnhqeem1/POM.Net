<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ServerFileDialog.ascx.vb" Inherits="IM.POM.VB.Net.ServerFileDialog" %>

<div id="divFileList" runat="server" style="position:absolute; z-index:1918; overflow:auto; visibility:hidden; float:right; height:200px; width:150px; border-style:solid; border-width:1px; padding:0px; background-image:url(../images/honey.png);">
    <table class="entry" cellpadding="1" cellspacing="1" border="0" style="border-style:none;">
        <tr>
            <td id="tdButton" runat="server" align="left" valign="top">
                <asp:UpdatePanel ID="upButton" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:ImageButton ID="ibtnOk" runat="server" CommandName="SELECT" ImageUrl="~/images/images_tick.gif" Height="20px" Width="20px" />
                        <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="CANCEL" ImageUrl="~/images/btn_cross.gif" Height="20px" Width="20px" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ibtnOK" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ibtnCancel" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width:100%;">
                <asp:UpdatePanel ID="upTree" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:XmlDataSource ID="xdsServer" runat="Server" XPath="ServerFiles/ServerDir" EnableCaching="false" />
                        <asp:TreeView ID="tvServerFile" runat="server" DataSourceID="xdsServer" ShowExpandCollapse="true" ShowLines="true" Width="100%">
                            <NodeStyle CssClass="normalCheckBox" Font-Bold="true" />
                            <LeafNodeStyle CssClass="normalCheckBox" />
                            <DataBindings>
                                <asp:TreeNodeBinding DataMember="ServerFiles" Text="Files On Server" />
                                <asp:TreeNodeBinding DataMember="ServerDir" TextField="Name" ValueField="ID" ShowCheckBox="false" SelectAction="None" ImageUrl="~/images/Folder.gif" />
                                <asp:TreeNodeBinding DataMember="ServerFile" TextField="Name" ValueField="ID" ShowCheckBox="false" SelectAction="None" ImageUrl="~/images/File.gif" />
                            </DataBindings>
                        </asp:TreeView>
                        <asp:Timer ID="tServerFile" runat="server" Enabled="true" Interval="45000" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tvServerFile" EventName="TreeNodeCheckChanged" />
                        <asp:AsyncPostBackTrigger ControlID="tServerFile" EventName="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>
<asp:Button ID="btnServerFile" runat="server" />