Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data

Public Class Constants
    Public Const PurchaseOrder As String = "PO"
    Public Const FOC As String = "Yes"
    Public Const POExpiry As String = "PO"

    Public Const ColProdtId As Integer = 1
    Public Const ColProdtDesc As Integer = 2
    Public Const ColCasePackId As Integer = 3
    Public Const ColCasePackQty As Integer = 5
    Public Const ColCaseCost As Integer = 6
    Public Const ColFOC As Integer = 7
    Public Const ColMinOOQ As Integer = 9
    Public Const ColPurchQty As Integer = 14
    Public Const ColTotalSales As Integer = 15
    Public Const ColPromos As Integer = 16

    Public Const ColDelete As Integer = 17
    Public Const ColPrevQty As Integer = 18
    Public Const ColFreeCasePackId As Integer = 19
    Public Const ColSuggested As Integer = 20

    Public Const CNDNAdjReasons As String = "TODO:"
    Public Const POSource As String = "POSRC"
    Public Const CURRMAIN As String = "CURRMAIN"
    Public Const TransEnq As String = "TRNRSN"
    Public Const POType As String = "POTYPE"
    Public Const IBTType As String = "IBTTYP"
    Public Const DCIBType As String = "DCFTYP"
    Public Const InvStatus As String = "INVSTS"
    Public Const POStatus As String = "POSTAT"
    Public Const DisType As String = "PODIS"
    Public Const IBTDisType As String = "IBTDIS"
    Public Const DCIBTDisType As String = "DCFTDIS"
    Public Const POFresh As String = "POFRSH"
    Public Const Frequency As String = "FREQ"
    Public Const DeptCode As String = "DEPT"
    Public Const DepartmentRange As String = "DEPRNG"
    Public Const DepartmentByDivision As String = "DEPTBYDIV"
    Public Const NONFreshDeptCode As String = "NONFRESHDEPT"
    Public Const POORDTYPE As String = "PORDR"
    Public Const Category As String = "CAT"
    Public Const CategoryRange As String = "CATRNG"
    Public Const NONFreshCategory As String = "NONFRESHCAT"
    Public Const SalesPerson As String = "SALES"
    Public Const ShipStore As String = "SHIPSTORE"
    Public Const RecIBT_SHIPSTORE As String = "RecIBT_SHIPSTORE"
    Public Const RecICT_SHIPSTORE As String = "RecICT_SHIPSTORE"
    Public Const Store As String = "STORE"
    Public Const Company As String = "COMPANY"
    Public Const Division As String = "DIVISION"
    Public Const Div_Catg As String = "DIV_CATG"
    Public Const DivisionSelected As String = "DIVISIONSELECTED"
    Public Const DivisionWithUserRights As String = "DIVWITHUSRRIGHTS"
    Public Const NONFreshDivisionWithUserRights As String = "NONFRESHDIV"
    Public Const SubCategory As String = "SUBCAT"
    Public Const PrdClass As String = "CLASS"
    Public Const SubClass As String = "SUBCLASS"
    Public Const Job As String = "JOB"
    Public Const DCBatchStores As String = "DCBTSTR"
    Public Const DCNoBatchStores As String = "DCNOSTR"
    Public Const Recovery As String = "REC"
    Public Const STOREDCWMS As String = "STOREDCWMS"
    Public Const NewSite As String = "NewSite"
    Public Const SubClass_Catg As String = "SUBCLASS_CATG"
    Public Const OCL_DC As String = "OCL_DC"
    Public Const ICTDCList As String = "ICT_DC"
    Public Const PromotionPriceLabel As String = "PROMO_LABEL"

    'Folder Path Constants *********************
    'IBT
    Public Const OutFolder As String = "OTPTH"
    Public Const InFolder As String = "INPTH"
    'OHB
    Public Const OHBOutFolder As String = "OHBOPT"
    Public Const OHBInFolder As String = "OHBIPT"
    'Cost Price Change, Immediate Price Change    
    Public Const IPCOutPath As String = "IPCOPT"
    Public Const IPCInPath As String = "IPCIPT" 'Not Created
    Public Const CPCOutPath As String = "CPOPTH" 'Not Created
    Public Const CPCInPath As String = "CPIPTH" 'Not Created

    Public Const SAOutFolder As String = "SADPTH"
    Public Const SAInFolder As String = "SAUPTH"

    'Common
    Public Const CopyFileExePath As String = "C:\\Exe\\CopyFiles.exe"
    Public Const InProcessPath As String = "PINPTH"
    Public Const OutProcessPath As String = "POTPTH"
    Public Const InErrorPath As String = "EINPTH"
    Public Const OutErrorPath As String = "EOTPTH"
    Public Const InTempPath As String = "TINPTH"
    Public Const OutTempPath As String = "TOTPTH"
    '*******************************************

    Public Const IBTStore As String = "IBTSTORE"
    Public Const ClusterStore As String = "CLUSTSTORE"
    Public Const HamperStatus As String = "HAMPERSTATUS"
    Public Const VendorStatus As String = "VENDSTAT"
    'Add By Farnia @ 20 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
    Public Const WithShelfCapacity As String = "WSHLFC"
    '-----------------------------------------------------------------------------
    'Add By Saber @ 30/03/2016 For DCL 5449 - Show New Column Shelf Capacity on Po001 - Proforma
    Public Const ProformaShelfCapacity As String = "PROFSC"
    '-----------------------------------------------------------------------------

    Public Const HamperTxType As String = "HAMPERTXTYPE"
    Public Const POSPromo As String = "POS"
    Public Const HHTId As String = "HHT"
    Public Const GRNCondition As String = "GRNCND"
    Public Const GRNNo As String = "GRNSTS"
    Public Const BatchSts As String = "BatchSts"
    Public Const GRNInvStatus As String = "GISTAT"
    Public Const CostPriceStatus As String = "CSTCHG"
    Public Const IPCReasons As String = "IPCRSN"
    Public Const ControlType As String = "CTLTYP"
    Public Const IDTType As String = "IDTTYP"
    Public Const ExpensesType As String = "EXPTYP"
    Public Const StockAdjType As String = "SAJTYP"
    Public Const StockTakeAdjType As String = "STAJTYP"
    Public Const IDTInDept As String = "IDTIN"
    Public Const StockAdjTranType As String = "SAJTRN"
    Public Const StockDisposalReasons As String = "SDPRSN"
    Public Const STACCR As String = "STACCR"
    'Added by Shah for DCL 5386
    Public Const FileUploadType As String = "FILUPL"
    Public Const FileUploadMode As String = "UPMODE"
    Public Const BaseExcelData As String = "BaseExcelData"
    '*********************************************************************
    'Added by   : Huong Yu Seng
    'Added on   : July 15, 2008
    'Reason     : DCL 1388 - For new control type "Sales INvoice" and reason code for third-party sales
    '*********************************************************************
    Public Const SalesInvoiceReason As String = "SIRSN"
    Public Const SalesInvoiceReasons As String = "SIRSNS"
    '*********************************************************************
    'Modified Start by Sharon Tan Shu Yin
    'Modified on 29/11/2007
    'Modified Purpose DCL863
    Public Const HQApprovedAdjReasons As String = "HQARSN"
    'Modified End by Sharon Tan Shu Yin
    Public Const IDTReasons As String = "IDTRSN"
    Public Const PAJReasons As String = "PAJRSN"    'Reson code for Promotional Adjustment
    Public Const StockTakeAdjReasons As String = "STKRSN"
    Public Const CashieringErrorAdjReasons As String = "CEARSN"

    Public Const CNDNIBTAdjReasons As String = "IBTRSN"
    Public Const CNDNGRNAdjReasons As String = "GRNRSN"
    Public Const TEXT As String = "text"
    Public Const VALUE As String = "value"
    Public Const TRANSACTION_ID As String = "txnid"
    Public Const DCCode As String = "DC"
    Public Const MESSAGE = "msg"
    Public Const IBTStatus = "IBTSTA"

    'Screen ID Constants
    Public Const FreshStockTake As String = "STF01"
    Public Const C_OHBAdjustment As String = "PO01"
    Public Const HouseKeeping As String = "STA01"
    Public Const HandheldTerminal As String = "STA02"
    Public Const HighShrink As String = "STA03"
    Public Const ShelfCapacity As String = "STA04"
    Public Const SalePerson As String = "STA05"
    '*******************************************
    'Add By  : Farnia Emami
    'Add at  : 24 Oct 2014
    'Purpose : DCL 5176 - Gap Check
    Public Const GapCheck As String = "STA07"
    '*******************************************

    Public Const PRODENQ As String = "MST03"
    Public Const PRODHRCH As String = "MST07"
    Public Const HAMPENQ As String = "MST06"
    Public Const HAMPHRCH As String = "MST08"
    Public Const GRNENQ As String = "GRN01"
    Public Const INCOMGRN As String = "GRN02"
    Public Const VENDENQ As String = "MST01"
    Public Const TRASN As String = "TRASN"
    Public Const VPCENQ As String = "MST02"
    Public Const TranEnqHamp As String = "HMP01"
    Public Const BuildHamp As String = "HMP02"
    Public Const BreakHamp As String = "HMP03"
    Public Const CPC As String = "CSP01"
    Public Const IPC As String = "IPC01"
    Public Const RECPO As String = "PO02"
    Public Const SCHVendors As String = "PO07"
    Public Const IBTTransferRequest As String = "IBT01"
    Public Const IBTTransferIN As String = "IBT02"
    Public Const IBTTransferOUT As String = "IBT03"
    Public Const IBTAllocationIN As String = "IBT04"
    Public Const IBTAllocationOut As String = "IBT15"
    Public Const IBTRecovery As String = "REC001"
    Public Const CONFIG_OFFLINE_STORE As String = "IBT16"

    Public Const DCSch As String = "IBT05"
    Public Const ID_RecommendedIBT As String = "IBT06"
    Public Const ID_RecommendedICT As String = "IBT17"
    Public Const RTWConsolidation As String = "RTWC"

    Public Const PromotionAllocation As String = "PromoAlloc"

    'BizTalk - leewc
    Public Const IBTUpdStat As String = "IBT07"

    Public Const ModifyDCSch As String = "IBT11"
    Public Const VENDPRDetails As String = "MST11"
    Public Const CREATEGRA As String = "GRA01"
    Public Const InvMatching As String = "INV01"
    Public Const InvBatch As String = "INV02"
    Public Const InvPost As String = "INV03"
    Public Const DWNFSST As String = "DWN01"
    Public Const ADDMODIFYHHT As String = "STA11"
    Public Const ADDMODIFYSALESPERSON As String = "STA12"
    ' Gaurav Verma - 13 Mar 09 - DCL# 1966 - screen to configure Pallet rounding percentage for IBT
    Public Const PalletRoundingPer As String = "STA13"
    Public Const PROMOTIONEnquiry As String = "MST04"
    Public Const POSPROMOEnquiry As String = "MST05"
    Public Const WEEKLYSchedule As String = "PO06"
    Public Const ACTIVATEDPO As String = "GRN03"
    Public Const STOCKAdjustment As String = "SAJ01"
    '******************PO *********************
    Public Const POMAIN As String = "PO03"
    Public Const IBTTR As String = "IBT01"
    Public Const IBTTO As String = "IBT03"
    Public Const IBTTI As String = "IBT02"
    Public Const IBTAI As String = "IBT04"
    Public Const VENDSCH As String = "PO05"
    Public Const PRELISTPO As String = "PO08"
    Public Const PODCMAIN As String = "PODC01"
    Public Const STKMAIN As String = "STK01"

    ' Modified by   : Foong Kok Loon
    ' Modified on   : 20 April 2008
    ' Purpose       : Export PO Screen
    Public Const EXPORTPO As String = "PO09"

    ' Modified by   : Foong Kok Loon
    ' Modified on   : 20 April 2008
    ' Purpose       : Export PO Screen
    Public Const FTCONSO As String = "IBT13"

    ' Modified by   : Foong Kok Loon
    ' Modified on   : 17 July 2008
    ' Purpose       : P-Stock Maintenance Screen
    Public Const PSTOCK As String = "IBT14"


    'Added by   : Tan Chin Seng (Eric)
    'Added on   : Jan 26, 2011
    'Reason     : DCL 2979 - POM Ordering Enhancement Phase 2
    Public Const DISTYP As String = "DISTYP"

    'Added by   : Lu
    'Added on   : 20 Feb 2014
    'Reason     : DCL 5052 Show blocked reason code
    Public Const BFORCo As String = "BFORCo"

    '******************************************
    'Screen Function Constants
    Public Const CONST_F1 As String = "F1"
    Public Const CONST_F2 As String = "F2"
    Public Const CONST_F3 As String = "F3"
    Public Const CONST_F4 As String = "F4"
    Public Const CONST_F5 As String = "F5"
    Public Const CONST_F6 As String = "F6"
    Public Const CONST_F7 As String = "F7"
    Public Const CONST_F8 As String = "F8"
    Public Const CONST_F9 As String = "F9"
    Public Const CONST_F10 As String = "F10"
    Public Const CONST_F11 As String = "F11"
    Public Const CONST_F12 As String = "F12"
    Public Const CONST_F13 As String = "F13"
    Public Const CONST_F14 As String = "F14"
    Public Const CONST_F15 As String = "F15"
    Public Const CONST_F16 As String = "F16"
    Public Const CONST_F17 As String = "F17"
    Public Const CONST_F18 As String = "F18"
    Public Const CONST_F19 As String = "F19"
    Public Const CONST_F20 As String = "F20"
    Public Const CONST_F21 As String = "F21"
    ' DCL# 1487 - Gaurav Verma 27 Aug 2008
    Public Const CONST_F22 As String = "F22"
    ' DCL# 1554 - Gaurav Verma 21 Nov 2008
    Public Const CONST_F23 As String = "F23"
    Public Const CONST_F24 As String = "F24"
    Public Const CONST_F25 As String = "F25"

    ' DCL# 1800 - Manoj Bisht Allocation out reports 
    Public Const CONST_F26 As String = "F26"
    Public Const CONST_F27 As String = "F27"
    Public Const CONST_F28 As String = "F28"
    Public Const CONST_F29 As String = "F29"
    Public Const CONST_F30 As String = "F30"

    ' DCL# 1752 - Gaurav Verma 23 Dec 2008
    '*********** IBT STATUS 
    Public Const CREATED As String = "Created"
    Public Const REQUESTED As String = "Requested"
    Public Const TRANSFEROUT As String = "Transfer-Out"
    Public Const TRANSFERIN As String = "Transfer-In"
    Public Const CONFIRMED As String = "Confirmed"
    Public Const CANCEL As String = "Cancelled"
    Public Const EXPIRED As String = "Expired"
    '****************** SEQUENCE TYPE CONSTANTS
    Public Const IPCSEQ As String = "IPC"
    Public Const CPCSEQ As String = "CPC"
    Public Const OHBSEQ As String = "OHB"
    Public Const UPCSEQ As String = "UPC"
    Public Const WSSEQ As String = "WS"
    Public Const IBTCONSEQ As String = "IBTCONSEQ"
    Public Const IBTFTSEQ As String = "IBTFTSEQ"
    '***************** NO OF SQEQ CHARS CONSTANTS
    Public Const IPCNOCHAR As Integer = 7
    Public Const CPCNOCHAR As Integer = 7
    Public Const OHBNOCHAR As Integer = 7
    Public Const UPCNOCHAR As Integer = 5
    Public Const WSNOCHAR As Integer = 2
    Public Const IBTCONNOCHAR As Integer = 8
    Public Const IBTFTCHAR As Integer = 8
    '***************** Home Page *************
    Public Const Created_Status As Integer = 1
    Public Const Requested_Status As Integer = 2
    Public Const TrfOut_Status As Integer = 3
    Public Const TrfIn_Status As Integer = 4
    Public Const HHTExpire_Status As Integer = 5

    '***************** Report *************\
    Public Const MasterReports As String = "MSTR"
    Public Const HamperReports As String = "HAMR"
    Public Const POReports As String = "POR"
    Public Const GRReports As String = "GRR"
    Public Const INVReports As String = "INVR"
    Public Const RTVReports As String = "RTVR"
    Public Const IBTReports As String = "IBTR"
    Public Const StkAdjReports As String = "SADJR"
    Public Const IVTRReports As String = "IVTRR"
    Public Const SAttReports As String = "SATTR"
    Public Const ALCReports As String = "ALCR"
    Public Const RTWReports As String = "RTWR"

    ' Modified by   : Shee Chong Hoo
    ' Modified on   : 6 May 2008
    ' Purpose       : DCL1181 - NEW REPORT
    Public Const SALINVReports As String = "SALINVR"
    Public Const SALINVR001 As String = "SALR001"

    'Added By Sachin <HHT Ordering (26 Aug 2008)>
    Public Const HHTORD As String = "HHTPO"

    'Added By Gaurav Verma <DCL# 1562 - Product Status (22 Sep 2008)>
    Public Const ProdStatus As String = "PSTAT"

    'Added By Gaurav Verma <DCL# 784 - Screen - Oracle FIN Mapping (2 Oct 2008)>
    Public Const OFINMapping As String = "OFINMAP"

    'Added By Gaurav Verma <DCL# 784 - Screen - Group Store (14 Oct 2008)>
    Public Const GRPSTR As String = "GRPSTR"
    Public Const GRPSTRADD As String = "GRPSTRADD"
    Public Const OrgID As String = "OrgID"
    Public Const CompanyCode As String = "CompanyCode"

    'DCL 1962-Start
    Public Const RTWDisType As String = "RTWDIS"
    Public Const RTWStatus = "RTWSTA"
    Public Const RTWType As String = "RTWTYP"
    Public Const VENDOR As String = "RTWVEN"
    Public Const RTWDC As String = "RTWDC"
    Public Const DIVTYPE As String = "DIVTYPE"
    'DCL 1962-End

    Public Const HQSYSTEM1 As String = "SAP"
    Public Const HQSYSTEM2 As String = "PMM"

    'Added by Mark Milan <Sales and GP HQ SAP BI Report> <18-June-2009>
    Public Const SALESGPReports As String = "SALEGPR"
    Public Const SALESGPHQSAPBI As String = "SGPRSAP01"

    Public Const NonTradeProduct As String = "NT01"
    Public Const NonTradeVPC As String = "NT02"
    Public Const NonTradeIPC As String = "NT03"
    Public Const NonTradeNewSiteCreation As String = "NT04"

    'Added By Lu, 24 May 2011, DCL 3066 - Sales, Profit, Inventory and Shrinkage Analysis Report
    Public Const BIReports As String = "BIR"
    Public Const SPISABS_Period As String = "SPISP"
    Public Const SPISABS_Range As String = "SPISDR"
    Public Const STKAG_Period As String = "STKAGP"
    Public Const UNEXPIREDPO As String = "GRN05"

    'Added By Eric, 06 June 2011, DCL 3070 & 3067
    Public Const STKDAYS_Period As String = "STKDAYS"
    Public Const STKDAYDC_Period As String = "STKDAYDC"
    Public Const ReportMaster As String = "RptMst"


    Public Const ProdStatus_Active_Delete_Only As String = "PSTAT_AD"

    'Added By Shah for DCL 5405
    Public Const SMSUSR As String = "SMSUSR"
    Public Const SMSTYP As String = "SMSTYP"


