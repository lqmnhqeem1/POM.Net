#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: Future Price
''' Version		: 1.0
''' </summary>
''' <remarks>
''' Author		: Amit Rawat
''' Created On	: 05 August 2006
''' Modified By	: 
''' Modified On	:
''' Revision History:
''' </remarks>
#End Region

#Region "Imports"
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports System.Xml
Imports POM.Lib.Log
#End Region

Public Class FuturePrice
    Inherits System.Web.UI.Page

#Region "Load Region"
    ''' <summary>
    ''' Load Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            If Not Page.IsPostBack Then
                Dim objXml As XmlDocument
                Dim objData As DataAccess = New DataAccess()
                Dim dsProd As DataSet = New DataSet()
                Dim strPrdtId As String = Convert.ToString(Request.QueryString("productid"))
                If (strPrdtId Is Nothing) Then
                    strPrdtId = ""
                End If
                Dim strCost As String = Convert.ToString(Request.QueryString("Cst"))
                If (strCost Is Nothing) Then
                    strCost = ""
                End If
                objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId, "Store_Code", Utility.UserStoreId()) '"1015")
                dsProd = objData.ExecuteSpDataSet("usp_ProdEnq_FuturePrice_Search", objXml)
                If (objData.DbMessage.Count <> 0) Then
                    If (objData.DbMessage.Code(0) = "10001") Then
                        alertError.Visible = true
                        lblError.Text = objData.DbMessage.Message(0)
                        Return
                    Else
                        Dim strError As String = objData.DbMessage.Code(0)
                        Server.Transfer("~/ErrorPage.aspx?Code=" & strError)
                        Return
                    End If
                End If
                If (Not dsProd Is Nothing) Then
                    AssignValue(dsProd, strCost)
                End If
            End If
        Catch httpex As Threading.ThreadAbortException

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
    Private Sub AssignValue(ByVal dsValue As DataSet, ByVal strCst As String)
        Try
            If (dsValue.Tables(0).Rows.Count <> 0) Then
                lblRegPrice.Text = (dsValue.Tables(0).Rows(0)("Reg_Price")).ToString()
                lblRegPriceDate.Text = (dsValue.Tables(0).Rows(0)("Reg_Price_Date")).ToString()
                lblEffecRSP.Text = (dsValue.Tables(0).Rows(0)("Eff_Price")).ToString()
                If (strCst <> "") Then
                    lblLRCLabel.Text = "Cost"
                    lblLRC.Text = Convert.ToDouble(strCst).ToString("#.000")
                Else
                    lblLRC.Text = (dsValue.Tables(0).Rows(0)("LRC")).ToString()
                End If
                lblPriorDesc.Text = (dsValue.Tables(0).Rows(0)("Prior_Desc")).ToString()
                lblPriceEventNo.Text = (dsValue.Tables(0).Rows(0)("Price_Event_No")).ToString()
                lblPriceEvtStDt.Text = (dsValue.Tables(0).Rows(0)("Event_Start")).ToString()
                lblPriceEvtEdDt.Text = (dsValue.Tables(0).Rows(0)("Event_End")).ToString()
                lblPWPPrice.Text = (dsValue.Tables(0).Rows(0)("PWP_Price")).ToString()
                lblGST.Text = (dsValue.Tables(0).Rows(0)("GST")).ToString()
                lblPWPStDt.Text = (dsValue.Tables(0).Rows(0)("PWP_Start")).ToString()
                lblPWPEdDt.Text = (dsValue.Tables(0).Rows(0)("PWP_End")).ToString()
                'lblGP.Text = (dsValue.Tables(0).Rows(0)("energy_per_srv")).ToString()
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