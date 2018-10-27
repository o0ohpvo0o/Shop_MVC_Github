var common = {
    init: function () {
        this.registerEvent();
    },
    registerEvent: function () {
        //$("#txtKeyword").autocomplete({
        //    minLength: 0,
        //    source: function (request, response) {
        //        $.ajax({
        //            url: "/Product/GetNameProduct",
        //            dataType: "json",
        //            data: {
        //                term: request.term
        //            },
        //            success: function (res) {
        //                response(res.data);
        //            }
        //        });
        //    },
        //    focus: function (event, ui) {
        //        $("#txtKeyword").val(ui.item.label);
        //        return false;
        //    },
        //    select: function (event, ui) {
        //        $("#txtKeyword").val(ui.item.label);

        //        return false;
        //    }
        //})
        //    .autocomplete("instance")._renderItem = function (ul, item) {
        //        return $("<li>")
        //            .append("<div>" + item.label + "</div>")
        //            .appendTo(ul);
        //    };



        $('#txtKeyword').autoComplete({
            minChars: 0,
            source: function (request, suggest) {
                $.ajax({
                    url: "/Product/GetNameProduct",
                    dataType: "json",
                    data: {
                        term: request
                    },
                    success: function (res) {
                        suggest(res.data);
                        request = request.toLowerCase();
                        var choices = res.data;
                        var matches = [];
                        for (i = 0; i < choices.length; i++)
                            if (~choices[i].toLowerCase().indexOf(request)) {
                                matches.push(choices[i]);
                            } else {
                                matches.push("");
                            }
                                
                        suggest(matches);
                    }
                });
            }
        });




    },
}
common.init();