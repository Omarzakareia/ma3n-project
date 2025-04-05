<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PatientsReport.aspx.cs" Inherits="HospitalSystem.Reports.PatientsReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v24.2.Web.WebForms, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; margin-bottom: 20px;">
        <asp:Button ID="btnDaily" runat="server" Text="Daily" CssClass="btn btn-primary" OnClick="btnDaily_Click" />
        <asp:Button ID="btnMonthly" runat="server" Text="Monthly" CssClass="btn btn-success" OnClick="btnMonthly_Click" />
    </div>

    <h1 style="text-align: center;">Patient Records</h1>
    <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server" Width="100%" Height="800px" />


</asp:Content>
