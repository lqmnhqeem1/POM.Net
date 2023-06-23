<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeBehind="VendorProductCostEnquiry.aspx.vb" 
    Inherits="IM_POM_VB.Net.VendorProductCostEnquiry" Title="Vendor Product Cost Enquiry" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>--%>
<%--<%@ Register TagName="ListOfValues" TagPrefix="pom" Src="~/controls/ListOfValues.ascx" %>--%>
<%@ Register TagName="ListOfValues" TagPrefix="pom" Src="~/controls/ListOfValues.ascx" %>

<asp:Content ID="cVendorProductCostEnquiry" ContentPlaceHolderID="tContent" runat="server">
    <script language="javascript" type="text/javascript" src="js/masterfile.js"></script>
    <script language="javascript" type="text/javascript" src="js/masterfile.js"></script>

    <form runat="server" method="post">
        <h1 class="display-5">Vendor Product Cost Enquiry</h1>
        <div class="card-header w-25 rounded-top" >
            <asp:Label ID="lblSearchResult" runat="server" CssClass="text-white h5" Text="Search"></asp:Label>
        </div>
        <div class="card">
             <div class="card-body">
                      <div class="form-row">
                          <div class="form-group col-md-6">
                              <asp:label ID="lblVendorCode" for="inputVendorCode"  runat="server" CssClass="normalLabel">Vendor Code</asp:label>
                              <br />
                              <%--<asp:Textbox runat="server"  class="form-control" id="txtVendorCode" MaxLength="10" TabIndex="1"></asp:Textbox>--%>
                              <%--LOH<pom:ListOfValues ID="lovVendor" runat="server" Code="vend" MaxLength="10" TabIndex="1" />--%>
                              <asp:Textbox runat="server"  class="form-control" id="lovVendor" MaxLength="10" TabIndex="1"></asp:Textbox>
                           </div>
                           <div class="form-group col-md-6">
                              <asp:label ID="lblVendorName" for="inputVendorName" runat="server" CssClass="normalLabel">Vendor Name</asp:label>
                              <br />
                              <asp:Textbox runat="server" class="form-control" id="txtVendorName" MaxLength="50" TabIndex="2"></asp:Textbox>
                            </div>
                      </div>
                      <div class="form-row">
                          <div class="form-group col-md-6">
                              <asp:label ID="lblPoductCode" for="inputProductCode"  runat="server" CssClass="normalLabel">Product Code</asp:label>
                              <br />
                              <%--LOH<pom:ListOfValues ID="lovProduct" runat="server" Code="prod" MaxLength="15" TabIndex="3" />--%>
                              <asp:Textbox runat="server"  class="form-control" id="lovProduct" MaxLength="10" TabIndex="1"></asp:Textbox>
                          </div>
                          <div class="form-group col-md-6">
                              <asp:Label ID="lblProductDesc" CssClass="normalLabel" runat="server">Product Description</asp:Label>
                              <br />
                              <asp:TextBox ID="txtProductDesc" runat="server" MaxLength="50" class="form-control" TabIndex="4"></asp:TextBox>
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
                     <div class="container d-flex justify-content-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary ml-2" Text="Reset" />
                    </div>
             </div>
        </div>
        <br />
        <div class="card-header w-25 rounded-top mt-4" >
            <asp:Label ID="lblDetails" runat="server" CssClass="text-white h5" Text="Vendor Product Details"></asp:Label>
        </div>
        <div class="card">
            <div class="card-body" style="overflow-x: auto;">
                <asp:Label ID="lblResults" runat="server" CssClass="redLabel"></asp:Label>
                <div style="overflow: auto; width: 100%;">
                    <asp:DataGrid AllowPaging="true" PageSize="10" ID="grdVendorProductDetails" GridLines="Vertical"
                        CssClass="table table-bordered" Width="100%" Height="100%" AutoGenerateColumns="False"
                        runat="server" TabIndex="9">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderStyle-Width="5%" HeaderText="Vendor Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkVendorCode" runat="server" CssClass="gridhighlightHyperLink"
                                        NavigateUrl='<%# "VendorProductDetails.aspx?VendorCode=" &  DataBinder.Eval(Container.DataItem,"Vendor_Code") & "&ProductCode=" & DataBinder.Eval(Container.DataItem,"Product_Code") & "&CasePackId=" & DataBinder.Eval(Container.DataItem,"Case_Pack_Id") %>'
                                        Text='<%# DataBinder.Eval(Container.DataItem, "vendor_code") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Vendor_Name" HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="19%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Product_Code" HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Product_Desc" HeaderText="Product Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="19%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Case_Pack_Id" HeaderText="Case Pack Id" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UOM" HeaderText="UOM" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Left" Width="8%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Case_Qty" HeaderText="Case Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ReturnFlag" HeaderText="Return Flag" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LRC" HeaderText="LRC" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Right" Width="6%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RegularCost" HeaderText="Regular Cost" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Right" Width="6%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EffCost" HeaderText="Eff. Cost" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Right" Width="6%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Vendor_Product_Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="6%"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle CssClass="normalDataGridPager" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <asp:Label ID="lblPages" runat="server" CssClass="normalLabel" Text="Page Set"></asp:Label>&nbsp;
        <asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="True" CssClass="normalDropDownList"
            Width="114px" TabIndex="9">
        </asp:DropDownList><asp:Label ID="lblDesc" runat="server" CssClass="normalLabel"
            Text="(Each Page Set Contains Max 10 Pages)"></asp:Label>
    </form>
</asp:Content>
