var login = {
    registerControl: function () {
        $('#btnLoginSubmit').click(function () {
            login.login();
        });
    },
    login: function () {
        $.ajax({
            url: '/AdministratorManager/Login/Login',
            data: {
                userName: $('#Username').val(),
                password: $('#Password').val()
            },
            beforeSend: function () {
                $('#btnLoginSubmit').prop('disabled', true);
                $('#btnLoginSubmit').html('Đang đăng nhập');
            },
            success: function (res) {
                $('#btnLoginSubmit').prop('disabled', false);
                $('#btnLoginSubmit').html('Đăng nhập');
                if (res.status) {
                    base.toastrAlert('Đăng nhập thành công', 'success');
                    setTimeout(function () {
                        window.location.href = '/AdministratorManager/DashBoard/Index';
                    }, 1000);
                }
                else {
                    base.toastrAlert(res.msg, 'error');
                }
            }
        });
    },
};
$(document).ready(function () {
    login.registerControl();
});