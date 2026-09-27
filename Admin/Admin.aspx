<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.AdminPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Admin Dashboard</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ─── Page Header ──────────────────────────────────────── --%>
    <div class="adm-header">
        <div>
            <h2 class="adm-title">
                <i class="fas fa-gauge-high" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Admin Dashboard
            </h2>
            <p class="adm-subtitle">Platform overview &amp; real-time activity</p>
        </div>
        <div class="d-flex gap-2 flex-wrap">
            <a href="<%= ResolveUrl("~/Admin/Reports.aspx") %>" class="btn btn-outline-primary btn-sm" id="btn-goto-reports">
                <i class="fas fa-chart-bar me-1" aria-hidden="true"></i>Reports
            </a>
            <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" class="btn btn-secondary btn-sm" id="btn-goto-challenges">
                <i class="fas fa-flag me-1" aria-hidden="true"></i>Challenges
            </a>
            <a href="<%= ResolveUrl("~/Admin/Announcements.aspx") %>" class="btn btn-secondary btn-sm" id="btn-goto-ann">
                <i class="fas fa-bullhorn me-1" aria-hidden="true"></i>Announcements
            </a>
        </div>
    </div>

    <%-- ─── Stat Cards ────────────────────────────────────────── --%>
    <div class="row g-3 mb-3">
        <div class="col-6 col-md-3">
            <div class="ca-stat-card accent" aria-label="Total users">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Total Users</div>
                    <div class="ca-stat-value" data-count="<%= VM.TotalUsers %>"><%= VM.TotalUsers %></div>
                    <div class="ca-stat-delta up">
                        <i class="fas fa-arrow-up" aria-hidden="true"></i>
                        +<%= VM.NewUsersToday %> today
                    </div>
                </div>
                <div class="ca-stat-icon">
                    <i class="fas fa-users" aria-hidden="true"></i>
                </div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="ca-stat-card success" aria-label="Total challenges">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Challenges</div>
                    <div class="ca-stat-value" data-count="<%= VM.TotalChallenges %>"><%= VM.TotalChallenges %></div>
                    <div class="ca-stat-delta neutral">
                        <i class="fas fa-flag" aria-hidden="true"></i>
                        Across 8 categories
                    </div>
                </div>
                <div class="ca-stat-icon">
                    <i class="fas fa-flag" aria-hidden="true"></i>
                </div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="ca-stat-card warning" aria-label="Active competitions">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Active Comps</div>
                    <div class="ca-stat-value" data-count="<%= VM.ActiveCompetitions %>"><%= VM.ActiveCompetitions %></div>
                    <div class="ca-stat-delta up">
                        <i class="fas fa-bolt" aria-hidden="true"></i>
                        Running now
                    </div>
                </div>
                <div class="ca-stat-icon">
                    <i class="fas fa-bolt" aria-hidden="true"></i>
                </div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="ca-stat-card info" aria-label="Total submissions">
                <div class="ca-stat-info">
                    <div class="ca-stat-label">Submissions</div>
                    <div class="ca-stat-value" data-count="<%= VM.TotalSubmissions %>"><%= VM.TotalSubmissions %></div>
                    <div class="ca-stat-delta up">
                        <i class="fas fa-arrow-up" aria-hidden="true"></i>
                        +<%= VM.SubmissionsToday %> today
                    </div>
                </div>
                <div class="ca-stat-icon">
                    <i class="fas fa-paper-plane" aria-hidden="true"></i>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Charts Row ─────────────────────────────────────────── --%>
    <div class="row g-3 mb-3">
        <div class="col-12 col-lg-8">
            <div class="ca-card h-100">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">
                            <i class="fas fa-chart-line me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                            Submissions (7 days)
                        </h3>
                        <div class="ca-card-subtitle">Correct vs. incorrect flag submissions</div>
                    </div>
                    <a href="<%= ResolveUrl("~/Admin/Reports.aspx") %>" class="btn btn-secondary btn-sm">View Report</a>
                </div>
                <div class="adm-chart-wrap">
                    <canvas id="submissionsChart" aria-label="Daily submissions chart"></canvas>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-4">
            <div class="ca-card h-100">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">
                            <i class="fas fa-chart-pie me-2" style="color:var(--ca-info);" aria-hidden="true"></i>
                            By Category
                        </h3>
                        <div class="ca-card-subtitle">Challenge distribution</div>
                    </div>
                </div>
                <div class="adm-chart-wrap">
                    <canvas id="categoryChart" aria-label="Challenge category distribution chart"></canvas>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Recent Submissions + Activity ─────────────────────── --%>
    <div class="row g-3">
        <div class="col-12 col-lg-7">
            <div class="adm-table-card">
                <div class="adm-table-toolbar">
                    <span style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                        <i class="fas fa-paper-plane me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                        Recent Submissions
                    </span>
                    <a href="<%= ResolveUrl("~/Admin/Reports.aspx") %>" class="btn btn-secondary btn-sm ms-auto">See All</a>
                </div>
                <div class="table-responsive">
                    <table class="table mb-0" aria-label="Recent submissions">
                        <thead>
                            <tr>
                                <th>User</th>
                                <th>Challenge</th>
                                <th class="text-center">Points</th>
                                <th class="text-center">Result</th>
                                <th>Time</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptRecentSubmissions" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <span style="font-weight:600;font-size:0.82rem;"><%# Eval("Username") %></span>
                                        </td>
                                        <td>
                                            <div style="font-size:0.82rem;color:var(--ca-text-primary);"><%# Eval("ChallengeName") %></div>
                                            <div style="font-size:0.72rem;color:var(--ca-text-muted);"><%# Eval("Category") %></div>
                                        </td>
                                        <td class="text-center">
                                            <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);">
                                                <%# Eval("Points") %>
                                            </span>
                                        </td>
                                        <td class="text-center">
                                            <%# (bool)Eval("IsCorrect") 
                                                ? "<span class=\"adm-status correct\">Correct</span>"
                                                : "<span class=\"adm-status wrong\">Wrong</span>" %>
                                        </td>
                                        <td style="font-size:0.75rem;color:var(--ca-text-muted);"><%# Eval("TimeAgo") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="col-12 col-lg-5">
            <div class="ca-card h-100">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">
                        <i class="fas fa-bolt me-2" style="color:var(--ca-warning);" aria-hidden="true"></i>
                        User Activity
                    </h3>
                </div>
                <ul class="ca-activity-list">
                    <asp:Repeater ID="rptUserActivity" runat="server">
                        <ItemTemplate>
                            <li class="ca-activity-item">
                                <div class="ca-activity-icon"
                                     style="background:var(--ca-<%# Eval("IconColorClass") %>-light);color:var(--ca-<%# Eval("IconColorClass") %>);"
                                     aria-hidden="true">
                                    <i class="fas <%# Eval("IconClass") %>"></i>
                                </div>
                                <div class="ca-activity-body">
                                    <div class="ca-activity-text">
                                        <strong><%# Eval("Username") %></strong>
                                        <%# Eval("Action").ToString() == "solved" ? " solved " : 
                                            Eval("Action").ToString() == "registered" ? " created an account" : 
                                            Eval("Action").ToString() == "joined_team" ? " joined team " : 
                                            Eval("Action").ToString() == "joined_comp" ? " joined competition " : 
                                            " performed an action" %>
                                        
                                        <%# Eval("Action").ToString() != "registered" 
                                            ? "<em style=\"color:var(--ca-text-primary);\">" + Eval("Detail") + "</em>" 
                                            : "" %>
                                    </div>
                                    <div class="ca-activity-time"><%# Eval("TimeAgo") %></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>

    <%-- ─── Quick Links ────────────────────────────────────────── --%>
    <div class="ca-card mt-3">
        <h3 class="ca-card-title mb-3">Quick Admin Actions</h3>
        <div class="ca-quick-actions" style="grid-template-columns:repeat(auto-fit,minmax(130px,1fr));">
            <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>" class="ca-quick-btn" id="qb-new-challenge">
                <i class="fas fa-plus-circle" aria-hidden="true"></i>New Challenge
            </a>
            <a href="<%= ResolveUrl("~/Admin/AnnouncementForm.aspx") %>" class="ca-quick-btn" id="qb-new-ann">
                <i class="fas fa-bullhorn" aria-hidden="true"></i>Announcement
            </a>
            <a href="<%= ResolveUrl("~/Admin/Reports.aspx") %>" class="ca-quick-btn" id="qb-reports">
                <i class="fas fa-chart-bar" aria-hidden="true"></i>Reports
            </a>
            <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" class="ca-quick-btn" id="qb-manage-ch">
                <i class="fas fa-shield-halved" aria-hidden="true"></i>Manage Challenges
            </a>
        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
