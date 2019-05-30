<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cbs_cd.aspx.cs" Inherits="cbs_cd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>衣見鍾情 - 導購系統</title>
    <link href="cbs_css/bootstrap.css" rel="stylesheet" />
    <link href="cbs_css/lightbox.css" rel="stylesheet" type="text/css" media="all" />
    <link href="cbs_css/style.css" rel="stylesheet" />
    <link href="cbs_css/main_styles.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="styles/single_styles.css" />
    <link rel="stylesheet" type="text/css" href="styles/categories_styles.css" />
    <link rel="stylesheet" type="text/css" href="cbs_css/style2.css">
    <link rel="stylesheet" type="text/css" href="cbs_css/nivo-lightbox/nivo-lightbox.css">
    <link rel="stylesheet" type="text/css" href="cbs_css/nivo-lightbox/default.css">



    <link rel="stylesheet" type="text/css" href="cbs_css/style.css" />
    <%--  <link rel="stylesheet" type="text/css" href="styles/bootstrap4/bootstrap.min.css"/>--%>


    <%--<link rel="stylesheet" type="text/css" href="plugins/OwlCarousel2-2.2.1/owl.carousel.css"/>--%>
    <link rel="stylesheet" type="text/css" href="styles/main_styles.css" />
    <link rel="stylesheet" type="text/css" href="styles/responsive.css" />
</head>
<body>
    
    <nav class="navbar navbar-default color-fill navbar-fixed-top">
        <div class="col-md-12">
            <div class="nav">
                <button class="btn-nav">
                    <span class="icon-bar top"></span>
                    <span class="icon-bar middle"></span>
                    <span class="icon-bar bottom"></span>
                </button>
            </div>
            <a class="navbar-brand" href="fsComebuy.aspx">
                <h1>FirstSight</h1>
                <%--<img class="logo" src="img/logo.png" alt="logo" />--%>
            </a>
            <div class="nav-content hideNav hidden">
                <ul class="nav-list vcenter">
                    <li class="nav-item"><a class="item-anchor" href="fsComebuy.aspx">首頁</a></li>
                    <li class="nav-item"><a class="item-anchor" href="cbs_cd.aspx">商品瀏覽</a></li>

                </ul>
            </div>
        </div>
    </nav>
    <section id="cd_display" runat="server">
        <div class="container">
            <%--<ul class="portfolio-sorting list-inline text-center">--%>
            <%--<li><a href="#" data-group="all" class="active">All</a></li>--%>

            <asp:PlaceHolder ID="PH_cbs_tags" runat="server"></asp:PlaceHolder>

            <%--</ul>--%>
            <div class="row">
                <div class="col-sm-12">
                    <div class="lightbox-grid square-thumbs" data-gallery-title="Gallery">
                        <asp:PlaceHolder ID="PH_cbs_cds" runat="server"></asp:PlaceHolder>
                        <%--<ul class="portfolio-items list-unstyled" id="grid">
                            <li class="col-md-4 col-sm-12 col-xs-12 no-gutter" data-groups='["moment","people"]'>
                                <a href="img/portfolio1.jpg" data-lightbox="true">
                                    <div class="background-image-holder">
                                        <img alt="portfolio" class="background-image" src="img/portfolio1.jpg" />
                                    </div>
                                </a>
                            </li>
                        </ul>--%>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div id="cddetail_display" runat="server">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
  



<%--    <div id="likeit_display" runat="server" visible="false">
        <div class="row">
            <div class="portfolio-items">

                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/01-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/01-small.jpg" class="img-responsive" alt="Project Title" />
                                <asp:Image ID="Image1" runat="server" /></a>
                        </div>
                        <div style="float: right">
                            <asp:Image ID="Image2" src="img/圖片1.png" Height="150px" Width="150px" runat="server" /></div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 illustration">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/02-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/02-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>
                        <div style="float: right">
                            <asp:Image src="img/圖片2.png" Height="150px" Width="150px" runat="server" /></div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/03-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/03-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>
                        <div style="float: right">
                            <asp:Image src="img/圖片3.png" Height="150px" Width="150px" runat="server" /></div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/03-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/03-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>
                        <div style="float: right">
                            <asp:Image src="img/圖片4.png" Height="150px" Width="150px" runat="server" /></div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/03-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/03-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>

                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/03-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/03-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3 col-lg-3 graphic">
                    <div class="portfolio-item">
                        <div class="hover-bg">
                            <a href="img/portfolio/03-large.jpg" title="Project Title" data-lightbox-gallery="gallery1">
                                <div class="hover-text">
                                    <h4>Project Title</h4>
                                </div>
                                <img src="img/portfolio/03-small.jpg" class="img-responsive" alt="Project Title" />
                            </a>
                        </div>
                    </div>
                </div>

            </div>
        </div>
       
    </div>--%>

    <!-- script -->
    <script src="cbs_js/jquery.js"></script>
    <script src="cbs_js/bootstrap.min.js"></script>
    <script src="cbs_js/lightbox-plus-jquery.js"></script>
    <script src="cbs_js/jquery.shuffle.min.js"></script>
    <script src="cbs_js/modernizr.js"></script>
    <script src="cbs_js/script.js"></script>
    <script type="text/javascript" src="cbs_js/nivo-lightbox.js"></script>
    <script type="text/javascript" src="cbs_js/jquery.isotope.js"></script>
    <script type="text/javascript" src="cbs_js/main.js"></script>
</body>
</html>

