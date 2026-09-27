<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Challenges.aspx.cs"
         Inherits="CyberArenaWebForms.Challenges.ChallengesPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Challenges</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ═══════════════════════════════════════════════════════
         Flash message (replaces TempData["SubmitResult"])
    ═══════════════════════════════════════════════════════ --%>
    <asp:Panel ID="pnlFlash" runat="server" Visible="false">
        <div class="alert ca-auto-dismiss d-flex align-items-center gap-2 mb-3"
             role="alert"
             id="flash-alert">
            <i class="fas fa-fw" id="flash-icon" aria-hidden="true"></i>
            <asp:Literal ID="litFlashMsg" runat="server" />
        </div>
    </asp:Panel>

    <%-- ═══════════════════════════════════════════════════════
         Page Header
    ═══════════════════════════════════════════════════════ --%>
    <div class="ch-page-header">
        <div>
            <h2 class="ch-page-title">
                <i class="fas fa-flag me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Challenges
            </h2>
            <p class="ch-page-subtitle">
                <strong style="color:var(--ca-text-primary);">
                    <asp:Literal ID="litSolvedCount" runat="server" />
                </strong>
                of <strong style="color:var(--ca-text-primary);">
                    <asp:Literal ID="litTotalCount" runat="server" />
                </strong> solved &nbsp;&middot;&nbsp;
                <strong style="color:var(--ca-accent);">
                    <asp:Literal ID="litEarnedPoints" runat="server" />
                </strong>
                / <asp:Literal ID="litTotalPoints" runat="server" /> pts
            </p>
        </div>
        <div class="d-flex align-items-center gap-2">
            <div style="display:flex;align-items:center;gap:0.75rem;background:var(--ca-bg-card);border:1px solid var(--ca-border);border-radius:var(--ca-radius);padding:0.55rem 1rem;">
                <div style="width:100px;">
                    <div class="ca-progress">
                        <div class="ca-progress-bar" id="overall-progress-bar"
                             data-width="<%= VM.ProgressPct %>"></div>
                    </div>
                </div>
                <span style="font-size:0.8rem;color:var(--ca-text-muted);">
                    <asp:Literal ID="litProgressPct" runat="server" />% done
                </span>
            </div>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Category Tabs (fully rendered server-side, filtered client-side)
    ═══════════════════════════════════════════════════════ --%>
    <div class="ch-cat-tabs" id="cat-tabs" role="tablist" aria-label="Filter by category">

        <%-- "All" tab --%>
        <button class="ch-cat-tab active"
                data-cat="All" role="tab"
                aria-selected="true">
            <i class="fas fa-th-large" aria-hidden="true"></i>
            All
            <span class="ch-cat-count"><%= VM.TotalCount %></span>
        </button>

        <%-- Per-category tabs (rendered by Repeater) --%>
        <asp:Repeater ID="rptCatTabs" runat="server">
            <ItemTemplate>
                <button class="ch-cat-tab"
                        data-cat="<%# Eval("Name") %>"
                        role="tab"
                        aria-selected="false">
                    <i class="fas <%# Eval("Icon") %>" aria-hidden="true"></i>
                    <%# Eval("ShortName") %>
                    <span class="ch-cat-count"><%# Eval("Count") %></span>
                </button>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Toolbar — search, difficulty filter, sort, view toggle
    ═══════════════════════════════════════════════════════ --%>
    <div class="ch-toolbar" role="search">

        <%-- Search --%>
        <div class="ch-search-wrap">
            <span class="ch-search-icon">
                <i class="fas fa-magnifying-glass" aria-hidden="true"></i>
            </span>
            <input type="search"
                   id="ch-search"
                   class="ch-search-input"
                   placeholder="Search challenges&#x2026;"
                   aria-label="Search challenges" />
        </div>

        <div class="ch-toolbar-divider" aria-hidden="true"></div>

        <%-- Difficulty pills --%>
        <div class="ch-filter-group" role="group" aria-label="Filter by difficulty">
            <span class="ch-filter-label">Difficulty</span>
            <button class="ch-pill all active" data-diff="All" aria-pressed="true">All</button>
            <button class="ch-pill easy"       data-diff="Easy"   aria-pressed="false">Easy</button>
            <button class="ch-pill medium"     data-diff="Medium" aria-pressed="false">Medium</button>
            <button class="ch-pill hard"       data-diff="Hard"   aria-pressed="false">Hard</button>
        </div>

        <div class="ch-toolbar-divider" aria-hidden="true"></div>

        <%-- Sort --%>
        <select class="ch-sort-select" id="ch-sort" aria-label="Sort challenges">
            <option value="points-asc">Points &#x2191;</option>
            <option value="points-desc">Points &#x2193;</option>
            <option value="solvers-desc">Most Solved</option>
            <option value="title-asc">A &#x2192; Z</option>
        </select>

        <%-- View toggle --%>
        <div class="ch-view-toggle" role="group" aria-label="View mode">
            <button class="ch-view-btn active" id="view-grid"
                    title="Grid view" aria-label="Grid view">
                <i class="fas fa-grid-2" aria-hidden="true"></i>
            </button>
            <button class="ch-view-btn" id="view-list"
                    title="List view" aria-label="List view">
                <i class="fas fa-list" aria-hidden="true"></i>
            </button>
        </div>
    </div>

    <%-- Count bar --%>
    <div class="ch-count-bar">
        <p class="ch-count-text" id="ch-count-label" aria-live="polite">
            Showing <strong id="ch-visible-count"><%= VM.TotalCount %></strong> challenges
        </p>
        <div class="d-flex align-items-center gap-2">
            <span class="ca-tag easy"><i class="fas fa-circle me-1" aria-hidden="true"></i>Easy</span>
            <span class="ca-tag medium"><i class="fas fa-circle me-1" aria-hidden="true"></i>Medium</span>
            <span class="ca-tag hard"><i class="fas fa-circle me-1" aria-hidden="true"></i>Hard</span>
        </div>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         Challenge Grid (card view)
    ═══════════════════════════════════════════════════════ --%>
    <div class="ch-grid" id="ch-grid" role="list">
        <asp:Repeater ID="rptChallenges" runat="server">
            <ItemTemplate>
                <a class="ch-card <%# (bool)Eval("IsSolved") ? "solved" : "" %>"
                   href="<%= ResolveUrl("~/Challenges/ChallengeDetails.aspx") %>?id=<%# Eval("Id") %>"
                   id="ch-card-<%# Eval("Id") %>"
                   role="listitem"
                   data-title="<%# Eval("Title").ToString().ToLower() %>"
                   data-category="<%# Eval("Category") %>"
                   data-difficulty="<%# Eval("Difficulty") %>"
                   data-points="<%# Eval("Points") %>"
                   data-solvers="<%# Eval("SolverCount") %>"
                   aria-label="<%# Eval("Title") %>, <%# Eval("Difficulty") %>, <%# Eval("Points") %> points<%# (bool)Eval("IsSolved") ? ", solved" : "" %>">

                    <%-- Card top row --%>
                    <div class="ch-card-top">
                        <div class="ch-card-icon <%# Eval("CategoryCssClass") %>" aria-hidden="true">
                            <i class="fas <%# Eval("CategoryIcon") %>"></i>
                        </div>
                        <%# (bool)Eval("IsSolved")
                            ? "<span class=\"ch-solved-badge\" aria-label=\"Solved\"><i class=\"fas fa-circle-check\" aria-hidden=\"true\"></i> Solved</span>"
                            : ((bool)Eval("HasAttachment")
                                ? "<span style=\"font-size:0.72rem;color:var(--ca-text-muted);\" title=\"Has attachment\"><i class=\"fas fa-paperclip\" aria-hidden=\"true\"></i></span>"
                                : "") %>
                    </div>

                    <%-- Title --%>
                    <h3 class="ch-card-title"><%# Eval("Title") %></h3>

                    <%-- Description preview --%>
                    <%# !string.IsNullOrEmpty((string)Eval("ShortDescription"))
                        ? "<p style=\"font-size:0.8rem;color:var(--ca-text-muted);margin:0;line-height:1.5;overflow:hidden;display:-webkit-box;-webkit-line-clamp:2;-webkit-box-orient:vertical;\">"
                          + System.Web.HttpUtility.HtmlEncode((string)Eval("ShortDescription")) + "</p>"
                        : "" %>

                    <%-- Meta row --%>
                    <div class="ch-card-meta">
                        <span class="ch-diff <%# Eval("DifficultyLower") %>"><%# Eval("Difficulty") %></span>
                        <span class="ch-card-category"><%# Eval("Category") %></span>
                    </div>

                    <%-- Footer --%>
                    <div class="ch-card-footer">
                        <div class="ch-points">
                            <%# Eval("Points") %> <small>pts</small>
                        </div>
                        <div class="ch-solvers-count" aria-label="<%# Eval("SolverCount") %> solvers">
                            <i class="fas fa-users" aria-hidden="true"></i> <%# Eval("SolverCount") %>
                        </div>
                    </div>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <%-- Empty state (shown by JS when no matches) --%>
    <div class="ch-empty d-none" id="ch-empty" role="status" aria-live="polite">
        <i class="fas fa-flag" aria-hidden="true"></i>
        <h4>No challenges found</h4>
        <p>Try adjusting your search or filters.</p>
        <button class="btn btn-secondary btn-sm mt-2" onclick="resetFilters()">
            <i class="fas fa-rotate-left me-1" aria-hidden="true"></i> Reset Filters
        </button>
    </div>

    <%-- ═══════════════════════════════════════════════════════
         List view (hidden by default, toggled by view-list button)
    ═══════════════════════════════════════════════════════ --%>
    <div class="d-none" id="ch-list-view">
        <div class="ca-card p-0" style="overflow:hidden;">
            <div class="table-responsive">
                <table class="table mb-0" aria-label="Challenges list">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Challenge</th>
                            <th>Category</th>
                            <th>Difficulty</th>
                            <th class="text-end">Points</th>
                            <th class="text-end">Solvers</th>
                            <th class="text-center">Status</th>
                        </tr>
                    </thead>
                    <tbody id="ch-list-body">
                        <asp:Repeater ID="rptListView" runat="server">
                            <ItemTemplate>
                                <tr class="ch-list-row"
                                    data-title="<%# Eval("Title").ToString().ToLower() %>"
                                    data-category="<%# Eval("Category") %>"
                                    data-difficulty="<%# Eval("Difficulty") %>"
                                    data-points="<%# Eval("Points") %>"
                                    data-solvers="<%# Eval("SolverCount") %>"
                                    style="cursor:pointer;"
                                    onclick="window.location.href='<%= ResolveUrl("~/Challenges/ChallengeDetails.aspx") %>?id=<%# Eval("Id") %>'">
                                    <td style="color:var(--ca-text-muted);font-size:0.82rem;"><%# Eval("Id") %></td>
                                    <td>
                                        <div class="d-flex align-items-center gap-2">
                                            <div class="ch-card-icon <%# Eval("CategoryCssClass") %>"
                                                 style="width:28px;height:28px;font-size:0.75rem;"
                                                 aria-hidden="true">
                                                <i class="fas <%# Eval("CategoryIcon") %>"></i>
                                            </div>
                                            <span style="font-weight:500;"><%# Eval("Title") %></span>
                                            <%# (bool)Eval("HasAttachment") ? "<i class=\"fas fa-paperclip\" style=\"color:var(--ca-text-muted);font-size:0.75rem;\" title=\"Has attachment\" aria-hidden=\"true\"></i>" : "" %>
                                        </div>
                                    </td>
                                    <td><span style="font-size:0.82rem;color:var(--ca-text-muted);"><%# Eval("Category") %></span></td>
                                    <td><span class="ch-diff <%# Eval("DifficultyLower") %>"><%# Eval("Difficulty") %></span></td>
                                    <td class="text-end">
                                        <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);"><%# Eval("Points") %></span>
                                    </td>
                                    <td class="text-end" style="color:var(--ca-text-muted);font-size:0.82rem;"><%# Eval("SolverCount") %></td>
                                    <td class="text-center">
                                        <%# (bool)Eval("IsSolved")
                                            ? "<span class=\"ch-solved-badge\"><i class=\"fas fa-check\" aria-hidden=\"true\"></i> Solved</span>"
                                            : "<span style=\"font-size:0.75rem;color:var(--ca-text-muted);\">&#x2014;</span>" %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</div><%-- /.stagger-children --%>

