<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test2.aspx.cs" Inherits="test2" %>
<%@ Import Namespace="System.IO" %>  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  

<script runat="server">  
    protected void Button1_Click(object sender, System.EventArgs e) {
        string fileName = "123.jpg";
        string deleteFolder = Request.PhysicalApplicationPath + "images\\clothes\\";

        try
        {
            File.Delete(deleteFolder+fileName);
        }
        catch(System.IO.IOException ex)
        {

        }


        //string uploadFolder = Request.PhysicalApplicationPath + "images\\clothes\\";  
        //if (FileUpload1.HasFile)  
        //{  
        //    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);  
        //    FileUpload1.SaveAs(uploadFolder + "Test"+ extension);  
        //    Label1.Text = "File uploaded successfully as: " + "Test"+ extension;  
        //}  
        //else  
        //{  
        //    Label1.Text = "First select a file.";  
        //}  
    }
</script>  

<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">  
    <title>asp.net FileUpload example: how to rename file when upload (change file name when upload)</title>  
</head>  
<body>  
    <form id="form1" runat="server">  
    <div>  
        <h2 style="color:Green">asp.net FileUpload example: File Rename</h2>  
        <asp:Label   
             ID="Label1"   
             runat="server"   
             Font-Size="Large"  
             ForeColor="OrangeRed"  
             >  
        </asp:Label>  
        <br /><br />  
        <asp:FileUpload   
             ID="FileUpload1"   
             runat="server"   
             BackColor="DeepPink"   
             ForeColor="AliceBlue"   
             />  
        <asp:Button   
             ID="Button1"   
             runat="server"   
             Font-Bold="true"   
             ForeColor="DeepPink"   
             OnClick="Button1_Click"  
             Text="Upload It"  
             />     
    </div>  
    </form>  
    <asp:FileUpload ID="FU1" runat="server" onclick="Button3_Click"/><asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" Enabled="false"/><br/>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</body>  
</html>  
