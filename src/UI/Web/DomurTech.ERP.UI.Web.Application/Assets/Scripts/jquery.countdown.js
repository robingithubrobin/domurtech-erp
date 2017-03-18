(function ($) {
    $.fn.countdown = function (options) {
        var thisLabel = $(this);
        var minute = thisLabel.html();
        var second = 0;
        var settings = $.extend({
            url: "#"
        }, options);
        thisLabel.html([Math.pow(10, 2 - minute.toString().length), minute].join("").substr(1) + ":" + [Math.pow(10, 2 - second.toString().length), second].join("").substr(1));
        setInterval(function () {
            if (second > 0) {
                second = second - 1;
            }
            else {
                second = 59;
                if (minute > 0) {
                    minute = minute - 1;
                }
                else {
                    if (thisLabel.html() != null) {
                        parent.location = settings.url;
                    }
                }
            }
            thisLabel.html([Math.pow(10, 2 - minute.toString().length), minute].join("").substr(1) + ":" + [Math.pow(10, 2 - second.toString().length), second].join("").substr(1));
        }, 1000);
        return this;
    };
}(jQuery));