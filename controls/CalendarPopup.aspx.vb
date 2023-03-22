Public Class CalendarPopup
    Inherits System.Web.UI.Page

    Protected Sub clPopupCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles clPopupCalendar.SelectionChanged
        CType(Page.Header, HtmlHead).Controls.Add(New LiteralControl("<script type=""text/javascript"" language=""javascript"">setSelectedDate('" & clPopupCalendar.SelectedDate.ToString("dd/MM/yyyy") & "');</script>"))
    End Sub

End Class