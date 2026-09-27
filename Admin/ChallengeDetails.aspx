<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChallengeDetails.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.ChallengeDetailsPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Challenge #<%= VM.Id %> — <%= VM.Title %></asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ─── Breadcrumb + Actions ────────────────────────────── --%>
    <div class="d-flex align-items-center justify-content-between mb-3 flex-wrap gap-2">
        <nav aria-label="breadcrumb" style="font-size:0.82rem;">
            <ol class="breadcrumb mb-0" style="background:none;padding:0;">
                <li class="breadcrumb-item">
                    <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" style="color:var(--ca-text-muted);">
                        <i class="fas fa-shield-halved me-1" aria-hidden="true"></i>Admin
                    </a>
                </li>
                <li class="breadcrumb-item">
                    <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" style="color:var(--ca-text-muted);">Challenges</a>
                </li>
                <li class="breadcrumb-item active" style="color:var(--ca-text-secondary);" aria-current="page">
                    #<%= VM.Id %>
                </li>
            </ol>
        </nav>

        <div class="d-flex gap-2">
            <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>?id=<%= VM.Id %>"
               class="btn btn-primary btn-sm" id="btn-admin-edit">
                <i class="fas fa-pen me-1" aria-hidden="true"></i>Edit
            </a>
            <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>"
               class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Back
            </a>
        </div>
    </div>

    <div class="row g-4">

        <%-- ── Main Info ── --%>
        <div class="col-lg-8">
            <div class="ch-form-card">
                <div class="ch-form-section">
                    <div class="d-flex align-items-center gap-3 mb-3">
                        <div class="ch-card-icon <%= CyberArenaWebForms.Models.ChallengeCategories.CssClass(VM.Category) %>"
                             style="width:44px;height:44px;font-size:1.1rem;" aria-hidden="true">
                            <i class="fas <%= CyberArenaWebForms.Models.ChallengeCategories.Icon(VM.Category) %>"></i>
                        </div>
                        <div>
                            <h2 style="font-family:'Space Grotesk',sans-serif;font-size:1.25rem;font-weight:700;color:var(--ca-text-primary);margin:0;">
                                <%= VM.Title %>
                            </h2>
                            <div class="d-flex gap-2 mt-1">
                                <span class="ch-diff <%= VM.DifficultyLower %>"><%= VM.Difficulty %></span>
                                <span class="adm-status <%= VM.StatusLower %>"><%= VM.Status %></span>
                            </div>
                        </div>
                    </div>

                    <div class="row g-3">
                        <div class="col-sm-6">
                            <div style="background:var(--ca-bg-base);border-radius:var(--ca-radius);padding:1rem;">
                                <div style="font-size:0.72rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.4rem;">Category</div>
                                <div style="font-size:0.9rem;color:var(--ca-text-primary);"><%= VM.Category %></div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div style="background:var(--ca-bg-base);border-radius:var(--ca-radius);padding:1rem;">
                                <div style="font-size:0.72rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.4rem;">Difficulty</div>
                                <span class="ch-diff <%= VM.DifficultyLower %>"><%= VM.Difficulty %></span>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div style="background:var(--ca-bg-base);border-radius:var(--ca-radius);padding:1rem;">
                                <div style="font-size:0.72rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.4rem;">Points</div>
                                <div style="font-family:'Space Grotesk',sans-serif;font-size:1.5rem;font-weight:700;color:var(--ca-accent);"><%= VM.Points %></div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div style="background:var(--ca-bg-base);border-radius:var(--ca-radius);padding:1rem;">
                                <div style="font-size:0.72rem;font-weight:700;text-transform:uppercase;letter-spacing:0.08em;color:var(--ca-text-muted);margin-bottom:0.4rem;">Solvers</div>
                                <div style="font-family:'Space Grotesk',sans-serif;font-size:1.5rem;font-weight:700;color:var(--ca-text-primary);"><%= VM.SolverCount %></div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-calendar-plus" aria-hidden="true"></i> Timeline
                    </div>
                    <div class="d-flex gap-3 flex-wrap">
                        <div>
                            <div style="font-size:0.72rem;color:var(--ca-text-muted);margin-bottom:0.2rem;">Created</div>
                            <div style="font-size:0.875rem;color:var(--ca-text-primary);"><%= VM.CreatedAt.ToString("dd MMM yyyy, HH:mm") %></div>
                        </div>
                        <% if (VM.UpdatedAt.HasValue) { %>
                            <div>
                                <div style="font-size:0.72rem;color:var(--ca-text-muted);margin-bottom:0.2rem;">Last Updated</div>
                                <div style="font-size:0.875rem;color:var(--ca-text-primary);"><%= VM.UpdatedAt.Value.ToString("dd MMM yyyy, HH:mm") %></div>
                            </div>
                        <% } %>
                    </div>
                </div>

                <% if (VM.HasAttachment) { %>
                    <div class="ch-form-section">
                        <div class="ch-form-section-title">
                            <i class="fas fa-paperclip" aria-hidden="true"></i> Attachment
                        </div>
                        <div class="ch-file-item" style="max-width:320px;">
                            <i class="fas fa-file-zipper ch-file-icon" aria-hidden="true"></i>
                            <span class="ch-file-name">challenge_<%= VM.Id %>_files.zip</span>
                            <i class="fas fa-download" style="color:var(--ca-text-muted);font-size:0.8rem;" aria-hidden="true"></i>
                        </div>
                    </div>
                <% } %>
            </div>
        </div>

        <%-- ── Sidebar ── --%>
        <div class="col-lg-4">
            <div class="ch-form-card mb-4">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-bolt" aria-hidden="true"></i> Admin Actions
                    </div>
                    <div class="d-flex flex-column gap-2">
                        <a href="<%= ResolveUrl("~/Admin/ChallengeForm.aspx") %>?id=<%= VM.Id %>"
                           class="btn btn-primary w-100" id="btn-detail-edit">
                            <i class="fas fa-pen me-2" aria-hidden="true"></i>Edit Challenge
                        </a>
                        <a href="<%= ResolveUrl("~/Challenges/ChallengeDetails.aspx") %>?id=<%= VM.Id %>"
                           class="btn btn-secondary w-100" target="_blank" id="btn-detail-preview">
                            <i class="fas fa-eye me-2" aria-hidden="true"></i>Preview as Participant
                        </a>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-outline-danger w-100" OnClientClick="return confirm('Delete this challenge? This cannot be undone.');" OnClick="btnDelete_Click">
                            <i class="fas fa-trash me-2" aria-hidden="true"></i>Delete Challenge
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="ch-form-card">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-chart-bar" aria-hidden="true"></i> Statistics
                    </div>
                    <div class="d-flex flex-column gap-3">
                        <div class="d-flex justify-content-between">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Challenge ID</span>
                            <span style="font-family:'Space Grotesk',sans-serif;font-weight:600;color:var(--ca-text-primary);">#<%= VM.Id %></span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Points</span>
                            <span style="font-family:'Space Grotesk',sans-serif;font-weight:700;color:var(--ca-accent);"><%= VM.Points %> pts</span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Solvers</span>
                            <span style="font-weight:600;color:var(--ca-text-primary);"><%= VM.SolverCount %></span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Status</span>
                            <span class="adm-status <%= VM.StatusLower %>"><%= VM.Status %></span>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Attachment</span>
                            <span style="font-size:0.82rem;color:var(--ca-text-primary);"><%= VM.HasAttachment ? "Yes" : "No" %></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</div>

</asp:Content>
