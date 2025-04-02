$(document).ready(function () {
    var table = $("#tblRegister").DataTable({
        "language": {
            "emptyTable": "Record is not there!"
        }
    });

    var lengthDiv = $("#tblRegister_wrapper .dataTables_length");
    var filterDiv = $("#tblRegister_wrapper .dataTables_filter");

    // Wrap Pagination Dropdown
    lengthDiv.wrap('<div class="d-flex align-items-center"></div>');

    // Define filter dropdowns (id, label)
    var filters = [
       /* { id: "ddlStatus", label: "Status" },*/
        { id: "ddlReferral", label: "Referral" },
        { id: "ddlClient", label: "Client" }
    ];

    // Dynamically calculate column width based on the number of elements
    var colWidth = `col-sm-${Math.floor(12 / (filters.length + 2))}`; // +2 for Pagination & Search

    // Function to create dropdown dynamically with dynamic width
    function createDropdown(id, label) {
        return $(
            `<div class="${colWidth} d-flex align-items-center">
                        <label class="fw-bold me-2">${label}:</label>
                        <select class="form-control customFilter" id="${id}">
                            <option value="">Select</option>
                        </select>
                    </div>`
        );
    }

    // Create Row Container
    var filterContainer = $('<div class="row w-100 mt-2"></div>');

    // Append Pagination
    filterContainer.append(lengthDiv.parent().addClass(colWidth));

    // Append Filters
    filters.forEach(filter => {
        filterContainer.append(createDropdown(filter.id, filter.label));
    });

    // Append Search Box
    filterContainer.append(filterDiv.parent().addClass(colWidth));

    // Insert into DataTable wrapper
    $("#tblRegister_wrapper").prepend(filterContainer);

    // Fetch Dropdown Data from API
    var urls = [];
    urls.push({ "Key": "ReferBy", "Value": "/Client/ManageClient/DdlReferBy" }
        , { "Key": "Client", "Value": "/Client/ManageClient/DdlClients" }
    );
    urls.forEach(function (item) {
        $.ajax({
            url: item.Value,
            method: "GET",
            success: function (data) {
                var ddl = "";
                if (item.Key === "ReferBy") ddl = "#ddlReferral";
                if (item.Key === "Client") ddl = "#ddlClient";
                populateDropdown(ddl, data);                
            },
            error: function (error) {
                console.log("Error loading filter data:", error);
            }
        });
    });




    // Function to populate dropdown
    function populateDropdown(selector, options) {
        options.forEach(function (item) {
            $(selector).append(new Option(item.Name,item.Id));
        });
    }

    // Apply Select2 for better UI
    $(".customFilter").select2({
        width: '100%',
        placeholder: "Select",
        allowClear: true
    });
    $(".customFilter").on("change", function () {       
        let client = $("#ddlClient").val();
        let referral = $("#ddlReferral").val();
       // let status = $("#ddlStatus").val();
        $.ajax({
            url: "/Report/Register/Search",
            type: "GET",
            data: { ClientId: client, ReferalBy: referral, Status: "" },
            success: function (response) {
                table.clear().draw(); // Clear existing data and re-draw without destroying the table
                $("#tblRegister tbody").html(response); // Update only tbody with new rows
                table.rows.add($("#tblRegister tbody tr")).draw();                
            },
            error: function () {
                alert("Error fetching filtered data.");
            }
        });
    });

    // Filtering logic
    //$(".customFilter").on("change", function () {
    //    let status = $("#ddlStatus").val();
    //    let referral = $("#ddlReferral").val();
    //    let client = $("#ddlClient").val();
    //    let table = $("#tblRegister").DataTable();
    //    $("#tblRegister tbody").load("/Report/Register/Search?ClientId=" + client + "&ReferalBy=" + referral + "&Status=" + status + " tbody", function () {
    //        table.destroy(); // Destroy DataTable before updating
    //        table = $("#tblRegister").DataTable(); // Reinitialize DataTable after content is loaded
    //    });
        //$("#viewAll").load("/Report/Register/Search?ClientId=" + client + "&ReferalBy=" + referral + "&Status=" + status, function () {
        //    table.destroy(); // Destroy existing DataTable instance before updating
        //    table = $("#tblRegister").DataTable(); // Reinitialize DataTable after content is loaded
        //});
       /* $('#viewAll').load("/Report/Register/Search?ClientId=" + client + "&ReferalBy=" + referral + "&Status=" + status);*/
        //$.ajax({
        //    url: "/Report/Register/Search",
        //    type: "GET",
        //    data: {
        //        Status: status,
        //        ReferalBy: referral,
        //        ClientId: client
        //    },
        //    success: function (response) {
        //        table.clear().rows.add(response.data).draw(); // Clear & reload table
        //    }
        //});
        ////table.column(2).search(status).column(3).search(referral).column(4).search(client).draw();
    //});
});