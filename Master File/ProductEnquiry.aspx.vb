#Region "Page Header"
''' <summary>
''' System		: POM.Net
''' Screen Name	: Product Enquiry
''' Version		: 1.0
''' </summary>
''' <remarks>
''' Author		: Amit Rawat
''' Created On	: 27 July 2006
''' Modified By	: 
''' Modified On	:
''' Revision History:
''' </remarks>
#End Region

#Region "Imports"
Imports POM.Lib.Data
Imports POM.Lib.UI
Imports POM.Lib.Security
Imports System.Xml
Imports POM.Lib.Log
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports WebServiceClient
#End Region

Partial Class ProductEnquiry

    Inherits System.Web.UI.Page
    Protected strPrdtId As String
    Dim dsProd As New DataSet
    'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Dim strValueMode As String
    '------------------------------------------------------------------------------
    Dim strValue As String

#Region "Private Constant"
    Private Const STR_DEPT As String = "DEPT"
    ' Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Const STR_DEPTTo As String = "DEPTTo"
    ' ------------------------------------------------------------------------------
    Private Const STR_CAT As String = "CAT"
    ' Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Const STR_CATTo As String = "CATTo"
    ' ------------------------------------------------------------------------------
    Private Const STR_PRDT_CODE As String = "Product_Code"
    Private Const STR_PRDT_DESC As String = "Prod_Desc"
    Private Const STR_UPC As String = "UPC"
    Private Const STR_DEPT_SP As String = "Dept"
    ' Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Const STR_DEPTTo_SP As String = "DeptTo"
    ' ------------------------------------------------------------------------------
    Private Const STR_CAT_SP As String = "Catg"
    ' Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Const STR_CATTo_SP As String = "CatgTo"
    ' ------------------------------------------------------------------------------
    Private Const STR_STORE_SP As String = "Store_Code"
    ' Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Const STR_NewPSC_SP As String = "NewProductShelfCapacity"
    Private Const STR_User_SP As String = "UserId"
    ' ------------------------------------------------------------------------------
    Private Const STR_SP_NAME As String = "usp_ProductEnquirySearch"
    Private Const STR_STORE_QS As String = "storeid"
    Private Const STR_BLANK As String = ""
    Private Const INT_UNHIDE As Int32 = 1
    Private Const INT_HIDE As Int32 = 0
    Private Const STR_MASTER As String = "0"
    Private Const STR_HIGHSHRINK As String = "1"
    Private Const STR_SELFCAPACITY As String = "2"
    '---------------------------------------------------------------------------------
    'Saber, 10/05/2016, Display Front Facing
    Private Const STR_Front_Facing As String = "Front_Facing"

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

            'Dim strValue As String = Convert.ToString(Request.QueryString("ScrId"))

            strValue = Convert.ToString(Request.QueryString("ScrId")) 'declare strValue as global variable. need to use the same variable at btnSearch
            Dim strScreenName As String = String.Empty
            If (strValue Is Nothing) Then
                strValue = ""
            End If
            If (strValue = STR_MASTER) Then
                strPrdtId = "ProductHierarchy.aspx"
                strScreenName = "Product Hierarchy"
            ElseIf (strValue = STR_HIGHSHRINK) Then
                strPrdtId = "~/Store Attribute/HighShrinkHighValue.aspx"
                strScreenName = "High Shrink High Value"
            ElseIf (strValue = STR_SELFCAPACITY) Then
                strPrdtId = "~/Store Attribute/ShelfCapacity.aspx"
                strScreenName = "Shelf Capacity"
                lblTitle.Text = "Shelf Capacity Enquiry"
            Else
                strPrdtId = "ProductHierarchy.aspx"
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

            'added by faizal 2/7/2013 - show HVSv row based on strValue


            If (Not IsPostBack) Then


                If strValue <> 1 Then
                    'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    If (strValue = 0) Then
                        Me.dgProduct.Columns(1).Visible = False
                        Me.dgProduct.Columns(3).Visible = False
                        Me.dgProduct.Columns(4).Visible = False
                        Me.dgProduct.Columns(5).Visible = False
                        Me.dgProduct.Columns(6).Visible = False
                        Me.dgProduct.Columns(7).Visible = False
                        Me.btnSave.Visible = False
                        HSHVLine.Visible = True
                        'Add By Farnia @ 27 Aug 2014
                        'Me.TR1.Attributes("Class") = "d1"
                        'Me.TR2.Attributes("Class") = "d0"
                        'Me.TR3.Attributes("Class") = "d1"
                        'Me.TR4.Attributes("Class") = "d0"
                        'Me.WShelfCapacityLine.Attributes("Class") = "d1"
                        Me.HSHVLine.Attributes("Class") = "d1"
                        Me.TDTest1.Visible = True
                        Me.TDTest2.Visible = False
                        'Me.TDTest1.Attributes("Class") = "d0"
                        'Me.TDTest2.Attributes("Class") = "d0"
                        'Me.trButton.Attributes("Class") = "d0"
                        Me.cbHGHV.Checked = True
                        Me.cbBlockOfPro.Checked = True
                        'Saber, Checkbox for Front Facing 11/05/2016
                        Me.cbFFI.Visible = False
                        Me.cbFFI_NO.Visible = False
                        '-------------------------------------------

                        btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntry2(" &
                                    "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " &
                                    "'" & txtProductCode.ClientID & "', " &
                                    "'" & txtPrdtDesc.ClientID & "', " &
                                    "'" & txtUPC.ClientID & "', " &
                                    "'" & ddlDept.ClientID & "', " &
                                    "'" & ddlDeptTo.ClientID & "', " &
                                    "'" & ddlCatg.ClientID & "', " &
                                    "'" & ddlCatgTo.ClientID & "', " &
                                    "'" & cbBlockOfPro.ClientID & "', " &
                                    "'" & cbBlockOfPro_NO.ClientID & "', " &
                                    "'" & ddlStatus.ClientID & "');")

                        '--------------------------------------------
                    ElseIf (strValue = 2) Then

                        Me.dgProduct.Columns(0).Visible = False
                        Me.dgProduct.Columns(3).Visible = False
                        Me.dgProduct.Columns(4).Visible = False
                        Me.dgProduct.Columns(5).Visible = False
                        HSHVLine.Visible = True
                        'Add By Farnia @ 27 Aug 2014
                        Me.TDTest1.Visible = True
                        Me.TDTest2.Visible = False
                        '-----------------------------
                        'Saber, Checkbox for Front Facing 11/05/2016
                        Me.cbFFI.Visible = True
                        Me.cbFFI_NO.Visible = True
                        '-------------------------------------------
                        'Add By Farnia @ 20 Aug July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        Me.WShelfCapacityLine.Visible = True
                        WShelfCapacityLine.Attributes("Class") = "d0"
                        'trButton.Attributes("Class") = "d0"
                        ViewState("WithShelfCapacity") = Convert.ToString(Request.QueryString("Mode"))
                        ViewState("Div") = Convert.ToString(Request.QueryString("Div"))
                        Me.cbHGHV.Checked = True
                        Me.cbBlockOfPro.Checked = True
                        If (ViewState("WithShelfCapacity") = "2") Then
                            Me.btnSave.Visible = True
                            Me.ddlWithShelfCapacity.SelectedValue = "2"
                            cbBlockOfPro.Checked = False
                            cbBlockOfPro_NO.Checked = True
                            ViewState("HyperlinkClick") = True
                            ViewState("FirstTime") = True
                            Dim objData As DataAccess = New DataAccess()
                            Dim dsDep As DataSet = New DataSet()
                            Dim objXml As XmlDocument

                            objXml = DataAccess.BuildXmlParam("DivisionCode", ViewState("Div"))
                            dsDep = objData.ExecuteSpDataSet("usp_AutoSelectDepartmentFromDiv", objXml)

                            ddlDept.SelectedValue = dsDep.Tables(0).Rows(0).Item(0)
                            ddlDeptTo.SelectedValue = dsDep.Tables(0).Rows(0).Item(1)

                        Else
                            ViewState("WithShelfCapacity") = "0"
                            '----------------------------------------------------------------
                            'ADD BY  : FARNIA
                            'ADD AT  : 21 January 2015
                            'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
                            '----------------------------------------------------------------
                            btnUpload.Visible = True
                            btnWin7Upload.Visible = True

                            EnabledButton(True)

                            If (Not Access.ScreenAccess(Constants.PRODENQ)) Then
                                Server.Transfer("~/ErrorPage.aspx?Code=10019&ScrId=" & strScreenName & "")
                                Return
                            End If

                            ViewState("FileSeqNo") = generateSeqNo()
                            Me.btnUpload.OnClientClick = "javascript:if(confirm(""Are You sure you want to upload ?"")){return (" & Utility.InvokeClientExe(Page, "",
                                                       New String() {ConfigurationManager.AppSettings("Upload.exe"),
                                                       "", "SC",
                                                       ConfigurationManager.AppSettings("FTPServer"),
                                                       ConfigurationManager.AppSettings("FTPUser"),
                                                       ConfigurationManager.AppSettings("FTPPassword"),
                                                       ConfigurationManager.AppSettings("ClientDest_SC").Replace("~", Utility.UserStoreId),
                                                       ConfigurationManager.AppSettings("ServerSource_SC").Replace("~", Utility.UserStoreId),
                                                       Utility.UserStoreId,
                                                       ViewState("FileSeqNo").ToString}) & " ==0);} else return false;"

                            Me.btnWin7Upload.OnClientClick = "javascript:if(confirm(""Are You sure you want to upload ?"")){return (" & Utility.InvokeClientExe(Page, "",
                                                       New String() {ConfigurationManager.AppSettings("Upload.exe"),
                                                       "", "SCWIN7",
                                                       ConfigurationManager.AppSettings("FTPServer"),
                                                       ConfigurationManager.AppSettings("FTPUser"),
                                                       ConfigurationManager.AppSettings("FTPPassword"),
                                                       ConfigurationManager.AppSettings("ClientDest_SC").Replace("~", Utility.UserStoreId),
                                                       ConfigurationManager.AppSettings("ServerSource_SC").Replace("~", Utility.UserStoreId),
                                                       Utility.UserStoreId,
                                                       ViewState("FileSeqNo").ToString}) & " ==0);} else return false;"




                            '----------------------------------------------------------------
                            'END
                            '----------------------------------------------------------------
                        End If


                        btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntry3(" &
                                    "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " &
                                    "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " &
                                    "'" & txtProductCode.ClientID & "', " &
                                    "'" & txtPrdtDesc.ClientID & "', " &
                                    "'" & txtUPC.ClientID & "', " &
                                    "'" & ddlDept.ClientID & "', " &
                                    "'" & ddlDeptTo.ClientID & "', " &
                                    "'" & ddlCatg.ClientID & "', " &
                                    "'" & ddlCatgTo.ClientID & "', " &
                                    "'" & cbBlockOfPro.ClientID & "', " &
                                    "'" & cbBlockOfPro_NO.ClientID & "', " &
                                    "'" & ddlWithShelfCapacity.ClientID & "', " &
                                    "'" & cbFFI.ClientID & "'," &
                                    "'" & cbFFI_NO.ClientID & "', " &
                                    "'" & ddlStatus.ClientID & "');")

                        '-----------------------------------------------------------------------------------
                        ViewState("PageIndex") = 1
                        Session("PageIndex") = ViewState("PageIndex")

                        'Commented By Farnia @ 
                        'Add By Farnia @ 29 Aug July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        'If (ViewState("WithShelfCapacity") = "2") Then
                        '    ViewState("IsLiveFiltering") = 1
                        '    ViewState("SCOPEProdId") = ""
                        '    ViewState("SCOPEProdDesc") = ""
                        '    ViewState("SCOPEProdUPC") = ""
                        '    ViewState("SCOPEDept") = ""
                        '    ViewState("SCOPEDepTo") = ""
                        '    ViewState("SCOPECatg") = ""
                        '    ViewState("SCOPECatgTo") = ""
                        '    ViewState("SCOPEUserStore") = Utility.UserStoreId()
                        '    ViewState("SCOPEPageIndex") = 1
                        '    ViewState("SCOPEStatus") = ""
                        '    ViewState("SCOPEHGHV") = ""
                        '    ViewState("SCOPEBlForPro") = "F"
                        '    ViewState("SCOPEWithShelfCapacity") = ViewState("WithShelfCapacity")
                        '    ViewState("SCOPEDiv") = ViewState("Div")
                        'End If
                        ' ----------------------------------------------------------------------------------
                    End If
                    '-------------------------------------------------------------------------------
                    'HSHVLine.Visible = false
                    'add by farnia 
                    'Me.TDTest1.Visible = True
                    'Me.TDTest2.Visible = True
                    'Me.TDTest3.Visible = False
                    'Me.TDTest4.Visible = False
                    '-------------------


                    ' modified by Nicholas
                    ' Modified on 6/08/2013
                    ' Commented By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    ' Me.dgProduct.Columns(2).Visible = False
                    ' Me.dgProduct.Columns(4).Visible = False
                    '-------------------------------------------------------------------------------------
                    'Me.cbHGHV.Checked = True
                    'Me.cbBlockOfPro.Checked = True

                    'Comment By Farnia @ 2 Sept 2014 For DCL 5137 - Show shelf capacity on shelf label
                    'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    'btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntry2(" & _
                    '                "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text) & "', " & _
                    '                "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " & _
                    '                "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " & _
                    '                "'" & txtProductCode.ClientID & "', " & _
                    '                "'" & txtPrdtDesc.ClientID & "', " & _
                    '                "'" & txtUPC.ClientID & "', " & _
                    '                "'" & ddlDept.ClientID & "', " & _
                    '                "'" & ddlDeptTo.ClientID & "', " & _
                    '                "'" & ddlCatg.ClientID & "', " & _
                    '                "'" & ddlCatgTo.ClientID & "', " & _
                    '                "'" & ddlStatus.ClientID & "');")
                    ' -----------------------------------------------------------------------------------

                    'Commented By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    'btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntry(" & _
                    '                "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text) & "', " & _
                    '                "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " & _
                    '                "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " & _
                    '                "'" & txtProductCode.ClientID & "', " & _
                    '                "'" & txtPrdtDesc.ClientID & "', " & _
                    '                "'" & txtUPC.ClientID & "', " & _
                    '                "'" & ddlDept.ClientID & "', " & _
                    '                "'" & ddlCatg.ClientID & "', " & _
                    '                "'" & ddlStatus.ClientID & "');")

                Else
                    'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    Me.dgProduct.Columns(1).Visible = False
                    Me.dgProduct.Columns(4).Visible = False
                    Me.dgProduct.Columns(5).Visible = False
                    Me.dgProduct.Columns(6).Visible = False
                    Me.dgProduct.Columns(7).Visible = False
                    Me.HSHVLine.Visible = True
                    Me.WShelfCapacityLine.Visible = False
                    Me.btnSave.Visible = False
                    HSHVLine.Attributes("Class") = "d0"
                    'trButton.Attributes("Class") = "d1"

                    '-------------------------------------------------------------------------------

                    'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntryCheckBox2(" &
                                                                           "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text, lblHghShrHghVal.Text, lblBlockOfProcurement.Text) & "', " &
                                                                           "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " &
                                                                           "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " &
                                                                           "'" & txtProductCode.ClientID & "', " &
                                                                           "'" & txtPrdtDesc.ClientID & "', " &
                                                                           "'" & txtUPC.ClientID & "', " &
                                                                           "'" & ddlDept.ClientID & "', " &
                                                                           "'" & ddlDeptTo.ClientID & "', " &
                                                                           "'" & ddlCatg.ClientID & "', " &
                                                                           "'" & ddlCatgTo.ClientID & "', " &
                                                                           "'" & ddlStatus.ClientID & "', " &
                                                                           "'" & cbHGHV.ClientID & "', " &
                                                                           "'" & cbHGHV_No.ClientID & "', " &
                                                                           "'" & cbBlockOfPro.ClientID & "', " &
                                                                           "'" & cbBlockOfPro_NO.ClientID & "');")

                    'Commented By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    'btnSearch.Attributes.Add("OnClick", "javascript:return validateProductEnquiryEntryCheckBox(" & _
                    '                                   "'" & Utility.GetMessage("20005", lblProductCode.Text, lblProdDesc.Text, lblBarCode.Text, lblDept.Text, lblstatus.Text, lblHghShrHghVal.Text, lblBlockOfProcurement.Text) & "', " & _
                    '                                   "'" & Utility.GetMessage("20004", lblProductCode.Text) & "', " & _
                    '                                   "'" & Utility.GetMessage("20004", lblBarCode.Text) & "', " & _
                    '                                   "'" & txtProductCode.ClientID & "', " & _
                    '                                   "'" & txtPrdtDesc.ClientID & "', " & _
                    '                                   "'" & txtUPC.ClientID & "', " & _
                    '                                   "'" & ddlDept.ClientID & "', " & _
                    '                                   "'" & ddlCatg.ClientID & "', " & _
                    '                                   "'" & ddlStatus.ClientID & "', " & _
                    '                                   "'" & cbHGHV.ClientID & "', " & _
                    '                                   "'" & cbHGHV_No.ClientID & "', " & _
                    '                                   "'" & cbBlockOfPro.ClientID & "', " & _
                    '                                   "'" & cbBlockOfPro_NO.ClientID & "');")

                End If

                'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                'Dim ar As ArrayList = Utility.UserDepartments()
                'Dim str As String = ""
                'Dim strComma As String = ","
                'Dim intCount As Int32
                'For intCount = 0 To ar.Count - 1
                '    If (intCount = 0) Then
                '        str = str + ar.Item(intCount)
                '    Else
                '        str = str + strComma + ar.Item(intCount)
                '    End If
                'Next
                '---------------------------------------------------------------

                'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                LoadDropDownLists()
                '----------------------------------------------------


                'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
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

                dgProduct.DataSource = GetDataSet().Tables(0)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = False
                txtProductCode.Focus()

                'Commented By Farnia @ 1 Sept 2014 For DCL 5137 - Show shelf capacity on shelf label
                'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                'If (ViewState("WithShelfCapacity") = "2") Then
                '    FetchData()
                'End If
                ' -----------------------------------------------------------------------------------
                ' -----------------------------------------------------------------------------------

                'Add By Farnia @ 1 Sept 2014 For DCL 5137 - Show shelf capacity on shelf label
                If (ViewState("HyperlinkClick") = True) Then
                    LiveFiltering()
                End If
                ' ------------------------------------------------------------------------------

                If (Utility.FromGoBack) Then
                    'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    'If Not (IsNothing(Session("ProdId")) Or IsNothing(Session("ProdDesc")) Or IsNothing(Session("ProdUPC")) Or IsNothing(Session("Dept")) Or IsNothing(Session("Catg")) Or IsNothing(Session("Status"))) Then
                    '------------------------------------------------------------------------------------
                    'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                    If Not (IsNothing(Session("ProdId")) Or IsNothing(Session("ProdDesc")) Or IsNothing(Session("ProdUPC")) Or IsNothing(Session("Dept")) Or IsNothing(Session("DeptTo")) Or IsNothing(Session("Catg")) Or IsNothing(Session("CatgTo")) Or IsNothing(Session("Status"))) Then
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
                        'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        If Not IsNothing(Session("DeptTo")) Then
                            Utility.SetDropDownSelectedValue(ddlDeptTo, CType(Session("DeptTo"), String))
                        End If
                        ddlDeptTo_SelectedIndexChanged1(Nothing, Nothing)
                        '------------------------------------------------------------------------------
                        If Not IsNothing(Session("Catg")) Then
                            Utility.SetDropDownSelectedValue(ddlCatg, CType(Session("Catg"), String))
                        End If
                        'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        If Not IsNothing(Session("CatgTo")) Then
                            Utility.SetDropDownSelectedValue(ddlCatgTo, CType(Session("CatgTo"), String))
                        End If
                        '-----------------------------------------------------------------------------
                        '*****************************************************************

                        '****************Added By Rashi Goyal Dated: 3rd Nov. '06*********
                        If Not IsNothing(Session("Status")) Then
                            Utility.SetDropDownSelectedValue(ddlStatus, CType(Session("Status"), String))
                        End If
                        '**********************END****************************************

                        'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
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

                        'Saber, 11/05/2016, For Front Facing
                        If Not IsNothing(Session("FFI")) Then
                            If (Session("FFI") = "") Then
                                Me.cbFFI.Checked = True
                                Me.cbFFI_NO.Checked = True
                            ElseIf (Session("FFI") = "T") Then
                                Me.cbFFI.Checked = True
                                Me.cbFFI_NO.Checked = False
                            ElseIf (Session("FFI") = "F") Then
                                Me.cbFFI.Checked = False
                                Me.cbFFI_NO.Checked = True
                            ElseIf (Session("FFI") = "X") Then
                                Me.cbFFI.Checked = False
                                Me.cbFFI_NO.Checked = False
                            End If
                        End If
                        '---------------------------------------------

                        'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        If Not IsNothing(Session("HGHV")) Then
                            If (Session("HGHV") = "") Then
                                Me.cbHGHV.Checked = True
                                Me.cbHGHV_No.Checked = True
                            ElseIf (Session("HGHV") = "1") Then
                                Me.cbHGHV.Checked = True
                                Me.cbHGHV_No.Checked = False
                            ElseIf (Session("HGHV") = "0") Then
                                Me.cbHGHV.Checked = False
                                Me.cbHGHV_No.Checked = True
                            ElseIf (Session("HGHV") = "2") Then
                                Me.cbHGHV.Checked = False
                                Me.cbHGHV_No.Checked = False
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
                        'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        ViewState("DepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
                        '---------------------------------------------------------------
                        ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
                        'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                        ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
                        '---------------------------------------------------------------------
                        ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()

                        'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
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

                        'Saber, 11/05/2016, For Front Facing Filtering
                        Dim FFI As String = ""
                        If Me.cbFFI.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                            FFI = ""
                        ElseIf Me.cbFFI.Checked = True Then
                            FFI = "T"
                        ElseIf Me.cbFFI_NO.Checked = True Then
                            FFI = "F"
                        ElseIf (Me.cbFFI.Checked = False And Me.cbFFI_NO.Checked = False) Then
                            FFI = "X"
                        End If
                        ViewState("FFI") = FFI
                        '--------------------------------------------------

                        'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
                        Dim HGHValue As String = ""

                        If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                            HGHValue = ""
                        ElseIf Me.cbHGHV.Checked = True Then
                            HGHValue = "1"
                        ElseIf Me.cbHGHV_No.Checked = True Then
                            HGHValue = "0"
                        ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                            HGHValue = "2"
                        End If
                        ViewState("HGHV") = HGHValue

                        'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
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

            '----------------------------------------------------------------
            'ADD BY  : FARNIA
            'ADD AT  : 21 January 2015
            'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
            '----------------------------------------------------------------
            If (ViewState("WithShelfCapacity") = "0") Then
                CheckWindowCompatible()
            End If
            '----------------------------------------------------------------
            'END
            '----------------------------------------------------------------

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
            'Commented By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And txtUPC.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlStatus.SelectedValue = "") Then
            'Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label

            'Commented By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label(Create a function call LiveFitering for the whole commented part)
            'If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And txtUPC.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And ddlStatus.SelectedValue = "") Then
            '    If strValue <> 1 Then
            '        lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
            '        lblError.Text = ""
            '        dgProduct.DataSource = GetDataSet().Tables(0)
            '        dgProduct.DataBind()
            '        dgProduct.PagerStyle.Visible = False
            '        Return
            '    End If
            'End If

            'checking for DCL4901 - must check at least one checkbox
            If Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                lblHerr.Text = Utility.GetMessage("20001", "At Least One Checkbox for High Shrink / High Value")
                lblError.CssClass = ""
                lblError.Text = ""
                dgProduct.DataSource = GetDataSet().Tables(0)
                dgProduct.DataBind()
                dgProduct.PagerStyle.Visible = False
                Return
            End If

            'checking for DCL4901 - must check at least one checkbox
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

            'dgProduct.CurrentPageIndex = 0
            'dsProd = New DataSet()
            'Session("ProductEnqTable") = Nothing

            'ViewState("PageIndex") = "1"
            'ViewState("ProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            'ViewState("Dept") = ddlDept.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("DepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
            ''------------------------------------------------------------------------------
            'ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
            ''------------------------------------------------------------------------------
            'ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("Mode") = "Search"
            ''------------------------------------------------------------------------------
            ''added by faizal 1/7/2013
            'Dim HGHValue As String = ""
            'Dim BlOfPro As String = ""

            ''If Me.cbHGHV.Checked = True Then
            ''    HGHValue = "True"
            ''End If
            'If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
            '    HGHValue = ""
            'ElseIf Me.cbHGHV.Checked = True Then
            '    HGHValue = "1"
            'ElseIf Me.cbHGHV_No.Checked = True Then
            '    HGHValue = "0"
            'End If

            ''If Me.cbBlockOfPro.Checked = True Then
            ''    BlOfPro = "T"
            ''End If
            'If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
            '    BlOfPro = ""
            'ElseIf Me.cbBlockOfPro.Checked = True Then
            '    BlOfPro = "T"
            'ElseIf Me.cbBlockOfPro_NO.Checked = True Then
            '    BlOfPro = "F"
            'End If

            'ViewState("HGHV") = HGHValue
            'ViewState("BlForPro") = BlOfPro

            'Session("ProdId") = ViewState("ProdId")
            'Session("ProdDesc") = ViewState("ProdDesc")
            'Session("ProdUPC") = ViewState("ProdUPC")
            'Session("Dept") = ViewState("Dept")
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Session("DepTo") = ViewState("DepTo")
            ''-----------------------------------------------------------------------------
            'Session("Catg") = ViewState("Catg")
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Session("CatgTo") = ViewState("CatgTo")
            ''-----------------------------------------------------------------------------
            'Session("Status") = ViewState("Status")
            'Session("HGHV") = ViewState("HGHV")
            'Session("BlForPro") = ViewState("BlForPro")

            'FetchData()
            'lblHerr.Text = ""
            ''new codes for DCL4901 
            'Me.btnPrint.Visible = False
            'Dim strFunc As String() = Access.ScreenFunctions(Constants.PRODENQ)
            'If Not IsNothing(strFunc) Then
            '    If (strFunc.Length > 0) Then
            '        Dim intcounter As Int32
            '        For intcounter = 0 To strFunc.Length - 1
            '            If strFunc(intcounter) = Constants.CONST_F4 Then
            '                If Me.dgProduct.Items.Count > 0 Then
            '                    If strValue = 1 Then
            '                        Me.btnPrint.Visible = True
            '                    End If
            '                End If
            '            End If
            '        Next
            '    End If
            'End If
            '---------------------------------------------------------------------------------------------------------------

            'Add By Farnia @ 28 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Define Search Scope Criteria
            Dim HGHValue As String = ""
            Dim BlOfPro As String = ""

            'Saber, 11/05/2016, For Front Facing Filtering
            Dim FFI As String = ""
            '--------------------------------------------

            If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                HGHValue = ""
            ElseIf Me.cbHGHV.Checked = True Then
                HGHValue = "1"
            ElseIf Me.cbHGHV_No.Checked = True Then
                HGHValue = "0"
            ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                HGHValue = "2"
            End If

            If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = ""
            ElseIf Me.cbBlockOfPro.Checked = True Then
                BlOfPro = "T"
            ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = "F"
            ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                BlOfPro = "X"
            End If

            'Saber, 11/05/2016, For Front Facing Filtering
            If Me.cbFFI.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                FFI = ""
            ElseIf Me.cbFFI.Checked = True Then
                FFI = "T"
            ElseIf Me.cbFFI_NO.Checked = True Then
                FFI = "F"
            ElseIf (Me.cbFFI.Checked = False And Me.cbFFI_NO.Checked = False) Then
                FFI = "X"
            End If
            '--------------------------------------------------

            ViewState("IsLiveFiltering") = 0
            ViewState("SCOPEProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            ViewState("SCOPEDept") = ddlDept.SelectedValue.ToString().Trim()
            ViewState("SCOPEDepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
            ViewState("SCOPECatg") = ddlCatg.SelectedValue.ToString().Trim()
            ViewState("SCOPECatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
            ViewState("SCOPEUserStore") = Utility.UserStoreId()
            ViewState("SCOPEPageIndex") = 1
            ViewState("SCOPEStatus") = ddlStatus.SelectedValue.ToString().Trim()
            ViewState("SCOPEHGHV") = HGHValue
            ViewState("SCOPEBlForPro") = BlOfPro
            ViewState("SCOPEWithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()
            'ViewState("SCOPEDiv") = ViewState("Div")
            ViewState("SCOPEDiv") = ""
            ViewState("Div") = ""
            ViewState("WithShelfCapacity") = ""
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
            'Saber,11/05/2016 For Front Facing Filtering
            ViewState("SCOPEFFI") = FFI
            '------------------------------------------------------------------------------

            'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            LiveFiltering()

            '------------------------------------------------------------------------------


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
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ddlDeptTo.SelectedIndex = 0
            Dim test As String = ddlDeptTo.SelectedItem.Text
            'ddlDeptTo.SelectedIndex = -2
            ddlDeptTo.SelectedItem.Text = "--Select--"
            'ddlDeptTo.SelectedIndex = -1
            '------------------------------------------------------------------------------
            ddlCatg.SelectedIndex = -1
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ddlCatgTo.SelectedIndex = 0
            ddlCatgTo.SelectedItem.Text = "--Select--"
            'ddlCatgTo.SelectedIndex = -1
            '------------------------------------------
            'Add By Farnia @ 21 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            ddlWithShelfCapacity.SelectedIndex = -1
            '------------------------------------------------------------------------------
            ddlStatus.SelectedIndex = -1
            'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            cbHGHV.Checked = True
            cbHGHV_No.Checked = True
            cbBlockOfPro.Checked = True
            cbBlockOfPro_NO.Checked = True
            btnSave.Visible = False
            '------------------------------------------------------------------------------
            lblError.Text = ""
            lblHerr.Text = ""
            lblError.CssClass = ""
            lblHerr.CssClass = ""
            dgProduct.CurrentPageIndex = 0
            Session("ProductEnqTable") = Nothing

            Session("ProdId") = Nothing
            Session("ProdDesc") = Nothing
            Session("ProdUPC") = Nothing
            Session("Dept") = Nothing
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            Session("DeptTo") = Nothing
            '------------------------------------------------------------------------------
            Session("Catg") = Nothing
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            Session("CatgTo") = Nothing
            '------------------------------------------------------------------------------
            Session("Status") = Nothing
            Session("PES_PageSet") = Nothing
            Session("PES_PageNo") = Nothing

            'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
            Session("SCOPEIsLiveFiltering") = Nothing
            Session("SCOPEProdId") = Nothing
            Session("SCOPEProdDesc") = Nothing
            Session("SCOPEProdUPC") = Nothing
            Session("SCOPEDept") = Nothing
            Session("SCOPEDeptTo") = Nothing
            Session("SCOPECatg") = Nothing
            Session("SCOPECatgTo") = Nothing
            Session("SCOPEUserStore") = Nothing
            Session("SCOPEPageIndex") = "1"
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
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            ViewState("DepTo") = ""
            '------------------------------------------------------------------------------
            ViewState("Catg") = ""
            'Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            ViewState("CatgTo") = ""
            '------------------------------------------------------------------------------
            'Add By Farnia @ 21 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            ViewState("WithShelfCapacity") = ""
            '------------------------------------------------------------------------------
            ViewState("Status") = ""
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

            '*************************************************************************
            '**** Modified By   : Foong Kok Loon
            '**** Modified On   : 14 August 2013
            '**** DCL           : 4901 - Set Print Button Invisible After Reset
            '*************************************************************************
            btnPrint.Visible = False
            '*************************************************************************

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("IsLiveFiltering") = 0
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

            ' ----------------------------------------------------------------------------------

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (ViewState("WithShelfCapacity") = "2") Then
            '    FetchData()
            '    btnSave.Visible = True
            'End If
            ' -----------------------------------------------------------------------------------

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

            'Added By Amit Rawat for Category Enhancement request Starts 
            If strDept = "" Then
                'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                LoadDropDownLists()
                'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                'Dim ar As ArrayList = Utility.UserDepartments()
                'Dim str As String = ""
                'Dim strComma As String = ","
                'Dim intCount As Int32
                'For intCount = 0 To ar.Count - 1
                '    If (intCount = 0) Then
                '        str = str + ar.Item(intCount)
                '    Else
                '        str = str + strComma + ar.Item(intCount)
                '    End If
                'Next
                'strDept = str
                '-----------------------------------------
            End If
            'Added By Amit Rawat for Category Enhancement request Ends
            'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Utility.DropDownDataBind(ddlCatg, STR_CAT, strDept)
            '----------------------------------------------------------------------

            'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
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
                Utility.DropDownDataBind(Me.ddlCatgTo, Constants.CategoryRange, sDeptRange)

            End If
            '-------------------------------------------------------------------------------------

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If (Not ViewState("IsLiveFiltering") Is Nothing) Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If
            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If
            '------------------------------------------------------------------------------

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
#End Region

#Region "Page index change"

    'Add By Farnia @ 21 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub dgProduct_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProduct.ItemDataBound
        Dim objCheckbox As CheckBox,
            objTextbox As TextBox,
            objTextbox2 As TextBox,
            objTextboxESC As TextBox,
            btnPrdSCAddressLink As LinkButton,
            strHeaderControlId As String = "",
            strfrontfacing As String = ""
        Try
            Select Case e.Item.ItemType

                Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.SelectedItem, ListItemType.EditItem
                    'saber,DCL5751,18/07/2018
                    strfrontfacing = CType(e.Item.FindControl("lblFrontFacing"), Label).Text
                    '**********************************
                    objCheckbox = CType(e.Item.Cells(5).FindControl("cbWithShelfCapacity"), CheckBox)
                    objTextbox = CType(e.Item.Cells(7).FindControl("txtShelfCapacity"), TextBox)
                    objCheckbox.Attributes.Add("onclick", "NoShelfCapacity('" & objCheckbox.ClientID & "' , '" & objTextbox.ClientID & "');")
                    'TSJ @ 20170215 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC) removed condition
                    'If (objCheckbox.Checked) Then
                    objTextbox.Enabled = False
                    'End If

                    objTextbox2 = CType(e.Item.Cells(7).FindControl("txtShelfCapacity"), TextBox)
                    'koklam, DCL5538 - SCFF control, 17/8/2016
                    strHeaderControlId = objTextbox2.ClientID
                    'txtFront_facing = Trim(e.Item.Cells(8).Text)

                    'TSJ @ 20170215 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC)
                    objTextboxESC = CType(e.Item.Cells(6).FindControl("txtExtendedShelfCapacity"), TextBox)
                    objTextboxESC.Attributes.Add("onblur", "ChangeShelfCapacityTo3Decimal('" & objTextboxESC.ClientID & "');")

                    'saber,DCL5751,18/07/2018
                    'If CType(strfrontfacing, Double) = 0 Then
                    If strfrontfacing = "YES" Then
                        objTextboxESC.Enabled = False
                        'objTextboxESC.Visible = False
                    End If
                    '************************************

                    'TSJ @ 20170215 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC) removed condition
                    'If (Not String.IsNullOrEmpty(txtFront_facing) AndAlso Convert.ToDouble(txtFront_facing) > 0) Then
                    '    objTextbox2.Attributes.Add("onblur", "ValidateIsZero('" & objTextbox2.ClientID & "');")
                    'Else
                    '    objTextbox2.Attributes.Add("onblur", "ChangeShelfCapacityTo3Decimal('" & objTextbox2.ClientID & "');")
                    'End If

                    'TSJ @ 20170215 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC)
                    btnPrdSCAddressLink = CType(e.Item.FindControl("hlnkPrdSCAddress"), System.Web.UI.WebControls.LinkButton)
                    'btnPrdSCAddressLink.Attributes.Add("onclick", "return openProductShelfCapacityAdress('" & btnPrdSCAddressLink.Text.Trim() & "');")
                    btnPrdSCAddressLink.OnClientClick = "return openProductShelfCapacityAdress('" & btnPrdSCAddressLink.Text.Trim() & "');"

            End Select
        Catch ex As Exception
            Dim objEx As New Exception("Error in dgProduct ItemDataBound", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try

    End Sub


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
            'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String) _
            '        , STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_CAT_SP, CType(ViewState("Catg"), String), _
            '        STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String), _
            '        "HGHV", CType(ViewState("HGHV"), String), "BlForPro", CType(ViewState("BlForPro"), String))  '"1015") ' temp Commented 

            'Commented By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String) _
            '                    , STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_DEPTTo_SP, CType(ViewState("DepTo"), String) _
            '                    , STR_CAT_SP, CType(ViewState("Catg"), String), STR_CATTo_SP, CType(ViewState("CatgTo"), String), _
            '                    STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String), _
            '                    "HGHV", CType(ViewState("HGHV"), String), "BlForPro", CType(ViewState("BlForPro"), String), "Mode", CType(ViewState("Mode"), String))  '"1015") ' temp Commented 

            'Commented By Farnia @ 28 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String) _
            '                                , STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_DEPTTo_SP, CType(ViewState("DepTo"), String) _
            '                                , STR_CAT_SP, CType(ViewState("Catg"), String), STR_CATTo_SP, CType(ViewState("CatgTo"), String), _
            '                                STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String), _
            '                                "HGHV", CType(ViewState("HGHV"), String), "BlForPro", CType(ViewState("BlForPro"), String), "Mode", CType(ViewState("WithShelfCapacity"), String), "Div", CType(ViewState("Div"), String))  '"1015") ' temp Commented 

            'Add By Farnia @ 28 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If (ViewState("IsLiveFiltering") = 0) Then
                objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(ViewState("ProdId"), String), STR_PRDT_DESC, CType(ViewState("ProdDesc"), String) _
                                                , STR_UPC, CType(ViewState("ProdUPC"), String), STR_DEPT_SP, CType(ViewState("Dept"), String), STR_DEPTTo_SP, CType(ViewState("DepTo"), String) _
                                                , STR_CAT_SP, CType(ViewState("Catg"), String), STR_CATTo_SP, CType(ViewState("CatgTo"), String),
                                                STR_STORE_SP, Utility.UserStoreId(), "Page_Index", CType(ViewState("PageIndex"), String), "Status", CType(ViewState("Status"), String),
                                                "HGHV", CType(ViewState("HGHV"), String), "BlForPro", CType(ViewState("BlForPro"), String), "Mode", CType(ViewState("WithShelfCapacity"), String),
                                                "Div", CType(ViewState("Div"), String), "FFI", CType(ViewState("FFI"), String), "LiveFProduct_Code", "", "LiveFProd_Desc", "",
                                                "LiveFUPC", "", "LiveFDept", "", "LiveFDeptTo", "",
                                                "LiveFCatg", "", "LiveFCatgTo", "",
                                                "LiveFStore_Code", Utility.UserStoreId(), "LiveFPage_Index", "", "LiveFStatus", "",
                                                "LiveFHGHV", "", "LiveFBlForPro", "", "LiveFMode", "", "LiveFDiv", "",
                                                "IsLiveFiltering", ViewState("IsLiveFiltering"), "LiveFFrontFacing", "") ', _
                'STR_Front_Facing, CType(ViewState("FrontFacing"), String)) '"1015") ' temp Commented 
            ElseIf (ViewState("IsLiveFiltering") = 1) Then
                objXml = DataAccess.BuildXmlParam(STR_PRDT_CODE, CType(Session("SCOPEProdId"), String), STR_PRDT_DESC, CType(Session("SCOPEProdDesc"), String),
                                                               STR_UPC, CType(Session("SCOPEProdUPC"), String), STR_DEPT_SP, CType(Session("SCOPEDept"), String), STR_DEPTTo_SP, CType(Session("SCOPEDeptTo"), String),
                                                               STR_CAT_SP, CType(Session("SCOPECatg"), String), STR_CATTo_SP, CType(Session("SCOPECatgTo"), String),
                                                               STR_STORE_SP, Session("SCOPEUserStore"), "Page_Index", CType(Session("SCOPEPageIndex"), String), "Status", CType(Session("SCOPEStatus"), String),
                                                               "HGHV", CType(Session("SCOPEHGHV"), String), "BlForPro", CType(Session("SCOPEBlForPro"), String), "Mode", CType(Session("SCOPEWithShelfCapacity"), String),
                                                               "Div", CType(Session("SCOPEDiv"), String), "FFI", CType(Session("SCOPEFFI"), String),
                                                               "LiveFProduct_Code", CType(ViewState("ProdId"), String), "LiveFProd_Desc", CType(ViewState("ProdDesc"), String),
                                                               "LiveFUPC", CType(ViewState("ProdUPC"), String), "LiveFDept", CType(ViewState("Dept"), String), "LiveFDeptTo", CType(ViewState("DepTo"), String),
                                                               "LiveFCatg", CType(ViewState("Catg"), String), "LiveFCatgTo", CType(ViewState("CatgTo"), String),
                                                               "LiveFStore_Code", Utility.UserStoreId(), "LiveFPage_Index", CType(ViewState("PageIndex"), String), "LiveFStatus", CType(ViewState("Status"), String),
                                                               "LiveFHGHV", CType(ViewState("HGHV"), String), "LiveFBlForPro", CType(ViewState("BlForPro"), String), "LiveFMode", CType(ViewState("WithShelfCapacity"), String),
                                                               "LiveFDiv", CType(ViewState("Div"), String),
                                                               "IsLiveFiltering", ViewState("IsLiveFiltering"), "LiveFFrontFacing", CType(ViewState("FFI"), String)) ', _
                'STR_Front_Facing, CType(ViewState("FrontFacing"), String))


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
                '                                                "IsLiveFiltering", ViewState("IsLiveFiltering"))  '"1015") ' temp Commented 

            End If


            Dim intCount As Int32
            For intCount = 0 To ar.Count - 1
                DataAccess.BuildXmlParam(objXml, 2, "DepartmentNo", ar.Item(intCount).ToString().Trim())
            Next
            'BlForPro
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
                lblError.CssClass = ""
                lblError.Text = ""
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

    'added by faizal
    'this funtion was used to print informations
    Private Sub FetchDataForPrint()
        Dim DeptInfo As String = Nothing
        Dim strUrl As String
        Dim strServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("ReportingPath")
        Try

            Dim ar As ArrayList = Utility.UserDepartments()

            Dim intCount As Int32
            For intCount = 0 To ar.Count - 1
                DeptInfo += "'" & ar.Item(intCount).ToString().Trim() & "'"
                If intCount < ar.Count - 1 Then
                    DeptInfo += ","
                End If
            Next

            If strValue = 2 Then

                'Modified By : Saber Ang
                'Date        : 07/02/2018
                'Purpose     : DCL5690 - DCL5690 - Revised Shelf Capacity logic and report
                '***************************************************************************
                strUrl = "<script language=javascript>window.open('" + strServer + "Report_FrontFacingShelfCapacity&rc:Parameters=false&rs:Command=Render&Product_Code=" +
                         CType(ViewState("ProdId1"), String) + "&Prod_Desc=" + CType(ViewState("ProdDesc1"), String) + "&UPC=" + CType(ViewState("ProdUPC1"), String) + "&Dept=" + CType(ViewState("Dept1"), String) + "&DeptTo=" + CType(ViewState("DeptTo1"), String) +
                         "&Catg=" + CType(ViewState("Catg1"), String) + "&CatgTo=" + CType(ViewState("CatgTo1"), String) + "&Store_Code=" + Utility.UserStoreId + "&Status=" + CType(ViewState("Status1"), String) + "&FrontFacing=" + CType(ViewState("FFI"), String) +
                        "&BlForPro=" + CType(ViewState("BlForPro1"), String) + "&AllDept=" + CType(DeptInfo, String).Replace("'", "") + "&ShelfCapacity=" + CType(ViewState("WithShelfCapacity"), String) + "','new_winMast002','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"
                '----------------------------------------------------------------------------------------------------------
                '***************************************************************************

                'add on deptInfo into arguments after this break
                'Commented By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                'strUrl = "<script language=javascript>window.open('" + strServer + "Report_HighShrinkHighValue&rc:Parameters=false&rs:Command=Render&Product_Code=" + _
                'CType(ViewState("ProdId1"), String) + "&Prod_Desc=" + CType(ViewState("ProdDesc1"), String) + "&UPC=" + CType(ViewState("ProdUPC1"), String) + "&Dept=" + CType(ViewState("Dept1"), String) + _
                '"&Catg=" + CType(ViewState("Catg1"), String) + "&Store_Code=" + Utility.UserStoreId + "&Status=" + CType(ViewState("Status1"), String) + _
                '"&HGHV=" + CType(ViewState("HGHV1"), String) + "&BlForPro=" + CType(ViewState("BlForPro1"), String) + "&AllDept=" + CType(DeptInfo, String).Replace("'", "") + "','new_winMast002','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"
                '------------------------------------------------------------------------------------------------------------
            Else

                'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
                strUrl = "<script language=javascript>window.open('" + strServer + "Report_HighShrinkHighValue&rc:Parameters=false&rs:Command=Render&Product_Code=" +
                CType(ViewState("ProdId1"), String) + "&Prod_Desc=" + CType(ViewState("ProdDesc1"), String) + "&UPC=" + CType(ViewState("ProdUPC1"), String) + "&Dept=" + CType(ViewState("Dept1"), String) + "&DeptTo=" + CType(ViewState("DeptTo1"), String) +
                "&Catg=" + CType(ViewState("Catg1"), String) + "&CatgTo=" + CType(ViewState("CatgTo1"), String) + "&Store_Code=" + Utility.UserStoreId + "&Status=" + CType(ViewState("Status1"), String) +
                "&HGHV=" + CType(ViewState("HGHV1"), String) + "&BlForPro=" + CType(ViewState("BlForPro1"), String) + "&AllDept=" + CType(DeptInfo, String).Replace("'", "") + "','new_winMast002','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"
                '----------------------------------------------------------------------------------------------------------
            End If

            CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl(strUrl))
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "Report", String.Empty)

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
        ' add by farnia 
        ViewState("IsLiveFiltering") = 0
        Call FetchData()
        Session("PES_PageSet") = ViewState("PageIndex")
    End Sub
