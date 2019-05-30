<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fsComebuy.aspx.cs" Inherits="fsComebuy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>衣見鍾情 - 導購系統</title>
    <link href="cbs_css/bootstrap.css" rel="stylesheet" />
    <link href="cbs_css/style.css" rel="stylesheet" />
    <link href="cbs_css/main_styles.css" rel="stylesheet" />
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
            <a class="navbar-brand" href="fsComebuy.aspx">
                <h1>FirstSight</h1>
            </a>
            <div class="nav-content hideNav hidden">
                <ul class="nav-list vcenter">
                    <li class="nav-item"><a class="item-anchor" href="fsComebuy.aspx">首頁</a></li>
                    <li class="nav-item"><a class="item-anchor" href="?page=scan">掃描</a></li>
                    <li class="nav-item"><a class="item-anchor" href="cbs_cd.aspx">瀏覽商品</a></li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Header -->
    <div class="span12" >
        <div class="col-md-12 no-gutter text-center fill"  id="scan" runat="server" visible="false" style="margin-bottom:200px">
            <div style="height:380px"></div>
            <div><video id="preview" height="800" width="1280"></video></div>
            
        </div>
        <asp:PlaceHolder ID="PH_slideshow" runat="server"></asp:PlaceHolder>
        <%--<div class="col-md-6 no-gutter text-center">
            <div id="header" data-speed="2" data-type="background">
                <div id="headslide" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <h2>eo45555555</h2>
                            <img src="img/4.jpg" alt="Slide" />
                        </div>
                        <div class="item">
                            <img src="img/3.jpg" alt="Slide" />
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
        </div>--%>
    </div>
    <div style="clear: both;"></div>
    <!-- script -->
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="cbs_js/jquery.js"></script>

    <script src="cbs_js/bootstrap.min.js"></script>
    <script src="cbs_js/menu-color.js"></script>
    <script src="cbs_js/modernizr.js"></script>
    <script src="js/instascan.min.js"></script>
    <script src="cbs_js/find.js"></script>
    <script type='text/javascript'>
        var JQ = jQuery.noConflict(true);
        // Toggle Menu
        JQ(window).load(function () {
            JQ(".btn-nav").on("click tap", function () {
                JQ(".nav-content").toggleClass("showNav hideNav").removeClass("hidden");
                JQ(this).toggleClass("animated");
            });
        });


        // Filtered Portfol

    </script>

</body>

</html>
