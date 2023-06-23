Imports POM.Lib.UI
Imports POM.Lib.Data
Imports POM.Lib.Log
Imports System.Xml
Imports POM.Lib.Security
Imports System.Web.UI

Public Class VendorProductCostEnquiry
    Inherits System.Web.UI.Page
#Region "Objects"

    Dim objXmlDoc As XmlDocument
    Dim objDataAcces As DataAccess
    Dim dsSearchResults As New DataSet

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'set focus on first input field
        'lovVendor.TextEntryHandle.Focus() LOH

        'set the search button as Default button when user press enter key
        Dim strCheckKeyScript As String
        strCheckKeyScript = "<script type=""text/javascript"" language=""javascript"">" &
                            "function checkKey(event){" &
                            "var defcontrol;" &
                            "if(event.keyCode==13){" &
                            "defcontrol = document.getElementById('" & btnSearch.ClientID & "');" &
                            "if(defcontrol != null) defcontrol.focus();" &
                            "}" &
                            "return false;" &
                            "}" &
                            "</script>"

        'check atleast one of the search criteria is entered    
        'validate whether vendor_code is numeric
        '   LOH
        'btnSearch.Attributes.Add("OnClick", "javascript:return validateVendorProductCostEnquiryEntry(" &
        '                    "'" & Utility.GetMessage("20005", lblVendorCode.Text, lblVendorName.Text, lblDepartment.Text, lblPoductCode.Text, lblProductDesc.Text) & "', " &
        '                    "'" & Utility.GetMessage("20002", lblVendorCode.Text) & "', " &
        '                    "'" & Utility.GetMessage("20004", lblPoductCode.Text) & "', " &
        '                    "'" & lovVendor.TextEntryHandle.ClientID & "', " &
        '                    "'" & txtVendorName.ClientID & "', " &
        '                    "'" & lovProduct.TextEntryHandle.ClientID & "', " &  
        '                    "'" & txtProductDesc.ClientID & "', " &
        '                    "'" & ddlDepartment.ClientID & "', " &
        '                    "'" & ddlCategory.ClientID & "');")

        btnSearch.Attributes.Add("OnClick", "javascript:return validateVendorProductCostEnquiryEntry(" &
                            "'" & Utility.GetMessage("20005", lblVendorCode.Text, lblVendorName.Text, lblDepartment.Text, lblPoductCode.Text, lblProductDesc.Text) & "', " &
                            "'" & Utility.GetMessage("20002", lblVendorCode.Text) & "', " &
                            "'" & Utility.GetMessage("20004", lblPoductCode.Text) & "', " &
                            "'" & lovVendor.ClientID & "', " &
                            "'" & txtVendorName.ClientID & "', " &
                            "'" & lovProduct.ClientID & "', " &
                            "'" & txtProductDesc.ClientID & "', " &
                            "'" & ddlDepartment.ClientID & "', " &
                            "'" & ddlCategory.ClientID & "');")

        'make Results label false by default
        lblResults.Visible = False

        Try
            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            btnSearch.Enabled = False
            If (Not Access.ScreenAccess(Constants.VPCENQ)) Then
                Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=" & "Vendor Product Cost Enquiry")
            Else
                Dim strFunc As String() = Access.ScreenFunctions(Constants.VPCENQ)
                If Not IsNothing(strFunc) Then
                    If (strFunc.Length > 0) Then
                        Dim intcounter As Int32
                        For intcounter = 0 To strFunc.Length - 1
                            If strFunc(intcounter) = Constants.CONST_F1 Then
                                btnSearch.Enabled = True
                            End If
                        Next
                    End If
                End If
            End If

            Page.ClientScript.RegisterClientScriptBlock(GetType(String), "checkKey", strCheckKeyScript)

            'populate LOV values
            'lovProduct.AdditionalData = txtProductDesc.ClientID & "=proddesc"      LOH
            'lovVendor.AdditionalData = txtVendorName.ClientID & "=vendname"        LOH

            If Not Page.IsPostBack Then
                'ViewState("PageIndex") = 1
                ShowGridHeader()
                'populate department/category dropdowns
                PopulateDropDowns()

                'to maintain state for go back
                If (Utility.FromGoBack) Then
                    If Not (IsNothing(Session("vendcode")) Or IsNothing(Session("vendname")) Or IsNothing(Session("prodcode")) Or IsNothing(Session("proddesc")) Or IsNothing(Session("deptcode")) Or IsNothing(Session("catcode"))) Then
                        If Not IsNothing(Session("vendcode")) Then
                            'lovVendor.TextEntryHandle.Text = CType(Session("vendcode"), String)    LOH
                            lovVendor.Text = CType(Session("vendcode"), String)     'LOH
                        End If
                        If Not IsNothing(Session("vendname")) Then
                            txtVendorName.Text = CType(Session("vendname"), String)
                        End If
                        If Not IsNothing(Session("prodcode")) Then
                            'lovProduct.Text = CType(Session("prodcode"), String)       LOH
                            lovProduct.Text = CType(Session("prodcode"), String)     'LOH
                        End If
                        If Not IsNothing(Session("proddesc")) Then
                            lovProduct.Text = CType(Session("proddesc"), String)
                        End If
                        If Not IsNothing(Session("deptcode")) Then
                            ddlDepartment.SelectedValue = CType(Session("deptcode"), String)
                        End If
                        '****************Modified By:Indira Tiwari**************
                        ddlDepartment_SelectedIndexChanged(Nothing, Nothing)
                        If Not IsNothing(Session("catcode")) Then
                            ddlCategory.SelectedValue = CType(Session("catcode"), String)
                        End If
                        '********************************************************
                        'check this
                        If Not IsNothing(Session("VPC_PageSet")) Then
                            ViewState("PageIndex") = CType(Session("VPC_PageSet"), String)
                        Else
                            ViewState("PageIndex") = 1
                        End If

                        'ViewState("vendcode") = lovVendor.TextEntryHandle.Text.Trim()       LOH
                        ViewState("vendcode") = lovVendor.Text.Trim()       'LOH
                        ViewState("vendname") = txtVendorName.Text.Trim()
                        'ViewState("prodcode") = lovProduct.TextEntryHandle.Text.Trim()      LOH
                        ViewState("prodcode") = lovProduct.Text.Trim()       'LOH
                        ViewState("proddesc") = txtProductDesc.Text.Trim()
                        ViewState("deptcode") = ddlDepartment.SelectedValue
                        ViewState("catcode") = ddlCategory.SelectedValue
                        BindGrid()
                        If Not IsNothing(Session("VPC_PageNo")) Then
                            grdVendorProductDetails_PageIndexChanged(grdVendorProductDetails, New System.Web.UI.WebControls.DataGridPageChangedEventArgs(grdVendorProductDetails, CType(Session("VPC_PageNo"), Int32)))
                        Else
                        End If
                    End If
                End If
            End If
        Catch thEx As System.Threading.ThreadAbortException
            'ignore
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
    ''' show grid header only
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowGridHeader()
        Try
            Dim dsTempVendor As New DataSet

            'clear the grid
            grdVendorProductDetails.DataSource = dsTempVendor

            'build dataset with the header
            dsTempVendor.Tables.Add()
            dsTempVendor.Tables(0).Columns.Add()
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Vendor_Code", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Vendor_Name", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Product_Code", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Product_Desc", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Case_Pack_Id", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("UOM", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Case_Qty", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("ReturnFlag", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("LRC", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("RegularCost", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("EffCost", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Vendor_Product_Status", System.Type.GetType("System.String")))

            grdVendorProductDetails.DataSource = dsTempVendor
            grdVendorProductDetails.DataBind()

            'hide the pager style
            grdVendorProductDetails.PagerStyle.Visible = False

            grdVendorProductDetails.CurrentPageIndex = 0

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

    ''' <summary>
    ''' Search for the  vendor product details for the given search criteria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            grdVendorProductDetails.CurrentPageIndex = 0
            BindGrid()

            ViewState("PageIndex") = "1"

            'ViewState("vendcode") = lovVendor.TextEntryHandle.Text.Trim()      LOH
            ViewState("vendcode") = lovVendor.Text.Trim()       'LOH
            ViewState("vendname") = txtVendorName.Text.Trim()
            'ViewState("prodcode") = lovProduct.TextEntryHandle.Text.Trim()     LOH
            ViewState("prodcode") = lovProduct.Text.Trim()      'LOH
            ViewState("proddesc") = txtProductDesc.Text.Trim()
            ViewState("deptcode") = ddlDepartment.SelectedValue
            ViewState("catcode") = ddlCategory.SelectedValue

            Session("vendcode") = ViewState("vendcode")
            Session("vendname") = ViewState("vendname")
            Session("prodcode") = ViewState("prodcode")
            Session("proddesc") = ViewState("proddesc")
            Session("deptcode") = ViewState("deptcode")
            Session("catcode") = ViewState("catcode")
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred in performing search operation", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx?code=" & 50001)
        End Try

    End Sub

    ''' <summary>
    ''' bind the grid with the given search criteria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BindGrid()
        Dim strVendorName As String = txtVendorName.Text.Trim().Replace("'", """")
        Dim strProductDescr As String = txtProductDesc.Text.Trim()
        Dim strDepartment As String
        Dim strCategory As String

        If ddlDepartment.SelectedIndex <> 0 Then
            strDepartment = ddlDepartment.SelectedValue.Trim()
        Else
            strDepartment = "-1"
        End If

        If ddlCategory.SelectedIndex <> 0 Then
            strCategory = ddlCategory.SelectedValue.Trim()
        Else
            strCategory = "-1"
        End If

        Dim intStoreCode As Integer = Convert.ToInt32(Utility.UserStoreId)

        If strVendorName.Length = 0 Then
            strVendorName = String.Empty
        End If

        If strProductDescr.Length = 0 Then
            strProductDescr = String.Empty
        End If

        Try
            objDataAcces = New DataAccess
            Dim arrList As ArrayList = Utility.UserDepartments()
            If IsNothing(ViewState("PageIndex")) Then ViewState("PageIndex") = 1
            objXmlDoc = DataAccess.BuildXmlParam("VENDOR_CODE", IIf(lovVendor.Text.Trim().Length = 0, System.DBNull.Value, lovVendor.Text.Trim()), "VENDOR_NAME", strVendorName, "PRODUCT_CODE", IIf(lovProduct.Text.Trim().Length = 0, System.DBNull.Value, lovProduct.Text.Trim()), "PRODUCT_DESC", strProductDescr, "DEPT_CODE", strDepartment, "CAT_CODE", strCategory, "STORE_CODE", intStoreCode, "PAGE_INDEX", CType(ViewState("PageIndex"), String))

            Dim intCount As Int32
            For intCount = 0 To arrList.Count - 1
                DataAccess.BuildXmlParam(objXmlDoc, 2, "DepartmentNo", arrList.Item(intCount).ToString().Trim())
            Next
            dsSearchResults = objDataAcces.ExecuteSpDataSet("usp_VendorProductCostEnquiry", objXmlDoc)

            If ddlPages.Items.Count > 0 Then
                ddlPages.Items.Clear()
            End If

            If (dsSearchResults.Tables(1).Rows.Count = 0) Then
                'when the search results not found
                ShowGridHeader()
                lblResults.Visible = True
                lblResults.Text = Utility.GetMessage("10001")
            Else
                'when the search results found show them same 
                'and show the VendorProducts button as well
                grdVendorProductDetails.DataSource = dsSearchResults.Tables(1)
                grdVendorProductDetails.DataBind()

                Dim i As Integer
                For i = 0 To CType(dsSearchResults.Tables(0).Rows(0).Item(0), Integer) - 1
                    ddlPages.Items.Add(New ListItem(i + 1, i + 1))
                Next
                ddlPages.SelectedIndex = CType(ViewState("PageIndex"), Int32) - 1

                'show the pager style
                grdVendorProductDetails.PagerStyle.Visible = True

            End If

        Catch ex As Exception

            Dim objEx As New Exception(Reflection.MethodInfo.GetCurrentMethod().Name, ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        Finally
            objDataAcces = Nothing
            objXmlDoc = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' reset the page controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            'clear the controls
            ClearControls()

            'show grid header only
            ShowGridHeader()

            'clear the session/viewstate variables
            ViewState("PageIndex") = "1"

            ViewState("vendcode") = ""
            ViewState("vendname") = ""
            ViewState("prodcode") = ""
            ViewState("proddesc") = ""
            ViewState("deptcode") = ""
            ViewState("catcode") = ""
            ViewState("VPC_PageSet") = ""
            ViewState("VPC_PageNo") = ""

            Session("vendcode") = Nothing
            Session("vendname") = Nothing
            Session("prodcode") = Nothing
            Session("proddesc") = Nothing
            Session("deptcode") = Nothing
            Session("catcode") = Nothing
            Session("VPC_PageSet") = Nothing
            Session("VPC_PageNo") = Nothing
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while reseting controls", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx?code=" & 50001)
        End Try


    End Sub

    ''' <summary>
    ''' clear the controls other than Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControls()
        Try
            lovVendor.Text = ""
            lovProduct.Text = ""
            txtVendorName.Text = ""
            txtProductDesc.Text = ""
            ddlPages.Items.Clear()
            'lovVendor.TextEntryHandle.Focus()      LOH
            lovVendor.Text = ""       'LOH
            grdVendorProductDetails.CurrentPageIndex = 0
            'populate department and category drop downs                
            PopulateDropDowns()
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

    ''' <summary>
    ''' populate categories when user selected the department
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        Try
            Dim strDept As String = ddlDepartment.SelectedValue.ToString()
            If strDept = "" Then
                'strDept = "-1"
                'Added by Amit Rawat for Category Enhancement request Starts
                Dim arrayList As ArrayList = Utility.UserDepartments()
                Dim sbldDept As StringBuilder = New StringBuilder()
                Dim strComma As String = ","
                Dim intCount As Int32
                For intCount = 0 To arrayList.Count - 1
                    If (intCount = 0) Then
                        sbldDept.Append(arrayList.Item(intCount))
                    Else
                        sbldDept.Append(strComma + arrayList.Item(intCount))
                    End If
                Next
                strDept = sbldDept.ToString()
                'Added by Amit Rawat for Category Enhancement request Starts
            End If

            Utility.DropDownDataBind(ddlCategory, Constants.Category, strDept)

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

    ''' <summary>
    ''' PageIndex Change
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdVendorProductDetails_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdVendorProductDetails.PageIndexChanged
        Try
            grdVendorProductDetails.CurrentPageIndex = e.NewPageIndex
            'Session("PageIndex") = e.NewPageIndex
            BindGrid()
            Session("VPC_PageNo") = e.NewPageIndex
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while page index change", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx?code=" & 50001)
        End Try
    End Sub

    ''' <summary>
    ''' Data Paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlPages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPages.SelectedIndexChanged
        Try
            ViewState("PageIndex") = ddlPages.SelectedItem.ToString
            grdVendorProductDetails.CurrentPageIndex = 0
            Call BindGrid()
            Session("VPC_PageSet") = ddlPages.SelectedItem.ToString
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while page set change", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("~/ErrorPage.aspx?code=" & 50001)
        End Try
    End Sub

    ''' <summary>
    ''' PopulateDropDowns
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateDropDowns()
        Dim arrayList As ArrayList = Utility.UserDepartments()
        Dim sbldDept As StringBuilder = New StringBuilder()
        Dim strComma As String = ","
        Dim intCount As Int32
        For intCount = 0 To arrayList.Count - 1
            If (intCount = 0) Then
                sbldDept.Append(arrayList.Item(intCount))
            Else
                sbldDept.Append(strComma + arrayList.Item(intCount))
            End If
        Next
        'populate department and category drop downs    
        Utility.DropDownDataBind(ddlDepartment, Constants.DeptCode, sbldDept.ToString())
        'Commented By Amit Rawat for Category Enhancement request Starts
        'Utility.DropDownDataBind(ddlCategory, Constants.Category, "-1")
        'Commented By Amit Rawat for Category Enhancement request Ends

        'Added By Amit Rawat for Category Enhancement request Starts
        Utility.DropDownDataBind(ddlCategory, Constants.Category, sbldDept.ToString())
        'Added By Amit Rawat for Category Enhancement request Ends

    End Sub
End Class