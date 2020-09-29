<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KHGYS.aspx.cs" Inherits="STSY.CKGL.KHGYS" MasterPageFile="~/sonluk.Master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>客户供应商</b>
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
                                    <asp:Button ID="btnXZJL" runat="server" Text="新增" CssClass="sonluk-button" OnClick="btnXZJL_Click" />
                                    <asp:Button ID="btnSCJL" runat="server" Text="删除" CssClass="sonluk-button" OnClick="btnSCJL_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnXG" runat="server" Text="修改" CssClass="sonluk-button" OnClick="btnXG_Click" />
                                    <asp:Button ID="btnBC" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnBC_Click" />
                                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divLB" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbRI" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <div id="divBT" runat="server" style="width: 350px; height: 500px; overflow: auto">
                                    <asp:GridView ID="gdKHGYS" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdKHGYS_RowDataBound" CellPadding="0">
                                        <Columns>
                                            <asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编号">
                                                <ItemTemplate>
                                                    <asp:Label ID="BH" runat="server" Text='<%# Bind("ClSN") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="MC" runat="server" Text='<%# Bind("ClName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="客户">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbKH" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="供应商">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbGYS" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ClientID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="ClientID" runat="server" Text='<%# Bind("ClientID") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="编号" Width="55"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBH" runat="server" Width="80"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="名称" Width="55"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMC" runat="server" Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="开户银行" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtKFYH" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="账号" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtZH" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="联系人" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtLXR" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="地址" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDZ" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="邮编" Width="55"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtYB" runat="server" Width="80"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="电话" Width="55"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDH" runat="server" Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="传真" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCZ" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="EMAIL" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtEMAIL" runat="server" Width="350"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:CheckBox ID="cbGYS" runat="server" Text="供应商" />
                                            <asp:CheckBox ID="cbKH" runat="server" Text="客户" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="备注" Width="55"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtBZ" runat="server" Width="350" TextMode="MultiLine" Height="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
