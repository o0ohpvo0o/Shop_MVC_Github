var register = {
    init: function () {
        this.LoadProvince();
        this.registerEventDistrict();
        this.registerEventPrecinct();
    },

    registerEventPrecinct: function () {
        $('#ddlDistrict').off('change').on('change', function () {
            var provinceID = $('#ddlProvince').val();
            var districtID = $('#ddlDistrict').val();
            if (provinceID != '' && districtID != '') {
                register.LoadPrecinct(provinceID, districtID);
            } else {
                $('#ddlPrecinct').html('');
            }
        });
    },

    registerEventDistrict: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                register.LoadDistrict(id);
            } else {
                $('#ddlDistrict').html('');
            }
        });
    },

    LoadProvince: function () {

        $.ajax({
            url: '/User/LoadProvince',
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="">----Select Province----</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            },
        });
    },

    LoadDistrict: function (provinceID) {

        $.ajax({
            type: "POST",
            data: { provinceID: provinceID },
            dataType: "json",
            url: '/User/LoadDistrict',
            success: function (res) {
                var html = '<option value="">----Select District----</option>';
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    })
                    $('#ddlDistrict').html(html)
                }
            }
        })
    },

    LoadPrecinct: function (province, district) {
        $.ajax({
            type: "POST",
            data: {
                provinceID: province,
                districtID: district,
            },
            url: '/User/LoadPrecinct',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var data = res.data;
                    var html = '<option value="">----Select Precinct----</option>';
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinct').html(html)
                }
            }
        });
    },
}
register.init();