<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="STSY.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="customers" border="1" cellspacing="0" cellpadding="0">
                <tr>
                    <th>Company</th>
                    <th>Contact</th>
                    <th>Country</th>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Apple</td>
                    <td>Steven Jobs</td>
                    <td>USA</td>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Baidu</td>
                    <td>Li YanHong</td>
                    <td>China</td>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Google</td>
                    <td>Larry Page</td>
                    <td>USA</td>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Lenovo</td>
                    <td>Liu Chuanzhi</td>
                    <td>China</td>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Microsoft</td>
                    <td>Bill Gates</td>
                    <td>USA</td>
                </tr>

                <tr onmouseover="style.backgroundColor='#CCCCCC'" onmouseout="style.backgroundColor='#FFFFFF'">
                    <td>Nokia</td>
                    <td>Stephen Elop</td>
                    <td>Finland</td>
                </tr>


            </table>
        </div>
    </form>
</body>
</html>
