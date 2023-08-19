var historyTransfer = {
    registerControl: function () {
        if ($('#tblHistoryTransfer').length > 0) {
            historyTransfer.getlistHistoryTransfer();
        }
        if ($('#tblPlayGame').length > 0) {
            historyTransfer.getlistPlayGames();
        }
        $('#btnSearch').click(function () {
            if ($('#tblHistoryTransfer').length > 0) {
                historyTransfer.getlistHistoryTransfer();
            }
            if ($('#tblPlayGame').length > 0) {
                historyTransfer.getlistPlayGames();
            }
        });
        $('#sltAccount').select2();
    },
    getlistHistoryTransfer: function () {
        $('#tblHistoryTransfer').bootstrapTable('destroy');
        $('#tblHistoryTransfer').bootstrapTable({
            method: 'get',
            url: '/AdministratorManager/HistoryTransfer/ListAllPaging',
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    idAccount: $('#sltAccount').val(),
                    type: $('#sltType').val()
                };
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            limit: 10,
            pageSize: 10,
            pageList: [10, 20, 30, 50],
            search: false,
            //showColumns: true,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: 'Id',
                    title: 'Mã GD',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return '#' + value;
                    }
                },
                {
                    field: 'Type',
                    title: 'Ngân hàng/ví',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if (value == 1) {
                            return '<span class="badge badge-pill badge-soft-primary font-size-12">Dự đoán</span>';
                        }
                        else if (value == 2) {
                            return '<span class="badge badge-pill badge-soft-primary font-size-12">Dự đoán</span>';
                        }
                        else if (value == 3) {
                            return '<span class="badge badge-pill badge-soft-danger font-size-12">Rút tiền</span>';
                        }
                        else if (value == 4) {
                            return '<span class="badge badge-pill badge-soft-danger font-size-12">Hoàn tiền</span>';
                        }
                        else if (value == 5) {
                            return '<span class="badge badge-pill badge-soft-danger font-size-12">Cộng/trừ tiền</span>';
                        }
                    }
                },
                {
                    field: 'AmountBefore',
                    title: 'Trước GD',
                    align: 'center',
                    valign: 'middle',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return '<span class="text-primary">' + accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s") + '</span>';
                    }
                },
                {
                    field: 'AmountModified',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if (row.AmountBefore > row.AmountAfter) {
                            return '<span class="text-danger">-' + accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s") + '</span>';
                        }
                        else {
                            return '<span class="text-success">+' + accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s") + '</span>';
                        }
                    }
                },
                {
                    field: 'AmountAfter',
                    title: 'Sau GD',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return '<span class="text-dark">' + accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s") + '</span>';
                    }
                },
                {
                    field: 'CreatedDate',
                    title: 'Ngày GD',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return base.bigdateFormatJsonDMY(value);
                    }

                },
                {
                    field: 'Note',
                    title: 'Ghi chú',
                    align: 'center',
                    valign: 'middle'
                },
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblHistoryTransfer').bootstrapTable('refresh', { silent: true });
                }
            }
        });
    },
    getlistPlayGames: function () {
        $('#tblPlayGame').bootstrapTable('destroy');
        $('#tblPlayGame').bootstrapTable({
            method: 'get',
            url: '/AdministratorManager/HistoryTransfer/ListAllPlayHistoryPaging',
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    idAccount: $('#sltAccount').val()
                };
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            limit: 10,
            pageSize: 10,
            pageList: [10, 20, 30, 50],
            search: false,
            //showColumns: true,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: 'Id',
                    title: 'Mã GD',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return '#' + value;
                    }
                },
                {
                    field: 'Username',
                    title: 'Khách hàng',
                    align: 'center',
                    valign: 'middle',
                    valign: 'middle'
                },
                {
                    field: 'SessionId',
                    title: 'Phiên',
                    align: 'center',
                    valign: 'middle',
                    valign: 'middle'
                },
                {
                    field: 'Value',
                    title: 'Đặt cược',
                    align: 'center',
                    valign: 'middle',
                    valign: 'middle'
                },
                {
                    field: 'Result',
                    title: 'Kết quả',
                    align: 'center',
                    valign: 'middle',
                    valign: 'middle'
                },
                {
                    field: 'Amount',
                    title: 'Tiền cược',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s")
                    }
                },
                {
                    field: 'AmountReceive',
                    title: 'Số tiền nhận',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if (row.Result != null && row.Result != '') {
                            return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s");
                        }
                        else {
                            return '';
                        }
                    }
                },
                {
                    field: 'CreatedDate',
                    title: 'Ngày cược',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return base.bigdateFormatJsonDMY(value);
                    }

                }
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblPlayGame').bootstrapTable('refresh', { silent: true });
                }
            }
        });
    },
};
$(document).ready(function () {
    historyTransfer.registerControl();
});