'use strict';

(() => {
    const dialog = document.getElementById('cms-image-picker-dialog');
    const frame = dialog.querySelector('iframe');
    const preview = document.getElementById('cms-image-preview-dialog');
    const previewImage = preview.querySelector('img');
    const previewStatus = preview.querySelector('[data-cms-preview-status]');
    let onSelect;
    let selectedUrl;
    let trigger;
    let previewTrigger;

    function showDialog(modal, opener) {
        // Keep dialogs within an edit form's Bootstrap focus trap.
        (opener?.closest('.modal.show') || document.body).append(modal);
        modal.showModal();
    }

    function open(callback, opener = document.activeElement) {
        if (dialog.open) return;

        const url = new URL(document.body.dataset.cmsImagePickerUrl, window.location.href);
        url.searchParams.set('imageOnly', 'true');
        url.searchParams.set('dialog', 'true');
        onSelect = callback;
        selectedUrl = null;
        trigger = opener;

        frame.src = url.href;
        showDialog(dialog, opener);
    }

    function showPreview(input, button) {
        previewTrigger = button;
        previewImage.hidden = true;
        previewStatus.hidden = false;
        previewStatus.textContent = 'Chưa có URL ảnh.';
        showDialog(preview, button);

        const value = input.value.trim();
        if (!value) return;

        let url;
        try {
            url = new URL(value, document.baseURI);
        } catch {
            // Leave the URL unset so the dialog can show the validation message.
        }
        if (!url || !['http:', 'https:'].includes(url.protocol)) {
            previewStatus.textContent = 'URL ảnh không hợp lệ.';
            return;
        }

        previewStatus.textContent = 'Đang tải ảnh…';
        previewImage.src = url.href;
    }

    previewImage.addEventListener('load', () => {
        previewImage.hidden = false;
        previewStatus.hidden = true;
    });
    previewImage.addEventListener('error', () => {
        previewStatus.textContent = 'Không tải được ảnh từ URL này.';
    });
    preview.querySelector('[data-cms-preview-close]').addEventListener('click', () => preview.close());
    preview.addEventListener('click', event => {
        if (event.target === preview) preview.close();
    });
    preview.addEventListener('close', () => {
        previewImage.removeAttribute('src');
        document.body.append(preview);
        previewTrigger?.focus();
    });

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
    preview.addEventListener('keydown', event => {
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
        const button = event.target.closest('button[data-cms-image-browse], button[data-cms-image-preview]');
        if (!button) return;

        const input = button.closest('.input-group')?.querySelector('input[data-cms-image-url]');
        if (!input) return;

        if (button.hasAttribute('data-cms-image-preview')) {
            showPreview(input, button);
            return;
        }

        if (input.disabled || input.readOnly) return;

        open(url => {
            input.value = url;
            input.dispatchEvent(new Event('input', { bubbles: true }));
            input.dispatchEvent(new Event('change', { bubbles: true }));
        }, button);
    });

    window.cmsImagePicker = { open };
})();
