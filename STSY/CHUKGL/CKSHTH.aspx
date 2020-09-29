<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CKSHTH.aspx.cs" Inherits="STSY.CHUKGL.CKSHTH" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>出库审核退回</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCXSX" runat="server" Text="查询刷新" CssClass="sonluk-button" OnClick="btnCXSX_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnDJTH" runat="server" Text="单据退回" CssClass="sonluk-button" OnClick="btnDJTH_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="BL" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbRI" runat="server" Text="0"></asp:Label>
                                                    <asp:Label ID="lbISLB" runat="server" Text="TRUE"></asp:Label>
                                                    <asp:Label ID="lbnStatus" runat="server" Text="0"></asp:Label>
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
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lbtnCKDLB" runat="server" OnClick="lbtnCKDLB_Click">出库单列表</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="lbtnCKDTX" runat="server" OnClick="lbtnCKDTX_Click">出库单填写</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRKDTX" align="left" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="仓库" Width="60"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCKBH" runat="server" Width="310">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="客户" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlKHBH" runat="server" Width="310">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="出库日期" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCKRQ" runat="server" onClick="WdatePicker()"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="出库类型" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCKLX" runat="server">
                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="0 其他出库"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 销售出库"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 生产出库"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 采购退货"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 仓库盘亏"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="出库单号" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCKDH" runat="server" Width="310"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <asp:GridView ID="gdCKDH" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdCKDH_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="编号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BH" runat="server" Text='<%# Bind("mtSN") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="物料名称">
                                                        <ItemTemplate>
                                                            <asp:Label ID="WLMC" runat="server" Text='<%# Bind("mtName") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="规格">
                                                        <ItemTemplate>
                                                            <asp:Label ID="GG" runat="server" Text='<%# Bind("mtSpec") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单位">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DW" runat="server" Text='<%# Bind("mtUnit") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="数量">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:Label ID="mtNumber" runat="server" Text='<%# Bind("mtNumber") %>'>
                                                                </asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单价">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:Label ID="DJ" runat="server" Text='<%# Bind("mtPrice") %>'>
                                                                </asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总金额">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:Label ID="mtTotal" runat="server" Text='<%# Bind("mtTotal") %>'>
                                                                </asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="库位名称">
                                                        <ItemTemplate>
                                                            <asp:Label ID="KWMC" runat="server" Text='<%# Bind("PlaceName") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="说明">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SM" runat="server" Text='<%# Bind("MXMemo") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td width="100" align="left">出库备注
                                            </td>
                                            <td colspan="3" width="500">
                                                <asp:TextBox ID="txtCKBZ" runat="server" Width="500" Height="50" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">制单人
                                            </td>
                                            <td width="200">
                                                <asp:TextBox ID="txtZDR" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                            <td width="100">制单时间
                                            </td>
                                            <td width="200">
                                                <asp:TextBox ID="txtZDSJ" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divRKDLB" runat="server" align="left">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">未提交</asp:ListItem>
                                        <asp:ListItem Value="1">已提交</asp:ListItem>
                                        <asp:ListItem Value="2">已审核</asp:ListItem>
                                        <asp:ListItem Value="3">全部</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="仓库" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBCKBH" runat="server" Width="310">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="客户" Width="60"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlLBKHBH" runat="server" Width="310">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="出库日期" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBCKRQS" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                                到
                                                <asp:TextBox ID="txtLBCKRQE" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="出库类型" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBCKLX" runat="server">
                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="0 其他出库"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 销售出库"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 生产出库"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 采购退货"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 仓库盘亏"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="出库单号" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBCKDH" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdCKDLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdCKDLB_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="操作">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="仓库编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKBH" runat="server" Text='<%# Bind("StockSN") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="仓库名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKMC" runat="server" Text='<%# Bind("StockNAME") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="客户编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="KHBH" runat="server" Text='<%# Bind("ClSN") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="客户名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="KHMC" runat="server" Text='<%# Bind("ClName") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="出库单编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKDBH" runat="server" Text='<%# Bind("ChuKDNO") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="账期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZQ" runat="server" Text='<%# Bind("SMonY") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="审核">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbSH" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="出库日期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKRQ" runat="server" Text='<%# Bind("ChuKDate1") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="出库说明">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKSM" runat="server" Text='<%# Bind("ChuKMem") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="制单人">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDR" runat="server" Text='<%# Bind("fillName") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="制单时间">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDSJ" runat="server" Text='<%# Bind("fillTime") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ChuKDID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ChuKDID" runat="server" Text='<%# Bind("ChuKDID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="nStatus" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="nStatus" runat="server" Text='<%# Bind("nStatus") %>'>
                                                                </asp:Label>
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
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
