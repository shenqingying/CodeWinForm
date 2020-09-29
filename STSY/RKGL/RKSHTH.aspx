<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RKSHTH.aspx.cs" Inherits="STSY.RKGL.RKSHTH"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>入库审核退回</b>
    </div>
    <div align="left">
        <table width="1000">
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
                                    <asp:Label ID="lbnStatus" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbISHAVEFP" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div align="left">
        <table align="left" width="1100">
            <tr>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" BackColor="#cccccc" OnClick="LinkButton1_Click">入库单填写</asp:LinkButton>
                    |
                    <asp:LinkButton ID="LinkButton2" runat="server" BackColor="#ffffff" OnClick="LinkButton2_Click">入库单列表</asp:LinkButton>
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
                                                <asp:Label ID="Label1" runat="server" Text="仓库编号" Width="70"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCKBH" runat="server" Width="280">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="供应商编号" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlGYSBH" runat="server" Width="280">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="入库日期"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKRQ" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="入库类型" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRKLX" runat="server">
                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="0 其他入库"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 采购入库"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 生产入库"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 销售退货"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 仓库盘盈"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="入库单号" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKDH" runat="server" Width="280"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="left">
                                        <tr>
                                            <asp:GridView ID="gdRKDH" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" CellPadding="0" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="选择">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbXZ" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="类别编号">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLBBH" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="类别编号">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlLBMC" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="物料规格">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWLGG" runat="server" Text='<%# Bind("WLGG") %>' Width="90" ReadOnly="True">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单位">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDW" runat="server" Text='<%# Bind("DW") %>' Width="50" ReadOnly="True">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="数量">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:TextBox ID="txtSL" runat="server" Text='<%# Bind("SL") %>' Width="60">
                                                                </asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="单价">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:TextBox ID="txtDJ" runat="server" Text='<%# Bind("DJ") %>' Width="60">
                                                                </asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="总金额">
                                                        <ItemTemplate>
                                                            <div style="float: right; text-align: right">
                                                                <asp:TextBox ID="txtZJE" runat="server" Text='<%# Bind("ZJE") %>' Width="70" ReadOnly="True">
                                                                </asp:TextBox>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="库位名称">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlKWMC" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="说明">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSM" runat="server" Text='<%# Bind("SM") %>' Width="110">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RuKMXID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RuKMXID" runat="server" Text='<%# Bind("RuKMXID") %>'>
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
                                            <td width="100" align="left">入库备注
                                            </td>
                                            <td colspan="3" width="500">
                                                <asp:TextBox ID="txtRKBZ" runat="server" Width="500" Height="50" TextMode="MultiLine"></asp:TextBox>
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
                                        <asp:ListItem Value="4">已审核未关联发票</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="仓库编号" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBCKBH" runat="server" Width="280">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="供应商编号" Width="70"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlLBGYSBH" runat="server" Width="280">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="入库日期" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBRKRQS" runat="server" onClick="WdatePicker()" Width="120"></asp:TextBox>
                                                到
                                                            <asp:TextBox ID="txtLBRKRQE" runat="server" onClick="WdatePicker()" Width="120"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="入库类型" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBRKLX" runat="server" Width="90">
                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="0 其他入库"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 采购入库"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 生产入库"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 销售退货"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 仓库盘盈"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text="入库单号" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBRKDH" runat="server" Width="110"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdRKDLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDLB_RowDataBound" CellPadding="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="操作">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="仓库编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKBH" runat="server" Text='<%# Bind("StockSN") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="仓库名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CKMC" runat="server" Text='<%# Bind("StockNAME") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="供应商编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GYSBH" runat="server" Text='<%# Bind("ClSN") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="供应商名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="GYSMC" runat="server" Text='<%# Bind("ClName") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="入库单编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RKDBH" runat="server" Text='<%# Bind("RuKDNO") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="账期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZQ" runat="server" Text='<%# Bind("SMonY") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="审核">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbSH" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="入库日期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RKRQ" runat="server" Text='<%# Bind("RuKDate1") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="入库备注">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RKBZ" runat="server" Text='<%# Bind("RuKMem") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="制单人">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDR" runat="server" Text='<%# Bind("fillName") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="制单时间">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDSJ" runat="server" Text='<%# Bind("fillTime") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RuKDID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RuKDID" runat="server" Text='<%# Bind("RuKDID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="nStatus" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="nStatus" runat="server" Text='<%# Bind("nStatus") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISHAVEFP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ISHAVEFP" runat="server" Text='<%# Bind("ISHAVEFP") %>'>
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
