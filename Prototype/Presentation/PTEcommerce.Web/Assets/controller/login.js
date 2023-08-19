var login = {
    registerControl: function () {
        $('#btnLogin').click(function () {
            login.login();
        });
        $('#btnRegister').click(function () {
            login.register();
        });
        $('#btnClickShowRecoveryPassword').click(function () {
            $('#modalShowRecoveryPassword').addClass('opened');
        });
        $('#btnRecoveryPassword').click(function () {
            login.recoverypassword();
        });
    },
    login: function () {
        $.ajax({
            url: '/dang-nhap',
            type: 'POST',
            data: {
                userName: $('#username').val(),
                password: $('#password').val()
            },
            beforeSend: function () {
                $('#btnLogin').prop('disabled', true);
            },
            success: function (res) {
                $('#btnLogin').prop('disabled', false);
                if (res.status) {
                    base.success(res.msg);
                    setTimeout(function () {
                        window.location.href = '/';
                    }, 1000);
                }
                else {
                    base.error(res.msg);
                }
            }
        });
    },
    register: function () {
        $.ajax({
            url: '/dang-ky',
            type: 'POST',
            data: {
                Username: $('#username').val(),
                Password: $('#password').val(),
                RePassword: $('#repassword').val()
            },
            beforeSend: function () {
                $('#btnRegister').prop('disabled', true);
            },
            success: function (res) {
                $('#btnRegister').prop('disabled', false);
                if (res.status) {
                    base.success(res.msg);
                    setTimeout(function () {
                        window.location.href = '/dang-nhap';
                    }, 2000);
                }
                else {
                    base.error(res.msg);
                }
            }
        });
    }
};
$(document).on('keypress', function (e) {
    if (e.which == 13) {
        if ($('#btnLogin').length > 0) {
            login.login();
        }
        if ($('#btnRegister').length > 0) {
            login.register();
        }
    }
});
$(document).ready(function () {
    login.registerControl();
});