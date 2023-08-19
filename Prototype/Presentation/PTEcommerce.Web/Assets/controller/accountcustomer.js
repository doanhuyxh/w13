var is_busy = false;
var offset = 0;
var stopped = false;
var accountcustomer = {
    registerControl: function () {
        $('#btnChangePassword').click(function () {
            accountcustomer.changePassword();
        });
        $('#btnUpdateInformation').click(function () {
            accountcustomer.updateInformation();
        });
        accountcustomer.reloadData();
    },
    reloadDataResult: function () {
        if ($('#order-history-order').length > 0) {
            if (is_busy) {
                return false;
            }
            if (stopped) {
                return false;
            }
            is_busy = true;
            offset += 10;
            if ($('#order-history-order').children().length == offset) {
                $.ajax({
                    url: '/lay-don-hang-cua-toi',
                    type: 'get',
                    data: {
                        offset: offset
                    },
                    beforeSend: function () {
                        $('.loading-data').show();
                    },
                    success: function (res) {
                        $('.loading-data').hide();
                        if (res != '' && res != '\r\n') {
                            $("#order-history-order").append(res);
                            if ($('#order-history-order').children().length == offset + 10) {
                                $('#btnLoadMore').show();
                            }
                            else {
                                $('#btnLoadMore').hide();
                            }
                        }
                        else {
                            $('#btnLoadMore').hide();
                        }
                    }
                }).always(function () {
                    is_busy = false;
                });
            }
            return false;
        }
    },
    updateInformation: function () {
        $.ajax({
            url: '/cap-nhat-thong-tin',
            type: 'post',
            data: {
                BankId: $('#sltBank').val(),
                BankAccount: $('#inputBankAccount').val(),
                BankNumber: $('#inputBankNumber').val(),
                PhoneNumber: $('#inputPhoneNumber').val(),
                FullName: $('#inputFullName').val(),
                Gender: $('#sltGender').val()
            },
            beforeSend: function () {
                $('#btnUpdateInformation').prop('disabled', true);
            },
            success: function (res) {
                $('#btnUpdateInformation').prop('disabled', false);
                if (res.status) {
                    base.success(res.msg);
                }
                else {
                    base.error(res.msg);
                }
            }
        });
    },
    reloadData: function () {
        if ($('#order-history-order').length > 0) {
            $.ajax({
                url: '/lay-don-hang-cua-toi',
                type: 'get',
                data: {
                    offset: 0
                },
                beforeSend: function () {
                    $('#order-history-order').empty();
                    $('.loading-data').show();
                },
                success: function (res) {
                    if (res != '' && res != '\r\n') {
                        $('#order-history-order').html(res);
                        if ($('#order-history-order').children().length == offset + 10) {
                            $('#btnLoadMore').show();
                        }
                        else {
                            $('#btnLoadMore').hide();
                        }
                    }
                    else {
                        $('#order-history-order').html('<div class="text-center"><p class="pt-3 text-danger"><b>Không có dữ liệu </b></p></div>');
                    }
                }
            });
        }
    },
    changePassword: function () {
        $.ajax({
            url: '/doi-mat-khau',
            type: 'POST',
            data: {
                confirmPassword: $('#confirmpassword').val(),
                passwordNew: $('#password').val()
            },
            beforeSend: function () {
                $('#btnChangePassword').prop('disabled', true);
            },
            success: function (res) {
                $('#btnChangePassword').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    setTimeout(function () {
                        window.location.href = '/dang-nhap';
                    }, 1000);
                }
                else {
                    base.error(res.message);
                }
            }
        });
    }
};
$(document).ready(function () {
    accountcustomer.registerControl();
});