<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs"
         Inherits="CyberArenaWebForms.Account.ProfilePage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">My Profile</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ─── Flash Messages ────────────────────────────────────── --%>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success d-flex align-items-center gap-2 mb-3">
        <i class="fas fa-circle-check fa-fw" aria-hidden="true"></i>
        <asp:Literal ID="litSuccessMsg" runat="server"></asp:Literal>
    </asp:Panel>
    
    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger d-flex align-items-center gap-2 mb-3">
        <i class="fas fa-circle-exclamation fa-fw" aria-hidden="true"></i>
        <asp:Literal ID="litErrorMsg" runat="server"></asp:Literal>
    </asp:Panel>

    <%-- ─── Profile Hero Card ────────────────────────────────── --%>
    <div class="ca-profile-hero mb-4">
        <div class="ca-profile-avatar">
            <% if (!string.IsNullOrEmpty(VM.AvatarUrl)) { %>
                <img src="<%= VM.AvatarUrl %>" alt="<%= VM.Username %> avatar" />
            <% } else { %>
                <%= VM.AvatarInitials %>
            <% } %>
        </div>

        <div class="ca-profile-info">
            <h2 class="ca-profile-name"><%= VM.FullName ?? VM.Username %></h2>
            <p class="ca-profile-username">
                <i class="fas fa-at me-1" style="color:var(--ca-text-muted);" aria-hidden="true"></i><%= VM.Username %>
                <% if (!string.IsNullOrEmpty(VM.TeamName)) { %>
                    <span class="ms-2">
                        <i class="fas fa-users me-1" style="color:var(--ca-text-muted);" aria-hidden="true"></i>
                        <span class="ca-tag easy"><%= VM.TeamRole %></span>
                        <span style="color:var(--ca-text-muted);"><%= VM.TeamName %></span>
                    </span>
                <% } %>
            </p>
            <div class="ca-profile-badges">
                <span class="ca-tag easy"><i class="fas fa-shield-halved me-1" aria-hidden="true"></i>Participant</span>
                <% if (VM.CompetitionsWon > 0) { %>
                    <span class="ca-tag medium"><i class="fas fa-trophy me-1" aria-hidden="true"></i><%= VM.CompetitionsWon %>× Winner</span>
                <% } %>
                <% if (VM.ChallengesSolved >= 30) { %>
                    <span class="ca-tag hard"><i class="fas fa-fire me-1" aria-hidden="true"></i>Elite Hacker</span>
                <% } %>
                <span class="ca-tag" style="background:var(--ca-bg-base);color:var(--ca-text-muted);">
                    <i class="fas fa-calendar-plus me-1" aria-hidden="true"></i>
                    Joined <%= VM.JoinDate.ToString("MMM yyyy") %>
                </span>
            </div>

            <% if (!string.IsNullOrEmpty(VM.Bio)) { %>
                <p class="mt-2 mb-0" style="font-size:0.875rem;color:var(--ca-text-secondary);max-width:500px;"><%= VM.Bio %></p>
            <% } %>

            <%-- Social links --%>
            <div class="d-flex align-items-center gap-3 mt-2">
                <% if (!string.IsNullOrEmpty(VM.Location)) { %>
                    <span style="font-size:0.8rem;color:var(--ca-text-muted);">
                        <i class="fas fa-location-dot me-1" aria-hidden="true"></i><%= VM.Location %>
                    </span>
                <% } %>
                <% if (!string.IsNullOrEmpty(VM.Website)) { %>
                    <a href="<%= VM.Website %>" target="_blank" rel="noopener noreferrer"
                       style="font-size:0.8rem;color:var(--ca-accent);">
                        <i class="fas fa-globe me-1" aria-hidden="true"></i>Website
                    </a>
                <% } %>
                <% if (!string.IsNullOrEmpty(VM.GitHubHandle)) { %>
                    <a href="https://github.com/<%= VM.GitHubHandle %>" target="_blank" rel="noopener noreferrer"
                       style="font-size:0.8rem;color:var(--ca-text-secondary);">
                        <i class="fab fa-github me-1" aria-hidden="true"></i>@<%= VM.GitHubHandle %>
                    </a>
                <% } %>
            </div>
        </div>

        <%-- Stats --%>
        <div class="ca-profile-stats d-none d-md-flex">
            <div class="ca-profile-stat">
                <span class="ca-profile-stat-value" data-count="<%= VM.TotalScore %>"><%= VM.TotalScore.ToString("N0") %></span>
                <span class="ca-profile-stat-label">Score</span>
            </div>
            <div class="ca-profile-stat">
                <span class="ca-profile-stat-value">#<%= VM.CurrentRank %></span>
                <span class="ca-profile-stat-label">Global Rank</span>
            </div>
            <div class="ca-profile-stat">
                <span class="ca-profile-stat-value" data-count="<%= VM.ChallengesSolved %>"><%= VM.ChallengesSolved %></span>
                <span class="ca-profile-stat-label">Solved</span>
            </div>
        </div>
    </div>

    <%-- ─── Stats Row (mobile) ───────────────────────────────── --%>
    <div class="row g-3 mb-4 d-md-none">
        <div class="col-4">
            <div class="ca-stat-card accent text-center p-3">
                <div>
                    <div class="ca-stat-label">Score</div>
                    <div class="ca-stat-value" style="font-size:1.4rem;"><%= VM.TotalScore.ToString("N0") %></div>
                </div>
            </div>
        </div>
        <div class="col-4">
            <div class="ca-stat-card info text-center p-3">
                <div>
                    <div class="ca-stat-label">Rank</div>
                    <div class="ca-stat-value" style="font-size:1.4rem;">#<%= VM.CurrentRank %></div>
                </div>
            </div>
        </div>
        <div class="col-4">
            <div class="ca-stat-card success text-center p-3">
                <div>
                    <div class="ca-stat-label">Solved</div>
                    <div class="ca-stat-value" style="font-size:1.4rem;"><%= VM.ChallengesSolved %></div>
                </div>
            </div>
        </div>
    </div>

    <%-- ─── Main Grid ────────────────────────────────────────── --%>
    <div class="row g-4">

        <%-- ── Left Column ── --%>
        <div class="col-lg-8">

            <%-- Edit Profile Card --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">
                            <i class="fas fa-pen-to-square me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                            Edit Profile
                        </h3>
                    </div>
                </div>

                <div class="row g-3">
                    <div class="col-md-6">
                        <label for="txtFullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Your real name (optional)" />
                        <asp:RegularExpressionValidator ID="revFullName" runat="server" ControlToValidate="txtFullName" ValidationExpression="^.{0,100}$" ErrorMessage="Full name cannot exceed 100 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Email</label>
                        <input type="email"
                               class="form-control"
                               value="<%= VM.Email %>"
                               disabled
                               style="opacity:0.6;cursor:not-allowed;"
                               title="Email cannot be changed here. Contact support." />
                    </div>
                    <div class="col-12">
                        <label for="txtBio" class="form-label">Bio</label>
                        <asp:TextBox ID="txtBio" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Tell others about yourself..." MaxLength="300" />
                        <div class="d-flex justify-content-end mt-1">
                            <small class="text-muted" id="bio-count">0/300</small>
                        </div>
                        <asp:RegularExpressionValidator ID="revBio" runat="server" ControlToValidate="txtBio" ValidationExpression="^.{0,300}$" ErrorMessage="Bio cannot exceed 300 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtLocation" class="form-label">Location</label>
                        <div class="input-group">
                            <span class="input-group-text" style="background:var(--ca-bg-card);border-color:var(--ca-border);color:var(--ca-text-muted);">
                                <i class="fas fa-location-dot" aria-hidden="true"></i>
                            </span>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="City, Country" />
                        </div>
                        <asp:RegularExpressionValidator ID="revLocation" runat="server" ControlToValidate="txtLocation" ValidationExpression="^.{0,100}$" ErrorMessage="Location cannot exceed 100 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtWebsite" class="form-label">Website</label>
                        <div class="input-group">
                            <span class="input-group-text" style="background:var(--ca-bg-card);border-color:var(--ca-border);color:var(--ca-text-muted);">
                                <i class="fas fa-globe" aria-hidden="true"></i>
                            </span>
                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="https://yoursite.com" />
                        </div>
                        <asp:RegularExpressionValidator ID="revWebsite" runat="server" ControlToValidate="txtWebsite" ValidationExpression="^(https?://)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$" ErrorMessage="Invalid website URL format." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtGitHub" class="form-label">GitHub Username</label>
                        <div class="input-group">
                            <span class="input-group-text" style="background:var(--ca-bg-card);border-color:var(--ca-border);color:var(--ca-text-muted);">@</span>
                            <asp:TextBox ID="txtGitHub" runat="server" CssClass="form-control" placeholder="yourgithub" />
                        </div>
                        <asp:RegularExpressionValidator ID="revGitHub" runat="server" ControlToValidate="txtGitHub" ValidationExpression="^[a-zA-Z0-9-]{1,39}$" ErrorMessage="Invalid GitHub username format." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                </div>

                <div class="d-flex gap-2 mt-4">
                    <asp:LinkButton ID="btnSaveProfile" runat="server" CssClass="btn btn-primary" OnClick="btnSaveProfile_Click" ValidationGroup="ProfileGroup">
                        <i class="fas fa-floppy-disk me-2" aria-hidden="true"></i>Save Changes
                    </asp:LinkButton>
                    <a href="<%= ResolveUrl("~/Account/Profile.aspx") %>" class="btn btn-secondary">
                        Cancel
                    </a>
                </div>
            </div>

            <%-- Change Password --%>
            <div class="ca-card">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">
                        <i class="fas fa-key me-2" style="color:var(--ca-warning);" aria-hidden="true"></i>
                        Change Password
                    </h3>
                </div>

                <div class="row g-3">
                    <div class="col-12">
                        <label for="txtCurrentPassword" class="form-label">Current Password</label>
                        <div class="ca-input-group">
                            <span class="ca-input-icon"><i class="fas fa-lock" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Your current password" />
                            <button class="ca-pwd-toggle" type="button" data-target="<%= txtCurrentPassword.ClientID %>" aria-label="Show/hide">
                                <i class="fas fa-eye" aria-hidden="true"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvCurrentPwd" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="Current password is required." CssClass="field-validation-error d-block mt-1" ValidationGroup="PwdGroup" Display="Dynamic" />
                    </div>
                    <div class="col-md-6">
                        <label for="txtNewPassword" class="form-label">New Password</label>
                        <div class="ca-input-group">
                            <span class="ca-input-icon"><i class="fas fa-lock-open" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Min. 8 characters" data-pwd-strength="cp-strength" />
                            <button class="ca-pwd-toggle" type="button" data-target="<%= txtNewPassword.ClientID %>" aria-label="Show/hide">
                                <i class="fas fa-eye" aria-hidden="true"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New password is required." CssClass="field-validation-error d-block mt-1" ValidationGroup="PwdGroup" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revNewPwd" runat="server" ControlToValidate="txtNewPassword" ValidationExpression="^.{8,}$" ErrorMessage="Password must be at least 8 characters long." CssClass="field-validation-error d-block mt-1" ValidationGroup="PwdGroup" Display="Dynamic" />
                        
                        <div class="ca-pwd-strength mt-2" id="cp-strength" aria-live="polite">
                            <div class="ca-pwd-strength-bar"></div>
                            <div class="ca-pwd-strength-bar"></div>
                            <div class="ca-pwd-strength-bar"></div>
                            <div class="ca-pwd-strength-bar"></div>
                            <span class="ca-pwd-strength-label"></span>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label for="txtConfirmPassword" class="form-label">Confirm New Password</label>
                        <div class="ca-input-group">
                            <span class="ca-input-icon"><i class="fas fa-lock-open" aria-hidden="true"></i></span>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Repeat new password" />
                            <button class="ca-pwd-toggle" type="button" data-target="<%= txtConfirmPassword.ClientID %>" aria-label="Show/hide">
                                <i class="fas fa-eye" aria-hidden="true"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm password is required." CssClass="field-validation-error d-block mt-1" ValidationGroup="PwdGroup" Display="Dynamic" />
                        <asp:CompareValidator ID="cvConfirmPwd" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ErrorMessage="Passwords do not match." CssClass="field-validation-error d-block mt-1" ValidationGroup="PwdGroup" Display="Dynamic" />
                    </div>
                </div>

                <div class="mt-4">
                    <asp:LinkButton ID="btnChangePassword" runat="server" CssClass="btn btn-primary" OnClick="btnChangePassword_Click" ValidationGroup="PwdGroup">
                        <i class="fas fa-rotate me-2" aria-hidden="true"></i>Update Password
                    </asp:LinkButton>
                </div>
            </div>

        </div>

        <%-- ── Right Column ── --%>
        <div class="col-lg-4">

            <%-- Account Info --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">Account Info</h3>
                </div>
                <div class="d-flex flex-column gap-3">
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Username</span>
                        <span style="font-size:0.875rem;font-weight:600;color:var(--ca-text-primary);"><%= VM.Username %></span>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Email</span>
                        <span style="font-size:0.875rem;color:var(--ca-text-secondary);"><%= VM.Email %></span>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Team</span>
                        <span style="font-size:0.875rem;color:var(--ca-text-primary);">
                            <%= VM.TeamName ?? "— No team" %>
                        </span>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Team role</span>
                        <span class="ca-tag <%= VM.TeamRole == "Captain" ? "medium" : "easy" %>"><%= VM.TeamRole ?? "—" %></span>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Member since</span>
                        <span style="font-size:0.875rem;color:var(--ca-text-secondary);"><%= VM.JoinDate.ToString("dd MMM yyyy") %></span>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Last active</span>
                        <span style="font-size:0.875rem;color:var(--ca-text-secondary);">
                            <%= VM.LastActiveDate?.ToString("dd MMM, HH:mm") ?? "—" %>
                        </span>
                    </div>
                </div>
            </div>

            <%-- CTF Stats --%>
            <div class="ca-card mb-4">
                <div class="ca-card-header">
                    <h3 class="ca-card-title">CTF Statistics</h3>
                </div>
                <div class="d-flex flex-column gap-3">

                    <div>
                        <div class="d-flex justify-content-between mb-1">
                            <span style="font-size:0.82rem;color:var(--ca-text-muted);">Challenges Solved</span>
                            <span style="font-size:0.82rem;font-weight:600;color:var(--ca-text-primary);"><%= VM.ChallengesSolved %> / <%= VM.TotalChallenges %></span>
                        </div>
                        <div class="ca-progress">
                            <div class="ca-progress-bar" style="width:<%= (int)((double)VM.ChallengesSolved / VM.TotalChallenges * 100) %>%"></div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-between">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Total Score</span>
                        <span style="font-size:0.875rem;font-weight:700;color:var(--ca-accent);"><%= VM.TotalScore.ToString("N0") %> pts</span>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Global Rank</span>
                        <span style="font-size:0.875rem;font-weight:700;color:var(--ca-text-primary);">#<%= VM.CurrentRank %> of <%= VM.TotalParticipants %></span>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Competitions Entered</span>
                        <span style="font-size:0.875rem;font-weight:600;color:var(--ca-text-primary);"><%= VM.CompetitionsEntered %></span>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span style="font-size:0.82rem;color:var(--ca-text-muted);">Competitions Won</span>
                        <span class="ca-tag medium">
                            <i class="fas fa-trophy me-1" aria-hidden="true"></i><%= VM.CompetitionsWon %>
                        </span>
                    </div>
                </div>
            </div>

            <%-- Danger Zone --%>
            <div class="ca-card" style="border-color:rgba(239,68,68,0.2);">
                <div class="ca-card-header">
                    <h3 class="ca-card-title" style="color:var(--ca-danger);">
                        <i class="fas fa-triangle-exclamation me-2" aria-hidden="true"></i>Danger Zone
                    </h3>
                </div>
                <p style="font-size:0.82rem;color:var(--ca-text-muted);margin-bottom:1rem;">
                    These actions are irreversible. Please be certain before proceeding.
                </p>
                <button type="button" class="btn btn-outline-danger btn-sm w-100" disabled
                        title="This feature will be enabled by the backend team"
                        data-bs-toggle="tooltip" data-bs-placement="top">
                    <i class="fas fa-trash me-2" aria-hidden="true"></i>Delete Account
                </button>
            </div>

        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    // Bio character counter
    var bioEl = document.getElementById('<%= txtBio.ClientID %>');
    var bioCount = document.getElementById('bio-count');
    if (bioEl && bioCount) {
        bioCount.textContent = bioEl.value.length + '/300';
        bioEl.addEventListener('input', function () {
            bioCount.textContent = bioEl.value.length + '/300';
        });
    }

    // Password toggle
    document.querySelectorAll('.ca-pwd-toggle').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var input = document.getElementById(this.dataset.target);
            var icon = this.querySelector('i');
            if (input.type === 'password') {
                input.type = 'text';
                icon.className = 'fas fa-eye-slash';
            } else {
                input.type = 'password';
                icon.className = 'fas fa-eye';
            }
        });
    });
}());
</script>
</asp:Content>
