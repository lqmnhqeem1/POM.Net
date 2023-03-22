// JScript File

//Add By Farnia @ 2 Sept 2014 For DCL 5137 - Show shelf capacity on shelf label
function validateProductEnquiryEntry3(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddlDept, ddlDeptTo, ddlCatg, ddlCatgTo, cbBlockOfPro, cbBlockOfPro_No, ddlWithShelfCapacity, ddlStatus) {
    if (msgsearch1 == null)
        return false;

    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddlDept);
    ddldeptTocontrol = document.getElementById(ddlDeptTo);
    ddlcatcontrol = document.getElementById(ddlCatg);
    ddlcatTocontrol = document.getElementById(ddlCatgTo);
    //add by farnia 1 Sep 
    cbBlockOfProControl = document.getElementById(cbBlockOfPro);
    cbBlockOfPro_NoControl = document.getElementById(cbBlockOfPro_No);
    ddlWithShelfCapacityControl = document.getElementById(ddlWithShelfCapacity);
    //-------------------
    ddlStatuscontrol = document.getElementById(ddlStatus);

    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddldeptTocontrol == null || ddlcatcontrol == null || ddlcatTocontrol == null || ddlStatuscontrol == null || cbBlockOfProControl == null || cbBlockOfPro_NoControl == null || ddlWithShelfCapacityControl == null)
        return false;

    // any one madatory check
    //Modify by farnia
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddldeptTocontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlcatTocontrol.selectedIndex > 0 || ddlStatuscontrol.selectedIndex > 0 || cbBlockOfProControl.checked == true || ddlWithShelfCapacityControl.selectedIndex > 0 || cbBlockOfPro_NoControl.checked == true);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;
}
//END Add By Farnia

//Add By Farnia @ 27 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
function ChangeShelfCapacityTo3Decimal(textbox) {
    debugger;
    objTextBox = document.getElementById(textbox);
    if (objTextBox.value == "") {
        objTextBox.value = "0.000";
    }
    else {
        if (objTextBox.value >= 1000) {
            alert("Cannot enter amount more than 999.");
            objTextBox.value = "0.000";
        }
        else {
            var objTextBoxValue = (parseFloat(objTextBox.value)).toFixed(3);
            objTextBox.value = objTextBoxValue;
        }

    }
}


//Add By Farnia @ 21 Aug 2014 For DCL 5137 - Show shelf capacity on shelf label
function NoShelfCapacity(checkbox, textbox) {
    objCheckbox = document.getElementById(checkbox);
    objTextBox = document.getElementById(textbox);

    if (objCheckbox.checked) {
        objTextBox.disabled = true;
        objTextBox.value = "0.000";
    }
    else {
        objTextBox.disabled = false;
    }
}


//Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
function validateProductEnquiryEntry2(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddlDept, ddlDeptTo, ddlCatg, ddlCatgTo, cbBlockOfPro, cbBlockOfPro_No, ddlStatus) {
    if (msgsearch1 == null)
        return false;

    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddlDept);
    ddldeptTocontrol = document.getElementById(ddlDeptTo);
    ddlcatcontrol = document.getElementById(ddlCatg);
    ddlcatTocontrol = document.getElementById(ddlCatgTo);
    //add by farnia 1 Sep 
    cbBlockOfProControl = document.getElementById(cbBlockOfPro);
    cbBlockOfPro_NoControl = document.getElementById(cbBlockOfPro_No);
    //-------------------
    ddlStatuscontrol = document.getElementById(ddlStatus);

    //Modify By farnia 
    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddldeptTocontrol == null || ddlcatcontrol == null || ddlcatTocontrol == null || ddlStatuscontrol == null || cbBlockOfProControl == null || cbBlockOfPro_NoControl == null)
        return false;

    // any one madatory check
    //Modify by farnia
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddldeptTocontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlcatTocontrol.selectedIndex > 0 || ddlStatuscontrol.selectedIndex > 0 || cbBlockOfProControl.checked == true || cbBlockOfPro_NoControl.checked == true);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;
}
//END Add By Farnia

