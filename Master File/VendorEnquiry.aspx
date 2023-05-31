<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VendorEnquiry.aspx.vb" Inherits="IM_POM_VB.Net.VendorDetails" %> <%-- Inherits="IM_POM_VB.Net.WebForm2" --%>
<asp:Content ID="Content1" ContentPlaceHolderID="tContent" runat="server" style="zoom:75%;">
    <%--16/2/2023--%>
    <%--<style type="text/css">
        .button-div {
            text-align: right;
        }
        .button-width {
            width: 120px;
            height: 45px;
            margin-right:18px;
        }
        .header-top {
            vertical-align:top !important;
        }
    </style>--%>
    <form runat="server" method="post">
    <h1 class="display-5">Vendor Enquiry</h1>
    <div class="card-header w-25 rounded-top" >
        <asp:Label ID="lblSearchResult" runat="server" CssClass="text-white h5" Text="Search"></asp:Label>
    </div>
        <div class="card">
             <div class="card-body">
                      <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label ID="lblVendorCode" for="inputVendorCode"  runat="server" CssClass="normalLabel">Vendor Code</asp:label>
                          <br />
                          <%--<input type="email" class="form-control" id="txtVendorCode" placeholder="Vendor Code">--%>
                          <asp:Textbox runat="server"  class="form-control" id="txtVendorCode" MaxLength="10" TabIndex="1"></asp:Textbox>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label ID="lblVendorName" for="inputVendorName" runat="server" CssClass="normalLabel">Vendor Name</asp:label>
                          <%--<input type="password" class="form-control" id="txtVendorName" placeholder="Vendor Name">--%>
                          <br />
                          <asp:Textbox runat="server" class="form-control" id="txtVendorName" MaxLength="50" TabIndex="2"></asp:Textbox>
                        </div>
                      </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                              <asp:label id="lblDepartment" CssClass="normalLabel" runat="server" for="inputDept">Department</asp:label>
                                <br />
                                <asp:DropDownList ID="ddlDepartment" runat="server" autopostback="true" class="form-control" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6">
                              <asp:label runat="server" for="inputCategory">Category</asp:label>
                                <%--<select id="inputState" class="form-control">
                                    <option selected>--Select--</option>
                                    <option>...</option>
                                </select>--%>
                                <br />
                                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control"  TabIndex="4">
                                </asp:DropDownList>
                                 <%--CssClass="inputDropDown"--%>
                            </div>
                          </div>
                      <%--LOH - Comment--%>
                      <%--<div class="form-group">
                        <div class="form-check">
                          <input class="form-check-input" type="checkbox" id="gridCheck">
                          <label class="form-check-label" for="gridCheck">
                            Check me out
                          </label>
                        </div>
                      </div>--%>
                        <div class="button-div">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary button-width" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary button-width" TabIndex="6" />
                        </div>
            </div>
        </div>
    <br />
        <div class="card-header w-25 rounded-top mt-4" >
            <asp:Label ID="lblVendorDetails" runat="server" CssClass="text-white h5" Text="Vendor Details"></asp:Label>
        </div>
        <div class="card">
                <%--<Label>Vendor Details</Label>--%>
                <div class="card-body" style="overflow-x: auto;">
                    <asp:Label ID="lblResults" runat="server" CssClass="redLabel"></asp:Label>
                    <div style="overflow: auto; width: 100%;">
                    <asp:DataGrid ID="grdVendorDetails" GridLines="Vertical" Width="100%" CellPadding="2" AutoGenerateColumns="False"
                        CssClass="table table-bordered" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="vendor_code" HeaderText="Vendor Code">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Vendor Name" >
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('VendorCompanyDetails.aspx?vendorcode=' + <%# DataBinder.Eval(Container.DataItem, "vendor_code") %>,null,'height=320,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')">
                                        <%# DataBinder.Eval(Container.DataItem, "vendor_name") %>
                                    </a>
                                </ItemTemplate>
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
                                <%--LOH - Comment--%>
                                <%--<ItemTemplate>
                                    <asp:CheckBox ID="chkSchedule" runat="server" CssClass="normalCheckBox" Enabled="false"
                                        Checked='<%# DataBinder.Eval(Container.DataItem, "Schedule_Flag") %>' />
                                </ItemTemplate>--%>
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
                </div>
        </div>
    </form>
</asp:Content>