<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JDRYGL.aspx.cs" Inherits="STSY.JDGL.JDRYGL" MasterPageFile="~/sonluk.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-header">
        <b>接待人员管理</b>
    </div>
    <div align="left">
        <table>
            <tr>
                <td>
                    <div id="divLB" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbBMRI" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbLXRRI" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lbDDRI" runat="server" Text="0"></asp:Label>
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
                                                <asp:LinkButton ID="lbtnTZBM" runat="server" OnClick="lbtnTZBM_Click">通知部门</asp:LinkButton>
                                                |
                                                <asp:LinkButton ID="lbtnLXR" runat="server" OnClick="lbtnLXR_Click">联系人</asp:LinkButton>
                                                |
                                                <asp:LinkButton ID="lbtnDD" runat="server" OnClick="lbtnDD_Click">地点</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divTZBM" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnBMADD" runat="server" Text="新增" OnClick="btnBMADD_Click" />
                                                    <asp:Button ID="btnBMFIND" runat="server" Text="查询" OnClick="btnBMFIND_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="BMADD" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="部门名称"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBMMC" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="部门备注"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBMBZ" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnBMSAVE" runat="server" Text="保存" OnClick="btnBMSAVE_Click" />
                                                    <asp:Button ID="btnBMCLEAR" runat="server" Text="取消" OnClick="btnBMCLEAR_Click" />
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
                                                    <asp:GridView ID="GDBM" runat="server" AutoGenerateColumns="False" OnRowDataBound="GDBM_RowDataBound" CellPadding="0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="操作">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="修改" ID="lbtnMODEFY" runat="server" OnClick="lbtnBMMODEFY_OnClick" ForeColor="Red">修改</asp:LinkButton>
                                                                    <asp:LinkButton Text="删除" ID="lbtnDELETE" runat="server" OnClick="lbtnBMDELETE_OnClick" ForeColor="Red">删除</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="部门ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DEPTID" runat="server" Text='<%# Bind("DEPTID") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="部门名称">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DEPTNAME" runat="server" Text='<%# Bind("DEPTNAME") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="备注">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DEPTMEMO" runat="server" Text='<%# Bind("DEPTMEMO") %>' Width="100">
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
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divLXR" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnLXRADD" runat="server" Text="新增" OnClick="btnLXRADD_Click" />
                                                    <asp:Button ID="btnLXRFIND" runat="server" Text="查询" OnClick="btnLXRFIND_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="LXRADD" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="联系人"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLXR" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="联系电话"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLXRLXDH" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLXRBZ" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnLXRSAVE" runat="server" Text="保存" OnClick="btnLXRSAVE_Click" />
                                                    <asp:Button ID="btnLXRCLEAR" runat="server" Text="取消" OnClick="btnLXRCLEAR_Click" />
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
                                                    <asp:GridView ID="GVLXR" runat="server" AutoGenerateColumns="False" OnRowDataBound="GDBM_RowDataBound" CellPadding="0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="操作">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="修改" ID="lbtnMODEFY" runat="server" OnClick="lbtnLXRMODEFY_OnClick" ForeColor="Red">修改</asp:LinkButton>
                                                                    <asp:LinkButton Text="删除" ID="lbtnDELETE" runat="server" OnClick="lbtnLXRDELETE_OnClick" ForeColor="Red">删除</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="联系人ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LXRID" runat="server" Text='<%# Bind("LXRID") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="联系人">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LXR" runat="server" Text='<%# Bind("LXR") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="联系电话">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LXDH" runat="server" Text='<%# Bind("LXDH") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="备注">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LXDES" runat="server" Text='<%# Bind("LXDES") %>' Width="100">
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
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divDD" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnDDADD" runat="server" Text="新增" OnClick="btnDDADD_Click" />
                                                    <asp:Button ID="btnDDFIND" runat="server" Text="查询" OnClick="btnDDFIND_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divDDADD" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text="地点"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDDDD" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" Text="说明"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDDSM" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnDDSAVE" runat="server" Text="保存" OnClick="btnDDSAVE_Click" />
                                                    <asp:Button ID="btnDDCLEAR" runat="server" Text="取消" OnClick="btnDDCLEAR_Click" />
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
                                                    <asp:GridView ID="GVDD" runat="server" AutoGenerateColumns="False" OnRowDataBound="GDBM_RowDataBound" CellPadding="0">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="操作">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton Text="修改" ID="lbtnMODEFY" runat="server" OnClick="lbtnDDMODEFY_OnClick" ForeColor="Red">修改</asp:LinkButton>
                                                                    <asp:LinkButton Text="删除" ID="lbtnDELETE" runat="server" OnClick="lbtnDDDELETE_OnClick" ForeColor="Red">删除</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="地点ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ADDID" runat="server" Text='<%# Bind("ADDID") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="地点">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ADDNAME" runat="server" Text='<%# Bind("ADDNAME") %>' Width="100">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="说明">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ADDMEMO" runat="server" Text='<%# Bind("ADDMEMO") %>' Width="100">
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
