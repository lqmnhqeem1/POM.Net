#Region "Imports"
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports System.Xml
Imports POM.Lib.Log
#End Region

Partial Class VendorProduct
    Inherits System.Web.UI.Page
    Protected strPrdtId As String
    Protected strPrdDesc As String
    Protected strVendC As String
    Protected strVendN As String
    Protected strCaseId As String
    Protected strCaseQty As String

#Region "Load Event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            strPrdtId = Convert.ToString(Request.QueryString("productid"))
            strPrdDesc = Convert.ToString(Request.QueryString("Desc"))
            If (strPrdtId Is Nothing) Then
                strPrdtId = ""
            End If
            If (strPrdDesc Is Nothing) Then
                strPrdDesc = ""
            End If
            If Not Page.IsPostBack Then
                FetchData()
            End If
        Catch thEx As Threading.ThreadAbortException
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
    Private Sub FetchData()
        Dim objData As DataAccess = New DataAccess()
        Dim dsProd As DataSet = New DataSet()
        Dim objXml As XmlDocument
        Try
            dgVenProduct.CurrentPageIndex = 0

            objXml = DataAccess.BuildXmlParam("Product_Code", strPrdtId, "Store_Code", Utility.UserStoreId())
            dsProd = objData.ExecuteSpDataSet("usp_ProdEnq_VendorProduct_Search", objXml)
            If (objData.DbMessage.Count <> 0) Then
                If (objData.DbMessage.Code(0) = "10001") Then
                    dgVenProduct.DataSource = GetDataSet().Tables(0)
                    dgVenProduct.DataBind()
                    lblError.Text = objData.DbMessage.Message(0)
                    lbtnViewHistory.Enabled = False
                    dgVenProduct.PagerStyle.Visible = False
                    Return
                Else
                    Dim strError As String = objData.DbMessage.Code(0)
                    Server.Transfer("~/ErrorPage.aspx?Code=" & strError)
                    Return
                End If
            End If
            If (Not dsProd Is Nothing) Then
                dgVenProduct.DataSource = dsProd.Tables(0)
                dgVenProduct.DataBind()
                'set the fous at first record radio button
                Dim rdoVendorDetails As RadioButton = CType(dgVenProduct.Items(0).Cells(0).Controls(1), RadioButton)
                rdoVendorDetails.Checked = True
                lbtnViewHistory.Enabled = True
                lblError.Text = ""
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
    Private Function GetDataSet() As DataSet
        Try
            Dim dsEmpty As DataSet = New DataSet()
            Dim dtEmpty As DataTable = New DataTable()
            Dim dcEmpty As DataColumnCollection = dtEmpty.Columns
            dcEmpty.Add("Vendor_Code", "".GetType)
            dcEmpty.Add("Vendor_Name", "".GetType)
            dcEmpty.Add("Case_Id", "".GetType)
            dcEmpty.Add("Case_Qty")
            dcEmpty.Add("Case_Cost")
            dcEmpty.Add("Min_Order_Qty")
            dcEmpty.Add("Purc_qty")
            dcEmpty.Add("Free_Product_Code")
            dcEmpty.Add("Free_Case_Pack_Id")
            dcEmpty.Add("Free_Case_Qty")
            dcEmpty.Add("Free_qty")
            dcEmpty.Add("UOM")
            dcEmpty.Add("UOM_Name")
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

#Region "Data Bound"
    Protected Sub dgVenProduct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgVenProduct.ItemDataBound
        Try
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem
                    CType(e.Item.FindControl("rdoVendorProduct"), RadioButton).Attributes.Add("OnClick", "javascript:exclusiveRadio('" & frmVendorProduct.ClientID & "', '" & CType(e.Item.FindControl("rdoVendorProduct"), RadioButton).ClientID & "');")
            End Select
        Catch ex As Exception
            Dim objEx As New Exception(Reflection.MethodInfo.GetCurrentMethod().Name, ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "Page Index Change"
    Protected Sub dgVenProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgVenProduct.PageIndexChanged
        Try
            dgVenProduct.CurrentPageIndex = e.NewPageIndex()
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

#Region "For Cost History"
    Protected Sub lbtnViewHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnViewHistory.Click
        Try
            Dim intCount As Integer
            For intCount = 0 To dgVenProduct.Items.Count - 1
                Dim rdoVend As RadioButton = CType(dgVenProduct.Items.Item(intCount).Cells(0).Controls(1), RadioButton)
                If rdoVend.Checked = True Then
                    'redirect to the VPCHistory
                    strVendC = dgVenProduct.Items.Item(intCount).Cells(1).Text.Trim().Replace("&nbsp;", "")
                    strVendN = dgVenProduct.Items.Item(intCount).Cells(2).Text.Trim().Replace("&nbsp;", "")
                    strCaseId = dgVenProduct.Items.Item(intCount).Cells(3).Text.Trim().Replace("&nbsp;", "")
                    strCaseQty = dgVenProduct.Items.Item(intCount).Cells(5).Text.Trim().Replace("&nbsp;", "")
                    GoTo Redirect
                End If
            Next

Redirect:
            Response.Write("<script language='javascript'>window.open('VendorProductCostHistory.aspx?VendorCode=" + strVendC &
                            "&ProductCode=" + strPrdtId + "&VendorName=" + strVendN + "&CasePackId=" + strCaseId &
                            "&CaseQty=" + strCaseQty + "&ProductDesc=" + strPrdDesc + "','VPCHistory'," &
                            "'height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no');</script>")

            'Dim rdoBtn As RadioButton = CType(e.Item.FindControl("rdoVendorProduct"), RadioButton)
            'e.Item.ItemType = 
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