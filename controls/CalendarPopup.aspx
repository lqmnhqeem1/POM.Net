<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalendarPopup.aspx.vb" Inherits="IM_POM_VB.Net.CalendarPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calendar</title>
    <style type="text/css">
        body.calendarContainer, table..calendarContainer
        {
            width: 180px;
            height: 200px;
            margin: 0px 0px 0px 0px;
            border: none;
            text-align: left;
            vertical-align: top;
        }
        
        table.normalCalendar, .normalCalendar
        {
	        border: normal 1px #ff66cc;
	        background-color:#66cc99;
	        color:#663399;
	        font: normal 8px Verdana;
	        width: 180px;
	        text-align:center;
        }

        tr.normalCalendarTitle td, .normalCalendarTitle
        {
	        background-color: #588d59;
	        font: bold 9pt Verdana;
	        color:#ffffcc;
        }

        td.normalCalendarSelectedDay, .normalCalendarSelectedDay
        {
	        background-color:#CCCCFF;
	        font-weight:bold;
        }

        tr.normalCalendarDayHeader td, .normalCalendarDayHeader
        {
	        background-color: #e2fae2;
	        cursor:default;
	        font: bold 10px Verdana;
	        text-align:center;
	        border-bottom:1px solid #808080;
        }

        tr.normalCalendarClose td, .normalCalendarClose
        {
	        background-color: #e2fae2;
	        cursor: default;
	        font: bold 10px Verdana;
	        text-align: right;
	        border-bottom: 1px solid #808080;
        }
    </style>
    <script type="text/javascript" src="../commonjs/common.js"></script>
    <script type="text/javascript" language="javascript">
        function setSelectedDate(datevalue)
        {
            var controlid, containerid, controlobject;
            
            if(! datevalue) return;
            
            controlid = getQueryStringValue("codeentry");
            controlobject = window.top.document.getElementById(controlid);
            if(controlobject) controlobject.value = datevalue;
            
            containerid = getQueryStringValue("container");
            controlobject = window.top.document.getElementById(containerid);
            if(controlobject) controlobject.style.display = "none";
        }
        
        function closeCalendar()
        {
            var containerid, controlobject;
            
            containerid = getQueryStringValue("container");
            if(containerid && containerid.length > 0)
            {
                controlobject = window.top.document.getElementById(containerid);
                if(controlobject) controlobject.style.display = "none";
            }
        }
    </script>
</head>
<body class="calendarContainer">
    <form id="frmCalendar" runat="server">
        <table class="calendarContainer" cellpadding="0" cellspacing="0" border="0">
            <tr class="normalCalendarClose"><td><a onclick="javascript:closeCalendar();"><img alt="Close" style="border:none;"  src="../images/calx.gif"/></a></td></tr>
            <tr>
                <td>
                    <asp:Calendar ID="clPopupCalendar" runat="server" CssClass="normalCalendar" Height="200px" DayNameFormat="FirstLetter" ShowGridLines="true">
                        <TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>
                        <SelectorStyle BackColor="#FFCC66"></SelectorStyle>
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
                        <DayHeaderStyle Height="1px" CssClass="normalCalendarDayHeader"></DayHeaderStyle>
                        <SelectedDayStyle CssClass="normalCalendarSelectedDay"></SelectedDayStyle>
                        <TitleStyle CssClass="normalCalendarTitle" BackColor="#6699cc"></TitleStyle>
                        <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
                    </asp:Calendar>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
