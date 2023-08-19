var base = {
    registerControl: function () {
        base.refreshInformation();
    },
    smalldateFormatJsonDMY: function (datetime) {
        if (datetime == '' || datetime == undefined || datetime == null) {
            return '';
        } else {
            var newdate = new Date(parseInt(datetime.substr(6)));
            var month = newdate.getMonth() + 1;
            var day = newdate.getDate();
            var year = newdate.getFullYear();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            return day + "/" + month + "/" + year;
        }
    },
    bigdateFormatJsonDMY: function (datetime) {
        if (datetime == '' || datetime == undefined || datetime == null) {
            return '';
        } else {
            var newdate = new Date(parseInt(datetime.substr(6)));
            var month = newdate.getMonth() + 1;
            var day = newdate.getDate();
            var year = newdate.getFullYear();
            var hour = newdate.getHours();
            var min = newdate.getMinutes();
            var sec = newdate.getSeconds();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            if (hour < 10)
                hour = "0" + hour;
            if (min < 10)
                min = "0" + min;
            if (sec < 10)
                sec = "0" + sec;
            return day + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;
        }
    },
    notification: function (type, message) {
        var strAlert = '<div class="alert-classic"><div class="alert alert-' + type + ' d-flex align-items-center" role="alert">' + (type == 'danger' ? '<i class="iconly-Danger icli"></i>' : '<i class="iconly-Tick-Square icli" ></i>') + '<div>' + message + '</div></div></div>';
        return strAlert;
    },
    success: function (message) {
        toastr({
            type: "success",
            message: message,
            timer: 2000
        });
    },
    error: function (message) {
        toastr({
            type: "error",
            message: message,
            timer: 2000
        });
    },
    copy: function (content) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(content).select();
        document.execCommand("copy");
        $temp.remove();
    },
    refreshInformation: function () {
        $.ajax({
            url: '/lay-thong-tin-user',
            type: 'GET',
            success: function (res) {
                if (res.code == 404) {
                    base.error('Hết thời gian đăng nhập, chuyển hướng về trang đăng nhập sau 2 giây')
                    setTimeout(function () {
                        window.location.href = '/dang-xuat';
                    }, 2000);
                }
                else if (res.code == 401) {
                    base.error('error', 'Tài khoản đang đăng nhập ở một máy khác, chuyển hướng về trang đăng nhập sau 2 giây');
                    setTimeout(function () {
                        window.location.href = '/dang-xuat';
                    }, 2000);
                }
                else {
                    if ($('.moneyReload').length > 0) {
                        $('.moneyReload').html(accounting.formatMoney(res.AmountAvaiable, "đ", 0, ".", ",", "%v%s"));
                    }
                }
            }
        });
    }
};
$(document).ready(function () {
    base.registerControl();
});