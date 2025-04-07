<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="HospitalSystem.Billings.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 class="card-title">Invoice Details</h5>
            </div>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False" />
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddlPatient" class="form-label">Select Patient:</label>
                            <telerik:RadDropDownList ID="ddlPatient" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddlDoctor" class="form-label">Select Doctor:</label>
                            <telerik:RadDropDownList ID="ddlDoctor" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="ddlBillingVault" class="form-label">Billing Vault:</label>
                            <telerik:RadDropDownList ID="ddlBillingVault" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtAmountPaid" class="form-label">Amount Paid:</label>
                            <asp:TextBox ID="txtAmountPaid" runat="server" TextMode="Number" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvAmountPaid" runat="server" 
                                ControlToValidate="txtAmountPaid" InitialValue="0"
                                ErrorMessage="Amount Paid is required!" ForeColor="Red" 
                                Display="Dynamic" CssClass="invalid-feedback" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtTotalAmount" class="form-label">Total Amount:</label>
                            <asp:TextBox ID="txtTotalAmount" runat="server" TextMode="Number" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvTotalAmount" runat="server" 
                                ControlToValidate="txtTotalAmount" InitialValue="0"
                                ErrorMessage="Total Amount is required!" ForeColor="Red" 
                                Display="Dynamic" CssClass="invalid-feedback" />
                        </div>
                    </div>
                </div>

                <div class="form-group mt-4">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>