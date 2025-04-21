$(document).ready(function () {
    BindType('dynamicdata');
    console.log(typeof ConfirmDelete);
    $(document).on("change", ".doctypechange", function () {
        var id = $(this).attr('id').split("_")[1];
        BindDocument('Documents_' + id + '__DocId', $("#Documents_" + id + "__TypeId").val());
    });

    $('#CaseDocUpload').submit(function (e) {
        debugger;
        e.preventDefault();
        var form = $('#CaseDocUpload')[0];
        var formData = new FormData(form);
        $.ajax({
            url: form.action, // or manually: '/CaseManage/UploadCaseDocs'
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.success === true) {
                    Swal.fire({
                        title: 'Success!',
                        text: 'Document uploaded successfully!',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            refreshDocsTable(response.caseId, response.caseType);
                        }
                    });
                } else {
                    Swal.fire({
                        title: 'Failure!',
                        text: response.message,
                        icon: 'fail',
                        confirmButtonText: 'OK'
                    })
                }
            },
            error: function (xhr) {
                alert("Error: " + xhr.responseText);
            }
        });
    });
});
function refreshDocsTable(caseId, caseType) {
    $.ajax({
        url: '/Litigation/CaseManage/GetUpdatedDocs?caseId=' + caseId + "&reft=" + caseType,
        type: 'GET',
        success: function (html) {
            $('#tblCaseHistory tbody').html(html);
        },
        error: function () {
            alert('Failed to refresh document list.');
        }
    });
}

function ConfirmDelete(DocId, fpath) {
    Swal.fire({
        title: "Are you sure?",
        text: "Do you want to delete the file: " + fpath + "?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            deleteFile(DocId, fpath);
        }
    });
};

function deleteFile(fileId, fpath) {
    fetch("/Litigation/CaseManage/DeleteDoc?id=" + fileId + "&fPath=" + fpath, {
        method: 'POST'
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                Swal.fire("Deleted!", "Your file has been deleted.", "success")
                    .then(() =>
                        //location.reload()
                        refreshDocsTable($("#CaseId").val(), $("#Reference").val()));
            } else {
                Swal.fire("Error!", "Something went wrong.", "error");
            }
        });
};

function removeKolom(id) {
    debugger;
    $(".removedoc_" + id).remove();
}
var incval = 0;
function defaultload() {
    var content = '<tr class="removedoc_' + incval + '"><td><select class="form-control select2bs4 doctypechange" id="Documents_' + incval + '__TypeId" name="Documents[' + incval + '].TypeId">' +
        '</select></td><td><select class="form-control select2bs4" id="Documents_' + incval + '__DocId" name="Documents[' + incval + '].DocId">' +
        '</select></td><td><input type="date" id="OrderDate_' + incval + '__DocDate" name="Documents[' + incval + '].DocDate" class="form-control" /></td><td><input type="file" name="Documents[' + incval + '].Document" id="Documents_' + incval + '__Document" width="300px" placeholder="Enter court/Bench adress" class="form-control" /></td><td><button class="add-btn-repeat btn btn-success" id="btnDraft" onclick="addElement()" type="button"><i class="fa fa-plus" aria-hidden="true"></i></button> </td></tr>';
    $('#tblDocs tbody').append(content);
    var data = $("#dynamicdata").html();
    $("#Documents_" + incval + "__TypeId").html(data)
    $("#Documents_" + incval + "__TypeId").select2({
        placeholder: "Select a type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    incval++;
}
function validation() {
    var count = incval - 1;
    var type = $("#Documents_" + count + "__TypeId").val();
    var doc = $("#Documents_" + count + "__DocId").val();
    var imgv = $("#Documents_" + count + "__Document").val();
    if (type == "" || doc == "" || imgv == "") {
        return false;
    }
    return true;
}
function addElement(e) {
    var vald = validation();
    if (vald == true) {
        var content = '<tr class="removedoc_' + incval + '"><td><select class="form-control select2bs4 doctypechange" id="Documents_' + incval + '__TypeId" name="Documents[' + incval + '].TypeId">' +
            '</select></td><td><select class="form-control select2bs4" id="Documents_' + incval + '__DocId" name="Documents[' + incval + '].DocId">' +
            '</select></td><td><input type="date" id="OrderDate_' + incval + '__DocDate" name="Documents[' + incval + '].DocDate" class="form-control" /></td><td><input type="file" name="Documents[' + incval + '].Document" id="Documents_' + incval + '__Document" width="300px" placeholder="Enter court/Bench adress" class="form-control" /></td><td> <button type="button" onclick="removeKolom(' + incval + ')" class="btn btn-warning"><i class="fa fa-trash" aria-hidden="true"></i></button></td></tr>';
        $('#tblDocs tbody').append(content);
        var data = $("#dynamicdata").html();
        $("#Documents_" + incval + "__TypeId").html(data)
        $("#Documents_" + incval + "__TypeId").select2({
            placeholder: "Select a type",
            theme: "bootstrap4",
            escapeMarkup: function (m) {
                return m;
            }
        });
        $("#Documents_" + incval + "__DocId").select2({
            placeholder: "Select a document",
            theme: "bootstrap4",
            escapeMarkup: function (m) {
                return m;
            }
        });
        incval++;
    } else {
        alert("Please select all the fileds are required");
    }
}

function BindType(ddl) {
    $.getJSON("/Litigation/casemanage/DOCTypes", function (data) {
        $("#" + ddl).empty();;
        $.each(data, function (key, value) {
            $("#" + ddl).append(`<option /><option value="${key}">${value}</option>`);
        });
        defaultload();
    });
}
function BindDocument(ddl, v) {
    $("#" + ddl).empty();
    $.getJSON("/Litigation/casemanage/DdlDOTypes?TypeId=" + v, function (data) {
        $.each(data.Data, function (i, item) {
            $("#" + ddl).append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
    $("#" + ddl).select2({
        placeholder: "Select a document",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
}