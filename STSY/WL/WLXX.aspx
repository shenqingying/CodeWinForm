<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WLXX.aspx.cs" Inherits="STSY.WL.WLXX"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>物料信息</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSX" runat="server" Text="刷新" CssClass="sonluk-button" OnClick="btnSX_Click" />
                                    &nbsp;
                    <asp:Button ID="btnZJLB" runat="server" Text="增加类别" CssClass="sonluk-button" OnClick="btnZJLB_Click" />
                                    <asp:Button ID="btnSCLB" runat="server" Text="删除类别" CssClass="sonluk-button" OnClick="btnSCLB_Click" />
                                    &nbsp;
                    <asp:Button ID="btnZJWL" runat="server" Text="增加物料" CssClass="sonluk-button" OnClick="btnZJWL_Click" />
                                    <asp:Button ID="btnSCWL" runat="server" Text="删除物料" CssClass="sonluk-button" OnClick="btnSCWL_Click" />
                                    &nbsp;
                    <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divLabel" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbISXZLB" runat="server" Text="FALSE"></asp:Label>
                                    <asp:Label ID="lbISXZWL" runat="server" Text="FALSE"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div align="left">
                        <table align="center" border="1">
                            <tr>
                                <td width="100" align="center">编号
                                </td>
                                <td width="100" align="center">名称
                                </td>
                                <td width="100" align="center">规格
                                </td>
                                <td rowspan="2" style="vertical-align: top">
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <div id="divLB" runat="server" align="left">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text="类别编号" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLBBH" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text="类别种类" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlLBZL" runat="server" Width="200">
                                                                        <asp:ListItem Value="-1" Text=""></asp:ListItem>
                                                                        <asp:ListItem Value="0" Text="A"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="B"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="C"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="类别名称" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtLBMC" runat="server" Width="450"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text="备注" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtLBBZ" runat="server" Width="450"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divWL" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text="类别" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:DropDownList ID="ddlWLLB" runat="server" Width="450">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text="编号" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLBH" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text="条码" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLTM" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text="名称" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtWLMC" runat="server" Width="450"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text="规格" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLGG" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text="单位" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLDW" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="单价" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtWLDJ" runat="server" Width="450"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="上限" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLSX" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="下限" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWLXX" runat="server" Width="200"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text="备注" Width="50"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtWLBZ" runat="server" Width="450"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left" style="vertical-align: top">
                                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="News" NodeIndent="10" ExpandDepth="0"
                                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" Width="300">
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
