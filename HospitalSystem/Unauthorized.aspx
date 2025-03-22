<%@ Page Title="Unauthorized Access" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="HospitalSystem.Unauthorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .unauthorized-container {
            text-align: center;
            padding: 50px;
        }
        .unauthorized-message {
            font-size: 20px;
            color: red;
            font-weight: bold;
        }
        .home-link {
            margin-top: 20px;
            display: inline-block;
            padding: 10px 20px;
            background-color: #007bff;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
        }
        .home-link:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="unauthorized-container">
        <h2>Unauthorized Access</h2>
        <p class="unauthorized-message">You do not have permission to access this page.</p>
        <a href="Default.aspx" class="home-link">Return to Home</a>
    </div>
</asp:Content>
