function Follow () {
    $.ajax({
        url: '/api/Relationship/Follow/' + ModelId,
        type: 'POST',
        contentType: "application/json;charset=utf-8",        
        success: function (data) {
            $.get('/ProfileActions/' + ModelId, function (data) {
                $("#ProfileActions").replaceWith(data);
            });
        }
    });
}

function Unfollow () {
    $.ajax({
        url: '/api/Relationship/Unfollow/' + ModelId,
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.get('/ProfileActions/' + ModelId, function (data) {
                $("#ProfileActions").replaceWith(data);
            });
        }
    });
}

function WallRefresh (Count,Offset, newPost) {
    $.ajax({
        url: '/Wall',
        type: 'GET',
        data: {userId:ModelId, count:Count, offset:Offset},
        success: function (data) {
            twemoji.size = '16x16';
            data = twemoji.parse(data);
            $('#Wall').replaceWith(data);
            if(newPost)
            {
                $('#Wall .WallPost:first-child').hide();
                $('#Wall .WallPost:first-child').show(1000);
            }
        }
    });
}

function WallPostDelete(postId){
    $.ajax({
        url: '/api/Wall',
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(postId),
        success: function (data) {
            WallRefresh(10,0);
        }
    });
}

function WallPostEdit(tPostId){
    var post = $('#post_' + tPostId) ;

    var content = $('#post_' + tPostId + ' .WallPostText').clone();
    content.find("img.emoji").each(function () {
        this.replaceWith(this.alt);
    });
    var postText = content.html().trim();

    var t = $('<table></table>').attr('id', 'EditPostWindow');
    var ta = $('<textarea></textarea>').attr('id', 'EditPostWindowTextArea').addClass('TextArea100Px').html(postText);
    var td1 = $('<td></td>');
    var tr1 = $('<tr></tr>');
    var td2 = $('<td></td>').attr('align','right');
    var tr2 = $('<tr></tr>');
    var btn = $('<button></button>').attr('id', 'EditPostWindowButton').addClass('PurpleButton').html('Save');
    
    td1.append(ta);
    tr1.append(td1);
    t.append(tr1);
    td2.append(btn);
    tr2.append(td2);
    t.append(tr2);

    post.removeClass('myShadow');
    post.html(t);

    $(post).find('#EditPostWindowButton').click(function () {
        var p = {
            Id: tPostId,
            Text: $('#EditPostWindowTextArea').val().trim()};
        $.ajax({
            url: '/api/Wall',
            type: 'PUT',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(p),
            success: function (data) {
                WallRefresh(10,0);
            }
        });
    });
}

function WallPostCreate () {
    var p = {
        OwnerId: ModelId,
        Text: $('#CreatePostWindowTextArea').val()};
    $.ajax({
        url: '/api/Wall',
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(p),
        success: function (data) {
            WallRefresh(10,0,true);
        }
    });
}

function ShowPostCreationWindow () {
    $('#CreatePostWindow').show();
    $('#CreatePostButton').hide();
}        