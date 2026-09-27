<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChallengeDetails.aspx.cs"
         Inherits="CyberArenaWebForms.Challenges.ChallengeDetailsPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <%-- ═══════════════════════════════════════════════════════
         Flash message (from POST redirect via SessionHelper)
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlFlash" runat="server" Visible="false">
        <div class="alert ca-auto-dismiss d-flex align-items-center gap-2 mb-3"
             role="alert" id="flash-alert">
            <i class="fas fa-fw" id="flash-icon" aria-hidden="true"></i>
            <asp:Literal ID="litFlashMsg" runat="server" />
        </div>
    </asp:Panel>

    <%-- ═══════════════════════════════════════════════════════
         Breadcrumb + Prev/Next navigation
    ═══════════════════════════════════════════════════════ --%>
    <div class="d-flex align-items-center justify-content-between mb-3 flex-wrap gap-2">
        <nav aria-label="breadcrumb" style="font-size:0.82rem;">
            <ol class="breadcrumb mb-0" style="background:none;padding:0;">
                <li class="breadcrumb-item">
                    <a href="<%= ResolveUrl("~/Challenges/Challenges.aspx") %>"
                       style="color:var(--ca-text-muted);">
                        <i class="fas fa-flag me-1" aria-hidden="true"></i>Challenges
                    </a>
                </li>
                <li class="breadcrumb-item active"
                    style="color:var(--ca-text-secondary);"
                    aria-current="page">
                    <asp:Literal ID="litBreadcrumbTitle" runat="server" />
                </li>
            </ol>
        </nav>

        <div class="d-flex gap-2">
            <asp:HyperLink ID="lnkPrev" runat="server"
                           CssClass="btn btn-secondary btn-sm"
                           ID="btn-prev-challenge"
                           Visible="false">
                <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Prev
            </asp:HyperLink>
            <asp:HyperLink ID="lnkNext" runat="server"
                           CssClass="btn btn-secondary btn-sm"
                           ID="btn-next-challenge"
                           Visible="false">
                Next <i class="fas fa-arrow-right ms-1" aria-hidden="true"></i>
            </asp:HyperLink>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Main Two-Column Layout
    ═══════════════════════════════════════════════════════ --%>
    <div class="ch-detail-layout">

        <%-- ── Left Column: Description + Hints + Files ──────── --%>
        <div>

            <%-- Challenge card --%>
            <div class="ch-detail-card mb-4">

                <%-- Header --%>
                <div class="ch-detail-header">
                    <h1 class="ch-detail-title">
                        <asp:Literal ID="litTitle" runat="server" />
                    </h1>
                    <div class="ch-detail-badges">
                        <div class="ch-card-icon" id="catIconDiv" runat="server"
                             style="width:32px;height:32px;font-size:0.85rem;"
                             aria-hidden="true">
                            <asp:Literal ID="litCatIcon" runat="server" />
                        </div>
                        <span style="font-size:0.875rem;color:var(--ca-text-secondary);">
                            <asp:Literal ID="litCategory" runat="server" />
                        </span>
                        <span class="ch-diff" id="diffBadge" runat="server">
                            <asp:Literal ID="litDifficulty" runat="server" />
                        </span>
                        <asp:Panel ID="pnlSolvedBadge" runat="server" Visible="false"
                                   style="display:inline;">
                            <span class="ch-solved-badge">
                                <i class="fas fa-circle-check" aria-hidden="true"></i> Solved
                                <asp:Literal ID="litSolvedAt" runat="server" />
                            </span>
                        </asp:Panel>
                    </div>
                </div>

                <%-- Body --%>
                <div class="ch-detail-body">

                    <%-- Description --%>
                    <div class="mb-3">
                        <h2 style="font-size:0.78rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.75rem;">
                            <i class="fas fa-align-left me-1" aria-hidden="true"></i> Description
                        </h2>
                        <div class="ch-description">
                            <asp:Literal ID="litDescription" runat="server" />
                        </div>
                    </div>

                    <%-- Attachments --%>
                    <asp:Panel ID="pnlAttachments" runat="server" Visible="false"
                               CssClass="ch-files-section">
                        <h3 class="ch-detail-section-label"
                            style="font-size:0.78rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.75rem;">
                            <i class="fas fa-paperclip me-1" aria-hidden="true"></i> Files &amp; Downloads
                        </h3>
                        <asp:Repeater ID="rptAttachments" runat="server">
                            <ItemTemplate>
                                <a class="ch-file-item"
                                   href="<%# Eval("DownloadUrl") %>"
                                   download
                                   aria-label="Download <%# Eval("FileName") %>">
                                    <i class="fas <%# Eval("FileIcon") %> ch-file-icon" aria-hidden="true"></i>
                                    <span class="ch-file-name"><%# Eval("FileName") %></span>
                                    <span class="ch-file-size"><%# Eval("FileSize") %></span>
                                    <i class="fas fa-download" style="color:var(--ca-text-muted);font-size:0.8rem;" aria-hidden="true"></i>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>

                    <%-- Hints --%>
                    <asp:Panel ID="pnlHints" runat="server" Visible="false"
                               CssClass="ch-hint-section">
                        <h3 style="font-size:0.78rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.75rem;">
                            <i class="fas fa-lightbulb me-1" style="color:var(--ca-warning);" aria-hidden="true"></i>
                            Hints (<asp:Literal ID="litHintCount" runat="server" /> available)
                        </h3>
                        <asp:Repeater ID="rptHints" runat="server">
                            <ItemTemplate>
                                <div class="ch-hint-card mb-2">
                                    <button class="ch-hint-toggle"
                                            id="hint-toggle-<%# Eval("Number") %>"
                                            onclick="toggleHint(<%# Eval("Number") %>)"
                                            type="button"
                                            aria-expanded="false"
                                            aria-controls="hint-body-<%# Eval("Number") %>">
                                        <span>
                                            <i class="fas fa-lightbulb me-1" aria-hidden="true"></i>
                                            Hint <%# Eval("Number") %>
                                            <%# (int)Eval("PointPenalty") > 0
                                                ? "<span style=\"font-size:0.72rem;opacity:0.7;\">(&#x2212;" + Eval("PointPenalty") + " pts penalty)</span>"
                                                : "" %>
                                        </span>
                                        <i class="fas fa-chevron-down"
                                           id="hint-chevron-<%# Eval("Number") %>"
                                           aria-hidden="true"></i>
                                    </button>
                                    <div class="ch-hint-body"
                                         id="hint-body-<%# Eval("Number") %>"
                                         role="region"
                                         aria-labelledby="hint-toggle-<%# Eval("Number") %>">
                                        <%# Eval("Text") %>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>

                </div>
            </div><%-- /.ch-detail-card --%>

            <%-- ─── Challenge Statistics card ───────────────── --%>
            <div class="ca-card">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">
                        <i class="fas fa-chart-bar me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                        Challenge Statistics
                    </h3>
                </div>
                <div class="row g-3">
                    <div class="col-6 col-md-3">
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value"
                                  data-count="<%= VM != null ? VM.Points.ToString() : "0" %>">
                                <asp:Literal ID="litStatPoints" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Points</span>
                        </div>
                    </div>
                    <div class="col-6 col-md-3">
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value"
                                  data-count="<%= VM != null ? VM.SolverCount.ToString() : "0" %>">
                                <asp:Literal ID="litStatSolvers" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Solvers</span>
                        </div>
                    </div>
                    <div class="col-6 col-md-3">
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value">
                                <asp:Literal ID="litStatSolveRate" runat="server" />%
                            </span>
                            <span class="ch-stat-mini-label">Solve Rate</span>
                        </div>
                    </div>
                    <div class="col-6 col-md-3">
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value">
                                <asp:Literal ID="litStatHints" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Hints</span>
                        </div>
                    </div>
                </div>

                <%-- Solve progress bar (only when solvers > 0) --%>
                <asp:Panel ID="pnlSolveProgress" runat="server" Visible="false">
                    <div class="mt-3">
                        <div class="d-flex justify-content-between mb-1">
                            <span style="font-size:0.78rem;color:var(--ca-text-muted);">Solve progress</span>
                            <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                                <asp:Literal ID="litSolveProgressPct" runat="server" />%
                            </span>
                        </div>
                        <div class="ca-progress">
                            <div class="ca-progress-bar" id="solve-progress-bar"
                                 data-width="<%= VM != null ? VM.SolvePercent.ToString() : "0" %>">
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

        </div><%-- /.left column --%>

        <%-- ── Right Column: Flag Submit ──────────────────────── --%>
        <div>
            <div class="ch-flag-card">

                <div class="ch-flag-header">
                    <h2 class="ch-flag-title">
                        <i class="fas fa-flag me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                        Submit Flag
                    </h2>
                </div>

                <div class="ch-flag-body">

                    <%-- Quick stats --%>
                    <div class="ch-stats-grid">
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value" style="color:var(--ca-accent);">
                                <asp:Literal ID="litFlagPoints" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Points</span>
                        </div>
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value">
                                #<asp:Literal ID="litFlagRank" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Your rank</span>
                        </div>
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value">
                                <asp:Literal ID="litFlagSolvers" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Solvers</span>
                        </div>
                        <div class="ch-stat-mini">
                            <span class="ch-stat-mini-value">
                                <asp:Literal ID="litFlagDifficulty" runat="server" />
                            </span>
                            <span class="ch-stat-mini-label">Difficulty</span>
                        </div>
                    </div>

                    <%-- Already-solved notice --%>
                    <asp:Panel ID="pnlAlreadySolved" runat="server" Visible="false"
                               CssClass="ch-already-solved"
                               role="status">
                        <i class="fas fa-circle-check" aria-hidden="true"></i>
                        <div>
                            <span style="font-weight:600;display:block;">Already solved!</span>
                            <asp:Literal ID="litAlreadySolvedDate" runat="server" />
                        </div>
                    </asp:Panel>

                    <%-- Submission result panel (shown after POST) --%>
                    <asp:Panel ID="pnlSubmitResult" runat="server" Visible="false"
                               role="alert">
                        <asp:Literal ID="litSubmitResult" runat="server" />
                    </asp:Panel>

                    <%-- Flag submission form --%>
                    <%-- The <form runat="server"> lives in Site.Master (MainForm).
                         The asp:TextBox and asp:Button below post back to it. --%>

                    <%-- Flag validation error --%>
                    <asp:Panel ID="pnlFlagError" runat="server" Visible="false">
                        <span class="field-validation-error d-block mt-1">
                            <asp:Literal ID="litFlagError" runat="server" />
                        </span>
                    </asp:Panel>

                    <label class="ch-flag-label" for="txtFlag">
                        <i class="fas fa-terminal me-1" aria-hidden="true"></i> Enter Flag
                    </label>
                    <div class="ch-flag-input-wrap">
                        <i class="fas fa-flag ch-flag-icon" aria-hidden="true"></i>
                        <asp:TextBox ID="txtFlag"
                                     runat="server"
                                     CssClass="ch-flag-input"
                                     placeholder="CTF{your_flag_here}"
                                     autocomplete="off"
                                     spellcheck="false"
                                     aria-label="Flag input"
                                     aria-describedby="flag-hint"
                                     ClientIDMode="Static" />
                    </div>
                    <p id="flag-hint"
                       style="font-size:0.75rem;color:var(--ca-text-muted);margin-top:0.4rem;margin-bottom:0;">
                        Format: <code style="color:var(--ca-accent);background:var(--ca-accent-light);padding:0.1em 0.4em;border-radius:3px;">CTF{...}</code>
                    </p>
                    <asp:RequiredFieldValidator
                        ID="rfvFlag" runat="server"
                        ControlToValidate="txtFlag"
                        ErrorMessage="Please enter a flag."
                        CssClass="field-validation-error d-block mt-1"
                        Display="Dynamic"
                        ValidationGroup="FlagGroup" />

                    <%-- Visible HTML submit button (icon support) → triggers hidden asp:Button --%>
                    <button type="button"
                            id="btn-submit-flag-ui"
                            class="ch-flag-submit"
                            onclick="handleFlagSubmit(this);">
                        <i class="fas fa-paper-plane me-2" aria-hidden="true"></i>
                        <asp:Literal ID="litSubmitBtnText" runat="server" />
                    </button>

                    <%-- Real postback button (hidden) --%>
                    <asp:Button ID="btnSubmitFlag"
                                runat="server"
                                Text="Submit Flag"
                                OnClick="btnSubmitFlag_Click"
                                ValidationGroup="FlagGroup"
                                UseSubmitBehavior="false"
                                style="display:none;" />

                    <%-- Hidden challenge ID --%>
                    <asp:HiddenField ID="hfChallengeId" runat="server" />

                    <%-- Hint reminder (shown only when hints available and not solved) --%>
                    <asp:Panel ID="pnlHintReminder" runat="server" Visible="false">
                        <div class="mt-3 pt-3" style="border-top:1px solid var(--ca-border-light);">
                            <p style="font-size:0.78rem;color:var(--ca-text-muted);margin:0;">
                                <i class="fas fa-lightbulb me-1"
                                   style="color:var(--ca-warning);" aria-hidden="true"></i>
                                Stuck? Scroll down to see
                                <strong><asp:Literal ID="litHintReminderCount" runat="server" /> hint<asp:Literal ID="litHintReminderPlural" runat="server" /></strong> available.
                            </p>
                        </div>
                    </asp:Panel>

                </div><%-- /.ch-flag-body --%>
            </div>
        </div><%-- /.right column --%>

    </div><%-- /.ch-detail-layout --%>

