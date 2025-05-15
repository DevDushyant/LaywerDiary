$(document).ready(function () {
    //initCKEditor($("#LanguageCode").val())
    $("#StateId").select2({
        placeholder: "Select a state",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#CourtTypeId").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#LanguageCode").select2({
        placeholder: "Select a court language",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseCategoryId").select2({
        placeholder: "Select case category",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#StateId").on("change", function () {
        BindCaseCategory($("#CourtTypeId").val());
        BindLanguages($("#StateId").val());
    });
});

BindCaseCategory = function (CategoryId) {
    $("#CaseCategoryId").empty()
    $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + CategoryId, function (data) {
        $.each(data, function (i, item) {
            $("#CaseCategoryId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}

BindLanguages = function (StateId) {
    $("#LanguageCode").empty();
    $.getJSON("/LawyerDiary/CourtForm/LoadLanguages?StateId=" + StateId, function (data) {
        debugger;
        $.each(data, function (i, item) {
            $("#LanguageCode").append(`<option /><option value="${item.Code}">${item.Name}</option>`);
        });
    });
}

//$("#LanguageCode").on("change", function () {
//    initCKEditor($("#LanguageCode").val());    
//});

//$("#btnReset").on('click', function () {
//    CKEDITOR.instances['FormTemplate'].setData();
//    $("#FormTemplate").val('');
//});

$(document).ready(function () {
    tinymce.init({
        selector: '#FormTemplate',
        height: 500,
        menubar: true,
        plugins: [
            'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview', 'anchor',
            'searchreplace', 'visualblocks', 'visualchars', 'code', 'fullscreen',
            'insertdatetime', 'media', 'table', 'help', 'wordcount',
            'print', 'hr', 'pagebreak', 'nonbreaking', 'autosave'
        ],
        toolbar: 'undo redo | formatselect | fontselect fontsizeselect | ' +
            'bold italic underline strikethrough forecolor backcolor | ' +
            'alignleft aligncenter alignright alignjustify | ' +
            'bullist numlist outdent indent | table | hr pagebreak | ' +
            'link image media | removeformat code preview print | help',
        content_style: `
            body { font-family:Helvetica,Arial,sans-serif; font-size:14px; padding:20px; }
            hr.tiny-ruler { border: none; border-top: 1px dashed #888; margin: 20px 0; }
        `,
        setup: function (editor) {
            editor.on('keydown', function (e) {
                if (e.key === 'Tab') {
                    e.preventDefault();
                    if (e.shiftKey) {
                        editor.execCommand('Outdent');
                    } else {
                        editor.execCommand('Indent');
                    }
                }
            });
        },
        table_toolbar: 'tableprops tabledelete | tableinsertrowbefore tableinsertrowafter tabledeleterow | ' +
            'tableinsertcolbefore tableinsertcolafter tabledeletecol',
        branding: false
    });
});



//$(document).ready(function () {
//    CKEDITOR.replace('FormTemplate');
//    $("#btnReset").on('click', function () {
//        CKEDITOR.instances['FormTemplate'].setData();
//        $("#FormTemplate").val('');
//    });
//});

//$(document).ready(function () {
//    $('#CourtFormTemplate').submit(function (event) {
//        event.preventDefault();
//        var url = $("#CourtFormTemplate").attr("action");
//        var data = $("#CourtFormTemplate").serialize();
//        $.ajax({
//            type: 'POST',            
//            url: url,
//            data: data,
//            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',

//            success: function (response) {
//                if (response.success) {
//                    Swal.fire('Success', response.message, 'success');
//                } else {
//                    Swal.fire('Error', response.message, 'error');
//                }
//            },
//            error: function () {
//                Swal.fire('Error', 'An unexpected error occurred.', 'error');
//            }
//        });
//    });
//});



