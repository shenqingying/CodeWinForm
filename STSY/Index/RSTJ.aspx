<%@ Page Title="" Language="C#" MasterPageFile="~/sonluk.Master" AutoEventWireup="true" CodeBehind="RSTJ.aspx.cs" Inherits="STSY.Index.RSTJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 340px;
        }
        .auto-style2 {
            width: 340px;
            height: 19px;
        }
        .auto-style3 {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>人数统计</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnCXSX" runat="server" Text="查询刷新" CssClass="sonluk-button" OnClick="btnCXSX_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="BL" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    
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
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divRKDTX" align="left" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="查询日期:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCXRQS" runat="server" onClick="WdatePicker()" Width="120"></asp:TextBox>
                                                                <%--到
                                                            <asp:TextBox ID="txtRKRQE" runat="server" onClick="WdatePicker()" Width="120"></asp:TextBox>--%>
                                                            </td>
                                                            
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td class="auto-style1">
                                                                <asp:Label ID="Label17" runat="server" Text="上午打卡人数：" Font-Size="Large"></asp:Label>
                                                                
                                                            </td>
                                                            <td class="auto-style1">
                                                                <asp:Label ID="Label18" runat="server" Text="下午打卡人数：" Font-Size="Large"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style2">
                                                                <asp:Label ID="lbshangwu" runat="server" Text="0" Font-Size="X-Large"></asp:Label>
                                                                
                                                            </td>
                                                            <td class="auto-style2">
                                                                <asp:Label ID="lbxiawu" runat="server" Text="0" Font-Size="X-Large"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <asp:Label ID="Label1" runat="server" Text="提示：该数字为2：30到9：00上班人数" ForeColor="Gray"></asp:Label>
                                                            </td>
                                                            <td class="auto-style3">
                                                                <asp:Label ID="Label2" runat="server" Text="提示：该数字为9：00到18：00的上班人数" ForeColor="Gray"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <asp:Label ID="Label4" runat="server" Text="周末可能会有部分科室人员未打卡，该人数没有计算在内" ForeColor="Gray"></asp:Label>
                                                            </td>
                                                            <td class="auto-style3">
                                                                <asp:Label ID="Label5" runat="server" Text="由于打卡时间距离饭点较近，建议看前几天的作为参考" ForeColor="Gray"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="入库备注" Width="70"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtRKBZ" runat="server" Width="500" Height="50" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="制单人" Width="70"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZDR" runat="server" Width="200"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text="制单时间" Width="70"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtZDSJ" runat="server" Width="200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </div>
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

