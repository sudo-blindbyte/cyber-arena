<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcements.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.AnnouncementsPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Manage Announcements</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <div class="adm-header">
        <div>
            <h2 class="adm-title">
                <i class="fas fa-bullhorn" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Announcements
            </h2>
            <p class="adm-subtitle">Create, edit, and manage platform announcements</p>
        </div>
        <div class="d-flex gap-2 flex-wrap">
            <a href="<%= ResolveUrl("~/Admin/Admin.aspx") %>" class="btn btn-secondary btn-sm" id="btn-back-adm">
                <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Dashboard
            </a>
            <a href="<%= ResolveUrl("~/Admin/AnnouncementForm.aspx") %>" class="btn btn-primary" id="btn-new-ann">
                <i class="fas fa-plus me-2" aria-hidden="true"></i>New Announcement
            </a>
        </div>
    </div>

    <%-- ─── Flash Message ────────────────────────────────────── --%>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success mb-3">
        <i class="fas fa-check-circle me-2" aria-hidden="true"></i>
        <asp:Literal ID="litSuccessMsg" runat="server"></asp:Literal>
    </asp:Panel>

    <%-- ─── Stats strip ────────────────────────────────────────── --%>
    <div class="adm-stat-row mb-3">
        <div class="adm-stat-chip">
            <i class="fas fa-bullhorn" style="color:var(--ca-accent);" aria-hidden="true"></i>
            <strong><%= VM.Announcements.Count %></strong>
            <span>Total</span>
        </div>
        <div class="adm-stat-chip">
            <i class="fas fa-thumbtack" style="color:var(--ca-warning);" aria-hidden="true"></i>
            <strong><%= VM.Pinned.Count %></strong>
            <span>Pinned</span>
        </div>
        <div class="adm-stat-chip">
            <i class="fas fa-bell" style="color:var(--ca-success);" aria-hidden="true"></i>
            <strong><%= VM.Regular.Count %></strong>
            <span>Regular</span>
        </div>
    </div>

    <%-- ─── Announcements Table ────────────────────────────────── --%>
    <div class="adm-table-card">
        <div class="adm-table-toolbar">
            <span style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">All Announcements</span>
        </div>
        <div class="table-responsive">
            <table class="table mb-0" aria-label="Admin announcements table">
                <thead>
                    <tr>
                        <th style="width:40px;">ID</th>
                        <th>Title</th>
                        <th>Author</th>
                        <th class="text-center">Pinned</th>
                        <th>Published</th>
                        <th class="text-center" style="width:100px;">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptAnnouncements" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="color:var(--ca-text-muted);font-size:0.82rem;font-family:'Space Grotesk',sans-serif;">
                                    #<%# Eval("Id") %>
                                </td>
                                <td>
                                    <div style="font-weight:600;font-size:0.875rem;color:var(--ca-text-primary);">
                                        <%# Eval("Title") %>
                                    </div>
                                    <div style="font-size:0.75rem;color:var(--ca-text-muted);"><%# Eval("Excerpt") %></div>
                                </td>
                                <td style="font-size:0.82rem;color:var(--ca-text-muted);"><%# Eval("AuthorName") %></td>
                                <td class="text-center">
                                    <%# (bool)Eval("IsPinned") 
                                        ? "<i class=\"fas fa-thumbtack\" style=\"color:var(--ca-warning);\" title=\"Pinned\" aria-label=\"Pinned\"></i>" 
                                        : "<span style=\"color:var(--ca-border);\">—</span>" %>
                                </td>
                                <td style="font-size:0.78rem;color:var(--ca-text-muted);white-space:nowrap;">
                                    <%# Convert.ToDateTime(Eval("PublishedAt")).ToString("dd MMM yyyy") %>
                                </td>
                                <td class="text-center">
                                    <div class="adm-actions justify-content-center">
                                        <a href="#" class="adm-action-btn view" title="View public" aria-label="View announcement <%# Eval("Id") %>"
                                           id="adm-ann-view-<%# Eval("Id") %>">
                                            <i class="fas fa-eye" aria-hidden="true"></i>
                                        </a>
                                        <a href="<%= ResolveUrl("~/Admin/AnnouncementForm.aspx") %>?id=<%# Eval("Id") %>"
                                           class="adm-action-btn edit" title="Edit" aria-label="Edit announcement <%# Eval("Id") %>"
                                           id="adm-ann-edit-<%# Eval("Id") %>">
                                            <i class="fas fa-pen" aria-hidden="true"></i>
                                        </a>
                                        <button type="button" class="adm-action-btn delete"
                                                title="Delete" aria-label="Delete announcement <%# Eval("Id") %>"
                                                id="adm-ann-delete-<%# Eval("Id") %>"
                                                onclick="return confirmAnnDelete('<%# Eval("Title").ToString().Replace("'", "\\'") %>', <%# Eval("Id") %>)">
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
        <div style="padding:0.75rem 1.25rem;border-top:1px solid var(--ca-border-light);display:flex;justify-content:space-between;align-items:center;">
            <span style="font-size:0.78rem;color:var(--ca-text-muted);">
                <%= VM.Announcements.Count %> announcements total
            </span>
            <a href="<%= ResolveUrl("~/Admin/AnnouncementForm.aspx") %>" class="btn btn-primary btn-sm">
                <i class="fas fa-plus me-1" aria-hidden="true"></i>Add Announcement
            </a>
        </div>
    </div>

</div>

<%-- Delete confirmation modal & logic --%>
<asp:HiddenField ID="hfDeleteId" runat="server" />
<asp:Button ID="btnHiddenDelete" runat="server" CssClass="d-none" OnClick="btnHiddenDelete_Click" />

<div class="modal fade" id="annDeleteModal" tabindex="-1" aria-labelledby="annDeleteModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" style="max-width:420px;">
        <div class="modal-content" style="background:var(--ca-bg-card);border:1px solid var(--ca-border);border-radius:var(--ca-radius-xl);">
            <div class="modal-header border-0 pb-0">
                <h5 class="modal-title" id="annDeleteModalLabel" style="color:var(--ca-text-primary);">
                    <i class="fas fa-triangle-exclamation me-2" style="color:var(--ca-danger);" aria-hidden="true"></i>
                    Confirm Delete
                </h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" style="color:var(--ca-text-secondary);">
                Delete <strong id="annDeleteTitle" style="color:var(--ca-text-primary);">this announcement</strong>?
                <br /><small class="text-muted mt-2 d-block">This action cannot be undone.</small>
            </div>
            <div class="modal-footer border-0">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <button type="button" class="btn btn-danger" id="confirmAnnDeleteBtn">
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
    var modalElement = document.getElementById('annDeleteModal');
    if (!modalElement) return;
    var modal = new bootstrap.Modal(modalElement);
    
    window.confirmAnnDelete = function (title, id) {
        document.getElementById('annDeleteTitle').textContent = '"' + title + '"';
        document.getElementById('<%= hfDeleteId.ClientID %>').value = id;
        modal.show();
        return false;
    };

    document.getElementById('confirmAnnDeleteBtn').addEventListener('click', function () {
        modal.hide();
        document.getElementById('<%= btnHiddenDelete.ClientID %>').click();
    });
}());
</script>
</asp:Content>
