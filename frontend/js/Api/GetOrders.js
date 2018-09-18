function GetOrders() {
    console.log("Get")
    fetch(uri)
    .then(res => res.json())//response type
    .then(data => console.log(data)); //log the data;
    return false

}