<script>
(function () {
    Chart.defaults.color = '#64748B';
    Chart.defaults.borderColor = 'rgba(255,255,255,0.06)';

    // ── Submissions Line Chart ──────────────────────────────
    var subCtx = document.getElementById('submissionsChart');
    if (subCtx) {
        new Chart(subCtx.getContext('2d'), {
            type: 'line',
            data: {
                labels: <%= SubLabelsJson %>,
                datasets: [
                    {
                        label: 'Correct',
                        data: <%= SubCorrectJson %>,
                        borderColor: '#10B981',
                        backgroundColor: 'rgba(16,185,129,0.1)',
                        borderWidth: 2,
                        pointRadius: 3,
                        tension: 0.35,
                        fill: true
                    },
                    {
                        label: 'Wrong',
                        data: <%= SubWrongJson %>,
                        borderColor: '#EF4444',
                        backgroundColor: 'rgba(239,68,68,0.08)',
                        borderWidth: 2,
                        pointRadius: 3,
                        tension: 0.35,
                        fill: true
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: 'bottom', labels: { usePointStyle: true, padding: 14 } }
                },
                scales: {
                    x: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } } },
                    y: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } }, beginAtZero: true }
                }
            }
        });
    }

    // ── Category Doughnut Chart ─────────────────────────────
    var catCtx = document.getElementById('categoryChart');
    if (catCtx) {
        new Chart(catCtx.getContext('2d'), {
            type: 'doughnut',
            data: {
                labels: <%= CatLabelsJson %>,
                datasets: [{
                    data: <%= CatCountsJson %>,
                    backgroundColor: [
                        'rgba(59,130,246,0.8)','rgba(16,185,129,0.8)','rgba(245,158,11,0.8)',
                        'rgba(239,68,68,0.8)','rgba(139,92,246,0.8)','rgba(20,184,166,0.8)',
                        'rgba(248,113,113,0.8)','rgba(167,139,250,0.8)'
                    ],
                    borderWidth: 0,
                    hoverOffset: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: { usePointStyle: true, padding: 8, font: { size: 10 } }
                    }
                },
                cutout: '65%'
            }
        });
    }
}());
</script>
</asp:Content>
