<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ProductEnquiry.aspx.vb" Inherits="IM_POM_VB.Net.ProductEnquiry" %>
<%--<%@ Page Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeFile="ProductEnquiry.aspx.vb"%>--%>
<%@ Register Src="~/controls/ListOfValues.ascx" TagName="ListOfValues" TagPrefix="pom" %>
<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">

    <script type="text/javascript" src="../controls/js/clientexe.js"></script>
    
    <script language="javascript" type="text/javascript" src="js/masterfile.js?v=2"></script>

    <script language="javascript" type="text/javascript">
        //LOH - Comment
        //var strPrd = '<%--=strPrdtId--%>'
        function TABLE1_onclick() {
        }
    </script>
    <div id="tblDetail" runat="server">

    </div>
    <h1><asp:Label ID="lblTitle" runat="server" Text="Product Enquiry"></asp:Label></h1>
    <div class="container d-flex justify-content-end mb-2">
        <asp:Button class="btn btn-primary mr-2" ID="btnUpload" runat="server" Text="Uplaod" Visible="false"></asp:Button>
        <asp:Button class="btn btn-primary" ID="btnWin7Upload" runat="server" Text="Upload (Window 75)" Visible="false"></asp:Button>
    </div>
    <asp:Label ID="lblSearchH" runat="server" CssClass="captionLabel" Text="Search"></asp:Label>
         <asp:Label ID ="lblMessage" runat ="server" CssClass="redLabel" ></asp:Label>
    <div class="card">
             <div class="card-body">
                <form runat="server">
                    <div class="form-row" runat="server" id="TR1">
                        <div class="form-group col-md-6">
                          <asp:label  id="lblProductCode" for="lblProductCode" runat="server"  Text="Product Code"></asp:label>
                          <asp:TextBox ID="txtProductCode" runat="server" class="form-control" MaxLength="15" Width="100%" AutoPostBack ="true" ></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label id="lblProdDesc" for="lblProdDesc" runat="server" Text="Product Description"></asp:label>
                          <asp:TextBox ID="txtPrdtDesc" class="form-control" runat="server" Width="100%" MaxLength="60" TabIndex="2"></asp:TextBox>
                        </div>  
                    </div>  

                    <div class="form-row" id="TR2" runat="server">
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblDept" for="lblDept" runat="server" Text="Department From"></asp:Label><br/>
                          <asp:DropDownList ID="ddlDept" runat="server" Class="form-control" AutoPostBack="True" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:Label ID="lblDeptTo" for="lblDeptTo" runat="server" Text="Department To"></asp:Label><br />
                          <asp:DropDownList ID="ddlDeptTo" runat="server" Class="form-control" AutoPostBack="True" Width="100%"></asp:DropDownList>
                        </div>  
                    </div> 

                    <div class="form-row" id="TR3" runat="server">
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblCategory" for="lblCategory" runat="server" Text="Category From"></asp:Label><br />
                         <asp:DropDownList ID="ddlCatg" runat="server" Class="form-control" AutoPostBack="True" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblCategoryTo" for="lblCategoryTo" runat="server" Text="Category To"></asp:Label><br />
                         <asp:DropDownList ID="ddlCatgTo" runat="server" Class="form-control" AutoPostBack="True" Width="100%"></asp:DropDownList>
                        </div>  
                    </div> 
                   
                    <div class="form-row" id="TR4" runat="server">
                        <div class="form-group col-md-6">
                         <asp:label ID="lblBarCode" for="lblBarCode" runat="server" Text="Product Bar Code"></asp:label>
                         <asp:Textbox  class="form-control" id="txtUPC" runat="server" placeholder="Product Bar Code"></asp:Textbox>
                        </div>
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblstatus" for="lblStatus" runat="server" Text="Status"></asp:Label><br />
                         <asp:DropDownList ID="ddlStatus" runat="server" Class="form-control" AutoPostBack="True" Width="50%"></asp:DropDownList>
                        </div>  
                    </div> 

                    <div class="form-row" runat ="server" id ="WShelfCapacityLine" >
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblWithShelfCapacity" for="lblWithShelfCapacity" runat="server" Text="With Shelf Capacity"></asp:Label><br />
                         <asp:DropDownList ID="ddlWithShelfCapacity" runat="server" Class="form-control" AutoPostBack="True" Width="25%"></asp:DropDownList>
                        </div>  
                        <div class="form-group col-md-6">
                         <asp:Label ID="lblFFI" runat="server" CssClass="normalLabel" Text="Front Facing Indicator"></asp:Label> <br />
                         <asp:CheckBox ID="cbFFI" runat="server" Text="YES" Checked="True" AutoPostBack ="true"  />
                         <asp:CheckBox ID="cbFFI_NO" runat="server" Text="NO" Checked="True" AutoPostBack="true" />
                        </div>
                      </div> 

                    <div class="form-row" runat="server" id="HSHVLine" >
                        <div class="form-group col-md-6">
                            <div runat="server" id="TDTest1">
                              <asp:Label ID="lblBlockOfProcurement" for="lblBlockOfProcurement" runat="server" Text="Blocked For Ordering" ></asp:Label> <br />
                            </div>
                            <div runat="server" id="TDTest2">
                                <asp:CheckBox ID="cbBlockOfPro" runat="server" Text="YES" Checked="True" AutoPostBack ="true" />
                                <asp:CheckBox ID="cbBlockOfPro_NO" runat="server" Text="NO" Checked="True" AutoPostBack="true"  />
                            </div>
              
                        </div>  
                        <div class="form-group col-md-6">
                            <div runat="server" id="TDTest3">
                                <asp:Label ID="lblHghShrHghVal" for="lblHighShrHghVal" runat="server" Text="High Shrink / High Value"></asp:Label> <br />
                            </div>
                            <div runat="server" id="TDTest4">
                                <asp:CheckBox ID="cbHGHV" runat="server" Text="YES" Checked="True" AutoPostBack="true" />
                                <asp:CheckBox ID="cbHGHV_No" runat="server" Text="NO" Checked="True" AutoPostBack ="true" />
                            </div>
                         </div>
                      </div> 
             
                        <div class="button-div" runat ="server" id ="trButton">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary button-width" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary button-width" TabIndex="6" />
                        </div>

            
                        <asp:Label ID="lblHerr" runat="server" CssClass="normalLabel" Text="Page Set"></asp:Label>
                        <asp:DropDownList ID="ddlPages" runat="server" CssClass="normalDropDownList" AutoPostBack="True"
                            Width="114px">
                        </asp:DropDownList>
                        <asp:Label ID="lblSearchResult" runat="server" CssClass="normalLabel" Text="(Each Page Set Contains Max 10 Pages)"></asp:Label>
       
                        <asp:Label ID="lblError" runat="server" CssClass="redLabel"></asp:Label>
     
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
                                 <%--   <asp:HyperLink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Product_Code")%>'
                                        NavigateUrl='<%# strPrdtId + "?productid=" & DataBinder.Eval(Container.DataItem,"Product_Code") & "&divcode=" & DataBinder.Eval(Container.DataItem,"Division_Code") %>'
                                        ID="hypProductId" />--%>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Long_Description" HeaderText="Description" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left" ></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>

                            <asp:BoundColumn DataField="Status" HeaderText="Status" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            
                            
                        </Columns>
                        <PagerStyle Mode="NumericPages" CssClass="normalDataGridPager" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
   
                    <asp:Button ID="btnPrint" runat="server" Height="20px" Text="Print" CssClass="searchButton" Visible="False"/>
            <asp:Button ID="btnSave" runat="server" Height="20px" Text="Save" CssClass="searchButton" Visible="False"/>
    </table>


    </asp:Content>


