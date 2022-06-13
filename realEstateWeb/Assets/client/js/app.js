// File javascript để lấy dữ liệu

// Khai báo URL service của bạn ở đây
var baseService = "/Home";

var provinceUrl = baseService + "/GetAllProvinces";
var districtUrl = baseService + "/GetAllDistrictByProvinceId";
var wardUrl = baseService + "/GetAllWardByDistrictId";
var villageUrl = baseService + "/GetAllVillagedByWardId";

$(document).ready(function () {
    // load danh sách country
    $("#abc").on('change', function () {
    _getProvince();
    });
    
    //$("#ddlCountry").on('change', function () {
    //    var id = $(this).val();
    //    if (id != undefined && id != '') {
    //        _getProvince(id);
    //    }
    //});

    $("#ddlProvince").on('change', function () {
       
        var provinceText = $("#ddlProvince option:selected").text();
        $("#divtp").val(provinceText);
        $("#divResult").val(provinceText);
        //var provinceText = $("#ddlProvince option:selected").text();
        //var html = provinceText
        //$("#divResult").val(html);
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getDistrict(id);

        }
        
    });
    $("#ddlDistrict").on('change', function () {
        
        var provinceText = $("#ddlProvince option:selected").text();
        var districtText = $("#ddlDistrict option:selected").text();
        var html = districtText + ", " + provinceText;
        $("#divqh").val(districtText);
        $("#divResult").val(html);
        var id = $(this).val();
        if (id != undefined && id != '') {
            _getWard(id);
        }
    });
    $("#ddlWard").on('change', function () {
        //var id = $(this).val();
        //if (id != undefined && id != '') {
        //    _getVillage(id);
        //}
        var provinceText = $("#ddlProvince option:selected").text();
        var districtText = $("#ddlDistrict option:selected").text();
        var wardText = $("#ddlWard option:selected").text();
        var html = wardText + ", " + districtText + ", " + provinceText;
        $("#divResult").val(html);
        $("#divpx").val(wardText);
    });
    //$("#ddlVillage").on('change', function () {
    //    /* var countryText = $("#ddlCountry option:selected").text();*/
    //    var provinceText = $("#ddlProvince option:selected").text();
    //    var districtText = $("#ddlDistrict option:selected").text();
    //    var wardText = $("#ddlWard option:selected").text();
    //    var html = " Tỉnh thành: " + provinceText + " " + "Quận huyện: " + districtText + " " + "Xã phường: " + wardText ;
    //    $("#divResult").val(html);
    //});
});

function _getProvince() {
    $.get(provinceUrl, function (data) {
        if (data != null && data != undefined && data.length) {
            var html = '';
            html += '<option value="">Tất cả</option>';
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
            html += '<option value="">Tất cả</option>';
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
            html += '<option value="">Tất cả</option>';
            $.each(data, function (key, item) {
                html += '<option value=' + item.wardid + '>' + item.name + '</option>';
            });
            $("#ddlWard").html(html);
        }
    });
}
//function _getVillage(id) {
//    $.get(villageUrl + "/" + id, function (data) {
//        if (data != null && data != undefined && data.length) {
//            var html = '';
//            html += '<option value="">chọn đường phố</option>';
//            $.each(data, function (key, item) {
//                html += '<option value=' + item.villageid + '>' + item.name + '</option>';
//            });
//            $("#ddlVillage").html(html);
//        }
//    });
//}