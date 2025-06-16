$(document).ready(function () {

    var table = $('#tblTHearingCaseDetail').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        pageLength: 10,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
        // No 'dom' to keep UI clean
        language: {
            emptyTable: "No cases found.",
            lengthMenu: "Show _MENU_ entries",
            search: "Search:"
        }
    });

    // 1️⃣ Create buttons manually
    var bringTodatBtn = $('<button class="btn btn-sm btn-success mr-2"><i class="fa fa-calendar"></i> Bring Today</button>');
    var exportBtn = $('<button class="btn btn-sm btn-outline-success mr-2">Export to Excel</button>');
    var updateBtn = $('<button class="btn btn-sm btn-outline-primary">Update Common Proceeding</button>');
    

    // Common function to handle selected case IDs
    function getSelectedCaseIds() {
        var selectedCaseIds = [];
        $("#tblTHearingCaseDetail tbody input[type='checkbox']:checked").each(function () {
            var caseId = $(this).closest("tr").find("input[type='hidden']").val();
            if (caseId) {
                selectedCaseIds.push(caseId);
            }
        });
        return selectedCaseIds;
    }

    // Export Button handler
    bringTodatBtn.on('click', function (event) {
        event.preventDefault();  // Prevent the default button action
        jQueryModalGet('/Litigation/hearing/GetCaseHearing', 'Bring Today'); // Trigger the Excel export
    });

    // Export Button handler
    exportBtn.on('click', function (event) {
        event.preventDefault();  // Prevent the default button action
        table.button('.buttons-excel').trigger();  // Trigger the Excel export
    });

    // Update Button handler
    updateBtn.on('click', function (event) {
        event.preventDefault();  // Prevent the default button action

        var selectedCaseIds = getSelectedCaseIds();

        if (selectedCaseIds.length === 0) {
            Swal.fire("Error!", "Please select at least one case.", "error");
            return;
        }

        var caseIdsString = selectedCaseIds.join(',');
        jQueryModalGet('/Litigation/hearing/BulkProceeding?CaseIds=' + caseIdsString, 'Case Proceedings');
    });

    // 3️⃣ Append to div
    $('#customTableActions').append(bringTodatBtn, exportBtn, updateBtn);

    // 4️⃣ Re-initialize with buttons programmatically
    new $.fn.dataTable.Buttons(table, {
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Today Case Hearing',
                filename: 'Today_Case_Hearing_' + new Date().toISOString().split('T')[0],
                className: 'd-none', // hidden but still triggerable
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6]
                }
            }
        ]
    });

    table.buttons(0, null).container().appendTo(document.body); // Hidden, but functional
});




//$(document).ready(function () {
//    initializeDataTable();
//});
//function initializeDataTable() {
//    $('#tblTHearingCaseDetail').DataTable({
//        paging: true,
//        searching: true,
//        ordering: true,
//        pageLength: 10,
//        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "All"]],
//        language: {
//            emptyTable: "There are no pending works!",
//            search: "Search all fields:"
//        },
//        dom: 'Bfrtip',
//        buttons: [
//            {
//                extend: 'excelHtml5',
//                text: 'Export to Excel',
//                title: 'Today Case Hearing',
//                filename: function () {
//                    var date = new Date().toISOString().split('T')[0];
//                    return 'Today_Case_Hearing_' + date;
//                },
//                exportOptions: {
//                    columns: [0, 1, 2, 3, 4, 5, 6]
//                }
//            },
//            {
//                text: 'Update Common Proceeding',
//                action: function () {
//                    var selectedCaseIds = [];

//                    $("#tblTHearingCaseDetail tbody input[type='checkbox']:checked").each(function () {
//                        var caseId = $(this).closest("tr").find("input[type='hidden']").val();
//                        if (caseId) {
//                            selectedCaseIds.push(caseId);
//                        }
//                    });

//                    if (selectedCaseIds.length === 0) {
//                        Swal.fire("Error!", "Please select at least one case.", "error");
//                        return;
//                    }

//                    var caseIdsString = selectedCaseIds.join(',');
//                    jQueryModalGet('/Litigation/hearing/BulkProceeding?CaseIds=' + caseIdsString, 'Case Proceedings');
//                }
//            }
//        ]
//    });
//}