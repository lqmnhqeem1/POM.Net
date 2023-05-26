#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: Ingredients
''' Version		: 1.0
''' </summary>
''' <remarks>
''' Author		: Amit Rawat
''' Created On	: 04 August 2006
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
Public Class Ingredients
    Inherits System.Web.UI.Page

#Region "Page Load"
    ''' <summary>
    ''' Page Load
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
                objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId) '"1015")
                dsProd = objData.ExecuteSpDataSet("usp_ProdEnq_Ingredients_Search", objXml)
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
    Private Sub AssignValue(ByVal dsValue As DataSet)
        Try
            If (dsValue.Tables(0).Rows.Count <> 0) Then
                lblPrdCodeValue.Text = (dsValue.Tables(0).Rows(0)("product_code")).ToString()
                lblPrdDescvalue.Text = (dsValue.Tables(0).Rows(0)("Long_Description")).ToString()
                lblMalay.Text = (dsValue.Tables(0).Rows(0)("malay_description")).ToString()
                lblMinWeight.Text = (dsValue.Tables(0).Rows(0)("Min_wt")).ToString()
                lblIng1.Text = (dsValue.Tables(0).Rows(0)("ingredient_1")).ToString()
                lblIng2.Text = (dsValue.Tables(0).Rows(0)("ingredient_2")).ToString()
                lblIng3.Text = (dsValue.Tables(0).Rows(0)("ingredient_3")).ToString()
                lblHidEnergy.Text = (dsValue.Tables(0).Rows(0)("energy_per")).ToString()
                lblHidProtein.Text = (dsValue.Tables(0).Rows(0)("protein_per")).ToString()
                lblHidFat.Text = (dsValue.Tables(0).Rows(0)("fat_per")).ToString()
                lblHidCarbo.Text = (dsValue.Tables(0).Rows(0)("carbohydrate_per")).ToString()
                lblHidNutrition.Text = (dsValue.Tables(0).Rows(0)("nutrition_values_per")).ToString()
                lblSerEnergy.Text = (dsValue.Tables(0).Rows(0)("energy_per_srv")).ToString()
                lblSerProtein.Text = (dsValue.Tables(0).Rows(0)("protein_per_srv")).ToString()
                lblSerFat.Text = (dsValue.Tables(0).Rows(0)("fat_per_srv")).ToString()
                lblSerCarbo.Text = (dsValue.Tables(0).Rows(0)("carbohydrate_per_srv")).ToString()
                lblSerNutrition.Text = (dsValue.Tables(0).Rows(0)("nutrition_values_per_srv")).ToString()
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