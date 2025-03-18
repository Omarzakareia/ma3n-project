<%@ Page Title="User Management" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="HospitalSystem.Users.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="text-align: center; font-size: 22px; font-weight: bold; margin-top: 15px; margin-bottom: 15px">User Management</h2>

    <div style="text-align: right; margin-bottom: 10px;">
        <telerik:RadButton ID="btnActiveUsers" runat="server" Text="Active Users" OnClick="btnActiveUsers_Click" CssClass="btn btn-primary" />
        <telerik:RadButton ID="btnDeletedUsers" runat="server" Text="Deleted Users" OnClick="btnDeletedUsers_Click" CssClass="btn btn-secondary" />
    </div>

    <div style="margin-top: 20px">
        <label for="txtMaxAttempts">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Max Failed Login Attempts:</label>
        <asp:TextBox ID="txtMaxAttempts" runat="server" Text="5"></asp:TextBox>
        <asp:Button ID="btnSaveAttempts" runat="server" Text="Save" OnClick="btnSaveAttempts_Click" CssClass="mb-4" />
        <asp:Label ID="lblSaveStatus" runat="server" ForeColor="Green" Visible="False"></asp:Label>
    </div>

    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
        
        <!-- Active Users Page -->
        <telerik:RadPageView ID="PageActiveUsers" runat="server">
            <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                AllowSorting="True" AutoGenerateEditColumn="True"
                ShowFooter="True" CssClass="mx-2" OnDeleteCommand="RadGrid1_DeleteCommand" OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand">
                
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="UserID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="UserID" HeaderText="UserID" ReadOnly="True" UniqueName="UserID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" UniqueName="FullName"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role" UniqueName="RoleName"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreatedAt" HeaderText="Created At" UniqueName="CreatedAt"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsLocked" HeaderText="Is Locked" UniqueName="IsLocked"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Delete" UniqueName="DeleteColumn"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <!-- Deleted Users Page -->
        <telerik:RadPageView ID="PageDeletedUsers" runat="server">
            <telerik:RadGrid ID="RadGridDeletedUsers" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                AllowSorting="True" ShowFooter="True" CssClass="mx-2">
                
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="UserID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="UserID" HeaderText="UserID" ReadOnly="True" UniqueName="UserID"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" UniqueName="FullName"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role" UniqueName="RoleName"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeletedAt" HeaderText="Deleted At" UniqueName="DeletedAt"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeletedBy" HeaderText="Deleted By" UniqueName="DeletedBy"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

    </telerik:RadMultiPage>

</asp:Content>
