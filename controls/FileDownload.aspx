<%@ Import Namespace="System.IO"%>
<script language="VB" runat="server">
    Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
 
        Dim root As String
        
        Try
            'Check User Session State
            If Utility.UserStoreId.CompareTo(String.Empty) = 0 Then Response.Redirect("~/Login.aspx")
        
            root = ConfigurationManager.AppSettings("POMOutFolder").Replace("~", Utility.UserStoreId)
            Dim filepath As String = Request.Params("file")
        
            If Not filepath Is Nothing Then
                If File.Exists(filepath) And filepath.StartsWith(root) Then
                    Dim filename As String = Path.GetFileName(filepath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & filename & """")
                    Response.Flush()
                    Response.WriteFile(filepath)
                End If
            End If
        Catch tex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub
</script>