#End Region

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And txtUPC.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlStatus.SelectedValue = "") Then
        '    lblHerr.Text = Utility.GetMessage("20001", "Atleast One Input Value")
        '    lblError.Text = ""
        '    dgProduct.DataSource = GetDataSet().Tables(0)
        '    dgProduct.DataBind()
        '    dgProduct.PagerStyle.Visible = False
        '    Return
        'End If


        Try
            'dgProduct.CurrentPageIndex = 0
            'dsProd = New DataSet()
            'Session("ProductEnqTable") = Nothing

            ViewState("PageIndex1") = "1"
            ViewState("ProdId1") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            ViewState("ProdDesc1") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            ViewState("ProdUPC1") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            ViewState("Dept1") = ddlDept.SelectedValue.ToString().Trim()
            'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            ViewState("DeptTo1") = ddlDeptTo.SelectedValue.ToString().Trim()
            '------------------------------------------------------------------------------
            ViewState("Catg1") = ddlCatg.SelectedValue.ToString().Trim()
            'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            ViewState("CatgTo1") = ddlCatgTo.SelectedValue.ToString().Trim()
            '------------------------------------------------------------------------------
            ViewState("Status1") = ddlStatus.SelectedValue.ToString().Trim()
            ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()
            'added by faizal 1/7/2013
            Dim HGHValue As String = ""
            Dim BlOfPro As String = ""
            Dim FFI As String = ""

            'If Me.cbHGHV.Checked = True Then
            '    HGHValue = "True"
            'End If
            If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                HGHValue = ""
            ElseIf Me.cbHGHV.Checked = True Then
                HGHValue = "1"
            ElseIf Me.cbHGHV_No.Checked = True Then
                HGHValue = "0"
            End If

            'If Me.cbBlockOfPro.Checked = True Then
            '    BlOfPro = "T"
            'End If
            If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = ""
            ElseIf Me.cbBlockOfPro.Checked = True Then
                BlOfPro = "T"
            ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                BlOfPro = "F"
            ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                BlOfPro = "X"
            End If

            If (strValue = 2) Then
                If Me.cbFFI.Checked = True And Me.cbFFI_NO.Checked = True Then
                    FFI = ""
                ElseIf Me.cbFFI.Checked = True Then
                    FFI = "T"
                ElseIf Me.cbFFI_NO.Checked = True Then
                    FFI = "F"
                ElseIf (Me.cbFFI.Checked = False And Me.cbFFI_NO.Checked = False) Then
                    FFI = "X"
                End If
                ViewState("FFI") = FFI
                Session("FFI") = ViewState("FFI")
            End If






            ViewState("HGHV1") = HGHValue
            ViewState("BlForPro1") = BlOfPro


            Session("ProdId1") = ViewState("ProdId1")
            Session("ProdDesc1") = ViewState("ProdDesc1")
            Session("ProdUPC1") = ViewState("ProdUPC1")
            Session("Dept1") = ViewState("Dept1")
            'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            Session("DeptTo1") = ViewState("DeptTo1")
            '------------------------------------------------------------------------------
            Session("Catg1") = ViewState("Catg1")
            'Add By Farnia @ 30 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            Session("CatgTo1") = ViewState("CatgTo1")
            '------------------------------------------------------------------------------
            Session("Status1") = ViewState("Status1")
            Session("HGHV1") = ViewState("HGHV1")
            Session("BlForPro1") = ViewState("BlForPro1")


            FetchDataForPrint()
            lblHerr.Text = ""
            lblHerr.CssClass = ""
        Catch ex As Exception
            Dim objEx As New Exception("Error in Fetching Data", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
        'Dim strServer As String = System.Configuration.ConfigurationManager.AppSettings.Item("ReportingPath")
        'Dim product_Code As String = ""

        'For index As Integer = 0 To dgProduct.Items.Count - 1
        '    product_Code += CType(dgProduct.Items(index).FindControl("hypProductId"), HyperLink).Text.Trim()
        '    If index < dgProduct.Items.Count - 1 Then
        '        product_Code += "|"
        '    End If
        'Next
        'Dim strUrl As String = ""

        ''@iintStoreCode
        'strUrl = "<script language=javascript>window.open('" + strServer + "Report_HighShrinkHighValue&rc:Parameters=false&rs:Command=Render&Product_Code=" + product_Code + "&iintStoreCode=" + Utility.UserStoreId() + "','new_win','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=1,top=100,left=140');</script>"

        'CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl(strUrl))
        'ClientScript.RegisterClientScriptBlock(Page.GetType(), "Report", String.Empty)
    End Sub

    'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
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
            'Utility.DropDownDataBind(ddlDept, Constants.DeptCode, str)
            'Utility.DropDownDataBind(ddlCatg, STR_CAT, str)

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

            'Add By Farnia @ 20 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If (ddlWithShelfCapacity.SelectedValue = "") Then
                Utility.DropDownDataBind(ddlWithShelfCapacity, Constants.WithShelfCapacity)
            End If
            '-----------------------------------------------------------------------------

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

    'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub ddlDeptTo_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptTo.SelectedIndexChanged
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
                Utility.DropDownDataBind(Me.ddlCatgTo, Constants.CategoryRange, sDeptRange)

            End If

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If
            '------------------------------------------------------------------------------

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

    'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub ddlCatg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatg.SelectedIndexChanged
        Try
            Dim sCatRange As String = String.Empty
            Dim bFilter As Boolean = True
            Dim dtTable As DataTable = Nothing

            If Me.ddlCatg.SelectedValue Is Nothing = False Then
                Utility.DropDownDataBind(Me.ddlCatgTo, Constants.Category)

                If ddlCatg.SelectedValue.Trim.Length > 0 Then
                    dtTable = ddlCatgTo.DataSource
                    dtTable.DefaultView.RowFilter = " value >= '" & ddlCatg.SelectedValue & "'"
                    ddlCatgTo.DataSource = dtTable
                    ddlCatgTo.DataBind()
                End If
                sCatRange = ddlCatg.SelectedValue & "|" & ddlCatgTo.SelectedValue

            End If

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If
            '------------------------------------------------------------------------------

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
    'Add By Farnia @ 22 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub ddlCatgTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatgTo.SelectedIndexChanged
        Try
            Dim sCatRange As String = String.Empty

            If Not ddlCatg.SelectedValue Is Nothing Then
                If ddlCatg.SelectedValue = "" Then
                    ddlCatg.SelectedValue = ddlCatgTo.SelectedValue
                End If
                sCatRange = ddlCatg.SelectedValue & "|" & ddlCatg.SelectedValue
                If ddlCatgTo.SelectedValue Is Nothing = False Then
                    If ddlCatgTo.SelectedValue.Trim.Length > 0 Then
                        sCatRange = ddlCatg.SelectedValue & "|" & ddlCatgTo.SelectedValue
                    End If
                End If
            End If

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If
            '------------------------------------------------------------------------------

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
    'Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim i As Int16 = 0
            Dim objXml As XmlDocument
            Dim objDataAccess As New DataAccess
            Dim flag As Boolean = False
            Dim flagValidation As Boolean = False
            Dim currentDgIndex As Int16
            Dim Validation As String = ""

            For i = 0 To (dgProduct.Items.Count - 1)
                If (CType(dgProduct.Items(i).FindControl("cbWithShelfCapacity"), CheckBox).Checked = True) Then
                    objXml = DataAccess.BuildXmlParam("Product_Code", dgProduct.Items(i).Cells(1).Text.ToString(), "No_ShelfCapacity", CType(dgProduct.Items(i).FindControl("cbWithShelfCapacity"), CheckBox).Checked, "Store_Code", Utility.UserStoreId(), "NewProductShelfCapacity", 0.0, "UserId", Utility.UserId, "PreviousShefCapacity", dgProduct.Items(i).Cells(4).Text.ToString())
                    objDataAccess.ExecuteSpDataSet("usp_UpdateProductShelfCapacity", objXml)
                    flag = True
                Else
                    'Dim txtShelfCapacity As String = ""
                    'If (Not CType(dgProduct.Items(i).FindControl("txtShelfCapacity"), TextBox).Text = "") Then
                    '    txtShelfCapacity = CType(dgProduct.Items(i).FindControl("txtShelfCapacity"), TextBox).Text
                    '    If (Not IsNumeric(txtShelfCapacity)) Then
                    '        Validation += "," + dgProduct.Items(i).Cells(1).Text
                    '        flagValidation = True
                    '    Else
                    '        If (Not dgProduct.Items(i).Cells(4).Text = CType(dgProduct.Items(i).FindControl("txtShelfCapacity"), TextBox).Text) Then
                    '            objXml = DataAccess.BuildXmlParam("Product_Code", dgProduct.Items(i).Cells(1).Text.ToString(), "No_ShelfCapacity", CType(dgProduct.Items(i).FindControl("cbWithShelfCapacity"), CheckBox).Checked, "Store_Code", Utility.UserStoreId(), "NewProductShelfCapacity", CType(dgProduct.Items(i).FindControl("txtShelfCapacity"), TextBox).Text, "UserId", Utility.UserId, "PreviousShefCapacity", dgProduct.Items(i).Cells(4).Text.ToString())
                    '            objDataAccess.ExecuteSpDataSet("usp_UpdateProductShelfCapacity", objXml)
                    '            flag = True
                    '        End If
                    '    End If
                    'End If

                    Dim txtExtendedShelfCapacity As String = ""
                    If (Not CType(dgProduct.Items(i).FindControl("txtExtendedShelfCapacity"), TextBox).Text = "") Then
                        txtExtendedShelfCapacity = CType(dgProduct.Items(i).FindControl("txtExtendedShelfCapacity"), TextBox).Text
                        If (Not IsNumeric(txtExtendedShelfCapacity)) Then
                            Validation += "," + dgProduct.Items(i).Cells(1).Text
                            flagValidation = True
                        Else
                            If (Not dgProduct.Items(i).Cells(4).Text = CType(dgProduct.Items(i).FindControl("txtExtendedShelfCapacity"), TextBox).Text) Then
                                objXml = DataAccess.BuildXmlParam("Product_Code", CType(dgProduct.Items(i).FindControl("hlnkPrdSCAddress"), LinkButton).Text.ToString(), "No_ShelfCapacity", CType(dgProduct.Items(i).FindControl("cbWithShelfCapacity"), CheckBox).Checked, "Store_Code", Utility.UserStoreId(), "NewProductShelfCapacity", CType(dgProduct.Items(i).FindControl("txtShelfCapacity"), TextBox).Text, "UserId", Utility.UserId, "PreviousShefCapacity", dgProduct.Items(i).Cells(4).Text.ToString(), "NewExtendedProductShelfCapacity", CType(dgProduct.Items(i).FindControl("txtExtendedShelfCapacity"), TextBox).Text, "PreviousExtendedShelfCapacity", dgProduct.Items(i).Cells(4).Text.ToString())
                                objDataAccess.ExecuteSpDataSet("usp_UpdateProductShelfCapacity", objXml)
                                flag = True
                            End If
                        End If
                    End If
                End If
            Next

            ViewState("PageIndex") = ddlPages.SelectedItem.ToString
            currentDgIndex = dgProduct.CurrentPageIndex
            'dgProduct.CurrentPageIndex = 0
            Call FetchData()
            Session("PES_PageSet") = ViewState("PageIndex")
            dgProduct.CurrentPageIndex = currentDgIndex


            'make sure that records have update
            If (flagValidation = True) Then
                Response.Write("<script type=""text/javascript"">alert(""Shelf Capacity should be numeric. "");</script")
            Else
                If flag = True Then
                    lblError.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblError.Visible = True
                    lblError.Text = Utility.GetMessage("10355")
                End If
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in saving records", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub txtProductCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProductCode.TextChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If (Not ViewState("IsLiveFiltering") Is Nothing) Then

                ViewState("IsLiveFiltering") = 1
                LiveFiltering()

            End If
            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub txtPrdtDesc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrdtDesc.TextChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If
            ' -----------------------------------------------------------------------------
            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub txtUPC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUPC.TextChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub cbHGHV_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbHGHV.CheckedChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbHGHV_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub cbHGHV_No_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbHGHV_No.CheckedChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbHGHV_No_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Added by Saber, 11/05/2016 For Front Facing
    Protected Sub cbFFI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFFI.CheckedChanged
        Try
            'lblFFI.Text = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbFFI_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
    'Added by Saber, 11/05/2016 For Front Facing
    Protected Sub cbFFI_NO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbFFI_NO.CheckedChanged
        Try
            'lblFFI.Text = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in cbFFI_CheckedChanged", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub cbBlockOfPro_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBlockOfPro.CheckedChanged
        Try

            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub cbBlockOfPro_NO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbBlockOfPro_NO.CheckedChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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

    'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
    Private Sub LiveFiltering()
        Try

            'Add By Farnia @ 1 Sept 2014 For DCL 5137 - Show shelf capacity on shelf label
            Dim HGHValue As String = ""
            Dim BlOfPro As String = ""
            Dim FFI As String = ""

            If (strValue = 0) Then
                'Master File -> Product Enquiry
                If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And txtUPC.Text.Trim() = "" And ddlStatus.SelectedValue = "" And cbBlockOfPro.Checked = False And cbBlockOfPro_NO.Checked = False) Then
                    lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
                    lblError.CssClass = ""
                    lblError.Text = ""
                    dgProduct.DataSource = GetDataSet().Tables(0)
                    dgProduct.DataBind()
                    dgProduct.PagerStyle.Visible = False
                    ddlPages.Items.Clear()
                    btnPrint.Visible = False
                    Return
                Else

                    If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                        HGHValue = ""
                    ElseIf Me.cbHGHV.Checked = True Then
                        HGHValue = "1"
                    ElseIf Me.cbHGHV_No.Checked = True Then
                        HGHValue = "0"
                    ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                        HGHValue = "2"
                    End If

                    If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = ""
                    ElseIf Me.cbBlockOfPro.Checked = True Then
                        BlOfPro = "T"
                    ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = "F"
                    ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                        BlOfPro = "X"
                    End If

                    If Me.cbFFI.Checked = True And Me.cbFFI_NO.Checked = True Then
                        FFI = ""
                    ElseIf Me.cbFFI.Checked = True Then
                        FFI = "T"
                    ElseIf Me.cbFFI_NO.Checked = True Then
                        FFI = "F"
                    ElseIf (Me.cbFFI.Checked = False And Me.cbFFI_NO.Checked = False) Then
                        FFI = "X"
                    End If

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
                    ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
                    ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
                    ViewState("BlForPro") = BlOfPro
                    ViewState("HGHV") = HGHValue
                    ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()

                    Session("ProdId") = ViewState("ProdId")
                    Session("ProdDesc") = ViewState("ProdDesc")
                    Session("ProdUPC") = ViewState("ProdUPC")
                    Session("Dept") = ViewState("Dept")
                    Session("DeptTo") = ViewState("DepTo")
                    Session("Catg") = ViewState("Catg")
                    Session("CatgTo") = ViewState("CatgTo")
                    Session("Status") = ViewState("Status")
                    Session("BlForPro") = ViewState("BlForPro")
                    Session("HGHV") = ViewState("HGHV")
                    Session("FFI") = ViewState("FFI")

                    FetchData()

                End If

            ElseIf (strValue = 1) Then
                'Store Attributes -> HSHV
                If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And txtUPC.Text.Trim() = "" And ddlStatus.SelectedValue = "" And cbBlockOfPro.Checked = False And cbBlockOfPro_NO.Checked = False And cbHGHV.Checked = False And cbHGHV_No.Checked = False) Then
                    lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
                    lblError.CssClass = ""
                    lblError.Text = ""
                    dgProduct.DataSource = GetDataSet().Tables(0)
                    dgProduct.DataBind()
                    dgProduct.PagerStyle.Visible = False
                    ddlPages.Items.Clear()
                    btnPrint.Visible = False
                    Return
                Else

                    If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                        HGHValue = ""
                    ElseIf Me.cbHGHV.Checked = True Then
                        HGHValue = "1"
                    ElseIf Me.cbHGHV_No.Checked = True Then
                        HGHValue = "0"
                    ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                        HGHValue = "2"
                    End If

                    If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = ""
                    ElseIf Me.cbBlockOfPro.Checked = True Then
                        BlOfPro = "T"
                    ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = "F"
                    ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                        BlOfPro = "X"
                    End If

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
                    ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
                    ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
                    ViewState("BlForPro") = BlOfPro
                    ViewState("HGHV") = HGHValue
                    ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()

                    Session("ProdId") = ViewState("ProdId")
                    Session("ProdDesc") = ViewState("ProdDesc")
                    Session("ProdUPC") = ViewState("ProdUPC")
                    Session("Dept") = ViewState("Dept")
                    Session("DeptTo") = ViewState("DepTo")
                    Session("Catg") = ViewState("Catg")
                    Session("CatgTo") = ViewState("CatgTo")
                    Session("Status") = ViewState("Status")
                    Session("BlForPro") = ViewState("BlForPro")
                    Session("HGHV") = ViewState("HGHV")

                    FetchData()
                End If

            ElseIf (strValue = 2 And ViewState("HyperlinkClick") = True) Then
                'Shelf Capacity -> Home page Hyperlink Click

                If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And txtUPC.Text.Trim() = "" And ddlStatus.SelectedValue = "" And ddlWithShelfCapacity.SelectedValue = "" And cbBlockOfPro.Checked = False And cbBlockOfPro_NO.Checked = False) Then
                    lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
                    lblError.CssClass = ""
                    lblError.Text = ""
                    dgProduct.DataSource = GetDataSet().Tables(0)
                    dgProduct.DataBind()
                    dgProduct.PagerStyle.Visible = False
                    ddlPages.Items.Clear()
                    btnPrint.Visible = False
                    Return
                Else

                    If (ViewState("FirstTime") = True) Then
                        If (ViewState("Div") = 999) Then

                            Dim objData As DataAccess = New DataAccess()
                            Dim dsDep As DataSet = New DataSet()
                            Dim objXml As XmlDocument

                            objXml = DataAccess.BuildXmlParam("DivisionCode", ViewState("Div"))
                            dsDep = objData.ExecuteSpDataSet("usp_AutoSelectDepartmentFromDiv", objXml)

                            ViewState("IsLiveFiltering") = 1
                            ViewState("SCOPEProdId") = ""
                            ViewState("SCOPEProdDesc") = ""
                            ViewState("SCOPEProdUPC") = ""
                            ViewState("SCOPEDept") = dsDep.Tables(0).Rows(0).Item(0)
                            ViewState("SCOPEDepTo") = dsDep.Tables(0).Rows(0).Item(1)
                            ViewState("SCOPECatg") = ""
                            ViewState("SCOPECatgTo") = ""
                            ViewState("SCOPEUserStore") = Utility.UserStoreId()
                            ViewState("SCOPEPageIndex") = 1
                            ViewState("SCOPEStatus") = ""
                            ViewState("SCOPEHGHV") = ""
                            ViewState("SCOPEBlForPro") = "F"
                            ViewState("SCOPEWithShelfCapacity") = ViewState("WithShelfCapacity")
                            ViewState("SCOPEDiv") = ViewState("Div")
                            ViewState("FirstTime") = False

                            'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
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
                            Session("SCOPEFFI") = ViewState("SCOPEFFI")

                        Else

                            ViewState("IsLiveFiltering") = 1
                            ViewState("SCOPEProdId") = ""
                            ViewState("SCOPEProdDesc") = ""
                            ViewState("SCOPEProdUPC") = ""
                            ViewState("SCOPEDept") = ""
                            ViewState("SCOPEDepTo") = ""
                            ViewState("SCOPECatg") = ""
                            ViewState("SCOPECatgTo") = ""
                            ViewState("SCOPEUserStore") = Utility.UserStoreId()
                            ViewState("SCOPEPageIndex") = 1
                            ViewState("SCOPEStatus") = ""
                            ViewState("SCOPEHGHV") = ""
                            ViewState("SCOPEBlForPro") = "F"
                            ViewState("SCOPEWithShelfCapacity") = ViewState("WithShelfCapacity")
                            ViewState("SCOPEDiv") = ViewState("Div")
                            ViewState("FirstTime") = False

                            'Added By: Farnia Emami, Added at: 14 Nov 2014, Purpose: DCL 5184 - Fix search issue on Product Enquiry (Simplify)
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
                            Session("SCOPEFFI") = ViewState("FFI")

                        End If

                    End If


                    If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                        HGHValue = ""
                    ElseIf Me.cbHGHV.Checked = True Then
                        HGHValue = "1"
                    ElseIf Me.cbHGHV_No.Checked = True Then
                        HGHValue = "0"
                    ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                        HGHValue = "2"
                    End If

                    If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = ""
                    ElseIf Me.cbBlockOfPro.Checked = True Then
                        BlOfPro = "T"
                    ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = "F"
                    ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                        BlOfPro = "X"
                    End If



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
                    ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
                    ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
                    ViewState("BlForPro") = BlOfPro
                    ViewState("HGHV") = HGHValue
                    ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()

                    Session("ProdId") = ViewState("ProdId")
                    Session("ProdDesc") = ViewState("ProdDesc")
                    Session("ProdUPC") = ViewState("ProdUPC")
                    Session("Dept") = ViewState("Dept")
                    Session("DeptTo") = ViewState("DepTo")
                    Session("Catg") = ViewState("Catg")
                    Session("CatgTo") = ViewState("CatgTo")
                    Session("Status") = ViewState("Status")
                    Session("BlForPro") = ViewState("BlForPro")
                    Session("HGHV") = ViewState("HGHV")

                    FetchData()

                End If

            ElseIf (strValue = 2) Then
                'Store Attributes -> Shelf Capacity
                If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And txtUPC.Text.Trim() = "" And ddlStatus.SelectedValue = "" And ddlWithShelfCapacity.SelectedValue = "" And cbBlockOfPro.Checked = False And cbBlockOfPro_NO.Checked = False) Then
                    lblHerr.CssClass = "alert alert-danger d-flex justify-content-center"
                    lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
                    lblError.CssClass = ""
                    lblError.Text = ""
                    dgProduct.DataSource = GetDataSet().Tables(0)
                    dgProduct.DataBind()
                    dgProduct.PagerStyle.Visible = False
                    ddlPages.Items.Clear()
                    btnPrint.Visible = False
                    Return
                Else
                    If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
                        HGHValue = ""
                    ElseIf Me.cbHGHV.Checked = True Then
                        HGHValue = "1"
                    ElseIf Me.cbHGHV_No.Checked = True Then
                        HGHValue = "0"
                    ElseIf Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
                        HGHValue = "2"
                    End If

                    If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = ""
                    ElseIf Me.cbBlockOfPro.Checked = True Then
                        BlOfPro = "T"
                    ElseIf Me.cbBlockOfPro_NO.Checked = True Then
                        BlOfPro = "F"
                    ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
                        BlOfPro = "X"
                    End If

                    If Me.cbFFI.Checked = True And Me.cbFFI_NO.Checked = True Then
                        FFI = ""
                    ElseIf Me.cbFFI.Checked = True Then
                        FFI = "T"
                    ElseIf Me.cbFFI_NO.Checked = True Then
                        FFI = "F"
                    ElseIf (Me.cbFFI.Checked = False And Me.cbFFI_NO.Checked = False) Then
                        FFI = "X"
                    End If

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
                    ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
                    ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
                    ViewState("BlForPro") = BlOfPro
                    ViewState("HGHV") = HGHValue
                    ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()
                    ViewState("FFI") = FFI

                    Session("ProdId") = ViewState("ProdId")
                    Session("ProdDesc") = ViewState("ProdDesc")
                    Session("ProdUPC") = ViewState("ProdUPC")
                    Session("Dept") = ViewState("Dept")
                    Session("DeptTo") = ViewState("DepTo")
                    Session("Catg") = ViewState("Catg")
                    Session("CatgTo") = ViewState("CatgTo")
                    Session("Status") = ViewState("Status")
                    Session("BlForPro") = ViewState("BlForPro")
                    Session("HGHV") = ViewState("HGHV")
                    Session("FFI") = ViewState("FFI")

                    FetchData()

                End If
            End If
            ' -------------------------------------------------------------------------------------------------------------------


            ' Commented By Farnia @ 1 sept 2014 for DCL 5137 - Show shelf capacity on shelf label
            ''added by faizal 1/7/2013

            'If (Not ViewState("WithShelfCapacity") = "2") Then

            '    If (txtProductCode.Text.Trim() = "" And txtPrdtDesc.Text.Trim() = "" And txtUPC.Text.Trim() = "" And ddlDept.SelectedValue = "" And ddlDeptTo.SelectedValue = "" And ddlCatg.SelectedValue = "" And ddlCatgTo.SelectedValue = "" And ddlStatus.SelectedValue = "" And ddlWithShelfCapacity.SelectedValue = "") Then
            '        If strValue <> 1 Then
            '            lblHerr.Text = Utility.GetMessage("20001", "At Least One Input Value")
            '            lblError.Text = ""
            '            dgProduct.DataSource = GetDataSet().Tables(0)
            '            dgProduct.DataBind()
            '            dgProduct.PagerStyle.Visible = False
            '            'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            '            ddlPages.Items.Clear()
            '            btnPrint.Visible = False
            '            '-------------------------------------------------------------------------------
            '            Return
            '        End If
            '    End If

            '    'checking for DCL4901 - must check at least one checkbox
            '    If Me.cbHGHV.Checked = False And Me.cbHGHV_No.Checked = False Then
            '        lblHerr.Text = Utility.GetMessage("20001", "At Least One Checkbox for High Shrink / High Value")
            '        lblError.Text = ""
            '        dgProduct.DataSource = GetDataSet().Tables(0)
            '        dgProduct.DataBind()
            '        dgProduct.PagerStyle.Visible = False
            '        'Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            '        ddlPages.Items.Clear()
            '        btnPrint.Visible = False
            '        '-------------------------------------------------------------------------------
            '        Return
            '    End If

            '    'checking for DCL4901 - must check at least one checkbox
            '    If Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False Then
            '        BlOfPro = "X"
            '        'lblHerr.Text = Utility.GetMessage("20001", "At Least One Checkbox Blocked For Ordering Value")
            '        'lblError.Text = ""
            '        'dgProduct.DataSource = GetDataSet().Tables(0)
            '        'dgProduct.DataBind()
            '        'dgProduct.PagerStyle.Visible = False
            '        ''Add By Farnia @ 31 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            '        'ddlPages.Items.Clear()
            '        'btnPrint.Visible = False
            '        '-------------------------------------------------------------------------------
            '        'Return
            '    End If
            'End If


            'Commented By Farnia @ 1 sept 2014 for DCL 5137 - Show shelf capacity on shelf label
            'dgProduct.CurrentPageIndex = 0
            'dsProd = New DataSet()
            'Session("ProductEnqTable") = Nothing

            'ViewState("PageIndex") = "1"
            'ViewState("ProdId") = txtProductCode.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdDesc") = txtPrdtDesc.Text.Replace("'", "''").ToString().Trim()
            'ViewState("ProdUPC") = txtUPC.Text.Replace("'", "''").ToString().Trim()
            'ViewState("Dept") = ddlDept.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("DepTo") = ddlDeptTo.SelectedValue.ToString().Trim()
            ''------------------------------------------------------------------------------
            'ViewState("Catg") = ddlCatg.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'ViewState("CatgTo") = ddlCatgTo.SelectedValue.ToString().Trim()
            ''------------------------------------------------------------------------------
            'ViewState("Status") = ddlStatus.SelectedValue.ToString().Trim()
            ''Add By Farnia @ 20 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (Not ViewState("WithShelfCapacity") = "2") Then
            '    If (Not ViewState("HyperlinkClick") = True) Then
            '        ViewState("WithShelfCapacity") = ddlWithShelfCapacity.SelectedValue.ToString().Trim()
            '    End If
            'End If
            '' ----------------------------------------------------------------------------



            ''If Me.cbHGHV.Checked = True Then
            ''    HGHValue = "True"
            ''End If
            'If Me.cbHGHV.Checked = True And Me.cbHGHV_No.Checked = True Then
            '    HGHValue = ""
            'ElseIf Me.cbHGHV.Checked = True Then
            '    HGHValue = "1"
            'ElseIf Me.cbHGHV_No.Checked = True Then
            '    HGHValue = "0"
            'End If

            ''If Me.cbBlockOfPro.Checked = True Then
            ''    BlOfPro = "T"
            ''End If
            'If Me.cbBlockOfPro.Checked = True And Me.cbBlockOfPro_NO.Checked = True Then
            '    BlOfPro = ""
            'ElseIf Me.cbBlockOfPro.Checked = True Then
            '    BlOfPro = "T"
            'ElseIf Me.cbBlockOfPro_NO.Checked = True Then
            '    BlOfPro = "F"
            'ElseIf (Me.cbBlockOfPro.Checked = False And Me.cbBlockOfPro_NO.Checked = False) Then
            '    BlOfPro = "X"
            'End If

            'ViewState("HGHV") = HGHValue
            'ViewState("BlForPro") = BlOfPro

            'Session("ProdId") = ViewState("ProdId")
            'Session("ProdDesc") = ViewState("ProdDesc")
            'Session("ProdUPC") = ViewState("ProdUPC")
            'Session("Dept") = ViewState("Dept")
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Session("DeptTo") = ViewState("DepTo")
            ''-----------------------------------------------------------------------------
            'Session("Catg") = ViewState("Catg")
            ''Add By Farnia @ 23 July 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Session("CatgTo") = ViewState("CatgTo")
            ''-----------------------------------------------------------------------------
            'Session("Status") = ViewState("Status")
            ''Add By Farnia @ 20 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'Session("WithShelfCapacity") = ViewState("WithShelfCapacity")
            ''-----------------------------------------------------------------------------
            'Session("HGHV") = ViewState("HGHV")
            'Session("BlForPro") = ViewState("BlForPro")

            'FetchData()
            ' ----------------------------------------------------------------------------------------------------------------------
            lblHerr.Text = ""
            lblHerr.CssClass = ""
            'new codes for DCL4901 
            Me.btnPrint.Visible = False
            Dim strFunc As String() = Access.ScreenFunctions(Constants.PRODENQ)
            If Not IsNothing(strFunc) Then
                If (strFunc.Length > 0) Then
                    Dim intcounter As Int32
                    For intcounter = 0 To strFunc.Length - 1
                        If strFunc(intcounter) = Constants.CONST_F4 Then
                            If Me.dgProduct.Items.Count > 0 Then
                                If strValue = 1 Then
                                    Me.btnPrint.Visible = True
                                End If
                            End If
                        End If
                    Next
                End If
            End If

            If (dgProduct.Items.Count > 0 And strValue = 2) Then
                btnSave.Visible = True
                btnPrint.Visible = True
            End If

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

    'Add By Farnia @ 20 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
    Protected Sub ddlWithShelfCapacity_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWithShelfCapacity.SelectedIndexChanged
        Try
            'Add By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            If Not ViewState("IsLiveFiltering") Is Nothing Then
                ViewState("IsLiveFiltering") = 1
                LiveFiltering()
            End If

            'Commented By Farnia @ 29 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
            'If (dgProduct.Items.Count > 0 Or (dgProduct.Items.Count = 0 And lblError.Text.Length > 0)) Then
            '    LiveFiltering()
            'End If

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
    '----------------------------------------------------------------
    'ADD BY  : FARNIA
    'ADD AT  : 21 January 2015
    'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
    '----------------------------------------------------------------
    Private Sub EnabledButton(ByVal Enable As Boolean)
        If Enable Then
            btnUpload.Enabled = Utility.GetFunctionAccess(Constants.ShelfCapacity, Constants.CONST_F2)
            btnWin7Upload.Enabled = Utility.GetFunctionAccess(Constants.ShelfCapacity, Constants.CONST_F2)
        Else
            btnUpload.Enabled = False
            btnWin7Upload.Enabled = False
        End If
    End Sub
    '----------------------------------------------------------------
    'END
    '----------------------------------------------------------------
    '----------------------------------------------------------------
    'ADD BY  : FARNIA
    'ADD AT  : 21 January 2015
    'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
    '----------------------------------------------------------------
    Private Function generateSeqNo() As Integer
        Try
            Dim objRandomNo As System.Random = New Random()
            Dim intMinValue As Int32 = 10000000
            Dim intMaxValue As Int64 = 99999999
            Dim intRandom As Int32 = objRandomNo.Next(intMinValue, intMaxValue)

            Return intRandom
        Catch ex As Exception
            Dim objEx As New Exception("Error in Uploading Files", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Function
    '----------------------------------------------------------------
    'END
    '----------------------------------------------------------------
    '----------------------------------------------------------------
    'ADD BY  : FARNIA
    'ADD AT  : 21 January 2015
    'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
    '----------------------------------------------------------------
    Private Sub CheckWindowCompatible()
        Dim requestInfo As String()
        Dim versionInfo As String()
        Dim Compatible As Boolean = False

        Try
            requestInfo = Split(Request.UserAgent, ";")

            If requestInfo(2).IndexOf("Windows NT") >= 0 Then
                versionInfo = Split(requestInfo(2).Trim, "NT")
                If Convert.ToDecimal("6.1") <= Convert.ToDecimal(versionInfo(1).Trim) Then
                    Compatible = True
                End If
            End If

            If Compatible = True Then
                Me.btnWin7Upload.Visible = True
                Me.btnUpload.Visible = False
            Else
                Me.btnWin7Upload.Visible = False
                Me.btnUpload.Visible = True
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in CheckWindowCompatible", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
        End Try
    End Sub
    '----------------------------------------------------------------
    'END
    '----------------------------------------------------------------

    '----------------------------------------------------------------
    'ADD BY  : FARNIA
    'ADD AT  : 21 January 2015
    'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
    '----------------------------------------------------------------
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim objXmlDoc As XmlDocument
            Dim objDataAccess As New DataAccess
            Dim intTable As Integer

            If objDataAccess Is Nothing Then objDataAccess = New DataAccess
            objXmlDoc = DataAccess.BuildXmlParam("StoreCode", Utility.UserStoreId, "UserID", Utility.UserId, "FileSeqNo", ViewState("FileSeqNo").ToString)


            Dim dsResult As DataSet = objDataAccess.ExecuteSpDataSet("usp_HHTSC_BatchUpload", objXmlDoc)
            Dim strTransactions As String = String.Empty

            intTable = dsResult.Tables.Count - 1

            If (objDataAccess.DbMessage.Count <> 0) Then
                lblMessage.Text = objDataAccess.DbMessage.Message(0)
                lblMessage.Visible = True

            ElseIf Not dsResult Is Nothing AndAlso dsResult.Tables.Count > 0 AndAlso dsResult.Tables(2).Rows.Count > 0 Then
                If dsResult.Tables(intTable).Rows(0)("Status") = 1 Then
                    lblMessage.Text = Utility.GetMessage("10211")
                    lblMessage.Visible = True

                    dgProduct.DataSource = dsResult.Tables(2)
                    dgProduct.DataBind()
                    dgProduct.Visible = True

                    'btnPrint.Enabled = Utility.GetFunctionAccess(Constants.GapCheck, Constants.CONST_F2)
                    btnPrint.Enabled = True

                End If

            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in Upload file Processing", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("..\ErrorPage.aspx?code=50001")
        End Try
    End Sub
    '----------------------------------------------------------------
    'END
    '----------------------------------------------------------------

    '----------------------------------------------------------------
    'ADD BY  : FARNIA
    'ADD AT  : 21 January 2015
    'PURPOSE : DCL 5214 - Shelf capacity maintenance via HHT
    '----------------------------------------------------------------
    Protected Sub btnWin7Upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWin7Upload.Click
        Try

            Dim objXmlDoc As XmlDocument
            Dim objDataAccess As New DataAccess
            Dim intTable As Integer

            If objDataAccess Is Nothing Then objDataAccess = New DataAccess
            objXmlDoc = DataAccess.BuildXmlParam("StoreCode", Utility.UserStoreId, "UserID", Utility.UserId, "FileSeqNo", ViewState("FileSeqNo").ToString)

            Dim dsResult As DataSet = objDataAccess.ExecuteSpDataSet("usp_HHTSC_BatchUpload", objXmlDoc)
            Dim strTransactions As String = String.Empty

            intTable = dsResult.Tables.Count - 1

            If (objDataAccess.DbMessage.Count <> 0) Then
                lblMessage.Text = objDataAccess.DbMessage.Message(0)
                lblMessage.Visible = True

            ElseIf Not dsResult Is Nothing AndAlso dsResult.Tables.Count > 0 AndAlso dsResult.Tables(2).Rows.Count > 0 Then

                If dsResult.Tables(intTable).Rows(0)("Status") = 1 Then
                    lblMessage.Text = Utility.GetMessage("10211")
                    lblMessage.Visible = True

                    dgProduct.DataSource = dsResult.Tables(2)
                    dgProduct.DataBind()
                    dgProduct.Visible = True

                    btnPrint.Enabled = Utility.GetFunctionAccess(Constants.GapCheck, Constants.CONST_F2)

                End If
            End If

        Catch ex As Exception
            Dim objEx As New Exception("Error in Upload file Processing", ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)
            Response.Redirect("..\ErrorPage.aspx?code=50001")
        End Try
    End Sub
    '----------------------------------------------------------------
    'END
    '----------------------------------------------------------------
End Class