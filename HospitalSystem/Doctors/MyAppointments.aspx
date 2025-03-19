<%@ Page Title="My Appointments" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MyAppointments.aspx.cs" Inherits="HospitalSystem.Doctors.MyAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>My Appointments</h2>

    <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True" 
        AllowSorting="True" OnNeedDataSource="RadGrid1_NeedDataSource">
        
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
        
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        
        <MasterTableView AutoGenerateColumns="False" DataKeyNames="AppointmentID">
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
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
