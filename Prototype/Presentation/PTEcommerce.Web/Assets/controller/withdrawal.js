var is_busy = false;
var offset = 0;
var stopped = false;
var withdrawal = {
    registerControl: function () {
        $('#btnLoadMore').click(function () {
            withdrawal.reloadDataResult();
        });
        withdrawal.reloadData();
        $('#btnLoadMore').hide();
        $('#btnWithdrawal').click(function () {
            withdrawal.createWithdrawal();
        });
    },
    createWithdrawal: function () {
        $.ajax({
            url: '/yeu-cau-rut-tien',
            type: 'post',
            data: {
                BankId: $('#sltBank').val(),
                BankAccount: $('#inputBankAccount').val(),
                BankNumber: $('#inputBankNumber').val(),
                Amount: $('#inputAmount').val()
            },
            beforeSend: function () {
                $('#btnWithdrawal').prop('disabled', true);
            },
            success: function (res) {
                $('#btnWithdrawal').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                }
                else {
                    base.error(res.message);
                    if (res.code == 401) {
                        setTimeout(function () {
                            window.location.href = '/dang-nhap';
                        }, 1000);
                    }

                }
            }
        });
    },
    reloadDataResult: function () {
        if (is_busy) {
            return false;
        }
        if (stopped) {
            return false;
        }
        is_busy = true;
        offset += 10;
        if ($('#order-history').children().length == offset) {
            $.ajax({
                url: '/lay-lich-su-rut-tien',
                type: 'get',
                data: {
                    offset: offset
                },
                success: function (res) {
                    if (res != '' && res != '\r\n') {
                        $("#order-history").append(res);
                        if ($('#order-history').children().length == offset + 10) {
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

    },
    reloadData: function () {
        $.ajax({
            url: '/lay-lich-su-rut-tien',
            type: 'get',
            data: {
                offset: 0
            },
            beforeSend: function () {
                $('#order-history').empty();
            },
            success: function (res) {
                if (res != '' && res != '\r\n') {
                    $('#order-history').html(res);
                    if ($('#order-history').children().length == offset + 10) {
                        $('#btnLoadMore').show();
                    }
                    else {
                        $('#btnLoadMore').hide();
                    }
                }
                else {
                    $('#order-history').html('<div class="text-center"><b class="text-white"><i class="bi bi-info"></i> Không có dữ liệu </b></div>');
                }
            }
        });
    },
};
$(document).ready(function () {
    withdrawal.registerControl();
});