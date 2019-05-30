<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comebuy.aspx.cs" Inherits="comebuy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>衣見鍾情 - 導購系統</title>
    <link href="comebuy/bootstrap.css" rel="stylesheet" />
    <link href="comebuy/style.css" rel="stylesheet" />
</head>
<body>
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="col-md-12">
            <div class="nav">
                <button class="btn-nav">
                    <span class="icon-bar inverted top"></span>
                    <span class="icon-bar inverted middle"></span>
                    <span class="icon-bar inverted bottom"></span>
                </button>
            </div>
            <a class="navbar-brand" href="index.html">
                <img class="logo" src="img/logo.png" alt="logo" />
            </a>
            <div class="nav-content hideNav hidden">
                <ul class="nav-list vcenter">
                    <li class="nav-item"><a class="item-anchor" href="index.aspx">Home</a></li>
                    <li class="nav-item"><a class="item-anchor" href="about.aspx">About Me</a></li>
                    <li class="nav-item"><a class="item-anchor" href="portfolio.aspx">Portfolio</a></li>
                    <li class="nav-item"><a class="item-anchor" href="contact.aspx">Contact</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Header -->
    <div class="span12">
        <div class="col-md-6 no-gutter text-center fill">
            <div class="preview-container">
                                                            <ul class="camera_list">
                                                                <p class="c_title">目前鏡頭</p>
                                                            </ul>
                                                            <video id="preview"></video>
                                                        </div>
            <%--<h2 class="vcenter">Simple is beautiful</h2>--%>
        </div>
        <div class="col-md-6 no-gutter text-center">
            <div id="header" data-speed="2" data-type="background">
                <div id="headslide" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <img src="img/4.jpg" alt="Slide" />
                        </div>
                        <div class="item">
                            <img src="img/3.jpg" alt="Slide"/>
                        </div>
                        <div class="item">
                            <img src="img/1.jpg" alt="Slide" />
                        </div>
                        <div class="item">
                            <img src="img/2.jpg" alt="Slide" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;"></div>
    <!-- script -->
    <script src="comebuy/jquery.js"></script>
    <script src="/bootstrap.min.js"></script>
    <script src="comebuy/menu-color.js"></script>
    <script src="comebuy/modernizr.js"></script>
    <script src="comebuy/script.js"></script>
</body>

</html>
