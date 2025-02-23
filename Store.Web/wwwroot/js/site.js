// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateCart(bookId) {
    $.ajax({
        url: '/Order/AddItem',
        type: 'POST',
        data: { id: bookId },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
                $('button.btn-add-to-cart[onclick="updateCart(' + bookId + ')"]').blur();
                $('button[onclick="updateCart(' + bookId + ')"]').blur();
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при додаванні: ' + error);
        }
    });
}

function removeItem(bookId) {
    $.ajax({
        url: '/Order/RemoveItem',
        type: 'POST',
        data: { id: bookId },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
                $('button[onclick="removeItems(' + bookId + ')"]').blur();
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при видаленні: ' + error);
        }
    });
}

function removeItems(bookId) {
    $.ajax({
        url: '/Order/RemoveItems',
        type: 'POST',
        data: { id: bookId },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
                $('button[onclick="removeItems(' + bookId + ')"]').blur();
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при видаленні: ' + error);
        }
    });
}

// Оновлення статусу кошика в шаблоні
function updateCartStatus(response) {
    var cartStatus = $('.cart-status');
    if (response.totalCount > 0) {
        cartStatus.html(
            '<a href="/Order/Index" class="text-white text-decoration-none">' +
            '<strong>У кошику ' + response.totalCount + ' товарів на суму ' + response.totalPrice + '</strong>' +
            '</a>'
        );
    } else {
        cartStatus.html('<strong>Кошик порожній</strong>');
    }
}

// Оновлення рядка на сторінці кошика
function updateCartRow(bookId, response) {
    if ($('.item-count-' + bookId).length) { // Якщо ми на сторінці кошика
        if (response.itemCount > 0) {
            $('.item-count-' + bookId).text(response.itemCount);
        } else {
            $('tr[data-book-id="' + bookId + '"]').remove();
        }
        $('.total-count').text(response.totalCount);
        $('.total-price').text(response.totalPrice);
    }
}