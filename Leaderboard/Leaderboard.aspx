<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leaderboard.aspx.cs"
         Inherits="CyberArenaWebForms.Leaderboard.LeaderboardPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Leaderboard</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ═══════════════════════════════════════════════════════
         Page Header + Search
    ═══════════════════════════════════════════════════════ --%>
    <div class="p3-page-header">
        <div>
            <h2 class="p3-page-title">
                <i class="fas fa-trophy" style="color:var(--ca-warning);" aria-hidden="true"></i>
                Leaderboard
            </h2>
            <p class="p3-page-subtitle">Top performers across all challenges</p>
        </div>
        <div class="d-flex align-items-center gap-2">
            <div class="lb-search-wrap">
                <span class="lb-search-icon">
                    <i class="fas fa-magnifying-glass" aria-hidden="true"></i>
                </span>
                <input type="search"
                       id="lb-search"
                       class="lb-search-input"
                       placeholder="Search players or teams&#x2026;"
                       value="<%= System.Web.HttpUtility.HtmlAttributeEncode(VM != null ? VM.SearchQuery ?? "" : "") %>"
                       aria-label="Search leaderboard" />
            </div>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Current User Rank Banner
    ═══════════════════════════════════════════════════════ --%>
    <div class="ca-card d-flex align-items-center gap-3 mb-3" style="padding:1rem 1.4rem;">
        <i class="fas fa-user-circle"
           style="color:var(--ca-accent);font-size:1.2rem;"
           aria-hidden="true"></i>
        <div>
            <span style="font-size:0.85rem;color:var(--ca-text-muted);">Your current rank</span>
            <div style="font-family:'Space Grotesk',sans-serif;font-size:1.1rem;font-weight:700;color:var(--ca-text-primary);">
                #<asp:Literal ID="litCurrentRank" runat="server" />
                &mdash;
                <asp:Literal ID="litCurrentUsername" runat="server" />
            </div>
        </div>
        <a href="<%= ResolveUrl("~/Dashboard/Dashboard.aspx") %>"
           class="btn btn-outline-primary btn-sm ms-auto"
           id="btn-my-dashboard">
            <i class="fas fa-gauge-high me-1" aria-hidden="true"></i>My Dashboard
        </a>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Top 3 Podium (shown only when Gold/Silver/Bronze exist)
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlPodium" runat="server" Visible="false">
        <div class="ca-card mb-3 overflow-hidden" style="padding:0;">
            <div style="padding:1.25rem 1.5rem 0;border-bottom:1px solid var(--ca-border-light);display:flex;align-items:center;justify-content:space-between;">
                <span class="ca-section-title" style="margin:0;">
                    <i class="fas fa-medal me-2" style="color:var(--ca-warning);" aria-hidden="true"></i>
                    Top Performers
                </span>
            </div>

            <div class="lb-podium" aria-label="Top 3 leaderboard podium">

                <%-- 2nd place — Silver --%>
                <div class="lb-podium-item">
                    <div class="lb-podium-avatar silver" aria-label="Rank 2">
                        <asp:Literal ID="litSilverInitials" runat="server" />
                    </div>
                    <div class="lb-podium-name">
                        <asp:Literal ID="litSilverName" runat="server" />
                    </div>
                    <div class="lb-podium-team">
                        <asp:Literal ID="litSilverTeam" runat="server" />
                    </div>
                    <div class="lb-podium-score">
                        <asp:Literal ID="litSilverScore" runat="server" />
                    </div>
                    <div class="lb-podium-block silver">
                        <span class="lb-podium-rank silver">#2</span>
                    </div>
                </div>

                <%-- 1st place — Gold --%>
                <div class="lb-podium-item first">
                    <div class="lb-podium-avatar gold" aria-label="Rank 1">
                        <asp:Literal ID="litGoldInitials" runat="server" />
                    </div>
                    <div class="lb-podium-name">
                        <asp:Literal ID="litGoldName" runat="server" />
                    </div>
                    <div class="lb-podium-team">
                        <asp:Literal ID="litGoldTeam" runat="server" />
                    </div>
                    <div class="lb-podium-score">
                        <asp:Literal ID="litGoldScore" runat="server" />
                    </div>
                    <div class="lb-podium-block gold">
                        <span class="lb-podium-rank gold">#1</span>
                    </div>
                </div>

                <%-- 3rd place — Bronze --%>
                <div class="lb-podium-item">
                    <div class="lb-podium-avatar bronze" aria-label="Rank 3">
                        <asp:Literal ID="litBronzeInitials" runat="server" />
                    </div>
                    <div class="lb-podium-name">
                        <asp:Literal ID="litBronzeName" runat="server" />
                    </div>
                    <div class="lb-podium-team">
                        <asp:Literal ID="litBronzeTeam" runat="server" />
                    </div>
                    <div class="lb-podium-score">
                        <asp:Literal ID="litBronzeScore" runat="server" />
                    </div>
                    <div class="lb-podium-block bronze">
                        <span class="lb-podium-rank bronze">#3</span>
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>

    <%-- ═══════════════════════════════════════════════════════
         Full Leaderboard Table
    ═══════════════════════════════════════════════════════ --%>
    <div class="adm-table-card">
        <div class="adm-table-toolbar" style="justify-content:space-between;">
            <span style="font-size:0.85rem;font-weight:600;color:var(--ca-text-primary);">
                All Participants
            </span>
            <span style="font-size:0.82rem;color:var(--ca-text-muted);"
                  id="lb-count-label" aria-live="polite">
                <strong id="lb-visible-count">
                    <asp:Literal ID="litEntryCount" runat="server" />
                </strong> participants
            </span>
        </div>

        <div class="table-responsive">
            <table class="table mb-0" aria-label="Full leaderboard">
                <thead>
                    <tr>
                        <th style="width:60px;">Rank</th>
                        <th>Player</th>
                        <th>Team</th>
                        <th class="text-center">Score</th>
                        <th class="text-center">Solved</th>
                    </tr>
                </thead>
                <tbody id="lb-table-body">
                    <asp:Repeater ID="rptLeaderboard" runat="server">
                        <ItemTemplate>
                            <tr class="lb-table-row <%# (bool)Eval("IsCurrentUser") ? "lb-current-user" : "" %>"
                                data-username="<%# Eval("Username").ToString().ToLower() %>"
                                data-team="<%# Eval("TeamName").ToString().ToLower() %>">

                                <%-- Rank --%>
                                <td>
                                    <span class="ca-rank-badge <%# Eval("RankBadgeClass") %>">
                                        <%# Eval("Rank") %>
                                    </span>
                                </td>

                                <%-- Player --%>
                                <td>
                                    <div class="d-flex align-items-center gap-2">
                                        <div class="ca-user-avatar-sm <%# Eval("AvatarAltClass") %>"
                                             aria-hidden="true">
                                            <%# Eval("AvatarInitials") %>
                                        </div>
                                        <div>
                                            <div style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                                                <%# Eval("Username") %>
                                                <%# (bool)Eval("IsCurrentUser")
                                                    ? "<span class=\"badge bg-primary ms-1\">You</span>"
                                                    : "" %>
                                            </div>
                                        </div>
                                    </div>
                                </td>

                                <%-- Team --%>
                                <td>
                                    <a href="<%= ResolveUrl("~/Teams/Teams.aspx") %>"
                                       style="font-size:0.82rem;color:var(--ca-text-muted);">
                                        <%# Eval("TeamName") %>
                                    </a>
                                </td>

                                <%-- Score --%>
                                <td class="text-center">
                                    <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);">
                                        <%# string.Format("{0:N0}", Eval("TotalScore")) %>
                                    </span>
                                </td>

                                <%-- Solved --%>
                                <td class="text-center"
                                    style="color:var(--ca-text-secondary);font-size:0.875rem;">
                                    <%# Eval("ChallengesSolved") %>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <%-- Empty state (shown by JS when search returns no matches) --%>
        <div class="ca-empty-state d-none" id="lb-empty" role="status" aria-live="polite">
            <i class="fas fa-trophy" aria-hidden="true"></i>
            <p>No participants match your search.</p>
        </div>

        <%-- Table footer --%>
        <div style="padding:0.75rem 1.25rem;border-top:1px solid var(--ca-border-light);display:flex;align-items:center;justify-content:space-between;">
            <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                Showing <span id="lb-footer-count">
                    <asp:Literal ID="litFooterCount" runat="server" />
                </span> participants
            </span>
            <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                <i class="fas fa-circle-info me-1" aria-hidden="true"></i>
                Scores update every 5 minutes
            </span>
        </div>
    </div>

