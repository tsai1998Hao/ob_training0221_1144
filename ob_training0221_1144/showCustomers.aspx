<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showCustomers.aspx.cs" Inherits="ob_training0221_1144.showCustomers" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>customer頁面
        </div>
    </form>
</body>
</html>--%>



<%--<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>顧客資料</h2>
    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CssClass="table" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="顧客名稱" SortExpression="name" />
            <asp:BoundField DataField="phone" HeaderText="聯絡電話" SortExpression="phone" />
            <asp:BoundField DataField="address" HeaderText="聯絡地址" SortExpression="address" />
            <asp:BoundField DataField="user_id" HeaderText="使用者ID" SortExpression="user_id" />
        </Columns>
    </asp:GridView>
</asp:Content>--%>



<!DOCTYPE html>
<html>
<head>
    <title>顧客資料</title>
</head>
<body>
    <h2>顧客資料</h2>
    <table border="1">
        <tr>
            <th>顧客名稱</th>
            <th>電話</th>
            <th>地址</th>
        </tr>
        <asp:Literal ID="CustomerData" runat="server"></asp:Literal>
    </table>
</body>
</html>
