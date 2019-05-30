using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wish_tag : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ww_id, mm_id = "";
        string tag_id = Request.Form["tag_id"]; //接受傳入的參數

        try
        {
            mm_id = Session["mmid"].ToString();
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();
            ww_id = mm_id.Replace("mm", "ww");

            SqlCommand check_cd = new SqlCommand(@"Select fw_tag_id From Follow Where fw_tag_id=@fw_tag_id and fw_ww_id=@fw_ww_id;", cnSQL);
            check_cd.Parameters.Add(new SqlParameter("@fw_tag_id", tag_id));
            check_cd.Parameters.Add(new SqlParameter("@fw_ww_id", ww_id));

            //Response.Write("tag_id:" + tag_id + "ww_id:" + ww_id);

            if (check_cd.ExecuteScalar() == null)//資料為空(商品不重複)
            {
                try
                {
                    SqlCommand newsc = new SqlCommand(@"Insert Into Follow(fw_ww_id,fw_tag_id,fw_date) 
                                                          Values(@fw_ww_id,@fw_tag_id,CONVERT(varchar(100), GETDATE(), 23));", cnSQL);
                    newsc.Parameters.Add(new SqlParameter("@fw_ww_id", ww_id));
                    newsc.Parameters.Add(new SqlParameter("@fw_tag_id", tag_id));

                    newsc.ExecuteNonQuery();
                    newsc.Dispose();
                    cnSQL.Close();
                    Response.Write("Y");
                }


                catch
                {
                    cnSQL.Close();

                    Response.Write("No");
                    //Response.Write("商品ID:" + cd_id + ",商品數量:" + sc_num + ",sc_id:" + sc_id + ",mm_id:" + sc_mm_id+"成功拉幹");
                }
            }
            else
            {
                try
                {
                    cnSQL.Close();
                    Response.Write("R");
                }
                //cnSQL.Close();
                catch { }
            }
        }//這個帳號
        catch
        {
            

            Response.Write("N");
        }
       

        //string mm_act = "abc123";
        //string tag_id = "tag0000008";

        
        //SqlCommand find_mmid = new SqlCommand(@"Select mm_id From Members Where mm_act=@mm_act;", cnSQL);
        //find_mmid.Parameters.Add(new SqlParameter("@mm_act", mm_act));
        //mm_id = find_mmid.ExecuteScalar().ToString();

        


        
    }
}