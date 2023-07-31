$(document).ready(function () {
    console.log('test')
    // Hide the textbox on page load
    $("#textboxContainer").hide();

    // Function to show/hide the textbox based on the selected DistributionTypeId
    function toggleTextbox() {
        var selectedTypeId = $("#surveyType").val();

        // Change the condition based on the DistributionTypeId that should show the textbox
        if (selectedTypeId === "2") {
            $("#textboxContainer").show();
        } else {
            $("#textboxContainer").hide();
        }
    }

    // Call the function when the page loads and when the dropdown selection changes
    toggleTextbox();
    $("#surveyType").change(toggleTextbox);
});