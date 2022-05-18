﻿var realestate = {
    init: function () {
        realestate.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/RealEstate/changeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response)
                    if (response.status == true) {
                        btn.text('kích hoạt');
                    } else {
                        btn.text('khóa');
                    }                   
                }
            });
        });
        
    }
}
realestate.init();