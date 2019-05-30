<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default - 複製 (2).aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html>
<head>
    <title>衣見鍾情</title>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--CSS樣式-->
    <link href="https://fonts.googleapis.com/css?family=Josefin+Slab:400,600,700" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Sacramento" rel="stylesheet">

    <link rel="stylesheet" href="styles/adm.css">
    <link rel="stylesheet" href="styles/bg.css">
    <link rel="stylesheet" type="text/css" href="styles/bootstrap4/bootstrap.min.css">
    <link href="plugins/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="plugins/OwlCarousel2-2.2.1/owl.carousel.css">
    <link rel="stylesheet" type="text/css" href="plugins/OwlCarousel2-2.2.1/owl.theme.default.css">
    <link rel="stylesheet" type="text/css" href="plugins/OwlCarousel2-2.2.1/animate.css">
    <link rel="stylesheet" type="text/css" href="styles/main_styles.css">
    <link rel="stylesheet" type="text/css" href="styles/responsive.css">
    <link rel="stylesheet" href="plugins/themify-icons/themify-icons.css">
    <link rel="stylesheet" type="text/css" href="plugins/jquery-ui-1.12.1.custom/jquery-ui.css">
    <link rel="stylesheet" type="text/css" href="styles/single_styles.css">
    <link rel="stylesheet" type="text/css" href="styles/single_responsive.css">
    <link rel="stylesheet" type="text/css" href="styles/categories_styles.css">
    <link rel="stylesheet" type="text/css" href="styles/categories_responsive.css">
    <%--購物車CSS--%>
    <link rel="stylesheet" type="text/css" media='all' href="styles/carts.css">
    <link rel='stylesheet' id='woocommerce-smallscreen-css' href='https://colorlib.com/tyche/wp-content/plugins/woocommerce/assets/css/woocommerce-smallscreen.css?ver=3.5.1' type='text/css' media='only screen and (max-width: 768px)' />


    <style>
        @import url(https://fonts.googleapis.com/earlyaccess/notosanstc.css);

        html, body, h1, h2, h3, h4, h5, h6 {
            font-family: "Noto Sans TC", sans-serif
        }

        * {
            font-family: font-family: 'Noto Sans TC', sans-serif;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="super_container">

                    <!-- Header -->

                    <header class="header trans_300">

                        <!-- Top Navigation -->

                        <div class="main_nav_container">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <div class="logo_container">
                                            <a href="Default.aspx"><span>衣</span>見鍾情</a>
                                        </div>
                                        <%--遊客功能選單--%>
                                        <nav id="nav_nologin" class="navbar" runat="server">
                                            <ul class="navbar_menu">
                                                <li><a href="Default.aspx">首頁</a></li>
                                                <li><a href="?page=scan">掃描</a></li>
                                                <%--先暫時坐在這邊--%>
                                                <li><a href="?kind=EVENTS">商品瀏覽</a></li>
                                            </ul>
                                            <ul class="navbar_user">
                                                <li><a href="#"><i class="fa fa-search" aria-hidden="true"></i></a></li>
                                                <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
                                                    <ul class="account_selection">
                                                        <li><a href="/login.aspx"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
                                                        <li><a href="?page=register"><i class="fa fa-user-plus" aria-hidden="true"></i>註冊</a></li>
                                                    </ul>
                                                </li>
                                            </ul>
                                            <div class="hamburger_container">
                                                <i class="fa fa-bars" aria-hidden="true"></i>
                                            </div>

                                        </nav>
                                        <%--會員功能選單--%>
                                        <nav id="nav_user" class="navbar" runat="server">
                                            <ul class="navbar_menu">
                                                <li><a href="Default.aspx">首頁</a></li>
                                                <li><a href="?page=scan">掃描</a></li>
                                                <%--先暫時坐在這邊--%>
                                                <li><a href="?kind=EVENTS">商品瀏覽</a></li>
                                                <li><a href="?page=wish">許願池</a></li>
                                                <li><a href="?page=order">我的訂單</a></li>
                                            </ul>
                                            <ul class="navbar_user">
                                                <li class="account"><a href="?page=profile"><i class="fa fa-user" aria-hidden="true"></i></a></li>
                                                <li class="checkout">
                                                    <a href="?page=ckout">
                                                        <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                                                        <span id="checkout_items" class="checkout_items" runat="server"></span>
                                                    </a>
                                                </li>
                                            </ul>
                                            <div class="hamburger_container">
                                                <i class="fa fa-bars" aria-hidden="true"></i>
                                            </div>
                                            <div>
                                                <asp:Label ID="lb_hello" runat="server" Text="" CssClass="col-lg-9" Visible="False"></asp:Label>
                                                <asp:Button ID="Button1" runat="server" Text="登出" OnClick="btn_userlogout_Click" />
                                            </div>
                                        </nav>

                                        <%--我是分割線管理者功能選單--%>
                                        <nav id="nav_administrator" class="navbar" runat="server">
                                            <ul class="navbar_menu">
                                                <li><a href="Default.aspx">首頁</a></li>
                                                <li><a href="?page=scan">掃描</a></li>
                                                <li><a href="?page=produceqr">產生商品/訂單QRcode</a></li>
                                                <%-- 之後自己把這搬走哦--%>
                                                <li><a href="?page=admmm">會員管理</a></li>
                                                <li><a href="?page=admcd">商品管理</a></li>
                                                <li><a href="?page=admod">訂單管理</a></li>
                                            </ul>
                                            <ul class="navbar_user">
                                                <li class="account"><a href="?page=profile"><i class="fa fa-user" aria-hidden="true"></i></a></li>
                                            </ul>
                                            <div class="hamburger_container">
                                                <i class="fa fa-bars" aria-hidden="true"></i>
                                            </div>
                                            <div>
                                                <asp:Label ID="lb_hello2" runat="server" Text="" CssClass="col-lg-9" Visible="False"></asp:Label>
                                                <asp:Button ID="btn_userlogout" runat="server" Text="登出" OnClick="btn_userlogout_Click" />
                                            </div>
                                        </nav>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </header>

                    <!-- 商品資訊頁面PH3 -->
                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                    </div>

                    <!-- 商品類別頁面PH4 -->
                    <div class="container product_section_container" id="kind_div" visible="false" runat="server">
                        <div class="row">
                            <div class="col product_section clearfix">

                                <!-- Breadcrumbs -->

                                <div class="breadcrumbs d-flex flex-row align-items-center">
                                    <ul>
                                        <li><a href="Default.aspx">首頁</a></li>
                                        <li class="active"><a href="#" id="kindpage" runat="server"><i class="fa fa-angle-right" aria-hidden="true"></i></a></li>
                                    </ul>
                                </div>

                                <!-- Sidebar -->

                                <div class="sidebar">
                                    <div class="sidebar_section">
                                        <div class="sidebar_title">
                                            <h5>商品分類</h5>
                                        </div>
                                        <ul class="sidebar_categories">
                                            <%--//id="li_" runat="server"
                                            <li><a href="?kind=TS">T-Shirt</a></li>
                                            <li><a href="?kind=D">連身裙</a></li>
                                            <li><a href="?kind=J">夾克</a></li>--%>
                                            <%--<li class="active"><a href="?kind=OS"><span><i class="fa fa-angle-double-right" aria-hidden="true"></i></span>襯衫</a></li>--%>
                                            <%--<li><a href="?kind=OS"><span><i></i></span>襯衫</a></li>
                                            <li><a href="?kind=S">毛衣</a></li>
                                            <li><a href="?kind=OW">外套</a></li>
                                            <li><a href="?kind=WC">背心</a></li>--%>

                                            <%--TEST--%>
                                            <%--id="li_" runat="server"--%>
                                            <%--id="i_"  aria-hidden="true" runat="server"--%>
                                            <li id="li_EVENTS" runat="server"><a href="?kind=EVENTS"><span><i id="i_EVENTS" aria-hidden="true" runat="server"></i></span>促銷活動</a></li>
                                            <li id="li_TS" runat="server"><a href="?kind=TS"><span><i id="i_TS" aria-hidden="true" runat="server"></i></span>T-Shirt</a></li>
                                            <li id="li_D" runat="server"><a href="?kind=D"><span><i id="i_D" aria-hidden="true" runat="server"></i></span>連身裙</a></li>
                                            <li id="li_J" runat="server"><a href="?kind=J"><span><i id="i_J" aria-hidden="true" runat="server"></i></span>夾克</a></li>                                            
                                            <li id="li_OS" runat="server"><a href="?kind=OS"><span><i id="i_OS" aria-hidden="true" runat="server"></i></span>襯衫</a></li>
                                            <li id="li_S" runat="server"><a href="?kind=S"><span><i id="i_S" aria-hidden="true" runat="server"></i></span>毛衣</a></li>
                                            <li id="li_OW" runat="server"><a href="?kind=OW"><span><i id="i_OW" aria-hidden="true" runat="server"></i></span>外套</a></li>
                                            <li id="li_WC" runat="server"><a href="?kind=WC"><span><i id="i_WC" aria-hidden="true" runat="server"></i></span>背心</a></li>

                                        </ul>
                                    </div>

                                    <!-- 價格篩選 -->
                                    <div class="sidebar_section">
                                        <div class="sidebar_title">
                                            <h5>價格區間</h5>
                                        </div>
                                        <p>
                                            <input type="text" id="amount" readonly style="border: 0; color: #f6931f; font-weight: bold;">
                                        </p>
                                        <div id="slider-range"></div>
                                        <div class="filter_button"><span>filter</span></div>
                                    </div>

                                    <!-- Sizes -->
                                    <div class="sidebar_section">
                                        <div class="sidebar_title">
                                            <h5>Sizes</h5>
                                        </div>
                                        <ul class="checkboxes">
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>S</span></li>
                                            <li class="active"><i class="fa fa-square" aria-hidden="true"></i><span>M</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>L</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>XL</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>XXL</span></li>
                                        </ul>
                                    </div>

                                    <!-- Color -->
                                    <div class="sidebar_section">
                                        <div class="sidebar_title">
                                            <h5>Color</h5>
                                        </div>
                                        <ul class="checkboxes">
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Black</span></li>
                                            <li class="active"><i class="fa fa-square" aria-hidden="true"></i><span>Pink</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>White</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Blue</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Orange</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>White</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Blue</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Orange</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>White</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Blue</span></li>
                                            <li><i class="fa fa-square-o" aria-hidden="true"></i><span>Orange</span></li>
                                        </ul>
                                        <div class="show_more">
                                            <span><span>+</span>Show More</span>
                                        </div>
                                    </div>

                                </div>

                                <!-- Main Content -->

                                <div class="main_content">

                                    <!-- Products -->

                                    <div class="products_iso">
                                        <div class="row">
                                            <div class="col">

                                                <!-- Product Sorting -->

                                                <div class="product_sorting_container product_sorting_container_top">
                                                    <ul class="product_sorting">
                                                        <li>
                                                            <span class="type_sorting_text">Default Sorting</span>
                                                            <i class="fa fa-angle-down"></i>
                                                            <ul class="sorting_type">
                                                                <li class="type_sorting_btn" data-isotope-option='{ "sortBy": "original-order" }'><span>Default Sorting</span></li>
                                                                <li class="type_sorting_btn" data-isotope-option='{ "sortBy": "price" }'><span>Price</span></li>
                                                                <li class="type_sorting_btn" data-isotope-option='{ "sortBy": "name" }'><span>Product Name</span></li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <span>Show</span>
                                                            <span class="num_sorting_text">6</span>
                                                            <i class="fa fa-angle-down"></i>
                                                            <ul class="sorting_num">
                                                                <li class="num_sorting_btn"><span>6</span></li>
                                                                <li class="num_sorting_btn"><span>12</span></li>
                                                                <li class="num_sorting_btn"><span>24</span></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                    <div class="pages d-flex flex-row align-items-center">
                                                        <div class="page_current">
                                                            <span>1</span>
                                                            <ul class="page_selection">
                                                                <li><a href="#">1</a></li>
                                                                <li><a href="#">2</a></li>
                                                                <li><a href="#">3</a></li>
                                                            </ul>
                                                        </div>
                                                        <div class="page_total"><span>of</span> 3</div>
                                                        <div id="next_page" class="page_next"><a href="#"><i class="fa fa-long-arrow-right" aria-hidden="true"></i></a></div>
                                                    </div>

                                                </div>

                                                <!-- Product Grid -->

                                                <div class="product-grid">
                                                    <!-- 迴圈開始 -->
                                                    <div class="articlebody">
                                                        <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                                                    </div>
                                                    <!-- 迴圈結束 -->

                                                </div>

                                                <!-- Product Sorting -->

                                                <div class="product_sorting_container product_sorting_container_bottom clearfix">
                                                    <ul class="product_sorting">
                                                        <li>
                                                            <span>Show:</span>
                                                            <span class="num_sorting_text">04</span>
                                                            <i class="fa fa-angle-down"></i>
                                                            <ul class="sorting_num">
                                                                <li class="num_sorting_btn"><span>01</span></li>
                                                                <li class="num_sorting_btn"><span>02</span></li>
                                                                <li class="num_sorting_btn"><span>03</span></li>
                                                                <li class="num_sorting_btn"><span>04</span></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                    <span class="showing_results">Showing 1–3 of 12 results</span>
                                                    <div class="pages d-flex flex-row align-items-center">
                                                        <div class="page_current">
                                                            <span>1</span>
                                                            <ul class="page_selection">
                                                                <li><a href="#">1</a></li>
                                                                <li><a href="#">2</a></li>
                                                                <li><a href="#">3</a></li>
                                                            </ul>
                                                        </div>
                                                        <div class="page_total"><span>of</span> 3</div>
                                                        <div id="next_page_1" class="page_next"><a href="#"><i class="fa fa-long-arrow-right" aria-hidden="true"></i></a></div>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                    <%--掃描qrcode--%>
                    <div class="body" id="scan_qr_div" runat="server" visible="false" style="margin-top: 100px">
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
                                            <div class="col-md-6 col-sm-6">
                                                <div class="explanation">
                                                    <p class="day_text text-center">QRcode之讀取資料</p>
                                                    <p class="text-center">
                                                        <table class="table">
                                                            <i class="fa fa-refresh fa-spin fa-3x fa-fw"></i>
                                                            <tr>
                                                                <th>QRCODE辨識內容</th>
                                                                <td id="r_id"></td>
                                                            </tr>
                                                        </table>
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
                <%--手機端展開頁--%>
                <div class="fs_menu_overlay"></div>
                <div class="hamburger_menu">
                    <div class="hamburger_close"><i class="fa fa-times" aria-hidden="true"></i></div>
                    <div class="hamburger_menu_content text-right">
                        <ul class="menu_top_nav">
                            <li class="menu_item has-children">
                                <a href="#">會員資料<i class="fa fa-angle-down"></i></a>
                                <ul class="menu_selection">
                                    <li><a href="#"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
                                    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>註冊</a></li>
                                </ul>
                            </li>
                            <li class="menu_item"><a href="Default.aspx">首頁</a></li>
                            <li class="menu_item"><a href="?page=scan">掃描</a></li>
                            <li class="menu_item has-children">
                                <a href="#">瀏覽商品<i class="fa fa-angle-down"></i></a>
                                <ul class="menu_selection">
                                    <li><a href="#">牛逼牛逼</a></li>
                                    <li><a href="#">西西</a></li>
                                </ul>
                            </li>
                            <li class="menu_item"><a href="#">pages</a></li>
                            <li class="menu_item"><a href="#">blog</a></li>
                            <li class="menu_item"><a href="#">contact</a></li>
                        </ul>
                    </div>
                </div>
                <div id="fslogo" class="main_sl_1g" runat="server">
                    <div class="container">


                        <nav class="fh5co-nav" role="navigation">
                            <div class="menu-2">
                                <div id="fh5co-logo">
                                    <h1>FirstSight<span>.</span></h1>
                                </div>
                            </div>
                        </nav>
                    </div>
                </div>
                <div id="start_display" runat="server">
                    <!-- Slider -->

                    <%--<div class="main_slider" style="background-image:url(images/slider_1.jpg)">
		    <div class="container fill_height">
			    <div class="row align-items-center fill_height">
				    <div class="col">
					    <div class="main_slider_content">
						    <h6>Spring / Summer Collection 2017</h6>
						    <h1>Get up to 30% Off New Arrivals</h1>
						    <div class="red_button shop_now_button"><a href="#">shop now</a></div>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>--%>

                    <!-- Banner 原上方三張圖 -->

                    <%-- <div class="banner">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="banner_item align-items-center" style="background-image: url(images/banner_1.jpg)">
                                        <div class="banner_category">
                                            <a href="categories.html">金毛小姐姐</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="banner_item align-items-center" style="background-image: url(images/banner_2.jpg)">
                                        <div class="banner_category">
                                            <a href="categories.html">包包</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="banner_item align-items-center" style="background-image: url(images/banner_3.jpg)">
                                        <div class="banner_category">
                                            <a href="categories.html">黑人小哥哥</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <%--優惠--%>
                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolderevent" runat="server"></asp:PlaceHolder>
                    </div>

                    <!-- 首頁商品PH1 -->
                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>



                    <!-- 優惠倒數 -->

                    <%--<div class="deal_ofthe_week">
		    <div class="container">
			    <div class="row align-items-center">
				    <div class="col-lg-6">
					    <div class="deal_ofthe_week_img">
						    <img src="images/deal_ofthe_week.png" alt="">
					    </div>
				    </div>
				    <div class="col-lg-6 text-right deal_ofthe_week_col">
					    <div class="deal_ofthe_week_content d-flex flex-column align-items-center float-right">
						    <div class="section_title">
							    <h2>Deal Of The Week</h2>
						    </div>
						    <ul class="timer">
							    <li class="d-inline-flex flex-column justify-content-center align-items-center">
								    <div id="day" class="timer_num">03</div>
								    <div class="timer_unit">Day</div>
							    </li>
							    <li class="d-inline-flex flex-column justify-content-center align-items-center">
								    <div id="hour" class="timer_num">15</div>
								    <div class="timer_unit">Hours</div>
							    </li>
							    <li class="d-inline-flex flex-column justify-content-center align-items-center">
								    <div id="minute" class="timer_num">45</div>
								    <div class="timer_unit">Mins</div>
							    </li>
							    <li class="d-inline-flex flex-column justify-content-center align-items-center">
								    <div id="second" class="timer_num">23</div>
								    <div class="timer_unit">Sec</div>
							    </li>
						    </ul>
						    <div class="red_button deal_ofthe_week_button"><a href="#">shop now</a></div>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>--%>

                    <!-- 熱銷商品PH2 -->

                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                    </div>

                    <!-- Benefit -->

                    <div class="benefit">
                        <div class="container">
                            <div class="row benefit_row">
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-truck" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>free shipping</h6>
                                            <p>Suffered Alteration in Some Form</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-money" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>cach on delivery</h6>
                                            <p>The Internet Tend To Repeat</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-undo" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>45 days return</h6>
                                            <p>Making it Look Like Readable</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-clock-o" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>opening all week</h6>
                                            <p>8AM - 09PM</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Blogs -->
                    <%--<asp:Image ID="Image1" runat="server" Height="150px" Width="150px" ImageUrl="~/images/slider_1.jpg" />--%>
                    <%--<div class="blogs">
                        <div class="container">
                            <div class="row">
                                <div class="col text-center">
                                    <div class="section_title">
                                        <h2>Latest Blogs</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="row blogs_container">
                                <div class="col-lg-4 blog_item_col">
                                    <div class="blog_item">
                                        <div class="blog_background" style="background-image: url(images/blog_1.jpg)"></div>
                                        <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
                                            <h4 class="blog_title">Here are the trends I see coming this fall</h4>
                                            <span class="blog_meta">by admin | dec 01, 2017</span>
                                            <a class="blog_more" href="#">Read more</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 blog_item_col">
                                    <div class="blog_item">
                                        <div class="blog_background" style="background-image: url(images/blog_2.jpg)"></div>
                                        <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
                                            <h4 class="blog_title">Here are the trends I see coming this fall</h4>
                                            <span class="blog_meta">by admin | dec 01, 2017</span>
                                            <a class="blog_more" href="#">Read more</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 blog_item_col">
                                    <div class="blog_item">
                                        <div class="blog_background" style="background-image: url(images/blog_3.jpg)"></div>
                                        <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
                                            <h4 class="blog_title">Here are the trends I see coming this fall</h4>
                                            <span class="blog_meta">by admin | dec 01, 2017</span>
                                            <a class="blog_more" href="#">Read more</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    
                </div> <%-- <-start display --%>
                
                <%--購物車複製--%>
                <div id="cart_display" visible="false" runat="server">
                    <div class="container">
                        <div class="row">
                            <div id="primary" class="col-md-12">
                                <div class="col-md-12">
                                    <%--  --%>
                                    <div class="carts_top">
                                        <div class="container">
                                            <div class="row">
                                                <div class="col text-center">
                                                    <div class="section_title top_1g">
                                                        <h2>購物車</h2>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row blogs_container">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div id="cart_table" class="woocommerce" runat="server" visible="true">
                            
                            <div class="articlebody "><asp:PlaceHolder ID="PH_cart" runat="server"></asp:PlaceHolder></div>
                           
                            <div class="col-lg-4 " style="margin-top: 200px;">
                                <asp:Label ID="Label4" runat="server" Text="付款方式:"></asp:Label>
                                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="payment" Text="貨到付款" />
                                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="payment" Text="銀行轉帳" />
                                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="payment" Text="其他" />
                                <br>
                                <asp:Label ID="Label5" runat="server" Text="取貨方式:"></asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="itemSelected">
                                    <asp:ListItem Value="10" Selected="True">超商取貨</asp:ListItem>
                                    <asp:ListItem Value="11">宅配</asp:ListItem>
                                    <asp:ListItem Value="12">郵寄掛號</asp:ListItem>
                                </asp:DropDownList>
                                <br>
                                <br>
                                <asp:Label ID="lb_seladr" runat="server" Text="取貨門市:" Visible="True"></asp:Label>
                                &nbsp;<asp:TextBox ID="tb_pickadr" runat="server"></asp:TextBox>
                                <br>
                                <asp:CheckBox ID="ckb_takemmadr" runat="server" Text="同會員資料地址" />
                                <br>
                                <br>
                                <asp:Label ID="Label1" runat="server" Text="聯絡電話:"></asp:Label>&nbsp;<asp:TextBox ID="tb_odrphone" runat="server"></asp:TextBox>
                                <br>
                                <asp:CheckBox ID="ckb_takemmphone" Text="同會員資料電話" runat="server" />
                                <br>

                                <br>

                                <div class="form-group">
                                    <textarea name="message" id="message" class="form-control" rows="4" placeholder="備註" required></textarea>
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <div class="main_sl_1g ">
                                <asp:Button ID="btn_checkout" runat="server" Text="結帳" class="btn btn-lg btn-success btn-block" />
                            </div>
                        </div>
                        <div id="cart_noitem" runat="server" visible="true">
                            <H2>購物車尚未有商品!</H2>
                        </div>
                    </div>
                </div>
                

                     <%--許願池start--%>
                <div id="wish_display" visible="false" runat="server">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>許願池</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                        </div>
                    </div>
                    <%--許願第一排--%>

                     <div class="articlebody"><asp:PlaceHolder ID="PH_wish" runat="server"></asp:PlaceHolder></div>
                  


                    <%--許願第二排--%>
                        <div class="row centered1G mt1G grid1G">
                            <div class="mt"></div>
                            <div class="col-lg-4">
                                
                                <a href="#">
                                    <img src="images/product_4.png" alt=""> <div class="favorite favorite_left"></div>
                                        </a>
                            </div>
                          
                            <div class="col-lg-4">
                                <a href="#">
                                    <img src="images/product_4.png" alt=""><div class="favorite"></div></a>
                            </div>
                            <div class="col-lg-4">
                                <a href="#">
                                    <img src="images/product_6.png" alt=""></a>
                            </div>
                            </div>
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title">
                                    <h2>Following Tags</h2>
                                </div>
                            </div>
                        </div>                    
                    </div>
                    <%--追蹤tag--%>
                    <div class="main_s2_1g">
                        <div class="widget widget_tag_cloud text-center">
                            <div class="tagcloud">
                                <a href="#" style="color: black">Autumn</a>
                                <a href="#" style="color: black">dress</a>
                                <a href="#" style="color: black">fashion</a>
                                <a href="#" style="color: black">jackets</a>
                                <a href="#" style="color: black">stockings</a>
                                <a href="#" style="color: black">10000000</a>
                                <a href="#" style="color: black">2</a>
                                <a href="#" style="color: black">3</a>
                                <a href="#" style="color: black">4</a>
                                <a href="#" style="color: black">5</a>
                                <a href="#" style="color: black">6</a>
                            </div>
                        </div>
                    </div>
                </div>
                <%--許願池end--%>

                <%--訂單--%>
                <div id="order_display" visible="false" runat="server">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>我的訂單</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                        </div>
                        <div class="articlebody" id="order_table" runat="server" Visible="false"><asp:PlaceHolder ID="PH_order" runat="server" ></asp:PlaceHolder></div>
                    <div id="order_noitem" runat="server" visible="true">
                            <h2>尚未有訂單紀錄!</h2>
                        </div> 
                    </div>                    
                                       
                </div>
               <%-- profile START--%>
                <div id="profile_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>個人資料</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">


                                <div id="pf_maindiv" class="form-group" runat="server">
                                    <h5>使用者</h5>
                                    <asp:Label ID="lb_pfact" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <div id="pf_pwddiv" runat="server" visible="true">
                                    <h5>密碼</h5>
                                    <asp:Button ID="btn_changepw" runat="server" Text="修改密碼" class="btn btn-lg btn-block btn-info" OnClick="btn_changepw_Click" />
                                    </div>
                                    <h5>姓名</h5>
                                    <asp:TextBox ID="tb_pfname" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lb_pfname" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <h5>手機</h5>
                                    <asp:TextBox ID="tb_pfphone" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lb_pfphone" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <h5>信箱</h5>
                                    <asp:TextBox ID="tb_pfmail" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lb_pfemail" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <h5>地址</h5>
                                    <asp:TextBox ID="tb_pfadr" class="form-control" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lb_pfadr" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <h5>生日</h5>
                                    <asp:TextBox ID="tb_pfbd" class="form-control" runat="server" TextMode="Date" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lb_pfbd" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    <p class="help-block text-danger"></p>
                                    <asp:CheckBox ID="ckb_notification" runat="server" Text="接受許願池通知" Enabled="False" />
                                </div>
                                <div id="pf_pwdchgdiv" class="form-group" runat="server" visible="false">
                                    <h5>舊密碼</h5>
                                    <asp:TextBox ID="tb_pfoldpw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    <h5>新密碼</h5>
                                    <asp:TextBox ID="tb_pfnewpw" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                                    <h5>確認新密碼</h5>
                                    <asp:TextBox ID="tb_pfnewpwck" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                                    <p id="p_error" runat="server" style="color:red" visible="false"></p>
                                </div>
                                <asp:Button ID="btn_editpf" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_editpf_Click" />
                                <asp:Button ID="btn_pfsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_pfsubmit_Click"/>
                                <asp:Button ID="btn_pfback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_pfback_Click"/>
                            </div>
                        </div>
                    </div>                                     
                </div>
                <%-- profile END--%>
                <%-- register START--%>
                 <div id="register_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>註冊</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>帳號</h5>
                                    <asp:TextBox ID="tb_rgact" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>密碼</h5>
                                    <asp:TextBox ID="tb_rgpwd" class="form-control" runat="server" TextMode="Password"></asp:TextBox>      
                                    <p class="help-block text-danger"></p>
                                    <h5>確認密碼</h5>
                                    <asp:TextBox ID="tb_rgpwdck" class="form-control" runat="server" TextMode="Password"></asp:TextBox>                                  
                                    <p class="help-block text-danger"></p>
                                    <h5>姓名</h5>
                                    <asp:TextBox ID="tb_rgname" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>手機</h5>
                                    <asp:TextBox ID="tb_rgphone" class="form-control" runat="server" ></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>信箱</h5>
                                    <asp:TextBox ID="tb_rgmail" class="form-control" runat="server" ></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>地址</h5>
                                    <asp:TextBox ID="tb_rgadr" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>生日</h5>
                                    <asp:TextBox ID="tb_rgbd" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <asp:CheckBox ID="ckb_rgnotice" runat="server" Text="接受許願池通知" />
                                    <p id="p_rgerror" runat="server" style="color:red" visible="false"></p>
                                </div>
                                <asp:Button ID="btn_rgsubmit" runat="server" Text="註冊" class="btn btn-lg btn-block btn-success" OnClick="btn_rgsubmit_Click" />
                                <asp:Button ID="btn_rgback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" OnClick="btn_rgback_Click" />
                            </div>
                        </div>
                    </div>                                     
                </div>
                <%-- register END--%>

                <%--管理員-會員管理START--%>
                <div id="adm_memberm_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>會員管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_adm_newadm" runat="server" Text="新增管理員" class="btn btn-lg btn-block btn-link" />
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_adm_newuser" runat="server" Text="新增會員" class="btn btn-lg btn-block btn-link" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_Member" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="mm_no" DataSourceID="SqlDataSource_mm"
                                Width="80%" OnRowCommand="GV_RowCommand_mm" CssClass="GVTable" BorderWidth="1px">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="mm_no" HeaderText="" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol"></HeaderStyle>

                                        <ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="mm_id" HeaderText="會員編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="mm_act" HeaderText="帳號"></asp:BoundField>

                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="user_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="user_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                    <div class="col-10 text-center">
                    </div>

                </div>
                <%--管理員-會員管理END--%>

                <%--管理員-會員詳細資料START--%>
                <div id="adm_mmdetail_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>會員詳細資料</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_resetpw" runat="server" Text="重設會員密碼" class="btn btn-lg btn-block btn-link" /></div>
                    </div>
                    
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width: 80%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    <th style="padding-left: 10px;"></th>
                                    <th>會員資料</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="姓名"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmname" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmname" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="地址"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmadr" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmadr" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="電話"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmphone" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmphone" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="信箱"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmmail" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmmail" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="生日"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmbd" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmbd" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="Date"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Text="訂單數"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_odnum" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="是否接受通知"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmnotice" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmnotice" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        

                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_adm_mmedit" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_mmedit_Click" />
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="btn_adm_mmsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_adm_mmsubmit_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btn_adm_mmback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_adm_mmback_Click"/>
                        </div>
                    </div>
                </div>
                <%--管理員-會員詳細資料END--%>

                <%--管理員-新增管理員START--%>

                <%--管理員-新增管理員END--%>

                <%--管理員-新增會員START--%>

                <%--管理員-新增會員END--%>

                <%--管理員-商品管理START--%>
                <div id="adm_cdm_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>商品管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_adm_newcd" runat="server" Text="新增商品" class="btn btn-lg btn-block btn-link" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_cd" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="cd_no" DataSourceID="SqlDataSource_cd"
                                Width="80%" OnRowCommand="GV_RowCommand_cd" CssClass="GVTable" BorderWidth="1px" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="cd_no" HeaderText="" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol"></HeaderStyle>

                                        <ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_id" HeaderText="會員編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_name" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_group" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_place" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_inserttime" HeaderText="帳號"></asp:BoundField>


                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="cd_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="cd_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                    

                </div>
                <%--管理員-商品管理END--%>

                <%--管理員-商品詳細START--%>
                <div id="Div1" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>商品詳細資料</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="Button2" runat="server" Text="重設會員密碼" class="btn btn-lg btn-block btn-link" /></div>
                    </div>

                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width: 80%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    <th style="padding-left: 10px;"></th>
                                    <th>商品資料</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="商品名稱"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox6" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Group"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox7" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="顏色"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox8" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="種類"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox9" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="材質"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox10" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Text="價格"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="製造地"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label29" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox11" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="商品擺放區"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="TextBox12" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="Button5" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" />
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="Button6" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="Button7" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" />
                        </div>
                    </div>
                </div>
                <%--管理員-商品詳細END--%>

                <%--管理員-訂單管理START--%>
                <div id="adm_odm_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>商品管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="Button8" runat="server" Text="新增訂單" class="btn btn-lg btn-block btn-link" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_od" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="od_no" DataSourceID="SqlDataSource_od"
                                Width="80%" OnRowCommand="GV_RowCommand_od" CssClass="GVTable" BorderWidth="1px" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="od_no" HeaderText="" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol"></HeaderStyle>

                                        <ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_id" HeaderText="會員編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_mm_id" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_phone" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_total" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_paymant" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_delivery" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_d_address" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_note" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_orderdate" HeaderText="帳號"></asp:BoundField>


                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="cd_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="cd_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                   
                </div>
                <%--管理員-訂單管理END--%>

                <%--管理員-訂單詳細START--%>
                <div id="Div2" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>商品管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="Button9" runat="server" Text="新增訂單" class="btn btn-lg btn-block btn-link" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_oddetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="od_no" DataSourceID="SqlDataSource_oddetail"
                                Width="80%" OnRowCommand="GV_RowCommand_oddetail" CssClass="GVTable" BorderWidth="1px" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="od_no" HeaderText="" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol"></HeaderStyle>

                                        <ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField> 

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_cd_id" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_s_amount" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_m_amount" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_l_amount" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_xl_amount" HeaderText="帳號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>


                                
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                   
                </div>

                <%-- 分割 --%>
                
                <%--管理員-訂單詳細END--%>



                <!-- Footer -->

                <footer class="footer">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="footer_nav_container d-flex flex-sm-row flex-column align-items-center justify-content-lg-start justify-content-center text-center">
                                    <ul class="footer_nav">
                                        <li><a href="#">Blog</a></li>
                                        <li><a href="#">FAQs</a></li>
                                        <li><a href="contact.html">Contact us</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="footer_social d-flex flex-row align-items-center justify-content-lg-end justify-content-center">
                                    <ul>
                                        <li><a href="#"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                                        <li><a href="#"><i class="fa fa-twitter" aria-hidden="true"></i></a></li>
                                        <li><a href="#"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
                                        <li><a href="#"><i class="fa fa-skype" aria-hidden="true"></i></a></li>
                                        <li><a href="#"><i class="fa fa-pinterest" aria-hidden="true"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="footer_nav_container">
                                    <div class="cr">©2018 All Rights Reserverd. This template is made with <i class="fa fa-heart-o" aria-hidden="true"></i>by <a href="#">Colorlib</a></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>

                </div>
                <asp:SqlDataSource ID="SqlDataSource_mm" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceMM_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_cd" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceCD_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_od" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceOD_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_oddetail" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceODDETAIL_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <%--JS引用--%>
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="styles/bootstrap4/popper.js"></script>
    <script src="styles/bootstrap4/bootstrap.min.js"></script>
    <script src="plugins/Isotope/isotope.pkgd.min.js"></script>
    <script src="plugins/OwlCarousel2-2.2.1/owl.carousel.js"></script>
    <script src="plugins/easing/easing.js"></script>
    <script src="js/custom.js"></script>
  <%--  <script src="js/single_custom.js"></script>--%>
     <script src="js/single.js"></script>
    <script src="plugins/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
    <script src="js/categories_custom.js"></script>
    <%--QRCODE SCAN--%>
    <script src="js/instascan.min.js"></script>
    <script src="js/find.js"></script>
    <%--購物車--%>
    <script>
        /*購物車商品數量start*/
        $('.sminus').ready(function () {
            $('.sminus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額 ???????????????????????????????????????
                var sc_cd_idvalue = sc_cd_id + "svalue";//數量


                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                var total = 0;
                if (sc_cd_value > 0) {
                    sc_cd_value += -1;
                total += -1;//商品總量
                }               
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text());}

                $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#" + sc_cd_iddiscountprice).text(price);
                        } else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                       caltotal();
                    }
                })
                $("#" + sc_cd_idvalue).text(sc_cd_value);
                
            });
            
        });
        $('.splus').ready(function () {
            $('.splus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "svalue";//數量

                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                
                sc_cd_value += 1;
                var total = 0;
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                total += 1;


                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.mminus').ready(function () {
            $('.mminus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price";//總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "mvalue";//數量

                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                var total = 0;
                if (sc_cd_value > 0) {
                    sc_cd_value += -1;
                    total += -1;
                }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }

                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.mplus').ready(function () {
            $('.mplus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "mvalue";//數量

                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                var total = 0;
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                total += 1;
                sc_cd_value += 1;

                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.lminus').ready(function () {
            $('.lminus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "lvalue";//數量

                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());

                var total = 0;
                

                if (sc_cd_value > 0) {
                    sc_cd_value += -1;
                    
                total += -1;//總量
                }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                    if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.lplus').ready(function () {
            $('.lplus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "lvalue";//數量

                var sc_cd_value = parseInt($("#" + sc_cd_idvalue).text());

                var total = 0;
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                total += 1;

                sc_cd_value += 1;

                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.xlminus').ready(function () {
            $('.xlminus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "xlvalue";//數量

                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                 var total = 0;
                if (sc_cd_value > 0) {
                    sc_cd_value += -1;
                    total += -1;
                }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                

                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);
                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })
                
                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        $('.xlplus').ready(function () {
            $('.xlplus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
                var sc_cd_idvalue = sc_cd_id + "xlvalue";//數量
                
                var sc_cd_value = parseInt($("#"+sc_cd_idvalue).text());
                sc_cd_value += 1;
                var total = 0;
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'svalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'mvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'lvalue']).text()); }
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                total += 1;

                 $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id,
                    success: function (msg) {
                        //這裡要分割msg 
                        var x = msg.split(",");
                        //x[0]轉成 int * total 
                        if (x[1] != "N") {//是否為折扣商品 
                            var price = 0;
                            price = x[0] * x[2] * total;
                            $("#"+sc_cd_iddiscountprice).text(price);
                        }
                        else {
                            var price = 0;
                            price = Math.round(x[0] * x[2] * total);
                            $("#" + sc_cd_iddiscountprice).text('$' + price);

                        }
                        var price = 0;
                        price = x[0] * total;
                        $("#" + sc_cd_idprice).text('$' + price);
                        caltotal();
                       
                    }
                })

                $("#"+sc_cd_idvalue).text(sc_cd_value);
            });
        });
        /*購物車商品數量end*/

        //加入許願池
        $('.lbh').ready(function () {
            $('.lbh').click(function () {
                var fav = $('.product_favorite');
				    fav.toggleClass('active');

                $.ajax({
                    type: 'POST', url: 'wish.aspx',
                    data: 'cd_id=' + $(this).attr("id").substr(0, 10),
                    success: function (msg) {
                        
                        //alert(msg);
                    }
                })
            });
        });

        $(window).on('load', function()
        {
            if (lb_hello.visible = true) {
                //alert("1985");
                $.ajax({
                    type: 'POST', url: 'checkwish.aspx',
                    //data: 'cd_id=' + $(this).attr("id").substr(0, 10),
                    success: function (msg) {
                        //alert("1990");
                        var x = msg.split(",");
                        //alert(x.length);

                        var i = 0;
                        do {
                            $("#" + x[i] + "wish").addClass('active');
                            $("#" + x[i] + "wish2").addClass('active');
                            i++;
                        } while (i < x.length)


                        //alert(msg);
                    }
                });
            }
            //alert("2004");
            


            /*點擊加入許願池*/ 
	        var favp = $('.product_favorite');
            

            if($('.favorite').length)
    	    {
    		    var favs = $('.favorite');

    		    favs.each(function()
    		    {
    			    var fav = $(this);
    			    var active = false;
    			    if(fav.hasClass('active'))
    			    {
    				    active = true;
    			    }

    			    fav.on('click', function()
    			    {
    				    if(active)
    				    {
    					    fav.removeClass('active');
    					    active = false;
    				    }
    				    else
    				    {
    					    fav.addClass('active');
    					    active = true;
    				    }
    			    });
    		    });
            }
            /*點擊加入許願池end*/
        });

        //when page load 執行事件
        //$(document).ready(function($)
        //{
        //    //alert("123");
        //});

        //新增購物車
        $('.shoppingcart').ready(function () {
            $('.shoppingcart').click(function () {

                $.ajax({
                    type: 'POST', url: 'car.aspx',
                    data: 'cd_id=' + $(".product_name").attr("id") + '&sc_num=' + $("#quantity_value").text(),
                    success: function (msg) {

                        if (msg != "Y") {
                            var number = parseInt($('.checkout_items').text(), 10);
                            $('.checkout_items').text(number + 1)
                            alert("新增購物車成功!!" + number);
                        }
                        else if (msg == "N") {
                            alert("請先登入後，再進行操作");

                            setTimeout(function () {
                                window.open("login.aspx");
                            }, 1000);
                        }
                        else if (msg == "R") {
                            alert("商品已在購物車!");
                        }
                        //alert(msg);
                    }
                })
            });
        });

        //刪除購物車
        $('.remove').ready(function () {
            $('.remove').click(function () {
                //alert($(this).attr("id"));
                $.ajax({
                    type: 'POST', url: 'delete_cart.aspx',
                    data: 'cd_id=' + $(this).attr("id"),
                    success: function (msg) {
                        
                        if (msg == "Y") {
                            var number = parseInt($('.checkout_items').text(), 10);
                            if (number > 1) {
                                $('.checkout_items').text(number - 1)
                            }
                            else {
                                $('.checkout_items').text(0)
                            }

                            alert("刪除商品成功!!");
                            setTimeout("window.location.reload()", 1500);

                        }
                        else if (msg == "N") {
                            alert("刪除失敗! 請重新操作!");

                            setTimeout("window.location.reload()", 1500);
                        }

                        //alert(msg)
                        //setTimeout("window.location.reload()", 1500);
                    }
                })
            });
        });

        function caltotal()
        {
            var total = 0; 

            $(".cal_total").each(function () {

                total = total + parseInt($(this).text().substr(1),10);
                
 
	            });
            //alert(total);
            $('#final_price').text("$"+total);

        
        }
    </script>
</body>
</html>
