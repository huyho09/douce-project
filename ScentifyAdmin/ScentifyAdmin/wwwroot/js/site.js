$(document).ready(function () {
    $("#perfume-form input, #perfume-form textarea, #perfume-form select").on("input change", function () {
        updatePreview();
    });

    function updatePreview() {
        var formData = $("#perfume-form").serialize(); // Serialize form data

        $.ajax({
            url: "/products/review", // Controller Action URL
            type: "GET",
            data: formData,
            success: function (response) {
                // Set the iframe content using srcdoc
            $("#preview-container").attr("srcdoc", response);
            },
            error: function () {
                alert("Failed to load preview. Please try again.");
            }
        });
    }
    if ($("#perfume-form").length > 0) {

        // Call preview update on page load
        updatePreview();
    }
});
function toggleFullscreen() {
    var iframe = document.getElementById("preview-container");

    if (iframe.requestFullscreen) {
        iframe.requestFullscreen();
    } else if (iframe.mozRequestFullScreen) { // Firefox
        iframe.mozRequestFullScreen();
    } else if (iframe.webkitRequestFullscreen) { // Chrome, Safari
        iframe.webkitRequestFullscreen();
    } else if (iframe.msRequestFullscreen) { // IE/Edge
        iframe.msRequestFullscreen();
    }
}