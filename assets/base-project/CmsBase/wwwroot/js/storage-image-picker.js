'use strict';

(() => {
    const picker = document.getElementById('storage-picker');
    const grid = picker.querySelector('[data-media-grid]');
    const template = picker.querySelector('[data-media-card]');
    const path = picker.querySelector('[data-media-path]');
    const notice = picker.querySelector('[data-media-notice]');
    const search = picker.querySelector('[data-media-search]');
    const selection = picker.querySelector('[data-media-selection]');
    const confirmSelection = picker.querySelector('[data-media-confirm]');
    const pagination = picker.querySelector('[data-media-pagination]');
    const more = picker.querySelector('[data-media-more]');
    const folderForm = picker.querySelector('#storage-new-folder');
    const newFolder = picker.querySelector('[data-media-new-folder]');
    const upload = picker.querySelector('[data-media-upload]');
    const fileInput = picker.querySelector('[data-media-file-input]');
    const inDialog = picker.dataset.inDialog === 'true';
    let currentPrefix = '';
    let page = 1;
    let pending;
    let selectedButton;
    let searchTimer;
    let writing = false;

    function showNotice(text, error = false) {
        notice.textContent = text;
        notice.classList.toggle('text-danger', error);
    }

    function setSelection(button, url = '') {
        selectedButton?.setAttribute('aria-pressed', 'false');
        selectedButton = button;
        button?.setAttribute('aria-pressed', 'true');
        selection.value = url;
        confirmSelection.disabled = !url;
    }

    function choose(url) {
        if (!url) return;

        if (inDialog) {
            window.parent.postMessage({ type: 'cms:image-picker:select', url }, window.location.origin);
        } else if (window.parent !== window && window.parent.imagePickerType === 'ckeditor') {
            window.parent.setCKEditorImage(url);
        } else if (window.parent !== window && window.parent.imagePickerType === 'event') {
            window.parent.setImagePicker(url);
        } else {
            selection.select();
        }
    }

    function navigate(prefix) {
        clearTimeout(searchTimer);
        currentPrefix = prefix;
        search.value = '';
        renderPath();
        load(true);
    }

    function renderPath() {
        path.replaceChildren();
        const parts = ['Gốc', ...currentPrefix.split('/').filter(Boolean)];
        let prefix = '';
        parts.forEach((part, index) => {
            if (index > 0) prefix += part + '/';
            const item = document.createElement('li');
            item.className = 'breadcrumb-item';
            if (index === parts.length - 1) {
                item.setAttribute('aria-current', 'page');
                item.textContent = part;
            } else {
                const button = document.createElement('button');
                const targetPrefix = prefix;
                button.type = 'button';
                button.textContent = part;
                button.addEventListener('click', () => navigate(targetPrefix));
                item.append(button);
            }
            path.append(item);
        });
    }

    async function request(url, options) {
        const response = await fetch(url, options);
        if (response.redirected || response.status === 401) throw new Error('Phiên đăng nhập đã hết hạn.');
        if (response.status === 403) throw new Error('Máy chủ từ chối yêu cầu Storage (HTTP 403).');
        if (!response.ok) throw new Error('Yêu cầu thất bại (HTTP ' + response.status + ').');
        if (!response.headers.get('content-type')?.includes('application/json')) {
            throw new Error('Storage trả về phản hồi không hợp lệ.');
        }
        const data = await response.json();
        if (!data.success) throw new Error(data.message || 'Yêu cầu thất bại.');
        return data;
    }

    async function load(reset) {
        if (reset) {
            pending?.abort();
            page = 1;
            setSelection(null);
            grid.replaceChildren();
            pagination.hidden = true;
        } else if (pending || pagination.hidden) {
            return false;
        }

        const controller = new AbortController();
        pending = controller;
        more.disabled = true;
        grid.setAttribute('aria-busy', 'true');
        showNotice('Đang tải…');

        const url = new URL(picker.dataset.listUrl, window.location.href);
        url.search = new URLSearchParams({
            prefix: currentPrefix,
            page,
            keyword: search.value.trim(),
            imageOnly: picker.dataset.imageOnly
        });

        try {
            const data = await request(url, { signal: controller.signal });
            if (pending !== controller) return false;
            data.folders.forEach(name => appendCard(name, { folder: true }));
            data.files.forEach(file => appendCard(file.key.split('/').pop(), { file }));
            pagination.hidden = !data.hasMore;
            page++;
            const itemName = picker.dataset.imageOnly === 'true' ? 'ảnh' : 'tệp';
            const emptyMessage = search.value.trim()
                ? 'Không có kết quả khớp với từ khóa.'
                : 'Thư mục này chưa có ' + itemName + ' hoặc thư mục con.';
            showNotice(grid.childElementCount ? '' : emptyMessage);
            return true;
        } catch (error) {
            if (error.name !== 'AbortError') showNotice(error.message, true);
            return false;
        } finally {
            if (pending === controller) {
                pending = null;
                more.disabled = false;
                grid.removeAttribute('aria-busy');
            }
        }
    }

    function appendCard(name, { folder, file }) {
        const card = template.content.firstElementChild.cloneNode(true);
        const button = card.querySelector('.cms-media-card-select');
        const preview = card.querySelector('.cms-media-preview');
        const title = card.querySelector('.cms-media-name');
        const remove = card.querySelector('.cms-media-delete');
        title.textContent = name;
        title.title = name;

        if (folder) {
            card.classList.add('cms-media-folder');
            preview.firstElementChild.className = 'ri ri-folder-open-line';
            button.setAttribute('aria-label', 'Mở thư mục ' + name);
            button.addEventListener('click', () => navigate(currentPrefix + name + '/'));
        } else {
            button.setAttribute('aria-pressed', 'false');
            button.addEventListener('click', () => setSelection(button, file.url));
            button.addEventListener('dblclick', () => choose(file.url));
            if (file.isImage) {
                const image = document.createElement('img');
                image.loading = 'lazy';
                image.decoding = 'async';
                image.alt = '';
                image.addEventListener('error', () => image.remove());
                image.src = file.url;
                preview.append(image);
            }
        }

        if (remove) {
            remove.setAttribute('aria-label', 'Xóa ' + name);
            remove.addEventListener('click', () => {
                const message = folder
                    ? 'Xóa thư mục “' + name + '” và toàn bộ tệp bên trong?'
                    : 'Xóa tệp “' + name + '”?';
                if (window.confirm(message)) {
                    const body = folder ? { folderName: name, prefix: currentPrefix } : { key: file.key };
                    write(folder ? picker.dataset.deleteFolderUrl : picker.dataset.deleteUrl, body, 'Đã xóa.');
                }
            });
        }
        grid.append(card);
    }

    async function write(url, values, message) {
        if (writing) return false;
        const prefix = currentPrefix;
        const body = values instanceof FormData ? values : new FormData();
        if (!(values instanceof FormData)) {
            Object.entries(values).forEach(([key, value]) => body.append(key, value));
        }
        body.append('__RequestVerificationToken', picker.querySelector('[name="__RequestVerificationToken"]').value);
        writing = true;
        showNotice('Đang lưu…');

        try {
            await request(url, { method: 'POST', body });
            if (prefix === currentPrefix && await load(true) && grid.childElementCount) showNotice(message);
            return true;
        } catch (error) {
            showNotice(error.message, true);
            return false;
        } finally {
            writing = false;
        }
    }

    search.addEventListener('input', () => {
        clearTimeout(searchTimer);
        searchTimer = setTimeout(() => load(true), 250);
    });
    more.addEventListener('click', () => load(false));
    confirmSelection.addEventListener('click', () => choose(selection.value));

    if (inDialog) {
        const cancel = () => window.parent.postMessage({ type: 'cms:image-picker:close' }, window.location.origin);
        picker.querySelector('[data-media-cancel]').addEventListener('click', cancel);
        document.addEventListener('keydown', event => {
            if (event.key === 'Escape' && !event.defaultPrevented) cancel();
        });
    } else if (window.parent === window) {
        confirmSelection.hidden = true; // The readonly URL field remains available on the full page.
    }

    if (folderForm) {
        newFolder.addEventListener('click', () => {
            folderForm.hidden = !folderForm.hidden;
            newFolder.setAttribute('aria-expanded', String(!folderForm.hidden));
            if (!folderForm.hidden) folderForm.elements.folderName.focus();
        });
        folderForm.addEventListener('submit', async event => {
            event.preventDefault();
            const name = folderForm.elements.folderName.value.trim();
            if (!name) return;
            if (await write(picker.dataset.createFolderUrl, { folderName: name, prefix: currentPrefix }, 'Đã tạo thư mục.')) {
                folderForm.reset();
                folderForm.hidden = true;
                newFolder.setAttribute('aria-expanded', 'false');
            }
        });
        upload.addEventListener('click', () => { if (!writing) fileInput.click(); });
        fileInput.addEventListener('change', async () => {
            if (!fileInput.files.length) return;
            const body = new FormData();
            for (const file of fileInput.files) body.append('files', file);
            body.append('prefix', currentPrefix);
            upload.disabled = true;
            await write(picker.dataset.uploadUrl, body, 'Đã tải tệp lên.');
            fileInput.value = '';
            upload.disabled = false;
        });
    }

    renderPath();
    load(true);
})();
