const uri = 'https://localhost:44389/api/order';

function addItem() {
    const order = {
        'email': $('#add-email').val(),
        'lastname': $('#add-lastname').val(),
        'firstname': $('#add-firstname').val(),
        'bemerkung': $('#add-bemerkung').val(),
        'ticketE': $('#add-ticketESa').val(),
        'ticketK': $('#add-ticketKSa').val(),
        'ticketKK': $('#add-ticketKKSa').val(),
        'ticketE': $('#add-ticketESo').val(),
        'ticketK': $('#add-ticketKSo').val(),
        'ticketKK': $('#add-ticketKKSo').val(),
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify({Order: order}),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('fail');
        },
        success: function (result) {
            getData();
            $('#add-name').val('');
        }
    });
}