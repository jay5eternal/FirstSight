using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Configuration;

public partial class wish_delete : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        string cd_id = Request.Form["cd_id"];
        string mm_act = Session["user"].ToString();
        //string cd_id = "cd00000001";
        //string mm_act = "abc123";
        string ww_mm_id = "";


        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        ww_mm_id = find_mmid.ExecuteScalar().ToString();



        try
        {
            SqlCommand deletewish = new SqlCommand(@"delete from Wishing_Wells where ww_mm_id=@ww_mm_id and ww_cd_id=@ww_cd_id", cnSQL);
            deletewish.Parameters.Add(new SqlParameter("@ww_mm_id", ww_mm_id));
            deletewish.Parameters.Add(new SqlParameter("@ww_cd_id", cd_id));

            deletewish.ExecuteNonQuery();
            deletewish.Dispose();
            cnSQL.Close();           
        }
        catch
        {
            Response.Write("N");    //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id + ",mm_id:" + sc_mm_id+"成功拉幹");
        }
    }
}
