'use strict';

let currentEditor = null;

window.imagePickerType = "";


// ===============================
// Image Picker Plugin
// ===============================

class ImagePickerPlugin {


    constructor(editor) {

        this.editor = editor;

    }


    init() {

        const editor = this.editor;


        editor.ui.componentFactory.add(
            'imagePicker',
            locale => {


                const button = new CKEDITOR.ButtonView(locale);


                button.set({

                    label: 'Chọn ảnh CDN',

                    tooltip: true,

                    withText: true

                });



                button.on('execute', () => {

                    currentEditor = editor;

                    if (window.cmsImagePicker) {
                        window.cmsImagePicker.open(url => openCKEditorImageSizeDialog(url, editor));
                        return;
                    }
                    window.imagePickerType = "ckeditor";

                    openImagePickerCKEditor();

                });



                return button;


            }
        );

    }

}


class YoutubePlugin {
    constructor(editor) {
        this.editor = editor;
    }

    init() {
        const editor = this.editor;
        editor.ui.componentFactory.add('youtube', locale => {
            const button = new CKEDITOR.ButtonView(locale);
            button.set({ label: 'YouTube', tooltip: true, withText: true });
            button.on('execute', () => {
                const url = prompt("Nhập link YouTube:");
                if (!url) return;
                const videoId = getYoutubeId(url);
                if (!videoId) {
                    alert("Link YouTube không hợp lệ!");
                    return;
                }
                openYoutubeSizeDialog(editor, videoId);
            });
            return button;
        });
    }
}


class ResizeCdnImagePlugin {
    constructor(editor) {
        this.editor = editor;
    }

    init() {
        const editor = this.editor;

        editor.ui.componentFactory.add('resizeCdnImage', locale => {
            const button = new CKEDITOR.ButtonView(locale);

            button.set({
                label: 'Đổi kích thước ảnh',
                tooltip: true,
                withText: true
            });

            button.on('execute', () => {
                const image = editor.model.document.selection.getSelectedElement();

                if (!image || !['imageBlock', 'imageInline'].includes(image.name)) {
                    alert('Vui lòng chọn ảnh cần chỉnh kích thước.');
                    return;
                }

                currentEditor = editor;
                openCKEditorImageSizeDialog(null, editor, image);
            });

            return button;
        });
    }
}

// ===============================
// Init CKEditor
// ===============================

