<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="HospitalSystem.Patients.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="PatientID" HeaderText="Patient ID" UniqueName="PatientID" />
                <telerik:GridBoundColumn DataField="Full_Name" HeaderText="Patient Name" UniqueName="Full_Name" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Patient Phone" UniqueName="Phone" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
