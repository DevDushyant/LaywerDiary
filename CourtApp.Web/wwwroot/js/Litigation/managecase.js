$(document).ready(function () {
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

    $("#LinkedWith").select2({
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
        debugger;
        $("#CaseCategoryId").empty();
        if ($("#CourtTypeId").val() === "b512e36f-0b6e-4a14-80d0-65266b2b320d" || $("#CourtTypeId").val() === "359acb34-7b8c-4fc8-a276-8b198ea5105c") {
            $('#divDistrictCourt').addClass('div_hide');
            $('#divHighCourt').removeClass('div_hide');
        }
        else {
            $('#divHighCourt').addClass('div_hide');
            $('#divDistrictCourt').removeClass('div_hide');
        }
        $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + $("#CourtTypeId").val(), function (data) {
            $.each(data, function (i, item) {
                $("#CaseCategoryId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#AgainstCourtTypeId_0").on("change", function () {
        $("#AgainstCaseCategoryId_0").empty();
        if ($("#AgainstCourtTypeId_0").val() === "359acb34-7b8c-4fc8-a276-8b198ea5105c") {
            $('#AgainstDivDistrictCourt_0').addClass('div_hide');
            $('#AgainstDivHighCourt_0').removeClass('div_hide');
        }
        else {
            $('#AgainstDivHighCourt_0').addClass('div_hide');
            $('#AgainstDivDistrictCourt_0').removeClass('div_hide');
        }
        $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + $("#CourtTypeId").val(), function (data) {
            $.each(data, function (i, item) {
                $("#AgainstCaseCategoryId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#StateId").on('change', function () {
        $("#CourtBenchId").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
            $.each(data.Data, function (i, item) {
                $("#CourtBenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
            });
        });
    });

    $("#AgainstStateId_0").on('change', function () {
        $("#AgainstCourtBenchId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstCourtBenchId_0").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
            });
        });
    });

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
        $("#AgainstComplexBenchId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtComplex?CDistrictId=" + $("#CourtDistrictId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstComplexBenchId_0").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#ComplexBenchId").on("change", function () {
        $("#CourtBenchId").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=0&ComplexId=" + $("#ComplexBenchId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#CourtBenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
            });
        });
    });

    $("#AgainstComplexBenchId_0").on("change", function () {
        $("#AgainstCourtBenchId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=0&ComplexId=" + $("#ComplexBenchId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstCourtBenchId_0").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
            });
        });
    });

});

function addElement(e) {

}