function openYoutubeSizeDialog(editor, videoId) {
    let $modal = $("#youtubeSizeModal");

    if (!$modal.length) {
        $("body").append(`
            <div class="modal fade" id="youtubeSizeModal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Kích thước video YouTube</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Đơn vị kích thước</label>
                                <select id="youtubeSizeUnit" class="form-control">
                                    <option value="%">Phần trăm (%)</option>
                                    <option value="px">Pixel (px)</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label>Chiều rộng</label>
                                <div class="input-group">
                                    <input type="number" id="youtubeSizeValue" class="form-control" min="1" max="100" value="100" step="1">
                                    <span class="input-group-addon" id="youtubeSizeSuffix">%</span>
                                </div>
                                <small class="text-muted" id="youtubeSizeHint">1–100% chiều rộng vùng nội dung.</small>
                            </div>
                            <p class="text-muted">Chiều cao tự động tính theo tỷ lệ video 16:9.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Hủy</button>
                            <button type="button" class="btn btn-primary" id="btnInsertYoutube">Nhúng video</button>
                        </div>
                    </div>
                </div>
            </div>
        `);

        $modal = $("#youtubeSizeModal");

        $("#youtubeSizeUnit").on("change", function () {
            const unit = $(this).val();
            $("#youtubeSizeSuffix").text(unit);
            $("#youtubeSizeValue").attr("max", unit === "%" ? 100 : 10000);
            $("#youtubeSizeValue").val(unit === "%" ? 100 : 560);
            $("#youtubeSizeHint").text(unit === "%" ? "1–100% chiều rộng vùng nội dung." : "Nhập chiều rộng video theo pixel.");
        });

        $("#btnInsertYoutube").on("click", function () {
            const targetEditor = $modal.data("editor");
            const targetVideoId = $modal.data("video-id");
            const unit = $("#youtubeSizeUnit").val();
            const width = Number($("#youtubeSizeValue").val());
            const maxWidth = unit === "%" ? 100 : 10000;

            if (!targetEditor || !targetVideoId) {
                alert("Không tìm thấy CKEditor hoặc video YouTube.");
                return;
            }

            if (!Number.isInteger(width) || width < 1 || width > maxWidth) {
                alert(unit === "%" ? "Chiều rộng phải từ 1 đến 100%." : "Chiều rộng phải từ 1 đến 10000px.");
                return;
            }

            const widthStyle = `${width}${unit}`;
            const pixelWidth = unit === "px" ? width : 1280;
            const pixelHeight = Math.round(pixelWidth * 9 / 16);

            const html = `<p><iframe src="https://www.youtube.com/embed/${targetVideoId}" style="display:block;width:${widthStyle};max-width:100%;aspect-ratio:16/9;border:0;" width="${pixelWidth}" height="${pixelHeight}" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe></p>`;

            try {
                const viewFragment = targetEditor.data.processor.toView(html);
                const modelFragment = targetEditor.data.toModel(viewFragment);

                targetEditor.model.change(() => {
                    targetEditor.model.insertContent(modelFragment);
                });
            } catch (error) {
                console.error("Lỗi chèn video YouTube:", error);
                alert("Không thể chèn video. Bạn hãy kiểm tra Console.");
                return;
            }

            $modal.one("hidden.bs.modal", function () {
                targetEditor.editing.view.focus();
            });

            hideModalCompat($modal);
        });
    }

    $modal.data("editor", editor);
    $modal.data("video-id", videoId);
    $("#youtubeSizeUnit").val("%").trigger("change");
    $("#youtubeSizeValue").val(100);
    if (window.bootstrap && window.bootstrap.Modal) {
        window.bootstrap.Modal.getOrCreateInstance($modal[0]).show();
    } else {
        $modal.modal("show");
    }
}
function hideModalCompat($modal) {
    if (window.bootstrap && window.bootstrap.Modal) {
        const instance = window.bootstrap.Modal.getInstance($modal[0]) || window.bootstrap.Modal.getOrCreateInstance($modal[0]);
        instance.hide();
    } else if (typeof $modal.modal === "function") {
        $modal.modal("hide");
    } else {
        console.error("Không tìm thấy Bootstrap Modal.");
    }
}
function initCKEditor(elementId) {

    const element = document.querySelector('#' + elementId);

    if (!element) {
        return;
    }

    // Kiểm tra thư viện CKEditor đã tải chưa
    if (typeof window.CKEDITOR === 'undefined') {
        console.error(
            'CKEDITOR chưa được tải!',
            elementId
        );
        return;
    }

    const {
        ClassicEditor,
        Essentials,
        Paragraph,
        Bold,
        Italic,
        Underline,
        Strikethrough,
        Subscript,
        Superscript,
        RemoveFormat,
        Font,
        FontSize,
        FontFamily,
        FontColor,
        FontBackgroundColor,
        Heading,
        Alignment,
        List,
        ListProperties,
        Indent,
        IndentBlock,
        Link,
        AutoLink,
        Image,
        ImageBlock,
        ImageInline,
        ImageCaption,
        ImageStyle,
        ImageToolbar,
        ImageResize,
        ImageInsert,
        ImageInsertViaUrl,
        ImageUpload,
        Table,
        TableToolbar,
        TableCellProperties,
        TableProperties,
        TableColumnResize,
        TableCaption,
        Code,
        CodeBlock,
        MediaEmbed,
        AutoMediaEmbed,
        BlockQuote,
        HorizontalLine,
        Highlight,
        FindAndReplace,
        SpecialCharacters,
        SpecialCharactersEssentials,
        SpecialCharactersLatin,
        SpecialCharactersMathematical,
        SpecialCharactersCurrency,
        Autoformat,
        AutoImage,
        WordCount,
        Undo,
        GeneralHtmlSupport,
        SourceEditing
    } = window.CKEDITOR;

    // Kiểm tra các thành phần bắt buộc
    if (!ClassicEditor || !window.CKEDITOR.ButtonView) {
        console.error(
            'CKEditor đã tải nhưng thiếu ClassicEditor hoặc ButtonView.',
            window.CKEDITOR
        );
        return;
    }

    ClassicEditor.create(element, {

        licenseKey: 'GPL',

        plugins: [
            Essentials,
            Paragraph,
            Bold,
            Italic,
            Underline,
            Strikethrough,
            Subscript,
            Superscript,
            RemoveFormat,
            Font,
            FontSize,
            FontFamily,
            FontColor,
            FontBackgroundColor,
            Heading,
            Alignment,
            List,
            ListProperties,
            Indent,
            IndentBlock,
            Link,
            AutoLink,
            Image,
            ImageBlock,
            ImageInline,
            ImageCaption,
            ImageStyle,
            ImageToolbar,
            ImageResize,
            ImageInsert,
            ImageInsertViaUrl,
            ImageUpload,
            Table,
            TableToolbar,
            TableCellProperties,
            TableProperties,
            TableColumnResize,
            TableCaption,
            Code,
            CodeBlock,
            MediaEmbed,
            AutoMediaEmbed,
            BlockQuote,
            HorizontalLine,
            Highlight,
            FindAndReplace,
            SpecialCharacters,
            SpecialCharactersEssentials,
            SpecialCharactersLatin,
            SpecialCharactersMathematical,
            SpecialCharactersCurrency,
            Autoformat,
            AutoImage,
            WordCount,
            Undo,
            GeneralHtmlSupport,
            SourceEditing,
            ImagePickerPlugin,
            YoutubePlugin,
            ResizeCdnImagePlugin
        ],

        toolbar: {
            items: [
                'undo',
                'redo',
                '|',
                'sourceEditing',
                'findAndReplace',
                '|',
                'heading',
                '|',
                'bold',
                'italic',
                'underline',
                'strikethrough',
                'subscript',
                'superscript',
                'removeFormat',
                '|',
                'fontFamily',
                'fontSize',
                'fontColor',
                'fontBackgroundColor',
                'highlight',
                '|',
                'alignment',
                '|',
                'bulletedList',
                'numberedList',
                'outdent',
                'indent',
                '|',
                'link',
                'insertImage',
                'imagePicker',
                'youtube',
                'mediaEmbed',
                'insertTable',
                'blockQuote',
                'code',
                'codeBlock',
                'horizontalLine',
                'specialCharacters'
            ],
            shouldNotGroupWhenFull: false
        },

        mediaEmbed: {
            previewsInData: true
        },

        heading: {
            options: [
                {
                    model: 'paragraph',
                    title: 'Paragraph',
                    class: 'ck-heading_paragraph'
                },
                {
                    model: 'heading1',
                    view: 'h1',
                    title: 'Heading 1',
                    class: 'ck-heading_heading1'
                },
                {
                    model: 'heading2',
                    view: 'h2',
                    title: 'Heading 2',
                    class: 'ck-heading_heading2'
                },
                {
                    model: 'heading3',
                    view: 'h3',
                    title: 'Heading 3',
                    class: 'ck-heading_heading3'
                }
            ]
        },

        fontSize: {
            options: [
                10, 11, 12, 13, 'default',
                16, 18, 20, 24, 28, 32, 36, 48
            ]
        },

        fontFamily: {
            options: [
                'default',
                'Arial, Helvetica, sans-serif',
                'Times New Roman, Times, serif',
                'Courier New, Courier, monospace',
                'Georgia, serif',
                'Verdana, Geneva, sans-serif',
                'Tahoma, Geneva, sans-serif'
            ],
            supportAllValues: true
        },

        fontColor: {
            columns: 8
        },

        fontBackgroundColor: {
            columns: 8
        },

        image: {
            toolbar: [
                'imageStyle:inline',
                'imageStyle:block',
                'imageStyle:side',
                '|',
                'toggleImageCaption',
                'imageTextAlternative',
                '|',
                'resizeImage:25',
                'resizeImage:50',
                'resizeImage:75',
                'resizeImage:original',
                '|',
                'resizeCdnImage'
            ]
        },

        table: {
            contentToolbar: [
                'tableColumn',
                'tableRow',
                'mergeTableCells',
                '|',
                'tableProperties',
                'tableCellProperties',
                'toggleTableCaption'
            ]
        },

        htmlSupport: {
            htmlIframeSandbox: false,
            allow: [
                {
                    name: /./,
                    attributes: true,
                    classes: true,
                    styles: true
                }
            ]
        },

        link: {
            decorators: {
                openInNewTab: {
                    mode: 'manual',
                    label: 'Mở tab mới',
                    attributes: {
                        target: '_blank',
                        rel: 'noopener noreferrer'
                    }
                }
            }
        }

    })
        .then(editor => {

            window[elementId] = editor;

            const editable = editor.ui.view.editable.element;

            function autoResizeCKEditor() {
                editable.style.height = 'auto';
                editable.style.height =
                    Math.max(editable.scrollHeight, 400) + 'px';
                editable.style.overflowY = 'hidden';
            }

            editor.model.document.on('change:data', function () {
                autoResizeCKEditor();
            });

            setTimeout(function () {
                autoResizeCKEditor();
            }, 100);

            console.log('CKEditor loaded:', elementId);

        })
        .catch(error => {
            console.error('CKEditor error:', error);
        });
}
function getYoutubeId(url) {
    try {
        const parsed = new URL(url.trim());
        let id = "";

        if (parsed.hostname === "youtu.be" || parsed.hostname.endsWith(".youtu.be")) {
            id = parsed.pathname.split("/").filter(Boolean)[0] || "";
        } else if (parsed.hostname.endsWith("youtube.com") || parsed.hostname === "youtube-nocookie.com") {
            id = parsed.searchParams.get("v") || "";
            if (!id) {
                const parts = parsed.pathname.split("/").filter(Boolean);
                if (["embed", "shorts", "live", "v"].includes(parts[0])) id = parts[1] || "";
            }
        }

        return /^[a-zA-Z0-9_-]{11}$/.test(id) ? id : null;
    } catch {
        return null;
    }
}


