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
                        btn.text('Đã duyệt');
                    } else {
                        btn.text('Chờ kiểm duyệt');
                    }
                }
            });
        });
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imagesManange').modal('show');
            $('#hidProductID').val($(this).data('id'));
            realestate.loadImages();
        });

        $('#btnChooImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imagesList').append('<div><img src="' + url + '" width="100" /><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>');

                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            };
            finder.popup();
        });

        $('#btnSaveImages').off('click').on('click', function () {
            var images = [];
            var id = $('#hidProductID').val();
            $.each($('#imagesList img'), function (i, item) {
                images.push($(item).prop('src'));
            })
            console.log(id);
            $.ajax({
                url: '/PostManage/SaveImages',
                type: 'POST',
                data: {
                    id: id,
                    images: JSON.stringify(images)
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        $('#imagesManange').modal('hide');
                        $('#imagesList').html('');
                        alert('Lưu thành công');
                    }

                    //thong bao thanh cong
                }
            });
        });
    },
    loadImages: function () {
        $.ajax({
            url: '/PostManage/LoadImages',
            type: 'GET',
            data: {
                id: $('#hidProductID').val()
            },
            dataType: 'json',
            success: function (response) {

                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    html += '<div><img src="' + item + '" width="100" /><a href="#" class="btn-delImage"><i class="fa fa-times"></i></a></div>'
                });
                 $('#imagesList').html(html);

                $('.btn-delImage').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            }
        });
    }
}
realestate.init();