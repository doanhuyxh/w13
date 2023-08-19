var process = {
    init: function () {
        setInterval(function () {
            process.countDownTime();
        }, 1000);
        process.getCurrentSession();
        process.getResultSession();
        process.historyPlay();
        $('#checkMax, #checkMin, #checkDouble, #checkSingle').change(function () {
            var listChoose = [];
            if ($('#checkMax').is(':checked')) {
                listChoose.push('1');
            }
            if ($('#checkMin').is(':checked')) {
                listChoose.push('2');
            }
            if ($('#checkDouble').is(':checked')) {
                listChoose.push('3');
            }
            if ($('#checkSingle').is(':checked')) {
                listChoose.push('4');
            }
            var pricePerChoose = parseInt($('#txtPricePerChoose').val());
            $('#txtPriceAllOrder').val(accounting.formatMoney(pricePerChoose * listChoose.length, "đ", 0, ".", ",", "%v%s"));
        });
        $('#btnSubmitPlayGame').click(function () {
            process.playGame();
        });
        $('#txtPricePerChoose').change(function () {
            var listChoose = [];
            if ($('#checkMax').is(':checked')) {
                listChoose.push('1');
            }
            if ($('#checkMin').is(':checked')) {
                listChoose.push('2');
            }
            if ($('#checkDouble').is(':checked')) {
                listChoose.push('3');
            }
            if ($('#checkSingle').is(':checked')) {
                listChoose.push('4');
            }
            if ($('#txtPricePerChoose').val() == '') {
                base.error('Vui lòng nhập số tiền cược');
                return false;
            }
            else {
                var pricePerChoose = parseInt($('#txtPricePerChoose').val());
                $('#txtPriceAllOrder').val(accounting.formatMoney(pricePerChoose * listChoose.length, "đ", 0, ".", ",", "%v%s"));
            }
        });
    },
    countDownTime: function () {
        let timer = parseInt($('#valueTimeCountDown').val());
        if (timer < 0) {
            process.getResultSession();
            process.getCurrentSession();
            return;
        }
        let minutes = parseInt(timer / 60, 10);
        let seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        $('#timeCountDown').html(minutes + ":" + seconds);
        $('#valueTimeCountDown').val(timer - 1);
    },
    getCurrentSession: function () {
        $.ajax({
            url: '/phien-moi',
            type: 'get',
            success: function (res) {
                if (res.status) {
                    $('#valueTimeCountDown').val(res.countdown);
                    $('#sessionName').html(res.session);
                    $('#sessionCurrent').val(res.session);
                    process.historyPlay();
                }
            }
        });
    },
    getResultSession: function () {
        $.ajax({
            url: '/phien-cu',
            type: 'get',
            success: function (res) {
                if (res.status) {
                    $('#resultSession1').html(res.value1);
                    $('#resultSession2').html(res.value2);
                }
            }
        });
    },
    historyPlay: function () {
        $.ajax({
            url: '/lich-su-du-doan',
            type: 'get',
            data: {
                page: 1
            },
            success: function (res) {
                $('#listHistoryPlayGames').html(res);
            }
        });
    },
    playGame: function () {
        var listChoose = [];
        if ($('#checkMax').is(':checked')) {
            listChoose.push('1');
        }
        if ($('#checkMin').is(':checked')) {
            listChoose.push('2');
        }
        if ($('#checkDouble').is(':checked')) {
            listChoose.push('3');
        }
        if ($('#checkSingle').is(':checked')) {
            listChoose.push('4');
        }
        if ($('#txtPricePerChoose').val() == '') {
            base.error('Vui lòng nhập số tiền cược');
            return false;
        }
        if (listChoose.length > 0) {
            $.ajax({
                url: '/du-doan',
                type: 'post',
                data: {
                    value: listChoose.toString(),
                    sessionId: $('#sessionCurrent').val(),
                    price: $('#txtPricePerChoose').val()
                },
                beforeSend: function () {
                    $('#btnSubmitPlayGame').prop('disabled', true);
                },
                success: function (res) {
                    $('#btnSubmitPlayGame').prop('disabled', false);
                    if (res.status) {
                        process.historyPlay();
                        base.success(res.msg);
                        $('#playGamesModal').modal('hide');
                    }
                    else {
                        base.error(res.msg);
                    }
                }
            });
        }
        else {
            base.error('Vui lòng chọn dự đoán');
            return false;
        }
    }
};
$(document).ready(function () {
    process.init();
});