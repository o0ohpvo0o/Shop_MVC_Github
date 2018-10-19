var contact = {
    init: function () {
        this.registerEvents();
    },

    registerEvents: function () {
        $('#btnSubmit').off('click').on('click', function () {
            var name = $('#txtName').val();
            var phone = $('#txtPhone').val();
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();

            $.ajax({
                type: "POST",
                url: "/contact/send",
                data: {
                    name: name,
                    email: email,
                    phone: phone,
                    content: content,
                },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        alert('Sent');
                        contact.resetForm();
                    }
                },
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtPhone').val('');
        $('#txtEmail').val('');
        $('#txtContent').val('');
    },
}
contact.init();