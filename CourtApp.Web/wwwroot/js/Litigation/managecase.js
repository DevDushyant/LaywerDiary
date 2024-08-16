$(document).ready(function () {

    $("#StateId").select2({
        placeholder: "Select a state",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstStateId_0").select2({
        placeholder: "Select a state",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtTypeId").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCourtTypeId_0").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtId").select2({
        placeholder: "Select a court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtId").select2({
        placeholder: "Select a court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });



    $("#StrengthId").select2({
        placeholder: "Select strength",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstStrengthId_0").select2({
        placeholder: "Select strength",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseCategoryId").select2({
        placeholder: "Select case category",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCourtComplexId_0").select2({
        placeholder: "Select complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCaseCategoryId_0").select2({
        placeholder: "Select case category",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtDistrictId").select2({
        placeholder: "Select a district",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCourtDistrictId_0").select2({
        placeholder: "Select a district",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstCourtId_0").select2({
        placeholder: "Select a court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#ComplexBenchId").select2({
        placeholder: "Select a complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstComplexBenchId_0").select2({
        placeholder: "Select a complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseTypeId").select2({
        placeholder: "Select case type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCaseTypeId_0").select2({
        placeholder: "Select case type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtBenchId").select2({
        placeholder: "Select court/bench",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCourtBenchId_0").select2({
        placeholder: "Select court/bench",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#BenchId").select2({
        placeholder: "Select bench",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstBenchId_0").select2({
        placeholder: "Select bench",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseYear").select2({
        placeholder: "Select year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstYear_0").select2({
        placeholder: "Select year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#SelectedFirstTitle").select2({
        placeholder: "Select a title first",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#SelectedSecondTitle").select2({
        placeholder: "Select a title second",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#LinkedClient").select2({
        placeholder: "Select a client",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#FirstTitleCode").select2({
        placeholder: "Select a First Title",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#SecoundTitleCode").select2({
        placeholder: "Select a Second Title",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseStageCode").select2({
        placeholder: "Select a case stage",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseStageCode").select2({
        placeholder: "Select a case stage",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#LinkedCaseId").select2({
        placeholder: "Select a case",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CisYear").select2({
        placeholder: "Select cis year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCisYear_0").select2({
        placeholder: "Select cis year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#LinkedCaseId").select2({
        placeholder: "Select link case",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#Cadre_0").select2({
        placeholder: "Select cadre",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtTypeId").on("change", function () {
        $("#CaseCategoryId").empty();
        if ($("#CourtTypeId").val() === "b512e36f-0b6e-4a14-80d0-65266b2b320d" || $("#CourtTypeId").val() === "359acb34-7b8c-4fc8-a276-8b198ea5105c") {
            $('#divDistrictCourt').addClass('div_hide');
            $('#divHighCourt').removeClass('div_hide');
            $('.highCourt').css('display', 'block');
            $('.Court').css('display', 'none');
            BindBench();
        }
        else {
            $('.highCourt').css('display', 'none');
            $('.Court').css('display', 'block');
            $('#divHighCourt').addClass('div_hide');
            $('#divDistrictCourt').removeClass('div_hide');
            BindDistrictCourt();
        }
        BindCaseCategory($("#CourtTypeId").val())
    });

    $("#AgainstCourtTypeId_0").on("change", function () {       
        $("#AgainstCaseCategoryId_0").empty();
        if ($("#AgainstCourtTypeId_0").val() === "b512e36f-0b6e-4a14-80d0-65266b2b320d" || $("#AgainstCourtTypeId_0").val() === "359acb34-7b8c-4fc8-a276-8b198ea5105c") {
            $('.AhighCourt').css('display', 'block');
            $('.ACourt').css('display', 'none');
            BindAgainstBench();
        }
        else {
            $('.AhighCourt').css('display', 'none');
            $('.ACourt').css('display', 'block');
            BindAgainstDistrictCourt();
        }
        BindAgainstCaseCategory($("#AgainstCourtTypeId_0").val())
    });

    //$("#StateId").on('change', function () {
    //    $("#CourtBenchId").empty();
    //    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
    //        $.each(data.Data, function (i, item) {
    //            $("#CourtBenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
    //        });
    //    });
    //});

    //$("#AgainstStateId_0").on('change', function () {
    //    $("#AgainstCourtBenchId_0").empty();
    //    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
    //        $.each(data.Data, function (i, item) {
    //            $("#AgainstCourtBenchId_0").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
    //        });
    //    });
    //});

    $("#CaseCategoryId").on('change', function () {
        $("#CaseTypeId").empty();
        $.getJSON("/Litigation/CaseManage/LoadTypeOfCase?natureId=" + $("#CaseCategoryId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#CaseTypeId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#AgainstCaseCategoryId_0").on('change', function () {
        $("#AgainstCaseTypeId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadTypeOfCase?natureId=" + $("#AgainstCaseCategoryId_0").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstCaseTypeId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#CourtDistrictId").on("change", function () {
        $("#ComplexBenchId").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtComplex?CDistrictId=" + $("#CourtDistrictId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#ComplexBenchId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });

    });

    $("#AgainstCourtDistrictId_0").on("change", function () {
        $("#AgainstCourtComplexId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtComplex?CDistrictId=" + $("#AgainstCourtDistrictId_0").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstCourtComplexId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
        
    });

    $("#ComplexBenchId").on("change", function () {
        BindCourt();
    });

    $("#AgainstCourtComplexId_0").on("change", function () {
        BindAgainstCourt();
    });

});

function addElement(e) {

}

BindBench = function () {
    $("#BenchId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
        $.each(data.Data, function (i, item) {
            $("#BenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}
BindAgainstBench = function () {
    $("#AgainstBenchId_0").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#AgainstCourtTypeId_0").val() + "&StateId=" + $("#AgainstStateId_0").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgainstBenchId_0").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}


BindCourt = function () {
    $("#CourtId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=" + $("#ComplexBenchId").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#CourtId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}
BindAgainstCourt = function () {
    $("#AgainstCourtId_0").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#AgainstCourtTypeId_0").val() + "&StateId=" + $("#AgainstStateId_0").val() + "&ComplexId=" + $("#AgainstCourtComplexId_0").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgainstCourtId_0").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}

BindDistrictCourt = function () {
    $("#CourtDistrictId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtDistrictByState?StateId=" + $("#StateId").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#CourtDistrictId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}
BindAgainstDistrictCourt = function () {
    $("#AgainstCourtDistrictId_0").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtDistrictByState?StateId=" + $("#AgainstStateId_0").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgainstCourtDistrictId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}

BindCaseCategory = function (CategoryId) {
    $("#CaseCategoryId").empty()
    $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + CategoryId, function (data) {
        $.each(data, function (i, item) {
            $("#CaseCategoryId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}
BindAgainstCaseCategory = function (CategoryId) {
    $("#AgainstCaseCategoryId_0").empty()
    $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + CategoryId, function (data) {
        $.each(data, function (i, item) {
            $("#AgainstCaseCategoryId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}