//Add By Farnia @ 24 July 2014 For DCL 5137 - Show shelf capacity on shelf label
function validateProductEnquiryEntryCheckBox2(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddldept, ddldeptTo, ddlcat, ddlcatTo, ddlStatus, cbHGHV, cbHGHV_No, cbBlockOfPro, cbBlockOfPro_No) {
    if (msgsearch1 == null)
        return false;


    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddldept);
    ddldeptTocontrol = document.getElementById(ddldeptTo);
    ddlcatcontrol = document.getElementById(ddlcat);
    ddlcatTocontrol = document.getElementById(ddlcatTo);
    ddlStatuscontrol = document.getElementById(ddlStatus);
    ddlHGHVcontrol = document.getElementById(cbHGHV);
    ddlHGHV_Nocontrol = document.getElementById(cbHGHV_No);
    ddlBlockOfProcontrol = document.getElementById(cbBlockOfPro);
    ddlBlockOfPro_Nocontrol = document.getElementById(cbBlockOfPro_No);

    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddldeptTocontrol == null || ddlcatcontrol == null || ddlcatTocontrol == null || ddlStatuscontrol == null || ddlHGHVcontrol == null || ddlHGHV_Nocontrol == null || ddlBlockOfProcontrol == null || ddlBlockOfPro_Nocontrol == null)
        return false;

    // any one madatory check
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddldeptTocontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlcatTocontrol.selectedIndex > 0 || ddlStatuscontrol.selectedIndex > 0 || ddlHGHVcontrol.checked == true || ddlHGHV_Nocontrol.checked == true || ddlBlockOfProcontrol.checked == true || ddlBlockOfPro_Nocontrol.checked == true);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;
}
//END Add By Farnia

function displayVPC(lblVendorCodeVal, lblPrdCodeVal, lblVendorNameVal, lblCasePackIdVal, lblCaseQtyVal, lblPrdDescVal) {

    //   lblVendorCodeVal = document.getElementById(lblVendorCodeVal);
    //  lblPrdCodeVal = document.getElementById(lblPrdCodeVal);
    //  lblVendorNameVal = document.getElementById(lblVendorNameVal);
    // lblCasePackIdVal = document.getElementById(lblCasePackIdVal);
    // lblCaseQtyVal = document.getElementById(lblCaseQtyVal);
    // lblPrdDescVal = document.getElementById(lblPrdDescVal);
    window.open('VendorProductCostHistory.aspx?VendorCode='
        + lblVendorCodeVal
        + '&&ProductCode='
        + lblPrdCodeVal
        + '&&VendorName='
        + lblVendorNameVal
        + '&&CasePackId='
        + lblCasePackIdVal
        + '&&CaseQty='
        + lblCaseQtyVal
        + '&&ProductDesc='
        + lblPrdDescVal, 'VPCHistory', 'height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')
}
function displayLRC(lblVendorCodeVal, lblPrdCodeVal, lblVendorNameVal, lblCasePackIdVal, lblCaseQtyVal, lblPrdDescVal) {
    window.open('LastReceivedCostHistory.aspx?VendorCode='
        + lblVendorCodeVal
        + '&&ProductCode='
        + lblPrdCodeVal
        + '&&VendorName='
        + lblVendorNameVal
        + '&&CasePackId='
        + lblCasePackIdVal
        + '&&CaseQty='
        + lblCaseQtyVal
        + '&&ProductDesc='
        + lblPrdDescVal, 'VPCHistory', 'height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=no')


    //window.open('LastReceivedCostHistory.aspx?VendorCode='+ lblVendorCodeVal            + '&&ProductCode=' 
    //              + lblPrdCodeVal
    //            + '&&VendorName='
    //          + lblVendorNameVal
    //        +'&&CasePackId=' 
    //      + lblCasePackIdVal
    //    + '&&CaseQty=' 
    //  + lblCaseQtyVal
    //+ '&&ProductDesc='
    //+ lblPrdDescVal
    //,'LRCHistory','height=500,width=780,status=yes,toolbar=no,scrollbars=no');
}



//Modified By Rashi Goyal Dated: 3rd Nov. '06
function validateProductEnquiryEntry(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddldept, ddlcat, ddlStatus) {
    if (msgsearch1 == null)
        return false;


    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddldept);
    ddlcatcontrol = document.getElementById(ddlcat);
    ddlStatuscontrol = document.getElementById(ddlStatus);

    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddlcatcontrol == null || ddlStatuscontrol == null)
        return false;

    // any one madatory check
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlStatuscontrol.selectedIndex > 0);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;

}
//END

