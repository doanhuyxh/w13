var dashboard = {
    registerControl: function () {
        dashboard.getPlayGameBySession();
        $('#btnChangeResultSession').click(function () {
            dashboard.updateResultSession();
        });
        dashboard.startChatHub();
        setInterval(function () {
            dashboard.countDownTime();
        }, 1000);
    },

    getPlayGameBySession: function () {
        var searchUrl = "/AdministratorManager/Dashboard/GetPlayGameBySessionId";
        $('#tblHistoryPlaySession').bootstrapTable('destroy');
        $('#tblHistoryPlaySession').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    sessionId: parseInt($('#sessionIdText').val())
                };
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            limit: 100000,
            pageSize: 100000,
            pageList: [20, 50, 100, 200, 500],
            search: false,
            //showColumns: true,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: 'Id',
                    title: 'Mã đặt cược',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'Username',
                    title: 'Tài khoản',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'Value',
                    title: 'Đặt cược',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'Amount',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s");
                    }
                }, {
                    field: 'TotalValue',
                    title: 'Tổng đặt',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s");
                    }
                },
                {
                    field: 'CreatedDate',
                    title: 'Ngày đặt',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return base.bigdateFormatJsonDMY(value);
                    }
                }
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblHistoryPlaySession').bootstrapTable('refresh', { silent: true });
                }
            }
        });
    },
    updateResultSession: function () {
        $.ajax({
            url: '/AdministratorManager/Dashboard/ChangeResultSession',
            type: 'POST',
            data: {
                result1: parseInt($('#sltResult1').val()),
                result2: parseInt($('#sltResult2').val()),
                sessionId: parseInt($('#sessionIdText').val())
            },
            success: function (res) {
                if (res.status) {
                    base.toastrAlert('Tác vụ thành công', 'success');
                }
                else {
                    base.toastrAlert(res.message, 'error');
                }
            }
        });
    },
    countDownTime: function () {
        let timer = parseInt($('#valueTimeCountDown').val());
        if (timer < 0) {
            return;
        }
        let minutes = parseInt(timer / 60, 10);
        let seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        $('#timeCountDown').html(minutes + ":" + seconds);
        $('#valueTimeCountDown').val(timer - 1);
    },
    startChatHub: function () {
        var chat = $.connection.chatHub;
        // Create a function that the hub can call to broadcast chat messages.  
        chat.client.broadcastMessage = function (session, value1, value2) {
            if (parseInt(session) != parseInt($('#sessionIdText').val()))
            {
                base.nosuccess('Phiên #' + session + ' đã tạo mới. Chỉnh sửa kết quả ngay!');
                $('.sessionId').html(session);
                $('#sessionIdText').val(session);
                $('#sltResult1').val(value1);
                $('#sltResult2').val(value2);
                $('#valueTimeCountDown').val(180);
                dashboard.getPlayGameBySession();
            }
        };
        chat.client.updatePlaygame = function () {
            $('#tblHistoryPlaySession').bootstrapTable('refresh', { silent: true });
        };
        // Start the connection.  
        $.connection.hub.start().done();
    },
};
$(document).ready(function () {
    dashboard.registerControl();
});