<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KCYJ.aspx.cs" Inherits="STSY.WL.KCYJ"
    MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>库存预警</b>
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
                                            <td>类别编号
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLBBH" Width="200" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLBBH_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>物料编号
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlWLBH" runat="server" Width="200"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>物料条码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLTM" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                            <td>物料规格
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLGG" runat="server" Width="200"></asp:TextBox>
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
                                                <asp:GridView ID="gdYJKC" runat="server"
                                                    AutoGenerateColumns="False" OnRowDataBound="gdYJKC_RowDataBound" AllowPaging="True" OnPageIndexChanging="gdYJKC_PageIndexChanging" PageSize="100">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="类别">
                                                            <ItemTemplate>
                                                                <asp:Label ID="typeSN" runat="server" Text='<%# Bind("typeSN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="类别名称">
                                                            <ItemTemplate>
                                                                <asp:Label ID="typeName" runat="server" Text='<%# Bind("typeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="条码">
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
                                                        <asp:TemplateField HeaderText="规格">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtSpec" runat="server" Text='<%# Bind("mtSpec") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="单位">
                                                            <ItemTemplate>
                                                                <asp:Label ID="mtUnit" runat="server" Text='<%# Bind("mtUnit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="最小库存">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="MinNum" runat="server" Text='<%# Bind("MinNum") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="最大库存">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="MaxNum" runat="server" Text='<%# Bind("MaxNum") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="库存">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="smtNumber" runat="server" Text='<%# Bind("smtNumber") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="库存金额">
                                                            <ItemTemplate>
                                                                <div style="float: right; text-align: right">
                                                                    <asp:Label ID="smtTotal" runat="server" Text='<%# Bind("smtTotal") %>'></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" />
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
