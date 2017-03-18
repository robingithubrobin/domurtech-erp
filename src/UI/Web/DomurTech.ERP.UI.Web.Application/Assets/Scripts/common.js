function UpperCase(str) {
    str = str.replace("Ç", "Ç");
    str = str.replace("Ğ", "Ğ");
    str = str.replace("I", "I");
    str = str.replace("İ", "İ");
    str = str.replace("Ö", "Ö");
    str = str.replace("Ş", "Ş");
    str = str.replace("Ü", "Ü");
    str = str.replace("ç", "Ç");
    str = str.replace("ğ", "Ğ");
    str = str.replace("ı", "I");
    str = str.replace("i", "İ");
    str = str.replace("ö", "Ö");
    str = str.replace("ş", "Ş");
    str = str.replace("ü", "Ü");
    str = str.toUpperCase();
    return str;
}

function UpperCaseFirst(str) {
    return (str + "").replace(/^([a-z])|\s+([a-z])/g, function ($1) {
        return UpperCase($1);
    });
}

function ConvertToSeoLiteral(str) {
    str = str.replace(" ", ".");
    str = str.replace("Ç", "c");
    str = str.replace("Ğ", "g");
    str = str.replace("I", "i");
    str = str.replace("İ", "i");
    str = str.replace("Ö", "o");
    str = str.replace("Ş", "s");
    str = str.replace("Ü", "u");
    str = str.replace("ç", "c");
    str = str.replace("ğ", "g");
    str = str.replace("ı", "i");
    str = str.replace("i", "i");
    str = str.replace("ö", "o");
    str = str.replace("ş", "s");
    str = str.replace("ü", "u");
    str = str.toLowerCase();
    return str;
}