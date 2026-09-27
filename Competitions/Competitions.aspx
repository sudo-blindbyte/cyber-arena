<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Competitions.aspx.cs"
         Inherits="CyberArenaWebForms.Competitions.CompetitionsPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Competitions</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ═══════════════════════════════════════════════════════
         Page Header
    ═══════════════════════════════════════════════════════ --%>
    <div class="p3-page-header">
        <div>
            <h2 class="p3-page-title">
                <i class="fas fa-bolt" style="color:var(--ca-warning);" aria-hidden="true"></i>
                Competitions
            </h2>
            <p class="p3-page-subtitle"><asp:Literal ID="litCompCount" runat="server" /> competitions available</p>
        </div>
        <div class="d-flex gap-2 flex-wrap">
            <button type="button" class="p3-filter-btn <%= VM != null && VM.FilterStatus == "all" ? "active" : "" %>"
                    id="btn-filter-all" onclick="filterComps('all')" aria-pressed="<%= VM != null && VM.FilterStatus == "all" ? "true" : "false" %>">
                All
            </button>
            <button type="button" class="p3-filter-btn <%= VM != null && VM.FilterStatus == "active" ? "active" : "" %>"
                    id="btn-filter-active" onclick="filterComps('active')" aria-pressed="<%= VM != null && VM.FilterStatus == "active" ? "true" : "false" %>">
                <span class="d-flex align-items-center gap-1">
                    <span style="width:6px;height:6px;border-radius:50%;background:var(--ca-success);display:inline-block;"></span>
                    Active
                </span>
            </button>
            <button type="button" class="p3-filter-btn <%= VM != null && VM.FilterStatus == "upcoming" ? "active" : "" %>"
                    id="btn-filter-upcoming" onclick="filterComps('upcoming')" aria-pressed="<%= VM != null && VM.FilterStatus == "upcoming" ? "true" : "false" %>">
                Upcoming
            </button>
            <button type="button" class="p3-filter-btn <%= VM != null && VM.FilterStatus == "ended" ? "active" : "" %>"
                    id="btn-filter-ended" onclick="filterComps('ended')" aria-pressed="<%= VM != null && VM.FilterStatus == "ended" ? "true" : "false" %>">
                Ended
            </button>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Active competitions banner
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlActiveBanner" runat="server" Visible="false">
        <div class="alert alert-success mb-3 d-flex align-items-center gap-2" role="alert" style="border-left-color:var(--ca-success);">
            <i class="fas fa-circle-play fa-fw" aria-hidden="true"></i>
            <span>
                <strong><asp:Literal ID="litActiveCount" runat="server" /> active competition<asp:Literal ID="litActivePlural" runat="server" /> running now!</strong>
                Join and start solving challenges.
            </span>
        </div>
    </asp:Panel>

    <%-- ═══════════════════════════════════════════════════════
         Competitions Grid
    ═══════════════════════════════════════════════════════ --%>
    <div class="row g-3" id="comp-grid">
        <asp:Repeater ID="rptCompetitions" runat="server" OnItemCommand="rptCompetitions_ItemCommand">
            <ItemTemplate>
                <div class="col-12 col-md-6 col-xl-4 comp-grid-item" data-status="<%# Eval("Status") %>">
                    <div class="comp-card <%# Eval("StatusClass") %> h-100">
                        <div class="comp-card-header">
                            <div class="d-flex align-items-center gap-2" style="flex:1;min-width:0;">
                                <div class="comp-icon <%# Eval("StatusClass") %>" aria-hidden="true">
                                    <i class="fas fa-bolt"></i>
                                </div>
                                <div style="min-width:0;">
                                    <div class="comp-card-name"><%# Eval("Name") %></div>
                                    <div class="comp-card-desc"><%# Eval("Description") %></div>
                                </div>
                            </div>
                            <span class="ca-comp-status <%# Eval("StatusClass") %> ms-2 flex-shrink-0">
                                <%# Eval("Status").ToString().ToUpper() %>
                            </span>
                        </div>

                        <div class="comp-card-meta">
                            <div class="comp-meta-item">
                                <i class="fas fa-flag" aria-hidden="true"></i>
                                <%# Eval("ChallengeCount") %> challenges
                            </div>
                            <div class="comp-meta-item">
                                <i class="fas fa-users" aria-hidden="true"></i>
                                <%# Eval("ParticipantCount") %> / <%# Eval("MaxParticipants") %>
                            </div>
                            <div class="comp-meta-item">
                                <i class="fas fa-user-group" aria-hidden="true"></i>
                                <%# Eval("Format") %>
                            </div>
                        </div>

                        <div class="comp-meta-item" style="font-size:0.78rem;color:var(--ca-text-muted);">
                            <i class="fas fa-clock" aria-hidden="true"></i>
                            <%# Eval("TimeDisplay") %>
                        </div>

                        <%-- Progress bar for active competitions --%>
                        <asp:PlaceHolder runat="server" Visible='<%# Eval("Status").ToString() == "active" && (int)Eval("MaxParticipants") > 0 %>'>
                            <div>
                                <div class="d-flex justify-content-between mb-1" style="font-size:0.72rem;color:var(--ca-text-muted);">
                                    <span>Participants</span>
                                    <span><%# (int)((double)(int)Eval("ParticipantCount") / (int)Eval("MaxParticipants") * 100) %>%</span>
                                </div>
                                <div class="ca-progress">
                                    <div class="ca-progress-bar <%# ((int)((double)(int)Eval("ParticipantCount") / (int)Eval("MaxParticipants") * 100)) > 80 ? "warning" : "" %>"
                                         data-width="<%# (int)((double)(int)Eval("ParticipantCount") / (int)Eval("MaxParticipants") * 100) %>" style="width:0;"></div>
                                </div>
                            </div>
                        </asp:PlaceHolder>

                        <div class="d-flex gap-2 mt-auto">
                            <a href="<%= ResolveUrl("~/Competitions/CompetitionDetails.aspx") %>?id=<%# Eval("Id") %>"
                               class="btn btn-outline-primary btn-sm flex-1 w-100">
                                <i class="fas fa-eye me-1" aria-hidden="true"></i>View Details
                            </a>
                            
                            <asp:PlaceHolder runat="server" Visible='<%# Eval("Status").ToString() == "active" && !(bool)Eval("IsJoined") %>'>
                                <asp:Button ID="btnJoin" runat="server" Text="Join" CssClass="btn btn-primary btn-sm"
                                            CommandName="Join" CommandArgument='<%# Eval("Id") %>' />
                            </asp:PlaceHolder>

                            <asp:PlaceHolder runat="server" Visible='<%# (bool)Eval("IsJoined") %>'>
                                <span class="badge bg-success align-self-center">Joined</span>
                            </asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <%-- Empty state --%>
    <div class="ca-empty-state d-none" id="comp-empty" role="status" aria-live="polite">
        <i class="fas fa-bolt" aria-hidden="true"></i>
        <p>No competitions match this filter.</p>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
function filterComps(status) {
    var items = document.querySelectorAll('.comp-grid-item');
    var empty = document.getElementById('comp-empty');
    var vis   = 0;

    // Update button states
    document.querySelectorAll('.p3-filter-btn').forEach(function (btn) {
        btn.classList.remove('active');
        btn.setAttribute('aria-pressed', 'false');
    });
    var active = document.getElementById('btn-filter-' + status);
    if (active) { active.classList.add('active'); active.setAttribute('aria-pressed', 'true'); }

    items.forEach(function (item) {
        var show = status === 'all' || item.dataset.status === status;
        item.style.display = show ? '' : 'none';
        if (show) vis++;
    });

    if (empty) empty.classList.toggle('d-none', vis > 0);
}

// Initialize progress bars
document.addEventListener("DOMContentLoaded", function() {
    var progressBars = document.querySelectorAll('.ca-progress-bar');
    progressBars.forEach(function(bar) {
        var width = bar.getAttribute('data-width');
        if (width) {
            setTimeout(function() {
                bar.style.width = width + '%';
            }, 100);
        }
    });

    // Apply initial filter if any
    var currentFilter = '<%= VM != null ? VM.FilterStatus : "all" %>';
    filterComps(currentFilter);
});
</script>
</asp:Content>
