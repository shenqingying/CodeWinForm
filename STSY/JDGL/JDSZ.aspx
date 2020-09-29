<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JDSZ.aspx.cs" Inherits="STSY.JDGL.JDSZ" MasterPageFile="~/sonluk.Master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>接待设置</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSX" runat="server" Text="刷新" CssClass="sonluk-button" OnClick="btnSX_Click" />
                                    &nbsp;
                                    <asp:Button ID="btnXG" runat="server" Text="修改" CssClass="sonluk-button" OnClick="btnXG_Click" />
                                    <asp:Button ID="btnSAVE" runat="server" Text="保存" CssClass="sonluk-button" OnClick="btnSAVE_Click" />
                                    <asp:Button ID="btnQX" runat="server" Text="取消" CssClass="sonluk-button" OnClick="btnQX_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divLB" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbRI" runat="server" Text="0"></asp:Label>
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
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="人员编号名称"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRY" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRY_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="对应权限"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbKF" runat="server" Text="客饭" />
                                <br />
                                <asp:CheckBox ID="cbSG" runat="server" Text="水果" />
                                <br />
                                <asp:CheckBox ID="cbGD" runat="server" Text="糕点" />
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
