using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;

public partial class wish : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {


        //try
        //{

        string cd_id = Request.Form["cd_id"]; //接受傳入的參數
        string mm_act = Session["user"].ToString();//阿這個帳號 是存在哪
        //string mm_act = "abc123";
        //string cd_id = "cd00000027";

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        var ww_mm_id = find_mmid.ExecuteScalar().ToString();

        var ww_id = ww_mm_id.Replace("mm", "ww");
            

        SqlCommand check_ww = new SqlCommand(@"Select ww_cd_id From Wishing_Wells Where ww_mm_id=@ww_mm_id and ww_cd_id=@ww_cd_id", cnSQL);
        check_ww.Parameters.Add(new SqlParameter("@ww_mm_id", ww_mm_id));
        check_ww.Parameters.Add(new SqlParameter("@ww_cd_id", cd_id));
        //Response.Write(check_ww.ExecuteScalar().ToString());

        if (check_ww.ExecuteScalar() == null)
        {
            try
            {
                SqlCommand newsc = new SqlCommand(@"Insert Into Wishing_Wells(ww_id,ww_mm_id,ww_cd_id,ww_wishdate) 
                                                          Values(@ww_id,@ww_mm_id,@ww_cd_id, CONVERT(varchar(100), GETDATE(), 23));", cnSQL);
                newsc.Parameters.Add(new SqlParameter("@ww_id", ww_id));
                newsc.Parameters.Add(new SqlParameter("@ww_mm_id", ww_mm_id));
                newsc.Parameters.Add(new SqlParameter("@ww_cd_id", cd_id));

                newsc.ExecuteNonQuery();
                newsc.Dispose();
                cnSQL.Close();
                Response.Write("Y");
            }
            catch (Exception)
            {
                 Response.Write("error");
            }
    }
        else
        {
            try
            {
                
                Response.Write("R");
            }
            catch { }
            
        }
        
    }
}

