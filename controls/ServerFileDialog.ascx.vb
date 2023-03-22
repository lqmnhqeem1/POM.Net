Imports System.Collections.Generic
Imports System.IO
Imports POM.Lib.UI
Imports POM.Lib.Data
Imports System.Xml
Imports POM.Lib.Log
Imports Microsoft.VisualBasic

Public Class ServerFileDialog
    Inherits System.Web.UI.UserControl

    Public Enum DisplayMode
        Collapsed
        Expanded
    End Enum

    <Flags()>
    Public Enum ItemType
        File = 1
        Folder = 2
    End Enum

    Public Class ServerFileItem
        Public ID As String
        Public Type As ItemType
        Public Path As String
        Public Name As String

        Public Sub New(ByVal itemID As Integer, ByVal itemType As ItemType, ByVal itemName As String, ByVal itemPath As String)
            ID = itemID
            Type = itemType
            Name = itemName
            Path = itemPath
        End Sub
    End Class

    Private Const C_CMDNAME As String = "C_CMDNAME"
    Private Const C_DISPLAYMODE As String = "C_DISPLAYMODE"
    Private Const C_ROOTPATH As String = "C_ROOTPATH"
    Private Const C_ITEMTYPE As String = "C_ITEMTYPE"
    Private Const C_FILEFILTER As String = "C_FILEFILTER"
    Private Const C_DATA As String = "C_DATA"

    Public Event FilesSelected As ServerFileEventHandler

    Public Property CssClass() As String
        Get
            Return btnServerFile.CssClass
        End Get
        Set(ByVal value As String)
            btnServerFile.CssClass = value
        End Set
    End Property

    Public Property Text() As String
        Get
            Return btnServerFile.Text
        End Get
        Set(ByVal value As String)
            btnServerFile.Text = value
        End Set
    End Property

    Public Property Mode() As DisplayMode
        Get
            Return CType(ViewState(C_DISPLAYMODE), DisplayMode)
        End Get
        Set(ByVal value As DisplayMode)
            ViewState(C_DISPLAYMODE) = value
        End Set
    End Property

    Public Property RootPath() As String
        Get
            If ViewState(C_ROOTPATH) Is Nothing OrElse CType(ViewState(C_ROOTPATH), String).Trim().Length = 0 Then _
                ViewState(C_ROOTPATH) = IIf(String.IsNullOrEmpty(Utility.UserStoreId) OrElse Utility.UserStoreId.Trim().Length = 0, ConfigurationManager.AppSettings("POMInFolder").Split("~")(0), ConfigurationManager.AppSettings("POMInFolder").Replace("~", Utility.UserStoreId))
            Return CType(ViewState(C_ROOTPATH), String)
        End Get
        Set(ByVal value As String)
            ViewState(C_ROOTPATH) = value
        End Set
    End Property

    Public Property DisplayType() As ItemType
        Get
            Return CType(ViewState(C_ITEMTYPE), ItemType)
        End Get
        Set(ByVal value As ItemType)
            ViewState(C_ITEMTYPE) = value
        End Set
    End Property

    Public Property FileFilter() As String
        Get
            Return CType(ViewState(C_FILEFILTER), String)
        End Get
        Set(ByVal value As String)
            ViewState(C_FILEFILTER) = value
        End Set
    End Property

    Public Property CommandName() As String
        Get
            Return CType(ViewState(C_CMDNAME), String)
        End Get
        Set(ByVal value As String)
            ViewState(C_CMDNAME) = value
        End Set
    End Property

    Public Property Height() As String
        Get
            Return divFileList.Style("height")
        End Get
        Set(ByVal value As String)
            divFileList.Style("height") = value
        End Set
    End Property

    Public Property Width() As String
        Get
            Return divFileList.Style("width")
        End Get
        Set(ByVal value As String)
            divFileList.Style("width") = value
        End Set
    End Property

    Protected Overridable Sub OnFilesSelected(ByVal e As ServerFileEventArgs)
        RaiseEvent FilesSelected(Me, e)
    End Sub

    Public Sub LoadServerFiles()

        Dim lItem As List(Of ServerFileItem),
            da As New DataAccess(),
            ds As DataSet,
            xNode As XmlNode,
            xData As XmlDocument

        lItem = GetSelectedItems()
        ds = da.ExecuteSqlQueryDataSet(String.Format("EXEC usp_ServerFile_Search '{0}', {1}, '{2}'", RootPath, CType(DisplayType, Integer), FileFilter).Replace("\\", "\"))
        tvServerFile.Nodes.Clear()
        xdsServer.Data = String.Empty
        For rndex As Integer = 0 To ds.Tables(0).Rows.Count - 1
            xdsServer.Data &= Convert.ToString(ds.Tables(0).Rows(rndex)(0))
        Next

        If lItem.Count > 0 Then
            xData = New XmlDocument()
            xData.LoadXml(xdsServer.Data)

            For sndex As Integer = 0 To lItem.Count - 1
                xNode = xData.SelectSingleNode(String.Format("/ServerFiles//*[@FullPath='{0}']", lItem(sndex).Path))
                If Not xNode Is Nothing Then _
                    xNode.Attributes("Check").Value = "1"
            Next
            xdsServer.Data = xData.OuterXml()
        End If

        ViewState(C_DATA) = xdsServer.Data
        tvServerFile.DataSourceID = xdsServer.ID
        tvServerFile.DataBind()
        upTree.Update()
    End Sub

    Public Function GetSelectedItems() As List(Of ServerFileItem)

        Dim xNode As XmlNode,
            xData As XmlDocument,
            lItem As New List(Of ServerFileItem)

        If tvServerFile.CheckedNodes.Count > 0 Then
            xData = New XmlDocument()
            xData.LoadXml(CType(ViewState(C_DATA), String))

            For index As Integer = 0 To tvServerFile.CheckedNodes.Count - 1
                xNode = xData.SelectSingleNode(String.Format("/ServerFiles//*[@ID={0}]", tvServerFile.CheckedNodes(index).Value))
                lItem.Add(New ServerFileItem(xNode.Attributes("ID").Value, IIf(xNode.Name.CompareTo("ServerFile") = 0, ItemType.File, ItemType.Folder), xNode.Attributes("Name").Value, xNode.Attributes("FullPath").Value))
            Next
        End If

        Return lItem
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                btnServerFile.OnClientClick = "controlShowHide('" & divFileList.ClientID & "', true); return false;"
                ibtnOk.OnClientClick = "controlShowHide('" & divFileList.ClientID & "', false); return true;"
                ibtnCancel.OnClientClick = "controlShowHide('" & divFileList.ClientID & "', false); return false;"

                If Mode = DisplayMode.Expanded Then
                    btnServerFile.Visible = False
                    tdButton.Visible = False
                    divFileList.Style("visibility") = "visible"
                    divFileList.Style("width") = "100%"
                    divFileList.Style.Remove("float")
                    divFileList.Style.Remove("z-index")
                    divFileList.Style.Remove("position")
                End If
            End If
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error loading ServerFileDialog", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    Protected Sub Button_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnOk.Click

        Dim xNode As XmlNode,
            xData As XmlDocument,
            sfe As ServerFileEventArgs

        Select Case CType(sender, ImageButton).CommandName
            Case "SELECT"
                If tvServerFile.CheckedNodes.Count > 0 Then
                    xData = New XmlDataDocument()
                    xData.LoadXml(CType(ViewState(C_DATA), String))

                    sfe = New ServerFileEventArgs()
                    For index As Integer = 0 To tvServerFile.CheckedNodes.Count - 1
                        xNode = xData.SelectSingleNode(String.Format("/ServerFiles//*[@ID={0}]", tvServerFile.CheckedNodes(index).Value))
                        sfe.SelectedItems.Add(New ServerFileItem(xNode.Attributes("ID").Value, IIf(xNode.Name.CompareTo("ServerFile") = 0, ItemType.File, ItemType.Folder), xNode.Attributes("Name").Value, xNode.Attributes("FullPath").Value))
                    Next
                    OnFilesSelected(sfe)
                End If
        End Select
    End Sub

    Protected Sub TreeView_TreeNodeDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvServerFile.TreeNodeDataBound

        Try
            e.Node.ShowCheckBox = Switch(CType(e.Node.DataItem, XmlElement).Name.CompareTo("ServerFile") = 0, IIf((DisplayType And ItemType.File) = ItemType.File, True, False), CType(e.Node.DataItem, XmlElement).Name.CompareTo("ServerDir") = 0, IIf((DisplayType And ItemType.Folder) = ItemType.Folder, True, False))
            If e.Node.ShowCheckBox Then _
                e.Node.Checked = IIf(CType(e.Node.DataItem, XmlElement).Attributes("Check").Value = "1", True, False)

        Catch ex As Exception
            Dim objEx As New ApplicationException("Error binding TreeNode", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)
        End Try
    End Sub

    Protected Sub Timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tServerFile.Tick

        Try
            LoadServerFiles()
        Catch ex As Exception
            Dim objEx As New ApplicationException("Error refreshing ServerFileDialog", ex)

            objEx.Source = IIf(IsNothing(ex.InnerException), Reflection.Assembly.GetExecutingAssembly.GetName(False).Name, ex.Source)
            ExceptionLog.Log(objEx)
        End Try
    End Sub
End Class

Public Class ServerFileEventArgs
    Inherits EventArgs

    Private items As List(Of ServerFileDialog.ServerFileItem)

    Public Sub New()
        items = New List(Of ServerFileDialog.ServerFileItem)
    End Sub

    Public Sub New(ByVal collection As IEnumerable(Of ServerFileDialog.ServerFileItem))
        items = New List(Of ServerFileDialog.ServerFileItem)(collection)
    End Sub

    Public ReadOnly Property Selection() As String()
        Get
            Return items.ConvertAll(Of String)(New Converter(Of ServerFileDialog.ServerFileItem, String)(AddressOf ExtractPathFromFileItem)).ToArray()
        End Get
    End Property

    Public ReadOnly Property SelectedItems() As List(Of ServerFileDialog.ServerFileItem)
        Get
            Return items
        End Get
    End Property

    Private Function ExtractPathFromFileItem(ByVal item As ServerFileDialog.ServerFileItem) As String
        Return item.Path
    End Function

End Class

Public Delegate Sub ServerFileEventHandler(ByVal sender As Object, ByVal e As ServerFileEventArgs)
