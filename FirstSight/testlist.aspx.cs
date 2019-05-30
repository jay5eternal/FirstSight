using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> allowedExtextsion = new List<string> ();
        allowedExtextsion.Add("5");
        allowedExtextsion.Add("6");
        allowedExtextsion.Add("7");
        allowedExtextsion.Add("8");

        string gggg = "6";
        Label1.Text = allowedExtextsion.IndexOf(gggg).ToString(); 
        
             


        
        //Label1.Text = allowedExtextsion[0];
    }
}