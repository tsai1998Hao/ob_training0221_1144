<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ob_training0221_1144.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>測試</h3>
        <button type="button" onclick="fakeLogin()">假裝登入</button>
    </main>






    <h1>會員註冊</h1>
    <div>
        <label for="name">Name:</label>
        <%--<input type="text" id="name" name="name" required />--%>
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
    <asp:LinkButton ID="btnFilter" runat="server" class="btn btn-primary" OnClick="btnFilter_Click" Visible="true" CausesValidation="true">註冊</asp:LinkButton>
    <%--<button type="submit"  onclick="registerUser()">新增會員資料</button>--%>
<%--    <asp:Button ID="btnSubmit" runat="server" Text="新增會員資料" OnClick="btnSubmit_Click" />--%>






    <script type="text/javascript">
        function fakeLogin() {
            // 在這裡可以執行任何登入過程的代碼
            alert("已成功登入！");

            // 然後跳轉到 showCustomers.aspx
            window.location.href = "showCustomers.aspx";
        }


        //function registerUser() {
        //    const name = document.getElementById('name').value;
        //    const email = document.getElementById('email').value;
        //    const password = document.getElementById('password').value;

        //    console.log(name, email, password);

        //    if (name && email && password) {
        //        const data = {
        //            name: name,
        //            email: email,
        //            password: password
        //        };

        //        fetch('/about.aspx', { // 指向你的後端頁面
        //            method: 'POST',
        //            headers: {
        //                'Content-Type': 'application/json'
        //            },
        //            body: JSON.stringify({
        //                name: "測試",
        //                email: "test@mail.com",
        //                password: "123456"
        //            })
        //        })
        //            .then(response => response.json())
        //            .then(data => {
        //                if (data.success) {
        //                    alert('會員資料新增成功');
        //                } else {
        //                    alert('會員資料新增失敗: ' + data.error);
        //                }
        //            })
        //            .catch(error => {
        //                console.error('Error:', error);
        //                alert('發生錯誤');
        //            });
        //    } else {
        //        alert('請填寫所有欄位');
        //    }
        //}




</script>

</asp:Content>





