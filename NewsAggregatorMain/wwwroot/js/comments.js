

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
    //create request
    request.open('GET', `/Comments/List?newsId=${newsId}`, true);
    //create request handler
    request.onload = function () {
        if (request.status >= 200 && request.status < 400) {
            let resp = request.responseText;
            if (!resp.includes('card-text')) {
                commentsContainer.innerHTML = '<h5>Оставьте первый комментарий </h5>' + resp;
            }
            else {

            commentsContainer.innerHTML = resp;
            }

            document.getElementById('create-comment-btn')
                .addEventListener("click", createComment);
        }
    }
    //send request
    request.send();
}

function validateCommentData() {

}

function createComment() {

    let commentText = document.getElementById('commentText').value;
    let newsId = document.getElementById('newsId').value;

    validateCommentData();

    var postRequest = new XMLHttpRequest();
    postRequest.open("POST", '/Comments/Create', true);
    postRequest.setRequestHeader('Content-Type', 'application/json');

    //let requestData = new {
    //    commentText: commentText
    //}

    postRequest.send(JSON.stringify({
        commentText: commentText,
        newsId: newsId
    }));

    postRequest.onload = function () {
        if (postRequest.status >= 200 && postRequest.status < 400) {
            document.getElementById('commentText').value = '';

            //commentsContainer.innerHTML += '';

            loadComments(newsId);
        }
    }
}

var getCommentsIntervalId = setInterval(function () {
    loadComments(newsId);
}, 15000);








/*
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
    request.open('GET', `/Comments/List?newsId=${newsId}`, true); /*//*1-й парам: тип отправляемого запроса, 2-й: куда будем все отправлять*,3-й: асинхронный ли это запрос*//*

    request.onload = function () {

        if (request.status >= 200 && request.status < 400) {
            let resp = request.responseText;*//*сюда пришел весь HTML*//*
            commentsContainer.innerHTML = resp;*//*подставляем пришедший HTML в блок*//*

           

            if (!resp.includes('card-text')) {
                let comm = document.getElementById('comments-container');
                comm.innerHTML = '<h3>Эту нововсть никто не комментировал (: </h3>' + resp;
            *//* comm.innerHTML = '<div>@await Html.PartialAsync("CreateCommentPartial")</div>';*/
            /*console.log(document.getElementById('create-comment-btn'));*//*
                document.getElementById('create-comment-btn').addEventListener("click", createComment);

            }
            else {
               *//* console.log(document.getElementById('create-comment-btn'));*//*
                commentsContainer.innerHTML = resp;*//*подставляем пришедший HTML в блок*//*
                document.getElementById('create-comment-btn').addEventListener("click", createComment);

            }
            document.getElementById('create-comment-btn').addEventListener("click", createComment);
        }
    }

    request.send();
    *//*после чего отправляем запрос*//*

    function createComment() {
        let commentText = document.getElementById('commentText').value;
        let newsId = document.getElementById('newsId').value;
        

        var postRequest = new XMLHttpRequest();
        postRequest.open("POST", '/Comments/Create', true);*//*начинаем читать*//*
        postRequest.setRequestHeader('Content-Type', 'application/json');
       

        postRequest.send(JSON.stringify({
            commentText: commentText,
            newsId: newsId
        }));

        postRequest.onload = function () {
            if (postRequest.status >= 200 && postRequest.status < 400) {
                document.getElementById('commentText').value = '';

                //commentsContainer.innerHTML += '';

                loadComments(newsId);
            }

    }


}






*/








