'use strict';

$(window).on('load', function() {

    var body = $('body');

    switch (body.attr('data-page')) {
        case "landing":
            var swiper = new Swiper(".swiper-intro", {
                pagination: {
                    el: ".pagination-intro",
                },
            });

            /* app install toast message */
            var toastElList = document.getElementById('toastinstall');
            var toastElinit = new bootstrap.Toast(toastElList, {
                // autohide: "!1",
                autohide: true,
                delay: 5000,
            });
            toastElinit.show();

            /* PWA add to phone Install ap button */
            var btnAdd = document.getElementById('addtohome');
            var defferedPrompt;
            window.addEventListener("beforeinstallprompt", function(event) {
                event.preventDefault();
                defferedPrompt = event;

                btnAdd.addEventListener("click", function(event) {
                    defferedPrompt.prompt();


                    defferedPrompt.userChoice.then((choiceResult) => {
                        if (choiceResult.outcome === 'accepted') {
                            console.log('User accepted the A2HS prompt');
                        } else {
                            console.log('User dismissed the A2HS prompt');
                        }
                        defferedPrompt = null;
                    });
                });
            });

            break;

        case "home":
            var swiper1 = new Swiper(".categoriesswiper", {
                slidesPerView: "auto",
                spaceBetween: 12,
            });

            var swiper2 = new Swiper(".offerslides", {
                slidesPerView: "1",
                spaceBetween: 10,
                pagination: {
                    el: ".pagination-offerslides",
                },
                breakpoints: {
                    640: {
                        slidesPerView: 1,
                    },
                    768: {
                        slidesPerView: 2,
                        spaceBetween: 20,
                    },
                    1024: {
                        slidesPerView: 3,
                        spaceBetween: 30,
                    },
                },
            });

            var swiper3 = new Swiper(".trendingslides", {
                slidesPerView: "auto",
                spaceBetween: 10,
            });

            var swiper4 = new Swiper(".shopslides", {
                slidesPerView: "auto",
                spaceBetween: 0,
            });

            break;
    }

});