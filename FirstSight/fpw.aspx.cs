using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class fpw : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //try
        //{
        //    MailMessage msg = new MailMessage();
        //    msg.To.Add("victor135757@gmail.com");
        //    //msg.To.Add("b@b.com");可以發送給多人
        //    //msg.CC.Add("c@c.com");
        //    //msg.CC.Add("c@c.com");可以抄送副本給多人 
        //    //這裡可以隨便填，不是很重要
        //    msg.From = new MailAddress("jay05eternal@gmail.com", "阿光", System.Text.Encoding.UTF8);
        //    /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
        //    msg.Subject = "測試標題";//郵件標題
        //    msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
        //    msg.Body = "測試一下"; //郵件內容
        //    msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
        //    //msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
        //    msg.IsBodyHtml = true;//是否是HTML郵件 
        //                          //msg.Priority = MailPriority.High;//郵件優先級 

        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new System.Net.NetworkCredential("XXXX@gmail.com", "*****"); //這裡要填正確的帳號跟密碼
        //    client.Host = "smtp.gmail.com"; //設定smtp Server
        //    client.Port = 25; //設定Port
        //    client.EnableSsl = true; //gmail預設開啟驗證
        //    client.Send(msg); //寄出信件
        //    client.Dispose();
        //    msg.Dispose();
            
        //}
        //catch (Exception ex)
        //{
            
        //}

    }
}