<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DGWellbing.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-container">
        <div class="auth-box">
            <h2>Welcome Back!</h2>

            <div>
                <label>Email<br />
        &nbsp;</label><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Width="312px"></asp:TextBox>
                <label>
                <br />
                &nbsp;Password<br />
        &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Width="312px"></asp:TextBox>
                </label>
                <br />
                <br /> 
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" CausesValidation="False" />
                <br />
                <br />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <br /> &nbsp;
            </div>
         </div>
        </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
</asp:Content>
