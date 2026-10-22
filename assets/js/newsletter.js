/*
 * Footer newsletter form (Views/Shared/_SiteFooter.cshtml -> NewsletterController.Subscribe).
 * Sends the email in the background and shows the result under the form.
 */
(function () {
    'use strict';

    var EMAIL_RX = /^[^@\s]+@[^@\s]+\.[^@\s]{2,}$/;

    function init(form) {
        var input = form.querySelector('input[name="email"]');
        var button = form.querySelector('button[type="submit"]');
        var message = form.parentNode.querySelector('.nl-message');
        var label = button ? button.querySelector('span') : null;
        var busy = false;

        function show(text, kind) {
            if (!message) return;
            message.textContent = text;
            message.className = 'nl-message nl-' + kind;
            message.hidden = false;
        }

        input.addEventListener('input', function () {
            form.classList.remove('nl-invalid');
            input.removeAttribute('aria-invalid');
        });

        form.addEventListener('submit', function (e) {
            e.preventDefault();
            if (busy) return;

            var email = (input.value || '').trim();
            if (!EMAIL_RX.test(email)) {
                form.classList.add('nl-invalid');
                input.setAttribute('aria-invalid', 'true');
                show('Please enter a valid email address.', 'error');
                input.focus();
                return;
            }

            busy = true;
            form.classList.add('nl-busy');
            // the label stays "Subscribe" (hidden behind a spinner by .nl-busy) so the button keeps its size
            if (button) { button.disabled = true; button.setAttribute('aria-label', 'Subscribing'); }

            var data = new FormData(form);
            data.set('email', email);
            data.set('source', 'Footer - ' + (document.title || window.location.pathname));

            fetch(form.action, {
                method: 'POST',
                body: data,
                credentials: 'same-origin',
                headers: { 'X-Requested-With': 'XMLHttpRequest' }
            })
                .then(function (r) { if (!r.ok) throw new Error('HTTP ' + r.status); return r.json(); })
                .then(function (res) {
                    if (res && res.ok) {
                        show(res.message || 'Thank you for subscribing!', res.already ? 'info' : 'success');
                        if (!res.already) form.classList.add('nl-done');
                        input.value = '';
                    } else {
                        show((res && res.message) || 'Something went wrong. Please try again.', 'error');
                    }
                })
                .catch(function () {
                    show('We couldn’t subscribe you right now. Please try again later.', 'error');
                })
                .then(function () {
                    busy = false;
                    form.classList.remove('nl-busy');
                    if (button) { button.disabled = false; button.removeAttribute('aria-label'); }
                });
        });
    }

    function start() {
        Array.prototype.forEach.call(document.querySelectorAll('.js-newsletter-form'), init);
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', start);
    else start();
})();
