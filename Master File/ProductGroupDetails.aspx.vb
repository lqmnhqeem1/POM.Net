Imports POM.Lib.UI
Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.Log

Partial Class ProductGroupDetails
    Inherits System.Web.UI.Page


#Region "Objects"
    Dim objXmlDoc As XmlDocument
    Dim objDataAcces As DataAccess
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If IsNothing(Utility.UserId) Then Response.Redirect("..\Login.aspx")

            If IsNothing(Request.QueryString("GroupCode")) Then
                Response.Redirect("~/ErrorPage.aspx")
            End If

            If Not IsPostBack Then
                lblProductGroupValue.Text = Request.QueryString("GroupCode")
                'lblGroupDescValue.Text = Request.QueryString("Desc")

                'Add by Farnia @ 20 May 2014 for DCL 5090 - Unable to search range discount with subclass group
                If (Request.QueryString("Mode") = "SCL") Then
                    Me.Title = "Sub Class Group Detail"
                    lblProductGroup.Text = "Sub Class Group Details"
                    lblGroupDesc.Text = "Sub Class Description"

                End If
            End If

            'Add Vy Farnia @ 20 May 2014 for DCL 5090 - Unable to search range discount with subclass group
            If (Request.QueryString("Mode") = "SCL") Then
                dgPrdGrpDetails.Visible = False
                dgSubClassGroupDetail.Visible = True
                FetchData()
            End If

            dgSubClassGroupDetail.Visible = True
            FetchData()

        Catch httpex As Threading.ThreadAbortException
        Catch ex As Exception
            Dim objEx As New Exception("Error in Search", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx")
        End Try
    End Sub

    Private Sub FetchData()
        Try
            If IsNothing(Utility.UserId) Then Response.Redirect("..\Login.aspx")
            Dim objDSGroupDetails As DataSet
            objDataAcces = New DataAccess

            objXmlDoc = DataAccess.BuildXmlParam("PrdGroupCode", Request.QueryString("GroupCode"))
            objDSGroupDetails = objDataAcces.ExecuteSpDataSet("usp_ProductGroupDetails", objXmlDoc)

            'Add by farnia @ 21 May 2014 For DCL 5090 - Unable to search range discount with subclass group
            If (Request.QueryString("Mode") = "SCL") Then
                objXmlDoc = DataAccess.BuildXmlParam("PrdGroupCode", Request.QueryString("GroupCode"))
                objDSGroupDetails = objDataAcces.ExecuteSpDataSet("usp_SubClass_Group_Detail", objXmlDoc)
                dgSubClassGroupDetail.DataSource = objDSGroupDetails.Tables(0)
                dgSubClassGroupDetail.DataBind()
            Else
                objXmlDoc = DataAccess.BuildXmlParam("PrdGroupCode", Request.QueryString("GroupCode"))
                objDSGroupDetails = objDataAcces.ExecuteSpDataSet("usp_ProductGroupDetails", objXmlDoc)
                dgPrdGrpDetails.DataSource = objDSGroupDetails.Tables(0)
                dgPrdGrpDetails.DataBind()
            End If


            If objDSGroupDetails.Tables.Count > 1 Then
                If objDSGroupDetails.Tables(1).Rows.Count > 0 Then
                    lblGroupDescValue.Text = objDSGroupDetails.Tables(1).Rows(0).Item("DESCRIPTION")
                    lblResults.Text = ""
                    btnPrint.Visible = True
                Else
                    lblResults.Text = Utility.GetMessage("10001")
                    btnPrint.Visible = False
                End If
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data from DB", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        End Try
    End Sub

    'Private Sub FetchSubClass()

    Protected Sub dgPrdGrpDetails_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPrdGrpDetails.PageIndexChanged
        Try
            dgPrdGrpDetails.CurrentPageIndex = e.NewPageIndex
            FetchData()
        Catch ex As Exception
            Dim objEx As New Exception("Error in Grid Paging", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx")
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Response.Redirect("~/ErrorPage.aspx?code=10003")
        'Response.Redirect("~/Master File/ProductGroupDetails.aspx?Action=Print")
        'Response.Write("Hello")
        'Response.End()
    End Sub
End Class
