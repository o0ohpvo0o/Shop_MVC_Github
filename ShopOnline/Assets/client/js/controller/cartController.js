/// <reference path="../jquery-3.3.1.min.js" />

var cart = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        //Continue Shopping
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        //Payment
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/payment-details";
        });

        //Update item in Cart
        $('#btnUpdate').off('click').on('click', function () {
            var listItem = $('.txtQuantity');
            var cartList = [];
            $.each(listItem, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id'),
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/update-cart'
                    }
                }
            });
        });

        //Delete Cart Items
        $('#btnDelete').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/update-cart'
                    }
                }
            });
        });

        //Delete specific Items in Cart
        $('.btnRemoveItem').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteItems',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/update-cart'
                    }
                },
            });
        });
    }
}
cart.init();