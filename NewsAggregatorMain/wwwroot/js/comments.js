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

let idFor;

function toggleComments(newsId) {
    idFor = newsId;
    if (commentsDisplaySwitcherElement != null) {
        if (isCommentsDisplayed == true) {
            commentsDisplaySwitcherElement.innerHTML = 'Display comments';
            document.getElementById('comments-container').innerHTML = '';
        } else {
            commentsDisplaySwitcherElement.innerHTML = 'Hide comments';
            let commentsContainer = document.getElementById('comments-container');
            loadComments(newsId, commentsContainer);
            let commentsArea = document.getElementById('comments-area');
            inputArea(commentsArea);
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



function deleteComment() {
    let commentId = document.getElementById('commentId').value;

    let request = new XMLHttpRequest();
    request.open('GET', `/Comments/DeleteComment?commentId=${commentId}`, true);
    request.onload = function () { }


    if (!confirm('Удалить комент?')) {
        return;

    }
    request.send();

    updateComments();
    updateComments();


}

function createComment() {

    let commentText = document.getElementById('commentText').value;
    let newsId = idFor; /*document.getElementById('idFor').value;*/

    var postRequest = new XMLHttpRequest();
    postRequest.open("POST", '/Comments/Create', true);
    postRequest.setRequestHeader('Content-Type', 'application/json');

    function isEmpty(commentText) {
        return (!commentText || 0 === commentText.length);
    }

    function isBlank(commentText) {
        return (!commentText || /^\s*$/.test(commentText));
    }

    String.prototype.isEmpty = function () {
        return (this.length === 0 || !this.trim());
    };

    if (isEmpty(commentText) == true) {
        alert('Коментарий пустой. Он не будет добавлен!');
        return;
    }

    if (isBlank == true) {
        alert('Коментарий пустой. Он не будет добавлен!');
        return;
    }

    if (String.prototype.isEmpty == true) {
        alert('Коментарий пустой. Он не будет добавлен!');
        return;
    }


    if (!commentText || commentText == " ") {
        alert('Коментарий пустой. Он не будет добавлен!');
        return;
    }

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


    alert('Коментарий успешно добавлен.');
    updateComments();
}


function inputArea(commentsArea) {
    let request = new XMLHttpRequest();
    request.open('GET', `/Comments/InputCommentArea`, true);
    request.onload = function () {
        let resp = request.responseText;
        commentsArea.innerHTML = resp;
    }
    request.send();
}


/*var getCommentsIntervalId = setInterval(function () {

    let commentsContainer = document.getElementById('comments-container');
    loadComments(idFor, commentsContainer);
}, 10000);
*/

function updateComments() {
    let commentsContainer = document.getElementById('comments-container');
    loadComments(idFor, commentsContainer);

}












