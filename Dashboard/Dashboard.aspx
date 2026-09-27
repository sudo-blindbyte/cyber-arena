<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
         Inherits="CyberArenaWebForms.Dashboard.DashboardPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Dashboard</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <%-- Dashboard uses the main cyber-arena.css (already in Site.Master) --%>
    <%-- No additional per-page CSS needed --%>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ═══════════════════════════════════════════════════════
         Welcome Header
    ═══════════════════════════════════════════════════════ --%>
    <div class="d-flex align-items-start justify-content-between mb-4 flex-wrap gap-3">
        <div>
            <h2 style="font-family:'Space Grotesk',sans-serif;font-size:1.5rem;font-weight:700;color:var(--ca-text-primary);margin-bottom:0.2rem;">
                Welcome back,
                <span style="color:var(--ca-accent);">
                    <asp:Literal ID="litUsername" runat="server" />
                </span> &#x1F44B;
            </h2>
            <p style="color:var(--ca-text-muted);font-size:0.875rem;margin:0;">
                <asp:Literal ID="litDate" runat="server" />
                &nbsp;&middot;&nbsp;
                <asp:Panel ID="pnlTeamName" runat="server" Visible="false" style="display:inline;">
                    <i class="fas fa-users me-1" aria-hidden="true"></i>
                    <asp:Literal ID="litTeamName" runat="server" />
                </asp:Panel>
            </p>
        </div>
        <div class="d-flex gap-2">
            <a href="<%= ResolveUrl("~/Challenges/Challenges.aspx") %>"
               class="btn btn-primary btn-sm" id="btn-browse-challenges">
                <i class="fas fa-flag me-1" aria-hidden="true"></i>Browse Challenges
            </a>
            <a href="<%= ResolveUrl("~/Competitions/Competitions.aspx") %>"
               class="btn btn-secondary btn-sm" id="btn-competitions">
                <i class="fas fa-bolt me-1" aria-hidden="true"></i>Competitions
            </a>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Stat Cards Row
    ═══════════════════════════════════════════════════════ --%>
    <div class="row g-3 mb-4">

        <%-- Total Score --%>
        <div class="col-6 col-xl-3">
            <div class="ca-stat-card accent" role="region" aria-label="Total score">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Total Score</div>
                    <div class="ca-stat-value"
                         data-count="<%= VM.TotalScore %>">
                        <asp:Literal ID="litTotalScore" runat="server" />
                    </div>
                    <div class="ca-stat-delta up">
                        <i class="fas fa-arrow-trend-up" aria-hidden="true"></i>
                        +<asp:Literal ID="litScoreDelta" runat="server" /> pts today
                    </div>
                </div>
                <div class="ca-stat-icon" aria-hidden="true"><i class="fas fa-star"></i></div>
            </div>
        </div>

        <%-- Challenges Solved --%>
        <div class="col-6 col-xl-3">
            <div class="ca-stat-card success" role="region" aria-label="Challenges solved">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Challenges Solved</div>
                    <div class="ca-stat-value"
                         data-count="<%= VM.ChallengesSolved %>">
                        <asp:Literal ID="litChallengesSolved" runat="server" />
                    </div>
                    <div class="ca-stat-delta neutral">
                        of <asp:Literal ID="litTotalChallenges" runat="server" /> total
                        &nbsp;&middot;&nbsp;
                        <asp:Literal ID="litSolvedPercent" runat="server" />%
                    </div>
                </div>
                <div class="ca-stat-icon" aria-hidden="true"><i class="fas fa-flag-checkered"></i></div>
            </div>
        </div>

        <%-- Global Rank --%>
        <div class="col-6 col-xl-3">
            <div class="ca-stat-card warning" role="region" aria-label="Global rank">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Global Rank</div>
                    <div class="ca-stat-value">
                        #<asp:Literal ID="litCurrentRank" runat="server" />
                    </div>
                    <div class="ca-stat-delta">
                        <asp:Literal ID="litRankDelta" runat="server" />
                    </div>
                </div>
                <div class="ca-stat-icon" aria-hidden="true"><i class="fas fa-trophy"></i></div>
            </div>
        </div>

        <%-- Team Rank --%>
        <div class="col-6 col-xl-3">
            <div class="ca-stat-card info" role="region" aria-label="Team rank">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Team Rank</div>
                    <div class="ca-stat-value">
                        #<asp:Literal ID="litTeamRank" runat="server" />
                    </div>
                    <div class="ca-stat-delta neutral">
                        <asp:Literal ID="litTeamNameStat" runat="server" />
                    </div>
                </div>
                <div class="ca-stat-icon" aria-hidden="true"><i class="fas fa-users"></i></div>
            </div>
        </div>

    </div><%-- /.row stat cards --%>

    <%-- ═══════════════════════════════════════════════════════
         Main Grid
    ═══════════════════════════════════════════════════════ --%>
    <div class="row g-4">

        <%-- ── Left Column (8 cols) ─────────────────────────── --%>
        <div class="col-lg-8">

            <%-- Score History Chart --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Score Progression</h3>
                        <p class="ca-card-subtitle">Last 7 days</p>
                    </div>
                    <span class="ca-tag success">
                        <i class="fas fa-arrow-trend-up me-1" aria-hidden="true"></i>+<asp:Literal ID="litChartDelta" runat="server" /> pts
                    </span>
                </div>

                <%-- Bar chart: rendered server-side as data-height divs (JS animates) --%>
                <div class="ca-chart-placeholder" role="img" aria-label="Score history bar chart">
                    <asp:Repeater ID="rptScoreHistory" runat="server">
                        <ItemTemplate>
                            <div class="ca-chart-bar <%# (bool)Eval("IsHighest") ? "active" : "" %>"
                                 data-height="<%# Eval("HeightPercent") %>"
                                 data-delay="<%# Container.ItemIndex * 70 %>"
                                 title="<%# Eval("Label") %>: <%# string.Format("{0:N0}", Eval("Score")) %> pts"
                                 role="img"
                                 aria-label="<%# Eval("Label") %>: <%# string.Format("{0:N0}", Eval("Score")) %> points">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <%-- X-axis labels --%>
                <div class="d-flex justify-content-between mt-2 px-1">
                    <asp:Repeater ID="rptChartLabels" runat="server">
                        <ItemTemplate>
                            <span style="font-size:0.68rem;color:var(--ca-text-muted);flex:1;text-align:center;">
                                <%# Eval("Label") %>
                            </span>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <%-- Recent Submissions Table --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Recent Submissions</h3>
                        <p class="ca-card-subtitle">Your latest flag captures</p>
                    </div>
                    <a href="#" class="btn btn-secondary btn-sm" style="font-size:0.78rem;">
                        View all <i class="fas fa-arrow-right ms-1" aria-hidden="true"></i>
                    </a>
                </div>

                <%-- Table shown when there are submissions --%>
                <asp:Panel ID="pnlSubmissionsTable" runat="server">
                    <div class="table-responsive">
                        <table class="table" aria-label="Recent submissions">
                            <thead>
                                <tr>
                                    <th>Challenge</th>
                                    <th>Category</th>
                                    <th>Difficulty</th>
                                    <th class="text-end">Points</th>
                                    <th class="text-end">Time</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptSubmissions" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <div class="d-flex align-items-center gap-2">
                                                    <i class="fas fa-circle-check"
                                                       style="color:var(--ca-success);font-size:0.8rem;"
                                                       aria-hidden="true"></i>
                                                    <span style="font-weight:500;"><%# Eval("ChallengeName") %></span>
                                                </div>
                                            </td>
                                            <td>
                                                <span class="ca-tag"
                                                      style="background:var(--ca-accent-light);color:var(--ca-accent);">
                                                    <%# Eval("Category") %>
                                                </span>
                                            </td>
                                            <td>
                                                <span class="ca-tag <%# Eval("Difficulty") %>">
                                                    <%# Eval("Difficulty").ToString().ToUpper() %>
                                                </span>
                                            </td>
                                            <td class="text-end">
                                                <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-success);">
                                                    +<%# Eval("Points") %>
                                                </span>
                                            </td>
                                            <td class="text-end"
                                                style="color:var(--ca-text-muted);font-size:0.8rem;white-space:nowrap;">
                                                <%# ((DateTime)Eval("SolvedAt")).ToString("HH:mm · dd MMM") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>

                <%-- Empty state --%>
                <asp:Panel ID="pnlSubmissionsEmpty" runat="server" Visible="false"
                           CssClass="ca-empty-state">
                    <i class="fas fa-flag" aria-hidden="true"></i>
                    <p>No submissions yet. Start solving challenges!</p>
                </asp:Panel>
            </div>

            <%-- Category Progress --%>
            <div class="ca-card">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">Category Progress</h3>
                </div>
                <div class="row g-3">
                    <asp:Repeater ID="rptCategoryProgress" runat="server">
                        <ItemTemplate>
                            <div class="col-sm-6">
                                <div style="background:var(--ca-bg-base);border-radius:var(--ca-radius);padding:1rem;">
                                    <div class="d-flex align-items-center justify-content-between mb-2">
                                        <div class="d-flex align-items-center gap-2">
                                            <span style="width:28px;height:28px;border-radius:var(--ca-radius-sm);background:var(--ca-accent-light);color:var(--ca-accent);display:flex;align-items:center;justify-content:center;font-size:0.8rem;"
                                                  aria-hidden="true">
                                                <i class="fas <%# Eval("Icon") %>"></i>
                                            </span>
                                            <span style="font-size:0.875rem;font-weight:600;color:var(--ca-text-primary);">
                                                <%# Eval("Name") %>
                                            </span>
                                        </div>
                                        <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                                            <%# Eval("Solved") %>/<%# Eval("Total") %>
                                        </span>
                                    </div>
                                    <div class="ca-progress">
                                        <div class="ca-progress-bar"
                                             data-width="<%# Eval("Percent") %>">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div><%-- /.col-lg-8 --%>

        <%-- ── Right Column (4 cols) ───────────────────────── --%>
        <div class="col-lg-4">

            <%-- Active Competitions --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">Active Competitions</h3>
                    <span class="ca-nav-badge" style="position:static;">
                        <asp:Literal ID="litCompCount" runat="server" />
                    </span>
                </div>

                <asp:Panel ID="pnlCompetitions" runat="server">
                    <div class="d-flex flex-column gap-3">
                        <asp:Repeater ID="rptCompetitions" runat="server">
                            <ItemTemplate>
                                <div class="ca-comp-card">
                                    <div class="ca-comp-card-header">
                                        <div>
                                            <div class="ca-comp-name"><%# Eval("Name") %></div>
                                            <div class="ca-comp-meta">
                                                <i class="fas fa-clock" aria-hidden="true"></i>
                                                <%# Eval("TimeRemaining") %> remaining
                                            </div>
                                        </div>
                                        <span class="ca-comp-status <%# Eval("Status") %>">
                                            <%# Eval("Status").ToString().ToUpper() %>
                                        </span>
                                    </div>

                                    <div class="d-flex justify-content-between mb-2">
                                        <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                                            Rank: <strong style="color:var(--ca-text-primary);">
                                                #<%# Eval("TeamRank") %> / <%# Eval("TotalTeams") %>
                                            </strong>
                                        </span>
                                        <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                                            Score: <strong style="color:var(--ca-accent);">
                                                <%# string.Format("{0:N0}", Eval("Score")) %>
                                            </strong>
                                        </span>
                                    </div>

                                    <div class="d-flex justify-content-between mb-1">
                                        <span style="font-size:0.75rem;color:var(--ca-text-muted);">Flags captured</span>
                                        <span style="font-size:0.75rem;color:var(--ca-text-muted);">
                                            <%# Eval("ChallengesSolved") %>/<%# Eval("TotalChallenges") %>
                                        </span>
                                    </div>
                                    <div class="ca-progress">
                                        <div class="ca-progress-bar"
                                             data-width="<%# Eval("ProgressPercent") %>">
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlCompetitionsEmpty" runat="server" Visible="false"
                           CssClass="ca-empty-state">
                    <i class="fas fa-bolt" aria-hidden="true"></i>
                    <p>No active competitions. Check the Competitions page.</p>
                </asp:Panel>
            </div>

            <%-- Recent Activity --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">Recent Activity</h3>
                </div>

                <asp:Panel ID="pnlActivity" runat="server">
                    <ul class="ca-activity-list" role="list" aria-label="Recent activity">
                        <asp:Repeater ID="rptActivity" runat="server">
                            <ItemTemplate>
                                <li class="ca-activity-item">
                                    <div class="ca-activity-icon"
                                         style="background:var(--ca-<%# Eval("IconColorClass") %>-light, var(--ca-accent-light));color:var(--ca-<%# Eval("IconColorClass") %>, var(--ca-accent));"
                                         aria-hidden="true">
                                        <i class="fas <%# Eval("Icon") %>"></i>
                                    </div>
                                    <div class="ca-activity-body">
                                        <div class="ca-activity-text"><%# Eval("Text") %></div>
                                        <div class="ca-activity-time">
                                            <i class="fas fa-clock me-1" aria-hidden="true"></i><%# Eval("TimeAgo") %>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </asp:Panel>

                <asp:Panel ID="pnlActivityEmpty" runat="server" Visible="false"
                           CssClass="ca-empty-state">
                    <i class="fas fa-clock" aria-hidden="true"></i>
                    <p>No recent activity.</p>
                </asp:Panel>
            </div>

            <%-- Quick Actions --%>
            <div class="ca-card">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">Quick Actions</h3>
                </div>
                <div class="ca-quick-actions">
                    <a href="<%= ResolveUrl("~/Challenges/Challenges.aspx") %>"
                       class="ca-quick-btn" id="qa-challenges">
                        <i class="fas fa-flag" aria-hidden="true"></i>
                        Solve Challenge
                    </a>
                    <a href="<%= ResolveUrl("~/Leaderboard/Leaderboard.aspx") %>"
                       class="ca-quick-btn" id="qa-leaderboard">
                        <i class="fas fa-list-ol" aria-hidden="true"></i>
                        Leaderboard
                    </a>
                    <a href="<%= ResolveUrl("~/Teams/Teams.aspx") %>"
                       class="ca-quick-btn" id="qa-team">
                        <i class="fas fa-users" aria-hidden="true"></i>
                        My Team
                    </a>
                    <a href="<%= ResolveUrl("~/Account/Profile.aspx") %>"
                       class="ca-quick-btn" id="qa-profile">
                        <i class="fas fa-circle-user" aria-hidden="true"></i>
                        Edit Profile
                    </a>
                </div>
            </div>

        </div><%-- /.col-lg-4 --%>
    </div><%-- /.row main grid --%>

