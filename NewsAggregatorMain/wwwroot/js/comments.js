

let commentDispleySwitherElement = document.getElementById('comments-display-switcher');

commentDispleySwitherElement.onmouseover = function () {
    commentDispleySwitherElement.className = commentDispleySwitherElement.className.replace('btn-primary', 'btn-info');
}

commentDispleySwitherElement.onmouseout = function () {
    commentDispleySwitherElement.className = commentDispleySwitherElement.className.replace('btn-info', 'btn-primary');
}

commentDispleySwitherElement.onmousedown = function () {
    commentDispleySwitherElement.className = commentDispleySwitherElement.className.replace('btn-info', 'btn-secondary');
}

commentDispleySwitherElement.onmouseup = function () {
    commentDispleySwitherElement.className = commentDispleySwitherElement.className.replace('btn-secondary','btn-info' );
}

let isCommentsDispley = false;

function loadComments() {
    if (isCommentsDispley==true) {
        commentDispleySwitherElement.innerHTML = 'Display comments';
        document.getElementById('comments-container').innerHTML='';
    }
    else {
        commentDispleySwitherElement.innerHTML = 'Hide comments';
        let comments = document.getElementById('comments-container');
        for (var i = 0; i < 10; i++) {

        }

        let aa = document.contains. innerHTML.comments;
        let aaa= document.innerHTML.comments;

    }
    isCommentsDispley = !isCommentsDispley;
}