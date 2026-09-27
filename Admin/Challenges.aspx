<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Challenges.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.ChallengesPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Challenge Management</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<%-- ─── Flash Messages ──────────────────────────────────────── --%>
<asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success ca-auto-dismiss d-flex align-items-center gap-2 mb-3">
    <i class="fas fa-circle-check fa-fw" aria-hidden="true"></i>
    <asp:Literal ID="litSuccessMsg" runat="server"></asp:Literal>
</asp:Panel>

<div class="stagger-children">

    <%-- ─── Page Header ─────────────────────────────────────── --%>
    <div class="adm-header">
        <div>
            <h2 class="adm-title">
                <i class="fas fa-shield-halved me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Challenge Management
            </h2>
            <p class="adm-subtitle">Create, edit, and manage all CTF challenges</p>
        </div>
        <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>"
           class="btn btn-primary" id="btn-create-challenge">
            <i class="fas fa-plus me-2" aria-hidden="true"></i>Create Challenge
        </a>
    </div>

    <%-- ─── Admin Stats ──────────────────────────────────────── --%>
    <div class="adm-stat-row">
        <div class="adm-stat-chip">
            <i class="fas fa-flag" style="color:var(--ca-accent);" aria-hidden="true"></i>
            <strong><%= VM.TotalCount %></strong>
            <span>Total</span>
        </div>
        <div class="adm-stat-chip">
            <i class="fas fa-circle-check" style="color:var(--ca-success);" aria-hidden="true"></i>
            <strong><%= VM.ActiveCount %></strong>
            <span>Active</span>
        </div>
        <div class="adm-stat-chip">
            <i class="fas fa-pen-ruler" style="color:var(--ca-warning);" aria-hidden="true"></i>
            <strong><%= VM.DraftCount %></strong>
            <span>Draft</span>
        </div>
        <div class="adm-stat-chip">
            <i class="fas fa-box-archive" style="color:var(--ca-text-muted);" aria-hidden="true"></i>
            <strong><%= VM.ArchivedCount %></strong>
            <span>Archived</span>
        </div>
    </div>

    <%-- ─── Table Card ──────────────────────────────────────── --%>
    <div class="adm-table-card">

        <%-- Toolbar --%>
        <div class="adm-table-toolbar">
            <%-- Search --%>
            <div class="ch-search-wrap" style="min-width:220px;flex:1;">
                <span class="ch-search-icon"><i class="fas fa-magnifying-glass" aria-hidden="true"></i></span>
                <input type="search"
                       id="adm-search"
                       class="ch-search-input"
                       placeholder="Search challenges…"
                       value="<%= VM.SearchQuery %>"
                       aria-label="Search admin challenges" />
            </div>

            <%-- Category filter --%>
            <select class="ch-sort-select" id="adm-cat-filter" aria-label="Filter by category" style="min-width:140px;">
                <option value="All">All Categories</option>
                <% foreach (var cat in CyberArenaWebForms.Models.ChallengeCategories.All) { %>
                    <option value="<%= cat %>" <%= VM.FilterCategory == cat ? "selected" : "" %>><%= cat %></option>
                <% } %>
            </select>

            <%-- Status filter --%>
            <select class="ch-sort-select" id="adm-status-filter" aria-label="Filter by status" style="min-width:120px;">
                <option value="All">All Status</option>
                <% foreach (var s in CyberArenaWebForms.Models.ChallengeStatus.All) { %>
                    <option value="<%= s %>" <%= VM.FilterStatus == s ? "selected" : "" %>><%= s %></option>
                <% } %>
            </select>

            <span style="font-size:0.82rem;color:var(--ca-text-muted);white-space:nowrap;" id="adm-count-label" aria-live="polite">
                <strong id="adm-visible-count"><%= VM.TotalCount %></strong> challenges
            </span>
        </div>

        <%-- Table --%>
        <div class="table-responsive">
            <table class="table mb-0" aria-label="Admin challenges table">
                <thead>
                    <tr>
                        <th style="width:50px;">ID</th>
                        <th>Title</th>
                        <th>Category</th>
                        <th>Difficulty</th>
                        <th class="text-center">Points</th>
                        <th class="text-center">Solvers</th>
                        <th class="text-center">Status</th>
                        <th>Created</th>
                        <th class="text-center" style="width:110px;">Actions</th>
                    </tr>
                </thead>
                <tbody id="adm-table-body">
                    <asp:Repeater ID="rptChallenges" runat="server">
                        <ItemTemplate>
                            <tr class="adm-table-row"
                                data-title="<%# Eval("Title").ToString().ToLower() %>"
                                data-category="<%# Eval("Category") %>"
                                data-status="<%# Eval("Status") %>">
                                <td style="color:var(--ca-text-muted);font-size:0.82rem;font-family:'Space Grotesk',sans-serif;">
                                    #<%# Eval("Id") %>
                                </td>
                                <td>
                                    <div class="d-flex align-items-center gap-2">
                                        <div class="ch-card-icon <%# CyberArenaWebForms.Models.ChallengeCategories.CssClass(Eval("Category").ToString()) %>"
                                             style="width:28px;height:28px;font-size:0.75rem;flex-shrink:0;"
                                             aria-hidden="true">
                                            <i class="fas <%# CyberArenaWebForms.Models.ChallengeCategories.Icon(Eval("Category").ToString()) %>"></i>
                                        </div>
                                        <div>
                                            <span style="font-weight:600;font-size:0.875rem;"><%# Eval("Title") %></span>
                                            <%# (bool)Eval("HasAttachment") 
                                                ? "<span class=\"ms-1\" title=\"Has attachment\"><i class=\"fas fa-paperclip\" style=\"color:var(--ca-text-muted);font-size:0.72rem;\" aria-hidden=\"true\"></i></span>" 
                                                : "" %>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <span style="font-size:0.82rem;color:var(--ca-text-muted);"><%# Eval("Category") %></span>
                                </td>
                                <td>
                                    <span class="ch-diff <%# Eval("DifficultyLower") %>"><%# Eval("Difficulty") %></span>
                                </td>
                                <td class="text-center">
                                    <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);"><%# Eval("Points") %></span>
                                </td>
                                <td class="text-center" style="color:var(--ca-text-muted);font-size:0.875rem;">
                                    <%# Eval("SolverCount") %>
                                </td>
                                <td class="text-center">
                                    <span class="adm-status <%# Eval("StatusLower") %>"><%# Eval("Status") %></span>
                                </td>
                                <td style="color:var(--ca-text-muted);font-size:0.78rem;white-space:nowrap;">
                                    <%# Convert.ToDateTime(Eval("CreatedAt")).ToString("dd MMM yy") %>
                                    <%# Eval("UpdatedAt") != null 
                                        ? "<br /><span style=\"font-size:0.72rem;opacity:0.7;\">Edited " + Convert.ToDateTime(Eval("UpdatedAt")).ToString("dd MMM yy") + "</span>"
                                        : "" %>
                                </td>
                                <td class="text-center">
                                    <div class="adm-actions justify-content-center">
                                        <a href="<%= ResolveUrl("~/Admin/ChallengeDetails.aspx") %>?id=<%# Eval("Id") %>"
                                           class="adm-action-btn view"
                                           title="View"
                                           aria-label="View <%# Eval("Title") %>"
                                           id="adm-view-<%# Eval("Id") %>">
                                            <i class="fas fa-eye" aria-hidden="true"></i>
                                        </a>
                                        <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>?id=<%# Eval("Id") %>"
                                           class="adm-action-btn edit"
                                           title="Edit"
                                           aria-label="Edit <%# Eval("Title") %>"
                                           id="adm-edit-<%# Eval("Id") %>">
                                            <i class="fas fa-pen" aria-hidden="true"></i>
                                        </a>
                                        <button type="button"
                                                class="adm-action-btn delete"
                                                title="Delete"
                                                aria-label="Delete <%# Eval("Title") %>"
                                                id="adm-delete-<%# Eval("Id") %>"
                                                onclick="return confirmDelete('<%# Eval("Title").ToString().Replace("'", "\\'") %>', <%# Eval("Id") %>)">
                                            <i class="fas fa-trash" aria-hidden="true"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <%-- Empty state --%>
        <div class="ch-empty d-none" id="adm-empty" role="status" aria-live="polite">
            <i class="fas fa-flag" aria-hidden="true"></i>
            <h4>No challenges found</h4>
            <p>Try adjusting your search or filters.</p>
        </div>

        <%-- Table footer --%>
        <div style="padding:0.75rem 1.25rem;border-top:1px solid var(--ca-border-light);display:flex;align-items:center;justify-content:space-between;flex-wrap:wrap;gap:0.5rem;">
            <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                Showing <span id="adm-count-footer"><%= VM.TotalCount %></span> of <%= VM.TotalCount %> challenges
            </span>
            <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>"
               class="btn btn-primary btn-sm">
                <i class="fas fa-plus me-1" aria-hidden="true"></i>Add Challenge
            </a>
        </div>
    </div>

