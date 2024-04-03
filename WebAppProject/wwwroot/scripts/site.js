'use strict';

document.addEventListener('DOMContentLoaded', function () {
    var body = document.querySelector('body');
    var contentWrapper = document.querySelector('.content-wrapper');
    var scroller = document.querySelector('.container-scroller');
    var footer = document.querySelector('.footer');
    var sidebar = document.querySelector('.sidebar');

    //Add active class to nav-link based on url dynamically
    //Active class can be hard coded directly in html file also as required

    function addActiveClass(element) {
        let indicator = document.querySelector('indicator').innerText;
        console.log('indicator = ' + indicator);
        console.log('nav-item id = ' + element.closest('.nav-item').getAttribute('id'));
        if (element.closest('.nav-item').getAttribute('id') == indicator) {
                element.closest('.nav-item').classList.add('active');
        }
    }

    document.querySelectorAll('.nav li a', sidebar).forEach(function (item) {
        addActiveClass(item);
    });

    document.querySelectorAll('.horizontal-menu .nav li a').forEach(function (item) {
        addActiveClass(item);
    });

    //Close other submenu in sidebar on opening any

    sidebar.addEventListener('show.bs.collapse', function (event) {
        var collapses = sidebar.querySelectorAll('.collapse.show');
        collapses.forEach(function (collapse) {
            collapse.classList.remove('show');
        });
    });

    //Change sidebar and content-wrapper height
    applyStyles();

    function applyStyles() {
        //Applying perfect scrollbar
        if (!body.classList.contains("rtl")) {
            if (document.querySelectorAll('.settings-panel .tab-content .tab-pane.scroll-wrapper').length) {
                const settingsPanelScroll = new PerfectScrollbar('.settings-panel .tab-content .tab-pane.scroll-wrapper');
            }
            if (document.querySelectorAll('.chats').length) {
                const chatsScroll = new PerfectScrollbar('.chats');
            }
            if (body.classList.contains("sidebar-fixed")) {
                if (document.querySelector('#sidebar')) {
                    var fixedSidebarScroll = new PerfectScrollbar('#sidebar .nav');
                }
            }
        }
    }

    document.querySelectorAll('[data-toggle="minimize"]').forEach(function (item) {
        item.addEventListener('click', function () {
            if ((body.classList.contains('sidebar-toggle-display')) || (body.classList.contains('sidebar-absolute'))) {
                body.classList.toggle('sidebar-hidden');
            } else {
                body.classList.toggle('sidebar-icon-only');
            }
        });
    });

    //checkbox and radios
    document.querySelectorAll(".form-check label,.form-radio label").forEach(function (item) {
        item.innerHTML += '<i class="input-helper"></i>';
    });

    //Horizontal menu in mobile
    document.querySelector('[data-toggle="horizontal-menu-toggle"]').addEventListener('click', function () {
        document.querySelector(".horizontal-menu .bottom-navbar").classList.toggle("header-toggled");
    });

    // Horizontal menu navigation in mobile menu on click
    var navItemClicked = document.querySelectorAll('.horizontal-menu .page-navigation >.nav-item');
    navItemClicked.forEach(function (item) {
        item.addEventListener('click', function (event) {
            if (window.matchMedia('(max-width: 991px)').matches) {
                if (!(this.classList.contains('show-submenu'))) {
                    navItemClicked.forEach(function (navItem) {
                        navItem.classList.remove('show-submenu');
                    });
                }
                this.classList.toggle('show-submenu');
            }
        });
    });

    window.addEventListener('scroll', function () {
        if (window.matchMedia('(min-width: 992px)').matches) {
            var header = document.querySelector('.horizontal-menu');
            if (window.scrollY >= 70) {
                header.classList.add('fixed-on-scroll');
            } else {
                header.classList.remove('fixed-on-scroll');
            }
        }
    });

    // focus input when clicking on search icon
    document.getElementById('navbar-search-icon').addEventListener('click', function () {
        document.getElementById("navbar-search-input").focus();
    });
});
