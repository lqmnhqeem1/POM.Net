<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductEnquirySimp.aspx.vb" %> <%--Inherits="IM_POM_VB.Net.ProductEnquirySimp" --%>

<%--<%@ Register Src="~/controls/ListOfValues.ascx" TagName="ListOfValues" TagPrefix="pom" %>--%>
<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">

    <script type="text/javascript" src="../controls/js/clientexe.js"></script>
    
   <!-- <script language="javascript" type="text/javascript" src="js/masterfile.js?v=2"></script> -->

    <%--<script language="javascript" type="text/javascript">
        var strPrd =  '<%=strPrdtId%>'
        function TABLE1_onclick() {
        }
    </script>--%>
     <h1 class="display-5">Product Enquiry (Simplify)</h1>
    <div class="container d-flex justify-content-end mb-2">
        <button class="btn btn-primary mr-2" ID="btnUpload" Visible="false">Upload</button>
        <button class="btn btn-primary" ID="btnWin7Upload" Visible="false">Upload (Window 75)</button>
         <%--- &nbsp;<asp:Button ID="btnUpload" runat="server" CssClass="normalButton" Text="Upload"  Visible="false"/>
                <asp:Button ID="btnWin7Upload" runat="server" CssClass="normalButton" Text="Upload (Window 7)" Width="120px" Visible="false"/> ---%>
    </div>
    <div class="card">
            <h5 class="card-header font-weight-light">Search</h5>
             <div class="card-body">
                <form runat="server">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label for="lblProductCode" runat="server">Product Code</asp:label>
                          <input type="text" class="form-control" id="txtProductCode" placeholder="Product Code">
                          <%--- <pom:ListOfValues ID = "ListOfValues1" runat="server" code="Promotion" MaxLength="12" TabIndex="1" /> ---%>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label for="lblProductDesc" runat="server">Product Description</asp:label>
                          <asp:TextBox ID="txtProductDesc" class="form-control" runat="server" Width="100%" MaxLength="60" TabIndex="2"></asp:TextBox>
                        </div>  
                      </div>  

                    <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblDept" runat="server" CssClass="normalLabel" Text="Department From"></asp:Label><br />
                           <asp:DropDownList ID="ddlDept" runat="server" Class="form-control" AutoPostBack="True"
                                Width="100%">
                            </asp:DropDownList>
                          <%--- <pom:ListOfValues ID = "ListOfValues1" runat="server" code="Promotion" MaxLength="12" TabIndex="1" /> ---%>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblDeptTo" runat="server" CssClass="normalLabel" Text="Department To"></asp:Label><br />
                           <asp:DropDownList ID="ddlDeptTo" runat="server" Class="form-control" AutoPostBack="True"
                                Width="100%">
                            </asp:DropDownList>
                        </div>  
                      </div> 

                    <div class="form-row">
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblCategory" runat="server" CssClass="normalLabel" Text="Category From"></asp:Label><br />
                           <asp:DropDownList ID="ddlCatg" runat="server" Class="form-control" AutoPostBack="True"
                                Width="100%"></asp:DropDownList>
                          <%--- <pom:ListOfValues ID = "ListOfValues1" runat="server" code="Promotion" MaxLength="12" TabIndex="1" /> ---%>
                        </div>
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblCategoryTo" runat="server" CssClass="normalLabel" Text="Category To"></asp:Label><br />
                           <asp:DropDownList ID="ddlCatgTo" runat="server" Class="form-control" AutoPostBack="True"
                                Width="100%"></asp:DropDownList>
                        </div>  
                      </div> 
                   
                    <div class="form-row">
                        <div class="form-group col-md-6">
                         <asp:label for="lblBarCode" runat="server">Product Bar Code</asp:label>
                          <asp:Textbox  class="form-control" id="txtUPC" runat="server" placeholder="Product Bar Code"></asp:Textbox>
                          <%--- <pom:ListOfValues ID = "ListOfValues1" runat="server" code="Promotion" MaxLength="12" TabIndex="1" /> ---%>
                        </div>
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblstatus" runat="server" CssClass="normalLabel" Text="Status"></asp:Label><br />
                           <asp:DropDownList ID="ddlStatus" runat="server" Class="form-control" AutoPostBack="True"
                                Width="50%"></asp:DropDownList>
                        </div>  
                      </div> 

                    <div class="form-row">
                       
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblWithShelfCapacity" runat="server" CssClass="normalLabel" Text="With Shelf Capacity"></asp:Label><br />
                           <asp:DropDownList ID="ddlWithShelfCapacity" runat="server" Class="form-control" AutoPostBack="True"
                                Width="25%"></asp:DropDownList>
                        </div>  
                        <div class="form-group col-md-6">
                            <asp:Label ID="lblFFI" runat="server" CssClass="normalLabel" Text="Front Facing Indicator"></asp:Label> <br />
                            <asp:CheckBox ID="cbFFI" runat="server" Text="YES" Checked="True" AutoPostBack ="true"  />
                            <asp:CheckBox ID="cbFFI_NO" runat="server" Text="NO" Checked="True" AutoPostBack="true" />
                        </div>
                      </div> 

                    <div class="form-row">
                       
                        <div class="form-group col-md-6">
                            <asp:Label ID="lblBlockOfProcurement" runat="server" CssClass="normalLabel" Text="Blocked For Ordering" ></asp:Label> <br />
                            <asp:CheckBox ID="cbBlockOfPro" runat="server" Text="YES" Checked="True" AutoPostBack ="true" />
                            <asp:CheckBox ID="cbBlockOfPro_NO" runat="server" Text="NO" Checked="True" AutoPostBack="true"  />
              
                        </div>  
                        <div class="form-group col-md-6">
                            <asp:Label ID="lblHghShrHghVal" runat="server" Text="High Shrink / High Value" CssClass="normalLabel"></asp:Label> <br />
                            <asp:CheckBox ID="cbHGHV" runat="server" Text="YES" Checked="True" AutoPostBack="true" />
                            <asp:CheckBox ID="cbHGHV_No" runat="server" Text="NO" Checked="True" AutoPostBack ="true" />
                        </div>
                      </div> 
             
                        <div class="button-div">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary button-width" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary button-width" TabIndex="6" />
                        </div>
                    </form>
            </div>
        </div>

    <table>
                <tr>
            <td>
                <div style="width: 100%;">
                   <asp:DataGrid ID="dgProduct" GridLines="Vertical" Width="100%" CellPadding="2" AutoGenerateColumns="False"
                        CssClass="normalDataGrid" runat="server" AllowPaging="True">
                        <SelectedItemStyle CssClass="normalDataGridSelectedItem"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="normalDataGridItem"></ItemStyle>
                        <HeaderStyle CssClass="normalDataGridHeader"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Product Code">
                                <ItemTemplate>
                                   <%-- <asp:HyperLink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Product_Code")%>'
                                        NavigateUrl='<%# strPrdtId + "?productid=" & DataBinder.Eval(Container.DataItem,"Product_Code") & "&divcode=" & DataBinder.Eval(Container.DataItem,"Division_Code") %>'
                                        ID="hypProductId" />--%>
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
                </div>
            </td>
        </tr>
       <%-- <tr>
            <td align="center">
                <asp:Label ID="lblPages" runat="server" CssClass="normalLabel" Text="Page Set"></asp:Label>
                <asp:DropDownList ID="ddlPages" runat="server" CssClass="normalDropDownList" AutoPostBack="True"
                    Width="114px">
                </asp:DropDownList>
                <asp:Label ID="lblPages1" runat="server" CssClass="normalLabel" Text="(Each Page Set Contains Max 10 Pages)"></asp:Label></td>
        </tr>
        <tr align="center">
            <td>
                <asp:Label ID="lblError" runat="server" CssClass="redLabel"></asp:Label></td>
        </tr>--%>
    </table>


    </asp:Content>



