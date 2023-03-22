#Region "Imports"
Imports System.Data
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Data
Imports POM.Lib.UI
#End Region

Public Class ListOfValuesPopup
    Inherits System.Web.UI.Page

#Region "Private Constants"
    Private Const RadioButtonColumn As Integer = 0
#End Region

#Region "Protected Methods"
    ''' <summary>
    ''' Create Child Event
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub CreateChildControls()

        Try
            MyBase.CreateChildControls()

            'Setup dynamic UI
            BuildParamEntry()

            If Not IsNothing(ViewState("autosearch")) Then
                SearchClick(btnSearch, New System.EventArgs())
                ViewState("autosearch") = Nothing
            End If

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error creating dynamic UI", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try

        End Try

    End Sub
#End Region

#Region "Private Methods"
    ''' <summary>
    ''' Method to build UI for search condition entry
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildParamEntry()

        Dim index As Integer = 0,
            jndex As Integer = 0,
            strClientParam As String = "",
            strSelectedParamId As String,
            sFilterParam As String = "",
            sFilterParamA As String(),
            trSParam As TableRow,
            tdSParam As TableCell,
            lblSParam As Label,
            txtSParam As TextBox,
            tUtility As System.Type,
            dsParam As DataSet,
            objParam As ArrayList,
            strLabel As String(),
            drParam As DataRow()

        Try
            dsParam = New DataSet
            dsParam.ReadXml(HttpContext.Current.Server.MapPath("~/App_Data/ListOfValues.xml"))
            drParam = dsParam.Tables(0).Select("id='" & CType(ViewState("lovcode"), String) & "'")

            strSelectedParamId = CType(ViewState("selectedparam"), String)
            If strSelectedParamId.Trim().Length = 0 Then
                strSelectedParamId = CType(drParam(0)("selectedParamId"), String).ToLower().Trim()
                ViewState("selectedparam") = strSelectedParamId
            End If

            drParam = drParam(0).GetChildRows(dsParam.Relations(0).RelationName)
            objParam = IIf(IsNothing(ViewState("searchparam")), New ArrayList(), CType(ViewState("searchparam"), ArrayList))

            ReDim strLabel(drParam.Length)
            strLabel(0) = "20005"
            ' Setting up Search Parameters
            For index = 0 To drParam.Length - 1
                If index Mod 2 = 0 Or IsNothing(trSParam) Then trSParam = New TableRow

                ' Label for entry
                tdSParam = New TableCell

                lblSParam = New Label
                lblSParam.Text = CType(drParam(index)("text"), String).Trim()
                lblSParam.CssClass = "normalLabel"
                lblSParam.TabIndex = 0

                If CType(drParam(index)("visible"), Boolean) Then tdSParam.Controls.Add(lblSParam)
                If tdSParam.Controls.Count > 0 Then trSParam.Cells.Add(tdSParam)

                ' Entry Control
                tdSParam = New TableCell

                txtSParam = New TextBox
                txtSParam.CssClass = "normalTextBox"
                txtSParam.ID = CType(drParam(index)("id"), String).ToLower().Trim()
                txtSParam.MaxLength = CType(drParam(index)("maxlength"), Integer)
                txtSParam.TabIndex = index
                If Not objParam.Contains(txtSParam.ID) Then
                    objParam.Add(txtSParam.ID)

                    'objParam.Add(IIf(txtSParam.ID.CompareTo(strSelectedParamId) = 0, CType(IIf(IsNothing(ViewState("partialentry")), "", ViewState("partialentry")), String), ""))
                    If txtSParam.ID.CompareTo(strSelectedParamId) = 0 Then
                        objParam.Add(CType(IIf(IsNothing(ViewState("partialentry")), "", ViewState("partialentry")), String))
                    Else
                        If Not IsNothing(ViewState("additionalfilter")) AndAlso Not CType(ViewState("additionalfilter"), String).Trim().Length = 0 Then
                            sFilterParamA = CType(ViewState("additionalfilter"), String).Split("|")
                            sFilterParam = ""
                            For jndex = 0 To sFilterParamA.Length - 1
                                If sFilterParamA(jndex).Split("=")(0).CompareTo(txtSParam.ID) = 0 Then sFilterParam = sFilterParamA(jndex).Split("=")(1)
                            Next
                        End If
                        objParam.Add(sFilterParam)
                    End If

                    txtSParam.Text = CType(objParam(objParam.Count - 1), String)
                    If txtSParam.Text.Trim().Length > 0 Then
                        ViewState("autosearch") = 19
                    End If

                Else
                    If Not IsNothing(Request.Form(txtSParam.ID)) Then objParam(objParam.IndexOf(txtSParam.ID) + 1) = Request.Form(txtSParam.ID)
                    txtSParam.Text = CType(objParam(objParam.IndexOf(txtSParam.ID) + 1), String)
                End If

                strLabel(index + 1) = lblSParam.Text

                tUtility = GetType(Utility)
                strClientParam = strClientParam & ", '" & txtSParam.ClientID & "', '" &
                                                    CType(drParam(index)("datatype"), String).ToLower().Trim() & "', '" &
                                                    CType(tUtility.InvokeMember("GetMessage", Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, New Object() {"20003", lblSParam.Text}), String) & "'"

                If CType(drParam(index)("visible"), Boolean) Then tdSParam.Controls.Add(txtSParam)
                If tdSParam.Controls.Count > 0 Then trSParam.Cells.Add(tdSParam)

                ' Configure Search Table Look N Feel
                If index Mod 2 = 0 AndAlso trSParam.Cells.Count > 0 Then
                    tblSearchParam.Rows.Add(trSParam)
                    If tblSearchParam.Rows.Count Mod 2 = 0 Then
                        trSParam.CssClass = "searchTableDarkRow0"
                    Else
                        trSParam.CssClass = "searchTableDarkRow1"
                    End If
                End If
            Next

            If ViewState("partialentry").Trim().Length = 0 And ViewState("event").Trim() = "AUTO" Then
                ClientScript.RegisterClientScriptBlock(GetType(String), "Empty", "<script type=""text/javascript"" language=""javascript"">passonData('" + ViewState("additionaldata").ToString.Split("=")(0) + "','" + String.Empty + "'); onDataPopulate('" & CType(ViewState("ondatapopulate"), String) & "'); self.close(); </script>")
                Dim s As String = "<script type=""text/javascript"" language=""javascript"">passonData('" + ViewState("additionaldata").ToString.Split("=")(0) + "','" + String.Empty + "'); onDataPopulate('" & CType(ViewState("ondatapopulate"), String) & "'); self.close(); </script>"
            End If

            ' Configure Search Table Final Look N Feel
            While tblSearchParam.Rows(tblSearchParam.Rows.Count - 1).Cells.Count Mod 4 <> 0
                tblSearchParam.Rows(tblSearchParam.Rows.Count - 1).Cells.Add(New TableCell())
            End While

            If Not IsNothing(trSParam) Then
                While trSParam.Cells.Count <> 4
                    trSParam.Cells.Add(New TableCell)
                End While
            End If

            If tblSearchParam.Rows.Count > 0 Then
                btnSearch.Visible = True
                btnSearch.TabIndex = index
                index += 1

                btnReset.Visible = True
                btnReset.TabIndex = index
                btnReset.OnClientClick = "javascript:return clearSearchParams('" & frmListOfValues.ClientID & "');"
                index += 1

                If Not IsNothing(tblSearchParam.FindControl(strSelectedParamId)) Then tblSearchParam.FindControl(strSelectedParamId).Focus()
            End If

            btnDone.TabIndex = index
            index += 1
            btnCancel.TabIndex = index

            ' Store in ViewState for future reference
            If Not IsNothing(objParam) Then ViewState("searchparam") = objParam

            tUtility = GetType(Utility)
            strClientParam = "'" & CType(tUtility.InvokeMember("GetMessage", Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, strLabel), String) & "' " & strClientParam
            btnSearch.OnClientClick = "javascript: return validateListOfValuesEntry(" & strClientParam & ");"

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Loading ListOfValues Control - Corrupt/Non-Existent XML base data or corrupt call", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx
        End Try
    End Sub

    ''' <summary>
    ''' Procedure to retrieve and bind data to grid
    ''' </summary>
    ''' <param name="PageIndex">Current Page Index of DataGrid</param>
    ''' <param name="PageSize">Number of records per page in datagrid</param>
    ''' <remarks></remarks>
    Private Sub BindDataSource(ByVal PageIndex As Integer, ByVal PageSize As Integer)

        Dim index As Integer,
            jindex As Integer,
            strDepts As String = "",
            strDept As String(),
            objDataField As BoundColumn,
            dsDataSource As DataSet,
            objXmlDoc As XmlDocument,
            daPOM As DataAccess,
            tDataAccess As System.Type,
            objParam As ArrayList,
            arrBXArgs As Object(),
            arrBXPArgs As String(),
            arrFXArgs As String()

        Try
            objParam = CType(IIf(IsNothing(ViewState("searchparam")), New ArrayList(), ViewState("searchparam")), ArrayList)
            If IsNothing(objParam) OrElse objParam.Count = 0 Then Exit Sub

            If IsNothing(ViewState("lovresultset")) Then
                tDataAccess = GetType(DataAccess)

                ' User Department string
                strDept = Utility.UserDepartments.ToArray(GetType(String))
                For index = 0 To strDept.Length - 1
                    strDepts = IIf(index = 0 OrElse strDept(index).Trim().Length = 0, "", strDepts & "|") & strDept(index).Trim()
                Next

                ' Create first level xml params
                ReDim arrBXArgs(9)
                arrBXArgs = New String() {"Key", CType(ViewState("lovcode"), String), "Store", Utility.UserStoreId, "Dept", strDepts, "PageIndex", PageIndex, "PageSize", PageSize}
                objXmlDoc = CType(tDataAccess.InvokeMember("BuildXmlParam", Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, arrBXArgs), XmlDocument)

                ' Create second level xml params
                ReDim arrBXArgs(2)
                ReDim arrBXPArgs(objParam.Count - 1)
                arrBXArgs(0) = objXmlDoc
                arrBXArgs(1) = 2

                For index = 0 To objParam.Count - 1 Step 2
                    arrBXPArgs(index) = CType(objParam(index), String)
                    arrBXPArgs(index + 1) = CType(objParam(index + 1), String)
                Next

                arrBXArgs(2) = arrBXPArgs
                tDataAccess.InvokeMember("BuildXmlParam", Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, arrBXArgs)

                'Attach additional data filtering condition if any
                If Not IsNothing(ViewState("additionalfilter")) AndAlso Not CType(ViewState("additionalfilter"), String).Trim().Length = 0 Then
                    arrFXArgs = CType(ViewState("additionalfilter"), String).Trim().Split("|")
                    If arrFXArgs.Length > 0 Then
                        ReDim arrBXArgs(2)
                        ReDim arrBXPArgs(arrFXArgs.Length * 2 - 1)

                        arrBXArgs(0) = objXmlDoc
                        arrBXArgs(1) = 3

                        jindex = 0
                        For index = 0 To arrFXArgs.Length - 1
                            If arrFXArgs(index).Split("=").Length <> 2 Then Throw New ArgumentException("Invalid AdditionalFilter format", "AdditionalFilter")
                            arrBXPArgs(jindex) = arrFXArgs(index).Split("=")(0)
                            arrBXPArgs(jindex + 1) = arrFXArgs(index).Split("=")(1)
                            jindex = jindex + 2

                        Next
                        arrBXArgs(2) = arrBXPArgs

                        tDataAccess.InvokeMember("BuildXmlParam", Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, arrBXArgs)

                    End If

                End If

                daPOM = New DataAccess()
                dsDataSource = daPOM.ExecuteSpDataSet("usp_ListOfValues", objXmlDoc)
                ViewState("lovresultset") = dsDataSource

            Else
                dsDataSource = CType(ViewState("lovresultset"), DataSet)
            End If

            ' Add Columns to Data Grid for display
            If dgLOV.Columns.Count <> dsDataSource.Tables(0).Columns.Count + 1 Then
                While dgLOV.Columns.Count > 1
                    dgLOV.Columns.RemoveAt(dgLOV.Columns.Count - 1)
                End While

                For index = 0 To dsDataSource.Tables(0).Columns.Count - 1
                    objDataField = New BoundColumn

                    objDataField.ItemStyle.CssClass = "normalLabel"
                    objDataField.DataField = dsDataSource.Tables(0).Columns(index).ColumnName
                    objDataField.HeaderText = dsDataSource.Tables(0).Columns(index).Caption

                    dgLOV.Columns.Add(objDataField)

                Next

            End If

            ' return result
            dgLOV.CurrentPageIndex = PageIndex
            dgLOV.DataSource = dsDataSource.Tables(0)
            dgLOV.DataBind()
            dgLOV.PagerStyle.Visible = (dgLOV.Items.Count > 0 And dgLOV.PageCount > 1)

            btnDone.Visible = (dgLOV.Items.Count > 0)

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error fetching ListOfValues data", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx

        Finally
            dsDataSource = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Returns Hastable of selected data row in ListOfValues
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSelectedValue() As Hashtable

        Dim index As Integer,
            jindex As Integer,
            objParam As ArrayList,
            objReturn As Hashtable,
            strValue As String()

        Try
            objReturn = New Hashtable

            objParam = CType(ViewState("searchparam"), ArrayList)
            If IsNothing(objParam) Then Throw New ApplicationException("Corrupt internal state")

            If IsNothing(Request.Form("hdFullValue")) Then
                strValue = hdFullValue.Value.Trim().Split("^")
            Else
                strValue = Request.Form("hdFullValue").Trim().Split("^")
            End If

            jindex = 0
            For index = 0 To objParam.Count - 1 Step 2
                If jindex >= strValue.Length Then Exit For
                objReturn.Add(objParam(index), strValue(jindex))
                jindex = jindex + 1

            Next

            Return objReturn

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error retrieving value from ListOfValues", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx

        Finally
            If Not IsNothing(objReturn) Then objReturn = Nothing
            If Not IsNothing(objParam) Then objParam = Nothing
        End Try

    End Function
