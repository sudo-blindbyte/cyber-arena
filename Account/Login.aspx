<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
         Inherits="CyberArenaWebForms.Account.LoginPage"
         MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Sign In</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="ca-auth-page">

    <%-- ─── Left Panel (Brand / Hero) ──────────────────────── --%>
    <div class="ca-auth-left" aria-hidden="true">
        <div class="ca-auth-grid"></div>

        <%-- Logo --%>
        <a class="ca-auth-logo" href="<%= ResolveUrl("~/Default.aspx") %>">
            <div class="ca-auth-logo-icon">
                <i class="fas fa-shield-halved"></i>
            </div>
            <div>
                <span class="ca-auth-logo-text">CyberArena</span>
                <span class="ca-auth-logo-sub">CTF Management Platform</span>
            </div>
        </a>

        <%-- Headline --%>
        <div class="ca-auth-brand">
            <h2 class="ca-auth-headline">
                Hack. Compete.<br /><span>Dominate.</span>
            </h2>
            <p class="ca-auth-subtext">
                Join thousands of security researchers competing in real-world
                Capture The Flag challenges across web, pwn, crypto, forensics and more.
            </p>
        </div>

        <%-- Features --%>
        <div class="ca-auth-features">
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-flag"></i></div>
                <span>100+ challenges across 6 categories</span>
            </div>
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-trophy"></i></div>
                <span>Real-time leaderboard &amp; rankings</span>
            </div>
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-users"></i></div>
                <span>Team-based competitions</span>
            </div>
        </div>

        <div class="ca-auth-shield" aria-hidden="true">
            <i class="fas fa-shield-halved"></i>
        </div>
    </div>

    <%-- ─── Right Panel (Form) ──────────────────────────────── --%>
    <div class="ca-auth-right">
        <div class="ca-auth-form-wrap">

            <div class="ca-auth-form-header">
                <h1 class="ca-auth-form-title">Welcome back</h1>
                <p class="ca-auth-form-subtitle">Sign in to your CyberArena account</p>
            </div>

            <%-- ── Server-side error panel (replaces ModelState summary) --%>
            <asp:Panel ID="pnlError" runat="server" Visible="false"
                       CssClass="alert alert-danger mb-3"
                       role="alert">
                <i class="fas fa-circle-exclamation me-2" aria-hidden="true"></i>
                <asp:Literal ID="litErrorMessage" runat="server" />
            </asp:Panel>

            <%-- ═══════════════════════════════════════════════
                 NOTE: The <form runat="server"> lives in Auth.Master
                 (id="AuthForm"). All controls below post back to it.
                 ═══════════════════════════════════════════════ --%>

            <%-- ── Email ──────────────────────────────────────── --%>
            <div class="ca-auth-field">
                <label for="txtEmail">Email address</label>
                <div class="ca-input-group">
                    <span class="ca-input-icon">
                        <i class="fas fa-envelope" aria-hidden="true"></i>
                    </span>
                    <asp:TextBox ID="txtEmail"
                                 runat="server"
                                 CssClass="form-control"
                                 TextMode="Email"
                                 placeholder="you@example.com"
                                 autocomplete="email"
                                 spellcheck="false"
                                 ClientIDMode="Static" />
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Email is required."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="LoginGroup" />
                <asp:RegularExpressionValidator
                    ID="revEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Enter a valid email address."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="LoginGroup" />
            </div>

            <%-- ── Password ──────────────────────────────────── --%>
            <div class="ca-auth-field">
                <label for="txtPassword">Password</label>
                <div class="ca-input-group">
                    <span class="ca-input-icon">
                        <i class="fas fa-lock" aria-hidden="true"></i>
                    </span>
                    <asp:TextBox ID="txtPassword"
                                 runat="server"
                                 CssClass="form-control"
                                 TextMode="Password"
                                 placeholder="••••••••"
                                 autocomplete="current-password"
                                 ClientIDMode="Static" />
                    <%-- Show/hide toggle — plain HTML, no postback --%>
                    <button class="ca-pwd-toggle"
                            type="button"
                            data-target="txtPassword"
                            aria-label="Show/hide password">
                        <i class="fas fa-eye" aria-hidden="true"></i>
                    </button>
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password is required."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="LoginGroup" />
            </div>

            <%-- ── Remember me + Forgot password row ──────────── --%>
            <div class="d-flex align-items-center justify-content-between mb-3">
                <div class="ca-auth-remember">
                    <asp:CheckBox ID="chkRememberMe"
                                  runat="server"
                                  ClientIDMode="Static" />
                    <label for="chkRememberMe">Remember me</label>
                </div>
                <a href="<%= ResolveUrl("~/Account/ForgotPassword.aspx") %>"
                   class="ca-forgot-link"
                   style="margin:0; font-size:0.82rem;">
                    Forgot password?
                </a>
            </div>

            <%-- ── Submit Button ──────────────────────────────── --%>
            <%-- asp:Button renders <input type="submit">, which cannot contain
                 inner HTML (icon). We wrap it with a styled <label> trick and
                 hide the real button — or use OnClientClick for the loading state
                 while keeping the real submit action on btnLogin. --%>

            <%-- Visible styled button (HTML) triggers the hidden asp:Button --%>
            <button type="button"
                    id="btn-login-ui"
                    class="ca-auth-submit"
                    onclick="handleLoginSubmit(this);">
                <i class="fas fa-arrow-right-to-bracket me-2" aria-hidden="true"></i>
                Sign In
            </button>

            <%-- Real server-side button (hidden, triggered by JS above) --%>
            <asp:Button ID="btnLogin"
                        runat="server"
                        Text="Sign In"
                        OnClick="btnLogin_Click"
                        ValidationGroup="LoginGroup"
                        UseSubmitBehavior="false"
                        style="display:none;" />

            <%-- ── Register link ──────────────────────────────── --%>
            <p class="ca-auth-footer-text">
                Don't have an account?
                <a href="<%= ResolveUrl("~/Account/Register.aspx") %>">Create one free</a>
            </p>

        </div><%-- /.ca-auth-form-wrap --%>
    </div><%-- /.ca-auth-right --%>

