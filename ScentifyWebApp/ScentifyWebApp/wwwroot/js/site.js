document.addEventListener("DOMContentLoaded", () => {
    // Get elements
    const openPopupBtn = document.querySelector(".open-popup-btn");
    const closePopupBtn = document.getElementById("closePopup");
    const popupOverlay = document.getElementById("popupOverlay");
    const promoPopup = document.getElementById("promoPopup");

    // Open popup
    openPopupBtn.addEventListener("click", () => {
        promoPopup.classList.add("active");
        popupOverlay.classList.add("active");
    });

    // Close popup
    closePopupBtn.addEventListener("click", () => {
        promoPopup.classList.remove("active");
        popupOverlay.classList.remove("active");
    });

    // Close when clicking outside the popup
    popupOverlay.addEventListener("click", () => {
        promoPopup.classList.remove("active");
        popupOverlay.classList.remove("active");
    });
})

function toggleCart() {
    document.querySelector('.cart-container').classList.toggle('active');
    document.querySelector('.modal-backdrop').classList.toggle('active');
}

// Function to add a product to the cart
function addToCart(productId, quantity) {
    $.ajax({
        url: "/Cart/AddToCart",
        type: "POST",
        data: { product: { Id: productId }, quantity: quantity },
        success: function (response) {
            console.log("Product added to cart!");
            updateCart();
        },
        error: function () {
            console.error("Error adding product to cart.");
        }
    });
}

// Function to remove a product from the cart
function removeFromCart(productId) {
    $.ajax({
        url: "/Cart/RemoveFromCart",
        type: "POST",
        data: { productId: productId },
        success: function (response) {
            console.log("Product removed from cart!");
            updateCart();
        },
        error: function () {
            console.error("Error removing product from cart.");
        }
    });
}

// Function to clear the cart
function clearCart() {
    $.ajax({
        url: "/Cart/ClearCart",
        type: "GET",
        success: function (response) {
            console.log("Cart cleared!");
            updateCart();
        },
        error: function () {
            console.error("Error clearing the cart.");
        }
    });
}

// Function to update the cart (reload cart content dynamically)
function updateCart() {
    $("#cart-container").load("/Cart/Index #cart-container > *");
}
