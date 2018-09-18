const uri = 'https://localhost:44389/api/order';

function addItem() {
    const order = {
        'ticketE': $('#add-ticketESa').val(),
        'ticketK': $('#add-ticketKSa').val(),
        'ticketKK': $('#add-ticketKKSa').val(),
        'ticketE': $('#add-ticketESo').val(),
        'ticketK': $('#add-ticketKSo').val(),
        'ticketKK': $('#add-ticketKKSo').val(),
    };
    var url = 'https://example.com/profile';
    var data = {
        email: $('#add-email').val(),
        clientLastName: $('#add-lastname').val(),
        clientFirstName: $('#add-firstname').val(),
        bemerkung: $('#add-bemerkung').val(),
        kadettLastName: "nöbu",
        kadettFirstName: "pipp",
        tickets: ticket
      };
    
    fetch(url, {
      method: 'POST', // or 'PUT'
      body: JSON.stringify(data), // data can be `string` or {object}!
      headers:{
        'Content-Type': 'application/json'
      }
    }).then(res => res.json())
    .then(response => console.log('Success:', JSON.stringify(response)))
    .catch(error => console.error('Error:', error));