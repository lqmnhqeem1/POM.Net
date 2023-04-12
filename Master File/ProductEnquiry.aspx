<%@ Page Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ProductEnquiry.aspx.vb" Inherits="IM_POM_VB.Net.ProductEnquiry" %>

<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">

    <script language="javascript" type="text/javascript" src="../controls/js/clientexe.js"></script>
    
    <script language="javascript" type="text/javascript" src="js/masterfile.js?v=2"></script>

<%--    <script language="javascript" type="text/javascript">
        var strPrd =  '<%=strPrdtId%>'
        function TABLE1_onclick() {
            }
    </script>--%>
    <form runat="server" method="post">
        <h1>  <asp:Label ID="lblTitle" runat="server" Text="Product Enquiry"></asp:Label> </h1>
        
        <div class="card-header w-25 rounded-top">
           <asp:Label ID="lblSearchH" runat="server" CssClass="text-white h5" Text="Search"></asp:Label>
       </div>

        <div class="card">
       
            <div class="card-body">

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblProductCode" runat="server" CssClass="normalLabel" Text="Product Code"></asp:Label><br />
                        <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" AutoPostBack ="true" ></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblProdDesc" runat="server" CssClass="normalLabel" Text="Product Description"></asp:Label>
                         <asp:TextBox ID="txtPrdtDesc" runat="server" CssClass="form-control" AutoPostBack ="true" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblDept" runat="server" CssClass="normalLabel" Text="Department From"></asp:Label><br/>
                        <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblDeptTo" runat="server" CssClass="normalLabel" Text="Department To"></asp:Label><br/>
                        <asp:DropDownList ID="ddlDeptTo" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                       <asp:Label ID="lblCategory" runat="server" CssClass="normalLabel" Text="Category From"></asp:Label><br/>
                       <asp:DropDownList ID="ddlCatg" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblCategoryTo" runat="server" CssClass="normalLabel" Text="Category To"></asp:Label><br />
                        <asp:DropDownList ID="ddlCatgTo" runat="server" CssClass="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblBarCode" runat="server" CssClass="normalLabel" Text="Product Bar Code"></asp:Label><br />
                        <asp:TextBox ID="txtUPC" runat="server" CssClass="form-control" AutoPostBack ="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-6">
                         <asp:Label ID="lblstatus" runat="server" CssClass="normalLabel" Text="Status"></asp:Label><br />
                         <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack ="true" >
                            </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row" runat ="server" id ="WShelfCapacityLine" visible="false">
                    <div class="form-group col-md-6">
                         <asp:Label ID ="lblWithShelfCapacity" runat ="server"  Text="With Shelf Capacity" CssClass ="normalLabel" ></asp:Label><br />
                         <asp:DropDownList ID="ddlWithShelfCapacity" runat ="server" CssClass ="form-control" AutoPostBack ="true"  ></asp:DropDownList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:Label ID="lblFFI" runat="server" CssClass="normalLabel" Text="Front Facing Indicator" Height="8px"></asp:Label><br />
                        <asp:CheckBox ID="cbFFI" runat="server" Text="YES" Checked="True" AutoPostBack ="true"  />
                        <asp:CheckBox ID="cbFFI_NO" runat="server" CssClass="ml-2" Text="NO" Checked="True" AutoPostBack="true" />
                    </div>
                </div>

                <div class="form-row" runat ="server" id="HSHVLine">
                    <div class="form-group col-md-6" id="TDTest1" runat="server">
                       <asp:Label ID="lblBlockOfProcurement" runat="server" CssClass="normalLabel" Text="Blocked For Ordering" Height="8px"></asp:Label><br />
                       <asp:CheckBox ID="cbBlockOfPro" runat="server" Text="YES" Checked="True" AutoPostBack ="true"  />
                       <asp:CheckBox ID="cbBlockOfPro_NO" runat="server" CssClass="ml-2" Text="NO" Checked="True" AutoPostBack="true" />
                    </div>
                    <div class="form-group col-md-6" id="TDTest2" runat="server">
                      <asp:Label ID="lblHghShrHghVal" runat="server" Text="High Shrink / High Value" CssClass="normalLabel"></asp:Label><br/>
                      <asp:CheckBox ID="cbHGHV" runat="server" Text="YES" Checked="True" AutoPostBack="true" />
                      <asp:CheckBox ID="cbHGHV_No" runat="server" CssClass="ml-2" Text="NO" Checked="True" AutoPostBack ="true"  />
                    </div>
                </div>

                <div class="container d-flex justify-content-end">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary ml-2" Text="Reset" />
                </div>
            </div>
        </div>

        <div class="mt-2 mb-2">
            <asp:Label ID="lblHerr" runat="server"></asp:Label> <br />
             <asp:Label ID="lblError" runat="server"></asp:Label><br />
            <asp:Label ID ="lblMessage" runat ="server" CssClass="redLabel" ></asp:Label>
        </div>

        <div class="card-header w-25 rounded-top mt-4" >
           <asp:Label ID="lblSearchResult" runat="server" CssClass="text-white h5" Text="Product Details"></asp:Label>
       </div>
        <div class="card">
            <div class="d-flex justify-content-end">
                 <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload"  Visible="false"/>
                 <asp:Button ID="btnWin7Upload" runat="server" CssClass="btn btn-primary ml-2" Text="Upload (Window 7)" Visible="false"/>
            </div>
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
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:TemplateColumn>
                            <%--Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label--%>
                             <%--<asp:BoundColumn DataField="Product_Code" HeaderText="Product Code" ReadOnly="True">
                                <ItemStyle HorizontalAlign="center"  VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="center"  VerticalAlign="Middle" Width="13%" />
                            </asp:BoundColumn>--%>
                            <asp:TemplateColumn HeaderText="Product Code">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="hlnkPrdSCAddress" Text='<%# DataBinder.Eval(Container.DataItem, "Product_Code") %>'></asp:LinkButton>
                                    <%--<asp:Label ID="lblPrdGroupDetails" runat="server" CssClass="normalLabel" Text='<%# DataBinder.Eval(Container.DataItem, "CONDITION_TYPE") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:TemplateColumn>
                            <%----------------------------------------------------------------------------------%>
                            <asp:BoundColumn DataField="Long_Description" HeaderText="Description" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="HSHVFlag" HeaderText="High Shrink / High Value">
                                <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />   
                            </asp:BoundColumn>
                            <%--Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label--%>
                            <asp:BoundColumn DataField="PreviousExtendedShelfCapacity" HeaderText="Previous Shelf Capacity">
                                <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                             <%---------------------------------------------------------------------------------%>
                             <%--Add By Farnia @ 19 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label--%>
                             <asp:TemplateColumn HeaderText ="No Shelf Capacity">
                                 <ItemTemplate >
                                    <asp:CheckBox ID ="cbWithShelfCapacity" runat ="server" CssClass="normalCheckBox" Checked='<%# DataBinder.Eval(Container.DataItem, "No_ShelfCapacity") %>'/>
                                 </ItemTemplate>
                                 <ItemStyle  Font-Bold ="false" Font-Italic ="false" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                 <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%"/>
                             </asp:TemplateColumn>
                             <%----------------------------------------------------------------------------------%>
                             <%--Add By TSJ @ 20170214 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC)--%>
                               <asp:TemplateColumn HeaderText="Extended Shelf Capacity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtExtendedShelfCapacity" runat ="server"  text='<%#DataBinder.Eval(Container.DataItem, "PreviousExtendedShelfCapacity")%>' CssClass="numericTextBox" MaxLength="6" Width="75px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:TemplateColumn>
                           <%----------------------------------------------------------------------------------%>
                             <%--Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label--%>
                               <asp:TemplateColumn HeaderText="Total Shelf Capacity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtShelfCapacity" runat ="server"  text='<%#DataBinder.Eval(Container.DataItem, "PreviousProductShelfCapacity")%>' CssClass="numericTextBox" MaxLength="6" Width="75px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:TemplateColumn>
                           <%----------------------------------------------------------------------------------%>
                           <%--Add By Saber @ 10 May 2016 For Displaying Front Facing--%>
                              <%--<asp:BoundColumn DataField="Front_Facing" HeaderText="Front Facing" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:BoundColumn>--%>
                           <%----------------------------------------------------------------------------------%>
                            <asp:BoundColumn DataField="Status" HeaderText="Status" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Isblocked" HeaderText="Blocked for Ordering">
                                <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundColumn>
                            <%--saber,18/07/2018, check front facing value--%>
                               <asp:TemplateColumn HeaderText="Front Facing Value" Visible="True">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />                                                
                                                    <ItemTemplate>                                                    
                                                        <asp:Label ID="lblFrontFacing" runat="server" Width="25px"
                                                            CssClass="normalLabel" Text='<%# DataBinder.Eval(Container.DataItem, "FrontFacing")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn> 
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>


            <div class="d-flex justify-content-center">
                <asp:Button ID="btnPrint" runat="server" Height="20px" Text="Print" CssClass="btn btn-info" Visible="False"/>
                <asp:Button ID="btnSave" runat="server" Height="20px" Text="Save" CssClass="btn btn-primary ml-2" Visible="False"/>
            </div>

            <div class="d-flex justify-content-center">
                 <asp:Label ID="lblPages" runat="server" CssClass="normalLabel" Text="Page Set "></asp:Label>
                <asp:DropDownList ID="ddlPages" runat="server" CssClass="form-control m-2 " AutoPostBack="True"
                    Width="50px" Height="20px">
                </asp:DropDownList>
                <asp:Label ID="lblPages1" runat="server" CssClass="normalLabel" Text="(Each Page Set Contains Max 10 Pages)"></asp:Label>
            </div>
        </div>
     </form>
</asp:Content>

