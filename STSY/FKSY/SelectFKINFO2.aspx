<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectFKINFO2.aspx.cs" Inherits="STSY.FKSY.SelectFKINFO2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Content/sonluk-ui/common.css" rel="stylesheet" />
    <link href="~/Content/sonluk-ui/color.css" rel="stylesheet" />
    <link href="~/Content/sonluk-ui/layout.css" rel="stylesheet" />
    <link href="~/Content/sonluk-ui/nav-bar.css" rel="stylesheet" />
    <link href="~/Content/sonluk-ui/control.css" rel="stylesheet" />
    <script src="../Scripts/WdatePicker.js" type="text/javascript"></script>
    <meta name="viewport" content="width=device-width" />
    <style type="text/css">
        body {
            font-size: 12px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <div runat="server" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbSTAFFID" runat="server" Text=""></asp:Label>
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
                                        <asp:Button ID="btnCXSX" runat="server" Text="查询刷新" CssClass="sonluk-button" OnClick="btnCXSX_Click" />
                                        <asp:Button ID="btnDC" runat="server" Text="导出" CssClass="sonluk-button" OnClick="btnDC_Click" />
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
                                        <asp:Label ID="Label1" runat="server" Text="访客编号：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFKZH" runat="server" Width="120" CssClass="sonluk-text-box"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="访客姓名：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFKXM" runat="server" Width="120" CssClass="sonluk-text-box"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="来访日期：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFKLFSJS" runat="server" Width="100" CssClass="sonluk-text-box" onClick="WdatePicker()"></asp:TextBox>
                                        -
                                    <asp:TextBox ID="txtFKLFSJE" runat="server" Width="100" CssClass="sonluk-text-box" onClick="WdatePicker()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="访客状态：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFKZT" runat="server" Width="120" CssClass="sonluk-ddl">
                                            <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                            <asp:ListItem Value="0" Text="在访"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="离开"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="公司代码：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGSDM" runat="server" Width="120" AutoPostBack="true" CssClass="sonluk-ddl" OnSelectedIndexChanged="ddlGSDM_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbJLMW" runat="server" Text="进入门卫：" Width="70"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlJLMW" runat="server" Width="120" CssClass="sonluk-ddl">
                                        </asp:DropDownList>
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
                                        <asp:GridView ID="gvFKINFO" runat="server" AutoGenerateColumns="False" Width="1500" OnRowDataBound="gvFKINFO_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="访客编号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKNO" runat="server" Text='<%# Bind("FKNO") %>' Width="80"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="访客姓名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKNAME" runat="server" Text='<%# Bind("FKNAME") %>' Width="50"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="性别">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKSEX" runat="server" Text='<%# Bind("FKSEX") %>' Width="25"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名族">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKMZ" runat="server" Text='<%# Bind("FKMZ") %>' Width="30"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="出生日期">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKCSRQ" runat="server" Text='<%# Bind("FKCSRQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="证件类型描述">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKZJLX" runat="server" Text='<%# Bind("FKZJLX") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="证件号码">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKZJHM" runat="server" Text='<%# Bind("FKZJHM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="住址">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKADD" runat="server" Text='<%# Bind("FKADD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单位">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKDW" runat="server" Text='<%# Bind("FKDW") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="车牌号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKCPH" runat="server" Text='<%# Bind("FKCPH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="访客证号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKZH" runat="server" Text='<%# Bind("FKZH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="箱单号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="XDH" runat="server" Text='<%# Bind("XDH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="封箱号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FXH" runat="server" Text='<%# Bind("FXH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="受访部门">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SFBM" runat="server" Text='<%# Bind("SFBM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="受访人员名称">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SFNAME" runat="server" Text='<%# Bind("SFNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="受访人工号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SFGH" runat="server" Text='<%# Bind("SFGH") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="访客来访事由">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKLFSY" runat="server" Text='<%# Bind("FKLFSY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="事由明细">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKLFSYMX" runat="server" Text='<%# Bind("FKLFSYMX") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="访客人数">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKSUM" runat="server" Text='<%# Bind("FKSUM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="随身物品">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FKSSWP" runat="server" Text='<%# Bind("FKSSWP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="进入门卫">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JRMW" runat="server" Text='<%# Bind("JRMW") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="来访时间">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LFTIME" runat="server" Text='<%# Bind("LFTIME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="是否离开">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISLK" runat="server" Text='<%# Bind("ISLK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="离开门卫">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LKMW" runat="server" Text='<%# Bind("LKMW") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="离开时间">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LKTIME" runat="server" Text='<%# Bind("LKTIME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="备注">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BZ" runat="server" Text='<%# Bind("BZ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
