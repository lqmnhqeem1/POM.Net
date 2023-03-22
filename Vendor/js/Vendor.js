// JScript File
//************************VEDNOR SCHDULE SCREEN *****************************

function validatePOMainConfirm(msgToShow) {
    inputbox = confirm(msgToShow);
    if (inputbox == true) {
        return true;
    }
    else {
        return false;
    }
}



function validateVendorScheEntryAdd(msgsearch1, msgsearch2, msgsearch3, ddldept, lovVendor) {

    if (msgsearch1 == null) return false;
    ddldeptcontrol = document.getElementById(ddldept);
    lovVendorcontrol = document.getElementById(lovVendor);
    if (lovVendorcontrol == null) return false;
    // any one madatory check   ||
    checkres = (trimAll(lovVendorcontrol.value).length > 0);
    //'**CHECK DEPT CODE IS BLANK **********************
    if (!checkres) {
        alert(msgsearch1);
        lovVendorcontrol.focus();
        return false;
    }
    //**IF VENDOR CODE IS ENTERED **********************
    else {

        //**CHECK IF ENTERED VALUE IS NUMERIC**************
        if (!isNumeric(lovVendorcontrol.value) || !isInteger(lovVendorcontrol.value)) {
            alert(msgsearch3);
            lovVendorcontrol.focus();
            return false;
        }

    }
    if (ddldept == null) return false;
    //'**CHECK DEPT CODE IS BLANK **********************
    checkres = (ddldeptcontrol.selectedIndex > 0);
    if (!checkres) {
        alert(msgsearch2);
        ddldeptcontrol.focus();
        return false;
    }

}

function validateVendorScheEntry(msgsearch1, msgsearch3, ddlcat, ddlorder, txtName, ddldept, lovVendor) {

    if (msgsearch1 == null) return false;
    ddldeptcontrol = document.getElementById(ddldept);
    ddlcatcontrol = document.getElementById(ddlcat);
    ddlorderdayscontrol = document.getElementById(ddlorder);
    txtnamecontrol = document.getElementById(txtName);
    lovVendorcontrol = document.getElementById(lovVendor);
    if (lovVendorcontrol == null) return false;
    // any one madatory check   ||
    checkres = (trimAll(lovVendorcontrol.value).length > 0 || trimAll(txtnamecontrol.value).length > 0 || ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlorderdayscontrol.selectedIndex > 0);

    if (checkres == false) {
        alert(msgsearch1);
        lovVendorcontrol.focus();
        return false;
    }
    //**IF VENDOR CODE IS ENTERED **********************
    else {
        if (trimAll(lovVendorcontrol.value).length > 0) {
            //**CHECK IF ENTERED VALUE IS NUMERIC**************
            if (!isNumeric(lovVendorcontrol.value)) {
                alert(msgsearch3);
                lovVendorcontrol.focus();
                return false;
            }
        }
    }

}
function ValidateOrderDays(drpFre, lOrderDays) {
    drpFre = document.getElementById(drpFre);
    if (drpFre == null) return false;

    lOrderDays = document.getElementById(lOrderDays);
    if (drpFre.value == 1) {
        lOrderDays.selectedIndex = -1;
        return false;
    }
    else {
        return false;
    }
    return true;
}

