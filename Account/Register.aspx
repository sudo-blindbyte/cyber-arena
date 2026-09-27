<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs"
         Inherits="CyberArenaWebForms.Account.RegisterPage"
         MasterPageFile="~/Auth.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Create Account</asp:Content>

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
                Start Your<br /><span>Hacking Journey</span>
            </h2>
            <p class="ca-auth-subtext">
                Create your free account and join the global community of
                CTF competitors. Begin with beginner challenges and work your
                way to expert-level exploits.
            </p>
        </div>

        <%-- Features --%>
        <div class="ca-auth-features">
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-rocket"></i></div>
                <span>Free to join — unlimited access</span>
            </div>
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-chart-line"></i></div>
                <span>Track your progress &amp; growth</span>
            </div>
            <div class="ca-auth-feature">
                <div class="ca-auth-feature-icon"><i class="fas fa-medal"></i></div>
                <span>Earn badges &amp; climb rankings</span>
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
                <h1 class="ca-auth-form-title">Create account</h1>
                <p class="ca-auth-form-subtitle">Join CyberArena &#8212; it&#8217;s completely free</p>
            </div>

            <%-- ── Server-side error panel (replaces ModelState summary) --%>
            <asp:Panel ID="pnlError" runat="server" Visible="false"
                       CssClass="alert alert-danger mb-3"
                       role="alert">
                <i class="fas fa-circle-exclamation me-2" aria-hidden="true"></i>
                <asp:Literal ID="litErrorMessage" runat="server" />
            </asp:Panel>

            <%-- ══════════════════════════════════════════════════════
                 NOTE: The <form runat="server"> lives in Auth.Master
                 (id="AuthForm"). All controls below post back to it.
                 ══════════════════════════════════════════════════════ --%>

            <%-- ── Username ────────────────────────────────────────── --%>
            <div class="ca-auth-field">
                <label for="txtUsername">Username</label>
                <div class="ca-input-group">
                    <span class="ca-input-icon">
                        <i class="fas fa-at" aria-hidden="true"></i>
                    </span>
                    <asp:TextBox ID="txtUsername"
                                 runat="server"
                                 CssClass="form-control"
                                 placeholder="coolhacker_42"
                                 autocomplete="username"
                                 spellcheck="false"
                                 ClientIDMode="Static" />
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvUsername" runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="Username is required."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
                <asp:RegularExpressionValidator
                    ID="revUsername" runat="server"
                    ControlToValidate="txtUsername"
                    ValidationExpression="^[a-zA-Z0-9_\-]{3,50}$"
                    ErrorMessage="Username must be 3–50 characters. Only letters, numbers, underscores and hyphens."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
            </div>

            <%-- ── Email ──────────────────────────────────────────── --%>
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
                                 ClientIDMode="Static" />
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="Email is required."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
                <asp:RegularExpressionValidator
                    ID="revEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Enter a valid email address."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
            </div>

            <%-- ── Password ──────────────────────────────────────── --%>
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
                                 placeholder="Min. 8 characters"
                                 autocomplete="new-password"
                                 data-pwd-strength="pwd-strength-meter"
                                 ClientIDMode="Static" />
                    <button class="ca-pwd-toggle" type="button"
                            data-target="txtPassword"
                            aria-label="Show/hide password">
                        <i class="fas fa-eye" aria-hidden="true"></i>
                    </button>
                </div>
                <%-- Password strength indicator (driven by JS) --%>
                <div class="ca-pwd-strength mt-2" id="pwd-strength-meter" aria-live="polite">
                    <div class="ca-pwd-strength-bar" aria-hidden="true"></div>
                    <div class="ca-pwd-strength-bar" aria-hidden="true"></div>
                    <div class="ca-pwd-strength-bar" aria-hidden="true"></div>
                    <div class="ca-pwd-strength-bar" aria-hidden="true"></div>
                    <span class="ca-pwd-strength-label"></span>
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password is required."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
                <asp:RegularExpressionValidator
                    ID="revPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ValidationExpression="^.{8,100}$"
                    ErrorMessage="Password must be at least 8 characters."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
            </div>

            <%-- ── Confirm Password ──────────────────────────────── --%>
            <div class="ca-auth-field">
                <label for="txtConfirmPassword">Confirm password</label>
                <div class="ca-input-group">
                    <span class="ca-input-icon">
                        <i class="fas fa-lock-open" aria-hidden="true"></i>
                    </span>
                    <asp:TextBox ID="txtConfirmPassword"
                                 runat="server"
                                 CssClass="form-control"
                                 TextMode="Password"
                                 placeholder="Repeat your password"
                                 autocomplete="new-password"
                                 ClientIDMode="Static" />
                    <button class="ca-pwd-toggle" type="button"
                            data-target="txtConfirmPassword"
                            aria-label="Show/hide password">
                        <i class="fas fa-eye" aria-hidden="true"></i>
                    </button>
                </div>
                <asp:RequiredFieldValidator
                    ID="rfvConfirmPassword" runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ErrorMessage="Please confirm your password."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
                <%-- CompareValidator replaces [Compare("Password")] data annotation --%>
                <asp:CompareValidator
                    ID="cvConfirmPassword" runat="server"
                    ControlToValidate="txtConfirmPassword"
                    ControlToCompare="txtPassword"
                    Operator="Equal"
                    ErrorMessage="Passwords do not match."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
            </div>

            <%-- ── Team Name (optional) ──────────────────────────── --%>
            <div class="ca-auth-field">
                <label for="txtTeamName">
                    Team name
                    <span style="color:var(--ca-text-muted);font-weight:400;">(optional)</span>
                </label>
                <div class="ca-input-group">
                    <span class="ca-input-icon">
                        <i class="fas fa-users" aria-hidden="true"></i>
                    </span>
                    <asp:TextBox ID="txtTeamName"
                                 runat="server"
                                 CssClass="form-control"
                                 placeholder="ByteBreakers"
                                 autocomplete="off"
                                 ClientIDMode="Static" />
                </div>
                <%-- Only max-length validation — field is optional --%>
                <asp:RegularExpressionValidator
                    ID="revTeamName" runat="server"
                    ControlToValidate="txtTeamName"
                    ValidationExpression="^.{0,100}$"
                    ErrorMessage="Team name cannot exceed 100 characters."
                    CssClass="field-validation-error mt-1 d-block"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup" />
            </div>

            <%-- ── Accept Terms ────────────────────────────────────── --%>
            <div class="mb-3">
                <div class="ca-auth-remember">
                    <asp:CheckBox ID="chkAcceptTerms"
                                  runat="server"
                                  ClientIDMode="Static" />
                    <label for="chkAcceptTerms" style="font-size:0.82rem;">
                        I agree to the
                        <a href="#" style="color:var(--ca-accent);">Terms of Service</a> and
                        <a href="#" style="color:var(--ca-accent);">Privacy Policy</a>
                    </label>
                </div>
                <%-- CustomValidator fires server-side to check the checkbox --%>
                <asp:CustomValidator
                    ID="cvAcceptTerms" runat="server"
                    ControlToValidate="chkAcceptTerms"
                    ErrorMessage="You must accept the Terms and Conditions."
                    CssClass="field-validation-error d-block mt-1"
                    Display="Dynamic"
                    ValidationGroup="RegisterGroup"
                    ClientValidationFunction="validateTerms"
                    OnServerValidate="cvAcceptTerms_ServerValidate" />
            </div>

            <%-- ── Submit Button ───────────────────────────────────── --%>
            <%-- Visible styled HTML button (for icon support) triggers hidden asp:Button --%>
            <button type="button"
                    id="btn-register-ui"
                    class="ca-auth-submit"
                    onclick="handleRegisterSubmit(this);">
                <i class="fas fa-user-plus me-2" aria-hidden="true"></i>
                Create Account
            </button>

            <%-- Real server-side button (hidden, triggered by JS above) --%>
            <asp:Button ID="btnRegister"
                        runat="server"
                        Text="Create Account"
                        OnClick="btnRegister_Click"
                        ValidationGroup="RegisterGroup"
                        UseSubmitBehavior="false"
                        style="display:none;" />

            <%-- ── Sign-in link ────────────────────────────────────── --%>
            <p class="ca-auth-footer-text">
                Already have an account?
                <a href="<%= ResolveUrl("~/Account/Login.aspx") %>">Sign in</a>
            </p>

        </div><%-- /.ca-auth-form-wrap --%>
    </div><%-- /.ca-auth-right --%>

