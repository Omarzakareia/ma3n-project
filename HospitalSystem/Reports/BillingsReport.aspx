<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BillingsReport.aspx.cs" Inherits="HospitalSystem.Reports.BillingsReport" %>
<<<<<<< HEAD
<%@ Register Assembly="DevExpress.XtraReports.v24.2.Web.WebForms, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; margin-top: 20px; margin-bottom: 20px;">
        <asp:Button ID="btnDaily" runat="server" Text="Daily" CssClass="btn btn-primary" OnClick="btnDaily_Click" />
        <asp:Button ID="btnMonthly" runat="server" Text="Monthly" CssClass="btn btn-success" OnClick="btnMonthly_Click" />
    </div>

    <h3 style="text-align: center;">Income Report</h3>
    <dx:aspxwebdocumentviewer id="ASPxWebDocumentViewer1" runat="server" width="100%" height="800px" />
=======
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
>>>>>>> billing-invoice
</asp:Content>
