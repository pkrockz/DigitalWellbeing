<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DGWellbing.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-container">
        <div class="auth-box">
            <h2>Welcome New User!</h2>
                <label>Name</label>
                <br />
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Width="312px"></asp:TextBox>
                <br />
                <br />
                <label>Email<br />
                </label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Width="312px"></asp:TextBox>
                <br />
                <br />
                <label>Password</label>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Width="313px"></asp:TextBox>
                <br />
                <br />
                <label>Confirm Password<br />
                </label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" Width="309px"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password is required." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtPassword"
                    ControlToCompare="txtConfirmPassword" ErrorMessage="Passwords do not match."
                    CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                <br />
                <br />
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" CausesValidation="False" UseSubmitBehavior="False" />
                <br />
            </div>
        </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
</asp:Content>
