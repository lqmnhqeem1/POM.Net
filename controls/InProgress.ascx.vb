
Partial Class InProgress
    Inherits System.Web.UI.UserControl

#Region "Private Variables"
    Private _sText As String = ""
#End Region

#Region "Public Properties"
    '''' <summary>
    '''' Gets / Sets Control Css Class
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Property CssClass() As String
    '    Get
    '        Return lblInProgress.CssClass
    '    End Get
    '    Set(ByVal value As String)
    '        lblInProgress.CssClass = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Gets / Sets Control Display Text
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Return _sText
        End Get
        Set(ByVal value As String)
            _sText = value
        End Set
    End Property

    ''' <summary>
    ''' Gets ClientID for container DIV
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ControlClientID() As String
        Get
            Return divInProgress.ClientID
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        divInProgress.Attributes.Add("src", Page.ResolveClientUrl("~/controls/InProgressPopup.aspx?text=" & Me.Text))
    End Sub
End Class