End Class

Public Class Helper

    Public Shared Function ToXmlString(ByVal item As Object) As String

        Dim xmlString As String = String.Empty,
            output As StringWriter = Nothing,
            xs As XmlSerializer = Nothing

        Try
            output = New StringWriter(New StringBuilder())
            xs = New XmlSerializer(item.GetType())
            xs.Serialize(output, item)

            xmlString = output.ToString().Replace("xmlns:xsd=""http://www.w3.org/2001/XMLSchema""", String.Empty).Replace("xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""", String.Empty).Replace("<?xml version=""1.0"" encoding=""utf-16""?>", String.Empty).Replace("\r\n", String.Empty).Trim()
            Return xmlString
        Catch
            Throw
        End Try
    End Function

    Public Shared Function GetXMLDocumentFromDataTable(ByVal dt As DataTable) As XmlDocument
        Dim xDoc As New XmlDocument(),
            strWriter As New StringWriter(),
            xTextWriter As XmlTextWriter = Nothing,
            strXmlString As String
        Try
            xTextWriter = New XmlTextWriter(strWriter)
            dt.WriteXml(xTextWriter, XmlWriteMode.IgnoreSchema)
            ' Write the Xml out to a string
            strXmlString = strWriter.ToString()
            'load the string of Xml into the document
            xDoc.LoadXml(strXmlString)
            Return xDoc
        Catch
            Throw
        End Try
    End Function

    '' =============================================      
    '' Author:		Eric Tan Chin Seng      
    '' Create date: 19 May 2011      
    '' Description: 3067 Stock Days by Merchandise Hierarchy by Article Report       
    '' =============================================    
    Public Shared Function GetReportDateFormat() As String
        Return "dd/MMM/yyyy"
    End Function

End Class