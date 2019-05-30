<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html>
<head>
    <title>衣見鍾情</title>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="Shortcut Icon" type="image/x-icon" href="img/icon.png" />
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
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"> <%--tag搜尋--%>
    <%--購物車CSS--%>
    <link rel="stylesheet" type="text/css" media='all' href="styles/carts.css">
    <link rel='stylesheet' id='woocommerce-smallscreen-css' href='https://colorlib.com/tyche/wp-content/plugins/woocommerce/assets/css/woocommerce-smallscreen.css?ver=3.5.1' type='text/css' media='only screen and (max-width: 768px)' />
    <%--colorpicker--%>
    <link rel="stylesheet" type="text/css" href="styles/colorpicker.css" />
    <link rel="stylesheet" media="screen" type="text/css" href="styles/layout.css" />

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

<body onload="noticeshow()">
    <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                
                                                <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
                                                    <ul class="account_selection">
                                                        <li><a href="?page=login"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
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
                                                <li><asp:Label ID="lb_hello" runat="server" Text=""  Visible="true"></asp:Label></li>
                                                <li><asp:Button ID="btn_userlogout" runat="server" Text="登出" OnClick="btn_userlogout_Click" class="btn  btn-dark btn-block btn_nodisplay"/></li>
                                                <li class="account"><a href="?page=profile"><i class="fa fa-user" aria-hidden="true"></i></a>                                                   
                                                </li>
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

                                        </nav>

                                        <%--我是分割線管理者功能選單--%>
                                        <nav id="nav_administrator" class="navbar" runat="server">
                                            <ul class="navbar_menu">
                                                <li><a href="Default.aspx">首頁</a></li>
                                                <li><a href="?page=scan">掃描</a></li>
                                                <li><a href="?page=produceqr">產生商品QRcode</a></li>
                                                
                                                <li><a href="?page=admmm">會員管理</a></li>
                                                <li><a href="?page=admcd">商品管理</a></li>
                                                <li><a href="?page=admod">訂單管理</a></li>
                                            </ul>
                                            <ul class="navbar_user">                                                
                                                <li><asp:Button ID="btn_admlogout" runat="server" Text="登出" OnClick="btn_userlogout_Click" class="btn btn-dark btn-block"/></li>

                                            </ul>
                                            <div class="hamburger_container">
                                                <i class="fa fa-bars" aria-hidden="true"></i>
                                            </div>
                                            <div>
                                                <asp:Label ID="lb_hello2" runat="server" Text="" CssClass="col-lg-9" Visible="False"></asp:Label>
                                                
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
                                        <div class="filter_button"><span>Go</span></div>
                                    </div>

                                    <!-- Sizes -->
                                  <%--  <div class="sidebar_section">
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
                                    </div>--%>

                                    <!-- TAGS -->
                                    <%--<div class="sidebar_section">
                                        
                                        <ul class="checkboxes">
                                  
                                            <div class="articlebody">
                                                        <asp:PlaceHolder ID="sql_tags" runat="server"></asp:PlaceHolder>
                                                    </div>


                                        </ul>
                                        <div class="show_more">
                                            <span><span>+</span>Show More</span>
                                        </div>
                                    </div>--%>

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
                                                            <span class="type_sorting_text">價格</span>
                                                            <i class="fa fa-angle-down"></i>
                                                            <ul class="sorting_type">
                                                                
                                                                <li class="type_sorting_btn" data-isotope-option='{ "sortBy": "price" }'><span>價格</span></li>
                                                                <li class="type_sorting_btn" data-isotope-option='{ "sortBy": "name" }'><span>商品名稱</span></li>
                                                            </ul>
                                                        </li>                                                        
                                                    </ul>                                                 
                                                </div>

                                                <!-- Product Grid -->

                                                <div class="product-grid">
                                                    <!-- 迴圈開始 -->
                                                    <div class="articlebody">
                                                        <asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
                                                    </div>
                                                    <!-- 迴圈結束 -->

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
               
                        <%--縮小訪客介面--%>
                        <nav id="nav_menu_nologin" class="navbar" runat="server">                                
                            <div class="fs_menu_overlay"></div>
                            <div class="hamburger_menu">
                            <div class="hamburger_close"><i class="fa fa-times" aria-hidden="true"></i></div>
                            <div class="hamburger_menu_content text-right">
                                 <ul class="menu_top_nav">
                                     <li class="menu_item"><a href="Default.aspx">首頁</a></li>
                                     <li class="menu_item"><a href="?page=scan">掃描</a></li>
                                     <li class="menu_item"><a href="?kind=EVENTS">商品瀏覽</a></li>
                                </ul>
                            </div>
                            </div>
                        </nav>
                     

                        <%--縮小會員選單--%>
                        <nav id="nav_menu_user" class="navbar" runat="server" visible="false">

                            <div class="fs_menu_overlay"></div>
                            <div class="hamburger_menu">
                            <div class="hamburger_close"><i class="fa fa-times" aria-hidden="true"></i></div>
                            <div class="hamburger_menu_content text-right">
                             <ul class="menu_top_nav">
                                <li class="menu_item"><a href="Default.aspx">首頁</a></li>
                                <li class="menu_item"><a href="?page=scan">掃描</a></li>
                                <li class="menu_item"><a href="?kind=EVENTS">商品瀏覽</a></li>
                                <li class="menu_item"><a href="?page=wish">許願池</a></li>
                                <li class="menu_item"><a href="?page=order">我的訂單</a></li>
                                <li class="menu_item"><a href="?page=logout">登出</a></li>

                            </ul>
                        <%--<ul class="navbar_user">
                        <li class="account"><a href="?page=profile"><i class="fa fa-user" aria-hidden="true"></i></a></li> --%>
                             </div>
                             </div>
                        </nav>	


                <%--logo--%>
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
                    <!-- 優惠倒數 -->

                    <div id="nope_display" class="deal_ofthe_week" runat="server" visible="false" >
		    <div class="container">               
			    <div class="row align-items-center">
                    
				    <div class="col-lg-6">
					    <div class="deal_ofthe_week_img">
						    <img src="images/love.png" alt="">
					    </div>
				    </div>
				    <div class="col-lg-6 text-right deal_ofthe_week_col">
					    <div class="deal_ofthe_week_content d-flex flex-column align-items-center float-right">
						    <div class="section_title">
							    <h2 style="color:#FF8888">歡迎使用 FirstSight!</h2>
						    </div>
						    
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>

                    <!-- 首頁商品PH1 -->
                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>



                    

                    <!-- 熱銷商品PH2 -->

                    <div class="articlebody">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                    </div>

                   <!-- 下方優惠 -->

                    <div class="benefit">
                        <div class="container">
                            <div class="row benefit_row">
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-truck" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>12/8前全館商品免運</h6>
                                            <p></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-money" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>貨到付款</h6>
                                            <p></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-undo" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>30天內不滿意退、換貨</h6>
                                            <p></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3 benefit_col">
                                    <div class="benefit_item d-flex flex-row align-items-center">
                                        <div class="benefit_icon"><i class="fa fa-clock-o" aria-hidden="true"></i></div>
                                        <div class="benefit_content">
                                            <h6>不分晝夜，全年無休</h6>
                                            <p></p>
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
                           
                            <div class="col-lg-4 " >
                                <input type="hidden" id="f_total" runat='server' />
                                <asp:Label ID="Label4" runat="server" Text="付款方式:"></asp:Label>
                                <asp:RadioButton ID="rbt_comepay" runat="server" GroupName="payment" Text="貨到付款" />
                                <asp:RadioButton ID="rbt_bank" runat="server" GroupName="payment" Text="銀行轉帳" />                              
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
                                <asp:CheckBox ID="ckb_takemmadr" visible="false" runat="server" Text="同會員資料地址" OnCheckedChanged="ckb_takemmadr_CheckedChanged" AutoPostBack="True" Checked="True" />
                                <br>
                                <br>
                                <asp:Label ID="Label1" runat="server" Text="聯絡電話:"></asp:Label>&nbsp;<asp:TextBox ID="tb_odrphone" runat="server" TextMode="Number"></asp:TextBox>
                                <br>
                                <asp:CheckBox ID="ckb_takemmphone" Text="同會員資料電話" runat="server" AutoPostBack="True" OnCheckedChanged="ckb_takemmphone_CheckedChanged" Checked="True" />
                                <br>

                                <br>

                                <div class="form-group">
                                    <textarea name="message" id="odr_message" class="form-control" rows="4" placeholder="備註" runat="server"></textarea>
                                    <p class="help-block text-danger"></p>
                                </div>
                            </div>
                            <div class="main_sl_1g pay">
                                <asp:Button ID="btn_checkout" runat="server" Text="結帳" class="btn btn-lg btn-success btn-block" onClick="btn_checkout_Click"/>
                            </div>
                        </div>
                        <div id="cart_noitem" runat="server" visible="true">
                            <H2>購物車尚未有商品!</H2>
                        </div>
                    </div>
                </div>
                
                <asp:Label ID="lb_temp_tag" runat="server" Text="" Visible="false"></asp:Label>
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
                            <div id="wish_noitem" runat="server" visible="true">
                            <H2>許願池尚未有商品!</H2>
                        </div>
                        </div>
                    </div>
                    <%--許願第一排--%>

                     <div class="articlebody"><asp:PlaceHolder ID="PH_wish" runat="server"></asp:PlaceHolder></div>
                    


                 
                    <%--<div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title">
                                    <h2>Following Tags</h2>
                                </div>
                            </div>
                        </div>                    
                    </div>
                    
                    <div class="main_s2_1g">
                        <div class="widget widget_tag_cloud text-center">
                            <div class="tagcloud">
                                <div class="articlebody"><asp:PlaceHolder ID="PH_wishtag" runat="server"></asp:PlaceHolder></div>
                                
                                
                            </div>
                        </div>
                    </div>--%>
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
                        <h4>待處理訂單</h4>
                        <div class="articlebody" id="order_table1" runat="server" Visible="false">
                            <asp:PlaceHolder ID="PH_order1" runat="server" ></asp:PlaceHolder>
                        </div>
                    <div id="order_noitem2" runat="server" visible="true">
                            <h2 style="color:darkred">尚未有待處理訂單!</h2>
                        </div> 
                    </div>
                    <div class="container"><hr  style="width:100%;background-color:blue"></div>
                    
                    <div class="container">
                        <div class="row">
                        </div>
                        <h4>訂單紀錄</h4>
                        <div class="articlebody" id="order_table" runat="server" Visible="false">
                            <asp:PlaceHolder ID="PH_order" runat="server" ></asp:PlaceHolder>
                        </div>
                    <div id="order_noitem" runat="server" visible="true">
                            <h2 style="color:darkred">尚未有訂單紀錄!</h2>
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
                                    <asp:TextBox ID="tb_pfphone" class="form-control" runat="server" Visible="false" TextMode="Number"></asp:TextBox>
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
                                <asp:Button ID="btn_pfpwdsubmit" runat="server" Text="確定修改密碼" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_pfpwdsubmit_Click"/>
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
                                    <asp:TextBox ID="tb_rgphone" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>信箱</h5>
                                    <asp:TextBox ID="tb_rgmail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
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
                <%-- Login START--%>
                <div id="login_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>登入</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    
                   
                                <div class="form-group">
                                    <asp:TextBox ID="tb_act" runat="server" class="form-control"  placeholder="帳號:"></asp:TextBox>                                    
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="tb_pwd" runat="server" class="form-control"  placeholder="密碼:" TextMode="Password"></asp:TextBox>
                                    <asp:Label ID="lb_error" runat="server" Text="" ForeColor="Red"></asp:Label>                                    
                                </div>
                                <%--<div class="checkbox">
                                    <label>
                                        <input name="remember" type="checkbox" value="Remember Me"/>Remember Me
                                    </label>
                                </div>--%>
                                <!-- Change this to a button or input when using this as a form -->
                                <asp:Button ID="btn_submit" runat="server" Text="登入" class="btn btn-lg btn-success btn-block" OnClick="btn_submit_Click"/> 
                                
                                <asp:Button ID="btn_back" runat="server" Text="返回" class="btn btn-lg btn-danger btn-block" OnClick="btn_back_Click"/> 
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6" style="float:right"><asp:Button ID="btn_fgpwd" runat="server" Text="忘記密碼" class="btn btn-lg btn-link btn-block" OnClick="btn_fgpwd_Click"/></div>
                           
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Login END--%>
                <%--忘記密碼驗證 start--%>

                 <div class="main_slider " id="fgpw_display" runat="server" visible="false">
        <div class="container">
            <div class="row">
            <div class="col-md-4 mar ">
                
                    <div class="panel-heading">
                        <h3 class="panel-title">請輸入會員帳號</h3>
                    </div>
                    <div class="panel-body">
                        
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox ID="tb_actvf" runat="server" class="form-control"  placeholder="請輸入會員帳號"></asp:TextBox>      
                                    <asp:Label ID="lb_rig" runat="server" Text="無此會員帳號" Visible="false" ForeColor="red"></asp:Label>
                                </div>
                                <div id="div_vfkey" class="form-group" runat="server" visible="false">
                                    <asp:TextBox ID="tb_vfkey" runat="server" class="form-control"  placeholder="請輸入信箱驗證碼"></asp:TextBox>      
                                    <asp:Label ID="lb_vferror" runat="server" Text="請輸入正確驗證碼" Visible="false" ForeColor="red"></asp:Label>
                                </div>
                                <asp:Button ID="btn_pwchange" runat="server" Text="確認" class="btn btn-lg btn-success btn-block" OnClick="btn_pwchange_Click" Visible="false"/>
                                <asp:Button ID="btn_actvf" runat="server" Text="發送" class="btn btn-lg btn-success btn-block" OnClick="btn_actvf_Click"/>
                                <asp:Button ID="btn_backatvf" runat="server" Text="返回" class="btn btn-lg btn-danger btn-block" OnClick="btn_backatvf_Click"/> 
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
            </div>



                 <%--忘記密碼驗證 end--%>
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
                        <div style="float: left">
                            <asp:Button ID="btn_adm_newadm" runat="server" Text="新增管理員" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_newadm_Click"/>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_adm_newuser" runat="server" Text="新增會員" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_newuser_Click"/>
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_Member" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="mm_no" DataSourceID="SqlDataSource_mm"
                                Width="100%" OnRowCommand="GV_RowCommand_mm" CssClass="gridview" BorderWidth="0px">
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
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" CssClass="pagination-ys"/>

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
                            <asp:Button ID="btn_resetpw" runat="server" Text="重設會員密碼" class="btn btn-lg btn-block btn-link" OnClick="btn_resetpw_Click"/></div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_mmdetailback" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_mmdetailback_Click"/></div>
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
                                        <asp:TextBox ID="tb_adm_mmphone" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="Number"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="信箱"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmmail" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmmail" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="Email"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="生日"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_mmbd" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_mmbd" runat="server" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="Date"></asp:TextBox>
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
                <div id="adm_newadm_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>新增管理員</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>帳號</h5>
                                    <asp:TextBox ID="tb_adm_act" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>密碼</h5>
                                    <asp:TextBox ID="tb_adm_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>      
                                    <p class="help-block text-danger"></p>
                                    <h5>確認密碼</h5>
                                    <asp:TextBox ID="tb_adm_pwck" class="form-control" runat="server" TextMode="Password"></asp:TextBox>                                  
                                    <p class="help-block text-danger"></p>
                                    <h5>姓名</h5>
                                    <asp:TextBox ID="tb_adm_name" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <p id="p_addadmerror" runat="server" style="color:red" visible="false"></p>
                                </div>
                                <asp:Button ID="btn_addadm" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_addadm_Click" />
                                <asp:Button ID="btn_addadmback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" OnClick="btn_addadmback_Click" />
                            </div>
                        </div>
                    </div>                                     
                </div>


                <%--管理員-新增管理員END--%>

                <%--管理員-新增會員START--%>
                <div id="adm_newuser_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>新增會員</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>帳號</h5>
                                    <asp:TextBox ID="tb_addu_act" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>密碼</h5>
                                    <asp:TextBox ID="tb_addu_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>      
                                    <p class="help-block text-danger"></p>
                                    <h5>確認密碼</h5>
                                    <asp:TextBox ID="tb_addu_pwck" class="form-control" runat="server" TextMode="Password"></asp:TextBox>                                  
                                    <p class="help-block text-danger"></p>
                                    <h5>姓名</h5>
                                    <asp:TextBox ID="tb_addu_name" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>手機</h5>
                                    <asp:TextBox ID="tb_addu_phone" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>信箱</h5>
                                    <asp:TextBox ID="tb_addu_mail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>地址</h5>
                                    <asp:TextBox ID="tb_addu_adr" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <h5>生日</h5>
                                    <asp:TextBox ID="tb_addu_bd" class="form-control" runat="server" TextMode="Date"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <asp:CheckBox ID="ckb_addu_notice" runat="server" Text="接受許願池通知" />
                                    <p id="p_addusererror" runat="server" style="color:red" visible="false"></p>
                                </div>
                                <asp:Button ID="btn_adduser" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_adduser_Click" />
                                <asp:Button ID="btn_adduserback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" OnClick="btn_adduserback_Click" />
                            </div>
                        </div>
                    </div>                                     
                </div>
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
                        <div class="row" style="height: 20px; padding-top: 20px">

                            <div class="col-4">
                                <asp:Button ID="btn_adm_newcd" runat="server" Text="新增商品" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_newcd_Click"/>

                            </div>
                            <div class="col-4">
                                <asp:Button ID="btn_adm_tag" runat="server" Text="TAG" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_tag_Click"/>

                            </div>
                            <div class="col-4">
                                <asp:Button ID="btn_adm_pe" runat="server" Text="優惠活動" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_pe_Click"/>

                            </div>
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_cd" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="cd_no" DataSourceID="SqlDataSource_cd"
                                Width="90%" OnRowCommand="GV_RowCommand_cd" CssClass="gridview" BorderWidth="0px" PagerSettings-Mode="Numeric">
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
                                    <asp:BoundField DataField="cd_id" HeaderText="商品編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_name" HeaderText="商品名稱"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_group" HeaderText="GROUP"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_place" HeaderText="放置區域"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="cd_inserttime" HeaderText="新增時間"></asp:BoundField>


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
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" cssclass="pagination-ys"/>

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
                <div id="adm_cddetail_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>商品詳細資料</h2>                                  
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_cddetailback" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_cddetailback_Click"/></div>
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
                                        <asp:Label ID="lb_adm_cdname" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdname" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label18" runat="server" Text="Group"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdgroup" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdgroup" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="顏色"></asp:Label></td>
                                    <td>
                                        <div id="div_adm_cdcolor" runat="server"></div>
                                        <div id="colorSelector" runat="server" visible="false"><div id="div_adm_colorpicker" runat="server"></div></div>
                                        <input type="hidden" id="temp_colorhex" runat="server" />
                                        <asp:Label ID="lb_colorcode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="尺寸"></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="ckb_adm_s" runat="server" Text="S" Enabled="False" /><br/>
                                        <asp:CheckBox ID="ckb_adm_m" runat="server" Text="M" Enabled="False"/><br/>
                                        <asp:CheckBox ID="ckb_adm_l" runat="server" Text="L" Enabled="False"/><br/>
                                        <asp:CheckBox ID="ckb_adm_xl" runat="server" Text="XL" Enabled="False"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="種類"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdkind" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdkind" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Text="材質"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdmaterial" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdmaterial" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Text="價格"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdprice" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdprice" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="Number"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Text="製造地"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdmadein" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdmadein" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text="商品擺放區"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdplace" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_cdplace" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                
                                    <td>
                                        <asp:Label ID="Label43" runat="server" Text="Tag"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_adm_cdtag" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:CheckBoxList ID="ckblist_adm_cd_tag" runat="server" DataSourceID="SqlDataSource_admcdtag" DataTextField="tag_name" DataValueField="tag_id" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ckblist_adm_cd_tag_SelectedIndexChanged"></asp:CheckBoxList>
                                        <asp:SqlDataSource runat="server" ID="SqlDataSource_admcdtag" ConnectionString='<%$ ConnectionStrings:DBConnect %>' SelectCommand="SELECT DISTINCT [tag_id], [tag_name] FROM [Tags]"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" Text="商品圖片"></asp:Label></td>
                                    <td>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:Image ID="img_cd" runat="server" Height="100px" Width="100px"/>
                                            </div>
                                            <div class="col-lg-6">
                                                <div id="div_adm_editcdpic" runat="server" visible="false">
                                            <asp:FileUpload ID="FU_cdpic" runat="server" /><asp:Label ID="lb_cdpic" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                            </div>
                                        </div>
                                        
                                                                                                               
                                    </td>
                                </tr>
                                
                            </table>
                            
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_adm_cdedit" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_cdedit_Click"/>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="btn_adm_cdsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_adm_cdsubmit_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btn_adm_cdback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_adm_cdback_Click"/>
                        </div>
                    </div>
                </div>
                <%--管理員-商品詳細END--%>

                <%--新增TAG START--%>
                <div id="adm_newtag_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>新增TAG</h2>
                                </div>
                            </div>
                            
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_addtag_back" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_addtagback_Click"/></div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>TAG</h5>
                                    <asp:TextBox ID="tb_tag" class="form-control" runat="server"></asp:TextBox>
                                    <p class="help-block text-danger"></p>
                                    <asp:Button ID="btn_tagcheck" runat="server" Text="檢查" OnClick="btn_tagcheck_Click"/>
                                </div>
                                <asp:Label ID="lb_check" Visible="false" runat="server" Text="HI"></asp:Label>
                                <asp:Button ID="btn_addtag" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_addtag_Click" />
                            </div>
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>商品編號</h5>
                                    <asp:TextBox ID="tb_cdhavetag" class="form-control" runat="server"></asp:TextBox>
                                    
                                    
                                </div>
                                <asp:Label ID="Label41" runat="server" Text="目前所選Tag:"></asp:Label>
                                <br />
                                <asp:Label ID="lb_cdhavetag" Visible="false" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                <asp:Button ID="btn_addhave" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_addhave_Click" />
                            </div>
                            <div class="col-lg-4 blog_item_col">
                                <asp:CheckBoxList ID="ckblist_tagname" runat="server" DataSourceID="SqlDataSource_tagname" DataTextField="tag_name" DataValueField="tag_id" AutoPostBack="True" OnSelectedIndexChanged="ckblist_tagname_SelectedIndexChanged"></asp:CheckBoxList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource_tagname" ConnectionString='<%$ ConnectionStrings:DBConnect %>' SelectCommand="SELECT DISTINCT [tag_name], [tag_id] FROM [Tags]"></asp:SqlDataSource>
                            </div>
                           
                            
                        </div>
                        
                    </div>                                     
                </div>
                <%--新增TAG END--%>

                <%--管理員-訂單管理START--%>
                <div id="adm_odm_display" runat="server" visible="false">


                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>訂單管理</h2>
                                </div>
                            </div>
                        </div>                        
                    </div>
                    <%--未完成訂單--%>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <h3>未處理訂單</h3>
                            <br />
                            <asp:GridView ID="GV_odn" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="od_no" DataSourceID="SqlDataSource_odn"
                                Width="80%" OnRowCommand="GV_RowCommand_odn" CssClass="gridview" BorderWidth="0px" PagerSettings-Mode="Numeric">
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
                                    <asp:BoundField DataField="od_id" HeaderText="訂單編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_mm_id" HeaderText="會員編號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_phone" HeaderText="連絡電話"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_total" HeaderText="總額"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_paymant" HeaderText="付款方式"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_delivery" HeaderText="到貨方式"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_d_address" HeaderText="到貨地址"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_note" HeaderText="備註"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_orderdate" HeaderText="訂單日期"></asp:BoundField>


                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="od_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="od_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" CssClass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                    <%--已完成訂單--%>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <h3>已處理訂單</h3>
                            <br />
                            <asp:GridView ID="GV_ody" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="od_no" DataSourceID="SqlDataSource_ody"
                                Width="80%" OnRowCommand="GV_RowCommand_ody" CssClass="gridview" BorderWidth="0px" PagerSettings-Mode="Numeric">
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
                                    <asp:BoundField DataField="od_id" HeaderText="訂單編號"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_mm_id" HeaderText="會員編號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_phone" HeaderText="連絡電話"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_total" HeaderText="總額"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_paymant" HeaderText="付款方式"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_delivery" HeaderText="到貨方式"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_d_address" HeaderText="到貨地址"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_note" HeaderText="備註"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="od_orderdate" HeaderText="訂單日期"></asp:BoundField>


                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="od_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="od_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" CssClass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>

                    <%--  --%>

                   
                </div>
                <%--管理員-訂單管理END--%>

                <%--管理員-訂單詳細START--%>
                <div id="adm_odmdetail_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>訂單明細</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_oddetailback" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_oddetailback_Click" />
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_oddetailadditem" runat="server" Text="新增商品" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_oddetailadditem_Click" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_oddetail" runat="server"
                                DataKeyNames="od_no"
                                Width="80%"
                                AutoGenerateEditButton="True"
                                AllowPaging="true"
                                OnRowCommand="GV_RowCommand_oddetail"
                                CssClass="gridview"
                                BorderWidth="0px"
                                OnRowEditing="GV_oddetail_RowEditing"
                                OnRowCancelingEdit="GV_oddetail_RowCancelingEdit"
                                OnRowUpdating="GV_oddetail_RowUpdating"
                                OnRowUpdated="GV_oddetail_RowUpdated"
                                OnPageIndexChanging="GV_oddetail_PageIndexChanging"
                                DataSourceID="SqlDataSource_oddetail" AutoGenerateColumns="False">

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

                                    <asp:TemplateField HeaderText="商品編號">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("odd_cd_id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>



                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_s_amount" HeaderText="S:數量"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_m_amount" HeaderText="M:數量"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_l_amount" HeaderText="L:數量"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="odd_xl_amount" HeaderText="XL:數量"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="dll_item_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" CssClass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                        <div class="col-lg-1 blog_item_col"></div>
                       
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width: 100%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    
                                    <th>付款方式</th>
                                    <th>取貨方式</th>
                                    <th>寄送地址</th>
                                    <th>連絡電話</th>
                                    <th>備註</th>
                                    <th>Done</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_adm_payment" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_payment" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:Label ID="lb_adm_delivery" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_delivery" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_adm_adr" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_adr" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_adm_phone" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_phone" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_adm_note" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_adm_note" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckb_done" runat="server" Enabled="False" />
                                    </td>
                                </tr>                               
                            </table>
                        </div>
                        <div class="col-lg-1 blog_item_col"></div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_adm_oddedit" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_oddedit_Click"/>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="btn_adm_oddsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_adm_oddsubmit_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btn_adm_oddback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_adm_oddback_Click"/>
                        </div>

                    </div>


                </div>

                <%-- 分割 --%>
                
                <%--管理員-訂單詳細END--%>
                <%--管理員-訂單詳細-新增商品START--%>
                <div id="div_odd_additem_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>訂單-新增商品</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>商品編號/商品名稱</h5>
                                    <asp:TextBox ID="tb_additeminput" class="form-control" runat="server"></asp:TextBox>
                                    <asp:Button ID="btn_sizeshow" runat="server" Text="確認" OnClick="btn_sizeshow_Click"/>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <div class="row">
                                        <asp:Label ID="lb_additems" runat="server" Text="S:數量" Visible="false"></asp:Label><asp:TextBox ID="tb_additems" runat="server" Text="0" Visible="false" Width="50px"></asp:TextBox>
                                    
                                    <asp:Label ID="lb_additemm" runat="server" Text="M:數量" Visible="false"></asp:Label><asp:TextBox ID="tb_additemm" runat="server" Text="0" Visible="false" Width="50px"></asp:TextBox>
                                    
                                    <asp:Label ID="lb_additeml" runat="server" Text="L:數量" Visible="false"></asp:Label><asp:TextBox ID="tb_additeml" runat="server" Text="0" Visible="false" Width="50px"></asp:TextBox>
                                    
                                    <asp:Label ID="lb_additemxl" runat="server" Text="XL:數量" Visible="false"></asp:Label><asp:TextBox ID="tb_additemxl" runat="server" Text="0" Visible="false" Width="50px"></asp:TextBox>
                                        
                                    </div>
                                    
                                </div>
                                <asp:Label ID="lb_additemmsg" Visible="false" runat="server" Text="HI"></asp:Label>
                                <asp:Button ID="btn_additem" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_additem_Click" />
                                <asp:Button ID="btn_additemback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" OnClick="btn_additemback_Click" />
                            </div>
                        </div>
                    </div>                                     
                </div>

                <%--管理員-訂單詳細-新增商品END--%>
                <%--管理員-新增商品START--%>
                <div id="div_addnewcd_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>新增商品</h2>                                  
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_addnewcd_back" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_addnewcd_back_Click"/></div>
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
                                        <asp:Label ID="Label3" runat="server" Text="商品名稱"></asp:Label></td>
                                    <td>                                        
                                        <asp:TextBox ID="tb_addnewcd_cdname" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Group"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_cdgroup" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="顏色"></asp:Label></td>
                                    <td>

                                        <div id="customWidget">
                                            <div id="colorSelector2">
                                                <div style="background-color: White"></div>
                                            </div>

                                        </div>
                                        <input type="hidden" id="temp_colorhex2" runat="server" />
                                        <asp:Label ID="lb_colorcode2" runat="server" Text=""></asp:Label>
                                        <%--<div id="colorSelector2" runat="server"><div id="div_adm_colorpicker2" runat="server"></div></div>

                                        
                                        <asp:Label ID="lb_colorcode2" runat="server" Text=""></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="尺寸"></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="ckb_addnewcd_s" runat="server" Text="S"  /><br/>
                                        <asp:CheckBox ID="ckb_addnewcd_m" runat="server" Text="M"  /><br/>
                                        <asp:CheckBox ID="ckb_addnewcd_l" runat="server" Text="L" /><br/>
                                        <asp:CheckBox ID="ckb_addnewcd_xl" runat="server" Text="XL" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Text="種類"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_kind" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Text="材質"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_cdmaterial" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label31" runat="server" Text="價格"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_cdprice" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED" TextMode="Number"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label33" runat="server" Text="製造地"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_cdmadein" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server" Text="商品擺放區"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_addnewcd_cdplace" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label44" runat="server" Text="Tag"></asp:Label></td>
                                    <td>
                                        <asp:CheckBoxList ID="ckblist_newcd_cdtag" runat="server" DataSourceID="SqlDataSource_tagname" DataTextField="tag_name" DataValueField="tag_id" AutoPostBack="True" OnSelectedIndexChanged="ckblist_newcd_cdtag_SelectedIndexChanged"></asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                            <asp:FileUpload ID="FU_cdimg" runat="server" /><asp:Label ID="lb_upmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
  
                        </div>
                        <br/>
                        <br/>
                        <br/>
<%--                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="Button5" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_cdedit_Click"/>
                        </div>--%>
                        <div class="col-lg-4"></div>
                        
                        <div class="col-lg-4 ">
                            <asp:Button ID="btn_adm_addnewcd_submit" runat="server" Text="新增" class="btn btn-lg btn-block btn-success"  OnClick="btn_adm_addnewcd_submit_Click"/>
                        </div>                        
                    </div>
                </div>
                <%--管理員-新增商品END--%>

                <%--管理員-優惠管理START--%>
                <div id="adm_event_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>優惠活動管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_pemback" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_pemback_Click"/>
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_newevent" runat="server" Text="新增優惠活動" class="btn btn-lg btn-block btn-link" OnClick="btn_admnewevent_Click"/>
                        </div>
                    </div>
                    <%--現在優惠活動start--%>
                    <div class="row blogs_container">
                        
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <h3>現在優惠活動</h3>
                            <asp:GridView ID="GV_pe" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="pe_title" DataSourceID="SqlDataSource_pe"
                                Width="80%" OnRowCommand="GV_RowCommand_pe" CssClass="gridview" BorderWidth="0px">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_title" HeaderText="優惠名稱"></asp:BoundField>
                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_starting_date" HeaderText="開始日期"></asp:BoundField>
                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_expiring_date" HeaderText="結束日期"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_status" HeaderText="活動狀態"></asp:BoundField>

                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="pe_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="pe_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" cssclass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                    </div>
                    <%--現在優惠活動end--%>
                    <%--過往優惠活動--%>
                    <div class="row blogs_container">
                        
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <h3>優惠活動紀錄</h3>
                            <asp:GridView ID="GV_pen" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="pe_title" DataSourceID="SqlDataSource_pen"
                                Width="80%" OnRowCommand="GV_RowCommand_pen" CssClass="gridview" BorderWidth="0px">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_title" HeaderText="優惠名稱"></asp:BoundField>
                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_starting_date" HeaderText="開始日期"></asp:BoundField>
                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_expiring_date" HeaderText="結束日期"></asp:BoundField>

                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_status" HeaderText="活動狀態"></asp:BoundField>

                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="pen_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="pen_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" cssclass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>
                    </div>
                    <%--過往優惠活動end--%>

                    <div class="col-10 text-center">
                    </div>

                </div>

                <%--管理員-優惠詳細資料START--%>
                <div id="adm_eventdetail_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>優惠活動詳細資料</h2>
                                </div>
                            </div>
                        </div>
                        
                        <div style="float: left">
                            <asp:Button ID="btn_ped_back" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_pedetailback_Click"/></div>
                        <div style="float: left">
                            <asp:Button ID="btn_ped_add" runat="server" Text="新增商品" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_pedetailadditem_Click" />
                        </div>
                    </div>
                    
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <asp:GridView ID="GV_ped" runat="server"
                                DataKeyNames="pe_cd_id"
                                Width="80%"
                                AllowPaging="true"
                                OnRowCommand="GV_RowCommand_pedetail"
                                CssClass="gridview"
                                BorderWidth="0px"
                                DataSourceID="SqlDataSource_pedetail" AutoGenerateColumns="False">

                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="pe_cd_id" HeaderText="商品編號" ></asp:BoundField>


                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="pe_count" HeaderText="活動售出數量"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="ped_item_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" cssclass="pagination-ys"/>

                                <RowStyle CssClass="td"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                        </div>

                        <div class="col-lg-1"></div>
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width:100%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    <th>活動標題</th>
                                    <th>開始時間</th>
                                    <th>結束時間</th>
                                    <th>主要優惠活動</th>
                                    <th>活動狀態</th>
                                    <th>輪播圖片</th>
                                    <th>折扣</th>

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lb_pe_title" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_pe_title" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_pe_stdate" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_pe_stdate" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_pe_exdate" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_pe_exdate" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckb_pe_main" runat="server" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckb_pe_status" runat="server" Enabled="False" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_pe_imgmsg" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:Image ID="img_pe" runat="server"  src="" Height="100px" Width="100px" visible="false"/>
                                    </td>
                                    <td>
                                        <asp:Label ID="lb_pe_discount" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_pe_discount" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                    
                                </tr>                               
                            </table>
                            <div id="div_FUpeimg" runat="server" visible="false" style="float:right">
                                <asp:FileUpload ID="FU_peimg" runat="server" /><asp:Label ID="lb_peimgmsg" runat="server" Text="" ForeColor="Red"></asp:Label><br/>
                                <asp:Button ID="btn_pdremoveimg" class="btn btn-lg btn-block btn-secondary" runat="server" Text="刪除照片" OnClick="btn_pdremoveimg_Click"/>
                            </div>
                            
                        </div>
                        
                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_adm_pededit" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_pededit_Click"/>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="btn_adm_pedsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_adm_pedsubmit_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btn_adm_pedback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_adm_pedback_Click"/>
                        </div>

                    </div>
                </div>
                <%--管理員-優惠詳細資料END--%>

                <%--管理員-優惠詳細資料-新增商品START--%>
                <div id="div_ped_additem_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>訂單-新增商品</h2>
                                </div>
                            </div>
                        </div>
                        <div class="row blogs_container">
                            <div class="col-lg-4 blog_item_col">
                                <div class="form-group">
                                    <h5>商品編號/商品名稱/KIND(kind_XX)</h5>
                                    <asp:TextBox ID="tb_ped_additeminput" class="form-control" runat="server"></asp:TextBox>
                                    <br/>
                                <asp:Label ID="lb_ped_additemsg" Visible="false" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:Button ID="btn_ped_additemsubmit" runat="server" Text="新增" class="btn btn-lg btn-block btn-success" OnClick="btn_ped_additemsubmit_Click" />
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Button ID="btn_ped_additemback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" OnClick="btn_ped_additemback_Click" />
                                        </div>                                                               
                                    </div>                                
                            </div>
                        </div>
                    </div>                                     
                </div>
                    </div>
                <%--管理員-優惠詳細資料-新增商品END--%>

                <%--管理員-新增優惠START--%>
                <div id="adm_newevent_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>新增優惠活動</h2>                                  
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_addnewpe_back" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_addnewpe_back_Click"/></div>
                    </div>

                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width: 80%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    <th style="padding-left: 10px;"></th>
                                    <th>活動資料</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="活動名稱"></asp:Label></td>
                                    <td>                                        
                                        <asp:TextBox ID="tb_newpetitle" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Text="商品名稱/商品編號/商品種類(kind_XX)"></asp:Label></td>
                                    <td>                                        
                                        <asp:TextBox ID="tb_newpecd" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED" TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="開始日期"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_newpest" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED" TextMode="DateTimeLocal"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label17" runat="server" Text="結束日期"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_newpeex" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED" TextMode="DateTimeLocal"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label36" runat="server" Text="折扣"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="tb_newpediscount" runat="server" Text="" BorderStyle="None" BorderWidth="0" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label32" runat="server" Text="主活動"></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="ckb_newpemain" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" Text="活動狀態"></asp:Label></td>
                                    <td>
                                        <asp:CheckBox ID="ckb_newpestatus" runat="server" />
                                    </td>
                                </tr>                               
                            </table>
                            <asp:FileUpload ID="FU_newpeimg" runat="server" /><asp:Label ID="Label39" runat="server" Text="" ForeColor="Red"></asp:Label>
  
                        </div>
                        <br/>
                        <br/>
                        <br/>
                        <div class="col-lg-4"></div>
                        
                        <div class="col-lg-4 ">
                            <asp:Button ID="btn_newpe_submit" runat="server" Text="新增" class="btn btn-lg btn-block btn-success"  OnClick="btn_newpe_submit_Click"/>
                        </div>                        
                    </div>                                     
                </div>
                <%--管理員-新增優惠END--%>
                <%--管理員-TAG管理START--%>
                <div id="adm_tag_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>TAG管理</h2>
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="btn_adm_tagmback" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_tagmback_Click" />
                        </div>
                        <div style="float: right">
                            <asp:Button ID="btn_adm_newtag" runat="server" Text="新增TAG" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_newtag_Click" />
                        </div>
                    </div>
                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <br />
                            <asp:GridView ID="GV_tag" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="tag_no" DataSourceID="SqlDataSource_tag"
                                Width="100%" OnRowCommand="GV_RowCommand_tag" CssClass="gridview" BorderWidth="0px" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <%--CELL[0] 這行是流水項次，不用改--%>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>

                                    <%--CELL[1] 主鍵值放在這裏(DataField,Sort...)，利用CSS設成不會show出來--%>
                                    <asp:BoundField DataField="tag_no" HeaderText="" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                        <HeaderStyle CssClass="hiddencol"></HeaderStyle>
                                        <ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>

                                    <%--列表欄位1 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="tag_id" HeaderText="TAG編號"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="tag_name" HeaderText="TAG名稱"></asp:BoundField>
                                    <%--列表欄位2 要改DataField和標題文字HeaderText--%>
                                    <asp:BoundField DataField="tag_date" HeaderText="TAG日期"></asp:BoundField>

                                    


                                    <%--明細按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DetailButton" runat="server" class="btn btn-lg btn-block btn-info"
                                                CommandName="tag_detail"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text=" 詳細 " />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>


                                    <%--刪除按鈕列 不用改--%>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="DelButton" runat="server" class="btn btn-lg btn-block btn-danger"
                                                CommandName="tag_delete"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                Text="刪除" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" cssclass="pagination-ys"/>

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
                <%--管理員-TAG管理END--%>
                <%--管理員-TAG詳細START--%>
                <div id="adm_tagdetail_display" runat="server" visible="false">
                    <div class="container">
                        <div class="row">
                            <div class="col text-center">
                                <div class="section_title top_1g">
                                    <h2>TAG詳細資料</h2>                                  
                                </div>
                            </div>
                        </div>
                        <div style="float: left">
                            <asp:Button ID="Button2" runat="server" Text="返回" class="btn btn-lg btn-block btn-link" OnClick="btn_adm_tagdetailback_Click"/></div>
                    </div>

                    <div class="row blogs_container">
                        <div class="col-lg-2 blog_item_col">
                        </div>
                        <div class="col-lg-10 blog_item_col">
                            <table style="border-width: 1px; border-style: solid; width: 80%; border-collapse: collapse;">
                                <tr style="color: White; background-color: #333333; font-weight: bold;">
                                    <th style="padding-left: 10px;"></th>
                                    <th>TAG資料</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label29" runat="server" Text="TAG編號"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_tag_id" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_tag_id" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Text="TAG名稱"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_tag_name" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                        <asp:TextBox ID="tb_tag_name" runat="server" Text="" BorderStyle="None" BorderWidth="0" Visible="false" ForeColor="RED"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label38" runat="server" Text="TAG日期"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_tag_date" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label40" runat="server" Text="TAG使用數"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lb_taguseamount" runat="server" Text="" ForeColor="Blue"></asp:Label>
                                    </td>
                                </tr>
                                
                                
                            </table>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-2">
                            <asp:Button ID="btn_adm_tagedit" runat="server" Text="修改資料" class="btn btn-lg btn-block btn-dark" OnClick="btn_adm_tagedit_Click"/>
                        </div>
                        <div class="col-lg-5"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3 ">
                            <asp:Button ID="btn_adm_tagsubmit" runat="server" Text="確定" class="btn btn-lg btn-block btn-success" Visible="false" OnClick="btn_adm_tagsubmit_Click"/>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btn_adm_tagback" runat="server" Text="返回" class="btn btn-lg btn-block btn-danger" Visible="false" OnClick="btn_adm_tagback_Click"/>
                        </div>
                    </div>
                </div>
                <%--管理員-TAG詳細END--%>
                    


                <!-- Footer -->

                <footer class="footer">
                    <div class="container">
                        <div class="row">
                            
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="footer_nav_container">
                                    <div class="cr"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>

                
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
                <asp:SqlDataSource ID="SqlDataSource_ody" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceODy_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_odn" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceODn_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_oddetail" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceODDETAIL_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_pe" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourcePE_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_pen" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourcePEn_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_pedetail" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourcePED_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource_tag" runat="server"
                        ConnectionString="<%$ ConnectionStrings:DBConnect %>"
                        OnSelecting="SqlDataSourceTAG_Selecting"
                        SelectCommand="SELECT '1';"
                        DeleteCommand="SELECT '1';"></asp:SqlDataSource>
                 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Timer ID="Timer_pe" runat="server" Enabled="False" Interval="1000" OnTick="Timer_pe_Tick"></asp:Timer>
                
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
    <%--colorpicker--%>
    <%--<script src="js/jquery.js"></script>--%>
	<script src="js/colorpicker.js"></script>
    <script src="js/eye.js"></script>
    <script src="js/utils.js"></script>
    <script src="js/layout.js?ver=1.0.2"></script>
    <%--TAG搜尋--%>
    <%--<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

    <%--購物車--%>
    <script>
        /*購物車商品數量start*/
        $('.sminus').ready(function () {
            $('.sminus').click(function () {
                var sc_cd_id = $(this).attr("id");
                sc_cd_id = sc_cd_id.substr(0, 10);
                var sc_cd_idprice = sc_cd_id + "price"; //總額
                var sc_cd_iddiscountprice = sc_cd_id + "discountprice"; //優惠總額
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
                if (!isNaN(parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()))) { total += parseInt($("#" + sc_cd_id + [id$ = 'xlvalue']).text()); }
                

                $.ajax({
                    type: 'POST', url: 'caltotal.aspx',
                    data: 'sc_cd_id=' + sc_cd_id + '&temp_szamount=' + sc_cd_id+",S,minus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",S,plus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",M,minus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",M,plus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",L,minus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",L,plus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",XL,minus",
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
                    data: 'sc_cd_id=' + sc_cd_id+ '&temp_szamount=' + sc_cd_id+",XL,plus",
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
        </script>
         <script>
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
             </script>
    <script>
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
                        for (i = 0; i < x.length;i++) {
                            $("#" + x[i] + "wish").addClass('active');
                            $("#" + x[i] + "wish2").addClass('active');


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
                        }                        
                    }
                });
            }

           

           
        });

        //when page load 執行事件
        //$(document).ready(function($)
        //{
        //    //alert("123");
        //});
        </script>
    <script>
        //新增購物車
        $('.shoppingcart').ready(function () {
            $('.shoppingcart').click(function () {

                $.ajax({
                    type: 'POST', url: 'car.aspx',
                    data: 'cd_id=' + $(".product_name").attr("id") + '&sc_num=' + $("#quantity_value").text(),
                    success: function (msg) {

                        if (msg == "Y") {
                            var number = parseInt($('.checkout_items').text(), 10);
                            $('.checkout_items').text(number + 1)
                            alert("新增購物車成功!!");
                        }
                        else if (msg == "N") {
                            alert("請先登入後，再進行操作");

                            setTimeout(function () {
                                window.open("?page=login");
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
        </script>
         <script>
        //許願池取消追蹤
        $('.delete_wish').ready(function () {
            $('.delete_wish').click(function () {                 
                //alert($(this).attr("id"));
                $.ajax({
                    type: 'POST', url: 'wish_delete.aspx',
                    data: 'cd_id=' + $(this).attr("id"),
                    success: function (msg) {
                     alert("刪除已取消追蹤!!");
                   setTimeout("window.location.reload()", 500);
                    }
                })
            });
        });
        
        </script>
         <script>
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
            var f_total = document.getElementById("f_total");
                f_total.value = total;
            $('#final_price').text("$"+total);
        }

         //search tag
        
    </script>



    <%--colorpicker--%>
    <script>
        $('#colorSelector').ColorPicker({
	color: '#0000ff',
	onShow: function (colpkr) {
		$(colpkr).fadeIn(500);
		return false;
	},
	onHide: function (colpkr) {
		$(colpkr).fadeOut(500);
		return false;
	},
	onChange: function (hsb, hex, rgb) {
        $('#colorSelector div').css('backgroundColor', '#' + hex);
        $('#lb_colorcode').text("#" + hex);
        var a = document.getElementById("temp_colorhex");
        a.value = hex;
        
	}
        });       
    </script>

    <script>
        $('#colorSelector2').ColorPicker({
	color: '#ffffff',
	onShow: function (colpkr) {
		$(colpkr).fadeIn(500);
		return false;
	},
	onHide: function (colpkr) {
		$(colpkr).fadeOut(500);
		return false;
	},
	onChange: function (hsb, hex, rgb) {
        $('#colorSelector2 div').css('backgroundColor', '#' + hex);
        $('#lb_colorcode2').text("#" + hex);
        var b = document.getElementById("temp_colorhex2");
        b.value = hex;
        
	}
        });
              
    </script>
    <%--<script>
    //追蹤TAG start
        $('.wish_tag').ready(function () {
            $('.wish_tag').click(function () {
                var txt;
                var r = confirm("確定要刪除此標籤嗎?");

                if (r == true) {

                    $.ajax({
                    type: 'POST', url: 'wish_tag.aspx',
                    data: 'tag_id=' + $(this).attr("id"),
                    success: function (msg) {
                        
                        if (msg == "Y") {
                           alert("追蹤成功!!");
                        }
                        else if (msg == "N") {
                            alert("請先登入後，再進行操作");

                            setTimeout(function () {
                                window.open("login.aspx");
                            }, 1000);
                        }
                        else if (msg == "R") {
                            alert("此TAG已在許願池!");
                        }
                        else if (msg == "NO") {
                            alert("發生未知錯誤請重新操作!");
                        }
                    }
                    })

                }
                else {
                    txt = "You pressed Cancel!";
                    alert(txt);
                }
                //alert(txt);
                alert($(this).attr("id"));
            });
        });
        //追蹤tag end
        </script>
    
    <!-- Display Notification -->
    
    
    <script type="text/javascript">
    
    var timeStart, timeEnd, time;

    
    function getTimeNow() {
        var now = new Date();
        return now.getTime();
    }

    function holdDown() {
        //获取鼠标按下时的时间
        timeStart = getTimeNow();

        //setInterval会每100毫秒执行一次，也就是每100毫秒获取一次时间
        time = setInterval(function () {
            timeEnd = getTimeNow();

            //如果此时检测到的时间与第一次获取的时间差有1000毫秒
            if (timeEnd - timeStart > 1000) {
                //便不再继续重复此函数 （clearInterval取消周期性执行）
                clearInterval(time);
                //字体变红
                alert('R!');
            }
        }, 100);
    }
    function holdUp() {
        //如果按下时间不到1000毫秒便弹起，
        clearInterval(time);
    }
       
    </script>--%>
    <%--通知--%>
    <script>
        function noticeshow() {
            var notification = null;
            function ShowNotification(title, body,url) {
                notification = new Notification(title, {
                    icon: '/img/iconbig.png',
                    body: body,
                    noscreen:true ,
                    onclick: function () {
                       
                        
                        parent.focus();
                        window.focus();
                        //just in case, older browsers
                        
                        this.close();
                    }
                })
                notification.onclick = function(e) { // Case 3: 綁定 click 事件
                    e.preventDefault();
                    window.open(url,'_self');
                                }

            }
            
            var result_ck = "<%=check_setting()%>";//JavaScript中呼叫C#後臺的函式
            var result_ans = result_ck.split(",");

            
            //alert(result_ck);

            if (result_ans[0]== "Y") {
                if (Notification && Notification.permission === 'default') {
                    Notification.requestPermission(function (permission) {
                        if (!('permission' in Notification)) {
                            Notification.permission = permission;
                        }
                    });
                }
                else if (Notification.permission === 'granted') {
                    ShowNotification("許願池通知", "許願池有"+result_ans[1]+"項商品正在特價快去實現願望!",'Default.aspx?page=wish');
                    //三秒後自動關閉
                    setTimeout(notification.close.bind(notification), 3000);
                    var user_close_nt = "<%=close_notice()%>";
                }
                else {
                    alert('請檢查是否你的瀏覽器支援');
                }

            }

            if (result_ans[0]== "ADM") {
                if (Notification && Notification.permission === 'default') {
                    Notification.requestPermission(function (permission) {
                        if (!('permission' in Notification)) {
                            Notification.permission = permission;
                        }
                    });
                }
                else if (Notification.permission === 'granted') {
                    ShowNotification("新訂單通知", "尚有"+result_ans[1]+"筆未處理訂單!",'Default.aspx?page=admod');
                    //三秒後自動關閉
                    setTimeout(notification.close.bind(notification), 3000);
                    var adm_close_nt = "<%=close_notice()%>";
                }
                else {
                    alert('請檢查是否你的瀏覽器支援');
                }

            }
            
        }
    </script>
    <script>
        function initTimer()
    {
    	if($('.timer').length)
    	{
    		// Uncomment line below and replace date
            
            var petime = "<%=pe_datetime()%>";
            //petime(bbbbb);

            var target_date = new Date(petime).getTime();
            //alert(bbbbb);
	    	// comment lines below
	    	//var date = new Date();
	    	//date.setDate(date.getDate() + 3);
	    	//var target_date = date.getTime();
	    	//----------------------------------------
	 
			// variables for time units
			var days, hours, minutes, seconds;

			var d = $('#day');
			var h = $('#hour');
			var m = $('#minute');
			var s = $('#second');

			setInterval(function ()
            {
                if (days == 0 && hours == 0 && minutes == 0 && seconds == 0)
                {
                    
                    
                    setTimeout("window.location.reload()", 500);

                }
			    // find the amount of "seconds" between now and target
			    var current_date = new Date().getTime();
			    var seconds_left = (target_date - current_date) / 1000;
			 
			    // do some time calculations
			    days = parseInt(seconds_left / 86400);
			    seconds_left = seconds_left % 86400;
			     
			    hours = parseInt(seconds_left / 3600);
			    seconds_left = seconds_left % 3600;
			     
			    minutes = parseInt(seconds_left / 60);
			    seconds = parseInt(seconds_left % 60);

			    // display result
			    d.text(days);
			    h.text(hours);
			    m.text(minutes);
			    s.text(seconds); 
			 
			}, 1000);
    	}	
    }
    </script>
    
</body>
</html>