// Modified by Nicholas 
// Modified on 08/07/2013
function validateProductEnquiryEntryCheckBox(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddldept, ddlcat, ddlStatus, cbHGHV, cbHGHV_No, cbBlockOfPro, cbBlockOfPro_No) {
    if (msgsearch1 == null)
        return false;


    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddldept);
    ddlcatcontrol = document.getElementById(ddlcat);
    ddlStatuscontrol = document.getElementById(ddlStatus);
    ddlHGHVcontrol = document.getElementById(cbHGHV);
    ddlHGHV_Nocontrol = document.getElementById(cbHGHV_No);
    ddlBlockOfProcontrol = document.getElementById(cbBlockOfPro);
    ddlBlockOfPro_Nocontrol = document.getElementById(cbBlockOfPro_No);

    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddlcatcontrol == null || ddlStatuscontrol == null || ddlHGHVcontrol == null || ddlHGHV_Nocontrol == null || ddlBlockOfProcontrol == null || ddlBlockOfPro_Nocontrol == null)
        return false;

    // any one madatory check
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlStatuscontrol.selectedIndex > 0 || ddlHGHVcontrol.checked == true || ddlHGHV_Nocontrol.checked == true || ddlBlockOfProcontrol.checked == true || ddlBlockOfPro_Nocontrol.checked == true);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;

}

function validateVendorDetailsEntry(msgSearch, msgAtleastOne, txtVendorName, ddlDepartment, ddlCategory, txtVendorCode) {
    if (msgAtleastOne == null || msgSearch == null) return false;

    vendorCodeControl = document.getElementById(txtVendorCode);
    vendorNameControl = document.getElementById(txtVendorName);
    ddlDepartmentControl = document.getElementById(ddlDepartment);
    ddlCategoryControl = document.getElementById(ddlCategory);

    if (vendorCodeControl == null || vendorNameControl == null || ddlDepartmentControl == null || ddlCategoryControl == null)
        return false;

    // any one madatory check    
    checkres = (trimAll(vendorCodeControl.value).length > 0 || trimAll(vendorNameControl.value).length > 0 || ddlDepartmentControl.selectedIndex > 0 || ddlCategoryControl.selectedIndex > 0);
    if (checkres == false) {
        alert(msgAtleastOne);
        vendorCodeControl.focus();
        return false;
    }

    if (vendorCodeControl.value.length != 0) {
        if (isInteger(trimAll(vendorCodeControl.value)) == false) {
            alert(msgSearch);
            vendorCodeControl.value = "";
            vendorCodeControl.focus();
            return false;
        }
    }
    return true;
}

//Modified By Rashi Goyal Date: 31st Oct '06
function validatePOSPromotionSearch(ProductCode, msgProductCode, PromotionCode, msgPromoCode, StartDate, msgStartDate, EndDate, msgEndDate, ProductDesc, PromoType, msgDateCompare, msgSelectOne) {
    var ObjProduct = document.getElementById(ProductCode + '_txtLOVCode');
    var ObjPromotion = document.getElementById(PromotionCode + '_txtLOVCode');
    var objStartDate = document.getElementById(StartDate + '_txtCalendarValue');
    var objEndDate = document.getElementById(EndDate + '_txtCalendarValue');
    var objProductDesc = document.getElementById(ProductDesc);
    var objPromoType = document.getElementById(PromoType);


    if ((ObjProduct.value == "") && (ObjPromotion.value == "") && (objStartDate.value == "") && (objEndDate.value == "") && (objProductDesc.value == "") && (objPromoType.value == "")) {

        alert(msgSelectOne);
        ObjPromotion.focus();
        return false;
    }

    if (ObjProduct.value == "" || isInteger(trimAll(ObjProduct.value))) {
        if (ObjPromotion.value == "" || isInteger(trimAll(ObjPromotion.value))) {
            if ((objStartDate.value == "") || isDate(trimAll(objStartDate.value))) {
                if ((objEndDate.value == "") || (isDate(trimAll(objEndDate.value)))) {
                }
                else {
                    //Invalid End Date
                    alert(msgEndDate);
                    objEndDate.focus();
                    return false;
                }
            }
            else {
                //Invalid Start Date
                alert(msgStartDate);
                objStartDate.focus();
                return false;
            }
        }
        else {
            //Invalid Promotion Code
            alert(msgPromoCode);
            ObjPromotion.focus();
            return false;
        }
    }
    else {
        //Invalid Product Code
        alert(msgProductCode);
        ObjProduct.focus();
        return false;
    }

    if ((objStartDate.value != "") && (objEndDate.value != "")) {
        var startDate = Date(objStartDate.value);
        var endDate = Date(objEndDate.value)
        if (DateCompare(objStartDate.value, objEndDate.value, msgDateCompare) == false) { return false; };
    }

    return true;
}
//End

