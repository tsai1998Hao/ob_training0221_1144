<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showCustomers.aspx.cs" Inherits="ob_training0221_1144.showCustomers" %>


<form id="form1" runat="server">
    <div>
        <%--OnRowEditing : (固定寫法)綁定CommandName="Edit"，執行我命名的gvCustomers_RowEditing--%>
        <%--OnRowUpdating : (固定寫法)綁定編輯按鈕，執行我命名的gvCustomers_RowEditing--%>
        <%--OnRowCancelingEdit : (固定寫法)綁定編輯按鈕，執行我命名的gvCustomers_RowEditing--%>
        <%--OnRowDeleting : (固定寫法)綁定編輯按鈕，執行我命名的gvCustomers_RowEditing--%>
        <!-- 修改為 RowCommand 事件用來新增     OnRowCommand="gvCustomers_RowCommand"-->    
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            OnRowEditing="gvCustomers_RowEditing"
            OnRowUpdating="gvCustomers_RowUpdating"
            OnRowCancelingEdit="gvCustomers_RowCancelingEdit"
            OnRowDeleting="gvCustomers_RowDeleting"
            OnRowCommand="gvCustomers_RowCommand"
            ShowFooter="true"> 
            <Columns>
                <%--BoundField代表: 只能顯示不能編輯--%>
                <asp:BoundField DataField="Id" HeaderText="Customer ID" SortExpression="Id" />


                <%--TemplateField代表: 可以編輯--%>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="phone">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("phone") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("phone") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="address">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("address") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("address") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="user_id">
                    <ItemTemplate>
                        <asp:TextBox ID="txtUser_id" runat="server" Text='<%# Bind("user_id") %>' Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUser_id" runat="server" Text='<%# Bind("user_id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                
        <asp:TemplateField>
            <FooterTemplate>
                <!-- 新增資料用的 TextBox 和 Button -->
                <asp:TextBox ID="txtInsertName" runat="server" placeholder="Enter Name"></asp:TextBox>
                <asp:TextBox ID="txtInsertPhone" runat="server" placeholder="Enter Phone"></asp:TextBox>
                <asp:TextBox ID="txtInsertAddress" runat="server" placeholder="Enter Address"></asp:TextBox>
                <asp:TextBox ID="txtInsertUserId" runat="server" placeholder="Enter User ID"></asp:TextBox>
                <asp:Button ID="btnInsert" runat="server" Text="Insert" CommandName="Insert" />
            </FooterTemplate>
        </asp:TemplateField>











                <%-- GridView 進入編輯模式後，會自動將 Edit 按鈕替換成 Update 按鈕，並顯示 Cancel 按鈕。--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="Update" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
     </div>
</form>
