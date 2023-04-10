<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="IM_POM_VB.Net.Login" %>
<%-- <asp:Content ID="Content1" ContentPlaceHolderID="tContent" runat="server">
    
    <h1 class="display-5">Home page</h1>
        <br />
        <div class="card">
            <h5 class="card-header font-weight-light">requirements</h5>
            <div class="card-body">
                <ul>
                    <li>jquery</li>
                    <li>bootstrap 4.3</li>
                    <li>fontawesome</li>
                </ul>
            </div>
        </div>
</asp:Content>--%>


<!------ Include the above in your HEAD tag ---------->
<!-- This is a very simple parallax effect achieved by simple CSS 3 multiple backgrounds, made by http://twitter.com/msurguy -->
<html>
<head runat="server">
    <%-- <script src="/js/jquery-3.2.1.min.js"></script>
    <link href="/css/bootstrap.min.css" rel="stylesheet">
    <link href="/css/style.css" rel="stylesheet" type="text/css">
    <script src="/js/bootstrap.min.js"></script>
    <script src="/js/util.js"></script>
    <script src="/js/index.js"></script>--%>

    <%--<link href="css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/TweenLite.min.js"></script>--%>
    
    <link href="../css/pom.net.css" type="text/css" rel="stylesheet" />
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/styles.css?version=1" rel="stylesheet" type="text/css" />
    <style type="text/css">
	    body{
        /*background: url(img/store_bkg.png);*/
        background-repeat: no-repeat;
        background-size: 100%;
	    background-color: white;
        /*background: url(img/default.png),url(img/default.png),url(img/default.png);    */
    }

    .vertical-offset-100{
        padding-top:100px;
    }

    @charset "utf-8";




    /*div.main{
        background: #0264d6;*/ /* Old browsers */
    /*background: -moz-radial-gradient(center, ellipse cover,  #0264d6 1%, #1c2b5a 100%);*/ /* FF3.6+ */
    /*background: -webkit-gradient(radial, center center, 0px, center center, 100%, color-stop(1%,#0264d6), color-stop(100%,#1c2b5a));*/ /* Chrome,Safari4+ */
    /*background: -webkit-radial-gradient(center, ellipse cover,  #0264d6 1%,#1c2b5a 100%);*/ /* Chrome10+,Safari5.1+ */
    /*background: -o-radial-gradient(center, ellipse cover,  #0264d6 1%,#1c2b5a 100%);*/ /* Opera 12+ */
    /*background: -ms-radial-gradient(center, ellipse cover,  #0264d6 1%,#1c2b5a 100%);*/ /* IE10+ */
    /*background: radial-gradient(ellipse at center,  #0264d6 1%,#1c2b5a 100%);*/ /* W3C */
    /*filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#0264d6', endColorstr='#1c2b5a',GradientType=1 );*/ /* IE6-9 fallback on horizontal gradient */
    /*height:calc(100vh);
    width:100%;
    }*/

    [class*="fontawesome-"]:before {
      font-family: 'FontAwesome', sans-serif;
    }

    /* ---------- GENERAL ---------- */

    * {
      box-sizing: border-box;
        margin:0px auto;

      &:before,
      &:after {
        box-sizing: border-box;
      }

    }

    body {
   
        color: #606468;
      font: 87.5%/1.5em 'Open Sans', sans-serif;
      margin: 0;
    }

    a {
	    color: #eee;
	    text-decoration: none;
    }

    a:hover {
	    text-decoration: underline;
    }

    input {
	    border: none;
	    font-family: 'Open Sans', Arial, sans-serif;
	    font-size: 14px;
	    line-height: 1.5em;
	    padding: 0;
	    -webkit-appearance: none;
    }

    p {
	    line-height: 1.5em;
    }

    .clearfix {
      *zoom: 1;

      &:before,
      &:after {
        content: ' ';
        display: table;
      }

      &:after {
        clear: both;
      }

    }
/*
    .container {
      left: 50%;
      position: fixed;
      top: 50%;
      transform: translate(-50%, -50%);
    }*/

    /* ---------- LOGIN ---------- */

    #login form{
	    width: 250px;
    }
    #login, .logo{
        display:inline-block;
        width:40%;
	    align-self: center;
    }
    #login{
    border-left:1px solid #fff;
      padding: 0px 22px;
      width: 59%;
    }
    .logo{
      color:#fff;
      font-size:50px;
      line-height: 125px;
      background-image: url(../images/Giant_logo.png);
      /*background-repeat: no-repeat;*/
      
    }

    #login form span.fa {
	    background-color: #fff;
	    border-radius: 3px 0px 0px 3px;
	    color: #000;
	    display: block;
	    float: left;
	    height: 40px;
        font-size:24px;
	    line-height: 50px;
	    text-align: center;
	    width: 50px;
    }

    #login form input {
	    height: 40px;
    }
    fieldset{
        padding:0;
        border:0;
        margin: 0;

    }
    #login form input[type="text"], input[type="password"] {
	    background-color: #fff;
	    border-radius: 0px 3px 3px 0px;
	    color: #000;
	    margin-bottom: 1em;
	    padding: 0 16px;
	    width: 250px;
    }

    #login form input[type="submit"] {
      border-radius: 3px;
      -moz-border-radius: 3px;
      -webkit-border-radius: 3px;
      background-color: #000000;
      color: #eee;
      font-weight: bold;
      /* margin-bottom: 2em; */
      text-transform: uppercase;
      padding: 5px 10px;
      height: 30px;
    }

    #login form input[type="submit"]:hover {
	    background-color: #d44179;
    }

    #login > p {
	    text-align: center;
    }

    #login > p span {
	    padding-left: 5px;
    }
    .middle {
      display: flex;
      width: 600px;
    }

    .customDropDown {
        font-family: 'Open Sans', Arial, sans-serif;
        font-size: 14px;
        line-height: 1.5em;
    }

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


    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(document).mousemove(function (e) {
                TweenLite.to($('body'),
                    .5,
                    {
                        css:
                        {
                            backgroundPosition: "" + parseInt(event.pageX / 8) + "px " + parseInt(event.pageY / '12') + "px, " + parseInt(event.pageX / '15') + "px " + parseInt(event.pageY / '15') + "px, " + parseInt(event.pageX / '30') + "px " + parseInt(event.pageY / '30') + "px"
                        }
                    });
            });
        });

        function checkKey(event) {
            var controlobject;

            switch (event.keyCode) {
                case 27:
                    controlobject = document.getElementById('btnCancel');
                    if (controlobject != null) controlobject.click();
                    break;
            }

            return false;
        }
    </script>--%>
