<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>
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
							    <a href="#"><span>衣</span>見鍾情</a>
						    </div>
                            <%--功能選單--%>
						    <nav id="nav_user" class="navbar" runat="server">
							    <ul class="navbar_menu">
								    <li><a href="manage.aspx">首頁</a></li>
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
										    <li><a href="#"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
										    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>註冊</a></li>
									    </ul>
								    </li>
								    
							    </ul>
							    <div class="hamburger_container">
								    <i class="fa fa-bars" aria-hidden="true"></i>
							    </div>
						    </nav>
                            <%--我是分割線--%>
                            <nav id="nav_administrator" class="navbar" runat="server">
							    <ul class="navbar_menu">
								    <li><a href="manage.aspx">首頁</a></li>
								    <li><a href="?page=scan">掃描</a></li><%--先暫時坐在這邊--%>
								    <li><a href="#">會員管理</a></li>
								    <li><a href="#">商品管理</a></li>
								    <li><a href="#">訂單管理</a></li>								    
							    </ul>
							    <ul class="navbar_user">
								    <li><a href="#"><i class="fa fa-search" aria-hidden="true"></i></a></li>
								    <li class="account"><a href="#"><i class="fa fa-user" aria-hidden="true"></i></a>
									    <ul class="account_selection">
										    <li><a href="#"><i class="fa fa-sign-in" aria-hidden="true"></i>登入</a></li>
										    <li><a href="#"><i class="fa fa-user-plus" aria-hidden="true"></i>註冊</a></li>
									    </ul>
								    </li>
								    
							    </ul>
							    <div class="hamburger_container">
								    <i class="fa fa-bars" aria-hidden="true"></i>
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
    <div class="main_slider " id="login_display" runat="server" visible="true">
        <div class="container">
            <div class="row">
            <div class="col-md-4 mar ">
                
                    <div class="panel-heading">
                        <h3 class="panel-title">請登入</h3>
                    </div>
                    <div class="panel-body">
                        
                            <fieldset>
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
                                <asp:Button ID="btn_fgpwd" runat="server" Text="忘記密碼" class="btn btn-lg btn-success btn-block" OnClick="btn_fgpwd_Click"/>
                                <asp:Button ID="btn_back" runat="server" Text="返回" class="btn btn-lg btn-danger btn-block" OnClick="btn_back_Click"/> 
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
            </div>
                <%--忘記密碼驗證 start--%>

                 <div class="main_slider " id="forpwd_display" runat="server" visible="false">
        <div class="container">
            <div class="row">
            <div class="col-md-4 mar ">
                
                    <div class="panel-heading">
                        <h3 class="panel-title">請登入</h3>
                    </div>
                    <div class="panel-body">
                        
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox ID="tb_actvf" runat="server" class="form-control"  placeholder="帳號驗證"></asp:TextBox>      
                                    <asp:Label ID="lb_rig" runat="server" Text="請輸入正確帳號" Visible="false"></asp:Label>
                                </div>
                                <asp:Button ID="Button1" runat="server" Text="發送" class="btn btn-lg btn-success btn-block" OnClick="btn_actvf_Click"/>
                                <asp:Button ID="Button3" runat="server" Text="返回" class="btn btn-lg btn-danger btn-block" OnClick="btn_backatvf_Click"/> 
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
            </div>



                 <%--忘記密碼驗證 end--%>
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
