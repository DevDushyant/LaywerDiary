var btn_delete = '<button type="button" onclick="removeKolom($(this))" class="btn btn-warning"><i class="fa fa-trash" aria-hidden="true"></i></button>';
var btn_add = '<button class="add-btn-repeat btn btn-success" onclick="addElement($(this))" type="button"><i class="fa fa-plus" aria-hidden="true"></i></button>';
function removeKolom(e) {
    alert('he')
    e.parents('.kolom').remove();
}
function addElement1(e) {
    debugger;
    var trlength = $('#tblFormFields tbody tr').length;
    var clonedRow = $('#tblFormFields tbody tr:last').clone();
    clonedRow.find('input').each(function (i) {
        if (i === 0) {
            NameAtr = "Form.Fields[" + trlength + "].Key";
            IdAtr = "Form_Fields_" + trlength + "__Key";
        }
        if (i === 1) {
            NameAtr = "Form.Fields[" + trlength + "].Name";
            IdAtr = "Form_Fields_" + trlength + "__Name";
        }

        if (i === 1) {
            NameAtr = "Form.Fields[" + trlength + "].IsRequire";
            IdAtr = "Form_Fields_" + trlength + "__IsRequire";
        }
        if (i === 2) {
            NameAtr = "Form.Fields[" + trlength + "].DispOrder";
            IdAtr = "Form_Fields_" + trlength + "__DispOrder";
        }
        if (i === 3) {
            NameAtr = "Form.Fields[" + trlength + "].Placeholder";
            IdAtr = "Form_Fields_" + trlength + "__Placeholder";
        }
        if (i === 4) {
            NameAtr = "Form.Fields[" + trlength + "].length.Min";
            IdAtr = "Form_Fields_" + trlength + "__length.Min";
        }
        if (i === 5) {
            NameAtr = "Form.Fields[" + trlength + "].length.Max";
            IdAtr = "Form_Fields_" + trlength + "__length.Max";
        }
        $(this).attr('name', NameAtr).attr('id', IdAtr);
        clonedRow.find("input").val("");
    });
    clonedRow.find('select').each(function (i) {
        NameAtr = "Form.Fields[" + trlength + "].Type";
        IdAtr = "Form_Fields_" + trlength + "__Type";
        $(this).attr('name', NameAtr).attr('id', IdAtr);
        clonedRow.find("select").val("");
    });

    clonedRow.find('button').find('button').replaceWith(btn_add);
    clonedRow.find('button.add-btn-repeat').replaceWith(btn_delete);
    $('#tblFormFields tbody').append(clonedRow);
}

function SelectBox() {
    clonedRow.find('select').each(function (i) {
        NameAtr = "Form.Fields[" + trlength + "].Type";
        IdAtr = "Form_Fields_" + trlength + "__Type";
        $(this).attr('name', NameAtr).attr('id', IdAtr);
        clonedRow.find("select").val("");
    });
}
var index = 0;
function addElement(e) {
    debugger;
    var tblRows = $("#tblFormFields tbody tr").length;
    if (tblRows === 1)
        ++index;
    else
        index = $("#tblFormFields tbody tr").length;
    var key = generateGUID();
    var html = "";
    html += "<tr>";
    html += "<td><input type='hidden' name='Form.Fields[" + index + "].Key' value='" + key + "' />" +
        "<input type='text' name='Form.Fields[" + index + "].Name' id='Form_Fields_" + index + "__Name' width='300px' placeholder='Enter label name' class='form-control' title='Please enter field label name!' /></td>";
    html += "<td><select name='Form.Fields[" + index + "].Type' id='Form_Fields_" + index + "__Type'  class='form-control' title='Please enter type name!'><option>--Select--</option></select></td>";
    html += "<td><button type='button' class='btn btn-warning delete'><i class='fa fa-trash' aria-hidden='true'></i></button></td>";
    html += "</tr>";
    $(".data-repeater").append(html);

    FieldType('Form_Fields_' + index + '__Type');
}
$(document).on("click", '.delete', function () {
    debugger;
    --index;
    $(this).closest('tr').remove();
});

function generateGUID() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function FieldType(ddl) {

    $.getJSON("/admin/generateform/FieldType", function (data) {
        debugger;
        $("#" + ddl).empty();
        $("#" + ddl).append("<option>--Select--</option>");
        $.each(data, function (key, value) {
            $("#" + ddl).append('<option value=' + value.Id + '>' + value.Name + '</option>');
        });
    });
}

$('#tbody').on('click', '.remove', function () {
    $(this).parent('td.text-center').parent('tr.rowClass').remove();
});

