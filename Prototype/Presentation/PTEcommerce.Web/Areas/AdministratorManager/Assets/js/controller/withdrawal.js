var withdrawal = {
    registerControl: function () {
        withdrawal.getlistnetwork();
        $('#sltAccountSearch, #sltBankSearch').select2();
        $('#btnSearch').click(function () {
            withdrawal.getlistnetwork();
        });
        $('#btnSubmitWithdrawal').click(function () {
            withdrawal.submitOrder();
        });
        $('#sltStatus').change(function () {
            if ($(this).val() == '2') {
                $('#ipNote').val('Chuyển thành công');
            }
            else {
                $('#ipNote').val('');
            }
        });
    },
    getlistnetwork: function () {
        var searchUrl = "/AdministratorManager/Withdrawal/ListAllPaging";
        $('#tblHistoryWithdrawal').bootstrapTable('destroy');
        $('#tblHistoryWithdrawal').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    status: $('#sltStatusSearch').val(),
                    idAccount: $('#sltAccountSearch').val()
                };
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            limit: 20,
            pageSize: 20,
            pageList: [20, 50, 100, 200, 500],
            search: false,
            //showColumns: true,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: 'Id',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if (row.Status == 1) {
                            var html = '<button type="button" class="btn btn-primary btn-sm btnShowConfirmOrder" data-nick="' + row.Nick + '" data-id="' + value + '" data-bankname="' + row.BankName + '" data-bankaccount="' + row.BankAccount + '" data-banknumber="' + row.BankNumber + '" data-amount="' + row.Amount + '" data-status="' + row.Status + '"><i class="bx bx-check"></i></button>';
                            return html;
                        }
                        else {
                            return '';
                        }
                    }
                },
                {
                    field: 'Username',
                    title: 'Tài khoản',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'BankName',
                    title: 'Ngân hàng - Ví',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'BankAccount',
                    title: 'Thông tin CK',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '';
                        html += '<span class="text-primary"><b>' + row.BankAccount + '</b></span><br/>';
                        html += '<span class="text-danger"><b>' + row.BankNumber + '</b></span>';
                        return html;
                    }
                },
                {
                    field: 'Amount',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s");
                    }
                },
                {
                    field: 'CreatedDate',
                    title: 'Ngày rút',
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
                {
                    field: 'Status',
                    title: 'Trạng thái',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        switch (value) {
                            case 1:
                                return '<span class="badge badge-pill badge-soft-primary font-size-11">Đang xử lý</span>';
                            case 2:
                                return '<span class="badge badge-pill badge-soft-success font-size-11">Đã duyệt</span>';
                            case 4:
                                return '<span class="badge badge-pill badge-soft-danger font-size-11">Đã hủy</span>';
                        }
                    }
                }
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblHistoryWithdrawal').bootstrapTable('refresh', { silent: true });
                }
                $('#spanTotalPrices').html(accounting.formatMoney(data.totalAmount, "đ", 0, ".", ",", "%v%s"));
                $('.btnShowConfirmOrder').click(function () {
                    var bankname = $(this).data('bankname');
                    var bankaccount = $(this).data('bankaccount');
                    var banknumber = $(this).data('banknumber');
                    var amount = $(this).data('amount');
                    var status = $(this).data('status');
                    var id = $(this).data('id');
                    $('#ipOrderId').val(id);
                    $('#ipBankName').val(bankname);
                    $('#ipBankAccount').val(bankaccount);
                    $('#ipBankNumber').val(banknumber);
                    $('#ipBankAmount').val(amount);
                    $('#sltStatus').val(status);
                    $('#myModalConfirmWithdrawal').modal('show');
                });
            }
        });
    },
    submitOrder: function () {
        if ($('#sltStatus').val() == 1) {
            base.toastrAlert('Vui lòng chọn trạng thái duyệt rút tiền', 'error');
        }
        else if ($('#ipNote').val() == '') {
            base.toastrAlert('Vui lòng nhập ghi chú rút tiền', 'error');
        }
        else {
            $.ajax({
                url: '/AdministratorManager/Withdrawal/SubmitOrder',
                type: 'post',
                data: {
                    id: $('#ipOrderId').val(),
                    status: $('#sltStatus').val(),
                    note: $('#ipNote').val()
                },
                success: function (res) {
                    if (res) {
                        $('#myModalConfirmWithdrawal').modal('hide');
                        base.toastrAlert(res.message, 'success');
                        $('#tblHistoryWithdrawal').bootstrapTable('refresh', { silent: true });
                    }
                    else {
                        base.toastrAlert(res.message, 'error');
                    }
                }
            })
        }
    }
};
$(document).ready(function () {
    withdrawal.registerControl();
});