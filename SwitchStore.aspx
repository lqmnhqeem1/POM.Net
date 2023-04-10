<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="SwitchStore.aspx.vb" Inherits="IM_POM_VB.Net.SwitchStore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Switch Store</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/pom.net.css" rel="stylesheet" type="text/css" />
     <link href="/css/styles.css?version=1" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="commonjs/common.js"></script>

    <script type="text/javascript" src="commonjs/rootlevelpage.js"></script>

    <script type="text/javascript" language="javascript">
            function checkKey(event)
            {
                var defcontrol;
                
                switch(event.keyCode)
                {
                    case 13:
                        defcontrol = document.getElementById('btnOk');
                        if(defcontrol) defcontrol.focus();
                        
                        break;
                    
                    case 27:
                        defcontrol = document.getElementById('btnCancel');
                        if(defcontrol) defcontrol.click();
                        
                        break;
                }
            }
    </script>
    <style type="text/css">
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
<body onclick="checkKey(event);">
    <div class="main">
         <form id="frmSwitchStore" runat="server" method="post">
        <center>
            <h1  class="mt-5 font-weight-bold " style="color: #eee;">POM.NET</h1>
                
                <asp:Image ID="Image1" runat="server" ImageUrl="../images/Giant_logo.png" AlternateText="Giant" />

            <div class="container bg-light mt-3"  style="border-style:solid; width:35%;">
                    <div>
                      
                            <asp:Label ID="lblUser" runat="server" CssClass="text-dark " Text="User: "></asp:Label>
                             <asp:Label ID="lblUserId" runat="server" CssClass="text-dark" ForeColor="black"
                                                            Font-Bold="true" Text=""></asp:Label>
                            <a id="lnkSignOut" runat="server">
                                <asp:Label ID="lblSignOut" runat="server" CssClass="btn btn-outline-danger ml-2" Font-Bold="true"
                                                                Text="Sign Out"></asp:Label>
                             </a>
                       
                        </div>
                   
                                           <hr />
                         <asp:Label ID="lblPageTile" runat="server" CssClass="h3 text-dark font-weight-bold d-flex justify-content-center" Text="Switch Store"></asp:Label>
                        <asp:Label ID="lblRoles" runat="server" CssClass="text-dark font-weight-bold d-flex justify-content-start ml-2" 
                            Text="Select a Store:"></asp:Label>
                        
                <div class="mt-3 mb-3">
                  
        
                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                 
                </div>

                 <div class="mb-3">
                        <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary" Text="Ok" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" />
                 </div>
                        

 

                </div>
        </center>
             <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Text="" Visible="false"></asp:Label>
                                     <asp:Label ID="lblStore" runat="server" CssClass="text-dark font-weight-bold mt-2" Text="Store:" Visible="false"></asp:Label>

             </form>
    </div>

</body>
</html>
