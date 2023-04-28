<%@ Page Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ProductEnquirySimp.aspx.vb" Inherits="IM_POM_VB.Net.ProductEnquirySimp" %>

<%@ Register Src="~/controls/ListOfValues.ascx" TagName="ListOfValues" TagPrefix="pom" %>
<asp:Content ID="cProductEnquirySimp" ContentPlaceHolderID="tContent" runat="Server">

    <script language="javascript" type="text/javascript" src="js/masterfile.js"></script>

<%--    <script language="javascript">
var strPrd =  '<%=strPrdtId%>'
    </script>--%>
    <form runat="server">
        <h1><asp:Label ID="lblTitle" runat="server" Text=" Product Enquiry (Simplify)"></asp:Label> </h1>

        <div class="card-header w-25 rounded-top">
           <asp:Label ID="lblSearchH" runat="server" CssClass="text-white h5" Text="Search"></asp:Label>
       </div>

        <div class="card">
            <div class="card-body">
                <div class="form-row">
                    <div class="form-group col-md-6">
                         <asp:Label ID="lblProductCode" runat="server" CssClass="normalLabel" Text="Product Code"></asp:Label><br />
                         <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" AutoPostBack ="True" ></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                         <asp:Label ID="lblProdDesc" runat="server" CssClass="normalLabel" Text="Product Description"></asp:Label><br />
                        <asp:TextBox ID="txtPrdtDesc" runat="server" CssClass="form-control" AutoPostBack = "True" ></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblDept" runat="server" CssClass="normalLabel" Text="Department From"></asp:Label><br />
                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblDeptTo" runat="server" CssClass="normalLabel" Text="Department To"></asp:Label><br />
                        <asp:DropDownList ID="ddlDeptTo" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblCategory" runat="server" CssClass="normalLabel" Text="Category From"></asp:Label><br />
                        <asp:DropDownList ID="ddlCatg" runat="server" CssClass="form-control" AutoPostBack="True" >
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblCatgTo" runat="server" CssClass="normalLabel" Text="Category To"></asp:Label><br />
                        <asp:DropDownList ID="ddlcatgTo" runat="server" CssClass="form-control" AutoPostBack="True" >
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblBarCode" runat="server" CssClass="normalLabel" Text="Product Bar Code"></asp:Label><br />
                        <asp:TextBox ID="txtUPC" runat="server" CssClass="form-control" AutoPostBack = "True" ></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblstatus" runat="server" CssClass="normalLabel" Text="Status"></asp:Label><br />
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack ="True" >
                            </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblBlockOfProcurement" runat="server" CssClass="normalLabel" Text="Blocked For Ordering"></asp:Label><br />
                        <asp:CheckBox ID="cbBlockOfPro" runat="server" Text="YES" Checked="True" AutoPostBack ="True"  />
                            <asp:CheckBox ID="cbBlockOfPro_NO" runat="server" CssClass="ml-2" Text="NO" Checked="True" AutoPostBack="True" />
                    </div>
                    <div class="form-group col-md-6">
                 
                    </div>
                </div>
                 <div class="container d-flex justify-content-end">
                     <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" />
                      <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary ml-2" Text="Reset" />
                 </div>
            </div>
        </div>

        <div class="mt-2 mb-2">
            <asp:Label ID="lblHerr" runat="server" CssClass="normalLabel" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblError" runat="server" CssClass="redLabel"></asp:Label>
        </div>

        <div class="card-header w-25 rounded-top mt-4" >
            <asp:Label ID="lblSearchResult" runat="server" CssClass="text-white h5" Text="Product Details"></asp:Label>
        </div>
        <div class="card">
            <asp:DataGrid ID="dgProduct" GridLines="Vertical" Width="100%" CellPadding="2" AutoGenerateColumns="False"
                        CssClass="table table-bordered" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Product Code">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product_Code")%>'
                                        NavigateUrl='<%# strPrdtId + "?productid=" & DataBinder.Eval(Container.DataItem, "Product_Code") & "&divcode=" & DataBinder.Eval(Container.DataItem, "Division_Code") %>'
                                        ID="hypProductId" />
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Long_Description" HeaderText="Description" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Status" HeaderText="Status" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Isblocked" HeaderText="Blocked for Ordering" ReadOnly ="True" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>

             <div class="d-flex justify-content-center">
                <asp:Label ID="lblPages" runat="server" CssClass="normalLabel" Text="Page Set"></asp:Label>
                <asp:DropDownList ID="ddlPages" runat="server" CssClass="form-control ml-2 mb-2 mr-2 " AutoPostBack="True"
                    Width="100px" Height="10px">
                </asp:DropDownList>
                <asp:Label ID="lblPages1" runat="server" CssClass="normalLabel" Text="(Each Page Set Contains Max 10 Pages)"></asp:Label>
            </div>

        </div>
    </form>
</asp:Content>
