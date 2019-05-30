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

public partial class caltotal : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string sc_cd_id = Request.Form["sc_cd_id"]; //接受傳入的參數
        string temp_szamount = Request.Form["temp_szamount"];

        string[] words = temp_szamount.Split(',');
        string[] szat = (string[])Session[words[0]];
        Session["N" + words[0]] = szat;
        
        
        //string[] szats2 = (string[])Session["N" + words[0]];

        if (words[2] == "minus")
        {
            switch (words[1])
            {
                case "S":
                    string[] szats = (string[])Session["N"+words[0]];
                    szats[0] = (Convert.ToInt32(szats[0]) - 1).ToString();
                    Session["N" + words[0]] = szats;
                    break;
                case "M":
                    string[] szatm = (string[])Session["N" + words[0]];
                    szatm[1] = (Convert.ToInt32(szatm[1]) - 1).ToString();
                    Session[words[0]] = szatm;
                    break;
                case "L":
                    string[] szatl = (string[])Session["N" + words[0]];
                    szatl[2] = (Convert.ToInt32(szatl[2]) - 1).ToString();
                    Session[words[0]] = szatl;
                    break;
                case "XL":
                    string[] szatxl = (string[])Session["N" + words[0]];
                    szatxl[3] = (Convert.ToInt32(szatxl[3]) - 1).ToString();
                    Session[words[0]] = szatxl;
                    break;


            }
        }
        else
        {
            switch (words[1])
            {
                case "S":
                    string[] szats = (string[])Session["N" + words[0]];
                    szats[0] = (Convert.ToInt32(szats[0]) + 1).ToString();
                    Session[words[0]] = szats;
                    break;
                case "M":
                    string[] szatm = (string[])Session["N" + words[0]];
                    szatm[1] = (Convert.ToInt32(szatm[1]) + 1).ToString();
                    Session[words[0]] = szatm;
                    break;
                case "L":
                    string[] szatl = (string[])Session["N" + words[0]];
                    szatl[2] = (Convert.ToInt32(szatl[2]) + 1).ToString();
                    Session[words[0]] = szatl;
                    break;
                case "XL":
                    string[] szatxl = (string[])Session["N" + words[0]];
                    szatxl[3] = (Convert.ToInt32(szatxl[3]) + 1).ToString();
                    Session[words[0]] = szatxl;
                    break;


            }
        }
        var a = "";
        var b = "";
        var c = "";
        //string sc_cd_id = "cd00000005"; //接受傳入的參數
        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand find_price = new SqlCommand(@"Select cd_price From Commodities Where cd_id=@cd_id;", cnSQL);
        find_price.Parameters.Add(new SqlParameter("@cd_id", sc_cd_id));
        a = find_price.ExecuteScalar().ToString();
        cnSQL.Close();
        //判斷是否為特惠 給你做一次 存入 b 擬就參考 我36-40的code
        try
        {
            cnSQL.Open();
            SqlCommand check_main = new SqlCommand(@"select pe_main from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL);
            check_main.Parameters.Add(new SqlParameter("@pe_cd_id", sc_cd_id));
            b = check_main.ExecuteScalar().ToString();
            cnSQL.Close();
        }
        catch
        {
            b = "Y";
        }
        try
        {
            cnSQL.Open();
            SqlCommand find_discount = new SqlCommand(@"select pe_discount from Promotion_events where pe_cd_id=@pe_cd_id;", cnSQL);
            find_discount.Parameters.Add(new SqlParameter("@pe_cd_id", sc_cd_id));
            c = find_discount.ExecuteScalar().ToString();
            cnSQL.Close();

        }
        catch
        {
        }


        Response.Write(a + "," + b + "," + c);
        



    }
}
