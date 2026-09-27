<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.ReportsPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Reports</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ─── Header ────────────────────────────────────────────── --%>
    <div class="adm-header">
        <div>
            <h2 class="adm-title">
                <i class="fas fa-chart-bar" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Reports
            </h2>
            <p class="adm-subtitle">Platform statistics and performance analytics</p>
        </div>
        <a href="<%= ResolveUrl("~/Admin/Admin.aspx") %>" class="btn btn-secondary btn-sm" id="btn-back-admin">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Admin Dashboard
        </a>
    </div>

    <%-- ─── Top Stat Cards ────────────────────────────────────── --%>
    <div class="row g-3 mb-3">
        <div class="col-6 col-md-3">
            <div class="report-stat-card accent">
                <div class="report-stat-val" data-count="<%= VM.TotalSubmissions %>"><%= VM.TotalSubmissions %></div>
                <div class="report-stat-label">Total Submissions</div>
                <div class="report-stat-sub">All time</div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="report-stat-card success">
                <div class="report-stat-val" data-count="<%= VM.CorrectSubmissions %>"><%= VM.CorrectSubmissions %></div>
                <div class="report-stat-label">Correct Flags</div>
                <div class="report-stat-sub"><%= VM.CorrectRate %>% success rate</div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="report-stat-card warning">
                <div class="report-stat-val" data-count="<%= VM.UniqueUsers %>"><%= VM.UniqueUsers %></div>
                <div class="report-stat-label">Active Users</div>
                <div class="report-stat-sub">Submitted at least once</div>
            </div>
        </div>
        <div class="col-6 col-md-3">
            <div class="report-stat-card info">
                <div class="report-stat-val"><%= VM.AvgSolveTime.ToString("F1") %>h</div>
                <div class="report-stat-label">Avg Solve Time</div>
                <div class="report-stat-sub">Per challenge</div>
            </div>
        </div>
    </div>

    <%-- ─── Charts Row 1 ───────────────────────────────────────── --%>
    <div class="row g-3 mb-3">
        <div class="col-12 col-lg-8">
            <div class="report-chart-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Submission Volume (7 days)</h3>
                        <div class="ca-card-subtitle">Total flag submissions per day</div>
                    </div>
                </div>
                <div class="report-chart-wrap">
                    <canvas id="subVolumeChart" aria-label="Submission volume chart"></canvas>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-4">
            <div class="report-chart-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Solves by Category</h3>
                        <div class="ca-card-subtitle">Distribution of solved challenges</div>
                    </div>
                </div>
                <div class="report-chart-wrap">
                    <canvas id="catSolvesChart" aria-label="Category solves chart"></canvas>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Charts Row 2 ───────────────────────────────────────── --%>
    <div class="row g-3 mb-3">
        <div class="col-12 col-lg-6">
            <div class="report-chart-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Team Scores</h3>
                        <div class="ca-card-subtitle">Top 6 team scores comparison</div>
                    </div>
                </div>
                <div class="report-chart-wrap">
                    <canvas id="teamScoresChart" aria-label="Team scores chart"></canvas>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-6">
            <div class="report-chart-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Daily Active Users</h3>
                        <div class="ca-card-subtitle">Unique users who submitted (7 days)</div>
                    </div>
                </div>
                <div class="report-chart-wrap">
                    <canvas id="userActiveChart" aria-label="Daily active users chart"></canvas>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Most Solved Table ───────────────────────────────────── --%>
    <div class="row g-3">
        <div class="col-12 col-lg-6">
            <div class="adm-table-card">
                <div class="adm-table-toolbar">
                    <span style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                        <i class="fas fa-arrow-up me-2" style="color:var(--ca-success);" aria-hidden="true"></i>
                        Most Solved Challenges
                    </span>
                </div>
                <div class="table-responsive">
                    <table class="table mb-0" aria-label="Most solved challenges">
                        <thead>
                            <tr>
                                <th>Challenge</th>
                                <th class="text-center">Solvers</th>
                                <th class="text-center">Rate</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptMostSolved" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div style="font-weight:600;font-size:0.82rem;"><%# Eval("Title") %></div>
                                            <div class="d-flex align-items-center gap-1 mt-1">
                                                <span class="ch-diff <%# Eval("DifficultyLower") %>" style="font-size:0.62rem;"><%# Eval("Difficulty") %></span>
                                                <span style="font-size:0.7rem;color:var(--ca-text-muted);"><%# Eval("Category") %></span>
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);">
                                                <%# Eval("SolverCount") %>
                                            </span>
                                        </td>
                                        <td class="text-center">
                                            <span style="font-size:0.8rem;color:var(--ca-success);"><%# Eval("SolveRate") %>%</span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-6">
            <div class="adm-table-card">
                <div class="adm-table-toolbar">
                    <span style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                        <i class="fas fa-arrow-down me-2" style="color:var(--ca-danger);" aria-hidden="true"></i>
                        Least Solved Challenges
                    </span>
                </div>
                <div class="table-responsive">
                    <table class="table mb-0" aria-label="Least solved challenges">
                        <thead>
                            <tr>
                                <th>Challenge</th>
                                <th class="text-center">Solvers</th>
                                <th class="text-center">Rate</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptLeastSolved" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div style="font-weight:600;font-size:0.82rem;"><%# Eval("Title") %></div>
                                            <div class="d-flex align-items-center gap-1 mt-1">
                                                <span class="ch-diff <%# Eval("DifficultyLower") %>" style="font-size:0.62rem;"><%# Eval("Difficulty") %></span>
                                                <span style="font-size:0.7rem;color:var(--ca-text-muted);"><%# Eval("Category") %></span>
                                            </div>
                                        </td>
                                        <td class="text-center">
                                            <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-danger);">
                                                <%# Eval("SolverCount") %>
                                            </span>
                                        </td>
                                        <td class="text-center">
                                            <span style="font-size:0.8rem;color:var(--ca-danger);"><%# Eval("SolveRate") %>%</span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Team Performance Table ──────────────────────────────── --%>
    <div class="adm-table-card mt-3">
        <div class="adm-table-toolbar">
            <span style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                <i class="fas fa-users me-2" style="color:var(--ca-info);" aria-hidden="true"></i>
                Team Performance
            </span>
        </div>
        <div class="table-responsive">
            <table class="table mb-0" aria-label="Team performance">
                <thead>
                    <tr>
                        <th style="width:60px;">Rank</th>
                        <th>Team</th>
                        <th class="text-center">Members</th>
                        <th class="text-center">Score</th>
                        <th class="text-center">Solved</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTeamPerformance" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><span class="ca-rank-badge <%# Eval("RankBadgeClass") %>"><%# Eval("Rank") %></span></td>
                                <td>
                                    <a href="<%= ResolveUrl("~/Teams/TeamDetails.aspx") %>?id=<%# Eval("Rank") %>"
                                       style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                                        <%# Eval("Name") %>
                                    </a>
                                </td>
                                <td class="text-center" style="color:var(--ca-text-muted);font-size:0.875rem;"><%# Eval("Members") %></td>
                                <td class="text-center">
                                    <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);">
                                        <%# Convert.ToInt32(Eval("Score")).ToString("N0") %>
                                    </span>
                                </td>
                                <td class="text-center" style="color:var(--ca-text-secondary);font-size:0.875rem;"><%# Eval("Solved") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
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

    // Submission Volume — Line
    var subCtx = document.getElementById('subVolumeChart');
    if (subCtx) {
        new Chart(subCtx.getContext('2d'), {
            type: 'line',
            data: {
                labels: <%= SubLabelsJson %>,
                datasets: [{
                    label: 'Submissions',
                    data: <%= SubDataJson %>,
                    borderColor: '#3B82F6',
                    backgroundColor: 'rgba(59,130,246,0.15)',
                    borderWidth: 2,
                    pointRadius: 4,
                    tension: 0.4,
                    fill: true
                }]
            },
            options: {
                responsive: true, maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    x: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } } },
                    y: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } }, beginAtZero: true }
                }
            }
        });
    }

    // Category Solves — Horizontal Bar
    var catCtx = document.getElementById('catSolvesChart');
    if (catCtx) {
        new Chart(catCtx.getContext('2d'), {
            type: 'bar',
            data: {
                labels: <%= CatLabelsJson %>,
                datasets: [{
                    label: 'Solves',
                    data: <%= CatSolvesJson %>,
                    backgroundColor: [
                        'rgba(59,130,246,0.75)','rgba(16,185,129,0.75)','rgba(245,158,11,0.75)',
                        'rgba(239,68,68,0.75)','rgba(139,92,246,0.75)','rgba(20,184,166,0.75)',
                        'rgba(248,113,113,0.75)','rgba(167,139,250,0.75)'
                    ],
                    borderRadius: 4,
                    borderWidth: 0
                }]
            },
            options: {
                responsive: true, maintainAspectRatio: false,
                indexAxis: 'y',
                plugins: { legend: { display: false } },
                scales: {
                    x: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 10 } }, beginAtZero: true },
                    y: { grid: { display: false }, ticks: { font: { size: 10 } } }
                }
            }
        });
    }

    // Team Scores — Bar
    var teamCtx = document.getElementById('teamScoresChart');
    if (teamCtx) {
        new Chart(teamCtx.getContext('2d'), {
            type: 'bar',
            data: {
                labels: <%= TeamLabelsJson %>,
                datasets: [{
                    label: 'Score',
                    data: <%= TeamScoresJson %>,
                    backgroundColor: 'rgba(59,130,246,0.75)',
                    borderRadius: 6,
                    borderWidth: 0
                }]
            },
            options: {
                responsive: true, maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    x: { grid: { display: false }, ticks: { font: { size: 11 } } },
                    y: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } }, beginAtZero: true }
                }
            }
        });
    }

    // Daily Active Users — Line
    var userCtx = document.getElementById('userActiveChart');
    if (userCtx) {
        new Chart(userCtx.getContext('2d'), {
            type: 'line',
            data: {
                labels: <%= UserLabelsJson %>,
                datasets: [{
                    label: 'Active Users',
                    data: <%= UserActiveJson %>,
                    borderColor: '#8B5CF6',
                    backgroundColor: 'rgba(139,92,246,0.12)',
                    borderWidth: 2,
                    pointRadius: 4,
                    tension: 0.35,
                    fill: true
                }]
            },
            options: {
                responsive: true, maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    x: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } } },
                    y: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { font: { size: 11 } }, beginAtZero: true }
                }
            }
        });
    }
}());
</script>
</asp:Content>
