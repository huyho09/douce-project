//document.addEventListener("DOMContentLoaded", () => {
//    // Get elements
//    const openPopupBtn = document.querySelector(".expand-btn");
//    const closePopupBtn = document.getElementById("closePopup");
//    const popupOverlay = document.getElementById("popupOverlay");
//    const promoPopup = document.getElementById("promoPopup");

//    // Open popup
//    openPopupBtn.addEventListener("click", () => {
//        promoPopup.classList.add("active");
//        popupOverlay.classList.add("active");
//    });

//    // Close popup
//    closePopupBtn.addEventListener("click", () => {
//        promoPopup.classList.remove("active");
//        popupOverlay.classList.remove("active");
//    });

//    // Close when clicking outside the popup
//    popupOverlay.addEventListener("click", () => {
//        promoPopup.classList.remove("active");
//        popupOverlay.classList.remove("active");
//    });
//})

let notiTimeout;
let changeTimeout;
function toggleCart() {
    document.querySelector('.cart-container').classList.toggle('active');
    document.querySelector('.modal-backdrop').classList.toggle('active');
}

$(document).ready(function () {
    // refresh data
    reloadCart();
    updatetotalQuantity();

    $(".add-to-cart-btn").on("click", function () {
        let productId = $(this).data("id");
        let quantity = 1; // Default to 1 if not provided

        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                reloadCart(function () {
                    updatetotalQuantity();
                    showNotiModal(response.message);
                });
            },
            error: function () {
                console.error("Error adding product to cart.");
            }
        });
    });
});

function reloadCart(callback) {
    fetch("/cart/index")
        .then(response => response.text())
        .then(html => {
            $("#cart-component").html(html);
            if (callback) {
                callback();
            }
        })
        .catch(error => console.error("Failed to reload cart:", error));
}

function removeCartItem(productId) {
    $.ajax({
        url: "/Cart/RemoveFromCart",
        type: "POST",
        data: { productId: productId },
        success: function (response) {
            reloadCart(function () {
                toggleCart();
                updatetotalQuantity();
                showNotiModal(response.message)
            });
        },
        error: function () {
            console.error("Error removing product from cart.");
        }
    });
}

function updateCartItem(_this, productId) {
    clearTimeout(changeTimeout);
    changeTimeout = setTimeout(() => {
        var $this = $(_this);
        var quantity = $this.val();
        $.ajax({
            url: "/Cart/UpdateQuantity",
            type: "POST",
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                reloadCart(function () {
                    toggleCart();
                    updatetotalQuantity();
                });
            },
            error: function () {
                console.error("Error adding product to cart.");
            }
        });
    }, 1000);
}

function clearAllCart() {
    $.ajax({
        url: "/Cart/ClearCart",
        type: "POST",
        success: function (response) {
            reloadCart(function () {
                updatetotalQuantity();
                showNotiModal(response.message);
            });
        },
        error: function () {
            console.error("Error removing product from cart.");
        }
    });
}

$(document).ready(function () {
    $("#infoModal").on('hide.bs.modal', function () {
        let modal = $(this).find(".modal-dialog");
        modal.addClass("hide-animation");
    });

    $("#infoModal").on('hidden.bs.modal', function () {
        let modal = $(this).find(".modal-dialog");
        modal.removeClass("hide-animation"); // Reset animation
    });

    // Show modal when button is clicked
    $(".add-to-cart").click(function () {
        $("#infoModal").modal("show");
    });
});

function showNotiModal(message) {
    $("#infoModal").modal("show");
    $("#infoMessage").text(message);

    clearTimeout(notiTimeout);

    notiTimeout = setTimeout(function () {
        $("#infoModal").modal("hide");
    }, 10000);
}

function updatetotalQuantity() {
    let total = 0;
    if ($(".cart-quantity").length > 0) {
        $(".cart-quantity").each(function (index, ele) {
            total += parseInt($(ele).val()) || 0; // Ensure numeric value, default to 0 if empty
        });
        $('#number-of-cart').text(total);
    }
}