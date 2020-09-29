<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WLKC.aspx.cs" Inherits="STSY.WL.WLKC"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>物料库存</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnFIND" runat="server" Text="查询" CssClass="sonluk-button" OnClick="btnFIND_Click" />
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
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="仓库编号" Width="60"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlCKBH" runat="server" Width="280">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="类别编号" Width="60"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlLBBH" Width="280" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="物料编号" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLBH" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="物料名称" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLMC" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="物料条码" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLTM" runat="server" Width="100"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="物料规格" Width="60"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLGG" runat="server" Width="100"></asp:TextBox>
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
                                                <asp:GridView ID="gdKC" runat="server" Width="950" AutoGenerateColumns="False" OnRowDataBound="gdKC_RowDataBound" OnPageIndexChanging="gdKC_PageIndexChanging" AllowPaging="True" PageSize="100">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="typeSN" runat="server" Text='<%# Bind("typeSN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="类别名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="typeName" runat="server" Text='<%# Bind("typeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="编码">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtNO" runat="server" Text='<%# Bind("mtNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtSN" runat="server" Text='<%# Bind("mtSN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="物料名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtName" runat="server" Text='<%# Bind("mtName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="规格型号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtSpec" runat="server" Text='<%# Bind("mtSpec") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtUnit" runat="server" Text='<%# Bind("mtUnit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="库位编号">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PlaceSN" runat="server" Text='<%# Bind("PlaceSN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="库位名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PlaceName" runat="server" Text='<%# Bind("PlaceName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="数量">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtNumber" runat="server" Text='<%# Bind("mtNumber") %>'></asp:Label>
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
                                                        <asp:TemplateField HeaderText="平均价">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="PJJ" runat="server" Text='<%# Bind("PJJ") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="卖出价">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtSalePrice" runat="server" Text='<%# Bind("mtSalePrice") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="买入价">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="mtBuyPrice" runat="server" Text='<%# Bind("mtBuyPrice") %>'></asp:Label>
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
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
