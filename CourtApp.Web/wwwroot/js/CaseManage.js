$(document).ready(function () {
    $("#CaseNatureId").select2({
        placeholder: "Select a nature",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#CaseNatureId").on("change", function () {

        $("#TypeOfCaseId").empty();
        $.getJSON("/Litigation/CaseManage/LoadTypeOfCase?natureId=" + $("#CaseNatureId").val(), function (data) {

            $.each(data.Data, function (i, item) {
                debugger;
                $("#CaseTypeId").append(`<option /><option value="${item.Id}">${item.Name_En}</option>`);
            });
        });
    });
    $("#CourtTypeCode").on("change", function () {
        $("#CourtName").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourt?CourtTypeId=" + $("#CourtTypeCode").val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#CourtName").append(`<option /><option value="${item.Id}">${item.CourtName}</option>`);
            });
        });
        //$.getJSON("/Litigation/CaseManage/LoadCourt?CourtTypeId=" + $("#CourtTypeCode").val(), function (data) {
        //    $.each(data.Data, function (i, item) {
        //        $("#CourtName").append(`<option /><option value="${item.Id}">${item.CourtName}</option>`);
        //    });
        //});
    });
    $("#AgainstCourtTypeId_0").on("change", function () {
        $("#AgainstCourtId_0").empty();
        $.getJSON("/Litigation/CaseManage/LoadCourt?CourtTypeId=" + $("#AgainstCourtTypeId_0").val(), function (data) {

            $.each(data.Data, function (i, item) {
                $("#AgainstCourtId_0").append(`<option /><option value="${item.Id}">${item.CourtName}</option>`);
            });
        });
    });
    $('body').on("change", '.AgainstCourtTypeId', function () {
        var idval = $(this).attr("id");
        var splitdata = idval.split("_")[1];
        debugger;
        $("#AgainstCourtId_" + splitdata).empty();
        $.getJSON("/Litigation/CaseManage/LoadCourt?CourtTypeId=" + $(this).val(), function (data) {
            $.each(data.Data, function (i, item) {
                $("#AgainstCourtId_" + splitdata).append(`<option /><option value="${item.Id}">${item.CourtName}</option>`);
            });
        });
    });

    $("#AgainstCourtUniqueId").select2({
        placeholder: "Select a court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#CaseTypeId").select2({
        placeholder: "Select a case type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CaseKindId").select2({
        placeholder: "Select a case kind",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#year").select2({
        placeholder: "Select a year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstYear").select2({
        placeholder: "Select a year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#CourtTypeCode").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstCourtTypeId").select2({
        placeholder: "Select a court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#CourtName").select2({
        placeholder: "Select a court",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstCourtName").select2({
        placeholder: "Select a against court",
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

    $("#FirstTitleType").select2({
        placeholder: "Select a First Title",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#SecondTitleType").select2({
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

    $("#AgainstCourtTypeId_0").select2({
        placeholder: "Select court type",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#AgainstCourtId_0").select2({
        placeholder: "Select court",
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

    $("#CisYear").select2({
        placeholder: "Select cis year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#AgainstCisYear_0").select2({
        placeholder: "Select against cis year",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });
    $("#Cadder_0").select2({
        placeholder: "Select cadder",
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

});