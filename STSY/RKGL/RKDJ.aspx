<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RKDJ.aspx.cs" Inherits="STSY.RKGL.RKDJ"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>入库登记</b>
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
                    <asp:Button ID="btnXZ" runat="server" Text="新增" CssClass="sonluk-button" OnClick="btnXZ_Click" />
                                    <asp:Button ID="btnBJ" runat="server" Text="编辑" CssClass="sonluk-button" OnClick="btnBJ_Click" />
                                    &nbsp;
                    <asp:Button ID="btnaddrow" runat="server" Text="增行" CssClass="sonluk-button" OnClick="btnaddrow_Click" />
                                    <asp:Button ID="btndeleterow" runat="server" Text="删行" CssClass="sonluk-button" OnClick="btndeleterow_Click" />
                                    &nbsp;
                    <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                    &nbsp;
                    <asp:Button ID="btnTJ" runat="server" Text="提交审核" CssClass="sonluk-button" OnClick="btnTJ_Click" />
                                    &nbsp;
                    <asp:Button ID="btnWLTJ" runat="server" Text="物料添加" CssClass="sonluk-button" OnClick="btnWLTJ_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="BL" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbRI" runat="server" Text="0" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbISADD" runat="server" Text="true" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbISED" runat="server" Text="false" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbnStatus" runat="server" Text="0" Visible="false"></asp:Label>
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
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text="物料类别" Width="70"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlWLLB" runat="server">
                                                                </asp:DropDownList>
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
                                                                    <asp:TemplateField HeaderText="选择">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbXZ" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="操作">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton Text="插入" ID="lbtnCR" runat="server" OnClick="lbtnCR_OnClick" ForeColor="red">插入</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="类别">
                                                                        <ItemTemplate>
                                                                            <%--                                                                            <asp:DropDownList ID="ddlLBBH" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLBBH_SelectedIndexChanged"
                                                                                onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                            </asp:DropDownList>--%>
                                                                            <asp:Label ID="lbLB" runat="server" Text='<%# Bind("LBBH") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="物料">
                                                                        <ItemTemplate>
                                                                            <%--                                                                            <asp:DropDownList ID="ddlLBMC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLBMC_SelectedIndexChanged"
                                                                                onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                            </asp:DropDownList>--%>
                                                                            <asp:Label ID="lbWL" runat="server" Text='<%# Bind("LBMC") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="物料规格">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtWLGG" runat="server" Text='<%# Bind("WLGG") %>' Width="90" onkeydown="if(event.keyCode==13){event.keyCode=9;}"
                                                                                Enabled="false">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单位">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDW" runat="server" Text='<%# Bind("DW") %>' Width="50" onkeydown="if(event.keyCode==13){event.keyCode=9;}"
                                                                                Enabled="false">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="数量">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSL" runat="server" Text='<%# Bind("SL") %>' Width="60" onkeydown="if(event.keyCode==13){event.keyCode=9;event.keyCode=9;}"
                                                                                AutoPostBack="true" OnTextChanged="txtSL_TextChanged" CssClass="sonluk-text-right" onclick="this.select()">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="单价">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDJ" runat="server" Text='<%# Bind("DJ") %>' Width="60" onkeydown="if(event.keyCode==13){event.keyCode=9;}"
                                                                                AutoPostBack="true" OnTextChanged="txtDJ_TextChanged" CssClass="sonluk-text-right" onclick="this.select()">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="总金额">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtZJE" runat="server" Text='<%# Bind("ZJE") %>' Width="70" onkeydown="if(event.keyCode==13){event.keyCode=9;}"
                                                                                Enabled="false" CssClass="sonluk-text-right">
                                                                            </asp:TextBox>
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
                                                                            <asp:TextBox ID="txtSM" runat="server" Text='<%# Bind("SM") %>' Width="110" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                            </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="RuKMXID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="RuKMXID" runat="server" Text='<%# Bind("RuKMXID") %>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="materialID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="materialID" runat="server" Text='<%# Bind("materialID") %>'>
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
                                                                <asp:TextBox ID="txtRKBZ" runat="server" Width="470" Height="50" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="制单人" Width="70"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZDR" runat="server" Width="200" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="制单时间"></asp:Label>
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>

                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gdRKDLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDLB_RowDataBound" CellPadding="0">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="操作">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="red">查看</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="序号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="XH" runat="server" Text='<%# Bind("XH") %>'>
                                                                                </asp:Label>
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
                                                                        <asp:TemplateField HeaderText="供应商编号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="GYSBH" runat="server" Text='<%# Bind("ClSN") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="供应商名称">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="GYSMC" runat="server" Text='<%# Bind("ClName") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="入库单编号">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RKDBH" runat="server" Text='<%# Bind("RuKDNO") %>'>
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
                                                                        <asp:TemplateField HeaderText="入库日期">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RKRQ" runat="server" Text='<%# Bind("RuKDate1") %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="入库备注">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="RKBZ" runat="server" Text='<%# Bind("RuKMem") %>'>
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
                                                                                <asp:Label ID="ZDSJ" runat="server" Text='<%# Bind("fillTime1") %>'>
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
