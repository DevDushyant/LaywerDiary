$(document).ready(function () {
    JqueryDataTable('tblCaseWohd', 'User Data List', '0,1,2,3,4,5,6,7');
});
$("#btnUpdate").on("click", function (e) {
    e.preventDefault();
    debugger;
    var caseDataList = [];
    $('#tblCaseWohd tbody tr').each(function () {
        var caseId = $(this).find('input[type="hidden"]').val();
        var nextDate = $(this).find('input[type="date"]').val();
        if (nextDate !== "")
            caseDataList.push({
                CaseId: caseId,
                HearingDt: nextDate
            });
    });
    if (caseDataList.length > 0) {
        $.ajax({
            url: "/Litigation/casemanage/UpdateHearingDate",
            type: "POST",
            data: { casedts: caseDataList },
            success: function (response) {
                if (response.Succeeded) {
                    Swal.fire({
                        title: "Hearing date updated for the selected cases!",
                        text: "Ok!",
                        icon: "success"
                    });
                }
                else {
                    Swal.fire({
                        title: "Good job!",
                        text: "You clicked the button!",
                        icon: "error"
                    });
                }
            },
            error: function (xhr) {
                console.error('An error occurred:', xhr.responseText);
            }
        });
    }
    else {
        Swal.fire({
            title: "Kindly fill the Hearing date before submit!",
            text: "You clicked the button!",
            icon: "error"
        });
    }
});