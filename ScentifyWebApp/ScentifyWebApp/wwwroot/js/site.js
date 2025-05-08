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
    reloadCart(function () {
        updatetotalQuantity();
    });

    $(".add-to-cart-btn").on("click", function () {
        let productId = $(this).data("id");
        let quantity = 1; // Default to 1 if not provided
        let currentSize = $('.product-info input:checked').data('volume');
        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: { productId: productId, quantity: quantity, volume: currentSize },
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

function removeCartItem(productId, volume) {
    $.ajax({
        url: "/Cart/RemoveFromCart",
        type: "POST",
        data: { productId: productId, volume: volume },
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

function removeCartItemInCheckOut(_this, productId, volume) {
    var $this = $(_this);
    $.ajax({
        url: "/Cart/RemoveFromCart",
        type: "POST",
        data: { productId: productId, volume: volume },
        success: function (response) {
            reloadCart(function () {
                //toggleCart();
                updatetotalQuantity();
                showNotiModal(response.message);
                if (response.status === 200) {
                    $this.closest(".product").remove();

                    // update total text
                    var sumPrice = sumTotalInCheckOut();
                    $('#sub-total-price').text(sumPrice);
                    $('#total-price').text(sumPrice);

                    let totalQuantity = $('.product .quantity').toArray().reduce((sum, el) => sum + (parseFloat($(el).val()) || 0), 0);
                    $('#total-quantity').text(totalQuantity);
                }

                // check empty
                if ($('.order-summary .product').length == 0) {
                    showNotiModal("Giỏ hàng đã trống!");
                    setTimeout(function () {
                        location.href = '/home';
                    }, 1000);
                }
            });
        },
        error: function () {
            console.error("Error removing product from cart.");
        }
    });
}

function sumTotalInCheckOut() {
    var total = 0;
    var $products = $('.product');
    if ($products != null && $products.length > 0) {
        $products.each((index, ele) => {
            var quantity = $(ele).find('.quantity').val();
            var price = $(ele).find('.product-price').text().trim().replace(',', '');
            total += (quantity * price);
        });
    }
    return total;
}

function updateCartItem(_this, productId) {
    clearTimeout(changeTimeout);
    changeTimeout = setTimeout(() => {
        var $this = $(_this);
        var quantity = $this.val();
        var volume = $('.product-info input:checked').data('volume');
        $.ajax({
            url: "/Cart/UpdateQuantity",
            type: "POST",
            data: { productId: productId, quantity: quantity, volume: volume },
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

function updateCartIteminCheckOut(_this, productId, volume) {
    clearTimeout(changeTimeout);
    changeTimeout = setTimeout(() => {
        var $this = $(_this);
        var quantity = $this.val();

        $.ajax({
            url: "/Cart/UpdateQuantity",
            type: "POST",
            data: { productId: productId, quantity: quantity, volume: volume },
            success: function (response) {
                reloadCart(function () {
                    //toggleCart();
                    updatetotalQuantity();
                    //showNotiModal(response.message);
                    if (response.status === 200) {
                        $this.closest(".product").find('.quantity').val(quantity);

                        // update total text
                        var sumPrice = sumTotalInCheckOut();
                        $('#sub-total-price').text(sumPrice);
                        $('#total-price').text(sumPrice);

                        let totalQuantity = $('.product .quantity').toArray().reduce((sum, el) => sum + (parseFloat($(el).val()) || 0), 0);
                        $('#total-quantity').text(totalQuantity);
                    }
                });
            },
            error: function () {
                console.error("Error adding product to cart.");
            }
        }, 200);
    });
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

function saveCart() {
    let customerInfo = $("#checkout-form").serializeArray(); // Convert form data to array
    let jsonData = {};

    // Convert form array to JSON object
    $.each(customerInfo, function () {
        if (jsonData[this.name]) {
            if (!Array.isArray(jsonData[this.name])) {
                jsonData[this.name] = [jsonData[this.name]];
            }
            jsonData[this.name].push(this.value);
        } else {
            jsonData[this.name] = this.value;
        }
    });
    $.ajax({
        url: "/save-invoice",
        type: "POST",
        data: jsonData,
        success: function (response) {
            reloadCart(function () {
                showNotiModal(response.message)
                if (response.status === 200) {
                    setTimeout(function () {
                        location.reload();
                    }, 10000);
                }
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
function isMobile() {
    return /Mobi|Android|iPhone|iPad|iPod/i.test(navigator.userAgent);
}
function updatetotalQuantity() {
    let total = 0;
    var cartQuan = isMobile() ? $(".cart-quantity-mobile input") : $(".cart-quantity:not(.mobile)");
    if (cartQuan.length > 0) {
        cartQuan.each(function (index, ele) {
            total += parseInt($(ele).val()) || 0; // Ensure numeric value, default to 0 if empty
        });
        $('#number-of-cart').text(total);
    } else {
        $('#number-of-cart').text('');
    }
}

document.getElementById("btn-search").addEventListener("click", function () {
    const searchInput = document.getElementById("search-input");
    searchInput.classList.toggle("active");
    if (searchInput.classList.contains("active")) {
        $(this).html("<svg class=\"custom-icon\">\r\n                            <use xlink:href=\"#close\"></use>\r\n                        </svg>");
        $('.search-group-box input').focus();
    } else {
        $(this).html("<svg class=\"custom-icon\">\r\n                            <use xlink:href=\"#custom-search\"></use>\r\n                        </svg>");
    }
});

document.getElementById("btn-search-popup").addEventListener("click", function () {
    if ($("#search-modal").is("visible")) {
        $("#search-modal").modal("hide");
    } else {
        $("#search-modal").modal("show");
        $('#search-modal .search-form input').focus();
    }
});
$(() => {
    $('#search-modal').on('shown.bs.modal', function () {
        requestAnimationFrame(() => {
            $('#search-modal .search-form input')[0].focus();
        });
    });
});

function changeSelectTitle(_this) {
    $("#selectedTitle").val($(_this).val());
}

$(document).ready(function () {
    var errors = $('input').closest('.floating-group:has(.field-validation-error:not(:empty))');
    if (errors?.length > 0) {
        errors.first().find('input').focus();
    }
});

$(document).on("ready", function () {
    if ($("#successModal").length > 0) {
        setTimeout(function () {
            $("#successModal").on('hide.bs.modal', function () {
                let modal = $(this).find(".modal-dialog");
                modal.addClass("hide-animation");
            });

            $("#successModal").on('hidden.bs.modal', function () {
                let modal = $(this).find(".modal-dialog");
                modal.removeClass("hide-animation"); // Reset animation
            });

            $("#successModal").on("click", function (e) {
                // If the click is NOT inside the modal-dialog, hide the modal
                if (!$(e.target).closest(".modal-dialog").length) {
                    $(this).removeClass("show");
                    $(this).attr("style", "");
                }
            });

            setTimeout(function () {
                $("#successModal").removeClass("show");
                $("#successModal").attr("style", "");
            }, 7000);
        }, 1000);
    }
});