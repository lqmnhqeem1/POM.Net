Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.UI

Partial Class ListOfValues
    Inherits System.Web.UI.UserControl

#Region "Private Variables"
    Private _strLOVCode As String,
            _strSelectedParamId As String = "",
            _strAdditionalData As String = "",
            _strAdditionalFilter As String = "",
            _strOnClientClick As String = "",
            _strOnDataPopulate As String = "",
            _bAutoMode As Boolean = False
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strClickScript As String = ""

        If Not Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType(), "ListOfValuesScript") Then
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ListOfValuesScript", String.Empty)
            CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl("<script type=""text/javascript"" src=""" & Page.ResolveUrl("~/controls/js/listofvalues.js") & """></script>"))

        End If

        ' Associate LOV button click to opening popup
        strClickScript = "javascript:if(" & IIf(Me.OnClientClick.Trim().Length > 0, Me.OnClientClick, "true") & "){openListOfValues('" & Page.ResolveClientUrl("~/controls/ListOfValuesPopup.aspx") & "', '" & Me.txtLOVCode.ClientID & "', '" & Me.Code & "', '" & Me.SelectedParamId & "', '" & Me.AdditionalData & "', '" & Me.AdditionalFilter & "', '" & Me.OnDataPopulate & "', '" & IIf(Me.AutoMode, "auto", "manual") & "','BUTTON');}return false;"
        btnSearch.Attributes.Add("onclick", strClickScript)
        btnSearch.Attributes.Add("autoscript", strClickScript.Replace("javascript:", "").Replace("return false;", "").Replace("BUTTON", "AUTO"))
        'If btnSearch.OnClientClick.Trim().Length = 0 Then btnSearch.OnClientClick = "javascript:if(" & IIf(Me.OnClientClick.Trim().Length > 0, Me.OnClientClick, "true") & "){openListOfValues('" & Me.txtLOVCode.ClientID & "', '" & Me.Code & "', '" & Me.SelectedParamId & "', '" & Me.AdditionalData & "', '" & Me.AdditionalFilter & "', '" & Me.OnDataPopulate & "', '" & IIf(Me.AutoMode, "auto", "manual") & "');}return false;"

    End Sub

#Region "Public Properties"
    ''' <summary>
    ''' Gets or Sets List Of Value Id to display
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property Code() As String
        Get
            Return _strLOVCode
        End Get

        Set(ByVal value As String)
            _strLOVCode = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the parameter id whose data would be returned
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property SelectedParamId() As String
        Get
            Return IIf(IsNothing(_strSelectedParamId), "", _strSelectedParamId)
        End Get

        Set(ByVal value As String)
            _strSelectedParamId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets List Of Values selected Data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property Text() As String
        Get
            Return txtLOVCode.Text
        End Get

        Set(ByVal value As String)
            txtLOVCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets CssClass for ListOfValues
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property ListOfValuesCssClass() As String
        Get
            Return Me.txtLOVCode.CssClass
        End Get

        Set(ByVal value As String)
            Me.txtLOVCode.CssClass = value
            Me.btnSearch.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the CssClass for ListOfValue TextBox
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property TextBoxCssClass() As String
        Get
            Return Me.txtLOVCode.CssClass
        End Get

        Set(ByVal value As String)
            Me.txtLOVCode.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the CssClass for ListOfValues Button
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property ButtonCssClass() As String
        Get
            Return Me.btnSearch.CssClass
        End Get

        Set(ByVal value As String)
            Me.btnSearch.CssClass = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets MaxLength of ListOfValues Entry
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property MaxLength() As Integer
        Get
            Return Me.txtLOVCode.MaxLength
        End Get

        Set(ByVal value As Integer)
            Me.txtLOVCode.MaxLength = value
        End Set
    End Property

    ''' <summary>
    ''' Gets Textbox object to associate with
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>One may need this handle in order to set validation controls</remarks>
    Public ReadOnly Property TextEntryHandle() As System.Web.UI.WebControls.TextBox
        Get
            Return Me.txtLOVCode
        End Get
    End Property

    ''' <summary>
    ''' Gets TextBox ClientID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public ReadOnly Property TextEntryClientID() As String
        Get
            Return txtLOVCode.ClientID
        End Get
    End Property

    ''' <summary>
    ''' Sets field-param mapping for any additional required data
    ''' </summary>
    ''' <value></value>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property AdditionalData() As String
        Get
            Return IIf(IsNothing(_strAdditionalData), "", _strAdditionalData)
        End Get

        Set(ByVal value As String)
            _strAdditionalData = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets param-field mapping for any additional data filtering
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property AdditionalFilter() As String
        Get
            Return IIf(IsNothing(_strAdditionalFilter), "", _strAdditionalFilter)
        End Get

        Set(ByVal value As String)
            _strAdditionalFilter = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Control Enable State
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property Enabled() As Boolean
        Get
            Return txtLOVCode.Enabled And btnSearch.Enabled
        End Get

        Set(ByVal value As Boolean)
            txtLOVCode.Enabled = value
            btnSearch.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets ListOfValues TabIndex
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property TabIndex() As Integer
        Get
            Return Me.txtLOVCode.TabIndex
        End Get

        Set(ByVal value As Integer)
            Me.txtLOVCode.TabIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Client Script to run OnClick of this button
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property OnClientClick() As String
        Get
            Return IIf(IsNothing(_strOnClientClick), "", _strOnClientClick)
        End Get

        Set(ByVal value As String)
            Me._strOnClientClick = IIf(value.Trim().EndsWith(";"), value.Trim().TrimEnd(";"), value.Trim())
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Client Script to run OnDataPopulate of this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property OnDataPopulate() As String
        Get
            Return IIf(IsNothing(_strOnDataPopulate), "", _strOnDataPopulate)
        End Get

        Set(ByVal value As String)
            Me._strOnDataPopulate = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets Auto Operation Mode of List Of Values
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Author: Tanmoy Paul</remarks>
    Public Property AutoMode() As Boolean
        Get
            Return IIf(IsNothing(_bAutoMode), False, _bAutoMode)
        End Get

        Set(ByVal value As Boolean)
            Me._bAutoMode = value
            If Me._bAutoMode Then
                Me.txtLOVCode.Attributes.Add("onblur", "return autoOpenLOV('" & txtLOVCode.ClientID & "', '" & btnSearch.ClientID & "');")
            Else
                Me.txtLOVCode.Attributes.Remove("onblur")
            End If
        End Set
    End Property

#End Region

End Class
