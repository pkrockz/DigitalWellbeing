<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DGWellbing.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Dashboard</h2>

    <!-- Focus Mode Statistics -->
    <div>
        <h3>Focus Mode Summary</h3>
        <p>Total Sessions: <asp:Label ID="lblTotalSessions" runat="server" Text="0"></asp:Label></p>
        <p>Total Time Spent: <asp:Label ID="lblTotalTime" runat="server" Text="0 mins"></asp:Label></p>
    </div>

    <!-- App Usage Summary -->
    <div>
        <h3>App Usage Summary</h3>
        <asp:GridView ID="gvAppUsage" runat="server" AutoGenerateColumns="true" CssClass="table"></asp:GridView>
    </div>
</asp:Content>