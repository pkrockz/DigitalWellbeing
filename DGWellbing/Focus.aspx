<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Focus.aspx.cs" Inherits="DGWellbing.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Focus Mode</h2>

    <div>
        <h3>Elapsed Time: <span id="timer">00:00:00</span></h3>

        <asp:Button ID="btnStartFocus" runat="server" Text="Start Focus Mode" OnClick="btnStartFocus_Click" />
        <asp:Button ID="btnPauseFocus" runat="server" Text="Pause" OnClientClick="togglePause(); return false;" Enabled="false" />
        <asp:Button ID="btnEndFocus" runat="server" Text="End Focus Mode" OnClick="btnEndFocus_Click" Enabled="false" />
    </div>

    <div>
        <h3>Previous Focus Sessions</h3>
        <asp:GridView ID="gvFocusSessions" runat="server" AutoGenerateColumns="true" CssClass="table"></asp:GridView>
    </div>

    <script>
        let startTime;
        let elapsedTime = 0;
        let timerInterval;
        let isPaused = false;

        function startTimer() {
            startTime = Date.now() - elapsedTime;
            timerInterval = setInterval(updateTimer, 1000);
        }

        function updateTimer() {
            if (!isPaused) {
                let currentTime = Date.now();
                elapsedTime = currentTime - startTime;
                let totalSeconds = Math.floor(elapsedTime / 1000);
                let hours = String(Math.floor(totalSeconds / 3600)).padStart(2, '0');
                let minutes = String(Math.floor((totalSeconds % 3600) / 60)).padStart(2, '0');
                let seconds = String(totalSeconds % 60).padStart(2, '0');
                document.getElementById("timer").innerText = `${hours}:${minutes}:${seconds}`;
            }
        }

        function togglePause() {
            isPaused = !isPaused;
            document.getElementById("<%= btnPauseFocus.ClientID %>").value = isPaused ? "Resume" : "Pause";
            if (!isPaused) startTimer();
        }
    </script>
</asp:Content>