<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SwitchRole.aspx.vb" Inherits="IM_POM_VB.Net.SwitchRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Switch Role</title>
    <link href="css/pom.net.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="commonjs/common.js"></script>

    <script type="text/javascript" src="commonjs/rootlevelpage.js"></script>

        <link href="../css/pom.net.css" type="text/css" rel="stylesheet" />
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/styles.css?version=1" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
            function checkKey(event)
            {
                switch(event.keyCode)
                {
                    case 13:
                        document.getElementById('btnSwitchRole').focus();
                        break;
                }
            }
    </script>
    <style>
        div.main {
    background: #5fb0e4;
    background: -moz-radial-gradient(center, ellipse cover, #0264d6 1%, #1c2b5a 100%);
    background: -webkit-gradient(radial, center center, 0px, center center, 100%, color-stop(1%,#0264d6), color-stop(100%,#1c2b5a));
    background: -webkit-radial-gradient(center, ellipse cover, #0264d6 1%,#1c2b5a 100%);
    background: -o-radial-gradient(center, ellipse cover, #0264d6 1%,#1c2b5a 100%);
    background: -ms-radial-gradient(center, ellipse cover, #0264d6 1%,#1c2b5a 100%);
    background: radial-gradient(ellipse at center, #7db97e 1%,#1c2b5a 100%);
    filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#0264d6', endColorstr='#1c2b5a',GradientType=1 );
    height: calc(100vh);
    width: 100%;
}
    </style>
</head>
<body onkeydown="checkKey(event)">
    
    <div class="main">
        <form id="frmSwitchRole" runat="server" method="post">

            <center>
                <h1  class="mt-5 font-weight-bold " style="color: #eee;">POM.NET</h1>
                
                <asp:Image ID="imgGiant" runat="server" ImageUrl="../images/Giant_logo.png" AlternateText="Giant" />

                <div class="container bg-light w-75 mt-3"  style="border-style:solid">
                    <div class="row ">
                        <div class="col col-6 ">
                            <asp:Label ID="lblStore" runat="server" CssClass="" Text="Store: "></asp:Label>
                                            <asp:Label ID="lblStoreId" runat="server" CssClass="l" Text="" Font-Bold="true"
                                                ForeColor="black"></asp:Label>
                        </div>
                           <div class="col col-6 ">
                               <asp:Label ID="lblUser" runat="server" CssClass="l" Text="User: "></asp:Label>
                                                        <asp:Label ID="lblUserId" runat="server" CssClass="mr-3" ForeColor="black"
                                                            Font-Bold="true" Text=""></asp:Label>
                               <a id="lnkSignOut" runat="server">
                                <asp:Label ID="lblSignOut" runat="server"  class="btn btn-outline-danger btn-sm" Font-Bold="true" Text="Sign Out"></asp:Label>
                           <%--  <asp:Image ID="imgSignOut" runat="server" ImageUrl="~/images/logout.gif" AlternateText="" />--%>

                            </a>
                           </div>
                        </div>
                   
                                           <hr />
                         <asp:Label ID="lblPageTile" runat="server" CssClass="h3 text-dark font-weight-bold d-flex justify-content-center" Text="Switch Role"></asp:Label>
                        <asp:Label ID="lblRoles" runat="server" CssClass="text-dark font-weight-bold d-flex justify-content-start ml-2" 
                            Text="Select Role:"></asp:Label>
 
                     <asp:DataGrid ID="dgRole" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-borderless" >
                                    <HeaderStyle CssClass="border border-dark" />
                                    <ItemStyle CssClass="table-secondary" />
                                    <AlternatingItemStyle CssClass="table-light" />
                                    <EditItemStyle CssClass="" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="optRole" runat="server" CssClass="normalRadioButton" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "roleid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRoleDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "roledesc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                             <asp:Button ID="btnSwitchRole" runat="server" CssClass="btn btn-primary mb-2" Text="Ok" Visible="false" />

                </div>
            
            </center>
                       <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Visible="false"></asp:Label>
                 <hr id="hrAbButton" runat="server" visible="false" />
                </form>
               </div>
  
                

</body>
</html>