// ===============================
// Insert Image From CDN
// ===============================

function setCKEditorImage(url) {
    if (!currentEditor || !url) return;

    const $picker = $("#imagePickerModal");

    if ($picker.length && ($picker.hasClass("in") || $picker.hasClass("show"))) {
        $picker.one("hidden.bs.modal", function () {
            openCKEditorImageSizeDialog(url, currentEditor);
        });

        $picker.modal("hide");
    } else {
        openCKEditorImageSizeDialog(url, currentEditor);
    }
}

function openCKEditorImageSizeDialog(url, editor = currentEditor, imageElement = null) {
    let $modal = $("#ckEditorImageSizeModal");

    if (!$modal.length) {
        $("body").append(`
            <div class="modal fade" id="ckEditorImageSizeModal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Kích thước ảnh CDN</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Đơn vị kích thước</label>
                                <select id="ckImageSizeUnit" class="form-control">
                                    <option value="%">Phần trăm (%)</option>
                                    <option value="px">Pixel (px)</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label>Chiều rộng</label>
                                <div class="input-group">
                                    <input type="number" id="ckImageSizeValue"
                                           class="form-control" min="1" max="100"
                                           value="100" step="1">
                                    <span class="input-group-addon" id="ckImageSizeSuffix">%</span>
                                </div>
                                <small class="text-muted" id="ckImageSizeHint">
                                    1–100% chiều rộng vùng nội dung.
                                </small>
                            </div>
                            <p class="text-muted">
                                <i class="fa fa-info-circle"></i>
                                Chiều cao tự động điều chỉnh theo tỷ lệ ảnh gốc.
                            </p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Hủy</button>
                            <button type="button" class="btn btn-primary" id="btnInsertSizedCKImage">
                                <i class="fa fa-check"></i> Chèn ảnh
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        `);

        $modal = $("#ckEditorImageSizeModal");

        $("#ckImageSizeUnit").on("change", function () {
            const unit = $(this).val();
            $("#ckImageSizeSuffix").text(unit);
            $("#ckImageSizeValue").attr("max", unit === "%" ? 100 : 10000);
            $("#ckImageSizeValue").val(unit === "%" ? 100 : 600);
            $("#ckImageSizeHint").text(
                unit === "%"
                    ? "1–100% chiều rộng vùng nội dung."
                    : "Nhập chiều rộng ảnh theo pixel."
            );
        });

        $("#btnInsertSizedCKImage").on("click", function () {
            const mode = $modal.data("mode");
            const targetEditor = $modal.data("editor");
            const imageElement = $modal.data("image-element");
            const unit = $("#ckImageSizeUnit").val();
            const width = Number($("#ckImageSizeValue").val());
            const maxWidth = unit === "%" ? 100 : 10000;

            if (!targetEditor) {
                alert("Không tìm thấy CKEditor.");
                return;
            }

            if (!Number.isFinite(width) || !Number.isInteger(width) || width < 1 || width > maxWidth) {
                alert(unit === "%" ? "Chiều rộng phải là số nguyên từ 1 đến 100%." : "Chiều rộng phải là số nguyên từ 1 đến 10000px.");
                return;
            }

            try {
                if (mode === "resize") {
                    if (!imageElement) {
                        alert("Không tìm thấy ảnh cần chỉnh kích thước.");
                        return;
                    }

                    targetEditor.model.change(writer => {
                        writer.setSelection(imageElement, "on");
                    });

                    targetEditor.execute("resizeImage", { width: `${width}${unit}` });
                } else {
                    const imageUrl = $modal.data("image-url");
                    if (!imageUrl) {
                        alert("Không tìm thấy URL ảnh CDN.");
                        return;
                    }

                    targetEditor.execute("insertImage", { source: imageUrl });
                    targetEditor.execute("resizeImage", { width: `${width}${unit}` });
                }

                $modal.modal("hide");
                targetEditor.editing.view.focus();
            } catch (error) {
                console.error("Không thể thay đổi kích thước ảnh:", error);
                alert("Không thể thay đổi kích thước ảnh. Vui lòng thử lại.");
            }
        });
    }

    $modal.data("image-url", url);
    $modal.data("editor", editor);
    $modal.data("image-element", imageElement);
    $modal.data("mode", imageElement ? "resize" : "insert");

    $modal.find(".modal-title").text(imageElement ? "Đổi kích thước ảnh" : "Kích thước ảnh CDN");
    $("#btnInsertSizedCKImage").html(imageElement ? '<i class="fa fa-check"></i> Áp dụng' : '<i class="fa fa-check"></i> Chèn ảnh');

    $("#ckImageSizeUnit").val("%").trigger("change");
    $("#ckImageSizeValue").val(100);
    $modal.modal("show");
}
function openImagePickerCKEditor() {
    const imagePickerUrl = window.imagePickerUrl || document.body.dataset.cmsImagePickerUrl;
    if (!imagePickerUrl) return;

    console.log("ImagePicker URL:", imagePickerUrl);

    $("#imagePickerFrame").attr("src", imagePickerUrl);
    $("#imagePickerModal").modal("show");
}
