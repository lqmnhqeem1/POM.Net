Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Log

Partial Class SiteMaster
    Inherits System.Web.UI.MasterPage
    Protected screenpx As String = System.Configuration.ConfigurationManager.AppSettings.Item("Screenpx")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objDataAccess As DataAccess,
            dsExpDate As DataSet

        Try
            Context.Response.AddHeader("X-UA-Compatible", "IE=7")

            If Utility.UserId.CompareTo(String.Empty) = 0 Then Response.Redirect("~/Login.aspx")
            If Utility.UserRoleId.CompareTo(String.Empty) = 0 Then
                Response.Redirect("~/SwitchRole.aspx")
            Else
                lblCurrRole.Text = Utility.UserRoleName
            End If
        Catch
            e.ToString()

        End Try

        lblLoginName.Text = Utility.UserName
        lblStoreId.Text = Utility.UserStoreName
        hlnkChangeStore.NavigateUrl = "~/SwitchStore.aspx?origin='" & Request.Url.PathAndQuery & "'"""
        hlnkChangeRole.NavigateUrl = "~/SwitchRole.aspx?origin='" & Request.Url.PathAndQuery & "'"
        hlnkChangeRole.Visible = (IsNothing(Session("SR_SingleRole")) OrElse Not CType(Session("SR_SingleRole"), Boolean))
        lnkSignOut.HRef = "~/Login.aspx"


    End Sub

    Protected Sub lnkSignOut_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSignOut.ServerClick

        Dim objDataAccess As DataAccess

        Try
            If IsNothing(objDataAccess) Then objDataAccess = New DataAccess()
            objDataAccess.ExecuteSpNoResult("usp_Audit_UserAccess", DataAccess.BuildXmlParam("UserId", Utility.UserId, "UserName", Utility.UserName, "Store", Utility.UserStoreId, "RoleId", Utility.UserRoleId, "Action", "Logout"))
            Session.Clear()
            Response.Redirect("~/Login.aspx")

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            'Ignore
        Finally
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
        End Try

    End Sub

    Public Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound ', Repeater3.ItemDataBound ', Repeater3.ItemDataBound
        Dim node As SiteMapNode = e.Item.DataItem
        Dim display As String
        If IsNothing(node) = False Then
            display = node.Description
            If IsNothing(display) = False AndAlso display = "hidden" Then
                e.Item.Visible = False
            End If
        End If


        If IsNothing(node) = False Then
            display = node.Description
            If Utility.UserStoreName.ToString().ToLower().Contains("tmc") AndAlso display = "giant" Then
                e.Item.Visible = False
            ElseIf Utility.UserStoreName.ToString().ToLower().Contains("mercato") AndAlso display = "giant" Then
                e.Item.Visible = False
            ElseIf Utility.UserStoreName.ToString().ToLower().Contains("cold storage") AndAlso display = "giant" Then
                e.Item.Visible = False
            ElseIf Utility.UserStoreName.ToString().ToLower().Contains("giant mini") AndAlso display = "giant" Then
                e.Item.Visible = False
            ElseIf Utility.UserStoreName.ToString().ToLower().Contains("giant mini") = False AndAlso Utility.UserStoreName.ToString().ToLower().Contains("giant") AndAlso display = "giant" Then
                e.Item.Visible = False
            End If
        End If


    End Sub

End Class