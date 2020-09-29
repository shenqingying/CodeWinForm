<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="STSY.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>双鹿电池食堂系统</title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            overflow: hidden;
        }
        
        
        .style1
        {
            height: 30px;
        }
        
        
        .style2
        {
            width: 183px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="608" background="images/login_03.gif">
                <table width="862" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="266" background="images/login_04.gif">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="95">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="424" height="95" background="images/login_06.gif">
                                        &nbsp;
                                    </td>
                                    <td background="images/login_07.gif" class="style2">
                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 113%; height: 68px;">
                                            <tr>
                                                <td width="19%" class="style1">
                                                    <div align="center">
                                                        <span class="STYLE3">用户</span></div>
                                                </td>
                                                <td class="style1">
                                                    <asp:TextBox ID="txtname" Width="53%" runat="server" Style="margin-left: 0px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1">
                                                    <div align="center">
                                                        <span class="STYLE3">密码</span></div>
                                                </td>
                                                <td class="style1">
                                                    <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="53%"></asp:TextBox>
                                                    <td class="style1">
                                            </tr>
                                            <%--      <tr>
                                                <td height="30">
                                                    <div align="center" style="width: 69px">
                                                        <span class="STYLE3">验证码</span></div>
                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtCode" runat="server" Width="33%" style="vertical-align:middle" 
                                                        ontextchanged="txtCode_TextChanged"></asp:TextBox>
                                                        <img title="看不清？" src="ValidateCode.ashx" Width="33%" style=" cursor:pointer; vertical-align:middle" onclick="this.src='ValidateCode.ashx?_='+Math.random()"/>
                                                    </td>
                                            </tr>--%>
                                        </table>
                                    </td>
                                    <td width="255" background="images/login_08.gif">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="247" valign="top" background="images/login_09.gif">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="22%" height="30">
                                        &nbsp;
                                    </td>
                                    <td width="56%">
                                        &nbsp;
                                        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Style="margin-left: 261px"
                                            Text="登录" Width="52px" />
                                    </td>
                                    <td width="22%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td height="30">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="44%" height="20" style="text-align: right">
                                                    &nbsp;
                                                </td>
                                                <td width="56%">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td bgcolor="#a2d962">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
