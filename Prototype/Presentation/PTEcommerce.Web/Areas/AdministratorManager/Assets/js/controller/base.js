var base = {
    registerControl: function () {
        $('#btnUpdatePasswordAdmin').click(function () {
            base.changePasswordAdmin();
        });
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
    copy: function (content) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(content).select();
        document.execCommand("copy");
        $temp.remove();
    },
    bigdateFormatJsonMDY: function (datetime) {
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
            return month + "/" + day + "/" + year + " " + hour + ":" + min + ":" + sec;
        }
    },
    nosuccess: function (text) {
        base.toastrAlert(text, 'success');
    },
    noerror: function (text) {
        base.toastrAlert(text, 'error');
    },
    changePasswordAdmin: function () {
        $.ajax({
            url: '/AdministratorManager/AccountCustomer/UpdatePasswordAdmin',
            type: 'POST',
            data: {
                password: $('#passwordAdmin').val(),
                repassword: $('#repasswordAdmin').val()
            },
            success: function (res) {
                if (res.status) {
                    base.toastrAlert(res.message, 'success');
                    window.location.href = '/Administratormanager';
                }
                else {
                    base.toastrAlert(res.message, 'error');
                }
            }
        });
    },
    get_hostname: function (url) {
        var domain = new URL(url)
        return domain.hostname;
    },
    toastrAlert: function (message, type) {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": 300,
            "hideDuration": 2000,
            "timeOut": 5000,
            "extendedTimeOut": 1000,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        toastr[type](message);
    },
};
$(document).ready(function () {
    base.registerControl();
});