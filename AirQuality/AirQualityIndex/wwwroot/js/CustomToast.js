function CustomToast() {
    const constants = {
        continueDisplay: 'continueDisplay'
    }

    const properties = {
        closeButton: 'closeButton',
        newestOnTop: 'newestOnTop',
        progressBar: 'progressBar',
        positionClass: 'positionClass',
        preventDuplicates: 'preventDuplicates',
        onclick: 'onclick',
        showDuration: 'showDuration',
        hideDuration: 'hideDuration',
        closeDuration: 'closeDuration',
        timeOut: 'timeOut',
        extendedTimeOut: 'extendedTimeOut',
        showEasing: 'showEasing',
        hideEasing: 'hideEasing',
        showMethod: 'showMethod',
        hideMethod: 'hideMethod',
        closeEasing: 'closeEasing'
    };

    const init = function() {
        let options = {};
        options[properties.closeButton] = true;
        options[properties.newestOnTop] = true;
        options[properties.progressBar] = true;
        options[properties.positionClass] = "toast-top-right";
        options[properties.preventDuplicates] = false;
        options[properties.onclick] = null;
        options[properties.showDuration] = "300";
        options[properties.hideDuration] = "1000";
        options[properties.closeDuration] = "300";
        options[properties.timeOut] = "5000";
        options[properties.extendedTimeOut] = "1000";
        options[properties.showEasing] = "swing";
        options[properties.hideEasing] = "linear";
        options[properties.showMethod] = "fadeIn";
        options[properties.hideMethod] = "fadeOut";
        options[properties.closeEasing] = "swing";
        toastr.options = options;
    };

    const createOverrideOptions = function (type) {
        let overrideOptions = {};
        if (type == constants.continueDisplay) {
            overrideOptions[properties.progressBar] = false;
            overrideOptions[properties.timeOut] = 0;
        }
        return overrideOptions;
    }
    const success = function (title, msg) {
        toastr.success(msg, title);        
    };

    const info = function (title, msg, isAutohide = true) {
        if (!isAutohide) {
            toastr.info(msg, title, createOverrideOptions(constants.continueDisplay));
        }
        else {
            toastr.info(msg, title);
        }
    };

    const warning = function (title, msg, isAutohide = true) {
        if (!isAutohide) {
            toastr.warning(msg, title, createOverrideOptions(constants.continueDisplay));
        }
        else {
            toastr.warning(msg, title);
        }
    };

    const error = function (title, msg, isAutohide = true) {
        if (!isAutohide) {
            toastr.error(msg, title, createOverrideOptions(constants.continueDisplay));
        }
        else {
            toastr.error(msg, title);            
        }
    };

    const hideNotification = function () {
        toastr.clear()
    }

    init();

    return {
        success: success,
        info: info,
        warning: warning,
        error: error,
        hide: hideNotification
    }
}