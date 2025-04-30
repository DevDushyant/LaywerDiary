$(document).ready(function () {
    $("#CaseIds").select2({
        placeholder: "Select a case",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });


    $("#FormTypeId").select2({
        placeholder: "Select form type",
        theme: "bootstrap4",
        allowClear: true,
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#FormTypeId").on("change", function () {
        $("#lblHeader").text($("#FormTypeId :selected").text())
    });

    $("#CaseIds").on("change", function () {
        $("#TitleIds").empty();
        $.getJSON("/Litigation/CaseInfoPrinting/GetCompTitlesByCases?caseIds=" + $("#CaseIds").val(), function (data) {
            console.log(data);
            $.each(data, function (i, item) {
                console.log(item.Name);
                $("#TitleIds").append(`<option /><option value="${item.Id}">${item.Name}</option>`);
            });
        });
    });

    $("#btnPrint").click(function () {
        printData();
    });

    function printData1() {
        var divToPrint = document.getElementById("printableArea");
        newWin = window.open("");
        newWin.document.write(divToPrint.outerHTML);
        newWin.print();
        newWin.close();
    }

    function printData() {
        var divToPrint = document.getElementById("printableArea");
        var newWin = window.open("", "_blank");

        newWin.document.write('<html><head><title>Print Form</title>');

        newWin.document.write(`
        <style>
            @media print {
                @page {
                    size: A4 portrait;
                    margin: 20mm 25mm 20mm 20mm;
                }

                body {
                    font-size: 13px; /* slightly smaller */
                    font-family: Arial, sans-serif;
                    line-height: 1.3;
                    margin: 0;
                    padding: 0;
                    text-align: justify;
                }

                .a4-size {
                    width: 100%;
                    max-width: 170mm;
                    margin: 0 auto;
                    padding-left: 10mm;
                    box-sizing: border-box;
                    page-break-inside: avoid;
                }

                table {
                    width: 100%;
                    table-layout: fixed;
                    border-collapse: collapse;
                }

                td {
                    word-wrap: break-word;
                    vertical-align: top;
                    padding: 4px;
                }

                * {
                    -webkit-print-color-adjust: exact;
                    print-color-adjust: exact;
                }
            }

            .a4-size {
                width: 100%;
                max-width: 170mm;
                margin: 0 auto;
                padding-left: 10mm;
                box-sizing: border-box;
            }

            table {
                width: 100%;
                table-layout: fixed;
            }

            td {
                word-wrap: break-word;
                vertical-align: top;
                padding: 4px;
            }
        </style>
    `);

        newWin.document.write('</head><body>');
        newWin.document.write(divToPrint.outerHTML);
        newWin.document.write('</body></html>');

        newWin.document.close();
        newWin.focus();
        newWin.print();
        newWin.close();
    }

    function printDatatoday() {
        var divToPrint = document.getElementById("printableArea");
        var newWin = window.open("", "_blank");

        newWin.document.write('<html><head><title>Print Form</title>');

        newWin.document.write(`
        <style>
            @media print {
                @page {
                    size: A4 portrait;
                    margin: 20mm 15mm 20mm 15mm;
                }

                body {
                    font-size: 14px;
                    font-family: Arial, sans-serif;
                    text-align: justify;
                    margin: 0;
                    padding: 0;
                }

                .a4-size {
                    width: 100%;
                    max-width: 180mm; /* Prevent content cutoff on right */
                    margin: 0 auto;
                    padding: 10mm;
                    box-sizing: border-box;
                    page-break-after: always;
                }

                table {
                    width: 100%;
                    table-layout: fixed;
                    border-collapse: collapse;
                }

                td {
                    word-wrap: break-word;
                    vertical-align: top;
                    padding: 4px;
                }

                * {
                    -webkit-print-color-adjust: exact;
                    print-color-adjust: exact;
                }
            }

            .a4-size {
                width: 100%;
                max-width: 180mm;
                margin: 0 auto;
                padding: 10mm;
                box-sizing: border-box;
            }

            table {
                width: 100%;
                table-layout: fixed;
            }

            td {
                word-wrap: break-word;
                vertical-align: top;
                padding: 4px;
            }
        </style>
    `);

        newWin.document.write('</head><body>');
        newWin.document.write(divToPrint.outerHTML);
        newWin.document.write('</body></html>');

        newWin.document.close();
        newWin.focus();
        newWin.print();
        newWin.close();
    }

    function printDataold() {
        var divToPrint = document.getElementById("printableArea");
        var newWin = window.open("", "_blank");

        newWin.document.write('<html><head><title>Print Form</title>');

        // Embedded CSS inside print function
        newWin.document.write(`
            <style>
                @media print {
                    @page {
                        size: A4 portrait; /* Ensures A4 print size */
                        margin: 20mm 10mm 20mm 10mm; /* Top, Right, Bottom, Left margins */
                    }

                    body {
                        font-size: 14px;
                        font-family: Arial, sans-serif;
                        text-align: justify; /* Ensures justified alignment */
                        margin: 0;
                        padding: 0;
                    }

                    /* Ensure the printable area is centered and does not cut content */
                    #printableArea {
                        width: 100%;
                        max-width: 210mm;
                        padding-left: 200px; /* Extra left margin as requested */
                        padding-right: 10px;
                        word-wrap: break-word;
                    }

                    /* Ensure no extra margin/padding for proper alignment */
                    .card-body {
                        margin: 0;
                        padding: 0;
                    }                    
                }
            </style>
        `);

        newWin.document.write('</head><body>');
        newWin.document.write(divToPrint.outerHTML);
        newWin.document.write('</body></html>');

        newWin.document.close();
        newWin.focus();
        newWin.print();
        newWin.close();
    }
    function printData2() {
        var divToPrint = document.getElementById("printableArea");
        var newWin = window.open("", "_blank");

        newWin.document.write(
            '<!DOCTYPE html>' +
            '<html lang="en">' +
            '<head>' +
            '<meta charset="UTF-8">' +
            '<meta name="viewport" content="width=device-width, initial-scale=1.0">' +
            '<title>Print</title>' +
            '<style>' +
            '@page { margin: 0; }' + // Removes browser header & footer
            'body { margin: 20px; padding: 0; font-family: Arial, sans-serif; }' +
            '.print-container { width: 750px; margin-left: 200px;margin-right: 50px; padding: 20px; text-align: justify; border: 1px solid #ddd; box-shadow: 2px 2px 10px rgba(0,0,0,0.1); page-break-after: always; }' +
            '.content { width: 100%; line-height: 1.6; font-size: 16px; }' +
            '.applicant-number { float: right; border: 1px solid black; height: 30px; width: 30px; text-align: center; font-weight: bold; }' +
            '.signature { text-align: right; font-weight: bold; margin-top: 40px; }' +
            '</style>' +
            '</head>' +
            '<body>' +
            divToPrint.outerHTML +
            '<script>' +
            'window.onload = function() {' +
            '  window.print();' +
            '  window.onafterprint = function() { window.close(); };' +
            '};' +
            '<\/script>' +
            '</body>' +
            '</html>'
        );

        newWin.document.close();
    }

    $("#TitleIds").select2({
        placeholder: "Select title",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    $("#btnSearch").on("click", function () {
        var t = $("#FormTypeId").val();
        var v = $("#CaseIds").val();
        var title = $("#TitleIds").val();
        if (t && v) {
            loadData(t, v, title);
        }
        else {
            Swal.fire({
                title: "Please select the form type or case for generating the report!",
                text: "error",
                icon: "error"
            });
        }
    });
});
function loadData(t, v, title) {
    $('#viewAll').load('/Litigation/CaseInfoPrinting/LoadFormPrinting?type=' + t + "&Cases=" + v + "&AppNo=" + title);
}