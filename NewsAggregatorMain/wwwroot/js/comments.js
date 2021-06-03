

let commentsDisplaySwitcherElement = document.getElementById('comments-display-switcher');


commentsDisplaySwitcherElement.onmouseover = function () {
    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace('btn-primary', 'btn-info');
}

commentsDisplaySwitcherElement.onmouseout = function () {
    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace('btn-info', 'btn-primary');
}

commentsDisplaySwitcherElement.onmousedown = function () {
    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace('btn-info', 'btn-secondary');
}

commentsDisplaySwitcherElement.onmouseup = function () {
    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace('btn-secondary', 'btn-info');
}

let isCommentsDisplayed = false;




function toggleComments(newsId) {
    //let url = window.location.pathname;
    //let id = url.substring(url.lastIndexOf('/') + 1);

    console.log(newsId);
    if (commentsDisplaySwitcherElement != null) {
        if (isCommentsDisplayed == true) {
            commentsDisplaySwitcherElement.innerHTML = 'Display comments';
            document.getElementById('comments-container').innerHTML = '';
        } else {
            commentsDisplaySwitcherElement.innerHTML = 'Hide comments';
            let commentsContainer = document.getElementById('comments-container');
            loadComments(newsId, commentsContainer);
        }
        isCommentsDisplayed = !isCommentsDisplayed;
    }

    commentsDisplaySwitcherElement.addEventListener('onclose', function () {
        alert('closed');
    }, false);
}

function loadComments(newsId, commentsContainer) {
    let request = new XMLHttpRequest();
    request.open('GET', `/Comments/List?newsId=${newsId}`, true); //*1-й парам: тип отправляемого запроса, 2-й: куда будем все отправлять*,3-й: асинхронный ли это запрос*/

    request.onload = function () {

        if (request.status >= 200 && request.status < 400) {
            let resp = request.responseText;/*сюда пришел весь HTML*/
            commentsContainer.innerHTML = resp;/*подставляем пришедший HTML в блок*/

            if (!resp.includes('card-text')) {
                let comm = document.getElementById('comments-container');
                comm.innerHTML = '<h3>Эту нововсть никто не комментировал (: </h3>' + resp;
                /* comm.innerHTML = '<div>@await Html.PartialAsync("CreateCommentPartial")</div>';*/
            }
            else {

                commentsContainer.innerHTML = resp;/*подставляем пришедший HTML в блок*/
            }
        }
    }

    request.send();
    /*после чего отправляем запрос*/

}



















/*function toggleComments(newsId) {
    /* let url = window.location.pathname;*//*получаем полный урл страницы*//*
let id = url.substring(url.lastIndexOf('/') + 1)*//*получаем Id из урла

console.log(id);*//*

if (commentDispleySwitherElement != null) {
    if (isCommentsDispley == true) {
        commentDispleySwitherElement.innerHTML = 'Display comments';
        document.getElementById('comments-container').innerHTML = '';
    }
    else {
        commentDispleySwitherElement.innerHTML = 'Hide comments';
        let commentsConteiner = document.getElementById('comments-container');
        loadComments(newsId, commentsContainer);
    }
    isCommentsDispley = !isCommentsDispley;
}
}

function loadComments(newsId, commentsContainer) {
let request = new XMLHttpRequest();
request.open('GET', `/Comments/List?newsId=${newsId}`, true);*//*1-й парам: тип отправляемого запроса, 2-й: куда будем все отправлять*,3-й: асинхронный ли это запрос*//*
request.onload = function () { *//*onload - событие "Как только данный реквест у нас произошел"*//*
    if (request.status >= 200 && request.status < 400) {
        let resp = request.responseText;
        *//*console.log(resp);*//*
}
}
request.send();
}*/