/*
 * Courses Hub page loader.
 * - Shows the branded loader (#pageLoader, Views/Shared/_PageLoader.cshtml) whenever a form is submitted
 *   (only after validation lets it through) and while save/delete requests run in the background.
 * - Skip a form with data-no-loader, skip a background request by adding its URL to IGNORE below.
 * - window.CHLoader.show("Loading...") / CHLoader.hide() can be called directly.
 */
(function () {
    'use strict';

    // Background requests that should never show the loader
    var IGNORE = [/SetSessionValues/i, /GetCategories/i, /SearchResults/i];

    var hideTimer = null;
    var pendingRequests = 0;
    var requestTimer = null;

    function el() { return document.getElementById('pageLoader'); }

    function show(message) {
        var loader = el();
        if (!loader) return;
        var text = loader.querySelector('.ch-loader-text');
        if (text) text.textContent = message || 'Loading…';
        loader.style.display = '';
        loader.classList.add('is-active');
        loader.setAttribute('aria-hidden', 'false');
        if (document.body) document.body.classList.add('ch-loading');
        clearTimeout(hideTimer);
        // safety net: never block the page for ever (e.g. a download or a failed request)
        hideTimer = setTimeout(hide, 45000);
    }

    function hide() {
        var loader = el();
        if (!loader) return;
        loader.classList.remove('is-active');
        loader.style.display = '';
        loader.setAttribute('aria-hidden', 'true');
        if (document.body) document.body.classList.remove('ch-loading');
        clearTimeout(hideTimer);
    }

    window.CHLoader = { show: show, hide: hide };

    function messageFor(form) {
        if (form.getAttribute('data-loader-text')) return form.getAttribute('data-loader-text');
        return 'Loading…';
    }

    function wantsLoader(form) {
        if (!form || form.hasAttribute('data-no-loader')) return false;
        var target = form.getAttribute('target');
        if (target && target !== '_self') return false;
        // still invalid according to jQuery Validate? then the page stays, no loader
        var jq = window.jQuery;
        var validator = jq && jq.fn && jq.fn.validate ? jq(form).data('validator') : null;
        if (validator) {
            // onsubmit:false means the page validates and shows the loader itself (e.g. Registration)
            if (validator.settings && validator.settings.onsubmit === false) return false;
            try { if (!jq(form).valid()) return false; } catch (e) { /* ignore */ }
        }
        if (typeof form.checkValidity === 'function' && form.noValidate !== true && !form.checkValidity()) return false;
        return true;
    }

    // 1) Normal submits: wait until every other submit handler has run, then show if not cancelled
    document.addEventListener('submit', function (e) {
        var form = e.target;
        setTimeout(function () {
            if (e.defaultPrevented) return;
            if (wantsLoader(form)) show(messageFor(form));
        }, 0);
    }, false);

    // 2) Scripts that submit with form.submit() (no submit event), e.g. jQuery Validate submitHandler
    var nativeSubmit = HTMLFormElement.prototype.submit;
    HTMLFormElement.prototype.submit = function () {
        if (!this.hasAttribute('data-no-loader')) show(messageFor(this));
        return nativeSubmit.apply(this, arguments);
    };

    // 3) Background save/delete requests (jQuery $.ajax or plain XMLHttpRequest); GET requests are ignored
    var origOpen = XMLHttpRequest.prototype.open;
    var origSend = XMLHttpRequest.prototype.send;
    XMLHttpRequest.prototype.open = function (method, url) {
        this.__chTrack = method && method.toUpperCase() !== 'GET' &&
            !IGNORE.some(function (re) { return re.test(String(url || '')); });
        return origOpen.apply(this, arguments);
    };
    XMLHttpRequest.prototype.send = function () {
        if (this.__chTrack) {
            pendingRequests++;
            // only show for requests that take a moment, so quick ones do not flash
            if (!requestTimer) {
                requestTimer = setTimeout(function () {
                    requestTimer = null;
                    if (pendingRequests > 0) show('Loading…');
                }, 300);
            }
            this.addEventListener('loadend', function () {
                pendingRequests = Math.max(0, pendingRequests - 1);
                if (pendingRequests === 0) {
                    clearTimeout(requestTimer);
                    requestTimer = null;
                    hide();
                }
            });
        }
        return origSend.apply(this, arguments);
    };

    // 4) Coming back with the browser Back button: page is restored from cache, so hide the loader
    window.addEventListener('pageshow', function () { hide(); });
})();
