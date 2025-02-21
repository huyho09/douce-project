document.addEventListener("DOMContentLoaded", () => {
    // Get elements
    const openPopupBtn = document.querySelector(".expand-btn");
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

//document.addEventListener("DOMContentLoaded", function () {
//    const menuButton = document.querySelector(".custom-hamburger");
//    const mobileMenu = document.querySelector(".mobile-menu");

//    menuButton.addEventListener("click", function () {
//        mobileMenu.classList.toggle("active");
//    });
//});

//document.addEventListener("DOMContentLoaded", function () {
//    const menuButton = document.querySelector(".custom-hamburger");
//    const mobileMenu = document.querySelector(".mobile-menu");
//    const overlay = document.createElement("div");
//    overlay.classList.add("mobile-overlay");
//    document.body.appendChild(overlay);

//    // Toggle menu on hamburger click
//    menuButton.addEventListener("click", function () {
//        mobileMenu.classList.toggle("active");
//        overlay.classList.toggle("active");
//        document.body.classList.toggle("menu-open");
//    });

//    // Close menu when clicking outside (overlay)
//    overlay.addEventListener("click", function () {
//        mobileMenu.classList.remove("active");
//        overlay.classList.remove("active");
//        document.body.classList.remove("menu-open");
//    });

//    // Handle submenu hover effect
//    document.querySelectorAll(".has-submenu").forEach(item => {
//        item.addEventListener("mouseenter", function () {
//            this.querySelector(".sub-menu").style.display = "flex";
//        });
//        item.addEventListener("mouseleave", function () {
//            this.querySelector(".sub-menu").style.display = "none";
//        });
//    });
//});


document.addEventListener("DOMContentLoaded", function () {
    const dropdowns = document.querySelectorAll(".custom-dropdown");

    dropdowns.forEach((dropdown) => {
        const toggleButton = dropdown.querySelector(".dropdown-toggle");
        const menu = dropdown.querySelector(".dropdown-menu");
        const selectedOption = dropdown.querySelector(".selected-option");

        // Toggle dropdown on button click
        toggleButton.addEventListener("click", function (event) {
            event.stopPropagation(); // Prevents immediate close when clicking button

            // Close other dropdowns before opening current one
            dropdowns.forEach((d) => {
                if (d !== dropdown) d.classList.remove("active");
            });

            dropdown.classList.toggle("active");
        });

        // Select option and close dropdown
        menu.addEventListener("click", function (event) {
            if (event.target.tagName === "LI") {
                selectedOption.textContent = event.target.textContent;
                dropdown.classList.remove("active");
            }
        });
    });

    // Close dropdowns when clicking outside
    document.addEventListener("click", function (event) {
        dropdowns.forEach((dropdown) => {
            if (!dropdown.contains(event.target)) {
                dropdown.classList.remove("active");
            }
        });
    });
});
