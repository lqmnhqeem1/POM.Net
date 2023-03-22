function openListOfValues(lovpage, controlname, lovid, selectedparamid, additionaldata, additionalfilter, ondatapopulate, opmode, initiator)
{
    var padditionalfilter, i, controlobject;
    var partialentry = document.getElementById(controlname);
    
    if(additionalfilter.split('|').length > 0 && additionalfilter.length > 0)
    {
        padditionalfilter = additionalfilter.split('|');
        
        additionalfilter = '';
        for(i= 0; i< padditionalfilter.length; i++)
        {
            if(padditionalfilter[i].split('=').length == 2)
            {
                controlobject = document.getElementById(padditionalfilter[i].split('=')[1]);
                if(controlobject != null)
                {
                    switch(controlobject.type)
                    {
                        case 'text':
                            additionalfilter += (additionalfilter.length >0? '|': '') + padditionalfilter[i].split('=')[0] + '=' + controlobject.value;
                            break;
                        
                        case 'radio':
                            additionalfilter += (additionalfilter.length >0? '|': '') + padditionalfilter[i].split('=')[0] + '=' + (controlobject.checked?1:0);
                            break;
                            
                        case 'checkbox':
                            additionalfilter += (additionalfilter.length >0? '|': '') + padditionalfilter[i].split('=')[0] + '=' + controlobject.checked;
                            break;
                        
                        case 'select-one':
                            additionalfilter += (additionalfilter.length >0? '|': '') + padditionalfilter[i].split('=')[0] + '=' + controlobject.value;
                            break;
                            
                        case 'hidden':
                            additionalfilter += (additionalfilter.length >0? '|': '') + padditionalfilter[i].split('=')[0] + '=' + controlobject.value;
                            break;
                    }
                }
            }
        }
    }
    else
    {
        additionalfilter = '';
    }
    
    ondatapopulate = trimAll(ondatapopulate);
    opmode = trimAll(opmode);
    
    window.open(lovpage + "?codeentry=" + controlname + 
                "&lovcode=" + lovid + 
                "&selectedparam=" + selectedparamid + 
                "&additionaldata=" + additionaldata + 
                "&additionalfilter=" + additionalfilter + 
                "&partialentry=" + (partialentry == null ? '' : partialentry.value) +
                "&ondatapopulate=" + ondatapopulate +
                "&opmode=" + opmode + 
                "&event=" + initiator, 
                "ListOfValues", "width=980, height=650, left=140, top=100, status=0, scrollbars=1, resizable=1, alwaysRaised=1, dependent=1");
    return false;
}

function autoOpenLOV(textControlId, btnControlId)
{
    var objTextControl, objBtnControl;
    
    objTextControl = document.getElementById(textControlId);
    if(objTextControl)
    {
        if(trimAll(objTextControl.value).length >= 0)
        {
            objBtnControl = document.getElementById(btnControlId);
            eval(objBtnControl.autoscript);
        }
    }
    
    return true;
}

function validateListOfValuesEntry()
{

    var i, msgsearch, returnval, controlobject;
    
    if(arguments.length < 1) return false;
    
    msgsearch = arguments[0];
    returnval = false;
    
    
    for(i= 1; i< arguments.length; i+=3)
        returnval = (returnval || !isBlank(arguments[i]));
    
    if(!returnval)
    {
        alert(arguments[0]);
        return false;
    }
    
    for(i= 1; i< arguments.length; i+=3)
    {
        returnval = true;
        controlobject = document.getElementById(arguments[i]);
        
        if(controlobject != null && trimAll(controlobject.value).length > 0)
        {
            switch(arguments[i+1])
            {
                case 'date':
                    returnval = isDate(controlobject.value);
                    break;
                
                case 'integer':
                    returnval = isInteger(parseInt(controlobject.value));
                    break;
                
                case 'numeric':
                    returnval = isNumeric(controlobject.value);
                    break;
                
                case 'alpha':
                    returnval = isAlphaNumeric(controlobject.value);
                    break;

                default:
                    returnval = true;
            }
        }
        
        if(!returnval)
        {
            alert(arguments[i+2]);
            controlobject.focus();
            return false;
        }
    }
    return true;
}

function clearSearchParams(formid)
{
    formobject = document.getElementById(formid);
    
    for(i= 0; i< formobject.elements.length; i++)
        if(formobject.elements[i].type == 'text') formobject.elements[i].value = '';
    
    return false;
}

function storeSearchParams(formid, hiddencontrolid)
{
    var strval = '';
    var hiddencontrol = document.getElementById(hiddencontrolid);
    var formobject = document.getElementById(formid);
    
    for(i= 0; i< formobject.elements.length; i++)
        if(formobject.elements[i].type == 'text') strval = strval + (strval.length == 0 ? '' : '^') + formobject.elements[i].id + '~' + formobject.elements[i].value;
    
    hiddencontrol.value = strval;
    return;
}

function radioClicked(formid, radioid, hiddencontrolid, rowindex)
{
    var strval = '';
    var hiddencontrol = document.getElementById(hiddencontrolid);
    var formobject = document.getElementById(formid);
    
    exclusiveRadio(formid, radioid);
    dglov = document.getElementById('dgLOV');

    for(i= 1; i< dglov.rows[rowindex].cells.length; i++)
        strval = strval + (trimAll(strval).length == 0 ? '' : '^') + dglov.rows[rowindex].cells[i].innerHTML;
    
    hiddencontrol.value = strval;
}

function passonData()
{
    var i, controlobject;
    
    if(arguments.length%2 != 0) return;
    
    for(i= 0; i< arguments.length; i+=2)
    {
        controlobject = window.opener.document.getElementById(arguments[i]);
        if(controlobject != null)
        {
            switch(controlobject.type)
            {
                case 'text':
                    controlobject.value = arguments[i+1];
                    break;
                
                case 'hidden':
                    controlobject.value = arguments[i+1];
                    break;
                    
                default:
                    controlobject.innerHTML = arguments[i+1];
                    break;
            }
        }
    }
    
    return;
}

function onDataPopulate(controlid)
{
    var objControl;
    
    if(controlid == null) return;
    
    objControl = window.opener.document.getElementById(controlid);
    if(objControl != null)
    {
        switch(objControl.type)
        {
            case 'button':
                objControl.click();
        }
    }
    
    return;
}
