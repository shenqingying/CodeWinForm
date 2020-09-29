<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYRBB.aspx.cs" Inherits="STSY.CKRBGL.HYRBB" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>耗用日报表</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSCBB" runat="server" Text="生成报表" CssClass="sonluk-button" OnClick="btnSCBB_Click" />
                                    <asp:Button ID="btnDC" runat="server" Text="导出" CssClass="sonluk-button" OnClick="btnDC_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="年份" Width="40"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtYEAR" runat="server" Width="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="月份" Width="40"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMOUTH" runat="server" Width="30"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="类别种类" Width="60"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbl1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">数量</asp:ListItem>
                                        <asp:ListItem Value="1">金额</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLBZL" runat="server" Width="150">
                                        <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                        <asp:ListItem Value="0" Text="A"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="B"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="仓库" Width="40"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlCKBH" runat="server" Width="150">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="类别" Width="40"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLB" runat="server" Width="100">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" ShowFooter="true"></asp:GridView>
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
