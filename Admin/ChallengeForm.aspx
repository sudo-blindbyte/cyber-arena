<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChallengeForm.aspx.cs"
         Inherits="CyberArenaWebForms.Admin.ChallengeFormPage"
         MasterPageFile="~/Admin.Master" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server"><%= VM.IsEdit ? "Edit Challenge" : "Create Challenge" %></asp:Content>

<asp:Content ContentPlaceHolderID="HeadStyles" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/CSS/challenges.css") %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="stagger-children">

    <%-- ─── Page Header ─────────────────────────────────────── --%>
    <div class="adm-header">
        <div>
            <h2 class="adm-title">
                <i class="fas <%= VM.IsEdit ? "fa-pen" : "fa-plus" %> me-2" style="color:var(--ca-accent);" aria-hidden="true"></i>
                <%= VM.IsEdit ? "Edit Challenge" : "Create Challenge" %>
            </h2>
            <p class="adm-subtitle">
                <%= VM.IsEdit ? $"Editing challenge #{VM.Id}" : "Fill in the details to create a new CTF challenge" %>
            </p>
        </div>
        <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" class="btn btn-secondary">
            <i class="fas fa-arrow-left me-1" aria-hidden="true"></i>Back to Challenges
        </a>
    </div>

    <%-- ─── Form ────────────────────────────────────────────── --%>
    <asp:ValidationSummary ID="vsErrors" runat="server" CssClass="alert alert-danger mb-3" HeaderText="<i class='fas fa-circle-exclamation me-2' aria-hidden='true'></i> Please fix the errors below before saving." DisplayMode="BulletList" />

    <div class="row g-4">

        <%-- ── Left Column ── --%>
        <div class="col-lg-8">

            <%-- Basic Info --%>
            <div class="ch-form-card mb-4">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-circle-info" aria-hidden="true"></i> Challenge Information
                    </div>

                    <div class="mb-3">
                        <label for="txtTitle" class="form-label">
                            Title <span style="color:var(--ca-danger);">*</span>
                        </label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="200" placeholder="e.g. SQL Injection Basics" AutoCompleteType="Disabled" />
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="txtTitle" ValidationExpression="^.{3,200}$" ErrorMessage="Title must be 3–200 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>

                    <div class="mb-0">
                        <label for="txtDescription" class="form-label">
                            Description <span style="color:var(--ca-danger);">*</span>
                        </label>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="6" CssClass="form-control" placeholder="Describe the challenge scenario..." />
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>
                </div>

                <%-- Category / Difficulty / Points --%>
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-sliders" aria-hidden="true"></i> Classification
                    </div>
                    <div class="row g-3">
                        <div class="col-md-4">
                            <label for="ddlCategory" class="form-label">
                                Category <span style="color:var(--ca-danger);">*</span>
                            </label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select ch-form-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Category is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        </div>
                        <div class="col-md-4">
                            <label for="ddlDifficulty" class="form-label">
                                Difficulty <span style="color:var(--ca-danger);">*</span>
                            </label>
                            <asp:DropDownList ID="ddlDifficulty" runat="server" CssClass="form-select ch-form-select">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDifficulty" runat="server" ControlToValidate="ddlDifficulty" ErrorMessage="Difficulty is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        </div>
                        <div class="col-md-4">
                            <label for="txtPoints" class="form-label">
                                Points <span style="color:var(--ca-danger);">*</span>
                            </label>
                            <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" TextMode="Number" placeholder="e.g. 250" />
                            <asp:RequiredFieldValidator ID="rfvPoints" runat="server" ControlToValidate="txtPoints" ErrorMessage="Points value is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                            <asp:RangeValidator ID="rvPoints" runat="server" ControlToValidate="txtPoints" Type="Integer" MinimumValue="1" MaximumValue="10000" ErrorMessage="Points must be between 1 and 10,000." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        </div>
                    </div>
                </div>

                <%-- Status --%>
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-toggle-on" aria-hidden="true"></i> Visibility
                    </div>
                    <div class="col-md-4">
                        <label for="ddlStatus" class="form-label">Status</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select ch-form-select">
                        </asp:DropDownList>
                        <small class="text-muted mt-1 d-block">
                            <strong>Active</strong> = visible to participants &nbsp;·&nbsp;
                            <strong>Draft</strong> = hidden &nbsp;·&nbsp;
                            <strong>Archived</strong> = read-only history
                        </small>
                    </div>
                </div>
            </div>

            <%-- Hint --%>
            <div class="ch-form-card mb-4">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-lightbulb" aria-hidden="true"></i> Hint
                        <span style="font-weight:400;text-transform:none;letter-spacing:normal;color:var(--ca-text-muted);font-size:0.78rem;">(optional)</span>
                    </div>

                    <div class="mb-3">
                        <label for="txtHint" class="form-label">Hint Text</label>
                        <asp:TextBox ID="txtHint" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Give participants a nudge in the right direction…" MaxLength="1000" />
                        <asp:RegularExpressionValidator ID="revHint" runat="server" ControlToValidate="txtHint" ValidationExpression="^.{0,1000}$" ErrorMessage="Hint cannot exceed 1000 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                    </div>

                    <div style="max-width:180px;">
                        <label for="txtHintPenalty" class="form-label">Point Penalty</label>
                        <div class="input-group">
                            <span class="input-group-text" style="background:var(--ca-bg-base);border-color:var(--ca-border);color:var(--ca-text-muted);">−</span>
                            <asp:TextBox ID="txtHintPenalty" runat="server" CssClass="form-control" TextMode="Number" placeholder="0" />
                        </div>
                        <asp:RangeValidator ID="rvHintPenalty" runat="server" ControlToValidate="txtHintPenalty" Type="Integer" MinimumValue="0" MaximumValue="10000" ErrorMessage="Penalty must be between 0 and 10000." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        <small class="text-muted mt-1 d-block">Points deducted when a hint is revealed</small>
                    </div>
                </div>
            </div>

            <%-- Attachment --%>
            <div class="ch-form-card">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-paperclip" aria-hidden="true"></i> Attachment
                        <span style="font-weight:400;text-transform:none;letter-spacing:normal;color:var(--ca-text-muted);font-size:0.78rem;">(optional)</span>
                    </div>

                    <% if (VM.IsEdit && !string.IsNullOrEmpty(VM.ExistingAttachmentName)) { %>
                        <div class="d-flex align-items-center gap-2 mb-3 p-3"
                             style="background:var(--ca-bg-base);border-radius:var(--ca-radius);border:1px solid var(--ca-border);">
                            <i class="fas fa-file-zipper" style="color:var(--ca-accent);" aria-hidden="true"></i>
                            <span style="font-size:0.875rem;color:var(--ca-text-secondary);flex:1;"><%= VM.ExistingAttachmentName %></span>
                            <span class="ca-tag easy">Current</span>
                        </div>
                        <p style="font-size:0.8rem;color:var(--ca-text-muted);margin-bottom:0.75rem;">
                            Upload a new file below to replace the existing attachment.
                        </p>
                    <% } %>

                    <div class="ch-upload-zone" id="upload-zone">
                        <asp:FileUpload ID="fuAttachment" runat="server" CssClass="form-control" style="position:absolute;width:100%;height:100%;opacity:0;cursor:pointer;z-index:10;" accept=".zip,.tar,.gz,.7z,.pdf,.txt,.py,.c,.cpp,.js,.bin,.exe" />
                        <i class="fas fa-cloud-arrow-up ch-upload-icon" aria-hidden="true"></i>
                        <p class="ch-upload-label" id="upload-label">
                            <strong style="color:var(--ca-accent);">Click to upload</strong> or drag and drop
                        </p>
                        <p class="ch-upload-hint">ZIP, TAR, PDF, Python, C/C++, binary — max 50MB</p>
                    </div>
                </div>
            </div>

        </div>

        <%-- ── Right Column ── --%>
        <div class="col-lg-4">

            <%-- Flag --%>
            <div class="ch-form-card mb-4">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-flag" aria-hidden="true"></i> Flag
                    </div>

                    <div class="ch-form-flag">
                        <label for="txtFlag" class="form-label">
                            Flag Value <span style="color:var(--ca-danger);">*</span>
                        </label>
                        <div class="ch-flag-input-wrap">
                            <i class="fas fa-flag ch-flag-icon" aria-hidden="true"></i>
                            <asp:TextBox ID="txtFlag" runat="server" CssClass="ch-flag-input form-control" TextMode="Password" placeholder="CTF{your_flag_here}" AutoCompleteType="Disabled" />
                            <button type="button" class="ca-pwd-toggle" data-target="<%= txtFlag.ClientID %>" aria-label="Show/hide flag" style="right:0.5rem;">
                                <i class="fas fa-eye" aria-hidden="true"></i>
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvFlag" runat="server" ControlToValidate="txtFlag" ErrorMessage="Flag is required." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revFlag" runat="server" ControlToValidate="txtFlag" ValidationExpression="^.{1,500}$" ErrorMessage="Flag cannot exceed 500 characters." CssClass="field-validation-error d-block mt-1" Display="Dynamic" />
                        
                        <small class="text-muted mt-2 d-block">
                            <i class="fas fa-lock me-1" aria-hidden="true"></i>
                            Flag is stored securely. Never exposed to participants.
                        </small>
                    </div>
                </div>
            </div>

            <%-- Preview Card --%>
            <div class="ch-form-card mb-4">
                <div class="ch-form-section">
                    <div class="ch-form-section-title">
                        <i class="fas fa-eye" aria-hidden="true"></i> Live Preview
                    </div>
                    <div class="ch-card" style="cursor:default;" id="preview-card" aria-label="Challenge card preview">
                        <div class="ch-card-top">
                            <div class="ch-card-icon ch-cat-web" id="preview-icon" aria-hidden="true">
                                <i class="fas fa-globe"></i>
                            </div>
                        </div>
                        <h3 class="ch-card-title" id="preview-title" style="font-size:0.9rem;">
                            Challenge Title Preview
                        </h3>
                        <div class="ch-card-meta" id="preview-meta">
                            <span class="ch-diff medium" id="preview-diff">Medium</span>
                            <span class="ch-card-category" id="preview-cat">Web Security</span>
                        </div>
                        <div class="ch-card-footer">
                            <div class="ch-points" id="preview-pts">
                                100 <small>pts</small>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Submit --%>
            <div class="ch-form-card">
                <div class="ch-form-section">
                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary w-100 mb-2" OnClick="btnSubmit_Click">
                        <i class="fas <%= VM.IsEdit ? "fa-floppy-disk" : "fa-plus" %> me-2" aria-hidden="true"></i>
                        <%= VM.IsEdit ? "Save Changes" : "Create" %>
                    </asp:LinkButton>
                    <a href="<%= ResolveUrl("~/Admin/Challenges.aspx") %>" class="btn btn-secondary w-100">
                        Cancel
                    </a>
                </div>
            </div>

        </div>
    </div>

