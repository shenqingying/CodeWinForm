<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPRKTJBB.aspx.cs" Inherits="STSY.YFGL.FPRKTJBB" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>发票入库统计报表</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSCBB" runat="server" Text="生成报表" CssClass="sonluk-button" OnClick="btnSCBB_Click" />
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
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="类别种类" Width="60"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLBZL" runat="server" Width="150">
                                        <asp:ListItem Value="-1" Text="=请选择="></asp:ListItem>
                                        <asp:ListItem Value="0" Text="A"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="B"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="C"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cb1" runat="server" Text="计算上期合计数据" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="仓库" Width="40"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlCKBH" runat="server" Width="150">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="类别" Width="40"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLB" runat="server" Width="150">
                                    </asp:DropDownList>
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
                            <td align="center">
                                <asp:GridView ID="gdRKBB" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKBB_RowDataBound" CellPadding="0" OnRowCreated="gdRKBB_RowCreated" AllowPaging="True" PageSize="100" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="物料类别">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("typeName") %>' Width="100">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物料名称">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("mtName") %>' Width="60">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="当期合计数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label400" runat="server" Text='<%# Bind("mtNumber") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="当期合计金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label401" runat="server" Text='<%# Bind("mtTotal") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="当期合计单价">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label402" runat="server" Text='<%# Bind("mtPrice") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上期合计数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label403" runat="server" Text='<%# Bind("pmtNumber") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上期合计金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label404" runat="server" Text='<%# Bind("pmtTotal") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上期合计单价">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label405" runat="server" Text='<%# Bind("pmtPrice") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="1日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("s1") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="1日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("j1") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="2日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label21" runat="server" Text='<%# Bind("s2") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="2日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label22" runat="server" Text='<%# Bind("j2") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="3日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("s3") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="3日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label32" runat="server" Text='<%# Bind("j3") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="4日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label41" runat="server" Text='<%# Bind("s4") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="4日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label42" runat="server" Text='<%# Bind("j4") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="5日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label51" runat="server" Text='<%# Bind("s5") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="5日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label52" runat="server" Text='<%# Bind("j5") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label61" runat="server" Text='<%# Bind("s6") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label62" runat="server" Text='<%# Bind("j6") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="7日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label71" runat="server" Text='<%# Bind("s7") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="7日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label72" runat="server" Text='<%# Bind("j7") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="8日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label81" runat="server" Text='<%# Bind("s8") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="8日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label82" runat="server" Text='<%# Bind("j8") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="9日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label91" runat="server" Text='<%# Bind("s9") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="9日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label92" runat="server" Text='<%# Bind("j9") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label101" runat="server" Text='<%# Bind("s10") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label102" runat="server" Text='<%# Bind("j10") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label111" runat="server" Text='<%# Bind("s11") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label112" runat="server" Text='<%# Bind("j11") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label121" runat="server" Text='<%# Bind("s12") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label122" runat="server" Text='<%# Bind("j12") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label131" runat="server" Text='<%# Bind("s13") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label132" runat="server" Text='<%# Bind("j13") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label141" runat="server" Text='<%# Bind("s14") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label142" runat="server" Text='<%# Bind("j14") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label151" runat="server" Text='<%# Bind("s15") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label152" runat="server" Text='<%# Bind("j15") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label161" runat="server" Text='<%# Bind("s16") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label162" runat="server" Text='<%# Bind("j16") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="17日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label171" runat="server" Text='<%# Bind("s17") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="17日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label172" runat="server" Text='<%# Bind("j17") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="18日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label181" runat="server" Text='<%# Bind("s18") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="18日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label182" runat="server" Text='<%# Bind("j18") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="19日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label191" runat="server" Text='<%# Bind("s19") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="19日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label192" runat="server" Text='<%# Bind("j19") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="20日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label201" runat="server" Text='<%# Bind("s20") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="20日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label202" runat="server" Text='<%# Bind("j20") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="21日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label211" runat="server" Text='<%# Bind("s21") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="21日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label212" runat="server" Text='<%# Bind("j21") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="22日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label221" runat="server" Text='<%# Bind("s22") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="22日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label222" runat="server" Text='<%# Bind("j22") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="23日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label231" runat="server" Text='<%# Bind("s23") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="23日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label232" runat="server" Text='<%# Bind("j23") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="24日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label241" runat="server" Text='<%# Bind("s24") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="24日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label242" runat="server" Text='<%# Bind("j24") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="25日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label251" runat="server" Text='<%# Bind("s25") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="25日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label252" runat="server" Text='<%# Bind("j25") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="26日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label261" runat="server" Text='<%# Bind("s26") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="26日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label262" runat="server" Text='<%# Bind("j26") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="27日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label271" runat="server" Text='<%# Bind("s27") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="27日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label272" runat="server" Text='<%# Bind("j27") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="28日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label281" runat="server" Text='<%# Bind("s28") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="28日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label282" runat="server" Text='<%# Bind("j28") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="29日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label291" runat="server" Text='<%# Bind("s29") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="29日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label292" runat="server" Text='<%# Bind("j29") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="30日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label301" runat="server" Text='<%# Bind("s30") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="30日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label302" runat="server" Text='<%# Bind("j30") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="31日数量">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label311" runat="server" Text='<%# Bind("s31") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="31日金额">
                                            <ItemTemplate>
                                                <div style="float: right; text-align: right">
                                                    <asp:Label ID="Label312" runat="server" Text='<%# Bind("j31") %>'>
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="当月合计加扣款金额：" Visible="False"></asp:Label>
                                <asp:Label ID="jkkhj" runat="server" Text="Label" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>


                    </table>
                </td>
            </tr>
            
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
