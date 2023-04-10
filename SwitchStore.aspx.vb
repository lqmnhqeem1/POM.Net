Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Log

Partial Class SwitchStore

    Inherits System.Web.UI.Page
    Protected screenpx As String = System.Configuration.ConfigurationManager.AppSettings.Item("Screenpx")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Context.Response.AddHeader("X-UA-Compatible", "IE=7")

            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            'Populate data
            If Not IsPostBack Then
                lblUserId.Text = Utility.UserName

                'Jerry Commented: Logo Section Temperory Comment
                'lblLogoVer.Text = System.Configuration.ConfigurationManager.AppSettings.Item("AppVer")

                'If Utility.UserStoreName.ToString().ToLower().Contains("tmc") Then
                '    imgGiant.ImageUrl = "~/images/TMCLogo.jpg"
                'ElseIf Utility.UserStoreName.ToString().ToLower().Contains("mercato") Then
                '    imgGiant.ImageUrl = "~/images/MercatoLogo.jpg"
                'ElseIf Utility.UserStoreName.ToString().ToLower().Contains("cold storage") Then
                '    imgGiant.ImageUrl = "~/images/ColdStorageLogo2.jpg"
                'ElseIf Utility.UserStoreName.ToString().ToLower().Contains("giant mini") Then
                '    imgGiant.ImageUrl = "~/images/GiantMini.jpg"
                'Else
                '    imgGiant.ImageUrl = "~/images/Giant2.jpg"
                'End If

                Utility.DropDownDataBind(ddlStore, Constants.Store)
                Utility.SetDropDownSelectedValue(ddlStore, Utility.UserStoreId)
                ddlStore.Attributes.Add("onkeypress", "return keySort(this);")

            End If

            'Configure Controls
            btnOk.OnClientClick = "return validateSwitchStoreEntry('" & ddlStore.ClientID & "', '" & Utility.GetMessage("10005", lblStore.Text) & "');"

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Loading Page", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch tex As Threading.ThreadAbortException
                'Ignore
            End Try

        End Try

    End Sub

    Protected Sub lnkSignOut_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSignOut.ServerClick

        Try
            Session.Clear()
            Response.Redirect("~/Login.aspx")

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Signing Out", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch iex As Threading.ThreadAbortException
                'Ignore
            End Try

        End Try

    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim objDataAccess As DataAccess,
            objDataSet As DataSet

        Try
            lblMessage.Text = ""
            lblMessage.Visible = False

            objDataAccess = New DataAccess()
            objDataSet = objDataAccess.ExecuteSpDataSet("usp_UserAuthentication_Search", DataAccess.BuildXmlParam("UserID", Utility.UserId, "StoreID", ddlStore.SelectedItem.Value))

            If objDataSet.Tables.Count < 2 Then
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Else
                If CType(objDataSet.Tables(1).Rows(0)("islocal"), Integer) = 1 Then
                    If CType(objDataSet.Tables(0).Rows(0)("HasAccess"), Integer) = 1 Then
                        Utility.UserStoreId = ddlStore.SelectedItem.Value
                        Utility.UserStoreName = ddlStore.SelectedItem.Text
                        Response.Redirect("~/Home.aspx")
                    Else
                        lblMessage.Text = Utility.GetMessage("10004")
                        lblMessage.Visible = True
                    End If
                Else
                    If CType(objDataSet.Tables(1).Rows(0)("storeip"), String).Trim().Length = 0 Then
                        lblMessage.Text = Utility.GetMessage("10022")
                        lblMessage.Visible = True
                    Else
                        Response.Redirect("http://" & CType(objDataSet.Tables(1).Rows(0)("storeip"), String) & "/" & ConfigurationManager.AppSettings("PUBLISH_WEB_SITE_NAME") & "/Login.aspx")
                    End If
                End If
            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error Switching Store", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch iex As Threading.ThreadAbortException
                'Ignore
            End Try

        Finally
            If Not IsNothing(objDataSet) Then objDataSet.Dispose()
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            If IsNothing(Request.QueryString("origin")) AndAlso Request.QueryString("origin").Trim().Length > 0 Then
                Response.Redirect(Request.QueryString("origin"))
            Else
                Response.Redirect("~/Home.aspx")
            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error cancelling store switch", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)

            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch iex As Threading.ThreadAbortException
                'Ignore
            End Try

        End Try

    End Sub
End Class
