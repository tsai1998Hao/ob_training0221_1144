<%@ Page Title="登入" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ob_training0221_1144.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>登入頁面</div>
    <p>    
        <label for="email">Email:</label>
        <asp:TextBox ID="email" runat="server" placeholder="請輸入電子信箱"  oninput="validateAoumt(this)" ></asp:TextBox>
        <label for="password">Password:</label>
        <asp:TextBox ID="password" runat="server" placeholder="請輸入密碼"  oninput="validateAoumt(this)" ></asp:TextBox>        
        <asp:LinkButton ID="btnLogin" runat="server" class="btn btn-primary" OnClick="btnLogin_Click" Visible="true" CausesValidation="true">登入</asp:LinkButton>
        <button type="button" onclick="fakeLogin()">測試用登入</button>

    </p>

    <script>
        function fakeLogin() {
            // 在這裡可以執行任何登入過程的代碼
            alert("已成功登入!！");

            // 然後跳轉到 showCustomers.aspx
            window.location.href = "showCustomers.aspx";
        }


    </script>


</asp:Content>


