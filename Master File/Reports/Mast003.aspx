<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Mast003.aspx.vb" Inherits="IM_POM_VB.Net.Mast003" %>

<asp:Content ID="cMast003" ContentPlaceHolderID="tContent" runat="Server">
    <form runat="server" method="post">
        <h1>  <asp:Label ID="lblTitle" runat="server" Text="Mast003 - SKU Count Report"></asp:Label> </h1>
            
        <div class="card-header w-25 rounded-top">
           <asp:Label ID="lblParam" runat="server" CssClass="text-white h5" Text="Report Parameter"></asp:Label>
       </div>

        <div class="card">
            <div class="card-body">
                
                <div class="form-row">
                    <div class="form-group col-md-6">
                       <asp:Label ID="lblHierSecCap" runat="server" CssClass="normalLabel" Text="Hierarchy Selection"></asp:Label>
                       <asp:Label ID="Label7" runat="server" CssClass="redLabel" Text="*" Width="3"></asp:Label>
                         <asp:DropDownList ID="ddlHierSecVal" runat="server" CssClass="form-control"
                                AutoPostBack="true">
                                <asp:ListItem Text="--Select--" Value="" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Division" Value="DIVISION"></asp:ListItem>
                                <asp:ListItem Text="Department" Value="DEPT"></asp:ListItem>
                                <asp:ListItem Text="Category" Value="CAT"></asp:ListItem>
                                <asp:ListItem Text="Sub-Category" Value="SUBCAT"></asp:ListItem>
                                <asp:ListItem Text="Class" Value="CLASS"></asp:ListItem>
                                <asp:ListItem Text="Sub-Class" Value="SUBCLASS"></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <!-- Empty Col-->
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                       <asp:Label ID="lblRangeFromCap" runat="server" CssClass="normalLabel" Text="Hierarchy Code From"></asp:Label>
                            <asp:Label ID="Label1" runat="server" CssClass="redLabel" Text="*" Width="3"></asp:Label>
                        <asp:DropDownList ID="ddlRangeFromVal" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="" Selected="true"></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblRangeToCap" runat="server" CssClass="normalLabel" Text="Hierarchy Code To"></asp:Label>
                            <asp:Label ID="Label2" runat="server" CssClass="redLabel" Text="*" Width="3"></asp:Label>
                        <asp:DropDownList ID="ddlRangeToVal" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="" Selected="true"></asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="d-flex justify-content-end">
                  <asp:Button ID="btnViewReport" CssClass="btn btn-primary" runat="server" Text="View Report">
                   </asp:Button>
                </div>
                 
            </div>
        </div>

            <table cellpadding="0" cellspacing="0" border="0" width="99%">
     
        

        <tr>
            <td>
                <table cellpadding="1" cellspacing="0" class="entry" width="100%">
                    <tr class="d0">
                        <td class="c0">
                            </td>
                        <td class="c0">
                           
                        </td>
                        <td class="c0">
                        </td>
                        <td class="c1">
                        </td>
                    </tr>
                    <tr class="d1">
                        <td>
                            </td>
                        <td>
                            
                        </td>
                        <td>
                            
                        <td>
                            
                        </td>
                    </tr>
                    <tr class="d0">
                        <td colspan="4" align="right">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <iframe id="ifrmReport" runat="server" frameborder="0" src="" width="100%" height="350px">
                </iframe>
            </td>
        </tr>
    </table>
    </form>

</asp:Content>

