'16/2/2023
Imports System.Data
Imports POM.Lib.UI
Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Security

Partial Class VendorDetails
    Inherits System.Web.UI.Page

#Region "Objects"
    Dim objXmlDoc As XmlDocument
    Dim objDataAcces As DataAccess
    '***Added by Rashi Goyal Date: 2nd Oct '06****
    Dim blAccess As Boolean = False
#End Region

    ''' <summary>
    ''' this event initializes the department, category drop downs on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim btnIsAccess As Boolean = False
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

        Try

            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            Page.ClientScript.RegisterClientScriptBlock(GetType(String), "checkKey", strCheckKeyScript)
            btnSearch.Enabled = False

            If (Not Access.ScreenAccess(Constants.VENDENQ)) Then
                Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=" & "Vendor Enquiry")
            Else
                btnIsAccess = Utility.GetFunctionAccess(Constants.VENDENQ, Constants.CONST_F1)
                If (btnIsAccess) Then
                    btnSearch.Enabled = True
                End If
                btnIsAccess = Utility.GetFunctionAccess(Constants.VENDENQ, Constants.CONST_F2)
                If (btnIsAccess) Then
                    blAccess = True
                    grdVendorDetails.Columns(8).Visible = True
                    'btnVendorProducts.Enabled = True
                Else
                    grdVendorDetails.Columns(8).Visible = False
                End If

            End If

            'set the focus to first input field
            txtVendorCode.Focus()

            'hide controls by default
            HideControls()

            'validate the vendor_code
            'For Client validation messages display

            btnSearch.Attributes.Add("OnClick", "return validateVendorDetailsEntry(" &
                                "'" & Utility.GetMessage("20002", lblVendorCode.Text) & "', " &
                                "'" & Utility.GetMessage("20005", lblVendorCode.Text, lblVendorName.Text, lblDepartment.Text) & "', " &
                                "'" & txtVendorName.ClientID & "', " &
                                "'" & ddlDepartment.ClientID & "', " &
                                "'" & ddlCategory.ClientID & "', " &
                                "'" & txtVendorCode.ClientID & "');")

            If Not Page.IsPostBack Then
                'populate department/category
                PopulateDropDowns()

                'show grid with header only
                ShowGridHeader()

                '***Commented By Rashi Goyal Date: 2nd Nov '06*********
                'hide vendor products button 
                'btnVendorProducts.Visible = False
                '******************END*********************************
            End If

        Catch thEx As Threading.ThreadAbortException
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
    ''' hide results label and vendorproducts button
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControls()

        lblResults.Visible = False

    End Sub

    ''' <summary>
    ''' populate dropdowns
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateDropDowns()
        Try
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

            'Populate Department
            Utility.DropDownDataBind(ddlDepartment, Constants.DeptCode, sbldDept.ToString())

            'Populate Category
            'Commented By Amit Rawat for Category Enhancement request Starts 
            'Utility.DropDownDataBind(ddlCategory, Constants.Category, "-1")
            'Commented By Amit Rawat for Category Enhancement request Ends

            'Added By Amit Rawat for Category Enhancement request Starts 
            Utility.DropDownDataBind(ddlCategory, Constants.Category, sbldDept.ToString())
            'Added By Amit Rawat for Category Enhancement request Ends
        Catch ex As Exception
            Dim objEx As New Exception("Error occurred in populating dropdowns", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        End Try
    End Sub

    ''' <summary>
    ''' show the grid with header only
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowGridHeader()

        Try
            Dim dsTempVendor As New DataSet
            'clear the grid
            grdVendorDetails.DataSource = dsTempVendor

            'build dataset with the header
            dsTempVendor.Tables.Add()
            dsTempVendor.Tables(0).Columns.Add()
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Select", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Vendor_Code", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Vendor_Name", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Status", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("Return_Flag", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("ActivePrdCount", System.Type.GetType("System.String")))
            dsTempVendor.Tables(0).Columns.Add(New DataColumn("DeletedPrdCount", System.Type.GetType("System.String")))

            grdVendorDetails.DataSource = dsTempVendor
            grdVendorDetails.DataBind()

            'hide the pager style
            grdVendorDetails.PagerStyle.Visible = False

            grdVendorDetails.CurrentPageIndex = 0

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
    ''' reset the grid and the rest of the controls on the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Try
            'clear the controls on the form
            ClearControls()

            'bind the grid with the header
            ShowGridHeader()

            '*************Commented By Rashi Goyal Date: 2nd Nov '06*************
            'hide vendor products
            'btnVendorProducts.Visible = False
            '*****************************END************************************

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in reseting controls.", ex)
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
    ''' clear the controls of the form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControls()
        Try
            'clear the controls
            txtVendorCode.Text = ""
            txtVendorName.Text = ""
            grdVendorDetails.CurrentPageIndex = 0
            PopulateDropDowns()
            txtVendorCode.Focus()
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while clearing controls.", ex)
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
    ''' this event populates the category drop down corresponding to the selected department
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        Try
            'populate corresponding categories on selection of a Department
            Dim strDept = ddlDepartment.SelectedValue.ToString()
            If strDept = "" Then
                'Added By Amit Rawat for Category Enhancement request Starts 
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
                'Added By Amit Rawat for Category Enhancement request Ends
            End If
            Utility.DropDownDataBind(ddlCategory, Constants.Category, strDept)
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in populating category when selected department.", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            'redirect to the error page
            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch th As Threading.ThreadAbortException
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' this event populates the grid with the given searching criteria 
    ''' on click event of search button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            grdVendorDetails.CurrentPageIndex = 0
            'bind the grid
            BindGrid()
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred while searching.", ex)
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
    ''' bind the grid with the given search criteria
    ''' </summary>    
    ''' <remarks></remarks>
    Private Sub BindGrid()

        Dim strVendorName As String = txtVendorName.Text.Trim().Replace("'", """")
        Dim strDepartment As String
        Dim strCategory As String

        strDepartment = ddlDepartment.SelectedItem.Value.Trim()
        strCategory = ddlCategory.SelectedItem.Value.Trim()

        Dim intStoreCode As Integer = Convert.ToInt32(Utility.UserStoreId)

        If strVendorName.Length = 0 Then
            strVendorName = String.Empty
        End If

        Dim dsSearchResults As New DataSet

        Try
            objDataAcces = New DataAccess
            Dim arrList As ArrayList = Utility.UserDepartments()
            objXmlDoc = DataAccess.BuildXmlParam("VENDOR_CODE", IIf(txtVendorCode.Text.Trim().Length = 0, System.DBNull.Value, txtVendorCode.Text.Trim()), "VENDOR_NAME", strVendorName, "DEPARTMENT_CODE", strDepartment, "CATEGORY_CODE", strCategory, "STORE_CODE", intStoreCode)

            Dim intCount As Int32
            For intCount = 0 To arrList.Count - 1
                DataAccess.BuildXmlParam(objXmlDoc, 2, "DepartmentNo", arrList.Item(intCount).ToString().Trim())
            Next
            dsSearchResults = objDataAcces.ExecuteSpDataSet("usp_VendorEnquiry", objXmlDoc)
            If (dsSearchResults.Tables(0).Rows.Count = 0) Then
                'when the search results not found
                ShowGridHeader()
                lblResults.Visible = True
                'btnVendorProducts.Visible = False

                lblResults.Text = Utility.GetMessage("10001")
            Else
                'when the search results found show them same 
                'and show the VendorProducts button as well
                'btnVendorProducts.Visible = True

                grdVendorDetails.DataSource = dsSearchResults
                grdVendorDetails.DataBind()

                'show the pager style
                grdVendorDetails.PagerStyle.Visible = True

                'show vendorPro ducts button and give the rights
                'btnVendorProducts.Visible = True

                ''Validate VendorProductEntry
                'btnVendorProducts.OnClientClick = "return validateVendorProductEntry('" & Utility.GetMessage("20030", grdVendorDetails.Columns(1).HeaderText) & "', '" & Page.Master.Page.Form.ClientID & "');"

                '***********Commented By Rashi Goyal Date: 2nd Nov '06*******************
                'If (Not Access.ScreenAccess(Constants.VENDENQ)) Then
                '    Response.Redirect("~/ErrorPage.aspx?Code=10019")

                'Else
                '    Dim strFunc As String() = Access.ScreenFunctions(Constants.VENDENQ)
                '    btnVendorProducts.Enabled = False
                '    If Not IsNothing(strFunc) Then
                '        If (strFunc.Length > 0) Then
                '            Dim intcounter As Int32
                '            For intcounter = 0 To strFunc.Length - 1
                '                If strFunc(intcounter) = Constants.CONST_F2 Then
                '                    btnVendorProducts.Enabled = True
                '                End If
                '            Next
                '        End If
                '    End If
                'End If
                '*****************************END*****************************************

                ''check the first radio button               
                'Dim rdoVendorDetails As RadioButton = CType(grdVendorDetails.Items(0).Cells(0).Controls(1), RadioButton)
                'rdoVendorDetails.Checked = True

                ''redirect to the page VendorProducts
                'Dim objGridItem As DataGridItem
                'Dim lblVendor As Label
                'Dim sbldScript As StringBuilder = New StringBuilder()
                'For Each objGridItem In grdVendorDetails.Items
                '    Dim rdoDetails As RadioButton = CType(objGridItem.Cells(0).Controls(1), RadioButton)
                '    If rdoDetails.Checked = True Then
                '        lblVendor = CType(objGridItem.Cells(3).FindControl("lblVendorName"), Label)
                '        'redirect to the page vendor products
                '        'btnVendorProducts.OnClientClick = "return openlinkVendorProducts(" & _
                '        '                                    "'" & objGridItem.Cells(1).Text.Trim() & "'," & _
                '        ''                                    "'" & lblVendor.Text & "');"

                '        btnVendorProducts.OnClientClick = "return ValidateAndOpenWindow(" & _
                '                                            "'" & objGridItem.Cells(1).Text.Trim() & "'," & _
                '                                            "'" & lblVendor.Text & "'," & _
                '                                            "'" & Utility.GetMessage("10005", grdVendorDetails.Columns(1).HeaderText) & "'," & _
                '                                            "'" & Page.Master.Page.Form.ClientID & "');"

                '    End If
                'Next

                '***********Commented By Rashi Goyal Date:2nd Nov '06***************************
                'btnVendorProducts.OnClientClick = "return validateVendorProductEntry(" & _
                '                                    "'" & Utility.GetMessage("10005", grdVendorDetails.Columns(1).HeaderText) & "'," & _
                '                                    "'" & Page.Master.Page.Form.ClientID & "');"
                '**************************END**************************************************

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
            dsSearchResults = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' item databound event handles the exclusive radio button option in the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdVendorDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdVendorDetails.ItemDataBound
        Try
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem

                    Dim xlink As HtmlAnchor = CType(e.Item.FindControl("lnkVendorEnq"), HtmlAnchor)
                    xlink.Attributes.Add("onClick", "return openlinkVendorProductsforDept('" + e.Item.Cells(0).Text + "','" + CType(e.Item.FindControl("lblVendorName"), Label).Text.Replace("'", "\'") + "','" + ddlDepartment.ClientID + "','" + ddlCategory.ClientID + "');")

                    '*************Added By Rashi Goyal Date: 2nd Nov '06*******************
                    If (blAccess) Then

                        grdVendorDetails.Columns(8).Visible = True
                    Else
                        grdVendorDetails.Columns(8).Visible = False
                    End If
                    '**************************END*****************************************

                    'CType(e.Item.FindControl("rdoVendor"), RadioButton).Attributes.Add("OnClick", "javascript:exclusiveRadio('" & Page.Master.FindControl("frmMasterPage").ClientID & "', '" & CType(e.Item.FindControl("rdoVendor"), RadioButton).ClientID & "');")

            End Select

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error in performing exclusive radio button check.", ex)
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
    ''' PageIndex Change Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdVendorDetails_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdVendorDetails.PageIndexChanged
        Try
            grdVendorDetails.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error occurred in pageindex change event.", ex)
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