$(function () {
    $("body *").each(function() {
        if ($(this).css("text-transform") === "uppercase") {
            $(this).attr("lang", "tr");
        }
    });

    $("#LoginCaptchaRefresh").click(function () {
        $("#LoginCaptchaImage").attr("src", $("#LoginCaptchaImage").attr("src") + "?timestamp=" + new Date().getTime());
    });
    $("#SignUpCaptchaRefresh").click(function () {
        $("#SignUpCaptchaImage").attr("src", $("#SignUpCaptchaImage").attr("src") + "?timestamp=" + new Date().getTime());
    });

    $("#ForgotPasswordCaptchaRefresh").click(function () {
        $("#ForgotPasswordCaptchaImage").attr("src", $("#ForgotPasswordCaptchaImage").attr("src") + "?timestamp=" + new Date().getTime());
    });
   

    $(".SelectListLoginLanguage").change(function () {
        var selectedVal = $(this).val();
        if (selectedVal != "") {
            parent.location = "/" + selectedVal + "/Language/Change/?returnUrl=/" + selectedVal + "/Account/Login/";
        }
    });

    $(".SelectListSignUpLanguage").change(function () {
        var selectedVal = $(this).val();
        if (selectedVal != "") {
            parent.location = "/" + selectedVal + "/Language/Change/?returnUrl=/" + selectedVal + "/Account/SignUp/";
        }
    });

    $(".SelectListForgotPasswordLanguage").change(function () {
        var selectedVal = $(this).val();
        if (selectedVal != "") {
            parent.location = "/" + selectedVal + "/Language/Change/?returnUrl=/" + selectedVal + "/Account/ForgotPassword/";
        }
    });


   




    $(".SelectListLoginLanguage").change(function () {
        var selectedVal = $(this).val();
        if (selectedVal != "") {
            parent.location = "/" + selectedVal + "/Language/Change/?returnUrl=/" + selectedVal + "/Account/Login/";
        }
    });


    $(".field-validation-error").parent().addClass("has-error");
});
