$(document).ready(function () {
    $('.form-image').click(function () { $('#customFile').trigger('click'); });
    $(function () {
        $('.selectpicker').selectpicker();
    });
    setTimeout(function () {
        $('body').addClass('loaded');
    }, 200);

    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                    console.log(res);
                },
                error: function (err) {
                    console.log(err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {                  
                    if (res.isValid) {
                        $('#viewAll').html(res.html)
                        $('#form-modal').modal('hide');
                    }                   
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryModalDelete = form => {
        if (confirm('Are you sure to delete this record ?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            $('#viewAll').html(res.html)
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }

    JqueryDataTable = function (tableId, ptitle, ecolumns) {
        $('#' + tableId).DataTable({            
            dom: 'Bfrtip',// This includes the Buttons, filtering input, processing indicator, table, info, and pagination controls            
            buttons: [
                {
                    extend: 'excelHtml5',
                    text: 'Export',
                    title: ptitle,
                    filename: function () {
                        var date = new Date();
                        return ptitle +"_"+ date.toISOString().split('T')[0];  // Exports with the current date
                    },
                    exportOptions: {
                        // Optional: This ensures that all columns (including hidden ones) are included in the export
                        //columns: ':visible'
                        columns: ecolumns
                    }
                }
            ],
            // Ensure pagination and info UI are enabled correctly
            paging: true,  // Enable pagination
            lengthChange: true,  // Enable length change dropdown (number of entries to display)
            pageLength: 10,  // Default number of rows to display per page
            processing: false,  // Show processing indicator when table is being drawn
            info: true,  // Display information about the table (e.g., "Showing 1 to 10 of 50 entries")
            searching: true,  // Enable search functionality
        });
    }
});

