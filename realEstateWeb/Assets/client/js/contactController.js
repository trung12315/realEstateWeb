﻿var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#txtName').val();
            var phone = $('#txtPhone').val();
            var email = $('#txtEmail').val();
            
            var address = $('#txtAddress').val();
            var message = $('#txtmessage').val();
            

            $.ajax({
                url: '/Contact/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    phone: phone,
                    email: email,
                    
                    address: address,
                    message: message
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Success');
                        contact.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtPhone').val('');
        $('#txtEmail').val('');
        
        $('#txtAddress').val('');
        $('#txtmessage').val('');
    }
}
contact.init();