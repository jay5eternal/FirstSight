using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Default2 : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        var page = Request.QueryString["page"];
        if (page == "kind")
        {

        }
        string strSQL;
        SqlConnection cnSQL = new SqlConnection();
        //連線字串
        cnSQL.ConnectionString = strConnString;
        /*新貨到start*/
        SqlCommand cmdSQL = new SqlCommand();
        cmdSQL.Connection = cnSQL;

        strSQL = "select top 10* from Commodities Order By cd_inserttime desc;";
        cmdSQL.CommandText = strSQL;
        cnSQL.Open();

        cmdSQL.ExecuteNonQuery();
        cmdSQL.Dispose();
        cnSQL.Close();


    }
}