</head>

<body onkeydown="checkKey(event);">
<%--<div class="container">
    <div class="row vertical-offset-100">
    	<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Log in</h3>
                    <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Text="" Visible="false"></asp:Label>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form" runat="server">
                    <fieldset>
			    	  	<div class="form-group">
			    		    <asp:Label ID="lblUserName" runat="server" CssClass="normalLabel" Text="User Name"></asp:Label>
			    		</div>
                        <div class="form-group">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="normalTextBox" TabIndex="1" Width="70%"></asp:TextBox>
                        </div>
			    		<div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" CssClass="normalLabel" Text="Password"></asp:Label>
			    		</div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="normalTextBox" TabIndex="2" Width="70%" TextMode="password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblStore" runat="server" CssClass="normalLabel" Text="Store"></asp:Label>
			    		</div>
                        <div>
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="normalDropDownList" TabIndex="3" Width="100%"></asp:DropDownList>
                        </div>
                        <br>
                        <asp:Button id="btnLogin" runat="server" CssClass="searchButton" Text="Log In" CausesValidation="false"/>
                        <asp:Button id="btnCancel" runat="server" CssClass="resetButton" Text="Cancel" OnClientClick="javascript:window.close();" CausesValidation="false"/>
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>--%>
    <div class="main" >   
	<div class="mt-5" style="">
		<center>
            <h1  class="mt-2 font-weight-bold " style="color: #eee;">POM.NET</h1>
			<div class="container" zoom="100%">
                <div class="row justify-content-sm-center">
                    <form accept-charset="UTF-8" role="form" runat="server">
                         <img src="../images/Giant_logo.png" />
					<div class="mt-3"> 
                        <div id="altMessage" runat="server" class="alert alert-danger w-75" role="alert" visible="false">
                           <asp:Label ID="lblMessage" runat="server" CssClass="redLabel" Text="" Visible="false"></asp:Label>
                        </div>
						<div class="w-75">
                            <div style="color: #eee;">
                                <asp:Label ID="lblUserName" CssClass="d-flex justify-content-start" runat="server" Text="User Name"></asp:Label>
                            </div>
                            <asp:TextBox ID="txtUserName" Class="form-control d-flex justify-content-start" Placeholder="Domain\UserId" runat="server"></asp:TextBox>
						</div> <!-- JS because of IE support; better: placeholder="Username" -->
						<div class="mt-2">
                            <div class="w-75" style="color: #eee;">
                                <asp:Label ID="lblPassword"  CssClass="d-flex justify-content-start" runat="server" Text="Password"></asp:Label>
                            </div>
                            <asp:TextBox ID="txtPassword" CssClass="form-control d-flex justify-content-start w-75" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox>
						</div> <!-- JS because of IE support; better: placeholder="Password" -->
                        <div class="w-75">
                            <div style="color: #eee;">
                                <asp:Label ID="lblStore"  CssClass="d-flex justify-content-start" runat="server"  Text="Store"></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddlStore" CssClass="form-control d-flex justify-content-start "  runat="server" ></asp:DropDownList>
						</div> <!-- JS because of IE support; better: placeholder="Password" -->
						<div>
                        <div class="mt-4">
                            <asp:Button id="btnLogin" runat="server" CssClass="btn btn-primary" Text="Log In" CausesValidation="false"/>
                            <asp:Button id="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-dark" CausesValidation="false"/>
                        </div>
						</div>
					</div>
                    <h5 class="mt-2 font-weight-bold " style="color: #eee;">Ver:1.1.0</h5>
					<div class="clearfix"></div>
				</form>

                </div>
				
				<div class="clearfix"></div>
			</div> <!-- end login -->

		</center> 
	</div>
    </div>
     
</body>
 <%--   <script type="text/javascript" src="../commonjs/common.js"></script>
    <script type="text/javascript" src="/js/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.min.js"></script>--%>
    <%--<script type="text/javascript" src="/js/util.js"></script>--%>
  <%--  <script type="text/javascript" src="/js/index.js"></script>--%>
</html>
