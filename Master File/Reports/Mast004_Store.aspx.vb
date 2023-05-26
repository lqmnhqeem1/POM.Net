Imports POM.Lib.UI
Imports POM.Lib.Data
Imports POM.Lib.Log
Imports System.Xml
Imports POM.Lib.Security

Public Class Mast004
    Inherits System.Web.UI.Page

    Protected Sub btnViewReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewReport.Click

        Dim strURL As String
        Dim dtmDate As Date
        Dim strDate As String
        Dim strServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("ReportingPath")
        Dim strUserRole As String
        Dim strUserRoleCode As String

        Try
            If Calendar1.Text = "" Then
                Response.Redirect("~\ErrorPage.aspx?code=60007")

            ElseIf IsDate(Calendar1.Text) = False Then
                Response.Redirect("~\ErrorPage.aspx?code=60006")

            Else
                If Date.TryParse(Calendar1.Text, dtmDate) Then
                    strDate = dtmDate.ToString("yyyy/MM/dd")
                    strUserRole = Utility.UserId
                    strUserRoleCode = Utility.UserRoleId

                    strURL = "<script type=""text/javascript"" language=""javascript"">window.open('" + strServer + "Mast004_Store&rc:Parameters=false&rs:Command=Render&idtmDateCreated=" + strDate + "&ichrUserRole=" + strUserRole + "&ichrUserRoleCode=" + strUserRoleCode + "&iintStoreCode=" + Utility.UserStoreId + " ','new_winMast004','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"
                    CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl(strURL))
                    ClientScript.RegisterClientScriptBlock(Page.GetType(), "Report", String.Empty)


                Else
                    Response.Redirect("~\ErrorPage.aspx?code=60009")

                End If
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

            If Not Utility.GetFunctionAccess(Constants.MasterReports, Constants.CONST_F6) Then Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=" & "Master File Report Mast-004 (Store Change)")
            'If (Not Access.ScreenAccess(Constants.MasterReports, Constants.CONST_F4)) Then _
            '    Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=Master File Report Mast-004")

            If Not Page.IsPostBack Then
                Calendar1.Text = Today()
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class