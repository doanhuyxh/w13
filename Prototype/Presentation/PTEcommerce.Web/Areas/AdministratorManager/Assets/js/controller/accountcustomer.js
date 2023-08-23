var account = {
    registerControl: function () {
        account.getlistaccount();
        $('#btnSearch').click(function () {
            account.getlistaccount();
        });
        $('#btnSubmitWithdrawal').click(function () {
            account.setamountforuser();
        });
        $('#btnSubmitChangePassword').click(function () {
            account.updatePassword();
        });
    },
    getlistaccount: function () {
        var searchUrl = "/AdministratorManager/AccountCustomer/ListAllPaging";
        $('#tblAccount').bootstrapTable('destroy');
        $('#tblAccount').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    searchString: $('#ipSearchString').val(),
                    month: $('#txtMonth').val(),
                    year: $('#txtYear').val(),
                    from: $('#txtFromAmount').val(),
                    to: $('#txtToAmount').val()
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
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {

                    field: 'Id',
                    title: 'Xem',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return `<button onclick="ViewAcc(${value})" type="button" class="p-1 btn btn-light"><i class="fa-solid fa-eye" style="color: #246ef0;"></i></button>`
                    }
                },
                {
                    field: 'Id',
                    title: 'Hành động',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '';
                        html += '<button type="button" class="btn btn-primary btn-sm btnShowAmountAccount" data-name="' + row.Username + '" data-id="' + value + '" >Cộng/trừ tiền</button> ';
                        html += '<button type="button" class="btn btn-success btn-sm btnShowChangePassword" data-name="' + row.Username + '" data-id="' + value + '" >Đổi mk</button> ';
                        return html;
                    }
                },
                {
                    field: 'Username',
                    title: 'Tài khoản',
                    align: 'center',
                    valign: 'middler'
                },
                {
                    field: 'AmountAvaiable',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return accounting.formatMoney(value, "đ", 0, ".", ",", "%v%s");
                    }
                },
                {
                    field: 'Email',
                    title: 'Email',
                    align: 'center',
                    valign: 'middle'
                },
                {
                    field: 'IsActive',
                    title: 'Trạng thái',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        console.log();
                        if (value) {
                            return `<button type="button" class="btn btn-success" style="cursor: auto !important;">Hoạt động</button>`
                        }
                        else {
                            return ` <button type="button" class="btn btn-danger" style="cursor: auto !important;">Ngừng hoạt động</button>`
                        }
                    }
                },
                {
                    field: 'CreatedDate',
                    title: 'Ngày tạo',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, index, row) {
                        return base.bigdateFormatJsonDMY(value);
                    }
                }
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblAccount').bootstrapTable('refresh', { silent: true });
                }
                $('.btnShowAmountAccount').click(function () {
                    var id = $(this).data('id');
                    var name = $(this).data('name');
                    $('#ipAccount').val(id);
                    $('#UsernameAdd').html(name);
                    $('#myModalConfirmAddAmount').modal('show');
                });
                $('.btnShowChangePassword').click(function () {
                    var id = $(this).data('id');
                    var name = $(this).data('name');
                    $('#ipAccountPassword').val(id);
                    $('#UsernamePass').html(name);
                    $('#myModalConfirmChangePassword').modal('show');
                });
            }
        });
    },
    setamountforuser: function () {
        $.ajax({
            url: '/AdministratorManager/AccountCustomer/SetAmount',
            type: 'POST',
            data: {
                idAccount: $('#ipAccount').val(),
                amount: $('#ipAmount').val(),
                note: $('#ipNote').val(),
                type: $('#sltType').val()
            },
            success: function (res) {
                if (res.status) {
                    base.toastrAlert('Tác vụ thành công', 'success');
                    $('#ipAmount').val('');
                    $('#ipNote').val('');
                    $('#myModalConfirmAddAmount').modal('hide');
                    $('#tblAccount').bootstrapTable('refresh', { silent: true });
                }
                else {
                    base.toastrAlert(res.msg, 'error');
                }
            }
        });
    },
    updatePassword: function () {
        $.ajax({
            url: '/AdministratorManager/AccountCustomer/UpdatePassword',
            type: 'POST',
            data: {
                idAccount: $('#ipAccount').val(),
                password: $('#ipPasswordChange').val(),
            },
            success: function (res) {
                if (res.status) {
                    base.toastrAlert(res.message, 'success');
                    $('#tblAccount').bootstrapTable('refresh', { silent: true });
                }
                else {
                    base.toastrAlert(res.message, 'error');
                }
            }
        });
    },
};
$(document).ready(function () {
    account.registerControl();
});