<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JGCX.aspx.cs" Inherits="STSY.RKGL.JGCX"
    MasterPageFile="~/sonluk.Master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>价格查询</b>
    </div>
    <div align="left">
        <table width="1000">
            <tr>
                <td>
                    <asp:Button ID="btnCXSX" runat="server" Text="刷新列表" CssClass="sonluk-button" OnClick="btnCXSX_Click" />
                    <asp:Button ID="btnDC" runat="server" Text="导出" CssClass="sonluk-button" OnClick="btnDC_Click" />
                </td>aasss
            </tr>
        </table>
    </div>
    <div align="left">
        <table align="left" border="2">
            <tr>
                <td width="100" align="center">编号
                </td>
                <td width="100" align="center">名称
                </td>
                <td width="100" align="center">规格
                </td>
                <td width="800" rowspan="2" style="vertical-align: top">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cbRKRQ" runat="server" Text="入库日期从" OnCheckedChanged="cbRKRQ_CheckedChanged" />
                                <asp:TextBox ID="txtRKDATES" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                                至
                                <asp:TextBox ID="txtRKDATEE" runat="server" onClick="WdatePicker()" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbtnWLCGQD" runat="server" OnClick="lbtnWLCGQD_Click">物料采购清单</asp:LinkButton>
                                |
                                <asp:LinkButton ID="lbtnWLCGJGQST" runat="server" OnClick="lbtnWLCGJGQST_Click" Visible="false">物料采购价格趋势图</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divBT" runat="server">
                                    <asp:GridView ID="gdWLRK" runat="server" AutoGenerateColumns="False"
                                        OnRowDataBound="gdWLRK_RowDataBound" CellPadding="0" AllowPaging="True" PageSize="50">
                                        <Columns>
                                            <asp:TemplateField HeaderText="物料编号">
                                                <ItemTemplate>
                                                    <asp:Label ID="WLBH" runat="server" Text='<%# Bind("mtSN") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单位">
                                                <ItemTemplate>
                                                    <asp:Label ID="DW" runat="server" Text='<%# Bind("mtUnit") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库日期">
                                                <ItemTemplate>
                                                    <asp:Label ID="RKRQ" runat="server" Text='<%# Bind("RuKDate1") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量">
                                                <ItemTemplate>
                                                    <div style="float: right; text-align: right">
                                                        <asp:Label ID="SL" runat="server" Text='<%# Bind("mtNumber") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                        </asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="单价">
                                                <ItemTemplate>
                                                    <div style="float: right; text-align: right">
                                                        <asp:Label ID="DJ" runat="server" Text='<%# Bind("mtPrice") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                        </asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总金额">
                                                <ItemTemplate>
                                                    <div style="float: right; text-align: right">
                                                        <asp:Label ID="ZJE" runat="server" Text='<%# Bind("mtTotal") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                        </asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="供应商名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="GYSMC" runat="server" Text='<%# Bind("ClName") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="说明">
                                                <ItemTemplate>
                                                    <asp:Label ID="SM" runat="server" Text='<%# Bind("MXMemo") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="入库单号">
                                                <ItemTemplate>
                                                    <asp:Label ID="RKDH" runat="server" Text='<%# Bind("RuKDNO") %>' onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divQST" runat="server">
                                    <asp:Chart ID="Chart1" runat="server" Width="800px" BorderDashStyle="Solid" Palette="BrightPastel" ImageType="Png" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" BackColor="#D3DFF0" BorderColor="26, 59, 105">
                                        <Titles>
                                            <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="价格统计表">
                                            </asp:Title>
                                        </Titles>
                                        <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                        <Series>
                                            <asp:Series Name="Series1" ChartType="Bubble" MarkerSize="8" MarkerStyle="Circle">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="300" colspan="3" align="left" style="vertical-align: top">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="News" NodeIndent="10" ExpandDepth="0"
                        OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                        <HoverNodeStyle Font-Underline="True" />
                        <NodeStyle Font-Names="Arial" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="False" />
                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
