function CustomToast() {

    function init() {
        toastr.options = {
            "closeButton": true,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "closeDuration": "300",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",
            "closeEasing" : "swing"
        }
    }

    function success(title, msg) {
        toastr.success(msg, title);
    }

    function info(title, msg) {
        toastr.info(msg, title);
    }

    function warning(title, msg) {
        toastr.warning(msg, title);
    }

    function error(title, msg) {
        toastr.error(msg, title);
    }

    init();

    return {
        success: success,
        info: info,
        warning: warning,
        error: error
    }
}