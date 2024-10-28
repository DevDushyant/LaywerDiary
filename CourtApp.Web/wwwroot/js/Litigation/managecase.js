$(document).ready(function () {

    $("#StateId").select2({
        placeholder: "Select a state",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#ClientId").select2({
        placeholder: "Select a client",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgStateId").select2({
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

    $("#AgCourtTypeId").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgBenchId").select2({
        placeholder: "Select a Bench",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgCourtDistrictId").select2({
        placeholder: "Select a Court District",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#BenchId").select2({
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

    $("#AgCourtId").select2({
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

    $("#AgStrengthId").select2({
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

    $("#AgComplexId").select2({
        placeholder: "Select complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgCaseCategoryId").select2({
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
    

    $("#ComplexId").select2({
        placeholder: "Select a complex",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgComplexId").select2({
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

    $("#AgCaseTypeId").select2({
        placeholder: "Select case type",
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

    $("#AgCaseYear").select2({
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

    $("#CaseStageId").select2({
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

    $("#AgCisYear").select2({
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

    $("#AgCadreId").select2({
        placeholder: "Select cadre",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    
    $("#CourtTypeId").on("change", function () {
        CourtTypeChange(this);
    });
    function CourtTypeChange() {
       
        var selectedValue = $('#CourtTypeId').val();
        $("#CaseCategoryId").empty();
        if ($("#CourtTypeId option:selected").text() === "High Court") {
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
    }

    $("#AgCourtTypeId").on("change", function () {       
        $("#AgCaseCategoryId").empty();
        if ($("#AgCourtTypeId option:selected").text() === "High Court") {
            $('.AhighCourt').css('display', 'block');
            $('.ACourt').css('display', 'none');
            BindAgainstBench();
        }
        else {
            $('.AhighCourt').css('display', 'none');
            $('.ACourt').css('display', 'block');
            BindAgainstDistrictCourt();
        }
        BindAgainstCaseCategory($("#AgCourtTypeId").val())
    });    

    $("#CaseCategoryId").on('change', function () {
        $("#CaseTypeId").empty();
        $.getJSON("/Litigation/CaseManage/LoadTypeOfCase?natureId=" + $("#CaseCategoryId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#CaseTypeId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#AgCaseCategoryId").on('change', function () {
        $("#AgCaseTypeId").empty();
        $.getJSON("/Litigation/CaseManage/LoadTypeOfCase?natureId=" + $("#AgCaseCategoryId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgCaseTypeId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });

    $("#CourtDistrictId").on("change", function () {
        $("#ComplexId").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtComplex?CDistrictId=" + $("#CourtDistrictId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#ComplexId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });

    });

    $("#AgCourtDistrictId").on("change", function () {
        $("#AgComplexId").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourtComplex?CDistrictId=" + $("#AgCourtDistrictId").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgComplexId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
        
    });

    $("#ComplexId").on("change", function () {       
        BindCourt();
    });

    $("#AgComplexId").on("change", function () {
        BindAgainstCourt();
    });

});
BindBench = function () {
    
    $("#BenchId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
        $.each(data.Data, function (i, item) {
            debugger;
            $("#BenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}
BindAgainstBench = function () {
    $("#AgainstCaseDetails_0__AgainstBenchId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#AgainstCourtTypeId_0").val() + "&StateId=" + $("#AgainstStateId_0").val() + "&ComplexId=00000000-0000-0000-0000-000000000000", function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgainstCaseDetails_0__AgainstBenchId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}


BindCourt = function () {
    $("#CourtId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#CourtTypeId").val() + "&StateId=" + $("#StateId").val() + "&ComplexId=" + $("#ComplexId").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#CourtId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
        });
    });
}
BindAgainstCourt = function () {
    $("#AgCourtId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtBench?CourtTypeId=" + $("#AgCourtTypeId").val() + "&StateId=" + $("#AgStateId").val() + "&ComplexId=" + $("#AgComplexId").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgCourtId").append(`<option /><option value="${item.Id}">${item.CourtBench_En}</option>`);
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
    $("#AgCourtDistrictId").empty();
    $.getJSON("/Litigation/CaseManage/LoadCourtDistrictByState?StateId=" + $("#AgStateId").val(), function (data) {
        $.each(data.Data, function (i, item) {
            $("#AgCourtDistrictId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
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
    $("#AgCaseCategoryId").empty()
    $.getJSON("/Litigation/CaseManage/LoadCaseCategory?CourtTypeId=" + CategoryId, function (data) {
        $.each(data, function (i, item) {
            $("#AgCaseCategoryId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
        });
    });
}