<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompetitionDetails.aspx.cs"
         Inherits="CyberArenaWebForms.Competitions.CompetitionDetailsPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- Flash message --%>
    <asp:Panel ID="pnlFlash" runat="server" Visible="false">
        <div class="alert ca-auto-dismiss alert-success d-flex align-items-center gap-2 mb-3"
             role="alert" id="flash-alert">
            <i class="fas fa-fw fa-circle-check" id="flash-icon" aria-hidden="true"></i>
            <asp:Literal ID="litFlashMsg" runat="server" />
        </div>
    </asp:Panel>

    <div class="mb-3">
        <a href="<%= ResolveUrl("~/Competitions/Competitions.aspx") %>" class="btn btn-secondary btn-sm" id="btn-back-comps">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>All Competitions
        </a>
    </div>

    <%-- ─── Competition Hero ──────────────────────────────────── --%>
    <div class="comp-hero mb-3">
        <div class="d-flex align-items-flex-start gap-3 flex-wrap mb-3">
            <div class="comp-icon" id="heroIcon" runat="server" style="width:56px;height:56px;font-size:1.3rem;flex-shrink:0;" aria-hidden="true">
                <i class="fas fa-bolt"></i>
            </div>
            <div style="flex:1;min-width:0;">
                <div class="d-flex align-items-center gap-2 flex-wrap mb-1">
                    <h2 style="font-family:'Space Grotesk',sans-serif;font-size:1.5rem;font-weight:700;color:var(--ca-text-primary);margin:0;">
                        <asp:Literal ID="litCompName" runat="server" />
                    </h2>
                    <span class="ca-comp-status" id="spanStatus" runat="server">
                        <asp:Literal ID="litStatusUpper" runat="server" />
                    </span>
                    
                    <asp:Panel ID="pnlJoinedBadge" runat="server" Visible="false" CssClass="badge bg-success">
                        Joined
                    </asp:Panel>
                </div>
                <p style="font-size:0.875rem;color:var(--ca-text-muted);margin:0;">
                    <asp:Literal ID="litDescription" runat="server" />
                </p>
            </div>

            <%-- Join / Register buttons --%>
            <asp:Panel ID="pnlJoinAction" runat="server" Visible="false" CssClass="flex-shrink-0">
                <asp:Button ID="btnJoinActive" runat="server" CssClass="btn btn-primary" Text="Join Competition" 
                            OnClick="btnJoin_Click" Visible="false" />
                
                <asp:Button ID="btnRegisterUpcoming" runat="server" CssClass="btn btn-outline-primary" Text="Register" 
                            OnClick="btnJoin_Click" Visible="false" />
            </asp:Panel>
        </div>

        <%-- Stat Row --%>
        <div class="row g-2 mb-3">
            <div class="col-6 col-md-3">
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val"><asp:Literal ID="litChallengeCount" runat="server" /></span>
                    <span class="comp-detail-stat-label">Challenges</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val"><asp:Literal ID="litParticipantCount" runat="server" /></span>
                    <span class="comp-detail-stat-label">Participants</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val"><asp:Literal ID="litFormat" runat="server" /></span>
                    <span class="comp-detail-stat-label">Format</span>
                </div>
            </div>
            <div class="col-6 col-md-3">
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val" style="font-size:1rem;">
                        <asp:Literal ID="litTimeDisplay" runat="server" />
                    </span>
                    <span class="comp-detail-stat-label">
                        <asp:Literal ID="litTimeLabel" runat="server" />
                    </span>
                </div>
            </div>
        </div>

        <%-- Schedule --%>
        <div class="d-flex gap-4 flex-wrap" style="font-size:0.8rem;color:var(--ca-text-muted);">
            <span>
                <i class="fas fa-play me-1" style="color:var(--ca-success);" aria-hidden="true"></i>
                Start: <strong style="color:var(--ca-text-primary);"><asp:Literal ID="litStartAt" runat="server" /> UTC</strong>
            </span>
            <span>
                <i class="fas fa-stop me-1" style="color:var(--ca-danger);" aria-hidden="true"></i>
                End: <strong style="color:var(--ca-text-primary);"><asp:Literal ID="litEndAt" runat="server" /> UTC</strong>
            </span>
        </div>
    </div>

    <%-- ─── Tabs ──────────────────────────────────────────────── --%>
    <div class="comp-tabs" role="tablist" aria-label="Competition sections">
        <button type="button" class="comp-tab-btn active" id="tab-overview" onclick="switchTab('overview')"
                role="tab" aria-selected="true" aria-controls="panel-overview">
            <i class="fas fa-info-circle" aria-hidden="true"></i>Overview
        </button>
        <button type="button" class="comp-tab-btn" id="tab-challenges" onclick="switchTab('challenges')"
                role="tab" aria-selected="false" aria-controls="panel-challenges">
            <i class="fas fa-flag" aria-hidden="true"></i>
            Challenges
            <span class="ca-nav-badge" style="margin-left:0.35rem;"><asp:Literal ID="litTabChallengeCount" runat="server" /></span>
        </button>
        <button type="button" class="comp-tab-btn" id="tab-leaderboard" onclick="switchTab('leaderboard')"
                role="tab" aria-selected="false" aria-controls="panel-leaderboard">
            <i class="fas fa-trophy" aria-hidden="true"></i>Leaderboard
        </button>
        <button type="button" class="comp-tab-btn" id="tab-rules" onclick="switchTab('rules')"
                role="tab" aria-selected="false" aria-controls="panel-rules">
            <i class="fas fa-scroll" aria-hidden="true"></i>Rules
        </button>
    </div>

    <%-- ─── Overview Panel ─────────────────────────────────────── --%>
    <div class="comp-panel active" id="panel-overview" role="tabpanel" aria-labelledby="tab-overview">
        <div class="row g-3">
            <div class="col-12 col-md-6">
                <div class="ca-card h-100">
                    <div class="ca-card-header">
                        <h3 class="ca-card-title">About</h3>
                    </div>
                    <p style="font-size:0.875rem;color:var(--ca-text-secondary);line-height:1.8;margin:0;">
                        <asp:Literal ID="litAboutDesc" runat="server" />
                    </p>
                    
                    <asp:Panel ID="pnlCapacity" runat="server" Visible="false" CssClass="mt-3">
                        <div class="d-flex justify-content-between mb-1" style="font-size:0.78rem;color:var(--ca-text-muted);">
                            <span>Capacity</span>
                            <span><asp:Literal ID="litCapacityLabel" runat="server" /></span>
                        </div>
                        <div class="ca-progress">
                            <div class="ca-progress-bar" id="capacityBar" runat="server" style="width:0;"></div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            
            <div class="col-12 col-md-6">
                <div class="ca-card h-100">
                    <div class="ca-card-header">
                        <h3 class="ca-card-title">Category Breakdown</h3>
                    </div>
                    <asp:Repeater ID="rptCategories" runat="server">
                        <ItemTemplate>
                            <div class="d-flex align-items-center gap-2 mb-2">
                                <div class="ca-nav-icon" style="width:16px;" aria-hidden="true">
                                    <i class="fas <%# CyberArenaWebForms.Models.ChallengeCategories.Icon(Eval("Cat").ToString()) %>" style="font-size:0.8rem;"></i>
                                </div>
                                <span style="font-size:0.8rem;color:var(--ca-text-secondary);min-width:120px;"><%# Eval("Cat") %></span>
                                <div class="ca-progress flex-1">
                                    <div class="ca-progress-bar" data-width="<%# Eval("Pct") %>" style="width:<%# Eval("Pct") %>%;"></div>
                                </div>
                                <span style="font-size:0.75rem;color:var(--ca-text-muted);min-width:20px;text-align:right;"><%# Eval("Count") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Challenges Panel ────────────────────────────────────── --%>
    <div class="comp-panel" id="panel-challenges" role="tabpanel" aria-labelledby="tab-challenges">
        <div class="adm-table-card">
            <div class="table-responsive">
                <table class="table mb-0" aria-label="Competition challenges">
                    <thead>
                        <tr>
                            <th>Challenge</th>
                            <th>Category</th>
                            <th>Difficulty</th>
                            <th class="text-center">Points</th>
                            <th class="text-center">Solvers</th>
                            <th class="text-center">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptChallenges" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <div class="d-flex align-items-center gap-2">
                                            <div class="ch-card-icon <%# Eval("CategoryCssClass") %>"
                                                 style="width:28px;height:28px;font-size:0.75rem;flex-shrink:0;"
                                                 aria-hidden="true">
                                                <i class="fas <%# Eval("CategoryIcon") %>"></i>
                                            </div>
                                            <span style="font-weight:600;font-size:0.875rem;"><%# Eval("Title") %></span>
                                        </div>
                                    </td>
                                    <td style="font-size:0.82rem;color:var(--ca-text-muted);"><%# Eval("Category") %></td>
                                    <td><span class="ch-diff <%# Eval("DifficultyLower") %>"><%# Eval("Difficulty") %></span></td>
                                    <td class="text-center">
                                        <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);"><%# Eval("Points") %></span>
                                    </td>
                                    <td class="text-center" style="color:var(--ca-text-muted);font-size:0.875rem;"><%# Eval("SolverCount") %></td>
                                    <td class="text-center">
                                        <%# (bool)Eval("IsSolved") 
                                            ? "<span class=\"badge bg-success\"><i class=\"fas fa-check me-1\" aria-hidden=\"true\"></i>Solved</span>"
                                            : "<span style=\"font-size:0.75rem;color:var(--ca-text-muted);\">—</span>" %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <%-- ─── Leaderboard Panel ───────────────────────────────────── --%>
    <div class="comp-panel" id="panel-leaderboard" role="tabpanel" aria-labelledby="tab-leaderboard">
        <div class="adm-table-card">
            <div class="table-responsive">
                <table class="table mb-0" aria-label="Competition leaderboard">
                    <thead>
                        <tr>
                            <th style="width:60px;">Rank</th>
                            <th>Player</th>
                            <th>Team</th>
                            <th class="text-center">Score</th>
                            <th class="text-center">Solved</th>
                            <th>Last Solve</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptLeaderboard" runat="server">
                            <ItemTemplate>
                                <tr class="<%# (bool)Eval("IsCurrentUser") ? "lb-current-user" : "" %>">
                                    <td><span class="ca-rank-badge <%# Eval("RankBadgeClass") %>"><%# Eval("Rank") %></span></td>
                                    <td>
                                        <span style="font-weight:600;font-size:0.875rem;">
                                            <%# Eval("Username") %>
                                            <%# (bool)Eval("IsCurrentUser") ? "<span class=\"badge bg-primary ms-1\">You</span>" : "" %>
                                        </span>
                                    </td>
                                    <td style="font-size:0.82rem;color:var(--ca-text-muted);"><%# Eval("TeamName") %></td>
                                    <td class="text-center">
                                        <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);"><%# Eval("Score") %></span>
                                    </td>
                                    <td class="text-center" style="color:var(--ca-text-muted);font-size:0.875rem;"><%# Eval("Solved") %></td>
                                    <td style="font-size:0.78rem;color:var(--ca-text-muted);">
                                        <%# string.Format("{0:HH:mm}", Eval("LastSolve")) %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <%-- ─── Rules Panel ─────────────────────────────────────────── --%>
    <div class="comp-panel" id="panel-rules" role="tabpanel" aria-labelledby="tab-rules">
        <div class="ca-card">
            <div class="ca-card-header">
                <h3 class="ca-card-title">
                    <i class="fas fa-scroll me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                    Competition Rules
                </h3>
            </div>
            <div style="line-height:1.9;">
                <asp:Repeater ID="rptRules" runat="server">
                    <ItemTemplate>
                        <div class="d-flex align-items-start gap-2 mb-2">
                            <i class="fas fa-check-circle mt-1 flex-shrink-0"
                               style="color:var(--ca-success);font-size:0.85rem;" aria-hidden="true"></i>
                            <span style="font-size:0.875rem;color:var(--ca-text-secondary);"><%# Container.DataItem %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
