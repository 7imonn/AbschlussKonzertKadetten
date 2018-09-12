const uri = 'https://localhost:44389/api/Ticket';

function addItem() {
    const order = {
        'email': $('#add-email').val(),
        'lastname': $('#add-lastname').val(),
        'firstname': $('#add-firstname').val(),
        'bemerkung': $('#add-bemerkung').val(),
        'ticketE': $('#add-ticketE').val(),
        'ticketK': $('#add-ticketK').val(),
        'ticketKK': $('#add-ticketKK').val(),
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