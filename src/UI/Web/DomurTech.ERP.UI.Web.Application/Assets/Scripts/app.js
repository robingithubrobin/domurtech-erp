$(function () {
    $("body *").each(function() {
        if ($(this).css("text-transform") === "uppercase") {
            $(this).attr("lang", "tr");
        }
    });
    $(".field-validation-error").parent().addClass("has-error");
});
