$(window).bind("resize", resizeWindow);

function resizeWindow(e) {
    var winHeight = $(window).height();
    var winWidth = $(window).width();

    if (winHeight) {
        $("#main-content").css("min-height", winHeight);
    }
}

function showMessage(id, type, message) {
    switch (type) {
        case 'error':
            $(id).removeClass('alert-sucess').removeClass('alert-info').addClass('alert-danger');
            break;
        case 'success':
            $(id).removeClass('alert-danger').removeClass('alert-info').addClass('alert-success');
            break;
        default:
            $(id).removeClass('alert-danger').removeClass('alert-info').addClass('alert-success');
            break;
    }
    $(id).fadeIn('fast', function () {
    });
    $(id).html(message);
}

(function () {
    resizeWindow(null);
})();