<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="VendorPrdtDetails.aspx.vb" Inherits="IM_POM_VB.Net.VendorPrdtDetails" %>
<%@ Register Src="~/controls/ListOfValues.ascx" TagName="ListOfValues" TagPrefix="pom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tContent" runat="server">
    <%--<table id="tblOuter" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tbody>
            <tr class="title">
                <td>
                    Vendor Product Detail</td>
            </tr>
            <tr>
                <td>
                    <table class="entry" cellpadding="1" cellspacing="0">
                        <tr class="d0">
                            <td>
                                <asp:Label ID="Label1" runat="server" CssClass="normalLabel">Vendor Code</asp:Label></td>
                            <td>
                                <pom:ListOfValues ID="lovHamperCode" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" CssClass="normalLabel">Vendor Description</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtVendorDesc" runat="server" NAME="Textbox1" CssClass="normalTextBox"></asp:TextBox></td>
                        </tr>
                        <tr class="d1">
                            <td>
                                <asp:Label ID="Label3" runat="server" CssClass="normalLabel">Department</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="normalDropDownList">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>G1</asp:ListItem>
                                    <asp:ListItem>G2</asp:ListItem>
                                    <asp:ListItem>G3</asp:ListItem>
                                    <asp:ListItem>G4</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:Label ID="Label4" runat="server" CssClass="normalLabel">Category</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlCat" runat="server" CssClass="normalDropDownList">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>001</asp:ListItem>
                                    <asp:ListItem>002</asp:ListItem>
                                    <asp:ListItem>003</asp:ListItem>
                                    <asp:ListItem>005</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="d0">
                            <td colspan="9" align="right">
                                <asp:Button ID="btnSearch" runat="server" CssClass="normalButton" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <td colspan="9">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="readOnlyDataGrid">
                    <tbody>
                        <tr class="title">
                            <td>
                                Product Details</td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;<asp:DataGrid ID="dgProductDetails" runat="server" AutoGenerateColumns="False"
                                    CellPadding="2" CssClass="normalDataGrid" GridLines="Vertical">
                                    <SelectedItemStyle CssClass="normalDataGridSelectedItem" />
                                    <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                                    <ItemStyle CssClass="normalDataGridItem" />
                                    <HeaderStyle CssClass="normalDataGridHeader" />
                                    <Columns>
                                        <asp:HyperLinkColumn HeaderText="Product Code" NavigateUrl="prdtDetails.aspx" Target="_blank"
                                            Text="Product_Code"></asp:HyperLinkColumn>
                                        <asp:BoundColumn DataField="Prod_Desc" HeaderText="Product Description" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FOC" HeaderText="FOC" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Purchange_Type" HeaderText="Purchange Type" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Return_Flag" HeaderText="Return Flag" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Purchase_Cost" HeaderText="Purchase Cost" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Case_Pack" HeaderText="Case Pack" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GST" HeaderText="GST" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid></td>
                        </tr>
                    </tbody>
                </table>--%>
    <div class="card">
            <h5 class="card-header font-weight-light">Vendor Product Details</h5>
             <div class="card-body">
                <form runat="server">
                      <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label ID="Label1" for="inputVendorCode" runat="server">Vendor Code</asp:label>
                          <%--<input type="email" class="form-control" id="txtVendorCode" placeholder="Vendor Code">--%>
                          <asp:Textbox runat="server" class="form-control" id="txtVendorCode" MaxLength="10" width="100%" TabIndex="1"></asp:Textbox>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label ID="Label2" for="inputVendorName" runat="server">Vendor Description</asp:label>
                          <%--<input type="password" class="form-control" id="txtVendorName" placeholder="Vendor Name">--%>
                          <asp:Textbox runat="server" class="form-control" id="txtVendorDesc" name="Textbox1" MaxLength="50" Width="100%" TabIndex="2"></asp:Textbox>
                        </div>
                      </div>
                        <div class="form-row">
                            <div class="form-group col-md-6">
                              <asp:label id="lblDepartment" runat="server" for="inputDept">Department</asp:label>
                                <%--<select id="inputState" class="form-control">
                                    <option selected>--Select--</option>
                                    <option>...</option>
                                </select>--%>
                                <asp:DropDownList ID="ddlDepartment" runat="server" autopostback="true" class="form-control" TabIndex="3">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6">
                              <asp:label runat="server" for="inputCategory">Category</asp:label>
                                <%--<select id="inputState" class="form-control">
                                    <option selected>--Select--</option>
                                    <option>...</option>
                                </select>--%>
                                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" TabIndex="4">
                                </asp:DropDownList>
                            </div>
                          </div>
                      <div class="form-group">
                        <div class="form-check">
                          <input class="form-check-input" type="checkbox" id="gridCheck">
                          <label class="form-check-label" for="gridCheck">
                            Check me out
                          </label>
                        </div>
                      </div>
                        <div class="button-div">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary button-width" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary button-width" TabIndex="6" />
                        </div>
                    </form>
            </div>
        </div>
</asp:Content>