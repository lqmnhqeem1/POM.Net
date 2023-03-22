function toggleControlDisplay(controlid) {
    controlobject = document.getElementById(controlid);

    if (controlobject == null) return;

    controlobject.style.display = (controlobject.style.display == "none" ? "" : "none");
}

function controlShowHide(controlid, showhideflag) {
    controlobject = document.getElementById(controlid);
    if (controlobject == null) return;

    controlobject.style.visibility = (showhideflag ? "visible" : "hidden");
}

function getQueryStringValue(key) {
    var i, keyval;

    if (!key) return null;

    keyval = window.location.search.substr(1).split("&");
    for (i = 0; i < keyval.length; i++) {
        if (keyval[i].split("=")[0].toLowerCase() == key) return keyval[i].split("=")[1];
    }

    return '';
}

function splitParam(data, index, delimiter) {
    var i = 0, s = 0, e = 0;

    if (trimAll(data).length == 0 || index < 1 || delimiter == null) return data;

    while (i < index) {
        s = s + e + (i == 0 ? 0 : 1);

        e = 0;
        e = data.substring(s, data.length).indexOf(delimiter);
        if (e < 0 && i == index - 1) {
            e = data.length;
            break;
        }
        else if (e < 0) break;

        i += 1;
    }

    return (e >= 0) ? (data.substring(s, s + e)) : "";
}

function clearTableContents(tableid) {
    tblContainer = document.getElementById(tableid);
    if (tblContainer == null) return;

    while (tblContainer.rows.length > 0)
        tblContainer.deleteRow(tblContainer.rows.length - 1);

}

function exclusiveRadio(formid, currentradioid) {
    var formobject = document.getElementById(formid);
    if (formobject == null || currentradioid == null) return;

    for (i = 0; i < formobject.length; i++)
        if (formobject.elements[i].type == "radio")
            formobject.elements[i].checked = (formobject.elements[i].id == currentradioid);
}

function countRadioSelected(formid) {
    var count = 0;

    var formobject = document.getElementById(formid);
    if (formobject == null) return -1;

    for (i = 0; i < formobject.length; i++)
        if (formobject.elements[i].type == "radio") count += (formobject.elements[i].checked ? 1 : 0);

    return count;
}

function countCheckBoxSelected(formid) {
    var i, count, formobject;

    formobject = document.getElementById(formid);
    if (!formobject) return -1;

    count = 0;
    for (i = 0; i < formobject.length; i++)
        if (formobject.elements[i].type == "checkbox") count += (formobject.elements[i].checked ? 1 : 0);

    return count;
}

function isBlank(controlid) {
    controlobject = document.getElementById(controlid);
    if (controlobject == null) return true;

    return (trimAll(controlobject.value).length == 0);
}
function mandatoryDropDownSelection(controlid) {
    controlobject = document.getElementById(controlid);
    if (controlobject == null) return false;

    return (controlobject.selectedIndex != 0);
}

function clearControlsOfType(controltype) {
    for (i = 0; i < document.forms.length; i++)
        for (j = 0; j < document.forms[i].elements.length; j++)
            if (document.forms[i].elements[j].type == controltype) document.forms[i].elements[j].value = "";
}

function clearFormControlsOfType(formid, controltype) {
    var formobject = document.getElementById(formid);
    if (formobject == null) return;

    for (i = 0; i < formobject.elements.length; j++)
        if (formobject.elements[i].type == controltype) formobject.elements[i].value = "";
}

function switchroleShowHide(bdivid, fdivid, showhideflag) {
    divcontrol = document.getElementById(bdivid);
    if (divcontrol == null) return;
    divcontrol.style.top = '0px';
    divcontrol.style.left = '0px';
    divcontrol.style.height = screen.height + 'px';
    divcontrol.style.width = screen.width + 'px';
    controlShowHide(bdivid, showhideflag);

    divcontrol = document.getElementById(fdivid);
    if (divcontrol == null) return;
    divcontrol.style.top = (screen.availHeight / 2 - 100) + 'px';
    divcontrol.style.top = '200px';
    divcontrol.style.left = (screen.availWidth / 2 - 200) + 'px';
    controlShowHide(fdivid, showhideflag);
}

function getElementByNameLike(controlname, controltype) {
}

function addClientMessage(displaymid, displayid, message) {
    floatmcontrol = document.getElementById(displaymid);
    floatcontrol = document.getElementById(displayid);
    if (floatmcontrol == null || floatcontrol == null || message == null) return;

    lastrow = floatcontrol.rows.length;
    newrow = floatcontrol.insertRow(lastrow);
    //newrow.class = ('d' + lastrow%2);
    newcell = newrow.insertCell(0);
    textnode = document.createTextNode(message);
    newcell.appendChild(textnode);

    controlShowHide(displaymid, true);

    return;
}

function leftTrim(sString) {
    if (sString == null) return sString;

    while (sString.substring(0, 1) == ' ')
        sString = sString.substring(1, sString.length);

    return sString;
}

