
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

$(document).ready(function () {
    $('#tblUserCase').DataTable({
        "paging": true,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]], // Dropdown for selecting page size
        "pageLength": 10, // Default page size
        "ordering": true,
        "searching": true,
        "processing": true,
        "serverSide": true, // Enable server-side processing
        "ajax": {
            "url": "/Litigation/CaseManage/LoadAll", // Endpoint for data retrieval
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "sno" },
            { "data": "court" },
            { "data": "caseType" },
            { "data": "no" },
            { "data": "year" },
            { "data": "caseDetail" },
            { "data": "caseStage" },
            { "data": "nextDate" },
            { "data": "actions", "orderable": false } // Disable ordering for action buttons
        ]
    });
});
