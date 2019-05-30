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
        SqlDataSource_mm.SelectCommand = "SELECT * from Members where mm_who='0';"; /*記得刪掉*/
        SqlDataSource_cd.SelectCommand = "SELECT * from Commodities;";
        SqlDataSource_od.SelectCommand = "SELECT * from Orders;";
        SqlDataSource_oddetail.SelectCommand = "select od_no,od_id,od_mm_id,odd_cd_id,odd_s_amount,odd_m_amount,odd_l_amount,odd_xl_amount from Orders o join Order_detail odd on o.od_id=odd.odd_od_id where od_id='od00000001';";
        if (Session["identity"] != null)
        {
            if (Session["identity"].ToString() == "0")
            {
                
                
                string mm_id,sc_count,od_count;
                string user_name = Session["user"].ToString();
                SqlConnection cnSQL2 = new SqlConnection(strConnString);
                cnSQL2.Open();

                mm_id = Session["mmid"].ToString();
                SqlCommand find_sc_cd_num = new SqlCommand(@"select count(*) from Shopping_Carts where sc_mm_id=@sc_mm_id;", cnSQL2);
                find_sc_cd_num.Parameters.Add(new SqlParameter("@sc_mm_id", mm_id));
                sc_count = find_sc_cd_num.ExecuteScalar().ToString();

                SqlCommand find_od_num = new SqlCommand(@"select count(*) from Orders where od_mm_id=@od_mm_id;", cnSQL2);
                find_od_num.Parameters.Add(new SqlParameter("@od_mm_id", mm_id));
                od_count = find_od_num.ExecuteScalar().ToString();


                if (sc_count == "0") { cart_table.Visible = false; cart_noitem.Visible = true; }
                else { cart_table.Visible = true; cart_noitem.Visible = false; }

                if (od_count == "0") { order_table.Visible = false; order_noitem.Visible = true; }
                else { order_table.Visible = true; order_noitem.Visible = false; }


                start_display.Visible = true;
                lb_hello.Text = "親愛的" + user_name + "會員,您好!";
                //lb_hello.Text = Session["user"].ToString();
                lb_hello.Visible = true;
                
                checkout_items.InnerText = sc_count;

                nav_nologin.Visible = false;
                nav_administrator.Visible = false;
                nav_user.Visible = true;
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
                    if (pf_reader["mm_notice"].ToString() == "Y") { ckb_notification.Checked = true; }
                    else { ckb_notification.Checked = false; }
                }
                   
                pf_reader.Close();


                /*PROFILE  END*/

                /*訂單START*/

                String outsideorder = "";
                Label insideorder = new Label();//產生多次

                //strSQLorder = "select * from Members mb left join Orders od on mb.mm_id=od.od_mm_id where mm_act='"; 
                //strSQLorder +=  Session["user"].ToString() + "' order by od.od_orderdate desc ;";


                SqlCommand orderproduce = new SqlCommand(@"select * from  Orders where od_mm_id=@od_mm_id order by od_orderdate desc ;", cnSQL2);
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
                    a++;
                    insideorder.Text += "<tbody><tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                    insideorder.Text += "<td class=\"text-center\" data-title=\"NO\">" + a + "</td>";
                    insideorder.Text += "<td class=\"temp_pname product-name\" data-title=\"訂單日期\">" + readerorder["od_orderdate"] + "</td>";
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
                cart_outside += "<th class=\"product-remove\">&nbsp;</th><th class=\"product-thumbnail\">商品圖片</th><th class=\"product-name\">商品名稱</th>";
                cart_outside += "<th class=\"product-price\">價格</th><th class=\"product-quantity\">數量</th><th class=\"product-subtotal\">單項總額</th></tr></thead><tbody>";

                cart_outside2 += "<div class=\"row blogs_container\"><div class=\"col-lg-8 blog_item_col\"><div class=\"blog_item\"><div class=\"cart-collaterals\"><div class=\"cart_totals text-center\">";
                cart_outside2 += "<h2>優惠折抵</h2><table class=\"shop_table shop_table_responsive cart woocommerce-cart-form__contents\" cellspacing=\"0\"><thead>";
                cart_outside2 += "<tr><th class=\"product-name\">商品圖片</th><th class=\"product-name\">商品名稱</th><th class=\"product-price\">原價</th><th class=\"product-quantity\">活動優惠</th><th class=\"product-subtotal\">折扣</th></tr></thead><tbody>";


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

                    if (cart_reader["pe_discount"].ToString() != "" && cart_reader["pe_discount"].ToString() != null)
                    {
                        sc_num = Convert.ToInt32(cart_reader["sc_num"].ToString());

                        pe_discount = Convert.ToDouble(cart_reader["pe_discount"].ToString());

                        cd_price = Convert.ToInt32(cart_reader["cd_price"].ToString());
                        event_price = Convert.ToInt32(sc_num * cd_price * pe_discount);
                        event_price_one = Convert.ToInt32( cd_price * pe_discount);
                        savemoney = cd_price - event_price_one;


                        itemprice = Convert.ToInt32(sc_num * cd_price);
                        /*偷做 活動優惠INSIDE START*/
                        cart_inside2.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside2.Text += "<td class=\"product-thumbnail\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside2.Text += "<td class=\"product-name\" data-title=\"商品名稱\"><span>"+ cart_reader["cd_name"] + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$"+ cd_price + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-price\" data-title=\"活動優惠\"><span class=\"woocommerce-Price-amount amount\">$"+ event_price_one + "</span></td>";
                        cart_inside2.Text += "<td class=\"product-subtotal\" data-title=\"折扣\"><span class=\"woocommerce-Price-amount amount\">$"+ savemoney + "</span></td></tr>";
                        /*偷做 活動優惠INSIDE END*/

                        cart_inside.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside.Text += "<td class=\"product-remove\"><a class=\"remove\" href=\"#\" id=\""+ cart_reader["sc_cd_id"] + "\">×</a> </td>";
                        cart_inside.Text += "<td class=\"product-thumbnail\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside.Text += "<td class=\"temp_pname product-name\" data-title=\"商品名稱\"><span>" + cart_reader["cd_name"] + "</span> </td>";
                        cart_inside.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$" + cd_price + "</span></td>";
                        /*找SIZE START*/
                        if (cart_reader["sz_s"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\" id=\"" + cart_reader["sc_cd_id"] + "sminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            cart_size += "<span class=\"squantity_value\"  id=\"" + cart_reader["sc_cd_id"] + "svalue\">" + sc_num + "</span><span class=\"splus\" id=\""+ cart_reader["sc_cd_id"] + "splus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                            ck_first = 1;
                        }
                        if (cart_reader["sz_m"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">M</div><div style=\"float: right\"><span class=\"mminus\" id=\"" + cart_reader["sc_cd_id"] + "mminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">" + sc_num + "</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"mquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "mvalue\">0</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        if (cart_reader["sz_l"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">L</div><div style=\"float: right\"><span class=\"lminus\" id=\"" + cart_reader["sc_cd_id"] + "lminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">" + sc_num + "</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"lquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "lvalue\">0</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        if (cart_reader["sz_xl"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">XL</div><div style=\"float: right\"><span class=\"xlminus\" id=\"" + cart_reader["sc_cd_id"] + "xlminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">" + sc_num + "</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">0</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        /*找SIZE END*/
                        //輩分cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br><div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\"><i aria-hidden=\"true\" class=\"fa fa-minus\"></i></span><span class=\"squantity_value\">" + sc_num + "</span> <span class=\"splus\"><i aria-hidden=\"true\" class=\"fa fa-plus\"></i></span></div><br></td>";
                        cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br>" + cart_size + "<br></td>";
                        cart_inside.Text += "<td class=\"product-subtotal\" data-title=\"單項總額\"><span class=\"woocommerce-Price-amount amount \"id=\"" + cart_reader["sc_cd_id"] + "price\" style=\"text-decoration: line-through\">$" + itemprice + "</span> <span class=\"woocommerce-Price-amount amount cal_total\" style=\"color: red; display: block\"id=\"" + cart_reader["sc_cd_id"] + "discountprice\">$" + event_price + "</span></td>";
                        //cart_inside.Text += "";
                        cart_totalfee +=event_price;
                        cart_inside.Text = cart_inside.Text + "</tr>";
                    }
                    else
                    {
                        sc_num = Convert.ToInt32(cart_reader["sc_num"].ToString());
                        cd_price = Convert.ToInt32(cart_reader["cd_price"].ToString());
                        itemprice = Convert.ToInt32(sc_num * cd_price);


                        cart_inside.Text += "<tr class=\"woocommerce-cart-form__cart-item cart_item\">";
                        cart_inside.Text += "<td class=\"product-remove\"><a class=\"remove\" href=\"#\" id=\"" + cart_reader["sc_cd_id"] + "\">×</a> </td>";
                        cart_inside.Text += "<td class=\"product-thumbnail\"><a href=\"?pid=" + cart_reader["sc_cd_id"] + "\"><img width=\"255\" height=\"320\" src=\"images/clothes/" + cart_reader["sc_cd_id"] + ".jpg\" class=\"attachment-woocommerce_thumbnail size-woocommerce_thumbnail\" alt=\"\" /></a></td>";
                        cart_inside.Text += "<td class=\"temp_pname product-name\" data-title=\"商品名稱\"><span>" + cart_reader["cd_name"] + "</span> </td>";
                        cart_inside.Text += "<td class=\"product-price\" data-title=\"商品價格\"><span class=\"woocommerce-Price-amount amount\">$" + cd_price + "</span></td>";
                        /*找SIZE START*/
                        if (cart_reader["sz_s"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\" id=\"" + cart_reader["sc_cd_id"] + "sminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            cart_size += "<span class=\"squantity_value\" id=\"" + cart_reader["sc_cd_id"] + "svalue\">" + sc_num + "</span><span class=\"splus\" id=\"" + cart_reader["sc_cd_id"] + "splus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>";
                            ck_first = 1;
                        }
                        if (cart_reader["sz_m"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">M</div><div style=\"float: right\"><span class=\"mminus\" id=\"" + cart_reader["sc_cd_id"] + "mminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">" + sc_num + "</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"mquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "mvalue\">0</span><span class=\"mplus\" id=\"" + cart_reader["sc_cd_id"] + "mplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        if (cart_reader["sz_l"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">L</div><div style=\"float: right\"><span class=\"lminus\" id=\"" + cart_reader["sc_cd_id"] + "lminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">" + sc_num + "</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"lquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "lvalue\">0</span><span class=\"lplus\" id=\"" + cart_reader["sc_cd_id"] + "lplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        if (cart_reader["sz_xl"].ToString() == "Y")
                        {
                            cart_size += "<div style=\"float: left\">XL</div><div style=\"float: right\"><span class=\"xlminus\" id=\"" + cart_reader["sc_cd_id"] + "xlminus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span>";
                            if (ck_first == 0) { cart_size += "<span class=\"xlquantity_value\"id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">" + sc_num + "</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; ck_first = 1; }
                            else { cart_size += "<span class=\"xlquantity_value\" id=\"" + cart_reader["sc_cd_id"] + "xlvalue\">0</span><span class=\"xlplus\" id=\"" + cart_reader["sc_cd_id"] + "xlplus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div></br>"; }
                        }
                        /*找SIZE END*/
                        //輩分cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br><div style=\"float: left\">S</div><div style=\"float: right\"><span class=\"sminus\"><i aria-hidden=\"true\" class=\"fa fa-minus\"></i></span><span class=\"squantity_value\">" + sc_num + "</span> <span class=\"splus\"><i aria-hidden=\"true\" class=\"fa fa-plus\"></i></span></div><br></td>";
                        cart_inside.Text += "<td class=\"product-name\" data-title=\"數量\"><br>" + cart_size + "<br></td>";
                        cart_inside.Text += "<td class=\"product-subtotal\" data-title=\"單項總額\"><span class=\"woocommerce-Price-amount amount cal_total\" id=\"" + cart_reader["sc_cd_id"] + "price\">$" + itemprice + "</td>";          
                        cart_totalfee += itemprice;

                        cart_inside.Text =  cart_inside.Text + "</tr>";
                    }

                }
                cart_outside3 += "<div class=\"col-lg-4 blog_item_col\"><div class=\"blog_item\"><div class=\"cart-collaterals\"><div class=\"cart_totals text-center\">";
                cart_outside3 += "<h2>購物車總金額</h2><table class=\"shop_table shop_table_responsive\">";
                cart_outside3 += "<tr class=\"cart-subtotal\"><th>運費</th><td data-title=\"Subtotal\"><span class=\"woocommerce-Price-amount amount\">$0</span></td></tr>";
                cart_outside3 += "<tr class=\"order-total\"><th>總額</th><td data-title=\"Total\"><strong><span class=\"woocommerce-Price-amount amount\" id=\"final_price\">$"+cart_totalfee+"</span></strong></td></tr>";
                cart_outside3 += "</table></div></div></div></div></div>";

                cart_inside.Text = cart_outside+ cart_inside.Text + "</tbody></table>";
                cart_inside.Text += cart_outside2 + cart_inside2.Text + "</tr></tbody></table></div></div></div></div>";
                cart_inside.Text += cart_outside3;

                PH_cart.Controls.Add(cart_inside);
                cart_reader.Close();
                /*購物車END*/

                //許願池start
                String outsidewish = "";
                Label insidewish = new Label();//產生多次
                                               //cnSQL2.Open();
                                               //strSQLorder = "select * from Members mb left join Orders od on mb.mm_id=od.od_mm_id where mm_act='"; 
                                               //strSQLorder +=  Session["user"].ToString() + "' order by od.od_orderdate desc ;";


                SqlCommand orderwish = new SqlCommand(@"select * from  Wishing_Wells join Commodities cd on ww_cd_id = cd.cd_id where ww_mm_id = @ww_mm_id order by ww_wishdate desc;", cnSQL2);
                orderwish.Parameters.Add(new SqlParameter("@ww_mm_id", mm_id));

                SqlDataReader readerwish = orderwish.ExecuteReader();
                insidewish.Text = "";

                outsidewish += "<div class=\"row blogs_container\">";

                while (readerwish.Read())
                {
                    insidewish.Text += "<div class=\"col-lg-4 blog_item_col\"><div class=\"blog_item\">";
                    insidewish.Text += "<div class=\"blog_background\" style=\"background-image: url(images/clothes/" + readerwish["cd_id"] + ".jpg)\"></div>";
                    insidewish.Text += "<div class=\"blog_content d-flex flex-column align-items-center justify-content-center text-center\">";
                    insidewish.Text += "<h4 class=\"blog_title\">" + readerwish["cd_name"] + "</h4>";
                    insidewish.Text += "<span class=\"blog_meta\">" + "$" + "" + readerwish["cd_price"] + " </span>";
                    insidewish.Text += "<span class=\"blog_meta\">" + readerwish["ww_wishdate"] + "</span>";
                    insidewish.Text += "<a class=\"blog_more\" href=\"#\">去瞧瞧</a></div></div>";
                    insidewish.Text += "<div class=\"text-center\"ID=\"delete_wish\" runat=\"server\" ><a style=color:red;\">取消追蹤</a> </td></div></div>";
                    
                }
                insidewish.Text = outsidewish + insidewish.Text + "</div>";


                PH_wish.Controls.Add(insidewish);
                readerwish.Close();
                //許願池end

            }

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
                    adm_memberm_display.Visible = true;

                }
                if (admpage != null && admpage == "admcd")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_odm_display.Visible = false;
                    adm_cdm_display.Visible = true;

                }

                if (admpage != null && admpage == "admod")
                {
                    fslogo.Visible = false;
                    adm_memberm_display.Visible = false;
                    adm_cdm_display.Visible = false;
                    adm_odm_display.Visible = true;


                }
                
                try
                {
                    var getmm_no = Request.QueryString["mm_no"].ToString();
                    if (getmm_no != null)
                    {
                        fslogo.Visible = false;
                        adm_memberm_display.Visible = false;
                        adm_cdm_display.Visible = false;
                        adm_odm_display.Visible = false;
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


                            lb_adm_mmname.Text=mmdetail_reader["mm_name"].ToString();
                            tb_adm_mmname.Text = mmdetail_reader["mm_name"].ToString();

                            lb_adm_mmadr.Text = mmdetail_reader["mm_adr"].ToString();
                            tb_adm_mmadr.Text = mmdetail_reader["mm_adr"].ToString();

                            lb_adm_mmphone.Text = mmdetail_reader["mm_phone"].ToString();
                            tb_adm_mmphone.Text = mmdetail_reader["mm_phone"].ToString();

                            lb_adm_mmmail.Text = mmdetail_reader["mm_mail"].ToString();
                            tb_adm_mmmail.Text = mmdetail_reader["mm_mail"].ToString();

                            lb_adm_mmbd.Text = dt.ToString("yyyy/MM/dd");
                            tb_adm_mmbd.Text = lb_adm_mmbd.Text;

                            lb_adm_mmnotice.Text = mmdetail_reader["mm_notice"].ToString();
                            tb_adm_mmnotice.Text = mmdetail_reader["mm_notice"].ToString();

                            lb_adm_odnum.Text = od_count;

                        }
                        cnSQL3.Close();
                                        
                        mmdetail_reader.Close();

                    }
                } catch { }
                



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

        if (page != null && page=="scan")
		{
            fslogo.Visible = false;
            start_display.Visible = false;
            cart_display.Visible = false;
            wish_display.Visible = false;
            order_display.Visible = false;
            profile_display.Visible = false;
            register_display.Visible = false;
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
            register_display.Visible = true;



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
        if (Session["identity"] != null)
        {
            while (reader.Read())
            {
                double pe_discount = 0;
                if (reader["pe_discount"].ToString() != "" && reader["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader["pe_discount"].ToString()) * 10;
                }
                inside.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if (reader["pe_main"].ToString() == "N")
                {
                    inside.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }//sale 改折數 100-100*reader["pe_discount"]  = 20% off
                inside.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ reader["cd_id"]+"wish"+"\"></div>";
                inside.Text += "<div class=\"product_info\">";
                inside.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader["cd_id"] + "\">" + reader["cd_name"] + "</a></h6>";
                inside.Text += "<div class=\"product_price\">" + "$" + reader["cd_price"] + "</div></div></div></div></div>";
            }
        }
        else
        {
            while (reader.Read())
            {
                double pe_discount = 0;
                if (reader["pe_discount"].ToString() != "" && reader["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader["pe_discount"].ToString())*10;
                }
                
                inside.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                //inside.Text += "<div class=\"favorite favorite_left lbh\" id=\""+reader2["cd_id"]+"wish"+"\"></div>";??
                if (reader["pe_main"].ToString() == "N")
                {
                    inside.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                inside.Text += "<div class=\"product_info\">";
                inside.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader["cd_id"] + "\">" + reader["cd_name"] + "</a></h6>";
                inside.Text += "<div class=\"product_price\">" + "$" + reader["cd_price"] + "</div></div></div></div></div>";
            }
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


        strSQL1 = "select top 10* from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id";        
        cmdSQL1.CommandText = strSQL1;
        cnSQL.Open();
        SqlDataReader reader1 = cmdSQL1.ExecuteReader();
       
        //頭
        outside1 += "<div class=\"best_sellers\"><div class=\"container\"><div class=\"row\"><div class=\"col text-center\">";
        outside1 += "<div class=\"section_title new_arrivals_title\"><h2>熱銷商品</h2></div></div></div>";
        outside1 += "<div class=\"row\"><div class=\"col\"><div class=\"product_slider_container\"><div class=\"owl-carousel owl-theme product_slider\">";
        inside1.Text = "";


        if (Session["identity"] != null)
        {
            while (reader1.Read())
            {
                double pe_discount = 0;
                if (reader1["pe_discount"].ToString() != "" && reader1["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader1["pe_discount"].ToString()) * 10;
                }
                inside1.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside1.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader1["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if (reader1["pe_main"].ToString() == "N")
                {
                    inside1.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                inside1.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ reader1["cd_id"]+"wish2"+"\"></div>";
                inside1.Text += "<div class=\"product_info\">";
                inside1.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader1["cd_id"] + "\">" + reader1["cd_name"] + "</a></h6>";
                inside1.Text += "<div class=\"product_price\">" + "$" + reader1["cd_price"] + "</div></div></div></div></div>";
            }
        }
        else
        {
            while (reader1.Read())
            {
                double pe_discount = 0;
                if (reader1["pe_discount"].ToString() != "" && reader1["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(reader1["pe_discount"].ToString()) * 10;
                }
                inside1.Text += "<div class=\"owl-item product_slider_item\"><div class=\"product-item\"><div class=\"product discount\">";
                inside1.Text += "<div class=\"product_image\"><img src=\"images/clothes/" + reader1["cd_id"] + ".jpg\" alt=\"\"></div>"; /*圖片*/
                if (reader1["pe_main"].ToString() == "N")
                {
                    inside1.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                //inside1.Text += "<div class=\"favorite favorite_left lbh\" id=\""+reader2["cd_id"]+"wish"+"\"></div>";
                inside1.Text += "<div class=\"product_info\">";
                inside1.Text += "<h6 class=\"product_name\"><a href=\"?pid=" + reader1["cd_id"] + "\">" + reader1["cd_name"] + "</a></h6>";
                inside1.Text += "<div class=\"product_price\">" + "$" + reader1["cd_price"] + "</div></div></div></div></div>";
            }
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


            strSQL2 = "select * from Commodities where cd_id='";
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
 /*商品價格*/   inside2.Text += "<div class=\"product_price\">" +"$"+ readersingle["cd_price"] + "</div>";
 /*選擇尺寸*/   inside2.Text += "<div class=\"product_color\">";
                inside2.Text += "<span>選擇尺寸:</span><ul>";
                inside2.Text += "<li style=\"background: #e54e5d\"></li><li style=\"background: #252525\"></li><li style=\"background: #60b3f3\"></li></ul></div>";
/*加入購物車*/  inside2.Text += "<div class=\"quantity d-flex flex-column flex-sm-row align-items-sm-center\">";
                inside2.Text += "<span>數量:</span><div class=\"quantity_selector\">";
                inside2.Text += "<span class=\"minus\"><i class=\"fa fa-minus\" aria-hidden=\"true\"></i></span><span id=\"quantity_value\" >1</span>";
                inside2.Text += "<span class=\"plus\"><i class=\"fa fa-plus\" aria-hidden=\"true\"></i></span></div>";
                inside2.Text += "<a class=\"shoppingcart\">加入購物車</a>";
                inside2.Text += "<div class=\"product_favorite d-flex flex-column align-items-center justify-content-center lbh\" id=\"" + readersingle["cd_id"] + "wish" + "\"></div>";
                inside2.Text += "</div></div></div></div></div>";
            }
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
            cd_kind = cd_kindCM.ExecuteScalar().ToString();
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
                if (reader2["pe_main"].ToString() == "N")
                {
                    inside3.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\"><span>" + pe_discount + "折</span></div>";
                }
                inside3.Text += "<div class=\"favorite favorite_left lbh\" id=\""+reader2["cd_id"]+"wish2"+"\"></div>";
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
                if (readerkind["pe_main"].ToString() == "N")
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
                if (readerkind["pe_main"].ToString() == "N")
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
                    
                    inside3.Text += "<div class=\"favorite favorite_left lbh\" id=\""+ readerkind["cd_id"]+"wish"+"\"></div>";
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
        //優惠活動start
        SqlCommand cmdSQLevent = new SqlCommand();
        cmdSQL.Connection = cnSQL;
        //strSQL 為select form where insert
        String strSQLevent;
        Label insideevent = new Label();//產生多次


        strSQLevent = "select * from Promotion_events where pe_main='Y';";
        cmdSQL.CommandText = strSQLevent;
        cnSQL.Open();
        SqlDataReader readerevent = cmdSQL.ExecuteReader();
        insideevent.Text = "";
        
            while (readerevent.Read())
            {
                insideevent.Text += "<div class=\"deal_ofthe_week\">";
                insideevent.Text += "<div class=\"container\"><div class=\"row align-items-center\"><div class=\"col-lg-6\"><div class=\"deal_ofthe_week_img\">";
                insideevent.Text += "<img src =\"images/" + readerevent["pe_cd_id"] + ".png\" alt=\"\"></div></div>";
   /*圖片*/   /*insideevent.Text += "<img src =\"images/deal_ofthe_week.png\" alt=\"\"></div></div>";*/
                insideevent.Text +="<div class=\"col-lg-6 text-right deal_ofthe_week_col\"><div class=\"deal_ofthe_week_content d-flex flex-column align-items-center float-right\">";
     /*標題*/   insideevent.Text += "<div class=\"section_title\"><h2>"+readerevent["pe_title"] +"</h2></div>";                
                insideevent.Text += "<ul class=\"timer\"><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
 /*倒數天數*/   insideevent.Text += "<div id =\"day\" class=\"timer_num\">09</div><div class=\"timer_unit\">天</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"hour\" class=\"timer_num\">10</div><div class=\"timer_unit\">小時</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"minute\" class=\"timer_num\">45</div><div class=\"timer_unit\">分鐘</div>";
                insideevent.Text += "</li><li class=\"d-inline-flex flex-column justify-content-center align-items-center\">";
                insideevent.Text += "<div id =\"second\" class=\"timer_num\">23</div><div class=\"timer_unit\">秒</div>";
                insideevent.Text += "</li></ul><div class=\"red_button deal_ofthe_week_button\"><a href=\"#\">shop now</a></div>";
                insideevent.Text += "</div></div></div></div></div>";
            }
              
        cnSQL.Close();
        PlaceHolderevent.Controls.Add(insideevent);
        //優惠活動end

       



    }/* <- pageload的尾巴*/







    protected void btn_admlogout_Click(object sender, EventArgs e)
    {
        DateTime dtDay = DateTime.Today.AddDays(-365);
        Response.Cookies["user"].Expires = dtDay;
        Session.RemoveAll();
        Session["identity"] = null;
        Response.Redirect("/Default.aspx");
    }

    protected void btn_userlogout_Click(object sender, EventArgs e)
    {
        DateTime dtDay = DateTime.Today.AddDays(-365);
        Response.Cookies["user"].Expires = dtDay;
        Session.RemoveAll();
        Session["identity"] = null;
        Response.Redirect("/Default.aspx");
    }

    protected void itemSelected(object sender, EventArgs e)
    {

        string sel_val = DropDownList1.SelectedValue.ToString();
        switch (sel_val)
        {
            case "10":
                lb_seladr.Text = "取貨門市:";

                break;
            case "11":
                lb_seladr.Text = "宅配地址:";

                break;
            case "12":
                lb_seladr.Text = "郵寄地址:";

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
            //要轉成QRCode 的內容
            string content = reader["cd_id"].ToString();
            string content2 = "cart"+reader["cd_id"].ToString();
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
            //儲存圖片
            bitmap.Save(Request.PhysicalApplicationPath + @"\commodities_img\" + content + ".png", System.Drawing.Imaging.ImageFormat.Png);
            bitmap2.Save(Request.PhysicalApplicationPath + @"\commodities_img\" + content2 + ".png", System.Drawing.Imaging.ImageFormat.Png);
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
        tb_pfbd.Text = lb_pfbd.Text;
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
                    Response.Redirect("/Default.aspx");
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
        btn_pfsubmit.Visible = true;
        btn_pfback.Visible = true;
    }

    protected void btn_rgback_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Default.aspx");
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
            /**/


            /**/

        }
        if (e.CommandName.ToString().ToUpper() == "USER_DELETE2")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteuser = new SqlCommand(@"DELETE FROM Members where mm_no=@mm_no;", cnSQL);
            deleteuser.Parameters.Add(new SqlParameter("@mm_no", mm_no));

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
        string mm_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "USER_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?mm_no=" + mm_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "USER_DELETE2")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteuser = new SqlCommand(@"DELETE FROM Members where mm_no=@mm_no;", cnSQL);
            deleteuser.Parameters.Add(new SqlParameter("@mm_no", mm_no));

        }

    }

    protected void SqlDataSourceCD_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_cd.Attributes.Add("bordercolor", "#000000");
    }

    protected void GV_RowCommand_od(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_od.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string mm_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "USER_DETAIL")
        {
            //導去明細頁
            Response.Redirect("?mm_no=" + mm_no);

            /**/
            
                /**/

            }
        if (e.CommandName.ToString().ToUpper() == "USER_DELETE2")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteuser = new SqlCommand(@"DELETE FROM Members where mm_no=@mm_no;", cnSQL);
            deleteuser.Parameters.Add(new SqlParameter("@mm_no", mm_no));

        }

    }

    protected void SqlDataSourceOD_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_od.Attributes.Add("bordercolor", "#000000");
    }

    protected void GV_RowCommand_oddetail(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GV_od.Rows[index];

        //取得ID,要看你列表的SQL將ID放的位置狀況
        string mm_no = row.Cells[1].Text;

        if (e.CommandName.ToString().ToUpper() == "USER_DETAIL")
        {
            //導去明細頁

            Response.Redirect("?mm_no=" + mm_no);
            //adm_mmdetail_display.Visible = true;

        }
        if (e.CommandName.ToString().ToUpper() == "USER_DELETE2")
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand deleteuser = new SqlCommand(@"DELETE FROM Members where mm_no=@mm_no;", cnSQL);
            deleteuser.Parameters.Add(new SqlParameter("@mm_no", mm_no));

        }

    }

    protected void SqlDataSourceODDETAIL_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        GV_od.Attributes.Add("bordercolor", "#000000");
    }

    protected void btn_adm_mmedit_Click(object sender, EventArgs e)
    {
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
    }

    protected void btn_adm_mmback_Click(object sender, EventArgs e)
    {
        string mm_no = Request.QueryString["mm_no"].ToString();
        Response.Redirect("?mm_no=" + mm_no);
    }

    protected void btn_adm_mmsubmit_Click(object sender, EventArgs e)
    {
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand updatemm = new SqlCommand(@"UPDATE Members SET mm_adr=@mm_adr, mm_name=@mm_name,mm_phone=@mm_phone,mm_mail=@mm_mail,
                                            mm_bd=@mm_bd,mm_notice=@mm_notice where mm_no=@mm_no;", cnSQL);
        string mm_no = Request.QueryString["mm_no"].ToString();
        DateTime dt = Convert.ToDateTime(tb_adm_mmbd.Text);


        updatemm.Parameters.Add(new SqlParameter("@mm_no", mm_no));
        updatemm.Parameters.Add(new SqlParameter("@mm_adr", tb_adm_mmadr.Text));
        updatemm.Parameters.Add(new SqlParameter("@mm_name", "周淡淡"));
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