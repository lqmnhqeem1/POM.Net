<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="VendorCompanyDetails.aspx.vb" Inherits="IM_POM_VB.Net.VendorCompanyDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vendor Company Details</title>
    <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
     <link rel="stylesheet" href="/css/styles.css" type="text/css" />
</head>
<body>
    <form id="frmFurturePrice" runat="server" >
        <center>
            <div class="card-header w-75 rounded-top">
               <a class="d-flex justify-content-end text-white" href="javascript:window.close();">Close X</a>
               <asp:Label ID="lblFP" runat="server" CssClass="text-white h5" Text="Vendor Company Details"></asp:Label>
            </div>
            <div class="w-75">
                <table class="table table-bordered table-striped d-flex justify-content-center" >
                <tr>
                <td>
                    <table class="entry" runat="server">
                        <tr class="d0" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblVendorCode" runat="server" CssClass="normalLabel" Text="Vendor Code"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblVendorCodeVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblCreateDate" runat="server" CssClass="normalLabel" Text="Creation Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblCreateDateVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblVendorName" runat="server" CssClass="normalLabel" Text="Vendor Name"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblVendorNameVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblCountry" runat="server" CssClass="normalLabel" Text="Country"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblCountryVal" CssClass="normalLabel" runat="server"></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblCurrency" runat="server" CssClass="normalLabel" Text="Currency"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblCurrencyVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblReturnFlag" runat="server" CssClass="normalLabel" Text="Return Flag"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblReturnFlagVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:15px">
                             <td class="c0">
                                <asp:Label ID="lblAddress1" runat="server" CssClass="normalLabel" Text="Address1"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblAddress1Val" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                            </td>
                            <td class="c1">
                            </td>
                        </tr>
                        <tr class="d0" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="Label1" runat="server" CssClass="normalLabel" Text="Address2"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblAddress2Val" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td class="c0">
                            </td>
                            <td class="c1">
                            </td>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblAddress3" runat="server" CssClass="normalLabel" Text="Address3"></asp:Label>
                            </td>
                            <td class="c1">
                                <asp:Label ID="lblAddress3Val" runat="server" CssClass="normalLabel"></asp:Label>
                            </td>
                            <td class="c0">
                            </td>
                            <td class="c1">
                            </td>
                        </tr>
                        <tr class="d0" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblCity" runat="server" CssClass="normalLabel" Text="City"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblCityVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblState" runat="server" CssClass="normalLabel" Text="State"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblStateVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblZip" runat="server" CssClass="normalLabel" Text="Zip"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblZipVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblEmail" runat="server" CssClass="normalLabel" Text="Email"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblEmailVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblAreaCode" runat="server" CssClass="normalLabel" Text="Area Code"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblAreaCodeVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblAreaFaxCode" runat="server" CssClass="normalLabel" Text="Area Fax"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblAreaFaxVal" runat="server" CssClass="normalLabel"></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblTelephone" runat="server" CssClass="normalLabel" Text="Telephone"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblTelephoneVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblFax" runat="server" CssClass="normalLabel" Text=" Fax "></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblFaxval" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:15px">
                            <td class="c0" style="height: 15px">
                                <asp:Label ID="lblCoReg" runat="server" CssClass="normalLabel" Text="Company Reg No."></asp:Label></td>
                            <td class="c1" style="height: 15px">
                                <asp:Label ID="lblCoRegval" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0" style="height: 15px">
                                <asp:Label ID="lblStatus" runat="server" CssClass="normalLabel" Text="Status"></asp:Label></td>
                            <td class="c1" style="height: 15px">
                                <asp:Label ID="lblStatusVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                <asp:Label ID="lblContactPerson" runat="server" CssClass="normalLabel" Text="Contact Person"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblContactPersonVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblContactEmail" runat="server" CssClass="normalLabel" Text="Email Address"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblContactEmailVal" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:15px">
                         <%-- saber,DCL5789,18/10/2018--%>
                            <td class="c0">
                                <asp:Label ID="lblGstReg" runat="server" CssClass="normalLabel" Text=" GST/VAT Reg No. " Visible="false"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblGstRegVal" runat="server" CssClass="normalLabel" Text="" Visible="false"></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblGSTEffDate" runat="server" CssClass="normalLabel" Text="GST Effective Date" Visible="false"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblGSTEffDateValue" runat="server" CssClass="normalLabel" Text="" Visible="false"></asp:Label></td>
                                <%-- ******************************************************--%>
                        </tr>
                        <tr class="d1" style="height:15px">
                            <td class="c0">
                                </td>
                            <td class="c1">
                                </td>
                            <td class="c0">
                                </td>
                            <td class="c1">
                                </td>
                        </tr>
                    </table>
                </td>
                </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>
