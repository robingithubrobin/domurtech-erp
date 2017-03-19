$(function () {
    $("body *").each(function() {
        if ($(this).css("text-transform") === "uppercase") {
            $(this).attr("lang", "tr");
        }
    });
    $(".field-validation-error").parent().addClass("has-error");

    $("#Step3Form #FirstName").blur(function () {
        var value = $(this).val();
        $(this).val(UpperCaseFirst(value));
    });

    $("#Step3Form #LastName").blur(function () {
        var value = $(this).val();
        $(this).val(UpperCase(value));
    });

    $("#Step3Form #Username").focus(function () {
        var name = $("#Step3Form #FirstName").val();
        var surname = $("#Step3Form #LastName").val();
        $(this).val(ConvertToSeoLiteral(name) + "." + ConvertToSeoLiteral(surname));
    });

});
