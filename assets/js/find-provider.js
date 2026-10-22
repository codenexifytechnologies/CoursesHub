/*
 * Courses Hub "Find a provider" questions (Views/Shared/_FindProviderModal.cshtml).
 * - Any element with data-find-provider="<course code or title>" (optional data-course-title, data-course-code) opens it.
 * - Q1: Australian citizen / permanent resident?  Yes -> postcode,  No -> country of citizenship.
 * - The questions open on every click with empty answers (nothing is remembered between clicks).
 */
(function () {
    'use strict';

    var modal = document.getElementById('fpModal');
    if (!modal) return;

    var state = { course: '', resident: null };
    var lastTrigger = null;

    function $(sel) { return modal.querySelector(sel); }
    function steps() { return modal.querySelectorAll('.fp-step'); }

    // Same ranges as CourseFilterHelper.StateFromPostcode
    function stateFromPostcode(pc) {
        if (!/^\d{4}$/.test(pc)) return null;
        var n = parseInt(pc, 10);
        if ((n >= 200 && n <= 299) || (n >= 2600 && n <= 2618) || (n >= 2900 && n <= 2920)) return 'Australian Capital Territory';
        if (n >= 800 && n <= 999) return 'Northern Territory';
        if (n >= 1000 && n <= 2999) return 'New South Wales';
        if ((n >= 3000 && n <= 3999) || (n >= 8000 && n <= 8999)) return 'Victoria';
        if ((n >= 4000 && n <= 4999) || (n >= 9000 && n <= 9999)) return 'Queensland';
        if (n >= 5000 && n <= 5999) return 'South Australia';
        if (n >= 6000 && n <= 6999) return 'Western Australia';
        if (n >= 7000 && n <= 7999) return 'Tasmania';
        return null;
    }

    function go(answers) {
        var params = ['course=' + encodeURIComponent(state.course), 'resident=' + answers.resident];
        if (answers.resident === 'yes') params.push('postcode=' + encodeURIComponent(answers.postcode));
        else params.push('country=' + encodeURIComponent(answers.country));
        if (window.CHLoader) window.CHLoader.show('Loading\u2026');
        window.location.href = (window.CH_FIND_PROVIDER_URL || '/FindProvider') + '?' + params.join('&');
    }

    function showStep(name) {
        Array.prototype.forEach.call(steps(), function (s) { s.hidden = s.getAttribute('data-fp-step') !== name; });
        $('[data-fp-back]').hidden = name === 'resident';
        $('.fp-progress span').style.width = name === 'resident' ? '50%' : '100%';
        var focusEl = name === 'postcode' ? $('#fpPostcode') : name === 'country' ? $('#fpCountry') : $('.fp-choice');
        if (focusEl) setTimeout(function () { focusEl.focus(); }, 30);
    }

    function open(trigger) {
        lastTrigger = trigger;
        state.course = trigger.getAttribute('data-find-provider') || '';
        var code = trigger.getAttribute('data-course-code') || '';
        var title = trigger.getAttribute('data-course-title') || state.course;
        var codeEl = $('.fp-course-code');
        codeEl.textContent = code;
        codeEl.hidden = !code;
        $('.fp-course-title').textContent = title;

        // every time: no answer selected and empty fields
        state.resident = null;
        Array.prototype.forEach.call(modal.querySelectorAll('.fp-choice'), function (b) {
            b.classList.remove('is-selected');
            b.setAttribute('aria-pressed', 'false');
        });
        $('#fpPostcode').value = '';
        $('#fpCountry').value = '';
        updatePostcode();
        updateCountry();

        modal.hidden = false;
        document.body.classList.add('fp-open');
        requestAnimationFrame(function () { modal.classList.add('is-open'); });
        showStep('resident');
    }

    function close() {
        modal.classList.remove('is-open');
        document.body.classList.remove('fp-open');
        modal.hidden = true;
        if (lastTrigger && lastTrigger.focus) lastTrigger.focus();
    }

    function updatePostcode() {
        var input = $('#fpPostcode');
        input.value = input.value.replace(/\D/g, '').slice(0, 4);
        var st = stateFromPostcode(input.value);
        var detected = $('[data-fp-step="postcode"] .fp-detected');
        detected.hidden = !st;
        detected.querySelector('span').textContent = st ? 'Postcode ' + input.value + ', ' + st : '';
        $('[data-fp-step="postcode"] [data-fp-go]').disabled = !st;
        input.setAttribute('aria-invalid', input.value.length === 4 && !st ? 'true' : 'false');
        return st;
    }

    function updateCountry() {
        var value = $('#fpCountry').value.trim();
        $('[data-fp-step="country"] [data-fp-go]').disabled = value.length < 2;
        return value;
    }

    function submitStep() {
        if (state.resident === 'yes') {
            var pc = $('#fpPostcode').value;
            if (!updatePostcode()) return;
            var a = { resident: 'yes', postcode: pc };
            go(a);
        } else if (state.resident === 'no') {
            var country = updateCountry();
            if (country.length < 2) return;
            var b = { resident: 'no', country: country };
            go(b);
        }
    }

    // Open: the questions are asked every time, starting empty
    document.addEventListener('click', function (e) {
        var trigger = e.target.closest ? e.target.closest('[data-find-provider]') : null;
        if (!trigger) return;
        e.preventDefault();
        open(trigger);
    });

    modal.addEventListener('click', function (e) {
        if (e.target.closest('[data-fp-close]')) { close(); return; }
        if (e.target.closest('[data-fp-back]')) { showStep('resident'); return; }
        if (e.target.closest('[data-fp-go]')) { submitStep(); return; }
        var choice = e.target.closest('[data-fp-resident]');
        if (choice) {
            state.resident = choice.getAttribute('data-fp-resident');
            Array.prototype.forEach.call(modal.querySelectorAll('.fp-choice'), function (b) {
                var on = b === choice;
                b.classList.toggle('is-selected', on);
                b.setAttribute('aria-pressed', on ? 'true' : 'false');
            });
            showStep(state.resident === 'yes' ? 'postcode' : 'country');
        }
    });

    $('#fpPostcode').addEventListener('input', updatePostcode);
    $('#fpCountry').addEventListener('input', updateCountry);
    modal.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') close();
        if (e.key === 'Enter' && (e.target.id === 'fpPostcode' || e.target.id === 'fpCountry')) { e.preventDefault(); submitStep(); }
    });
})();
