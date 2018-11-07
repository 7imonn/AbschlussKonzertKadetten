// THIS FILE IS USED IN BACKEND FORMULAR AND INTRO PAGE

const urlRedactor = 'https://kadetten-dev.scapp.io/api/redactor';
//const urlRedactor = 'https://localhost:44389/api/redactor';
//  const urlRedactor = '/api/redactor';

var UrlindexOfFormular = document.URL.indexOf("formular.html");
var UrlindexOfIntro = document.URL.indexOf("intro.html");
var UrlindexOfReservationen = document.URL.indexOf("reservationen.html");



//window.addDomListener(window, 'load', addMap());

function GetformularStatus() {
    var header = base64Request();
    var req = new Request(urlRedactor + "/active", {
        method: 'GET',
        headers: header
    });
    fetch(req)
        .then(res => res.json())
        .then(function (data) {
            if (UrlindexOfFormular >= 0)
                document.querySelector('#form-active-button').setAttribute("data-status-active", data);

            if (data === true) {
                document.querySelector('#registration-live > span').classList.add("active");
                document.querySelector('#registration-live > p').innerHTML = "Formular aktiv";

                if (UrlindexOfFormular > 0) {
                    document.querySelector('#form-active-button > span').classList.add("active");
                    document.querySelector('#form-active-button > p').innerHTML = "Formular deaktivieren";
                }

                if (UrlindexOfIntro > 0) {
                    document.querySelector('.editor-hidden-input').setAttribute('data-redactor', "intro-active");
                }



            }
            else if (data === false) {
                document.querySelector('#registration-live > span').classList.remove("active");
                document.querySelector('#registration-live > p').innerHTML = "Formular inaktiv";

                if (UrlindexOfFormular > 0) {
                    document.querySelector('#form-active-button > span').classList.remove("active");
                    document.querySelector('#form-active-button > p').innerHTML = "Formular aktivieren";
                }

                if (UrlindexOfIntro > 0) {
                    document.querySelector('.editor-hidden-input').setAttribute('data-redactor', "intro-inactive");
                }
            }
            createRedactor();
        });


}


function createRedactor() {
    var editorExists = document.querySelectorAll("#editor");
    var editorExistAlredy = document.getElementsByClassName("ql-editor");
    var hiddenInputs = document.querySelectorAll('.editor-hidden-input')
    for (x = 0; x < hiddenInputs.length; x++) {
       var hiddenInput = hiddenInputs[x];
       var name = hiddenInput.getAttribute('data-redactor');
       var buildEditor = editorExists[x]

       if (name !== 0) {

            var toolbarOptions = [[{ 'header': [3, false] }], ['bold'], ['link']];
            var quill = new Quill(buildEditor, {
                theme: 'snow',
                modules: {
                    toolbar: toolbarOptions
                }
            });
            
            var header = base64Request();
            var req = new Request(urlRedactor + "/" + name, {
                method: 'GET',
                headers: header
            });
            fetch(req)
                .then(res => res.json())
                .then(function (data) {
                    var text = data.text;
                    var buildEditorId = '#'+buildEditor.getAttribute('id')
                    document.querySelector(buildEditorId+' > .ql-editor').innerHTML = text;

                });

        }
    }
}


function postRedactor() {
    var editor = document.querySelector('.ql-editor');
    var UrlindexOf = document.URL.indexOf("intro.html");
    var datas = [];
    var inputs = null;
    if (UrlindexOf > 0) {
        inputs = document.querySelectorAll('.editor-hidden-input');
    }
    else
        inputs = document.querySelectorAll('#formularform > div > input');

    for (x = 0; x < inputs.length; x++) {
        var data = {};
        var name = inputs[x].getAttribute('data-redactor');
        if (inputs[x].classList.contains("editor-hidden-input"))
            var content = editor.innerHTML;
        else
            content = inputs[x].value;


        data = {
            Name: name,
            Text: content
        };
        datas.push(data);
    }
    var header = base64Request();
    var req = new Request(urlRedactor, {
        method: 'Put',
        body: JSON.stringify(datas),
        headers: header
    });
    fetch(req)
        .then((function (myJson) {
            if (myJson.status == 200) {
             safeNotification();
            }
        }));
}

// function postRedactor() {
//     var editors = document.querySelectorAll('.ql-editor');
//     var UrlindexOf = document.URL.indexOf("intro.html");
//     var datas = [];
//     var inputs = null;
    
//     //redactors
//     var editorInputs = document.querySelectorAll('.editor-hidden-input');
    