#End Region

#Region "Event Handling"
    Private Sub SearchClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click

        Dim strOnLoadScript As String

        Try
            lblMessage.Text = Utility.GetMessage("10001")
            lblMessage.Visible = False
            dgLOV.PagerStyle.Visible = True

            trResult.Visible = True
            ViewState("lovresultset") = Nothing
            BindDataSource(0, dgLOV.PageSize)
            If dgLOV.Items.Count = 0 Then
                lblMessage.Visible = True
                dgLOV.PagerStyle.Visible = False
            Else
                CType(dgLOV.Items(0).Cells(RadioButtonColumn).FindControl("optSelect"), RadioButton).Checked = True
                btnDone.Enabled = True
            End If

            'strOnLoadScript = "<script type=""text/javascript"" language=""javascript"">" & _
            '                        "controlShowHide('" & pipLOV.ControlClientID & "', false);" & _
            '                "</script>"
            'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "OnLoad", strOnLoadScript)

            If CType(ViewState("opmode"), String).Trim().ToUpper().CompareTo("AUTO") = 0 AndAlso dgLOV.Items.Count = 1 Then _
                Done_Click(btnDone, New EventArgs())

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error fetching ListOfValues data", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try

        End Try

    End Sub

    Protected Sub Done_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone.Click

        Dim index As Integer,
            jndex As Integer,
            iInitLen As Integer,
            strScript As String,
            strAdditionalData As String(),
            strADChunk As String(),
            strADSubChunkI As String(),
            objSelection As Hashtable

        Try
            objSelection = GetSelectedValue()
            If objSelection.Count > 1 Then
                strAdditionalData = CType(IIf(IsNothing(ViewState("additionaldata")), "", ViewState("additionaldata")), String).Split("|")
                strScript = "<script type=""text/javascript"" language=""javascript"">" &
                            "passonData("

                iInitLen = strScript.Length

                If Not IsNothing(ViewState("codeentry")) AndAlso Not IsNothing(ViewState("selectedparam")) Then
                    strScript += "'" & CType(ViewState("codeentry"), String) & "', '" & CType(IIf(IsNothing(objSelection(ViewState("selectedparam"))), "", objSelection(ViewState("selectedparam"))), String) & "'"
                End If

                For index = 0 To strAdditionalData.Length - 1
                    strADChunk = strAdditionalData(index).Split("=")
                    If strADChunk.Length = 2 Then
                        strADSubChunkI = strADChunk(0).Split("-")
                        If strADSubChunkI.Length > 1 Then
                            For jndex = 0 To strADSubChunkI.Length - 1
                                strScript += IIf(strScript.Length <> iInitLen, ", ", "") & "'" & strADSubChunkI(jndex) & "', '" & IIf(IsNothing(objSelection(strADChunk(1))), "", IIf(CType(objSelection(strADChunk(1)), String).Split("-").Length > jndex, CType(objSelection(strADChunk(1)), String).Split("-")(jndex), "")) & "'"
                            Next
                        Else
                            strScript += IIf(strScript.Length <> iInitLen, ", ", "") & "'" & strADChunk(0) & "', '" & IIf(IsNothing(objSelection(strADChunk(1))), "", CType(objSelection(strADChunk(1)), String)) & "'"
                        End If
                    End If

                Next

                strScript += ");" &
                            "onDataPopulate('" & CType(ViewState("ondatapopulate"), String) & "');" &
                            "self.close();" &
                            "</script>"

                ClientScript.RegisterClientScriptBlock(GetType(String), "Done_Click", strScript)
            End If


        Catch ex As Exception
            Dim objEx As New Exception("Error Returning ListOfValues", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try

        Finally
            If Not IsNothing(objSelection) Then objSelection = Nothing
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strCheckKeyScript As String

        Try
            strCheckKeyScript = "<script type=""text/javascript"" language=""javascript"">" &
                                    "function checkKey(event){" &
                                        "var defcontrol;" &
                                        "switch(event.keyCode){" &
                                            "case 13:" &
                                                "defcontrol = document.getElementById('" & btnSearch.ClientID & "');" &
                                                "if(defcontrol != null) defcontrol.focus();" &
                                                "break;" &
                                            "case 27:" &
                                                "defcontrol = document.getElementById('" & btnCancel.ClientID & "');" &
                                                "if(defcontrol != null) defcontrol.click();" &
                                                "break;" &
                                        "}" &
                                    "}" &
                                "</script>"
            Page.ClientScript.RegisterClientScriptBlock(GetType(String), "CheckKey", strCheckKeyScript)

            If Not IsPostBack Then
                ViewState("codeentry") = ""
                ViewState("lovcode") = ""
                ViewState("selectedparam") = ""
                ViewState("additionaldata") = ""
                ViewState("additionalfilter") = ""
                ViewState("partialentry") = ""
                ViewState("ondatapopulate") = ""
                ViewState("opmode") = ""

                If Request.QueryString("codeentry") Is Nothing OrElse
                        CType(Request.QueryString("codeentry"), String).Trim().Length = 0 OrElse
                        Request.QueryString("lovcode") Is Nothing OrElse
                        CType(Request.QueryString("lovcode"), String).Trim().Length = 0 Then _
                        Response.Write("<script type=""text/javascript"" language=""javascript"">window.close();</script>")

                ViewState("codeentry") = Request.QueryString("codeentry").Trim()
                ViewState("lovcode") = Request.QueryString("lovcode").Trim()
                If Not IsNothing(Request.QueryString("selectedparam")) Then _
                    ViewState("selectedparam") = Request.QueryString("selectedparam").Trim()
                If Not IsNothing(Request.QueryString("additionaldata")) Then _
                    ViewState("additionaldata") = Request.QueryString("additionaldata").Trim()
                If Not IsNothing(Request.QueryString("additionalfilter")) Then _
                    ViewState("additionalfilter") = Request.QueryString("additionalfilter").Trim()
                If Not IsNothing(Request.QueryString("partialentry")) Then _
                    ViewState("partialentry") = Request.QueryString("partialentry").Trim()
                If Not IsNothing(Request.QueryString("ondatapopulate")) Then _
                    ViewState("ondatapopulate") = Request.QueryString("ondatapopulate").Trim()
                If Not IsNothing(Request.QueryString("opmode")) Then _
                    ViewState("opmode") = Request.QueryString("opmode").Trim()
                If Not IsNothing(Request.QueryString("event")) Then _
                    ViewState("event") = Request.QueryString("event").Trim()

            End If

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error loading ListOfValues", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try
        End Try
    End Sub

    Protected Sub DataGridListOfValues_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgLOV.PageIndexChanged

        Try
            BindDataSource(e.NewPageIndex, dgLOV.PageSize)

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Paging data", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try

        End Try
    End Sub

    Protected Sub DataGridListOfValues_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgLOV.ItemDataBound

        Dim index As Integer,
            sValue As String = ""

        Try
            Select Case e.Item.ItemType ' You can select the item type that you want
                Case ListItemType.AlternatingItem, ListItemType.Item, ListItemType.EditItem
                    CType(e.Item.FindControl("optSelect"), RadioButton).Attributes.Add("OnClick", "javascript:radioClicked('" & frmListOfValues.ClientID + "', '" & CType(e.Item.FindControl("optSelect"), RadioButton).ClientID & "', '" & hdFullValue.ClientID & "', '" & (dgLOV.Items.Count + 1) & "');")

                    If dgLOV.Items.Count = 0 Then
                        For index = 1 To e.Item.Cells.Count - 1
                            sValue = sValue & IIf(sValue.Trim().Length = 0, "", "^") & e.Item.Cells(index).Text.Replace("&nbsp;", "").Trim()
                        Next
                        hdFullValue.Value = sValue
                    End If
            End Select

        Catch ex As Exception
            Dim objEx As New Exception("Error bounding data to datagrid", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                lblMessage.Text = Utility.GetMessage("50001")
                lblMessage.Visible = True
            Catch iex As Exception
                'Ignore
            End Try

        End Try

    End Sub

#End Region
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub

End Class