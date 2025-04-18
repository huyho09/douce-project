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

function ClickLogOut() {
    $.ajax({
        type: "POST", // or "GET" depending on your requirements
        url: "/LogOut/Invoke",
        beforeSend: function () {
            appendLoading();
        },
        success: function (result) {
            // Handle the success response
            document.location.href = result;
        },
        complete: function () {
            hideLoading();
        },
        error: function (error) {
            // Handle the error response
            console.error("Error:", error);
        }
    });
}

function updateInvoiceStatus(status) {
    let selectedRows = [];
    $('.row-check:checked').each(function () {
        let row = $(this).closest('tr').attr('data-id');
        selectedRows.push(row);
    });

    if (selectedRows.length > 0) {
        $.ajax({
            url: "/Invoices/UpdateInvoiceStatus",
            type: "POST",
            data: { invoiceIds: selectedRows, status: status },
            success: function (response) {
                alert('Successful!');
                location.reload();
            },
            error: function () {
                console.error("Error updateInvoiceStatus!");
                alert('Failed');
            }
        });
    } else {
        alert('Please select at least one item!');
    }
}

//function uploadImage(index) {
//    const fileInput = document.getElementById(`file-input-${index}`);
//    const placeholder = document.getElementById(`placeholder-${index}`);
//    const imgInput = document.getElementById(`image-input-${index}`);
//    const file = fileInput.files[0];

//    if (file) {
//        // Create a FormData object to send the file
//        const formData = new FormData();
//        formData.append('file', file);

//        // Add product_id to the FormData (assuming product_id is available)
//        const productId = $('#productId').val();
//        formData.append('productId', productId);

//        // Make an AJAX call to the UploadImage endpoint
//        fetch('/api/image/upload', {
//            method: 'POST',
//            body: formData
//        })
//            .then(response => {
//                if (!response.ok) {
//                    throw new Error('Failed to upload image');
//                }
//                return response.json(); // Expecting the server to return JSON with the image URL
//            })
//            .then(data => {
//                // Create an image element
//                const img = document.createElement('img');
//                img.src = data.imageUrl; // Use the URL returned from the server
//                img.className = 'uploaded-image';
//                img.alt = 'Uploaded image';

//                // Replace the placeholder content with the new image
//                placeholder.innerHTML = '';
//                placeholder.appendChild(img);

//                // Ensure the placeholder remains clickable for future uploads
//                placeholder.onclick = () => document.getElementById(`file-input-${index}`).click();

//                // Store the image URL in the hidden input (if needed)
//                if (imgInput) {
//                    imgInput.value = data.imageUrl;
//                }
//            })
//            .catch(error => {
//                console.error('Error uploading image:', error);
//                alert('Failed to upload image. Please try again.');
//            });
//    }
//}