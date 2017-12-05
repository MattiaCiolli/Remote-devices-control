function deviceFunctions() {
    var selectedDevice = $('#dispositivi').value();
    alert(selectedDevice);
    if (!selectedDevice) {
        alert("Please select a device");
        return;
    }

    $.ajax({
        type: "GET",
        url: 'http://localhost:54610/Devices/asd1/Functions',
        dataType: "json",
        success: function (data) {
            var $el = $("#funzioni");
            $el.empty();
            $.each(data, function (i, obj) {
                $el.append($("<ion-option></ion-option>").attr("value", obj.id).text(obj.descrizione));
            });
        },
        error: function (msg) {
            console.log(msg);
            alert("Invio ordine fallito, si prega di riprovare...");
        }
    });
}