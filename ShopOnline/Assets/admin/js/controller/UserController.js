var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var currentUser = $(this);
            var id = parseInt(currentUser.data('id'));
            $.ajax({
                type: "POST",
                data: { id: id },
                url: "/Admin/User/ChangeStatus",
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log();
                    if (response.status) {
                        currentUser.text("Active");
                    }
                    else {
                        currentUser.text("Blocked");
                    }
                },
            });
        });
    }
}

user.init();