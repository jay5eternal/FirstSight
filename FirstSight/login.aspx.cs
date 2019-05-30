using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using ZXing;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    String linksameword = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    String strConnString = WebConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        nav_administrator.Visible = false;


    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {

        //{
        //    SqlCommand maidentity = new SqlCommand(@"Select ma_identity From Management where ma_account=@ma_account;", cnSQL);
        //    maidentity.Parameters.Add(new SqlParameter("@ma_account", name));
        //    ma_identity = maidentity.ExecuteScalar().ToString();
        //}
        //資料庫連線
        SqlConnection cnSQL = new SqlConnection();
        //連線字串
        cnSQL.ConnectionString = strConnString;
        //SqlCommand
        SqlCommand cmdSQL = new SqlCommand();
        cmdSQL.Connection = cnSQL;
        //strSQL 為select form where insert
        String strSQL, act, dbpwd;
        if (tb_act.Text != "")
        {
            act = tb_act.Text.Replace("'", "");
            strSQL = "Select mm_pwd FROM Members WHERE mm_act = '" + act + "';";
            cmdSQL.CommandText = strSQL;
            string result = "";

            cnSQL.Open();
            try
            {
                var firstColumn = cmdSQL.ExecuteScalar();
                if (firstColumn != null)
                {
                    dbpwd = firstColumn.ToString();
                    if (tb_pwd.Text == dbpwd)
                    {

                        //Set Cookie and forward

                        String result_who, result_id;
                        //Response.Cookies["user"].Value = Server.UrlEncode(tb_act.Text);
                        //DateTime dayT = DateTime.Today.AddDays(1);
                        //Response.Cookies["user"].Expires = dayT;
                        SqlCommand findwho = new SqlCommand("Select mm_who From Members where mm_act = '" + tb_act.Text + "';", cnSQL);
                        SqlCommand findid = new SqlCommand("Select mm_id From Members where mm_act = '" + tb_act.Text + "';", cnSQL);
                        result_who = findwho.ExecuteScalar().ToString();
                        result_id = findid.ExecuteScalar().ToString();
                        Session["identity"] = result_who;
                        Session["user"] = tb_act.Text;
                        Session["mmid"] = result_id;


                        cnSQL.Close();
                        Response.Redirect("/Default.aspx");

                    }
                    else
                    {
                        cnSQL.Close();
                        lb_error.Text = "密碼錯誤";
                    }
                }

            }
            catch (SqlException ex)
            {
                result = "Error:" + ex.Number;
            }
            cnSQL.Close();

        }
        else
        {
            cnSQL.Close();
            lb_error.Text = "帳號不可為空!";
        }





    }
    protected void btn_fgpwd_Click(object sender, EventArgs e)
    {
        login_display.Visible = false;
        forpwd_display.Visible = true;
    }

    protected void btn_actvf_Click(object sender, EventArgs e)
    {

        try
        {
            SqlConnection cnSQL = new SqlConnection(strConnString);
            cnSQL.Open();

            //strSQL 為select form where insert
            String mm_act = "", sc_count, act, sc_count1, mm_mail = "", mm_pwd = "", sc_count2;
            mm_act = tb_actvf.Text;


            SqlCommand find_sc_cd_num = new SqlCommand(@"select mm_act from Members where mm_act=@mm_act;", cnSQL);
            find_sc_cd_num.Parameters.Add(new SqlParameter("@mm_act", mm_act));
            sc_count = find_sc_cd_num.ExecuteScalar().ToString();
            //

            SqlCommand find_sc_cd_num1 = new SqlCommand(@"select mm_mail from Members where mm_act=@mm_act;", cnSQL);
            find_sc_cd_num1.Parameters.Add(new SqlParameter("@mm_act", sc_count));
            sc_count1 = find_sc_cd_num1.ExecuteScalar().ToString();


            if (mm_act == sc_count)
            {

                try
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(sc_count1);
                    //msg.To.Add("b@b.com");可以發送給多人
                    //msg.CC.Add("c@c.com");
                    //msg.CC.Add("c@c.com");可以抄送副本給多人 
                    //這裡可以隨便填，不是很重要
                    msg.From = new MailAddress("jay05eternal@gmail.com", "阿光", System.Text.Encoding.UTF8);
                    /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
                    msg.Subject = "FirstSight重設密碼";//郵件標題
                    msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼

                    Random ram = new Random();//亂數種子
                    int i = ram.Next(0, 100000);//回傳0-99的亂數
                    string ii = Convert.ToString(i);


                    msg.Body = ii; //郵件內容
                    msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                                                                 //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
                    msg.IsBodyHtml = true;//是否是HTML郵件 
                                          //msg.Priority = MailPriority.High;//郵件優先級 

                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("chu104ands171206@gmail.com", "android1206"); //這裡要填正確的帳號跟密碼
                    client.Host = "smtp.gmail.com"; //設定smtp Server
                    client.Port = 25; //設定Port
                    client.EnableSsl = true; //gmail預設開啟驗證
                    client.Send(msg); //寄出信件
                    client.Dispose();
                    msg.Dispose();

                    SqlCommand updatepf = new SqlCommand(@"UPDATE Members SET mm_pwd=@mm_pwd where mm_act=@mm_act", cnSQL);
                    updatepf.Parameters.Add(new SqlParameter("@mm_act", sc_count));
                    updatepf.Parameters.Add(new SqlParameter("@mm_pwd", ii));
                    updatepf.ExecuteNonQuery();
                    updatepf.Dispose();
                    cnSQL.Close();
                    Response.Redirect("login.aspx");

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                lb_rig.Visible = true;
            }


        }
        catch
        { }


    }

    protected void btn_backatvf_Click(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Default.aspx");
        //if (tb_act.Text != "")
        //{

        //    String SQL = "Select a_pwd FROM administrator WHERE a_acc = '" + tb_act.Text + "'";

        //    QDBII oDB = new QDBII();

        //    String DBPWD = oDB.OneSQL(SQL);

        //    if (tb_pwd.Text == DBPWD)
        //    {

        //        //Set Cookie and forward

        //        Response.Cookies["user"].Value = Server.UrlEncode(tb_act.Text);
        //        DateTime dayT = DateTime.Today.AddDays(1);
        //        Response.Cookies["user"].Expires = dayT;



        //        Server.Transfer("/index.aspx");

        //    }
        //    else
        //    {
        //        lb_error.Text = "密碼錯誤";
        //    }

        //}
        //else
        //{
        //    lb_error.Text = "帳號不可為空!";
        //}
    }


}