<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showCustomers.aspx.cs" Inherits="ob_training0221_1144.showCustomers" %>



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
