<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RKlist.aspx.cs" Inherits="STSY.RKGL.RKlist" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>入库清单</b>
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
                                    <asp:Label ID="Label1" runat="server" Text="开始日期" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBegin" runat="server" Width="80" onClick="WdatePicker()"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="结束日期" Width="50"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEnd" runat="server" Width="80" onClick="WdatePicker()"></asp:TextBox>
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
                                    <asp:GridView ID="GridView1" runat="server"  OnRowDataBound="GridView1_RowDataBound" ShowFooter="true"></asp:GridView>
                                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
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
