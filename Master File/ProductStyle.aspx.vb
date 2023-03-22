#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: Product Style
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
Partial Class ProductStyle
    Inherits System.Web.UI.Page

#Region "Load Event"
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
                objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId) '"1015")
                dsProd = objData.ExecuteSpDataSet("usp_ProdEnq_Style_Search", objXml)
                If (objData.DbMessage.Count <> 0) Then
                    If (objData.DbMessage.Code(0) = "20008") Then
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
                lblPrdDescvalue.Text = (dsValue.Tables(0).Rows(0)("long_description")).ToString()
                lblStyleCode.Text = (dsValue.Tables(0).Rows(0)("product_code1")).ToString()
                lblStyleDesc.Text = (dsValue.Tables(0).Rows(0)("long_description1")).ToString()
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
