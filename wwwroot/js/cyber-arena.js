/**
 * CyberArena — Main JavaScript
 * Sidebar toggle, active nav, micro-animations, utilities
 */

(function () {
    'use strict';

    // ─── DOM Ready ───────────────────────────────────────────
    document.addEventListener('DOMContentLoaded', function () {
        initSidebar();
        initActiveNav();
        initPasswordToggles();
        initPasswordStrength();
        initOtpInputs();
        initChartBars();
        initTooltips();
        initFormSubmitLoading();
    });

    // ─── Sidebar Toggle ───────────────────────────────────────
    function initSidebar() {
        var toggleBtn = document.getElementById('ca-sidebar-toggle');
        var sidebar = document.getElementById('ca-sidebar');
        var overlay = document.getElementById('ca-sidebar-overlay');

        if (!toggleBtn || !sidebar) return;

        toggleBtn.addEventListener('click', function () {
            sidebar.classList.toggle('open');
            if (overlay) overlay.classList.toggle('active');
            document.body.style.overflow = sidebar.classList.contains('open') ? 'hidden' : '';
        });

        if (overlay) {
            overlay.addEventListener('click', function () {
                sidebar.classList.remove('open');
                overlay.classList.remove('active');
                document.body.style.overflow = '';
            });
        }

        // Close on Escape
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && sidebar.classList.contains('open')) {
                sidebar.classList.remove('open');
                if (overlay) overlay.classList.remove('active');
                document.body.style.overflow = '';
            }
        });
    }

    // ─── Active Nav Link ─────────────────────────────────────
    function initActiveNav() {
        var links = document.querySelectorAll('.ca-nav-link');
        var path = window.location.pathname.toLowerCase();

        links.forEach(function (link) {
            var href = (link.getAttribute('href') || '').toLowerCase();
            if (!href || href === '#') return;
            // Strip trailing slash
            var clean = href.replace(/\/$/, '') || '/';
            if (path === clean || (clean !== '/' && path.startsWith(clean))) {
                link.classList.add('active');
            }
        });
    }

    // ─── Password Toggles ─────────────────────────────────────
    function initPasswordToggles() {
        document.querySelectorAll('.ca-pwd-toggle').forEach(function (btn) {
            btn.addEventListener('click', function () {
                var targetId = btn.dataset.target;
                var input = targetId
                    ? document.getElementById(targetId)
                    : btn.closest('.ca-input-group')?.querySelector('input');
                if (!input) return;

                var isText = input.type === 'text';
                input.type = isText ? 'password' : 'text';
                var icon = btn.querySelector('i') || btn;
                if (isText) {
                    icon.className = icon.tagName === 'I' ? 'fas fa-eye' : 'fas fa-eye';
                } else {
                    icon.className = icon.tagName === 'I' ? 'fas fa-eye-slash' : 'fas fa-eye-slash';
                }
            });
        });
    }

    // ─── Password Strength Indicator ─────────────────────────
    function initPasswordStrength() {
        var pwdInputs = document.querySelectorAll('input[data-pwd-strength]');
        pwdInputs.forEach(function (input) {
            var meter = document.getElementById(input.dataset.pwdStrength);
            if (!meter) return;
            var bars = meter.querySelectorAll('.ca-pwd-strength-bar');
            var label = meter.querySelector('.ca-pwd-strength-label');

            input.addEventListener('input', function () {
                var score = getPasswordStrength(input.value);
                updateStrengthMeter(bars, label, score);
            });
        });
    }

    function getPasswordStrength(pwd) {
        if (!pwd) return 0;
        var score = 0;
        if (pwd.length >= 8)  score++;
        if (pwd.length >= 12) score++;
        if (/[A-Z]/.test(pwd)) score++;
        if (/[0-9]/.test(pwd)) score++;
        if (/[^A-Za-z0-9]/.test(pwd)) score++;
        return Math.min(score, 4);
    }

    function updateStrengthMeter(bars, label, score) {
        var levels = ['', 'Weak', 'Fair', 'Good', 'Strong'];
        var classes = ['', 'filled-weak', 'filled-fair', 'filled-strong', 'filled-strong'];
        bars.forEach(function (bar, i) {
            bar.className = 'ca-pwd-strength-bar';
            if (i < score) bar.classList.add(classes[score]);
        });
        if (label) {
            label.textContent = levels[score] || '';
            label.style.color = score <= 1 ? 'var(--ca-danger)'
                : score <= 2 ? 'var(--ca-warning)'
                : 'var(--ca-success)';
        }
    }

    // ─── OTP Inputs ──────────────────────────────────────────
    function initOtpInputs() {
        var otpInputs = document.querySelectorAll('.ca-otp-input');
        if (!otpInputs.length) return;

        otpInputs.forEach(function (input, idx) {
            input.addEventListener('input', function () {
                input.value = input.value.replace(/[^0-9]/g, '').slice(-1);
                if (input.value && idx < otpInputs.length - 1) {
                    otpInputs[idx + 1].focus();
                }
            });
            input.addEventListener('keydown', function (e) {
                if (e.key === 'Backspace' && !input.value && idx > 0) {
                    otpInputs[idx - 1].focus();
                }
            });
            input.addEventListener('paste', function (e) {
                e.preventDefault();
                var pasted = (e.clipboardData || window.clipboardData).getData('text').replace(/\D/g, '');
                otpInputs.forEach(function (inp, i) {
                    inp.value = pasted[i] || '';
                });
                var last = Math.min(pasted.length, otpInputs.length) - 1;
                if (last >= 0) otpInputs[last].focus();
            });
        });
    }

    // ─── Animated Chart Bars ─────────────────────────────────
    function initChartBars() {
        var bars = document.querySelectorAll('.ca-chart-bar[data-height]');
        // Use IntersectionObserver for entrance animation
        if (!bars.length) return;

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var bar = entry.target;
                    var height = bar.dataset.height || '50';
                    setTimeout(function () {
                        bar.style.height = height + '%';
                    }, parseInt(bar.dataset.delay || '0'));
                    observer.unobserve(bar);
                }
            });
        }, { threshold: 0.2 });

        bars.forEach(function (bar) {
            bar.style.height = '0%';
            bar.style.transition = 'height 0.7s cubic-bezier(0.4, 0, 0.2, 1)';
            observer.observe(bar);
        });
    }

    // ─── Bootstrap Tooltips ──────────────────────────────────
    function initTooltips() {
        if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
            var tooltipEls = document.querySelectorAll('[data-bs-toggle="tooltip"]');
            tooltipEls.forEach(function (el) {
                new bootstrap.Tooltip(el);
            });
        }
    }

    // ─── Form Submit Loading State ────────────────────────────
    function initFormSubmitLoading() {
        document.querySelectorAll('form[data-loading]').forEach(function (form) {
            form.addEventListener('submit', function () {
                var btn = form.querySelector('[type="submit"]');
                if (!btn) return;
                btn.disabled = true;
                var original = btn.innerHTML;
                btn.innerHTML = '<span class="ca-spinner me-2"></span>Please wait...';
                // Restore after 10s as fallback
                setTimeout(function () {
                    btn.disabled = false;
                    btn.innerHTML = original;
                }, 10000);
            });
        });
    }

    // ─── Progress bar width animation ────────────────────────
    (function animateProgressBars() {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var bar = entry.target;
                    var width = bar.dataset.width || '0';
                    bar.style.width = width + '%';
                    observer.unobserve(bar);
                }
            });
        }, { threshold: 0.1 });

        document.querySelectorAll('.ca-progress-bar[data-width]').forEach(function (bar) {
            bar.style.width = '0%';
            bar.style.transition = 'width 1s cubic-bezier(0.4, 0, 0.2, 1)';
            observer.observe(bar);
        });
    })();

    // ─── Counter Animation ────────────────────────────────────
    (function animateCounters() {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var el = entry.target;
                    var target = parseInt(el.dataset.count || '0');
                    var duration = parseInt(el.dataset.duration || '1200');
                    var start = 0;
                    var step = Math.ceil(target / (duration / 16));
                    var interval = setInterval(function () {
                        start += step;
                        if (start >= target) {
                            el.textContent = target.toLocaleString();
                            clearInterval(interval);
                        } else {
                            el.textContent = start.toLocaleString();
                        }
                    }, 16);
                    observer.unobserve(el);
                }
            });
        }, { threshold: 0.3 });

        document.querySelectorAll('[data-count]').forEach(function (el) {
            observer.observe(el);
        });
    })();

    // ─── Flash/alert auto-dismiss ─────────────────────────────
    setTimeout(function () {
        document.querySelectorAll('.ca-auto-dismiss').forEach(function (el) {
            el.style.transition = 'opacity 0.5s';
            el.style.opacity = '0';
            setTimeout(function () { el.remove(); }, 500);
        });
    }, 4000);

})();
