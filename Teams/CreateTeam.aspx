<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTeam.aspx.cs"
         Inherits="CyberArenaWebForms.Teams.CreateTeamPage"
         MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Create Team</asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/part3.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <div class="p3-page-header">
        <div>
            <h2 class="p3-page-title">
                <i class="fas fa-users" style="color:var(--ca-accent);" aria-hidden="true"></i>
                Create Team
            </h2>
            <p class="p3-page-subtitle">Assemble your crew and dominate the leaderboard</p>
        </div>
        <a href="<%= ResolveUrl("~/Teams/Teams.aspx") %>" class="btn btn-secondary">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Back to Teams
        </a>
    </div>

    <div class="row justify-content-center">
        <div class="col-12 col-lg-7">
            <div class="ca-card">
                <div class="ca-card-header">
                    <div>
                        <h3 class="ca-card-title">Team Details</h3>
                        <div class="ca-card-subtitle">You will automatically become the team captain</div>
                    </div>
                    <i class="fas fa-shield-halved" style="color:var(--ca-accent);font-size:1.3rem;" aria-hidden="true"></i>
                </div>

                <%-- Form fields --%>
                <div class="mb-4">
                    <label for="txtName" class="form-label">Team Name</label>
                    <asp:TextBox ID="txtName" runat="server"
                                 CssClass="form-control"
                                 ClientIDMode="Static"
                                 placeholder="e.g. NullByte, ByteForce, PhantomByte&#x2026;"
                                 autocomplete="off"
                                 MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                                ControlToValidate="txtName"
                                                ErrorMessage="Team name is required."
                                                CssClass="field-validation-error d-block mt-1"
                                                Display="Dynamic"
                                                ValidationGroup="CreateTeamGroup" />
                    <asp:RegularExpressionValidator ID="revName" runat="server"
                                                    ControlToValidate="txtName"
                                                    ValidationExpression="^.{2,50}$"
                                                    ErrorMessage="Team name must be 2–50 characters."
                                                    CssClass="field-validation-error d-block mt-1"
                                                    Display="Dynamic"
                                                    ValidationGroup="CreateTeamGroup" />
                    <div class="form-text" style="color:var(--ca-text-muted);font-size:0.75rem;">
                        2–50 characters. Choose something memorable.
                    </div>
                </div>

                <div class="mb-4">
                    <label for="txtDescription" class="form-label">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server"
                                 TextMode="MultiLine" Rows="3"
                                 CssClass="form-control"
                                 ClientIDMode="Static"
                                 placeholder="Describe your team's specialty, members, or strategy&#x2026;"
                                 MaxLength="500"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revDescription" runat="server"
                                                    ControlToValidate="txtDescription"
                                                    ValidationExpression="^[\s\S]{0,500}$"
                                                    ErrorMessage="Description cannot exceed 500 characters."
                                                    CssClass="field-validation-error d-block mt-1"
                                                    Display="Dynamic"
                                                    ValidationGroup="CreateTeamGroup" />
                    <div class="form-text" style="color:var(--ca-text-muted);font-size:0.75rem;">
                        Optional — up to 500 characters.
                    </div>
                </div>

                <div class="mb-4">
                    <label for="txtCountry" class="form-label">Country / Region</label>
                    <asp:TextBox ID="txtCountry" runat="server"
                                 CssClass="form-control"
                                 ClientIDMode="Static"
                                 placeholder="e.g. India, United States, Germany&#x2026;"
                                 MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCountry" runat="server"
                                                    ControlToValidate="txtCountry"
                                                    ValidationExpression="^.{0,100}$"
                                                    ErrorMessage="Country cannot exceed 100 characters."
                                                    CssClass="field-validation-error d-block mt-1"
                                                    Display="Dynamic"
                                                    ValidationGroup="CreateTeamGroup" />
                </div>

                <%-- Team preview --%>
                <div class="p-3 mb-4 rounded-3" style="background:var(--ca-bg-base);border:1px solid var(--ca-border-light);">
                    <div class="d-flex align-items-center gap-3">
                        <div class="team-avatar" id="preview-avatar" aria-hidden="true" style="width:44px;height:44px;">?</div>
                        <div>
                            <div style="font-weight:600;font-size:0.9rem;color:var(--ca-text-primary);" id="preview-name">Your Team Name</div>
                            <div style="font-size:0.75rem;color:var(--ca-text-muted);">
                                <i class="fas fa-crown" style="color:var(--ca-warning);" aria-hidden="true"></i>
                                Captain: <asp:Literal ID="litCaptainName" runat="server" Text="h4x0r_pro" /> &middot; 1 member
                            </div>
                        </div>
                    </div>
                </div>

                <div class="d-flex gap-2 justify-content-end">
                    <a href="<%= ResolveUrl("~/Teams/Teams.aspx") %>" class="btn btn-secondary" id="btn-cancel-team">
                        Cancel
                    </a>
                    
                    <%-- HTML UI Button --%>
                    <button type="button" class="btn btn-primary" id="btn-submit-team-ui" onclick="handleCreateTeamSubmit(this);">
                        <i class="fas fa-plus me-2" aria-hidden="true"></i>Create Team
                    </button>
                    
                    <%-- Hidden ASP.NET Button --%>
                    <asp:Button ID="btnCreateTeam" runat="server"
                                Text="Create Team"
                                OnClick="btnCreateTeam_Click"
                                ValidationGroup="CreateTeamGroup"
                                UseSubmitBehavior="false"
                                style="display:none;" />
                </div>
            </div>
        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
    function handleCreateTeamSubmit(btn) {
        if (typeof Page_ClientValidate === 'function' && !Page_ClientValidate('CreateTeamGroup')) return;
        
        btn.disabled = true;
        btn.innerHTML = '<i class="fas fa-circle-notch fa-spin me-2"></i>Creating&#x2026;';
        __doPostBack('<%= btnCreateTeam.UniqueID %>', '');
    }

    (function () {
        var nameInput  = document.getElementById('txtName');
        var previewName   = document.getElementById('preview-name');
        var previewAvatar = document.getElementById('preview-avatar');

        if (nameInput) {
            nameInput.addEventListener('input', function () {
                var val = nameInput.value.trim();
                previewName.textContent   = val || 'Your Team Name';
                previewAvatar.textContent = val ? val[0].toUpperCase() : '?';
            });
            // trigger once on load
            nameInput.dispatchEvent(new Event('input'));
        }
    }());
</script>
</asp:Content>
