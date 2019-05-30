<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>衣見鍾情 - 導購系統</title>
    <link href="cbs_css/bootstrap.css" rel="stylesheet" />
    <link href="cbs_css/style.css" rel="stylesheet" />
    <link href="cbs_css/main_styles.css" rel="stylesheet"/>
</head>
<body>
    <ul class="navbar navbar-default navbar-fixed-top">
        <div class="col-md-12">
            <div class="nav">
                <button class="btn-nav">
                    <span class="icon-bar inverted top"></span>
                    <span class="icon-bar inverted middle"></span>
                    <span class="icon-bar inverted bottom"></span>
                </button>
            </div>
            <a class="navbar-brand" href="index.aspx">
                <h1>FirstSight</h1>
                <%--<img class="logo" src="img/logo.png" alt="logo" />--%>
            </a>
            <div class="nav-content hideNav hidden">
                <ul class="nav-list vcenter">
                    <li class="nav-item"><a class="item-anchor" href="index.aspx">首頁</a></li>
                    <li class="nav-item"><a class="item-anchor" href="about.aspx">熱銷商品</a></li>
                    <li class="nav-item"><a class="item-anchor" href="portfolio.aspx">瀏覽商品</a></li>
                    <li class="nav-item"><a class="item-anchor" href="kind" id="kinds">商品分類</a>
                        <ul class="account_selection">
                        <li><a href="?kind=TS">T-Shirt</a></li>
                        <li><a href="?kind=D">連身裙</a></li>
                        <li><a href="?kind=J">夾克</a></li>
                        <li><a href="?kind=OS">襯衫</a></li>
                        <li><a href="?kind=S">毛衣</a></li>
                        <li><a href="?kind=OW">外套</a></li>
                        <li><a href="?kind=WC">背心</a></li>
                        </ul>
                    </li>
                </ul>
                </div>
                        
                    
        </div>
        </ul>
    <!-- Header -->
    <div class="span12">
        <div class="body" id="scan_qr_div" runat="server" style="margin-top: 100px">
                        <div class="wrapper_index">
                            <div class="container">
                                <div class="row">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="explanation">
                                                    <p class="day_text text-center">掃描QRcode</p>
                                                    <p class="text-center">
                                                        <div class="preview-container">
                                                            <ul class="camera_list">
                                                                <p class="c_title">目前鏡頭</p>
                                                            </ul>
                                                            <video id="preview"></video>
                                                        </div>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        <div class="col-md-6 no-gutter text-center">
            <div id="header" data-speed="2" data-type="background">
                <div id="headslide" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <img src="images/blog_1.jpg" alt="Slide" />
                        </div>
                        <div class="item">
                            <img src="images/blog_2.jpg" alt="Slide" />
                        </div>
                        <div class="item">
                            <img src="images/blog_3.jpg" alt="Slide" / />
                        </div>
                        <div class="item">
                            <img src="images/desc_2.jpg" alt="Slide" / />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <div style="clear: both;"></div>
    <!-- script -->
    <script src="cbs_js/jquery.js"></script>
    <script src="cbs_js/bootstrap.min.js"></script>
    <script src="cbs_js/menu-color.js"></script>
    <script src="cbs_js/modernizr.js"></script>
    <script src="cbs_js/script.js"></script>
    <script src="js/instascan.min.js"></script>
    <script src="js/find.js"></script>


</body>

</html>
