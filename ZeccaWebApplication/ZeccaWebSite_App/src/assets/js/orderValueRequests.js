//changes the request's order value 
function updateOrder() {
    var i = 0;
    $(".order").each(
        function () {
            $(".reqOrder").text("Richiesta " + i);
            i++;
        }
    );
};