function switchTab(name) {
    document.querySelectorAll('.comp-tab-btn').forEach(function (btn) {
        btn.classList.remove('active');
        btn.setAttribute('aria-selected', 'false');
    });
    document.querySelectorAll('.comp-panel').forEach(function (p) {
        p.classList.remove('active');
    });
    var tb = document.getElementById('tab-' + name);
    if(tb) {
        tb.classList.add('active');
        tb.setAttribute('aria-selected', 'true');
    }
    var pn = document.getElementById('panel-' + name);
    if(pn) {
        pn.classList.add('active');
    }
}

(function () {
    // Flash auto-dismiss
    var flash = document.querySelector('.ca-auto-dismiss');
    if (flash) { 
        setTimeout(function () { 
            flash.style.opacity = '0'; 
            flash.style.transition = 'opacity 0.5s'; 
            setTimeout(function () { flash.style.display = 'none'; }, 500); 
        }, 4000); 
    }
    
    // Set active progress bars on load
    var progressBars = document.querySelectorAll('.ca-progress-bar');
    progressBars.forEach(function(bar) {
        var width = bar.getAttribute('data-width');
        if (width) {
            setTimeout(function() {
                bar.style.width = width + '%';
            }, 100);
        }
    });
}());
</script>
</asp:Content>