//     for (x = 0; x < editorInputs.length; x++) {
//         var data = {};
//         var name = editorInputs[x].getAttribute('data-redactor');
//         var content = editors[x].innerHTML;

//         data = {
//             Name: name,
//             Text: content
//         };
        
//         var header = base64Request();
// 	    var req = new Request(urlRedactor+'/'+name, {
// 	        method: 'Put',
// 	        body: JSON.stringify(data),
// 	        headers: header
// 	    });
// 	    fetch(req)
// 	        .then((function (myJson) {
// 	            // if (myJson.status == 401) {
// 	            // 	window.location.pathname = "/admin/login.html";
// 	            // }
// 	        }));
// 	}
	
// 	//inputs
// 	inputs = document.querySelectorAll('#formularform > div > input');
        

//     for (x = 0; x < inputs.length; x++) {
//         var data = {};
//         var name = inputs[x].getAttribute('data-redactor');
//         content = inputs[x].value;
        
//         data = {
//             Name: name,
//             Text: content
//         };
//         datas.push(data);
//         var header = base64Request();
// 	    var req = new Request(urlRedactor+'/'+name, {
// 	        method: 'Put',
// 	        body: JSON.stringify(datas),
// 	        headers: header
// 	    });
// 	    fetch(req)
// 	        .then((function (myJson) {
// 	            // if (myJson.status == 401) {
// 	            // 	window.location.pathname = "/admin/login.html";
// 	            // }
// 	        }));
//     }
// }

function postFormularStatus() {
    var button = document.querySelector('#form-active-button');
    var status = button.getAttribute('data-status-active');

    if (status === "false")
        status = true;
    else if (status === "true")
        status = false;
    var header = base64Request();
    var req = new Request(urlRedactor + "/active/" + status, {
        method: 'Put',
        headers: header
    });
    fetch(req)
        .then(function (myJson) {
            if (myJson.status === 200) {
	            safeNotification();
                button.setAttribute("data-status-active", status);
                GetformularStatus();

                if (status === true) {
                    //document.querySelector('.editor-hidden-input').setAttribute('data-redactor', "intro-active");
                    document.querySelector('#form-active-button > span').classList.add("active");
                }
                if (status === false) {
                    //document.querySelector('.editor-hidden-input').setAttribute('data-redactor', "intro-inactive");
                    document.querySelector('#form-active-button > span').classList.remove("active");
                }
            }
            // if (myJson.status == 401) {
            // 	window.location.pathname = "/admin/login.html";
            // }
        });
}

function getConcertInfo(name) {
    var insertElement = document.querySelector('#' + name);
    var header = base64Request();
    var req = new Request(urlRedactor + "/" + name, {
        method: 'Get',
        headers: header
    });
    fetch(req)
        .then(res => res.json())
        .then(function (data) {
            //insertElement.innerHTML = data.text;
            insertElement.setAttribute('value', data.text);
        });
}

function getUserName() {
    document.querySelector('#user-panel > div > p').innerHTML = getCookieName();
}

/*document.addEventListener('DOMContentLoaded', function() {
	getRedactor();
 }, false);*/


window.onload = function () {
    GetformularStatus();
    if (getCookiePw() === null && getCookieName() === null) {
        window.location.pathname = "/admin/login.html";
    }
    if (UrlindexOfFormular >= 0) {
        getConcertInfo('title-concert-1');
        getConcertInfo('time-concert-1');
        getConcertInfo('title-concert-2');
        getConcertInfo('time-concert-2');
    }
    getUserName();
};

function logOut() {
    var datetime = new Date();
    datetime.setTime(datetime.getTime() - (1000 * 60 * 60 * 24));
    var expires = "expires=" + datetime.toGMTString();
    document.cookie = "username=;expires=" + expires;
    document.cookie = "pw=;expires=" + expires;
    window.location.pathname = "/admin/login.html";
}
function getCookiePw() {
    var pw = document.cookie.match('(^|;) ?pw=([^;]*)(;|$)');
    return pw ? pw[2] : null;

}
function getCookieName() {
    var username = document.cookie.match('(^|;) ?username=([^;]*)(;|$)');
    return username ? username[2] : null;
}

function base64Request() {
    var h = new Headers();
    h.append('Content-Type', 'application/json');
    var name = getCookieName();
    var pw = getCookiePw();
    var string = '' + name + ':' + pw + '';
    var encoded = window.btoa(string);
    var auth = 'Basic ' + encoded;
    h.append('Authorization', auth);
    return h;
}

function safeNotification()	{
	var notification = document.querySelector('#safe-notification');
	notification.classList.add("in");
	setTimeout(function(){ notification.classList.remove("in"); }, 2500);
}