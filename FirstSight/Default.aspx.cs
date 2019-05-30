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
using System.Reflection;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    int pe_total = 0;
    string temp_total = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        
        SqlDataSource_mm.SelectCommand = "SELECT * from Members where mm_who='0';"; /*記得刪掉*/
        SqlDataSource_cd.SelectCommand = "SELECT * from Commodities ;";
        SqlDataSource_ody.SelectCommand = "SELECT * from Orders where od_done='Y'";
        SqlDataSource_odn.SelectCommand = "SELECT * from Orders where od_done='N'";
        SqlDataSource_oddetail.SelectCommand = "select od_no,od_id,od_mm_id,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount from Orders o join Order_detail odd on o.od_id=odd.odd_od_id where od_id='od00000001';";
        SqlDataSource_pe.SelectCommand = "SELECT DISTINCT pe_title ,pe_starting_date,pe_expiring_date,pe_status,pe_main from Promotion_events where pe_status='Y'; ;";
        SqlDataSource_pen.SelectCommand = "SELECT DISTINCT pe_title ,pe_starting_date,pe_expiring_date,pe_status,pe_main from Promotion_events where pe_status='N';";
        SqlDataSource_tag.SelectCommand = "SELECT * from Tags;";

        /*************/
        string pemain_count;
        SqlConnection cnSQL0 = new SqlConnection(strConnString);
        cnSQL0.Open();
        SqlCommand find_pe_main = new SqlCommand(@"select count(*) from Promotion_events where pe_status ='Y' and pe_main='Y';", cnSQL0);        
        pemain_count = find_pe_main.ExecuteScalar().ToString(); //FS:原本.ToString()
        if (Convert.ToInt32(pemain_count )>=1) //FS:原本是=="1"
        {
            //優惠活動start
            
            
            //strSQL 為select form where insert
            
            Label insideevent = new Label();//產生多次

            SqlCommand produce_pemain = new SqlCommand(@"select top 1 * from Promotion_events where pe_main='Y' and pe_status ='Y';", cnSQL0);
            
            SqlDataReader readerevent = produce_pemain.ExecuteReader();
            insideevent.Text = "";
            
            while (readerevent.Read())
            {
                insideevent.Text += "<div class=\"deal_ofthe_week\">";
                insideevent.Text += "<div class=\"container\"><div class=\"row align-items-center\"><div class=\"col-lg-6\"><div class=\"deal_ofthe_week_img\">";
                insideevent.Text += "<img src =\"images/gift.png\" alt=\"\"></div></div>";
                /*圖片*/   /*insideevent.Text += "<img src =\"images/deal_ofthe_week.png\" alt=\"\"></div></div>";*/
                insideevent.Text += "<div class=\"col-lg-6 text-right deal_ofthe_week_col\"><div class=\"deal_ofthe_week_content d-flex flex-column align-items-center float-right\">";
                /*標題*/
                insideevent.Text += "<div class=\"section_title \"><h2 style=\"color:#FF8888\">" + readerevent["pe_title"] + "</h2></div>";
                insideevent.Text += "<ul class=\"timer\"><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                /*倒數天數*/
                insideevent.Text += "<div id =\"day\" class=\"timer_num\">00</div><div class=\"timer_unit\">天</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"hour\" class=\"timer_num\">00</div><div class=\"timer_unit\">小時</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"minute\" class=\"timer_num\">00</div><div class=\"timer_unit\">分鐘</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"second\" class=\"timer_num\">00</div><div class=\"timer_unit\">秒</div>";
                insideevent.Text += "</li></ul>";
                insideevent.Text += "</div></div></div></div></div>";
                Session["pemain_time"] = readerevent["pe_expiring_date"];
            }
            readerevent.Close();
            cnSQL0.Close();
            PlaceHolderevent.Controls.Add(insideevent);
            //優惠活動end
        }
        else
        {
            nope_display.Visible = true;cnSQL0.Close();
        }
        /*優惠活動自動關閉*/
        cnSQL0.Open();
        int pe_count;
        
        SqlCommand find_pe = new SqlCommand(@"select count(DISTINCT pe_title) from Promotion_events where pe_status ='Y' ;", cnSQL0);
        pe_count = Convert.ToInt32(find_pe.ExecuteScalar().ToString());
        pe_total = pe_count;
        if (pe_count >= 1)
        {
            //優惠活動start


            //strSQL 為select form where insert

            

            SqlCommand produce_pe = new SqlCommand(@"select DISTINCT pe_title,pe_expiring_date from Promotion_events where pe_status ='Y';", cnSQL0);

            SqlDataReader readerpe = produce_pe.ExecuteReader();

            int count = 1;
            while (readerpe.Read())
            {

                
                Session["petime"+count] = readerpe["pe_expiring_date"];
                count += 1;
            }

            cnSQL0.Close();
            Timer_pe.Enabled = true;
            //優惠活動end
        }
        else
        {
            
        }


        //Response.Redirect(Session["pemain_time"].ToString());


        // SqlConnection cnSQLtemptag = new SqlConnection();
        // cnSQLtemptag.ConnectionString = strConnString;
        // SqlCommand cmdSQLtemptag = new SqlCommand();
        // cmdSQLtemptag.Connection = cnSQLtemptag;
        // String strSQLtemptag;
        // strSQLtemptag = "select * from  Tags";
        // //strSQLtemptag += Request.QueryString["pid"] + "');";
        // cmdSQLtemptag.CommandText = strSQLtemptag;
        // cnSQLtemptag.Open();
        // SqlDataReader readertemptag = cmdSQLtemptag.ExecuteReader();

        // while (readertemptag.Read())
        // {
        //     lb_temp_tag.Text += readertemptag["tag_name"] + ",";
        // }
        //// ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('" + lb_temp_tag.Text + "');", true);
        // Session["temp_tag"] = lb_temp_tag.Text;


        /*************/
        if (Session["identity"] != null)
        {
            if (Session["identity"].ToString() == "0")
            {
                
                
                string mm_id,sc_count,od_count,odn_count,ww_count;
                string user_name = Session["user"].ToString();
                SqlConnection cnSQL2 = new SqlConnection(strConnString);
                cnSQL2.Open();

                mm_id = Session["mmid"].ToString();
                SqlCommand find_sc_cd_num = new SqlCommand(@"select count(*) from Shopping_Carts where sc_mm_id=@sc_mm_id;", cnSQL2);
                find_sc_cd_num.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));
                sc_count = find_sc_cd_num.ExecuteScalar().ToString();

                SqlCommand find_od_num = new SqlCommand(@"select count(*) from Orders where od_done='Y' and od_mm_id=@od_mm_id;", cnSQL2);
                find_od_num.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));
                od_count = find_od_num.ExecuteScalar().ToString();

                SqlCommand find_odn_num = new SqlCommand(@"select count(*) from Orders where od_done='N' and od_mm_id=@od_mm_id;", cnSQL2);
                find_odn_num.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));
                odn_count = find_odn_num.ExecuteScalar().ToString();

                SqlCommand find_ww_num = new SqlCommand(@"Select count(*) From Wishing_Wells Where ww_mm_id=@ww_mm_id", cnSQL2);
                find_ww_num.Parameters.Add(new SqlParameter("@ww_mm_id", mm_id));
                ww_count= find_ww_num.ExecuteScalar().ToString();
                if (sc_count == "0") { cart_table.Visible = false; cart_noitem.Visible = true; }
                else { cart_table.Visible = true; cart_noitem.Visible = false; }

                if (od_count == "0") { order_table.Visible = false; order_noitem.Visible = true; }
                else { order_table.Visible = true; order_noitem.Visible = false; }

                if (odn_count == "0") { order_table1.Visible = false; order_noitem2.Visible = true; }
                else { order_table1.Visible = true; order_noitem2.Visible = false; }

                if (ww_count == "0") { wish_noitem.Visible = true; }
                else { wish_noitem.Visible = false; }


                start_display.Visible = true;
                lb_hello.Text =  user_name + "會員,您好!";
                //lb_hello.Text = Session["user"].ToString();
                lb_hello.Visible = true;
                
                checkout_items.InnerText = sc_count;

                nav_nologin.Visible = false;
                nav_menu_nologin.Visible = false;
                nav_administrator.Visible = false;
                nav_user.Visible = true;
                nav_menu_user.Visible = true;
                /*PROFILE START*/
                SqlCommand pfproduce = new SqlCommand(@"select * from Members where mm_id=@mm_id;", cnSQL2);
                pfproduce.Parameters.Add(new SqlParameter("@mm_id", mm_id));
                SqlDataReader pf_reader = pfproduce.ExecuteReader();
                while (pf_reader.Read())
                {
                    DateTime dt = Convert.ToDateTime(pf_reader["mm_bd"].ToString());
                    lb_pfact.Text = pf_reader["mm_act"].ToString();
                    lb_pfname.Text = pf_reader["mm_name"].ToString();
                    lb_pfphone.Text= pf_reader["mm_phone"].ToString();
                    lb_pfemail.Text= pf_reader["mm_mail"].ToString();
                    lb_pfadr.Text= pf_reader["mm_adr"].ToString();
                    lb_pfbd.Text= dt.ToString("yyyy/MM/dd");
                    if (!IsPostBack)
                    {
                        if (pf_reader["mm_notice"].ToString() == "Y") { ckb_notification.Checked = true; }
                        else { ckb_notification.Checked = false; }
                    }
                    else
                    {
                        ckb_notification.Controls.Clear();
                    }
                }
                   
                pf_reader.Close();


                /*PROFILE  END*/
               
                /*訂單START*/

                String outsideorder = "";
                Label insideorder = new Label();//產生多次

                //strSQLorder = "select * from Members mb left join Orders od on mb.mm_id=od.od_mm_id where mm_act='"; 
                //strSQLorder +=  Session["user"].ToString() + "' order by od.od_orderdate desc ;";


                SqlCommand orderproduce = new SqlCommand(@"select * from  Orders where od_done='Y' and od_mm_id=@od_mm_id order by od_id desc ;", cnSQL2);
                orderproduce.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));

                SqlDataReader readerorder = orderproduce.ExecuteReader();
                insideorder.Text = "";
                int a = 0;
                outsideorder += "<div class=\"woocommerce\"><form class=\"woocommerce-cart-form\">";
                outsideorder += "<table class=\"shop_table shop_table_responsive cart woocommerce-cart-form__contents\" cellspacing=\"0\"><thead><tr>";
                outsideorder += "<th class=\"product-remove\">NO.</th><th class=\"product-remove\">訂單日期</th><th class=\"product-remove\">訂單編號</th>";
                outsideorder += "<th class=\"product-remove\">付款方式</th><th class=\"product-remove\">取貨方式</th><th class=\"product-remove\">寄送地址</th>";
                outsideorder += "<th class=\"product-remove\">連絡電話</th><th class=\"product-remove\">金額</th><th class=\"product-remove\">備註</th></tr></thead>";
                while (readerorder.Read())
                {
                    DateTime date = Convert.ToDateTime(readerorder["od_orderdate"].ToString());
                    a++;
                    insideorder.Text += "<tbody><tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                    insideorder.Text += "<td class=\"text-center\" data-title=\"NO\">" + a + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"訂單日期\">" + date.ToShortDateString() + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"訂單編號\">" + readerorder["od_id"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"付款方式\">" + readerorder["od_paymant"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"取貨方式\">" + readerorder["od_delivery"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"寄送地址\">" + readerorder["od_d_address"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"連絡電話\">" + readerorder["od_phone"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"金額\">" + readerorder["od_total"] + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"備註\">" + readerorder["od_note"] + "</td></br></tr></tbody>";
                }
                insideorder.Text = outsideorder + insideorder.Text + "</table></form></div></div></div>";

                
                PH_order.Controls.Add(insideorder);
                readerorder.Close();

                /*訂單 END*/
                /*用戶端未處理訂單 start*/
                String outsideorder1 = "";
                Label insideorder1 = new Label();//產生多次



                SqlCommand orderproduce1 = new SqlCommand(@"select * from  Orders where od_done='N' and od_mm_id=@od_mm_id order by od_no desc ;", cnSQL2);
                orderproduce1.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));

                SqlDataReader readerorder1 = orderproduce1.ExecuteReader();
                insideorder1.Text = "";
                int a1 = 0;
                
                outsideorder1 += "<div class=\"woocommerce\"><form class=\"woocommerce-cart-form\">";
                outsideorder1 += "<table class=\"shop_table shop_table_responsive cart woocommerce-cart-form__contents\" cellspacing=\"0\"><thead><tr>";
                outsideorder1 += "<th class=\"product-remove\">NO.</th><th class=\"product-remove\">訂單日期</th><th class=\"product-remove\">訂單編號</th>";
                outsideorder1 += "<th class=\"product-remove\">付款方式</th><th class=\"product-remove\">取貨方式</th><th class=\"product-remove\">寄送地址</th>";
                outsideorder1 += "<th class=\"product-remove\">連絡電話</th><th class=\"product-remove\">金額</th><th class=\"product-remove\">備註</th></tr></thead>";
                while (readerorder1.Read())
                {
                    DateTime date =Convert.ToDateTime( readerorder1["od_orderdate"].ToString());
                    a1++;
                    insideorder1.Text += "<tbody><tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                    insideorder1.Text += "<td class=\"text-center\" data-title=\"NO\">" + a1 + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"訂單日期\">" + date.ToShortDateString() + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"訂單編號\">" + readerorder1["od_id"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"付款方式\">" + readerorder1["od_paymant"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"取貨方式\">" + readerorder1["od_delivery"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"寄送地址\">" + readerorder1["od_d_address"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"連絡電話\">" + readerorder1["od_phone"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"金額\">" + readerorder1["od_total"] + "</td>";
                    insideorder1.Text += "<td class=\"temp_pname product-name\" data-title=\"備註\">" + readerorder1["od_note"] + "</td></br></tr></tbody>";
                }
                insideorder1.Text = outsideorder1 + insideorder1.Text + "</table></form></div></div></div>";


                PH_order1.Controls.Add(insideorder1);
                readerorder1.Close();

                /*用戶端未處理訂單 end*/


                /*購物車START*/
                String cart_outside = "";//只產生一次
                String cart_outside2 = "";//優惠折抵
                String cart_outside3 = "";//購物車總金額
                Label cart_inside = new Label();//產生多次
                cart_inside.Text = "";
                Label cart_inside2 = new Label();//產生多次
                cart_inside2.Text = "";
                int cart_totalfee = 0;



                SqlCommand cart_produce = new SqlCommand(@"select * from Shopping_Carts sc  join Commodities cd on sc.sc_cd_id=cd.cd_id join Sizes sz on sc.sc_cd_id=sz.sz_cd_id left join Promotion_events pe on sc.sc_cd_id=pe.pe_cd_id where sc_mm_id=@sc_mm_id ;", cnSQL2);
                cart_produce.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));

                SqlDataReader cart_reader = cart_produce.ExecuteReader();
                cart_outside += "<table cellspacing=\"0\" class=\"shop_table shop_table_responsive cart woocommerce-cart-form__contents\"><thead><tr>";
                cart_outside += "<th class=\"product-remove\">&nbsp;</th><th class=\"product-img\">商品圖片</th><th class=\"product-name\">商品名稱</th>";
                cart_outside += "<th class=\"product-price\">價格</th><th class=\"product-quantity\">數量</th><th class=\"product-subtotal\">單項總額</th></tr></thead><tbody>";

                cart_outside2 += "<div class=\"row\"><div class=\"col-lg-8 \"><div class=\"\"><div class=\"cart-collaterals\"><div class=\"cart_totals text-center\">";
                cart_outside2 += "<h2>優惠折抵</h2><table class=\"shop_table  cart woocommerce-cart-form__contents\" cellspacing=\"0\"><thead>";
                cart_outside2 += "<tr><th class=\"product-name\">商品圖片</th><th class=\"product-name\">商品名稱</th><th class=\"product-price\">原價</th><th class=\"product-quantity\">活動優惠</th><th class=\"product-subtotal\">折扣</th></tr></thead><tbody>";

                //string[] temp_amount = new String[4] { "9", "9", "9", "9" };
                while (cart_reader.Read())
                {
                    String cart_size = "";
                    double pe_discount = 0;
                    int ck_first = 0;
                    int sc_num = 0;
                    int cd_price = 0;
                    int itemprice = 0;
                    int event_price = 0;
                    int savemoney = 0;
                    int event_price_one = 0;
                    Session[cart_reader["sc_cd_id"].ToString()] = new String[4] { "9", "9", "9", "9" };

                    if (cart_reader["pe_discount"].ToString() != "" && cart_reader["pe_discount"].ToString() != null)
                    {
                        sc_num = Convert.ToInt32(cart_reader["sc_num"].ToString());

                        pe_discount = Convert.ToDouble(cart_reader["pe_discount"].ToString());

                        cd_price = Convert.ToInt32(cart_reader["cd_price"].ToString());
                        event_price = Convert.ToInt32(sc_num * cd_price * pe_discount);
                        event_price_one = Convert.ToInt32(cd_price * pe_discount);
                        savemoney = cd_price - event_price_one;


                        itemprice = Convert.ToInt32(sc_num * cd_price);
                        /*偷做 活動優惠INSIDE START*/
                        cart_inside2.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside2.Text += "<td class=\"product-img\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside2.Text += "<td class=\"product-name\" data-title=\"商品名稱\"><span>" + cart_reader["cd_name"] + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$" + cd_price + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-price\" data-title=\"活動優惠\"><span class=\"woocommerce-Price-amount amount\">$" + event_price_one + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-subtotal\" data-title=\"折扣\"><span class=\"woocommerce-Price-amount amount\">$" + savemoney + "</span></td></tr>";
                        /*偷做 活動優惠INSIDE END*/

                        cart_inside.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside.Text += "<td class=\"product-remove\"><a class=\"remove\" href=\"#\" id=\"" + cart_reader["sc_cd_id"] + "\">×</a> </td>";
                        cart_inside.Text += "<td class=\"product-img\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside.Text += "<td class=\"temp_pname product-name\" data-title=\"商品名稱\"><span>" + cart_reader["cd_name"] + "</span> </td>";
                        cart_inside.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$" + cd_price + "</span></td>";
                        /*找SIZE START*/
                        if (cart_reader["sz_s"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\" id=\"" + cart_reader["sc_cd_id"] + "sminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            cart_size += "<span class=\"squantity_value\"  id=\"" + cart_reader["sc_cd_id"] + "svalue\">" + sc_num + "</span><span class=\"splus\" id=\"" + cart_reader["sc_cd_id"] + "splus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                            string[] cz_s = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_s[0] = sc_num.ToString();
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_s;


                            ck_first = 1;
                        }
                        else
                        {
                            string[] cz_s = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_s[0] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_s;
                        }
                        if (cart_reader["sz_m"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">M</div><div style=\"float: right\"><span class=\"mminus\" id=\"" + cart_reader["sc_cd_id"] + "mminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">" + sc_num + "</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_m[1] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_m;
                            }
                            else
                            {
                                cart_size += "<span class=\"mquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "mvalue\">0</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_m[1] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_m;
                            }
                        }
                        else
                        {
                            string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_m[1] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_m;
                        }
                        if (cart_reader["sz_l"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">L</div><div style=\"float: right\"><span class=\"lminus\" id=\"" + cart_reader["sc_cd_id"] + "lminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">" + sc_num + "</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_l[2] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_l;
                            }
                            else
                            {
                                cart_size += "<span class=\"lquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "lvalue\">0</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_l[2] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_l;
                            }
                        }
                        else
                        {
                            string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_l[2] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_l;
                        }
                        if (cart_reader["sz_xl"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">XL</div><div style=\"float: right\"><span class=\"xlminus\" id=\"" + cart_reader["sc_cd_id"] + "xlminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">" + sc_num + "</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_xl[3] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;
                            }
                            else
                            {
                                cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">0</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_xl[3] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;
                            }
                        }
                        else
                        {
                            string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_xl[3] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;
                        }
                        /*找SIZE END*/
                        //輩分cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br><div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\"><i aria-hidden=\"true\" class=\"fa fa-minus\"></i></span><span class=\"squantity_value\">" + sc_num + "</span> <span class=\"splus\"><i aria-hidden=\"true\" class=\"fa fa-plus\"></i></span></div><br></td>";
                        cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br>" + cart_size + "<br></td>";
                        cart_inside.Text += "<td class=\"product-subtotal\" data-title=\"單項總額\"><span class=\"woocommerce-Price-amount amount \"id=\"" + cart_reader["sc_cd_id"] + "price\" style=\"text-decoration: line-through\">$" + itemprice + "</span> <span class=\"woocommerce-Price-amount amount cal_total\" style=\"color: red; display: block\"id=\"" + cart_reader["sc_cd_id"] + "discountprice\">$" + event_price + "</span></td>";
                        //cart_inside.Text += "";
                        cart_totalfee += event_price;
                        cart_inside.Text = cart_inside.Text + "</tr>";
                    }
                    else
                    {
                        sc_num = Convert.ToInt32(cart_reader["sc_num"].ToString());
                        cd_price = Convert.ToInt32(cart_reader["cd_price"].ToString());
                        itemprice = Convert.ToInt32(sc_num * cd_price);


                        cart_inside.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside.Text += "<td class=\"product-remove\"><a class=\"remove\" href=\"#\" id=\"" + cart_reader["sc_cd_id"] + "\">×</a> </td>";
                        cart_inside.Text += "<td class=\"product-img\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside.Text += "<td class=\"temp_pname product-name\" data-title=\"商品名稱\"><span>" + cart_reader["cd_name"] + "</span> </td>";
                        cart_inside.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$" + cd_price + "</span></td>";
                        /*找SIZE START*/
                        if (cart_reader["sz_s"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\" id=\"" + cart_reader["sc_cd_id"] + "sminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            cart_size += "<span class=\"squantity_value\" id=\"" + cart_reader["sc_cd_id"] + "svalue\">" + sc_num + "</span><span class=\"splus\" id=\"" + cart_reader["sc_cd_id"] + "splus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                            string[] cz_s = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_s[0] = sc_num.ToString();
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_s;
                            ck_first = 1;

                        }
                        else
                        {
                            string[] cz_s = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_s[0] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_s;
                        }
                        if (cart_reader["sz_m"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">M</div><div style=\"float: right\"><span class=\"mminus\" id=\"" + cart_reader["sc_cd_id"] + "mminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">" + sc_num + "</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_m[1] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_m;
                            }
                            else
                            {
                                cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">0</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_m[1] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_m;
                            }
                        }
                        else
                        {
                            string[] cz_m = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_m[1] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_m;


                        }
                        if (cart_reader["sz_l"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">L</div><div style=\"float: right\"><span class=\"lminus\" id=\"" + cart_reader["sc_cd_id"] + "lminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">" + sc_num + "</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_l[2] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_l;


                            }
                            else
                            {
                                cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">0</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_l[2] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_l;
                            }
                        }
                        else
                        {
                            string[] cz_l = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_l[2] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_l;

                        }
                        if (cart_reader["sz_xl"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">XL</div><div style=\"float: right\"><span class=\"xlminus\" id=\"" + cart_reader["sc_cd_id"] + "xlminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0)
                            {
                                cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">" + sc_num + "</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1;
                                string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_xl[3] = sc_num.ToString();
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;

                            }
                            else
                            {
                                cart_size += "<span class=\"xlquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">0</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                                string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                                cz_xl[3] = "0";
                                Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;
                            }
                        }
                        else
                        {
                            string[] cz_xl = (string[])Session[cart_reader["sc_cd_id"].ToString()];
                            cz_xl[3] = "0";
                            Session[cart_reader["sc_cd_id"].ToString()] = cz_xl;
                        }


                        /*找SIZE END*/
                        //輩分cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br><div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\"><i aria-hidden=\"true\" class=\"fa fa-minus\"></i></span><span class=\"squantity_value\">" + sc_num + "</span> <span class=\"splus\"><i aria-hidden=\"true\" class=\"fa fa-plus\"></i></span></div><br></td>";
                        cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br>" + cart_size + "<br></td>";
                        cart_inside.Text += "<td class=\"product-subtotal\" data-title=\"單項總額\"><span class=\"woocommerce-Price-amount amount cal_total\" id=\"" + cart_reader["sc_cd_id"] + "price\">$" + itemprice + "</td>";
                        cart_totalfee += itemprice;

                        cart_inside.Text = cart_inside.Text + "</tr>";
                    }

                }
                //string[] test = (string[])Session["cd00000001"];
                //Response.Redirect("?page=login" + test[0] + test[1] + test[2] + test[3]);


                cart_outside3 += "<div class=\"col-lg-4 blog_item_col\"><div class=\"blog_item\"><div class=\"cart-collaterals\"><div class=\"cart_totals text-center\">";
                cart_outside3 += "<h2>購物車總金額</h2><table class=\"shop_table shop_table_responsive\">";
                cart_outside3 += "<tr class=\"cart-subtotal\"><th>運費</th><td data-title=\"運費\"><span class=\"woocommerce-Price-amount amount\">$0</span></td></tr>";
                cart_outside3 += "<tr class=\"order-total\"><th>總額</th><td data-title=\"總額\"><strong><span class=\"woocommerce-Price-amount amount\" id=\"final_price\">$" + cart_totalfee + "</span></strong></td></tr>";
                cart_outside3 += "</table></div></div></div></div></div>";

                cart_inside.Text = cart_outside + cart_inside.Text + "</tbody></table>";
                cart_inside.Text += cart_outside2 + cart_inside2.Text + "</tr></tbody></table></div></div></div></div>";
                cart_inside.Text += cart_outside3;

                PH_cart.Controls.Add(cart_inside);
                cart_reader.Close();
                temp_total = cart_totalfee.ToString();
                /*購物車END*/

                //許願池start
                String outsidewish = "";
                Label insidewish = new Label();//產生多次
                                               //cnSQL2.Open();
                                               //strSQLorder = "select * from Members mb left join Orders od on mb.mm_id=od.od_mm_id where mm_act='"; 
                                               //strSQLorder +=  Session["user"].ToString() + "' order by od.od_orderdate desc ;";


                SqlCommand orderwish = new SqlCommand(@"select *,(select count(*) from Promotion_events where pe_cd_id=ww_cd_id ) as 'isinpe'  from  Wishing_Wells join Commodities cd on ww_cd_id = cd.cd_id left join Promotion_events pe on ww_cd_id= pe.pe_cd_id where ww_mm_id = @ww_mm_id order by ww_wishdate desc;", cnSQL2);
                orderwish.Parameters.Add(new SqlParameter("@ww_mm_id", mm_id));

                SqlDataReader readerwish = orderwish.ExecuteReader();
                insidewish.Text = "";

                outsidewish += "<div class=\"row blogs_container\">";

                while (readerwish.Read())
                {
                    if (readerwish["isinpe"].ToString() == "1" && readerwish["pe_status"].ToString() == "Y")
                    {
                        double discount = Convert.ToDouble(readerwish["pe_discount"].ToString());
                        int price = Convert.ToInt32(readerwish["cd_price"].ToString());
                        int event_price = Convert.ToInt32(price * discount);
                        insidewish.Text += "<div class=\"col-lg-4 blog_item_col\"><div class=\"blog_item\">";
                        insidewish.Text += "<span style=\"color:red;font-size:100px;float:left \">.</span>";
                        insidewish.Text += "<div class=\"blog_background\" style=\"background-image: url(images/clothes/" + readerwish["cd_id"] + ".jpg)\"></div>";
                        insidewish.Text += "<div class=\"blog_content d-flex flex-column align-items-center justify-content-center text-center\">";
                        insidewish.Text += "<h4 class=\"blog_title\">" + readerwish["cd_name"] + "</h4>";
                        insidewish.Text += "<span class=\"blog_meta\" style=\"text-decoration: line-through\">" + "$" + "" + readerwish["cd_price"] + "</span><span class=\"blog_meta\">$" + event_price + "</span>";
                        insidewish.Text += "<span class=\"blog_meta\">" + readerwish["ww_wishdate"] + "</span>";
                        insidewish.Text += "<a class=\"blog_more\" href=\"?pid=" + readerwish["cd_id"] + "\">去瞧瞧</a></div></div>";
                        insidewish.Text += "<div class=\"text-center delete_wish\"id=\"" + readerwish["cd_id"] + "\" runat=\"server\" ><a style=color:red;\">取消追蹤</a> </td></div></div>";


                    }
                    else
                    {
                        insidewish.Text += "<div class=\"col-lg-4 blog_item_col\"><div class=\"blog_item\">";
                        insidewish.Text += "<div class=\"blog_background\" style=\"background-image: url(images/clothes/" + readerwish["cd_id"] + ".jpg)\"></div>";
                        insidewish.Text += "<div class=\"blog_content d-flex flex-column align-items-center justify-content-center text-center\">";
                        insidewish.Text += "<h4 class=\"blog_title\">" + readerwish["cd_name"] + "</h4>";
                        insidewish.Text += "<span class=\"blog_meta\">" + "$" + "" + readerwish["cd_price"] + " </span>";
                        insidewish.Text += "<span class=\"blog_meta\">" + readerwish["ww_wishdate"] + "</span>";
                        insidewish.Text += "<a class=\"blog_more\" href=\"?pid=" + readerwish["cd_id"] + "\">去瞧瞧</a></div></div>";
                        insidewish.Text += "<div class=\"text-center delete_wish\"id=\"" + readerwish["cd_id"] + "\" runat=\"server\" ><a style=color:red;\">取消追蹤</a> </td></div></div>";
                    }



                }
                insidewish.Text = outsidewish + insidewish.Text + "</div>";


                PH_wish.Controls.Add(insidewish);
                readerwish.Close();
                //許願池end


                //許願池tag start

                //Label insidewishtag = new Label();
                //SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL2);
                //find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_id));
                //var ww_id = mm_id.Replace("mm", "ww");

                //SqlCommand wishtag = new SqlCommand(@"select * from  Follow join Tags on fw_tag_id=tag_id where fw_ww_id = @ww_mm_id;", cnSQL2);
                //wishtag.Parameters.Add(new SqlParameter("@ww_mm_id", ww_id));

                //SqlDataReader readerwishtag = wishtag.ExecuteReader();
                //insidewishtag.Text = "";

                //while (readerwishtag.Read())
                //{

                //    insidewishtag.Text += "<a href=\"#\" style=\"color: black\" id=\""+readerwishtag["tag_id"]+ " \" >" + readerwishtag["tag_name"] + "</a>";
                    
                //}

                //PH_wishtag.Controls.Add(insidewishtag);
                //readerwishtag.Close();

                //許願池tag end



            }   /* <-- Session=0尾 */


            if (Session["identity"].ToString() == "1")
            {
                start_display.Visible = false;
                string adm_name = Session["user"].ToString();
                lb_hello2.Visible = true;
                lb_hello2.Text = "親愛的" + adm_name + "管理員,您好!";
                nav_nologin.Visible = false;
                nav_user.Visible = false;
                nav_administrator.Visible = true;

                var admpage = Request.QueryString["page"];

                if (admpage != null && admpage == "admmm")
                {
                    fslogo.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_memberm_display.Visible = true;

                }
                if (admpage != null && admpage == "admcd")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_cdm_display.Visible = true;

                }

                if (admpage != null && admpage == "admod")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_odm_display.Visible = true;


                }
                if (admpage != null && admpage == "newadmm")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_newadm_display.Visible = true;


                }
                if (admpage != null && admpage == "newuser")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_newuser_display.Visible = true;


                }
                if (admpage != null && admpage == "newtag")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_newtag_display.Visible = true;

                }
                //adm_odd_additem
                if (admpage != null && admpage == "adm_odd_additem")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    div_odd_additem_display.Visible = true;                   
                   
                }
                if (admpage != null && admpage == "adm_ped_additem")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    div_ped_additem_display.Visible = true;
                    

                }
                if (admpage != null && admpage == "admaddcd")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    div_odd_additem_display.Visible = false;
                    adm_event_display.Visible = false;
                    div_ped_additem_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    div_addnewcd_display.Visible = true;

                    if (!IsPostBack)
                    {
                        ckb_addnewcd_s.Checked = false;
                        ckb_addnewcd_m.Checked = false;
                        ckb_addnewcd_l.Checked = false;
                        ckb_addnewcd_xl.Checked = false;                        
                    }
                    else
                    {
                        ckb_addnewcd_s.Controls.Clear();
                        ckb_addnewcd_m.Controls.Clear();
                        ckb_addnewcd_l.Controls.Clear();
                        ckb_addnewcd_xl.Controls.Clear();
                    }

                }
                if (admpage != null && admpage == "admevent")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_newevent_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_event_display.Visible = true;
                    

                }
                if (admpage != null && admpage == "admnewevent")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    adm_tag_display.Visible = false;
                    adm_newevent_display.Visible = true;
                    


                }
                if (admpage != null && admpage == "admtag")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_newtag_display.Visible = false;
                    adm_newuser_display.Visible = false;
                    div_addnewcd_display.Visible = false;
                    adm_event_display.Visible = false;
                    adm_tagdetail_display.Visible = false;
                    adm_tag_display.Visible = true;
                    
                }


                try
                {
                    var getmm_no = Request.QueryString["mm_no"].ToString();


                    /*mm*/
                    if (getmm_no != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
                        adm_cddetail_display.Visible = false;
                        adm_odmdetail_display.Visible = false;
                        adm_eventdetail_display.Visible = false;
                        adm_tagdetail_display.Visible = false;
                        adm_mmdetail_display.Visible = true;

                        SqlConnection cnSQL3 = new SqlConnection(strConnString);

                        string od_count;
                        SqlCommand mmdetail_produce = new SqlCommand(@"select * from Members where mm_no=@mm_no;", cnSQL3);
                        mmdetail_produce.Parameters.Add(new SqlParameter("@mm_no", getmm_no));

                        SqlCommand find_od_num = new SqlCommand(@"select count(*) from Members m join Orders o on m.mm_id=o.od_mm_id where mm_no=@mm_no;", cnSQL3);
                        find_od_num.Parameters.Add(new SqlParameter("@mm_no", getmm_no));

                        cnSQL3.Open();
                        od_count = find_od_num.ExecuteScalar().ToString();
                        SqlDataReader mmdetail_reader = mmdetail_produce.ExecuteReader();


                        while (mmdetail_reader.Read())
                        {

                            DateTime dt = Convert.ToDateTime(mmdetail_reader["mm_bd"].ToString());
                            lb_adm_mmname.Text = mmdetail_reader["mm_name"].ToString();
                            lb_adm_mmadr.Text = mmdetail_reader["mm_adr"].ToString();
                            lb_adm_mmphone.Text = mmdetail_reader["mm_phone"].ToString();
                            lb_adm_mmmail.Text = mmdetail_reader["mm_mail"].ToString();
                            lb_adm_mmbd.Text = dt.ToString("yyyy/MM/dd");
                            lb_adm_mmnotice.Text = mmdetail_reader["mm_notice"].ToString();
                            lb_adm_odnum.Text = od_count;

                        }
                        cnSQL3.Close();

                        mmdetail_reader.Close();

                    }

                }
                catch { }
                try
                {
                    /*CD*/
                    var getcd_no = Request.QueryString["cd_no"].ToString();
                    if (getcd_no != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
                        adm_mmdetail_display.Visible = false;
                        adm_odmdetail_display.Visible = false;
                        adm_eventdetail_display.Visible = false;
                        adm_tagdetail_display.Visible = false;
                        adm_cddetail_display.Visible = true;

                        SqlConnection cnSQL3 = new SqlConnection(strConnString);
                        SqlCommand cddetail_produce = new SqlCommand(@"select * from Commodities c join Sizes s on c.cd_id =s.sz_cd_id where cd_no=@cd_no;", cnSQL3);
                        cddetail_produce.Parameters.Add(new SqlParameter("@cd_no", getcd_no));
                        cnSQL3.Open();

                        SqlDataReader cddetail_reader = cddetail_produce.ExecuteReader();
                        SqlConnection cnSQL4 = new SqlConnection(strConnString);
                        cnSQL4.Open();
                        string temp_tag_name = "";
                        while (cddetail_reader.Read())
                        {

                            string colorcode = "#" + cddetail_reader["cd_color"].ToString();
                            string strstyle = "width:30px;height:25px;background-color:" + colorcode;

                            string colorname = ConvertColor(colorcode);
                            if (!IsPostBack)
                            {
                                img_cd.Attributes.Clear();
                                if (cddetail_reader["sz_s"].ToString() == "Y") { ckb_adm_s.Checked = true; } else { ckb_adm_s.Checked = false; }
                                if (cddetail_reader["sz_m"].ToString() == "Y") { ckb_adm_m.Checked = true; } else { ckb_adm_m.Checked = false; }
                                if (cddetail_reader["sz_l"].ToString() == "Y") { ckb_adm_l.Checked = true; } else { ckb_adm_l.Checked = false; }
                                if (cddetail_reader["sz_xl"].ToString() == "Y") { ckb_adm_xl.Checked = true; } else { ckb_adm_xl.Checked = false; }
                                div_adm_cdcolor.Attributes.Add("style", strstyle);
                                lb_colorcode.Text = colorcode;
                                img_cd.Attributes.Add("src", "images/clothes/"+cddetail_reader["cd_id"].ToString()+".jpg");
                            }
                            else
                            {
                                ckb_adm_s.Controls.Clear();
                                ckb_adm_m.Controls.Clear();
                                ckb_adm_l.Controls.Clear();
                                ckb_adm_xl.Controls.Clear();
                                div_adm_cdcolor.Attributes.Clear();
                                
                                //lb_colorcode.Text = "";
                            }

                            lb_adm_cdname.Text = cddetail_reader["cd_name"].ToString();
                            lb_adm_cdgroup.Text = cddetail_reader["cd_group"].ToString();
                            div_adm_cdcolor.Attributes.Add("style", strstyle);
                            lb_adm_cdkind.Text = cddetail_reader["cd_kind"].ToString();
                            lb_adm_cdmaterial.Text = cddetail_reader["cd_material"].ToString();
                            lb_adm_cdprice.Text = cddetail_reader["cd_price"].ToString();
                            lb_adm_cdmadein.Text = cddetail_reader["cd_madein"].ToString();
                            lb_adm_cdplace.Text = cddetail_reader["cd_place"].ToString();
                            try
                            {
                                SqlCommand cdtag_produce = new SqlCommand(@"select * from Have h join Tags t on h.have_tag_id=tag_id where have_cd_id=@have_cd_id;", cnSQL4);
                                cdtag_produce.Parameters.Add(new SqlParameter("@have_cd_id", cddetail_reader["cd_id"].ToString()));
                                SqlDataReader cdtag_produce_reader = cdtag_produce.ExecuteReader();
                                while (cdtag_produce_reader.Read())
                                {
                                    temp_tag_name += cdtag_produce_reader["tag_name"] + ",";

                                }
                                temp_tag_name += "@";
                                string tag_name = temp_tag_name.Replace(",@", "");
                                if (tag_name == "@") { lb_adm_cdtag.Text = "無"; }
                                else { lb_adm_cdtag.Text = tag_name; }

                                cdtag_produce_reader.Close();
                            }
                            catch
                            {
                                lb_adm_cdtag.Text = "無";
                            }

                        }
                        cnSQL4.Close();
                        cnSQL4.Dispose();
                        cnSQL3.Close();
                        cddetail_reader.Close();
                    }
                }
                catch { }
                try
                {
                    /*OD*/
                    var getod_no = Request.QueryString["od_no"].ToString();
                    if (getod_no != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
                        adm_mmdetail_display.Visible = false;
                        adm_cddetail_display.Visible = false;
                        adm_eventdetail_display.Visible = false;
                        adm_tagdetail_display.Visible = false;
                        adm_odmdetail_display.Visible = true;

                        SqlDataSource_oddetail.SelectCommand = "select od_no,od_id,od_mm_id,od_done,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount from Orders o join Order_detail odd on o.od_id=odd.odd_od_id where od_no='" + getod_no + "';";

                        SqlConnection cnSQL3 = new SqlConnection(strConnString);
                        SqlCommand oddetail_produce = new SqlCommand(@"select * from Orders where od_no=@od_no;", cnSQL3);
                        oddetail_produce.Parameters.Add(new SqlParameter("@od_no", getod_no));
                        cnSQL3.Open();

                        SqlDataReader oddetail_reader = oddetail_produce.ExecuteReader();

                        while (oddetail_reader.Read())
                        {                            
                            if (!IsPostBack)
                            {
                                if (oddetail_reader["od_done"].ToString() == "Y") { ckb_done.Checked = true; } else { ckb_done.Checked = false; }
                            }
                            else
                            {
                                ckb_done.Controls.Clear();
                            }

                            lb_adm_payment.Text = oddetail_reader["od_paymant"].ToString();
                            lb_adm_delivery.Text = oddetail_reader["od_delivery"].ToString();
                            lb_adm_adr.Text = oddetail_reader["od_d_address"].ToString();
                            lb_adm_phone.Text = oddetail_reader["od_phone"].ToString();
                            lb_adm_note.Text = oddetail_reader["od_note"].ToString();

                        }
                        cnSQL3.Close();
                        oddetail_reader.Close();
                    }

                }
                catch
                {

                }
                try
                {
                    /*PED*/
                    var getpe_title = Request.QueryString["pe_title"].ToString();
                    if (getpe_title != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
                        adm_mmdetail_display.Visible = false;
                        adm_cddetail_display.Visible = false;
                        adm_odmdetail_display.Visible = false;
                        adm_tagdetail_display.Visible = false;
                        adm_eventdetail_display.Visible = true;

                        SqlDataSource_pedetail.SelectCommand = "select * from  Promotion_events where pe_title='" + getpe_title + "';";

                        SqlConnection cnSQL3 = new SqlConnection(strConnString);
                        SqlCommand pedetail_produce = new SqlCommand(@"select * from Promotion_events where pe_title=@pe_title;", cnSQL3);
                        pedetail_produce.Parameters.Add(new SqlParameter("@pe_title", getpe_title));
                        cnSQL3.Open();

                        SqlDataReader pedetail_reader = pedetail_produce.ExecuteReader();

                        while (pedetail_reader.Read())
                        {
                            //"yyyy-MM-dd"
                            DateTime dt_st = Convert.ToDateTime(pedetail_reader["pe_starting_date"].ToString());
                            DateTime dt_ex = Convert.ToDateTime(pedetail_reader["pe_expiring_date"].ToString());
                            lb_pe_title.Text = pedetail_reader["pe_title"].ToString();
                            lb_pe_stdate.Text = dt_st.ToString();
                            lb_pe_exdate.Text = dt_ex.ToString();
                            if (!IsPostBack)
                            {
                                if (pedetail_reader["pe_imgPath"].ToString() != "" && pedetail_reader["pe_imgPath"].ToString() != " " && pedetail_reader["pe_imgPath"].ToString() != null)
                                {
                                    //string path = "img/logo.png";
                                    img_pe.Visible = true;
                                    img_pe.Attributes.Add("src", pedetail_reader["pe_imgPath"].ToString());
                                }
                                else { lb_pe_imgmsg.Text = "無"; }

                                if (pedetail_reader["pe_main"].ToString() == "Y") { ckb_pe_main.Checked = true; } else { ckb_pe_main.Checked = false; }
                                if (pedetail_reader["pe_status"].ToString() == "Y") { ckb_pe_status.Checked = true; } else { ckb_pe_status.Checked = false; }
                            }
                            else
                            {
                                ckb_pe_status.Controls.Clear();
                                ckb_pe_main.Controls.Clear();
                                lb_pe_imgmsg.Text = lb_pe_imgmsg.Text;
                                
                            }

                            lb_pe_discount.Text = pedetail_reader["pe_discount"].ToString();

                        }
                        cnSQL3.Close();
                        pedetail_reader.Close();

                    }
                }
                catch
                {

                }
                try
                {
                    /*tag*/
                    var gettag_no = Request.QueryString["tag_no"].ToString();
                    if (gettag_no != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
                        adm_mmdetail_display.Visible = false;
                        adm_cddetail_display.Visible = false;
                        adm_odmdetail_display.Visible = false;
                        adm_eventdetail_display.Visible = false;
                        adm_tagdetail_display.Visible = true;
                        

                        SqlConnection cnSQL3 = new SqlConnection(strConnString);

                        SqlCommand tagdetail_produce = new SqlCommand(@"select * from Tags where tag_no=@tag_no", cnSQL3);
                        tagdetail_produce.Parameters.Add(new SqlParameter("@tag_no", gettag_no));
                        cnSQL3.Open();
                        SqlDataReader tagdetail_reader = tagdetail_produce.ExecuteReader();


                        while (tagdetail_reader.Read())
                        {
                            SqlConnection cnSQL4 = new SqlConnection(strConnString);
                            cnSQL4.Open();
                            SqlCommand find_taguse = new SqlCommand(@"select count(*) from Have where have_tag_id=@have_tag_id;", cnSQL4);
                            find_taguse.Parameters.Add(new SqlParameter("@have_tag_id", tagdetail_reader["tag_id"]));                           
                            string result_taguse = find_taguse.ExecuteScalar().ToString();
                            lb_taguseamount.Text = result_taguse;
                            lb_tag_id.Text = tagdetail_reader["tag_id"].ToString();
                            lb_tag_name.Text = tagdetail_reader["tag_name"].ToString();
                            lb_tag_date.Text = tagdetail_reader["tag_date"].ToString();
                        }
                        cnSQL3.Close();

                        tagdetail_reader.Close();


                    }
                }
                catch
                {

                }

                /*複製用try { } catch { }*/

            }
            var page2 = Request.QueryString["page"];
            if (page2 != null && page2 == "wish")
            {
                fslogo.Visible = false;
                start_display.Visible = false;
                cart_display.Visible = false;
                profile_display.Visible = false;
                order_display.Visible = false;
                register_display.Visible = false;
                wish_display.Visible = true;
            }
            if (page2 != null && page2 == "ckout")
            {
                fslogo.Visible = false;
                start_display.Visible = false;
                wish_display.Visible = false;
                order_display.Visible = false;
                profile_display.Visible = false;
                register_display.Visible = false;
                cart_display.Visible = true;

                if (cart_table.Visible == true)
                {
                    string odrphone= Session["mmphone"].ToString();
                    if (!IsPostBack)
                    {
                        if (ckb_takemmphone.Checked == true)
                        {


                            tb_odrphone.Text = odrphone;
                            
                        }
                        

                    }

                }
            }

            if (page2 != null && page2 == "order")
            {
                fslogo.Visible = false;
                start_display.Visible = false;
                wish_display.Visible = false;
                cart_display.Visible = false;
                profile_display.Visible = false;
                register_display.Visible = false;
                order_display.Visible = true;
            }
            if (page2 != null && page2 == "profile")
            {
                fslogo.Visible = false;
                start_display.Visible = false;
                cart_display.Visible = false;
                wish_display.Visible = false;
                order_display.Visible = false;
                register_display.Visible = false;
                profile_display.Visible = true;
            }

            if (page2 != null && page2 == "logout")
            {
                Session.RemoveAll();
                Session["identity"] = null;
                Session["user"] = null;
                Session["mmid"] = null;
                Session["mmphone"] = null;
                Session["mmadr"] = null;
                Session["notice"] = null;
                Response.Redirect("Default.aspx");
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

        //start_display.Visible = true;
        PlaceHolder1.Controls.Clear();
        PlaceHolder2.Controls.Clear();
		PlaceHolder3.Controls.Clear();
        PlaceHolder4.Controls.Clear();
        //掃描頁面

        var page = Request.QueryString["page"];


        if (page != null && page == "produceqr")
        {
            produceqr();
        }

        if (page != null && page == "scan")
        {
            fslogo.Visible = false;
            start_display.Visible = false;
            cart_display.Visible = false;
            wish_display.Visible = false;
            order_display.Visible = false;
            profile_display.Visible = false;
            register_display.Visible = false;
            login_display.Visible = false;
            fgpw_display.Visible = false;
            scan_qr_div.Visible = true;

        }
        if (page != null && page == "register")
        {
            fslogo.Visible = false;
            start_display.Visible = false;
            cart_display.Visible = false;
            wish_display.Visible = false;
            order_display.Visible = false;
            profile_display.Visible = false;
            scan_qr_div.Visible = false;
            login_display.Visible = false;
            fgpw_display.Visible = false;
            register_display.Visible = true;

        }
        if (page != null && page == "login")
        {
            fslogo.Visible = false;
            start_display.Visible = false;
            cart_display.Visible = false;
            wish_display.Visible = false;
            order_display.Visible = false;
            profile_display.Visible = false;
            scan_qr_div.Visible = false;
            register_display.Visible = false;
            fgpw_display.Visible = false;
            login_display.Visible = true;

        }
        if (page != null && page == "fgpw")
        {
            fslogo.Visible = false;
            start_display.Visible = false;
            cart_display.Visible = false;
            wish_display.Visible = false;
            order_display.Visible = false;
            profile_display.Visible = false;
            scan_qr_div.Visible = false;
            register_display.Visible = false;
            login_display.Visible = false;
            fgpw_display.Visible = true;


        }



        //資料庫連線
        SqlConnection cnSQL = new SqlConnection();
        //連線字串
        cnSQL.ConnectionString = strConnString;
        /*新貨到start*/
        SqlCommand cmdSQL = new SqlCommand();
        cmdSQL.Connection = cnSQL;
        //strSQL 為select form where insert
        String strSQL;
        String outside = "";//只產生一次
        Label inside = new Label();//產生多次
        strSQL = "select top 10* from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id Order By cd_inserttime desc;";
        cmdSQL.CommandText = strSQL;
        cnSQL.Open();
        SqlDataReader reader = cmdSQL.ExecuteReader();
        
        outside += "<div class=\"best_sellers\"><div class=\"container\"><div class=\"row\"><div class=\"col text-center\">";
        outside += "<div class=\"section_title new_arrivals_title\"><h2>新貨到</h2></div></div></div>";
        outside += "<div class=\"row\"><div class=\"col\"><div class=\"product_slider_container\"><div class=\"owl-carousel owl-theme product_slider\">";
        inside.Text = "";
        
            while (reader.Read())
            {
                double pe_discount = 0;
                if (reader["pe_discount"].ToString() != "" && reader["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader["pe_discount"].ToString()) * 10;
                }
                inside.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if ((reader["pe_main"].ToString() == "N" || reader["pe_main"].ToString() == "Y") && reader["pe_status"].ToString() == "Y")
            {
                    inside.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }//sale 改折數 100-100*reader["pe_discount"]  = 20% off
                if (Session["identity"] != null)
                {
                    inside.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ reader["cd_id"]+"wish"+"\"></div>";
                }                    
                inside.Text += "<div class=\"product_info\">";
                inside.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader["cd_id"] + "\">" + reader["cd_name"] + "</a></h6>";
                inside.Text += "<div class=\"product_price\">" + "$" + reader["cd_price"] + "</div></div></div></div></div>";
            }
        
       
          
        cnSQL.Close();
        inside.Text = outside + inside.Text + "</div><div class=\"product_slider_nav_left product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-left\" aria-hidden=\"true\"></i></div>";
        inside.Text += "<div class=\"product_slider_nav_right product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i></div>";
        inside.Text += "</div></div></div></div></div>";
        PlaceHolder1.Controls.Add(inside);
        /*新貨到end*/


        /*熱銷商品start*/
        SqlCommand cmdSQL1 = new SqlCommand();        
        cmdSQL1.Connection = cnSQL;        
        //strSQL 為select form where insert
        String strSQL1;
        String outside1 = "";//只產生一次
        Label inside1 = new Label();//產生多次


        strSQL1 = "select top 10* from Hot_saled left join Promotion_events pe on hot_cd_id = pe.pe_cd_id left join Commodities on hot_cd_id = cd_id order by hot_count desc;";        
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
                double pe_discount = 0;
                if (reader1["pe_discount"].ToString() != "" && reader1["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader1["pe_discount"].ToString()) * 10;
                }
                inside1.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside1.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader1["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if ((reader1["pe_main"].ToString() == "N" || reader1["pe_main"].ToString() == "Y") && reader1["pe_status"].ToString() == "Y")
            {
                    inside1.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
            if (Session["identity"] != null)
            {
                inside1.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ reader1["cd_id"]+"wish2"+"\"></div>";
            }
                
                inside1.Text += "<div class=\"product_info\">";
                inside1.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader1["cd_id"] + "\">" + reader1["cd_name"] + "</a></h6>";
                inside1.Text += "<div class=\"product_price\">" + "$" + reader1["cd_price"] + "</div></div></div></div></div>";
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
            fslogo.Visible = false;
            start_display.Visible = false;
            SqlCommand cmdSQL2 = new SqlCommand();
            cmdSQL2.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL2;
            //String outside2 = "";//只產生一次
            Label inside2 = new Label();//產生多次


            /****************/
            SqlConnection cnSQLgroup = new SqlConnection();
            cnSQLgroup.ConnectionString = strConnString;
            SqlCommand cmdSQLgroup = new SqlCommand();
            cmdSQLgroup.Connection = cnSQLgroup;
            String strSQLgroup;
            strSQLgroup = "select * from  Commodities where cd_group=  (SELECT cd_group FROM Commodities WHERE cd_id = '";
            strSQLgroup += Request.QueryString["pid"] + "');";
            cmdSQLgroup.CommandText = strSQLgroup;
            cnSQLgroup.Open();
            SqlDataReader readergroup = cmdSQLgroup.ExecuteReader();

            /****************/



            strSQL2 = "select * ,(select count(*) from Promotion_events where pe_cd_id=cd_id ) as 'isinpe' from Commodities left join Have on have_cd_id=cd_id left join Tags on have_tag_id=tag_id left join Promotion_events pe on cd_id = pe.pe_cd_id where cd_id='";
            strSQL2 += Request.QueryString["pid"] + "';";
            cmdSQL2.CommandText = strSQL2;
            cnSQL.Open();
            SqlDataReader readersingle = cmdSQL2.ExecuteReader();
            inside2.Text = "";
            while (readersingle.Read())
            {
                inside2.Text += "<div class=\"container single_product_container\"style=\"margin-top: 140px;\">";
                inside2.Text += "<div class=\"row\"><div class=\"col\">";
                inside2.Text += "<div class=\"breadcrumbs d-flex flex-row align-items-center\">";
                inside2.Text += "<ul><li><a href=\"Default.aspx\">首頁</a></li>";
                inside2.Text += "<!-- 資料庫類別(撈資料庫) --><li><a href=\"?kind="+ readersingle["cd_kind"] + "\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>"+ readersingle["cd_kind"]+"</a></li>";
                inside2.Text += "<li class=\"active\"><a href=\"#\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>商品資訊</a></li></ul>";
                inside2.Text += "</div></div></div>";
                inside2.Text += "<!-- 商品圖片 --><div class=\"row\"><div class=\"col-lg-7\"><div class=\"single_product_pics\">";
                inside2.Text += "<div class=\"row\"><div class=\"col-lg-9 image_col order-lg-2 order-1\">";
                inside2.Text += "<div class=\"single_product_image\"><div class=\"single_product_image_background\" style=\"background-image:url(images/clothes/"+ readersingle["cd_id"]+".jpg)\"></div>";
                inside2.Text += "</div></div></div></div></div>";
                inside2.Text += "<div class=\"col-lg-5\">";
                inside2.Text += "<div class=\"product_details\">";
                inside2.Text += "<div class=\"product_details_title\">";
 /*商品名稱*/   inside2.Text += "<h2 class=\"product_name\" id=\""+ readersingle["cd_id"] + "\"><a href=\"?pid=" + readersingle["cd_id"] + "\">" + readersingle["cd_name"] + "</a></h2>";
 /*商品材質*/   inside2.Text += "<p>" +"材質："+ readersingle["cd_material"] + " </p></div>";
                if (readersingle["isinpe"].ToString() == "1" && readersingle["pe_status"].ToString() == "Y")
                {
                    double discount = Convert.ToDouble(readersingle["pe_discount"].ToString());
                    int price = Convert.ToInt32(readersingle["cd_price"].ToString());
                    int event_price = Convert.ToInt32(price * discount);
                    inside2.Text += "<div class=\"product_price\" style=\"text-decoration:line-through\">" + "$" + readersingle["cd_price"] + "</div></br>";
                    inside2.Text += "<div class=\"product_price\">" + "$" + event_price + "</div>";
                }
                else
                {
                    inside2.Text += "<div class=\"product_price\">" + "$" + readersingle["cd_price"] + "</div>";
                }
                /*商品價格*/
                /*選擇尺寸*/
                inside2.Text += "<div class=\"product_color\">";
                inside2.Text += "<span>其他款式:</span><ul>";
                while (readergroup.Read())
                {
                    inside2.Text += "<a href=\"?pid=" + readergroup["cd_id"] + "\"><li style=\"  background: #" + readergroup["cd_color"] + ";border:solid 0.1px #000000;\"></li></a>";
                }
                inside2.Text += "</ul></div>";
                
/*加入購物車*/  inside2.Text += "<div class=\"quantity d-flex flex-column flex-sm-row align-items-sm-center\">";
                inside2.Text += "<span>數量:</span><div class=\"quantity_selector\">";
                inside2.Text += "<span class=\"minus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span><span id=\"quantity_value\" >1</span>";
                inside2.Text += "<span class=\"plus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div>";
                inside2.Text += "<a class=\"shoppingcart\"> 加入購物車</a>";
                if (Session["identity"] != null)
                {
                inside2.Text += "<div class=\"product_favorite d-flex flex-column align-items-center justify-content-center lbh\" id=\"" + readersingle["cd_id"] + "wish" + "\"></div>";
                }
                    
/*tags*/        inside2.Text += "</div><div class=\"quantity d-flex flex-column flex-sm-row align-items-sm-center\"><div class=\"widget widget_tag_cloud text - center\">";
                while (readersingle.Read())
                {

                    inside2.Text += "<a class=\"wish_tag\" href=\"#\" style=\"color: black\" id =" + readersingle["have_tag_id"] + "> #" + readersingle["tag_name"] + "</a>";
                }
                inside2.Text += "</div ></div ></div></div></div></div>";
                
            }
            readersingle.Close();
            cnSQL.Close();

            /*您可能會感興趣的商品...start*/
            SqlCommand cmdSQL4 = new SqlCommand();
            cmdSQL4.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL3;
            String outside3 = "";//只產生一次
            Label inside3 = new Label();//產生多次

            SqlCommand cd_kindCM = new SqlCommand();
            cd_kindCM.Connection = cnSQL;
            String strcd_kindSQL;            
            strcd_kindSQL = "select cd_kind from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id where cd_id='" + Request.QueryString["pid"] + "'";
            cd_kindCM.CommandText = strcd_kindSQL;
            
            cnSQL.Open();
            String cd_kind = "";
            try
            {
                cd_kind = cd_kindCM.ExecuteScalar().ToString();
            }
            catch { }
            cnSQL.Close();

            strSQL3 = "select top 10 *, NewID() as random from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id where cd_kind='" + cd_kind + "' order by random;";//亂序產生同類型商品
            cmdSQL4.CommandText = strSQL3;
            cnSQL.Open();
            SqlDataReader reader2 = cmdSQL4.ExecuteReader();
            //頭
            outside3 += "<div class=\"best_sellers\"><div class=\"container\"><div class=\"row\"><div class=\"col text-center\">";
            outside3 += "<div class=\"section_title new_arrivals_title\"><h2>您可能也喜歡...</h2></div></div></div>";
            outside3 += "<div class=\"row\"><div class=\"col\"><div class=\"product_slider_container\"><div class=\"owl-carousel owl-theme product_slider\">";
            inside3.Text = "";
            while (reader2.Read())
            {
                double pe_discount = 0;
                if (reader2["pe_discount"].ToString() != "" && reader2["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader2["pe_discount"].ToString()) * 10;
                }
                inside3.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside3.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader2["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if ((reader2["pe_main"].ToString() == "N" || reader2["pe_main"].ToString() == "Y") && reader2["pe_status"].ToString() == "Y")
                {
                    inside3.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                if (Session["identity"] != null)
                {
                inside3.Text += "<div class=\"favorite favorite_left lbh\" id=\""+reader2["cd_id"]+"wish2"+"\"></div>";
                }
                    
                inside3.Text += "<div class=\"product_info\">";
                inside3.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader2["cd_id"] + "\">" + reader2["cd_name"] + "</a></h6>";
                inside3.Text += "<div class=\"product_price\">" + "$" + reader2["cd_price"] + "</div></div></div></div></div>";

            }
            cnSQL.Close();
            inside3.Text = outside3 + inside3.Text + "</div><div class=\"product_slider_nav_left product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-left\" aria-hidden=\"true\"></i></div>";
            inside3.Text += "<div class=\"product_slider_nav_right product_slider_nav d-flex align-items-center justify-content-center flex-column\"><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i></div>";
            inside3.Text += "</div></div></div></div></div>";
            /*您可能會感興趣的商品...end*/

            inside2.Text += inside3.Text;
            PlaceHolder3.Controls.Add(inside2);
        }
        /*商品資訊end*/


        /*商品類別start*/
        var kind = Request.QueryString["kind"];
        if (kind != null)
        {
            fslogo.Visible = false;
            //kindpage.InnerText = " > "+kind;
            start_display.Visible = false;
            kind_div.Visible = true;
            SqlCommand cmdSQL3 = new SqlCommand();
            cmdSQL3.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL3;
            //String outside3 = "";//只產生一次
            Label inside3 = new Label();//產生多次


            strSQL3 = "select * from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id where cd_kind='";
            strSQL3 += Request.QueryString["kind"] + "';";
            cmdSQL3.CommandText = strSQL3;
            cnSQL.Open();
            SqlDataReader readerkind = cmdSQL3.ExecuteReader();
            inside3.Text = "";
            while (readerkind.Read())
            {
                
                double pe_discount = 0;
                if (readerkind["pe_discount"].ToString() != "" && readerkind["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(readerkind["pe_discount"].ToString()) * 10;
                }
                inside3.Text += "<div class=\"product-item men\">";
                inside3.Text += "<div class=\"product discount product_filter\">";
                inside3.Text += "<div class=\"product_image\">";
                inside3.Text += "<img src=\"images/clothes/"+ readerkind["cd_id"] + ".jpg\" alt=\"\">";
                inside3.Text += "</div>";
                if ((readerkind["pe_main"].ToString() == "N" || readerkind["pe_main"].ToString() == "Y") && readerkind["pe_status"].ToString() == "Y")
                {
                    inside3.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                inside3.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ readerkind["cd_id"]+"wish"+"\"></div>";
                inside3.Text += "<div class=\"product_info\">";
                inside3.Text += "<h6 class=\"product_name\"><a href=\"?pid="+ readerkind["cd_id"] + "\">"+ readerkind["cd_name"] + "</a></h6>";
                inside3.Text += "<div class=\"product_price\">"+"$"+ readerkind["cd_price"] +"</div>";
                inside3.Text += "</div></div></div>";

                switch (kind)
                {
                    case "TS":
                        li_TS.Attributes.Add("class", "active");
                        i_TS.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > T-Shirt";
                        break;
                    case "D":
                        li_D.Attributes.Add("class", "active");
                        i_D.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 連身裙";
                        break;
                    case "J":
                        li_J.Attributes.Add("class", "active");
                        i_J.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 夾克";
                        break;
                    case "OS":
                        li_OS.Attributes.Add("class", "active");
                        i_OS.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 襯衫";
                        break;
                    case "S":
                        li_S.Attributes.Add("class", "active");
                        i_S.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 毛衣";
                        break;
                    case "OW":
                        li_OW.Attributes.Add("class", "active");
                        i_OW.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 外套";
                        break;
                    case "WC":
                        li_WC.Attributes.Add("class", "active");
                        i_WC.Attributes.Add("class", "fa fa-angle-double-right");
                        kindpage.InnerText = " > 背心";
                        break;

                }


            }
            cnSQL.Close();
            PlaceHolder4.Controls.Add(inside3);
        }
        /*商品類別end*/
        /*KIND-EVENTS start*/
        /*商品類別start*/

        if (kind == "EVENTS")
        {
            fslogo.Visible = false;
            kindpage.InnerText = "> 促銷活動";
            li_EVENTS.Attributes.Add("class", "active");
            i_EVENTS.Attributes.Add("class", "fa fa-angle-double-right");
            start_display.Visible = false;
            kind_div.Visible = true;
            SqlCommand cmdSQL3 = new SqlCommand();
            cmdSQL3.Connection = cnSQL;
            //strSQL 為select form where insert
            String strSQL3;
            //String outside3 = "";//只產生一次
            Label inside3 = new Label();//產生多次


            strSQL3 = "select * from Commodities cd join Promotion_events pe on cd.cd_id = pe.pe_cd_id";            
            cmdSQL3.CommandText = strSQL3;
            cnSQL.Open();
            SqlDataReader readerkind = cmdSQL3.ExecuteReader();
            inside3.Text = "";
            
            while (readerkind.Read())
            {
                if (readerkind["pe_status"].ToString() == "Y")
                {
                    double pe_discount = 0;
                    if (readerkind["pe_discount"].ToString() != "" && readerkind["pe_discount"].ToString() != null)
                    {
                        pe_discount = Convert.ToDouble(readerkind["pe_discount"].ToString()) * 10;
                    }
                    inside3.Text += "<div class=\"product-item men\">";
                    inside3.Text += "<div class=\"product discount product_filter\">";
                    inside3.Text += "<div class=\"product_image\">";
                    inside3.Text += "<img src=\"images/clothes/" + readerkind["cd_id"] + ".jpg\" alt=\"\">";
                    inside3.Text += "</div>";

                    inside3.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                    if (Session["identity"] != null)
                    {
                        inside3.Text += "<div class=\"favorite favorite_left lbh\" id=\"" + readerkind["cd_id"] + "wish" + "\"></div>";
                    }

                    inside3.Text += "<div class=\"product_info\">";
                    inside3.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + readerkind["cd_id"] + "\">" + readerkind["cd_name"] + "</a></h6>";
                    inside3.Text += "<div class=\"product_price\">" + "$" + readerkind["cd_price"] + "</div>";
                    inside3.Text += "</div></div></div>";
                }
                
             
            }
            cnSQL.Close();
            PlaceHolder4.Controls.Add(inside3);
        }

        /*KIND-EVENTS END*/
        



        //TAGS start
        //SqlCommand cmdSQLtags = new SqlCommand();
        //cmdSQL.Connection = cnSQL;
        ////strSQL 為select form where insert
        //String strSQLtags;
        //Label insidetags = new Label();//產生多次


        //strSQLtags = "select * from Tags;";
        //cmdSQL.CommandText = strSQLtags;
        //cnSQL.Open();
        //SqlDataReader readertags = cmdSQL.ExecuteReader();
        //insidetags.Text = "";

        //while (readertags.Read())
        //{
        //    insidetags.Text += "<li><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i><span>"+readertags["tag_name"]+"</span></li>";
           
        //}

        //cnSQL.Close();
        //sql_tags.Controls.Add(insidetags);
        //TAGS end




    }/* <- pageload的尾巴*/







    protected void btn_admlogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session["identity"] = null;
        Session["user"] = null;
        Session["mmid"] = null;
        Session["mmphone"] = null;
        Session["mmadr"] = null;
        Session["notice"] = null;
        Response.Redirect("Default.aspx");
    }

    protected void btn_userlogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session["identity"] = null;
        Session["user"] = null;
        Session["mmid"] = null;
        Session["mmphone"] = null;
        Session["mmadr"] = null;
        Session["notice"] = null;
        Response.Redirect("Default.aspx");
    }

    protected void itemSelected(object sender, EventArgs e)
    {
        string mm_id = Session["mmid"].ToString();
        
        
        string sel_val = DropDownList1.SelectedValue.ToString();
        switch (sel_val)
        {
            case "10":
                lb_seladr.Text = "取貨門市:";
                ckb_takemmadr.Visible = false;
                tb_pickadr.Text = "";
                break;
            case "11":
                lb_seladr.Text = "宅配地址:";
                ckb_takemmadr.Visible = true;
                
                if (ckb_takemmadr.Checked == true && ckb_takemmadr.Visible == true)
                {
                    
                    tb_pickadr.Text = Session["mmadr"].ToString();

                }
                                
                break;
            case "12":
                lb_seladr.Text = "郵寄地址:";
                ckb_takemmadr.Visible = true;

                if (ckb_takemmadr.Checked == true && ckb_takemmadr.Visible == true)
                {

                    tb_pickadr.Text = Session["mmadr"].ToString();

                }

                break;
        }
    }


    protected void produceqr()
    {  /*商品QR*/
        SqlConnection cnSQL = new SqlConnection();
        cnSQL.ConnectionString = strConnString;
        SqlCommand cmdSQL = new SqlCommand();
        cmdSQL.Connection = cnSQL;
        PlaceHolder1.Controls.Clear();
        String strSQL;
        strSQL = "select cd_id from Commodities;";
        cmdSQL.CommandText = strSQL;
        cnSQL.Open();
        SqlDataReader reader = cmdSQL.ExecuteReader();
        while (reader.Read())
        {
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Bitmap bitmap2 = null;
            System.Drawing.Bitmap bitmap3 = null;
            //要轉成QRCode 的內容
            string content = reader["cd_id"].ToString();
            string content2 = "cart"+reader["cd_id"].ToString();
            string content3 = "wish" + reader["cd_id"].ToString();
            //QRCode的設定.
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    //產生出圖片的高度
                    Height = 180,
                    //產生出圖片的寬度
                    Width = 180,
                    //文字是使用哪種編碼方式
                    CharacterSet = "UTF-8",

                    //錯誤修正容量
                    //L水平    7%的字碼可被修正
                    //M水平    15%的字碼可被修正
                    //Q水平    25%的字碼可被修正
                    //H水平    30%的字碼可被修正
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M
                }
            };
            //將要編碼的文字產生出QRCode的圖檔
            bitmap = writer.Write(content);
            bitmap2 = writer.Write(content2);
            bitmap3 = writer.Write(content3);
            //儲存圖片
            bitmap.Save(Request.PhysicalApplicationPath + @"\commodities_img\" + content + ".png", System.Drawing.Imaging.ImageFormat.Png);
            bitmap2.Save(Request.PhysicalApplicationPath + @"\commodities_img\" + content2 + ".png", System.Drawing.Imaging.ImageFormat.Png);
            bitmap3.Save(Request.PhysicalApplicationPath + @"\commodities_img\" + content3 + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
        cnSQL.Close();

        //訂單QR
        SqlCommand cmdSQL1 = new SqlCommand();
        cmdSQL1.Connection = cnSQL;
        String strSQL1;
        strSQL1 = "select od_id from Orders;";
        cmdSQL1.CommandText = strSQL1;
        cnSQL.Open();
        SqlDataReader reader1 = cmdSQL1.ExecuteReader();
        while (reader1.Read())
        {
            System.Drawing.Bitmap bitmap = null;
            //要轉成QRCode 的內容
            string content = reader1["od_id"].ToString();
            //QRCode的設定.
            ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    //產生出圖片的高度
                    Height = 180,
                    //產生出圖片的寬度
                    Width = 180,
                    //文字是使用哪種編碼方式
                    CharacterSet = "UTF-8",

                    //錯誤修正容量
                    //L水平    7%的字碼可被修正
                    //M水平    15%的字碼可被修正
                    //Q水平    25%的字碼可被修正
                    //H水平    30%的字碼可被修正
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M
                }
            };
            //將要編碼的文字產生出QRCode的圖檔
            bitmap = writer.Write(content);
            //儲存圖片
            bitmap.Save(Request.PhysicalApplicationPath + @"\order_img\" + content + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
        cnSQL.Close();
    }



    protected void btn_editpf_Click(object sender, EventArgs e)
    {

        tb_pfname.Text = lb_pfname.Text;
        tb_pfname.Visible = true;
        lb_pfname.Visible = false;
        tb_pfphone.Text = lb_pfphone.Text;
        tb_pfphone.Visible = true;
        lb_pfphone.Visible = false;
        tb_pfmail.Text = lb_pfemail.Text;
        tb_pfmail.Visible = true;
        lb_pfemail.Visible = false;
        tb_pfadr.Text = lb_pfadr.Text;
        tb_pfadr.Visible = true;
        lb_pfadr.Visible = false;
        tb_pfbd.Text = lb_pfbd.Text.Replace("/","-");
        tb_pfbd.Visible = true;
        lb_pfbd.Visible = false;
        ckb_notification.Enabled = true;
        btn_editpf.Visible = false;
        btn_pfsubmit.Visible = true;
        btn_pfback.Visible = true;
    }

    protected void btn_pfback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=profile");
    }

    protected void btn_pfsubmit_Click(object sender, EventArgs e)
    {
        if (tb_pfname.Text == "" || tb_pfname.Text == " " || tb_pfphone.Text == "" || tb_pfphone.Text == " " || tb_pfmail.Text == ""|| tb_pfmail.Text == " "|| tb_pfadr.Text == ""|| tb_pfadr.Text == " "|| tb_pfbd.Text == ""|| tb_pfbd.Text == " ")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('會員資料不完整!');", true);

        }
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand updatepf = new SqlCommand(@"UPDATE Members SET mm_adr=@mm_adr, mm_name=@mm_name,mm_phone=@mm_phone,mm_mail=@mm_mail,
                                            mm_bd=@mm_bd,mm_notice=@mm_notice where mm_id=@mm_id;", cnSQL);
        string notice;
        DateTime dt = Convert.ToDateTime( tb_pfbd.Text);
        
        if (ckb_notification.Checked == true) { notice = "Y"; } else { notice = "N"; }
        updatepf.Parameters.Add(new SqlParameter("@mm_id", Session["mmid"].ToString()));
        updatepf.Parameters.Add(new SqlParameter("@mm_adr", tb_pfadr.Text));
        updatepf.Parameters.Add(new SqlParameter("@mm_name", tb_pfname.Text));
        updatepf.Parameters.Add(new SqlParameter("@mm_phone", tb_pfphone.Text));
        updatepf.Parameters.Add(new SqlParameter("@mm_mail", tb_pfmail.Text));
        updatepf.Parameters.Add(new SqlParameter("@mm_bd", dt.ToShortDateString()));
        updatepf.Parameters.Add(new SqlParameter("@mm_notice", notice));
        updatepf.ExecuteNonQuery();
        updatepf.Dispose();
        cnSQL.Close();
        Response.Redirect("?page=profile");

    }
    protected void btn_rgsubmit_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);

        if (tb_rgact.Text != null && tb_rgact.Text != "" && tb_rgpwd.Text != null && tb_rgpwd.Text != "" && tb_rgpwdck.Text != null && tb_rgpwdck.Text != ""
            && tb_rgname.Text != null && tb_rgname.Text != "" && tb_rgphone.Text != null && tb_rgphone.Text != "" && tb_rgmail.Text != null && tb_rgmail.Text != ""
            && tb_rgadr.Text != null && tb_rgadr.Text != "" && tb_rgbd.Text != null && tb_rgbd.Text != "")
        {
            cnSQL.Open();
            string result_actck;
            SqlCommand ck_mm_act = new SqlCommand(@"select count(*) from Members where mm_act=@mm_act;", cnSQL);
            ck_mm_act.Parameters.Add(new SqlParameter("@mm_act", tb_rgact.Text));
            result_actck = ck_mm_act.ExecuteScalar().ToString();
            if (result_actck == "1") { p_rgerror.InnerText = "帳號重複!"; p_rgerror.Visible = true; cnSQL.Close(); }
            else
            {
                if (tb_rgpwd.Text == tb_rgpwdck.Text)
                {
                    string rg_notice;
                    if (ckb_rgnotice.Checked == true) { rg_notice = "Y"; } else { rg_notice = "N"; }
                    SqlCommand newuser = new SqlCommand(@"Insert Into Members(mm_id,mm_who,mm_act,mm_pwd,mm_adr,mm_name,mm_phone,mm_mail,mm_bd,mm_notice) 
                                                          Values(@mm_id,@mm_who,@mm_act,@mm_pwd,@mm_adr,@mm_name,@mm_phone,@mm_mail,@mm_bd,@mm_notice);", cnSQL);
                    newuser.Parameters.Add(new SqlParameter("@mm_id", " "));
                    newuser.Parameters.Add(new SqlParameter("@mm_who", "0"));
                    newuser.Parameters.Add(new SqlParameter("@mm_act", tb_rgact.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_pwd", tb_rgpwd.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_adr", tb_rgadr.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_name", tb_rgname.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_phone", tb_rgphone.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_mail", tb_rgmail.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_bd", tb_rgbd.Text));
                    newuser.Parameters.Add(new SqlParameter("@mm_notice", rg_notice));
                    newuser.ExecuteNonQuery();
                    newuser.Dispose();
                    cnSQL.Close();
                    Response.Redirect("Default.aspx");
                }
                else { p_rgerror.InnerText = "密碼與密碼確認不符!"; p_rgerror.Visible = true; cnSQL.Close(); }
            }

        }

        else { p_rgerror.InnerText = "有欄位沒填!"; p_rgerror.Visible = true; }

    }
    protected void btn_changepw_Click(object sender, EventArgs e)
    {
        pf_maindiv.Visible = false;
        btn_editpf.Visible = false;
        pf_pwdchgdiv.Visible = true;
        btn_pfpwdsubmit.Visible = true;
        btn_pfback.Visible = true;
    }

    protected void btn_rgback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void GV_RowCommand_mm(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_Member.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string mm_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "USER_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?mm_no=" + mm_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "USER_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteuser = new SqlCommand(@"DELETE FROM Members where mm_no=@mm_no;", cnSQL);
            deleteuser.Parameters.Add(new SqlParameter("@mm_no", mm_no));
            deleteuser.ExecuteNonQuery();
            deleteuser.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admmm");


        }

    }

    protected void SqlDataSourceMM_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_Member.Attributes.Add("bordercolor", "#000000");
    }

    protected void GV_RowCommand_cd(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_cd.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string cd_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "CD_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?cd_no=" + cd_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "CD_DELETE")
        {
            try
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_no=@cd_no;", cnSQL);
                deletecd.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                deletecd.ExecuteNonQuery();
                deletecd.Dispose();

                SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_no=@cd_no;", cnSQL);
                find_cd_id.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                string cd_id = find_cd_id.ExecuteScalar().ToString();

                cnSQL.Close();

                string delfileName = cd_id + ".jpg";
                string deleteFolder = Request.PhysicalApplicationPath + "images\\clothes\\";

                try
                {
                    File.Delete(deleteFolder + delfileName);
                }
                catch
                {

                }
                Response.Redirect("?page=admcd");
            }
            catch
            {

            }
            


        }

    }

    protected void SqlDataSourceCD_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_cd.Attributes.Add("bordercolor", "#000000");
    }
    protected void GV_RowCommand_ody(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_ody.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string od_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "OD_DETAIL")
        {
            //導去明細頁
            Response.Redirect("?od_no=" + od_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "OD_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteod = new SqlCommand(@"DELETE FROM Orders where od_no=@od_no;", cnSQL);
            deleteod.Parameters.Add(new SqlParameter("@od_no", od_no));
            deleteod.ExecuteNonQuery();
            deleteod.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admod");
        }

    }

    protected void GV_RowCommand_odn(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_odn.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string od_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "OD_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?od_no=" + od_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "OD_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteod = new SqlCommand(@"DELETE FROM Orders where od_no=@od_no;", cnSQL);
            deleteod.Parameters.Add(new SqlParameter("@od_no", od_no));
            deleteod.ExecuteNonQuery();
            deleteod.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admod");
        }

    }
    protected void SqlDataSourceODn_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_odn.Attributes.Add("bordercolor", "#000000");
    }
    protected void SqlDataSourceODy_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_ody.Attributes.Add("bordercolor", "#000000");
    }

    protected void GV_RowCommand_oddetail(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_oddetail.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string od_no = Request.QueryString["od_no"].ToString();
        Label lb = row.FindControl("Label1") as Label;
        string odd_cd_id = lb.Text;

        if (e.CommandName.ToString().ToUpper() == "DLL_ITEM_DELETE")
        {

            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();


            SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
            find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
            string od_id = find_od_id.ExecuteScalar().ToString();


            SqlCommand deletedllitem = new SqlCommand(@"DELETE FROM Order_detail where odd_od_id=@odd_od_id and odd_cd_id=@odd_cd_id;", cnSQL);

            deletedllitem.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            deletedllitem.Parameters.Add(new SqlParameter("@odd_cd_id", odd_cd_id));

            deletedllitem.ExecuteNonQuery();
            deletedllitem.Dispose();


            cnSQL.Close();
            check_od(od_id);
            Response.Redirect("?od_no=" + od_no);


        }

    }

    protected void SqlDataSourceODDETAIL_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_oddetail.Attributes.Add("bordercolor", "#000000");
    }
    /**/
    protected void GV_oddetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_oddetail.PageIndex = e.NewPageIndex;

        //Bind data to the GridView control.

    }

    protected void GV_oddetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
         
        
        //Set the edit index.
        GV_oddetail.EditIndex = e.NewEditIndex;

        


        //Bind data to the GridView control.

    }

    protected void GV_oddetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        GV_oddetail.EditIndex = -1;
        //Bind data to the GridView control.

    }

    protected void GV_oddetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
            
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string od_no = Request.QueryString["od_no"].ToString();
        GridViewRow row = GV_oddetail.Rows[e.RowIndex];
        string cd_id = e.NewValues[1].ToString();
        
        
        try
        {
            
            int s = Convert.ToInt32(e.NewValues[2].ToString());
            int m = Convert.ToInt32(e.NewValues[3].ToString());
            int l = Convert.ToInt32(e.NewValues[4].ToString());
            int xl = Convert.ToInt32(e.NewValues[5].ToString());
       
            SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
            find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
            string od_id = find_od_id.ExecuteScalar().ToString();

            SqlCommand update_odd = new SqlCommand(@"UPDATE Order_detail SET odd_s_amount=@odd_s_amount, odd_m_amount=@odd_m_amount,odd_l_amount=@odd_l_amount,odd_xl_amount=@odd_xl_amount
                                            where odd_od_id=@odd_od_id and odd_cd_id=@odd_cd_id;;", cnSQL);
            update_odd.Parameters.Add(new SqlParameter("@odd_s_amount", s));
            update_odd.Parameters.Add(new SqlParameter("@odd_m_amount", m));
            update_odd.Parameters.Add(new SqlParameter("@odd_l_amount", l));
            update_odd.Parameters.Add(new SqlParameter("@odd_xl_amount", xl));
            update_odd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            update_odd.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
            update_odd.ExecuteNonQuery();
            update_odd.Dispose();
            cnSQL.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('成功!');", true);
            check_od(od_id);
            Response.Redirect("?od_no=" + od_no);


            //GV_oddetail.EditIndex = -1;       


        }
        catch (Exception ex)
        {
            e.Cancel = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('==');", true);

           
        }
        
        
        




        //Response.Redirect("?mm_no=" + ss);






        //GV_oddetail.EditIndex = -1;



    }
    protected void GV_oddetail_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if (e.Exception != null) { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('成功！');", true); }
        else { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('有甚麼地方似乎出錯了！');", true); }

        
        //string s = ((TextBox)(row.Cells[5].Controls[0])).Text;
        //string m = ((TextBox)(row.Cells[6].Controls[0])).Text;
        //string l = ((TextBox)(row.Cells[7].Controls[0])).Text;
        //string xl = ((TextBox)(row.Cells[8].Controls[0])).Text;
    }

    /**/
    protected void btn_adm_mmedit_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('第五種方式！');", true);
        lb_adm_mmname.Visible = false;
        tb_adm_mmname.Visible = true;

        lb_adm_mmadr.Visible = false;
        tb_adm_mmadr.Visible = true;

        lb_adm_mmphone.Visible = false;
        tb_adm_mmphone.Visible = true;

        lb_adm_mmmail.Visible = false;
        tb_adm_mmmail.Visible = true;

        lb_adm_mmbd.Visible = false;
        tb_adm_mmbd.Visible = true;

        lb_adm_mmnotice.Visible = false;
        tb_adm_mmnotice.Visible = true;

        btn_adm_mmedit.Visible = false;
        btn_adm_mmsubmit.Visible = true;
        btn_adm_mmback.Visible = true;

        tb_adm_mmname.Text = lb_adm_mmname.Text;
        tb_adm_mmadr.Text = lb_adm_mmadr.Text;
        tb_adm_mmphone.Text = lb_adm_mmphone.Text;
        tb_adm_mmmail.Text = lb_adm_mmmail.Text;
        tb_adm_mmbd.Text = lb_adm_mmbd.Text.Replace("/", "-");
        tb_adm_mmnotice.Text = lb_adm_mmnotice.Text;


    }

    protected void btn_adm_mmback_Click(object sender, EventArgs e)
    {
        string mm_no = Request.QueryString["mm_no"].ToString();
        Response.Redirect("?mm_no=" + mm_no);
    }

    protected void btn_adm_mmsubmit_Click(object sender, EventArgs e)
    {
        if ((tb_adm_mmname.Text=="" || tb_adm_mmname.Text == " ")|| (tb_adm_mmadr.Text == "" || tb_adm_mmadr.Text == " ") ||(tb_adm_mmphone.Text == "" || tb_adm_mmphone.Text == " ") || (tb_adm_mmmail.Text == "" || tb_adm_mmmail.Text == " ") || (tb_adm_mmbd.Text == "" || tb_adm_mmbd.Text == " ") )
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('有欄位空白!');", true);
        }
        else
        {
            if (tb_adm_mmphone.Text.Contains("e") || tb_adm_mmphone.Text.Contains("E") || tb_adm_mmphone.Text.Contains("+") || tb_adm_mmphone.Text.Length > 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('電話格式錯誤!(含符號 或 長度超過 10)');", true);
                return;
            }
            else
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                SqlCommand updatemm = new SqlCommand(@"UPDATE Members SET mm_adr=@mm_adr, mm_name=@mm_name,mm_phone=@mm_phone,mm_mail=@mm_mail,
                                            mm_bd=@mm_bd,mm_notice=@mm_notice where mm_no=@mm_no;", cnSQL);
                string mm_no = Request.QueryString["mm_no"].ToString();
                DateTime dt = Convert.ToDateTime(tb_adm_mmbd.Text);


                updatemm.Parameters.Add(new SqlParameter("@mm_no", mm_no));
                updatemm.Parameters.Add(new SqlParameter("@mm_adr", tb_adm_mmadr.Text));
                updatemm.Parameters.Add(new SqlParameter("@mm_name", tb_adm_mmname.Text));
                updatemm.Parameters.Add(new SqlParameter("@mm_phone", tb_adm_mmphone.Text));
                updatemm.Parameters.Add(new SqlParameter("@mm_mail", tb_adm_mmmail.Text));
                updatemm.Parameters.Add(new SqlParameter("@mm_bd", dt.ToShortDateString()));
                updatemm.Parameters.Add(new SqlParameter("@mm_notice", tb_adm_mmnotice.Text));
                updatemm.ExecuteNonQuery();
                updatemm.Dispose();
                cnSQL.Close();
                Response.Redirect("?mm_no=" + mm_no);
            }
            
        }
        
    }

    protected void btn_resetpw_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand resetpw = new SqlCommand(@"UPDATE Members SET mm_pwd=@mm_pwd where mm_no=@mm_no;", cnSQL);
        string mm_no = Request.QueryString["mm_no"].ToString();


        resetpw.Parameters.Add(new SqlParameter("@mm_no", mm_no));
        resetpw.Parameters.Add(new SqlParameter("@mm_pwd", "123456"));
        resetpw.ExecuteNonQuery();
        resetpw.Dispose();
        cnSQL.Close();
        Response.Redirect("?mm_no=" + mm_no);
    }
    protected void btn_adm_mmdetailback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admmm");
    }
    protected void btn_adm_newadm_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=newadmm");
    }
    protected void btn_adm_newuser_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=newuser");
    }

    protected void btn_addadm_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);

        if (tb_adm_act.Text != null && tb_adm_act.Text != "" && tb_adm_pw.Text != null && tb_adm_pw.Text != "" && tb_adm_pwck.Text != null && tb_adm_pwck.Text != ""
            && tb_adm_name.Text != null && tb_adm_name.Text != "")
        {
            cnSQL.Open();
            string result_actck;
            SqlCommand ck_mm_act = new SqlCommand(@"select count(*) from Members where mm_act=@mm_act;", cnSQL);
            ck_mm_act.Parameters.Add(new SqlParameter("@mm_act", tb_adm_act.Text));
            result_actck = ck_mm_act.ExecuteScalar().ToString();
            if (result_actck == "1") { p_addadmerror.InnerText = "帳號重複!"; p_addadmerror.Visible = true; cnSQL.Close(); }
            else
            {
                if (tb_adm_pw.Text == tb_adm_pwck.Text)
                {

                    SqlCommand newadm = new SqlCommand(@"Insert Into Members(mm_id,mm_who,mm_act,mm_pwd,mm_adr,mm_name,mm_phone,mm_mail,mm_bd,mm_notice) 
                                                          Values(@mm_id,@mm_who,@mm_act,@mm_pwd,@mm_adr,@mm_name,@mm_phone,@mm_mail,@mm_bd,@mm_notice);", cnSQL);
                    newadm.Parameters.Add(new SqlParameter("@mm_id", " "));
                    newadm.Parameters.Add(new SqlParameter("@mm_who", "1"));
                    newadm.Parameters.Add(new SqlParameter("@mm_act", tb_adm_act.Text));
                    newadm.Parameters.Add(new SqlParameter("@mm_pwd", tb_adm_pw.Text));
                    newadm.Parameters.Add(new SqlParameter("@mm_adr", DBNull.Value));
                    newadm.Parameters.Add(new SqlParameter("@mm_name", tb_adm_name.Text));
                    newadm.Parameters.Add(new SqlParameter("@mm_phone", DBNull.Value));
                    newadm.Parameters.Add(new SqlParameter("@mm_mail", DBNull.Value));
                    newadm.Parameters.Add(new SqlParameter("@mm_bd", DBNull.Value));
                    newadm.Parameters.Add(new SqlParameter("@mm_notice", DBNull.Value));
                    newadm.ExecuteNonQuery();
                    newadm.Dispose();
                    cnSQL.Close();
                    Response.Redirect("?page=admmm");
                }
                else { p_addadmerror.InnerText = "密碼與密碼確認不符!"; p_addadmerror.Visible = true; cnSQL.Close(); }
            }

        }

        else { p_addadmerror.InnerText = "有欄位沒填!"; p_addadmerror.Visible = true; }

    }
    protected void btn_addadmback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admmm");
    }

    protected void btn_adduser_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);

        if (tb_addu_act.Text != null && tb_addu_act.Text != "" && tb_addu_pw.Text != null && tb_addu_pw.Text != "" && tb_addu_pwck.Text != null && tb_addu_pwck.Text != ""
            && tb_addu_name.Text != null && tb_addu_name.Text != "" && tb_addu_phone.Text != null && tb_addu_phone.Text != "" && tb_addu_mail.Text != null && tb_addu_mail.Text != ""
            && tb_addu_adr.Text != null && tb_addu_adr.Text != "" && tb_addu_bd.Text != null && tb_addu_bd.Text != "")
        {
            cnSQL.Open();
            string result_actck;
            SqlCommand ck_mm_act = new SqlCommand(@"select count(*) from Members where mm_act=@mm_act;", cnSQL);
            ck_mm_act.Parameters.Add(new SqlParameter("@mm_act", tb_addu_act.Text));
            result_actck = ck_mm_act.ExecuteScalar().ToString();
            if (result_actck == "1") { p_addusererror.InnerText = "帳號重複!"; p_addusererror.Visible = true; cnSQL.Close(); }
            else
            {
                if (tb_addu_pw.Text == tb_addu_pwck.Text)
                {
                    string addu_notice;
                    if (ckb_addu_notice.Checked == true) { addu_notice = "Y"; } else { addu_notice = "N"; }
                    SqlCommand adduser = new SqlCommand(@"Insert Into Members(mm_id,mm_who,mm_act,mm_pwd,mm_adr,mm_name,mm_phone,mm_mail,mm_bd,mm_notice) 
                                                          Values(@mm_id,@mm_who,@mm_act,@mm_pwd,@mm_adr,@mm_name,@mm_phone,@mm_mail,@mm_bd,@mm_notice);", cnSQL);
                    adduser.Parameters.Add(new SqlParameter("@mm_id", " "));
                    adduser.Parameters.Add(new SqlParameter("@mm_who", "0"));
                    adduser.Parameters.Add(new SqlParameter("@mm_act", tb_addu_act.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_pwd", tb_addu_pw.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_adr", tb_addu_adr.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_name", tb_addu_name.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_phone", tb_addu_phone.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_mail", tb_addu_mail.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_bd", tb_addu_bd.Text));
                    adduser.Parameters.Add(new SqlParameter("@mm_notice", addu_notice));
                    adduser.ExecuteNonQuery();
                    adduser.Dispose();
                    cnSQL.Close();
                    Response.Redirect("?page=admmm");
                }
                else { p_addusererror.InnerText = "密碼與密碼確認不符!"; p_addusererror.Visible = true; cnSQL.Close(); }
            }

        }

        else { p_addusererror.InnerText = "有欄位沒填!"; p_addusererror.Visible = true; }

    }

    protected void btn_adduserback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admmm");
    }

    private string ConvertColor(string ColorHexa)  /*顏色名稱轉換*/
    {
        Color color = ColorTranslator.FromHtml(ColorHexa);
        string colorName = "";
        PropertyInfo[] pinfos = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);

        foreach (PropertyInfo pinfo in pinfos)
        {
            Color namedColor = (Color)pinfo.GetValue(null, null);
            if (namedColor.ToArgb() == color.ToArgb())
            {
                colorName = namedColor.Name;
                break;
            }
        }

        return colorName;
    }

    protected void btn_adm_cdedit_Click(object sender, EventArgs e)
    {
        lb_adm_cdname.Visible = false;
        tb_adm_cdname.Visible = true;

        lb_adm_cdgroup.Visible = false;
        tb_adm_cdgroup.Visible = true;

        div_adm_cdcolor.Visible = false;
        colorSelector.Visible = true;

        lb_adm_cdkind.Visible = false;
        tb_adm_cdkind.Visible = true;

        lb_adm_cdmaterial.Visible = false;
        tb_adm_cdmaterial.Visible = true;

        lb_adm_cdprice.Visible = false;
        tb_adm_cdprice.Visible = true;

        lb_adm_cdmadein.Visible = false;
        tb_adm_cdmadein.Visible = true;

        lb_adm_cdplace.Visible = false;
        tb_adm_cdplace.Visible = true;

        lb_adm_cdtag.Visible = false;
        ckblist_adm_cd_tag.Visible = true;

        ckb_adm_s.Enabled = true;
        ckb_adm_m.Enabled = true;
        ckb_adm_l.Enabled = true;
        ckb_adm_xl.Enabled = true;

        div_adm_editcdpic.Visible = true;

        btn_adm_cdedit.Visible = false;
        btn_adm_cdsubmit.Visible = true;
        btn_adm_cdback.Visible = true;

        string colorcode = div_adm_cdcolor.Style.Value.Replace("width:30px;height:25px;background-color:", "");
        string strstyle = "width:30px;height:25px;background-color:" + colorcode;
        tb_adm_cdname.Text = lb_adm_cdname.Text;
        tb_adm_cdgroup.Text = lb_adm_cdgroup.Text;
        div_adm_colorpicker.Attributes.Add("style", strstyle);
        tb_adm_cdkind.Text = lb_adm_cdkind.Text;
        tb_adm_cdmaterial.Text = lb_adm_cdmaterial.Text;
        tb_adm_cdprice.Text = lb_adm_cdprice.Text;
        tb_adm_cdmadein.Text = lb_adm_cdmadein.Text;
        tb_adm_cdplace.Text = lb_adm_cdplace.Text;

    }

    protected void btn_adm_cdback_Click(object sender, EventArgs e)
    {
        string cd_no = Request.QueryString["cd_no"].ToString();
        Response.Redirect("?cd_no=" + cd_no);
    }

    protected void btn_adm_cdsubmit_Click(object sender, EventArgs e)
    {
        if ((tb_adm_cdname.Text=="" || tb_adm_cdname.Text==" ") || (tb_adm_cdgroup.Text == "" || tb_adm_cdgroup.Text == " ")  || (ckb_adm_s.Checked==false&& ckb_adm_m.Checked == false && ckb_adm_l.Checked == false && ckb_adm_xl.Checked == false) || (tb_adm_cdkind.Text == "" || tb_adm_cdkind.Text == " ") || (tb_adm_cdmaterial.Text == "" || tb_adm_cdmaterial.Text == " ") || (tb_adm_cdprice.Text == "" || tb_adm_cdprice.Text == " ") || (tb_adm_cdmadein.Text == "" || tb_adm_cdmadein.Text == " ") || (tb_adm_cdplace.Text == "" || tb_adm_cdplace.Text == " ") )
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('有欄位沒填！');", true);return;
        }
        else
        {
            if (lb_adm_cdname.Text == tb_adm_cdname.Text)
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                SqlCommand updatecd = new SqlCommand(@"UPDATE Commodities SET cd_name=@cd_name, cd_group=@cd_group,cd_color=@cd_color,cd_kind=@cd_kind,
                                            cd_material=@cd_material,cd_price=@cd_price ,cd_madein=@cd_madein,cd_place=@cd_place where cd_no=@cd_no;", cnSQL);
                string cd_no = Request.QueryString["cd_no"].ToString();

                SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_no=@cd_no;", cnSQL);
                find_cd_id.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                string cd_id = find_cd_id.ExecuteScalar().ToString();

                SqlCommand updatesz = new SqlCommand(@"UPDATE Sizes SET sz_s=@sz_s, sz_m=@sz_m,sz_l=@sz_l,sz_xl=@sz_xl where sz_cd_id=@sz_cd_id;", cnSQL);
                string temp_colorhex = Request.Params["temp_colorhex"];
                if (temp_colorhex==" " || temp_colorhex == "") { temp_colorhex = lb_colorcode.Text.Replace("#", ""); } 


                updatecd.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                updatecd.Parameters.Add(new SqlParameter("@cd_name", tb_adm_cdname.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_group", tb_adm_cdgroup.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_color", temp_colorhex));/*顏色改不到*/
                updatecd.Parameters.Add(new SqlParameter("@cd_kind", tb_adm_cdkind.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_material", tb_adm_cdmaterial.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_price", tb_adm_cdprice.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_madein", tb_adm_cdmadein.Text));
                updatecd.Parameters.Add(new SqlParameter("@cd_place", tb_adm_cdplace.Text));
                updatecd.ExecuteNonQuery();
                updatecd.Dispose();

                string sz_s, sz_m, sz_l, sz_xl;
                if (ckb_adm_s.Checked == true) { sz_s = "Y"; } else { sz_s = "N"; }
                if (ckb_adm_m.Checked == true) { sz_m = "Y"; } else { sz_m = "N"; }
                if (ckb_adm_l.Checked == true) { sz_l = "Y"; } else { sz_l = "N"; }
                if (ckb_adm_xl.Checked == true) { sz_xl = "Y"; } else { sz_xl = "N"; }

                updatesz.Parameters.Add(new SqlParameter("@sz_s", sz_s));
                updatesz.Parameters.Add(new SqlParameter("@sz_m", sz_m));
                updatesz.Parameters.Add(new SqlParameter("@sz_l", sz_l));
                updatesz.Parameters.Add(new SqlParameter("@sz_xl", sz_xl));
                updatesz.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));
                updatesz.ExecuteNonQuery();
                updatesz.Dispose();
               
                cnSQL.Close();

                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                string uploadFolder = Request.PhysicalApplicationPath + "images\\clothes\\";
                string deleteFolder = uploadFolder;
                string delfileName = cd_id + ".jpg";
                if (FU_cdpic.HasFile)
                {
                    string extension = Path.GetExtension(FU_cdpic.PostedFile.FileName);


                    if (allowedExtextsion.IndexOf(extension) == -1)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入商品!');", true);
                        return;
                    }
                    try
                    {
                        
                        File.Delete(deleteFolder + delfileName);
                        FU_cdpic.SaveAs(uploadFolder + cd_id + extension);

                    }
                    catch
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入商品!');", true);
                        return;
                    }

                }
                DateTime dt = DateTime.Now;
                cnSQL.Open();

                foreach (ListItem listitem in ckblist_adm_cd_tag.Items)
                {
                    if (listitem.Selected)
                    {
                        SqlCommand ck_have = new SqlCommand(@"select count(*) from Have where have_cd_id=@have_cd_id and have_tag_id=@have_tag_id;", cnSQL);
                        ck_have.Parameters.Add(new SqlParameter("@have_cd_id", cd_id));
                        ck_have.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                        int result_ckhave = Convert.ToInt32(ck_have.ExecuteScalar().ToString());

                        if (result_ckhave == 0)
                        {
                            SqlCommand newhave = new SqlCommand(@"Insert Into Have(have_tag_id,have_cd_id,have_date) 
                                                          Values(@have_tag_id,@have_cd_id,@have_date) ;", cnSQL);
                            newhave.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                            newhave.Parameters.Add(new SqlParameter("@have_cd_id", cd_id));
                            newhave.Parameters.Add(new SqlParameter("@have_date", dt.ToShortDateString()));
                            newhave.ExecuteNonQuery();
                            newhave.Dispose();
                        }


                    }
                }
                cnSQL.Close();
                cnSQL.Dispose();

                Response.Redirect("?cd_no=" + cd_no);
            }
            else
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                SqlCommand check_cd_name = new SqlCommand(@"select count(*) from Commodities where cd_name=@cd_name;", cnSQL);
                check_cd_name.Parameters.Add(new SqlParameter("@cd_name", tb_adm_cdname.Text));
                string result_ck_cdname= check_cd_name.ExecuteScalar().ToString();
                if (result_ck_cdname == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品名稱重複！');", true); return;
                }
                else
                {
                    SqlCommand updatecd = new SqlCommand(@"UPDATE Commodities SET cd_name=@cd_name, cd_group=@cd_group,cd_color=@cd_color,cd_kind=@cd_kind,
                                            cd_material=@cd_material,cd_price=@cd_price ,cd_madein=@cd_madein,cd_place=@cd_place where cd_no=@cd_no;", cnSQL);
                    string cd_no = Request.QueryString["cd_no"].ToString();

                    SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_no=@cd_no;", cnSQL);
                    find_cd_id.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                    string cd_id = find_cd_id.ExecuteScalar().ToString();

                    SqlCommand updatesz = new SqlCommand(@"UPDATE Sizes SET sz_s=@sz_s, sz_m=@sz_m,sz_l=@sz_l,sz_xl=@sz_xl where sz_cd_id=@sz_cd_id;", cnSQL);
                    string temp_colorhex = Request.Params["temp_colorhex"];
                    if (temp_colorhex == " " || temp_colorhex == "") { temp_colorhex = lb_colorcode.Text.Replace("#", ""); }

                    updatecd.Parameters.Add(new SqlParameter("@cd_no", cd_no));
                    updatecd.Parameters.Add(new SqlParameter("@cd_name", tb_adm_cdname.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_group", tb_adm_cdgroup.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_color", temp_colorhex));/*顏色改不到*/
                    updatecd.Parameters.Add(new SqlParameter("@cd_kind", tb_adm_cdkind.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_material", tb_adm_cdmaterial.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_price", tb_adm_cdprice.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_madein", tb_adm_cdmadein.Text));
                    updatecd.Parameters.Add(new SqlParameter("@cd_place", tb_adm_cdplace.Text));
                    updatecd.ExecuteNonQuery();
                    updatecd.Dispose();

                    string sz_s, sz_m, sz_l, sz_xl;
                    if (ckb_adm_s.Checked == true) { sz_s = "Y"; } else { sz_s = "N"; }
                    if (ckb_adm_m.Checked == true) { sz_m = "Y"; } else { sz_m = "N"; }
                    if (ckb_adm_l.Checked == true) { sz_l = "Y"; } else { sz_l = "N"; }
                    if (ckb_adm_xl.Checked == true) { sz_xl = "Y"; } else { sz_xl = "N"; }

                    updatesz.Parameters.Add(new SqlParameter("@sz_s", sz_s));
                    updatesz.Parameters.Add(new SqlParameter("@sz_m", sz_m));
                    updatesz.Parameters.Add(new SqlParameter("@sz_l", sz_l));
                    updatesz.Parameters.Add(new SqlParameter("@sz_xl", sz_xl));
                    updatesz.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));
                    updatesz.ExecuteNonQuery();
                    updatesz.Dispose();

                    cnSQL.Close();

                    List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                    string uploadFolder = Request.PhysicalApplicationPath + "images\\clothes\\";
                    if (FU_cdpic.HasFile)
                    {
                        string extension = Path.GetExtension(FU_cdpic.PostedFile.FileName);


                        if (allowedExtextsion.IndexOf(extension) == -1)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入商品!');", true);
                            return;
                        }
                        try
                        {
                            FU_cdpic.SaveAs(uploadFolder + cd_id + extension);

                        }
                        catch
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入商品!');", true);
                            return;
                        }

                    }
                    DateTime dt = DateTime.Now;
                    cnSQL.Open();
                    foreach (ListItem listitem in ckblist_adm_cd_tag.Items)
                    {
                        if (listitem.Selected)
                        {
                            SqlCommand ck_have = new SqlCommand(@"select count(*) from Have where have_cd_id=@have_cd_id and have_tag_id=@have_tag_id;", cnSQL);
                            ck_have.Parameters.Add(new SqlParameter("@have_cd_id", cd_id));
                            ck_have.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                            int result_ckhave = Convert.ToInt32(ck_have.ExecuteScalar().ToString());

                            if (result_ckhave == 0)
                            {
                                SqlCommand newhave = new SqlCommand(@"Insert Into Have(have_tag_id,have_cd_id,have_date) 
                                                          Values(@have_tag_id,@have_cd_id,@have_date) ;", cnSQL);
                                newhave.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                                newhave.Parameters.Add(new SqlParameter("@have_cd_id", cd_id));
                                newhave.Parameters.Add(new SqlParameter("@have_date", dt.ToShortDateString()));
                                newhave.ExecuteNonQuery();
                                newhave.Dispose();
                            }


                        }
                    }
                    cnSQL.Close();
                    cnSQL.Dispose();

                    Response.Redirect("?cd_no=" + cd_no);
                }
                    
            }
        }
        
    }
    protected void btn_adm_cddetailback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admcd");
    }
    protected void btn_adm_oddetailback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admod");
    }
    protected void btn_adm_oddetailadditem_Click(object sender, EventArgs e)
    {
        string od_no = Request.QueryString["od_no"].ToString();
        Response.Redirect("?od_no="+od_no+"&page=adm_odd_additem");
    }
    protected void check_od(string od_id)
    {

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();

        SqlCommand find_odd_num = new SqlCommand(@"select count(*) from Order_detail where odd_od_id=@odd_od_id;", cnSQL);
        find_odd_num.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
        int odd_count = Convert.ToInt32(find_odd_num.ExecuteScalar().ToString());
        cnSQL.Close();

        if (odd_count > 0)
        {
            
            int total = 0;
            cnSQL.Open();
            SqlCommand recal_total = new SqlCommand(@"select * from Order_detail where odd_od_id=@odd_od_id;", cnSQL);
            recal_total.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            SqlDataReader recaltotal_reader = recal_total.ExecuteReader();
            while (recaltotal_reader.Read())
            {
                SqlConnection cnSQL2 = new SqlConnection(strConnString);
                cnSQL2.Open();

                SqlCommand find_ispe = new SqlCommand(@"select count(*) from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                find_ispe.Parameters.Add(new SqlParameter("@pe_cd_id", recaltotal_reader["odd_cd_id"].ToString()));

                int pe_count = Convert.ToInt32(find_ispe.ExecuteScalar().ToString());
                if (pe_count > 0)
                {
                    SqlCommand find_discount = new SqlCommand(@"select pe_discount from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                    find_discount.Parameters.Add(new SqlParameter("@pe_cd_id", recaltotal_reader["odd_cd_id"].ToString()));
                    Double discount = Convert.ToDouble(find_discount.ExecuteScalar().ToString());

                    SqlCommand find_price = new SqlCommand(@"select cd_price from Commodities where cd_id=@cd_id;", cnSQL2);
                    find_price.Parameters.Add(new SqlParameter("@cd_id", recaltotal_reader["odd_cd_id"].ToString()));
                    int price = Convert.ToInt32(find_price.ExecuteScalar().ToString());

                    int s_amount = Convert.ToInt32(recaltotal_reader["odd_s_amount"]);
                    int m_amount = Convert.ToInt32(recaltotal_reader["odd_m_amount"]);
                    int l_amount = Convert.ToInt32(recaltotal_reader["odd_l_amount"]);
                    int xl_amount = Convert.ToInt32(recaltotal_reader["odd_xl_amount"]);
                    total += Convert.ToInt32((s_amount + m_amount + l_amount + xl_amount) * price * discount);

                }
                else
                {

                    SqlCommand find_price = new SqlCommand(@"select cd_price from Commodities where cd_id=@cd_id;", cnSQL2);
                    find_price.Parameters.Add(new SqlParameter("@cd_id", recaltotal_reader["odd_cd_id"].ToString()));
                    int price = Convert.ToInt32(find_price.ExecuteScalar().ToString());

                    int s_amount = Convert.ToInt32(recaltotal_reader["odd_s_amount"]);
                    int m_amount = Convert.ToInt32(recaltotal_reader["odd_m_amount"]);
                    int l_amount = Convert.ToInt32(recaltotal_reader["odd_l_amount"]);
                    int xl_amount = Convert.ToInt32(recaltotal_reader["odd_xl_amount"]);
                    total += (s_amount + m_amount + l_amount + xl_amount) * price;

                    
                }

                cnSQL2.Close();

                
            }
            recaltotal_reader.Close();
            SqlCommand updateod_total = new SqlCommand(@"UPDATE Orders SET od_total=@od_total where od_id=@od_id;", cnSQL);
            updateod_total.Parameters.Add(new SqlParameter("@od_id", od_id));
            updateod_total.Parameters.Add(new SqlParameter("@od_total", total));
            updateod_total.ExecuteNonQuery();
            updateod_total.Dispose();
            cnSQL.Close();
            

            }
        else
        {
            cnSQL.Open();
            SqlCommand deleteod = new SqlCommand(@"DELETE FROM Orders where od_id=@od_id ;", cnSQL);

            deleteod.Parameters.Add(new SqlParameter("@od_id", od_id));
            deleteod.ExecuteNonQuery();
            deleteod.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admod");
        }

    }
    

    protected void btn_adm_newtag_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=newtag");
    }
    protected void btn_addtag_Click(object sender, EventArgs e)
    {
        if (lb_check.Text== "此TAG可使用")
        {
            DateTime dt = DateTime.Now;

            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand newtag = new SqlCommand(@"Insert Into Tags(tag_id,tag_name,tag_date)
                                                         Values(@tag_id,@tag_name,@tag_date);", cnSQL);

            newtag.Parameters.Add(new SqlParameter("@tag_id", DBNull.Value));
            newtag.Parameters.Add(new SqlParameter("@tag_name", tb_tag.Text));
            newtag.Parameters.Add(new SqlParameter("@tag_date", dt.ToShortDateString()));
            newtag.ExecuteNonQuery();
            newtag.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=newtag");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('請先檢查TAG名稱！');", true);
        }
        
    }


    protected void btn_adm_tagback_Click(object sender, EventArgs e)
    {
        string tag_no = Request.QueryString["tag_no"].ToString();
        Response.Redirect("?tag_no=" + tag_no);
        //Response.Redirect("?page=admtag");
    }
    protected void btn_adm_tagsubmit_Click(object sender, EventArgs e)
    {
        string tag_no = Request.QueryString["tag_no"].ToString();
        if (lb_tag_name.Text == tb_tag_name.Text)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未更改TAG名稱');", true);
        }
        else
        {
            if (tb_tag_name.Text == "" || tb_tag_name.Text == " ") { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('TAG名稱不可空白');", true); }
            else
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                string result_tagck;
                SqlCommand tagcheck = new SqlCommand(@"SELECT count(*) FROM Tags where tag_name=@tag_name;", cnSQL);
                tagcheck.Parameters.Add(new SqlParameter("@tag_name", tb_tag_name.Text));
                result_tagck = tagcheck.ExecuteScalar().ToString();
                lb_check.Visible = true;
                if (result_tagck == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('TAG名稱重複');", true);
                    cnSQL.Close();
                    return;

                }
                else
                {
                    SqlCommand updatetag = new SqlCommand(@"UPDATE Tags SET tag_name=@tag_name  where tag_no=@tag_no", cnSQL);
                    updatetag.Parameters.Add(new SqlParameter("@tag_no", tag_no));
                    updatetag.Parameters.Add(new SqlParameter("@tag_name", tb_tag_name.Text));
                    updatetag.ExecuteNonQuery();
                    updatetag.Dispose();
                    cnSQL.Close();
                    Response.Redirect("?tag_no=" + tag_no);

                }
                
            }

            
            
            
        }
        
    }
    protected void btn_adm_tagedit_Click(object sender, EventArgs e)
    {
        lb_tag_name.Visible = false;
        tb_tag_name.Visible = true;

        btn_adm_tagedit.Visible = false;
        btn_adm_tagsubmit.Visible = true;
        btn_adm_tagback.Visible = true;

        tb_tag_name.Text = lb_tag_name.Text;
    }
    protected void btn_adm_tagdetailback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admtag");
    }


    protected void btn_addtagback_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=admcd");
    }

    protected void btn_tagcheck_Click(object sender, EventArgs e)
    {

        if (tb_tag.Text == "" || tb_tag.Text == " ") { lb_check.Visible = true; lb_check.Text = "TAG名稱不可空白"; }
        else
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            string result_tagck;
            SqlCommand tagcheck = new SqlCommand(@"SELECT count(*)
        FROM Tags
		where tag_name=@tag_name;", cnSQL);
            tagcheck.Parameters.Add(new SqlParameter("@tag_name", tb_tag.Text));
            result_tagck = tagcheck.ExecuteScalar().ToString();
            lb_check.Visible = true;
            if (result_tagck == "1")
            {
                lb_check.Text = "TAG重複!";

            }
            else
            {
                
                lb_check.Text = "此TAG可使用";
            }
            cnSQL.Close();
        }
        
    }
    protected void GV_RowCommand_tag(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GV_tag.Rows[index];

            //取得ID,要看你列表的SQL將ID放的位置狀況
            string tag_no = row.Cells[1].Text;

            if (e.CommandName.ToString().ToUpper() == "TAG_DETAIL")
            {
                //導去明細頁
                Response.Redirect("?tag_no=" + tag_no);
                //adm_mmdetail_display.Visible = true;

            }
            if (e.CommandName.ToString().ToUpper() == "TAG_DELETE")
            {
                SqlConnection cnSQL = new SqlConnection(strConnString);
                cnSQL.Open();
                SqlCommand deleteod = new SqlCommand(@"DELETE FROM Tags where tag_no=@tag_no;", cnSQL);
                deleteod.Parameters.Add(new SqlParameter("@tag_no", tag_no));
                deleteod.ExecuteNonQuery();
                deleteod.Dispose();
                cnSQL.Close();
                Response.Redirect("?page=admtag");
            }
        }
        catch
        {

        }
        

    }


    protected void ck_size(string cd_id)
    {
        string size_s, size_m, size_l, size_xl;

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();



        SqlCommand find_size = new SqlCommand(@"select * from sizes where sz_cd_id=@sz_cd_id;", cnSQL);

        find_size.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));

        SqlDataReader size_reader = find_size.ExecuteReader();
        
        while (size_reader.Read())
        {
            if (size_reader["sz_s"].ToString() == "Y") { size_s = "Y"; } else { size_s = "N"; }
            if (size_reader["sz_m"].ToString() == "Y") { size_m = "Y"; } else { size_m = "N"; }
            if (size_reader["sz_l"].ToString() == "Y") { size_l = "Y"; } else { size_l = "N"; }
            if (size_reader["sz_xl"].ToString() == "Y") { size_xl = "Y"; } else { size_xl = "N"; }

            size_reader.Close();
            cnSQL.Close();
            

            //string od_no = Request.QueryString["od_no"].ToString();
            //SqlConnection cnSQL = new SqlConnection(strConnString);
            //cnSQL.Open();
            //SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
            //find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
            //string od_id = find_od_id.ExecuteScalar().ToString();

            //SqlCommand update_odd = new SqlCommand(@"UPDATE Order_detail SET odd_s_amount=@odd_s_amount, odd_m_amount=@odd_m_amount,odd_l_amount=@odd_l_amount,odd_xl_amount=@odd_xl_amount
            //                                where odd_od_id=@odd_od_id and odd_cd_id=@odd_cd_id;;", cnSQL);
            //update_odd.Parameters.Add(new SqlParameter("@odd_s_amount", s));
            //update_odd.Parameters.Add(new SqlParameter("@odd_m_amount", m));
            //update_odd.Parameters.Add(new SqlParameter("@odd_l_amount", l));
            //update_odd.Parameters.Add(new SqlParameter("@odd_xl_amount", xl));
            //update_odd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            //update_odd.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
            //update_odd.ExecuteNonQuery();
            //update_odd.Dispose();
            //cnSQL.Close();
            //Response.Redirect("?od_no=" + od_no);

        }
        
        
    }
    protected void test(int s, int m,int l,int xl ,string cd_id)
    {
        string size_s, size_m, size_l, size_xl="6";

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();



        SqlCommand find_size = new SqlCommand(@"select * from sizes where sz_cd_id=@sz_cd_id;", cnSQL);

        find_size.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));

        SqlDataReader size_reader = find_size.ExecuteReader();

        while (size_reader.Read())
        {
            if (size_reader["sz_s"].ToString() == "Y") { size_s = "Y"; } else { size_s = "N"; }
            if (size_reader["sz_m"].ToString() == "Y") { size_m = "Y"; } else { size_m = "N"; }
            if (size_reader["sz_l"].ToString() == "Y") { size_l = "Y"; } else { size_l = "N"; }
            if (size_reader["sz_xl"].ToString() == "Y") { size_xl = "Y"; } else { size_xl = "N"; }

            


            //string od_no = Request.QueryString["od_no"].ToString();
            //SqlConnection cnSQL = new SqlConnection(strConnString);
            //cnSQL.Open();
            //SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
            //find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
            //string od_id = find_od_id.ExecuteScalar().ToString();

            //SqlCommand update_odd = new SqlCommand(@"UPDATE Order_detail SET odd_s_amount=@odd_s_amount, odd_m_amount=@odd_m_amount,odd_l_amount=@odd_l_amount,odd_xl_amount=@odd_xl_amount
            //                                where odd_od_id=@odd_od_id and odd_cd_id=@odd_cd_id;;", cnSQL);
            //update_odd.Parameters.Add(new SqlParameter("@odd_s_amount", s));
            //update_odd.Parameters.Add(new SqlParameter("@odd_m_amount", m));
            //update_odd.Parameters.Add(new SqlParameter("@odd_l_amount", l));
            //update_odd.Parameters.Add(new SqlParameter("@odd_xl_amount", xl));
            //update_odd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            //update_odd.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
            //update_odd.ExecuteNonQuery();
            //update_odd.Dispose();
            //cnSQL.Close();
            //Response.Redirect("?od_no=" + od_no);
        }
        
        cnSQL.Close();

        //SqlConnection cnSQL = new SqlConnection(strConnString);
        //cnSQL.Open();



        //SqlCommand find_size = new SqlCommand(@"select * from sizes where sz_cd_id=@sz_cd_id;", cnSQL);

        //find_size.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));

        //SqlDataReader size_reader = find_size.ExecuteReader();

        //while (size_reader.Read())
        //{
        //    if (size_reader["sz_s"].ToString() == "Y") { SizeDic["S"] = "Y"; } else { SizeDic["S"] = "N"; }
        //    if (size_reader["sz_m"].ToString() == "Y") { SizeDic["M"] = "Y"; } else { SizeDic["M"] = "N"; }
        //    if (size_reader["sz_l"].ToString() == "Y") { SizeDic["L"] = "Y"; } else { SizeDic["L"] = "N"; }
        //    if (size_reader["sz_xl"].ToString() == "Y") { SizeDic["XL"] = "Y"; } else { SizeDic["XL"] = "N"; }

        //    size_reader.Close();
        //    cnSQL.Close();


        //    //string od_no = Request.QueryString["od_no"].ToString();
        //    //SqlConnection cnSQL = new SqlConnection(strConnString);
        //    //cnSQL.Open();
        //    //SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
        //    //find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
        //    //string od_id = find_od_id.ExecuteScalar().ToString();

        //    //SqlCommand update_odd = new SqlCommand(@"UPDATE Order_detail SET odd_s_amount=@odd_s_amount, odd_m_amount=@odd_m_amount,odd_l_amount=@odd_l_amount,odd_xl_amount=@odd_xl_amount
        //    //                                where odd_od_id=@odd_od_id and odd_cd_id=@odd_cd_id;;", cnSQL);
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_s_amount", s));
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_m_amount", m));
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_l_amount", l));
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_xl_amount", xl));
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
        //    //update_odd.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
        //    //update_odd.ExecuteNonQuery();
        //    //update_odd.Dispose();
        //    //cnSQL.Close();
        //    //Response.Redirect("?od_no=" + od_no);

        //}
        Response.Redirect("?mm_no=" + "s:"+s+"M:"+m+"L:"+l+"XL:"+xl+","+cd_id);
    }

    protected void btn_adm_oddedit_Click(object sender, EventArgs e)
    {

        lb_adm_payment.Visible = false;
        tb_adm_payment.Visible = true;

        lb_adm_delivery.Visible = false;
        tb_adm_delivery.Visible = true;

        lb_adm_adr.Visible = false;
        tb_adm_adr.Visible = true;

        lb_adm_phone.Visible = false;
        tb_adm_phone.Visible = true;

        lb_adm_note.Visible = false;
        tb_adm_note.Visible = true;

        ckb_done.Enabled = true;

        btn_adm_oddedit.Visible = false;
        btn_adm_oddsubmit.Visible = true;
        btn_adm_oddback.Visible = true;

        tb_adm_payment.Text = lb_adm_payment.Text;
        tb_adm_delivery.Text = lb_adm_delivery.Text;
        tb_adm_adr.Text = lb_adm_adr.Text;
        tb_adm_phone.Text = lb_adm_phone.Text;
        tb_adm_note.Text = lb_adm_note.Text;

    }
    protected void btn_adm_oddsubmit_Click(object sender, EventArgs e)
    {
        string od_done;
        if (ckb_done.Checked==true) { od_done = "Y"; } else { od_done = "N"; }

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand updateod = new SqlCommand(@"UPDATE Orders SET od_paymant=@od_paymant, od_delivery=@od_delivery,od_d_address=@od_d_address,od_phone=@od_phone,
                                            od_note=@od_note,od_done=@od_done where od_no=@od_no;", cnSQL);
        string od_no = Request.QueryString["od_no"].ToString();

      

        updateod.Parameters.Add(new SqlParameter("@od_no", od_no));
        updateod.Parameters.Add(new SqlParameter("@od_paymant", tb_adm_payment.Text));
        updateod.Parameters.Add(new SqlParameter("@od_delivery", tb_adm_delivery.Text));
        updateod.Parameters.Add(new SqlParameter("@od_d_address", tb_adm_adr.Text));
        updateod.Parameters.Add(new SqlParameter("@od_phone", tb_adm_phone.Text));
        updateod.Parameters.Add(new SqlParameter("@od_note", tb_adm_note.Text));
        updateod.Parameters.Add(new SqlParameter("@od_done", od_done));
        updateod.ExecuteNonQuery();
        updateod.Dispose();
        cnSQL.Close();

        Response.Redirect("?od_no=" + od_no);
    }
    protected void btn_adm_oddback_Click (object sender, EventArgs e)
    {

        string od_no = Request.QueryString["od_no"].ToString();
        Response.Redirect("?od_no=" + od_no);
    }

    protected void btn_additem_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string cd_id;
        string od_no = Request.QueryString["od_no"].ToString();

        SqlCommand find_od_id = new SqlCommand(@"select od_id from Orders where od_no=@od_no;", cnSQL);
        find_od_id.Parameters.Add(new SqlParameter("@od_no", od_no));
        string od_id = find_od_id.ExecuteScalar().ToString();

        if (tb_additeminput.Text.Contains("cd") == true) {cd_id = tb_additeminput.Text; }
        else
        {
            SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
            find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_additeminput.Text));
            cd_id = find_cd_id.ExecuteScalar().ToString();
        }

        if (tb_additems.Text == "0" && tb_additemm.Text == "0" && tb_additeml.Text == "0" && tb_additemxl.Text == "0") { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('必須輸入商品數量!');", true); }
        else
        {
            SqlCommand ck_odd_cd = new SqlCommand(@"select count(*) from Order_detail where odd_cd_id=@odd_cd_id and odd_od_id=@odd_od_id;", cnSQL);
            ck_odd_cd.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
            ck_odd_cd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
            int result_odd_cd = Convert.ToInt32(ck_odd_cd.ExecuteScalar().ToString()) ;

            if (result_odd_cd == 1) { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在訂單!');", true); }
            if(result_odd_cd ==0)
            {
                SqlCommand newitem = new SqlCommand(@"Insert Into Order_detail(odd_od_id,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount) Values(@odd_od_id,@odd_cd_id,@odd_s_amount,@odd_m_amount,@odd_l_amount,@odd_xl_amount);", cnSQL);

                newitem.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                newitem.Parameters.Add(new SqlParameter("@odd_cd_id", cd_id));
                newitem.Parameters.Add(new SqlParameter("@odd_s_amount", tb_additems.Text));
                newitem.Parameters.Add(new SqlParameter("@odd_m_amount", tb_additemm.Text));
                newitem.Parameters.Add(new SqlParameter("@odd_l_amount", tb_additeml.Text));
                newitem.Parameters.Add(new SqlParameter("@odd_xl_amount", tb_additemxl.Text));
                newitem.ExecuteNonQuery();
                newitem.Dispose();
                cnSQL.Close();
                check_od(od_id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('成功!');", true);
                Response.Redirect("?od_no=" + od_no);
            }
            
        }
        
    }

    protected void btn_additemback_Click(object sender, EventArgs e)
    {
        string od_no = Request.QueryString["od_no"].ToString();
        Response.Redirect("?od_no=" + od_no);
    }
    protected void btn_sizeshow_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        try
        {
            string cd_id;          

            if (tb_additeminput.Text.Contains("cd") == true) { cd_id = tb_additeminput.Text; }
            else
            {
                SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
                find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_additeminput.Text));
                cd_id = find_cd_id.ExecuteScalar().ToString();
            }

            

            SqlCommand find_size = new SqlCommand(@"select * from sizes where sz_cd_id=@sz_cd_id;", cnSQL);
            find_size.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));

            SqlDataReader size_reader = find_size.ExecuteReader();

            while (size_reader.Read())
            {
                if (size_reader["sz_s"].ToString() == "Y") { lb_additems.Visible = true; tb_additems.Visible = true; } else { lb_additems.Visible = false; tb_additems.Visible = false; }
                if (size_reader["sz_m"].ToString() == "Y") { lb_additemm.Visible = true; tb_additemm.Visible = true; } else { lb_additemm.Visible = false; tb_additemm.Visible = false; }
                if (size_reader["sz_l"].ToString() == "Y") { lb_additeml.Visible = true; tb_additeml.Visible = true; } else { lb_additeml.Visible = false; tb_additeml.Visible = false; }
                if (size_reader["sz_xl"].ToString() == "Y") { lb_additemxl.Visible = true; tb_additemxl.Visible = true; } else { lb_additemxl.Visible = false; tb_additemxl.Visible = false; }
            }
            size_reader.Close();
            cnSQL.Close();
        }
        catch {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('沒有此商品!');", true);
        }
        
    }

    protected void btn_adm_addnewcd_back_Click(object sender, EventArgs e)
    {
        
        Response.Redirect("?page=admcd");
    }
    protected void btn_adm_addnewcd_submit_Click(object sender, EventArgs e)
    {
        if ((tb_addnewcd_cdname.Text ==""|| tb_addnewcd_cdname.Text == " ")|| (tb_addnewcd_cdgroup.Text == "" || tb_addnewcd_cdgroup.Text == " ") || (temp_colorhex2.Value == "" || temp_colorhex2.Value == " ") || (tb_addnewcd_kind.Text == "" || tb_addnewcd_kind.Text == " ") || (tb_addnewcd_cdmaterial.Text == "" || tb_addnewcd_cdmaterial.Text == " ") || (tb_addnewcd_cdprice.Text == "" || tb_addnewcd_cdprice.Text == " ") || (tb_addnewcd_cdmadein.Text == "" || tb_addnewcd_cdmadein.Text == " ") || (tb_addnewcd_cdplace.Text == "" || tb_addnewcd_cdplace.Text == " "))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('有欄位沒填!');", true); return;
        }

        int status = 1;

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();

        SqlCommand ck_cd = new SqlCommand(@"select count(*) from  Commodities where cd_name=@cd_name;", cnSQL);
        ck_cd.Parameters.Add(new SqlParameter("@cd_name", tb_addnewcd_cdname.Text));
        int result_ck_ck = Convert.ToInt32(ck_cd.ExecuteScalar().ToString());
        if (result_ck_ck > 0 || tb_addnewcd_cdname.Text == " ") { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品名稱重複 或 商品名稱格式錯誤');", true); return; }
        else
        {
            string temp_colorhex = Request.Params["temp_colorhex2"];
            try
            {
                DateTime dt = DateTime.Now;
                SqlCommand newcd = new SqlCommand(@"Insert Into Commodities(cd_id,cd_name,cd_group,cd_color,cd_kind,cd_material,cd_price,cd_madein,cd_place,cd_inserttime) 
                                                          Values(@cd_id,@cd_name,@cd_group,@cd_color,@cd_kind,@cd_material,@cd_price,@cd_madein,@cd_place,@cd_inserttime);", cnSQL);
                newcd.Parameters.Add(new SqlParameter("@cd_id", DBNull.Value));
                newcd.Parameters.Add(new SqlParameter("@cd_name", tb_addnewcd_cdname.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_group", tb_addnewcd_cdgroup.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_color", temp_colorhex));
                newcd.Parameters.Add(new SqlParameter("@cd_kind", tb_addnewcd_kind.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_material", tb_addnewcd_cdmaterial.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_price", tb_addnewcd_cdprice.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_madein", tb_addnewcd_cdmadein.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_place", tb_addnewcd_cdplace.Text));
                newcd.Parameters.Add(new SqlParameter("@cd_inserttime", dt));
                //Response.Redirect("?page=admcd"+",name="+ tb_addnewcd_cdname.Text + ",Group="+ tb_addnewcd_cdgroup.Text + ",color="+ temp_colorhex + ",kind="+ tb_addnewcd_kind.Text + ",mt="+ tb_addnewcd_cdmaterial.Text + ",price="+ tb_addnewcd_cdprice.Text + ",madein="+ tb_addnewcd_cdmadein.Text + ",place="+ tb_addnewcd_cdplace.Text );
                newcd.ExecuteNonQuery();
                newcd.Dispose();
                status = 0;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('新增失敗!(商品)');", true);
                return;

            }
        }

        string sz_s, sz_m, sz_l, sz_xl;
        if (status == 0)
        {
            SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
            find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_addnewcd_cdname.Text));
            string cd_id = find_cd_id.ExecuteScalar().ToString();
            DateTime dt = DateTime.Now;


            foreach (ListItem listitem in ckblist_newcd_cdtag.Items)
            {
                if (listitem.Selected)
                {



                    SqlCommand newhave = new SqlCommand(@"Insert Into Have(have_tag_id,have_cd_id,have_date) 
                                                          Values(@have_tag_id,@have_cd_id,@have_date) ;", cnSQL);
                    newhave.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                    newhave.Parameters.Add(new SqlParameter("@have_cd_id", cd_id));
                    newhave.Parameters.Add(new SqlParameter("@have_date", dt.ToShortDateString()));
                    newhave.ExecuteNonQuery();
                    newhave.Dispose();



                }
            }
            if (ckb_addnewcd_s.Checked == true) { sz_s = "Y"; } else { sz_s = "N"; }
            if (ckb_addnewcd_m.Checked == true) { sz_m = "Y"; } else { sz_m = "N"; }
            if (ckb_addnewcd_l.Checked == true) { sz_l = "Y"; } else { sz_l = "N"; }
            if (ckb_addnewcd_xl.Checked == true) { sz_xl = "Y"; } else { sz_xl = "N"; }
            if (sz_s == "N" && sz_m == "N" && sz_l == "N" && sz_xl == "N")
            {
                SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_id=@cd_id;", cnSQL);
                deletecd.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                deletecd.ExecuteNonQuery();
                deletecd.Dispose();
                cnSQL.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('沒選擇商品尺寸!請重新加入商品');", true);
                return;
               
            }
            else
            {
                try
                {
                    SqlCommand addsz = new SqlCommand(@"Insert Into Sizes (sz_cd_id,sz_s,sz_m,sz_l,sz_xl) Values (@sz_cd_id,@sz_s,@sz_m,@sz_l,@sz_xl) ;", cnSQL);

                    addsz.Parameters.Add(new SqlParameter("@sz_s", sz_s));
                    addsz.Parameters.Add(new SqlParameter("@sz_m", sz_m));
                    addsz.Parameters.Add(new SqlParameter("@sz_l", sz_l));
                    addsz.Parameters.Add(new SqlParameter("@sz_xl", sz_xl));
                    addsz.Parameters.Add(new SqlParameter("@sz_cd_id", cd_id));
                    addsz.ExecuteNonQuery();
                    addsz.Dispose();
                }
                catch
                {
                    SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_id=@cd_id;", cnSQL);
                    deletecd.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                    deletecd.ExecuteNonQuery();
                    deletecd.Dispose();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('新增失敗(SIZE)，請重新加入商品!!');", true);
                    cnSQL.Close();
                    return;
                }
                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                string uploadFolder = Request.PhysicalApplicationPath + "images\\clothes\\";
                if (FU_cdimg.HasFile)
                {
                    string extension = Path.GetExtension(FU_cdimg.PostedFile.FileName);


                    if (allowedExtextsion.IndexOf(extension) == -1)
                    {
                        SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_id=@cd_id;", cnSQL);
                        deletecd.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                        deletecd.ExecuteNonQuery();
                        deletecd.Dispose();
                        cnSQL.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入商品!');", true);                       
                        return;
                    }
                    try
                    {
                        FU_cdimg.SaveAs(uploadFolder + cd_id + extension);

                    }
                    catch (Exception ex)
                    {
                        SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_id=@cd_id;", cnSQL);
                        deletecd.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                        deletecd.ExecuteNonQuery();
                        deletecd.Dispose();                      
                        cnSQL.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入商品!');", true);
                        return;
                    }

                }
                else
                {
                    SqlCommand deletecd = new SqlCommand(@"DELETE FROM Commodities where cd_id=@cd_id;", cnSQL);
                    deletecd.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                    deletecd.ExecuteNonQuery();
                    deletecd.Dispose();          
                    cnSQL.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇上傳的商品圖片!請重新加入商品!');", true);
                    return;
                }
            }
                       
        }

        cnSQL.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品新增成功!');", true);
        Response.Redirect("?page=admcd");
    }
    protected void btn_adm_newcd_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=admaddcd");
    }

    protected void btn_admnewevent_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admnewevent");
    }

    protected void btn_adm_pedetailback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admevent");
    }
    protected void GV_RowCommand_pe(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_pe.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string pe_title = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "PE_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?pe_title=" + pe_title);


        }
        if (e.CommandName.ToString().ToUpper() == "PE_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deletepe = new SqlCommand(@"DELETE FROM Promotion_events where pe_title=@pe_title;", cnSQL);
            deletepe.Parameters.Add(new SqlParameter("@pe_title", pe_title));
            deletepe.ExecuteNonQuery();
            deletepe.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admevent");

        }

    }
    protected void GV_RowCommand_pen(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_pen.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string pe_title = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "PEN_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?pe_title=" + pe_title);


        }
        if (e.CommandName.ToString().ToUpper() == "PEN_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deletepe = new SqlCommand(@"DELETE FROM Promotion_events where pe_title=@pe_title;", cnSQL);
            deletepe.Parameters.Add(new SqlParameter("@pe_title", pe_title));
            deletepe.ExecuteNonQuery();
            deletepe.Dispose();
            cnSQL.Close();
            Response.Redirect("?page=admevent");

        }

    }
    protected void SqlDataSourcePE_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_pe.Attributes.Add("bordercolor", "#000000");
    }
    protected void SqlDataSourcePEn_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_pen.Attributes.Add("bordercolor", "#000000");
    }

    protected void btn_adm_pededit_Click(object sender, EventArgs e)
    {

        lb_pe_discount.Visible = false;
        tb_pe_discount.Visible = true;

        lb_pe_title.Visible = false;
        tb_pe_title.Visible = true;

        lb_pe_stdate.Visible = false;
        tb_pe_stdate.Visible = true;

        lb_pe_exdate.Visible = false;
        tb_pe_exdate.Visible = true;


        ckb_pe_main.Enabled = true;
        ckb_pe_status.Enabled = true;

        btn_adm_pededit.Visible = false;
        btn_adm_pedsubmit.Visible = true;
        btn_adm_pedback.Visible = true;
        div_FUpeimg.Visible = true;
        lb_pe_imgmsg.Text = lb_pe_imgmsg.Text;

        tb_pe_discount.Text = lb_pe_discount.Text;
        tb_pe_title.Text = lb_pe_title.Text;
        tb_pe_stdate.Text = lb_pe_stdate.Text;
        tb_pe_exdate.Text = lb_pe_exdate.Text;
    }

    protected void btn_adm_pedsubmit_Click(object sender, EventArgs e)
    {

        string pe_title = Request.QueryString["pe_title"].ToString();
        double discount = Convert.ToDouble(tb_pe_discount.Text);
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string main = "", status = "";
        if (ckb_pe_main.Checked == true) { main = "Y"; } else { main = "N"; }
        if (ckb_pe_status.Checked == true) { status = "Y"; } else { status = "N"; }
        DateTime dt_st = Convert.ToDateTime(tb_pe_stdate.Text);
        DateTime dt_ex = Convert.ToDateTime(tb_pe_exdate.Text);
        try
        {




            if (lb_pe_title.Text == tb_pe_title.Text)
            {
                if (main == "Y")
                {
                    SqlCommand ck_main = new SqlCommand(@"select pe_title from  Promotion_events where pe_main=@pe_main;", cnSQL);
                    ck_main.Parameters.Add(new SqlParameter("@pe_main", "Y"));
                    string result_ck_main = ck_main.ExecuteScalar().ToString();

                    if ((result_ck_main != "" && result_ck_main != null) && result_ck_main != pe_title)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('已有主要優惠活動!請先取消該活動在重新選擇!');", true);
                        cnSQL.Close();
                        return;
                    }
                    else
                    {
                        if (discount < 1 && discount > 0)
                        {

                            discount = Math.Round(discount, 2);
                            SqlCommand updatepe = new SqlCommand(@"UPDATE Promotion_events SET pe_title=@new_pe_title,pe_starting_date=@pe_starting_date,pe_expiring_date=@pe_expiring_date,pe_status=@pe_status,pe_main=@pe_main,pe_discount=@pe_discount where pe_title=@old_pe_title;", cnSQL);
                            updatepe.Parameters.Add(new SqlParameter("@new_pe_title", tb_pe_title.Text));
                            updatepe.Parameters.Add(new SqlParameter("@pe_starting_date", dt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                            updatepe.Parameters.Add(new SqlParameter("@pe_expiring_date", dt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                            updatepe.Parameters.Add(new SqlParameter("@pe_status", status));
                            updatepe.Parameters.Add(new SqlParameter("@pe_main", main));
                            updatepe.Parameters.Add(new SqlParameter("@pe_discount", discount));
                            updatepe.Parameters.Add(new SqlParameter("@old_pe_title", pe_title));
                            updatepe.ExecuteNonQuery();
                            updatepe.Dispose();
                            cnSQL.Close();
                            if (FU_peimg.HasFile)
                            {
                                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                string extension = Path.GetExtension(FU_peimg.PostedFile.FileName);


                                if (allowedExtextsion.IndexOf(extension) == -1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                    return;
                                }
                                try
                                {
                                    FU_peimg.SaveAs(uploadFolder + tb_pe_title.Text + extension);
                                    cnSQL.Open();
                                    SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_pe_title.Text));
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_pe_title.Text + ".jpg"));
                                    updatepeimg.ExecuteNonQuery();
                                    updatepeimg.Dispose();
                                    cnSQL.Close();


                                }
                                catch
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                    return;
                                }

                            }
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇上傳的商品圖片!請重新加入商品!');", true);
                            //    return;
                            //}
                            Response.Redirect("?pe_title=" + tb_pe_title.Text);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                            cnSQL.Close();
                            return;
                        }


                    }
                }
                else
                {
                    if (discount < 1 && discount > 0)
                    {

                        discount = Math.Round(discount, 2);
                        SqlCommand updatepe = new SqlCommand(@"UPDATE Promotion_events SET pe_title=@new_pe_title,pe_starting_date=@pe_starting_date,pe_expiring_date=@pe_expiring_date,pe_status=@pe_status,pe_main=@pe_main,pe_discount=@pe_discount where pe_title=@old_pe_title;", cnSQL);
                        updatepe.Parameters.Add(new SqlParameter("@new_pe_title", tb_pe_title.Text));
                        updatepe.Parameters.Add(new SqlParameter("@pe_starting_date", dt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                        updatepe.Parameters.Add(new SqlParameter("@pe_expiring_date", dt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                        updatepe.Parameters.Add(new SqlParameter("@pe_status", status));
                        updatepe.Parameters.Add(new SqlParameter("@pe_main", main));
                        updatepe.Parameters.Add(new SqlParameter("@pe_discount", discount));
                        updatepe.Parameters.Add(new SqlParameter("@old_pe_title", pe_title));
                        //Response.Redirect("?page=admcd"+ ","+tb_pe_title.Text+ ", "+status + ", " + main + ", " + discount + ", " + pe_title);
                        updatepe.ExecuteNonQuery();
                        updatepe.Dispose();
                        cnSQL.Close();

                        if (FU_peimg.HasFile)
                        {
                            List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                            string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                            string extension = Path.GetExtension(FU_peimg.PostedFile.FileName);


                            if (allowedExtextsion.IndexOf(extension) == -1)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                return;
                            }
                            try
                            {
                                FU_peimg.SaveAs(uploadFolder + tb_pe_title.Text + extension);
                                cnSQL.Open();
                                SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_pe_title.Text));
                                updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_pe_title.Text + ".jpg"));
                                updatepeimg.ExecuteNonQuery();
                                updatepeimg.Dispose();
                                cnSQL.Close();


                            }
                            catch
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                return;
                            }

                        }
                        Response.Redirect("?pe_title=" + tb_pe_title.Text);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                        cnSQL.Close();
                        return;
                    }
                }
            }
            else
            {
                SqlCommand ck_title = new SqlCommand(@"select count(*) from  Promotion_events where pe_title=@pe_title;", cnSQL);
                ck_title.Parameters.Add(new SqlParameter("@pe_title", tb_pe_title.Text));
                int result_ck_title = Convert.ToInt32(ck_title.ExecuteScalar().ToString());

                if (result_ck_title > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('活動名稱重複!請重新輸入!');", true);
                    cnSQL.Close();
                    return;
                }
                else
                {
                    if (main == "Y")
                    {
                        SqlCommand ck_main = new SqlCommand(@"select pe_title from  Promotion_events where pe_main=@pe_main;", cnSQL);
                        ck_main.Parameters.Add(new SqlParameter("@pe_main", "Y"));
                        string result_ck_main = ck_main.ExecuteScalar().ToString();

                        if ((result_ck_main != "" && result_ck_main != null) && result_ck_main != pe_title)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('已有主要優惠活動!請先取消該活動在重新選擇!');", true);
                            cnSQL.Close();
                            return;
                        }
                        else
                        {
                            if (discount < 1 && discount > 0)
                            {

                                discount = Math.Round(discount, 2);
                                SqlCommand updatepe = new SqlCommand(@"UPDATE Promotion_events SET pe_title=@new_pe_title,pe_starting_date=@pe_starting_date,pe_expiring_date=@pe_expiring_date,pe_status=@pe_status,pe_main=@pe_main,pe_discount=@pe_discount where pe_title=@old_pe_title;", cnSQL);
                                updatepe.Parameters.Add(new SqlParameter("@new_pe_title", tb_pe_title.Text));
                                updatepe.Parameters.Add(new SqlParameter("@pe_starting_date", dt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                                updatepe.Parameters.Add(new SqlParameter("@pe_expiring_date", dt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                                updatepe.Parameters.Add(new SqlParameter("@pe_status", status));
                                updatepe.Parameters.Add(new SqlParameter("@pe_main", main));
                                updatepe.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                updatepe.Parameters.Add(new SqlParameter("@old_pe_title", pe_title));
                                updatepe.ExecuteNonQuery();
                                updatepe.Dispose();
                                cnSQL.Close();
                                if (FU_peimg.HasFile)
                                {
                                    List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                    string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                    string extension = Path.GetExtension(FU_peimg.PostedFile.FileName);


                                    if (allowedExtextsion.IndexOf(extension) == -1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                        return;
                                    }
                                    try
                                    {
                                        FU_peimg.SaveAs(uploadFolder + tb_pe_title.Text + extension);
                                        cnSQL.Open();
                                        SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_pe_title.Text));
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_pe_title.Text + ".jpg"));
                                        updatepeimg.ExecuteNonQuery();
                                        updatepeimg.Dispose();
                                        cnSQL.Close();


                                    }
                                    catch
                                    {

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                        return;
                                    }

                                }
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇上傳的商品圖片!請重新加入商品!');", true);
                                //    return;
                                //}
                                Response.Redirect("?pe_title=" + tb_pe_title.Text);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                                cnSQL.Close();
                                return;
                            }


                        }
                    }
                    else
                    {
                        if (discount < 1 && discount > 0)
                        {

                            discount = Math.Round(discount, 2);
                            SqlCommand updatepe = new SqlCommand(@"UPDATE Promotion_events SET pe_title=@new_pe_title,pe_starting_date=@pe_starting_date,pe_expiring_date=@pe_expiring_date,pe_status=@pe_status,pe_main=@pe_main,pe_discount=@pe_discount where pe_title=@old_pe_title;", cnSQL);
                            updatepe.Parameters.Add(new SqlParameter("@new_pe_title", tb_pe_title.Text));
                            updatepe.Parameters.Add(new SqlParameter("@pe_starting_date", dt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                            updatepe.Parameters.Add(new SqlParameter("@pe_expiring_date", dt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                            updatepe.Parameters.Add(new SqlParameter("@pe_status", status));
                            updatepe.Parameters.Add(new SqlParameter("@pe_main", main));
                            updatepe.Parameters.Add(new SqlParameter("@pe_discount", discount));
                            updatepe.Parameters.Add(new SqlParameter("@old_pe_title", pe_title));
                            updatepe.ExecuteNonQuery();
                            updatepe.Dispose();

                            cnSQL.Close();
                            if (FU_peimg.HasFile)
                            {
                                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                string extension = Path.GetExtension(FU_peimg.PostedFile.FileName);


                                if (allowedExtextsion.IndexOf(extension) == -1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                    return;
                                }
                                try
                                {
                                    FU_peimg.SaveAs(uploadFolder + tb_pe_title.Text + extension);
                                    cnSQL.Open();
                                    SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_pe_title.Text));
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_pe_title.Text + ".jpg"));
                                    updatepeimg.ExecuteNonQuery();
                                    updatepeimg.Dispose();
                                    cnSQL.Close();


                                }
                                catch
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                    return;
                                }

                            }
                            Response.Redirect("?pe_title=" + tb_pe_title.Text);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                            cnSQL.Close();
                            return;
                        }
                    }
                }
            }






        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('優惠活動資料修改未成功請重新嘗試!');", true);
            cnSQL.Close();
            return;
        }

    }
    protected void btn_addevent_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=admcd");
    }
    protected void btn_adm_tag_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=admtag");
    }
    protected void btn_adm_pe_Click(object sender, EventArgs e)
    {

        Response.Redirect("?page=admevent");
    }
    protected void GV_RowCommand_pedetail(object sender, GridViewCommandEventArgs e)
    {
        string pe_title = Request.QueryString["pe_title"].ToString();
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_ped.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string cd_id = row.Cells[1].Text;


        if (e.CommandName.ToString().ToUpper() == "PED_ITEM_DELETE")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deletepeditem = new SqlCommand(@"DELETE FROM Promotion_events where pe_cd_id=@pe_cd_id and pe_title=@pe_title;", cnSQL);
            deletepeditem.Parameters.Add(new SqlParameter("@pe_cd_id", cd_id));
            deletepeditem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
            deletepeditem.ExecuteNonQuery();
            deletepeditem.Dispose();
            cnSQL.Close();
            Response.Redirect("?pe_title="+pe_title);

        }

    }
    protected void SqlDataSourceTAG_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_tag.Attributes.Add("bordercolor", "#000000");
    }
    protected void SqlDataSourcePED_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_ped.Attributes.Add("bordercolor", "#000000");
    }

    protected void btn_ped_additemsubmit_Click(object sender, EventArgs e)
    {
        DateTime st_date = new DateTime(), ex_date = new DateTime();
        string imgpath = "", status = "", main = "", discount = "";
        string pe_title = Request.QueryString["pe_title"].ToString();
        if (tb_ped_additeminput.Text == "" || tb_ped_additeminput.Text == " ")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未輸入欲加入商品名稱或編號或種類');", true);
            return;
        }
        else
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();


            SqlCommand peinfo = new SqlCommand(@"SELECT DISTINCT pe_title ,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount from Promotion_events where pe_title=@pe_title;", cnSQL);
            peinfo.Parameters.Add(new SqlParameter("@pe_title", pe_title));
            SqlDataReader peinfo_reader = peinfo.ExecuteReader();
            while (peinfo_reader.Read())
            {
                st_date = Convert.ToDateTime(peinfo_reader["pe_starting_date"].ToString());
                ex_date = Convert.ToDateTime(peinfo_reader["pe_expiring_date"].ToString());
                try { imgpath = peinfo_reader["pe_imgPath"].ToString(); } catch { imgpath = "NULL"; }
                status = peinfo_reader["pe_status"].ToString();
                main = peinfo_reader["pe_main"].ToString();
                discount = peinfo_reader["pe_discount"].ToString();
            }
            peinfo_reader.Close();


            try
            {
                string cd = "";


                if (tb_ped_additeminput.Text.Contains("cd") == true)
                {

                    cd = tb_ped_additeminput.Text;
                    SqlCommand ck_cd = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_title=@pe_title;", cnSQL);
                    ck_cd.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                    ck_cd.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                    int result_ck_cd = Convert.ToInt32(ck_cd.ExecuteScalar().ToString());
                    if (result_ck_cd > 0)
                    { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在此活動!');", true); cnSQL.Close(); return; }

                    else
                    {
                        SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';", cnSQL);
                        ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                        int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                        if (result_ck_cd_all > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                        }

                        else
                        {
                            try
                            {
                                if (imgpath == "" || imgpath == " " || imgpath == "NULL")
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                    cnSQL.Close();
                                    Response.Redirect("?pe_title=" + pe_title);
                                }
                                else
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", imgpath));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                    cnSQL.Close();
                                    Response.Redirect("?pe_title=" + pe_title);
                                }

                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品加入失敗請重新嘗試!');", true); cnSQL.Close(); return;
                            }

                        }
                    }
                }

                else if (tb_ped_additeminput.Text.Contains("kind_") == true)
                {
                    SqlConnection cnSQL2 = new SqlConnection(strConnString);
                    cnSQL2.Open();
                    cd = tb_ped_additeminput.Text.Replace("kind_", "").ToUpper();
                    SqlCommand kind_cdlist = new SqlCommand(@"select cd_id from Commodities where cd_kind=@cd_kind;", cnSQL);
                    kind_cdlist.Parameters.Add(new SqlParameter("@cd_kind", cd));



                    SqlDataReader kind_cdlist_reader = kind_cdlist.ExecuteReader();
                    while (kind_cdlist_reader.Read())
                    {

                        string kind_cd_id = kind_cdlist_reader["cd_id"].ToString();

                        SqlCommand ck_cd_all2 = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                        ck_cd_all2.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cd_id));
                        int result_ck_cd_all2 = Convert.ToInt32(ck_cd_all2.ExecuteScalar().ToString());
                        if (result_ck_cd_all2 > 0)
                        {

                        }
                        else
                        {
                            try
                            {
                                if (imgpath == "" || imgpath == " " || imgpath == "NULL")
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL2);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cd_id));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));
                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                }
                                else
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL2);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cd_id));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", imgpath));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                }

                            }
                            catch
                            {

                            }
                        }
                    }

                    cnSQL2.Close();
                    kind_cdlist_reader.Close();
                    cnSQL.Close();
                    Response.Redirect("?pe_title=" + pe_title);
                }
                else
                {

                    SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
                    find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_ped_additeminput.Text));
                    cd = find_cd_id.ExecuteScalar().ToString();

                    SqlCommand ck_cd = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_title=@pe_title;", cnSQL);
                    ck_cd.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                    ck_cd.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                    int result_ck_cd = Convert.ToInt32(ck_cd.ExecuteScalar().ToString());

                    if (result_ck_cd > 0)
                    { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在此活動!');", true); cnSQL.Close(); return; }

                    else
                    {
                        SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';;", cnSQL);
                        ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                        int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                        if (result_ck_cd_all > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                        }

                        else
                        {
                            try
                            {
                                if (imgpath == "" || imgpath == " " || imgpath == "NULL")
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                    cnSQL.Close();
                                    Response.Redirect("?pe_title=" + pe_title);
                                }
                                else
                                {
                                    SqlCommand newitem = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);

                                    newitem.Parameters.Add(new SqlParameter("@pe_cd_id", cd));
                                    newitem.Parameters.Add(new SqlParameter("@pe_title", pe_title));
                                    newitem.Parameters.Add(new SqlParameter("@pe_imgPath", imgpath));
                                    newitem.Parameters.Add(new SqlParameter("@pe_starting_date", st_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_expiring_date", ex_date));
                                    newitem.Parameters.Add(new SqlParameter("@pe_status", status));
                                    newitem.Parameters.Add(new SqlParameter("@pe_main", main));
                                    newitem.Parameters.Add(new SqlParameter("@pe_discount", discount));
                                    newitem.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                    newitem.ExecuteNonQuery();
                                    newitem.Dispose();
                                    cnSQL.Close();
                                    Response.Redirect("?pe_title=" + pe_title);
                                }

                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品加入失敗請重新嘗試!');", true); cnSQL.Close(); return;
                            }

                        }
                    }

                }

            }
            catch
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('沒有此商品!');", true);
            }

        }


    }

    protected void btn_adm_pedetailadditem_Click(object sender, EventArgs e)
    {
        string pe_title = Request.QueryString["pe_title"].ToString();
        Response.Redirect("?pe_title=" + pe_title + "&page=adm_ped_additem");
    }
    protected void btn_ped_additemback_Click(object sender, EventArgs e)
    {
        string pe_title = Request.QueryString["pe_title"].ToString();
        Response.Redirect("?pe_title=" + pe_title );
    }
    protected void btn_adm_pedback_Click(object sender, EventArgs e)
    {
        string pe_title = Request.QueryString["pe_title"].ToString();
        Response.Redirect("?pe_title=" + pe_title);
    }
    protected void btn_pemback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admcd");
    }
    protected void btn_adm_tagmback_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admcd");
    }
    protected void btn_pdremoveimg_Click(object sender, EventArgs e)
    {
        string pe_title = Request.QueryString["pe_title"].ToString();
        string delfileName = pe_title + ".jpg";
        string deleteFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        try
        {
            File.Delete(deleteFolder + delfileName);
            SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
            updatepeimg.Parameters.Add(new SqlParameter("@pe_title", pe_title));
            updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
            updatepeimg.ExecuteNonQuery();
            updatepeimg.Dispose();
            cnSQL.Close();
            Response.Redirect("?pe_title=" + pe_title);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('圖片刪除失敗請重新嘗試!');", true); cnSQL.Close(); return;
        }
        
       
    }
    protected void btn_newpe_submit_Click(object sender, EventArgs e)
    {
        
        string newmain = "", newstatus = "";
        if (ckb_newpemain.Checked == true) { newmain = "Y"; } else { newmain = "N"; }
        if (ckb_newpestatus.Checked == true) { newstatus = "Y"; } else { newstatus = "N"; }
        double newdiscount = Convert.ToDouble(tb_newpediscount.Text);
        DateTime newdt_st = Convert.ToDateTime(tb_newpest.Text);
        DateTime newdt_ex = Convert.ToDateTime(tb_newpeex.Text);
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand ck_title = new SqlCommand(@"select count(*) from  Promotion_events where pe_title=@pe_title;", cnSQL);
        ck_title.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
        int result_ck_title = Convert.ToInt32(ck_title.ExecuteScalar().ToString());


        if (tb_newpecd.Text == "" || tb_newpecd.Text == " " || tb_newpecd.Text == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('欲加入商品名稱/編號/種類(kund_XX) 未輸入!');", true); cnSQL.Close(); return;
        }

        if (result_ck_title > 0 )
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('活動名稱重複!請重新輸入!');", true);
            cnSQL.Close();
            return;
        }
        else
        {

            if (newmain == "Y")
            {
                SqlCommand ck_main = new SqlCommand(@"select count(pe_title) from  Promotion_events where pe_main='Y' and pe_status='Y';", cnSQL);

                string result_ck_main = ck_main.ExecuteScalar().ToString();

                if (result_ck_main != "0" && result_ck_main != tb_newpetitle.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('已有主要優惠活動!請先取消該活動在重新選擇!');", true);
                    cnSQL.Close();
                    return;
                }
                else
                {
                    if (newdiscount < 1 && newdiscount > 0)
                    {
                        if (tb_newpecd.Text.Contains("cd") == true)
                        {
                            SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';;", cnSQL);
                            ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", tb_newpecd.Text));
                            int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                            if (result_ck_cd_all > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                            }
                            try
                            {
                                newdiscount = Math.Round(newdiscount, 2);

                                SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);
                                newcd.Parameters.Add(new SqlParameter("@pe_cd_id", tb_newpecd.Text));
                                newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                                newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                                newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                                newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                                newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                                newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                newcd.ExecuteNonQuery();
                                newcd.Dispose();
                                cnSQL.Close();
                                if (FU_newpeimg.HasFile)
                                {
                                    List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                    string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                    string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);


                                    if (allowedExtextsion.IndexOf(extension) == -1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                        return;
                                    }
                                    try
                                    {
                                        FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                        cnSQL.Open();
                                        SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                        updatepeimg.ExecuteNonQuery();
                                        updatepeimg.Dispose();
                                        cnSQL.Close();

                                    }
                                    catch
                                    {

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                        Response.Redirect("?pe_title=" + tb_newpetitle.Text);
                                    }

                                }
                                Response.Redirect("?page=admevent");
                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('查無此商品編號或格式錯誤');", true);
                                cnSQL.Close();
                                return;
                            }
                        }
                        else if (tb_newpecd.Text.Contains("kind_") == true)
                        {
                            SqlConnection cnSQL2 = new SqlConnection(strConnString);
                            cnSQL2.Open();
                            string pe_cd = tb_newpecd.Text.Replace("kind_", "").ToUpper();
                            SqlCommand kind_cdlist = new SqlCommand(@"select cd_id from Commodities where cd_kind=@cd_kind;", cnSQL);
                            kind_cdlist.Parameters.Add(new SqlParameter("@cd_kind", pe_cd));

                            SqlDataReader kind_cdlist_reader = kind_cdlist.ExecuteReader();
                            while (kind_cdlist_reader.Read())

                            {

                                string kind_cd_id = kind_cdlist_reader["cd_id"].ToString();

                                SqlCommand ck_cd_all2 = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                                ck_cd_all2.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cd_id));
                                int result_ck_cd_all2 = Convert.ToInt32(ck_cd_all2.ExecuteScalar().ToString());
                                if (result_ck_cd_all2 > 0)
                                {

                                }
                                else
                                {
                                    try
                                    {
                                        newdiscount = Math.Round(newdiscount, 2);

                                        SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL2);
                                        newcd.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cdlist_reader["cd_id"].ToString()));
                                        newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                        newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                        newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                                        newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                                        newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                                        newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                                        newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                                        newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                        newcd.ExecuteNonQuery();
                                        newcd.Dispose();
                                        

                                    }
                                    catch
                                    {

                                    }
                                }

                            }
                            cnSQL2.Close();
                            kind_cdlist_reader.Close();
                            cnSQL.Close();

                            if (FU_newpeimg.HasFile)
                            {
                                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);


                                if (allowedExtextsion.IndexOf(extension) == -1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                    return;
                                }
                                try
                                {
                                    FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                    cnSQL.Open();
                                    SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                    updatepeimg.ExecuteNonQuery();
                                    updatepeimg.Dispose();
                                    cnSQL.Close();

                                }
                                catch
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                    Response.Redirect("?pe_title=" + tb_newpetitle.Text);
                                }

                            }
                            Response.Redirect("?page=admevent");

                        }
                        else
                        {

                            try
                            {
                                SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
                                find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_newpecd.Text));
                                string pe_cd_id = find_cd_id.ExecuteScalar().ToString();

                                SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';;", cnSQL);
                                ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", pe_cd_id));
                                int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                                if (result_ck_cd_all > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                                }

                                newdiscount = Math.Round(newdiscount, 2);

                                SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);
                                newcd.Parameters.Add(new SqlParameter("@pe_cd_id", pe_cd_id));
                                newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                                newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                                newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                                newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                                newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                                newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));

                                newcd.ExecuteNonQuery();
                                newcd.Dispose();
                                cnSQL.Close();
                                if (FU_newpeimg.HasFile)
                                {
                                    List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                    string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                    string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);


                                    if (allowedExtextsion.IndexOf(extension) == -1)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                        return;
                                    }
                                    try
                                    {
                                        FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                        cnSQL.Open();
                                        SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                        updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                        updatepeimg.ExecuteNonQuery();
                                        updatepeimg.Dispose();
                                        cnSQL.Close();

                                    }
                                    catch
                                    {

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                        Response.Redirect("?pe_title=" + tb_newpetitle.Text);
                                    }

                                }
                                Response.Redirect("?page=admevent");


                            }
                            catch
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('查無此商品名稱或格式錯誤');", true);
                                cnSQL.Close();
                                return;
                            }
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                        cnSQL.Close();
                        return;
                    }


                }
            }
            else
            {
                if (newdiscount < 1 && newdiscount > 0)
                {
                    if (tb_newpecd.Text.Contains("cd") == true)
                    {
                        SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';;", cnSQL);
                        ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", tb_newpecd.Text));
                        int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                        if (result_ck_cd_all > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                        }
                        try
                        {
                            newdiscount = Math.Round(newdiscount, 2);

                            SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);
                            newcd.Parameters.Add(new SqlParameter("@pe_cd_id", tb_newpecd.Text));
                            newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                            newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                            newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                            newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                            newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                            newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                            newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                            newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));
                            
                            newcd.ExecuteNonQuery();
                            newcd.Dispose();
                            cnSQL.Close();
                            if (FU_newpeimg.HasFile)
                            {
                                
                                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);


                                if (allowedExtextsion.IndexOf(extension) == -1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                    return;
                                }
                                //try
                                //{
                                    
                                    FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                    cnSQL.Open();
                                    SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                    updatepeimg.ExecuteNonQuery();
                                    updatepeimg.Dispose();
                                    cnSQL.Close();

                                //}
                                //catch
                                //{
                                    
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品圖片上傳未成功!，請重新加入!');", true);
                                    //Response.Redirect("?pe_title=" + tb_newpetitle.Text);
                                //}

                            }
                            Response.Redirect("?page=admevent");
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('查無此商品編號或格式錯誤');", true);
                            cnSQL.Close();
                            return;
                        }
                    }
                    else if (tb_newpecd.Text.Contains("kind_") == true)
                    {
                        
                        SqlConnection cnSQL2 = new SqlConnection(strConnString);
                        cnSQL2.Open();
                        string pe_cd = tb_newpecd.Text.Replace("kind_", "").ToUpper();
                        SqlCommand kind_cdlist = new SqlCommand(@"select cd_id from Commodities where cd_kind=@cd_kind;", cnSQL);
                        kind_cdlist.Parameters.Add(new SqlParameter("@cd_kind", pe_cd));

                        
                        SqlConnection cnSQL3 = new SqlConnection(strConnString);
                        
                        cnSQL3.Open();
                        SqlDataReader kind_cdlist_reader = kind_cdlist.ExecuteReader();
                        
                            while (kind_cdlist_reader.Read())
                            {

                                string kind_cd_id = kind_cdlist_reader["cd_id"].ToString();

                                SqlCommand ck_cd_all2 = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                                ck_cd_all2.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cd_id));
                                int result_ck_cd_all2 = Convert.ToInt32(ck_cd_all2.ExecuteScalar().ToString());
                                if (result_ck_cd_all2 > 0)
                                {

                                }
                                else
                                {
                                    

                                    newdiscount = Math.Round(newdiscount, 2);

                                    SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL3);
                                    newcd.Parameters.Add(new SqlParameter("@pe_cd_id", kind_cdlist_reader["cd_id"].ToString()));
                                    newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                    newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                                    newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                                    newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                                    newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                                    newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                                    newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                                    newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));
                                    
                                    newcd.ExecuteNonQuery();
                                    newcd.Dispose();

                                }

                            }
                            kind_cdlist_reader.Close();
                            cnSQL2.Close();
                            cnSQL3.Close();
                       
                            
                      
                        
                        
                        if (FU_newpeimg.HasFile)
                        {
                            List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                            string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                            string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);

                            if (allowedExtextsion.IndexOf(extension) == -1)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                return;
                            }
                            
                                FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                                                
                                SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                updatepeimg.ExecuteNonQuery();
                                updatepeimg.Dispose();
                                cnSQL.Close();                                                 

                        }
                        Response.Redirect("?page=admevent");

                    }
                    else
                    {

                        try
                        {
                            SqlCommand find_cd_id = new SqlCommand(@"select cd_id from Commodities where cd_name=@cd_name;", cnSQL);
                            find_cd_id.Parameters.Add(new SqlParameter("@cd_name", tb_newpecd.Text));
                            string pe_cd_id = find_cd_id.ExecuteScalar().ToString();

                            SqlCommand ck_cd_all = new SqlCommand(@"select count(*) from  Promotion_events where pe_cd_id=@pe_cd_id and pe_status='Y';;", cnSQL);
                            ck_cd_all.Parameters.Add(new SqlParameter("@pe_cd_id", pe_cd_id));
                            int result_ck_cd_all = Convert.ToInt32(ck_cd_all.ExecuteScalar().ToString());
                            if (result_ck_cd_all > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('商品已在其他優惠活動!');", true); cnSQL.Close(); return;
                            }

                            newdiscount = Math.Round(newdiscount, 2);
                            //Response.Redirect("?page=admevent"+","+ pe_cd_id+ "," + tb_newpetitle.Text + "," + newdt_st.ToString("yyyy-MM-dd HH:mm:ss") + "," + newdt_ex.ToString("yyyy-MM-dd HH:mm:ss") + "," + newstatus + "," + newmain + "," + newdiscount );
                            SqlCommand newcd = new SqlCommand(@"Insert Into Promotion_events(pe_cd_id,pe_title,pe_imgPath,pe_starting_date,pe_expiring_date,pe_status,pe_main,pe_discount,pe_count)
                                                         Values(@pe_cd_id,@pe_title,@pe_imgPath,@pe_starting_date,@pe_expiring_date,@pe_status,@pe_main,@pe_discount,@pe_count);", cnSQL);
                            newcd.Parameters.Add(new SqlParameter("@pe_cd_id", pe_cd_id));
                            newcd.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                            newcd.Parameters.Add(new SqlParameter("@pe_imgPath", DBNull.Value));
                            newcd.Parameters.Add(new SqlParameter("@pe_starting_date", newdt_st.ToString("yyyy-MM-dd HH:mm:ss")));
                            newcd.Parameters.Add(new SqlParameter("@pe_expiring_date", newdt_ex.ToString("yyyy-MM-dd HH:mm:ss")));
                            newcd.Parameters.Add(new SqlParameter("@pe_status", newstatus));
                            newcd.Parameters.Add(new SqlParameter("@pe_main", newmain));
                            newcd.Parameters.Add(new SqlParameter("@pe_discount", newdiscount));
                            newcd.Parameters.Add(new SqlParameter("@pe_count", "0"));
                            
                            newcd.ExecuteNonQuery();
                            newcd.Dispose();
                            cnSQL.Close();
                            if (FU_newpeimg.HasFile)
                            {
                                List<string> allowedExtextsion = new List<string> { ".jpg" }; /*允許上傳格式*/
                                string uploadFolder = Request.PhysicalApplicationPath + "images\\slideshow\\";
                                string extension = Path.GetExtension(FU_newpeimg.PostedFile.FileName);


                                if (allowedExtextsion.IndexOf(extension) == -1)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇正確檔案格式(jpg)!請重新加入!');", true);
                                    return;
                                }
                                
                                    FU_newpeimg.SaveAs(uploadFolder + tb_newpetitle.Text + extension);
                                    
                                    cnSQL.Open();
                                    SqlCommand updatepeimg = new SqlCommand(@"UPDATE Promotion_events SET pe_imgPath=@pe_imgPath where pe_title=@pe_title;", cnSQL);
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_title", tb_newpetitle.Text));
                                    updatepeimg.Parameters.Add(new SqlParameter("@pe_imgPath", "images/slideshow/" + tb_newpetitle.Text + ".jpg"));
                                    updatepeimg.ExecuteNonQuery();
                                    updatepeimg.Dispose();
                                    cnSQL.Close();                                

                            }
                            Response.Redirect("?page=admevent");


                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('查無此商品名稱或格式錯誤');", true);
                            cnSQL.Close();
                            return;
                        }
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('折扣格式輸入錯誤');", true);
                    cnSQL.Close();
                    return;
                }
            }

        }
          

         
    }
    protected void btn_adm_addnewpe_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=admevent");
    }

    public string check_setting()
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        try
        {
            string notice = Session["notice"].ToString();
            string id = Session["identity"].ToString();
            string mm_id = Session["mmid"].ToString();
            string odrn = "";
            int wish_in_pe = 0;
            if (notice == "Y" && id == "0")
            {
                SqlCommand find_wish = new SqlCommand(@"SELECT count(*) from Wishing_Wells where ww_cd_id in(select pe_cd_id from Promotion_events where pe_status='Y') and ww_mm_id=@ww_mm_id;", cnSQL);
                find_wish.Parameters.Add(new SqlParameter("@ww_mm_id", mm_id));
                wish_in_pe = Convert.ToInt32(find_wish.ExecuteScalar().ToString());
                cnSQL.Close();              
                if (wish_in_pe > 0) { return "Y," + wish_in_pe; } else { return "N"; }
                
            }
            else if (notice == "Y"&&id == "1")
            {
                SqlCommand find_odern = new SqlCommand(@"select count(*) from Orders where od_done='N';", cnSQL);
                odrn = find_odern.ExecuteScalar().ToString();
                cnSQL.Close();
                return "ADM,"+ odrn;
            }
            else
            {
                cnSQL.Close();
                return "N";
            }

            
            
        }
        catch { cnSQL.Close(); return "N"; }

    }

    public string close_notice()
    {
        Session["notice"] = "N";
        return "Y";
    }

        protected void btn_submit_Click(object sender, EventArgs e)
    {

        //{
        //    SqlCommand maidentity = new SqlCommand(@"Select ma_identity From Management where ma_account=@ma_account;", cnSQL);
        //    maidentity.Parameters.Add(new SqlParameter("@ma_account", name));
        //    ma_identity = maidentity.ExecuteScalar().ToString();
        //}
        //資料庫連線
        SqlConnection cnSQL = new SqlConnection();
        //連線字串
        cnSQL.ConnectionString = strConnString;
        //SqlCommand
        SqlCommand cmdSQL = new SqlCommand();
        cmdSQL.Connection = cnSQL;
        //strSQL 為select form where insert
        String strSQL, act, dbpwd;
        if (tb_act.Text != "")
        {
            act = tb_act.Text.Replace("'", "");
            strSQL = "Select mm_pwd FROM Members WHERE mm_act = '" + act + "';";
            cmdSQL.CommandText = strSQL;
            string result = "";

            cnSQL.Open();
            try
            {
                var firstColumn = cmdSQL.ExecuteScalar();
                if (firstColumn != null)
                {
                    dbpwd = firstColumn.ToString();
                    if (tb_pwd.Text == dbpwd)
                    {

                        //Set Cookie and forward


                        //Response.Cookies["user"].Value = Server.UrlEncode(tb_act.Text);
                        //DateTime dayT = DateTime.Today.AddDays(1);
                        //Response.Cookies["user"].Expires = dayT;

                        SqlCommand produce_mminfo = new SqlCommand(@"select * from Members where mm_act=@mm_act;", cnSQL);
                        produce_mminfo.Parameters.Add(new SqlParameter("@mm_act", tb_act.Text));
                        SqlDataReader produce_mminfo_reader = produce_mminfo.ExecuteReader();
                        while (produce_mminfo_reader.Read())
                        {
                            Session["identity"] = produce_mminfo_reader["mm_who"].ToString();
                            Session["user"] = produce_mminfo_reader["mm_act"].ToString();
                            Session["mmid"] = produce_mminfo_reader["mm_id"].ToString();
                            Session["mmphone"] = produce_mminfo_reader["mm_phone"].ToString();
                            Session["mmadr"] = produce_mminfo_reader["mm_adr"].ToString();
                            Session["notice"] = produce_mminfo_reader["mm_notice"].ToString();

                        }

                        if (Session["identity"].ToString() == "1") { Session["notice"] = "Y"; };


                        cnSQL.Close();
                        Response.Redirect("Default.aspx");

                    }
                    else
                    {
                        cnSQL.Close();
                        lb_error.Text = "密碼錯誤";
                    }
                }

            }
            catch (SqlException ex)
            {
                result = "Error:" + ex.Number;
            }
            cnSQL.Close();

        }
        else
        {
            cnSQL.Close();
            lb_error.Text = "帳號不可為空!";
        }


    }
    protected void btn_fgpwd_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=fgpw");

    }

    protected void btn_actvf_Click(object sender, EventArgs e)
    {



        string db_act = "";
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();

        //strSQL 為select form where insert
        String mm_act = "", sc_count, act, sc_count1, mm_mail = "", mm_pwd = "", sc_count2;
        mm_act = tb_actvf.Text;

        try
        {
            SqlCommand find_act = new SqlCommand(@"select mm_act from Members where mm_act=@mm_act;", cnSQL);
            find_act.Parameters.Add(new SqlParameter("@mm_act", mm_act));
            db_act = find_act.ExecuteScalar().ToString();
            //




            if (mm_act == db_act)
            {

                try
                {

                    SqlCommand find_mm_mail = new SqlCommand(@"select mm_mail from Members where mm_act=@mm_act;", cnSQL);
                    find_mm_mail.Parameters.Add(new SqlParameter("@mm_act", mm_act));
                    string m_mail = find_mm_mail.ExecuteScalar().ToString();
                    MailMessage msg = new MailMessage();
                    msg.To.Add(m_mail);
                    //msg.To.Add("b@b.com");可以發送給多人
                    //msg.CC.Add("c@c.com");
                    //msg.CC.Add("c@c.com");可以抄送副本給多人 
                    //這裡可以隨便填，不是很重要
                    msg.From = new MailAddress("chu104ands171206@gmail.com", "FirstSight衣見鍾情服務團隊", System.Text.Encoding.UTF8);
                    /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                    msg.Subject = "FirstSight - 身分驗證信";//郵件標題
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼

                    /*驗證信*/
                    string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                    int passwordLength = 8;//密碼長度
                    char[] chars = new char[passwordLength];
                    Random rd = new Random();

                    for (int j = 0; j < passwordLength; j++)
                    {
                        //allowedChars -> 這個String ，隨機取得一個字，丟給chars[i]
                        chars[j] = allowedChars[rd.Next(0, allowedChars.Length)];
                    }

                    string vfkey = new string(chars);

                    /*驗證信*/
                    //Random ram = new Random();//亂數種子
                    //    int i = ram.Next(0, 100000);//回傳0-99的亂數
                    //    string ii = Convert.ToString(i);



                    msg.Body = "親愛的會員您好！感謝您使用FirstSight！您正在進行忘記密碼服務，本次請求的驗證碼為：" + vfkey + "請在驗證碼輸入框輸入此驗證碼完成身分驗證。如果您不執行這項服務，請忽略此郵件，由此給您帶來的不便請諒解！FirstSight 服務團隊!"; //郵件內容
                    msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                                                                 //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
                    msg.IsBodyHtml = true;//是否是HTML郵件 
                                          //msg.Priority = MailPriority.High;//郵件優先級 

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("chu104ands171206@gmail.com", "******"); //這裡要填正確的帳號跟密碼
                    client.Host = "smtp.gmail.com"; //設定smtp Server
                    client.Port = 25; //設定Port
                    client.EnableSsl = true; //gmail預設開啟驗證
                    client.Send(msg); //寄出信件
                    client.Dispose();
                    msg.Dispose();
                    div_vfkey.Visible = true;
                    btn_pwchange.Visible = true;
                    tb_actvf.Enabled = false;
                    lb_rig.Visible = false;
                    btn_actvf.Visible = false;
                    Session["vfkey"] = vfkey;
                    cnSQL.Close();

                    //SqlCommand updatepf = new SqlCommand(@"UPDATE Members SET mm_pwd=@mm_pwd where mm_act=@mm_act", cnSQL);
                    //updatepf.Parameters.Add(new SqlParameter("@mm_act", sc_count));
                    //updatepf.Parameters.Add(new SqlParameter("@mm_pwd", ii));
                    //updatepf.ExecuteNonQuery();
                    //updatepf.Dispose();
                    //cnSQL.Close();
                    //Response.Redirect("login.aspx");

                }
                catch (Exception ex)
                {

                }
            }
        }
        catch { lb_rig.Visible = true; cnSQL.Close(); }






    }
    protected void btn_pwchange_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        if (tb_vfkey.Text == Session["vfkey"].ToString())
        {
            SqlCommand find_mm_mail = new SqlCommand(@"select mm_mail from Members where mm_act=@mm_act;", cnSQL);
            find_mm_mail.Parameters.Add(new SqlParameter("@mm_act", tb_actvf.Text));
            string m_mail = find_mm_mail.ExecuteScalar().ToString();
            /*新密碼*/
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            int passwordLength = 6;//密碼長度
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int j = 0; j < passwordLength; j++)
            {
                //allowedChars -> 這個String ，隨機取得一個字，丟給chars[i]
                chars[j] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            string npw = new string(chars);

            /*新密碼*/
            SqlCommand updatepf = new SqlCommand(@"UPDATE Members SET mm_pwd=@mm_pwd where mm_act=@mm_act", cnSQL);
            updatepf.Parameters.Add(new SqlParameter("@mm_act", tb_actvf.Text));
            updatepf.Parameters.Add(new SqlParameter("@mm_pwd", npw));
            updatepf.ExecuteNonQuery();
            updatepf.Dispose();


            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(m_mail);

                //這裡可以隨便填，不是很重要
                msg.From = new MailAddress("chu104ands171206@gmail.com", "FirstSight衣見鍾情服務團隊", System.Text.Encoding.UTF8);
                /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                msg.Subject = "FirstSight - 新密碼";//郵件標題
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼





                msg.Body = "親愛的會員您好！感謝您使用FirstSight！您正在進行忘記密碼服務，您的新密碼為：" + npw + "請盡快登入修改密碼。如果您不執行這項服務，請忽略此郵件，由此給您帶來的不便請諒解！FirstSight 服務團隊!"; //郵件內容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                                                             //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("chu104ands171206@gmail.com", "android1206"); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send(msg); //寄出信件
                client.Dispose();
                msg.Dispose();
                div_vfkey.Visible = true;
                btn_pwchange.Visible = true;
                tb_actvf.Enabled = false;
                lb_rig.Visible = false;

                cnSQL.Close();


            }
            catch
            {
                cnSQL.Close();
            }


            //Response.Redirect("?page=login");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('新密碼已寄到信箱囉！登入後請到個人訊息修改密碼!');", true);
        }
        else { lb_vferror.Visible = true; }
    }

    protected void btn_backatvf_Click(object sender, EventArgs e)
    {
        Response.Redirect("?page=login");
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
        //if (tb_act.Text != "")
        //{

        //    String SQL = "Select a_pwd FROM administrator WHERE a_acc = '" + tb_act.Text + "'";

        //    QDBII oDB = new QDBII();

        //    String DBPWD = oDB.OneSQL(SQL);

        //    if (tb_pwd.Text == DBPWD)
        //    {

        //        //Set Cookie and forward

        //        Response.Cookies["user"].Value = Server.UrlEncode(tb_act.Text);
        //        DateTime dayT = DateTime.Today.AddDays(1);
        //        Response.Cookies["user"].Expires = dayT;



        //        Server.Transfer("/index.aspx");

        //    }
        //    else
        //    {
        //        lb_error.Text = "密碼錯誤";
        //    }

        //}
        //else
        //{
        //    lb_error.Text = "帳號不可為空!";
        //}
    }


    protected void btn_checkout_Click(object sender, EventArgs e)
    {
        string odrphone, pickadr,odrnote;
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string mm_id = Session["mmid"].ToString();
        DateTime dt = DateTime.Now;
        string payment = "";
        if (rbt_comepay.Checked == false && rbt_bank.Checked == false ) { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未選擇付款方式!');", true); return; }
        if ((tb_pickadr.Text == " "|| tb_pickadr.Text == ""|| tb_pickadr.Text == null) ||(tb_pickadr.Text=="" && ckb_takemmadr.Checked == false )|| (tb_pickadr.Text==" "&& ckb_takemmadr.Checked == false)|| (tb_odrphone.Text == "" && ckb_takemmphone.Checked == false)|| (tb_odrphone.Text == " " && ckb_takemmphone.Checked == false))
        { ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('結帳資料不完整!');", true); return; }
        else
        {
            if (rbt_comepay.Checked) { payment = "貨到付款"; }
            if (rbt_bank.Checked) { payment = "銀行轉帳"; }
        }
        if(ckb_takemmadr.Checked == true)
        {
            pickadr = Session["mmadr"].ToString();          
        }
        else { pickadr = tb_pickadr.Text; }
        if (ckb_takemmphone.Checked == true)
        {
            odrphone = Session["mmphone"].ToString();         
        }
        else { odrphone = tb_odrphone.Text; }
        if (odr_message.Value==""|| odr_message.Value == "") { odrnote = "無"; } else { odrnote = odr_message.Value; }
        string total=Request.Params["f_total"];
        
        if (total == "0"|| total == ""|| total == " ") { total = temp_total; }

        string sel_text = DropDownList1.SelectedItem.Text;
        SqlCommand newod= new SqlCommand(@"Insert Into Orders(od_id,od_mm_id,od_total,od_orderdate,od_paymant,od_delivery,od_d_address,od_phone,od_note,od_done) Values(@od_id,@od_mm_id,@od_total,@od_orderdate,@od_paymant,@od_delivery,@od_d_address,@od_phone,@od_note,@od_done) ;", cnSQL);

        newod.Parameters.Add(new SqlParameter("@od_id", DBNull.Value));
        newod.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));
        newod.Parameters.Add(new SqlParameter("@od_total", total));
        newod.Parameters.Add(new SqlParameter("@od_orderdate", dt.ToShortDateString()));
        newod.Parameters.Add(new SqlParameter("@od_paymant", payment));
        newod.Parameters.Add(new SqlParameter("@od_delivery", sel_text));
        newod.Parameters.Add(new SqlParameter("@od_d_address", pickadr));
        newod.Parameters.Add(new SqlParameter("@od_phone", odrphone));
        newod.Parameters.Add(new SqlParameter("@od_note", odrnote));
        newod.Parameters.Add(new SqlParameter("@od_done", "N"));
        //Response.Redirect("?page=login" + mm_id + "," + total + "," + dt.ToShortTimeString() + "," + payment + "," + sel_text + "," + tb_pickadr.Text + "," + tb_odrphone.Text+ ","+odr_message.Value);
        newod.ExecuteNonQuery();
        newod.Dispose();
        

        //  [od_id]
        //,[od_mm_id]
        //,[od_total]
        //,[od_orderdate]
        //,[od_paymant]
        //,[od_delivery]
        //,[od_d_address]
        //,[od_phone]
        //,[od_note]
        //,[od_done]

        SqlCommand produce_order = new SqlCommand(@"select * from Shopping_Carts where sc_mm_id=@sc_mm_id;", cnSQL);

        produce_order.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));
        
        
        

        SqlDataReader produce_order_reader = produce_order.ExecuteReader();



        while (produce_order_reader.Read())
        {
            string od_id = "";
            SqlConnection cnSQL2 = new SqlConnection(strConnString);
            cnSQL2.Open();
            SqlCommand find_od_id = new SqlCommand(@"select MAX(od_id) from Orders where od_mm_id=@od_mm_id;", cnSQL2);
            find_od_id.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));
            od_id = find_od_id.ExecuteScalar().ToString();
            try
            {
                string[] amount = (string[])Session["N" + produce_order_reader["sc_cd_id"]];
                SqlCommand newodd = new SqlCommand(@"Insert Into Order_detail(odd_od_id,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount) Values(@odd_od_id,@odd_cd_id,@odd_s_amount,@odd_m_amount,@odd_l_amount,@odd_xl_amount);", cnSQL2);

                newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                newodd.Parameters.Add(new SqlParameter("@odd_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                newodd.Parameters.Add(new SqlParameter("@odd_s_amount", amount[0]));
                newodd.Parameters.Add(new SqlParameter("@odd_m_amount", amount[1]));
                newodd.Parameters.Add(new SqlParameter("@odd_l_amount", amount[2]));
                newodd.Parameters.Add(new SqlParameter("@odd_xl_amount", amount[3]));
                newodd.ExecuteNonQuery();
                newodd.Dispose();
                int hotcd;

                SqlCommand find_hotcd = new SqlCommand(@"select count(*) from Hot_saled where hot_cd_id=@hot_cd_id;", cnSQL2);
                find_hotcd.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                hotcd = Convert.ToInt32(find_hotcd.ExecuteScalar().ToString());


                int s_amount = Convert.ToInt32(amount[0]);
                int m_amount = Convert.ToInt32(amount[1]);
                int l_amount = Convert.ToInt32(amount[2]);
                int xl_amount = Convert.ToInt32(amount[3]);
                int all_amount = s_amount + m_amount + l_amount + xl_amount;
                int pe_all_amount = all_amount;
                if (hotcd == 1)
                {
                    int hotcount;
                    SqlCommand find_hotcount = new SqlCommand(@"select hot_count from Hot_saled where hot_cd_id=@hot_cd_id;", cnSQL2);
                    find_hotcount.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotcount = Convert.ToInt32(find_hotcount.ExecuteScalar().ToString());

                    SqlCommand hotsaleupdate = new SqlCommand(@"Update Hot_saled SET hot_count=@hot_count where hot_cd_id=@hot_cd_id;", cnSQL2);
                    all_amount = hotcount + all_amount;
                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    hotsaleupdate.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotsaleupdate.Parameters.Add(new SqlParameter("@hot_count", all_amount));

                    hotsaleupdate.ExecuteNonQuery();
                    hotsaleupdate.Dispose();
                }
                else
                {

                    SqlCommand hotsaleadd = new SqlCommand(@"Insert Into Hot_saled(hot_cd_id,hot_count) Values(@hot_cd_id,@hot_count);", cnSQL2);

                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    hotsaleadd.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotsaleadd.Parameters.Add(new SqlParameter("@hot_count", all_amount));

                    hotsaleadd.ExecuteNonQuery();
                    hotsaleadd.Dispose();
                }
                SqlCommand find_cdinpe = new SqlCommand(@"select count(*) from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                find_cdinpe.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                string cdinpe = find_cdinpe.ExecuteScalar().ToString();
                if (cdinpe != "0")
                {
                    int pecount;
                    SqlCommand find_pecount = new SqlCommand(@"select pe_count from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                    find_pecount.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    pecount = Convert.ToInt32(find_pecount.ExecuteScalar().ToString());

                    SqlCommand pecountupdate = new SqlCommand(@"Update Promotion_events SET pe_count=@pe_count where pe_cd_id=@pe_cd_id;", cnSQL2);
                    pe_all_amount = pecount + pe_all_amount;
                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    pecountupdate.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    pecountupdate.Parameters.Add(new SqlParameter("@pe_count", pe_all_amount));

                    pecountupdate.ExecuteNonQuery();
                    pecountupdate.Dispose();
                }



                cnSQL2.Close();
            }
            catch
            {
                string[] amount = (string[])Session[produce_order_reader["sc_cd_id"].ToString()];
                SqlCommand newodd = new SqlCommand(@"Insert Into Order_detail(odd_od_id,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount) Values(@odd_od_id,@odd_cd_id,@odd_s_amount,@odd_m_amount,@odd_l_amount,@odd_xl_amount);", cnSQL2);

                newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                newodd.Parameters.Add(new SqlParameter("@odd_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                newodd.Parameters.Add(new SqlParameter("@odd_s_amount", amount[0]));
                newodd.Parameters.Add(new SqlParameter("@odd_m_amount", amount[1]));
                newodd.Parameters.Add(new SqlParameter("@odd_l_amount", amount[2]));
                newodd.Parameters.Add(new SqlParameter("@odd_xl_amount", amount[3]));
                newodd.ExecuteNonQuery();
                newodd.Dispose();
                
                int hotcd;

                SqlCommand find_hotcd = new SqlCommand(@"select count(*) from Hot_saled where hot_cd_id=@hot_cd_id;", cnSQL2);
                find_hotcd.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                hotcd = Convert.ToInt32(find_hotcd.ExecuteScalar().ToString());


                int s_amount = Convert.ToInt32(amount[0]);
                int m_amount = Convert.ToInt32(amount[1]);
                int l_amount = Convert.ToInt32(amount[2]);
                int xl_amount = Convert.ToInt32(amount[3]);
                int all_amount = s_amount + m_amount + l_amount + xl_amount;
                int pe_all_amount = all_amount;
                if (hotcd == 1)
                {
                    int hotcount;
                    SqlCommand find_hotcount = new SqlCommand(@"select hot_count from Hot_saled where hot_cd_id=@hot_cd_id;", cnSQL2);
                    find_hotcount.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotcount = Convert.ToInt32(find_hotcount.ExecuteScalar().ToString());

                    SqlCommand hotsaleupdate = new SqlCommand(@"Update Hot_saled SET hot_count=@hot_count where hot_cd_id=@hot_cd_id;", cnSQL2);
                    all_amount = hotcount + all_amount;
                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    hotsaleupdate.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotsaleupdate.Parameters.Add(new SqlParameter("@hot_count", all_amount));

                    hotsaleupdate.ExecuteNonQuery();
                    hotsaleupdate.Dispose();
                }
                else
                {

                    SqlCommand hotsaleadd = new SqlCommand(@"Insert Into Hot_saled(hot_cd_id,hot_count) Values(@hot_cd_id,@hot_count);", cnSQL2);

                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    hotsaleadd.Parameters.Add(new SqlParameter("@hot_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    hotsaleadd.Parameters.Add(new SqlParameter("@hot_count", all_amount));

                    hotsaleadd.ExecuteNonQuery();
                    hotsaleadd.Dispose();
                }
                SqlCommand find_cdinpe = new SqlCommand(@"select count(*) from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                find_cdinpe.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                string cdinpe = find_cdinpe.ExecuteScalar().ToString();
                if (cdinpe != "0")
                {
                    int pecount;
                    SqlCommand find_pecount = new SqlCommand(@"select pe_count from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL2);
                    find_pecount.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    pecount = Convert.ToInt32(find_pecount.ExecuteScalar().ToString());

                    SqlCommand pecountupdate = new SqlCommand(@"Update Promotion_events SET pe_count=@pe_count where pe_cd_id=@pe_cd_id;", cnSQL2);
                    pe_all_amount = pecount + pe_all_amount;
                    // newodd.Parameters.Add(new SqlParameter("@odd_od_id", od_id));
                    pecountupdate.Parameters.Add(new SqlParameter("@pe_cd_id", produce_order_reader["sc_cd_id"].ToString()));
                    pecountupdate.Parameters.Add(new SqlParameter("@pe_count", pe_all_amount));

                    pecountupdate.ExecuteNonQuery();
                    pecountupdate.Dispose();
                }



                cnSQL2.Close();

            }

        }

        produce_order_reader.Close();
        SqlCommand deleteww = new SqlCommand(@"delete  from Wishing_Wells where ww_cd_id in ( select sc_cd_id from Shopping_Carts where sc_mm_id=@sc_mm_id) and ww_mm_id=@ww_mm_id;", cnSQL);
        deleteww.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));
        deleteww.Parameters.Add(new SqlParameter("@ww_mm_id", mm_id));
        deleteww.ExecuteNonQuery();
        deleteww.Dispose();
        SqlCommand deletesc = new SqlCommand(@"delete from Shopping_Carts where sc_mm_id=@sc_mm_id;", cnSQL);
        deletesc.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));
        deletesc.ExecuteNonQuery();
        deletesc.Dispose();
        cnSQL.Close();
        Response.Redirect("?page=ckout");

        
        

        //Response.Redirect("?page=login" + test[0] + test[1] + test[2] + test[3]);
    }
    protected void btn_pfpwdsubmit_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        string mm_id = Session["mmid"].ToString();
        if (tb_pfoldpw.Text != null && tb_pfoldpw.Text != "" && tb_pfnewpw.Text != null && tb_pfnewpw.Text != "" && tb_pfnewpwck.Text != null && tb_pfnewpwck.Text != "")
        {
            cnSQL.Open();
            string result_pwdck;
            SqlCommand ck_mm_pwd = new SqlCommand(@"select count(mm_pwd) from Members where mm_id=@mm_id and mm_pwd=@mm_pwd;", cnSQL);
            ck_mm_pwd.Parameters.Add(new SqlParameter("@mm_id", mm_id));
            ck_mm_pwd.Parameters.Add(new SqlParameter("@mm_pwd", tb_pfoldpw.Text));
            result_pwdck = ck_mm_pwd.ExecuteScalar().ToString();
            if (result_pwdck == "1")
            {
                
                if (tb_pfnewpw.Text == tb_pfnewpwck.Text)
                {
                    if (tb_pfoldpw.Text== tb_pfnewpw.Text)
                    {
                        p_error.InnerText = "新密碼不可與舊密碼相同"; p_error.Visible = true; cnSQL.Close();
                    }
                    else
                    {
                        SqlCommand updatepwd = new SqlCommand(@"UPDATE Members SET mm_pwd=@mm_pwd where mm_id=@mm_id;", cnSQL);


                        updatepwd.Parameters.Add(new SqlParameter("@mm_id", mm_id));
                        updatepwd.Parameters.Add(new SqlParameter("@mm_pwd", tb_pfnewpw.Text));
                        updatepwd.ExecuteNonQuery();
                        updatepwd.Dispose();
                        cnSQL.Close();
                        p_error.Visible = false;
                        p_error.InnerText = "";

                        Response.Redirect("?page=profile");
                    }
                    
                }
                else { p_error.InnerText = "新密碼與新密碼確認不符!"; p_error.Visible = true; cnSQL.Close(); }
            }
            else
            {
                p_error.InnerText = "舊密碼輸入錯誤!"; p_error.Visible = true; cnSQL.Close();
                
            }

        }

        else { p_error.InnerText = "有欄位沒填!"; p_error.Visible = true; cnSQL.Close(); }

    }
    public string pe_datetime()
    {
        try
        {
         DateTime dt = Convert.ToDateTime(Session["pemain_time"].ToString());
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch { return "NNONNO"; }

    }
    //public void shutdown_pemain()
    //{
    //    try
    //    {
    //        SqlConnection cnSQL = new SqlConnection(strConnString);
    //        cnSQL.Open();
    //        SqlCommand shutdownpe = new SqlCommand(@"UPDATE Promotion_events SET pe_status=@pe_status where pe_main=@pe_main;", cnSQL);
    //        shutdownpe.Parameters.Add(new SqlParameter("@pe_status", "N"));
    //        shutdownpe.Parameters.Add(new SqlParameter("@pe_main", "Y"));
    //        shutdownpe.ExecuteNonQuery();
    //        shutdownpe.Dispose();
    //        cnSQL.Close();
    //        Session["pemain_time"] = null;
            
    //    }
    //    catch { }


    //}

    public void shutdown_pe(int no,string pe_ex_date)
    {
        //try
        //{
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand shutdownpe = new SqlCommand(@"UPDATE Promotion_events SET pe_status=@pe_status where pe_expiring_date=@pe_expiring_date;", cnSQL);
            shutdownpe.Parameters.Add(new SqlParameter("@pe_status", "N"));
            shutdownpe.Parameters.Add(new SqlParameter("@pe_expiring_date", pe_ex_date));
            shutdownpe.ExecuteNonQuery();
            shutdownpe.Dispose();
            cnSQL.Close();
            //Session["petime" + no] = null;

        //}
        //catch { }
        //Response.Redirect(pe_ex_date + "活動結束");

    }

    protected void Timer_pe_Tick(object sender, EventArgs e)
    {

        
        DateTime dt_now = DateTime.Now;

        for (int i = 1; i <= pe_total; i++)
        {
            DateTime dt = Convert.ToDateTime(Session["petime"+i].ToString());
            DateTime dt2= Convert.ToDateTime(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            TimeSpan ts = new TimeSpan(dt2.Ticks-dt_now.Ticks);
            
            double countdown = ts.TotalSeconds;
            //Response.Redirect(  "活動結束"+ countdown+","+i+","+ dt2.ToString());
            if (countdown <= 0) { shutdown_pe(i,dt.ToString("yyyy-MM-dd HH:mm:ss")); }
            
        }
    }

    protected void ckb_takemmadr_CheckedChanged(object sender, EventArgs e)
    {
        string mm_id = Session["mmid"].ToString();
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string pickadr = "";
        if (ckb_takemmadr.Checked == true)
        {
            SqlCommand find_mm_adr = new SqlCommand(@"Select mm_adr From Members Where mm_id=@mm_id;", cnSQL);
            find_mm_adr.Parameters.Add(new SqlParameter("@mm_id", mm_id));
            pickadr = find_mm_adr.ExecuteScalar().ToString();
            tb_pickadr.Text = pickadr;
            cnSQL.Close();

        }
        else
        {
            tb_pickadr.Text = "";
            cnSQL.Close();
        }
    }

    protected void ckb_takemmphone_CheckedChanged(object sender, EventArgs e)
    {
        string mm_id = Session["mmid"].ToString();
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        string odrphone;
        if (ckb_takemmphone.Checked == true)
        {
            SqlCommand find_mm_phone = new SqlCommand(@"Select mm_phone From Members Where mm_id=@mm_id;", cnSQL);
            find_mm_phone.Parameters.Add(new SqlParameter("@mm_id", mm_id));
            odrphone = find_mm_phone.ExecuteScalar().ToString();
            tb_odrphone.Text = odrphone;
            cnSQL.Close();
        }
        else
        {
            tb_odrphone.Text = "";
            cnSQL.Close();
        }
    }
    protected void btn_addhave_Click(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        SqlConnection cnSQL = new SqlConnection(strConnString);

        if (tb_cdhavetag.Text != "" && tb_cdhavetag.Text != " " && tb_cdhavetag.Text != null && lb_cdhavetag.Text != "")
        {
            cnSQL.Open();
            foreach (ListItem listitem in ckblist_tagname.Items)
            {
                if (listitem.Selected)
                {
                    SqlCommand ck_have = new SqlCommand(@"select count(*) from Have where have_cd_id=@have_cd_id and have_tag_id=@have_tag_id;", cnSQL);
                    ck_have.Parameters.Add(new SqlParameter("@have_cd_id", tb_cdhavetag.Text));
                    ck_have.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                    int result_ckhave = Convert.ToInt32(ck_have.ExecuteScalar().ToString());

                    if (result_ckhave == 0)
                    {
                        SqlCommand newhave = new SqlCommand(@"Insert Into Have(have_tag_id,have_cd_id,have_date) 
                                                          Values(@have_tag_id,@have_cd_id,@have_date) ;", cnSQL);
                        newhave.Parameters.Add(new SqlParameter("@have_tag_id", listitem.Value));
                        newhave.Parameters.Add(new SqlParameter("@have_cd_id", tb_cdhavetag.Text));
                        newhave.Parameters.Add(new SqlParameter("@have_date", dt.ToShortDateString()));
                        newhave.ExecuteNonQuery();
                        newhave.Dispose();
                    }

                }
            }
            cnSQL.Close();
            cnSQL.Dispose();
            Response.Redirect("?page=newtag");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('未輸入商品編號 或 未選擇Tag!');", true);
            return;
        }

    }

    protected void ckblist_tagname_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tag_name = "";
        string tag_name2 = "";
        int tag_count = 0;
        foreach (ListItem listitem in ckblist_tagname.Items)
        {
            if (listitem.Selected)
            {

                tag_count += 1;
                if (tag_count > 7)
                {
                    listitem.Selected = false;
                    foreach (ListItem listitem2 in ckblist_tagname.Items)
                    {
                        if (listitem2.Selected)
                        {
                            tag_name2 += listitem2.Text + ",";
                            string show_tn2 = tag_name2.Substring(0, tag_name2.Length - 1);
                            lb_cdhavetag.Text = show_tn2;
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('不能超過7個Tag!');", true);
                    return;
                }
                tag_name += listitem.Text + ",";
            }

            if (tag_name == "")
            {
                lb_cdhavetag.Visible = false;
                lb_cdhavetag.Text = "";
            }
            else
            {

                lb_cdhavetag.Visible = true;
                string show_tn = tag_name.Substring(0, tag_name.Length - 1);
                lb_cdhavetag.Text = show_tn;
            }

        }

    }

    protected void ckblist_adm_cd_tag_SelectedIndexChanged(object sender, EventArgs e)
    {
        int tag_count = 0;

        if (lb_adm_cdtag.Text != "無")
        {
            string[] tagArray = lb_adm_cdtag.Text.Split(',');
            tag_count = tagArray.GetLength(0);

        }

        foreach (ListItem listitem in ckblist_adm_cd_tag.Items)
        {
            if (listitem.Selected)
            {

                tag_count += 1;
                if (tag_count > 7)
                {
                    listitem.Selected = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('不能超過7個Tag!');", true);
                    return;
                }
            }


        }
    }

    protected void ckblist_newcd_cdtag_SelectedIndexChanged(object sender, EventArgs e)
    {
        int tag_count = 0;
        foreach (ListItem listitem in ckblist_newcd_cdtag.Items)
        {
            if (listitem.Selected)
            {

                tag_count += 1;
                if (tag_count > 7)
                {
                    listitem.Selected = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('不能超過7個Tag!');", true);
                    return;
                }
            }


        }
    }

}
