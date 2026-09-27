<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs"
         Inherits="CyberArenaWebForms.AdminLogin"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Admin Login</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="row justify-content-center">
    <div class="col-12 col-md-6 col-lg-5">
        <div class="ca-card">
            <div class="ca-card-header text-center" style="display:block;">
                <div class="ca-icon-circle mx-auto mb-3" style="width:64px;height:64px;font-size:1.75rem;">
                    <i class="fas fa-user-shield" aria-hidden="true"></i>
                </div>
                <h2 class="ca-card-title mb-1">Admin Portal</h2>
                <div class="ca-card-subtitle">Restricted access</div>
            </div>

            <div class="ca-card-body p-4">
                
                <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger mb-4">
                    <i class="fas fa-triangle-exclamation me-2" aria-hidden="true"></i>
                    <asp:Literal ID="litErrorMsg" runat="server" />
                </asp:Panel>

                <div class="mb-3">
                    <label for="txtUsername" class="form-label">Admin Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" autocomplete="off" />
                </div>

                <div class="mb-4">
                    <label for="txtPassword" class="form-label">Passphrase</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <asp:Button ID="btnLogin" runat="server" Text="Authorise" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                
                <div class="mt-4 text-center">
                    <a href="<%= ResolveUrl("~/") %>" class="text-decoration-none" style="font-size:0.875rem;color:var(--ca-text-muted);">
                        <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Return to User Site
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
