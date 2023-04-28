<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductHierarchySimp.aspx.vb" Inherits="IM_POM_VB.Net.ProductHierarchySimp" %> <%--MasterPageFile="~/Site.Master"--%>
<asp:Content ID="cProductEnquiry" ContentPlaceHolderID="tContent" runat="Server">
       <script type="text/javascript" src="js/masterfile.js"></script>

    <%--<script language="javascript" type="text/javascript">
    var strPrd;
    var strPCd; 
    strPrd = '<%=strPrdtId%>';
    strPCd = '<%=strPrdtCd%>';
    </script>--%>
    <h1 class="display-5">Product Information (Simplify)</h1>   
     <div class="mt-2">
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    <div class="d-flex justify-content-center mt-2 mb-2">
        <a id="lnkBack" runat="server" class="btn btn-secondary" href="javascript:history.back();">
                                                Go Back</a>
    </div>
    <table class="table table-sm table-bordered table-striped">
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
                            <asp:Label ID="lblPurchaseType" runat="server" CssClass="normalLabel" Text="Purchase Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPurchaseTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label3" runat="server" CssClass="normalLabel" Text="Distribution Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDistributionTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="Label9" runat="server" CssClass="normalLabel" Text="Last Received Cost"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblLRC" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblPriceType" runat="server" CssClass="normalLabel" Text="Price Priority Type"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPriceTypeValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblBalQty" runat="server" CssClass="normalLabel" Text="On Hand Balance-Qty"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblBalQtyValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="Label14" runat="server" CssClass="normalLabel" Text="Status"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblStatus" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblRegularPrice" runat="server" CssClass="normalLabel" Text="Regular Price"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblRegularPriceValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblCurrentPrice" runat="server" CssClass="normalLabel" Text="Current Price"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCurrentPriceValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblRegStartDate" runat="server" CssClass="normalLabel" Text="Regular Price Start Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblRegStartDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblCurrentPriceStartDate" runat="server" CssClass="normalLabel" Text="Current Price Start Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCurrentPriceStartDateValue" runat="server" CssClass="normalLabel"
                                Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblRegEndDate" runat="server" CssClass="normalLabel" Text="Regular Price End Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblRegEndDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblCurrentEndDate" runat="server" CssClass="normalLabel" Text="Current Price End Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblCurrentEndDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPWPrice" runat="server" CssClass="normalLabel" Text="PWP Price"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPWPriceValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblIsRange" runat="server" CssClass="normalLabel" Text="Is Ranged"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblISRangeVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPWPriceStartDate" runat="server" CssClass="normalLabel" Text="PWP Price Start Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPWPriceStartDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblDeranged" runat="server" CssClass="normalLabel" Text="De-Ranged On Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblDerangedValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d1" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblPWPriceEndDate" runat="server" CssClass="normalLabel" Text="PWP Price End Date"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblPWPriceEndDateValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                            <asp:Label ID="lblOnOrder" runat="server" CssClass="normalLabel" Text="PO On Order"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblOnOrderValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                    </tr>
                    <tr class="d0" style="height:18px">
                        <td class="c0">
                            <asp:Label ID="lblIBTOnOrder" runat="server" CssClass="normalLabel" Text="IBT On Order"></asp:Label></td>
                        <td class="c1">
                            <asp:Label ID="lblIBTOnOrderValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        <td class="c0">
                        </td>
                        <td class="c1">
                        </td>
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
             
        </tbody>
    </table>
</asp:Content>

