/*$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: 'http://localhost:54610/dispositivi',
        dataType: "json",
        success: function (data) {
            var $el = $("#dispositivi");
            $el.empty();
            $.each(data, function (i, obj) {
                $el.append($("<ion-option></ion-option>").attr("value", obj.ID).text(obj.ID));
            });
        },
        error: function (msg) {
            console.log(msg);
            alert("Invio ordine fallito, si prega di riprovare...");
        }
    });
});*/