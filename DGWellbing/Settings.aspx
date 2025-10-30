<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="DGWellbing.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Settings</h2>

    <div>
        <h3>Notifications</h3>
        <asp:CheckBox ID="chkEnableReminders" runat="server" Text="Enable Reminders" />
    </div>

    <div>
        <h3>Theme</h3>
        <asp:RadioButtonList ID="rblTheme" runat="server">
            <asp:ListItem Text="Light Mode" Value="Light"></asp:ListItem>
            <asp:ListItem Text="Dark Mode" Value="Dark"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <br />
    <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" OnClick="btnSaveSettings_Click" CssClass="btn btn-primary" />

    <br /> <br />
    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-primary" OnClick="btnLogout_Click" />
</asp:Content>
