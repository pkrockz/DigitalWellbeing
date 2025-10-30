<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="DGWellbing.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reminders</h2>

    <div>
        <h3>Add Reminder</h3>
        <asp:TextBox ID="txtTitle" runat="server" placeholder="Reminder Title"></asp:TextBox>
        <asp:TextBox ID="txtDateTime" runat="server" placeholder="YYYY-MM-DD HH:MM"></asp:TextBox>
        <asp:Button ID="btnAddReminder" runat="server" Text="Add Reminder" OnClick="btnAddReminder_Click" CssClass="btn btn-primary" />
    </div>

    <div>
        <h3>Your Reminders</h3>
        <asp:GridView ID="gvReminders" runat="server" AutoGenerateColumns="False" CssClass="table"
            DataKeyNames="ReminderID" OnRowDeleting="gvReminders_RowDeleting">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="ReminderDateTime" HeaderText="Date & Time" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>