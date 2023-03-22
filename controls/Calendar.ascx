<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Calendar.ascx.vb" Inherits="IM_POM_VB.Net.CalendarControl" %>

<script type="text/javascript" language="javascript">
    function calendarShowHide(calcontainerid)
    {
        var calendarContainer = document.getElementById(calcontainerid);
        if(calendarContainer == null) return;
        toggleControlDisplay(calcontainerid);
    }
    
    function calendarOnFocus(calcontainerid)
    {
        var calendarContainer = document.getElementById(calcontainerid);
        if(calendarContainer) calendarContainer.style.display = "";
    }
    
    function calendarLostFocus(calcontainerid)
    {
        var calendarContainer = document.getElementById(calcontainerid);
        if(calendarContainer) calendarContainer.style.display = "none";
    }
    
    function keyDate(textbox, datesep)
    {
        var regx, retval;
        
        regx = /\d/;
        retval = regx.test(String.fromCharCode(window.event?event.keyCode:event.which));
        if(retval == true && (textbox.value.length == 2 || textbox.value.length == 5)) textbox.value += datesep;
        return retval;
    }
</script>
<table id="tblCalendarControl" cellpadding="0" cellspacing="0" style="font: normal 10px Verdana; margin:0px 0px 0px 0px; border:none 0px;">
    <tr>
        <td valign="bottom"><asp:TextBox ID="txtCalendarValue" runat="server" MaxLength="10" CssClass="normalTextBox"></asp:TextBox></td>
        <td valign="baseline"><asp:HyperLink ID="hlnkCalLink" runat="server" CssClass="normalHyperLink" ImageUrl="~/images/calendar.gif" BorderStyle="none"></asp:HyperLink></td>
    </tr>
    <tr>
        <td colspan="2">
            <iframe id="ifrmCalendar" runat="server" frameborder="0" style="width:180px; height:220px; position:absolute; display:none; z-index:1002;"></iframe>
        </td>
    </tr>
</table>
