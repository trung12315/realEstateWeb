// File javascript để lấy dữ liệu

// Khai báo URL service của bạn ở đây
var baseService = "/RealEstate";

var provinceUrl = baseService + "/GetAllProvinces";
var districtUrl = baseService + "/GetAllDistrictByProvinceId";
var wardUrl = baseService + "/GetAllWardByDistrictId";
var villageUrl = baseService + "/GetAllVillagedByWardId";
$(document).ready(function () {
    // load danh sách country
    _getProvince();
    //$("#ddlCountry").on('change', function () {
    //    var id = $(this).val();
    //    if (id != undefined && id != '') {
    //        _getProvince(id);
    //    }
    //});

    $("#ddlProvince").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getDistrict(id);
        }
    });
    $("#ddlDistrict").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getWard(id);
        }
    });
    $("#ddlWard").on('change', function () {
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getVillage(id);
        }
    });
    $("#ddlVillage").on('change', function () {
       /* var countryText = $("#ddlCountry option:selected").text();*/
        var provinceText = $("#ddlProvince option:selected").text();
        var districtText = $("#ddlDistrict option:selected").text();
        var wardText = $("#ddlWard option:selected").text();
        var villageText = $("#ddlVillage option:selected").text();
        var html = villageText+", " + wardText+", " + districtText +", "+ provinceText;
        $("#divResult").val(html);
    });
});

function _getProvince() {
    $.get(provinceUrl , function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">--Chọn Tỉnh/TP--</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.provinceid + '>' + item.name + '</option>';
            });
            $("#ddlProvince").html(html);
        }
    });
}
// truyền id của province vào
function _getDistrict(id) {
    $.get(districtUrl + "/" + id, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">chọn quận huyện</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.districtid + '>' + item.name + '</option>';
            });
            $("#ddlDistrict").html(html);
        }
    });
}
// truyền id của district vào
function _getWard(id) {
    $.get(wardUrl + "/" + id, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">chọn phường xã</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.wardid + '>' + item.name + '</option>';
            });
            $("#ddlWard").html(html);
        }
    });
}
function _getVillage(id) {
    $.get(villageUrl + "/" + id, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">chọn đường phố</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.villageid + '>' + item.name + '</option>';
            });
            $("#ddlVillage").html(html);
        }
    });
}
