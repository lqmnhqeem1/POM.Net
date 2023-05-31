Imports System.Data
Imports POM.Lib.UI
Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Security

Public Class Mast003
    Inherits System.Web.UI.Page

    Protected Sub ddlHierSecVal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHierSecVal.SelectedIndexChanged
        Try
            If ddlHierSecVal.SelectedValue <> Nothing And ddlHierSecVal.SelectedValue <> "Select" Then
                Utility.DropDownDataBind(ddlRangeFromVal, ddlHierSecVal.SelectedValue)
                Utility.DropDownDataBind(ddlRangeToVal, ddlHierSecVal.SelectedValue)

                ddlRangeFromVal.Attributes.Add("onchange", "if (Number(this.value) == 0) { " & ddlRangeToVal.ClientID & ".value = this.value; } else if (Number(this.value) > Number(" & ddlRangeToVal.ClientID & ".value)) { " & ddlRangeToVal.ClientID & ".value = this.value;} ")
                ddlRangeToVal.Attributes.Add("onchange", "if ((isNaN(" & ddlRangeFromVal.ClientID & ".value) == false) && (Number(this.value) < Number(" & ddlRangeFromVal.ClientID & ".value))) { " & ddlRangeFromVal.ClientID & ".value = this.value; }")
            Else
                Dim dtDataTable As DataTable
                Dim drDataRow As DataRow
                dtDataTable = New DataTable()
                dtDataTable.Columns.Add("value", System.Type.GetType("System.String"))
                dtDataTable.Columns.Add("text", System.Type.GetType("System.String"))
                dtDataTable.AcceptChanges()

                drDataRow = dtDataTable.NewRow
                drDataRow.Item("value") = "0"
                drDataRow.Item("text") = "--Select--"
                dtDataTable.Rows.Add(drDataRow)

                ddlRangeFromVal.DataSource = dtDataTable
                ddlRangeFromVal.DataBind()
                ddlRangeToVal.DataSource = dtDataTable
                ddlRangeToVal.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnViewReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewReport.Click
        Dim strURL As String = ""
        Dim strB As String = ""
        Dim strStoreCode As String = ""
        Dim intFrom As String = ""
        Dim intTo As String = ""
        Dim intHier As String = ""
        Dim strServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("ReportingPath")

        Try
            If ddlHierSecVal.SelectedValue = "" Or ddlRangeFromVal.SelectedValue = "" _
            Or ddlRangeToVal.SelectedValue = "" Then
                Response.Redirect("~\ErrorPage.aspx?code=60007")

            ElseIf ddlRangeFromVal.SelectedItem.Value > ddlRangeToVal.SelectedItem.Value Then
                Response.Redirect("~\ErrorPage.aspx?code=60003")

            Else

                If ddlHierSecVal.SelectedIndex = 1 Then
                    intHier = 1
                ElseIf ddlHierSecVal.SelectedIndex = 2 Then
                    intHier = 2
                ElseIf ddlHierSecVal.SelectedIndex = 3 Then
                    intHier = 3
                ElseIf ddlHierSecVal.SelectedIndex = 4 Then
                    intHier = 4
                ElseIf ddlHierSecVal.SelectedIndex = 5 Then
                    intHier = 5
                Else
                    intHier = 6
                End If


                intFrom = ddlRangeFromVal.SelectedItem.Value
                intTo = ddlRangeToVal.SelectedItem.Value
                strStoreCode = HttpContext.Current.Session("plibuserstoreid")

                strURL = "<script type=""text/javascript"" language=""javascript"">window.open('" + strServer + "Mast003&rc:Parameters=false&rs:Command=Render&iintHierarchy=" + intHier + "&ivarHierarchyFrom=" + intFrom + "&ivarHierarchyTo=" + intTo + "&iintStoreCode=" + strStoreCode + "','new_winMast003','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"
                CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl(strURL))
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "Report", String.Empty)

            End If
        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error generating report", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch iex As Exception
                'Ignore
            End Try
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            If Not Utility.GetFunctionAccess(Constants.MasterReports, Constants.CONST_F3) Then Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=" & "Master File Report Mast-003")
            'If (Not Access.ScreenAccess(Constants.MasterReports, Constants.CONST_F3)) Then _
            '    Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=Master File Report Mast-003")

            If Page.IsPostBack Then
                If ddlHierSecVal.SelectedItem.Value <> "" Then
                    lblRangeFromCap.Text = ddlHierSecVal.SelectedItem.Text & " Code From"
                    lblRangeToCap.Text = ddlHierSecVal.SelectedItem.Text & " Code To"
                Else
                    lblRangeFromCap.Text = "Hierarchy Code From"
                    lblRangeToCap.Text = "Hierarchy Code To"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class