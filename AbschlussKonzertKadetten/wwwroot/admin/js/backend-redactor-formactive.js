// THIS FILE IS USED IN BACKEND FORMULAR AND INTRO PAGE

// const urlRedactor = 'https://kadetten-dev.scapp.io/api/redactor';
//const urlRedactor = 'https://localhost:44389/api/redactor';
 const urlRedactor = '/api/redactor';

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
    if (document.querySelectorAll('.editor-hidden-input').length > 0) {
        var name = document.querySelector('.editor-hidden-input').getAttribute('data-redactor');
        if (name !== 0) {

            if (editorExists.length > 0 && editorExistAlredy.length === 0) {
                var toolbarOptions = [[{ 'header': [2, 3, false] }], ['bold'], ['link']];
                var quill = new Quill('#editor', {
                    theme: 'snow',
                    modules: {
                        toolbar: toolbarOptions
                    }
                });
            }
            var header = base64Request();
            var req = new Request(urlRedactor + "/" + name, {
                method: 'GET',
                headers: header
            });
            fetch(req)
                .then(res => res.json())
                .then(function (data) {
                    var text = data.text;
                    document.querySelector('.ql-editor').innerHTML = text;

                });

            //if (UrlindexOfFormular > 0)
            //getRedactor();

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
            // if (myJson.status == 401) {
            // 	window.location.pathname = "/admin/login.html";
            // }
        }));
}

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