<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BillingHistory.aspx.cs" Inherits="HospitalSystem.Billings.BillingHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container w-100">
        <a href="Patients.aspx" style="text-decoration: none;">
            <h2 class="text-center fw-bold mb-4 py-2 border-bottom shadow-sm bg-light text-success">Patients</h2>
        </a>
        <div class="d-flex justify-content-between align-items-center flex-wrap bg-light p-3 rounded shadow-sm mb-3">
            <!-- Search Box & Buttons (Left Side) -->
            <div class="d-flex flex-grow-1 me-3 align-items-stretch">
                <telerik:RadTextBox ID="txtSearch" runat="server" EmptyMessage="🔍 Search by Full Name..."
                    CssClass="form-control shadow-sm py-2" Width="75%" />

                <telerik:RadButton ID="btnSearch" runat="server" Text="🔍 Search" CssClass="btn btn-primary ms-2 px-3" />

                <telerik:RadButton ID="btnResetSearch" runat="server" Text="🔄 Reset" CssClass="btn btn-secondary ms-2 px-3" />
            </div>
        </div>
        <!-- Grid -->
        <div class="w-100 border shadow-sm p-3">
            <asp:Panel ID="pnlActivePatients" runat="server">
                <telerik:RadGrid ID="RadGridActive" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    CellSpacing="-1" GridLines="Both" OnItemCommand="RadGridActive_ItemCommand"
                    OnNeedDataSource="RadGridActive_NeedDataSource">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <GroupHeaderItemStyle BackColor="Red" />
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="BillingID">
                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                        <RowIndicatorColumn ShowNoSortIcon="False">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn ShowNoSortIcon="False">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="PatientFullName" HeaderText="Patient Full Name" UniqueName="PatientFullName" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="BillingID" HeaderText="Billing ID" UniqueName="BillingID" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="TotalAmount" HeaderText="Total Amount" UniqueName="TotalAmount" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="AmountPaid" HeaderText="Amount Paid" UniqueName="AmountPaid" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" UniqueName="Balance" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="BillingDate" HeaderText="Billing Date" UniqueName="BillingDate" DataFormatString="{0:MM/dd/yyyy}" />
                            <telerik:GridTemplateColumn HeaderText="Add Payment" UniqueName="AddPayment">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txtAdditionalPayment" runat="server" MinValue="0" CssClass="form-control" Width="100px" />
                                    <telerik:RadButton ID="btnAddPayment" runat="server" Text="💰 Add" CssClass="btn btn-success btn-sm ms-2"
                                        CommandName="AddPayment" CommandArgument='<%# Eval("BillingID") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn ShowNoSortIcon="False">
                            </EditColumn>
                        </EditFormSettings>
                        <HeaderStyle BackColor="Green" ForeColor="White" />
                        <CommandItemStyle BackColor="Red" BorderColor="Red" ForeColor="Red" />
                    </MasterTableView>
                    <HeaderStyle BackColor="Yellow" />
                    <CommandItemStyle BackColor="Green" BorderColor="Olive" BorderStyle="Solid" ForeColor="Green" />
                </telerik:RadGrid>

            </asp:Panel>
        </div>
    </div>
</asp:Content>
