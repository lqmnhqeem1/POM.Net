<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CurrentRSP.aspx.vb" Inherits="IM_POM_VB.Net.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Current RSP</title>
        <link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
     <link rel="stylesheet" href="/css/styles.css" type="text/css" />
   
</head> 
<body>
    <form id="frmCurrentRSP" runat="server">
        <center>
            <div class="card-header w-50 rounded-top">
           <a class="d-flex justify-content-end text-white" href="javascript:window.close();">Close X</a>
           <asp:Label ID="lblFP" runat="server" CssClass="text-white h5" Text="Current RSP"></asp:Label>
       </div>
            <div class="container alert alert-danger d-flex justify-content-center" id="alertError" runat="server" visible="false">
             <asp:Label ID="lblError" runat="server" CssClass="redLabel" ForeColor="Red"></asp:Label>
        </div>
            <div class="w-50">
                <table class="table table-bordered table-striped d-flex justify-content-center">
            <tr>
                <td>
                    <table id="tblDetail1" runat="server">
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblPrdCode" runat="server" CssClass="normalLabel" Text="Regular Price"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblRegPrice" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblPrdDesc" runat="server" CssClass="normalLabel" Text="Regular Price Update Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblRegPriceDate" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="Label1" runat="server" CssClass="normalLabel" Text="Effective RSP"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblEffecRSP" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblLRCLabel" runat="server" CssClass="normalLabel" Text="Last Received Cost"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblLRC" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblPrdType" runat="server" CssClass="normalLabel" Text="Priority Type Desc"></asp:Label></td>
                            <td class="c1"> 
                                <asp:Label ID="lblPriorDesc" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblPrdOrigin" runat="server" CssClass="normalLabel" Text="Price Event No."></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPriceEventNo" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblPurchaseType" runat="server" CssClass="normalLabel" Text="Price Event Start Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPriceEvtStDt" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="Label3" runat="server" CssClass="normalLabel" Text="Price Event End Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPriceEvtEdDt" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblPrivateLabel" runat="server" CssClass="normalLabel" Text="PWP Price"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPWPPrice" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                               <%-- saber,DCL5789,18/10/2018--%>
                            <td class="c0">
                                <asp:Label ID="lblOnOrder" runat="server" CssClass="normalLabel" Text="GST" visible="false"></asp:Label></td>
                            <td class="c1">
                            
                                <asp:Label ID="lblGST" runat="server" CssClass="normalLabel" Text="" visible="false"></asp:Label></td>
                               <%-- ******************************************************--%>
                        </tr>
                        <tr class="d1" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblBalQty" runat="server" CssClass="normalLabel" Text="PWP Start Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPWPStDt" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblBalValue" runat="server" CssClass="normalLabel" Text="PWP End Date"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblPWPEdDt" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                        <tr class="d0" style="height:18px">
                            <td class="c0">
                                <asp:Label ID="lblWAC" runat="server" CssClass="normalLabel" Text="Wt. Avg. Cost(WAC)"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblWACValue" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                            <td class="c0">
                                <asp:Label ID="lblGP" runat="server" CssClass="normalLabel" Text="GP (%)"></asp:Label></td>
                            <td class="c1">
                                <asp:Label ID="lblGPpct" runat="server" CssClass="normalLabel" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 100%; vertical-align: middle" colspan="4">
                    <asp:Label ID="lblRemark" runat="server" CssClass="normalLabel" 
                        Text="*Remark: Price Event No. for De-centralized item will be blank." /></td>
            </tr>
        </table>
            </div>
        </center>
        
    </form>
</body>
</html>
