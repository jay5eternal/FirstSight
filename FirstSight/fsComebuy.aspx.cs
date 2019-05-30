using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fsComebuy : System.Web.UI.Page
{
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        scan.Visible = false;
        var page = Request.QueryString["page"];

        if (page != null && page == "scan")
        {
            scan.Visible = true;
        }

            SqlConnection cnSQL = new SqlConnection(strConnString);
        cnSQL.Open();
        /*訂單START*/

        Label slideshow_inside = new Label();//產生多次
        String slideshow_outside = "";

        SqlCommand slideshow_produce = new SqlCommand(@"SELECT DISTINCT pe_title ,pe_imgPath from  Promotion_events where pe_status='Y' ;", cnSQL);

        SqlDataReader slideshow_reader = slideshow_produce.ExecuteReader();
        slideshow_inside.Text = "";
        slideshow_outside += "<div class=\"col-md-12 no-gutter text-center\"><div id=\"header\" data-speed=\"2\" data-type=\"background\"><div id=\"headslide\" class=\"carousel slide\" data-ride=\"carousel\">";
        slideshow_outside += "<div class=\"carousel-inner\" role=\"listbox\">";

        int i = 1;
        while (slideshow_reader.Read())
        {
            
            if (slideshow_reader["pe_imgPath"].ToString() != "" && slideshow_reader["pe_imgPath"].ToString() != " " && slideshow_reader["pe_imgPath"].ToString() != null)
            {
                if (i == 1)
                {
                    slideshow_inside.Text += "<div class=\"item active\"><h1 style=\"color:black;font-size:50px;color:red\">" + slideshow_reader["pe_title"] + "</h1><br><br><img src=\"images/slideshow/" + slideshow_reader["pe_title"] + ".jpg\" alt=\"Slide\" /></div>";
                }
                else
                {
                    slideshow_inside.Text += "<div class=\"item\"><h1 style=\"color:black;font-size:50px;color:red\">" + slideshow_reader["pe_title"] + "</h1><br><br><img src=\"images/slideshow/" + slideshow_reader["pe_title"] + ".jpg\" alt=\"Slide\" /></div>";
                }
                i = i + 1; 
            }
            
        }
        slideshow_inside.Text = slideshow_outside + slideshow_inside.Text + "</div></div></div></div></div>";
        

        PH_slideshow.Controls.Add(slideshow_inside);
        slideshow_reader.Close();
        cnSQL.Close();

        
    }
}
