Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Log

Partial Class SiteMaster
    Inherits System.Web.UI.MasterPage
    Protected screenpx As String = System.Configuration.ConfigurationManager.AppSettings.Item("Screenpx")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class

