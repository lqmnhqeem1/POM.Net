<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeBehind="VendorEnquiryForm.aspx.vb" Inherits="IM_POM_VB.Net.VendorEnquiryForm" %>

<asp:Content ID="cVendorDetails" ContentPlaceHolderID="tContent" runat="Server">

    <script language="javascript" type="text/javascript" src="js/masterfile.js"></script>

    <%--<table class="table" id="tblVendorDetails" cellspacing="0" cellpadding="0" width="99%" border="0">--%>
        <tr class="title">
            <td>
                Vendor Enquiry</td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblSearch" runat="server" CssClass="captionLabel" Text="Search"></asp:Label><hr />
            </td>
        </tr>
        <tr>
            <td>
                <%--<table class="entry" cellpadding="1" cellspacing="0">--%>
                <table class="d-flex flex-wrap" cellpadding="1" cellspacing="0">
                    <div class="form-row ">
                    <tr class="d0">
                        <div class="form-group col-sm-6">
                        <td class="c0">
                            <asp:Label ID="lblVendorCode" runat="server">Vendor Code</asp:Label></td>
                        <td class="c1">
                            <asp:TextBox ID="txtVendorCode" runat="server" MaxLength="10" CssClass="form-control input-lg"
                                Width="115px" TabIndex="1"></asp:TextBox></td>
                        <td class="c0">
                            <asp:Label ID="lblVendorName" runat="server">Vendor Name</asp:Label></td>
                        <td class="c1">
                            <asp:TextBox ID="txtVendorName" runat="server" MaxLength="50" CssClass="form-control input-lg"
                                Width="150px" TabIndex="2"></asp:TextBox></td>
                        </div>
                    </tr>
                    </div>
                    <div class="form-row">
                    <tr class="d1">
                        <div class="form-group col-sm-6">
                        <td class="c0">
                            <asp:Label ID="lblDepartment" runat="server">Department</asp:Label></td>
                        <td class="c1">
                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"
                                AutoPostBack="true" Width="250px" TabIndex="3">
                            </asp:DropDownList></td>
                        <td class="c0">
                            <asp:Label ID="lblCategory" runat="server">Category</asp:Label>
                        </td>
                        <td class="c1">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" Width="250px"
                                TabIndex="4">
                            </asp:DropDownList></td>
                        </div>
                    </tr>
                    </div>
                    <tr class="d0">
                        <td colspan="4" align="right">
                            <asp:Button ID="btnSearch" runat="server" CssClass="searchButton" Text="Search" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" CssClass="resetButton" Text="Reset" TabIndex="6" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="left" style="height: 53px">
                <asp:Label ID="lblDetails" CssClass="captionLabel" runat="server">Vendor Details</asp:Label><hr />
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 14px">
                <asp:Label ID="lblResults" runat="server" CssClass="redLabel"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 100%;">
                    <asp:DataGrid AllowPaging="True" ID="grdVendorDetails" CellPadding="1" GridLines="Vertical" CssClass="normalDataGrid" Width="100%" AutoGenerateColumns="False"
                        runat="server">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="vendor_code" HeaderText="Vendor Code">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Vendor Name">
                                <ItemStyle Wrap="True" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('VendorCompanyDetails.aspx?vendorcode=' + <%# DataBinder.Eval(Container.DataItem, "vendor_code") %>,null,'height=320,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')">
                                        <%# DataBinder.Eval(Container.DataItem, "vendor_name") %>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle VerticalAlign="Middle" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vendor_name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Vendor Schedule">
                                <HeaderStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle Wrap="True" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSchedule" runat="server" CssClass="normalCheckBox" Enabled="false"
                                        Checked='<%# DataBinder.Eval(Container.DataItem, "Schedule_Flag") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Return_Flag" HeaderText="Return Flag">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ActivePrdCount" HeaderText="Active Products">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DeletedPrdCount" HeaderText="Deleted Products">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="View Vendor Products">
                                <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                <ItemTemplate>
                                    <a href="#" runat="server" id="lnkVendorEnq">
                                        <asp:Image ID="Image1" ImageUrl="~/images/Search.gif" AlternateText="" runat="server" /></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Cost_Edit_Start_On" HeaderText="Cost Change Validity From"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundColumn>
                            <asp:BoundColumn DataField="Cost_Edit_End_On" HeaderText="Cost Change Validity To"><ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundColumn>
                        </Columns>
                        <PagerStyle CssClass="normalDataGridPager" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
    <%--</table>--%>
</asp:Content>
