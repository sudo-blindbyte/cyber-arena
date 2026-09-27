<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementForm.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.AnnouncementFormPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server"><%= VM.PageTitle %></asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <div class="p3-page-header">
        <div>
            <h2 class="p3-page-title">
                <i class="fas fa-bullhorn" style="color:var(--ca-accent);" aria-hidden="true"></i>
                <%= VM.PageTitle %>
            </h2>
            <p class="p3-page-subtitle">
                <%= VM.IsEdit ? "Update an existing announcement" : "Publish a new announcement to all participants" %>
            </p>
        </div>
        <a href="<%= ResolveUrl("~/Admin/Announcements.aspx") %>" class="btn btn-secondary" id="btn-cancel-ann-form">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Cancel
        </a>
    </div>

    <div class="row justify-content-center">
        <div class="col-12 col-lg-8">

            <asp:ValidationSummary ID="vsErrors" runat="server" CssClass="alert alert-danger mb-3" HeaderText="<i class='fas fa-circle-exclamation me-2' aria-hidden='true'></i> Please fix the errors below before publishing." DisplayMode="BulletList" />

            <div class="ca-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Announcement Details</h3>
                        <div class="ca-card-subtitle">
                            <%= VM.IsEdit ? $"Editing announcement #{VM.Id}" : "New announcement" %>
                        </div>
                    </div>
                    <% if (VM.IsPinned) { %>
                        <span class="ann-pin-badge">
                            <i class="fas fa-thumbtack" aria-hidden="true"></i>Pinned
                        </span>
                    <% } %>
                </div>

                <div class="mb-4">
                    <label for="txtTitle" class="form-label">Title</label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="200" AutoCompleteType="Disabled" placeholder="Clear, descriptive title…" />
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is required." CssClass="field-validation-error" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="txtTitle" ValidationExpression="^.{3,200}$" ErrorMessage="Title must be 3–200 characters." CssClass="field-validation-error" Display="Dynamic" />
                </div>

                <div class="mb-4">
                    <label for="txtBody" class="form-label">Announcement Body</label>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="8" CssClass="form-control" placeholder="Write the full announcement here. You can use line breaks to separate paragraphs." />
                    <asp:RequiredFieldValidator ID="rfvBody" runat="server" ControlToValidate="txtBody" ErrorMessage="Body is required." CssClass="field-validation-error" Display="Dynamic" />
                    <div class="form-text" style="color:var(--ca-text-muted);font-size:0.75rem;">
                        Line breaks will be rendered as separate paragraphs.
                    </div>
                </div>

                <div class="mb-4">
                    <div class="form-check" style="display:flex;align-items:center;gap:0.75rem;padding:1rem;background:var(--ca-bg-base);border:1px solid var(--ca-border);border-radius:var(--ca-radius);">
                        <asp:CheckBox ID="chkIsPinned" runat="server" CssClass="form-check-input" style="width:1.1em;height:1.1em;margin:0;cursor:pointer;" />
                        <div>
                            <label for="<%= chkIsPinned.ClientID %>" class="form-check-label" style="cursor:pointer;font-weight:600;color:var(--ca-text-primary);margin:0;">
                                <i class="fas fa-thumbtack me-1" style="color:var(--ca-warning);" aria-hidden="true"></i>
                                Pin this announcement
                            </label>
                            <div style="font-size:0.75rem;color:var(--ca-text-muted);">
                                Pinned announcements appear at the top of the list with a highlight.
                            </div>
                        </div>
                    </div>
                </div>

                <div class="d-flex gap-2 justify-content-end">
                    <a href="<%= ResolveUrl("~/Admin/Announcements.aspx") %>" class="btn btn-secondary">
                        Cancel
                    </a>
                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click">
                        <i class="fas <%= VM.IsEdit ? "fa-save" : "fa-bullhorn" %> me-2" aria-hidden="true"></i>
                        <%= VM.SubmitLabel %>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

</div>

</asp:Content>