function validatePromotionSearch(ProductCode, msgProductCode, PromotionCode, msgPromoCode, StartDate, msgStartDate, EndDate, msgEndDate, PromoDesc, ProductDesc, msgDateCompare, msgSelectOne) {

    var ObjProduct = document.getElementById(ProductCode + '_txtLOVCode');
    var ObjPromotion = document.getElementById(PromotionCode + '_txtLOVCode');
    var objStartDate = document.getElementById(StartDate + '_txtCalendarValue');
    var objEndDate = document.getElementById(EndDate + '_txtCalendarValue');
    var objPromoDesc = document.getElementById(PromoDesc);
    var objProductDesc = document.getElementById(ProductDesc);



    if ((ObjProduct.value == "") && (ObjPromotion.value == "") && (objStartDate.value == "") && (objEndDate.value == "") && (objPromoDesc.value == "") && (objProductDesc.value == "")) {

        alert(msgSelectOne);
        ObjPromotion.focus();
        return false;
    }

    if (ObjProduct.value == "" || isInteger(ObjProduct.value)) {
        if (ObjPromotion.value == "" || isInteger(ObjPromotion.value)) {
            if ((objStartDate.value == "") || isDate(objStartDate.value)) {
                if ((objEndDate.value == "") || (isDate(objEndDate.value))) {
                }
                else {
                    //Invalid End Date
                    alert(msgEndDate);
                    objEndDate.focus();
                    return false;
                }
            }
            else {
                //Invalid Start Date
                alert(msgStartDate);
                objStartDate.focus();
                return false;
            }
        }
        else {
            //Invalid Promotion Code
            alert(msgPromoCode);
            ObjPromotion.focus();
            return false;
        }
    }
    else {
        //Invalid Product Code
        alert(msgProductCode);
        ObjProduct.focus();
        return false;
    }

    if ((objStartDate.value != "") && (objEndDate.value != "")) {
        var startDate = Date(objStartDate.value);
        var endDate = Date(objEndDate.value)
        if (DateCompare(objStartDate.value, objEndDate.value, msgDateCompare) == false) { return false; };
    }
    return true;
}

function validateVendorProductCostEnquiryEntry(msgAtleastOne, msgNumeric, msgAlPhaNum, vendorCode, vendorName, productCode, productDesc, deptCode, catCode) {
    if (msgAtleastOne == null)
        return false;

    vendorcodecontrol = document.getElementById(vendorCode);
    vendornamecontrol = document.getElementById(vendorName);
    prodcodecontrol = document.getElementById(productCode);
    proddesccontrol = document.getElementById(productDesc);
    ddldeptcontrol = document.getElementById(deptCode);
    ddlcatcontrol = document.getElementById(catCode);

    if (vendorcodecontrol == null || vendornamecontrol == null || prodcodecontrol == null || proddesccontrol == null || ddldeptcontrol == null || ddlcatcontrol == null)
        return false;

    // any one madatory check    
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(vendorcodecontrol.value).length > 0 || trimAll(vendornamecontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0);
    if (checkres == false) {
        alert(msgAtleastOne);
        vendorcodecontrol.focus();
        return false;
    }
    if (vendorcodecontrol.value.length != 0) {
        if (isInteger(trimAll(vendorcodecontrol.value)) == false) {
            //vendor code should be numeric
            alert(msgNumeric);
            vendorcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(trimAll(prodcodecontrol.value)) == false) {
            alert(msgAlPhaNum);
            prodcodecontrol.value = "";
            prodcodecontrol.focus();
            return false;
        }
    }
    return true;
}

