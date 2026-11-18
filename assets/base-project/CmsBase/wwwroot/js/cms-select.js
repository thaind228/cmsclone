'use strict';

function initCmsSelect(select) {
    if (!select || !window.jQuery?.fn.select2 || select.classList.contains('select2-hidden-accessible')) return;

    const control = document.createElement('div');
    control.className = 'cms-select-control';
    select.before(control);
    control.append(select);

    const $select = $(select);
    $select.select2({
        allowClear: true,
        width: '100%',
        selectionCssClass: 'cms-select-selection',
        dropdownCssClass: 'cms-select-options'
    });

    const button = document.createElement('button');
    button.type = 'button';
    button.className = 'cms-select-preview-button';
    button.setAttribute('aria-label', 'Xem mục đã chọn');
    button.setAttribute('aria-expanded', 'false');

    const icon = document.createElement('i');
    icon.className = 'ri ri-list-check';
    icon.setAttribute('aria-hidden', 'true');
    button.append(icon);

    const count = document.createElement('span');
    count.className = 'cms-select-count';
    count.setAttribute('aria-hidden', 'true');
    if (select.multiple) button.append(count);

    const preview = document.createElement('div');
    preview.id = select.id + '-selection-preview';
    preview.className = 'cms-select-preview';
    preview.hidden = true;
    preview.tabIndex = 0;
    preview.setAttribute('role', 'region');
    preview.setAttribute('aria-label', 'Mục đã chọn');
    button.setAttribute('aria-controls', preview.id);

    const title = document.createElement('p');
    title.className = 'cms-select-preview-title';
    title.textContent = 'Đã chọn';
    const list = document.createElement('ul');
    preview.append(title, list);
    control.append(button, preview);

    let hoverTimer;
    let openedByHover = false;

    function showPreview(open, fromHover = false) {
        clearTimeout(hoverTimer);
        preview.hidden = !open;
        openedByHover = open && fromHover;
        button.setAttribute('aria-expanded', String(open));
    }

    function syncPreview() {
        const items = Array.from(select.selectedOptions)
            .filter(option => option.value !== '' && !option.hasAttribute('data-selection-placeholder'))
            .map(option => {
                const item = document.createElement('li');
                item.textContent = option.label.trim();
                return item;
            });

        count.textContent = items.length;
        list.replaceChildren(...items);
        button.hidden = items.length === 0;
        if (button.hidden) showPreview(false);

        // The preview shows the full label, so omit Select2's native tooltip.
        control.querySelectorAll('.select2-selection__rendered[title], .select2-selection__choice[title]')
            .forEach(label => label.removeAttribute('title'));
    }

    $select.on('change.cmsSelectionPreview', syncPreview);
    $select.on('select2:opening', () => showPreview(false));
    syncPreview();

    control.addEventListener('pointerenter', event => {
        clearTimeout(hoverTimer);
        if (event.pointerType !== 'mouse' || button.hidden || !preview.hidden) return;

        hoverTimer = setTimeout(() => {
            if (!control.querySelector('.select2-container--open')) showPreview(true, true);
        }, 300);
    });

    control.addEventListener('pointerleave', () => {
        clearTimeout(hoverTimer);
        if (openedByHover && !preview.contains(document.activeElement)) {
            hoverTimer = setTimeout(() => showPreview(false), 160);
        }
    });

    button.addEventListener('click', () => showPreview(preview.hidden || openedByHover));
    control.addEventListener('focusout', event => {
        if (!control.contains(event.relatedTarget) && (!openedByHover || !control.matches(':hover'))) {
            showPreview(false);
        }
    });
    document.addEventListener('pointerdown', event => {
        if (!control.contains(event.target)) showPreview(false);
    });
    document.addEventListener('keydown', event => {
        if (event.key !== 'Escape' || preview.hidden) return;
        const returnFocus = preview.contains(document.activeElement);
        showPreview(false);
        if (returnFocus) button.focus();
    });
}
