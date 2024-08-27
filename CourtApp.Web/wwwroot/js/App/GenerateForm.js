var btn_delete = '<button type="button" onclick="removeKolom($(this))" class="btn btn-warning"><i class="fa fa-trash" aria-hidden="true"></i></button>';
var btn_add = '<button class="add-btn-repeat btn btn-success" onclick="addElement($(this))" type="button"><i class="fa fa-plus" aria-hidden="true"></i></button>';
function removeKolom(e) {
    e.parents('.kolom').remove();
}
function addElement(e) {
    var trlength = $('#tblFormFields tbody tr').length;
    var clonedRow = $('#tblFormFields tbody tr:last').clone();
    clonedRow.find('input').each(function (i) {
        if (i === 0) {
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
