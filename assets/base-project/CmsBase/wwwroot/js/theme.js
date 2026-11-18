'use strict';

(() => {
    const key = 'cms.theme';
    const systemTheme = window.matchMedia('(prefers-color-scheme: dark)');
    const root = document.documentElement;
    let preference;

    try {
        preference = localStorage.getItem(key);
    } catch {
        // Use the system theme when storage is unavailable.
    }

    function applyTheme() {
        const dark = preference === 'dark' || (preference !== 'light' && systemTheme.matches);
        root.dataset.bsTheme = dark ? 'dark' : 'light';

        for (const toggle of document.querySelectorAll('[data-cms-theme-toggle]')) {
            toggle.setAttribute('aria-pressed', String(dark));
            toggle.title = dark ? 'Chuyển sang chế độ sáng' : 'Chuyển sang chế độ tối';
            toggle.hidden = false;
        }
    }

    applyTheme();
    document.addEventListener('DOMContentLoaded', applyTheme);
    systemTheme.addEventListener('change', applyTheme);

    document.addEventListener('click', event => {
        if (!event.target.closest('[data-cms-theme-toggle]')) return;

        preference = root.dataset.bsTheme === 'dark' ? 'light' : 'dark';
        applyTheme();

        try {
            localStorage.setItem(key, preference);
        } catch {
            // The selected theme still applies to the current page.
        }
    });

    window.addEventListener('storage', event => {
        if (event.key !== key && event.key !== null) return;
        preference = event.newValue;
        applyTheme();
    });
})();
