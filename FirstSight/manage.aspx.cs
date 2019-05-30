using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using ZXing;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

public partial class _Default : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["identity"] != null)
        {
            if (Session["identity"].ToString() == "0")
            {
                start_display.Visible = true;
                string user_name = Server.UrlDecode(Request.Cookies["user"].Value);
                lb_hello.Visible = true;
                lb_hello.Text ="親愛的"+user_name+"會員,您好!";
                checkout_items.InnerText = "6";
                nav_nologin.Visible = false;
                nav_administrator.Visible = false;
                nav_user.Visible = true;

            }
            if (Session["identity"].ToString() == "1")
            {
                start_display.Visible = false;
                string adm_name = Server.UrlDecode(Request.Cookies["user"].Value);
                lb_hello2.Visible = true;
                lb_hello2.Text = "親愛的"+ adm_name + "管理員,您好!";
                nav_nologin.Visible = false;
                nav_user.Visible = false;
                nav_administrator.Visible = true;
            }
        }
        else
        {
            start_display.Visible = true;
            checkout_items.Visible = false;
            nav_user.Visible = false;
            nav_administrator.Visible = false;
            nav_nologin.Visible = true;
            
        }
            
            

            PlaceHolder1.Controls.Clear();
            PlaceHolder2.Controls.Clear();
            PlaceHolder3.Controls.Clear();
            //掃描頁面

            var page = Request.QueryString["page"];
            if (page != null && page == "scan")
            {
                scan_qr_div.Visible = true;
            }
            /*新貨到Start*/
            //首頁商品PH1
            //資料庫連線
            SqlConnection cnSQL = new SqlConnection();
            //連線字串
            cnSQL.ConnectionString = strConnString;
            //SqlCommand
            SqlCommand cmdSQL = new SqlCommand();
            cmdSQL.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL;
            String outside = "";//只產生一次
            Label inside = new Label();//產生多次


            strSQL = "select top 10* from Commodities;";
            cmdSQL.CommandText = strSQL;
            cnSQL.Open();
            SqlDataReader reader = cmdSQL.ExecuteReader();
            //頭
            outside += "<div class=\"new_arrivals\"><div class=\"container\">";
            outside += "<div class=\"row\"><div class=\"col text-center\"><div class=\"section_title new_arrivals_title\"><h2>新貨到</h2></div></div></div>";
            outside += "<div class=\"row align-items-center\"><div class=\"col text-center\"><div class=\"new_arrivals_sorting\"><ul class=\"arrivals_grid_sorting clearfix button-group filters-button-group\">";
            outside += "<li class=\"grid_sorting_button button d-flex flex-column justify-content-center align-items-center active is-checked\" data-filter=\"*\">全部</li>";
            outside += "<li class=\"grid_sorting_button button d-flex flex-column justify-content-center align-items-center\" data-filter=\".women\">女裝</li>";
            outside += "<li class=\"grid_sorting_button button d-flex flex-column justify-content-center align-items-center\" data-filter=\".accessories\">配件</li>";
            outside += "<li class=\"grid_sorting_button button d-flex flex-column justify-content-center align-items-center\" data-filter=\".men\">男裝</li></ul></div></div></div>";
            outside += "<div class=\"row\"><div class=\"col\"><div class=\"product-grid\" data-isotope='{ \"itemSelector\": \".product-item\", \"layoutMode\": \"fitRows\" }'>";
            inside.Text = "";
            while (reader.Read())
            {
                if (reader["cd_kind"].ToString() == "TS")
                {
                    inside.Text += "<div class=\"product-item men\"><div class=\"product discount product_filter\">";
                }
                else if (reader["cd_kind"].ToString() == "J")
                {
                    inside.Text += "<div class=\"product-item women\"><div class=\"product discount product_filter\">";
                }
                else if (reader["cd_kind"].ToString() == "2")
                {
                    inside.Text += "<div class=\"product-item accessories\"><div class=\"product discount product_filter\">";
                }
                inside.Text += "<div class=\"product_image\"><img src=\"images/product/" + reader["cd_id"] + ".jpeg\" alt=\"\"></div>";//圖片
                inside.Text += "<div class=\"favorite favorite_left\"></div><div class=\"product_info\">";
                inside.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader["cd_id"] + "\">" + reader["cd_name"] + "</a></h6>";
                inside.Text += "<div class=\"product_price\">" + reader["cd_id"] + "</div>";
                inside.Text += "</div></div><div class=\"red_button add_to_cart_button\"><a href=\"#\" id=\"" + reader["cd_id"] + "\">加入購物車</a></div></div>";//r加入購物車


            }
            cnSQL.Close();
            inside.Text = outside + inside.Text + "</div></div></div></div></div>";
            PlaceHolder1.Controls.Add(inside);
            /*新貨到end*/


            /*熱銷商品start*/
            SqlCommand cmdSQL1 = new SqlCommand();
            cmdSQL1.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL1;
            String outside1 = "";//只產生一次
            Label inside1 = new Label();//產生多次


            strSQL1 = "select top 10* from  Commodities;";
            cmdSQL1.CommandText = strSQL1;
            cnSQL.Open();
            SqlDataReader reader1 = cmdSQL1.ExecuteReader();
            //頭
            outside1 += "<div class=\"best_sellers\"><div class=\"container\"><div class=\"row\"><div class=\"col text-center\">";
            outside1 += "<div class=\"section_title new_arrivals_title\"><h2>熱銷商品</h2></div></div></div>";
            outside1 += "<div class=\"row\"><div class=\"col\"><div class=\"product_slider_container\"><div class=\"owl-carousel owl-theme product_slider\">";
            inside1.Text = "";
            while (reader1.Read())
            {
                inside1.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside1.Text += "<div class=\"product_image\"><img src=\"images/product/" + reader1["cd_id"] + ".jpeg\" alt=\"\"></div>";
                inside1.Text += "<div class=\"favorite favorite_left\"></div>";
                inside1.Text += "<div class=\"product_info\">";
                inside1.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader1["cd_id"] + "\">" + reader1["cd_name"] + "</a></h6>";
                inside1.Text += "<div class=\"product_price\">" + reader1["cd_id"] + "</div></div></div></div></div>";

            }
            cnSQL.Close();
            inside1.Text = outside1 + inside1.Text + "</div><div class=\"product_slider_nav_left product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-left\" aria-hidden=\"true\"></i></div>";
            inside1.Text += "<div class=\"product_slider_nav_right product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i></div>";
            inside1.Text += "</div></div></div></div></div>";
            PlaceHolder2.Controls.Add(inside1);
            /*熱銷商品end*/

            /*商品資訊start*/
            var pid = Request.QueryString["pid"];
            if (pid != null)
            {
                start_display.Visible = false;
                SqlCommand cmdSQL2 = new SqlCommand();
                cmdSQL2.Connection = cnSQL;
                //strSQL 為select form where insert
                String strSQL2;
                //String outside2 = "";//只產生一次
                Label inside2 = new Label();//產生多次


                strSQL2 = "select * from Announcement where po_id='";
                strSQL2 += Request.QueryString["pid"] + "';";
                cmdSQL2.CommandText = strSQL2;
                cnSQL.Open();
                SqlDataReader reader2 = cmdSQL2.ExecuteReader();
                inside2.Text = "";
                while (reader2.Read())
                {
                    inside2.Text += "<div class=\"container single_product_container\"style=\"margin-top: 140px;\">";
                    inside2.Text += "<div class=\"row\"><div class=\"col\">";
                    inside2.Text += "<div class=\"breadcrumbs d-flex flex-row align-items-center\">";
                    inside2.Text += "<ul><li><a href=\"Default.aspx\">首頁</a></li>";
                    inside2.Text += "<!-- 資料庫類別(撈資料庫) --><li><a href=\"categories.html\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>Men's</a></li>";
                    inside2.Text += "<li class=\"active\"><a href=\"#\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>商品資訊</a></li></ul>";
                    inside2.Text += "</div></div></div>";
                    inside2.Text += "<!-- 商品圖片 --><div class=\"row\"><div class=\"col-lg-7\"><div class=\"single_product_pics\">";
                    inside2.Text += "<div class=\"row\"><div class=\"col-lg-9 image_col order-lg-2 order-1\">";
                    inside2.Text += "<div class=\"single_product_image\"><div class=\"single_product_image_background\" style=\"background-image:url(images/single_2.jpg)\"></div>";
                    inside2.Text += "</div></div></div></div></div>";
                    inside2.Text += "<div class=\"col-lg-5\">";
                    inside2.Text += "<div class=\"product_details\">";
                    inside2.Text += "<div class=\"product_details_title\">";
                    inside2.Text += "<!-- 商品名稱 --><h2>Pocket cotton sweatshirt</h2>";
                    inside2.Text += "<!-- 商品敘述 --><p>Nam tempus turpis at metus scelerisque placerat nulla deumantos solicitud felis. Pellentesque diam dolor, elementum etos lobortis des mollis ut...</p></div>";
                    inside2.Text += "<!-- 商品價格 --><div class=\"product_price\">$495.00</div>";
                    inside2.Text += "<!-- 選擇尺寸 --><div class=\"product_color\">";
                    inside2.Text += "<span>選擇尺寸:</span><ul>";
                    inside2.Text += "<li style=\"background: #e54e5d\"></li><li style=\"background: #252525\"></li><li style=\"background: #60b3f3\"></li></ul></div>";
                    inside2.Text += "<!-- 加入購物車 --><div class=\"quantity d-flex flex-column flex-sm-row align-items-sm-center\">";
                    inside2.Text += "<span>數量:</span><div class=\"quantity_selector\">";
                    inside2.Text += "<span class=\"minus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span><span id=\"quantity_value\">1</span>";
                    inside2.Text += "<span class=\"plus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div>";
                    inside2.Text += "<div class=\"red_button add_to_cart_button\"><a href=\"#\">加入購物車</a></div>";
                    inside2.Text += "<div class=\"product_favorite d-flex flex-column align-items-center justify-content-center\"></div>";
                    inside2.Text += "</div></div></div></div></div>";
                }
                cnSQL.Close();
                PlaceHolder3.Controls.Add(inside2);
            }/*商品資訊end*/
       
        
    }


}