</div><%-- /.stagger-children --%>

</asp:Content>

<%-- ─── Page Scripts ────────────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    var rows    = document.querySelectorAll('.lb-table-row');
    var empty   = document.getElementById('lb-empty');
    var countEl = document.getElementById('lb-visible-count');
    var footer  = document.getElementById('lb-footer-count');

    function applyFilter() {
        var q   = (document.getElementById('lb-search').value || '').toLowerCase().trim();
        var vis = 0;

        rows.forEach(function (row) {
            var user = row.dataset.username || '';
            var team = row.dataset.team     || '';
            var show = !q || user.includes(q) || team.includes(q);
            row.style.display = show ? '' : 'none';
            if (show) vis++;
        });

        if (empty)   empty.classList.toggle('d-none', vis > 0);
        if (countEl) countEl.textContent = vis;
        if (footer)  footer.textContent  = vis;
    }

    var searchEl = document.getElementById('lb-search');
    if (searchEl) searchEl.addEventListener('input', applyFilter);

    // Highlight current-user row with a subtle entrance animation
    var myRow = document.querySelector('.lb-current-user');
    if (myRow) {
        myRow.style.transition = 'background 0.4s ease';
        setTimeout(function () { myRow.style.background = 'var(--ca-accent-light, rgba(99,102,241,.08))'; }, 300);
    }
}());
</script>
</asp:Content>
