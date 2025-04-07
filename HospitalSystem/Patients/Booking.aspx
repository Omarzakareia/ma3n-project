<%@ Page Title="Book Appointment" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="HospitalSystem.Patients.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .booking-container {
            max-width: 800px;
            margin: 30px auto;
            padding: 30px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .booking-header {
            text-align: center;
            margin-bottom: 30px;
        }

        .booking-header h2 {
            color: #2c3e50;
            font-weight: 600;
            margin: 0;
        }

        .booking-header p {
            color: #7f8c8d;
            font-size: 1rem;
            margin-top: 10px;
        }

        .form-row {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            gap: 20px;
            margin-bottom: 20px;
            width: 100%;
        }

        .form-group {
            flex: 1 1 calc(50% - 20px);
            min-width: 250px;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: #34495e;
        }

        .RadComboBox, 
        .RadDatePicker, 
        .RadTimePicker {
            width: 100% !important;
        }

        .RadComboBox .rcbInputCell, 
        .RadDatePicker .rdtInput, 
        .RadTimePicker .rdtInput {
            width: 100% !important;
            border: 1px solid #ddd !important;
            border-radius: 4px !important;
            padding: 8px 12px !important;
            height: auto !important;
        }

        .RadDatePicker .rdtInput {
            width: 100% !important;
        }

        .form-group.status-container {
            text-align: left;
            width: 100%;
        }

        .btn-container {
            width: 100%;
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }

        .btn-book {
            background-color: #3498db !important;
            color: white !important;
            border: none !important;
            padding: 12px 24px !important;
            border-radius: 4px !important;
            font-weight: 500 !important;
            text-transform: uppercase;
            letter-spacing: 1px;
            transition: all 0.3s ease;
            font-size: 0.875rem !important;
            min-width: 200px;
            text-align: center;
        }

        .btn-book:hover {
            background-color: #2980b9 !important;
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        @media (max-width: 768px) {
            .form-row {
                flex-direction: column;
                gap: 15px;
            }

            .form-group {
                min-width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
        <div class="booking-container">
            <div class="booking-header">
                <h2>Book Appointment</h2>
                <p>Fill out the form below to schedule your appointment</p>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="ddlPatient">Patient</label>
                    <telerik:RadComboBox ID="ddlPatient" runat="server" EmptyMessage="Select Patient" AutoPostBack="false" Skin="Bootstrap">
                    </telerik:RadComboBox>
                </div>

                <div class="form-group">
                    <label for="ddlDoctor">Doctor</label>
                    <telerik:RadComboBox ID="ddlDoctor" runat="server" EmptyMessage="Select Doctor" AutoPostBack="false" Skin="Bootstrap">
                    </telerik:RadComboBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label for="AppointmentDate">Date</label>
                    <telerik:RadDatePicker ID="AppointmentDate" runat="server" MinDate='<%# DateTime.Today %>' Skin="Bootstrap">
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dddd, MMMM d, yyyy" DateFormat="yyyy-MM-dd" />
                    </telerik:RadDatePicker>
                </div>

                <div class="form-group">
                    <label for="AppointmentTime">Time</label>
                    <telerik:RadTimePicker ID="AppointmentTime" runat="server" Skin="Bootstrap">
                        <TimeView ID="TimeView1" runat="server" Interval="00:30:00" StartTime="08:00:00" EndTime="18:00:00" />
                    </telerik:RadTimePicker>
                </div>
            </div>

            <div class="form-group status-container">
                <label for="ddlStatus">Appointment Status</label>
                <telerik:RadComboBox ID="ddlStatus" runat="server" EmptyMessage="Select Status" Skin="Bootstrap">
                    <Items>
                        <telerik:RadComboBoxItem Text="Scheduled" Value="Scheduled" Selected="true" />
                        <telerik:RadComboBoxItem Text="Completed" Value="Completed" />
                        <telerik:RadComboBoxItem Text="Cancelled" Value="Cancelled" />
                    </Items>
                </telerik:RadComboBox>
            </div>

            <div class="btn-container">
                <telerik:RadButton ID="btnBook" runat="server" Text="Book Appointment" OnClick="btnBook_Click" CssClass="btn-book" Skin="Bootstrap" />
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>