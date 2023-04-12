<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductDetails.aspx.vb" Inherits="ProductDetails" %> <%--Inherits="IM_POM_VB.Net.ProductDetails"--%>

<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">
<%--        <script type="text/javascript" src="js/masterfile.js"></script>

    <script language="javascript" type="text/javascript">
var strPrd;
var strPCd; 
strPrd = '<%=strPrdtId%>';
strPCd = '<%=strPrdtCd%>';
    </script>--%>
    <h1 class="display-5">Product Details</h1>

    <a href="# "onclick="window.open('StoreAttribute.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">High Shrink/High Value Flag</a>&nbsp;|&nbsp;
	<a href="#" onclick="window.open('FuturePrice.aspx',null,'height=150,width=600,status=yes,toolbar=no,scrollbars=no')">Future Selling Price</a>&nbsp;|&nbsp; 
    <a href="#" onclick="window.open('PriceInfo.aspx',null,'height=150,width=600,status=yes,toolbar=no,scrollbars=no')">Current RSP</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('UPC.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">UPC</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('VendorProduct.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">Vendor-Product</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('LinkProducts.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">Link Products</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('Ingredients.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">Ingredients</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('ProductStyle.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">Product Style</a>&nbsp;|&nbsp; 
	<a href="#" onclick="window.open('ProductHierarchy.aspx',null,'height=150,width=400,status=yes,toolbar=no,scrollbars=no')">Product Hierarchy</a>&nbsp;|&nbsp; 
								
    
    <table class="table table-sm table-bordered">
        <tbody class="entry" id="tblDetail" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr class="d0">
        <td ><asp:Label ID="lblPrdCode" runat="server" CssClass="normalLabel" Text="Product Code"></asp:Label></td>
        <td ><asp:Label ID="lblPrdCodeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblPrdDesc" runat="server" CssClass="normalLabel" Text="Product Description"></asp:Label></td>
        <td ><asp:Label ID="lblPrdDescvalue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d1">
        <td ><asp:Label ID="lblPrdBrand" runat="server" CssClass="normalLabel" Text="Product Brand"></asp:Label></td>
        <td ><asp:Label ID="lblPrdBrandValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblPrdManuType" runat="server" CssClass="normalLabel" Text="Manufacturer Type"></asp:Label></td>
        <td ><asp:Label ID="lblPrdManuTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d0">
        <td ><asp:Label ID="lblPrdType" runat="server" CssClass="normalLabel" Text="Product Type"></asp:Label></td>
        <td ><asp:Label ID="lblPrdTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblPrdOrigin" runat="server" CssClass="normalLabel" Text="Product Origin"></asp:Label></td>
        <td ><asp:Label ID="lblPrdOriginValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d1">
        <td ><asp:Label ID="lblPurchaseType" runat="server" CssClass="normalLabel" Text="Purchase Type"></asp:Label></td>
        <td ><asp:Label ID="lblPurchaseTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblDistributionType" runat="server" CssClass="normalLabel" Text="Distribution Type"></asp:Label></td>
        <td ><asp:Label ID="lblDistributionTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d0">
        <td ><asp:Label ID="lblPrivateLabel" runat="server" CssClass="normalLabel" Text="Private Label"></asp:Label></td>
        <td ><asp:Label ID="lblPrivateLabelValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblOnOrder" runat="server" CssClass="normalLabel" Text="On Order"></asp:Label></td>
        <td ><asp:Label ID="lblOnOrderValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        
        <tr class="d1">
        <td ><asp:Label ID="lblBalQty" runat="server" CssClass="normalLabel" Text="On Hand Balance-Qty"></asp:Label></td>
        <td ><asp:Label ID="lblBalQtyValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblBalValue" runat="server" CssClass="normalLabel" Text="On Hand Balance-Value"></asp:Label></td>
        <td ><asp:Label ID="lblBalValValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d0">
        <td ><asp:Label ID="lblIndent" runat="server" CssClass="normalLabel" Text="Indent Flag"></asp:Label></td>
        <td ><asp:Label ID="lblIndentValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblFresh" runat="server" CssClass="normalLabel" Text="Flag For Fresh Products"></asp:Label></td>
        <td ><asp:Label ID="lblFreshValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d1">
        <td ><asp:Label ID="lblCreateDate" runat="server" CssClass="normalLabel" Text="Creation Date"></asp:Label></td>
        <td ><asp:Label ID="lblCreateDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblDeranged" runat="server" CssClass="normalLabel" Text="De-Ranged On Date"></asp:Label></td>
        <td ><asp:Label ID="lblDerangedValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        <tr class="d0">
        <td ><asp:Label ID="lblShelfLife" runat="server" CssClass="normalLabel" Text="Shelf-Life"></asp:Label></td>
        <td ><asp:Label ID="lblShelfLifeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        <td ><asp:Label ID="lblPrintDate" runat="server" CssClass="normalLabel" Text="Print Expiry Date"></asp:Label></td>
        <td ><asp:Label ID="lblPrintDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
        </tr>
        </tbody>
    </table>
</asp:Content>


