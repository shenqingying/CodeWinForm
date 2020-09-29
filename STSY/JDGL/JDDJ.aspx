<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JDDJ.aspx.cs" Inherits="STSY.JDGL.JDDJ" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>接待登记</b>
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
                                    <asp:Button ID="btnADD" runat="server" Text="新增" CssClass="sonluk-button" OnClick="btnADD_Click" />
                                    <asp:Button ID="btnED" runat="server" Text="编辑" CssClass="sonluk-button" OnClick="btnED_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnADDROW" runat="server" Text="增行" CssClass="sonluk-button" OnClick="btnADDROW_Click" />
                                    <asp:Button ID="btnDELETEROW" runat="server" Text="删行" CssClass="sonluk-button" OnClick="btnDELETEROW_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnDJTJ" runat="server" Text="单据提交" CssClass="sonluk-button" OnClick="btnDJTJ_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="BL" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbISLB" runat="server" Text="FALSE"></asp:Label>
                                    <asp:Label ID="lbRI" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbZDSTATUS" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbISED" runat="server" Text="FALSE"></asp:Label>
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
                                    <asp:LinkButton ID="lbtnJDDJ" runat="server" OnClick="lbtnJDDJ_Click">接待登记</asp:LinkButton>
                                    |
                                <asp:LinkButton ID="lbtnJDDJLB" runat="server" OnClick="lbtnJDDJLB_Click">接待登记列表</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divJDDJ" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="通知部门"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTZBM" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="联系人"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLXR" runat="server"></asp:DropDownList>
                                            </td>
                                            <%--                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="联系电话" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLXDH" runat="server" Width="100"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gdZDDJ" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdZDDJ_RowDataBound" CellPadding="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="选择">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbXZ" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="类型">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlLX" runat="server" onkeydown="if(event.keyCode==13){event.keyCode=9;}" AutoPostBack="true" OnSelectedIndexChanged="ddlLX_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求日期">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtXQRQ" runat="server" Text='<%# Bind("XQRQ") %>' Width="90" onkeydown="if(event.keyCode==13){event.keyCode=9;}" onClick="WdatePicker()">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求时间">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlXQSJ" runat="server" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                    <asp:ListItem Value="0">=请选择=</asp:ListItem>
                                                                    <asp:ListItem Value="8:00">8:00</asp:ListItem>
                                                                    <asp:ListItem Value="8:30">8:30</asp:ListItem>
                                                                    <asp:ListItem Value="9:00">9:00</asp:ListItem>
                                                                    <asp:ListItem Value="9:30">9:30</asp:ListItem>
                                                                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                                                    <asp:ListItem Value="10:30">10:30</asp:ListItem>
                                                                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                                                    <asp:ListItem Value="11:30">11:30</asp:ListItem>
                                                                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                                                    <asp:ListItem Value="12:30">12:30</asp:ListItem>
                                                                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
                                                                    <asp:ListItem Value="13:30">13:30</asp:ListItem>
                                                                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
                                                                    <asp:ListItem Value="14:30">14:30</asp:ListItem>
                                                                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
                                                                    <asp:ListItem Value="15:30">15:30</asp:ListItem>
                                                                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
                                                                    <asp:ListItem Value="16:30">16:30</asp:ListItem>
                                                                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="地点">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDD" runat="server" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:TextBox ID="txtSL" runat="server" Text='<%# Bind("SL") %>' Width="60" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                    </asp:TextBox>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="已确认">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbYQR" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="已结束">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbYJS" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求说明">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtXQSM" runat="server" Text='<%# Bind("XQSM") %>' Width="110" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
                                                <asp:Label ID="Label4" runat="server" Text="备注" Width="60"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtBZ" runat="server" Width="420" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="制单人" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtZDR" runat="server" Width="160"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="制单时间" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtZDSJ" runat="server" Width="160"></asp:TextBox>
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
                    <div id="divJDDJLB" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="6">
                                                <asp:RadioButtonList ID="rbLB" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">未提交</asp:ListItem>
                                                    <asp:ListItem Value="1">已提交未确认</asp:ListItem>
                                                    <asp:ListItem Value="2">已确认未结束</asp:ListItem>
                                                    <asp:ListItem Value="3">已结束</asp:ListItem>
                                                    <asp:ListItem Value="4">全部</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="通知部门" Width="50"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBTZBM" runat="server" Width="100"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text="联系人" Width="50"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBLXR" runat="server" Width="100"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="备注" Width="50"></asp:Label>
                                            </td>
                                            <td colspan="5">
                                                <asp:TextBox ID="txtLBBZ" runat="server" Width="450"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:CheckBox ID="cbLBXQRQ" runat="server" Text="需求日期" OnCheckedChanged="cbLBXQRQ_CheckedChanged" AutoPostBack="true" />
                                                &nbsp;
                                                <asp:TextBox ID="txtLBXQRQS" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                                到
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBXQRQE" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="地点" Width="50"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBDD" runat="server" Width="100"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text="类型" Width="50"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBLX" runat="server" Width="100"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="需求说明" Width="50"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLBXQSM" runat="server" Width="150"></asp:TextBox>
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
                                                <asp:GridView ID="gdZDDJLB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdZDDJLB_RowDataBound" CellPadding="0">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="操作">
                                                            <ItemTemplate>
                                                                <asp:LinkButton Text="查看" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="Red">查看</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="招待ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDID" runat="server" Text='<%# Bind("ZDID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ZDXMID") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="通知部门">
                                                            <ItemTemplate>
                                                                <asp:Label ID="TZBM" runat="server" Text='<%# Bind("DEPTNAME") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="联系人">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LXR" runat="server" Text='<%# Bind("STAFFNAME") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="联系电话">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LXDH" runat="server" Text='<%# Bind("STAFFMOBLE") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="类型名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LXMC" runat="server" Text='<%# Bind("XMTYPE") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求日期">
                                                            <ItemTemplate>
                                                                <asp:Label ID="XQRQ" runat="server" Text='<%# Bind("XMDATE1") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求时间">
                                                            <ItemTemplate>
                                                                <asp:Label ID="XQSJ" runat="server" Text='<%# Bind("XMTIME") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="地点">
                                                            <ItemTemplate>
                                                                <asp:Label ID="DD" runat="server" Text='<%# Bind("XMADD") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="SL" runat="server" Text='<%# Bind("XMNUM") %>'>
                                                                    </asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="已确认">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="YQR" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="已结束">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="YJS" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="标识">
                                                            <ItemTemplate>
                                                                <asp:Label ID="BS" runat="server" Text='<%# Bind("ZDSTATUS") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="需求说明">
                                                            <ItemTemplate>
                                                                <asp:Label ID="XQSM" runat="server" Text='<%# Bind("XMMEMO") %>'>
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
                                                        <asp:TemplateField HeaderText="备注">
                                                            <ItemTemplate>
                                                                <asp:Label ID="BZ" runat="server" Text='<%# Bind("ZDMEMO") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ZDSTATUS" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ZDSTATUS" runat="server" Text='<%# Bind("ZDSTATUS") %>'>
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
