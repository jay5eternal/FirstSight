using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;

public partial class checkwish : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //string ww_cd_id = ""; //接受傳入的參數
        ////string mm_act = Session["user"].ToString();//阿這個帳號 是存在哪
        //string mm_act = "abc123";
        //string ww_cd_id = "cd00000027";

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();

        //SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        //find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        //var ww_mm_id = find_mmid.ExecuteScalar().ToString();



        //cnSQL.Close();
        var ww_mm_id = Session["mmid"].ToString();
        

        SqlCommand check_ww = new SqlCommand(@"Select * From Wishing_Wells Where ww_mm_id=@ww_mm_id", cnSQL);
        check_ww.Parameters.Add(new SqlParameter("@ww_mm_id", ww_mm_id));
        SqlDataReader readersingle = check_ww.ExecuteReader();
        
        var msg = "";
        while (readersingle.Read())
        {
            msg += readersingle["ww_cd_id"].ToString()+",";
        }
        cnSQL.Close();
        Response.Write(msg);
    }
}