function rightTrim(sString) {
    if (sString == null) return sString;

    while (sString.substring(sString.length - 1, sString.length) == ' ')
        sString = sString.substring(0, sString.length - 1);

    return sString;
}

function trimAll(sString) {
    if (sString == null) return sString;

    while (sString.substring(0, 1) == ' ')
        sString = sString.substring(1, sString.length);

    while (sString.substring(sString.length - 1, sString.length) == ' ')
        sString = sString.substring(0, sString.length - 1);

    return sString;
}

function isAlphaNumeric(controlvalue) {
    var reg_alphanum = /^[\w\d-\s]*$/;

    return controlvalue.match(reg_alphanum) == controlvalue;
}

function isNumeric(controlvalue) {
    return !isNaN(controlvalue);
}

function isInteger(controlvalue) {
    var reg_integer = /^\d*$/;

    if (reg_integer.exec(controlvalue) == null) return false;
    return (reg_integer.exec(controlvalue).lastIndex == controlvalue.length);
}

function isValidDirectoryPath(s) {
    var reg_path = /^(file:\/\/[a-zA-Z]:\\|[a-zA-Z]:\\|\\\\|\/|\\)?(\w+[\\\/]{1,1})*/

    return reg_path.exec(s.replace('\\', '/')).lastIndex == s.length || reg_path.exec(s).lastIndex == s.length;
}

function isValidEmailId(s) {
    var reg_email = /^[\w.-]+@[A-Z0-9.-]+\.(?:[A-Z]{2}|com|org|net|biz|info|name|aero|jobs|museum)(?:[,;]+[\w.-]+@[A-Z0-9.-]+\.(?:[A-Z]{2}|com|org|net|biz|info|name|aero|jobs|museum))*$/i
    return s.match(reg_email) == s;
}

function isDigit(theDigit) {
    var digitArray = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9'), i;

    for (i = 0; i < digitArray.length; i++)
        if (theDigit == digitArray[i]) return true;

    return false;
}

function isPositiveInteger(theString) {
    var theData = new String(theString), i;

    if (!isDigit(theData.charAt(0)) && theData.charAt(0) != '+') return false;

    for (i = 1; i < theData.length; i++)
        if (!isDigit(theData.charAt(i))) return false;

    return true;
}

function keySort(dropdownlist, caseSensitive) {
    //check the keypressbuffer attribute is defined on the dropdownlist
    var undefined;
    if (dropdownlist.keypressBuffer == undefined) dropdownlist.keypressBuffer = '';

    //get the key that was pressed
    var key = String.fromCharCode(window.event.keyCode);
    dropdownlist.keypressBuffer += key;

    //convert buffer to lower case
    if (!caseSensitive) dropdownlist.keypressBuffer = dropdownlist.keypressBuffer.toLowerCase();

    //find if it is the start of any of the options
    var optionsLength = dropdownlist.options.length;
    for (var n = 0; n < optionsLength; n++) {
        optionText = dropdownlist.options[n].text;
        if (!caseSensitive) optionText = optionText.toLowerCase();

        if (optionText.indexOf(dropdownlist.keypressBuffer, 0) == 0) {
            dropdownlist.selectedIndex = n;
            return false;   //cancel the default behaviour since we have selected our own value
        }
    }

    //reset initial key to be inline with default behaviour
    dropdownlist.keypressBuffer = key;
    return true;    // give default behaviour
}

function isDateInFormat(s, f) {
    var a1 = s.split("/");
    var a2 = s.split("-");
    var e = true;
    var na, d, m, y, v;

    if (a1.length != 3 && a2.length != 3) {
        e = false;
    }
    else {
        if (a1.length == 3) na = a1;
        if (a2.length == 3) na = a2;

        if (isPositiveInteger(na[0]) && isPositiveInteger(na[1]) && isPositiveInteger(na[2])) {
            if (f == 1) {
                d = na[1];
                m = na[0];
            }
            else {
                d = na[0];
                m = na[1];
            }

            y = na[2];
            if (((e) && (y < 1000) || y.length > 4)) e = false;
            if (e) {
                v = new Date(m + "/" + d + "/" + y);
                if (v.getMonth() != m - 1) e = false;
            }
        }
        else {
            e = false;
        }
    }

    return e;
}

function isDate(s) {
    return isDateInFormat(s, 0);
}

function compareDate(fdate, sdate) {
    if (!isDate(fdate) || !isDate(sdate)) return null;
    if (fdate.split("/")[0] < sdate.split("/")[0] || fdate.split("/")[1] < sdate.split("/")[1] || fdate.split("/")[2] < sdate.split("/")[2]) return -1;
    if (fdate.split("/")[0] == sdate.split("/")[0] && fdate.split("/")[1] == sdate.split("/")[1] && fdate.split("/")[2] == sdate.split("/")[2]) return 0;
    if (fdate.split("/")[0] > sdate.split("/")[0] || fdate.split("/")[1] > sdate.split("/")[1] || fdate.split("/")[2] > sdate.split("/")[2]) return 1;
}

