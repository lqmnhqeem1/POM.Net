// function for client side validations on login page
// Author: Tanmoy Paul 02 Aug 2006
function validateLoginEntry(msguser1, msguser2, msgpass1, msgstore1, txtuser, txtpass, ddlstore) {
    if (msguser1 == null || msguser2 == null || msgpass1 == null || msgstore1 == null) return false;

    txtusercontrol = document.getElementById(txtuser);
    txtpasscontrol = document.getElementById(txtpass);
    ddlstorecontrol = document.getElementById(ddlstore);
    if (txtusercontrol == null || txtpasscontrol == null || ddlstorecontrol == null) return false;

    // validate user name
    if (trimAll(txtusercontrol.value).length == 0) {
        alert(msguser1);
        txtusercontrol.focus();
        return false;
    }
    useridchunk = txtusercontrol.value.split('\\');
    for (j = 0; j < useridchunk.length; j++) {
        useridsubchunk = useridchunk[j].split('\.');
        for (i = 0; i < useridsubchunk.length; i++)
            if (!isAlphaNumeric(useridsubchunk[i])) {
                alert(msguser2);
                txtusercontrol.focus();
                return false;
            }
    }

    // validate password
    if (trimAll(txtpasscontrol.value).length == 0) {
        alert(msgpass1);
        txtpasscontrol.focus();
        return false;
    }

    // validate store
    if (ddlstorecontrol.selectedIndex == 0) {
        alert(msgstore1);
        ddlstorecontrol.focus();
        return false;
    }

    return true;
}

// function for client side validations on switch role page
// Author: Tanmoy Paul 02 Aug 2006
function validateSwitchRoleEntry(formid, msgrole1) {
    if (countRadioSelected(formid) <= 0) {
        alert(msgrole1);
        return false;
    }

    return true;
}

// function for client side validations on switch store page
// Author: Tanmoy Paul 02 Aug 2006
function validateSwitchStoreEntry(controlid, dismsg) {
    var controlobject;

    if (controlid && dismsg) {
        controlobject = document.getElementById(controlid);
        if (controlobject) {
            if (!mandatoryDropDownSelection(controlid)) {
                alert(dismsg);
                return false;
            }
            else
                return true;
        }
    }

    return false;
}
