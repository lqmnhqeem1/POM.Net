Imports POM.Lib.UI
Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.Log
Imports POM.Lib.Security

Partial Class ViewPromotions
    Inherits System.Web.UI.Page


#Region "Objects"
    Dim objXmlDoc As XmlDocument
    Dim objDataAcces As DataAccess
    Dim Departments As ArrayList
    Dim strDepartment As String = String.Empty
    Dim objDSPromotion As DataSet
#End Region
    ''' <summary>
    ''' Search for Normal Promotion based on search criteria entered.
    ''' Tables used are Product_Price_Header, Product_Price, Product.
    ''' Search Allowed on any of the following : PromoCode, PromoDesc, ProductCode, ProductDesc, StartDate, EndDate, UserID, StoreCode.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Author Sachin Jain (31/07/2006)</remarks>

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'If Utility.UserId = "" Then Response.Redirect("..\Login.aspx")

            objDataAcces = New DataAccess
            grdPromotion.CurrentPageIndex = 0
            objDSPromotion = New DataSet

            Session("ProductEnqTable") = Nothing

            ViewState("PageIndex") = "1"

            ViewState("PromId") = lovPromoCode.Text.ToString().Trim()
            ViewState("PromDesc") = txtPromoDesc.Text.Trim()
            ViewState("ProdID") = lovProductCode.Text.ToString().Trim()
            ViewState("ProdDesc") = txtProductDesc.Text.ToString().Trim()
            ViewState("StartDate") = calStartDate.Text.ToString().Trim()
            ViewState("EndDate") = calEndDate.Text.ToString().Trim()


            Session("PromId") = ViewState("PromId")
            Session("PromDesc") = ViewState("PromDesc")
            Session("ProdID") = ViewState("ProdID")
            Session("ProdDesc") = ViewState("ProdDesc")
            Session("StartDate") = ViewState("StartDate")
            Session("EndDate") = ViewState("EndDate")

            FetchData()
        Catch threadEx As Threading.ThreadAbortException


        Catch ex As Exception
            Dim objEx As New Exception("Error on Page load", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("..\ErrorPage.aspx?code=50001")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Utility.UserId = "" Then Response.Redirect("..\Login.aspx")
            lovPromoCode.TextEntryHandle.Focus()
            lovPromoCode.AdditionalData = txtPromoDesc.ClientID & "=PromoDesc"



            If (Not Access.ScreenAccess(Constants.PROMOTIONEnquiry)) Then
                Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=Promotion Enquiry")
                Return
            Else
                Dim strFunc As String() = Access.ScreenFunctions(Constants.PROMOTIONEnquiry)
                Dim strFunctions As New ArrayList
                If Not IsNothing(strFunc) Then
                    If (strFunc.Length > 0) Then
                        Dim intcounter As Int32
                        For intcounter = 0 To strFunc.Length - 1
                            strFunctions.Add(strFunc(intcounter))   'Store Rights in ArrayList to retrieve later.
                        Next
                    End If
                    'ViewState.Add("Functions", strFunctions)
                End If

                If strFunctions.IndexOf("F1") < 0 Then btnSearch.Enabled = False
                'If strFunctions.IndexOf("F3") < 0 Then btnPrint.Enabled = False
                If grdPromotion.Items.Count > 0 Then
                    If strFunctions.IndexOf("F3") < 0 Then btnPrint.Enabled = False
                Else
                    btnPrint.Enabled = False
                End If

            End If

            If Not IsNothing(Session("ProductEnqTable")) Then
                objDSPromotion = CType(Session("ProductEnqTable"), DataSet)
            End If

            If (Not IsPostBack) Then
                grdPromotion.PagerStyle.Visible = False
                grdPromotion.DataSource = ShowGrid()
                grdPromotion.DataBind()

                'Add attributes for Date validation

                btnSearch.Attributes.Add("onClick", "return validatePromotionSearch('" +
                                            lovProductCode.ClientID.ToString + "','" + Utility.GetMessage("20002", lblProductCode.Text) + "','" +
                                            lovPromoCode.ClientID.ToString + "','" + Utility.GetMessage("20002", lblPromoCode.Text) + "','" +
                                            calStartDate.ClientID.ToString + "','" + Utility.GetMessage("20003", lblstartdate.Text) + "','" +
                                            calEndDate.ClientID.ToString + "','" + Utility.GetMessage("20003", lblEndDate.Text) + "','" +
                                            txtPromoDesc.ClientID.ToString + "','" + txtProductDesc.ClientID.ToString + "','" +
                                            Utility.GetMessage("30003") + "','" +
                                            Utility.GetMessage("20005", lblPromoCode.Text, lblPromoDesc.Text, lblProductCode.Text, lblProductDesc.Text, lblstartdate.Text, lblEndDate.Text) + "');")


                Departments = Utility.UserDepartments
                'Departments are passed to procedure in the format '|01|02|03|04|'
                For intIndex As Integer = 0 To Departments.Count - 1
                    strDepartment += "|" + Departments(intIndex).ToString
                Next
                If strDepartment.Length > 0 Then strDepartment += "|"

                ViewState.Add("Departments", strDepartment)

                If (Utility.FromGoBack) Then
                    If Not (IsNothing(Session("PromId")) Or IsNothing(Session("PromDesc")) Or IsNothing(Session("ProdID")) Or IsNothing(Session("ProdDesc")) Or IsNothing(Session("StartDate")) Or IsNothing(Session("EndDate"))) Then
                        If Not IsNothing(Session("PromID")) Then
                            lovPromoCode.Text = CType(Session("PromID"), String)
                        End If
                        If Not IsNothing(Session("PromDesc")) Then
                            txtPromoDesc.Text = CType(Session("PromDesc"), String)
                        End If
                        If Not IsNothing(Session("ProdID")) Then
                            lovProductCode.Text = CType(Session("ProdID"), String)
                        End If
                        If Not IsNothing(Session("ProdDesc")) Then
                            txtProductDesc.Text = CType(Session("ProdDesc"), String)
                        End If
                        If Not IsNothing(Session("StartDate")) Then
                            calStartDate.Text = CType(Session("StartDate"), String)
                        End If
                        If Not IsNothing(Session("EndDate")) Then
                            calEndDate.Text = CType(Session("EndDate"), String)
                        End If
                        If Not IsNothing(Session("PES_PageSet")) Then
                            ViewState("PageIndex") = CType(Session("PES_PageSet"), String)
                        Else
                            ViewState("PageIndex") = 1
                        End If

                        ViewState("PromId") = lovPromoCode.Text.ToString().Trim()
                        ViewState("PromDesc") = txtPromoDesc.Text.Trim()
                        ViewState("ProdID") = lovProductCode.Text.ToString().Trim()
                        ViewState("ProdDesc") = txtProductDesc.Text.ToString().Trim()
                        ViewState("StartDate") = calStartDate.Text.ToString().Trim()
                        ViewState("EndDate") = calEndDate.Text.ToString().Trim()

                        FetchData()
                        If Not IsNothing(Session("PES_PageNo")) Then
                            grdPromotion_PageIndexChanged(grdPromotion, New System.Web.UI.WebControls.DataGridPageChangedEventArgs(grdPromotion, CType(Session("PES_PageNo"), Int32)))
                        Else

                        End If
                    End If


                End If
            End If
            Dim strCheckKeyScript As String

            strCheckKeyScript = "<script type=""text/javascript"" language=""javascript"">" &
            "function checkKey(event){" &
            "var defcontrol;" &
            "if(event.keyCode==13){" &
            "defcontrol = document.getElementById('" & btnSearch.ClientID & "');" &
            "if(defcontrol != null) defcontrol.focus();" &
            "}" &
            "return false;" &
            "}" &
            "</script>"
            Page.ClientScript.RegisterClientScriptBlock(GetType(String), "checkKey", strCheckKeyScript)

        Catch tex As System.Threading.ThreadAbortException
            'ignore
        Catch ex As Exception
            Dim objEx As New Exception("Error on Page load", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("..\ErrorPage.aspx?code=50001")
        End Try
    End Sub


    ''' <summary>
    ''' To show grid on page when page initially loads
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowGrid() As DataSet
        Try

            Dim dsEmpty As DataSet = New DataSet()
            objDSPromotion = New DataSet()
            Dim dtEmpty As DataTable = New DataTable
            Dim dcEmpty As DataColumnCollection = dtEmpty.Columns

            'clear the grid
            dcEmpty.Add("Promotion_Code", "".GetType)
            dcEmpty.Add("Promo_Theme", "".GetType)
            dcEmpty.Add("Start_Date", "".GetType)
            dcEmpty.Add("End_Date", "".GetType)
            dcEmpty.Add("Price_Priority", "".GetType)
            dsEmpty.Tables.Add(dtEmpty)
            Return dsEmpty


        Catch ex As Exception
            Dim objEx As New Exception("Error in Showing blank grid", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("..\ErrorPage.aspx?code=50001")
        End Try
    End Function

    ''' <summary>
    ''' usp_PromotionSearch : PromotionCode, PromoDesc, ProductCode, ProductDesc, StartDate, EndDate, Departments (|01|02|03|04| etc.), StoreCode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FetchData()
        Try
            objDataAcces = New DataAccess
            objDSPromotion = New DataSet()

            objXmlDoc = DataAccess.BuildXmlParam("PromoCode", CType(ViewState("PromId"), String), "PromoDesc", CType(ViewState("PromDesc"), String), "ProductCode", CType(ViewState("ProdID"), String), "ProductDesc", CType(ViewState("ProdDesc"), String), "StartDate", CType(ViewState("StartDate"), String), "EndDate", CType(ViewState("EndDate"), String), "Departments", ViewState.Item("Departments"), "StoreCode", Utility.UserStoreId, "Page_Index", CType(ViewState("PageIndex"), String))
            objDSPromotion = objDataAcces.ExecuteSpDataSet("usp_PromotionSearch", objXmlDoc)


            If objDSPromotion.Tables(0).Rows.Count = 0 Then
                lblNoRecords.Visible = True
                grdPromotion.DataSource = ShowGrid().Tables(0)
                grdPromotion.DataBind()
                grdPromotion.PagerStyle.Visible = False
                btnPrint.Enabled = False
                Return
            Else
                Session("ProductEnqTable") = objDSPromotion
                grdPromotion.AllowPaging = True
                grdPromotion.DataSource = objDSPromotion.Tables(0)
                grdPromotion.DataBind()
                grdPromotion.PagerStyle.Visible = True
                lblNoRecords.Visible = False
                btnPrint.Enabled = True
            End If


        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data from DB", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        End Try
    End Sub

    ''' <summary>
    ''' Set page index
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdPromotion_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdPromotion.PageIndexChanged
        Try
            grdPromotion.CurrentPageIndex = e.NewPageIndex
            grdPromotion.DataSource = objDSPromotion.Tables(0)
            grdPromotion.DataBind()
            FetchData()
            Session("PES_PageNo") = e.NewPageIndex()
        Catch ex As Exception
            Dim objEx As New Exception("Error in Grid Paging", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    ''' <summary>
    ''' Reset all controls and remove records from grid. Set pageindex = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            grdPromotion.CurrentPageIndex = 0
            lovProductCode.Text = ""
            txtProductDesc.Text = ""
            lovPromoCode.Text = ""
            txtPromoDesc.Text = ""
            calStartDate.Text = ""
            calEndDate.Text = ""
            lblNoRecords.Visible = False
            Session("ProductEnqTable") = Nothing

            Session("PromID") = Nothing
            Session("PromDesc") = Nothing
            Session("ProdID") = Nothing
            Session("ProdDesc") = Nothing
            Session("StartDate") = Nothing
            Session("EndDate") = Nothing
            Session("PES_PageSet") = Nothing
            Session("PES_PageNo") = Nothing

            ViewState("PageIndex") = "1"
            ViewState("PromID") = ""
            ViewState("PromDesc") = ""
            ViewState("ProdID") = ""
            ViewState("ProdDesc") = ""
            ViewState("StartDate") = ""
            ViewState("EndDate") = ""
            grdPromotion.DataSource = ShowGrid().Tables(0)
            grdPromotion.DataBind()
            grdPromotion.PagerStyle.Visible = False


        Catch ex As Exception
            Dim objEx As New Exception("Error in Resetting Controls", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try

    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'Response.Redirect("~\ErrorPage.aspx?code=10003")
        Dim index As Integer,
            sPromoID As String = "",
            strURL As String
        Dim strServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("ReportingPath")

        Try
            For index = 0 To grdPromotion.Items.Count - 1
                'sVendor = sVendor & IIf(sVendor.Trim().Length = 0, "", ",") & grdSchedule.Items(index).Cells(0).Text.Replace("&nbsp;", "")
                sPromoID = sPromoID & IIf(sPromoID.Trim().Length = 0, "", ",") & grdPromotion.Items(index).Cells(0).Text.Replace("&nbsp;", "")

            Next
            strURL = "<script type=""text/javascript"" language=""javascript"">window.open('" + strServer + "PP001&rc:Parameters=false&rs:Command=Render&iintStoreCode=" + Utility.UserStoreId + "&iintPromotionID=" + sPromoID + "','new_winPP0001','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1');</script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PP001", strURL)
        Catch ex As Exception

        End Try
        'Response.Redirect("../errorpage.aspx?code=10003")

    End Sub


    Protected Sub lovPromoCode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lovPromoCode.Load

    End Sub
End Class
