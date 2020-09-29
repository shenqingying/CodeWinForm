<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PLCK.aspx.cs" Inherits="STSY.CHUKGL.PLCK" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>批量出库</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCXSX" runat="server" Text="查询刷新" CssClass="sonluk-button" OnClick="btnCXSX_Click" />
                                                    <asp:Button ID="btnQRTJ" runat="server" Text="确认出库" CssClass="sonluk-button" OnClick="btnQRTJ_Click" />
                                                    <asp:Button ID="btnTC" runat="server" Text="退出" CssClass="sonluk-button" OnClick="btnTC_Click" />
                                                    <asp:Button ID="btnALL" runat="server" Text="全选" CssClass="sonluk-button" OnClick="btnALL_Click" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div runat="server" visible="false">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbQX" runat="server" Text="FALSE"></asp:Label>
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
                                                    <asp:Label ID="Label1" runat="server" Text="物料类别" Width="70"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWLLB" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWLLB_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="物料名称" Width="70"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlWL" runat="server"></asp:DropDownList>
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
                                    <asp:GridView ID="gdKC" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdKC_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="选择">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbXZ" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="类别">
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
                                            <asp:TemplateField HeaderText="编号">
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
                                            <asp:TemplateField HeaderText="库位">
                                                <ItemTemplate>
                                                    <asp:Label ID="PlaceSN" runat="server" Text='<%# Bind("PlaceSN") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="库位名称">
                                                <ItemTemplate>
                                                    <asp:Label ID="PlaceName" runat="server" Text='<%# Bind("PlaceName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="数量">
                                                <ItemTemplate>
                                                    <asp:Label ID="mtNumber" runat="server" Text='<%# Bind("mtNumber") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="金额">
                                                <ItemTemplate>
                                                    <asp:Label ID="mtTotal" runat="server" Text='<%# Bind("mtTotal") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="平均价">
                                                <ItemTemplate>
                                                    <asp:Label ID="PJJ" runat="server" Text='<%# Bind("PJJ") %>'>
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
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
