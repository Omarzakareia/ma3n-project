<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="HospitalSystem.Patients.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="RadGrid1" runat="server">
        <MasterTableView DataKeyNames="PatientID">
            <Columns>
                <telerik:GridBoundColumn DataField="PatientID" HeaderText="ID" ReadOnly="true" UniqueName="PatientID" />
                <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" />
                <telerik:GridBoundColumn DataField="Age" HeaderText="Age" UniqueName="Age" />
                <telerik:GridBoundColumn DataField="Gender" HeaderText="Gender" UniqueName="Gender" />
                <telerik:GridBoundColumn DataField="ContactNumber" HeaderText="Contact" UniqueName="ContactNumber" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:RadRadioButton ID="RadRadioButton1" runat="server" Text="RadRadioButton"></telerik:RadRadioButton>
    <telerik:RadButton ID="RadButton1" runat="server" Text="RadButton"></telerik:RadButton>
</asp:Content>
