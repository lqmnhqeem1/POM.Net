#Region "Imports"

Imports POM.Lib.UI
Imports POM.Lib.Data
Imports POM.Lib.Log
Imports System.Xml

#End Region

Partial Class VendorProducts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Utility.UserId = "" Then
                Response.Redirect("~/Login.aspx")
            End If

            If Not Page.IsPostBack Then
                Dim strVendorCode As String = ""
                strVendorCode = Convert.ToString(Request.QueryString("vendorcode"))
                If (strVendorCode Is Nothing) Then
                    ShowGridHeader()
                    lblVendorCodeVal.Text = "-"
                    lblVendorDescVal.Text = "-"
                Else
                    'fill the vendor code and vendor name values
                    lblVendorCodeVal.Text = Convert.ToString(Request.QueryString("vendorcode"))
                    lblVendorDescVal.Text = Replace(Convert.ToString(Request.QueryString("vendorname")), "^^", "&")

                    'fetch the vendor products
                    grdVendorProduct.CurrentPageIndex = 0
                    FetchData()
                    '*********************************************************************
                    'Added by   : Tan Chin Seng (Eric)
                    'Added on   : Jan 19, 2011
                    'Reason     : DCL 2964 - Wrong info on unscheduled vendor on home page
                    '*********************************************************************
                    GetDepartmentCounter()
                    '*********************************************************************
                End If
            End If
        Catch threadEx As Threading.ThreadAbortException

        Catch ex As Exception
            Dim objEx As New Exception(Utility.GetMessage("50001"), ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

            'redirect to the error page
            Response.Redirect("~/ErrorPage.aspx?code=50001")
        End Try
    End Sub

    ''' <summary>
    ''' fetch the vendor product details for the given vendor Code
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FetchData()
        Dim objData As DataAccess = New DataAccess()
        Dim dsVendorProducts As DataSet = New DataSet()
        Dim objXml As XmlDocument
        Dim strVendorCode As String = String.Empty
        Dim strDistType As String = String.Empty
        Try

            Dim strDept As String = String.Empty
            Dim strCat As String = String.Empty
            If Not Request.QueryString("Dept") Is Nothing Then
                strDept = Request.QueryString("Dept")
            End If

            If Not Request.QueryString("Cat") Is Nothing Then
                strCat = Request.QueryString("Cat")
            End If

            '--------------------------------------------------------------
            ' Add by Lu, 28 March 2011, DCL 2978 POM Ordering Enhancement
            If Not Request.QueryString("DistType") Is Nothing Then
                strDistType = Request.QueryString("DistType")
            End If
            '--------------------------------------------------------------

            strVendorCode = Convert.ToString(Request.QueryString("vendorcode"))
            ' Update by Lu, 28 March 2011, DCL 2978 POM Ordering Enhancement
            objXml = DataAccess.BuildXmlParam("VENDOR_CODE", strVendorCode, "STORE_CODE", Utility.UserStoreId(), "Dept", strDept, "Cat", strCat, "DistType", strDistType)
            dsVendorProducts = objData.ExecuteSpDataSet("usp_VendorEnq_VendorProduct_Search", objXml)
            If (objData.DbMessage.Count <> 0) Then
                If (objData.DbMessage.Code(0) = "10001") Then
                    ShowGridHeader()

                    'make invisible the buttons
                    'LOH - Comment
                    lbtnVPCHistory.Visible = False
                    lbtnLRCHistory.Visible = False

                    lblError.Text = objData.DbMessage.Message(0)
                    Return
                Else
                    Dim strError As String = objData.DbMessage.Code(0)
                    Server.Transfer("~/ErrorPage.aspx?code=" & 50001)
                    Return
                End If
            End If
            If (Not dsVendorProducts Is Nothing) Then
                grdVendorProduct.DataSource = dsVendorProducts.Tables(0)
                grdVendorProduct.DataBind()

                'make visible the buttons
                'LOH - Comment
                lbtnVPCHistory.Visible = True
                lbtnLRCHistory.Visible = True

                'set the fous at first record radio button
                'LOH - Comment
                Dim rdoVendorDetails As RadioButton = CType(grdVendorProduct.Items(0).Cells(0).Controls(1), RadioButton)
                rdoVendorDetails.Checked = True

                lblError.Text = ""
            End If

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while binding grid", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        Finally
            objData = Nothing
            dsVendorProducts = Nothing
            objXml = Nothing
        End Try

    End Sub

    '*********************************************************************
    'Added by   : Tan Chin Seng (Eric)
    'Added on   : Jan 19, 2011
    'Reason     : DCL 2964 - Wrong info on unscheduled vendor on home page
    '*********************************************************************
    ''' <summary>
    ''' shows the total different department of the product
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDepartmentCounter()
        Dim dtDept As DataTable
        Dim strDept As String = ""
        Dim arryColumnsName(1) As String
        Dim drDept() As DataRow
        arryColumnsName(0) = "department_code"
        arryColumnsName(1) = "department_desc"

        '***************************************************************************
        '**** Modified On   : Foong Kok Loon
        '**** Modified By   : 2 November 2012
        '**** Purpose       : DCL 4761 - To avoid empty data set for vendor product 
        '****                            grif will cause unexpected error on screen
        '***************************************************************************
        Dim dtVendorProduct As DataTable
        dtVendorProduct = grdVendorProduct.DataSource

        If dtVendorProduct.Rows.Count <> 0 Then
            dtDept = dtVendorProduct.DefaultView.ToTable(True, arryColumnsName)
            drDept = dtDept.Select("", "department_code")
            lblDepartmentVal.Text = dtDept.Rows.Count
            For i As Integer = 0 To drDept.Length - 1
                If strDept <> "" Then strDept &= vbNewLine
                strDept &= drDept(i).Item("department_code").ToString & " (" & drDept(i).Item("department_desc").ToString & ")"
            Next
            lblDepartmentVal.Style.Add("cursor", "pointer")
            If drDept.Length <> 0 Then lblDepartmentVal.Attributes.Add("title", strDept)
        End If
        '***************************************************************************
    End Sub

    ''' <remarks></remarks>
    Private Sub ShowGridHeader()

        Try
            Dim dsTempVendor As New DataSet
            'clear the grid
            grdVendorProduct.DataSource = Nothing

            'build dataset with the header
            dsTempVendor.Tables.Add()
            dsTempVendor.Tables(0).Columns.Add()
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Select", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Product_Code", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Case_Id", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Case_Qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Case_Cost", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Min_Order_Qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Purc_qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Free_Product_Code", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Free_Case_Pack_Id", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Free_Case_Qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Free_qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("UOM", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("UOM_Name", System.Type.GetType("System.String")))

            grdVendorProduct.DataSource = dsTempVendor.Tables(0)
            grdVendorProduct.DataBind()

            'hide the pager style
            grdVendorProduct.PagerStyle.Visible = False

            grdVendorProduct.CurrentPageIndex = 0

        Catch ex As Exception
            Dim objEx As New Exception(Reflection.MethodInfo.GetCurrentMethod().Name, ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        End Try

    End Sub

    Protected Sub grdVendorProduct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdVendorProduct.ItemDataBound
        Try
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem
                    CType(e.Item.FindControl("rdoVendorProduct"), RadioButton).Attributes.Add("OnClick", "javascript:exclusiveRadio('" & "frmVendorProducts" & "', '" & CType(e.Item.FindControl("rdoVendorProduct"), RadioButton).ClientID & "');")
            End Select
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in ItemDataBound event.", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

            'redirect to the error page
            Response.Redirect("~/ErrorPage.aspx?code=50001")

        End Try

    End Sub

    Protected Sub grdVendorProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdVendorProduct.PageIndexChanged
        Try
            grdVendorProduct.CurrentPageIndex = e.NewPageIndex()
            FetchData()
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in PageIndexChange.", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

            'redirect to the error page
            Response.Redirect("~/ErrorPage.aspx?code=50001")

        End Try

    End Sub

    Protected Sub lbtnVPCHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnVPCHistory.Click
        Try
            'set the Hyperlinks here
            Dim objGridItem As DataGridItem
            For Each objGridItem In grdVendorProduct.Items
                Dim rdoVendorProduct As RadioButton = CType(objGridItem.Cells(0).Controls(1), RadioButton)
                If rdoVendorProduct.Checked = True Then
                    'redirect to the VPCHistory
                    Dim sbldScript As StringBuilder = New StringBuilder()
                    sbldScript.Append("<script language='javascript'>window.open('")
                    sbldScript.Append("VendorProductCostHistory.aspx?VendorCode=" & lblVendorCodeVal.Text.Trim())
                    sbldScript.Append("&ProductCode=" & objGridItem.Cells(1).Text.Trim())
                    sbldScript.Append("&VendorName=" & Replace(Replace(lblVendorDescVal.Text.Trim(), "&", "^^"), "'", "\'"))
                    sbldScript.Append("&CasePackId=" & objGridItem.Cells(3).Text.Trim())
                    sbldScript.Append("&CaseQty=" & objGridItem.Cells(4).Text.Trim())
                    sbldScript.Append("&ProductDesc=" & Replace(objGridItem.Cells(2).Text.Trim().Replace("'", " "), "&", "^^"))
                    sbldScript.Append("','VPCHistory','height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no');</script>")

                    Response.Write(sbldScript.ToString())
                End If
            Next
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in redirecting to VPCHistory page.", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

            'redirect to the error page
            Response.Redirect("~/ErrorPage.aspx?code=50001")

        End Try
    End Sub


    Protected Sub lbtnLRCHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnLRCHistory.Click
        Try
            'set the Hyperlinks here
            Dim objGridItem As DataGridItem
            For Each objGridItem In grdVendorProduct.Items
                Dim rdoVendorProduct As RadioButton = CType(objGridItem.Cells(0).Controls(1), RadioButton)
                If rdoVendorProduct.Checked = True Then
                    'redirect to the page LRCHistory
                    Dim sbldScript As StringBuilder = New StringBuilder()
                    sbldScript.Append("<script language='javascript'>window.open('")
                    sbldScript.Append("LastReceivedCostHistory.aspx?VendorCode=" & lblVendorCodeVal.Text.Trim())
                    sbldScript.Append("&ProductCode=" & objGridItem.Cells(1).Text.Trim())
                    sbldScript.Append("&VendorName=" & Replace(Replace(lblVendorDescVal.Text.Trim(), "&", "^^"), "'", "\'"))
                    sbldScript.Append("&CasePackId=" & objGridItem.Cells(3).Text.Trim())
                    sbldScript.Append("&CaseQty=" & objGridItem.Cells(4).Text.Trim())
                    sbldScript.Append("&ProductDesc=" & Replace(objGridItem.Cells(2).Text.Trim().Replace("'", " "), "&", "^^"))
                    sbldScript.Append("','LRCHistory','height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no');</script>")
                    Response.Write(sbldScript.ToString())
                End If
            Next
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in redirecting to page LRCHistory.", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

            'redirect to the error page
            Response.Redirect("~/ErrorPage.aspx?code=50001")

        End Try
    End Sub

End Class