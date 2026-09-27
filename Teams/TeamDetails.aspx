<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamDetails.aspx.cs"
         Inherits="CyberArenaWebForms.Teams.TeamDetailsPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
    <asp:Literal ID="litPageTitle" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- Back button --%>
    <div class="mb-3">
        <a href="<%= ResolveUrl("~/Teams/Teams.aspx") %>" class="btn btn-secondary btn-sm" id="btn-back-teams">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>All Teams
        </a>
    </div>

    <%-- ─── Team Hero ─────────────────────────────────────────── --%>
    <div class="team-hero mb-3">
        <div class="d-flex align-items-flex-start gap-3 flex-wrap">
            <div class="team-hero-avatar" aria-hidden="true">
                <asp:Literal ID="litAvatarInitials" runat="server" />
            </div>
            <div style="flex:1;min-width:0;">
                <div class="d-flex align-items-center gap-2 flex-wrap mb-1">
                    <h2 style="font-family:'Space Grotesk',sans-serif;font-size:1.5rem;font-weight:700;color:var(--ca-text-primary);margin:0;">
                        <asp:Literal ID="litTeamName" runat="server" />
                    </h2>
                    
                    <asp:Panel ID="pnlMyTeamBadge" runat="server" Visible="false" CssClass="badge bg-success">
                        My Team
                    </asp:Panel>
                    
                    <span class="ca-comp-status" id="spanRankStatus" runat="server" style="font-size:0.75rem;">
                        Rank #<asp:Literal ID="litRank" runat="server" />
                    </span>
                </div>
                
                <asp:Panel ID="pnlDescription" runat="server" Visible="false">
                    <p style="font-size:0.875rem;color:var(--ca-text-muted);margin:0 0 0.75rem;">
                        <asp:Literal ID="litDescription" runat="server" />
                    </p>
                </asp:Panel>
                
                <div class="d-flex align-items-center gap-3 flex-wrap" style="font-size:0.78rem;color:var(--ca-text-muted);">
                    <span>
                        <i class="fas fa-crown me-1" style="color:var(--ca-warning);" aria-hidden="true"></i>
                        Captain: <strong style="color:var(--ca-text-primary);"><asp:Literal ID="litCaptain" runat="server" /></strong>
                    </span>
                    <span>
                        <i class="fas fa-users me-1" aria-hidden="true"></i>
                        <asp:Literal ID="litMemberCount" runat="server" />
                    </span>
                    <span>
                        <i class="fas fa-globe me-1" aria-hidden="true"></i>
                        <asp:Literal ID="litCountry" runat="server" />
                    </span>
                    <span>
                        <i class="fas fa-calendar me-1" aria-hidden="true"></i>
                        Since <asp:Literal ID="litCreatedAt" runat="server" />
                    </span>
                </div>
            </div>
            <div class="d-flex gap-3 flex-shrink-0 flex-wrap">
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val" data-count="<%= VM != null ? VM.Score.ToString() : "0" %>">
                        <asp:Literal ID="litScore" runat="server" />
                    </span>
                    <span class="comp-detail-stat-label">Score</span>
                </div>
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val">
                        <asp:Literal ID="litChallengesSolved" runat="server" />
                    </span>
                    <span class="comp-detail-stat-label">Solved</span>
                </div>
                <div class="comp-detail-stat">
                    <span class="comp-detail-stat-val">
                        #<asp:Literal ID="litGlobalRank" runat="server" />
                    </span>
                    <span class="comp-detail-stat-label">Global Rank</span>
                </div>
            </div>
        </div>
    </div>

    <div class="row g-3">

        <%-- ─── Members ─────────────────────────────────────────── --%>
        <div class="col-12 col-lg-6">
            <div class="ca-card h-100">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">
                            <i class="fas fa-users me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                            Members
                        </h3>
                        <div class="ca-card-subtitle"><asp:Literal ID="litMemberCountTitle" runat="server" /> / 4 slots filled</div>
                    </div>
                    
                    <asp:Panel ID="pnlInviteButton" runat="server" Visible="false">
                        <button type="button" class="btn btn-outline-primary btn-sm" id="btn-invite" title="Invite member (coming soon)">
                            <i class="fas fa-user-plus me-1" aria-hidden="true"></i>Invite
                        </button>
                    </asp:Panel>
                </div>

                <asp:Repeater ID="rptMembers" runat="server">
                    <ItemTemplate>
                        <div class="ca-team-member">
                            <div class="ca-member-avatar <%# Eval("AvatarClass") %>" aria-hidden="true">
                                <%# Eval("AvatarInitials") %>
                            </div>
                            <div class="ca-member-info">
                                <div class="ca-member-name">
                                    <%# Eval("Username") %>
                                    <%# (bool)Eval("IsCaptain")
                                        ? "<i class=\"fas fa-crown ms-1\" style=\"color:var(--ca-warning);font-size:0.7rem;\" title=\"Captain\" aria-label=\"Team Captain\"></i>"
                                        : "" %>
                                </div>
                                <div class="ca-member-role">
                                    <%# (bool)Eval("IsCaptain") ? "Captain" : "Member" %> &middot;
                                    Joined <%# string.Format("{0:dd MMM yyyy}", Eval("JoinedAt")) %>
                                </div>
                            </div>
                            <div class="text-end">
                                <div class="ca-member-score"><%# string.Format("{0:N0}", Eval("Score")) %> pts</div>
                                <div style="font-size:0.72rem;color:var(--ca-text-muted);"><%# Eval("ChallengesSolved") %> solved</div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <%-- ─── Score Progression ───────────────────────────────── --%>
        <div class="col-12 col-lg-6">
            <div class="ca-card h-100">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">
                            <i class="fas fa-chart-line me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                            Score Progression
                        </h3>
                        <div class="ca-card-subtitle">Last 9 data points</div>
                    </div>
                </div>
                <div class="adm-chart-wrap" style="height:200px;">
                    <canvas id="teamScoreChart" aria-label="Team score progression chart"></canvas>
                </div>
            </div>
        </div>

    </div>

    <%-- ─── Challenge Progress ──────────────────────────────────── --%>
    <div class="ca-card mt-3">
        <div class="ca-card-header">
            <div>
                <h3 class="ca-card-title">
                    <i class="fas fa-flag me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                    Challenge Progress
                </h3>
                <div class="ca-card-subtitle">
                    <asp:Literal ID="litSolvedChallengesText" runat="server" /> of 
                    <asp:Literal ID="litTotalChallengesText" runat="server" /> challenges solved
                </div>
            </div>
            <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);">
                <asp:Literal ID="litSolvedPercentText" runat="server" />%
            </span>
        </div>
        <div class="ca-progress" style="height:8px;" aria-label="<%= VM != null ? VM.SolvedPercent.ToString() : "0" %>% challenges solved">
            <div class="ca-progress-bar" data-width="<%= VM != null ? VM.SolvedPercent.ToString() : "0" %>" style="width:0;" id="challenge-progress-bar"></div>
        </div>
        <div class="d-flex justify-content-between mt-2" style="font-size:0.75rem;color:var(--ca-text-muted);">
            <span><asp:Literal ID="litSolvedFooter" runat="server" /> solved</span>
            <span><asp:Literal ID="litTotalFooter" runat="server" /> total</span>
        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
<script>
(function () {
    var ctx = document.getElementById('teamScoreChart');
    if (ctx) {
        new Chart(ctx.getContext('2d'), {
            type: 'line',
            data: {
                labels: <%= ScoreLabelsJson %>,
                datasets: [{
                    label: 'Score',
                    data: <%= ScoreHistoryJson %>,
                    borderColor: '#3B82F6',
                    backgroundColor: 'rgba(59,130,246,0.1)',
                    borderWidth: 2,
                    pointBackgroundColor: '#3B82F6',
                    pointRadius: 3,
                    tension: 0.35,
                    fill: true
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    x: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { color: '#64748B', font: { size: 10 } } },
                    y: { grid: { color: 'rgba(255,255,255,0.05)' }, ticks: { color: '#64748B', font: { size: 10 } } }
                }
            }
        });
    }

    // Animate progress bar
    var pb = document.getElementById('challenge-progress-bar');
    if (pb) {
        var w = parseInt(pb.dataset.width || 0, 10);
        setTimeout(function () {
            pb.style.transition = 'width 1s ease';
            pb.style.width = w + '%';
        }, 200);
    }
}());
</script>
</asp:Content>