</div>

</asp:Content>

<asp:Content ContentPlaceHolderID="ScriptsContent" runat="server">
<script>
(function () {
    // ── Category icon map ────────────────────────────────
    var catIcons = {
        'Web Security':          { icon: 'fa-globe',           css: 'ch-cat-web'      },
        'Cryptography':          { icon: 'fa-key',             css: 'ch-cat-crypto'   },
        'Reverse Engineering':   { icon: 'fa-code',            css: 'ch-cat-rev'      },
        'Digital Forensics':     { icon: 'fa-magnifying-glass',css: 'ch-cat-forensics'},
        'Networking':            { icon: 'fa-network-wired',   css: 'ch-cat-network'  },
        'Steganography':         { icon: 'fa-image',           css: 'ch-cat-stego'    },
        'OSINT':                 { icon: 'fa-satellite-dish',  css: 'ch-cat-osint'    },
        'Binary Exploitation':   { icon: 'fa-terminal',        css: 'ch-cat-pwn'      },
    };

    // ── Live preview ─────────────────────────────────────
    function updatePreview() {
        var title  = document.getElementById('<%= txtTitle.ClientID %>').value.trim() || 'Challenge Title Preview';
        var cat    = document.getElementById('<%= ddlCategory.ClientID %>').value;
        var diff   = document.getElementById('<%= ddlDifficulty.ClientID %>').value;
        var pts    = document.getElementById('<%= txtPoints.ClientID %>').value || '100';

        document.getElementById('preview-title').textContent = title;
        document.getElementById('preview-diff').textContent  = diff;
        document.getElementById('preview-diff').className    = 'ch-diff ' + diff.toLowerCase();
        document.getElementById('preview-cat').textContent   = cat;
        document.getElementById('preview-pts').innerHTML     = pts + ' <small>pts</small>';

        var iconInfo = catIcons[cat] || catIcons['Web Security'];
        var iconEl = document.getElementById('preview-icon');
        iconEl.className = 'ch-card-icon ' + iconInfo.css;
        iconEl.innerHTML = '<i class="fas ' + iconInfo.icon + '"></i>';
    }

    ['<%= txtTitle.ClientID %>', '<%= ddlCategory.ClientID %>', '<%= ddlDifficulty.ClientID %>', '<%= txtPoints.ClientID %>'].forEach(function (id) {
        var el = document.getElementById(id);
        if (el) el.addEventListener('input', updatePreview);
    });
    updatePreview();

    // ── File upload label ────────────────────────────────
    var fileInput = document.getElementById('<%= fuAttachment.ClientID %>');
    if (fileInput) {
        fileInput.addEventListener('change', function () {
            var label = document.getElementById('upload-label');
            if (this.files && this.files.length > 0) {
                label.innerHTML = '<strong style="color:var(--ca-success);">' + this.files[0].name + '</strong>';
            } else {
                label.innerHTML = '<strong style="color:var(--ca-accent);">Click to upload</strong> or drag and drop';
            }
        });
    }

    // ── Drag-over styling ────────────────────────────────
    var zone = document.getElementById('upload-zone');
    if (zone) {
        zone.addEventListener('dragover',  function (e) { e.preventDefault(); zone.classList.add('drag-over'); });
        zone.addEventListener('dragleave', function ()  { zone.classList.remove('drag-over'); });
        zone.addEventListener('drop',      function ()  { zone.classList.remove('drag-over'); });
    }

    // ── Password toggle ──────────────────────────────────
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
