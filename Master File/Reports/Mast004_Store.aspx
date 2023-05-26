<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Mast004_Store.aspx.vb" Inherits="IM_POM_VB.Net.Mast004" %>

<asp:Content ID="cMast004" ContentPlaceHolderID="tContent" Runat="Server">
    <form runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="99%">
        <tr class="title"><td><asp:Label ID="lblPageTitle" runat="server" CssClass="titleLabel" Text="Mast004 - Daily Price Change Report (Store Change)"></asp:Label></td></tr>
        <tr><td style="text-align:left;"><asp:Label ID="lblParam" runat="server" CssClass="captionLabel" Text="Report Parameter"></asp:Label><hr /></td></tr>
        <tr>
            <td>
                <table cellpadding="1" cellspacing="0" class="entry" width="100%">
                    <tr class="d0">
                        <td><asp:Label ID="lblDate" runat="server" CssClass="normalLabel" Text="Effective Date Onward"></asp:Label>
                            <asp:Label ID="Label7" runat="server" CssClass="redLabel" Text="*" Width="3"></asp:Label></td>
                        <td><pom:Calendar ID="Calendar1"  MaxLength="10" runat="server"  /></td>
                    </tr> 
                    
                    
                    <tr class="d0"></tr>
                    
                </table>
            </td>
        </tr>
        <tr>
                    <td colspan="4" align="center"><asp:Button ID="btnViewReport" CssClass="normalButton" runat="server" Text="View Report"></asp:Button></td>
                    </tr>
        <tr>
            <td>
                <iframe id="ifrmReport" runat="server" frameborder="0" src="" width="100%" height="350px"></iframe>
            </td>
        </tr>
    </table>
    </form>
    
</asp:Content>