function changeselectionGridItem(containerid, controlid, selectedcss, normalcss) {
    var objControl, objContainer;

    if (containerid == null || controlid == null || selectedcss == null || normalcss == null) return;

    objControl = document.getElementById(controlid);
    if (objControl && objControl.type == 'checkbox') {
        objContainer = document.getElementById(containerid);
        if (objContainer) objContainer.attributes['class'].value = "'" + (objControl.checked ? selectedcss : normalcss) + "'";
    }
    return;
}

function onlyCAPS(event) {
    var keynum = (window.event ? event.keyCode : event.which);

    if (keynum >= 97 && keynum <= 122) {
        if (window.event)
            event.keyCode = keynum - 32;
        else
            event.which = keynum - 32;
    }

    return true;
}

function maskNumeric(textbox, maxvalue) {
    var curpos, key, keychar, reg;

    reg = /\d|\./;
    key = window.event ? event.keyCode : event.which;
    keychar = String.fromCharCode(key);
    curpos = getCursorPos(textbox);

    return reg.test(keychar) && (parseFloat(textbox.value.substring(0, curpos) + keychar + textbox.value.substring(curpos)) <= maxvalue);
}

function maskInteger(textbox, maxvalue) {
    var curpos, key, keychar, reg;

    reg = /\d/;
    key = window.event ? event.keyCode : event.which;
    keychar = String.fromCharCode(key);
    curpos = getCursorPos(textbox);

    return reg.test(keychar) && (parseInt(textbox.value.substring(0, curpos) + keychar + textbox.value.substring(curpos)) <= maxvalue);
}

function maskCustom(textbox, maskregx, capson) {
    var curpos, keynum, keychar, regx;

    keynum = window.event ? event.keyCode : event.which;
    if (capson == true && keynum >= 97 && keynum <= 122) {
        if (window.event)
            event.keyCode = keynum - 32;
        else
            event.which = keynum - 32;
    }
    curpos = getCursorPos(textbox);
    keychar = textbox.value.substring(0, curpos) + String.fromCharCode(keynum) + textbox.value.substring(curpos);
    regx = new RegExp(maskregx);

    return regx.test(keychar);
}

function getCursorPos(textElement) {
    //save off the current value to restore it later,
    var sOldText = textElement.value;

    //create a range object and save off it's text
    var objRange = document.selection.createRange();
    var sOldRange = objRange.text;

    //set this string to a small string that will not normally be encountered
    var sWeirdString = '#%~';
    textElement.maxLength += sWeirdString.length;

    //insert the weirdstring where the cursor is at
    objRange.text = sOldRange + sWeirdString; objRange.moveStart('character', (0 - sOldRange.length - sWeirdString.length));

    //save off the new string with the weirdstring in it
    var sNewText = textElement.value;

    //set the actual text value back to how it was
    objRange.text = sOldRange;

    //look through the new string we saved off and find the location of
    //the weirdstring that was inserted and return that value
    for (i = 0; i <= sNewText.length; i++) {
        var sTemp = sNewText.substring(i, i + sWeirdString.length);
        if (sTemp == sWeirdString) {
            var cursorPos = (i - sOldRange.length);
            textElement.maxLength -= sWeirdString.length;
            return cursorPos;
        }
    }
    textElement.maxLength -= sWeirdString.length;
}

function datecompare(startDate, EndDate, msgDateCompare) {
    var a1 = startDate.split("/");
    var a2 = EndDate.split("/");

    if (a1.length == 0) { a1 = startDate.split("-"); }
    if (a2.length == 0) { a2 = EndDate.split("-"); }

    var objStartDate = new Date;
    var objEndDate = new Date();
    var StartDay;

    /*
    **************************
    Modified by Sachin Jain
    DCL 1538; 19-Sep-2008
    ***************************
    if (a1[0] == 31) 
    {
        StartDay = (a1[0])*30+(a1[0]-2);
    }
    else
    {
        StartDay = a1[0];
    }
    
    //objStartDate.setDate(a1[0]);
    objStartDate.setDate(StartDay);
    objStartDate.setMonth(a1[1]-1);
    objStartDate.setFullYear(a1[2]);
    
    var objEndDate = new Date();
    var EndDay;
    
    if (a2[0] == 31) 
    {
        EndDay = (a2[1])*30+(a2[1]-2);
    }
    else
    {
        EndDay = a2[0];
    }
    //objEndDate.setDate(a2[0]);    
    objEndDate.setDate(EndDay);
    objEndDate.setMonth(a2[1]-1);
    objEndDate.setFullYear(a2[2]);
    
    */

    objStartDate = new Date(a1[2], a1[1] - 1, a1[0]);
    objEndDate = new Date(a2[2], a2[1] - 1, a2[0]);

    if (objStartDate > objEndDate) {
        alert(msgDateCompare);
        return false;
    }
    else {
        return true;
    }
}
