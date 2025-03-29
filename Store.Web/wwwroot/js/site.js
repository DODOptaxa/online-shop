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

function fetchCellPhoneErrors(cellPhone, returnUrl) {
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
                if (window.location.pathname !== '/Order/Confirmation') {
                    window.location.href = '/Order/Confirmation?cellPhone=' + response.cell_phone + "&returnUrl=" + returnUrl;
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

function fetchConfirmateCellPhoneErrors(cellPhone, code, returnUrl) {
    $.ajax({
        url: '/Order/Confirmate',
        type: 'POST',
        data: {
            cellPhone,
            code
        },
        success: function (response) {
            if (response.success === false) {
                window.location.href = returnUrl;
            }
            fetchErrors('confirmate-phone-error', response.errors);
        },
        error: function (xhr, status, error) {
            console.log('Помилка');
        }
    });
}

function fetchErrors(element, values) {
    var errorElement = $('.' + element) || $('#' + element);

    if (errorElement.length === 0) {
        console.log('Елемент із назвою "error-text" не знайдено');
        return;
    }
    errorElement.text('');
    if (values && typeof values === 'object' && !Array.isArray(values)) {
        var errorValues = Object.values(values);
        errorElement.text(errorValues.join('\n'));
    }
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

function addComment(bookId) {
    var content = $('#comment-content').val();
    $.ajax({
        url: '/Account/AddComment', 
        type: 'POST',
        data: {
            bookId: bookId,
            content: content
        },
        success: function (response) {
            if (response.success) {
                $('#comments-list').prepend(
                    `<div class="comment card card-no-animation mb-2" data-id="${response.comment.id}">
                        <div class="card-body">
                        <small class="text-muted"> ${response.comment.userName} </small>
                            <p class="comment-content">${response.comment.content}</p>
                            <div class="mt-2">
                                <button class="btn btn-sm btn-primary edit-comment">Редагувати</button>
                                <button class="btn btn-sm btn-danger delete-comment">Видалити</button>
                                <button class="btn btn-sm btn-success" onclick="location.reload();">Зберегти</button>
                                
                            </div>
                        </div>
                    </div>`
                );
                $('#comment-content').val('');
            } else {
                fetchErrors('comment-error', { error: response.message });
            }
        },
        error: function (xhr, status, error) {
            console.log('Помилка при додаванні коментаря: ' + error);
        }
    });
}

function changeComment(commentId) { 
    var commentDiv = $(`div[data-id="${commentId}"]`);
    var currentContent = commentDiv.find('.comment-content').text();
    var newContent = prompt("Введіть новий текст:", currentContent);
    if (newContent) {
        $.ajax({
            url: '/Account/ChangeComment', 
            type: 'POST',
            data: {
                id: commentId,
                content: newContent
            },
            success: function (response) {
                if (response.success) {
                    commentDiv.find('.comment-content').text(response.comment.content);
                } else {
                    fetchErrors('comment-error', { error: response.message });
                }
            },
            error: function (xhr, status, error) {
                console.log('Помилка при редагуванні коментаря: ' + error);
            }
        });
    }
}

function deleteComment(commentId) {
    if (confirm("Ви впевнені, що хочете видалити коментар?")) {
        $.ajax({
            url: '/Account/DeleteComment', 
            type: 'POST',
            data: {
                id: commentId
            },
            success: function (response) {
                if (response.success) {
                    $(`div[data-id="${commentId}"]`).remove();
                } else {
                    fetchErrors('comment-error', { error: response.message });
                }
            },
            error: function (xhr, status, error) {
                console.log('Помилка при видаленні коментаря: ' + error);
            }
        });
    }
}

$(document).ready(function () {
    
    $('#add-comment-form').submit(function (e) {
        e.preventDefault();
        var bookId = $(this).data('book-id');
        addComment(bookId);
    });

    
    $('#comments-list').on('click', '.edit-comment', function () {
        var commentId = $(this).closest('.comment').data('id');
        changeComment(commentId); 
    });

    
    $('#comments-list').on('click', '.delete-comment', function () {
        var commentId = $(this).closest('.comment').data('id');
        deleteComment(commentId);
    });
});