function DateCompare(startDate, EndDate, msgDateCompare) {
    var a1 = startDate.split("/");
    var a2 = EndDate.split("/");

    if (a1.length == 0) { a1 = startDate.split("-"); }
    if (a2.length == 0) { a2 = EndDate.split("-"); }

    var objStartDate = new Date;
    objStartDate.setDate(a1[0]);
    objStartDate.setMonth(a1[1] - 1);
    objStartDate.setFullYear(a1[2]);

    var objEndDate = new Date();
    objEndDate.setDate(a2[0]);
    objEndDate.setMonth(a2[1] - 1);
    objEndDate.setFullYear(a2[2]);

    if (objStartDate > objEndDate) {
        alert(msgDateCompare);
        return false;
    }
    else {
        return true;
    }

}

function validateVPCHistoryEntry(msgStartDate, msgEndDate, msgDateCompare, msgStartDateF, msgEndDateF, startDate, endDate) {
    //var vy, vm,vd;
    currdate = new Date();

    vd = currdate.getDate();
    vm = currdate.getMonth() + 1;
    vy = currdate.getFullYear();

    startdatecontrol = document.getElementById(startDate);
    enddatecontrol = document.getElementById(endDate);

    if (isDate(startdatecontrol.value) == false) {
        //Invalid Start Date
        alert(msgStartDate);
        return false;
    }

    else if (startdatecontrol.value.split('/')[0] > vd && startdatecontrol.value.split('/')[1] >= vm && startdatecontrol.value.split('/')[2] >= vy) {
        //cannot be future date
        alert(msgStartDateF);
        return false;
    }

    if (isDate(enddatecontrol.value) == false) {
        alert(msgEndDate);
        return false;
    }
    else if (enddatecontrol.value.split('/')[0] > vd && enddatecontrol.value.split('/')[1] >= vm && enddatecontrol.value.split('/')[2] >= vy) {
        //cannot be future date
        alert(msgEndDateF);
        return false;
    }

    if (DateCompare(startdatecontrol.value, enddatecontrol.value, msgDateCompare) == false) return false;

    return true;
}

function openlinkProductHierarchy(windid, productcode) {
    if (windid == null || productcode == null) return false;

    switch (windid) {
        case 'fp':
            window.open('FuturePrice.aspx?productid=' + productcode, 'FSP', 'height=180,width=600,status=yes,toolbar=no,scrollbars=no');
            break;

        case 'rsp':
            window.open('CurrentRSP.aspx?productid=' + productcode, 'CRSP', 'height=180,width=600,status=yes,toolbar=yes,scrollbars=no');
            break;

        case 'upc':
            window.open('UPC.aspx?productid=' + productcode, 'UPC', 'height=350,width=412,status=yes,toolbar=no,scrollbars=no');
            break;

        case 'ing':
            window.open('Ingredients.aspx?productid=' + productcode, 'ING', 'height=350,width=650,status=yes,toolbar=no,scrollbars=no');
            break;

        case 'vp':
            window.open('VendorProduct.aspx?productid=' + productcode, 'VP', 'height=550,width=650,status=yes,toolbar=no,scrollbars=no');
            break;

        case 'lp':
            window.open('LinkProducts.aspx?productid=' + productcode, 'LP', 'height=450,width=600,status=yes,toolbar=no,scrollbars=no');
            break;

        case 'ps':
            window.open('ProductStyle.aspx?productid=' + productcode, 'PS', 'height=150,width=600,status=yes,toolbar=no,scrollbars=no');
            break;
    }
    return false;
}

function openlinkVendorProducts(vendorcode, vendorname) {
    if (vendorcode == null || vendorname == null) return false;
    window.open('VendorProducts.aspx?vendorcode=' + vendorcode + '&vendorname=' + vendorname, 'VP', 'height=450,width=780,status=yes,toolbar=no,scrollbars=no');
    return false;
}

function openlinkVendorProductsforDept(vendorcode, vendorname, ddlDept, ddlCat) {
    Dept = document.getElementById(ddlDept);
    Cat = document.getElementById(ddlCat);
    vendorname = vendorname.replace("&", "^^")
    if (vendorcode == null || vendorname == null) return false;
    window.open('VendorProducts.aspx?vendorcode=' + vendorcode + '&vendorname=' + vendorname + '&Dept=' + Dept.value + '&Cat=' + Cat.value + '&DistType=', 'VP', 'height=550,width=980,top=100,left=140,status=yes,toolbar=no,scrollbars=1');
    return false;
}

