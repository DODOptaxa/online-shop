// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateCart(bookId) {
    $.ajax({
        url: '/Order/AddItem',
        type: 'POST',
        data: {
            id: bookId,
        },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при додаванні: ' + error);
        }
    });
}

function updateCartWiggle(bookId) {
    $.ajax({
        url: '/Order/AddItem',
        type: 'POST',
        data: { id: bookId },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
                $('.cart-status')
                    .addClass('wiggle')
                    .one('animationend', function () {
                        $(this).removeClass('wiggle');
                    });
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при додаванні: ' + error);
        }
    });
}

function fetchCellPhoneErrors(cellPhone) {
    $.ajax({
        url: '/Order/SendConfirmationCode',
        type: 'POST',
        data: {
            cellPhone: cellPhone
        },
        success: function (response) {

            $('.phone-success').text('');

            const currentPhone = $('.phone-input-field').val();
            if (response.cell_phone && response.cell_phone !== currentPhone) {
                $('.phone-input-field').val(response.cell_phone);
            }

            if (response.success === false) {
                const currentPath = window.location.pathname;
                if (currentPath !== '/Order/Confirmation') {
                    window.location.href = '/Order/Confirmation?cellPhone=' + response.cell_phone;
                }
                $('.phone-success').text('Відправлено успішно');
            }

            fetchErrors('phone-error', response.errors);
        },
        error: function (xhr, status, error) {
            console.log('Помилка');
        }
    });
}

function fetchConfirmateCellPhoneErrors(cellPhone, code) {
    $.ajax({
        url: '/Order/Confirmate',
        type: 'POST',
        data: {
            cellPhone,
            code
        },
        success: function (response) {
            if (response.success === false) {
                window.location.href = '/Order/StartDelivery';
            }
            fetchErrors('confirmate-phone-error', response.errors);
        },

        error: function (xhr, status, error) {
            console.log('Помилка');
        }
    });
}

function fetchErrors(element, values) {
    var errorElement = $('.'+element) || $('#'+element);

    if (errorElement.length === 0) {
        console.log('Елемент із назвою "error-text" не знайдено');
        return;
    }
    errorElement.text('');
    //if (values && typeof values === 'object' && !Array.isArray(values)) {
    var errorValues = Object.values(values);
    errorElement.text(errorValues.join('\n'));
}

function removeItem(bookId) {
    $.ajax({
        url: '/Order/RemoveItem',
        type: 'POST',
        data: {
            id: bookId,
        },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                updateCartRow(bookId, response);
                if (response.itemCount < 1) {
                    $('tr[data-book-id="' + bookId + '"]').remove();
                }
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
        data: {
            id: bookId
        },
        success: function (response) {
            if (response.success) {
                updateCartStatus(response);
                $('tr[data-book-id="' + bookId + '"]').remove();
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при видаленні: ' + error);
        }
    });
}

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

function updateCartRow(bookId, response) {
    if ($('.item-count-' + bookId).length) {
        $('.item-count-' + bookId).text(response.itemCount);
    }
        $('.total-count').text(response.totalCount);
        $('.total-price').text(response.totalPrice);
}