
$(document).ready(function () {
    loadData();
    $("#tblUserCase").DataTable();
    $('#reload').on('click', function () {
        loadData();
    });
    var table = $("#tblUserCase").DataTable();
    let arr = [];
    $("#btnProceeding").click(function () {
        var caseHtml = "";
        var CaseArr = [];
        $('#tblUserCase tbody tr').each(function () {
            if ($(this).find("input[type=Checkbox]").is(":checked")) {
                CaseArr.push($(this).find('input[type="hidden"]').val());
            }
        });
        if (CaseArr.length > 0) {
            $.each(CaseArr, function (index, value) {
                caseHtml += "<input type='hidden' value='" + value + "'/>";
            });
            $("#create-form").appendTo(caseHtml);
        }
        else {
            alert("Please select atleast one case!", "", "warning");
            return;
        }
    });

});

loadData = function () {
    $('#viewAll').load('/Litigation/CaseManage/LoadAll');
}