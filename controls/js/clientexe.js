// Function to invoke specified executable at client machine with given command line parameters
// Author: Tanmoy Paul
function InvokeClientExe()
{
    var i, sArgs = "", retval, objControl;

    if (arguments.length < 17) return false;
    for(i = 17; i < arguments.length; i++)
        sArgs = (i == 17 ? "" : sArgs + "|") + arguments[i];
    
    retval = window.showModalDialog(arguments[14] + "?exename=" + arguments[15] + "&args=" + sArgs,
                                    "", "dialogWidth:150px; dialogHeight:30px; center:1; status:0; scroll:0; resizable:0; help:0;");

    if (retval >= 0 && retval < arguments.length)
    {
        if (arguments[retval].length > 0) alert(arguments[retval]);
    }
    else
    {
        alert("Unexpected Error Occured. Contact System Administrator.");
    }
    
    objControl = document.getElementById(arguments[16]);
    if (objControl)
    {
        switch(objControl.type)
        {
            case 'button', 'submit':
                if(retval == 0) objControl.click();
                break;
                
            case 'text', 'hidden':
                objControl.value = retval;
                break;
         }
    }
    return retval;
}