</div><%-- /.ca-auth-page --%>

</asp:Content>

<%-- ─── Page-specific scripts ─────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
// ── Register submit handler ───────────────────────────────────
function handleRegisterSubmit(btn) {
    if (typeof Page_ClientValidate === 'function' &&
        !Page_ClientValidate('RegisterGroup')) {
        return false;
    }
    btn.disabled = true;
    btn.innerHTML = '<i class="fas fa-circle-notch fa-spin me-2"></i>Creating account\u2026';
    __doPostBack('<%= btnRegister.UniqueID %>', '');
}

// ── Client-side terms checkbox validator ─────────────────────
function validateTerms(sender, args) {
    var chk = document.getElementById('chkAcceptTerms');
    args.IsValid = chk && chk.checked;
}

// ── Password show/hide toggle ─────────────────────────────────
document.querySelectorAll('.ca-pwd-toggle').forEach(function (btn) {
    btn.addEventListener('click', function () {
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

// ── Password strength meter ───────────────────────────────────
(function () {
    var pwdInput = document.getElementById('txtPassword');
    var meter    = document.getElementById('pwd-strength-meter');
    if (!pwdInput || !meter) return;

    var bars  = meter.querySelectorAll('.ca-pwd-strength-bar');
    var label = meter.querySelector('.ca-pwd-strength-label');

    function calcStrength(val) {
        var score = 0;
        if (val.length >= 8)  score++;
        if (val.length >= 12) score++;
        if (/[A-Z]/.test(val) && /[a-z]/.test(val)) score++;
        if (/[0-9]/.test(val)) score++;
        if (/[^A-Za-z0-9]/.test(val)) score++;
        return Math.min(score, 4);
    }

    var levels = ['', 'Weak', 'Fair', 'Good', 'Strong'];
    var classes = ['', 'filled-weak', 'filled-fair', 'filled-fair', 'filled-strong'];

    pwdInput.addEventListener('input', function () {
        var score = calcStrength(this.value);
        bars.forEach(function (bar, i) {
            bar.className = 'ca-pwd-strength-bar';
            if (i < score) bar.classList.add(classes[score]);
        });
        label.textContent = this.value.length > 0 ? levels[score] : '';
    });
}());

// ── Real-time password match indicator ───────────────────────
(function () {
    var pwd     = document.getElementById('txtPassword');
    var confirm = document.getElementById('txtConfirmPassword');
    if (!pwd || !confirm) return;

    function checkMatch() {
        if (confirm.value.length === 0) {
            confirm.style.borderColor = '';
            return;
        }
        confirm.style.borderColor = pwd.value === confirm.value
            ? 'var(--ca-success)'
            : 'var(--ca-danger)';
    }

    pwd.addEventListener('input', checkMatch);
    confirm.addEventListener('input', checkMatch);
}());
</script>
</asp:Content>