function openProductGroupDetails(ProductGroupCode, Mode) {
    if (ProductGroupCode == null) return false;
    window.open('ProductGroupDetails.aspx?GroupCode=' + ProductGroupCode + '&Mode=' + Mode, 'PG', 'width=980,height=350,top=100,left=140,status=yes,toolbar=no,scrollbars=yes');
    return false;
}

function validateVendorProductEntry(msgAtleastOne, formid) {
    if (countRadioSelected(formid) == 0) {
        alert(msgAtleastOne);
        return false;
    }
    else {
        return true;
    }
}

function ValidateAndOpenWindow(vendorcode, vendorname, msgAtleastOne, formid) {
    if (vendorcode == null || vendorname == null || msgAtleastOne == null || formid == null) return false;

    //Atleast one radiobutton selected
    if (countRadioSelected(formid) == 0) {
        alert(msgAtleastOne);
        return false;
    }
    else {
        openlinkVendorProducts(vendorcode, vendorname);
    }

}

function validateHamperEnquiryEntry(msgsearch1, msgsearch2, msgsearch3, txtprodcode, txtproddesc, txtupc, ddldept, ddlcat, ddlstatus) {
    if (msgsearch1 == null)
        return false;

    prodcodecontrol = document.getElementById(txtprodcode);
    proddesccontrol = document.getElementById(txtproddesc);
    produpccontrol = document.getElementById(txtupc);
    ddldeptcontrol = document.getElementById(ddldept);
    ddlcatcontrol = document.getElementById(ddlcat);
    ddlstatuscontrol = document.getElementById(ddlstatus);

    if (prodcodecontrol == null || proddesccontrol == null || produpccontrol == null || ddldeptcontrol == null || ddlcatcontrol == null || ddlstatuscontrol == null)
        return false;

    // any one madatory check
    checkres = (trimAll(prodcodecontrol.value).length > 0 || trimAll(proddesccontrol.value).length > 0 || trimAll(produpccontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlstatuscontrol.selectedIndex > 0);
    if (checkres == false) {
        alert(msgsearch1);
        prodcodecontrol.focus();
        //addClientMessage(displaymid, displayid, msgsearch1);
        return false;
    }
    if (trimAll(prodcodecontrol.value).length > 0) {
        if (isAlphaNumeric(prodcodecontrol.value) == false) {
            alert(msgsearch2);
            prodcodecontrol.focus();
            return false;
        }
    }
    if (trimAll(produpccontrol.value).length > 0) {
        if (isAlphaNumeric(produpccontrol.value) == false) {
            alert(msgsearch3);
            produpccontrol.focus();
            return false;
        }
    }
    return checkres;

}


function ValidateIsZero(textbox) {
    objTextBox = document.getElementById(textbox);
    if (objTextBox.value == 0) {
        alert("Cannot insert 0 value when front facing more than 0")
        objTextBox.focus();
        this.select();
    }
    else {
        var objTextBoxValue = (parseFloat(objTextBox.value)).toFixed(3);
        objTextBox.value = objTextBoxValue;
    }
}

//TSJ @ 20170215 DCL5609 New concept of SC by address and Extended Shelf Capacity (ESC)
function openProductShelfCapacityAdress(ProductCode) {
    if (ProductCode == null) return false;
    window.open('ProductShelfCapacityByAddress.aspx?ProductCode=' + ProductCode, 'ProductShelfCapacityAddress', 'width=900,height=270,top=100,left=140,status=yes,toolbar=no,scrollbars=yes');
    //window.open('ProductGroupDetails.aspx?GroupCode='+ '1015156' +'&Mode='+'pdg' ,'PG','width=980,height=350,top=100,left=140,status=yes,toolbar=no,scrollbars=yes');
    return false;
}



function selectAll(checkboxId, formId) {
    var i, bChecked, objForm, objHCheckBox;

    objHCheckBox = document.getElementById(checkboxId);
    if (objHCheckBox) {
        bChecked = objHCheckBox.checked;

        objForm = document.getElementById(formId);
        if (objForm) {
            for (i = 0; i < objForm.elements.length; i++) {
                if (objForm.elements[i].type == "checkbox") {
                    if (objForm.elements[i].disabled == false) objForm.elements[i].checked = bChecked;
                }
            }
        }
    }
}


