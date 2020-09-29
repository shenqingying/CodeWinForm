<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="STSY.FKSY.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>GridView固定表头 清清月儿http://blog.csdn.net/21aspnet </title>
    <style>
        .Freezing {
            position: relative;
            table-layout: fixed;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }

            .Freezing th {
                text-overflow: ellipsis;
                overflow: hidden;
                white-space: nowrap;
                padding: 2px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="overflow-y: scroll; height: 200px; width: 300px" id="dvBody">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"
                OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="12px" OnRowCreated="GridView1_RowCreated">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <Columns>
                    <asp:BoundField DataField="身份证号码" HeaderText="编号" ReadOnly="True" />
                    <asp:BoundField DataField="邮政编码" HeaderText="邮政编码" SortExpression="邮政编码" />
                    <asp:BoundField DataField="家庭住址" HeaderText="家庭住址" />
                    <asp:BoundField DataField="姓名" HeaderText="姓名" />

                </Columns>
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="ms-formlabel DataGridFixedHeader" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="Freezing" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
