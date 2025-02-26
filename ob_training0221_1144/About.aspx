<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ob_training0221_1144.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
    </main>

    <h1>首頁---會員註冊</h1>
    <div>
        <label for="name">Name:</label>
        <asp:TextBox ID="name" runat="server" placeholder="請輸入會員名稱"  oninput="validateAoumt(this)" ></asp:TextBox>
    </div>
    <div>
        <label for="email">Email:</label>
        <asp:TextBox ID="email" runat="server" placeholder="請輸入電子信箱"  oninput="validateAoumt(this)" ></asp:TextBox>
    </div>
    <div>
        <label for="password">Password:</label>
        <asp:TextBox ID="password" runat="server" placeholder="請輸入密碼"  oninput="validateAoumt(this)" ></asp:TextBox>        
    </div>
    <asp:LinkButton ID="btnFilter" runat="server" class="btn btn-primary" OnClick="btnFilter_Click" Visible="true" CausesValidation="true" OnClientClick="return validatePasswordLength(document.getElementById('password'));">註冊</asp:LinkButton>
    <button type="button" onclick="goToLogin()">進入登入頁面</button>

    <script type="text/javascript">
        function goToLogin() {
            window.location.href = "Login.aspx";
        }

        //// 密碼長度驗證
        //function validatePasswordLength(input) {
        //    var minLength = 6;
        //    var maxLength = 16;

        //    if (input.value.length < minLength) {
        //        alert("密碼至少需要 " + minLength + " 個字");
        //        return false;
        //    } else if (input.value.length > maxLength) {
        //        alert("密碼最多只能 " + maxLength + " 個字");
        //        return false;
        //    }
        //    return true;
        //}

        //// 點擊註冊按鈕，執行密碼長度驗證
        //document.getElementById('btnFilter').onclick = function (event) {
        //    var passwordInput = document.getElementById('password');
        //    if (!validatePasswordLength(passwordInput)) {
        //        event.preventDefault();  // 阻止註冊
        //    }
        //}
    </script>

</asp:Content>
