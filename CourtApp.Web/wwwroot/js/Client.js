$(document).ready(function () {

    $("#AppearenceID").select2({
        placeholder: "Select a appearance",
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        }
    });

    // Automatically open the Select2 dropdown when the user tabs to the input
    $("#AppearenceID").on("focus", function () {
        console.log("Focus event triggered");
        setTimeout(() => {
            $(this).select2('open');
            console.log("Dropdown opened");
        }, 10);
    });

    let selectedIndex = -1; // Track the index of the currently highlighted suggestion

    $("#ReferalBy").on("input", function () {
        var query = $(this).val().toLowerCase();

        // If the input is empty, hide the suggestions box
        if (query.length < 1) {
            $("#suggestions").hide();
            return;
        }

        // Fetch the data from the server
        $.ajax({
            url: "/LawyerDiary/Lawyer/GetLawyer", // Replace with your API URL
            type: "GET",
            dataType: "json",
            data: { refral: query },
            success: function (data) {
                // Clear previous suggestions
                $("#suggestions").empty();

                // If data is returned, display suggestions
                if (data && data.length > 0) {
                    $.each(data, function (index, item) {
                        // Add each suggestion to the dropdown
                        $("#suggestions").append('<div class="suggestion-item" data-index="' + index + '">' + item + '</div>');
                    });
                    $("#suggestions").show(); // Show the suggestions box
                    selectedIndex = -1; // Reset selection index
                } else {
                    $("#suggestions").hide(); // Hide if no suggestions
                }
            }
        });
    });

    // Event listener for selecting a suggestion with a mouse click
    $("#suggestions").on("click", ".suggestion-item", function () {
        var selectedValue = $(this).text();
        $("#ReferalBy").val(selectedValue); // Replace input value with selected value
        $("#suggestions").hide(); // Hide suggestions after selection
    });
   
    

    // Function to highlight the current suggestion
    function updateHighlight() {
        var suggestions = $("#suggestions .suggestion-item");
        suggestions.removeClass("highlighted"); // Remove previous highlight
        if (selectedIndex >= 0 && selectedIndex < suggestions.length) {
            $(suggestions[selectedIndex]).addClass("highlighted"); // Highlight the new suggestion
        }
    }

    // Styling the highlighted item (optional)
    $("#suggestions").on("mouseenter", ".suggestion-item", function () {
        $(this).addClass("highlighted");
    }).on("mouseleave", ".suggestion-item", function () {
        $(this).removeClass("highlighted");
    });

    // Hide suggestions when clicking outside
    $(document).on("click", function (e) {
        if (!$(e.target).closest("#ReferalBy, #suggestions").length) {
            $("#suggestions").hide();
        }
    });
});


//$(document).ready(function () {     
//    $("#ReferalBy").autocomplete({
//        source: function (request, response) {
//            $.ajax({               
//                url: "/LawyerDiary/Lawyer/GetLawyer",
//                type: 'GET',
//                dataType: 'json',
//                data: { refral: request.term },
//                success: function (data) {                    
//                    response(data);
//                },                
//                error: function (xhr, status, error) {
//                    console.log("AJAX Error:", error);
//                }
//            });
//        },
//        minLength: 2,
//        onSelect: function (event, ui) {
//            debugger;
//            console.log("Selected Item:", ui.item);
//            $("#ReferalBy").val(ui.item.value); // Just set the name
//            return false; // Prevent default behavior
//        }
//    });

//});