using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cbs_cd : System.Web.UI.Page
{
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        /*訂單START*/

        Label cbstag_inside = new Label();//產生多次
        String cbstag_outside = "";

        SqlCommand cbstag_produce = new SqlCommand(@"select * from  Tags  ;", cnSQL);

        SqlDataReader cbstag_reader = cbstag_produce.ExecuteReader();
        cbstag_inside.Text = "";
        cbstag_outside += "<ul class=\"portfolio-sorting list-inline text-center\">";
        cbstag_outside += "<li><a href=\"#\" data-group=\"all\" class=\"active\">All</a></li>";


        while (cbstag_reader.Read())
        {
            cbstag_inside.Text += "<li><a href=\"#\" data-group=\"" + cbstag_reader["tag_name"] + "\" >" + cbstag_reader["tag_name"] + "</a></li>";
        }
        cbstag_inside.Text = cbstag_outside + cbstag_inside.Text+"</ul>";


        PH_cbs_tags.Controls.Add(cbstag_inside);
        cbstag_reader.Close();
        cnSQL.Close();

        /*訂單 END*/

        /*訂單START*/

        Label cbscd_inside = new Label();//產生多次
        String cbscd_outside = "";
        cnSQL.Open();
        SqlCommand cbscd_produce = new SqlCommand(@"select * from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id;", cnSQL);

        SqlDataReader cbscd_reader = cbscd_produce.ExecuteReader();
        cbscd_inside.Text = "";
        cbscd_outside += "<ul class=\"portfolio-items list-unstyled\" id=\"grid\">";
        //cbscd_outside += "<div class=\"row blogs_container\">";


        while (cbscd_reader.Read())
        {
            SqlConnection cnSQL2 = new SqlConnection(strConnString);
            cnSQL2.Open();
            String tagname_outside = "";
            SqlCommand cbshave_produce = new SqlCommand(@"select * from  Have where have_cd_id=@have_cd_id  ;", cnSQL2);
            cbshave_produce.Parameters.Add(new SqlParameter("@have_cd_id", cbscd_reader["cd_id"]));
            SqlDataReader cbshave_reader = cbshave_produce.ExecuteReader();
            while (cbshave_reader.Read())
            {
                SqlConnection cnSQL3 = new SqlConnection(strConnString);
                cnSQL3.Open();
                SqlCommand find_tagname = new SqlCommand(@"select tag_name from  Tags where tag_id=@tag_id  ;", cnSQL3);
                find_tagname.Parameters.Add(new SqlParameter("@tag_id", cbshave_reader["have_tag_id"]));
                string tag_name = find_tagname.ExecuteScalar().ToString();
                tagname_outside += "\"" + tag_name + "\",";
                cnSQL3.Close();
            }
            tagname_outside += "@";
            tagname_outside = tagname_outside.Replace(",@", "");
            //cbscd_inside.Text += "<li class=\"col-md-4 col-sm-12 col-xs-12 no-gutter\" data-groups='[" + tagname_outside + "]'>";
            //cbscd_inside.Text += "<a href=\"?pid=" + cbscd_reader["cd_id"] +"\"><div class=\"background-image-holder\"><img alt=\"portfolio\" class=\"background-image\" src=\"images/clothes/" + cbscd_reader["cd_id"] + ".jpg\" /></div></a>";
            //cbscd_inside.Text += "</li>";
            cbscd_inside.Text += "<li class=\"col-md-4 col-sm-12 col-xs-12 no-gutter\" data-groups='[" + tagname_outside + "]'>";

            cbscd_inside.Text += "<a href=\"?pid=" + cbscd_reader["cd_id"] + "\"><div class=\"background-image-holder\">";
            if ((cbscd_reader["pe_main"].ToString() == "N" || cbscd_reader["pe_main"].ToString() == "Y") && cbscd_reader["pe_status"].ToString() == "Y")
            {

                double pe_discount = 0;
                if (cbscd_reader["pe_discount"].ToString() != "" && cbscd_reader["pe_discount"].ToString() != null)
                {
                    pe_discount = Convert.ToDouble(cbscd_reader["pe_discount"].ToString()) * 10;
                }
                cbscd_inside.Text += "<div class=\"product_bubble product_bubble_right product_bubble_red d-flex flex-column align-items-center\" style=\"text-align:center\"><span>" + pe_discount + "折</span></div>";
            }
            cbscd_inside.Text += "<img alt=\"portfolio\" class=\"background-image\" src=\"images/clothes/" + cbscd_reader["cd_id"] + ".jpg\" /></div></a>";

            cbscd_inside.Text += "</li>";




            cnSQL2.Close();
        }
        cbscd_inside.Text = cbscd_outside + cbscd_inside.Text +"</ul></div>" ;


        PH_cbs_cds.Controls.Add(cbscd_inside);
        cbscd_reader.Close();
        cnSQL.Close();


        /*訂單 END*/

        /*商品資訊start*/
        var pid = Request.QueryString["pid"];
        if (pid != null)
        {
            cd_display.Visible = false;
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
            //cnSQL.Close();
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
                inside2.Text += "<ul><li class=\"active\"><h3><b>商品資訊</b></h3></li></ul>";
                inside2.Text += "</div></div></div>";
                inside2.Text += "<!-- 商品圖片 --><div class=\"row\"><div class=\"col-lg-7\"><div class=\"single_product_pics\">";
                inside2.Text += "<div class=\"row\"><div class=\"col-lg-9 image_col order-lg-2 order-1\">";
                inside2.Text += "<div class=\"single_product_image\"><div class=\"single_product_image_background\" style=\"background-image:url(images/clothes/" + readersingle["cd_id"] + ".jpg)\"></div>";
                inside2.Text += "</div></div></div></div></div>";
                inside2.Text += "<div class=\"col-lg-5\">";
                inside2.Text += "<div class=\"product_details\">";
                inside2.Text += "<div class=\"product_details_title\">";
                /*商品名稱*/
                inside2.Text += "<h2 class=\"product_name\" id=\"" + readersingle["cd_id"] + "\"><a href=\"?pid=" + readersingle["cd_id"] + "\">" + readersingle["cd_name"] + "</a></h2>";
                /*商品材質*/
                inside2.Text += "<p>" + "材質：" + readersingle["cd_material"] + " </p>";
                inside2.Text += "<p>" + "擺放區：" + readersingle["cd_place"] + " </p></div>";
                /*商品價格*/
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
                /*選擇尺寸*/
                inside2.Text += "<div class=\"product_color\">";
                inside2.Text += "<span>其他款式:</span><ul>";
                while (readergroup.Read())
                {
                    inside2.Text += "<a href=\"?pid=" + readergroup["cd_id"] + "\"><li style=\"  background: #" + readergroup["cd_color"] + ";border:solid 0.1px #000000;\"></li></a>";
                }
                inside2.Text += "</ul></div>";
                inside2.Text += "<div class=\"row\">";
                inside2.Text += "<div class=\"col-lg-4 col-sm-3\" style=\"background-image:url(commodities_img/cart" + readersingle["cd_id"] + ".png);height:150px \"></div>";
                inside2.Text += "<div class=\"col-lg-4 col-sm-4\"></div>";
                inside2.Text += "<div class=\"col-lg-4 col-sm-3\" style=\"background-image:url(commodities_img/wish" + readersingle["cd_id"] + ".png);height:150px \"></div>";
                inside2.Text += "<div class=\"col-lg-4 col-sm-4\"></div></div>";
                inside2.Text += "<div class=\"row\">";
                inside2.Text += "<div class=\"col-lg-1 col-sm-4\"></div>";
                inside2.Text += "<div class=\"col-lg-4 col-sm-4\">加入購物車</div>";
                inside2.Text += "<div class=\"col-lg-4 col-sm-4\"></div>";
                inside2.Text += "<div class=\"col-lg-3 col-sm-4\">加入許願池</div></div>";






                inside2.Text += "<div class=\"row\">";
                

                //inside2.Text += "</ul></div>";
                //inside2.Text += "<div class=\"row\">";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-5\"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-3\" style=\"background-image:url(commodities_img/cart" + readersingle["cd_id"] + ".png);height:150px \"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-4\"></div></div>";
                //inside2.Text += "<div class=\"row\">";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-5\"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-4\">加入購物車</div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-3\"></div></div>";



                //inside2.Text += "<div class=\"row\">";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-5\"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-3\" style=\"background-image:url(commodities_img/wish" + readersingle["cd_id"] + ".png);height:150px \"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-4\"></div></div>";
                //inside2.Text += "<div class=\"row\">";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-5\"></div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-4\">掃描加入許願池</div>";
                //inside2.Text += "<div class=\"col-lg-4 col-sm-3\"></div></div>";


                while (readersingle.Read())
                {

                    //inside2.Text += "<a class=\"wish_tag\" href=\"#\" style=\"color: black\" id =" + readersingle["have_tag_id"] + "> #" + readersingle["tag_name"] + "</a>";
                }
                inside2.Text += "</div ></div ></div>";
                inside2.Text += "</div></div>";
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

            /********/
            SqlConnection cnSQL9 = new SqlConnection(strConnString);
            cnSQL9.Open();
            SqlCommand cmdSQL9 = new SqlCommand();
            cmdSQL9.Connection = cnSQL9;
            //strSQL 為select form where insert
            String strSQL9;
            strSQL9 = "select top 4 *, NewID() as random from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id where cd_kind='" + cd_kind + "' order by random;";
            cmdSQL9.CommandText = strSQL9;
            
            SqlDataReader reader9 = cmdSQL9.ExecuteReader();
            /********/

            strSQL3 = "select top 4 * from Hot_saled left join Commodities on hot_cd_id = cd_id order by hot_count desc ;";
            //strSQL3 = "select top 10 *, NewID() as random from Commodities cd left join Promotion_events pe on cd.cd_id = pe.pe_cd_id where cd_kind='" + cd_kind + "' order by random;";//亂序產生同類型商品
            cmdSQL4.CommandText = strSQL3;
            cnSQL.Open();
            SqlDataReader reader2 = cmdSQL4.ExecuteReader();
            //頭
            outside3 += "<div class=\"best_sellers\"><div class=\"container\"><div class=\"row\"><div class=\"col text-center\">";
            outside3 += "<div class=\"section_title new_arrivals_title\"><h2>您可能也喜歡...</h2></div></div></div>";
            outside3 += "<div class=\"row\"><div class=\"col\"><div class=\"product_slider_container\"><div class=\"owl-carousel owl-theme product_slider\">";
            outside3 += "<div class=\"row\"><div class=\"portfolio-items\">";
            inside3.Text = "";
            int a = 0;
            List<string> pickedcd = new List<string>();  //判斷重複
            pickedcd.Add(pid);
            while (reader2.Read())
            {
                a++;
                pickedcd.Add(reader2["cd_id"].ToString());
                inside3.Text += "<div class=\"col-sm-6 col-md-3 col-lg-3 graphic\"><div class=\"portfolio-item\">";
 /*點下去*/     inside3.Text += "<div class=\"hover-bg\"><a href=\"?pid=" + reader2["cd_id"] + "\">";
                inside3.Text += "<div class=\"hover-text\"><h4>"+reader2["cd_name"]+"</h4></div>";
                inside3.Text += "<img src=\"images/clothes/" + reader2["cd_id"] + ".jpg\" class=\"img-responsive\" alt=\"Project Title\"/><asp:Image ID=\"Image1\" runat=\"server\" /></a></div>";
                if (a == 1)
                {
                    inside3.Text += "<div style=\"float:right\"><img src=\"img/NO1.png\" Height=\"150px\" Width=\"150px\"/></div>";
                }
                else if(a == 2){
                    inside3.Text += "<div style=\"float:right\"><img src=\"img/NO2.png\" Height=\"150px\" Width=\"150px\"/></div>";
                }
                else if(a == 3){
                    inside3.Text += "<div style=\"float:right\"><img src=\"img/NO3.png\" Height=\"150px\" Width=\"150px\"/></div>";
                }
                else if(a == 4){
                    inside3.Text += "<div style=\"float:right\"><img src=\"img/NO4.png\" Height=\"150px\" Width=\"150px\"/></div>";
                }
                inside3.Text += "</div></div>";
            }
            while (reader9.Read())
            {
                if (pickedcd.IndexOf(reader9["cd_id"].ToString()) == -1)
                {
                    inside3.Text += "<div class=\"col-sm-6 col-md-3 col-lg-3 graphic\"><div class=\"portfolio-item\">";
                    /*點下去*/
                    inside3.Text += "<div class=\"hover-bg\"><a href=\"?pid=" + reader9["cd_id"] + "\">";
                    inside3.Text += "<div class=\"hover-text\"><h4>" + reader9["cd_name"] + "</h4></div>";
                    inside3.Text += "<img src=\"images/clothes/" + reader9["cd_id"] + ".jpg\" class=\"img-responsive\" alt=\"Project Title\"/><asp:Image ID=\"Image1\" runat=\"server\" /></a></div>";
                    inside3.Text += "</div></div>";
                }
                
            }
                cnSQL.Close();
            inside3.Text = outside3 + inside3.Text + "</div></div>";
            /*您可能會感興趣的商品...end*/
            pickedcd.Clear();
            inside2.Text += inside3.Text;
            PlaceHolder1.Controls.Add(inside2);
            cddetail_display.Visible = true;
           // likeit_display.Visible = true;
        }
        /*商品資訊end*/



        /****************/
        
        /****************/
    }
}