<%@ Page Title="User Management" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="HospitalSystem.Users.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="UserManagement.aspx" style="text-decoration: none;">
        <h2 class="text-center fw-bold mb-4 py-2 border-bottom shadow-sm bg-light text-success">User Management</h2>
    </a>

    <div class="d-flex justify-content-between align-items-center flex-wrap bg-light px-3 rounded shadow-sm ">
        <div class="btn-group">
            <telerik:RadButton ID="btnActiveUsers" runat="server" Text="Active Users" OnClick="btnActiveUsers_Click" CssClass="btn btn-primary py-2" />
            <telerik:RadButton ID="btnDeletedUsers" runat="server" Text="Deleted Users" OnClick="btnDeletedUsers_Click" CssClass="btn btn-secondary py-2" />
        </div>

        <div class="d-flex align-items-center">
            <label for="txtMaxAttempts" class="me-2 fw-semibold text-secondary">Max Failed Login Attempts:</label>
            <asp:TextBox ID="txtMaxAttempts" runat="server" Text="5" CssClass="form-control me-2 text-center" Style="width: 60px;" />
            <asp:Button ID="btnSaveAttempts" runat="server" Text="Save" OnClick="btnSaveAttempts_Click" CssClass="btn btn-success" />
        </div>

        <asp:Label ID="lblSaveStatus" runat="server" CssClass="fw-bold ms-3 text-success" Visible="False"></asp:Label>
    </div>

    <!-- Add User and Search Section -->
    <div class="d-flex justify-content-between align-items-center flex-wrap bg-light p-3 rounded shadow-sm mb-3">
        <!-- Search Box & Buttons (Left Side) -->
        <div class="d-flex flex-grow-1 me-3 align-items-stretch">
            <telerik:RadTextBox ID="txtSearch" runat="server" EmptyMessage="🔍 Search by Full Name..." 
                CssClass="form-control shadow-sm py-2" Width="40%" />

            <telerik:RadButton ID="btnSearch" runat="server" Text="🔍 Search" CssClass="btn btn-primary ms-2 px-3"
                OnClick="btnSearch_Click" />

            <telerik:RadButton ID="btnResetSearch" runat="server" Text="🔄 Reset" CssClass="btn btn-secondary ms-2 px-3"
                OnClick="btnResetSearch_Click" />
        </div>

        <!-- Add User Button (Right Side) -->
        <telerik:RadButton ID="btnAddUser" runat="server" Text="Add User"
             OnClick="btnAddUser_Click" Style="background-color: #259843; color: white;" />
    </div>


    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">

        <!-- Active Users Page -->
        <telerik:RadPageView ID="PageActiveUsers" runat="server">
            <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True"
                AllowSorting="True" CssClass="mx-2 mb-3"
                OnDeleteCommand="RadGrid1_DeleteCommand"
                OnNeedDataSource="RadGrid1_NeedDataSource"
                OnUpdateCommand="RadGrid1_UpdateCommand"
                OnInsertCommand="RadGrid1_InsertCommand" Skin="Bootstrap">
                <PagerStyle BackColor="Green" ForeColor="Red" AlwaysVisible="True" />
                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="UserID">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="False" />
                    <RowIndicatorColumn ShowNoSortIcon="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn ShowNoSortIcon="False">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="UserID" HeaderText="UserID" ReadOnly="True" UniqueName="UserID">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PasswordHash" HeaderText="Password" UniqueName="Password">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" UniqueName="FullName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Role" UniqueName="RoleName">
                            <ItemTemplate>
                                <%# Eval("RoleName") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList ID="ddlRole" runat="server" SelectedValue='<%# Bind("RoleName") %>'>
                                    <Items>
                                        <telerik:DropDownListItem Text="Admin" Value="Admin" />
                                        <telerik:DropDownListItem Text="Staff" Value="Staff" />
                                        <telerik:DropDownListItem Text="Doctor" Value="Doctor" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="CreatedAt" HeaderText="Created At" ReadOnly="True" UniqueName="CreatedAt">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Is Locked" UniqueName="IsLocked">
                            <ItemTemplate>
                                <%# (bool)DataBinder.GetPropertyValue(Container.DataItem, "IsLocked") ? "Locked" : "Not Locked" %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadDropDownList ID="ddlIsLocked" runat="server" SelectedValue='<%# Bind("IsLocked") %>'>
                                    <Items>
                                        <telerik:DropDownListItem Text="Not Locked" Value="False" />
                                        <telerik:DropDownListItem Text="Locked" Value="True" />
                                    </Items>
                                </telerik:RadDropDownList>
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Delete" UniqueName="DeleteColumn">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />         
                            <ItemTemplate>
                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link text-danger">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0,0,256,256">
                                            <g fill="#ff0000" fill-rule="nonzero">
                                                <g transform="scale(8.53333,8.53333)">
                                                    <path d="M14.98438,2.48633c-0.55152,0.00862 -0.99193,0.46214 -0.98437,1.01367v0.5h-5.5c-0.26757,-0.00363 -0.52543,0.10012 -0.71593,0.28805c-0.1905,0.18793 -0.29774,0.44436 -0.29774,0.71195h-1.48633c-0.36064,-0.0051 -0.69608,0.18438 -0.87789,0.49587c-0.18181,0.3115 -0.18181,0.69676 0,1.00825c0.18181,0.3115 0.51725,0.50097 0.87789,0.49587h18c0.36064,0.0051 0.69608,-0.18438 0.87789,-0.49587c0.18181,-0.3115 0.18181,-0.69676 0,-1.00825c-0.18181,-0.3115 -0.51725,-0.50097 -0.87789,-0.49587h-1.48633c0,-0.26759 -0.10724,-0.52403 -0.29774,-0.71195c-0.1905,-0.18793 -0.44836,-0.29168 -0.71593,-0.28805h-5.5v-0.5c0.0037,-0.2703 -0.10218,-0.53059 -0.29351,-0.72155c-0.19133,-0.19097 -0.45182,-0.29634 -0.72212,-0.29212zM6,9l1.79297,15.23438c0.118,1.007 0.97037,1.76563 1.98438,1.76563h10.44531c1.014,0 1.86538,-0.75862 1.98438,-1.76562l1.79297,-15.23437z"></path>
                                                </g>
                                            </g>
                                        </svg>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridEditCommandColumn HeaderText="Edit" UniqueName="EditColumn">
                        </telerik:GridEditCommandColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn ShowNoSortIcon="False">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle AlwaysVisible="True" BackColor="Green" ForeColor="Black" />
                </MasterTableView>
                <HeaderStyle BackColor="Green" ForeColor="White" />
            </telerik:RadGrid>
        </telerik:RadPageView>

        <!-- Deleted Users Page -->
        <telerik:RadPageView ID="PageDeletedUsers" runat="server">
            <telerik:RadGrid ID="RadGridDeletedUsers" runat="server" AllowPaging="True"
                AllowSorting="True" CssClass="mx-2 mb-5" OnDeleteCommand="RadGridDeletedUsers_DeleteCommand" OnNeedDataSource="RadGridDeletedUsers_NeedDataSource">

                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="UserID">
                    <RowIndicatorColumn ShowNoSortIcon="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn ShowNoSortIcon="False">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="UserID" HeaderText="UserID" ReadOnly="True" UniqueName="UserID">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" UniqueName="FullName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role" UniqueName="RoleName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeletedAt" HeaderText="Deleted At" UniqueName="DeletedAt">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeletedBy" HeaderText="Deleted By" UniqueName="DeletedBy">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeletedByUser" HeaderText="Deleted By User" UniqueName="DeletedByUser">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Restore" UniqueName="RestoreColumn">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <div class="d-flex justify-content-center">
                                    <asp:LinkButton ID="btnRestore" runat="server" CommandName="Delete" CssClass="btn btn-link text-success">
                                        <svg fill="#25ad52" width="24" height="24" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 342.5 342.5" xml:space="preserve" stroke="#25ad52" transform="rotate(90)matrix(1, 0, 0, -1, 0, 0)" stroke-width="0.003425"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M254.37,22.255c-1.161-0.642-2.53-0.795-3.803-0.428c-1.274,0.367-2.35,1.226-2.992,2.387l-21.758,39.391 c-1.335,2.417-0.458,5.459,1.96,6.794C264.616,90.748,287.5,129.495,287.5,171.52c0,63.649-51.782,115.431-115.431,115.431 S56.638,235.169,56.638,171.52c0-23.888,7.557-47.427,21.382-66.897l34.478,34.478c1.338,1.337,3.315,1.806,5.109,1.21 c1.795-0.596,3.101-2.152,3.374-4.024L139.963,6.271c0.228-1.563-0.295-3.141-1.412-4.258c-1.117-1.117-2.7-1.639-4.258-1.412 L4.278,19.584c-1.872,0.273-3.428,1.579-4.023,3.374c-0.596,1.795-0.127,3.772,1.21,5.109l37.292,37.292 C14.788,95.484,1.638,133,1.638,171.52c0,93.976,76.455,170.431,170.431,170.431c93.976,0,170.431-76.455,170.431-170.431 C342.5,109.478,308.731,52.283,254.37,22.255z"></path> </g></svg>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn ShowNoSortIcon="False">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <HeaderStyle BackColor="Green" ForeColor="White" />
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
