var report = {
    init: function () {
        report.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Report/changeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response)
                    if (response.status == true) {
                        btn.text('Hiện BĐS');
                    } else {
                        btn.text('Ẩn bất động sản');
                    }
                }
            });
        });
    }
}
report.init();