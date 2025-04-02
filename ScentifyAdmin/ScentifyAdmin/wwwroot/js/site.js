// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function uploadImage(index) {
    const fileInput = document.getElementById(`file-input-${index}`);
    const placeholder = document.getElementById(`placeholder-${index}`);
    const imgInput = document.getElementById(`image-input-${index}`);
    const file = fileInput.files[0];

    if (file) {
        // Create a FileReader instance
        const reader = new FileReader();

        // When the file is read, this function will be triggered
        reader.onload = function (e) {
            // The result is the Base64 string
            const base64String = e.target.result;

            // Create an image element
            const img = document.createElement('img');
            img.src = base64String;
            img.className = 'uploaded-image';
            img.alt = 'Uploaded image';

            // Replace the placeholder content with the new image
            placeholder.innerHTML = '';
            placeholder.appendChild(img);

            // Ensure the placeholder remains clickable for future uploads
            placeholder.onclick = () => document.getElementById(`file-input-${index}`).click();
            imgInput.value = base64String;
            // Optionally log the Base64 string
            //console.log(`Base64 string for image ${index}:`, base64String);
        };

        // Read the file as a Data URL (this will trigger the onload event)
        reader.readAsDataURL(file);
    }
}