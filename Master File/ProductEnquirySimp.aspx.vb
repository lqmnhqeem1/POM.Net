#Region "Imports"
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Security
Imports System.Xml
Imports POM.Lib.Log
#End Region
Public Class ProductEnquirySimp
    Inherits System.Web.UI.Page
    Protected strPrdtId As String
    Dim dsProd As New DataSet

#Region "Private Constant"
    Private Const STR_DEPT As String = "DEPT"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_DEPTTo As String = "DEPTTo"
    Private Const STR_CAT As String = "CAT"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_CATTo As String = "CATTo"
    Private Const STR_PRDT_CODE As String = "Product_Code"
    Private Const STR_PRDT_DESC As String = "Prod_Desc"
    Private Const STR_UPC As String = "UPC"
    Private Const STR_DEPT_SP As String = "Dept"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_DEPTTo_SP As String = "DeptTo"
    Private Const STR_CAT_SP As String = "Catg"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_CATTo_SP As String = "CatgTo"
    Private Const STR_STORE_SP As String = "Store_Code"
    Private Const STR_SP_NAME As String = "usp_ProductEnquirySearch"
    Private Const STR_STORE_QS As String = "storeid"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_NewPSC_SP As String = "NewProductShelfCapacity"
    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Const STR_User_SP As String = "UserId"
    Private Const STR_BLANK As String = ""
    Private Const INT_UNHIDE As Int32 = 1
    Private Const INT_HIDE As Int32 = 0
    Private Const STR_MASTER As String = "0"
    Private Const STR_HIGHSHRINK As String = "1"
    Private Const STR_SELFCAPACITY As String = "2"
#End Region

