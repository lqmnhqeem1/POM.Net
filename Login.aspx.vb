Imports POM.Lib.Data
Imports POM.Lib.Security
Imports POM.Lib.UI
Imports POM.Lib.Log
Imports System.Xml
Imports System.Object
Imports System.ComponentModel.MarshalByValueComponent


Partial Class Login
    Inherits System.Web.UI.Page
    Protected screenpx As String = System.Configuration.ConfigurationManager.AppSettings.Item("Screenpx")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objDataSet As DataSet, objDataAccess As DataAccess
        Dim errMsg As Integer

        Try
            'Context.Response.AddHeader("X-UA-Compatible", "IE=7")

            If Not IsNothing(Request.QueryString("user")) AndAlso Request.QueryString("user").Trim().Length > 0 AndAlso
                Not IsNothing(Request.QueryString("pass")) AndAlso Request.QueryString("pass").Trim().Length > 0 AndAlso
                Not IsNothing(Request.QueryString("store")) AndAlso Request.QueryString("store").Trim().Length > 0 Then
                If AuthenticateOrRedirect(Request.QueryString("user"), Request.QueryString("pass"), Request.QueryString("store"), errMsg) Then
                    Response.Redirect("~/SwitchRole.aspx")
                Else
                    Response.Redirect("~/ErrorPage.aspx?code=10004")
                End If

            End If

            If Not IsPostBack Then
                Utility.DropDownDataBind(Me.ddlStore, Constants.Store)
                ddlStore.Attributes.Add("onkeypress", "return keySort(this);")
                objDataAccess = New DataAccess()
                objDataSet = objDataAccess.ExecuteSpDataSet("usp_PopulateCombo", DataAccess.BuildXmlParam("Key", Constants.ClusterStore, "Filter", ""))
                If Not IsNothing(objDataSet) Then
                    If objDataSet.Tables(0).Rows.Count > 1 Then Utility.SetDropDownSelectedValue(ddlStore, objDataSet.Tables(0).Rows(1)("value"))
                End If

                btnLogin.OnClientClick = "return validateLoginEntry(" &
                                        "'" & Utility.GetMessage("20001", lblUserName.Text) & "', " &
                                        "'" & Utility.GetMessage("20004", lblUserName.Text) & "', " &
                                        "'" & Utility.GetMessage("20001", lblPassword.Text) & "', " &
                                        "'" & Utility.GetMessage("20001", lblStore.Text) & "', " &
                                        "'" & txtUserName.ClientID & "', " &
                                        "'" & txtPassword.ClientID & "', " &
                                        "'" & ddlStore.ClientID & "');"
            End If

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error loading Login", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch tex As Threading.ThreadAbortException
                'Ignore
            End Try

        Finally
            If Not IsNothing(objDataSet) Then objDataSet.Dispose()
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
        End Try
    End Sub

    '-----------------------------------------------------------------------------------------
    '---- Modified by   : Nicholas Wong   
    '---- Modified on   : 12 June 2013
    '---- Purpose       : Log user fail login into audit fail login table 
    '---- Create ByRef in function to return error Message 
    '---- Ticket        : [WO 4884-1] - Failed login log in POM
    '---- Updated on    : 3 July 2013
    '-----------------------------------------------------------------------------------------

    Private Function AuthenticateOrRedirect(ByVal UserId As String, ByVal Password As String, ByVal StoreId As String, ByRef errMsg As Integer) As Integer

        Dim strMsg As String = "",
            sPropColl As String(),
            objDataSet As DataSet,
            objDataAccess As DataAccess,
            strADLDAP As String,
            existInAD As Integer

        Try

            ' Update By Lu, 24 Nov 2010, DCL 2940 Authentication based on configurable domain
            objDataAccess = New DataAccess()
            strADLDAP = objDataAccess.GetLDAP_PATH(Split(UserId, "\")(0))

            sPropColl = New String() {ConfigurationManager.AppSettings("ADUserId"), "",
                                        ConfigurationManager.AppSettings("ADUserName"), ""}
            If ActiveDirectory.AuthenticateUser(UserId, Password, strMsg, Nothing, strADLDAP) Then
                'objDataAccess = New DataAccess()
                objDataSet = objDataAccess.ExecuteSpDataSet("usp_UserAuthentication_Search", DataAccess.BuildXmlParam("UserID", UserId.Split("\")(1), "StoreID", StoreId))
                If objDataSet.Tables.Count < 2 Then
                    Response.Redirect("~/ErrorPage.aspx?code=50001")
                Else
                    If CType(objDataSet.Tables(1).Rows(0)("islocal"), Integer) = 1 Then
                        If CType(objDataSet.Tables(0).Rows(0)("HasAccess"), Integer) = 1 Then
                            Utility.UserId = UserId.Split("\")(1)
                            Utility.UserName = CType(objDataSet.Tables(0).Rows(0)("UserName"), String)
                            Utility.UserNetworkId = UserId
                            Utility.UserSecurityCredentials = "i=" & UserId & "»=" & Password

                            Utility.UserStoreId = StoreId
                            Utility.UserStoreName = CType(objDataSet.Tables(1).Rows(0)("storename"), String)

                            Return 1

                        Else

                            lblMessage.Text = Utility.GetMessage("10004")
                            errMsg = 6
                            Return 0
                        End If
                    Else
                        If CType(objDataSet.Tables(1).Rows(0)("storeip"), String).Trim().Length = 0 Then

                            lblMessage.Text = Utility.GetMessage("10022")
                            lblMessage.Visible = True
                            errMsg = 5
                            Return 0
                        Else
                            Response.Redirect("http://" & CType(objDataSet.Tables(1).Rows(0)("storeip"), String) & "/" & ConfigurationManager.AppSettings("PUBLISH_WEB_SITE_NAME") & "/Login.aspx?user=" & UserId & "&pass=" & Password & "&store=" & StoreId)
                            Return 2
                        End If
                    End If
                End If
            Else

                If strMsg.Trim().Length > 0 Then lblMessage.Text = Utility.GetMessage(strMsg)

                If strMsg = "501" Then

                    AuthenticatePOMad(UserId.ToString.Trim(), StoreId, existInAD)
                    If existInAD = 0 Then
                        errMsg = 1
                    ElseIf existInAD = 1 Then
                        errMsg = 2
                    End If
                ElseIf strMsg = "10004" Then
                    errMsg = 3
                ElseIf strMsg = "10153" Then
                    errMsg = 4
                End If
                Return 0
            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Failed to authenticate and populate user data", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx

        Finally
            If Not IsNothing(objDataSet) Then objDataSet.Dispose()
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
        End Try
    End Function

    '-----------------------------------------------------------------------------------------
    '---- Created by    : Nicholas Wong   
    '---- Created on    : 3 July 2013
    '---- Purpose       : To check user id exits in POMADuser table 
    '---- Ticket        : [WO 4884-1] - Failed login log in POM
    '-----------------------------------------------------------------------------------------

    Private Function AuthenticatePOMad(ByVal UserId As String, ByVal StoreId As String, ByRef existInAD As Integer) As Integer

        Dim strMsg As String = "",
            objDataSet As DataSet,
            objDataAccess As DataAccess


        Try
            If UserId.ToString.Trim().Contains("\") Then
                UserId = UserId.ToString.Trim().Split("\")(1)
            Else
                UserId = UserId.ToString.Trim()
            End If

            objDataAccess = New DataAccess()
            objDataSet = objDataAccess.ExecuteSpDataSet("usp_UserAuthentication_Search", DataAccess.BuildXmlParam("UserID", UserId.ToString.Trim(), "StoreID", StoreId))

            If objDataSet.Tables.Count < 1 Then
                existInAD = 1
            Else
                If CType(objDataSet.Tables(0).Rows(0)("HasAccess"), Integer) = 1 Then
                    existInAD = 0
                Else
                    existInAD = 1
                End If
            End If

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Failed to authenticate User Id exist in POMADuser", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            Throw objEx

        Finally
            If Not IsNothing(objDataSet) Then objDataSet.Dispose()
            If Not IsNothing(objDataAccess) Then objDataAccess = Nothing
        End Try
    End Function

    Protected Sub LoggingIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Dim objXmlDoc As XmlDocument
        Dim objDataAcces As DataAccess

        Try
            Dim domain As String = ""
            Dim userName As String = ""
            Dim reasonMsg As String = ""
            Dim errMsg As Integer

            lblMessage.Visible = False
            altMessage.Visible = False

            Select Case AuthenticateOrRedirect(txtUserName.Text.Trim(), txtPassword.Text.Trim(), ddlStore.SelectedItem.Value, errMsg)
                Case 0
                    lblMessage.Visible = True
                    altMessage.Visible = True
                    '-----------------------------------------------------------------------------------------
                    '---- Modified by   : Nicholas Wong   
                    '---- Modified on   : 12 June 2013
                    '---- Purpose       : Log user fail login into audit fail login table
                    '---- Ticket        : [WO 4884-1] - Failed login log in POM
                    '---- Updated on    : 3 July 2013
                    '-----------------------------------------------------------------------------------------
                    objXmlDoc = New XmlDocument
                    objDataAcces = New DataAccess

                    If txtUserName.Text.Trim().Contains("\") Then
                        domain = txtUserName.Text.Trim().Split("\")(0)
                        userName = txtUserName.Text.Trim().Split("\")(1)
                    Else
                        userName = txtUserName.Text.Trim()
                    End If

                    If errMsg = 1 Then
                        errMsg = 10346
                    ElseIf errMsg = 2 Then
                        errMsg = 10347
                    ElseIf errMsg = 3 Then
                        errMsg = 10348
                    ElseIf errMsg = 4 Then
                        errMsg = 10349
                    ElseIf errMsg = 5 Then
                        errMsg = 10022
                    ElseIf errMsg = 6 Then
                        errMsg = 10347
                    End If

                    lblMessage.Text = Utility.GetMessage(errMsg.ToString).ToString.Trim()
                    reasonMsg = Utility.GetMessage(errMsg.ToString).ToString.Trim()
                    objXmlDoc = DataAccess.BuildXmlParam("DOMAIN", domain.ToString, "USERID", userName.ToString.Trim(), "STORE_CODE", ddlStore.SelectedItem.Value, "REASON", reasonMsg.ToString.Trim())
                    objDataAcces.ExecuteSpNoResult("usp_AuditLogFailLogin", objXmlDoc)
                    '-----------------------------------------------------------------------
                Case 1
                    Response.Redirect("~/SwitchRole.aspx")

            End Select

        Catch tex As Threading.ThreadAbortException
            'Ignore
        Catch ex As Exception
            Dim objEx As New ApplicationException("Unable to authenticate user", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)

            Try
                Response.Redirect("~/ErrorPage.aspx?code=50001")
            Catch iex As Exception
                'Ignore
            End Try
        Finally
            objDataAcces = Nothing
            objXmlDoc = Nothing
        End Try

    End Sub

End Class