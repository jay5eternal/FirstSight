using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class colortest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
                     {
                          populateDdlMultiColor();
                         colorManipulation();
                      }
    }
    protected void ddlMultiColor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Color backColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
                  ddlMultiColor.BackColor = backColor;
                  colorManipulation();
                  ddlMultiColor.Items.FindByValue(ddlMultiColor.SelectedValue).Selected = true;
                  string strStyle = string.Format("background:{0};width:30px;height:25px;",
                                                  ddlMultiColor.SelectedItem.Value);
                  msgColor.Attributes.Add("style", strStyle);
    }

    private void colorManipulation()
    {
        for (int row = 0; row < ddlMultiColor.Items.Count - 1; row++)
                     {
                       Color backColor = Color.FromName(ddlMultiColor.Items[row].Value);
                          double grayValue = 0.299 * backColor.R + 0.587 * backColor.G + 0.114 * backColor.B;
                          string strTextColor = (grayValue < 127.5) ? "white" : "black";
                         string strStyle = string.Format("background-color:{0}; color: {1};",
                                                         ddlMultiColor.Items[row].Value, strTextColor);
               
                ddlMultiColor.Items[row].Attributes.Add("style", strStyle);
                      }
                  ddlMultiColor.BackColor = Color.FromName(ddlMultiColor.SelectedItem.Text);
    }

    private static List<string> finalColorList()
    {
        string[] allColors = Enum.GetNames(typeof(KnownColor));
                  string[] systemEnvironmentColors = new string[(typeof(SystemColors)).GetProperties().Length];
           
            int index = 0;
                  foreach (PropertyInfo member in (typeof(SystemColors)).GetProperties())
                      {
                          systemEnvironmentColors[index++] = member.Name;
                      }
           
            List<string> finalColorList = new List<string>();
                  foreach (string color in allColors)
                      {
                          if (Array.IndexOf(systemEnvironmentColors, color) < 0)
                              {
                                  finalColorList.Add(color);
                              }
                      }
           
            return finalColorList;
    }

    private void populateDdlMultiColor()
        {
            ddlMultiColor.DataSource = finalColorList();
            ddlMultiColor.DataBind();
       }
}