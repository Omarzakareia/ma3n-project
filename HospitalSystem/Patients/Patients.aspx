<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="HospitalSystem.Patients.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4"> <!-- Bootstrap Container -->

        <!-- Wrapper for Search Bar & Grid -->
        <div class="w-100 p-3"> <!-- Full Width & Padding -->
        
            <!-- Search Box -->
            <telerik:RadTextBox ID="txtSearch" runat="server" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"
                EmptyMessage="Search by Full Name..." CssClass="form-control w-100 p-3 mb-3" Width="100%" /> <!-- Full Width & Padding -->

            <!-- Grid Wrapper for Bootstrap Styling -->
            <div class="w-100 border shadow-sm p-3"> <!-- Full Width with Border & Shadow -->
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <MasterTableView>
                        <RowIndicatorColumn ShowNoSortIcon="False"></RowIndicatorColumn>
                        <ExpandCollapseColumn ShowNoSortIcon="False"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID" />
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Patient Name" UniqueName="FullName" />
                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Patient Phone" UniqueName="Phone" />
                        </Columns>
                        <EditFormSettings>
                            <EditColumn ShowNoSortIcon="False"></EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <HeaderStyle BackColor="#23408E" ForeColor="#ED1B24" />
                </telerik:RadGrid>
            </div>

        </div>

    </div>
</asp:Content>
