function emptyCart() {
    $.ajax({
        url: '/Home/EmptyCart',
        type: 'POST',
        success: function (response) {
            if (response.success) {
                $('.dropdown-menu .dropdown-item').remove();
                $('.total-price').text('Total: 0 SEK');
                $('.total-quantity').text('0');
                updateCartTotals(true);
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('An error occurred while emptying the cart.');
        }
    });
}

function adjustQuantity(itemId, newQuantity) {
    $.ajax({
        url: '/Home/AdjustQuantity',
        type: 'POST',
        data: {
            shoppingItemId: itemId,
            amount: newQuantity
        },
        success: function (response) {
            if (response.success) {
                $('.item-quantity[data-item-id="' + itemId + '"]').val(response.quantity);
                $('.total-quantity').text(response.totalQuantity);
                $('.total-price').text('Total: ' + response.totalPrice.toFixed(2) + ' SEK');
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('An error occurred while updating the quantity.');
        }
    });
}

$(function () {
    $(document).on('click', '.decrease-quantity', function (e) {
        e.preventDefault();
        var button = $(this);
        var itemId = button.data('item-id');
        var quantityInput = $('.item-quantity[data-item-id="' + itemId + '"]');
        var currentQuantity = parseInt(quantityInput.val());
        var newQuantity = currentQuantity - 1;

        adjustQuantity(itemId, newQuantity);
    });

    $(document).on('click', '.increase-quantity', function (e) {
        e.preventDefault();
        var button = $(this);
        var itemId = button.data('item-id');
        var quantityInput = $('.item-quantity[data-item-id="' + itemId + '"]');
        var currentQuantity = parseInt(quantityInput.val());
        var newQuantity = currentQuantity + 1;

        adjustQuantity(itemId, newQuantity);
    });

    $(document).on('change', '.item-quantity', function () {
        var input = $(this);
        var itemId = input.data('item-id');
        var newQuantity = parseInt(input.val());

        adjustQuantity(itemId, newQuantity);
    });

    $(document).on('click', '.empty-cart-button', function (e) {
        e.preventDefault();
        emptyCart();
    });
});