function validateScheduleModifyEntry(msgsearch1, msgsearch2, msgsearch3, msgsearch4, msgsearch5, msgsearch6, msgsearch7, msgLT, msgBS, msgChkFreq, msgAtleastOne, ddpFreq, drpLeadTime, drpOrder, drpBS) {
    //'msgsearch5 = "Frequency as 'Daily' and Order Days cannot be selected together"
    if (msgsearch1 == null) return false;
    ddpFreqcontrol = document.getElementById(ddpFreq);
    drpLeadTimecontrol = document.getElementById(drpLeadTime);
    drpOrdercontrol = document.getElementById(drpOrder);

    if (drpBS.length > 0)
        drpBScontrol = document.getElementById(drpBS);

    //****************FREQ CONTROL *********************
    var count = 0;
    for (i = 0; i < drpOrdercontrol.length; i++) {
        if (drpOrdercontrol.options[i].selected) {
            count++;
        }
    }

    //validations between frequency and orderdays      
    //either orderdays or frequency shd be selected  
    if (drpOrdercontrol.selectedIndex == -1 && ddpFreqcontrol.selectedIndex == 0) {
        alert(msgAtleastOne);
        ddpFreqcontrol.focus();
        return false;
    }
    //when orderdays selected frequency shd be selected
    if (drpOrdercontrol.selectedIndex > -1 && ddpFreqcontrol.selectedIndex == 1) {
        //invalid frequency
        alert(msgChkFreq);
        ddpFreqcontrol.focus();
        return false;
    }
    //when orderdays not selected frequency shd be daily   
    if (drpOrdercontrol.selectedIndex == -1 && ddpFreqcontrol.selectedIndex != 1) {
        //invalid frequency
        alert(msgChkFreq);
        ddpFreqcontrol.focus();
        return false;
    }
    //when multiple orderdays selected frequency shd not be selected
    if (count > 1 && ddpFreqcontrol.selectedIndex > 0) {
        alert(msgChkFreq);
        ddpFreqcontrol.focus();
        return false;
    }
    //when orderdays 1 day  selected : frequency shd not be nothing or daily
    if (count == 1 && ddpFreqcontrol.selectedIndex < 2) {
        alert(msgChkFreq);
        ddpFreqcontrol.focus();
        return false;
    }
    //

    //***************************************************
    //****************LEAD TIME  CONTROL *********************
    if (drpLeadTimecontrol == null) return false;
    checkres = (trimAll(drpLeadTimecontrol.value).length > 0);
    //'**CHECK LEAD CODE IS BLANK *********************
    if (!checkres) {
        alert(msgsearch2);
        drpLeadTimecontrol.focus();
        return false;
    }

    //**IF LEAD CODE IS ENTERED **********************          
    //**CHECK IF ENTERED VALUE IS INTEGER**************
    if (!isNumeric(drpLeadTimecontrol.value)) {
        alert(msgsearch6);
        drpLeadTimecontrol.focus();
        return false;
    }

    //CHECK WHETHER LEADTIME IS POSITIVE
    if ((drpLeadTimecontrol.value) < 0) {
        alert(msgLT);
        drpLeadTimecontrol.focus();
        return false;
    }

    //***************************************************
    //****************Order control TIME  CONTROL *********************
    //***************************************************
    if (drpOrdercontrol == null) return false;
    checkres = (drpOrdercontrol.selectedIndex == 0 && ddpFreqcontrol.selectedIndex > 1);

    //***************************************************
    //**************** BS    CONTROL *********************
    if (drpBS.length > 0) {
        if (drpBScontrol == null) return false;
        checkres = (trimAll(drpBScontrol.value).length > 0);
        //'**CHECK BS Control CODE IS BLANK *********************
        if (!checkres) {
            alert(msgsearch4);
            drpBScontrol.focus();
            return false;
        }
        //**IF LEAD CODE IS ENTERED **********************
        else {
            //**CHECK IF ENTERED VALUE IS NUMERIC**************
            if (!isNumeric(drpBScontrol.value)) {
                alert(msgsearch7);
                drpBScontrol.focus();
                return false;
            }
            else {
                if (drpBScontrol.value < 0) {
                    alert(msgBS);
                    drpBScontrol.focus();
                    return false;
                }
            }
        }
    }

    //***************************************************
}

function validateWklyOrderingScheduleEntry(msgAtleastOne, ddlDept, ddlCat, ddlOrderDays) {
    ddldeptcontrol = document.getElementById(ddlDept);
    ddlcatcontrol = document.getElementById(ddlCat);
    ddlorderDayscontrol = document.getElementById(ddlOrderDays);

    if ((ddldeptcontrol.selectedIndex > 0 || ddlcatcontrol.selectedIndex > 0 || ddlorderDayscontrol.selectedIndex > 0) == false) {
        alert(msgAtleastOne);
        ddldeptcontrol.focus();
        return false;
    }
    return true;
}