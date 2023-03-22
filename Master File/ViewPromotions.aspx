
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeFile="ViewPromotions.aspx.vb" Inherits="ViewPromotions" %>
<%@ Register Src="/controls/Calendar.ascx" TagName="Calendar" TagPrefix="pom" %>
<%--<%@ Register Src="/controls/ListOfValues.ascx" TagName="ListOfValues" TagPrefix="pom" %>--%>

<asp:Content ID="cViewPromotions" ContentPlaceHolderID="tContent" Runat="Server">
	<style type="text/css">
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
    </style>
<script type = "text/javascript" src = "js/masterfile.js"></script>
 <h1 class="display-5">Promotion Information</h1>
<br/>
 <div class="card">
            <h5 class="card-header font-weight-light">Search</h5>
             <div class="card-body">
                <form runat="server">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label id="lblPromoCode" for="lblPromoCode" runat="server">Promotion Code</asp:label>
                          <input type="text" class="form-control" id="txtPromoCode" placeholder="Promotion Code" />
                          <pom:ListOfValues ID = "lovPromoCode" runat="server" code="Promotion" MaxLength="12" TabIndex="1" ></pom:ListOfValues>
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label ID="lblPromoDesc" for="lblPromoDesc" runat="server">Promotion Description</asp:label>
                          <asp:TextBox ID="txtPromoDesc" class="form-control" runat="server" Width="100%" MaxLength="60" TabIndex="2"></asp:TextBox>
                        </div>  
                      </div>  
                    <div class="form-row">
                        <div class="form-group col-md-6">
                          <asp:label ID="lblProductCode" for="lblProductCode" runat="server">Product Code</asp:label>
                          <input type="text" class="form-control" id="txtProductCode" placeholder="Product Code">
                            <pom:ListOfValues ID="lovProductCode" runat="server" maxlength="12" Code="prod" TabIndex="3"/>
							
                        </div>
                        <div class="form-group col-md-6">
                          <asp:label ID="lblProductDesc" for="lblProductDesc" runat="server">Product Description</asp:label>
                          <asp:TextBox ID="txtProductDesc" class="form-control" runat="server" Width="100%" MaxLength="60" TabIndex="2"></asp:TextBox>
                        </div>
                      </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:label id="lblstartdate" runat="server" CssClass="normalLabel" Text="Start Date"></asp:label>
                           <%--- <input type="date" class="form-control" ID="calStartDate" runat="server" TabIndex="5" /> --> ---%>
                            <pom:Calendar ID = "calStartDate" class="form-control" runat="server" TabIndex="5" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:label id="lblEndDate" runat="server" CssClass="normalLabel" Text="End Date"></asp:label>
                           <%--- <input type="date" class="form-control" ID="calEndDate" runat="server" TabIndex="5" /> --> ---%>
                            <pom:Calendar ID="calEndDate" class="form-control" runat="server" TabIndex="5" />
                        </div>
                    </div>
                        <div class="button-div">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary button-width" TabIndex="5" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary button-width" TabIndex="6" />
                        </div>
                    </form>
            </div>
        </div>
    <br />
    <div class="card">
        <h5 class="card-header font-weight-light">View Promotion Tables</h5>
        
<table cellspacing="1" cellpadding="1" width="780" align="center">
			
				
					<tr style="HEIGHT: 5px">
					    <td colspan="5"></td>
					</tr>
				    <tr><td align="left"><asp:Label ID="lblSearchResult" runat="server" CssClass="captionLabel" Text="Promotions"></asp:Label></td></tr>
				    <tr><td style="height:10px" colspan="7">&nbsp;</td></tr>
					        <tr style="height:200px">
								<td colspan="7">
                                    <asp:datagrid ID="grdPromotion" runat="server" AutoGenerateColumns="false" CssClass="normalDataGrid" Width="100%" GridLines="Vertical" AllowPaging="True" PageSize="10" PagerStyle-Mode="numericpages" TabIndex="9">
                                        <HeaderStyle CssClass="normalDataGridHeader" />
                                        <ItemStyle CssClass="normalDataGridItem" />
                                        <AlternatingItemStyle CssClass="normalDataGridAlternatingItem" />
                                        <SelectedItemStyle CssClass="normalDataGridSelectedItem" />
                                        <PagerStyle CssClass="normalDataGridPager" Mode="NumericPages" />
                                        <Columns>
                                        <asp:BoundColumn HeaderText="Promo Code" DataField="Promotion_Code">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                         <asp:TemplateColumn HeaderText="Promotion Theme">
                                            <ItemStyle Wrap="True" />
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkPromoDetail" runat="server" CssClass="normalHyperLink" NavigateUrl='<%# "promotiondetail.aspx?promoCode=" & DataBinder.Eval(Container.DataItem, "Promotion_Code") & "&PromoTheme=" & DataBinder.Eval(Container.DataItem, "Promo_Theme") %>' Text='<%# DataBinder.Eval(Container.DataItem, "Promo_Theme") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                        
                                        <asp:BoundColumn HeaderText="Start Date" DataField="Start_Date">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="End Date" DataField="End_Date">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundColumn>                                        
                                        <asp:BoundColumn HeaderText="Price Priority" DataField="Price_Priority">
                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        </asp:BoundColumn>                                       
                                        </Columns>                                        
                                    </asp:datagrid>
                               </td>
                            </tr>
							<tr>
								<td align="center" colspan="9"></td>
							</tr>
	<tr>
            <td align="center">
                <asp:Label ID="lblNoRecords" runat="server" CssClass="redLabel" Text="No Records Found" visible="false"></asp:Label>                
            </td>
        </tr>
    <tr> 
            <td align="center">
 
                 <asp:Button ID="btnPrint" runat="server" CssClass="normalButton" Text="Print" />
           
            </td>
        </tr>						
				
	</table>

    </div>
</asp:Content>
