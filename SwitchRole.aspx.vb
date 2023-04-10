Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Log
Imports POM.Lib.Security

Partial Class SwitchRole
    Inherits System.Web.UI.Page
    Protected screenpx As String = System.Configuration.ConfigurationManager.AppSettings.Item("Screenpx")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objDataAccess As DataAccess,
            objDataTable As DataTable,
            objDataSet As DataSet

        Try

            Context.Response.AddHeader("X-UA-Compatible", "IE=7")

            If Utility.UserId.CompareTo(String.Empty) = 0 Then Response.Redirect("~/Login.aspx")

            If Not IsPostBack Then
                lblStoreId.Text = Utility.UserStoreName
                lblUserId.Text = Utility.UserName

                btnSwitchRole.OnClientClick = "javascript: return validateSwitchRoleEntry('" & Page.Form.ClientID & "', " &
                                               "'" & Utility.GetMessage("10005", dgRole.Columns(1).HeaderText) & "');"

                lblMessage.Text = Utility.GetMessage("10004")
                lblMessage.Text = "Failed to Authenticate"

                objDataAccess = New DataAccess
                objDataSet = objDataAccess.ExecuteSpDataSet("usp_SwitchRole_Search", DataAccess.BuildXmlParam("User", Utility.UserId, "Store", Utility.UserStoreId))

                If objDataSet.Tables.Count <> 2 Then Response.Redirect("~/ErrorPage.aspx?code=50001")
                If objDataSet.Tables(0).Rows.Count = 0 Then
                    lblMessage.Visible = True
                    btnSwitchRole.Visible = False
                End If

                dgRole.DataSource = objDataSet.Tables(0)
                dgRole.DataBind()
                ViewState("deptdatatable") = objDataSet.Tables(1)
                hrAbButton.Visible = True
                btnSwitchRole.Visible = True

                'In case of only one role being assigned to current user
                If dgRole.Items.Count = 1 Then AssignRole()

            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error loading user roles", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

        Finally
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
            If Not IsNothing(objDataSet) Then objDataSet.Dispose()
            If Not IsNothing(objDataTable) Then objDataTable.Dispose()
        End Try
    End Sub

    Protected Sub dgRole_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgRole.ItemDataBound

        Try
            Select Case e.Item.ItemType
                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem
                    CType(e.Item.FindControl("optRole"), RadioButton).Attributes.Add("OnClick", "javascript:exclusiveRadio('" & Page.Form.ClientID & "', '" & CType(e.Item.FindControl("optRole"), RadioButton).ClientID & "');")
            End Select

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error binding data to datagrid", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

        End Try
    End Sub

    Protected Sub btnSwitchRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSwitchRole.Click

        Try
            AssignRole()

        Catch ex As Exception
            Dim objEx As New ApplicationException

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

        End Try

    End Sub

    ''' <summary>
    ''' Method to assign role to current user
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignRole()

        Dim bSelected As Boolean = False,
            index As Integer,
            strReturnUrl As String,
            objRadio As RadioButton,
            arrDept As ArrayList,
            objTable As DataTable,
            objDataRow As DataRow,
            objDataRows As DataRow()

        Try
            For index = 0 To dgRole.Items.Count - 1
                Select Case dgRole.Items(index).ItemType
                    Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem
                        objRadio = CType(dgRole.Items(index).FindControl("optRole"), RadioButton)
                        If objRadio.Checked Then
                            bSelected = True
                            Exit For
                        End If
                End Select
            Next
            If dgRole.Items.Count = 1 Then Session("SR_SingleRole") = True

            index = index - IIf(dgRole.Items.Count = 1, 1, 0)
            If bSelected Or dgRole.Items.Count = 1 Then
                Utility.UserRoleId = CType(dgRole.Items(index).FindControl("lblRoleId"), Label).Text
                Utility.UserRoleName = CType(dgRole.Items(index).FindControl("lblRoleDesc"), Label).Text

                objTable = IIf(IsNothing(ViewState("deptdatatable")), New DataTable(), CType(ViewState("deptdatatable"), DataTable))
                objDataRows = objTable.Select("roleid='" & Utility.UserRoleId & "'")
                arrDept = New ArrayList
                For Each objDataRow In objDataRows
                    arrDept.Add(CType(objDataRow("deptid"), String))
                Next
                Utility.UserDepartments = arrDept

                If Not IsNothing(Request.QueryString("origin")) AndAlso CType(Request.QueryString("origin"), String).Trim().Length > 0 Then
                    Try
                        strReturnUrl = Request.MapPath(CType(Request.QueryString("origin"), String))
                        strReturnUrl = CType(Request.QueryString("origin"), String).Replace("'", "")
                    Catch hex As HttpException
                        strReturnUrl = "~/Home.aspx"
                    End Try
                Else
                    strReturnUrl = "~/Home.aspx"
                End If
                Response.Redirect(strReturnUrl)

            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Switching Role", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx

        End Try

    End Sub

    Protected Sub lnkSignOut_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSignOut.ServerClick

        Try
            Session.Clear()
            Response.Redirect("~/Login.aspx")

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            'Ignore
        End Try

    End Sub
End Class