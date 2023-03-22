<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductHierarchy.aspx.vb" Inherits="IM_POM_VB.Net.ProductHierarchy" %>
<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">
<%--        <script type="text/javascript" src="js/masterfile.js"></script>

    <script language="javascript" type="text/javascript">
var strPrd;
var strPCd; 
strPrd = '<%=strPrdtId%>';
strPCd = '<%=strPrdtCd%>';
    </script>--%>
    <h1 class="display-5">Product Information</h1>
                <a id="FSP" runat="server" href="#" onclick="window.open('FuturePrice.aspx?productid='+ strPrd,'FP','height=180,width=600,status=yes,toolbar=no,scrollbars=no', 'FSP')">
                    <asp:Label ID="lblFPLink" runat="server" CssClass="normalHyperLink" Text="Future Selling Price"></asp:Label></a>&nbsp;|
                <a id="CRSP" runat="server" href="#" onclick="window.open('CurrentRSP.aspx?productid='+ strPrd,'CRSP','height=180,width=600,status=yes,toolbar=no,scrollbars=no', 'CRSP')">
                    <asp:Label ID="Label2" runat="server" CssClass="normalHyperLink" Text="Current RSP"></asp:Label></a>&nbsp;|
                <a id="UPC" runat="server" href="#" onclick="window.open('UPC.aspx?productid='+ strPrd,'UPC','height=350,width=404,status=yes,toolbar=no,scrollbars=no', 'UPC')">
                    <asp:Label ID="Label11" runat="server" CssClass="normalHyperLink" Text="UPC"></asp:Label></a>&nbsp;|
                <a id="VP" runat="server" href="#" onclick="window.open('VendorProduct.aspx?productid='+ strPrd +'&Desc='+ strPCd,'VP','height=350,width=650,status=yes,toolbar=no,scrollbars=no', 'VP')">
                    <asp:Label ID="Label12" runat="server" CssClass="normalHyperLink" Text="Vendor Product"></asp:Label></a>&nbsp;|
                <a id="LP" runat="server" href="#" onclick="window.open('LinkProducts.aspx?productid='+ strPrd,'LP','height=450,width=600,status=yes,toolbar=no,scrollbars=no', 'LP')">
                    <asp:Label ID="Label13" runat="server" CssClass="normalHyperLink" Text="Link Products"></asp:Label></a>&nbsp;|
                <a id="ING" runat="server" href="#" onclick="window.open('Ingredients.aspx?productid='+ strPrd,'ING','height=350,width=650,status=yes,toolbar=no,scrollbars=no')">
                    <asp:Label ID="Label16" runat="server" CssClass="normalHyperLink" Text="Ingredients"></asp:Label></a>&nbsp;|
                <a id="PS" runat="server" href="#" onclick="window.open('ProductStyle.aspx?productid='+ strPrd,'PS','height=150,width=600,status=yes,toolbar=no,scrollbars=no')">
                    <asp:Label ID="Label17" runat="server" CssClass="normalHyperLink" Text="Product Style"></asp:Label></a>
    <div class="container">
        <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label>
    </div>

    <table class="table table-striped">
        <tbody class="entry" id="tblDetail1" runat="server" cellpadding="1" cellspacing="0" width="100%">
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPrdCode" runat="server" CssClass="normalLabel" Text="Product Code"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdCodeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblPrdDesc" runat="server" CssClass="normalLabel" Text="Product Description"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdDescvalue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblLinkProductCode" runat="server" CssClass="normalLabel" Text="Link Product Code"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblLinkProductCodeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblSetQty" runat="server" CssClass="normalLabel" Text="Set Quantity"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblSetQtyValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label1" runat="server" CssClass="normalLabel" Text="Product Brand"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdBrandValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblPrdManuType" runat="server" CssClass="normalLabel" Text="Manufacturer Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdManuTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPrdType" runat="server" CssClass="normalLabel" Text="Product Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblPrdOrigin" runat="server" CssClass="normalLabel" Text="Product Origin"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrdOriginValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPurchaseType" runat="server" CssClass="normalLabel" Text="Purchase Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPurchaseTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label3" runat="server" CssClass="normalLabel" Text="Distribution Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDistributionTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label9" runat="server" CssClass="normalLabel" Text="Last Received Cost"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblLRC" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblOnOrder" runat="server" CssClass="normalLabel" Text="PO On Order"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblOnOrderValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblBalQty" runat="server" CssClass="normalLabel" Text="On Hand Balance-Qty"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblBalQtyValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblBalValue" runat="server" CssClass="normalLabel" Text="On Hand Balance-Value"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblBalValValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblIndent" runat="server" CssClass="normalLabel" Text="Indent Flag"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblIndentValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblFresh" runat="server" CssClass="normalLabel" Text="Flag For Fresh Products"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblFreshValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblIsRange" runat="server" CssClass="normalLabel" Text="Is Ranged To Store"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblISRangeVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblDeranged" runat="server" CssClass="normalLabel" Text="De-Ranged On Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDerangedValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPrintDate" runat="server" CssClass="normalLabel" Text="Print Expiry Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPrintDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblShelfLife" runat="server" CssClass="normalLabel" Text="Shelf-Life"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblShelfLifeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label4" runat="server" CssClass="normalLabel" Text="Division"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDivs" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label6" runat="server" CssClass="normalLabel" Text="Department"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDept" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label5" runat="server" CssClass="normalLabel" Text="Category"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCatg" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label8" runat="server" CssClass="normalLabel" Text="Sub Category"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblSCatg" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label7" runat="server" CssClass="normalLabel" Text="Class"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblClass" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label10" runat="server" CssClass="normalLabel" Text="Sub Class"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblSClass" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblCentralFlag" runat="server" CssClass="normalLabel" Text="Centralised (Category)"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCentralflagVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label15" runat="server" CssClass="normalLabel" Text="Average Daily Sales"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblAvgSale" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label14" runat="server" CssClass="normalLabel" Text="Product Status"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblStatus" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <%-- 
                            
                        <td class="c0">
                            <asp:Label ID="lblTaxcaption" runat="server" CssClass="normalLabel" Text="Tax Flag"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lbltaxFlag" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            
                            --%>
                        <td class="c0">
                            <asp:Label ID="lblHSHVFlag" runat="server" CssClass="normalLabel" Text="HighShrink HighValue"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblHSHVFlagVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" id="trDimension" visible="false" runat="server" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblDim1" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDim1Value" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblDim2" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDim2Value" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblWt" runat="server" CssClass="normalLabel" Text="Weight"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblWtVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label18" runat="server" CssClass="normalLabel" Text="Shelf Capacity"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblShelfCapacity" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                       
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblCreateDate" runat="server" CssClass="normalLabel" Text="Creation Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCreateDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblIBTOnOrder" runat="server" CssClass="normalLabel" Text="IBT On Order"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblIBTonOrdervalue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblWAC" runat="server" CssClass="normalLabel" Text="Wt. Avg. Cost(WAC)"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblWACValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                        <td class="c0">
                            <asp:Label ID="lblTiHi" runat="server" CssClass="normalLabel" Text="TI * HI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblTiHiValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblSalesCom" runat="server" CssClass="normalLabel" Text="Sales Commission"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblSalesComValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                        <td class="c0" style="height: 18px">
                            <asp:Label ID="lblDCKVI" runat="server" CssClass="normalLabel" Text="Distribution Center KVI"></asp:Label>                       
                        </td>
                        <td class="c1" style="height: 18px">
                            <asp:Label ID="lblDCKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                    </tr>
                    <%--<tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblDCKVI" runat="server" CssClass="normalLabel" Text="Distribution Center KVI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblDCKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                            <br />
                        </td>
                        <td class="c0">
                        </td>
                        <td class="c1">
                        </td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblNonPrivateLabelKVI" runat="server" CssClass="normalLabel" Text="Non Private Label KVI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblNonPrivateLabelKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                            <br />
                        </td>
                        <td class="c0">
                            <asp:Label ID="lblPrivateLabelKVI" runat="server" CssClass="normalLabel" Text="Private Label KVI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblPrivateLabelKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                            <br />
                        </td>
                    </tr>--%>                                        
                    <tr class="d1" style="height:18px">
                    <%-- saber,DCL5789,18/10/2018--%>
                        <td class="c0" style="height: 18px">
                            <asp:Label ID="Label19" runat="server" CssClass="normalLabel" Text="GST Rate" Visible="false"></asp:Label></td>
                        <td class="c1" style="height: 18px">
                            <asp:Label ID="lblGSTRateValue" runat="server" CssClass="normalLabel" Visible="false"></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblNonPrivateLabelKVI" runat="server" CssClass="normalLabel" Text="Non Private Label KVI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblNonPrivateLabelKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                        <%-- ******************************************************--%>
                    </tr>
                    <tr runat="server" id="trReasonDesc" class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblIsBlocked" runat="server" CssClass="normalLabel" Text="Blocked Store PO/IBT to DC"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblIsBlockedValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblPrivateLabelKVI" runat="server" CssClass="normalLabel" Text="Private Label KVI"></asp:Label>
                        </td>
                        <td class="c1">
                            <asp:Label ID="lblPrivateLabelKVIValue" runat="server" CssClass="normalLabel" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0" style="height: 18px">
                            <asp:Label ID="lblIsBlockedDescText" runat="server" CssClass="normalLabel" Text="Blocked Procurement Reason"></asp:Label></td>
                        <td class="c1" style="height: 18px">
                            <asp:Label ID="lblIsBlockedDesc" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0" style="height: 18px"></td>
                        <td class="c1" style="height: 18px"></td>
                    </tr>                                     
                </tbody>
    </table>
</asp:Content>

