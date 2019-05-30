using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class delete_cart : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        string cd_id = Request.Form["cd_id"];
        string mm_act = Session["user"].ToString();
        string sc_mm_id = "";


        SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        sc_mm_id = find_mmid.ExecuteScalar().ToString();


        try
        {
            SqlCommand delscd = new SqlCommand(@"DELETE FROM Shopping_Carts WHERE sc_mm_id=@sc_mm_id and sc_cd_id=@cd_id;", cnSQL);
            delscd.Parameters.Add(new SqlParameter("@sc_mm_id", sc_mm_id));
            delscd.Parameters.Add(new SqlParameter("@cd_id", cd_id));

            delscd.ExecuteNonQuery();
            delscd.Dispose();
            cnSQL.Close();
            Response.Write("Y");
        }
        catch
        {
            cnSQL.Close();
            Response.Write("N");

            //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id + ",mm_id:" + sc_mm_id+"成功拉幹");
        }
    }
}