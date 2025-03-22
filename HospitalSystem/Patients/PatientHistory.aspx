<%@ Page Title="Patient History" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PatientHistory.aspx.cs" Inherits="HospitalSystem.Patients.PatientHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a style="text-decoration: none;">
        <h2 class="text-center fw-bold mb-4 py-2 border-bottom shadow-sm bg-light">Patient History</h2>
    </a>
    <div class="container mt-4">

        <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

        <div class="d-flex justify-content-center">
            <div class="mb-4 shadow-sm">
                <h4 class="mt-4 text-center">Appointments & Billing History</h4>
                <telerik:RadGrid ID="RadGridPatientHistory" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    CellSpacing="-1" GridLines="Both"
                    OnNeedDataSource="RadGridPatientHistory_NeedDataSource">
                    <MasterTableView DataKeyNames="PatientID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" UniqueName="FullName" />
                            <telerik:GridBoundColumn DataField="BillingID" HeaderText="Billing ID" UniqueName="BillingID" />
                            <telerik:GridBoundColumn DataField="TotalAmount" HeaderText="Total Amount" UniqueName="TotalAmount" DataFormatString="{0:C}" />
                            <telerik:GridBoundColumn DataField="AmountPaid" HeaderText="Amount Paid" UniqueName="AmountPaid" DataFormatString="{0:C}" />
                            <telerik:GridBoundColumn DataField="BillingDate" HeaderText="Billing Date" UniqueName="BillingDate" DataFormatString="{0:MM/dd/yyyy}" />
                            <telerik:GridBoundColumn DataField="PaymentStatus" HeaderText="Payment Status" UniqueName="PaymentStatus" />
                            <telerik:GridBoundColumn DataField="AppointmentID" HeaderText="Appointment ID" UniqueName="AppointmentID" />
                            <telerik:GridBoundColumn DataField="AppointmentDate" HeaderText="Appointment Date" UniqueName="AppointmentDate" DataFormatString="{0:MM/dd/yyyy}" />
                            <telerik:GridBoundColumn DataField="DoctorID" HeaderText="Doctor ID" UniqueName="DoctorID" />
                            <telerik:GridBoundColumn DataField="DoctorName" HeaderText="Doctor Name" UniqueName="DoctorName" />
                            <telerik:GridBoundColumn DataField="DepartmentName" HeaderText="Department Name" UniqueName="DepartmentName" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle BackColor="#23408E" ForeColor="#ED1B24" />
                </telerik:RadGrid>
            </div>
        </div>

    </div>
</asp:Content>

