Imports POM.Lib.Log

Partial Class ClientExe
    Inherits System.Web.UI.UserControl

#Region "Private Variables"
    Private _strRunText As String = ""
    Private _strOwnClientClick As String = ""
    Private _strParam As String()
#End Region

#Region "Public Properties"
    ''' <summary>
    ''' Gets or Sets the ClientExe control's Button Text Property
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Return btnClientExe.Text
        End Get
        Set(ByVal value As String)
            btnClientExe.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the ClientExe control's Button Text Property when control is processing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RunText() As String
        Get
            Return _strRunText
        End Get
        Set(ByVal value As String)
            _strRunText = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the TabIndex of ClientExe control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TabIndex() As Integer
        Get
            Return btnClientExe.TabIndex
        End Get
        Set(ByVal value As Integer)
            btnClientExe.TabIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the Cascading Style Sheet (CSS) class rendered by the web server control on the client
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CssClass() As String
        Get
            Return btnClientExe.CssClass
        End Get
        Set(ByVal value As String)
            btnClientExe.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the client side script that executes when a System.Web.UI.WebControls.Button controls's
    ''' System.Web.UI.Webcontrols.Button.Click event is raised
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OnClientClick() As String
        Set(ByVal value As String)
            btnClientExe.OnClientClick = "return (" & value & "?" & _strOwnClientClick & ":false);"
        End Set
    End Property

    ''' <summary>
    ''' Sets the command line arguments for client exe to be executed
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ClientExeParam() As String()
        Set(ByVal value As String())
            _strParam = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim index As Integer,
            strCmdLine As String = ""

        Try
            If Not Page.ClientScript.IsClientScriptBlockRegistered("ClientExe") Then
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientExe", String.Empty)
                CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl("<script type=""text/javascript"" src=""" & Page.ResolveUrl("~/controls/js/clientexe.js") & """></script>"))

            End If
            _strOwnClientClick = "return clientexeShowHide('" & btnClientExe.ClientID & "', '" & ifrmClientExe.ClientID & "', '" & Me.Text & "', '" & Me.RunText & "')"

            For index = 0 To _strParam.Length - 1
                strCmdLine = strCmdLine & IIf(strCmdLine.Trim().Length > 0, " ", "") & """" & _strParam(index) & """"
            Next
            ifrmClientExe.Attributes.Add("src", Page.ResolveClientUrl("~/controls/ClientExe.htm") & "?param=" & strCmdLine)

            If btnClientExe.OnClientClick.Trim().Length = 0 Then _
                btnClientExe.OnClientClick = _strOwnClientClick & ";"

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error loading ClientExe", ex)

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
