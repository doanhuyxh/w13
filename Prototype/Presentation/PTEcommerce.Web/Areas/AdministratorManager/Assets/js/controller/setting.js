var settingss = {
    registerControl: function () {
        settingss.getlistsetting()
        $('#btnUpdateSettingValue').click(function () {
            settingss.updateSetting();
        });
    },
    getlistsetting: function () {
        var searchUrl = "/AdministratorManager/Setting/GetListSetting";
        $('#tblSetting').bootstrapTable('destroy');
        $('#tblSetting').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    limit: p.limit,
                    offset: p.offset,
                    networkCode: $('#sltNetwork').val(),
                    priceCard: $('#sltPricesCard').val(),
                    typeCard: $('#stlTypeCard').val(),
                    partnerGroup: $('#sltPartnerGroup').val()
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
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: '',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return '<a href="javascript:void(0)" class="btn btn-primary btn-sm btnShowUpdateSetting" data-id="' + row.Id + '" data-display="' + row.Display_Name + '" data-value="' + row.Value + '">Sửa</button>';
                    }
                },
                {
                    field: 'Display_Name',
                    align: 'center',
                    valign: 'middle',
                    title: 'Chức năng'
                },
                {
                    field: 'Value',
                    title: 'Giá trị',
                    align: 'center',
                    valign: 'middle',
                    width: '900',
                    formatter: function (value, row, index) {
                        return '<a href="javascript:void(0)" data-toggle="tooltip" title="' + value + '">' + (value.length > 100 ? value.substr(0, 100) + '...' : value) + '</span>';
                    }
                },
            ],
            onLoadSuccess: function (data) {
                if (data.total > 0 && data.rows.length === 0) {
                    $('#tblSetting').bootstrapTable('refresh', { silent: true });
                }
                $('.btnShowUpdateSetting').click(function () {
                    $('#inputKeySetting').val($(this).data('id'));
                    $('#inputDisplayName').val($(this).data('display'));
                    $('#inputValueSetting').val($(this).data('value'));
                    $('#myModalShowSetting').modal('show');
                });
            }
        })
    },
    updateSetting: function () {
        $.ajax({
            url: '/AdministratorManager/Setting/UpdateSetting',
            type: 'GET',
            data: {
                id: $('#inputKeySetting').val(),
                value: $('#inputValueSetting').val()
            },
            success: function (res) {
                if (res) {
                    base.toastrAlert('Cập nhật thành công', 'success');
                    $('#myModalShowSetting').modal('hide');
                }
                else {
                    base.toastrAlert('Cập nhật không thành công', 'error');
                }
                $('#tblSetting').bootstrapTable('refresh', { silent: true });
            }
        })
    },
};
$(document).ready(function () {
    settingss.registerControl();
});