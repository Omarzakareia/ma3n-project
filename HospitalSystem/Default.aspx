<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HospitalSystem.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body {
            height: 100%;
            margin: 0;
            display: flex;
            flex-direction: column;
            font-family: 'Poppins', sans-serif !important;
            background-color: #f4f4f4;
        }

        .main-content {
            flex: 1;
        }

        .hero-section {
            position: relative;
            height: 100vh;
            overflow: hidden;
        }

        .overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            z-index: 1;
        }

        .carousel-inner img {
            width: 100%;
            height: 100vh;
            object-fit: cover;
        }

        .hero-content {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            text-align: center;
            color: white;
            z-index: 2;
            background: rgba(0, 0, 0, 0.4);
            padding: 20px 40px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
        }

        .hero-content h1 {
            font-size: 3em;
            margin-bottom: 15px;
            text-shadow: 2px 2px 6px rgba(0, 0, 0, 0.7);
        }

        .hero-content p {
            font-size: 1.5em;
            line-height: 1.6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-section">
        <div class="overlay"></div>
        <div class="carousel carousel-dark slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active" data-bs-interval="5000">
                    <img src="<%= ResolveUrl("~/img/hero1.jpg") %>" class="d-block w-100" alt="...">
                </div>
                <div class="carousel-item" data-bs-interval="5000">
                    <img src="<%= ResolveUrl("~/img/hero2.jpg") %>" class="d-block w-100" alt="...">
                </div>
                <div class="carousel-item" data-bs-interval="5000">
                    <img src="<%= ResolveUrl("~/img/hero3.jpeg") %>" class="d-block w-100" alt="...">
                </div>
            </div>
        </div>
        <div class="hero-content">
            <h1>Welcome To HOSCARES</h1>
            <p>Your health is our priority. Experience world-class medical care with us.</p>
        </div>
    </div>
</asp:Content>
