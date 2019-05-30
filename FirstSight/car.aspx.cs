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

public partial class car : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {




        try
        {

            string cd_id = Request.Form["cd_id"]; //接受傳入的參數
            string sc_num = Request.Form["sc_num"]; //接受傳入的參數
            string mm_act = Session["user"].ToString();//阿這個帳號 是存在哪
            string sc_id, sc_mm_id = "";
            //string cd_id = "cd00000056"; //接受傳入的參數
            ////string sc_num = "5"; //接受傳入的參數
            //string sc_mm_id = "mm00000003";
            //string sc_id = "";
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
            find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
            sc_mm_id = find_mmid.ExecuteScalar().ToString();

            sc_id = sc_mm_id.Replace("mm", "sc");


            SqlCommand check_cd = new SqlCommand(@"Select sc_cd_id From Shopping_Carts Where sc_id=@sc_id and sc_cd_id=@cd_id;", cnSQL);
            check_cd.Parameters.Add(new SqlParameter("@sc_id", sc_id));
            check_cd.Parameters.Add(new SqlParameter("@cd_id", cd_id));

            //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id+ ",mm_id:" + sc_mm_id);


            try
            {

                if (check_cd.ExecuteScalar() == null)//資料為空(商品不重複) +到購物車
                {
                    SqlCommand newsc = new SqlCommand(@"Insert Into Shopping_Carts(sc_id,sc_mm_id,sc_cd_id,sc_num) 
                                                          Values(@sc_id,@sc_mm_id,@cd_id,@sc_num);", cnSQL);
                    newsc.Parameters.Add(new SqlParameter("@sc_id", sc_id));
                    newsc.Parameters.Add(new SqlParameter("@sc_mm_id", sc_mm_id));
                    newsc.Parameters.Add(new SqlParameter("@cd_id", cd_id));
                    newsc.Parameters.Add(new SqlParameter("@sc_num", sc_num));
                    newsc.ExecuteNonQuery();
                    newsc.Dispose();
                    cnSQL.Close();
                    Response.Write("Y");

                }
                else
                {
                    cnSQL.Close();
                    Response.Write("R");
                } //商品重複
            }
            catch
            {
                Response.Write("Error");

                //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id + ",mm_id:" + sc_mm_id+"成功拉幹");
            }
        }
        catch
        {
            Response.Write("N");
        }

    }



    

}
