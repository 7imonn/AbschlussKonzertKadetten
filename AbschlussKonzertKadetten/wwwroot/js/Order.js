const uri = 'https://kadetten-dev.scapp.io/api/order';
//const uri = 'https://localhost:44389/api/order';
//  const uri = '/api/order';


function addItem() {
    var getfrom = document.getElementById("ticketform").elements;
    var items = document.getElementsByClassName("tickets");
    var tickets = [];
    var data = {
        email: getfrom.namedItem("email").value,
        phone: getfrom.namedItem("tel").value,
        clientLastName: getfrom.namedItem("lastname").value,
        clientFirstName: getfrom.namedItem("prename").value,
        bemerkung: getfrom.namedItem("text").value,
        kadettLastName: getfrom.namedItem("child-lastname").value,
        kadettFirstName: getfrom.namedItem("child-prename").value,
        KadettInKader: getfrom.namedItem("child-kader").checked,
        tickets: tickets
    };
    for (var i = 0; i < items.length; i++) {
        var ticket = {
            type: items[i].getAttribute('data-ticket'),
            quantity: Number(items[i].value),
            day: items[i].getAttribute('data-day')
        };
        tickets.push(ticket);
    }
    fetch(uri, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(function (myJson) {
        if (myJson.status === 200)
            window.location.pathname = "/formfeedback.html";
        else
            if (myJson.status === 409) {
                window.location.pathname = "/formemailerror.html";
            } else {
                window.location.pathname = "/formerror.html";
            }
    });
}