</asp:Content>

<%-- ─── Page Scripts ────────────────────────────────────────── --%>
<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    // ── State ──────────────────────────────────────────────
    var state = { cat: 'All', diff: 'All', q: '', sort: 'points-asc', view: 'grid' };

    // ── Elements ───────────────────────────────────────────
    var grid     = document.getElementById('ch-grid');
    var listView = document.getElementById('ch-list-view');
    var empty    = document.getElementById('ch-empty');
    var countLbl = document.getElementById('ch-visible-count');
    var searchEl = document.getElementById('ch-search');

    // ── Progress bar ───────────────────────────────────────
    var pb = document.getElementById('overall-progress-bar');
    if (pb) { setTimeout(function () { pb.style.transition = 'width 0.8s ease'; pb.style.width = (pb.dataset.width || 0) + '%'; }, 200); }

    // ── Category tabs ──────────────────────────────────────
    document.querySelectorAll('#cat-tabs .ch-cat-tab').forEach(function (btn) {
        btn.addEventListener('click', function () {
            state.cat = btn.dataset.cat;
            document.querySelectorAll('#cat-tabs .ch-cat-tab').forEach(function (b) {
                b.classList.remove('active');
                b.setAttribute('aria-selected', 'false');
            });
            btn.classList.add('active');
            btn.setAttribute('aria-selected', 'true');
            applyFilters();
        });
    });

    // ── Difficulty pills ───────────────────────────────────
    document.querySelectorAll('.ch-pill').forEach(function (btn) {
        btn.addEventListener('click', function () {
            state.diff = btn.dataset.diff;
            document.querySelectorAll('.ch-pill').forEach(function (b) {
                b.classList.remove('active');
                b.setAttribute('aria-pressed', 'false');
            });
            btn.classList.add('active');
            btn.setAttribute('aria-pressed', 'true');
            applyFilters();
        });
    });

    // ── Search ─────────────────────────────────────────────
    if (searchEl) {
        searchEl.addEventListener('input', function () {
            state.q = searchEl.value.toLowerCase().trim();
            applyFilters();
        });
    }

    // ── Sort ───────────────────────────────────────────────
    document.getElementById('ch-sort').addEventListener('change', function () {
        state.sort = this.value;
        sortCards();
    });

    // ── View toggle ────────────────────────────────────────
    document.getElementById('view-grid').addEventListener('click', function () {
        state.view = 'grid';
        grid.classList.remove('d-none');
        listView.classList.add('d-none');
        this.classList.add('active');
        document.getElementById('view-list').classList.remove('active');
    });
    document.getElementById('view-list').addEventListener('click', function () {
        state.view = 'list';
        listView.classList.remove('d-none');
        grid.classList.add('d-none');
        this.classList.add('active');
        document.getElementById('view-grid').classList.remove('active');
        applyFilters();
    });

    // ── Core filter ────────────────────────────────────────
    function matchesFilters(el) {
        var cat   = el.dataset.category  || '';
        var diff  = el.dataset.difficulty || '';
        var title = el.dataset.title || '';
        if (state.cat  !== 'All' && cat   !== state.cat)  return false;
        if (state.diff !== 'All' && diff  !== state.diff) return false;
        if (state.q && !title.includes(state.q))          return false;
        return true;
    }

    function applyFilters() {
        var visible = 0;
        grid.querySelectorAll('.ch-card').forEach(function (card) {
            var show = matchesFilters(card);
            card.style.display = show ? '' : 'none';
            if (show) visible++;
        });
        document.querySelectorAll('.ch-list-row').forEach(function (row) {
            row.style.display = matchesFilters(row) ? '' : 'none';
        });
        if (empty) empty.classList.toggle('d-none', visible > 0);
        if (countLbl) countLbl.textContent = visible;
    }

    // ── Sort (grid only) ───────────────────────────────────
    function sortCards() {
        var cards = Array.from(grid.querySelectorAll('.ch-card'));
        cards.sort(function (a, b) {
            switch (state.sort) {
                case 'points-desc':  return parseInt(b.dataset.points)  - parseInt(a.dataset.points);
                case 'points-asc':   return parseInt(a.dataset.points)  - parseInt(b.dataset.points);
                case 'solvers-desc': return parseInt(b.dataset.solvers) - parseInt(a.dataset.solvers);
                case 'title-asc':    return a.dataset.title.localeCompare(b.dataset.title);
                default:             return parseInt(a.dataset.points)  - parseInt(b.dataset.points);
            }
        });
        cards.forEach(function (c) { grid.appendChild(c); });
    }

    // ── Reset (called by empty-state button) ───────────────
    window.resetFilters = function () {
        state.cat = 'All'; state.diff = 'All'; state.q = '';
        if (searchEl) searchEl.value = '';
        document.querySelectorAll('#cat-tabs .ch-cat-tab').forEach(function (b) {
            b.classList.toggle('active', b.dataset.cat === 'All');
            b.setAttribute('aria-selected', b.dataset.cat === 'All' ? 'true' : 'false');
        });
        document.querySelectorAll('.ch-pill').forEach(function (b) {
            b.classList.toggle('active', b.dataset.diff === 'All');
            b.setAttribute('aria-pressed', b.dataset.diff === 'All' ? 'true' : 'false');
        });
        applyFilters();
    };

    // ── Flash auto-dismiss ─────────────────────────────────
    var flash = document.querySelector('.ca-auto-dismiss');
    if (flash) { setTimeout(function () { flash.style.opacity = '0'; flash.style.transition = 'opacity 0.5s'; setTimeout(function () { flash.style.display = 'none'; }, 500); }, 4000); }

    // ── Init ───────────────────────────────────────────────
    applyFilters();
    sortCards();
}());
</script>
</asp:Content>
