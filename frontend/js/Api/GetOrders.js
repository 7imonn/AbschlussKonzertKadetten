function GetOrders() {
    $.ajax({
        type: 'GET',
        url: 'https://localhost:44389/api/Order',
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, item) {
                $.each(item, function(key, value){
                console.log(value);
                })
            });
        },
        function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR, textStatus, errorThrown);
        },
    });
}