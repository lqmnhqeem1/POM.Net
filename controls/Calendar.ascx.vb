Imports POM.Lib.Log

Partial Class CalendarControl
    Inherits System.Web.UI.UserControl

#Region "Private Variables"
    Private _strCalendarCssClass As String = ""
#End Region

#Region "Public Properties"
    ''' <summary>
    ''' Gets/Sets the value of date control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Text() As String
        Get
            Return Me.txtCalendarValue.Text.Trim()
        End Get

        Set(ByVal value As String)
            Try
                'Me.txtCalendarValue.Text = Date.Parse(value).ToString("dd/MM/yyyy")
                Me.txtCalendarValue.Text = Date.Parse(value).ToShortDateString()
            Catch ex As Exception
                Me.txtCalendarValue.Text = ""
            End Try
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the CssClass for this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CalendarCssClass() As String
        Get
            Return Me._strCalendarCssClass
        End Get

        Set(ByVal value As String)
            Me._strCalendarCssClass = value
            Me.txtCalendarValue.CssClass = value
            Me.hlnkCalLink.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Calendar TextBox CssClass
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TextBoxCssClass() As String
        Get
            Return Me.txtCalendarValue.CssClass
        End Get

        Set(ByVal value As String)
            Me.txtCalendarValue.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Calendar Image CssClass
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ButtonCssClass() As String
        Get
            Return Me.hlnkCalLink.CssClass
        End Get

        Set(ByVal value As String)
            Me.hlnkCalLink.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the maxlength of entry
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MaxLength() As Integer
        Get
            Return Me.txtCalendarValue.MaxLength
        End Get

        Set(ByVal value As Integer)
            Me.txtCalendarValue.MaxLength = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the textbox for calendar control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TextEntryHandle() As TextBox
        Get
            Return Me.txtCalendarValue
        End Get
    End Property
    Public Property Enabled() As Boolean
        Get
            Return txtCalendarValue.Enabled And hlnkCalLink.Enabled
        End Get

        Set(ByVal value As Boolean)
            hlnkCalLink.Enabled = value
            txtCalendarValue.Enabled = value
        End Set
    End Property



    ''' <summary>
    ''' Gets the textbox client id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TextEntryClientID() As String
        Get
            Return txtCalendarValue.ClientID
        End Get
    End Property


    ''' <summary>
    ''' Gets/Sets TabIndex of Calendar Control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TabIndex() As Integer
        Get
            Return Me.txtCalendarValue.TabIndex
        End Get

        Set(ByVal value As Integer)
            Me.txtCalendarValue.TabIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets TabIndex of Calendar Control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TextChange() As Boolean
        Get
            Return Me.txtCalendarValue.AutoPostBack
        End Get

        Set(ByVal value As Boolean)
            Me.txtCalendarValue.AutoPostBack = value

        End Set
    End Property



#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ifrmCalendar.Attributes.Add("src", Page.ResolveClientUrl("~/controls/CalendarPopup.aspx?codeentry=" & txtCalendarValue.ClientID & "&container=" & ifrmCalendar.ClientID))
        hlnkCalLink.Attributes.Add("OnClick", "javascript:calendarShowHide('" & ifrmCalendar.ClientID & "');")
        'txtCalendarValue.Attributes.Add("onfocus", "javascript:calendarOnFocus('" & ifrmCalendar.ClientID & "');")
        txtCalendarValue.Attributes.Add("onblur", "javascript:calendarLostFocus('" & ifrmCalendar.ClientID & "');")
        txtCalendarValue.Attributes.Add("onkeypress", "return keyDate(this, '\/');")
    End Sub
End Class
