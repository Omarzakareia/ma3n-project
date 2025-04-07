<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AppointmentsReport.aspx.cs" Inherits="HospitalSystem.Reports.AppointmentsReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v24.2.Web.WebForms, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v24.2, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="AppointmentsReport.aspx" style="text-decoration: none;">
      <h2 class="text-center fw-bold mb-4 py-2 border-bottom shadow-sm bg-light text-success">Appointments Report</h2>
  </a>
  
 <div class="container mt-4">
     <asp:Button ID="btnDaily" runat="server" Text="Daily Report" OnClick="btnDaily_Click" />
<asp:Button ID="btnMonthly" runat="server" Text="Monthly Report" OnClick="btnMonthly_Click" />
     <asp:Button ID="btnAll" runat="server" Text="All" OnClick="btnAll_Click" CssClass="btn btn-secondary" />


        
        <div class="mt-4">

            <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server" ></dx:ASPxWebDocumentViewer>
        </div>

        
    </div></asp:Content>
