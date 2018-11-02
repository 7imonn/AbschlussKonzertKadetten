// THIS FILE ONLY GETS LOADED BY THE ANMELDUNG PAGE (FRONTEND)

const url = 'https://kadetten-dev.scapp.io/api/redactor';


function GetformularStatus() {
	fetch(url + "/active")
		.then(res => res.json())
		.then(function (data) {
			if (data == true) {
				// FIRE OFF GET ALL CONCERT INFOS
				getConcertInfo('title-concert-1', '#ticketform > fieldset:nth-child(3) > fieldset:nth-child(1) > legend > h3');
				getConcertInfo('time-concert-1', '#ticketform > fieldset:nth-child(3) > fieldset:nth-child(1) > legend > time');
				getConcertInfo('title-concert-2', '#ticketform > fieldset:nth-child(3) > fieldset:nth-child(2) > legend > h3');
				getConcertInfo('time-concert-2', '#ticketform > fieldset:nth-child(3) > fieldset:nth-child(2) > legend > time');

			}
			else if (data == false){
				document.querySelector('.content').innerHTML = '<h2>Die Reservation ist momentan nicht verfügber</h2>'
			}
		});
}

function getConcertInfo(name, selector) {
	var insertElement = document.querySelector(selector);
	fetch(url + "/" + name)
		.then(res => res.json())
		.then(function (data) {
			if (name.includes('time')){
				data.text = data.text.replace(':', '.');
				data.text += ' Uhr';
			}
			insertElement.innerHTML = data.text;
		});
}
 
 
window.onload = function () {
	GetformularStatus();	
}