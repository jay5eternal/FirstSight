<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>

<html>
    <head>
        <title>衣見鍾情</title>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!--CSS樣式-->
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
        <style>
            @import url(https://fonts.googleapis.com/earlyaccess/notosanstc.css);
            html,body,h1,h2,h3,h4,h5,h6 {font-family: "Noto Sans TC", sans-serif}
            * {
            font-family: font-family:'Noto Sans TC', sans-serif;
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
							    <a href="#"><span>衣</span>見鍾情</a>
						    </div>
                            <%--遊客功能選單--%>
                            <nav id="nav_nologin" class="navbar" runat="server">
							    <ul class="navbar_menu">
								    <li><a href="Default.aspx">首頁</a></li>
								    <li><a href="?page=scan">掃描</a></li><%--先暫時坐在這邊--%>
								    <li><a href="#">promotion</a></li>
								    <li><a href="#">pages</a></li>
								    <li><a href="#">blog</a></li>
								    <li><a href="contact.html">contact</a></li>
							    </ul>
							    <ul class="navbar_user">
								    <li><a href="#"><i class="fa fa-search" aria-hidden="true"></i></a></li>
								    <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
									    <ul class="account_selection">
                                            
										    <li><a href="/login.aspx"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
										    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>註冊</a></li>
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
								    <li><a href="?page=scan">掃描</a></li><%--先暫時坐在這邊--%>
								    <li><a href="#">promotion</a></li>
								    <li><a href="#">pages</a></li>
								    <li><a href="#">blog</a></li>
								    <li><a href="contact.html">contact</a></li>
							    </ul>
							    <ul class="navbar_user">
								    <li><a href="#"><i class="fa fa-search" aria-hidden="true"></i></a></li>
								    <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
									    <ul class="account_selection">
                                            
										    <li><a href="/login.aspx"><i class="fa fa-sign-in" aria-hidden="true"></i>資料設定</a></li>
										    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>登出</a></li>
									    </ul>
								    </li>
								    <li class="checkout">
									    <a href="#">
										    <i class="fa fa-shopping-cart" aria-hidden="true" ></i>
										    <span id="checkout_items" class="checkout_items" runat="server"></span>
									    </a>
								    </li>
							    </ul>
							    <div class="hamburger_container">
								    <i class="fa fa-bars" aria-hidden="true"></i>
							    </div>
                                <div >
                                <asp:Label ID="lb_hello" runat="server" Text="" CssClass="col-lg-9" Visible="False"></asp:Label>   
							    </div>
						    </nav>
                            
                            <%--我是分割線管理者功能選單--%>
                            <nav id="nav_administrator" class="navbar" runat="server">
							    <ul class="navbar_menu">
								    <li><a href="Default.aspx">首頁</a></li>
								    <li><a href="?page=scan">掃描</a></li><%--先暫時坐在這邊--%>
								    <li><a href="#">會員管理</a></li>
								    <li><a href="#">商品管理</a></li>
								    <li><a href="#">訂單管理</a></li>								    
							    </ul>
							    <ul class="navbar_user">
								    <li><a href="#"><i class="fa fa-search" aria-hidden="true"></i></a></li>
								    <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
									    <ul class="account_selection">
										    <li><a href="#"><i class="fa fa-sign-in" aria-hidden="true"></i>資料設定</a></li>
										    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>登出</a></li>
									    </ul>
								    </li>
								    
							    </ul>
							    <div class="hamburger_container">
								    <i class="fa fa-bars" aria-hidden="true"></i>
							    </div>
                                <div >
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

		<%--掃描qrcode--%>
<div class="body" id="scan_qr_div" runat="server" visible="false" style="margin-top:100px">
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
                                        <table class="table"><i class="fa fa-refresh fa-spin fa-3x fa-fw"></i>
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
				<li class="menu_item"><a href="#">promotion</a></li>
				<li class="menu_item"><a href="#">pages</a></li>
				<li class="menu_item"><a href="#">blog</a></li>
				<li class="menu_item"><a href="#">contact</a></li>
			</ul>
		</div>
	</div>
<div id="start_display" runat="server">
	    <!-- Slider -->

	    <div class="main_slider" style="background-image:url(images/slider_1.jpg)">
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
	    </div>

	    <!-- Banner -->

	    <div class="banner">
		    <div class="container">
			    <div class="row">
				    <div class="col-md-4">
					    <div class="banner_item align-items-center" style="background-image:url(images/banner_1.jpg)">
						    <div class="banner_category">
							    <a href="categories.html">women's</a>
						    </div>
					    </div>
				    </div>
				    <div class="col-md-4">
					    <div class="banner_item align-items-center" style="background-image:url(images/banner_2.jpg)">
						    <div class="banner_category">
							    <a href="categories.html">accessories's</a>
						    </div>
					    </div>
				    </div>
				    <div class="col-md-4">
					    <div class="banner_item align-items-center" style="background-image:url(images/banner_3.jpg)">
						    <div class="banner_category">
							    <a href="categories.html">men's</a>
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
	    

	    <!-- Deal of the week -->

	    <div class="deal_ofthe_week">
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
	    </div>

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

	    <div class="blogs">            
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
						    <div class="blog_background" style="background-image:url(images/blog_1.jpg)"></div>
						    <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
							    <h4 class="blog_title">Here are the trends I see coming this fall</h4>
							    <span class="blog_meta">by admin | dec 01, 2017</span>
							    <a class="blog_more" href="#">Read more</a>
						    </div>
					    </div>
				    </div>
				    <div class="col-lg-4 blog_item_col">
					    <div class="blog_item">
						    <div class="blog_background" style="background-image:url(images/blog_2.jpg)"></div>
						    <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
							    <h4 class="blog_title">Here are the trends I see coming this fall</h4>
							    <span class="blog_meta">by admin | dec 01, 2017</span>
							    <a class="blog_more" href="#">Read more</a>
						    </div>
					    </div>
				    </div>
				    <div class="col-lg-4 blog_item_col">
					    <div class="blog_item">
						    <div class="blog_background" style="background-image:url(images/blog_3.jpg)"></div>
						    <div class="blog_content d-flex flex-column align-items-center justify-content-center text-center">
							    <h4 class="blog_title">Here are the trends I see coming this fall</h4>
							    <span class="blog_meta">by admin | dec 01, 2017</span>
							    <a class="blog_more" href="#">Read more</a>
						    </div>
					    </div>
				    </div>
			    </div>
		    </div>
	    </div>

	    <!-- Newsletter -->

	    <div class="newsletter">
		    <div class="container">
			    <div class="row">
				    <div class="col-lg-6">
					    <div class="newsletter_text d-flex flex-column justify-content-center align-items-lg-start align-items-md-center text-center">
						    <h4>Newsletter</h4>
						    <p>Subscribe to our newsletter and get 20% off your first purchase</p>
					    </div>
				    </div>
				    <div class="col-lg-6">
					    <form action="post">
						    <div class="newsletter_form d-flex flex-md-row flex-column flex-xs-column align-items-center justify-content-lg-end justify-content-center">
							    <input id="newsletter_email" type="email" placeholder="Your email" required="required" data-error="Valid email is required.">
							    <button id="newsletter_submit" type="submit" class="newsletter_submit_btn trans_300" value="Submit">subscribe</button>
						    </div>
					    </form>
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
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
						    <div class="cr">©2018 All Rights Reserverd. This template is made with <i class="fa fa-heart-o" aria-hidden="true"></i> by <a href="#">Colorlib</a></div>
					    </div>
				    </div>
			    </div>
		    </div>
	    </footer>

    </div>
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
    <script src="js/single_custom.js"></script>
    <script src="plugins/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
	<%--QRCODE SCAN--%>
	<script src="js/instascan.min.js"></script>
	<script src="js/find.js"></script>
	<%--登入--%>
    
    

</body>
</html>
