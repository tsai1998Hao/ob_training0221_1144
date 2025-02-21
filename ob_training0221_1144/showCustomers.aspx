<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showCustomers.aspx.cs" Inherits="ob_training0221_1144.showCustomers" %>




<form id="form1" runat="server">
    <div>

<asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
    OnRowEditing="gvCustomers_RowEditing" 
    OnRowUpdating="gvCustomers_RowUpdating" 
    OnRowCancelingEdit="gvCustomers_RowCancelingEdit"
    OnRowDeleting="gvCustomers_RowDeleting">
    
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Customer ID" SortExpression="Id" />
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
