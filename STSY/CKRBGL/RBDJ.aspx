<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RBDJ.aspx.cs" Inherits="STSY.CKRBGL.RBDJ" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>日报登记</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSX" runat="server" Text="刷新" CssClass="sonluk-button" OnClick="btnSX_Click" />
                                                <asp:Button ID="btnXZJL" runat="server" Text="新增记录" CssClass="sonluk-button" OnClick="btnXZJL_Click" />
                                                <asp:Button ID="btnSCJL" runat="server" Text="删除记录" CssClass="sonluk-button" OnClick="btnSCJL_Click" />
                                                <asp:Button ID="btnXG" runat="server" Text="修改" CssClass="sonluk-button" OnClick="btnXG_Click" />
                                                <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                                                <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                            </td>
                                        </tr>
                                    </table>
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
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="1">
                        <tr style="vertical-align: top">
                            <td>
                                <table>
                                    <tr>
                                        <td>
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
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gdRBDJ" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRBDJ_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="操作">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="序号">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="XH" runat="server" Text='<%# Bind("XH") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="日期">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="UseDate" runat="server" Text='<%# Bind("UseDate1") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="食堂名称">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="StockName" runat="server" Text='<%# Bind("StockName") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DayUseID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="DayUseID" runat="server" Text='<%# Bind("DayUseID") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label3" runat="server" Text="日报登记记录" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="仓库" Width="60"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCKBH" runat="server" Width="150">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="发生日期" Width="60"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFSRQ" runat="server" onClick="WdatePicker()" Width="150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="填写人" Width="60"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTXR" runat="server" Width="145"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="填写时间" Width="60"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTXSJ" runat="server" Width="150"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="说明" Width="60"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtSM" runat="server" Width="365" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="项目列表" Width="60"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:GridView ID="gdRBXM" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRBXM_RowDataBound" CellPadding="0">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemID" runat="server" Text='<%# Bind("ItemID") %>' Width="100">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="项目名称">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ItemName" runat="server" Text='<%# Bind("ItemName") %>' Width="120">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="数量">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:TextBox ID="userNumber" runat="server" Text='<%# Bind("userNumber") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}" Width="140">
                                                                </asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
