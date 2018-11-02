// THIS FILE GETS LOADED BY THE INTRO/ INDEX PAGE AND THE FORM FEEDBACK PAGE (FRONTEND)

const url = 'https://kadetten-dev.scapp.io/api/redactor';

function GetformularStatus() {
	fetch(url + "/active")
		.then(res => res.json())
		.then(function (data) {
			if (data == true) {
				document.querySelector('body > main > article').setAttribute('data-redactor', "intro-active");
				var button = document.querySelector('body > footer > a');
				button.classList.add("active");
				getRedactor()
			}
			else if (data == false) {
				document.querySelector('body > main > article').setAttribute('data-redactor', "intro-inactive");
				var button = document.querySelector('body > footer > a');
				button.parentNode.removeChild(button);
				getRedactor()
			}
		});
}

function getRedactor() {
		var editortext = document.querySelector('.editortext');
		var name = editortext.getAttribute("data-redactor");
		fetch(url + "/" + name)
			.then(res => res.json())
			.then(function (data) {
				editortext.innerHTML = data.text;
			});
}

/*document.addEventListener('DOMContentLoaded', function() {
	getRedactor();
 }, false);*/
 
 
window.onload = function () {
	//CHECKING WHICH PAGE IT IS BECAUSE WE ONLY NEED TO CHECK FOR FORMSTATUS ON INDEX PAGE.
	if(document.URL.indexOf("index.html") > 0 || window.location.pathname == "/" ){
		GetformularStatus();
	}else{
		getRedactor();
	}
}