</div>

<%-- ─── Delete confirmation modal & logic ──────────────────── --%>
<asp:HiddenField ID="hfDeleteId" runat="server" />
<asp:Button ID="btnHiddenDelete" runat="server" CssClass="d-none" OnClick="btnHiddenDelete_Click" />

<div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width:420px;">
        <div class="modal-content" style="background:var(--ca-bg-card);border:1px solid var(--ca-border);border-radius:var(--ca-radius-xl);">
            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title" id="deleteModalLabel" style="color:var(--ca-text-primary);">
                    <i class="fas fa-triangle-exclamation me-2" style="color:var(--ca-danger);" aria-hidden="true"></i>
                    Confirm Delete
                </h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" style="color:var(--ca-text-secondary);">
                Are you sure you want to delete <strong id="deleteModalTitle" style="color:var(--ca-text-primary);">this challenge</strong>?
                <br /><small class="text-muted mt-2 d-block">This action cannot be undone.</small>
            </div>
            <div class="modal-footer border-0">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <button type="button" class="btn btn-danger" id="confirmDeleteBtn">
                    <i class="fas fa-trash me-1" aria-hidden="true"></i>Delete
                </button>
            </div>
        </div>
    </div>
</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    // ── Client-side filter ───────────────────────────────
    var rows    = document.querySelectorAll('.adm-table-row');
    var empty   = document.getElementById('adm-empty');
    var countEl = document.getElementById('adm-visible-count');
    var footerCount = document.getElementById('adm-count-footer');

    function applyAdmFilters() {
        var q   = (document.getElementById('adm-search').value || '').toLowerCase().trim();
        var cat = document.getElementById('adm-cat-filter').value;
        var st  = document.getElementById('adm-status-filter').value;
        var vis = 0;

        rows.forEach(function (row) {
            var title    = row.dataset.title    || '';
            var category = row.dataset.category || '';
            var status   = row.dataset.status   || '';
            var show = (!q || title.includes(q))
                    && (cat === 'All' || category === cat)
                    && (st  === 'All' || status   === st);
            row.style.display = show ? '' : 'none';
            if (show) vis++;
        });

        if (empty) {
            if (vis > 0) empty.classList.add('d-none');
            else empty.classList.remove('d-none');
        }
        if (countEl)      countEl.textContent      = vis;
        if (footerCount)  footerCount.textContent  = vis;
    }

    var searchEl = document.getElementById('adm-search');
    var catEl = document.getElementById('adm-cat-filter');
    var statusEl = document.getElementById('adm-status-filter');
    
    if (searchEl) searchEl.addEventListener('input', applyAdmFilters);
    if (catEl) catEl.addEventListener('change', applyAdmFilters);
    if (statusEl) statusEl.addEventListener('change', applyAdmFilters);

    // ── Delete confirmation ───────────────────────────────
    var modalElement = document.getElementById('deleteModal');
    if (!modalElement) return;
    var modal = new bootstrap.Modal(modalElement);

    window.confirmDelete = function (title, id) {
        document.getElementById('deleteModalTitle').textContent = '"' + title + '"';
        document.getElementById('<%= hfDeleteId.ClientID %>').value = id;
        modal.show();
        return false;
    };

    document.getElementById('confirmDeleteBtn').addEventListener('click', function () {
        modal.hide();
        document.getElementById('<%= btnHiddenDelete.ClientID %>').click();
    });
}());
</script>
</asp:Content>
