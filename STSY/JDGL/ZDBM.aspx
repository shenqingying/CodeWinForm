<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZDBM.aspx.cs" Inherits="STSY.JDGL.ZDBM" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>通知部门</b>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnFIND" runat="server" Text="刷新" CssClass="sonluk-button" />
                                |
                                <asp:Button ID="btnADDROW" runat="server" Text="增行" CssClass="sonluk-button" />
                                <asp:Button ID="btnDELETEROW" runat="server" Text="删行" CssClass="sonluk-button" />
                                |
                                <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" />
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
                                <asp:GridView ID="gdZDBM" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdZDBM_RowDataBound" CellPadding="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbXZ" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="部门ID">
                                            <ItemTemplate>
                                                <asp:Label ID="DEPTID" runat="server" Text='<%# Bind("DEPTID") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="部门名称">
                                            <ItemTemplate>
                                                <asp:TextBox ID="DEPTNAME" runat="server" Text='<%# Bind("DEPTNAME") %>' Width="90" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="备注">
                                            <ItemTemplate>
                                                <asp:TextBox ID="DEPTMEMO" runat="server" Text='<%# Bind("DEPTMEMO") %>' Width="90" onkeydown="if(event.keyCode==13){event.keyCode=9;}">
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
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