</asp:Content>

<%-- ─── Page Scripts ─────────────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
// ── Flag form submit handler ──────────────────────────────────
function handleFlagSubmit(btn) {
    if (typeof Page_ClientValidate === 'function' &&
        !Page_ClientValidate('FlagGroup')) return;

    var input = document.getElementById('txtFlag');
    var val   = input ? input.value.trim() : '';
    if (!val) {
        if (input) { input.style.borderColor = 'var(--ca-danger)'; input.focus(); }
        return;
    }

    btn.disabled = true;
    btn.innerHTML = '<i class="fas fa-circle-notch fa-spin me-2"></i>Submitting&#x2026;';
    __doPostBack('<%= btnSubmitFlag.UniqueID %>', '');
}

// ── Clear error border on input ───────────────────────────────
(function () {
    var inp = document.getElementById('txtFlag');
    if (inp) inp.addEventListener('input', function () { this.style.borderColor = ''; });
}());

// ── Hint accordion toggle ─────────────────────────────────────
function toggleHint(num) {
    var body    = document.getElementById('hint-body-'    + num);
    var chevron = document.getElementById('hint-chevron-' + num);
    var btn     = document.getElementById('hint-toggle-'  + num);
    if (!body) return;
    var isOpen = body.classList.contains('show');
    body.classList.toggle('show', !isOpen);
    if (chevron) chevron.style.transform = isOpen ? '' : 'rotate(180deg)';
    if (btn)     btn.setAttribute('aria-expanded', (!isOpen).toString());
}

// ── Animate solve progress bar ────────────────────────────────
(function () {
    var bar = document.getElementById('solve-progress-bar');
    if (bar) {
        var w = parseInt(bar.dataset.width || 0, 10);
        bar.style.width = '0%';
        setTimeout(function () { bar.style.transition = 'width 0.8s ease'; bar.style.width = w + '%'; }, 200);
    }
}());

// ── Flash auto-dismiss ────────────────────────────────────────
(function () {
    var flash = document.querySelector('.ca-auto-dismiss');
    if (flash) {
        setTimeout(function () {
            flash.style.opacity    = '0';
            flash.style.transition = 'opacity 0.5s';
            setTimeout(function () { flash.style.display = 'none'; }, 500);
        }, 4000);
    }
}());
</script>
</asp:Content>