var home = {
    registerControl: function () {
        home.getCurrentOrder();
        home.getHistoryWin();
        setInterval(function () {
            home.getHistoryWin();
        }, 10000);
        $('#radioCheckedAll, #radioCheckedMyAccount').change(function () {
            home.getHistoryWin();
        });
    },
    getHistoryWin: function () {
        $.ajax({
            url: '/lich-su-chien-thang',
            type: 'get',
            data: {
                idAccount: $('#radioCheckedAll').is(':checked') ? 0 : 1
            },
            success: function (res) {
                $('#listHistoryPlayWin').html(res);
            }
        });
    },
    getCurrentOrder: function () {
        $.ajax({
            url: '/don-hang-gan-day',
            type: 'get',
            success: function (res) {
                $('.listCurrentOrder').html(res);
                var swiper1 = new Swiper(".trendingslides2222", {
                    slidesPerView: "2",
                    spaceBetween: 12,
                });
            }
        });
    }
};
$(document).ready(function () {
    home.registerControl();
});