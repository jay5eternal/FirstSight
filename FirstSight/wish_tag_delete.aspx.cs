using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;

public partial class wish_tag_delete : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        //string tag_id = Request.Form["tag_id"]; //接受傳入的參數
        //string mm_act = Session["user"].ToString();//阿這個帳號 是存在哪
        string mm_act = "abc123";
        string tag_id = "tag0000002";

        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        var ww_mm_id = find_mmid.ExecuteScalar().ToString();

        var ww_id = ww_mm_id.Replace("mm", "ww");




        try
        {
            SqlCommand deletewishtag = new SqlCommand(@"delete from Follow where fw_ww_id=@ww_mm_id and fw_tag_id=@tag_id", cnSQL);
            deletewishtag.Parameters.Add(new SqlParameter("@ww_mm_id", ww_id));
            deletewishtag.Parameters.Add(new SqlParameter("@tag_id", tag_id));

            deletewishtag.ExecuteNonQuery();
            deletewishtag.Dispose();
            cnSQL.Close();
            Response.Write(tag_id);
            Response.Write("Y");
        }
        catch
        {
            Response.Write("N");    //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id + ",mm_id:" + sc_mm_id+"成功拉幹");
        }
    }
}
