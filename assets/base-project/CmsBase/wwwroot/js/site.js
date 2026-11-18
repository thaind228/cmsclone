'use strict';

const sidebarToggle = document.querySelector('[data-cms-sidebar-toggle]');
if (sidebarToggle) {
    const shell = sidebarToggle.closest('.cms-shell');
    const preferenceKey = 'cms.sidebarHidden';

    function setSidebarHidden(hidden) {
        shell.classList.toggle('cms-sidebar-hidden', hidden);
        const label = hidden ? 'Hiện điều hướng' : 'Ẩn điều hướng';
        sidebarToggle.setAttribute('aria-expanded', String(!hidden));
        sidebarToggle.setAttribute('aria-label', label);
        sidebarToggle.title = label;
    }

    try {
        setSidebarHidden(localStorage.getItem(preferenceKey) === 'true');
    } catch {
        setSidebarHidden(false);
    }

    sidebarToggle.hidden = false;
    sidebarToggle.addEventListener('click', () => {
        const hidden = !shell.classList.contains('cms-sidebar-hidden');
        setSidebarHidden(hidden);
        try {
            localStorage.setItem(preferenceKey, String(hidden));
        } catch {
            // Keep the toggle usable when browser storage is disabled.
        }
    });
}

const search = document.querySelector('#cms-function-search');
if (search) {
    const normalize = (value) => value.normalize('NFD')
        .replace(/\p{M}/gu, '')
        .toLocaleLowerCase('vi')
        .replaceAll('đ', 'd');

    const groups = Array.from(document.querySelectorAll('[data-function-group]'), (group) => ({
        group,
        name: normalize(group.querySelector('h3').textContent),
        links: Array.from(group.querySelectorAll('[data-function-link]'), (link) => ({
            link,
            name: normalize(link.textContent)
        }))
    }));
    const noResults = document.querySelector('[data-no-functions]');

    function filterFunctions() {
        const query = normalize(search.value.trim()).split(/\s+/);
        let anyVisible = false;

        for (const { group, name, links } of groups) {
            let hasMatch = false;
            for (const { link, name: linkName } of links) {
                const match = query.every(term => name.includes(term) || linkName.includes(term));
                link.hidden = !match;
                hasMatch ||= match;
            }
            group.hidden = !hasMatch;
            anyVisible ||= hasMatch;
        }

        noResults.hidden = anyVisible;
    }

    search.closest('label').hidden = false;
    search.addEventListener('input', filterFunctions);
    filterFunctions();
}

document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(element => {
    bootstrap.Tooltip.getOrCreateInstance(element);
});