</div><%-- /.ca-auth-page --%>

</asp:Content>

<%-- ─── Page-specific scripts ─────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
// ── Login submit handler ──────────────────────────────────────
function handleLoginSubmit(btn) {
    // Run ASP.NET client-side validators first
    if (typeof Page_ClientValidate === 'function' &&
        !Page_ClientValidate('LoginGroup')) {
        return false;
    }

    // Loading state
    btn.disabled = true;
    btn.innerHTML = '<i class="fas fa-circle-notch fa-spin me-2"></i>Signing in\u2026';

    // Fire the hidden asp:Button postback
    __doPostBack('<%= btnLogin.UniqueID %>', '');
}

// ── Password show/hide toggle ─────────────────────────────────
document.querySelectorAll('.ca-pwd-toggle').forEach(function (toggleBtn) {
    toggleBtn.addEventListener('click', function () {
        var targetId = this.getAttribute('data-target');
        var input    = document.getElementById(targetId);
        var icon     = this.querySelector('i');
        if (!input) return;

        if (input.type === 'password') {
            input.type = 'text';
            icon.classList.replace('fa-eye', 'fa-eye-slash');
            this.setAttribute('aria-label', 'Hide password');
        } else {
            input.type = 'password';
            icon.classList.replace('fa-eye-slash', 'fa-eye');
            this.setAttribute('aria-label', 'Show password');
        }
    });
});

// ── Enter key on form submits via the UI button ───────────────
document.addEventListener('keydown', function (e) {
    if (e.key === 'Enter') {
        var active = document.activeElement;
        // Only trigger if focus is inside an auth input
        if (active && (active.id === 'txtEmail' || active.id === 'txtPassword')) {
            e.preventDefault();
            handleLoginSubmit(document.getElementById('btn-login-ui'));
        }
    }
});
</script>
</asp:Content>
