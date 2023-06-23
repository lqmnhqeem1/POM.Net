#Region "Imports"

Imports POM.Lib.Data
Imports POM.Lib.Log
Imports POM.Lib.UI
Imports System.Xml

#End Region

Public Class VendorCompanyDetails
    Inherits System.Web.UI.Page

#Region "Objects"
    Dim objXmlDoc As XmlDocument
    Dim objDataAcces As DataAccess
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Utility.UserId.Equals(String.Empty) Then Response.Redirect("~/Login.aspx")

        GetVendorCompanyDetails()
    End Sub

    ''' <summary>
    ''' get the vendor comapny details for the given vendor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetVendorCompanyDetails()
        Try

            objDataAcces = New DataAccess
            If Request.Params("vendorcode") <> "" Then
                Dim strVendorCode As String = Request.Params("vendorcode").ToString()
                objXmlDoc = DataAccess.BuildXmlParam("VENDOR_CODE", strVendorCode, "STORE_CODE", Convert.ToInt32(Utility.UserStoreId))
                Dim dsVendorCompanyDetails As New DataSet
                dsVendorCompanyDetails = objDataAcces.ExecuteSpDataSet("usp_VendorCompanyDetails", objXmlDoc)
                Dim dtVendorCompanyDetails As DataTable
                dtVendorCompanyDetails = dsVendorCompanyDetails.Tables(0)

                'assign the values to the labels
                lblVendorCodeVal.Text = Convert.ToString(dtVendorCompanyDetails.Rows(0)("Vendor_Code"))
                lblCreateDateVal.Text = dtVendorCompanyDetails.Rows(0)("CreateDate").ToString()
                lblVendorNameVal.Text = dtVendorCompanyDetails.Rows(0)("Vendor_Name").ToString()
                lblCountryVal.Text = dtVendorCompanyDetails.Rows(0)("Country").ToString()
                lblCurrencyVal.Text = dtVendorCompanyDetails.Rows(0)("Currency").ToString()
                lblAddress1Val.Text = dtVendorCompanyDetails.Rows(0)("Address1").ToString()
                lblAddress2Val.Text = dtVendorCompanyDetails.Rows(0)("Address2").ToString()
                lblAddress3Val.Text = dtVendorCompanyDetails.Rows(0)("Address3").ToString()
                lblCityVal.Text = dtVendorCompanyDetails.Rows(0)("City").ToString()
                lblStateVal.Text = dtVendorCompanyDetails.Rows(0)("State").ToString()
                lblZipVal.Text = dtVendorCompanyDetails.Rows(0)("Zip").ToString()
                lblEmailVal.Text = dtVendorCompanyDetails.Rows(0)("Email").ToString()
                lblAreaCodeVal.Text = dtVendorCompanyDetails.Rows(0)("Area").ToString()
                lblAreaFaxVal.Text = dtVendorCompanyDetails.Rows(0)("Area_Fax").ToString()
                lblTelephoneVal.Text = dtVendorCompanyDetails.Rows(0)("Tel").ToString()
                lblFaxval.Text = dtVendorCompanyDetails.Rows(0)("Fax").ToString()
                lblCoRegval.Text = dtVendorCompanyDetails.Rows(0)("Company_Regno").ToString()
                lblGstRegVal.Text = dtVendorCompanyDetails.Rows(0)("Vat_Regno").ToString()
                lblStatusVal.Text = dtVendorCompanyDetails.Rows(0)("Status").ToString()
                lblContactPersonVal.Text = dtVendorCompanyDetails.Rows(0)("ContactPerson").ToString()
                lblContactEmailVal.Text = dtVendorCompanyDetails.Rows(0)("ContactPersonEmail").ToString()
                'GST Start
                lblGSTEffDateValue.Text = dtVendorCompanyDetails.Rows(0)("Eff_Date").ToString()
                'GST End
                '******************Modified By:Indira Tiwari Dt:30/10/2006***************
                '***********************START******************************
                lblReturnFlagVal.Text = dtVendorCompanyDetails.Rows(0)("Return_Flag").ToString
                '**************************END**************************
            End If

        Catch ex As Exception

            Dim objEx As New Exception(Reflection.MethodInfo.GetCurrentMethod().Name, ex)
            If IsNothing(ex.InnerException) Then
                objEx.Source = Reflection.Assembly.GetExecutingAssembly.GetName(False).Name
            Else
                objEx.Source = ex.Source
            End If
            ExceptionLog.Log(objEx)

        End Try

    End Sub

End Class