#Region "Load Event"
    ''' <summary>
    ''' Load Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

            Dim strValue As String = Convert.ToString(Request.QueryString("ScrId"))
            Dim strScreenName As String = String.Empty
            If (strValue Is Nothing) Then
                strValue = ""
            End If
            If (strValue = STR_MASTER) Then
                strPrdtId = "ProductHierarchySimp.aspx"
                strScreenName = "Product Hierarchy (Simplify)"
            ElseIf (strValue = STR_HIGHSHRINK) Then
                strPrdtId = "~/Store Attribute/HighShrinkHighValue.aspx"
                strScreenName = "High Shrink High Value"
            ElseIf (strValue = STR_SELFCAPACITY) Then
                strPrdtId = "~/Store Attribute/ShelfCapacity.aspx"
                strScreenName = "Shelf Capacity"
            Else
                strPrdtId = "ProductHierarchySimp.aspx"
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
            btnSearch.Enabled = False
            If (Not Access.ScreenAccess(Constants.PRODENQ)) Then
                'Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=Product Enquiry")
                Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=" & strScreenName & "")
                Return
            Else
                Dim strFunc As String() = Access.ScreenFunctions(Constants.PRODENQ)
                If Not IsNothing(strFunc) Then
                    If (strFunc.Length > 0) Then
                        Dim intcounter As Int32
                        For intcounter = 0 To strFunc.Length - 1
                            If strFunc(intcounter) = Constants.CONST_F1 Then
                                btnSearch.Enabled = True
                            End If
                            'If strFunc(intcounter) = Constants.CONST_F2 Then
                            '    If (strPrdtId = "~/Store Attribute/HighShrinkHighValue.aspx") Then
                            '        Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=HighShrink HighValue")
                            '        Return
                            '    End If
                            'End If
                            'If strFunc(intcounter) = Constants.CONST_F3 Then
                            '    If (strPrdtId = "~/Store Attribute/ShelfCapacity.aspx") Then
                            '        Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=Shelf Capacity")
                            '        Return
                            '    End If
                            'End If
                        Next
                    End If
                End If
                If (strPrdtId = "~/Store Attribute/HighShrinkHighValue.aspx") Then
                    Dim blHSHV As Boolean = Utility.GetFunctionAccess(Constants.PRODENQ, Constants.CONST_F2)
                    If (blHSHV = False) Then
                        Response.Redirect("~/ErrorPage.aspx?Code=10019&ScrId=HighShrink HighValue")
                    End If
                End If
                If (strPrdtId = "~/Store Attribute/ShelfCapacity.aspx") Then
                    Dim blSC As Boolean = Utility.GetFunctionAccess(Constants.PRODENQ, Constants.CONST_F3)
                    If (blSC = False) Then
                        Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=Shelf Capacity")
                        Return
                    End If
                End If
            End If

            If Not IsNothing(Session("ProductEnqTable")) Then
                dsProd = CType(Session("ProductEnqTable"), DataSet)
            End If
            If (Not IsPostBack) Then

                btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntry(" &
                                    "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " &
                                    "'" & txtProductCode.ClientID & "', " &
                                    "'" & txtPrdtDesc.ClientID & "', " &
                                    "'" & txtUPC.ClientID & "', " &
                                    "'" & ddlDept.ClientID & "', " &
                                    "'" & ddlCatg.ClientID & "', " &
                                    "'" & ddlStatus.ClientID & "');")

                Dim ar As ArrayList = Utility.UserDepartments()
                Dim str As String = ""
                Dim strComma As String = ","
                Dim intCount As Int32
                For intCount = 0 To ar.Count - 1
                    If (intCount = 0) Then
                        str = str + ar.Item(intCount)
                    Else
                        str = str + strComma + ar.Item(intCount)
                    End If
                Next

                'Commented By: Farnia Emami, Commented at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                'Utility.DropDownDataBind(ddlDept, Constants.DeptCode, str)

                ''Commented By Amit Rawat for Category Enhancement request Starts 
                ''Utility.DropDownDataBind(ddlCatg, Constants.Category, "-1")
                ''Commented By Amit Rawat for Category Enhancement request Ends

                ''UnCommented By Amit Rawat for Category Enhancement request Starts
                'Utility.DropDownDataBind(ddlCatg, STR_CAT, str)
                ''UnCommented By Amit Rawat for Category Enhancement request Ends

                ''*********Added by Rashi Goyal Dated: 3rd Nov. '06*************
                'Utility.DropDownDataBind(ddlStatus, Constants.VendorStatus)
                ''****************************END*******************************

                'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                LoadDropDownLists()


                dgProduct.DataSource = GetDataSet().Tables(0)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = False
                txtProductCode.Focus()


                If (Utility.FromGoBack) Then
                    'Modified By: Farnia Emami, Modified at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                    'If Not (IsNothing(Session("ProdId")) Or IsNothing(Session("ProdDesc")) Or IsNothing(Session("ProdUPC")) Or IsNothing(Session("Dept")) Or IsNothing(Session("Catg")) Or IsNothing(Session("Status"))) Then
                    If Not (IsNothing(Session("ProdId")) Or IsNothing(Session("ProdDesc")) Or IsNothing(Session("ProdUPC")) Or IsNothing(Session("Dept")) Or IsNothing(Session("DeptTo")) Or IsNothing(Session("Catg")) Or IsNothing(Session("CatgTo")) Or IsNothing(Session("Status")) Or IsNothing(Session("BlForPro"))) Then
                        If Not IsNothing(Session("ProdId")) Then
                            txtProductCode.Text = CType(Session("ProdId"), String)
                        End If
                        If Not IsNothing(Session("ProdDesc")) Then
                            txtPrdtDesc.Text = CType(Session("ProdDesc"), String)
                        End If
                        If Not IsNothing(Session("ProdUPC")) Then
                            txtUPC.Text = CType(Session("ProdUPC"), String)
                        End If
                        '************Modified By:Indira Tiwari***************************
                        If Not IsNothing(Session("Dept")) Then
                            Utility.SetDropDownSelectedValue(ddlDept, CType(Session("Dept"), String))
                        End If
                        ddlDept_SelectedIndexChanged(Nothing, Nothing)

                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        If Not IsNothing(Session("DeptTo")) Then
                            Utility.SetDropDownSelectedValue(ddlDeptTo, CType(Session("DeptTo"), String))
                        End If
                        ddlDeptTo_SelectedIndexChanged(Nothing, Nothing)

                        If Not IsNothing(Session("Catg")) Then
                            Utility.SetDropDownSelectedValue(ddlCatg, CType(Session("Catg"), String))
                        End If

                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        If Not IsNothing(Session("CatgTo")) Then
                            Utility.SetDropDownSelectedValue(ddlcatgTo, CType(Session("CatgTo"), String))
                        End If
                        '*****************************************************************

                        '****************Added By Rashi Goyal Dated: 3rd Nov. '06*********
                        If Not IsNothing(Session("Status")) Then
                            Utility.SetDropDownSelectedValue(ddlStatus, CType(Session("Status"), String))
                        End If
                        '**********************END****************************************

                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        If Not IsNothing(Session("BlForPro")) Then
                            If (Session("BlForPro") = "") Then
                                Me.cbBlockOfPro.Checked = True
                                Me.cbBlockOfPro_NO.Checked = True
                            ElseIf (Session("BlForPro") = "T") Then
                                Me.cbBlockOfPro.Checked = True
                                Me.cbBlockOfPro_NO.Checked = False
                            ElseIf (Session("BlForPro") = "F") Then
                                Me.cbBlockOfPro.Checked = False
                                Me.cbBlockOfPro_NO.Checked = True
                            ElseIf (Session("BlForPro") = "X") Then
                                Me.cbBlockOfPro.Checked = False
                                Me.cbBlockOfPro_NO.Checked = False
                            End If
                        End If


                        If Not IsNothing(Session("PES_PageSet")) Then
                            ViewState("PageIndex") = CType(Session("PES_PageSet"), String)
                        Else
                            ViewState("PageIndex") = 1
                        End If
                        ViewState("ProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
                        ViewState("ProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
                        ViewState("ProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
                        ViewState("Dept") = ddlDept.SelectedValue.ToString().Trim()
                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        ViewState("DepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
                        ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        ViewState("CatgTo") = ddlcatgTo.SelectedValue.ToString().Trim()
                        ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()

                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        Dim BlOfPro As String = ""
                        If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                            BlOfPro = ""
                        ElseIf Me.cbBlockOfPro.Checked = True Then
                            BlOfPro = "T"
                        ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                            BlOfPro = "F"
                        ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                            BlOfPro = "X"
                        End If
                        ViewState("BlForPro") = BlOfPro
                        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        ViewState("IsLiveFiltering") = 1

                        FetchData()
                        If Not IsNothing(Session("PES_PageNo")) Then
                            dgProduct_PageIndexChanged(dgProduct, New System.Web.UI.WebControls.DataGridPageChangedEventArgs(dgProduct, CType(Session("PES_PageNo"), Int32)))
                        Else

                        End If

                        Session("PES_PageSet") = Nothing
                        Session("PES_PageNo") = Nothing

                    End If
                End If
            End If
        Catch httpex As Threading.ThreadAbortException
        Catch ex As Exception
            Dim objEx As New Exception("Error in Populating Dropdown", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "Search Event"
    ''' <summary>
    ''' To Search products according to search criteria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            'Modified By: Farnia Emami, Modified at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            'If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And txtUPC.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlStatus.SelectedValue = "") Then
            If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlcatgTo.SelectedValue = "" And txtUPC.Text.Trim() = "" And ddlStatus.SelectedValue = "" And cbBlockOfPro.Checked = False And cbBlockOfPro_NO.Checked = False) Then
                lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                lblHerr.Text = Utility.GetMessage("20001", "Atleast One Input Value")
                lblError.CssClass = ""
                lblError.Text = ""
                dgProduct.DataSource = GetDataSet().Tables(0)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = False
                Return
            End If

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            If Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False Then
                lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                lblHerr.Text = Utility.GetMessage("20001", "At Least One Checkbox Blocked For Ordering Value")
                lblError.CssClass = ""
                lblError.Text = ""
                dgProduct.DataSource = GetDataSet().Tables(0)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = False
                Return
            End If

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Dim BlOfPro As String = ""
            If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = ""
            ElseIf Me.cbBlockOfPro.Checked = True Then
                BlOfPro = "T"
            ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = "F"
            ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                BlOfPro = "X"
            End If

            Dim HGHValue As String = ""

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ViewState("IsLiveFiltering") = 0
            ViewState("SCOPEProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEDept") = ddlDept.SelectedValue.ToString().Trim()
            ViewState("SCOPEDepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
            ViewState("SCOPECatg") = ddlCatg.SelectedValue.ToString().Trim()
            ViewState("SCOPECatgTo") = ddlcatgTo.SelectedValue.ToString().Trim()
            ViewState("SCOPEUserStore") = Utility.UserStoreId()
            ViewState("SCOPEPageIndex") = 1
            ViewState("SCOPEStatus") = ddlStatus.SelectedValue.ToString().Trim()
            ViewState("SCOPEBlForPro") = BlOfPro
            ViewState("SCOPEHGHV") = HGHValue
            ViewState("SCOPEWithShelfCapacity") = "0"
            ViewState("SCOPEDiv") = ""
            '------------------------------------------------------------------------------ 

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Session("SCOPEIsLiveFiltering") = ViewState("IsLiveFiltering")
            Session("SCOPEProdId") = ViewState("SCOPEProdId")
            Session("SCOPEProdDesc") = ViewState("SCOPEProdDesc")
            Session("SCOPEProdUPC") = ViewState("SCOPEProdUPC")
            Session("SCOPEDept") = ViewState("SCOPEDept")
            Session("SCOPEDeptTo") = ViewState("SCOPEDepTo")
            Session("SCOPECatg") = ViewState("SCOPECatg")
            Session("SCOPECatgTo") = ViewState("SCOPECatgTo")
            Session("SCOPEUserStore") = ViewState("SCOPEUserStore")
            Session("SCOPEPageIndex") = ViewState("SCOPEPageIndex")
            Session("SCOPEStatus") = ViewState("SCOPEStatus")
            Session("SCOPEBlForPro") = ViewState("SCOPEBlForPro")
            Session("SCOPEHGHV") = ViewState("SCOPEHGHV")
            Session("SCOPEWithShelfCapacity") = ViewState("SCOPEWithShelfCapacity")
            Session("SCOPEDiv") = ViewState("SCOPEDiv")


            'Commented By: Farnia Emami, Commented at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)

            'dgProduct.CurrentPageIndex = 0
            'dsProd = New DataSet()
            'Session("ProductEnqTable") = Nothing

            'ViewState("PageIndex") = "1"
            'ViewState("ProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            'ViewState("Dept") = ddlDept.SelectedValue.ToString().Trim()
            'ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
            'ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()

            'Session("ProdId") = ViewState("ProdId")
            'Session("ProdDesc") = ViewState("ProdDesc")
            'Session("ProdUPC") = ViewState("ProdUPC")
            'Session("Dept") = ViewState("Dept")
            'Session("Catg") = ViewState("Catg")
            'Session("Status") = ViewState("Status")

            'FetchData()
            'lblHerr.Text = ""

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            LiveFiltering()


        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try

    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Sub LiveFiltering()
        Try

            Dim BlOfPro As String = ""

            If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = ""
            ElseIf Me.cbBlockOfPro.Checked = True Then
                BlOfPro = "T"
            ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = "F"
            ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                BlOfPro = "X"
            End If

            Dim HGHValue As String = ""

            dgProduct.CurrentPageIndex = 0
            dsProd = New DataSet()
            Session("ProductEnqTable") = Nothing

            ViewState("PageIndex") = "1"
            ViewState("ProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            ViewState("ProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            ViewState("ProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            ViewState("Dept") = ddlDept.SelectedValue.ToString().Trim()
            ViewState("DepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
            ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
            ViewState("CatgTo") = ddlcatgTo.SelectedValue.ToString().Trim()
            ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
            ViewState("BlForPro") = BlOfPro
            ViewState("HGHV") = HGHValue
            ViewState("WithShelfCapacity") = "0"
            ViewState("Div") = ""

            Session("ProdId") = ViewState("ProdId")
            Session("ProdDesc") = ViewState("ProdDesc")
            Session("ProdUPC") = ViewState("ProdUPC")
            Session("Dept") = ViewState("Dept")
            Session("DeptTo") = ViewState("DepTo")
            Session("Catg") = ViewState("Catg")
            Session("CatgTo") = ViewState("CatgTo")
            Session("Status") = ViewState("Status")
            Session("BlForPro") = ViewState("BlForPro")

            FetchData()

        Catch ex As Exception
            Dim objEx As New Exception("Error in Live Filtering Function", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

#End Region

#Region "Reset Controls"
    ''' <summary>
    ''' Reset Controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Dim dsGrid As DataSet = New DataSet()
            txtProductCode.Text = STR_BLANK
            txtPrdtDesc.Text = STR_BLANK
            txtUPC.Text = STR_BLANK
            ddlDept.SelectedIndex = -1
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ddlDeptTo.SelectedItem.Text = "--Select--"
            ddlCatg.SelectedIndex = -1
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ddlcatgTo.SelectedItem.Text = "--Select--"
            ddlStatus.SelectedIndex = -1
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            cbBlockOfPro.Checked = True
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            cbBlockOfPro_NO.Checked = True
            lblError.Text = ""
            lblError.CssClass = ""
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            dgProduct.CurrentPageIndex = 0
            Session("ProductEnqTable") = Nothing

            Session("ProdId") = Nothing
            Session("ProdDesc") = Nothing
            Session("ProdUPC") = Nothing
            Session("Dept") = Nothing
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Session("DeptTo") = Nothing
            Session("Catg") = Nothing
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Session("CatgTo") = Nothing
            Session("Status") = Nothing
            Session("BlForPro") = Nothing
            Session("PES_PageSet") = Nothing
            Session("PES_PageNo") = Nothing

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Session("SCOPEIsLiveFiltering") = Nothing
            Session("SCOPEProdId") = Nothing
            Session("SCOPEProdDesc") = Nothing
            Session("SCOPEProdUPC") = Nothing
            Session("SCOPEDept") = Nothing
            Session("SCOPEDeptTo") = Nothing
            Session("SCOPECatg") = Nothing
            Session("SCOPECatgTo") = Nothing
            Session("SCOPEUserStore") = Nothing
            Session("SCOPEPageIndex") = Nothing
            Session("SCOPEStatus") = Nothing
            Session("SCOPEBlForPro") = Nothing
            Session("SCOPEHGHV") = Nothing
            Session("SCOPEWithShelfCapacity") = Nothing
            Session("SCOPEDiv") = Nothing


            ViewState("PageIndex") = "1"
            ViewState("ProdId") = ""
            ViewState("ProdDesc") = ""
            ViewState("ProdUPC") = ""
            ViewState("Dept") = ""
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ViewState("DepTo") = ""
            ViewState("Catg") = ""
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ViewState("CatgTo") = ""
            ViewState("Status") = ""
            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            ViewState("WithShelfCapacity") = ""

            dgProduct.DataSource = GetDataSet().Tables(0)
            dgProduct.DataBind()
            txtProductCode.Focus()
            Dim ar As ArrayList = Utility.UserDepartments()
            Dim str As String = ""
            Dim strComma As String = ","
            Dim intCount As Int32
            For intCount = 0 To ar.Count - 1
                If (intCount = 0) Then
                    str = str + ar.Item(intCount)
                Else
                    str = str + strComma + ar.Item(intCount)
                End If
            Next
            'Commented By Amit Rawat for Category Enhancement Request Starts
            'Utility.DropDownDataBind(ddlCatg, Constants.Category, "-1")
            'Commented By Amit Rawat for Category Enhancement Request Ends

            Utility.DropDownDataBind(ddlCatg, STR_CAT, str)

            ddlPages.Items.Clear()
            dgProduct.PagerStyle.Visible = False

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Me.ViewState.Remove("IsLiveFiltering")
            ViewState("SCOPEProdId") = ""
            ViewState("SCOPEProdDesc") = ""
            ViewState("SCOPEProdUPC") = ""
            ViewState("SCOPEDept") = ""
            ViewState("SCOPEDepTo") = ""
            ViewState("SCOPECatg") = ""
            ViewState("SCOPECatgTo") = ""
            ViewState("SCOPEUserStore") = ""
            ViewState("SCOPEPageIndex") = ""
            ViewState("SCOPEStatus") = ""
            ViewState("SCOPEHGHV") = ""
            ViewState("SCOPEBlForPro") = ""
            ViewState("SCOPEWithShelfCapacity") = ""
            ViewState("Div") = ""
            ViewState("SCOPEDiv") = ""
            ViewState("FirstTime") = False
            ViewState("HyperlinkClick") = False

            LoadDropDownLists()
            '-----------------------------------------------------------------------------------------------------------------------

        Catch ex As Exception
            Dim objEx As New Exception("Error in reseting Controls", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        End Try

    End Sub
#End Region

#Region "Populating DropDown"
    ''' <summary>
    ''' Populating Category
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDept.SelectedIndexChanged
        Try
            Dim strDept As String
            strDept = ddlDept.SelectedValue()

            'Commented By: Farnia Emami, Commented at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            'Added By Amit Rawat for Category Enhancement request Starts 
            'If strDept = "" Then
            '    Dim ar As ArrayList = Utility.UserDepartments()
            '    Dim str As String = ""
            '    Dim strComma As String = ","
            '    Dim intCount As Int32
            '    For intCount = 0 To ar.Count - 1
            '        If (intCount = 0) Then
            '            str = str + ar.Item(intCount)
            '        Else
            '            str = str + strComma + ar.Item(intCount)
            '        End If
            '    Next
            '    strDept = str
            'End If
            'Added By Amit Rawat for Category Enhancement request Ends
            'Utility.DropDownDataBind(ddlCatg, STR_CAT, strDept)


            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            If strDept = "" Then
                LoadDropDownLists()
            End If

            Dim sDepartment As String = "",
                sCategory As String = ""
            Dim dtTable As DataTable = Nothing
            Dim sDeptRange As String = String.Empty

            If Me.ddlDept.SelectedValue Is Nothing = False Then
                Utility.DropDownDataBind(Me.ddlDeptTo, Constants.DeptCode)

                If ddlDept.SelectedValue.Trim.Length > 0 Then
                    dtTable = ddlDeptTo.DataSource
                    dtTable.DefaultView.RowFilter = " value >= '" & ddlDept.SelectedValue & "'"
                    ddlDeptTo.DataSource = dtTable
                    ddlDeptTo.DataBind()
                End If

                sDeptRange = ddlDept.SelectedValue & "|" & ddlDept.SelectedValue

                Utility.DropDownDataBind(Me.ddlCatg, Constants.CategoryRange, sDeptRange)
                Utility.DropDownDataBind(Me.ddlcatgTo, Constants.CategoryRange, sDeptRange)

            End If

            If (Not ViewState("IsLiveFiltering") Is Nothing) Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in reassigning Data to dropdown", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Private Sub LoadDropDownLists()
        Try
            Dim ar As ArrayList = Utility.UserDepartments()
            Dim str As String = ""
            Dim strComma As String = ","
            Dim intCount As Int32
            For intCount = 0 To ar.Count - 1
                If (intCount = 0) Then
                    str = str + ar.Item(intCount)
                Else
                    str = str + strComma + ar.Item(intCount)
                End If
            Next

            Utility.DropDownDataBind(ddlDept, Constants.DeptCode)
            ddlDept.Attributes.Add("onkeypress", "return keySort(this);")
            Utility.DropDownDataBind(ddlDeptTo, Constants.DeptCode)
            ddlDeptTo.Attributes.Add("onkeypress", "return keySort(this);")

            Utility.DropDownDataBind(ddlCatg, Constants.Category)
            ddlCatg.Attributes.Add("onkeypress", "return keySort(this);")
            Utility.DropDownDataBind(ddlCatgTo, Constants.Category)
            ddlCatgTo.Attributes.Add("onkeypress", "return keySort(this);")

            If (ddlStatus.SelectedValue = "") Then
                Utility.DropDownDataBind(ddlStatus, Constants.VendorStatus)
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in LoadDropDownLists function", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub ddlDeptTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptTo.SelectedIndexChanged
        Try
            Dim strDept As String
            Dim sDeptRange As String = String.Empty
            strDept = ddlDept.SelectedValue()

            If strDept = "" Then
                LoadDropDownLists()
            End If
            If Not ddlDept.SelectedValue Is Nothing Then
                If ddlDept.SelectedValue = "" Then
                    ddlDept.SelectedValue = ddlDeptTo.SelectedValue
                End If
                sDeptRange = ddlDept.SelectedValue & "|" & ddlDept.SelectedValue
                If ddlDeptTo.SelectedValue Is Nothing = False Then
                    If ddlDeptTo.SelectedValue.Trim.Length > 0 Then
                        sDeptRange = ddlDept.SelectedValue & "|" & ddlDeptTo.SelectedValue
                    End If
                End If

                Utility.DropDownDataBind(Me.ddlCatg, Constants.CategoryRange, sDeptRange)
                Utility.DropDownDataBind(Me.ddlcatgTo, Constants.CategoryRange, sDeptRange)

            End If

            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in ddlDeptTo_SelectedIndexChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub ddlCatg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatg.SelectedIndexChanged
        Try
            Dim sCatRange As String = String.Empty
            Dim bFilter As Boolean = True
            Dim dtTable As DataTable = Nothing

            If Me.ddlCatg.SelectedValue Is Nothing = False Then
                Utility.DropDownDataBind(Me.ddlcatgTo, Constants.Category)

                If ddlCatg.SelectedValue.Trim.Length > 0 Then
                    dtTable = ddlcatgTo.DataSource
                    dtTable.DefaultView.RowFilter = " value >= '" & ddlCatg.SelectedValue & "'"
                    ddlcatgTo.DataSource = dtTable
                    ddlcatgTo.DataBind()
                End If
                sCatRange = ddlCatg.SelectedValue & "|" & ddlcatgTo.SelectedValue
            End If

            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in ddlCatg_SelectedIndexChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub ddlcatgTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcatgTo.SelectedIndexChanged
        Try
            Dim sCatRange As String = String.Empty

            If Not ddlCatg.SelectedValue Is Nothing Then
                If ddlCatg.SelectedValue = "" Then
                    ddlCatg.SelectedValue = ddlcatgTo.SelectedValue
                End If
                sCatRange = ddlCatg.SelectedValue & "|" & ddlCatg.SelectedValue
                If ddlcatgTo.SelectedValue Is Nothing = False Then
                    If ddlcatgTo.SelectedValue.Trim.Length > 0 Then
                        sCatRange = ddlCatg.SelectedValue & "|" & ddlcatgTo.SelectedValue
                    End If
                End If
            End If

            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in ddlCatgTo_SelectedIndexChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "Page index change"


    ''' <summary>
    ''' Page index change event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dgProduct_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProduct.PageIndexChanged
        Try
            dgProduct.CurrentPageIndex = e.NewPageIndex()
            dgProduct.DataSource = dsProd.Tables(1)
            dgProduct.DataBind()
            Session("PES_PageNo") = e.NewPageIndex()
        Catch ex As Exception
            Dim objEx As New Exception("Error in Page index change event", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "For implementing Paging and fetching records"
    ''' <summary>
    ''' Used to populate grid and implement paging
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FetchData()
        Dim objData As DataAccess = New DataAccess()
        Dim dsProd As DataSet = New DataSet()
        Dim objXml As XmlDocument
        Try
            If (ddlPages.Items.Count <> 0) Then
                ddlPages.Items.Clear()
            End If
            Dim ar As ArrayList = Utility.UserDepartments()
            Dim str As String = ""

            'Commented By: Farnia Emami, Commented at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            'objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String), STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_CAT_SP, CType(ViewState("Catg"), String), STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String))  '"1015") ' temp Commented 

            'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            If (ViewState("IsLiveFiltering") = 0) Then
                objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String) _
                                                , STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_DEPTTo_SP, CType(ViewState("DepTo"), String) _
                                                , STR_CAT_SP, CType(ViewState("Catg"), String), STR_CATTo_SP, CType(ViewState("CatgTo"), String),
                                                STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String),
                                                "HGHV", CType(ViewState("HGHV"), String), "BlForPro", CType(ViewState("BlForPro"), String), "Mode", CType(ViewState("WithShelfCapacity"), String),
                                                "Div", CType(ViewState("Div"), String), "LiveFProduct_Code", "", "LiveFProd_Desc", "",
                                                "LiveFUPC", "", "LiveFDept", "", "LiveFDeptTo", "",
                                                "LiveFCatg", "", "LiveFCatgTo", "",
                                                "LiveFStore_Code", Utility.UserStoreId(), "LiveFPage_Index", "", "LiveFStatus", "",
                                                "LiveFHGHV", "", "LiveFBlForPro", "", "LiveFMode", "", "LiveFDiv", "",
                                                "IsLiveFiltering", ViewState("IsLiveFiltering"))
            ElseIf (ViewState("IsLiveFiltering") = 1) Then
                objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(Session("SCOPEProdId"), String), STR_PRDT_DESC, CType(Session("SCOPEProdDesc"), String),
                                                                                STR_UPC, CType(Session("SCOPEProdUPC"), String), STR_DEPT_SP, CType(Session("SCOPEDept"), String), STR_DEPTTo_SP, CType(Session("SCOPEDeptTo"), String),
                                                                                STR_CAT_SP, CType(Session("SCOPECatg"), String), STR_CATTo_SP, CType(Session("SCOPECatgTo"), String),
                                                                                STR_STORE_SP, Session("SCOPEUserStore"), "Page_Index", CType(Session("SCOPEPageIndex"), String), "Status", CType(Session("SCOPEStatus"), String),
                                                                                "HGHV", CType(Session("SCOPEHGHV"), String), "BlForPro", CType(Session("SCOPEBlForPro"), String), "Mode", CType(Session("SCOPEWithShelfCapacity"), String),
                                                                                "Div", CType(Session("SCOPEDiv"), String),
                                                                                "LiveFProduct_Code", CType(ViewState("ProdId"), String), "LiveFProd_Desc", CType(ViewState("ProdDesc"), String),
                                                                                "LiveFUPC", CType(ViewState("ProdUPC"), String), "LiveFDept", CType(ViewState("Dept"), String), "LiveFDeptTo", CType(ViewState("DepTo"), String),
                                                                                "LiveFCatg", CType(ViewState("Catg"), String), "LiveFCatgTo", CType(ViewState("CatgTo"), String),
                                                                                "LiveFStore_Code", Utility.UserStoreId(), "LiveFPage_Index", CType(ViewState("PageIndex"), String), "LiveFStatus", CType(ViewState("Status"), String),
                                                                                "LiveFHGHV", CType(ViewState("HGHV"), String), "LiveFBlForPro", CType(ViewState("BlForPro"), String), "LiveFMode", CType(ViewState("WithShelfCapacity"), String),
                                                                                "LiveFDiv", CType(ViewState("Div"), String),
                                                                                "IsLiveFiltering", ViewState("IsLiveFiltering"))

                'objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("SCOPEProdId"), String), STR_PRDT_DESC, CType(ViewState("SCOPEProdDesc"), String), _
                '                                                STR_UPC, CType(ViewState("SCOPEProdUPC"), String), STR_DEPT_SP, CType(ViewState("SCOPEDept"), String), STR_DEPTTo_SP, CType(ViewState("SCOPEDepTo"), String), _
                '                                                STR_CAT_SP, CType(ViewState("SCOPECatg"), String), STR_CATTo_SP, CType(ViewState("SCOPECatgTo"), String), _
                '                                                STR_STORE_SP, ViewState("SCOPEUserStore"), "Page_Index", CType(ViewState("SCOPEPageIndex"), String), "Status", CType(ViewState("SCOPEStatus"), String), _
                '                                                "HGHV", CType(ViewState("SCOPEHGHV"), String), "BlForPro", CType(ViewState("SCOPEBlForPro"), String), "Mode", CType(ViewState("SCOPEWithShelfCapacity"), String), _
                '                                                "Div", CType(ViewState("SCOPEDiv"), String), _
                '                                                "LiveFProduct_Code", CType(ViewState("ProdId"), String), "LiveFProd_Desc", CType(ViewState("ProdDesc"), String), _
                '                                                "LiveFUPC", CType(ViewState("ProdUPC"), String), "LiveFDept", CType(ViewState("Dept"), String), "LiveFDeptTo", CType(ViewState("DepTo"), String), _
                '                                                "LiveFCatg", CType(ViewState("Catg"), String), "LiveFCatgTo", CType(ViewState("CatgTo"), String), _
                '                                                "LiveFStore_Code", Utility.UserStoreId(), "LiveFPage_Index", CType(ViewState("PageIndex"), String), "LiveFStatus", CType(ViewState("Status"), String), _
                '                                                "LiveFHGHV", CType(ViewState("HGHV"), String), "LiveFBlForPro", CType(ViewState("BlForPro"), String), "LiveFMode", CType(ViewState("WithShelfCapacity"), String), _
                '                                                "LiveFDiv", CType(ViewState("Div"), String), _
                '                                                "IsLiveFiltering", ViewState("IsLiveFiltering"))
            End If

            Dim intCount As Int32
            For intCount = 0 To ar.Count - 1
                DataAccess.BuildXmlParam(objXml, 2, "DepartmentNo", ar.Item(intCount).ToString().Trim())
            Next
            dsProd = objData.ExecuteSpDataSet("usp_ProductEnquirySearch", objXml)
            If (objData.DbMessage.Count <> 0) Then
                If (objData.DbMessage.Code(0) = "10001") Then
                    lblError.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblError.Text = objData.DbMessage.Message(0)
                    dgProduct.DataSource = GetDataSet().Tables(0)
                    dgProduct.DataBind()
                    dgProduct.PagerStyle.Visible = False
                    Return
                Else
                    Dim strError As String = objData.DbMessage.Code(0)
                    Server.Transfer("~/ErrorPage.aspx?Code=" & strError)
                    Return
                End If
            End If
            Dim i As Integer

            If (Not dsProd Is Nothing) Then
                Session("ProductEnqTable") = dsProd
                dgProduct.DataSource = dsProd.Tables(1)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = True
                lblError.Text = ""
                lblError.CssClass = ""
                For i = 0 To CType(dsProd.Tables(0).Rows(0).Item(0), Integer) - 1
                    ddlPages.Items.Add(New ListItem(i + 1, i + 1))
                Next
                ddlPages.SelectedIndex = CType(ViewState("PageIndex"), Int32) - 1
            End If
        Catch httpex As Threading.ThreadAbortException

        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
        Finally
            objData = Nothing
            dsProd = Nothing
            objXml = Nothing
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub txtProductCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductCode.TextChanged
        Try
            If (Not ViewState("IsLiveFiltering") Is Nothing) Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in product code textbox TextChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub txtPrdtDesc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrdtDesc.TextChanged
        Try
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in product description TextChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub txtUPC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUPC.TextChanged
        Try
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in txtUPC_TextChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in ddlStatus_SelectedIndexChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub cbBlockOfPro_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBlockOfPro.CheckedChanged
        Try
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbBlockOfPro_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
    Protected Sub cbBlockOfPro_NO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBlockOfPro_NO.CheckedChanged
        Try
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbBlockOfPro_NO_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
#End Region

#Region "Build Dataset"
    ''' <summary>
    ''' Returns Empty Dataset for displaying datagrid
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataSet() As DataSet
        Try
            Dim dsEmpty As DataSet = New DataSet()
            Dim dtEmpty As DataTable = New DataTable()
            Dim dcEmpty As DataColumnCollection = dtEmpty.Columns
            dcEmpty.Add("Product_Code", "".GetType)
            dcEmpty.Add("Long_Description", "".GetType)
            dsEmpty.Tables.Add(dtEmpty)
            Return dsEmpty
        Catch ex As Exception
            Dim objEx As New Exception("Error in reseting Controls", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            Throw objEx
            Return Nothing
        End Try

    End Function

#End Region

#Region "Combo for Pages"
    ''' <summary>
    ''' Selected Index Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlPages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPages.SelectedIndexChanged
        ViewState("PageIndex") = ddlPages.SelectedItem.ToString
        dgProduct.CurrentPageIndex = 0
        'Added By: Farnia Emami, Added at: 11 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
        ViewState("IsLiveFiltering") = 0
        Call FetchData()
        Session("PES_PageSet") = ViewState("PageIndex")
    End Sub
#End Region

End Class