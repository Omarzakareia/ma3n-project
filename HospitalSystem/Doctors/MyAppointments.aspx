<%@ Page Title="My Appointments" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MyAppointments.aspx.cs" Inherits="HospitalSystem.Doctors.MyAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <a href="MyAppointments.aspx" style="text-decoration: none;">
       <h2 class="text-center fw-bold mb-4 py-2 border-bottom shadow-sm bg-light text-success">Appointments</h2>
   </a>

    <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True" 
        AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource">
        
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
        
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="540px" />
        </ClientSettings>
        
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="AppointmentID">
<RowIndicatorColumn ShowNoSortIcon="False"></RowIndicatorColumn>

<ExpandCollapseColumn ShowNoSortIcon="False"></ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="AppointmentID" HeaderText="Appointment ID" UniqueName="AppointmentID"
                    ReadOnly="True" Visible="False" />
                
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" />
                
                <telerik:GridBoundColumn DataField="StaffName" HeaderText="Staff Name" UniqueName="StaffName"
                    SortExpression="StaffName" />
                
                <telerik:GridBoundColumn DataField="AppointmentDate" HeaderText="Appointment Date" UniqueName="AppointmentDate"
                    DataType="System.DateTime" SortExpression="AppointmentDate" />
                
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                    SortExpression="Status" />
            </Columns>
            <EditFormSettings>
                <EditColumn ShowNoSortIcon="False"></EditColumn>
            </EditFormSettings>
            <HeaderStyle BackColor="Green" ForeColor="White" />
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
