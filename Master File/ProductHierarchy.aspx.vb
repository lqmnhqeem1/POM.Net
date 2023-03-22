#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: Product Hierarchy 
''' Version		: 1.0
''' </summary>
''' <remarks>
''' Author		: Amit Rawat
''' Created On	: 02 August 2006
''' Modified By	: 
''' Modified On	:
''' Revision History:
''' </remarks>
#End Region

#Region "Imports"
Imports System.Data
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Security
Imports System.Diagnostics

#End Region
Partial Class ProductHierarchy
    Inherits System.Web.UI.Page
    Protected strPrdtId As String
    Protected strPrdtCd As String

#Region "Load Event"
    ''' <summary>
    ''' Load Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            If Not Page.IsPostBack Then
                Dim objXml As XmlDocument
                Dim objData As DataAccess = New DataAccess()
                Dim dsProd As DataSet = New DataSet()
                strPrdtId = Convert.ToString(Request.QueryString("productid"))
                If (strPrdtId Is Nothing) Then
                    strPrdtId = ""
                End If
                objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId, "Store_Code", Utility.UserStoreId()) '"1015")
                dsProd = objData.ExecuteSpDataSet("usp_ProductHierarchySearch", objXml)
                If (objData.DbMessage.Count <> 0) Then
                    If (objData.DbMessage.Code(0) = "10001") Then
                        lblError.Text = objData.DbMessage.Message(0)
                        Return
                    Else
                        Dim strError As String = objData.DbMessage.Code(0)
                        Server.Transfer("~/ErrorPage.aspx?Code=" & strError)
                        Return
                    End If
                End If
                If (Not dsProd Is Nothing) Then
                    AssignValue(dsProd)
                    'Changes Done by - Himanshu Baiswar for DCL 2059
                    'strPrdtCd = lblPrdDescvalue.Text.Replace("'", " ").Trim()
                    Dim specialChar As String = "~!@#$%^&*<+=`',.?>/\\\"""
                    Dim ret As String() = lblPrdDescvalue.Text.Split(specialChar.ToCharArray())
                    strPrdtCd = String.Concat(ret)
                End If
            End If

            'Dim href As System.Web.UI.HtmlControls.HtmlAnchor
            'Dim l-blText As Label
            FSP.HRef = ""
            CRSP.HRef = ""
            UPC.HRef = ""
            VP.HRef = ""
            LP.HRef = ""
            ING.HRef = ""
            PS.HRef = ""

            FSP.Attributes.Remove("onclick")
            CRSP.Attributes.Remove("onclick")
            UPC.Attributes.Remove("onclick")
            VP.Attributes.Remove("onclick")
            LP.Attributes.Remove("onclick")
            ING.Attributes.Remove("onclick")
            PS.Attributes.Remove("onclick")

            FSP.Style.Add("cursor", "auto")
            CRSP.Style.Add("cursor", "auto")
            UPC.Style.Add("cursor", "auto")
            VP.Style.Add("cursor", "auto")
            LP.Style.Add("cursor", "auto")
            ING.Style.Add("cursor", "auto")
            PS.Style.Add("cursor", "auto")

            If (Not Access.ScreenAccess(Constants.PRODHRCH)) Then
                Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=Product Detail")
                Return
            Else
                Dim strFunc As String() = Access.ScreenFunctions(Constants.PRODHRCH)
                If Not IsNothing(strFunc) Then
                    If (strFunc.Length > 0) Then
                        Dim intcounter As Int32
                        For intcounter = 0 To strFunc.Length - 1
                            If strFunc(intcounter) = Constants.CONST_F1 Then
                                FSP.HRef = "#"
                                FSP.Attributes.Add("onclick", "javascript:window.open('FuturePrice.aspx?productid='+ strPrd,'FP','height=200,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no', 'FSP')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F2 Then
                                CRSP.HRef = "#"
                                CRSP.Attributes.Add("onclick", "javascript:window.open('CurrentRSP.aspx?productid='+ strPrd,'CRSP','height=220,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no', 'CRSP')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F3 Then
                                UPC.HRef = "#"
                                UPC.Attributes.Add("onclick", "javascript:window.open('UPC.aspx?productid='+ strPrd,'UPC','height=350,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no', 'UPC')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F4 Then
                                VP.HRef = "#"
                                VP.Attributes.Add("onclick", "javascript:window.open('VendorProduct.aspx?productid='+ strPrd +'&Desc='+ strPCd,'VP','height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no', 'VP')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F5 Then
                                LP.HRef = "#"
                                LP.Attributes.Add("onclick", "javascript:window.open('LinkProducts.aspx?productid='+ strPrd,'LP','height=450,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no', 'LP')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F6 Then
                                ING.HRef = "#"
                                ING.Attributes.Add("onclick", "javascript:window.open('Ingredients.aspx?productid='+ strPrd,'ING','height=400,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')")
                            ElseIf strFunc(intcounter) = Constants.CONST_F7 Then
                                PS.HRef = "#"
                                PS.Attributes.Add("onclick", "javascript:window.open('ProductStyle.aspx?productid='+ strPrd,'PS','height=150,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')")
                            End If
                        Next
                    End If
                End If
            End If
        Catch httpex As Threading.ThreadAbortException
            'ignore
        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "Assign Values to Controls"
    ''' <summary>
    ''' Used to Assign Values to Controls
    ''' </summary>
    ''' <param name="dsValue"></param>
    ''' <remarks></remarks>
    Private Sub AssignValue(ByVal dsValue As DataSet)
        Try
            If (dsValue.Tables(0).Rows.Count <> 0) Then
                lblPrdCodeValue.Text = (dsValue.Tables(0).Rows(0)("product_code")).ToString().ToUpperInvariant()
                lblPrdDescvalue.Text = (dsValue.Tables(0).Rows(0)("Long_Description")).ToString().ToUpperInvariant()
                lblPrdBrandValue.Text = (dsValue.Tables(0).Rows(0)("Brand")).ToString().ToUpperInvariant()
                lblPrdManuTypeValue.Text = (dsValue.Tables(0).Rows(0)("Manufacturer")).ToString().ToUpperInvariant()
                lblPrdTypeValue.Text = (dsValue.Tables(0).Rows(0)("prd_type")).ToString().ToUpperInvariant()
                lblPrdOriginValue.Text = (dsValue.Tables(0).Rows(0)("prd_origin")).ToString().ToUpperInvariant()
                lblPurchaseTypeValue.Text = (dsValue.Tables(0).Rows(0)("purchase_type")).ToString().ToUpperInvariant()
                lblDistributionTypeValue.Text = (dsValue.Tables(0).Rows(0)("distribution_type")).ToString().ToUpperInvariant()
                '(dsValue.Tables(0).Rows(0)("product_code")).ToString()
                lblOnOrderValue.Text = (dsValue.Tables(0).Rows(0)("Product_OOQty")).ToString().ToUpperInvariant()
                lblBalQtyValue.Text = (dsValue.Tables(0).Rows(0)("product_ohb")).ToString().ToUpperInvariant()
                lblBalValValue.Text = (dsValue.Tables(0).Rows(0)("product_ohb_val")).ToString().ToUpperInvariant()
                lblIndentValue.Text = (dsValue.Tables(0).Rows(0)("Indent")).ToString().ToUpperInvariant()
                '(dsValue.Tables(0).Rows(0)("product_code")).ToString()
                lblCreateDateValue.Text = (dsValue.Tables(0).Rows(0)("create_date")).ToString().ToUpperInvariant()
                lblDerangedValue.Text = (dsValue.Tables(0).Rows(0)("derange_date")).ToString().ToUpperInvariant()
                lblShelfLifeValue.Text = (dsValue.Tables(0).Rows(0)("shelf_life")).ToString().ToUpperInvariant()
                lblPrintDateValue.Text = (dsValue.Tables(0).Rows(0)("PrintExpirydate")).ToString().ToUpperInvariant()
                lblDivs.Text = (dsValue.Tables(0).Rows(0)("DivDesc")).ToString().ToUpperInvariant()
                lblDept.Text = (dsValue.Tables(0).Rows(0)("DepDesc")).ToString().ToUpperInvariant()
                lblCatg.Text = (dsValue.Tables(0).Rows(0)("CatDesc")).ToString().ToUpperInvariant()
                lblSCatg.Text = (dsValue.Tables(0).Rows(0)("ScatDesc")).ToString().ToUpperInvariant()
                lblClass.Text = (dsValue.Tables(0).Rows(0)("ClsDesc")).ToString().ToUpperInvariant()
                lblSClass.Text = (dsValue.Tables(0).Rows(0)("SclsDesc")).ToString().ToUpperInvariant()
                lblLRC.Text = (dsValue.Tables(0).Rows(0)("LRC")).ToString().ToUpperInvariant()
                lblAvgSale.Text = (dsValue.Tables(0).Rows(0)("AvgSales")).ToString().ToUpperInvariant()
                lblStatus.Text = (dsValue.Tables(0).Rows(0)("Status")).ToString().ToUpperInvariant()
                'lbltaxFlag.Text = (dsValue.Tables(0).Rows(0)("tax_Flag")).ToString().ToUpperInvariant()
                lblCentralflagVal.Text = (dsValue.Tables(0).Rows(0)("Centralised")).ToString().ToUpperInvariant()
                lblSetQtyValue.Text = (dsValue.Tables(0).Rows(0)("SetQty")).ToString().ToUpperInvariant()
                lblFreshValue.Text = (dsValue.Tables(0).Rows(0)("IsFresh")).ToString()
                lblLinkProductCodeValue.Text = (dsValue.Tables(0).Rows(0)("UnitLinkPrdCode")).ToString()

                '***********************Added By Rashi Goyal Date: 30th Oct '06***********************
                lblWtVal.Text = (dsValue.Tables(0).Rows(0)("Weight")).ToString().ToUpperInvariant()
                '***************************************End*******************************************

                '***********************Added By Indira Tiwari Date: 30th Oct '06*********************
                lblHSHVFlagVal.Text = (dsValue.Tables(0).Rows(0)("HSHVFlag")).ToString().ToUpperInvariant()
                '***************************************End*******************************************

                '**********************Added By Rashi Goyal Date: 2nd Nov '06*************************
                lblISRangeVal.Text = (dsValue.Tables(0).Rows(0)("IsRanged")).ToString().ToUpperInvariant()
                lblShelfCapacity.Text = (dsValue.Tables(0).Rows(0)("Product_ShelfCapacity")).ToString().ToUpperInvariant()
                lblWACValue.Text = (dsValue.Tables(0).Rows(0)("Wt_Avg_Cost")).ToString().ToUpperInvariant()
                lblIBTonOrdervalue.Text = (dsValue.Tables(0).Rows(0)("IBTOOQ")).ToString().ToUpperInvariant()
                '***************************************End*******************************************

                '**********************Added by YuSeng Dated: 26/02/2007******************************
                lblSalesComValue.Text = (dsValue.Tables(0).Rows(0)("Sales_Commission")).ToString().ToUpperInvariant()
                '***************************************End*******************************************

                'Gaurav Verma - 16 Mar 09 - DCL# 1966 - Display TIHI Value in Product Enquiry
                lblTiHiValue.Text = (dsValue.Tables(0).Rows(0)("TIHIValue")).ToString()

                'Himanshu Baiswar -05 October 09.
                lblIsBlockedValue.Text = (dsValue.Tables(0).Rows(0)("Isblocked")).ToString().ToUpperInvariant()
                If (dsValue.Tables(0).Rows(0)("Isblocked")).ToString().ToUpper = "YES" Then
                    lblIsBlockedDescText.Text = "Blocked Procurement Reason"

                    'trReasonDesc.Visible = True
                    If dsValue.Tables(0).Rows(0)("Block_Ordering_Reason").ToString().Trim.Length > 0 Then
                        lblIsBlockedDesc.Text = String.Concat(dsValue.Tables(0).Rows(0)("Block_Ordering_Reason").ToString(),
                                                               " - ", (dsValue.Tables(0).Rows(0)("Block_Reason")).ToString())
                    Else
                        lblIsBlockedDesc.Text = (dsValue.Tables(0).Rows(0)("Block_Reason")).ToString()
                    End If
                Else
                    lblIsBlockedDescText.Text = ""
                    'trReasonDesc.Visible = False
                End If


                If dsValue.Tables(0).Rows(0)("StyleVarIndicator").ToString = "2" Then
                    With dsValue.Tables(0)
                        'If Product is a Variation Product then show Variation details
                        lblDim1.Text = .Rows(0)("Dim1").ToString()
                        lblDim1Value.Text = .Rows(0)("Dim1_Desc").ToString()
                        lblDim2.Text = .Rows(0)("Dim2").ToString()
                        lblDim2Value.Text = .Rows(0)("Dim2_Desc").ToString()
                    End With
                    trDimension.Visible = True
                End If

                '----------------------------------------------------------------------------------------------
                '---- Modified By	: Foong Kok Loon
                '---- Modified On	: 07 April 2014
                '---- Purpose		: DCL 5078 - KVI Indicator [ Display in Product Enquiry Screen ]
                '----------------------------------------------------------------------------------------------
                lblDCKVIValue.Text = dsValue.Tables(0).Rows(0)("DC_KVI").ToString().ToUpperInvariant()
                lblNonPrivateLabelKVIValue.Text = dsValue.Tables(0).Rows(0)("Non_Private_Label_KVI").ToString().ToUpperInvariant()
                lblPrivateLabelKVIValue.Text = dsValue.Tables(0).Rows(0)("Private_Label_KVI").ToString().ToUpperInvariant()
                '----------------------------------------------------------------------------------------------
                'SDM GST Start
                lblGSTRateValue.Text = dsValue.Tables(0).Rows(0)("GST_Rates").ToString()
                'SDM GST End
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in Assigning Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        Finally
            dsValue = Nothing
        End Try
    End Sub
#End Region
End Class
