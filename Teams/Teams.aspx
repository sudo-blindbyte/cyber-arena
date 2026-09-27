<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teams.aspx.cs"
         Inherits="CyberArenaWebForms.Teams.TeamsPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Teams</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ═══════════════════════════════════════════════════════
         Flash message (from POST redirect via SessionHelper)
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlFlash" runat="server" Visible="false">
        <div class="alert ca-auto-dismiss alert-success d-flex align-items-center gap-2 mb-3"
             role="alert" id="flash-alert">
            <i class="fas fa-fw fa-circle-check" id="flash-icon" aria-hidden="true"></i>
            <asp:Literal ID="litFlashMsg" runat="server" />
        </div>
    </asp:Panel>

    <%-- ═══════════════════════════════════════════════════════
         Page Header
    ═══════════════════════════════════════════════════════ --%>
    <div class="p3-page-header">
        <div>
            <h2 class="p3-page-title">
                <i class="fas fa-users" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Teams
            </h2>
            <p class="p3-page-subtitle"><asp:Literal ID="litTeamCount" runat="server" /> teams competing across all challenges</p>
        </div>
        <div class="d-flex align-items-center gap-2 flex-wrap">
            <div class="lb-search-wrap">
                <span class="lb-search-icon"><i class="fas fa-magnifying-glass" aria-hidden="true"></i></span>
                <input type="search"
                       id="team-search"
                       class="lb-search-input"
                       placeholder="Search teams&#x2026;"
                       value="<%= System.Web.HttpUtility.HtmlAttributeEncode(VM != null ? VM.SearchQuery ?? "" : "") %>"
                       aria-label="Search teams" />
            </div>
            
            <asp:Panel ID="pnlCreateTeam" runat="server" Visible="false">
                <a href="<%= ResolveUrl("~/Teams/CreateTeam.aspx") %>" class="btn btn-primary" id="btn-create-team">
                    <i class="fas fa-plus me-2" aria-hidden="true"></i>Create Team
                </a>
            </asp:Panel>
            
            <asp:Panel ID="pnlMyTeam" runat="server" Visible="false">
                <asp:HyperLink ID="lnkMyTeam" runat="server" CssClass="btn btn-outline-primary" id="btn-my-team">
                    <i class="fas fa-shield-halved me-2" aria-hidden="true"></i>My Team
                </asp:HyperLink>
            </asp:Panel>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Teams Grid
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlNoTeams" runat="server" Visible="false" CssClass="ca-empty-state">
        <i class="fas fa-users" aria-hidden="true"></i>
        <p>No teams found.</p>
        <a href="<%= ResolveUrl("~/Teams/CreateTeam.aspx") %>" class="btn btn-primary mt-2">Create a Team</a>
    </asp:Panel>

    <asp:Panel ID="pnlTeamsGrid" runat="server">
        <div class="row g-3" id="teams-grid">
            <asp:Repeater ID="rptTeams" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-sm-6 col-xl-4 team-grid-item"
                         data-name="<%# Eval("Name").ToString().ToLower() %>"
                         data-captain="<%# Eval("CaptainUsername").ToString().ToLower() %>">
                        <a href="<%= ResolveUrl("~/Teams/TeamDetails.aspx") %>?id=<%# Eval("Id") %>"
                           class="team-card <%# (bool)Eval("IsMyTeam") ? "my-team" : "" %> text-decoration-none h-100">

                            <div class="team-card-header">
                                <div class="team-avatar" aria-hidden="true">
                                    <%# Eval("Name").ToString()[0] %>
                                </div>
                                <div style="flex:1;min-width:0;">
                                    <div class="team-card-name">
                                        <%# Eval("Name") %>
                                        <%# (bool)Eval("IsMyTeam")
                                            ? "<span class=\"badge bg-success ms-1\" style=\"font-size:0.62rem;\">My Team</span>"
                                            : "" %>
                                    </div>
                                    <div class="team-card-captain">
                                        <i class="fas fa-crown" style="color:var(--ca-warning);font-size:0.68rem;" aria-hidden="true"></i>
                                        <%# Eval("CaptainUsername") %>
                                        <span style="margin-left:0.5rem;"><%# Eval("Country") %></span>
                                    </div>
                                </div>
                                <span class="ca-rank-badge <%# Eval("RankBadgeClass") %>" aria-label="Rank <%# Eval("Rank") %>">
                                    #<%# Eval("Rank") %>
                                </span>
                            </div>

                            <div class="team-card-stats">
                                <div class="team-card-stat">
                                    <span class="team-card-stat-val" data-count="<%# Eval("Score") %>"><%# string.Format("{0:N0}", Eval("Score")) %></span>
                                    <span class="team-card-stat-label">Score</span>
                                </div>
                                <div class="team-card-stat">
                                    <span class="team-card-stat-val"><%# Eval("ChallengesSolved") %></span>
                                    <span class="team-card-stat-label">Solved</span>
                                </div>
                                <div class="team-card-stat">
                                    <span class="team-card-stat-val"><%# Eval("MemberCount") %></span>
                                    <span class="team-card-stat-label">Members</span>
                                </div>
                            </div>

                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <%-- Empty state for search --%>
        <div class="ca-empty-state d-none" id="team-empty" role="status" aria-live="polite">
            <i class="fas fa-users" aria-hidden="true"></i>
            <p>No teams match your search.</p>
        </div>
    </asp:Panel>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    var items  = document.querySelectorAll('.team-grid-item');
    var empty  = document.getElementById('team-empty');

    var searchInput = document.getElementById('team-search');
    if (searchInput) {
        searchInput.addEventListener('input', function () {
            var q = this.value.toLowerCase().trim();
            var vis = 0;
            items.forEach(function (item) {
                var name    = item.dataset.name    || '';
                var captain = item.dataset.captain || '';
                var show = !q || name.includes(q) || captain.includes(q);
                item.style.display = show ? '' : 'none';
                if (show) vis++;
            });
            if (empty) empty.classList.toggle('d-none', vis > 0);
        });
    }
    
    // ── Flash auto-dismiss ─────────────────────────────────
    var flash = document.querySelector('.ca-auto-dismiss');
    if (flash) { setTimeout(function () { flash.style.opacity = '0'; flash.style.transition = 'opacity 0.5s'; setTimeout(function () { flash.style.display = 'none'; }, 500); }, 4000); }
}());
</script>
</asp:Content>
