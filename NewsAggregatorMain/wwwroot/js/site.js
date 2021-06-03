// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let loginRequest = new XMLHttpRequest();
loginRequest.open('GET', '/Account/UserInfo', true);
loginRequest.onload = function () {
    if (loginRequest.status >= 200 && loginRequest.status < 400) {
        let resp = loginRequest.responseText;
        document.getElementById('user-info').innerHTML = resp;
    }
}

//send request
loginRequest.send();