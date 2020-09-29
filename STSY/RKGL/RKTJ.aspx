<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RKTJ.aspx.cs" Inherits="STSY.RKGL.RKTJ" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>入库物料添加</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnCXWL" runat="server" Text="查询物料" CssClass="sonluk-button" OnClick="btnCXWL_Click" />
                                <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
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
                                <asp:Label ID="Label3" runat="server" Text="物料名称"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWLMC" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="类别"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLB" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="物料编号"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLBH" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="物料条码"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLTM" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="物料规格"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWLGG" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
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
                                <asp:GridView ID="gdRK" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdRKDLB_RowDataBound" CellPadding="0" AllowPaging="True" PageSize="100" OnPageIndexChanging="gdRK_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:LinkButton Text="添加" ID="lbtnFIND" runat="server" OnClick="lbtnFIND_OnClick" ForeColor="red">添加</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="类别编号">
                                            <ItemTemplate>
                                                <asp:Label ID="typeSN" runat="server" Text='<%# Bind("typeSN") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="类别名称">
                                            <ItemTemplate>
                                                <asp:Label ID="typeName" runat="server" Text='<%# Bind("typeName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="条码">
                                            <ItemTemplate>
                                                <asp:Label ID="mtNO" runat="server" Text='<%# Bind("mtNO") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物料编号">
                                            <ItemTemplate>
                                                <asp:Label ID="mtSN" runat="server" Text='<%# Bind("mtSN") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="物料名称">
                                            <ItemTemplate>
                                                <asp:Label ID="mtName" runat="server" Text='<%# Bind("mtName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="规格">
                                            <ItemTemplate>
                                                <asp:Label ID="mtSpec" runat="server" Text='<%# Bind("mtSpec") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="单位">
                                            <ItemTemplate>
                                                <asp:Label ID="mtUnit" runat="server" Text='<%# Bind("mtUnit") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="买入">
                                            <ItemTemplate>
                                                <asp:Label ID="mtBuyPrice" runat="server" Text='<%# Bind("mtBuyPrice") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="卖出">
                                            <ItemTemplate>
                                                <asp:Label ID="mtSalePrice" runat="server" Text='<%# Bind("mtSalePrice") %>'>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
