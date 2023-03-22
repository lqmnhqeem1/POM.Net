#Region "Imports"
Imports System.Data
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Security

#End Region
Partial Class Master_File_ProductHierarchySimp
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
                dsProd = objData.ExecuteSpDataSet("usp_ProductHierarchySimpSearch", objXml)
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
                    strPrdtCd = lblPrdDescvalue.Text.Replace("'", " ").Trim()
                End If
            End If

            If (Not Access.ScreenAccess(Constants.PRODHRCH)) Then
                Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=Product Detail")
                Return
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

                lblPurchaseTypeValue.Text = (dsValue.Tables(0).Rows(0)("purchase_type")).ToString().ToUpperInvariant()
                lblDistributionTypeValue.Text = (dsValue.Tables(0).Rows(0)("distribution_type")).ToString().ToUpperInvariant()

                lblOnOrderValue.Text = (dsValue.Tables(0).Rows(0)("Product_OOQty")).ToString().ToUpperInvariant()
                lblBalQtyValue.Text = (dsValue.Tables(0).Rows(0)("product_ohb")).ToString().ToUpperInvariant()

                lblDerangedValue.Text = (dsValue.Tables(0).Rows(0)("derange_date")).ToString().ToUpperInvariant()

                lblLRC.Text = (dsValue.Tables(0).Rows(0)("LRC")).ToString().ToUpperInvariant()

                lblStatus.Text = (dsValue.Tables(0).Rows(0)("Status")).ToString().ToUpperInvariant()



                '**********************Added By Rashi Goyal Date: 2nd Nov '06*************************
                lblISRangeVal.Text = (dsValue.Tables(0).Rows(0)("IsRanged")).ToString().ToUpperInvariant()
                lblIBTOnOrderValue.Text = (dsValue.Tables(0).Rows(0)("IBTOOQ")).ToString().ToUpperInvariant()
                '***************************************End*******************************************


                'If dsValue.Tables(0).Rows(0)("StyleVarIndicator").ToString = "2" Then
                '    With dsValue.Tables(0)
                '        'If Product is a Variation Product then show Variation details
                '        lblDim1.Text = .Rows(0)("Dim1").ToString()
                '        lblDim1Value.Text = .Rows(0)("Dim1_Desc").ToString()
                '        lblDim2.Text = .Rows(0)("Dim2").ToString()
                '        lblDim2Value.Text = .Rows(0)("Dim2_Desc").ToString()
                '    End With
                '    trDimension.Visible = True
                'End If
            End If
            If dsValue.Tables(1).Rows.Count <> 0 Then
                With dsValue.Tables(1)
                    lblPriceTypeValue.Text = .Rows(0)("Prior_Desc").ToString()
                    lblRegularPriceValue.Text = .Rows(0)("Reg_Price").ToString()
                    lblRegStartDateValue.Text = .Rows(0)("Reg_Start").ToString()
                    lblRegEndDateValue.Text = .Rows(0)("Reg_End").ToString()
                    lblCurrentPriceValue.Text = .Rows(0)("Eff_Price").ToString()
                    lblCurrentPriceStartDateValue.Text = .Rows(0)("Event_Start").ToString()
                    lblCurrentEndDateValue.Text = .Rows(0)("Event_End").ToString()
                    lblPWPriceValue.Text = .Rows(0)("PWP_Price").ToString()
                    lblPWPriceStartDateValue.Text = .Rows(0)("PWP_Start").ToString()
                    lblPWPriceEndDateValue.Text = .Rows(0)("PWP_End").ToString()
                End With
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
