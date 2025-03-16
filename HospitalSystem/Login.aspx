<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HospitalSystem.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-5 mt-5">
        <div class="row justify-content-center mb-3">
            <div class="col-xl-4">
                <div class="border p-4 shadow rounded bg-white">
                    
                    <!-- Validation Summary -->
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="grp" ForeColor="Red" />

                    <h4>Login</h4>

                    <!-- Email Field -->
                    <div class="mb-3">
                        <asp:Label class="form-label" ID="lblEmail" runat="server" Text="Email"></asp:Label>
                        <div class="d-flex align-items-center">
                            <asp:TextBox class="form-control me-2" ID="txtEmail" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Email is Required" 
                                ForeColor="Red" 
                                ControlToValidate="txtEmail" 
                                ValidationGroup="grp" 
                                Display="Dynamic" >*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <!-- Password Field -->
                    <div class="mb-3">
                        <asp:Label class="form-label" ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <div class="d-flex align-items-center">
                            <asp:TextBox class="form-control me-2" TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator2" 
                                runat="server" 
                                ErrorMessage="Password is Required" 
                                ControlToValidate="txtPassword" 
                                ForeColor="Red" 
                                ValidationGroup="grp" 
                                Display="Dynamic" >*</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <!-- Remember Me Checkbox -->
                    <div class="mb-3 form-check">
                        <asp:CheckBox ID="chkRememberMe" runat="server" />
                        <asp:Label CssClass="form-check-label" AssociatedControlID="chkRememberMe" runat="server">Remember Me</asp:Label>
                    </div>

                    <!-- Login Button -->
                    <asp:Button ID="btnLogin" runat="server" CssClass="primary-btn w-100 my-4" Text="Login" ValidationGroup="grp" OnClick="btnLogin_Click" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
