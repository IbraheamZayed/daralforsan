$(window).on('load', function () {
    $('.loader').fadeOut(1000);
    new WOW().init();
});

$(document).ready(function () {
    "use strict";

    $(".close-open-nav").on("click", function (e) {
        e.stopPropagation();
        $(this).toggleClass("active");
        if ($(this).hasClass("active")) {
            $(".nav_bar").addClass("active");
        } else {
            $(".nav_bar").removeClass("active");
        }
    });

    $("body").on("click", function () {
        $(".close-open-nav.active").click();
    });

    $('.counter').countUp();

});


$(document).ready(function () {
    "use strict";
    $(".owl-index").owlCarousel({
        loop: true,
        margin: 0,
        nav: true,
        dots: true,
        rtl: true,
        lazyLoad: true,
        smartSpeed: 2500,
        autoplay: true,
        autoplayTimeout: 4000,
        navText: ["<span><i class='fas fa-angle-double-left'></i></span>", "<span><i class='fas fa-angle-double-right'></i></span>"],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });


    $('.customers-testimonials').owlCarousel({
        loop: true,
        center: true,
        rtl: true,
        margin: -38,
        dots: true,
        autoplay: true,
        autoplayTimeout: 2500,
        smartSpeed: 1500,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            1170: {
                items: 3
            }
        }
    });

    //var Hwindow = $(window).height() - $("header").outerHeight();

    $(window).scroll(function () {
        if ($(this).scrollTop() > 1) {
            $("header").addClass("fixed");
        } else {
            $("header").removeClass("fixed");
        }
    });

    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $(".back-to-top").addClass("active");
        } else {
            $(".back-to-top").removeClass("active");
        }
    });

    $(".back-to-top").on("click", function () {
        $("html, body").animate({
            scrollTop: 0
        }, 500);
    });

});

$(document).ready(function () {
    "use strict";
    $("body [data-link]").on("click", function (e) {
        e.preventDefault();
        $("html, body").animate({
            scrollTop: $("#" + $(this).attr("data-link")).offset().top - 50
        }, 700);
    });

    $(window).on("scroll", function () {
        $(".body-content > section").each(function () {
            if ($(window).scrollTop() >= $(this).offset().top - 50) {
                $(".nav_bar a[data-link='" + $(this).attr("id") + "']").addClass("active").parent().siblings().children().removeClass("active");
            }
        });
    });


    $('.my-news-ticker').AcmeTicker({
        speed: 100,
        autoplay: 100,
        type: 'marquee',
        direction: 'right',
    });

});