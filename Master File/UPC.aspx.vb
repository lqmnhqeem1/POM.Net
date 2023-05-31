#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: UPC
''' Version		: 1.0
''' </summary>
''' <remarks>
''' Author		: Amit Rawat
''' Created On	: 03 August 2006
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
Public Class UPC
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
                dgUPC.PagerStyle.Visible = False
                FetchData()
            End If
        Catch tex As System.Threading.ThreadAbortException
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

#Region "For implementing Paging and fetching records"
    ''' <summary>
    ''' Used to populate grid and implement paging
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FetchData()
        Dim objData As DataAccess = New DataAccess()
        Dim dsProd As DataSet = New DataSet()
        Dim objXml As XmlDocument
        Try
            Dim strPrdtId As String = ""
            dgUPC.CurrentPageIndex = 0
            strPrdtId = Convert.ToString(Request.QueryString("productid"))
            If (strPrdtId Is Nothing) Then
                strPrdtId = ""
            End If
            objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId)
            dsProd = objData.ExecuteSpDataSet("usp_ProdEnq_UPC_Search", objXml)
            If (objData.DbMessage.Count <> 0) Then
                If (objData.DbMessage.Code(0) = "10001") Then
                    dgUPC.DataSource = GetDataSet().Tables(0)
                    dgUPC.DataBind()
                    lblError.Text = objData.DbMessage.Message(0)
                    dgUPC.PagerStyle.Visible = False
                    Return
                Else
                    Dim strError As String = objData.DbMessage.Code(0)
                    Server.Transfer("~/ErrorPage.aspx?Code=" & strError)
                    Return
                End If
            End If
            If (Not dsProd Is Nothing) Then
                dgUPC.DataSource = dsProd.Tables(0)
                dgUPC.DataBind()
                lblError.Text = ""
                dgUPC.PagerStyle.Visible = True
            End If

        Catch httpex As Threading.ThreadAbortException

        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        Finally
            objData = Nothing
            dsProd = Nothing
            objXml = Nothing
        End Try
    End Sub
#End Region

#Region "Build Dataset"
    ''' <summary>
    ''' Returns Empty Dataset for displaying datagrid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataSet() As DataSet
        Try
            Dim dsEmpty As DataSet = New DataSet()
            Dim dtEmpty As DataTable = New DataTable()
            Dim dcEmpty As DataColumnCollection = dtEmpty.Columns
            dcEmpty.Add("product_UPC", "".GetType)
            dcEmpty.Add("UPC_type", "".GetType)
            dcEmpty.Add("Active_Flag")
            dsEmpty.Tables.Add(dtEmpty)
            Return dsEmpty
        Catch ex As Exception
            Dim objEx As New Exception("Error in reseting Controls", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
            Return Nothing
        End Try

    End Function

#End Region

#Region "Page index change"
    ''' <summary>
    ''' Page index change event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dgUPC_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUPC.PageIndexChanged
        Try
            dgUPC.CurrentPageIndex = e.NewPageIndex()
            FetchData()
        Catch ex As Exception
            Dim objEx As New Exception("Error in Page index change event", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

End Class