<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FKQR.aspx.cs" Inherits="STSY.YFGL.FKQR" MasterPageFile="~/sonluk.Master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>付款确认</b>
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
                                    <asp:Button ID="btnQRFK" runat="server" Text="确认付款" CssClass="sonluk-button" OnClick="btnQRFK_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divLabel" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbISLB" runat="server" Text="FALSE" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbCaiGFPID" runat="server" Text="0" Visible="false"></asp:Label>
                                                    <asp:Label ID="lbisPay" runat="server" Text="FALSE" Visible="false"></asp:Label>

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
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">发票查询列表</asp:LinkButton>
                        |
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">发票录入</asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divFPLR" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="供应商" Width="70"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlGYS" runat="server" Width="300">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Text="入账日期" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRZRQ" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label17" runat="server" Text="入账类型" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRZLX" runat="server" Width="100">
                                                    <asp:ListItem Value="0">=请选择=</asp:ListItem>
                                                    <asp:ListItem Value="1">主食</asp:ListItem>
                                                    <asp:ListItem Value="2">副食</asp:ListItem>
                                                    <asp:ListItem Value="3">其他</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label18" runat="server" Text="发票日期" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFPRQ" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label19" runat="server" Text="发票金额" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFPJE" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label20" runat="server" Text="付款金额" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFKJE" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbXJFK" runat="server" Text="现金付款" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbFKHDQR" runat="server" Text="付款回单确认" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label21" runat="server" Text="发票号码" Width="70"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFPHM" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>发票说明
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtFPSM" runat="server" TextMode="MultiLine" Width="300"></asp:TextBox>
                                            </td>
                                            <td>回单确认说明
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtHDQRSM" runat="server" TextMode="MultiLine" Width="300"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="vertical-align: top">
                                    <asp:GridView ID="gdRKDLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDLB_RowDataBound" ShowFooter="true" OnSelectedIndexChanging="gdRKDLB_SelectedIndexChanging" Width="250">
                                        <Columns>
                                            <asp:TemplateField HeaderText="入库单号">
                                                <ItemTemplate>
                                                    <asp:Label ID="RuKDNO" runat="server" Text='<%# Bind("RuKDNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="RuKTotal" runat="server" Text='<%# Bind("RuKTotal") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库日期">
                                                <ItemTemplate>
                                                    <asp:Label ID="RuKDate" runat="server" Text='<%# Bind("RuKDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RuKDID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="RuKDID" runat="server" Text='<%# Bind("RuKDID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="仓库编号"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKCKBH" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="仓库名称"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtRKCKMC" runat="server" Width="300"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="供应商编号"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKGYSBH" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="供应商名称"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtRKGYSMC" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="入库日期"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKRKRQ" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="入库类型"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRKRKLX" runat="server">
                                                    <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                                    <asp:ListItem Value="0" Text="0 其他入库"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="1 采购入库"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 生产入库"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="3 销售退货"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="5 仓库盘盈"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="入库单号"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKRKDH" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdRKDH" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDH_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="materialID" runat="server" Text='<%# Bind("materialID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtName" runat="server" Text='<%# Bind("mtName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料规格">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtSpec" runat="server" Text='<%# Bind("mtSpec") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtUnit" runat="server" Text='<%# Bind("mtUnit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtNumber" runat="server" Text='<%# Bind("mtNumber") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单价">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtPrice" runat="server" Text='<%# Bind("mtPrice") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="总金额">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtTotal" runat="server" Text='<%# Bind("mtTotal") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="库位名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PlaceName" runat="server" Text='<%# Bind("PlaceName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="说明">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MXMemo" runat="server" Text='<%# Bind("MXMemo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="入库备注"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtRKRKBZ" runat="server" TextMode="MultiLine" Height="50" Width="450"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="制单人"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKZDR" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="制单时间"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRKZDSJ" runat="server" Width="200"></asp:TextBox>
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



    <div id="divFPCXLB" runat="server" align="left">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rbCX" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">已审核未付款</asp:ListItem>
                                    <asp:ListItem Value="1">已付款</asp:ListItem>
                                    <asp:ListItem Value="2">已回单</asp:ListItem>
                                    <asp:ListItem Value="3">全部</asp:ListItem>
                                </asp:RadioButtonList>
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
                                <asp:Label ID="Label11" runat="server" Text="供应商"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLBGYSBH" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbLBRZRQ" runat="server" Text="入账日期从" OnCheckedChanged="cbLBRZRQ_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLBRZRQS" runat="server" onClick="WdatePicker()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="至"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLBRZRQE" runat="server" onClick="WdatePicker()"></asp:TextBox>
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
                                <asp:CheckBox ID="cbLBFPRQ" runat="server" Text="发票日期从" OnCheckedChanged="cbLBFPRQ_CheckedChanged" AutoPostBack="true" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLBFPRQS" runat="server" onClick="WdatePicker()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="至"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLBFPRQE" runat="server" onClick="WdatePicker()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="发票号码"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLBFPHM" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gdFPMXLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdFPMXLB_RowDataBound"
                        Width="1500" AllowPaging="True" PageSize="100">
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" Width="30" ForeColor="Red">查看</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:Label ID="ClientID" runat="server" Text='<%# Bind("ClientID") %>' Width="20px">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="供应商名称">
                                <ItemTemplate>
                                    <asp:Label ID="ClName" runat="server" Text='<%# Bind("ClName") %>' Width="100px">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发票号码">
                                <ItemTemplate>
                                    <asp:Label ID="CaiGFPNO" runat="server" Text='<%# Bind("CaiGFPNO") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发票日期">
                                <ItemTemplate>
                                    <asp:Label ID="FaPDate1" runat="server" Text='<%# Bind("FaPDate1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="入账日期">
                                <ItemTemplate>
                                    <asp:Label ID="ACDate1" runat="server" Text='<%# Bind("ACDate1") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类">
                                <ItemTemplate>
                                    <asp:Label ID="ACType" runat="server" Text='<%# Bind("ACType") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发票金额">
                                <ItemTemplate>
                                    <div style="float: right; text-align: right">
                                        <asp:Label ID="FaPTotal" runat="server" Text='<%# Bind("FaPTotal1") %>'>
                                        </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="入库金额">
                                <ItemTemplate>
                                    <div style="float: right; text-align: right">
                                        <asp:Label ID="RuKTotal" runat="server" Text='<%# Bind("RuKTotal1") %>'>
                                        </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="加扣款">
                                <ItemTemplate>
                                    <div style="float: right; text-align: right">
                                        <asp:Label ID="JKK" runat="server" Text='<%# Bind("JKK") %>'>
                                        </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核人">
                                <ItemTemplate>
                                    <asp:Label ID="AudiName" runat="server" Text='<%# Bind("AudiName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="已审核">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbYSH" runat="server" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核时间">
                                <ItemTemplate>
                                    <asp:Label ID="AudiTime" runat="server" Text='<%# Bind("AudiTime") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款确认">
                                <ItemTemplate>
                                    <asp:Label ID="PayName" runat="server" Text='<%# Bind("PayName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="已付款">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbYFK" runat="server" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="现金">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbXJ" runat="server" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="回单">
                                <ItemTemplate>
                                    <asp:Label ID="PayType" runat="server" Text='<%# Bind("PayType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款时间">
                                <ItemTemplate>
                                    <asp:Label ID="PayTime" runat="server" Text='<%# Bind("PayTime") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="付款金额">
                                <ItemTemplate>
                                    <div style="float: right; text-align: right">
                                        <asp:Label ID="PayTotal" runat="server" Text='<%# Bind("PayTotal1") %>'>
                                        </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="回单确认">
                                <ItemTemplate>
                                    <asp:Label ID="RePayName" runat="server" Text='<%# Bind("RePayName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="已回单">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbYHD" runat="server" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="回单时间">
                                <ItemTemplate>
                                    <asp:Label ID="RePayTime" runat="server" Text='<%# Bind("RePayTime") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="回单确认说明">
                                <ItemTemplate>
                                    <asp:Label ID="RePayMemo" runat="server" Text='<%# Bind("RePayMemo") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="填写人">
                                <ItemTemplate>
                                    <asp:Label ID="fillName" runat="server" Text='<%# Bind("fillName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="填写时间">
                                <ItemTemplate>
                                    <asp:Label ID="fillTime" runat="server" Text='<%# Bind("fillTime") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="发票说明">
                                <ItemTemplate>
                                    <asp:Label ID="FaPMem" runat="server" Text='<%# Bind("FaPMem") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CaiGFPID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="CaiGFPID" runat="server" Text='<%# Bind("CaiGFPID") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="isPay" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="isPay" runat="server" Text='<%# Bind("isPay") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