</div><%-- /.stagger-children --%>

</asp:Content>

<%-- ─── Page-specific scripts ──────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
// ── Animate score counter on the stat cards ───────────────────
document.querySelectorAll('[data-count]').forEach(function (el) {
    var target = parseInt(el.getAttribute('data-count'), 10);
    if (isNaN(target)) return;
    var start = 0;
    var dur   = 900;
    var step  = Math.ceil(target / (dur / 16));
    var timer = setInterval(function () {
        start += step;
        if (start >= target) { start = target; clearInterval(timer); }
        el.querySelector('.ca-stat-value-text') ?
            (el.querySelector('.ca-stat-value-text').textContent = start.toLocaleString()) :
            null;
    }, 16);
});

// ── Animate bar chart (data-height → CSS height via JS) ──────
document.querySelectorAll('.ca-chart-bar[data-height]').forEach(function (bar) {
    var h     = parseInt(bar.getAttribute('data-height'), 10);
    var delay = parseInt(bar.getAttribute('data-delay') || '0', 10);
    bar.style.height = '0%';
    setTimeout(function () {
        bar.style.transition = 'height 0.6s ease';
        bar.style.height = h + '%';
    }, delay + 100);
});

// ── Animate progress bars (data-width → CSS width via JS) ────
document.querySelectorAll('.ca-progress-bar[data-width]').forEach(function (bar) {
    var w = parseInt(bar.getAttribute('data-width'), 10);
    bar.style.width = '0%';
    setTimeout(function () {
        bar.style.transition = 'width 0.8s ease';
        bar.style.width = w + '%';
    }, 200);
});
</script>
</asp:Content>
