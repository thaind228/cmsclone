'use strict';

(() => {
    const dialog = document.getElementById('cms-image-picker-dialog');
    const frame = dialog.querySelector('iframe');
    let onSelect;
    let selectedUrl;
    let trigger;

    function open(callback, opener = document.activeElement) {
        if (dialog.open) return;

        const url = new URL(document.body.dataset.cmsImagePickerUrl, window.location.href);
        url.searchParams.set('imageOnly', 'true');
        url.searchParams.set('dialog', 'true');
        onSelect = callback;
        selectedUrl = null;
        trigger = opener;

        // Keep the picker within an edit form's Bootstrap focus trap when opened from a modal.
        const parentModal = opener?.closest('.modal.show');
        (parentModal || document.body).append(dialog);
        frame.src = url.href;
        dialog.showModal();
    }

    dialog.addEventListener('close', () => {
        frame.removeAttribute('src');
        document.body.append(dialog);
        trigger?.focus();
        if (selectedUrl) onSelect(selectedUrl);
    });
    dialog.querySelector('[data-cms-image-close]').addEventListener('click', () => dialog.close());
    dialog.addEventListener('keydown', event => {
        if (event.key === 'Escape') event.stopPropagation();
    });

    window.addEventListener('message', event => {
        if (!dialog.open || event.origin !== window.location.origin || event.source !== frame.contentWindow) return;

        if (event.data?.type === 'cms:image-picker:close') {
            dialog.close();
        } else if (event.data?.type === 'cms:image-picker:select' &&
            typeof event.data.url === 'string' && /^https?:\/\//i.test(event.data.url)) {
            selectedUrl = event.data.url;
            dialog.close();
        }
    });

    document.addEventListener('click', event => {
        const button = event.target.closest('button[data-cms-image-browse]');
        if (!button) return;

        const input = button.closest('.input-group')?.querySelector('input[data-cms-image-url]');
        if (!input || input.disabled || input.readOnly) return;

        open(url => {
            input.value = url;
            input.dispatchEvent(new Event('input', { bubbles: true }));
            input.dispatchEvent(new Event('change', { bubbles: true }));
        }, button);
    });

    window.cmsImagePicker = { open };
})();
