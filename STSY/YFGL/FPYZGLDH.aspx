<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPYZGLDH.aspx.cs" Inherits="STSY.YFGL.FPYZGLDH"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>添加关联表单</b>
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
                                    <asp:Button ID="btnDC" runat="server" Text="导出" CssClass="sonluk-button" OnClick="btnDC_Click" />
                                    &nbsp;
                    <asp:Button ID="btnQRTJ" runat="server" Text="确认添加" CssClass="sonluk-button"
                        OnClick="btnQRTJ_Click" />
                                    &nbsp;
                    <asp:Button ID="btnTC" runat="server" Text="退出" CssClass="sonluk-button" OnClick="btnTC_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divLabel" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbRI" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbRKDRI" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbISSHOW" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbLB" runat="server" Text="0" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbGYS" runat="server" Text="0"></asp:Label>
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
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">入库单列表</asp:LinkButton>
                                    |
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">入库单基本信息</asp:LinkButton>
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
                                                                <asp:DropDownList ID="ddlCKBH" runat="server" Width="320" Enabled="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="供应商" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlGYSBH" runat="server" Width="320" Enabled="false">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="入库日期" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRKRQ" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="入库类型" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRKLX" runat="server" Enabled="false">
                                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="0 其他入库"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="1 采购入库"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="2 生产入库"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="3 销售退货"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="5 仓库盘盈"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="入库单号" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRKDH" runat="server" Width="310" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <asp:GridView ID="gdRKDH" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDH_RowDataBound" CellPadding="0" ShowFooter="true">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="类别编号">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlLBBH" runat="server" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="类别编号">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlLBMC" runat="server" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="物料规格">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtWLGG" runat="server" Text='<%# Bind("WLGG") %>' Width="90" Enabled="false">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单位">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDW" runat="server" Text='<%# Bind("DW") %>' Width="50" Enabled="false">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="数量">
                                                                        <ItemTemplate>
                                                                            <div style="float: right; text-align: right">
                                                                                <asp:TextBox ID="txtSL" runat="server" Text='<%# Bind("SL") %>' Width="60" Enabled="false">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单价">
                                                                        <ItemTemplate>
                                                                            <div style="float: right; text-align: right">
                                                                                <asp:TextBox ID="txtDJ" runat="server" Text='<%# Bind("DJ") %>' Width="60" Enabled="false">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="总金额">
                                                                        <ItemTemplate>
                                                                            <div style="float: right; text-align: right">
                                                                                <asp:TextBox ID="txtZJE" runat="server" Text='<%# Bind("ZJE") %>' Width="70" Enabled="false">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="库位名称">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlKWMC" runat="server" Enabled="false">
                                                                            </asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="说明">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSM" runat="server" Text='<%# Bind("SM") %>' Width="110" Enabled="false">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RuKMXID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="RuKMXID" runat="server" Text='<%# Bind("RuKMXID") %>' Enabled="false">
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
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="入库备注" Width="60"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtRKBZ" runat="server" Width="500" Height="50" TextMode="MultiLine"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="制单人" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZDR" runat="server" Width="200" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="制单时间" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZDSJ" runat="server" Width="200" Enabled="false"></asp:TextBox>
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
                                                        <asp:ListItem Value="0">已审核未关联发票</asp:ListItem>
                                                        <asp:ListItem Value="1">已审核入库单 </asp:ListItem>
                                                        <asp:ListItem Value="2">已关联发票且发票已审核</asp:ListItem>
                                                        <asp:ListItem Value="3">全部</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text="仓库" Width="60"></asp:Label>
                                                            </td>
                                                            <td colspan="4">
                                                                <asp:DropDownList ID="ddlLBCKBH" runat="server" Width="400">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text="供应商" Width="60"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLBGYSBH" runat="server" Width="320">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="cbRKRQ" runat="server" AutoPostBack="true" OnCheckedChanged="cbRKRQ_CheckedChanged" />
                                                            </td>
                                                            <td>入库日期
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLBRKRQS" runat="server" onClick="WdatePicker()" Width="90"></asp:TextBox>
                                                                到
                                                            <asp:TextBox ID="txtLBRKRQE" runat="server" onClick="WdatePicker()" Width="90"></asp:TextBox>
                                                            </td>
                                                            <td>入库类型
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlLBRKLX" runat="server">
                                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="0 其他入库"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="1 采购入库"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="2 生产入库"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="3 销售退货"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="5 仓库盘盈"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>入库单号
                                                            </td>
                                                            <td width="250">
                                                                <asp:TextBox ID="txtLBRKDH" runat="server" Width="310"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table align="left">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gdRKDLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDLB_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="操作">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="选择">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="cbXZ" runat="server"/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="仓库编号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="StockSN" runat="server" Text='<%# Bind("StockSN") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="仓库名称">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="StockName" runat="server" Text='<%# Bind("StockNAME") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="供应商编号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClSN" runat="server" Text='<%# Bind("ClSN") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="供应商名称">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ClName" runat="server" Text='<%# Bind("ClName") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="入库单编号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RuKDNO" runat="server" Text='<%# Bind("RuKDNO") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="总金额">
                                                                            <ItemTemplate>
                                                                                <div style="float: right; text-align: right">
                                                                                    <asp:Label ID="RuKTotal" runat="server" Text='<%# Bind("RuKTotal") %>'>
                                                                                    </asp:Label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="账期">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="SMonY" runat="server" Text='<%# Bind("SMonY") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="审核">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="cbSH" runat="server" Enabled="false" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="入库日期">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RuKDate" runat="server" Text='<%# Bind("RuKDate1") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="入库备注">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RuKMem" runat="server" Text='<%# Bind("RuKMem") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="制单人">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="fillName" runat="server" Text='<%# Bind("fillName") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="制单时间">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="fillTime" runat="server" Text='<%# Bind("fillTime") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="RuKDID" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RuKDID" runat="server" Text='<%# Bind("RuKDID") %>'>
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
