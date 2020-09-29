<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CKXX.aspx.cs" Inherits="STSY.CKGL.CKXX"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>仓库管理</b>
    </div>
    <div align="center">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSX" runat="server" Text="刷新" CssClass="sonluk-button" OnClick="btnSX_Click" />
                    &nbsp;
                    <asp:Button ID="btnZJCK" runat="server" Text="增加仓库" CssClass="sonluk-button" OnClick="btnZJCK_Click" />
                    <asp:Button ID="btnSCCK" runat="server" Text="删除仓库" CssClass="sonluk-button" OnClick="btnSCCK_Click" />
                    &nbsp;
                    <asp:Button ID="btnZJKW" runat="server" Text="增加库位" CssClass="sonluk-button" OnClick="btnZJKW_Click" />
                    <asp:Button ID="btnSCKW" runat="server" Text="删除库位" CssClass="sonluk-button" OnClick="btnSCKW_Click" />
                    &nbsp;
                    <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <table align="center" border="2">
            <tr>
                <td width="80" align="center">
                    编号
                </td>
                <td width="220" align="center">
                    名称
                </td>
                <td width="500" rowspan="2" style="vertical-align: top">
                    <div id="divBT" runat="server">
                        <%--                        <table width="500">
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="cb1" runat="server" Text="新增时保留上一条记录信息" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="cb2" runat="server" Text="保留后新增记录" />
                                </td>
                            </tr>
                        </table>--%>
                    </div>
                    <div id="divLB" runat="server">
                        <table width="500">
                            <tr>
                                <td width="80">
                                    仓库编号
                                </td>
                                <td width="420">
                                    <asp:TextBox ID="txtCKBH" runat="server" Width="420"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="80">
                                    仓库名称
                                </td>
                                <td width="420">
                                    <asp:TextBox ID="txtCKMC" runat="server" Width="420"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="80">
                                    关账日
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGZR" runat="server" Width="80">
                                        <asp:ListItem Value="0" Text="=请选择="></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="cbKSSY" runat="server" Text="开始使用" Enabled="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 当前期数
                                    <asp:TextBox ID="txtDQQS" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    年期数
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNQS" runat="server" Width="80"></asp:TextBox>
                                    <asp:CheckBox ID="CBKC" runat="server" Text="库存必须为正数" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divKW" runat="server">
                        <table width="500">
                            <tr>
                                <td width="50">
                                    库号
                                </td>
                                <td width="200">
                                    <asp:DropDownList ID="ddlKH" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddlKH_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td width="50">
                                    库名
                                </td>
                                <td width="200">
                                    <asp:TextBox ID="txtKM" runat="server" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="50">
                                    编号
                                </td>
                                <td width="200">
                                    <asp:TextBox ID="txtBH" runat="server" Width="200"></asp:TextBox>
                                </td>
                                <td width="50">
                                    名称
                                </td>
                                <td width="200">
                                    <asp:TextBox ID="txtMC" runat="server" Width="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="50">
                                    备注
                                </td>
                                <td width="450" colspan="3">
                                    <asp:TextBox ID="txtBZ" runat="server" Width="450" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="300" colspan="2" align="left" style="vertical-align: top">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="News" NodeIndent="10" ExpandDepth="0"
                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <HoverNodeStyle Font-Underline="True" />
                        <NodeStyle Font-Names="Arial" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
